---
title: Enable Stretch Database for a table
description: Enable Stretch Database for a table
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 07/25/2022
ms.service: sql-server-stretch-database
ms.topic: conceptual
ms.custom: seo-dt-2019
helpviewer_keywords:
  - "Stretch Database, enabling table"
  - "enabling table for Stretch Database"
---
# Enable Stretch Database for a table

[!INCLUDE [sqlserver2016-windows-only](../../includes/applies-to-version/sqlserver2016-windows-only.md)]

> [!IMPORTANT]  
> Stretch Database is deprecated in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]. [!INCLUDE [ssNoteDepFutureAvoid-md](../../includes/ssnotedepfutureavoid-md.md)]

To configure a table for Stretch Database, select **Stretch > Enable** for a table in SQL Server Management Studio to open the **Enable Table for Stretch** wizard. You can also use Transact-SQL to enable Stretch Database on an existing table, or to create a new table with Stretch Database enabled.

> [!IMPORTANT]  
> Stretch Database support is removed in SQL Server Management Studio v19. To manage Stretch Database, you can use SQL Server Management Studio v18.9.1 or lower.

- If you store cold data in a separate table, you can migrate the entire table.

- If your table contains both hot and cold data, you can specify a filter function to select the rows to migrate.

**Prerequisites**. If you select **Stretch > Enable** for a table, and you haven't yet enabled Stretch Database for the database, the wizard first configures the database for Stretch Database. Follow the steps in [Get started by running the Enable Database for Stretch Wizard](get-started-by-running-the-enable-database-for-stretch-wizard.md) instead of the steps in this article.

**Permissions**. Enabling Stretch Database on a database or a table requires db_owner permissions. Enabling Stretch Database on a table also requires ALTER permissions on the table.

> [!NOTE]
> Later, if you disable Stretch Database, remember that disabling Stretch Database for a table or for a database does not delete the remote object. If you want to delete the remote table or the remote database, you have to drop it by using the Azure management portal. The remote objects continue to incur Azure costs until you delete them manually.

## Use the wizard

### 1. Launch the wizard

1. In SQL Server Management Studio, in Object Explorer, select the table on which you want to enable Stretch.

1. Right-click and select **Stretch > Enable** to launch the wizard.

### 2. Introduction

Review the purpose of the wizard and the prerequisites.

### 3. Select database tables

Confirm that the table you want to enable is displayed and selected.

You can migrate an entire table or you can specify a filter function in the wizard. If you want to use a different type of filter function to select rows to migrate, do one of the following things.

- Exit the wizard and run the ALTER TABLE statement to enable Stretch for the table and to specify a filter function.

- Run the ALTER TABLE statement to specify a filter function after you exit the wizard. For the required steps, see [Add a filter function after running the Wizard](select-rows-to-migrate-by-using-a-filter-function-stretch-database.md#addafterwiz).

The ALTER TABLE syntax is described later in this article.

### 4. Summary

Review the values that you entered and the options that you selected in the wizard. Then select **Finish** to enable Stretch.

### 5. Results

Review the results.

## Use Transact-SQL

You can enable Stretch Database for an existing table or create a new table with Stretch Database enabled by using Transact-SQL.

### Options

Use the following options when you run CREATE TABLE or ALTER TABLE to enable Stretch Database on a table.

- Optionally, use the `FILTER_PREDICATE = <function>` clause to specify a function to select rows to migrate if the table contains both hot and cold data. The predicate must call an inline table-valued function. For more info, see [Select rows to migrate by using a filter function](select-rows-to-migrate-by-using-a-filter-function-stretch-database.md). If you don't specify a filter function, the entire table is migrated.

  > [!IMPORTANT]
  > If you provide a filter function that performs poorly, data migration also performs poorly. Stretch Database applies the filter function to the table by using the CROSS APPLY operator.

- Specify `MIGRATION_STATE = OUTBOUND` to start data migration immediately or `MIGRATION_STATE = PAUSED` to postpone the start of data migration.

### Enable Stretch Database for an existing table

To configure an existing table for Stretch Database, run the ALTER TABLE command.

The following example migrates the entire table and begins data migration immediately.

```sql
USE [<Stretch-enabled database name>];
GO
ALTER TABLE [<table name>]
    SET ( REMOTE_DATA_ARCHIVE = ON ( MIGRATION_STATE = OUTBOUND ) );
GO
```

The following example migrates only the rows identified by the `dbo.fn_stretchpredicate` inline table-valued function and postpones data migration. For more info about the filter function, see [Select rows to migrate by using a filter function](select-rows-to-migrate-by-using-a-filter-function-stretch-database.md).

```sql
USE [<Stretch-enabled database name>];
GO
ALTER TABLE [<table name>]
    SET ( REMOTE_DATA_ARCHIVE = ON (
        FILTER_PREDICATE = dbo.fn_stretchpredicate(),
        MIGRATION_STATE = PAUSED ) );
GO
```

For more info, see [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md).

### Create a new table with Stretch Database enabled

To create a new table with Stretch Database enabled, run the CREATE TABLE command.

The following example migrates the entire table and begins data migration immediately.

```sql
USE [<Stretch-enabled database name>];
GO
CREATE TABLE [<table name>]
    (
        col1 int
        /* replace the sample "col1" column shown above, with the actual list of columns */
    )
    WITH ( REMOTE_DATA_ARCHIVE = ON ( MIGRATION_STATE = OUTBOUND ) );
GO
```

The following example migrates only the rows identified by the `dbo.fn_stretchpredicate` inline table-valued function and postpones data migration. For more info about the filter function, see [Select rows to migrate by using a filter function](select-rows-to-migrate-by-using-a-filter-function-stretch-database.md).

```sql
USE [<Stretch-enabled database name>];
GO
CREATE TABLE [<table name>]
    (
        col1 int
        /* replace the sample "col1" column shown above, with the actual list of columns */
    )
    WITH ( REMOTE_DATA_ARCHIVE = ON (
        FILTER_PREDICATE = dbo.fn_stretchpredicate(),
        MIGRATION_STATE = PAUSED ) );
GO
```

For more info, see [CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md).

## See also

- [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)
- [CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md)
