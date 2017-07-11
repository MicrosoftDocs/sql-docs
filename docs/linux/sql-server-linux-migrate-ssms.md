---
# required metadata

title: Export and import a database on Linux with SSMS | Microsoft Docs
description: 
author: sanagama 
ms.author: sanagama 
manager: jhubbard
ms.date: 03/17/2017
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: 2210cfc3-c23a-4025-a551-625890d6845f

# optional metadata
# keywords: ""
# ROBOTS: ""
# audience: ""
# ms.devlang: ""
# ms.reviewer: ""
# ms.suite: ""
# ms.tgt_pltfrm: ""
ms.custom: H1Hack27Feb2017

---
# Export and import a database on Linux with SQL Server Management Studio on Windows

This topic shows how to use [SQL Server Management Studio (SSMS)](https://msdn.microsoft.com/library/mt238290.aspx) to export and import a database on SQL Server 2017 CTP 2.1 on Linux. SSMS is a Windows application, so use SSMS when you have a Windows machine that can connect to a remote SQL Server instance on Linux. 

You should always install and use the most recent version of SQL Server Management Studio (SSMS) as described in [Use SSMS on Windows to connect to SQL Server on Linux](sql-server-linux-develop-use-ssms.md)

> [!NOTE]
> If you are migrating a database from one SQL Server instance to another, the recommendation is to use [Backup and restore](sql-server-linux-migrate-restore-database.md).

## Export a database with SSMS

1. Start SSMS by typing **Microsoft SQL Server Management Studio** in the Windows search box, and then click the desktop app.

    ![SQL Server Management Studio](./media/sql-server-linux-develop-use-ssms/ssms.png) 

2. Connect to your source database in Object Explorer. The source database can be in Microsoft SQL Server running on-premises or in the cloud, on Linux, Windows or Docker and Azure SQL Database or Azure SQL Data Warehouse.

3. Right-click the source database in the Object Explorer, point to **Tasks**, and click **Export Data-Tier Application...**

4. In the export wizard, click **Next**, and then on the **Settings** tab, configure the export to save the BACPAC file to either a local disk location or to an Azure blob.

5. By default, all objects in the database are exported. Click the **Advanced tab** and choose the database objects that you wish to export.

6. Click **Next** and then click **Finish**.

The *.BACPAC file is successfully created at the location you chose and you are ready to import it into a target database.

## Import a database with SSMS

1. Start SSMS by typing **Microsoft SQL Server Management Studio** in the Windows search box, and then click the desktop app.

    ![SQL Server Management Studio](./media/sql-server-linux-develop-use-ssms/ssms.png) 

2. Connect to your target server in Object Explorer. The target server can be Microsoft SQL Server running on-premises or in the cloud, on Linux, Windows or Docker and Azure SQL Database or Azure SQL Data Warehouse.

3. Right-click the **Databases** folder in the Object Explorer and click **Import Data-tier Application...**

4. To create the database in your target server, specify a BACPAC file from your local disk or select the Azure storage account and container to which you uploaded your BACPAC file.

5. Provide the New database name for the database. If you are importing a database on Azure SQL Database, set the Edition of Microsoft Azure SQL Database (service tier), Maximum database size, and Service Objective (performance level).

6. Click **Next** and then click **Finish** to import the BACPAC file into a new database in your target server.

The *.BACPAC file is imported to create a new database in the target server you specified.

## See also
For more information on how to use SSMS, see [Use SQL Server Management Studio](https://msdn.microsoft.com/library/ms174173.aspx).
