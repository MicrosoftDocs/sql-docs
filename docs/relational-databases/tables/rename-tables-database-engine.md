---
title: Rename tables (Database Engine)
description: "Learn how to rename a database table."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 07/19/2024
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords:
  - "table renaming [SQL Server]"
  - "table names [SQL Server]"
  - "tables [SQL Server], Visual Database Tools"
  - "renaming tables"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# Rename tables (Database Engine)

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

This article covers how to rename a table in a database.

To rename a table in Azure Synapse Analytics or Parallel Data Warehouse, use [RENAME OBJECT](../../t-sql/statements/rename-transact-sql.md).

## <a id="Restrictions"></a> Limitations

> [!CAUTION]
> Think carefully before you rename a table. If existing queries, views, user-defined functions, stored procedures, or programs refer to that table, the name modification makes these objects invalid.

Renaming a table doesn't automatically rename references to that table. You must manually modify any objects that reference the renamed table. For example, if you rename a table and that table is referenced in a trigger, you must modify the trigger to reflect the new table name. Use [sys.sql_expression_dependencies](../../relational-databases/system-catalog-views/sys-sql-expression-dependencies-transact-sql.md) to list dependencies on the table before renaming it.

## Permissions

Requires ALTER permission on the table.

## <a id="SSMSProcedure"></a> Use SQL Server Management Studio

Always use the latest version of [SQL Server Management Studio (SSMS)](../../ssms/download-sql-server-management-studio-ssms.md).

### Rename a table

1. In **Object Explorer**, right-click the table you want to rename and choose **Design** from the shortcut menu.

1. From the **View** menu, choose **Properties**.

1. In the field for the **Name** value in the **Properties** window, type a new name for the table.

1. To cancel this action, press the ESC key before leaving this field.

1. From the **File** menu, choose **Save _table name_**.

### Rename a table

1. In **Object Explorer**, connect to an instance of [!INCLUDE [ssDE](../../includes/ssde-md.md)].  

1. On the Standard bar, select **New Query**.  

1. The following example renames the `SalesTerritory` table to `SalesTerr` in the `Sales` schema. Copy and paste the following example into the query window and select **Execute**.

    ```sql
    USE AdventureWorks2022;
    GO
    EXEC sp_rename 'Sales.SalesTerritory', 'SalesTerr';
    ```

> [!IMPORTANT]
> The `sp_rename` syntax for `@objname` should include the schema of the old table name, but `@newname` does not include the schema name when setting the new table name.

## Related content

- [sp_rename (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-rename-transact-sql.md)
- [sys.sql_expression_dependencies](../../relational-databases/system-catalog-views/sys-sql-expression-dependencies-transact-sql.md)
