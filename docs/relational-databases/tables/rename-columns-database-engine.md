---
title: "Rename columns (Database Engine)"
description: Learn how to rename a table column in the SQL Server Database Engine with SQL Server Management Studio or Transact-SQL.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 07/22/2024
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords:
  - "columns [SQL Server], names"
  - "renaming columns"
  - "column names [SQL Server]"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Rename columns (Database Engine)

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

You can rename a table column in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)].

## Limitations

Renaming a column doesn't automatically rename references to that column. You must modify any objects that reference the renamed column manually. For example, if you rename a table column and that column is referenced in a trigger, you must modify the trigger to reflect the new column name. Use [sys.sql_expression_dependencies](../system-catalog-views/sys-sql-expression-dependencies-transact-sql.md) to list dependencies on the object before renaming it.

Renaming a column doesn't automatically update the metadata for any objects which `SELECT` all columns (using `*`) from that table. For example, if you rename a table column, and that column is referenced by a non-schema-bound view or function that selects all columns (using `*`), the metadata for the view or function continues to reflect the original column name. Refresh the metadata using [sp_refreshsqlmodule](../system-stored-procedures/sp-refreshsqlmodule-transact-sql.md) or [sp_refreshview](../system-stored-procedures/sp-refreshview-transact-sql.md).

## Permissions

Requires `ALTER` permission on the object.

## <a id="SSMSProcedure"></a> Use SQL Server Management Studio

### Rename a column using Object Explorer

1. In **Object Explorer**, connect to an instance of [!INCLUDE [ssDE](../../includes/ssde-md.md)].
1. In **Object Explorer**, right-click the table in which you want to rename columns and choose **Rename**.
1. Type a new column name.

### Rename a column using table designer

1. In **Object Explorer**, right-click the table to which you want to rename columns and choose **Design**.
1. Under **Column Name**, select the name you want to change and type a new one.
1. On the **File** menu, select **Save _table name_**.

You can also change the name of a column in the **Column Properties** tab. Select the column whose name you want to change and type a new value for **Name**.

## <a id="TsqlProcedure"></a> Use Transact-SQL

### Rename a column

The following example renames the column `ErrorTime` in the table `dbo.ErrorLog` to `ErrorDateTime` in the `AdventureWorksLT` database.

```sql
EXEC sp_rename 'dbo.ErrorLog.ErrorTime', 'ErrorDateTime', 'COLUMN';
```

Note the output warning, and verify other objects or queries aren't broken:

```output
Caution: Changing any part of an object name could break scripts and stored procedures.
```

For more information, see [sp_rename](../system-stored-procedures/sp-rename-transact-sql.md).

## Related content

- [Modify columns](modify-columns-database-engine.md)
- [sys.sql_expression_dependencies (Transact-SQL)](../system-catalog-views/sys-sql-expression-dependencies-transact-sql.md)
- [sp_rename (Transact-SQL)](../system-stored-procedures/sp-rename-transact-sql.md)
