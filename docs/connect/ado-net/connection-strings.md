---
title: Connection strings for Microsoft.Data.SqlClient
description: Choose, build, store, and troubleshoot Microsoft.Data.SqlClient connection strings for SQL Server and Azure SQL.
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

# Connection strings for Microsoft.Data.SqlClient

A Microsoft.Data.SqlClient connection string tells the driver which SQL Server-compatible endpoint and database to use, how to authenticate, and how to configure the connection. Pass it to <xref:Microsoft.Data.SqlClient.SqlConnection> or <xref:Microsoft.Data.SqlClient.SqlConnectionStringBuilder>.

Start with four decisions:

1. Which server and database does the application use?
1. Which identity does the application run as?
1. How does the client validate the server certificate?
1. Which connection behavior does the workload need?

Keep credentials and access tokens out of the connection string when the chosen authentication method supports that design.

## Choose an authentication pattern

Use the narrowest pattern that fits the deployment.

| Environment | Preferred pattern | Core connection string |
| --- | --- | --- |
| SQL Server on Windows under a domain or local Windows identity | Windows integrated authentication | `Server=<server>;Database=<database>;Integrated Security=true;Encrypt=true` |
| Developer workstation connecting to SQL database in Microsoft Fabric | Microsoft Entra ID default credential chain | `Server=tcp:<server>,1433;Database=<database>;Authentication=Active Directory Default;Encrypt=Strict` |
| Application hosted in Azure and connecting to Azure SQL | Microsoft Entra ID managed identity | `Server=tcp:<server>.database.windows.net,1433;Database=<database>;Authentication=Active Directory Managed Identity;Encrypt=Strict` |
| Developer workstation connecting to Azure SQL | Microsoft Entra ID default credential chain | `Server=tcp:<server>.database.windows.net,1433;Database=<database>;Authentication=Active Directory Default;Encrypt=Strict` |
| Interactive desktop tool connecting to Azure SQL | Microsoft Entra ID interactive authentication | `Server=tcp:<server>.database.windows.net,1433;Database=<database>;Authentication=Active Directory Interactive;Encrypt=Strict` |
| Environment that requires SQL authentication | User name and password from a secret store | `Server=<server>;Database=<database>;User ID=<user_id>;Password=<password>;Encrypt=true` |

Microsoft.Data.SqlClient 7.0 and later versions require the version-matched `Microsoft.Data.SqlClient.Extensions.Azure` package for driver-provided Microsoft Entra ID authentication modes. You don't need that extension when application code supplies an access token or access token callback.

Authentication also requires database-side users, permissions, and identity configuration. For the complete choice matrix and setup, see [Microsoft Entra ID authentication](sql/azure-active-directory-authentication.md) and [SQL Server authentication](sql/authentication-sql-server.md).

## Specify the server and database

Use `Server` and `Database` as the canonical keyword names. The driver also accepts aliases such as `Data Source` for `Server` and `Initial Catalog` for `Database`.

Common server forms include:

```text
Server=server-name
Server=server-name\instance-name
Server=tcp:server-name,1433
Server=(localdb)\MSSQLLocalDB
```

Prefer an explicit protocol, host name, and port for production TCP connections. Use a stable DNS name that matches the server certificate instead of an IP address.

For an availability group listener, failover group, Azure SQL endpoint, or other multi-address TCP endpoint, also review `MultiSubnetFailover` in [Connection options](connection-options.md).

## Configure encryption and certificate validation

Microsoft.Data.SqlClient 4.0 and later versions default `Encrypt` to `true`. Microsoft.Data.SqlClient 5.0 and later versions also support `Encrypt=Strict` for servers that negotiate TDS 8.0.

Use:

- `Encrypt=Strict` when the server supports TDS 8.0 and has a certificate the client can validate.
- `Encrypt=true` for encrypted connections to other supported servers.
- `TrustServerCertificate=false`, the default, for production certificate validation.

Don't use `TrustServerCertificate=true` as a general connection fix. It encrypts the channel but skips server identity validation. Limit it to controlled development environments where a trusted certificate isn't available.

For server requirements, version behavior, and certificate options, see [Encryption and certificate validation](encryption-and-certificate-validation.md).

## Understand connection string syntax

A connection string is a semicolon-delimited list of keyword and value pairs:

```text
Server=tcp:sql.example.com,1433;Database=Orders;Integrated Security=true;Encrypt=true
```

Follow these rules:

