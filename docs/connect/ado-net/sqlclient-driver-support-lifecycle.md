---
title: SqlClient Driver Support Lifecycle
description: Product support lifecycle information for the Microsoft.Data.SqlClient .NET library.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: randolphwest, davidengel, paulmedynski, cmalhotra
ms.date: 03/17/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
dev_langs:
  - "csharp"
  - "vb"
---

# SqlClient driver support lifecycle

[!INCLUDE [Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

Microsoft.Data.SqlClient library follows the latest .NET Core support policy for all releases.

[View the .NET Core Support Policy](https://dotnet.microsoft.com/platform/support/policy/dotnet-core)

## Microsoft.Data.SqlClient release cadence

New stable (GA) releases are published every six months on a regular cadence beginning with version 1.2, along with 2 to 3 preview releases in between. Stakeholders and maintainers choose Long Term Support (LTS) releases based on a few qualifications and customer response.

### Actively supported releases

| Version | Official Release Date | Latest Patch Version | Patch Release Date | Support Level | End of Support |
| --- | --- | --- | --- | --- | --- |
| 7.0 | March 17, 2026 | [7.0.2](https://www.nuget.org/packages/Microsoft.Data.SqlClient/7.0.2) | June 25, 2026 | STS | |
| 6.1 | August 14, 2025 | [6.1.6](https://www.nuget.org/packages/Microsoft.Data.SqlClient/6.1.6) | June 25, 2025 | LTS | August 14, 2028 |

### Out of support releases

| Version | Release Date | Last Patch Version | Last Patch Release Date | Support Level | Support Ended |
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

## Azure Key Vault Provider release cadence

New stable (GA) releases for `Microsoft.Data.SqlClient.AlwaysEncrypted.AzureKeyVaultProvider` are published on demand when new features are added. Stakeholders and maintainers choose Long Term Support (LTS) releases based on a few qualifications and customer response.

### Actively supported Azure Key Vault Provider releases

| Version | Official Release Date | Latest Patch Version | Patch Release Date | Support Level | End of Support |
| --- | --- | --- | --- | --- | --- |
| 7.x | March 17, 2026 | [7.0.0](https://www.nuget.org/packages/Microsoft.Data.SqlClient.AlwaysEncrypted.AzureKeyVaultProvider/7.0.0) | March 17, 2026 | STS | |
| 6.x | August 14, 2025 | [6.1.2](https://www.nuget.org/packages/Microsoft.Data.SqlClient.AlwaysEncrypted.AzureKeyVaultProvider/6.1.2) | August 19, 2025 | LTS | August 14, 2028 |
| 5.x | February 2, 2024 | [5.1.0](https://www.nuget.org/packages/Microsoft.Data.SqlClient.AlwaysEncrypted.AzureKeyVaultProvider/5.1.0) | February 2, 2024 | LTS | February 3, 2027 |

### Out of support Azure Key Vault Provider releases

| Version | Official Release Date | Latest Patch Version | Patch Release Date | Support Level | End of Support |
| --- | --- | --- | --- | --- | --- |
| 3.x | June 14, 2021 | 3.0.0 | June 14, 2021 | LTS | June 15, 2024 |
| 2.x | March 3, 2021 | 2.0.0 | March 3, 2021 | LTS | March 4, 2024 |
| 1.x | November 19, 2019 | 1.2.0 | December 01, 2020 | LTS | November 21, 2022 |

## Microsoft.Data.SqlClient extensions

The major versions of extensions NuGet packages shipped starting with Microsoft.Data.SqlClient v7.0 onwards follow an LTS support cycle:

### Actively supported Microsoft.Data.SqlClient.Extensions.Abstractions releases

| Version | Official Release Date | Latest Patch Version | Patch Release Date | Support Level | End of Support |
| --- | --- | --- | --- | --- | --- |
| 1.0 | March 17, 2026 | [1.0.0](https://www.nuget.org/packages/Microsoft.Data.SqlClient.Extensions.Abstractions/1.0.0) | March 17, 2026 | LTS | March 17, 2029 |

### Actively supported Microsoft.Data.SqlClient.Extensions.Azure releases

| Version | Official Release Date | Latest Patch Version | Patch Release Date | Support Level | End of Support |
| --- | --- | --- | --- | --- | --- |
| 1.0 | March 17, 2026 | [1.0.0](https://www.nuget.org/packages/Microsoft.Data.SqlClient.Extensions.Azure/1.0.0) | March 17, 2026 | LTS | March 17, 2029 |

## Long Term Support (LTS) releases

LTS releases are supported for three years after the initial release.

## Standard Term Support (STS) releases

STS releases are supported for three months after a subsequent STS or LTS release.

## SQL version compatibility with Microsoft.Data.SqlClient

| Database version&nbsp;&#8594;<br />&#8595; Driver Version | Azure SQL Database | Azure Synapse Analytics | Azure SQL Managed Instance | SQL Server 2025 | SQL Server 2022 | SQL Server 2019 | SQL Server 2017 | SQL Server 2016 | SQL Server 2014 | SQL Server 2012 |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| 7.0 | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | | |
| 6.1 | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | | |
| 6.0 | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | | |
| 5.2 | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | |
| 5.1 | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| 5.0 | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| 4.1 | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| 4.0 | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| 3.1 | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| 3.0 | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| 2.1 | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| 2.0 | | Yes | Yes | | Yes | Yes | Yes | Yes | Yes | Yes |
| 1.1 | | | | | Yes | Yes | Yes | Yes | Yes | Yes |
| 1.0 | | | | | Yes | Yes | Yes | Yes | Yes | Yes |

## Supported OS versions

### Support for .NET Framework applications

Microsoft.Data.SqlClient supports all operating systems supported by .NET Framework v4.6.2 and above.

[.NET Framework system requirements](/dotnet/framework/get-started/system-requirements).

### Support for .NET Core applications

Microsoft.Data.SqlClient supports all operating systems supported by .NET versions under active support.

[.NET Core supported OS lifecycle policy](https://github.com/dotnet/core/blob/master/os-lifecycle-policy.md).

> [!NOTE]  
> Globalization Invariant mode is currently not supported.

### Support policy for dependency vulnerabilities

Microsoft.Data.SqlClient defines external dependencies in its NuGet package definition. NuGet tooling resolves those dependencies at application build time. The NuGet package definition is updated with secure versions of direct dependencies every minor release. Dependencies with known vulnerabilities are updated in hot fixes of supported versions only when it's possible to update the dependency without causing transitive dependency compatibility breaks. If it's not possible to update a vulnerable dependency in this manner, it's up to applications to update the dependency and ensure they don't have dependency conflicts.
