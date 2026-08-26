---
title: DBCC CHECKIDENT (Transact-SQL)
description: DBCC CHECKIDENT checks the current identity value for the specified table in SQL Server, and changes the value if needed.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: procha
ms.date: 08/26/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "CHECKIDENT"
  - "DBCC CHECKIDENT"
  - "CHECKIDENT_TSQL"
  - "DBCC_CHECKIDENT_TSQL"
helpviewer_keywords:
  - "checking identity values"
  - "reseeding identity values"
  - "resetting identity values"
  - "identity values [SQL Server]"
  - "identity values [SQL Server], checking"
  - "modifying identity values"
  - "current identity values"
  - "DBCC CHECKIDENT statement"
  - "identity values [SQL Server], reseeding"
  - "reporting current identity values"
dev_langs:
  - TSQL
ai-usage: ai-assisted
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =azure-sqldw-latest || =fabric || =fabric-sqldb"
---

# DBCC CHECKIDENT (Transact-SQL)

::: moniker range="=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azure-sqldw-latest || =azuresqldb-mi-current || =fabric-sqldb"

[!INCLUDE [sql-asdb-asdbmi-asa-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-fabricsqldb.md)]

Checks the current identity value for the specified table in [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] and, if needed, changes the identity value. You can also use `DBCC CHECKIDENT` to manually set a new current identity value for the identity column.

This article and the `IDENTITY` syntax differ on different platforms of the [SQL Database Engine](../../database-engine/sql-database-engine.md). For Microsoft Fabric Data Warehouse, [select Fabric Data Warehouse in the version dropdown list](../functions/openrowset-bulk-transact-sql.md?view=fabric&preserve-view=true).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

Syntax for SQL Server, Azure SQL Database, Azure SQL Managed Instance, SQL database in Fabric:

```syntaxsql
DBCC CHECKIDENT
 (
    table_name
        [ , { NORESEED | { RESEED [ , new_reseed_value ] } } ]
)
[ WITH NO_INFOMSGS ]
```

Syntax for Azure Synapse Analytics:

```syntaxsql
DBCC CHECKIDENT
 (
    table_name
        [ RESEED , new_reseed_value ]
)
[ WITH NO_INFOMSGS ]
```

## Arguments

#### *table_name*

The name of the table for which to check the current identity value. The table you specify must contain an identity column. Table names must follow the rules for [identifiers](../../relational-databases/databases/database-identifiers.md). Use delimiters for two- or three-part names, such as `Person.AddressType` or `[Person].[AddressType]`.

#### NORESEED

Specifies that the current identity value shouldn't be changed.

#### RESEED

Specifies that the current identity value should be changed.

#### *new_reseed_value*

The new value to use as the current value of the identity column.

#### WITH NO_INFOMSGS

Suppresses all informational messages.

## Remarks

The specific corrections that `DBCC CHECKIDENT` makes to the current identity value depend on the parameter specifications.

