---
title: "SQL Server 2022 Release Notes | Microsoft Docs"
description: Find information about SQL Server 2019 (16.x) limitations, known issues, help resources, and other release notes.
ms.date: 07/26/2022
ms.prod: sql
ms.reviewer: ""
ms.technology: release-landing
ms.topic: "article"
ms.custom:
- event-tier1-build-2022
author: MikeRayMSFT
ms.author: mikeray
monikerRange: "= sql-server-ver16"
---
# [!INCLUDE[SQL Server 2022](../includes/sssql22-md.md)] release notes

[!INCLUDE[SQL Server 2022](../includes/applies-to-version/sqlserver2022.md)]

This article describes requirements, limitations and known issues for the [!INCLUDE[SQL Server 2022](../includes/sssql22-md.md)] community technology preview (CTP) 2.1.

Complete details about licensing are in `License Terms` folder on the installation media.

## Hardware and software requirements

This release has the same hardware and software requirements as [SQL Server 2019](install/hardware-and-software-requirements-for-installing-sql-server-2019.md), except as noted below:

- .NET Framework: 4.7.2. [Download](https://dotnet.microsoft.com/download/dotnet-framework/net472).

## Feature notes

This section identifies known issues you may experience with this product:

- Certain SSIS functions require the [Microsoft ODBC Driver 18 for SQL Server](../connect/odbc/download-odbc-driver-for-sql-server.md) and [Microsoft OLE DB Driver 19 for SQL Server](../connect/oledb/download-oledb-driver-for-sql-server.md), which are not packaged in SQL Server 2022 CTP2.1 installation. For CTP2.1, please install the two drivers from the provided links as necessary. The new drivers will be installed by later versions of SQL Server 2022.

- When SQL Server is installed on an Azure Arc-enabled server, Setup UI fails to automatically populate the Azure parameters from the installed Connected Machine agent. If you specify a subscription ID, Service identity or Resource group that is different than those the Connected Machine agent uses, it will result in a non-functional Azure configuration. Currently in SQL Server 2022 CTP 2.1, we recommend that you skip the Azure extension for SQL Server in the configuration window by selecting **Next**. Instead, you use Azure portal to install the extension. For more information, see [Connect to Azure Arc from the Azure portal](azure-arc/connect.md).

## Build number

| Preview build | Version number | Date |
|:--|:--|:--|
| CTP 2.1 | 16.0.700.4 | July 27, 2022 |
| CTP 2.0 | 16.0.600.9 | May 20, 2022 |

## Next steps

For related information about new features and capabilities in this version of SQL Server, see:

[What's new in [!INCLUDE[sql-server-2022](../includes/sssql22-md.md)]](what-s-new-in-sql-server-2022.md).
