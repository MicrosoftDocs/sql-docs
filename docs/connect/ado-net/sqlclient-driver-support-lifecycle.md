---
title: "SqlClient driver support lifecycle"
description: "Page that contains product support lifecycle information."
ms.date: "09/08/2020"
dev_langs:
  - "csharp"
  - "vb"
ms.assetid: 6f5ff56a-a57e-49d7-8ae9-bbed697e42e3
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.topic: conceptual
author: David-Engel
ms.author: v-daenge
ms.reviewer: v-kaywon
---
# SqlClient driver support lifecycle

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

Microsoft.Data.SqlClient library follows the latest .NET Core support policy for all releases.

[View the .NET Core Support Policy](https://dotnet.microsoft.com/platform/support/policy/dotnet-core)

## Microsoft.Data.SqlClient release cadence

New stable (GA) releases will be published every six months on a regular cadence beginning with version 1.2, along with 2 to 3 preview releases in between. Long Term Support (LTS) releases will be chosen by stakeholders and maintainers based on a few qualifications and customer response.

### Release Life Cycles

| Version | Official Release Date | Latest Patch Version | Patch Release Date | Support Level  | End of Support |
| -- | -- | -- | -- | -- | -- |
| 2.0 | August 25, 2020 | 2.0.1 | | Current | |
| 1.1 | November 20, 2019 | 1.1.3 | May 15, 2020 | LTS | November 21, 2022 |
| 1.0 | August 28, 2019 | 1.0.19269.1 | September 26, 2019 | Current | February 20, 2020 |

### Long Term Support (LTS) Releases

LTS releases are supported for three years after the initial release.

### Current Releases

Current releases are supported for three months after a subsequent Current or LTS release.

## SQL version compatibility with Microsoft.Data.SqlClient

|Database version&nbsp;&#8594;<br />&#8595; Driver Version|Azure SQL Database|Azure Synapse Analytics|Azure SQL Managed Instance|SQL Server 2019|SQL Server 2017|SQL Server 2016|SQL Server 2014|SQL Server 2012|
|---|---|---|---|---|---|---|---|---|
|2.0|Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|
|1.1|Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|
|1.0|Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|
