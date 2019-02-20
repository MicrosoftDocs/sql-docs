---
title: "Install and configure AdventureWorks sample database - SQL | Microsoft Docs"
ms.prod: sql
ms.prod_service: sql
ms.technology: samples
ms.custom: ""
ms.date: "06/19/2018"
ms.reviewer: ""
ms.topic: conceptual
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# AdventureWorks Installation and configuration
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

  > [!div class="nextstepaction"]
  > [Please share your feedback about the SQL Docs Table of Contents!](https://aka.ms/sqldocsurvey)

AdventureWorks download links and installation instructions. 

## Prerequisites

- [SQL Server](https://www.microsoft.com/evalcenter/evaluate-sql-server-2016) or [Azure SQL Database](https://azure.microsoft.com/services/sql-database/). For the Full version of the sample, use SQL Server Evaluation/Developer/Enterprise Edition.
- [SQL Server Management Studio](../ssms/download-sql-server-management-studio-ssms.md). For the best results use the June 2016 release or later.
 
## Github links

- [All AdventureWorks files for SQL 2014 - 2016](https://github.com/Microsoft/sql-server-samples/releases/tag/adventureworks)
- [All AdventureWorks files for SQL 2012](https://github.com/Microsoft/sql-server-samples/releases/tag/adventureworks2012)
- [All AdventureWorks files for SQL 2008 and 2008R2](https://github.com/Microsoft/sql-server-samples/releases/tag/adventureworks2008r2)

## OLTP downloads

Direct links to the OLTP versions of AdventureWorks can be found below:

- [AdventureWorks2017.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorks2017.bak)
- [AdventureWorks2016.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorks2016.bak)
- [AdventureWorks2014.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorks2014.bak)
- [AdventureWorks2012.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorks2012.bak)
- [AdventureWorks2008R2.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks2008r2/adventure-works-2008r2-oltp.bak)


## Data Warehouse downloads

Direct links to the Data Warehouse versions of AdventureWorks can be found below:

- [AdventureWorksDW2017.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorksDW2017.bak)
- [AdventureWorksDW2016.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorksDW2016.bak)
- [AdventureWorksDW2014.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorksDW2014.bak)
- [AdventureWorksDW2012.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorksDW2012.bak)
- [AdventureWorksDW2008R2.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks2008r2/adventure-works-2008-dw.bak)

## Creation scripts
The below scripts can be used to create the entire AdventureWorks database, irrespective of version. 

- [AdventureWorks OLTP Scripts Zip](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorks-oltp-install-script.zip)
- [AdventureWorks DW Scripts Zip](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorksDW-data-warehouse-install-script.zip)

## Install to SQL Server

### Restore backup
Follow the below steps to restore a backup of your database using SQL Server Management Studio. 

1. Open SQL Server Management Studio and connect to the target SQL Server instance.
2. Right-click on the **Databases** node, and select **Restore Database**.
3. Select **Device** and click the ellipses (**...**)
4. In the dialog **Select backup devices**, click **Add**, navigate to the database backup in the filesystem of the server, and select the backup. Click **OK**.
5. If needed, change the target location for the data and log files, in the **Files** pane. Note that it is best practice to place data and log files on different drives.
6. Click **OK**. This will initiate the database restore. After it completes, you will have the AdventureWorks database installed on your SQL Server instance.

For more information on restoring a SQL Server database, see [Restore a database backup using SSMS](../relational-databases/backup-restore/restore-a-database-backup-using-ssms.md).


### Attach a datafile
Follow the below steps to attach the datafile for your database using SQL Server Management Studio.

1. Open SQL Server Management Studio and connect to the target SQL Server instance.
2. Right-click on the **Databases** node, and select **Attach**.
3. Select **Add** and navigate to the .MDF file you want to attach. 
1. Select the file and click **OK**. 
    1. The database you selected should be displayed in the bottom window. If the file is listed as "not found",  select the ellipses (**...**) next to the file name and update the path to the correct path. 
    1. If you only have the data file (.mdf), and not the log file (.ldf), then highlight the .ldf in the bottom window and select **Remove**. This will create a new log file. 
1. Select **OK** to attach the file. After the file is attached, you will have the AdventureWorks database installed on your SQL Server instance.  

For more information on attaching database files, see [Attach a database](../relational-databases/databases/attach-a-database.md). 

## Install to Azure SQL Database


If you do not yet have a SQL Server in Azure, navigate to the [Azure portal](https://portal.azure.com/) and create a new SQL Database. In the process of create a database, you will create a server. Make note of the server. See [this tutorial](https://azure.microsoft.com/documentation/articles/sql-database-get-started/) to create a database in minutes.

1. Connect to your Azure portal.
1. Select **Create a resource** in the top left of the navigation pane. 
1. Select **Databases** and then select **SQL Database**. 
1. Fill in the requested information.
1. In the **Select Source** field, select **Sample (AdventureWorksLT)** to restore a backup of the latest AdventureWorksLT backup.
1. Select **Create** to create your new SQL Database, which is the restored copy of the AdventureWorksLT database. 


## See also
[Tutorials for SQL Server Management Studio](../ssms/tutorials/tutorial-sql-server-management-studio.md)   
[Tutorials for SQL Server database engine](../relational-databases/database-engine-tutorials.md)
