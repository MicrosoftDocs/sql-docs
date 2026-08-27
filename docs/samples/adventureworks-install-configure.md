---
title: AdventureWorks Sample Databases
description: Follow these instructions to download and install AdventureWorks sample databases to SQL Server.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 03/16/2026
ms.service: sql
ms.subservice: samples
ms.topic: concept-article
ms.custom:
  - ignite-2025
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---

# AdventureWorks sample databases

[!INCLUDE [SQL Server Azure SQL Database Azure SQL MI FabricSQLDB](../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

This article provides direct links for downloading `AdventureWorks` sample databases and instructions for restoring them to your database.

For more information about samples, see the [Samples GitHub repository](https://github.com/microsoft/sql-server-samples/tree/master/samples/databases).

## Prerequisites

- [SQL Server](https://www.microsoft.com/evalcenter/evaluate-sql-server-2025) or [Azure SQL Database](https://azure.microsoft.com/services/sql-database/)
- [SQL Server Management Studio (SSMS)](/ssms/sql-server-management-studio-ssms) or [MSSQL extension for Visual Studio Code](../tools/visual-studio-code-extensions/mssql/mssql-extension-visual-studio-code.md)

## Download backup files

Use these links to download the appropriate sample database for your scenario.

- **OLTP** data works for most typical online transaction processing workloads.
- **Data Warehouse (DW)** data works for data warehousing workloads.
- **Lightweight (LT)** data is a lightweight and pared down version of the **OLTP** sample.

If you're not sure what you need, start with the OLTP version that matches your SQL Server version.

| OLTP | Data Warehouse | Lightweight |
| --- | --- | --- |
| [AdventureWorks2025.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorks2025.bak) | [AdventureWorksDW2025.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorksDW2025.bak) | [AdventureWorksLT2025.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorksLT2025.bak) |
| [AdventureWorks2022.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorks2022.bak) | [AdventureWorksDW2022.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorksDW2022.bak) | [AdventureWorksLT2022.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorksLT2022.bak) |
| [AdventureWorks2019.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorks2019.bak) | [AdventureWorksDW2019.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorksDW2019.bak) | [AdventureWorksLT2019.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorksLT2019.bak) |
| [AdventureWorks2017.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorks2017.bak) | [AdventureWorksDW2017.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorksDW2017.bak) | [AdventureWorksLT2017.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorksLT2017.bak) |
| [AdventureWorks2016.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorks2016.bak) | [AdventureWorksDW2016.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorksDW2016.bak) | [AdventureWorksLT2016.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorksLT2016.bak) |
| [AdventureWorks2016_EXT.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorks2016_EXT.bak) | [AdventureWorksDW2016_EXT.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorksDW2016_EXT.bak) | N/A |
| [AdventureWorks2014.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorks2014.bak) | [AdventureWorksDW2014.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorksDW2014.bak) | [AdventureWorksLT2014.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorksLT2014.bak) |
| [AdventureWorks2012.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorks2012.bak) | [AdventureWorksDW2012.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorksDW2012.bak) | [AdventureWorksLT2012.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorksLT2012.bak) |
| [AdventureWorks2008R2.bak](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks2008r2/adventure-works-2008r2-oltp.bak) | [AdventureWorksDW2008R2.bak](https://github.com/microsoft/sql-server-samples/releases/download/adventureworks2008r2/adventure-works-2008r2-dw.bak) | N/A |

You can find more files on GitHub:

- [SQL Server 2014 - 2025](https://github.com/Microsoft/sql-server-samples/releases/tag/adventureworks)
- [SQL Server 2012](https://github.com/Microsoft/sql-server-samples/releases/tag/adventureworks2012)
- [SQL Server 2008 and 2008 R2](https://github.com/Microsoft/sql-server-samples/releases/tag/adventureworks2008r2)

## Restore to SQL Server

Use the `.bak` file to restore your sample database to your [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] instance. You can restore the database by using the [RESTORE Statements](../t-sql/statements/restore-statements-transact-sql.md) T-SQL command, or by using the graphical interface (GUI) in [SSMS](/ssms/sql-server-management-studio-ssms), the [MSSQL extension for Visual Studio Code](../tools/visual-studio-code-extensions/mssql/mssql-extension-visual-studio-code.md), or any T-SQL query tool.

### [SSMS](#tab/ssms)

If you're not familiar with using SSMS, review [Connect and query using SSMS](/ssms/quickstarts/ssms-connect-query-sql-server) to get started.

To restore your database in SSMS, follow these steps:

1. Download the appropriate `.bak` file from one of the links provided in the [Download backup files](#download-backup-files) section of this article.
1. Move the `.bak` file to your [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] backup location. This location varies depending on your installation location, instance name, and version of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. For example, the default location for a default instance of [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] is:

   ```output
   C:\Program Files\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSQL\Backup
   ```

1. Open SSMS and connect to your [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] instance.
1. Right-click **Databases** in **Object Explorer** and then select **Restore Database...** to start the **Restore Database** wizard.

   :::image type="content" source="media/adventureworks-install-configure/restore-db-ssms.png" alt-text="Screenshot showing the steps for starting the Restore Database wizard.":::

1. Select **Device**, and then select the ellipsis (**...**) to choose a device.
1. Select **Add**, and then choose the `.bak` file you recently moved to the backup location. If you moved your file to this location but you don't see it in the wizard, [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] or the user signed into [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] doesn't have permission to this file in this folder.
1. Select **OK** to confirm your database backup selection and close the **Select backup devices** window.
1. Check the **Files** tab to confirm that the **Restore as** location and file names match your intended location and file names in the **Restore Database** wizard.
1. Select **OK** to restore your database.

   :::image type="content" source="media/adventureworks-install-configure/restore-db-wizard-ssms.png" alt-text="Screenshot showing the Restore Database window. The backup set to restore and the OK option are highlighted." lightbox="media/adventureworks-install-configure/restore-db-wizard-ssms.png":::

For more information on restoring a [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] database, see [Restore a Database Backup Using SSMS](../relational-databases/backup-restore/restore-a-database-backup-using-ssms.md).

### [Transact-SQL (T-SQL)](#tab/tsql)

You can restore your sample database by using T-SQL. The following example restores [!INCLUDE [sssampledbobject-md](../includes/sssampledbobject-md.md)], but the database name and installation file path can vary depending on your environment.

To restore [!INCLUDE [sssampledbobject-md](../includes/sssampledbobject-md.md)] on Windows, modify values as appropriate to your environment and then run the following T-SQL command:

```sql
USE [master];
GO

RESTORE DATABASE [AdventureWorks2025]
FROM DISK = N'C:\Program Files\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSQL\Backup\AdventureWorks2025.bak'
WITH
    FILE = 1,
    NOUNLOAD,
    STATS = 5;
GO
```

To restore [!INCLUDE [sssampledbobject-md](../includes/sssampledbobject-md.md)] on Linux, change the Windows filesystem path to Linux, and then run the following T-SQL command:

```sql
USE [master];
GO

RESTORE DATABASE [AdventureWorks2025]
FROM DISK = '/var/opt/mssql/backup/AdventureWorks2025.bak'
WITH
    MOVE 'AdventureWorks2025' TO '/var/opt/mssql/data/AdventureWorks2025_Data.mdf',
    MOVE 'AdventureWorks2025_log' TO '/var/opt/mssql/data/AdventureWorks2025_log.ldf',
    FILE = 1,
    NOUNLOAD,
    STATS = 5;
GO
```

---

## Deploy to Azure SQL Database

You have two options for viewing sample SQL Database data. You can use a sample when you create a new database, or you can deploy a database from [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] directly to Azure by using SSMS.

To get sample data for SQL Managed Instance instead, see [Quickstart: Restore a database to Azure SQL Managed Instance with SSMS](/azure/azure-sql/managed-instance/restore-sample-database-quickstart).

### Deploy a new sample database

When you create a new database in SQL Database, you can create a blank database, restore from a backup, or select sample data to populate your new database.

Follow these steps to deploy a new sample `AdventureWorksLT` database in Azure SQL Database:

1. Go to [Azure SQL hub at aka.ms/azuresqlhub](https://aka.ms/azuresqlhub).
1. In the resource menu, expand **Azure SQL Database** and select **SQL databases**.
1. Select the **+ Create** dropdown list and select **SQL database**.

   :::image type="content" source="media/adventureworks-install-configure/create-sql-database.png" alt-text="Screenshot from the Azure portal showing the SQL databases page, the Create button, and the SQL database option." lightbox="media/adventureworks-install-configure/create-sql-database.png":::

1. In the **Create SQL Database** page, fill in the necessary information to create your database.
1. On the **Additional settings** tab, choose **Sample** as the existing data under **Data source**:

   :::image type="content" source="media/adventureworks-install-configure/deploy-sample-to-azure.png" alt-text="Screenshot that shows the Sample option under Use existing data." lightbox="media/adventureworks-install-configure/deploy-sample-to-azure.png":::

1. Select **Next: Tags**.

1. Consider using Azure tags. For example, use the `Owner` or `CreatedBy` tag to identify who created the resource, and use the `Environment` tag to identify whether this resource is in Production, Development, or another environment. For more information, see [Develop your naming and tagging strategy for Azure resources](/azure/cloud-adoption-framework/ready/azure-best-practices/naming-and-tagging).

1. Select **Create** to create your new SQL Database, which is the restored copy of the `AdventureWorksLT` database.

### Deploy a database from SQL Server

SSMS allows you to deploy a database directly to Azure SQL Database. This method is intended for development and testing, so it doesn't currently provide data validation. Don't use this deployment method for production.

To deploy a sample database from [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] to SQL Database, follow these steps:

1. Connect to your [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] in SSMS.
1. If you didn't already, [restore the sample database to SQL Server](#restore-to-sql-server).
1. Right-click your restored database in **Object Explorer**, and then select **Tasks** > **Deploy Database to Microsoft Azure SQL Database**.

   :::image type="content" source="media/adventureworks-install-configure/deploy-db-to-azure.png" alt-text="Screenshot that shows the menu steps for deploying a database to SQL Database.":::

1. Complete the steps in the wizard to connect to SQL Database and deploy your database.

## Deploy to SQL database in Microsoft Fabric

To load a sample `AdventureWorksLT` database in a new SQL database in Microsoft Fabric, [create a new SQL database in Fabric](/fabric/database/sql/create). Then, under **Build your database**, select the **Sample data** button.

## Scripts for creating a database

Instead of restoring a database, you can use scripts to create the `AdventureWorks` databases, regardless of version.

Use the following scripts to create the entire `AdventureWorks` database:

- [AdventureWorks OLTP scripts zip](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorks-oltp-install-script.zip)
- [AdventureWorks DW scripts zip](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorksDW-data-warehouse-install-script.zip)

You can find additional information about using the scripts on [GitHub](https://github.com/Microsoft/sql-server-samples/releases/tag/adventureworks).

## Related content

- [Database Engine Tutorials](../relational-databases/database-engine-tutorials.md)
- [Quickstart: Connect and query a SQL Server instance using SSMS](/ssms/quickstarts/ssms-connect-query-sql-server)
- [MSSQL extension for Visual Studio Code](../tools/visual-studio-code-extensions/mssql/mssql-extension-visual-studio-code.md)
