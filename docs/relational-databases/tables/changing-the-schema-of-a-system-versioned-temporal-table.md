---
description: "Changing the Schema of a System-Versioned Temporal Table"
title: "Changing the Schema of a System-Versioned Temporal Table | Microsoft Docs"
ms.custom: ""
ms.date: 04/21/2022
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: panant
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Changing the schema of a system-versioned temporal table

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

Use the **ALTER TABLE** statement to add, alter or remove a column.

## Examples

Here are some examples that change the schema of temporal table.

```sql
ALTER TABLE dbo.Department
    ALTER COLUMN DeptName varchar(100);

ALTER TABLE dbo.Department
    ADD WebAddress nvarchar(255) NOT NULL
    CONSTRAINT DF_WebAddress DEFAULT 'www.mycompany.com';

ALTER TABLE dbo.Department
    ADD TempColumn INT;

GO

ALTER TABLE dbo.Department
    DROP COLUMN TempColumn;

/* Setting IsHidden property for period columns.
Use ALTER COLUMN <period_column> DROP HIDDEN to clear IsHidden flag */

ALTER TABLE dbo.Department
    ALTER COLUMN ValidFrom ADD HIDDEN;

ALTER TABLE dbo.Department
    ALTER COLUMN ValidTo ADD HIDDEN;
```

### Important remarks

- **CONTROL** permission on current and history tables is required to change schema of temporal table.
- During an **ALTER TABLE** operation, the system holds a schema lock on both tables.
- Specified schema change is propagated to history table appropriately (depending on type of change).
- Adding **varchar(max)**, **nvarchar(max)**, **varbinary(max)** or XML columns with defaults will be an update data operation on all editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].
- If row size after column addition exceeds the row size limit, new columns cannot be added online.
- Once you extend a table with a new NOT NULL column, consider dropping default constraint on the history table as all columns in that table are automatically populated by the system.
- Online option (**WITH (ONLINE = ON**) has no effect on **ALTER TABLE ALTER COLUMN** in case of system-versioned temporal table. ALTER column is not performed as online regardless of which value was specified for ONLINE option.
- You can use **ALTER COLUMN** to change **IsHidden** property for period columns.
- You cannot use direct **ALTER** for the following schema changes. For these types of changes, set **SYSTEM_VERSIONING = OFF**.

  - Adding a computed column
  - Adding an **IDENTITY** column
  - Adding a **SPARSE** column or changing existing column to be **SPARSE** when the history table is set to **DATA_COMPRESSION = PAGE** or **DATA_COMPRESSION = ROW**, which is the default for the history table.
  - Adding a **COLUMN_SET**
  - Adding a **ROWGUIDCOL** column or changing existing column to be **ROWGUIDCOL**
  - Altering a **NULL** column to **NOT NULL** if the column contains null values in the current or history table

The following example demonstrates changing the schema where setting **SYSTEM_VERSIONING = OFF** is still required (adding **IDENTITY** column). This example disables the data consistency check. This check is unnecessary when the schema change is made within a transaction as no concurrent data changes can occur.

```sql
    BEGIN TRAN
        ALTER TABLE [dbo].[CompanyLocation] SET (SYSTEM_VERSIONING = OFF);
        ALTER TABLE [CompanyLocation] ADD Cntr INT IDENTITY (1,1);
        ALTER TABLE [dbo].[CompanyLocationHistory] ADD Cntr INT NOT NULL DEFAULT 0;
        ALTER TABLE [dbo].[CompanyLocation]
    SET
         (
            SYSTEM_VERSIONING = ON
           ( HISTORY_TABLE = [dbo].[CompanyLocationHistory])
         );
    COMMIT;
```

## Next steps

- [Temporal Tables](../../relational-databases/tables/temporal-tables.md)
- [Getting Started with System-Versioned Temporal Tables](../../relational-databases/tables/getting-started-with-system-versioned-temporal-tables.md)
- [Manage Retention of Historical Data in System-Versioned Temporal Tables](../../relational-databases/tables/manage-retention-of-historical-data-in-system-versioned-temporal-tables.md)
- [System-Versioned Temporal Tables with Memory-Optimized Tables](../../relational-databases/tables/system-versioned-temporal-tables-with-memory-optimized-tables.md)
- [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)
- [Creating a System-Versioned Temporal Table](../../relational-databases/tables/creating-a-system-versioned-temporal-table.md)
- [Modifying Data in a System-Versioned Temporal Table](../../relational-databases/tables/modifying-data-in-a-system-versioned-temporal-table.md)
- [Querying Data in a System-Versioned Temporal Table](../../relational-databases/tables/querying-data-in-a-system-versioned-temporal-table.md)
- [Stopping System-Versioning on a System-Versioned Temporal Table](../../relational-databases/tables/stopping-system-versioning-on-a-system-versioned-temporal-table.md)
