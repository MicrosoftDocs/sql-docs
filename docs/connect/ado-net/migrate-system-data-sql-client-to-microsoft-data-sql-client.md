---
title: Migrate from System.Data.SqlClient to Microsoft.Data.SqlClient
description: Migrate a .NET application from System.Data.SqlClient to Microsoft.Data.SqlClient and validate namespace, encryption, parameter, configuration, and runtime changes.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest, davidengel, paulmedynski, cmalhotra
ms.date: 09/16/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
dev_langs:
  - csharp
ai-usage: ai-assisted
---

# Migrate from System.Data.SqlClient to Microsoft.Data.SqlClient

Microsoft.Data.SqlClient is the supported provider for new SQL Server features in .NET applications. It preserves the ADO.NET programming model used by `System.Data.SqlClient`, but the packages, namespaces, defaults, and some public types differ.

Treat the migration as a provider update, not only a namespace replacement.

## Plan the migration

Before changing code:

1. Record the versions of .NET, `System.Data.SqlClient`, SQL Server, and Microsoft SQL services that the application supports.
1. Inventory authentication modes, connection string keywords, custom certificates, Always Encrypted providers, `DbProviderFactories` configuration, SQL Server user-defined types, and `System.Data.SqlTypes` usage.
1. Run the application's current tests and save a baseline for connection, query, transaction, retry, and performance behavior.
1. Search direct and transitive package references:

   ```console
   dotnet list package --include-transitive
   ```

Migrate one application or shared data access library at a time. Don't pass provider-specific objects between code that still uses `System.Data.SqlClient` and code that uses `Microsoft.Data.SqlClient`.

## Replace the package

Remove an explicit `System.Data.SqlClient` package reference, if present:

```console
dotnet remove package System.Data.SqlClient
```

Add Microsoft.Data.SqlClient:

```console
dotnet add package Microsoft.Data.SqlClient
```

If Microsoft.Data.SqlClient 7.0 or later uses a driver-provided Microsoft Entra authentication mode, also add:

```console
dotnet add package Microsoft.Data.SqlClient.Extensions.Azure --version <same-version-as-Microsoft.Data.SqlClient>
```

For version and package selection, see [Install, update, and deploy Microsoft.Data.SqlClient](download-microsoft-sqlclient-data-provider.md).

## Update namespaces

Replace the primary provider namespace:

```diff
-using System.Data.SqlClient;
+using Microsoft.Data.SqlClient;
```

Update fully qualified names, aliases, generated code, dependency injection registrations, reflection strings, configuration, and test doubles that refer to `System.Data.SqlClient`.

Don't replace the general `System.Data` or `System.Data.Common` namespaces. `Microsoft.Data.SqlClient` continues to use ADO.NET types such as `CommandType`, `DbType`, `IsolationLevel`, `DataTable`, `DbConnection`, and `DbCommand` from those namespaces.

Some SQL Server-specific types move to other `Microsoft.Data` namespaces:

| Type | Previous namespace | Microsoft.Data.SqlClient namespace |
| --- | --- | --- |
| `SqlDataRecord`, `SqlMetaData` | `Microsoft.SqlServer.Server` | `Microsoft.Data.SqlClient.Server` |
| `SqlFileStream` | `System.Data.SqlTypes` | `Microsoft.Data.SqlTypes` |
| `SqlNotificationRequest` | `System.Data.Sql` | `Microsoft.Data.Sql` |
| `OperationAbortedException` | `System.Data` | `Microsoft.Data` |

In `Microsoft.Data.SqlClient` 5.0 and later, other SQL Server common language runtime (CLR) types remain in `Microsoft.SqlServer.Server`. Update each type from compiler errors and the [Microsoft.Data.SqlClient API reference](/dotnet/api/microsoft.data.sqlclient), rather than replacing the entire namespace.

## Update .NET Framework configuration

An application that resolves providers through `DbProviderFactories` might need a provider registration in `App.config` or `Web.config`:

```xml
<configuration>
  <system.data>
    <DbProviderFactories>
      <add name="SqlClient Data Provider"
           invariant="Microsoft.Data.SqlClient"
           description=".NET data provider for SQL Server"
           type="Microsoft.Data.SqlClient.SqlClientFactory, Microsoft.Data.SqlClient" />
    </DbProviderFactories>
  </system.data>
</configuration>
```

Update code that requests the provider invariant name:

```csharp
DbProviderFactory factory =
    DbProviderFactories.GetFactory("Microsoft.Data.SqlClient");
```

Don't add this configuration when the application creates `SqlConnection` directly and doesn't use `DbProviderFactories`.

## Review encryption and certificate validation

Microsoft.Data.SqlClient uses more secure defaults than System.Data.SqlClient.

