---
title: Change the Schema of a System-Versioned Temporal Table
description: Change the schema of a system-versioned temporal table with the ALTER TABLE statement.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: panant
ms.date: 08/18/2026
ms.service: sql
ms.subservice: table-view-index
ms.topic: how-to
ms.custom:
  - ignite-2025
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Change the schema of a system-versioned temporal table

[!INCLUDE [sqlserver2016-asdb-asdbmi-fabricsqldb](../../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-fabricsqldb.md)]

Use the `ALTER TABLE` statement to add, alter, or remove a column.

## Remarks

You need `CONTROL` permission on the current and history tables to change the schema of a temporal table.

During an `ALTER TABLE` operation, the system holds a schema lock on both tables.

The [!INCLUDE [ssde-md](../../../includes/ssde-md.md)] propagates the schema change to the history table, depending on the type of change.

Adding **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, or **xml** columns with defaults is an update data operation on all editions of [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)].

If the row size after adding a column exceeds the row size limit, you can't add new columns online.

Once you extend a table with a new `NOT NULL` column, consider dropping the default constraint on the history table, as the system automatically populates all columns in that table.

The online option (`WITH (ONLINE = ON`) has no effect on `ALTER TABLE ALTER COLUMN` with temporal tables. The `ALTER` column operation doesn't run online, regardless of the value you specify for the `ONLINE` option.

You can use `ALTER COLUMN` to change the `IsHidden` property for period columns.

You can't use direct `ALTER` for the following schema changes. For these types of changes, set `SYSTEM_VERSIONING = OFF`.

- Adding a computed column
- Adding an `IDENTITY` column
- Adding a `SPARSE` column or changing an existing column to be `SPARSE` when the history table is set to `DATA_COMPRESSION = PAGE` or `DATA_COMPRESSION = ROW`, which is the default for the history table.
- Adding a `COLUMN_SET`
- Adding a `ROWGUIDCOL` column or changing an existing column to be `ROWGUIDCOL`
- Altering a `NULL` column to `NOT NULL` if the column contains null values in the current or history table

## Examples

### A. Change the schema of a temporal table

Here are some examples that change the schema of a temporal table.

```sql
ALTER TABLE dbo.Department
    ALTER COLUMN DeptName VARCHAR (100);

ALTER TABLE dbo.Department
    ADD WebAddress NVARCHAR (255)
        CONSTRAINT DF_WebAddress DEFAULT 'www.example.com' NOT NULL;

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

The following example shows how to change the schema when you still need to set `SYSTEM_VERSIONING = OFF` (adding an `IDENTITY` column). This example disables the data consistency check. This check is unnecessary when you make the schema change within a transaction, because no concurrent data changes can occur.

```sql
BEGIN TRANSACTION;

ALTER TABLE [dbo].[CompanyLocation]
    SET (SYSTEM_VERSIONING = OFF);

ALTER TABLE [CompanyLocation]
    ADD Cntr INT IDENTITY (1, 1);

ALTER TABLE [dbo].[CompanyLocationHistory]
    ADD Cntr INT
        CONSTRAINT DF_Cntr DEFAULT 0 NOT NULL;

ALTER TABLE [dbo].[CompanyLocation]
SET (
    SYSTEM_VERSIONING = ON
    (HISTORY_TABLE = [dbo].[CompanyLocationHistory])
);

COMMIT TRANSACTION;
```

## Related content

- [Temporal tables](overview.md)
- [Get started with system-versioned temporal tables](get-started.md)
- [Manage retention of historical data in system-versioned temporal tables](manage-retention.md)
- [System-versioned temporal tables with memory-optimized tables](memory-optimized.md)
- [ALTER TABLE (Transact-SQL)](../../../t-sql/statements/alter-table-transact-sql.md)
- [Create a system-versioned temporal table](create.md)
- [Modify data in a system-versioned temporal table](modify-data.md)
- [Query data in a system-versioned temporal table](query-data.md)
- [Stop system-versioning on a system-versioned temporal table](stop-system-versioning.md)
