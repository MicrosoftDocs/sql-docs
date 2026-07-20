---
title: "Increase the Size of a Database"
description: "Increase the Size of a Database"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 11/21/2024
ms.service: sql
ms.topic: how-to
helpviewer_keywords:
  - "databases [SQL Server], size"
  - "increasing database size"
  - "database size [SQL Server], increasing"
  - "size [SQL Server], databases"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017"
---
# Increase the size of a database

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to increase the size of a database in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)]. The database is expanded by either increasing the size of an existing data or log file, or by adding a new file to the database.

## Limitations

You can't add or remove a file while a `BACKUP` statement is running.

## Permissions

Requires `ALTER` permission on the database.

<a id="SSMSProcedure"></a>

## Use SQL Server Management Studio

1. In **Object Explorer**, connect to an instance of the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)], and then expand that instance.

1. Expand **Databases**, right-click the database to increase, and then select **Properties**.

1. In **Database Properties**, select the **Files** page.

1. To increase the size of an existing file, increase the value in the **Initial Size (MB)** column for the file. You must increase the size of the database by at least 1 megabyte.

1. To increase the size of the database by adding a new file, select **Add** and then enter the values for the new file. For more information, see [Add Data or Log Files to a Database](add-data-or-log-files-to-a-database.md).

1. Select **OK**.

<a id="TsqlProcedure"></a>

## Use Transact-SQL

1. Connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. From the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**.

   This example changes the size of the file `test1dat3` to 200 MB.

   ```sql
   USE master;
   GO

   ALTER DATABASE AdventureWorks2022
       MODIFY FILE (NAME = test1dat3, SIZE = 200 MB);
   GO
   ```

For more examples, see [ALTER DATABASE (Transact-SQL) File and Filegroup Options](../../t-sql/statements/alter-database-transact-sql-file-and-filegroup-options.md).

## Related content

- [Add Data or Log Files to a Database](add-data-or-log-files-to-a-database.md)
- [Shrink a database](shrink-a-database.md)
