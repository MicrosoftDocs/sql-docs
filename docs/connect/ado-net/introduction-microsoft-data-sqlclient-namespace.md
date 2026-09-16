---
title: Microsoft.Data.SqlClient Namespace and Compatibility
description: Understand the Microsoft.Data.SqlClient namespace, its relationship to ADO.NET and System.Data.SqlClient, and where to find version-specific changes.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest, davidengel, paulmedynski, cmalhotra
ms.date: 09/14/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
ms.custom:
  - sfi-ropc-nochange
  - ignite-2025
ai-usage: ai-assisted
---

# Microsoft.Data.SqlClient namespace and compatibility

The `Microsoft.Data.SqlClient` namespace contains the supported .NET data provider for SQL Server-compatible databases. It began as a fork of `System.Data.SqlClient` and retains the familiar ADO.NET programming model while shipping new SQL Server features independently of the .NET runtime.

Use `Microsoft.Data.SqlClient` for new development.

## Install and import the namespace

Add the NuGet package:

```console
dotnet add package Microsoft.Data.SqlClient
```

Import the namespace:

```csharp
using Microsoft.Data.SqlClient;
```

Most applications use `SqlConnection`, `SqlCommand`, `SqlParameter`, `SqlDataReader`, and `SqlTransaction`. For the complete public surface, see the [Microsoft.Data.SqlClient API reference](/dotnet/api/microsoft.data.sqlclient).

## Relationship to ADO.NET

Microsoft.Data.SqlClient implements provider-independent types from <xref:System.Data.Common>, including `DbConnection`, `DbCommand`, `DbDataReader`, and `DbTransaction`. It also provides SQL Server-specific APIs such as:

- `SqlBulkCopy`
- `SqlBatch`
- `SqlConnectionStringBuilder`
- `SqlConfigurableRetryFactory`
- `SqlDependency`
- SQL Server-specific authentication, encryption, and data type support

For guidance on choosing provider-independent or SqlClient-specific APIs, see [ADO.NET architecture with Microsoft.Data.SqlClient](overview-sqlclient-driver.md).

## Relationship to System.Data.SqlClient

`System.Data.SqlClient` is the older provider that ships with .NET Framework and earlier .NET releases. `Microsoft.Data.SqlClient` uses many of the same type names, but it has different packages, namespaces, defaults, features, and support policies.

Don't reference both providers in new application code. Types with the same name from the two packages aren't interchangeable. A `System.Data.SqlClient.SqlConnection`, for example, can't be passed where a `Microsoft.Data.SqlClient.SqlConnection` is required.

Migration usually starts by adding the `Microsoft.Data.SqlClient` package and changing namespace references, but you must also review connection encryption, parameters, configuration, transitive dependencies, and runtime behavior. Follow [Migrate from System.Data.SqlClient to Microsoft.Data.SqlClient](migrate-system-data-sql-client-to-microsoft-data-sql-client.md) before you deploy.

## Version compatibility

The driver package version is independent of the application's .NET version and the database engine version. Check all three:

| Component | What to verify |
| --- | --- |
| Microsoft.Data.SqlClient | The release line is supported and contains the features your application uses. |
| .NET or .NET Framework | The application targets a runtime supported by that driver release. |
| SQL Server-compatible database | The driver release supports the database version and the server supports any requested protocol feature, such as TDS 8.0. |

For current matrices, see [SqlClient driver support lifecycle](sqlclient-driver-support-lifecycle.md).

## Version-specific changes

Driver releases can add APIs, move optional dependencies, and change secure defaults. Review changes before every minor or major update.

Examples include:

- Version 4.0 changed `Encrypt` to default to `true`.
- Version 5.0 introduced `SqlConnectionEncryptOption` and `Encrypt=Strict` for TDS 8.0.
- Version 5.2 introduced `SqlBatch` on supported modern .NET targets.
- Version 7.0 moved driver-provided Microsoft Entra authentication into `Microsoft.Data.SqlClient.Extensions.Azure`.

See [What's new in Microsoft.Data.SqlClient](microsoft-data-sql-client-release-notes.md) for release-note links and an upgrade summary.

## Related namespaces and packages

Some features use types outside the main namespace or optional packages.

| Namespace or package | Purpose |
| --- | --- |
| `Microsoft.Data.SqlClient.Server` | SqlClient implementations of `SqlDataRecord` and `SqlMetaData`. |
| `Microsoft.Data.SqlTypes` | SqlClient-specific types such as `SqlFileStream`, `SqlJson`, and `SqlVector<T>`. |
| `Microsoft.Data.SqlClient.Diagnostics` | Strongly typed diagnostic event payloads. |
| `Microsoft.Data.SqlClient.Extensions.Azure` | Driver-provided Microsoft Entra authentication for version 7.0 and later. |
| `Microsoft.Data.SqlClient.AlwaysEncrypted.AzureKeyVaultProvider` | Azure Key Vault integration for Always Encrypted. |

Don't infer package requirements from a namespace alone. Follow the feature article and the NuGet package dependency information for the driver version you use.

## Related content

- [Install, update, and deploy Microsoft.Data.SqlClient](download-microsoft-sqlclient-data-provider.md)
- [Migrate from System.Data.SqlClient to Microsoft.Data.SqlClient](migrate-system-data-sql-client-to-microsoft-data-sql-client.md)
- [What's new in Microsoft.Data.SqlClient](microsoft-data-sql-client-release-notes.md)
- [Microsoft.Data.SqlClient release notes](https://github.com/dotnet/SqlClient/tree/main/release-notes)
