---
title: "Rename Columns (Database Engine)"
description: "Rename Columns (Database Engine)"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.technology: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "columns [SQL Server], names"
  - "renaming columns"
  - "column names [SQL Server]"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: ""
ms.custom: FY21Q2Fresh
ms.date: "12/02/2021"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Rename columns (Database Engine)

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

You can rename a table column in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].

## <a name="BeforeYouBegin"></a> Before you begin

### <a name="Restrictions"></a> Limitations and restrictions

Renaming a column won't automatically rename references to that column. You must modify any objects that reference the renamed column manually. For example, if you rename a table column and that column is referenced in a trigger, you must modify the trigger to reflect the new column name. Use [sys.sql_expression_dependencies](../../relational-databases/system-catalog-views/sys-sql-expression-dependencies-transact-sql.md) to list dependencies on the object before renaming it.

 
### <a name="Permissions"></a> Permissions

Requires ALTER permission on the object.

## <a name="SSMSProcedure"></a> Using SQL Server Management Studio

### Rename a column using Object Explorer

1. In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].
2. In **Object Explorer**, right-click the table in which you want to rename columns and choose **Rename**.
3. Type a new column name.

### Rename a column using Table Designer

1. In **Object Explorer**, right-click the table to which you want to rename columns and choose **Design**.
2. Under **Column Name**, select the name you want to change and type a new one.
3. On the **File** menu, select **Save _table name_**.

> [!NOTE]
> You can also change the name of a column in the **Column Properties** tab. Select the column whose name you want to change and type a new value for **Name**.

## <a name="TsqlProcedure"></a> Using Transact-SQL

### Rename a column

The following example renames the column `ErrorTime` in the table `dbo.ErrorLog` to `ErrorDateTime` in the `AdventureWorksLT` database.

```sql
EXEC sp_rename 'dbo.ErrorLog.ErrorTime', 'ErrorDateTime', 'COLUMN';
```

Note the output warning, and verify other objects or queries have not been broken:

```output
Caution: Changing any part of an object name could break scripts and stored procedures.
```

For more information, see [sp_rename &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-rename-transact-sql.md).

## Next steps

- [Modify Columns](modify-columns-database-engine.md)
- [sys.sql_expression_dependencies &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-sql-expression-dependencies-transact-sql.md)
- [sp_rename &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-rename-transact-sql.md) 
