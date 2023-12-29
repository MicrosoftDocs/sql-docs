---
title: "Configure properties of a data collector"
description: Configure properties of a data collector in SQL Server Management Studio.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 12/28/2023
ms.service: sql
ms.subservice: supportability
ms.topic: conceptual
f1_keywords:
  - "sql13.swb.dc.datacollectionprop.general.f1"
  - "sql13.swb.dc.datacollectionprop.advanced.f1"
---
# Configure properties of a data collector

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article discusses how you can configure the properties of a data collector.

## Data Collection properties (General tab)

Use this page to configure settings for the management data warehouse, and specify where collected data should be stored before it uploads to the data warehouse.

#### Enable Data Collection

Select this checkbox to enable data collection. This has the same effect as running the `sp_syscollector_enable_collector` stored procedure. Clearing this checkbox disables data collection and has the same effect as running the `sp_syscollector_disable_collector` stored procedure.

#### Server

Displays the name of the server that will host the management data warehouse

#### Database name

Displays the name of the relational database that is used for the management data warehouse.

#### Test connection

Test the connection to the specified **Server** using information provided when data collection is configured.

#### Cache directory

Specifies the directory where collected data is stored on the system collecting the data before it's uploaded to the management data warehouse. If **Cache directory** isn't specified, the data collector attempts to locate the `%TEMP%` and `%TMP%` environment variables, and uses one these locations as the default location for temporary storage. If these environment variables aren't configured, an error occurs and you're prompted to create a cache directory.

## Data Collection properties (Advanced tab)

Use this page to configure the retry settings for the connection to the management data warehouse.

#### Number of times to retry if upload fails

Specifies the number of times to retry an upload to the management data warehouse if an upload fails. The default is 1.

## Related content

- [Manage data collection](manage-data-collection.md)
- [Configure the management data warehouse (SQL Server Management Studio)](configure-the-management-data-warehouse-sql-server-management-studio.md)
