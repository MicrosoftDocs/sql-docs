---
title: "AdventureWorks sample databases"
description: Follow these instructions to download and install AdventureWorks sample databases to SQL Server using Transact-SQL (T-SQL), SQL Server Management Studio (SSMS), or Azure Data Studio.  
ms.service: sql
ms.subservice: samples
ms.date: 11/12/2021
ms.reviewer: ""
ms.topic: conceptual
author: MashaMSFT
ms.author: mathoma
ms.custom: "seo-lt-2019"
---
# AdventureWorks sample databases

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

This article provides direct links to download AdventureWorks sample databases, as well as instructions for restoring them to SQL Server and Azure SQL Database. 

For more information about samples, see the [Samples GitHub repository](https://github.com/microsoft/sql-server-samples/tree/master/samples/databases). 

## Prerequisites

- [SQL Server](https://www.microsoft.com/evalcenter/evaluate-sql-server-2019) or [Azure SQL Database](https://azure.microsoft.com/services/sql-database/)
- [SQL Server Management Studio](../ssms/download-sql-server-management-studio-ssms.md) or [Azure Data Studio](../azure-data-studio/download-azure-data-studio.md)

## Download backup files 

Use these links to download the appropriate sample database for your scenario. 

- **OLTP** data is for most typical online transaction processing workloads. 
- **Data Warehouse (DW)** data is for data warehousing workloads. 
- **Lightweight (LT)** data is a lightweight and pared down version of the **OLTP** sample. 

If you're not sure what you need, start with the OLTP version that matches your SQL Server version. 

|**OLTP** |**Data Warehouse** |**Lightweight**|
|---------|---------|---------|
|[AdventureWorks2019.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorks2019.bak)|[AdventureWorksDW2019.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorksDW2019.bak)|[AdventureWorksLT2019.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorksLT2019.bak)|
|[AdventureWorks2017.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorks2017.bak)|[AdventureWorksDW2017.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorksDW2017.bak)|[AdventureWorksLT2017.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorksLT2017.bak)|
|[AdventureWorks2016.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorks2016.bak)|[AdventureWorksDW2016.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorksDW2016.bak)|[AdventureWorksLT2016.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorksLT2016.bak)|
|[AdventureWorks2016_EXT.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorks2016_EXT.bak)|[AdventureWorksDW2016_EXT.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorksDW2016_EXT.bak)| N/A |
|[AdventureWorks2014.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorks2014.bak)|[AdventureWorksDW2014.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorksDW2014.bak)|[AdventureWorksLT2014.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorksLT2014.bak)|
|[AdventureWorks2012.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorks2012.bak)|[AdventureWorksDW2012.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorksDW2012.bak)|[AdventureWorksLT2012.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorksLT2012.bak)|
|[AdventureWorks2008R2.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks2008r2/adventure-works-2008r2-oltp.bak)| [AdventureWorksDW2008R2.bak](https://github.com/microsoft/sql-server-samples/releases/download/adventureworks2008r2/adventure-works-2008r2-dw.bak) | N/A |

Additional files can be found directly on GitHub: 

- [SQL Server 2014 - 2019](https://github.com/Microsoft/sql-server-samples/releases/tag/adventureworks)
- [SQL Server 2012](https://github.com/Microsoft/sql-server-samples/releases/tag/adventureworks2012)
- [SQL Server 2008 and 2008R2](https://github.com/Microsoft/sql-server-samples/releases/tag/adventureworks2008r2)


## Restore to SQL Server 

You can use the `.bak` file to restore your sample database to your SQL Server instance. You can do so using the [RESTORE (Transact-SQL)](../t-sql/statements/restore-statements-transact-sql.md) command, or using the graphical interface (GUI) in [SQL Server Management Studio](../ssms/download-sql-server-management-studio-ssms.md) or [Azure Data Studio](../azure-data-studio/download-azure-data-studio.md).

# [SQL Server Management Studio (SSMS)](#tab/ssms)

If you're not familiar using SQL Server Management Studio (SSMS), you can see [connect & query](../ssms/quickstarts/ssms-connect-query-sql-server.md) to get started. 

To restore your database in SQL Server Management Studio, follow these steps:

1. Download the appropriate `.bak` file from one of links provided in the [download backup files](#download-backup-files) section.
2. Move the `.bak` file to your SQL Server backup location. This varies depending on your installation location, instance name and version of SQL Server. For example, the default location for a default instance of SQL Server 2019 is:

   `C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\Backup`. 

3. Open SQL Server Management Studio (SSMS) and connect to your SQL Server. 
4. Right-click **Databases** in **Object Explorer** > **Restore Database...** to launch the **Restore Database** wizard. 

   :::image type="content" source="media/adventureworks-install-configure/restore-db-ssms.png" alt-text="Screenshot showing how to choose to restore your database by right-clicking databases in Object Explorer and then selecting Restore Database.":::


1. Select **Device** and then select the ellipses **(...)** to choose a device. 
1. Select **Add** and then choose the `.bak` file you recently moved to the backup location. If you moved your file to this location but you're not able to see it in the wizard, this typically indicates a permissions issue - SQL Server or the user signed into SQL Server does not have permission to this file in this folder. 
1. Select **OK** to confirm your database backup selection and close the **Select backup devices** window. 
1. Check the **Files** tab to confirm the **Restore as** location and file names match your intended location and file names in the **Restore Database** wizard. 
1. Select **OK** to restore your database. 

   :::image type="content" source="media/adventureworks-install-configure/restore-db-wizard-ssms.png" alt-text="Screenshot showing the Restore Database window with the backup set to restore highlighted and the OK option called out.":::

For more information on restoring a SQL Server database, see [Restore a database backup using SSMS](../relational-databases/backup-restore/restore-a-database-backup-using-ssms.md).

# [Transact-SQL (T-SQL)](#tab/tsql)

You can restore your sample database using Transact-SQL (T-SQL). An example to restore AdventureWorks2019 is provided below, but the database name and installation file path may vary depending on your environment. 

To restore AdventureWorks2019 to **Windows**, modify values as appropriate to your environment and then run the following Transact-SQL (T-SQL) command:


```sql
USE [master]
RESTORE DATABASE [AdventureWorks2019] 
FROM  DISK = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\Backup\AdventureWorks2019.bak' 
WITH  FILE = 1,  NOUNLOAD,  STATS = 5
GO

```

To restore AdventureWorks2019 to **Linux**, change the Windows filesystem path to Linux, and then run the following Transact-SQL (T-SQL) command: 


```sql
USE [master]
RESTORE DATABASE [AdventureWorks2019]
FROM DISK = '/var/opt/mssql/backup/AdventureWorks2019.bak'
WITH MOVE 'AdventureWorks2017' TO '/var/opt/mssql/data/AdventureWorks2019.mdf',
MOVE 'AdventureWorks2017_log' TO '/var/opt/mssql/data/AdventureWorks2019_log.ldf',
FILE = 1,  NOUNLOAD,  STATS = 5
GO
```

# [Azure Data Studio](#tab/data-studio)

If you're not familiar using [Azure Data Studio Studio](../azure-data-studio/download-azure-data-studio.md), see [connect & query](../azure-data-studio/quickstart-sql-server.md) to get started

To restore your database in Azure Data Studio, follow these steps:

1. Download the appropriate `.bak` file from one of links provided in the [download backup files](#download-backup-files) section.
1. Move the `.bak` file to your SQL Server backup location. This varies depending on your installation location, instance name and version of SQL Server. For example, the default location for a default instance of SQL Server 2019 is:

    `C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\Backup`.

1. Open Azure Data Studio Studio and connect to your SQL Server instance.
1. Right-click on your server and select **Manage**.

   :::image type="content" source="media/adventureworks-install-configure/ads-manage.png" alt-text="Screenshot showing Azure Data Studio with the Manage option highlighted and called out.":::

1. Select **Restore**

   :::image type="content" source="media/adventureworks-install-configure/ads-restore-database.png" alt-text="Select restore from the top menu to restore your database.":::

1. On the **General** tab, fill in the values listed under **Source**.
    1. Under **Restore from**, select *Backup file*.
    1. Under **Backup file path**, select the location you stored the .bak file. 
    
   :::image type="content" source="media/adventureworks-install-configure/ads-source.png" alt-text="Select your backup file path":::
    
    This auto-populates the rest of the fields such as **Database**, **Target database** and **Restore to**. 

   :::image type="content" source="media/adventureworks-install-configure/ads-destination-restore-plan.png" alt-text="Once you have chosen a backup file path, the rest of the fields autopopulate":::

1. Select **Restore** to restore your database. 

   :::image type="content" source="media/adventureworks-install-configure/ads-restore.png" alt-text="Once you're ready, select Restore to restore your database.":::

---

## Deploy to Azure SQL Database

You have two options to view sample Azure SQL Database data. You can use a sample when you create a new database, or you can deploy a database from SQL Server directly to Azure using SQL Server Management Studio (SSMS).

To get sample data for Azure SQL Managed Instance instead, see [restore World Wide Importers to SQL Managed Instance](/azure/azure-sql/managed-instance/restore-sample-database-quickstart). 

### Deploy new sample database

When you create a new database in Azure SQL Database, you have the option to create a blank database, restore from a backup or select sample data to populate your new database. 

Follow these steps to add a sample data to your new database: 

1. Connect to your Azure portal.
1. Select **Create a resource** in the top left of the navigation pane. 
1. Select **Databases** and then select **SQL Database**. 
1. Fill in the requested information to create your database. 
1. On the **Additional settings** tab, choose **Sample** as the existing data under **Data source**: 

   :::image type="content" source="media/adventureworks-install-configure/deploy-sample-to-azure.png" alt-text="Choose sample as the data source on the Additional settings tab in the Azure portal when creating your Azure SQL Database":::

1. Select **Create** to create your new SQL Database, which is the restored copy of the AdventureWorksLT database.

### Deploy database from SQL Server

SQL Server Management Studio provides the ability to deploy a database directly to Azure SQL Database. This method does not currently provide data validation so is intended for development and testing and should not be used for production. 

To deploy a sample database from SQL Server to Azure SQL Database, follow these steps:

1. Connect to your SQL Server in SQL Server Management Studio. 
1. If you haven't already done so, [restore the sample database to SQL Server](#restore-to-sql-server). 
1. Right-click your restored database in **Object Explorer** > **Tasks** > **Deploy Database to Microsoft Azure SQL Database...**. 

   :::image type="content" source="media/adventureworks-install-configure/deploy-db-to-azure.png" alt-text="Choose to deploy your database to Microsoft Azure SQL Database from right-clicking your database and selecting Tasks":::

1. Follow the wizard to connect to Azure SQL Database and deploy your database. 


## Creation scripts

Instead of restoring a database, alternatively, you can use scripts to create the AdventureWorks databases regardless of version. 

The below scripts can be used to create the entire AdventureWorks database: 

- [AdventureWorks OLTP Scripts Zip](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorks-oltp-install-script.zip)
- [AdventureWorks DW Scripts Zip](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorksDW-data-warehouse-install-script.zip)

Additional information about using the scripts can be found on [GitHub](https://github.com/Microsoft/sql-server-samples/releases/tag/adventureworks). 

## Next steps

Once you've restored your sample database, using the following tutorials to get started with SQL Server: 


[Tutorials for SQL Server database engine](../relational-databases/database-engine-tutorials.md)   
[Connect and query with SQL Server Management Studio (SSMS)](../ssms/quickstarts/ssms-connect-query-sql-server.md)   
[Connect and query with Azure Data Studio](../ssms/quickstarts/ssms-connect-query-sql-server.md)