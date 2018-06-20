---
title: "Install and configure AdventureWorks sample database - SQL | Microsoft Docs"
ms.prod: sql
ms.prod_service: sql
ms.technology: samples
ms.custom: ""
ms.date: "06/19/2018"
ms.reviewer: ""
ms.suite: "sql"
ms.tgt_pltfrm: ""
ms.topic: conceptual
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Installation and configuration
[!INCLUDEappliesto-ss-asdb-asdw-pdw-md]
AdventureWorks download links and installation instructions. 

## Prerequisites

- [SQL Server 2016](https://www.microsoft.com/en-us/evalcenter/evaluate-sql-server-2016) (or higher) or [Azure SQL Database](https://azure.microsoft.com/services/sql-database/). For the Full version of the sample, use SQL Server Evaluation/Developer/Enterprise Edition.
- [SQL Server Management Studio](../ssms/download-sql-server-management-studio-ssms.md). For the best results use the June 2016 release or later.
 
## Github links

- [All AdventureWorks files for SQL 2014 - 2016](https://github.com/Microsoft/sql-server-samples/releases/tag/adventureworks)
- [All AdventureWorks files for SQL 2012](https://github.com/Microsoft/sql-server-samples/releases/tag/adventureworks2012)
- [All AdventurewWorks files for SQL 2008 and 2008R2](https://github.com/Microsoft/sql-server-samples/releases/tag/adventureworks2008r2)

## OLTP downloads

Direct links to the OLTP versions of AdventureWorks can be found below:

- [AdventureWorks2017.bak - OLTP](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorks2017.bak)
- [AdventureWorks2016.bak - OLTP](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorks2016.bak)
- [AdventureWorks2014.bak - OLTP](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorks2014.bak)
- [AdventureWorks2012.bak - OLTP](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorks2012.bak)
- [AdventureWorks2008R2.bak - OLTP](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks2008r2/adventure-works-2008r2-oltp.bak)


## Lightweight downloads

Direct links to the  lightweight versions of AdventureWorks can be found below:

- [AdventureWorks2017.bak - LT](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorksLT2017.bak)
- [AdventureWorks2016.bak - LT](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorksLT2016.bak)
- [AdventureWorks2014.bak - LT](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorksLT2014.bak)
- [AdventureWorks2012.bak - LT](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorksLT2012.bak)
- [AdventureWorks2008R2.bak - LT](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks2008r2/adventure-works-2008r2-lt.bak)

## Data Warehouse downloads

Direct links to the Data Warehouse versions of AdventureWorks can be found below:

- [AdventureWorks2017.bak  - DW](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorksDW2017.bak)
- [AdventureWorks2016.bak  - DW](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorksDW2016.bak)
- [AdventureWorks2014.bak  - DW](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorksDW2014.bak)
- [AdventureWorks2012.bak  - DW](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorksDW2012.bak)
- [AdventureWorks2008R2.bak  - DW](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks2008r2/adventure-works-2008-dw.bak)

## Creation scripts
The below scripts can be used to create the entire AdventureWorks database, irrespective of version. 

- [AdventureWorks OLTP Scripts Zip](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorkDW-oltp-install-script.zip)
- [AdventureWorks DW Scripts Zip](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorkDW-data-warehouse-install-script.zip)

## Install

### SQL Server

#### Restore backup
To restore a backup to a SQL Server instance, you can use Management Studio.

1. Open SQL Server Management Studio and connect to the target SQL Server instance.
2. Right-click on the **Databases** node, and select **Restore Database**.
3. Select **Device** and click on the button **...**
4. In the dialog **Select backup devices**, click **Add**, navigate to the database backup in the filesystem of the server, and select the backup. Click **OK**.
5. If needed, change the target location for the data and log files, in the **Files** pane. Note that it is best practice to place data and log files on different drives.
6. Click **OK**. This will initiate the database restore. After it completes, you will have the database AdventureWorks database installed on your SQL Server instance.

For more information on restoring a SQL Server database, see [Restore a database backup using SSMS](../relational-databases/backup-restore/restore-a-database-backup-using-ssms.md).


#### Attach a datafile
To attach a SQL Server database file (.MDF) to a SQL Server instance, you can use Management Studio.

1. Open SQL Server Management Studio and connect to the target SQL Server instance.
2. Right-click on the **Databases** node, and select **Attach**.
3. Select **Add** and navigate to the .MDF file you want to attach. 
1. Select the file and click **OK**. 
    1. The database you selected should be displayed in the bottom window. If the file is listed as "not found",  select the ellipses next to the file name and update the path to the correct path. 
1. Select **OK** to attach the file. 

For more information on attaching database files, see [Attach a database](../relational-databases/databases/attach-a-database.md). 


### Azure SQL Database

To import a bacpac into a new SQL Database, you can use Management Studio.

1. (optional) If you do not yet have a SQL Server in Azure, navigate to the Azure portal, and create a new SQL Database. In the process of create a database, you will create a server. Make note of the server.
   - See [this tutorial](https://azure.microsoft.com/documentation/articles/sql-database-get-started/) to create a database in minutes
2. Open SQL Server Management Studio and connect to your server in Azure.
3. Right-click on the **Databases** node, and select **Import Data-Tier Application**.
4. In the **Import Settings** select **Import from local disk** and select the bacpac of the sample database from your file system.
5. Under **Database Settings** change the database name to *WideWorldImporters* and select the target edition and service objective to use.
6. Click **Next** and **Finish** to kick off deployment. It will a few minutes to complete on a P1. If a lower pricing tier is desired, it is recommended to import into a new P1 database, and then change the pricing tier to the desired level.

## See also
[Tutorials for SQL Server Management Studio](../ssms/tutorials/tutorial-sql-server-management-studio.md)
[Tutorials for SQL Server database engine](../relational-databases/database-engine-tutorials.md)