---
title: Export and import a database on Linux
description: This article shows how to use SQL Server Management Studio and SqlPackage.exe to export and import a database on SQL Server on Linux.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/15/2024
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
ms.custom:
  - linux-related-content
---
# Export and import a database on Linux with SSMS or SqlPackage.exe on Windows

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This article shows how to use [SQL Server Management Studio](../ssms/download-sql-server-management-studio-ssms.md) (SSMS) and [SqlPackage](../tools/sqlpackage/sqlpackage.md) to export and import a database on SQL Server on Linux. SSMS and SqlPackage.exe are Windows applications, so use this technique when you have a Windows machine that can connect to a remote SQL Server instance on Linux.

You should always install and use the most recent version of SSMS as described in [Use SQL Server Management Studio on Windows to manage SQL Server on Linux](sql-server-linux-manage-ssms.md).

For information about migrating a database from one SQL Server instance to another, see [Migrate a SQL Server database from Windows to Linux using backup and restore](sql-server-linux-migrate-restore-database.md).

## Export a database with SSMS

1. Start SSMS by typing **Microsoft SQL Server Management Studio** in the Windows search box, and then select the desktop app.

   :::image type="content" source="media/sql-server-linux-manage-ssms/ssms.png" alt-text="Screenshot of SQL Server Management Studio.":::

1. Connect to your source database in Object Explorer. The source database can be in Microsoft SQL Server running on-premises or in the cloud, on Linux, Windows, or Docker and Azure SQL Database or Azure Synapse Analytics.

1. Right-click the source database in the Object Explorer, point to **Tasks**, and select **Export Data-Tier Application...**

1. In the export wizard, select **Next**, and then on the **Settings** tab, configure the export to save the BACPAC file to either a local disk location or to an Azure blob.

1. By default, all objects in the database are exported. Select the **Advanced tab** and choose the database objects that you wish to export.

1. Select **Next** and then select **Finish**.

The `.bacpac` file is successfully created at the location you chose, and you're ready to import it into a target database.

## Import a database with SSMS

1. Start SSMS by typing **Microsoft SQL Server Management Studio** in the Windows search box, and then select the desktop app.

   :::image type="content" source="media/sql-server-linux-manage-ssms/ssms.png" alt-text="Screenshot of SQL Server Management Studio again.":::

1. Connect to your target server in Object Explorer. The target server can be Microsoft SQL Server running on-premises or in the cloud, on Linux, Windows, or Docker and Azure SQL Database or Azure Synapse Analytics.

1. Right-click the **Databases** folder in the Object Explorer and select **Import Data-tier Application...**

1. To create the database in your target server, specify a BACPAC file from your local disk, or select the Azure storage account and container to which you uploaded your BACPAC file.

1. Provide the new database name for the database. If you're importing a database on Azure SQL Database, set the Edition of Microsoft Azure SQL Database (service tier), Maximum database size, and Service Objective (performance level).

1. Select **Next** and then select **Finish** to import the BACPAC file into a new database in your target server.

The `.bacpac` file is imported to create a new database in the target server you specified.

## <a id="sqlpackage"></a> SqlPackage command-line option

It's also possible to use the SQL Server Data Tools (SSDT) command-line tool, [SqlPackage](../tools/sqlpackage/sqlpackage.md), to export and import BACPAC files.

The following example command exports a BACPAC file:

```bash
SqlPackage.exe /a:Export /ssn:tcp:<your_server> /sdn:<your_database> /su:<username> /sp:<password> /tf:<path_to_bacpac>
```

Use the following command to import database schema and user data from a `.bacpac` file:

```bash
SqlPackage.exe /a:Import /tsn:tcp:<your_server> /tdn:<your_database> /tu:<username> /tp:<password> /sf:<path_to_bacpac>
```

## Related content

- [What is SQL Server Management Studio (SSMS)?](../ssms/sql-server-management-studio-ssms.md)
- [SqlPackage](../tools/sqlpackage/sqlpackage.md)
