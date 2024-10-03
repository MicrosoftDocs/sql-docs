---
title: Change the schema of a system-versioned temporal table
description: Change the schema of a system-versioned temporal table with the ALTER TABLE statement.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: panant
ms.date: 07/29/2024
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Change the schema of a system-versioned temporal table

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

Use the `ALTER TABLE` statement to add, alter, or remove a column.

## Remarks

`CONTROL` permission on current and history tables is required to change schema of temporal table.

During an `ALTER TABLE` operation, the system holds a schema lock on both tables.

Specified schema change is propagated to history table appropriately (depending on type of change).

Adding **varchar(max)**, **nvarchar(max)**, **varbinary(max)** or XML columns with defaults, is an update data operation on all editions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

If row size after column addition exceeds the row size limit, new columns can't be added online.

Once you extend a table with a new `NOT NULL` column, consider dropping the default constraint on the history table, as the system automatically populates all columns in that table.

The online option (`WITH (ONLINE = ON`) has no effect on `ALTER TABLE ALTER COLUMN` with temporal tables. `ALTER` column isn't performed as online, regardless of which value was specified for `ONLINE` option.

You can use `ALTER COLUMN` to change the `IsHidden` property for period columns.

You can't use direct `ALTER` for the following schema changes. For these types of changes, set `SYSTEM_VERSIONING = OFF`.

- Adding a computed column
- Adding an `IDENTITY` column
- Adding a `SPARSE` column or changing existing column to be `SPARSE` when the history table is set to `DATA_COMPRESSION = PAGE` or `DATA_COMPRESSION = ROW`, which is the default for the history table.
- Adding a `COLUMN_SET`
- Adding a `ROWGUIDCOL` column or changing existing column to be `ROWGUIDCOL`
- Altering a `NULL` column to `NOT NULL` if the column contains null values in the current or history table

## Examples

### A. Change the schema of a temporal table

Here are some examples that change the schema of temporal table.

```sql
ALTER TABLE dbo.Department
    ALTER COLUMN DeptName varchar(100);

ALTER TABLE dbo.Department
    ADD WebAddress nvarchar(255) NOT NULL
    CONSTRAINT DF_WebAddress DEFAULT 'www.example.com';

ALTER TABLE dbo.Department
    ADD TempColumn INT;
GO

ALTER TABLE dbo.Department
    DROP COLUMN TempColumn;
```

### B. Add period columns using the HIDDEN flag

```sql
ALTER TABLE dbo.Department
    ALTER COLUMN ValidFrom ADD HIDDEN;

ALTER TABLE dbo.Department
    ALTER COLUMN ValidTo ADD HIDDEN;
```

You can use `ALTER COLUMN <period_column> DROP HIDDEN` to clear the hidden flag on a period column.

### C. Change the schema with SYSTEM_VERSIONING set to OFF

The following example demonstrates changing the schema where setting `SYSTEM_VERSIONING = OFF` is still required (adding an `IDENTITY` column). This example disables the data consistency check. This check is unnecessary when the schema change is made within a transaction as no concurrent data changes can occur.

```sql
BEGIN TRANSACTION

ALTER TABLE [dbo].[CompanyLocation] SET (SYSTEM_VERSIONING = OFF);
ALTER TABLE [CompanyLocation] ADD Cntr INT IDENTITY (1, 1);

ALTER TABLE [dbo].[CompanyLocationHistory]
    ADD Cntr INT NOT NULL
    CONSTRAINT DF_Cntr DEFAULT 0;

ALTER TABLE [dbo].[CompanyLocation] SET
(
    SYSTEM_VERSIONING = ON
    (HISTORY_TABLE = [dbo].[CompanyLocationHistory])
);

COMMIT;
```

## Related content

- [Temporal tables](temporal-tables.md)
- [Get started with system-versioned temporal tables](getting-started-with-system-versioned-temporal-tables.md)
- [Manage retention of historical data in system-versioned temporal tables](manage-retention-of-historical-data-in-system-versioned-temporal-tables.md)
- [System-versioned temporal tables with memory-optimized tables](system-versioned-temporal-tables-with-memory-optimized-tables.md)
- [ALTER TABLE (Transact-SQL)](../../t-sql/statements/alter-table-transact-sql.md)
- [Create a system-versioned temporal table](creating-a-system-versioned-temporal-table.md)
- [Modify data in a system-versioned temporal table](modifying-data-in-a-system-versioned-temporal-table.md)
- [Query data in a system-versioned temporal table](querying-data-in-a-system-versioned-temporal-table.md)
- [Stop system-versioning on a system-versioned temporal table](stopping-system-versioning-on-a-system-versioned-temporal-table.md)
