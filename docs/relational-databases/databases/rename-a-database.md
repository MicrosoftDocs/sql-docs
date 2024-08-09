---
title: Rename a database
description: Learn how to rename a user-defined database in SQL Server, Azure SQL Database, or Azure SQL Managed Instance.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 07/25/2024
ms.service: sql
ms.topic: conceptual
helpviewer_keywords:
  - "databases [SQL Server], renaming"
  - "renaming databases"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Rename a database

[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article describes how to rename a user-defined database in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)], [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], or [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS) or [!INCLUDE [tsql](../../includes/tsql-md.md)] (T-SQL). The name of the database can include any characters that follow the rules for identifiers.

> [!NOTE]  
> To rename a database in Azure Synapse Analytics or Parallel Data Warehouse, use the [RENAME](../../t-sql/statements/rename-transact-sql.md) statement.

## Limitations

- System databases can't be renamed.

- The database name can't be changed while other users are accessing the database.

  - Use SSMS Activity Monitor to find other connections to the database, and close them. For more information, see [Open Activity Monitor in SQL Server Management Studio (SSMS)](../performance-monitor/open-activity-monitor-sql-server-management-studio.md).

  - In [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)], you can set a database in single user mode to close any open connections. For more information, see [set the database to single-user mode](set-a-database-to-single-user-mode.md).

  - In [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], you must make sure no other users have an open connection to the database to be renamed.

- Renaming a database doesn't change the physical name of the database files on disk, or the logical names of the files. For more information, see [Database Files and Filegroups](database-files-and-filegroups.md#logical-and-physical-file-names).

- It isn't possible to rename an Azure SQL database configured in an [active geo-replication](/azure/azure-sql/database/active-geo-replication-overview) relationship.

## Permissions

Requires `ALTER` permission on the database.

## Use SQL Server Management Studio (SSMS)

Use the following steps to rename a [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] or Azure SQL database using SSMS.

1. In SSMS, select **Object Explorer**. To open **Object Explorer**, press **F8**. Or on the top menu, select **View** > **Object Explorer**:

1. In **Object Explorer**, connect to an instance of [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)], and then expand that instance.

1. Make sure that there are no open connections to the database. If you're using [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)], you can [set the database to single-user mode](set-a-database-to-single-user-mode.md) to close any open connections and prevent other users from connecting while you're changing the database name.

1. In Object Explorer, expand **Databases**, right-click the database to rename, and then select **Rename**.

1. Enter the new database name, and then select **OK**

1. If the database was your default database, see [Reset your default database after rename](#reset-your-default-database-after-rename).

1. Refresh the database list in Object Explorer.

## Use Transact-SQL

### Rename a SQL Server database by placing it in single-user mode

Use the following steps to rename a [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] database using T-SQL in SSMS, including the steps to place the database in single-user mode. After the rename, this example places the database back in multi-user mode.

1. Connect to the `master` database for your instance.

1. Open a query window.

1. Copy and paste the following example into the query window and select **Execute**. This example changes the name of the `MyTestDatabase` database to `MyTestDatabaseCopy`.

   > [!WARNING]  
   > To quickly obtain exclusive access, the code sample uses the termination option `WITH ROLLBACK IMMEDIATE`. This cases all incomplete transactions to be rolled back and any other connections to the `MyTestDatabase` database to be immediately disconnected.

   ```sql
   USE master;
   GO
   ALTER DATABASE MyTestDatabase SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
   GO
   ALTER DATABASE MyTestDatabase MODIFY NAME = MyTestDatabaseCopy;
   GO
   ALTER DATABASE MyTestDatabaseCopy SET MULTI_USER;
   GO
   ```

1. Optionally, if the database was your default database, see [Reset your default database after rename](#reset-your-default-database-after-rename).

### Rename an Azure SQL Database database

Use the following steps to rename an Azure SQL database using T-SQL in SQL Server Management Studio.

1. Connect to the `master` database for your instance.

1. Open a query window.

1. Make sure that no one is using the database.

1. Copy and paste the following example into the query window and select **Execute**. This example changes the name of the `MyTestDatabase` database to `MyTestDatabaseCopy`.

   ```sql
   ALTER DATABASE MyTestDatabase MODIFY NAME = MyTestDatabaseCopy;
   ```

## Backup after renaming a database

After renaming a database in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)], back up the `master` database. In Azure SQL Database, this process isn't needed, as backups occur automatically.

## Reset your default database after rename

If the database you're renaming was set as the default database of a [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] login, they might encounter Error 4064, `Can't open user default database`. Use the following command to change the default to the renamed database:

```sql
USE [master]
GO
ALTER LOGIN [login] WITH DEFAULT_DATABASE=[new-database-name];
GO
```

## Related content

- [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md)
- [Database Identifiers](database-identifiers.md)
