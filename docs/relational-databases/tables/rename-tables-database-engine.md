---
title: Rename Tables (Database Engine)
description: "Rename Tables (Database Engine)"
ms.service: sql
ms.reviewer: ""
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "table renaming [SQL Server]"
  - "table names [SQL Server]"
  - "tables [SQL Server], Visual Database Tools"
  - "renaming tables"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom: ""
ms.date: "07/08/2021"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# Rename Tables (Database Engine)

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

Rename a table in SQL Server or Azure SQL Database.

To rename a table in Azure Synapse Analytics or Parallel Data Warehouse, use the t-sql [RENAME OBJECT](../../t-sql/statements/rename-transact-sql.md) statement. 

> [!CAUTION]
> Think carefully before you rename a table. If existing queries, views, user-defined functions, stored procedures, or programs refer to that table, the name modification makes these objects invalid.

 **In This Topic**

- **Before you begin:**

    [Limitations and Restrictions](#Restrictions)

    [Security](#Security)

- **To rename a table, using:**

    [SQL Server Management Studio](#SSMSProcedure)

    [Transact-SQL](/previous-versions/sql/sql-server-2008-r2/ms188351(v=sql.105))

## <a name="BeforeYouBegin"></a> Before You Begin

### <a name="Restrictions"></a> Limitations and Restrictions  
Renaming a table doesn't automatically rename references to that table. You must manually modify any objects that reference the renamed table. For example, if you rename a table and that table is referenced in a trigger, you must modify the trigger to reflect the new table name. Use [sys.sql_expression_dependencies](../../relational-databases/system-catalog-views/sys-sql-expression-dependencies-transact-sql.md) to list dependencies on the table before renaming it.

### <a name="Security"></a> Security

#### <a name="Permissions"></a> Permissions

Requires ALTER permission on the table.

## <a name="SSMSProcedure"></a> Using SQL Server Management Studio

#### To rename a table

1. In Object Explorer, right-click the table you want to rename and choose **Design** from the shortcut menu.

2. From the **View** menu, choose **Properties**.

3. In the field for the **Name** value in the **Properties** window, type a new name for the table.

4. To cancel this action, press the ESC key before leaving this field.

5. From the File menu, choose **Save** _table name_.


#### To rename a table

1. In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  

2. On the Standard bar, select **New Query**.  

3. The following example renames the `SalesTerritory` table to `SalesTerr` in the `Sales` schema. Copy and paste the following example into the query window and select **Execute**.

    ```sql
    USE AdventureWorks2012;
    GO
    EXEC sp_rename 'Sales.SalesTerritory', 'SalesTerr';
    ```

> [!IMPORTANT]
> Note that the `sp_rename` syntax for @objname should include the schema of the old table name, but @newname does not include the schema name when setting the new table name.

For more examples, see [sp_rename &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-rename-transact-sql.md).