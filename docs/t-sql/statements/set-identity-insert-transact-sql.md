---
title: SET IDENTITY_INSERT (Transact-SQL)
description: Transact-SQL reference for the SET IDENTITY_INSERT statement. When set to ON, this permits inserting explicit values into the identity column of a table.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest, procha
ms.date: 08/26/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "SET IDENTITY_INSERT"
  - "SET_IDENTITY_INSERT_TSQL"
  - "IDENTITY_INSERT_TSQL"
  - "IDENTITY_INSERT"
helpviewer_keywords:
  - "IDENTITY_INSERT option"
  - "SET IDENTITY_INSERT statement"
  - "identity values [SQL Server], explicit values"
  - "identity columns [SQL Server], explicit values"
dev_langs:
  - TSQL
ai-usage: ai-assisted
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =azure-sqldw-latest || =fabric || =fabric-sqldb"
---
# SET IDENTITY_INSERT (Transact-SQL)

::: moniker range="=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =azure-sqldw-latest || =fabric-sqldb"

[!INCLUDE [sql-asdb-asdbmi-asa-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-fabricsqldb.md)]

By using this statement, you can insert explicit values into the [IDENTITY](../functions/identity-function-transact-sql.md) column of a table.

This article and the `IDENTITY` syntax differ on different platforms of the [SQL Database Engine](../../database-engine/sql-database-engine.md). For Microsoft Fabric Data Warehouse, [select Fabric Data Warehouse in the version dropdown list](set-identity-insert-transact-sql.md?view=fabric&preserve-view=true).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
SET IDENTITY_INSERT [ [ database_name . ] schema_name . ] table_name { ON | OFF }
```

## Arguments

#### *database_name*

The name of the database where the specified table resides.

#### *schema_name*

The name of the schema that contains the table.

#### *table_name*

The name of a table with an identity column.

## Remarks

At any time, only one table in a session can have the `IDENTITY_INSERT` property set to `ON`. If a table already has this property set to `ON`, and you issue a `SET IDENTITY_INSERT ON` statement for another table, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] returns an error message that states `SET IDENTITY_INSERT` is already `ON`, and reports the table for which `ON` is set.

- When the `increment` argument of the [IDENTITY function](../functions/identity-function-transact-sql.md) is positive, and the value inserted is larger than the current identity value for the table, the SQL Database Engine automatically uses the new inserted value as the current identity value.
- When the `increment` argument of the [IDENTITY function](../functions/identity-function-transact-sql.md) is negative, and the value inserted is smaller than the current identity value for the table, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] automatically uses the new inserted value as the current identity value.

The setting of `SET IDENTITY_INSERT` is set at execute or run time and not at parse time.

## Permissions

You must own the table or have `ALTER` permission on the table.

## Examples

The following example creates a table with an identity column and shows how the `SET IDENTITY_INSERT` setting can be used to fill a gap in the identity values caused by a `DELETE` statement.

```sql
USE AdventureWorks2022;
GO
```

Create tool table.

```sql
CREATE TABLE dbo.Tool
(
    ID INT IDENTITY NOT NULL PRIMARY KEY,
    Name VARCHAR (40) NOT NULL
);
GO
```

Insert values into products table.

```sql
INSERT INTO dbo.Tool (Name)
VALUES ('Screwdriver'),
    ('Hammer'),
    ('Saw'),
    ('Shovel');
GO
```

Create a gap in the identity values.

```sql
DELETE dbo.Tool
WHERE Name = 'Saw';
GO

SELECT *
FROM dbo.Tool;
GO
```

Try to insert an explicit ID value of 3.

```sql
INSERT INTO dbo.Tool (ID, Name)
VALUES (3, 'Garden shovel');
GO
```

The preceding `INSERT` code returns the following error:

```output
An explicit value for the identity column in table 'AdventureWorks2022.dbo.Tool' can only be specified when a column list is used and IDENTITY_INSERT is ON.
```

Set `IDENTITY_INSERT` to `ON`.

```sql
SET IDENTITY_INSERT dbo.Tool ON;
GO
```

Try to insert an explicit ID value of 3.

```sql
INSERT INTO dbo.Tool (ID, Name)
VALUES (3, 'Garden shovel');
GO

SELECT *
FROM dbo.Tool;
GO
```

Drop tool table.

```sql
DROP TABLE dbo.Tool;
GO
```

## Related content

- [CREATE TABLE (Transact-SQL)](create-table-transact-sql.md)
- [CREATE TABLE (Transact-SQL) IDENTITY (Property)](create-table-transact-sql-identity-property.md)
- [SCOPE_IDENTITY (Transact-SQL)](../functions/scope-identity-transact-sql.md)
- [INSERT (Transact-SQL)](insert-transact-sql.md)
- [SET Statements (Transact-SQL)](set-statements-transact-sql.md)

::: moniker-end

::: moniker range="=fabric"

[!INCLUDE [fabricdw](../../includes/applies-to-version/fabric-dw.md)]

Use `SET IDENTITY_INSERT` to insert explicit values into the `IDENTITY` column of a table in Fabric Data Warehouse. Use `SET IDENTITY_INSERT` when you need to insert specific values into an identity column, such as during data migration, disaster recovery, or when populating sentinel values in dimension tables.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
SET IDENTITY_INSERT [ schema_name. ] table_name { ON | OFF }
```

