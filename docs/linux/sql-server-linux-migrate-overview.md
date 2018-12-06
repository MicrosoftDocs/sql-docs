---
title: Migrate databases to SQL Server on Linux | Microsoft Docs
description: This article describes the different options for migrating databases and data to SQL Server on Linux.
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 03/17/2017
ms.topic: conceptual
ms.prod: sql
ms.technology: linux
ms.assetid: 1619489d-377a-4f32-8930-d4f536539689
ms.custom: "sql-linux"
---
# Migrate databases and structured data to SQL Server on Linux 

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

You can migrate your databases and data to SQL Server running on Linux. The method you choose to use depends on the source data and your specific scenario. The following sections provide best practices for various migration scenarios.

## Migrate from SQL Server on Windows
If you want to migrate SQL Server databases on Windows to SQL Server on Linux, the recommended technique is to use SQL Server backup and restore.

1. Create a backup of the database on the Windows machine.
2. Transfer the backup file to the target SQL Server Linux machine.
3. Restore the backup on the Linux machine. 

For a tutorial on migrating a database with backup and restore, see the following topic:

- [Restore a SQL Server database from Windows to Linux](sql-server-linux-migrate-restore-database.md).

It is also possible to export your database to a BACPAC file (a compressed file that contains your database schema and data). If you have a BACPAC file, you can transfer this file to your Linux machine and then import it to SQL Server. For more information, see the following topics:

- [Export and import a database with SSMS or SqlPackage.exe](sql-server-linux-migrate-ssms.md)

## Migrate from other database servers
You can migrate databases on other database systems to SQL Server on Linux. This includes Microsoft Access, DB2, MySQL, Oracle, and Sybase databases. In this scenario, use the SQL Server Management Assistant (SSMA) to automate the migration to SQL Server on Linux. For more information, see [Use SSMA to migrate databases to SQL Server on Linux](sql-server-linux-migrate-ssma.md).  

## Migrate structured data
There are also techniques for importing raw data. You might have structured data files that were exported from other databases or data sources. In this case, you can use the bcp tool to bulk insert the data. Or you can run SQL Server Integration Services on Windows to import the data into a SQL Server database on Linux. SQL Server Integration Services enables you to run more complex transformations on the data during the import. 

For more information on these techniques, see the following topics:

- [Bulk copy data with bcp](sql-server-linux-migrate-bcp.md)
- [Extract, transform, and load data for SQL Server on Linux with SSIS](sql-server-linux-migrate-ssis.md) 
