---
title: "Install & configure WideWorldImportersDW sample database"
description: Follow these instructions to download, install, and configure the WideWorldImportersDW sample database with SQL Server Management Studio.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/01/2023
ms.service: sql
ms.subservice: samples
ms.topic: conceptual
ms.custom: intro-installation
monikerRange: ">=sql-server-2016 || >=sql-server-linux-2017 || =azure-sqldw-latest || >=aps-pdw-2016 || =azuresqldb-mi-current"
---
# Install and configure WideWorldImportersDW sample database

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

This article contains installation and configuration instructions for the `WideWorldImportersDW` database.

## Prerequisites

- [SQL Server 2016](https://www.microsoft.com/evalcenter/evaluate-sql-server-2016) with Service Pack 1 (and later versions), or [!INCLUDE [ssazure-sqldb](../includes/ssazure-sqldb.md)]. To use the full version of the sample, use [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Developer or Enterprise editions.

- [SQL Server Management Studio](../ssms/download-sql-server-management-studio-ssms.md) (SSMS).

## Download

Download the sample `WideWorldImportersDW` database backup/BACPAC that corresponds to your edition of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] or [!INCLUDE [ssazure-sqldb](../includes/ssazure-sqldb.md)].

The latest release of the sample is available from [wide-world-importers-release](https://go.microsoft.com/fwlink/?LinkID=800630).

Source code to recreate the sample database is available from [wide-world-importers-source](https://github.com/Microsoft/sql-server-samples/tree/master/samples/databases/wide-world-importers/sample-scripts). Data population is based on ETL from the OLTP database (`WideWorldImporters`).

## Install

You can use SSMS to restore a backup to [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], or import a BACPAC into a new Azure SQL database.

### [SQL Server](#tab/sql-server)

Restore a backup to a [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] instance using SSMS:

1. Open SSMS and connect to the target [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] instance.
1. Right-click on the **Databases** node, and select **Restore Database**.
1. Select **Device** and select the ellipses button (**...**).
1. In the dialog **Select backup devices**, select **Add**, navigate to the database backup in the filesystem of the server, and select the backup. Select **OK**.
1. If needed, change the target location for the data and log files, in the **Files** pane. It is best practice to place data and log files on different drives.
1. Select **OK**. This step initiates the database restore. After it completes, you will have the database `WideWorldImporters` installed on your [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] instance.

### [Azure SQL Database](#tab/sql-database)

Import a BACPAC into a new Azure SQL database, using SSMS:

1. *(optional)* If you don't yet have a [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] in Azure, navigate to the [Azure portal](https://portal.azure.com/) and create a new SQL database. In the process of creating a database, you create a server. Make note of the server.
   - See [this quickstart](/azure/azure-sql/database/single-database-create-quickstart) to create a database in minutes.
1. Open SSMS and connect to your server in Azure.
1. Right-click on the **Databases** node, and select **Import Data-Tier Application**.
1. In the **Import Settings**, select **Import from local disk** and select the BACPAC of the sample database from your file system.
1. Under **Database Settings** change the database name to `WideWorldImportersDW` and select the target edition and service objective to use.
1. Select **Next** and **Finish** to kick off deployment. It takes a few minutes to complete. When you specify a service objective lower than S2, it may take longer.

---

## Configure PolyBase

**Applies to:** [!INCLUDE [sssql16-md](../includes/sssql16-md.md)] and later versions, Developer and Enterprise edition

The sample database can make use of PolyBase to query files in Hadoop or Azure Blob Storage. However, that feature isn't installed by default with [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]; you need to select it during [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Setup. Therefore, a post-installation step is required.

1. In SSMS, connect to the `WideWorldImportersDW` database and open a new query window.
1. Run the following Transact-SQL command to enable the use of PolyBase in the database:

   ```sql
   EXECUTE [Application].[Configuration_ApplyPolyBase];
   ```