| Behavior | System.Data.SqlClient | Microsoft.Data.SqlClient |
| --- | --- | --- |
| Default encryption | `Encrypt=false` | `Encrypt=true` starting with version 4.0 |
| Server certificate validation | Validates the certificate only when client encryption is enabled | Starting with version 2.0, validates the certificate according to `TrustServerCertificate` when the server forces encryption, even if `Encrypt=false` |
| Strict encryption | Not supported | `Encrypt=Strict` starting with version 5.0 for TDS 8.0-capable servers |
| `SqlConnectionStringBuilder.Encrypt` type | `bool` | `SqlConnectionEncryptOption` starting with version 5.0 |

Don't set `Encrypt=false` or `TrustServerCertificate=true` as a general migration fix. Configure a certificate that the client trusts and use a server name that matches the certificate. Use `TrustServerCertificate=true` only for controlled development environments where validation isn't possible.

The change to `SqlConnectionEncryptOption` is source-compatible in common assignments through implicit conversions, but it's a binary breaking change. Recompile every assembly that accesses `SqlConnectionStringBuilder.Encrypt`.

For details, see [Encryption and certificate validation](encryption-and-certificate-validation.md).

## Review connection strings

Microsoft.Data.SqlClient adds keywords and aliases that System.Data.SqlClient doesn't recognize. For example, it accepts aliases with spaces such as `Application Intent` and `Multi Subnet Failover`.

Don't build a connection string with `Microsoft.Data.SqlClient.SqlConnectionStringBuilder` and then pass it to `System.Data.SqlClient`. During a staged migration, keep each connection string builder paired with its provider.

Review authentication, encryption, retry, failover, and certificate keywords against [Connection string syntax](connection-string-syntax.md).

## Review parameter behavior

Test date and time parameters explicitly:

| Parameter | System.Data.SqlClient behavior | Microsoft.Data.SqlClient behavior |
| --- | --- | --- |
| `DbType.Time` with a `DateTime` value | Accepts the value | Use a `TimeSpan` value |
| `DbType.Date` with a `DateTime` value | Can send date and time components | Truncates the time components |

Specify `SqlDbType`, length, precision, and scale for parameters where SQL Server type inference can change query plans or conversion behavior. Don't use `AddWithValue` as a migration shortcut when the database type is known.

## Check transitive provider references

A direct package removal doesn't guarantee that `System.Data.SqlClient` is gone. Run:

```console
dotnet list package --include-transitive
```

If both providers remain:

1. Identify the package that brings in `System.Data.SqlClient`.
1. Update or replace that dependency when possible.
1. Keep provider-specific types inside the dependency boundary when both must remain.
1. Use explicit namespace aliases only as a temporary aid. Don't pass a connection, transaction, parameter, or reader from one provider to the other.

Pay particular attention to SQL Server CLR type libraries and older data access frameworks that expose `System.Data.SqlClient` types in their public APIs.

## Review globalization behavior

.NET Framework and .NET versions before .NET 5 use National Language Support (NLS) globalization on Windows. Current .NET versions use International Components for Unicode (ICU) by default across Windows, Linux, and macOS.

This runtime difference can change some `SqlString` comparisons. SQL Server uses NLS comparison behavior. If client-side `SqlString` comparisons must match server behavior, test affected values and review [Globalization and ICU](/dotnet/core/extensions/globalization-icu). An application can [use NLS instead of ICU](/dotnet/core/extensions/globalization-icu#use-nls-instead-of-icu) when required.

Globalization-invariant mode isn't supported by Microsoft.Data.SqlClient.

## Validate the migrated application

Build and test on every supported target framework and operating system.

Validate:

- Package restore and published output.
- SQL authentication, Windows integrated authentication, and Microsoft Entra authentication used by the application.
- TLS negotiation, certificate validation, and connection string parsing.
- Connection pooling and access token refresh.
- Parameter types, null values, precision, scale, date, and time behavior.
- Transactions, cancellation, timeouts, retries, and failover.
- Always Encrypted, SQL Server CLR types, bulk copy, query notifications, and other provider-specific features used by the application.
- Logging, counters, tracing, and exception handling.

Run representative queries against every supported database engine version. A successful compile doesn't validate connection security, runtime dependencies, or data conversions.

## Related content

- [Microsoft.Data.SqlClient namespace and compatibility](introduction-microsoft-data-sqlclient-namespace.md)
- [Microsoft.Data.SqlClient release notes and updates](microsoft-data-sql-client-release-notes.md)
- [Porting cheat sheet in the SqlClient repository](https://github.com/dotnet/SqlClient/blob/main/porting-cheat-sheet.md)
- [Microsoft.Data.SqlClient API reference](/dotnet/api/microsoft.data.sqlclient)
