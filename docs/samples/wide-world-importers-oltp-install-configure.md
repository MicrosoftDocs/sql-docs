---
title: "Install and configure WideWorldImporters sample database - SQL | Microsoft Docs"
ms.prod: sql
ms.prod_service: sql
ms.technology: samples
ms.custom: ""
ms.date: "04/04/2018"
ms.reviewer: ""
ms.topic: conceptual
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Installation and configuration
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
Wide World Importers OLTP database installation and configuration instructions.

## Prerequisites

- [SQL Server 2016](https://www.microsoft.com/evalcenter/evaluate-sql-server-2016) (or higher) or [Azure SQL Database](https://azure.microsoft.com/services/sql-database/). For the Full version of the sample, use SQL Server Evaluation/Developer/Enterprise Edition.
- [SQL Server Management Studio](../ssms/download-sql-server-management-studio-ssms.md). For the best results use the June 2016 release or later.

## Download

The latest release of the sample:

[wide-world-importers-release](https://go.microsoft.com/fwlink/?LinkID=800630)

Download the sample WideWorldImporters database backup/bacpac that corresponds to your edition of SQL Server or Azure SQL Database.

Source code to recreate the sample database is available from the following location. Note that recreating the sample will result in slight differences in the data, since there is a random factor in the data generation:

[wide-world-importers](https://github.com/Microsoft/sql-server-samples/tree/master/samples/databases/wide-world-importers/wwi-database-scripts)

## Install


### SQL Server

To restore a backup to a SQL Server instance, you can use Management Studio.

1. Open SQL Server Management Studio and connect to the target SQL Server instance.
2. Right-click on the **Databases** node, and select **Restore Database**.
3. Select **Device** and click on the button **...**
4. In the dialog **Select backup devices**, click **Add**, navigate to the database backup in the filesystem of the server, and select the backup. Click **OK**.
5. If needed, change the target location for the data and log files, in the **Files** pane. Note that it is best practice to place data and log files on different drives.
6. Click **OK**. This will initiate the database restore. After it completes, you will have the database WideWorldImporters installed on your SQL Server instance.

### Azure SQL Database

To import a bacpac into a new SQL Database, you can use Management Studio.

1. (optional) If you do not yet have a SQL Server in Azure, navigate to the [Azure portal](https://portal.azure.com/) and create a new SQL Database. In the process of create a database, you will create a server. Make note of the server.
   - See [this tutorial](https://azure.microsoft.com/documentation/articles/sql-database-get-started/) to create a database in minutes
2. Open SQL Server Management Studio and connect to your server in Azure.
3. Right-click on the **Databases** node, and select **Import Data-Tier Application**.
4. In the **Import Settings** select **Import from local disk** and select the bacpac of the sample database from your file system.
5. Under **Database Settings** change the database name to *WideWorldImporters* and select the target edition and service objective to use.
6. Click **Next** and **Finish** to kick off deployment. It will a few minutes to complete on a P1. If a lower pricing tier is desired, it is recommended to import into a new P1 database, and then change the pricing tier to the desired level.

## Configuration

### Full-Text Indexing

The sample database can make use of Full-Text Indexing. However, that feature is not installed by default with SQL Server - you need to select it during SQL Server setup (it is enabled by default in Azure SQL DB). Therefore, a post-installation step is required.

1. In SQL Server Management Studio, connect to the WideWorldImporters database and open a new query window.
2. Run the following T-SQL command to enable the use of Full-Text Indexing in the database:
    `EXECUTE Application.Configuration_ApplyFullTextIndexing`


### SQL Server Audit

Applies to: SQL Server

Enabling auditing in SQL Server requires server configuration. To enable SQL Server audit for the WideWorldImporters sample, run the following statement in the database:

    EXECUTE [Application].[Configuration_ApplyAuditing]

In Azure SQL Database, Audit is configured through the [Azure portal](https://portal.azure.com/).

### Row-Level Security

Applies to: Azure SQL Database

Row-Level Security is not enabled by default in the bacpac download of WideWorldImporters. To enable Row-Level Security in the database, run the following stored procedure:

    EXECUTE [Application].[Configuration_ApplyRowLevelSecurity]

