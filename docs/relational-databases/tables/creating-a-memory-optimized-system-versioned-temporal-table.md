---
title: Create a memory-optimized system-versioned temporal table
description: Learn how to create a memory-optimized system-versioned temporal table.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/29/2024
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Create a memory-optimized system-versioned temporal table

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

Similar to creating a disk-based history table, you can create a memory-optimized temporal table in several ways.

To create memory-optimized tables, you must first create [the memory optimized filegroup](../in-memory-oltp/the-memory-optimized-filegroup.md).

## Create a memory-optimized temporal table with default history table

Creating a temporal table with a default history table is a convenient option when you want to control naming and still rely on system to create history table with default configuration. In the following example, a new system-versioned memory-optimized temporal table linked to a new disk-based history table.

```sql
CREATE SCHEMA History;
GO

CREATE TABLE dbo.Department (
    DepartmentNumber CHAR(10) NOT NULL PRIMARY KEY NONCLUSTERED,
    DepartmentName VARCHAR(50) NOT NULL,
    ManagerID INT NULL,
    ParentDepartmentNumber CHAR(10) NULL,
    ValidFrom DATETIME2 GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,
    ValidTo DATETIME2 GENERATED ALWAYS AS ROW END HIDDEN NOT NULL,
    PERIOD FOR SYSTEM_TIME(ValidFrom, ValidTo)
)
WITH (
    MEMORY_OPTIMIZED = ON,
    DURABILITY = SCHEMA_AND_DATA,
    SYSTEM_VERSIONING = ON (HISTORY_TABLE = History.DepartmentHistory)
);
```

## Create a memory-optimized temporal table with an existing history table

You can create a temporal table linked to an existing history table when you wish to add system-versioning using an existing table. This scenario is useful when you want to migrate a custom temporal solution to built-in support. In the following example, a new temporal table is created linked to an existing history table.

```sql
--Existing table
CREATE TABLE Department_History (
    DepartmentNumber CHAR(10) NOT NULL,
    DepartmentName VARCHAR(50) NOT NULL,
    ManagerID INT NULL,
    ParentDepartmentNumber CHAR(10) NULL,
    ValidFrom DATETIME2 NOT NULL,
    ValidTo DATETIME2 NOT NULL
);

--Temporal table
CREATE TABLE Department (
    DepartmentNumber CHAR(10) NOT NULL PRIMARY KEY NONCLUSTERED,
    DepartmentName VARCHAR(50) NOT NULL,
    ManagerID INT NULL,
    ParentDepartmentNumber CHAR(10) NULL,
    ValidFrom DATETIME2 GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,
    ValidTo DATETIME2 GENERATED ALWAYS AS ROW END HIDDEN NOT NULL,
    PERIOD FOR SYSTEM_TIME(ValidFrom, ValidTo)
)
WITH (
        SYSTEM_VERSIONING = ON (
            HISTORY_TABLE = dbo.Department_History,
            DATA_CONSISTENCY_CHECK = ON
        ),
    MEMORY_OPTIMIZED = ON,
    DURABILITY = SCHEMA_AND_DATA
);
```

## Related content

- [System-versioned temporal tables with memory-optimized tables](system-versioned-temporal-tables-with-memory-optimized-tables.md)
- [Work with memory-optimized system-versioned temporal tables](working-with-memory-optimized-system-versioned-temporal-tables.md)
- [Monitor memory-optimized system-versioned temporal tables](monitoring-memory-optimized-system-versioned-temporal-tables.md)
- [Memory-optimized system-versioned temporal table performance](memory-optimized-system-versioned-temporal-tables-performance.md)
- [Temporal tables](temporal-tables.md)
- [Temporal table system consistency checks](temporal-table-system-consistency-checks.md)
- [Manage retention of historical data in system-versioned temporal tables](manage-retention-of-historical-data-in-system-versioned-temporal-tables.md)
- [Temporal table metadata views and functions](temporal-table-metadata-views-and-functions.md)