| `DBCC CHECKIDENT` command | Identity correction or corrections made |
| --- | --- |
| `DBCC CHECKIDENT (<table_name>, NORESEED)` | Current identity value isn't reset. `DBCC CHECKIDENT` returns the current identity value and the current maximum value of the identity column. If the two values aren't the same, reset the identity value to avoid potential errors or gaps in the sequence of values. |
| `DBCC CHECKIDENT (<table_name>)`<br /><br />or<br /><br />`DBCC CHECKIDENT (<table_name>, RESEED)` | If the current identity value for a table is less than the maximum identity value stored in the identity column, `DBCC CHECKIDENT` resets it by using the maximum value in the identity column. See the [Exceptions](#exceptions) section that follows. |
| `DBCC CHECKIDENT (<table_name>, RESEED, <new_reseed_value>)` | Current identity value is set to the `new_reseed_value`. If no rows are inserted into the table since the table was created, or if all rows are removed by using the `TRUNCATE TABLE` statement, the first row inserted after you run `DBCC CHECKIDENT` uses `new_reseed_value` as the identity. If rows are present in the table, or if all rows are removed by using the `DELETE` statement, the next row inserted uses `new_reseed_value` + the [current increment](../functions/ident-incr-transact-sql.md) value. If a transaction inserts a row and is later rolled back, the next row inserted uses `new_reseed_value` + the [current increment](../functions/ident-incr-transact-sql.md) value as if the row was deleted. If the table isn't empty, setting the identity value to a number less than the maximum value in the identity column can result in one of the following conditions:<br /><br />- If a `PRIMARY KEY` or `UNIQUE` constraint exists on the identity column, error message 2627 is generated on later insert operations into the table because the generated identity value conflicts with existing values.<br /><br />- If a `PRIMARY KEY` or `UNIQUE` constraint doesn't exist, later insert operations result in duplicate identity values. |

## Exceptions

The following table lists conditions when `DBCC CHECKIDENT` doesn't automatically reset the current identity value, and provides methods for resetting the value.

| Condition | Reset methods |
| --- | --- |
| The current identity value is larger than the maximum value in the table. | Execute `DBCC CHECKIDENT (<table_name>, NORESEED)` to determine the current maximum value in the column. Next, specify that value as the *new_reseed_value* in a `DBCC CHECKIDENT (<table_name>, RESEED, <new_reseed_value>)` command.<br /><br />or<br /><br />Execute `DBCC CHECKIDENT (<table_name>, RESEED, <new_reseed_value>)` with `new_reseed_value` set to a low value, and then run `DBCC CHECKIDENT (<table_name>, RESEED)` to correct the value. |
| All rows are deleted from the table. | Execute `DBCC CHECKIDENT (<table_name>, RESEED, <new_reseed_value>)` with `new_reseed_value` set to the new starting value. |

## Change the seed value

The seed value is the value inserted into an identity column for the first row loaded into the table. All subsequent rows contain the current identity value plus the increment value where current identity value is the last identity value generated for the table or view.

You can't use `DBCC CHECKIDENT` for the following tasks:

- Change the original seed value specified for an identity column when the table or view was created.

- Reseed existing rows in a table or view.

To change the original seed value and reseed any existing rows, drop the identity column and recreate it specifying the new seed value. When the table contains data, the identity numbers are added to the existing rows with the specified seed and increment values. The order in which the rows are updated isn't guaranteed.

## Result sets

Whether or not you specify any options for a table that contains an identity column, `DBCC CHECKIDENT` returns the following message for all operations except one. That operation is specifying a new seed value.

> Checking identity information: current identity value '\<current identity value>', current column value '\<current column value>'. DBCC execution completed. If DBCC printed error messages, contact your system administrator.

When `DBCC CHECKIDENT` is used to specify a new seed value by using `RESEED <new_reseed_value>`, the following message is returned.

> Checking identity information: current identity value '\<current identity value>'. DBCC execution completed. If DBCC printed error messages, contact your system administrator.

## Permissions

Caller must own the schema that contains the table, or be a member of the **sysadmin** fixed server role, the **db_owner** fixed database role, or the **db_ddladmin** fixed database role.

[!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] requires **db_owner** permissions.

## Examples

### A. Reset the current identity value, if it's needed

The following example resets the current identity value, if needed, for the specified table in the database.

```sql
USE AdventureWorks2022;
GO
DBCC CHECKIDENT ('Person.AddressType');
GO
```

### B. Report the current identity value

The following example reports the current identity value for the specified table in the database, and doesn't correct the identity value if it's incorrect.

```sql
USE AdventureWorks2022;
GO
DBCC CHECKIDENT ('Person.AddressType', NORESEED);
GO
```

### C. Force the current identity value to a new value

The following example forces the current identity value for the `AddressTypeID` column in the `AddressType` table to a value of 10. Because the table has existing rows, the next row inserted uses 11 as the value. The new current identity value defined for the column plus 1 is the column's increment value.

```sql
USE AdventureWorks2022;
GO
DBCC CHECKIDENT ('Person.AddressType', RESEED, 10);
GO
```

### D. Reset the identity value on an empty table

The following example assumes a table identity of `(1, 1)` and forces the current identity value for the `ErrorLogID` column in the `ErrorLog` table to a value of 1, after deleting all records from the table. Because the table has no existing rows, the next row inserted uses 1 as the value. The new current identity value *without* adding the increment value defined for the column after TRUNCATE, or adding the increment value after DELETE.

```sql
USE AdventureWorks2022;
GO
TRUNCATE TABLE dbo.ErrorLog
GO
DBCC CHECKIDENT ('dbo.ErrorLog', RESEED, 1);
GO
DELETE FROM dbo.ErrorLog
GO
DBCC CHECKIDENT ('dbo.ErrorLog', RESEED, 0);
GO
```

## Related content

- [ALTER TABLE (Transact-SQL)](../statements/alter-table-transact-sql.md)
- [CREATE TABLE (Transact-SQL)](../statements/create-table-transact-sql.md)
- [DBCC (Transact-SQL)](dbcc-transact-sql.md)
- [IDENTITY (Property) (Transact-SQL)](../statements/create-table-transact-sql-identity-property.md)
- [CREATE TABLE (Transact-SQL) IDENTITY (Property)](../statements/create-table-transact-sql-identity-property.md)
- [Replicate Identity Columns](../../relational-databases/replication/publish/replicate-identity-columns.md)
- [USE (Transact-SQL)](../language-elements/use-transact-sql.md)
- [IDENT_SEED (Transact-SQL)](../functions/ident-seed-transact-sql.md)
- [IDENT_INCR (Transact-SQL)](../functions/ident-incr-transact-sql.md)

::: moniker-end

::: moniker range="=fabric"

[!INCLUDE [fabricdw](../../includes/applies-to-version/fabric-dw.md)]

Reseeds the identity value of a table in Fabric Data Warehouse. Use `DBCC CHECKIDENT` after inserting explicit values with [SET IDENTITY_INSERT](../statements/set-identity-insert-transact-sql.md) to realign identity ranges and prevent conflicts with future automatically generated values.

This article and the `IDENTITY` syntax differ on different platforms of the [SQL Database Engine](../../database-engine/sql-database-engine.md). 

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
DBCC CHECKIDENT ( 'table_name' [ , RESEED ] )
```

## Arguments

#### *table_name*

The name of the table for which to reseed the identity value. The table must contain an identity column. Table names can include the schema name, such as `'dbo.DimCustomer'` or `'[dbo].[DimCustomer]'`.

#### RESEED

The `RESEED` keyword is optional, but `DBCC CHECKIDENT` always performs the reseed operation. Including the option or omitting it produces the same results.

The `RESEED` keyword instructs the Database Engine to scan all used and reserved identity ranges and set the next identity value to avoid conflicts with existing values. In Fabric Data Warehouse, `RESEED` performs a range-aware scan across all distributed compute nodes to determine the correct next value.

## Remarks

- In Fabric Data Warehouse, the only argument that `DBCC CHECKIDENT` supports is the `RESEED` option, which it always performs. The system automatically determines the correct next identity value ranges based on existing data. You can't specify a custom reseed value.
- Always run `DBCC CHECKIDENT` after using `IDENTITY_INSERT` to insert explicit values. This operation ensures that future automatically generated values don't collide with manually inserted values.
- In Fabric Data Warehouse, specifying a custom `new_reseed_value` isn't supported. Attempting to provide a value returns the following error: `Specifying a custom seed in Fabric Data Warehouse is not supported.`
- `DBCC CHECKIDENT` acquires locks that can block concurrent DML and DDL operations. To minimize disruption, run the command in isolation when other processes aren't actively using the table.

## Permissions

You must own the table or have `ALTER` permission on the table.

## Examples

### A. Reseed after inserting sentinel values

After populating sentinel values in a dimension table with `SET IDENTITY_INSERT`, reseed the identity column to prevent future automatically generated values from colliding with manually inserted values.

```sql
-- Insert sentinel values
SET IDENTITY_INSERT dbo.DimCustomer ON;

INSERT INTO dbo.DimCustomer (CustomerKey, CustomerName)
VALUES (-1, 'Unknown'),
       (-2, 'Not Applicable');

SET IDENTITY_INSERT dbo.DimCustomer OFF;

-- Reseed to realign future identity values
DBCC CHECKIDENT('dbo.DimCustomer', RESEED);
```

### B. Reseed after bulk migration

After migrating historical data with explicit identity values, reseed the table to ensure that new rows receive values that don't overlap with migrated data.

```sql
-- After migrating data with IDENTITY_INSERT
DBCC CHECKIDENT('dbo.DimProduct', RESEED);

-- Future inserts receive auto-generated values beyond the migrated range
INSERT INTO dbo.DimProduct (ProductName, Category)
VALUES ('New Widget', 'Hardware');
```

### C. Reseed after COPY INTO with IDENTITY_INSERT

After loading data with `COPY INTO` while `IDENTITY_INSERT` is `ON`, reseed the identity column. `COPY INTO` options override any session-level setting for `IDENTITY_INSERT`.

```sql
COPY INTO dbo.DimStore (StoreKey 1, StoreName 2, Region 3)
FROM 'https://storage.blob.core.windows.net/migration/dimstore.csv'
WITH (
    FILE_TYPE = 'CSV',
    IDENTITY_INSERT = 'ON'
);

-- Reseed after bulk load
DBCC CHECKIDENT('dbo.DimStore', RESEED);
```

### D. Reseed without the RESEED keyword

The `RESEED` keyword is optional, but `DBCC CHECKIDENT` always performs the reseed operation. Including the option or omitting it produces the same results.

```sql
-- Reseed without the RESEED keyword
DBCC CHECKIDENT('dbo.DimStore');

-- Reseed with the RESEED keyword
DBCC CHECKIDENT('dbo.DimStore', RESEED);
```

## Related content

- [IDENTITY columns in Fabric Data Warehouse](/fabric/data-warehouse/identity)
- [SET IDENTITY_INSERT (Transact-SQL)](../statements/set-identity-insert-transact-sql.md)
- [Tutorial: Use IDENTITY columns in Fabric Data Warehouse](/fabric/data-warehouse/tutorial-identity)
- [Migrate IDENTITY columns to Fabric Data Warehouse](/fabric/data-warehouse/migrate-identity-columns)

::: moniker-end
