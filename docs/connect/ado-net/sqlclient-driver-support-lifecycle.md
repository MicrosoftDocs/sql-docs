---
title: SqlClient Driver Support Lifecycle
description: Review supported Microsoft.Data.SqlClient releases, support dates, target frameworks, operating systems, and database compatibility.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest, davidengel, paulmedynski, cmalhotra
ms.date: 09/16/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
ai-usage: ai-assisted
---

# SqlClient driver support lifecycle

Microsoft.Data.SqlClient ships independently of .NET. A supported application uses a supported driver release on a supported .NET or .NET Framework runtime and operating system.

## Support levels

- Long-Term Support (LTS) releases receive support for three years after their initial release.
- Standard-Term Support (STS) end dates are listed for each release line.
- Preview releases aren't supported for production use.
- Only supported release lines receive servicing fixes.

## Actively supported Microsoft.Data.SqlClient releases

| Version | Initial release | Latest patch | Patch release | Support level | End of support |
| --- | --- | --- | --- | --- | --- |
| 7.0 | March 17, 2026 | [7.0.3](https://www.nuget.org/packages/Microsoft.Data.SqlClient/7.0.3) | September 10, 2026 | STS | To be determined by the next release |
| 6.1 | August 14, 2025 | [6.1.7](https://www.nuget.org/packages/Microsoft.Data.SqlClient/6.1.7) | September 10, 2026 | LTS | August 14, 2028 |

Use the latest patch in the selected release line. Patch releases contain fixes and don't intentionally add breaking features.

## Out-of-support Microsoft.Data.SqlClient releases

| Version | Initial release | Last patch | Patch release | Support level | Support ended |
| --- | --- | --- | --- | --- | --- |
| 6.0 | January 9, 2025 | 6.0.5 | January 16, 2026 | STS | February 14, 2026 |
| 5.2 | February 28, 2024 | 5.2.3 | April 29, 2025 | STS | August 28, 2025 |
| 5.1 | January 19, 2023 | 5.1.9 | January 13, 2026 | LTS | January 20, 2026 |
| 5.0 | July 29, 2022 | 5.0.2 | March 31, 2023 | STS | July 19, 2023 |
| 4.1 | January 31, 2022 | 4.1.1 | September 13, 2022 | STS | January 29, 2023 |
| 4.0 | November 18, 2021 | 4.0.6 | August 21, 2024 | LTS | November 19, 2024 |
| 3.1 | March 30, 2022 | 3.1.7 | August 20, 2024 | LTS | March 30, 2025 |
| 3.0 | June 9, 2021 | 3.0.1 | September 24, 2021 | STS | May 18, 2022 |
| 2.1 | November 19, 2020 | 2.1.7 | January 9, 2024 | LTS | November 20, 2023 |
| 2.0 | June 16, 2020 | 2.0.1 | August 25, 2020 | STS | May 19, 2021 |
| 1.1 | November 20, 2019 | 1.1.4 | March 10, 2021 | LTS | November 21, 2022 |
| 1.0 | August 28, 2019 | 1.0.19269.1 | September 26, 2019 | STS | May 20, 2020 |

Upgrade applications on an out-of-support line. Review [What's new in Microsoft.Data.SqlClient](microsoft-data-sql-client-release-notes.md) and [Migrate from System.Data.SqlClient to Microsoft.Data.SqlClient](migrate-system-data-sql-client-to-microsoft-data-sql-client.md) for changes that affect an update.

## Target frameworks

Microsoft.Data.SqlClient 7.0 supports applications that target:

| Application target | Operating systems |
| --- | --- |
| .NET Framework 4.6.2 or later | Supported Windows versions for the selected .NET Framework version |
| .NET 8 or later | Supported Windows, Linux, and macOS versions for the selected .NET version |

The package includes a .NET Standard 2.0 compatibility asset for libraries. Executable applications must target a supported .NET or .NET Framework runtime. For deployment steps, see [Install, update, and deploy Microsoft.Data.SqlClient](download-microsoft-sqlclient-data-provider.md).

## Database compatibility

This matrix lists actively supported driver lines and database products. `Yes` means that the driver supports connecting to the database product. Individual features can require a newer driver, database engine, service tier, or protocol version.

| Database product | 7.0 | 6.1 |
| --- | --- | --- |
| [Azure SQL Database](/azure/azure-sql/database/sql-database-paas-overview) | Yes | Yes |
| [Azure SQL Managed Instance](/azure/azure-sql/managed-instance/sql-managed-instance-paas-overview) | Yes | Yes |
| [Azure Synapse Analytics](/azure/synapse-analytics/overview-what-is) | Yes | Yes |
| [SQL database in Microsoft Fabric](/fabric/database/sql/connect) | Yes | Yes |
| [Warehouse in Microsoft Fabric](/fabric/data-warehouse/connectivity) | Yes | Yes |
| [SQL Server 2025](../../sql-server/what-s-new-in-sql-server-2025.md) | Yes | Yes |
| [SQL Server 2022](../../sql-server/what-s-new-in-sql-server-2022.md) | Yes | Yes |
| [SQL Server 2019](../../sql-server/what-s-new-in-sql-server-2019.md) | Yes | Yes |
| [SQL Server 2017](../../sql-server/what-s-new-in-sql-server-2017.md) | Yes | Yes |

SQL database and Warehouse in Microsoft Fabric use TDS endpoints and require Microsoft Entra authentication. Warehouse doesn't support Multiple Active Result Sets (MARS). Review the linked Fabric connectivity articles for current service-specific authentication requirements and limitations.

## Azure Key Vault Provider support

`Microsoft.Data.SqlClient.AlwaysEncrypted.AzureKeyVaultProvider` ships separately from the core driver.

| Version | Initial release | Latest patch | Patch release | Support level | End of support |
| --- | --- | --- | --- | --- | --- |
| 7.x | March 17, 2026 | [7.0.3](https://www.nuget.org/packages/Microsoft.Data.SqlClient.AlwaysEncrypted.AzureKeyVaultProvider/7.0.3) | September 10, 2026 | STS | To be determined by the next release |
| 6.x | August 14, 2025 | [6.1.2](https://www.nuget.org/packages/Microsoft.Data.SqlClient.AlwaysEncrypted.AzureKeyVaultProvider/6.1.2) | August 19, 2025 | LTS | August 14, 2028 |
| 5.x | February 2, 2024 | [5.1.0](https://www.nuget.org/packages/Microsoft.Data.SqlClient.AlwaysEncrypted.AzureKeyVaultProvider/5.1.0) | February 2, 2024 | LTS | February 3, 2027 |

Out-of-support provider lines:

| Version | Initial release | Last patch | Patch release | Support level | Support ended |
| --- | --- | --- | --- | --- | --- |
| 3.x | June 14, 2021 | 3.0.0 | June 14, 2021 | LTS | June 15, 2024 |
| 2.x | March 3, 2021 | 2.0.0 | March 3, 2021 | LTS | March 4, 2024 |
| 1.x | November 19, 2019 | 1.2.0 | December 1, 2020 | LTS | November 21, 2022 |

## Microsoft.Data.SqlClient extension support

The extension packages introduced with Microsoft.Data.SqlClient 7.0 initially used version 1.0.0. Starting with version 7.0.2, the core driver and its companion packages use aligned versions. Applications should reference matching versions.

| Package | Latest aligned version | Initial release | Support level | End of support |
| --- | --- | --- | --- | --- |
| `Microsoft.Data.SqlClient.Extensions.Abstractions` | 7.0.3 | March 17, 2026 | LTS | March 17, 2029 |
| `Microsoft.Data.SqlClient.Extensions.Azure` | 7.0.3 | March 17, 2026 | LTS | March 17, 2029 |

## Supported operating systems

Microsoft.Data.SqlClient supports:

- .NET Framework applications on operating systems supported by [.NET Framework 4.6.2 or later](/dotnet/framework/get-started/system-requirements).
- .NET applications on operating systems supported by an active [.NET release](https://github.com/dotnet/core/blob/main/os-lifecycle-policy.md).

Globalization-invariant mode isn't supported.

## Dependency vulnerability policy

Microsoft.Data.SqlClient declares external dependencies in its NuGet package. NuGet resolves those dependencies when an application restores packages.

The package definition is updated with secure direct-dependency versions in minor releases. A supported driver line receives a hotfix for a vulnerable dependency only when the dependency can be updated without a transitive compatibility break. Otherwise, update the affected dependency in the application and test for dependency conflicts.

## Related content

- [Install, update, and deploy Microsoft.Data.SqlClient](download-microsoft-sqlclient-data-provider.md)
- [What's new in Microsoft.Data.SqlClient](microsoft-data-sql-client-release-notes.md)
- [Microsoft.Data.SqlClient releases](https://github.com/dotnet/SqlClient/releases)
