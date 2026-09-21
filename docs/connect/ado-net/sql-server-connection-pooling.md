---
title: SQL Server connection pooling with Microsoft.Data.SqlClient
description: Configure and diagnose Microsoft.Data.SqlClient connection pooling for SQL Server and Azure SQL production workloads.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, davidengel, paulmedynski, cmalhotra
ms.date: 09/16/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
dev_langs:
  - csharp
ai-usage: ai-assisted
---

# SQL Server connection pooling with Microsoft.Data.SqlClient

Microsoft.Data.SqlClient connection pooling reuses authenticated physical connections. `SqlConnection.Open` or `OpenAsync` checks a pool for a usable connection. `Close`, `Dispose`, or `DisposeAsync` resets and returns it. This approach avoids a network connection, authentication, and session setup for every operation.

Pooling is enabled by default. Use this application pattern:

```csharp
await using var connection = new SqlConnection(connectionString);
await connection.OpenAsync(cancellationToken);

using var command = new SqlCommand(sql, connection);
await command.ExecuteNonQueryAsync(cancellationToken);
```

Open late, dispose early, and let the pool manage physical connections. Don't keep one `SqlConnection` open globally.

## Understand pool keys

A connection can be reused only from its matching pool. The pool key includes more than the destination server.

| Input | Pool behavior |
| --- | --- |
| Connection string | The text must match exactly. Keyword order differences create separate pools, even when the effective settings are equivalent. |
| Windows integrated authentication | The Windows identity is part of the key. The same string used under different identities creates different pools. |
| `SqlCredential` | The object instance is part of the key. Separate instances create separate pools even when they contain the same user name and password. |
| `SqlConnection.AccessToken` | The access token value is part of the key. Replacing token strings can create new pools and leave connections authenticated with old tokens in existing pools. |
| `SqlConnection.AccessTokenCallback` | The callback is part of the key. Reuse the same callback instance for connections that should share a pool. The returned token value isn't the pool key. |
| Custom SSPI context provider | The provider instance participates in the connection configuration. Reuse one provider instance for connections that should pool together. |
| Ambient transaction | Enlisted connections use transaction-specific subdivisions inside the matching pool. |

The database, authentication mode, encryption options, application name, pooling options, and every other connection string value contribute through the exact string.

Build one canonical connection string and reuse it. Avoid per-request values in `Application Name`, `Workstation ID`, or other keywords.

## Choose token APIs that can pool

For Microsoft Entra ID access tokens, use an authentication mode supplied by Microsoft.Data.SqlClient or a stable <xref:Microsoft.Data.SqlClient.SqlConnection.AccessTokenCallback%2A>.

`AccessTokenCallback` was introduced in Microsoft.Data.SqlClient 5.2. The driver calls it when it needs a token and can request a refreshed token for a reused pool. Keep the callback deterministic for the authentication parameters that the driver supplies, and reuse the same delegate instance.

When code sets <xref:Microsoft.Data.SqlClient.SqlConnection.AccessToken%2A> directly:

- The token string becomes part of the pool key.
- The application owns token expiration and refresh.
- A pooled physical connection can outlive the token used to create it.
- Call <xref:Microsoft.Data.SqlClient.SqlConnection.ClearPool%2A> after replacing an expired token if that pool can no longer be used safely.

Don't create a new callback lambda or credential object for every request. Object identity differences can fragment the pools.

Microsoft.Data.SqlClient 7.0 adds <xref:Microsoft.Data.SqlClient.SqlConnection.SspiContextProvider%2A> for custom Kerberos or NTLM negotiation. Treat the provider as application-scoped connection configuration, not per-request state.

## Size each pool

These connection string options control one pool:

| Keyword | Default | Effect |
| --- | --- | --- |
| `Pooling` | `true` | Enables or disables pooling. |
| `Min Pool Size` | `0` | Sets the minimum number of physical connections the pool retains after it's created. |
| `Max Pool Size` | `100` | Sets the maximum number of physical connections in the pool. |
| `Connect Timeout` | 15 seconds | Sets how long `Open` waits when no usable connection is available. |
| `Load Balance Timeout` | `0` seconds | Discards a connection when it returns to the pool if its age exceeds the configured value. `Connection Lifetime` is an alias. |

The pool creates connections as demand grows until it reaches `Max Pool Size`. When all connections are in use, later opens wait for a connection to return. If the wait exceeds `Connect Timeout`, the open fails.

Don't raise `Max Pool Size` before checking:

- Every connection and reader is disposed on every path.
- Commands and transactions finish promptly.
- The query workload isn't blocked or saturated.
- The database connection limit can handle `Max Pool Size` multiplied by every pool in every application instance.

A positive `Min Pool Size` keeps connections open during idle periods. Use it only when measurements justify warm connections. It usually works against scale-to-zero, serverless auto-pause, and burstable cloud designs.

With the default `Load Balance Timeout=0`, periodic cleanup normally removes unused connections above `Min Pool Size` after about four to eight minutes, or the pool removes them when it detects that the server connection is broken. Treat that interval as implementation behavior, not a per-connection idle guarantee. The pool doesn't send a validation query before every checkout because that round trip removes much of the pooling benefit.

## Handle authentication blocking periods

After an authentication timeout or other authentication failure, the pool can enter a blocking period. During that period, matching open attempts rethrow the original exception without making another authentication attempt.

The first blocking period is five seconds. After another failure, the period doubles up to one minute.

`Pool Blocking Period` controls this behavior:

| Value | Behavior |
| --- | --- |
| `Auto` | Enables blocking for ordinary SQL Server endpoints and disables it for recognized Azure SQL endpoint suffixes. A vanity DNS name might not receive the Azure behavior. |
| `AlwaysBlock` | Enables the blocking period for every endpoint. |
| `NeverBlock` | Disables the blocking period. |

