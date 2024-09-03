---
title: "IDENTITY (Property) (Transact-SQL)"
description: Creates an identity column in a table.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 10/24/2023
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "IDENTITY_TSQL"
  - "IDENTITY"
helpviewer_keywords:
  - "IDENTITY property"
  - "columns [SQL Server], creating"
  - "identity columns [SQL Server], IDENTITY property"
  - "autonumbers, identity numbers"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# CREATE TABLE (Transact-SQL) IDENTITY (Property)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

Creates an identity column in a table. This property is used with the CREATE TABLE and ALTER TABLE [!INCLUDE [tsql](../../includes/tsql-md.md)] statements.

> [!NOTE]  
> The IDENTITY property is different from the SQL-DMO `Identity` property that exposes the row identity property of a column.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
IDENTITY [ (seed , increment) ]
```

[!INCLUDE [synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]

## Arguments

#### *seed*

The value that is used for the very first row loaded into the table.

#### *increment*

The incremental value that is added to the identity value of the previous row that was loaded.

> [!NOTE]  
> In Azure Synapse Analytics values for identity aren't incremental due to the distributed architecture of the data warehouse. For more information, see [Using IDENTITY to create surrogate keys in a Synapse SQL pool](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-tables-identity#allocation-of-values).

You must specify both the seed and increment or neither. If neither is specified, the default is (1,1).

## Remarks

Identity columns can be used for generating key values. The identity property on a column guarantees the following conditions:

- Each new value is generated based on the current seed and increment.

- Each new value for a particular transaction is different from other concurrent transactions on the table.

The identity property on a column doesn't guarantee the following conditions:

- **Uniqueness of the value** - Uniqueness must be enforced by using a `PRIMARY KEY` or `UNIQUE` constraint or `UNIQUE` index.

  > [!NOTE]  
  > Azure Synapse Analytics doesn't support `PRIMARY KEY` or `UNIQUE` constraint or `UNIQUE` index. For more information, see [Using IDENTITY to create surrogate keys in a Synapse SQL pool](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-tables-identity#what-is-a-surrogate-key).

- **Consecutive values within a transaction** - A transaction inserting multiple rows isn't guaranteed to get consecutive values for the rows because other concurrent inserts might occur on the table. If values must be consecutive, then the transaction should use an exclusive lock on the table or use the `SERIALIZABLE` isolation level.

- **Consecutive values after server restart or other failures** - [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] might cache identity values for performance reasons and some of the assigned values can be lost during a database failure or server restart. This can result in gaps in the identity value upon insert. If gaps aren't acceptable, then the application should use its own mechanism to generate key values. Using a sequence generator with the `NOCACHE` option can limit the gaps to transactions that are never committed.

- **Reuse of values** - For a given identity property with specific seed/increment, the identity values aren't reused by the engine. If a particular insert statement fails, or if the insert statement is rolled back then the consumed identity values are lost and aren't generated again. This can result in gaps when the subsequent identity values are generated.

These restrictions are part of the design in order to improve performance, and because they're acceptable in many common situations. If you can't use identity values because of these restrictions, create a separate table holding a current value and manage access to the table and number assignment with your application.

If a table with an identity column is published for replication, the identity column must be managed in a way that is appropriate for the type of replication used. For more information, see [Replicate Identity Columns](../../relational-databases/replication/publish/replicate-identity-columns.md).

Only one identity column can be created per table.

In memory-optimized tables, the seed and increment must be set to `1, 1`. Setting the seed or increment to a value other than `1` results in the following error: `The use of seed and increment values other than 1 is not supported with memory optimized tables`.

Once the identity property is set on a column, it can't be removed. The data type can be changed as long as the new data type is compatible with the identity property.

## Examples

### A. Use the IDENTITY property with CREATE TABLE

The following example creates a new table using the `IDENTITY` property for an automatically incrementing identification number.

```sql
USE AdventureWorks2022;
GO
IF OBJECT_ID('dbo.new_employees', 'U') IS NOT NULL
    DROP TABLE new_employees;
GO

CREATE TABLE new_employees (
    id_num INT IDENTITY(1, 1),
    fname VARCHAR(20),
    minit CHAR(1),
    lname VARCHAR(30)
);

