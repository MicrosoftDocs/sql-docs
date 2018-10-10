---
title: Export and import a database on Linux | Microsoft Docs
description: 
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 10/02/2017
ms.topic: conceptual
ms.prod: sql
ms.technology: linux
ms.assetid: 2210cfc3-c23a-4025-a551-625890d6845f
ms.custom: "sql-linux"
---
# Export and import a database on Linux with SSMS or SqlPackage.exe on Windows

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

This article shows how to use [SQL Server Management Studio (SSMS)](../ssms/download-sql-server-management-studio-ssms.md) and [SqlPackage.exe](https://msdn.microsoft.com/library/hh550080.aspx) to export and import a database on SQL Server on Linux. SSMS and SqlPackage.exe are Windows applications, so use this technique when you have a Windows machine that can connect to a remote SQL Server instance on Linux.

You should always install and use the most recent version of SQL Server Management Studio (SSMS) as described in [Use SSMS on Windows to connect to SQL Server on Linux](sql-server-linux-manage-ssms.md)

> [!NOTE]
> If you are migrating a database from one SQL Server instance to another, the recommendation is to use [Backup and restore](sql-server-linux-migrate-restore-database.md).

## Export a database with SSMS

1. Start SSMS by typing **Microsoft SQL Server Management Studio** in the Windows search box, and then click the desktop app.

    ![SQL Server Management Studio](./media/sql-server-linux-manage-ssms/ssms.png) 

2. Connect to your source database in Object Explorer. The source database can be in Microsoft SQL Server running on-premises or in the cloud, on Linux, Windows or Docker and Azure SQL Database or Azure SQL Data Warehouse.

3. Right-click the source database in the Object Explorer, point to **Tasks**, and click **Export Data-Tier Application...**

4. In the export wizard, click **Next**, and then on the **Settings** tab, configure the export to save the BACPAC file to either a local disk location or to an Azure blob.

5. By default, all objects in the database are exported. Click the **Advanced tab** and choose the database objects that you wish to export.

6. Click **Next** and then click **Finish**.

The *.BACPAC file is successfully created at the location you chose and you are ready to import it into a target database.

## Import a database with SSMS

1. Start SSMS by typing **Microsoft SQL Server Management Studio** in the Windows search box, and then click the desktop app.

    ![SQL Server Management Studio](./media/sql-server-linux-manage-ssms/ssms.png) 

2. Connect to your target server in Object Explorer. The target server can be Microsoft SQL Server running on-premises or in the cloud, on Linux, Windows or Docker and Azure SQL Database or Azure SQL Data Warehouse.

3. Right-click the **Databases** folder in the Object Explorer and click **Import Data-tier Application...**

4. To create the database in your target server, specify a BACPAC file from your local disk or select the Azure storage account and container to which you uploaded your BACPAC file.

5. Provide the New database name for the database. If you are importing a database on Azure SQL Database, set the Edition of Microsoft Azure SQL Database (service tier), Maximum database size, and Service Objective (performance level).

6. Click **Next** and then click **Finish** to import the BACPAC file into a new database in your target server.

The *.BACPAC file is imported to create a new database in the target server you specified.

## <a id="sqlpackage"></a> SqlPackage command-line option

It is also possible to use the SQL Server Data Tools (SSDT) command-line tool, [SqlPackage.exe](https://msdn.microsoft.com/library/hh550080.aspx), to export and import BACPAC files.

The following example command exports a BACPAC file:

```bash
SqlPackage.exe /a:Export /ssn:tcp:<your_server> /sdn:<your_database> /su:<username> /sp:<password> /tf:<path_to_bacpac>
```

Use the following command to import database schema and user data from a .BACPAC file:

```bash
SqlPackage.exe /a:Import /tsn:tcp:<your_server> /tdn:<your_database> /tu:<username> /tp:<password> /sf:<path_to_bacpac>

```

## See also
For more information on how to use SSMS, see [Use SQL Server Management Studio](https://msdn.microsoft.com/library/ms174173.aspx). For more information on SqlPackage.exe, see the [SqlPackage reference documentation](https://msdn.microsoft.com/library/hh550080.aspx).