Keep `Auto` unless the application's measured retry design requires a different choice. Disabling the blocking period can turn a credential, firewall, or outage problem into an authentication storm.

The blocking period is separate from configurable retry logic. A retry provider that opens the same pool during a blocking period receives the cached exception.

## Manage connection lifetime and clearing

The pool automatically clears the affected pool when it recognizes a fatal error, such as a failover. The pool closes idle connections and discards checked-out connections when they return.

Use the clearing APIs for a known configuration or credential boundary:

- <xref:Microsoft.Data.SqlClient.SqlConnection.ClearPool%2A> clears the pool associated with one `SqlConnection` configuration.
- <xref:Microsoft.Data.SqlClient.SqlConnection.ClearAllPools%2A> clears every Microsoft.Data.SqlClient pool in the process or application domain.

The pool closes idle connections in a cleared pool. The pool marks connections that are currently in use so it discards them when returned.

Clearing pools causes later opens to perform physical logins. Don't use it as periodic maintenance, a general error handler, or a substitute for disposing connections.

`Load Balance Timeout` provides gradual age-based turnover. Use it when a deployment or clustered service needs old physical connections to leave over time. Confirm that the chosen value doesn't cause excessive hard connects.

## Understand transactions

With `Enlist=true`, the default, a connection opened inside `System.Transactions.Transaction.Current` automatically enlists in that transaction.

When an enlisted connection closes, the pool places it in a transaction-specific subdivision. A later open under the same transaction can reuse it. The physical connection doesn't return to the general pool until the transaction completes.

Long or abandoned ambient transactions can therefore:

- Hold physical connections out of the general pool.
- Consume pool capacity after the logical connection closes.
- Keep server locks and transaction state alive.

Keep transactions bounded, complete them explicitly, and monitor stasis connections. Set `Enlist=false` only when the connection must remain outside an ambient transaction.

## Prevent pool fragmentation

Pool fragmentation creates many small pools instead of a few reusable pools. Common causes include:

- Connection string keyword order or alias differences.
- One connection string per customer, user, request, or database.
- Integrated authentication under many Windows identities.
- New `SqlCredential`, access token callback, or SSPI provider instances per request.
- Direct access tokens that change on each refresh.
- High-cardinality application names or workstation IDs.

Normalize connection strings with `SqlConnectionStringBuilder` and centralize connection creation.

If the application intentionally connects to many databases or identities, include the resulting pool count in capacity planning. Don't run `USE` with an untrusted database name to collapse pools. Database isolation, permissions, session state, and pool reset behavior must remain explicit.

## Account for application roles and session state

The pool resets reusable SQL Server session state before assigning a physical connection to another logical connection. Application code should still set any required session state within its unit of work.

SQL Server application roles activated with `sp_setapprole` can't be reset safely for ordinary pooling. Prefer database users, contained users, roles, row-level security, or another authorization design. If an application role is unavoidable, use a documented cookie-based reversal pattern or disable pooling for that isolated path after testing.

Dispose readers, finish or roll back transactions, and don't leave commands running when the connection closes. Don't rely on temporary tables or other session state surviving across logical connections.

## Use cloud-hosted pooling patterns

For Azure App Service, Azure Functions, containers, Kubernetes, and other horizontally scaled hosts:

- Calculate the possible database connections across all instances, processes, pool keys, and replicas.
- Use managed identity or a stable access token callback instead of rotating token strings in connection objects.
- Keep `Min Pool Size=0` unless a measured cold-start requirement justifies retained sessions.
- Expect a new instance to start with an empty pool.
- Keep connection strings identical across instances that serve the same workload.
- Bound connection attempts and retries to avoid synchronized login bursts during failover or scale-out.
- Set `MultiSubnetFailover=true` for Azure SQL and other supported multi-address TCP endpoints.

Connection pools are local to the application process. They aren't shared across application instances, containers, or hosts.

## Diagnose pool behavior

Use [SqlClient diagnostic counters](diagnostic-counters.md) to observe:

- Hard connects and disconnects, which represent physical server connections.
- Soft connects and disconnects, which represent pool checkout and return.
- Active and free pooled connections.
- Active pool groups and pools.
- Stasis connections.
- Reclaimed connections where application code didn't dispose the logical connection.

Correlate client counters with SQL Server sessions, waits, blocking, and resource limits. A pool timeout can mean a connection leak, slow queries, blocked transactions, too much concurrency, pool fragmentation, or a database capacity limit.

Use [event source tracing](enable-eventsource-tracing.md) for targeted pooler traces. Tracing is verbose. Enable it for a bounded diagnostic window and protect any captured connection metadata.

## Production checklist

- Keep pooling enabled.
- Reuse one canonical connection string per workload and database.
- Dispose connections, commands, readers, and transactions on every path.
- Reuse credential, token callback, and SSPI provider instances.
- Set finite connection and command timeouts.
- Size the total connection budget across every application instance.
- Monitor hard connects, pool counts, free connections, stasis, and timeouts.
- Clear pools only for a credential, token, or configuration boundary that the provider can't detect, or when diagnostics confirm that stale connections remain.
- Load test scale-out, failover, and credential refresh behavior before production.

## Related content

- [Connect to a data source](connecting-to-data-source.md)
- [Connection strings](connection-strings.md)
- [Connection options](connection-options.md).
- [SqlClient diagnostic counters](diagnostic-counters.md)
- [Configurable retry logic](configurable-retry-logic.md)
- [Microsoft Entra ID authentication](sql/azure-active-directory-authentication.md)
