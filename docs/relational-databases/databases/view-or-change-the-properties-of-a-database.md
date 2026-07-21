---
title: View or Change the Properties of a Database
description: Learn how to view or change the properties of a database in SQL Server with SQL Server Management Studio or Transact-SQL.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 07/20/2026
ms.service: sql
ms.subservice: supportability
ms.topic: how-to
helpviewer_keywords:
  - "displaying databases"
  - "database viewing [SQL Server]"
  - "databases [SQL Server], viewing"
  - "viewing databases"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# View or change the properties of a database

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article describes how to view or change the properties of a database in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] with [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)]. After you change a database property, the modification takes effect immediately.

## Recommendations

- When `AUTO_CLOSE` is `ON`, some columns in the [sys.databases](../system-catalog-views/sys-databases-transact-sql.md) catalog view and `DATABASEPROPERTYEX` function return `NULL` because the database is unavailable to retrieve the data. To resolve this issue, open the database.

## Permissions

To change the properties of a database, you need `ALTER` permission on the database. To view the properties of a database, you need at least membership in the **public** fixed database role.

<a id="SSMSProcedure"></a>

## Use SQL Server Management Studio

1. In **Object Explorer**, connect to an instance of the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)], and then expand that instance.

1. Expand **Databases**, right-click the database to view, and then select **Properties**.

1. In the **Database Properties** dialog box, select a page to view the corresponding information. For example, select the **Files** page to view data and log file information.

<a id="TsqlProcedure"></a>

## Use Transact-SQL

Transact-SQL provides a number of different methods for viewing the properties of a database and for changing the properties of a database. To view the properties of a database, use the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function and the [sys.databases](../system-catalog-views/sys-databases-transact-sql.md) catalog view. To change the properties of a database, use the version of the `ALTER DATABASE` statement for your environment: [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql.md) or [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql.md). To view database scoped properties, use the [sys.database_scoped_configurations](../system-catalog-views/sys-database-scoped-configurations-transact-sql.md) catalog view. To alter database scoped properties, use the [ALTER DATABASE SCOPED CONFIGURATION](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md) statement.

### View a property of a database with the `DATABASEPROPERTYEX` function

1. Connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)] and then connect to the database for which you want to view its properties.

1. From the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. This example uses the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) system function to return the status of the `AUTO_SHRINK` database option in the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] database. A return value of 1 means that the option is set to `ON`, and a return value of 0 means that the option is set to `OFF`.

   ```sql
   SELECT DATABASEPROPERTYEX('AdventureWorks2025', 'IsAutoShrink');
   ```

### View the properties of a database by querying `sys.databases`

1. Connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)] and then connect to the database for which you want to view its properties.

1. From the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. This example queries the [sys.databases](../system-catalog-views/sys-databases-transact-sql.md) catalog view to view several properties of the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] database. This example returns the database ID number (`database_id`), whether the database is read-only or read-write (`is_read_only`), the collation for the database (`collation_name`), and the database compatibility level (`compatibility_level`).

   ```sql
   SELECT database_id,
          is_read_only,
          collation_name,
          compatibility_level
   FROM sys.databases
   WHERE name = 'AdventureWorks2025';
   ```

### View the properties of a database-scoped configuration by querying `sys.databases_scoped_configuration`

1. Connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)] and then connect to the database for which you want to view its properties.

1. From the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. This example queries the [sys.database_scoped_configurations](../system-catalog-views/sys-database-scoped-configurations-transact-sql.md) catalog view to view several properties of the current database.

   ```sql
   SELECT configuration_id,
          name,
          value,
          value_for_secondary
   FROM sys.database_scoped_configurations;
   ```

   For more examples, see [sys.database_scoped_configurations](../system-catalog-views/sys-database-scoped-configurations-transact-sql.md).

### Change the properties of a SQL Server database with `ALTER DATABASE`

1. Connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. From the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window. The example checks the state of snapshot isolation on the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] database, changes the state of the property, and then verifies the change.

   To check the state of snapshot isolation, select the first `SELECT` statement and select **Execute**.

   To change the state of snapshot isolation, select the `ALTER DATABASE` statement and select **Execute**.

   To verify the change, select the second `SELECT` statement, and select **Execute**.

   ```sql
   USE AdventureWorks2025;
   GO

   -- Check the state of the snapshot_isolation_framework in the database.
   SELECT name,
          snapshot_isolation_state,
          snapshot_isolation_state_desc AS [Description]
   FROM sys.databases
   WHERE name = N'AdventureWorks2025';
   GO

   USE master;
   GO

   ALTER DATABASE AdventureWorks2025
       SET ALLOW_SNAPSHOT_ISOLATION ON;
   GO

   -- Check again.
   SELECT name,
          snapshot_isolation_state,
          snapshot_isolation_state_desc AS [Description]
   FROM sys.databases
   WHERE name = N'AdventureWorks2025';
   GO
   ```

### Change the database-scoped properties with `ALTER DATABASE SCOPED CONFIGURATION`

1. Connect to a database in your SQL Server instance.

1. From the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window. The following example sets `MAXDOP` for a secondary database to the value for the primary database.

   ```sql
   ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY
       SET MAXDOP = PRIMARY;
   ```

## Related content

- [sys.databases (Transact-SQL)](../system-catalog-views/sys-databases-transact-sql.md)
- [DATABASEPROPERTYEX (Transact-SQL)](../../t-sql/functions/databasepropertyex-transact-sql.md)
- [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md)
- [ALTER DATABASE SCOPED CONFIGURATION (Transact-SQL)](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md)
- [sys.database_scoped_configurations (Transact-SQL)](../system-catalog-views/sys-database-scoped-configurations-transact-sql.md)
