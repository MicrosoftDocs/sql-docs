---
title: Connection options for Microsoft.Data.SqlClient
description: Configure Microsoft.Data.SqlClient connection timeouts, routing, pooling, retry, network, certificate, and server identity options.
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

# Connection options for Microsoft.Data.SqlClient

Microsoft.Data.SqlClient connection options control how the driver establishes, identifies, routes, retries, and pools a connection. Set them in a connection string or through matching <xref:Microsoft.Data.SqlClient.SqlConnectionStringBuilder> properties.

For Microsoft Entra ID authentication, see [Microsoft Entra ID authentication](sql/azure-active-directory-authentication.md). For TLS settings, see [Encryption and certificate validation](encryption-and-certificate-validation.md).

## Set options with SqlConnectionStringBuilder

Use the builder instead of concatenating connection string fragments:

```csharp
var builder = new SqlConnectionStringBuilder
{
    DataSource = "tcp:sql.example.com,1433",
    InitialCatalog = "Orders",
    IntegratedSecurity = true,
    Encrypt = SqlConnectionEncryptOption.Mandatory,
    ApplicationName = "Orders.Worker",
    ConnectTimeout = 30,
    ConnectRetryCount = 3,
    ConnectRetryInterval = 10,
    MultiSubnetFailover = true,
};
```

The code uses builder property names. The tables use common connection string spellings. The driver also accepts documented aliases.

## Timeout options

| Keyword | Default | Behavior | Version |
| --- | --- | --- | --- |
| `Connect Timeout` | 15 seconds | Limits the time to establish a connection. When the pool is at `Max Pool Size`, it also bounds the wait for a usable pooled connection. `Connection Timeout` and `Timeout` are aliases. | All Microsoft.Data.SqlClient versions |
| `Command Timeout` | 30 seconds | Sets the default timeout for commands associated with the connection. Set <xref:Microsoft.Data.SqlClient.SqlCommand.CommandTimeout%2A> on a command when one operation needs a different limit. A value of `0` has no time limit and can leave work waiting indefinitely. | Microsoft.Data.SqlClient 2.1 and later versions |

Connection and command timeouts measure different work. `Connect Timeout` doesn't limit query execution. `Command Timeout` doesn't limit authentication or the wait for a pooled connection.

A <xref:System.Threading.CancellationToken> is separate from both settings. Pass it to `OpenAsync`, command execution, and reader methods so the caller can stop waiting before a timeout expires.

## Workload identity and routing options

| Keyword | Default | Behavior | Version |
| --- | --- | --- | --- |
| `Application Name` | Provider-defined name | Identifies the workload in SQL Server sessions, auditing, and diagnostics. Use one stable, low-cardinality name for each deployed workload. | All Microsoft.Data.SqlClient versions |
| `Application Intent` | `ReadWrite` | `ReadOnly` requests read-intent routing when the target and availability group are configured for it. It doesn't make SQL statements read-only. | All Microsoft.Data.SqlClient versions |

`Application Intent=ReadOnly` normally pairs with an availability group listener or a service endpoint that supports read routing. See [High availability and disaster recovery](sql/sqlclient-support-high-availability-disaster-recovery.md).

## Network and packet options

| Keyword | Default | Behavior | Version |
| --- | --- | --- | --- |
| `Packet Size` | 8,000 bytes | Sets the Tabular Data Stream (TDS) network packet size. Supported values are 512 through 32,768 bytes. Keep the default unless workload measurements and server configuration justify a change. | All Microsoft.Data.SqlClient versions |
| `MultiSubnetFailover` | `false` | Uses parallel TCP connection attempts to IP addresses returned for a multi-address endpoint. Set it to `true` for Azure SQL endpoints, availability group listeners, and failover cluster instances reached over TCP. | All Microsoft.Data.SqlClient versions |

`MultiSubnetFailover=true` isn't supported with named instances, non-TCP protocols, database mirroring, or endpoints configured with more than 64 IP addresses. It's safe for a single-IP TCP endpoint.

Microsoft.Data.SqlClient 7.0 also has a process-wide AppContext switch that can make every connection behave as if `MultiSubnetFailover=true`. The connection string default remains `false` when that switch isn't enabled. See [AppContext switches in SqlClient](appcontext-switches.md).

## Pooling options

