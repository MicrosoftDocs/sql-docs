---
title: Create check constraints
description: Learn how to can create a check constraint in a table to specify the data values that are acceptable in one or more columns in the SQL Server Database Engine.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 04/22/2024
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords:
  - "table constraints [SQL Server]"
  - "attaching check constraints"
  - "columns [SQL Server], constraints"
  - "constraints [SQL Server], check"
  - "CHECK constraints, attaching"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Create check constraints

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

You can create a check constraint in a table to specify the data values that are acceptable in one or more columns in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)]. For more information on adding column constraints, see [ALTER TABLE column_constraint](../../t-sql/statements/alter-table-column-constraint-transact-sql.md).

For more information, see [Unique constraints and check constraints](unique-constraints-and-check-constraints.md).

## Remarks

To query existing check constraints, use the [sys.check_constraints](../system-catalog-views/sys-check-constraints-transact-sql.md) system catalog view.

## Permissions

Requires `ALTER` permissions on the table.

## <a id="SSMSProcedure"></a> Use SQL Server Management Studio

1. In **Object Explorer**, expand the table to which you want to add a check constraint, right-click **Constraints** and select **New Constraint**.

1. In the **Check Constraints** dialog box, select in the **Expression** field and then select the ellipses **(...)**.

1. In the **Check Constraint Expression** dialog box, type the SQL expressions for the check constraint. For example, to limit the entries in the `SellEndDate` column of the `Product` table to a value that is either greater than or equal to the date in the `SellStartDate` column, or is a `NULL` value, type:

   ```sql
   SellEndDate >= SellStartDate
   ```

   Or, to require entries in the `zip` column to be five digits, type:

   ```sql
   zip LIKE '[0-9][0-9][0-9][0-9][0-9]'
   ```

   > [!NOTE]  
   > Make sure to enclose any non-numeric constraint values in single quotation marks (`'`).

1. Select **OK**.

1. In the **Identity** category, you can change the name of the check constraint and add a description (extended property) for the constraint.

1. In the **Table Designer** category, you can set when the constraint is enforced.

   | Action | Select `Yes` for the following options |
   | --- | --- |
   | Test the constraint on data that existed before you created the constraint | **Check Existing Data On Creation Or Enabling** |
   | Enforce the constraint whenever a replication operation occurs on this table | **Enforce For Replication** |
   | Enforce the constraint whenever a row of this table is inserted or updated | **Enforce For INSERTs And UPDATEs** |

1. Select **Close**.

## <a id="TsqlProcedure"></a> Use Transact-SQL

1. In **Object Explorer**, connect to an instance of [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. On the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**.

   First, create the constraint.

   ```sql
   ALTER TABLE dbo.DocExc
   ADD ColumnD INT NULL CONSTRAINT CHK_ColumnD_DocExc CHECK (
       ColumnD > 10
       AND ColumnD < 50
   );
   GO
   ```

   To test the constraint, first add values that pass the check constraint.

   ```sql
   INSERT INTO dbo.DocExc (ColumnD) VALUES (49);
   ```

   Next, attempt to add values that fail the check constraint.

   ```sql
   INSERT INTO dbo.DocExc (ColumnD) VALUES (55);
   ```

## Related content

- [ALTER TABLE (Transact-SQL)](../../t-sql/statements/alter-table-transact-sql.md)
- [ALTER TABLE column_constraint (Transact-SQL)](../../t-sql/statements/alter-table-column-constraint-transact-sql.md)
- [Unique constraints and check constraints](unique-constraints-and-check-constraints.md)
- [Column Properties (General Page)](column-properties-general-page.md)
- [Add Columns to a Table (Database Engine)](add-columns-to-a-table-database-engine.md)
- [Specify Default Values for Columns](specify-default-values-for-columns.md)
- [Create unique constraints](create-unique-constraints.md)
