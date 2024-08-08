---
title: "Specify computed columns in a table"
description: "Learn how to create and define computed columns in a database table."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 07/19/2024
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords:
  - "computed columns, define"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Specify computed columns in a table

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

A computed column is a virtual column that isn't physically stored in the table, unless the column is marked `PERSISTED`. A computed column expression can use data from other columns to calculate a value for the column to which it belongs. You can specify an expression for a computed column in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS) or [!INCLUDE [tsql](../../includes/tsql-md.md)] (T-SQL).

## Limitations

- A computed column can't be used as a `DEFAULT` or `FOREIGN KEY` constraint definition or with a `NOT NULL` constraint definition. However, if the computed column value is defined by a deterministic expression and the data type of the result is allowed in index columns, a computed column can be used as a key column in an index or as part of any `PRIMARY KEY` or `UNIQUE` constraint.

   For example, if the table has integer columns `a` and `b`, a computed column defined as `a + b` might be indexed, but computed column defined as `a + DATEPART(dd, GETDATE())` can't be indexed, because the value might change in subsequent invocations.
- A computed column can't be the target of an INSERT or UPDATE statement.
- `SET QUOTED_IDENTIFIER` must be `ON` when you're creating or changing indexes on computed columns or indexed views. For more information, see [SET QUOTED_IDENTIFIER (Transact-SQL)](../../t-sql/statements/set-quoted-identifier-transact-sql.md).

## <a id="Security"></a> Permissions

Requires ALTER permission on the table.

## <a id="SSMSProcedure"></a> Use SQL Server Management Studio

### <a id="NewColumn"></a> Add a new computed column

1. In **Object Explorer**, expand the table for which you want to add the new computed column. Right-click **Columns** and select **New Column**.
1. Enter the column name and accept the default data type (**nchar**(10)). The [!INCLUDE [ssDE](../../includes/ssde-md.md)] determines the data type of the computed column by applying the rules of data type precedence to the expressions specified in the formula. For example, if the formula references a column of type **money** and a column of type **int**, the computed column will be of type **money** because that data type has the higher precedence. For more information, see [Data Type Precedence (Transact-SQL)](../../t-sql/data-types/data-type-precedence-transact-sql.md).
1. In the **Column Properties** tab, expand the **Computed Column Specification** property.
1. In the **(Formula)** child property, enter the expression for this column in the grid cell to the right. For example, in a `SalesTotal` column, the formula you enter might be `SubTotal+TaxAmt+Freight`, which adds the value in these columns for each row in the table.

   > [!IMPORTANT]
   > When a formula combines two expressions of different data types, the rules for data type precedence specify that the data type with the lower precedence is converted to the data type with the higher precedence. If the conversion is not a supported implicit conversion, the error `Error validating the formula for column column_name.` is returned. Use the `CAST` or `CONVERT` function to resolve the data type conflict. For example, if a column of type **nvarchar** is combined with a column of type **int**, the integer type must be converted to **nvarchar** as shown in this formula `('Prod'+CONVERT(nvarchar(23),ProductID))`. For more information, see [CAST and CONVERT (Transact-SQL)](../../t-sql/functions/cast-and-convert-transact-sql.md).

1. Indicate whether the data is persisted by choosing **Yes** or **No** from the dropdown for the **Is Persisted** child property.

1. On the **File** menu, select **Save** _table name_.

#### <a id="to-add-a-computed-column-definition-to-an-existing-column"></a> Add a computed column definition to an existing column

1. In **Object Explorer**, right-click the table with the column for which you want to change and expand the **Columns** folder.
1. Right-click the column for which you want to specify a computed column formula and select **Delete**. Select **OK**.
1. Add a new column and specify the computed column formula by following the previous procedure to add a new computed column.

## <a id="TsqlProcedure"></a> Use Transact-SQL

### <a id="to-add-a-computed-column-when-creating-a-table"></a> Add a computed column when creating a table

The following example creates a table with a computed column that multiplies the value in the `QtyAvailable` column times the value in the `UnitPrice` column.

```sql
CREATE TABLE dbo.Products
   (
      ProductID int IDENTITY (1,1) NOT NULL
      , QtyAvailable smallint
      , UnitPrice money
      , InventoryValue AS QtyAvailable * UnitPrice
    );

-- Insert values into the table.
INSERT INTO dbo.Products (QtyAvailable, UnitPrice)
   VALUES (25, 2.00), (10, 1.5);

-- Display the rows in the table.
SELECT ProductID, QtyAvailable, UnitPrice, InventoryValue
FROM dbo.Products;

-- Update values in the table.
UPDATE dbo.Products 
SET UnitPrice = 2.5
WHERE ProductID = 1;

-- Display the rows in the table, and the new values for UnitPrice and InventoryValue.
SELECT ProductID, QtyAvailable, UnitPrice, InventoryValue
FROM dbo.Products;
```

### <a id="to-add-a-new-computed-column-to-an-existing-table"></a> Add a new computed column to an existing table

The following example adds a new column to the table created in the previous example.

```sql
ALTER TABLE dbo.Products ADD RetailValue AS (QtyAvailable * UnitPrice * 1.5);
```

Optionally, add the PERSISTED argument to physically store the computed values in the table:

```sql
ALTER TABLE dbo.Products ADD RetailValue AS (QtyAvailable * UnitPrice * 1.5) PERSISTED;
```

### <a id="to-change-an-existing-column-to-a-computed-column"></a> Change an existing column to a computed column

The following example modifies the column added in the previous example.

```sql
ALTER TABLE dbo.Products DROP COLUMN RetailValue;
GO

ALTER TABLE dbo.Products ADD RetailValue AS (QtyAvailable * UnitPrice * 1.5);
GO
```

## Related content

- [ALTER TABLE (Transact-SQL)](../../t-sql/statements/alter-table-transact-sql.md)
- [ALTER TABLE computed_column_definition (Transact-SQL)](../../t-sql/statements/alter-table-computed-column-definition-transact-sql.md)