| Keyword | Default | Behavior | Version |
| --- | --- | --- | --- |
| `Pooling` | `true` | Reuses physical connections for matching connection configurations. Disable it only for diagnosis or a measured workload that can't pool safely. | All Microsoft.Data.SqlClient versions |
| `Min Pool Size` | `0` | Keeps at least this many physical connections in a pool after the pool is created. A positive value can keep database sessions open until the pool or process ends. | All Microsoft.Data.SqlClient versions |
| `Max Pool Size` | `100` | Caps physical connections in one pool. Requests wait up to `Connect Timeout` when the pool is full. | All Microsoft.Data.SqlClient versions |
| `Load Balance Timeout` | `0` seconds | Discards a connection when it returns to the pool if its age exceeds this value. `Connection Lifetime` is an alias. `0` disables age-based removal. | All Microsoft.Data.SqlClient versions |
| `Pool Blocking Period` | `Auto` | Controls whether the pool temporarily rethrows a cached login failure. `Auto` disables the blocking period for recognized Azure SQL endpoints and enables it for other endpoints. | All Microsoft.Data.SqlClient versions |
| `Enlist` | `true` | Automatically enlists an opened connection in the ambient `System.Transactions` transaction. | All Microsoft.Data.SqlClient versions |

Pool settings apply to each distinct pool, not to the entire process or database server. Before raising `Max Pool Size`, confirm that connections and readers are disposed promptly and that the database can accept the resulting total across every application instance.

For pool keys, token behavior, blocking periods, clearing, and diagnostics, see [SQL Server connection pooling](sql-server-connection-pooling.md).

## Connection recovery options

| Keyword | Default | Behavior | Version |
| --- | --- | --- | --- |
| `Connect Retry Count` | `1` | Sets the retry count for qualifying transient failures during initial connection and for restoring a broken idle connection. The effective default is `2` for recognized Azure SQL endpoints and `5` for recognized Azure Synapse and on-demand endpoints. `0` disables these retries. | All Microsoft.Data.SqlClient versions |
| `Connect Retry Interval` | 10 seconds | Sets the delay before later initial connection or idle recovery attempts. Valid values are 1 through 60 seconds. | All Microsoft.Data.SqlClient versions |

The first retry during connection recovery is immediate. `Connect Retry Interval` applies before later attempts. To bypass built-in initial-open retry for one operation, use an open overload with <xref:Microsoft.Data.SqlClient.SqlConnectionOverrides.OpenWithoutRetry>.

These keywords don't retry a command that fails while it's running. Use [configurable retry logic](configurable-retry-logic.md) for a custom open or command policy. Retry commands only when repeating their effects is safe.

## Server identity and certificate options

These options solve specific certificate or Kerberos naming requirements. They don't replace normal authentication and certificate validation.

| Keyword | Default | Behavior | Version |
| --- | --- | --- | --- |
| `Host Name In Certificate` | Server host name | Supplies the expected Common Name (CN) or Subject Alternative Name (SAN) when the connection uses a DNS alias that differs from the certificate. | Microsoft.Data.SqlClient 5.0 and later versions |
| `Server Certificate` | Empty | Supplies a PEM, DER, or CER file that must exactly match the server certificate when `Encrypt=Mandatory` or `Encrypt=Strict`. | Microsoft.Data.SqlClient 5.1 and later versions |
| `Server SPN` | Derived from the server name | Overrides the Service Principal Name (SPN) used for integrated authentication to the primary server. Configure it only when the deployed Kerberos naming requires an explicit SPN. | Microsoft.Data.SqlClient 5.0 and later versions |
| `Failover Partner SPN` | Derived from the failover partner | Overrides the SPN for a database mirroring failover partner. Database mirroring is deprecated. Use availability groups for new deployments. | Microsoft.Data.SqlClient 5.0 and later versions |

`Host Name In Certificate` changes the name used for certificate matching. It doesn't trust an untrusted issuer. `Server Certificate` pins an exact certificate file and requires an application update when that certificate rotates.

Incorrect SPN overrides can prevent Kerberos authentication or weaken the intended identity check. Fix DNS and SPN registration instead of setting overrides when possible.

Keep connection strings small. Add an option only when you can state which behavior it changes and how the workload verifies that behavior.

## Review option changes

Before changing an option in production:

1. Record the current connection string, driver version, endpoint type, and observed problem.
1. Change one behavior at a time.
1. Test connection establishment, authentication, certificate validation, pooling, failover, cancellation, and query execution.
1. Measure hard connects, pool waits, connection latency, and error numbers.
1. Confirm the setting on every deployed instance.

Connection strings are part of the pool key. A staged rollout can temporarily create both old and new pools, which increases the total number of physical database connections.

## Related content

- [Connection strings](connection-strings.md)
- [Connect to a data source](connecting-to-data-source.md)
- [SQL Server connection pooling](sql-server-connection-pooling.md)
- [Encryption and certificate validation](encryption-and-certificate-validation.md)
- [High availability and disaster recovery](sql/sqlclient-support-high-availability-disaster-recovery.md)
- <xref:Microsoft.Data.SqlClient.SqlConnectionStringBuilder>