## Arguments

#### *schema_name*

The name of the schema that contains the table.

#### *table_name*

The name of a table with an identity column.

## Remarks

At any time, only one table in a session can have the `IDENTITY_INSERT` property set to `ON`. If a table already has this property set to `ON` and you issue `SET IDENTITY_INSERT ON` for another table, an error identifies the table for which the property is already set.

After completing explicit inserts, set `IDENTITY_INSERT` back to `OFF` and run [DBCC CHECKIDENT](../database-console-commands/dbcc-checkident-transact-sql.md) with `RESEED` to realign the identity range and prevent potential conflicts with future automatically generated values.

Fabric Data Warehouse doesn't guarantee uniqueness of identity values when `IDENTITY_INSERT` is used. Explicitly inserted values can introduce duplicates unless you run `DBCC CHECKIDENT` to realign identity metadata before the system generates more values.

## Permissions

You must own the table or have `ALTER` permission on the table.

## Limitations

`SET IDENTITY_INSERT` applies only to `INSERT` and `COPY INTO` statements. It doesn't allow you to update existing identity column values.

## Examples

### A. Insert sentinel values into a dimension table

The most common use for `IDENTITY_INSERT` is populating sentinel values, such as `-1` for "Unknown," in dimension tables during data warehouse setup or migration.

```sql
-- Create a dimension table with an IDENTITY column
CREATE TABLE dbo.DimCustomer (
    CustomerKey BIGINT IDENTITY,
    CustomerName VARCHAR(100),
    Email VARCHAR(200)
);

-- Enable IDENTITY_INSERT to add sentinel rows
SET IDENTITY_INSERT dbo.DimCustomer ON;

INSERT INTO dbo.DimCustomer (CustomerKey, CustomerName, Email)
VALUES (-1, 'Unknown', 'N/A');

INSERT INTO dbo.DimCustomer (CustomerKey, CustomerName, Email)
VALUES (-2, 'Not Applicable', 'N/A');

SET IDENTITY_INSERT dbo.DimCustomer OFF;

-- Reseed to prevent conflicts with future auto-generated values
DBCC CHECKIDENT('dbo.DimCustomer', RESEED);
```

### B. Migrate data while preserving existing identity values

When migrating from SQL Server or Azure Synapse Analytics, use `IDENTITY_INSERT` to preserve existing identity values and maintain referential integrity.

```sql
-- Assume dbo.DimProduct has an IDENTITY column named ProductKey
SET IDENTITY_INSERT dbo.DimProduct ON;

INSERT INTO dbo.DimProduct (ProductKey, ProductName, Category, ListPrice)
VALUES (1, 'Widget A', 'Hardware', 19.99),
       (2, 'Widget B', 'Hardware', 29.99),
       (3, 'Gadget C', 'Electronics', 49.99);

SET IDENTITY_INSERT dbo.DimProduct OFF;

-- Reseed after migration
DBCC CHECKIDENT('dbo.DimProduct', RESEED);
```

### C. Fill a gap in identity values

If rows are deleted from a table, use `IDENTITY_INSERT` to fill gaps in the identity sequence when needed.

```sql
CREATE TABLE dbo.Tool (
    ID BIGINT IDENTITY,
    Name VARCHAR(40) NOT NULL
);

INSERT INTO dbo.Tool (Name)
VALUES ('Screwdriver'), ('Hammer'), ('Saw'), ('Shovel');

-- Delete a row, creating a gap
DELETE FROM dbo.Tool WHERE Name = 'Saw';

-- Fill the gap with an explicit value
SET IDENTITY_INSERT dbo.Tool ON;

INSERT INTO dbo.Tool (ID, Name)
VALUES (3, 'Garden shovel');

SET IDENTITY_INSERT dbo.Tool OFF;
DBCC CHECKIDENT('dbo.Tool', RESEED);
```

### D. Insert explicit values with COPY INTO

The `COPY INTO` statement supports the `IDENTITY_INSERT` option to ingest explicit values within the command. `COPY INTO` options override any session-level setting for `IDENTITY_INSERT`.

```sql
COPY INTO dbo.Employees (EmployeeID 1, FirstName 2, LastName 3)
FROM 'https://myaccount.blob.core.windows.net/myblobcontainer/folder1/'
WITH (
    FILE_TYPE = 'CSV',
    IDENTITY_INSERT = 'ON'
);
```

## Related content

- [IDENTITY columns in Fabric Data Warehouse](/fabric/data-warehouse/identity)
- [DBCC CHECKIDENT (Transact-SQL)](../database-console-commands/dbcc-checkident-transact-sql.md)
- [Tutorial: Use IDENTITY columns in Fabric Data Warehouse](/fabric/data-warehouse/tutorial-identity)
- [Migrate IDENTITY columns to Fabric Data Warehouse](/fabric/data-warehouse/migrate-identity-columns)

::: moniker-end