INSERT new_employees (fname, minit, lname)
VALUES ('Karin', 'F', 'Josephs');

INSERT new_employees (fname, minit, lname)
VALUES ('Pirkko', 'O', 'Koskitalo');
```

### B. Use generic syntax for finding gaps in identity values

The following example shows generic syntax for finding gaps in identity values when data is removed.

> [!NOTE]  
> The first part of the following [!INCLUDE [tsql](../../includes/tsql-md.md)] script is designed for illustration only. You can run the [!INCLUDE [tsql](../../includes/tsql-md.md)] script that starts with the comment: `-- Create the img table`.

```sql
-- Here is the generic syntax for finding identity value gaps in data.
-- The illustrative example starts here.
SET IDENTITY_INSERT tablename ON;

DECLARE @minidentval column_type;
DECLARE @maxidentval column_type;
DECLARE @nextidentval column_type;

SELECT @minidentval = MIN($IDENTITY),
    @maxidentval = MAX($IDENTITY)
FROM tablename

IF @minidentval = IDENT_SEED('tablename')
    SELECT @nextidentval = MIN($IDENTITY) + IDENT_INCR('tablename')
    FROM tablename t1
    WHERE $IDENTITY BETWEEN IDENT_SEED('tablename')
            AND @maxidentval
        AND NOT EXISTS (
            SELECT *
            FROM tablename t2
            WHERE t2.$IDENTITY = t1.$IDENTITY + IDENT_INCR('tablename')
            )
ELSE
    SELECT @nextidentval = IDENT_SEED('tablename');

SET IDENTITY_INSERT tablename OFF;

-- Here is an example to find gaps in the actual data.
-- The table is called img and has two columns: the first column
-- called id_num, which is an increasing identification number, and the
-- second column called company_name.
-- This is the end of the illustration example.
-- Create the img table.
-- If the img table already exists, drop it.
-- Create the img table.
IF OBJECT_ID('dbo.img', 'U') IS NOT NULL
    DROP TABLE img;
GO

CREATE TABLE img (
    id_num INT IDENTITY(1, 1),
    company_name SYSNAME
);

INSERT img (company_name)
VALUES ('New Moon Books');

INSERT img (company_name)
VALUES ('Lucerne Publishing');

-- SET IDENTITY_INSERT ON and use in img table.
SET IDENTITY_INSERT img ON;

DECLARE @minidentval SMALLINT;
DECLARE @nextidentval SMALLINT;

SELECT @minidentval = MIN($IDENTITY)
FROM img

IF @minidentval = IDENT_SEED('img')
    SELECT @nextidentval = MIN($IDENTITY) + IDENT_INCR('img')
    FROM img t1
    WHERE $IDENTITY BETWEEN IDENT_SEED('img')
            AND 32766
        AND NOT EXISTS (
            SELECT *
            FROM img t2
            WHERE t2.$IDENTITY = t1.$IDENTITY + IDENT_INCR('img')
            )
ELSE
    SELECT @nextidentval = IDENT_SEED('img');

SET IDENTITY_INSERT img OFF;
```

## Related content

- [ALTER TABLE (Transact-SQL)](alter-table-transact-sql.md)
- [CREATE TABLE (Transact-SQL)](create-table-transact-sql.md)
- [DBCC CHECKIDENT (Transact-SQL)](../database-console-commands/dbcc-checkident-transact-sql.md)
- [IDENT_INCR (Transact-SQL)](../functions/ident-incr-transact-sql.md)
- [&#x40;&#x40;IDENTITY (Transact-SQL)](../functions/identity-transact-sql.md)
- [IDENTITY (Function) (Transact-SQL)](../functions/identity-function-transact-sql.md)
- [IDENT_SEED (Transact-SQL)](../functions/ident-seed-transact-sql.md)
- [SELECT (Transact-SQL)](../queries/select-transact-sql.md)
- [SET IDENTITY_INSERT (Transact-SQL)](set-identity-insert-transact-sql.md)
- [Replicate Identity Columns](../../relational-databases/replication/publish/replicate-identity-columns.md)
