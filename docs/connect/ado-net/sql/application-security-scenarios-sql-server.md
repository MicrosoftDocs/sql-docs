---
title: Security Best Practices for Microsoft.Data.SqlClient
description: Protect SqlClient applications with validated TLS, passwordless authentication, least privilege, parameterized commands, and safe secret handling.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, paulmedynski, cmalhotra
ms.date: 09/21/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
ms.custom: sfi-ropc-nochange
ai-usage: ai-assisted
---
# Security best practices for Microsoft.Data.SqlClient

<a id="application-security-scenarios-in-sql-server"></a>

[!INCLUDE [Driver_ADONET_Download](../../../includes/driver_adonet_download.md)]

Protect a database application at the connection, identity, query, and deployment layers. A successful authentication doesn't prove that the connection uses Transport Layer Security (TLS) to validate the server or that the identity has only the permissions the application needs.

## Security checklist

| Area | Action |
| --- | --- |
| Network encryption. | Use `Encrypt=true` or `Encrypt=Strict` on compatible endpoints, and keep `TrustServerCertificate=false` for remote production connections. |
| Authentication. | Prefer Windows integrated authentication, managed identity, or workload identity when the target and host support them. |
| Authorization. | Grant only the required database permissions. Use separate identities for schema deployment and application requests. |
| Query construction. | Parameterize data values. Select identifiers that can't be parameters from an application-controlled allow list. |
| Connection configuration. | Use `SqlConnectionStringBuilder` and trusted endpoint configuration. Don't concatenate user-controlled connection-string fragments. |
| Secrets. | Use a protected secret store or host identity. Never commit passwords, private keys, or bearer tokens. |
| Pooling. | Keep identity selection consistent with the pool key, especially for token callbacks. |
| Errors and diagnostics. | Return safe application errors and retain restricted diagnostic detail without credentials or tokens. |
| Maintenance. | Update supported driver and identity-library dependencies, and exercise connection and authentication behavior after upgrades. |

See [Encryption and certificate validation](../encryption-and-certificate-validation.md) before changing settings in response to a certificate error. `TrustServerCertificate=true` removes an identity check; it doesn't fix a certificate deployment.

## Common threats

Review how untrusted input reaches database commands and connection settings, which identity executes each operation, and what diagnostic information leaves your service.

### SQL injection

SQL injection occurs when an application treats untrusted input as executable Transact-SQL (T-SQL) instead of data. Use command parameters for values, with explicit types and appropriate sizes. Validate input for business rules as well.

Stored procedures don't prevent injection if they concatenate untrusted input into dynamic SQL. Parameterize dynamic statements with `sp_executesql` where appropriate. If a table name, column name, or sort direction must vary, select it from an application-controlled allow list; ordinary value parameters can't replace SQL identifiers or syntax.

Don't rely on quote escaping, a connection-string builder, or an object-relational mapper alone to make arbitrary raw SQL safe. See [Commands and parameters](../commands-parameters.md) and [Write secure dynamic SQL](writing-secure-dynamic-sql.md).

### Elevation of privilege

Run application requests under a least-privileged database identity. Don't grant server administrator or database-owner permissions just to make an application error disappear.

Separate schema migration credentials from runtime credentials. Grant access through roles appropriate to the required tables or procedures. Use certificate signing or carefully scoped impersonation only when the operation needs permissions beyond those of the caller, and review that boundary separately.

Authentication isn't application authorization. If a service connects as one database identity for many users, it still must enforce which records and operations each user can access.

### Probing and intelligent observation

Don't return raw database exception details, connection strings, or stack traces to untrusted clients. Return an application-level error and a correlation identifier when appropriate.

Keep diagnostic records in a restricted logging system. Record information such as the operation, SqlClient error number, retry attempt, and correlation identifier. Review exception text and tracing configuration for query text, parameter values, endpoints, or personal data before collecting or exporting them.

Handle errors explicitly. Don't convert a failed query or authentication attempt into a success response with empty data. A certificate failure, invalid credential, or denied permission requires correction rather than unlimited retries.

### Authentication

Use an identity that's appropriate for the application host. Windows authentication doesn't require storing a SQL password. Azure-hosted workloads can use managed identity where supported; federated hosts can use workload identity. Interactive authentication is for users, not unattended services.

Use <xref:Microsoft.Data.SqlClient.SqlConnectionStringBuilder> to assign connection-string values. It quotes and escapes values correctly, but it doesn't authorize destinations. Keep the server, database, authentication mode, and TLS policy under application or administrator control.

Don't let an untrusted request choose `TrustServerCertificate`, `Encrypt`, `Authentication`, or an arbitrary server address. For multitenant applications, resolve authorized tenant configuration rather than accepting complete connection strings from requests.

See [SQL Server and Windows authentication](authentication-sql-server.md) and [Microsoft Entra authentication](azure-active-directory-authentication.md).

### Passwords

When SQL authentication or a client-secret credential is necessary, retrieve the secret from protected configuration or a secret store. Restrict read access, rotate the secret, and remove old credentials after deployment.

Environment variables can carry injected secrets, but they aren't a secret store and can be exposed by process diagnostics or deployment tooling. Don't print them or include them in crash reports. Keep development secrets out of source control.

Leave `Persist Security Info=false`, the default. This setting limits exposure through an opened connection's connection-string property; it doesn't erase the original configuration string or protect a secret you logged.

## Access tokens and connection pooling

A connection pool reuses authenticated physical connections. Keep each pool associated with the intended security context.

- A literal `AccessToken` participates in the pool key. SqlClient can't renew it or infer its expiry from that property. Manage token lifetime and clear affected pools as needed.
- An `AccessTokenCallback` participates in the pool key. Reuse a stable delegate and credential instance for connections that should share a pool.
- Return the same identity for the same callback inputs. Don't read the current web user's identity inside a shared callback unless the pool inputs consistently isolate that identity.
- Never log access tokens, client secrets, certificate private keys, or complete credential-bearing connection strings.

Use the [token callback guidance](azure-active-directory-authentication.md#using-accesstokencallback) to implement acquisition and renewal. A token callback doesn't customize TLS certificate validation.

## Deployment and service boundaries

Use supported releases of Microsoft.Data.SqlClient and the authentication library. Keep the core driver and Azure extension versions aligned when you use driver-provided Microsoft Entra modes. Test upgrades against your server's certificate configuration and actual authentication flow, not only connection-string parsing.

Keep databases off public networks where possible, and restrict access to the application's required endpoints. TLS protects traffic in transit; it doesn't enforce database permissions or encrypt database files on disk.

LocalDB is a Windows development tool. Protect its files and don't use a LocalDB-only run as evidence for remote TLS or Microsoft Entra behavior.

Check the destination's capabilities before adopting a SQL Server feature. SQL database in Microsoft Fabric requires Microsoft Entra identities and doesn't support SQL logins, application roles, or Always Encrypted tables. See [Fabric SQL database limitations](/fabric/database/sql/limitations).

## In this section

[Write secure dynamic SQL](writing-secure-dynamic-sql.md) explains how to parameterize dynamic statements and review execution permissions.

## Related content

- [SQL Server security](sql-server-security.md)
- [Encryption and certificate validation](../encryption-and-certificate-validation.md)
- [SQL Server connection pooling](../sql-server-connection-pooling.md)
- [High availability and disaster recovery](sqlclient-support-high-availability-disaster-recovery.md)
