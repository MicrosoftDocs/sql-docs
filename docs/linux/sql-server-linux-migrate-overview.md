---
title: Migrate databases to SQL Server on Linux
description: This article describes the different options for migrating databases and data to SQL Server on Linux.
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/23/2023
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
ms.custom:
  - intro-migration
  - linux-related-content
---
# Migrate databases and structured data to SQL Server on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

You can migrate your databases and data to [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] running on Linux. The method you choose to use depends on the source data and your specific scenario. The following sections provide best practices for various migration scenarios.

> [!IMPORTANT]  
> [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] cross-platform availability groups, which include heterogeneous replicas with complete high-availability and disaster recovery support, is available with DH2i DxEnterprise. For more information, see [SQL Server Availability Groups with Mixed Operating Systems](https://support.dh2i.com/docs/guides/dxenterprise/sql_server/mssql-ag-mixed-os-qsg).

## Migrate from SQL Server on Windows

If you want to migrate [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] databases on Windows to [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux, the recommended technique is to use [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] backup and restore.

1. Create a backup of the database on the Windows machine.
1. Transfer the backup file to the target [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Linux machine.
1. Restore the backup on the Linux machine.

For a tutorial on migrating a database with backup and restore, see the following article:

- [Restore a SQL Server database from Windows to Linux](sql-server-linux-migrate-restore-database.md).

It's also possible to export your database to a BACPAC file (a compressed file that contains your database schema and data). If you have a BACPAC file, you can transfer this file to your Linux machine, and then import it to [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. For more information, see the following articles:

- [Export and import a database with SSMS or SqlPackage.exe](sql-server-linux-migrate-ssms.md)

## Migrate from other database servers

You can migrate databases on other database systems to [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux. This includes Microsoft Access, DB2, MySQL, Oracle, and Sybase databases. In this scenario, use the SQL Server Management Assistant (SSMA) to automate the migration to [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux. For more information, see [Use SSMA to migrate databases to SQL Server on Linux](sql-server-linux-migrate-ssma.md).

## Migrate structured data

There are also techniques for importing raw data. You might have structured data files that were exported from other databases or data sources. In this case, you can use the bcp tool to bulk insert the data. Or you can run SQL Server Integration Services (SSIS) on Windows to import the data into a [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] database on Linux. SSIS enables you to run more complex transformations on the data during the import.

## Related content

- [Bulk copy data with bcp](sql-server-linux-migrate-bcp.md)
- [Extract, transform, and load data for SQL Server on Linux with SSIS](sql-server-linux-migrate-ssis.md)
