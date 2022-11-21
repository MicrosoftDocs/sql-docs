---
title: "Rename Columns (Database Engine)"
description: "Rename Columns (Database Engine)"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 07/27/2022
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
ms.custom: FY22Q2Fresh
helpviewer_keywords:
  - "columns [SQL Server], names"
  - "renaming columns"
  - "column names [SQL Server]"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Rename columns (Database Engine)

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

You can rename a table column in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].

## <a id="Restrictions"></a> Limitations and restrictions

Renaming a column won't automatically rename references to that column. You must modify any objects that reference the renamed column manually. For example, if you rename a table column and that column is referenced in a trigger, you must modify the trigger to reflect the new column name. Use [sys.sql_expression_dependencies](../../relational-databases/system-catalog-views/sys-sql-expression-dependencies-transact-sql.md) to list dependencies on the object before renaming it.

Renaming a column doesn't automatically update the metadata for any objects which SELECT all columns (using the `*`) from that table. For example, if you rename a table column and that column is referenced by a non-schema-bound view or function that SELECTs all columns (using the `*`), the metadata for the view or function continues to reflect the original column name. Refresh the metadata using [sp_refreshsqlmodule](../../relational-databases/system-stored-procedures/sp-refreshsqlmodule-transact-sql.md) or [sp_refreshview](../../relational-databases/system-stored-procedures/sp-refreshview-transact-sql.md).

## Permissions

Requires ALTER permission on the object.

## <a id="SSMSProcedure"></a> Use SQL Server Management Studio

### Rename a column using Object Explorer

1. In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].
2. In **Object Explorer**, right-click the table in which you want to rename columns and choose **Rename**.
3. Type a new column name.

### Rename a column using table designer

1. In **Object Explorer**, right-click the table to which you want to rename columns and choose **Design**.
2. Under **Column Name**, select the name you want to change and type a new one.
3. On the **File** menu, select **Save _table name_**.

You can also change the name of a column in the **Column Properties** tab. Select the column whose name you want to change and type a new value for **Name**.

## <a id="TsqlProcedure"></a> Use Transact-SQL

### Rename a column

The following example renames the column `ErrorTime` in the table `dbo.ErrorLog` to `ErrorDateTime` in the `AdventureWorksLT` database.

```sql
EXEC sp_rename 'dbo.ErrorLog.ErrorTime', 'ErrorDateTime', 'COLUMN';
```

Note the output warning, and verify other objects or queries haven't been broken:

```output
Caution: Changing any part of an object name could break scripts and stored procedures.
```

For more information, see [sp_rename &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-rename-transact-sql.md).

## Next steps

- [Modify columns](modify-columns-database-engine.md)
- [sys.sql_expression_dependencies &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-sql-expression-dependencies-transact-sql.md)
- [sp_rename &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-rename-transact-sql.md) 
