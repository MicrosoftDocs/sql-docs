---
title: Create a Memory-Optimized System-Versioned Temporal Table
description: Learn how to create a memory-optimized system-versioned temporal table.
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/18/2026
ms.service: sql
ms.subservice: table-view-index
ms.topic: how-to
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Create a memory-optimized system-versioned temporal table

[!INCLUDE [sqlserver2016-asdbmi](../../../includes/applies-to-version/sqlserver2016-asdbmi.md)]

Similar to creating a disk-based history table, you can create a memory-optimized temporal table in several ways.

To create memory-optimized tables, you must first create [the memory optimized filegroup](../../in-memory-oltp/the-memory-optimized-filegroup.md).

> [!NOTE]  
> Memory-optimized temporal tables are only available in [!INCLUDE [ssnoversion](../../../includes/ssnoversion-md.md)] and [!INCLUDE [ssazuremi-md](../../../includes/ssazuremi-md.md)]. Memory-optimized tables and temporal tables are independently available in [!INCLUDE [ssazure-sqldb](../../../includes/ssazure-sqldb.md)].

## Create a memory-optimized temporal table with default history table

Creating a temporal table with a default history table is a convenient option when you want to control naming and still rely on the system to create the history table with a default configuration. In the following example, a new system-versioned memory-optimized temporal table is linked to a new disk-based history table.

```sql
CREATE SCHEMA History;
GO

CREATE TABLE dbo.Department
(
    DepartmentNumber CHAR (10) NOT NULL PRIMARY KEY NONCLUSTERED,
    DepartmentName VARCHAR (50) NOT NULL,
    ManagerID INT NULL,
    ParentDepartmentNumber CHAR (10) NULL,
    ValidFrom DATETIME2 GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,
    ValidTo DATETIME2 GENERATED ALWAYS AS ROW END HIDDEN NOT NULL,
    PERIOD FOR SYSTEM_TIME (ValidFrom, ValidTo)
)
WITH (
    SYSTEM_VERSIONING = ON (
        HISTORY_TABLE = dbo.Department_History
    ),
    MEMORY_OPTIMIZED = ON,
    DURABILITY = SCHEMA_AND_DATA
);
```

## Create a memory-optimized temporal table with an existing history table

You can create a temporal table linked to an existing history table when you want to add system-versioning using an existing table. This scenario is useful when you want to migrate a custom temporal solution to built-in support. In the following example, a new temporal table is created linked to an existing history table.

First, create the existing history table:

```sql
CREATE TABLE Department_History
(
    DepartmentNumber CHAR (10) NOT NULL,
    DepartmentName VARCHAR (50) NOT NULL,
    ManagerID INT NULL,
    ParentDepartmentNumber CHAR (10) NULL,
    ValidFrom DATETIME2 NOT NULL,
    ValidTo DATETIME2 NOT NULL
);
```

Then, convert a temporal table that links to the history table.

```sql
CREATE TABLE Department
(
    DepartmentNumber CHAR (10) NOT NULL PRIMARY KEY NONCLUSTERED,
    DepartmentName VARCHAR (50) NOT NULL,
    ManagerID INT NULL,
    ParentDepartmentNumber CHAR (10) NULL,
    ValidFrom DATETIME2 GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,
    ValidTo DATETIME2 GENERATED ALWAYS AS ROW END HIDDEN NOT NULL,
    PERIOD FOR SYSTEM_TIME (ValidFrom, ValidTo)
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

- [System-versioned temporal tables with memory-optimized tables](memory-optimized.md)
- [Work with memory-optimized system-versioned temporal tables](work-with-memory-optimized.md)
- [Monitor memory-optimized system-versioned temporal tables](monitor-memory-optimized.md)
- [Memory-optimized system-versioned temporal table performance](memory-optimized-performance.md)
- [Temporal tables](overview.md)
- [Temporal table system consistency checks](consistency-checks.md)
- [Manage retention of historical data in system-versioned temporal tables](manage-retention.md)
- [Temporal table metadata views and functions](metadata-views-functions.md)
