---
title: SqlClient driver support lifecycle
description: Product support lifecycle information for the Microsoft.Data.SqlClient .NET library.
author: David-Engel
ms.author: v-davidengel
ms.date: 10/24/2022
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
dev_langs:
  - "csharp"
  - "vb"
---
# SqlClient driver support lifecycle

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

Microsoft.Data.SqlClient library follows the latest .NET Core support policy for all releases.

[View the .NET Core Support Policy](https://dotnet.microsoft.com/platform/support/policy/dotnet-core)

## Microsoft.Data.SqlClient release cadence

New stable (GA) releases are published every six months on a regular cadence beginning with version 1.2, along with 2 to 3 preview releases in between. Long Term Support (LTS) releases will be chosen by stakeholders and maintainers based on a few qualifications and customer response.

### Actively supported releases

| Version | Official Release Date | Latest Patch Version | Patch Release Date | Support Level | End of Support |
|--|--|--|--|--|--|
| 5.0 | July 29, 2022 | 5.0.1 | October 11, 2022 | Current | - |
| 4.1 | January 31, 2022 | 4.1.1 | September 13, 2022 | Current | January 29, 2023 |
| 4.0 | November 18, 2021 | 4.0.2 | September 13, 2022 | LTS | November 19, 2024 |
| 3.1 | March 30, 2022 | 3.1.1 | August 12, 2022 | Current | January 29, 2023 |
| 2.1 | November 19, 2020 | 2.1.5 | August 30, 2022 | LTS | November 20, 2023 |
| 1.1 | November 20, 2019 | 1.1.4 | March 10, 2021 | LTS | November 21, 2022 |

### Out of support releases

| Version | Release Date | Last Patch Version | Last Patch Release Date | Support Ended |
|--|--|--|--|--|
| 3.0 | June 9, 2021 | 3.0.1 | September 24, 2021 | May 18, 2022 |
| 2.0 | June 16, 2020 | 2.0.1 | August 25, 2020 | May 19, 2021 |
| 1.0 | August 28, 2019 | 1.0.19269.1 | September 26, 2019 | May 20, 2020 |

## Azure Key Vault Provider release cadence

New stable (GA) releases for `Microsoft.Data.SqlClient.AlwaysEncrypted.AzureKeyVaultProvider` are published on demand when new features are added. Long Term Support (LTS) releases will be chosen by stakeholders and maintainers based on a few qualifications and customer response.

### Actively supported Azure Key Vault Provider releases

| Version | Official Release Date | Latest Patch Version | Patch Release Date | Support Level | End of Support |
|--|--|--|--|--|--|
| 3.x | June 14, 2021 | 3.0.0 | June 14, 2021 | LTS | June 15, 2024 |
| 2.x | March 3, 2021 | 2.0.0 | March 3, 2021 | LTS | March 4, 2024 |
| 1.x | November 19, 2019 | 1.2.0 | December 01, 2020 | LTS | November 21, 2022 |

## Long Term Support (LTS) releases

LTS releases are supported for three years after the initial release.

## Current releases

Current releases are supported for three months after a subsequent Current or LTS release.

## SQL version compatibility with Microsoft.Data.SqlClient

|Database version&nbsp;&#8594;<br />&#8595; Driver Version|Azure SQL Database|Azure Synapse Analytics|Azure SQL Managed Instance|SQL Server 2019|SQL Server 2017|SQL Server 2016|SQL Server 2014|SQL Server 2012|
|---|---|---|---|---|---|---|---|---|
|5.0|Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|
|4.1|Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|
|4.0|Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|
|3.0|Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|
|2.1|Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|
|2.0|Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|
|1.1|Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|
|1.0|Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|

## Supported OS versions

### Support for .NET Framework applications

Microsoft.Data.SqlClient supports all operating systems supported by .NET Framework v4.6.2 and above.

[.NET Framework system requirements](/dotnet/framework/get-started/system-requirements).

### Support for .NET Core applications

Microsoft.Data.SqlClient supports all operating systems supported by .NET Core v3.1 and above.

[.NET Core supported OS lifecycle policy](https://github.com/dotnet/core/blob/master/os-lifecycle-policy.md).

> [!NOTE]
> Globalization Invariant mode is currently not supported.