- Keyword names aren't case-sensitive.
- Values can be case-sensitive.
- A final semicolon is optional.
- Quote a value with single or double quotation marks when it contains a semicolon or leading or trailing whitespace.
- Escape the quote that encloses a value by doubling it.
- Don't use duplicate keywords. The parser uses the last value, which makes the effective configuration hard to review.

The set of accepted keywords and aliases belongs to the provider. A connection string accepted by `Microsoft.Data.SqlClient` might not work with `System.Data.SqlClient` or another data provider.

## Build connection strings safely

Use <xref:Microsoft.Data.SqlClient.SqlConnectionStringBuilder> when code needs to add, validate, or replace values. Don't concatenate untrusted values into a connection string.

```csharp
string baseConnectionString =
    configuration.GetConnectionString("Orders")
    ?? throw new InvalidOperationException(
        "Connection string 'Orders' wasn't configured.");

var builder = new SqlConnectionStringBuilder(baseConnectionString)
{
    ApplicationName = "Orders.Api",
    ConnectTimeout = 30,
};

string connectionString = builder.ConnectionString;
```

The builder:

- Rejects unsupported keywords and invalid values.
- Maps aliases to canonical properties.
- Quotes values when required.
- Prevents a value from injecting another keyword.

The builder doesn't protect a password or token after it enters process memory. It also doesn't decide whether a server, identity, or certificate setting is safe.

## Store connection information outside code

Load connection strings from the configuration system used by the application. Current .NET applications commonly use environment variables, user secrets for local development, Azure App Configuration, and Azure Key Vault-backed configuration.

Keep these rules:

- Don't commit passwords, client secrets, access tokens, or production connection strings.
- Prefer an identity-based authentication method that doesn't require a password in the connection string.
- Restrict access to the configuration source.
- Rotate stored secrets and restart or refresh applications that cache them.
- Don't write connection strings to logs, exceptions, traces, or telemetry.
- Leave `Persist Security Info=false`, the default, so an opened connection doesn't expose security-sensitive values through its connection string.

For .NET configuration providers, see [Configuration in .NET](/dotnet/core/extensions/configuration). For additional controls, see [Protect connection information](protecting-connection-information.md).

## Keep pool keys stable

Connection pooling uses an exact connection configuration as part of its pool key. Equivalent strings can create separate pools when their text differs, including when keywords appear in a different order.

Build one canonical connection string at application startup and reuse it. Don't add request IDs, user names, access tokens, or other per-request values to the string. For the complete key rules, see [SQL Server connection pooling](sql-server-connection-pooling.md).

## Separate connection and command settings

A connection string controls connection establishment and session behavior. A command controls one SQL operation.

| Requirement | Configure on |
| --- | --- |
| Time allowed to establish a connection or obtain one from the pool | `Connect Timeout` connection option |
| Default command execution timeout | `Command Timeout` connection option, when supported by the driver version |
| Timeout for one command | <xref:Microsoft.Data.SqlClient.SqlCommand.CommandTimeout%2A> |
| Cancellation from the caller | `CancellationToken` passed to asynchronous APIs |
| Retry policy for opening a connection or executing a command | Configurable retry logic on `SqlConnection` or `SqlCommand` |

Don't treat a longer timeout as retry logic. A timeout limits one wait. A retry starts another attempt and must be bounded and safe to repeat.

## Review version-sensitive behavior

| Driver version | Connection string change |
| --- | --- |
| 4.0 | `Encrypt` defaults to `true`. |
| 5.0 | `Encrypt=Strict` and `HostNameInCertificate` are available. `SqlConnectionStringBuilder.Encrypt` uses `SqlConnectionEncryptOption`. |
| 5.1 | `ServerCertificate` can match the server certificate against a file. |
| 5.2 | <xref:Microsoft.Data.SqlClient.SqlConnection.AccessTokenCallback%2A> is available for renewable application-supplied tokens. |
| 7.0 | Driver-provided Microsoft Entra ID authentication moves to `Microsoft.Data.SqlClient.Extensions.Azure`. |
| 7.0.2 | The core driver and its companion packages use aligned versions. |

Use a supported stable driver version and read the release notes before an update. For current versions, see [SqlClient driver support lifecycle](sqlclient-driver-support-lifecycle.md).

## Related content

- [Use Microsoft.Data.SqlClient in a .NET app](get-started-sqlclient-driver.md)
- [Connection options](connection-options.md).
- [Connect to a data source](connecting-to-data-source.md)
- [Microsoft Entra ID authentication](sql/azure-active-directory-authentication.md)
- [Encryption and certificate validation](encryption-and-certificate-validation.md)
- [Microsoft.Data.SqlClient API reference](/dotnet/api/microsoft.data.sqlclient)
