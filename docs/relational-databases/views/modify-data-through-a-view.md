---
title: "Modify Data Through a View"
description: Learn how to modify data through a view.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 09/26/2025
ms.service: sql
ms.subservice: table-view-index
ms.topic: how-to
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "data modifications [SQL Server], views"
  - "views [SQL Server], modifying data through"
  - "modifying data [SQL Server], views"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Modify data through a view

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

You can modify the data of an underlying base table in SQL Server by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)].

## Limitations

See the section 'Updatable Views' in [CREATE VIEW](../../t-sql/statements/create-view-transact-sql.md).

## Permissions

Requires `UPDATE`, `INSERT`, or `DELETE` permissions on the target table, depending on the action being performed.

<a id="SSMSProcedure"></a>

## Use SQL Server Management Studio

### Modify table data through a view

1. In **Object Explorer**, expand the database that contains the view and then expand **Views**.

1. Right-click the view and select **Edit Top 200 Rows**.

1. You might need to modify the `SELECT` statement in the **SQL** pane to return the rows to be modified.

1. In the **Results** pane, locate the row to be changed or deleted. To delete the row, right-click the row and select **Delete**. To change data in one or more columns, modify the data in the column.

   You can't delete a row if the view references more than one base table. You can only update columns that belong to a single base table.

1. To insert a row, scroll down to the end of the rows and insert the new values.

   You can't insert a row if the view references more than one base table.

<a id="TsqlProcedure"></a>

## Use Transact-SQL

### Update table data through a view

1. In **Object Explorer**, connect to an instance of [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. On the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. This example changes the value in the `StartDate` and `EndDate` columns for a specific employee by referencing columns in the view `HumanResources.vEmployeeDepartmentHistory`. This view returns values from two tables. This statement succeeds because the columns being modified are from only one of the base tables.

   ```sql
   USE AdventureWorks2022;
   GO

   UPDATE HumanResources.vEmployeeDepartmentHistory
       SET StartDate = '20110203',
           EndDate   = GETDATE()
   WHERE LastName = N'Smith'
         AND FirstName = 'Samantha';
   GO
   ```

For more information, see [UPDATE](../../t-sql/queries/update-transact-sql.md).

### Insert table data through a view

1. In **Object Explorer**, connect to an instance of [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. On the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. The example inserts a new row into the base table `HumanResources.Department` by specifying the relevant columns from the view `HumanResources.vEmployeeDepartmentHistory`. The statement succeeds because only columns from a single base table are specified and the other columns in the base table have default values.

   ```sql
   USE AdventureWorks2022;
   GO

   INSERT INTO HumanResources.vEmployeeDepartmentHistory (Department, GroupName)
   VALUES ('MyDepartment', 'MyGroup');
   GO
   ```

For more information, see [INSERT](../../t-sql/statements/insert-transact-sql.md).

## Related content

- [UPDATE (Transact-SQL)](../../t-sql/queries/update-transact-sql.md)
- [INSERT (Transact-SQL)](../../t-sql/statements/insert-transact-sql.md)
