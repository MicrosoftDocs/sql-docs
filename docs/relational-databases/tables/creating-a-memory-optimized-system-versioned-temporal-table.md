---
description: "Creating a Memory-Optimized System-Versioned Temporal Table"
title: "Creating a Memory-Optimized System-Versioned Temporal Table | Microsoft Docs"
ms.custom: ""
ms.date: 03/04/2022
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
author: rwestMSFT
ms.author: randolphwest
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Creating a Memory-Optimized System-Versioned Temporal Table


[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]


Similar to creating a disk-based history table, you can create a memory-optimized temporal table in a number of ways.

> [!NOTE]
> To create memory-optimized tables, you must first create [The Memory Optimized Filegroup](../../relational-databases/in-memory-oltp/the-memory-optimized-filegroup.md).

Creating a temporal table with a default history table is a convenient option when you want to control naming and still rely on system to create history table with default configuration. In the example below, a new system-versioned memory-optimized temporal table linked to a new disk-based history table.

```sql
CREATE SCHEMA History
GO
CREATE TABLE dbo.Department
   (  
      DepartmentNumber char(10) NOT NULL PRIMARY KEY NONCLUSTERED,
      DepartmentName varchar(50) NOT NULL,
      ManagerID int NULL,
      ParentDepartmentNumber char(10) NULL,
      ValidFrom datetime2 GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,
      ValidTo datetime2 GENERATED ALWAYS AS ROW END HIDDEN NOT NULL,
      PERIOD FOR SYSTEM_TIME (ValidFrom, ValidTo)
   )
WITH
   (
       MEMORY_OPTIMIZED = ON, DURABILITY = SCHEMA_AND_DATA,
          SYSTEM_VERSIONING = ON ( HISTORY_TABLE = History.DepartmentHistory )
    );
```

Creating a temporal table linked to an existing history table is useful when you wish to add system-versioning using an existing table, such as when you wish to migrate a custom temporal solution to built-in support. In the example below, a new temporal table is created linked to an existing history table.

```sql
--Existing table
CREATE TABLE Department_History
   (
      DepartmentNumber char(10) NOT NULL,
      DepartmentName varchar(50) NOT NULL,
      ManagerID int NULL,
      ParentDepartmentNumber char(10) NULL,
      ValidFrom datetime2 NOT NULL, ValidTo datetime2 NOT NULL
   )
;
--Temporal table
CREATE TABLE Department
   (
      DepartmentNumber char(10) NOT NULL PRIMARY KEY NONCLUSTERED,
      DepartmentName varchar(50) NOT NULL,
      ManagerID INT NULL,
      ParentDepartmentNumber char(10) NULL,
      ValidFrom datetime2 GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,
      ValidTo datetime2 GENERATED ALWAYS AS ROW END HIDDEN NOT NULL,
      PERIOD FOR SYSTEM_TIME (ValidFrom, ValidTo)
   )
WITH
   (
      SYSTEM_VERSIONING = ON
         (  
            HISTORY_TABLE = dbo.Department_History
            , DATA_CONSISTENCY_CHECK = ON
         )  
      , MEMORY_OPTIMIZED = ON
      , DURABILITY = SCHEMA_AND_DATA
   )
;
```

## See

- [System-Versioned Temporal Tables with Memory-Optimized Tables](../../relational-databases/tables/system-versioned-temporal-tables-with-memory-optimized-tables.md)
- [Working with Memory-Optimized System-Versioned Temporal Tables](../../relational-databases/tables/working-with-memory-optimized-system-versioned-temporal-tables.md)
- [Monitoring Memory-Optimized System-Versioned Temporal Tables](../../relational-databases/tables/monitoring-memory-optimized-system-versioned-temporal-tables.md)
- [Performance Considerations with Memory-Optimized System-Versioned Temporal Tables](../../relational-databases/tables/memory-optimized-system-versioned-temporal-tables-performance.md)
- [Temporal Tables](../../relational-databases/tables/temporal-tables.md)
- [Temporal Table System Consistency Checks](../../relational-databases/tables/temporal-table-system-consistency-checks.md)
- [Manage Retention of Historical Data in System-Versioned Temporal Tables](../../relational-databases/tables/manage-retention-of-historical-data-in-system-versioned-temporal-tables.md)
- [Temporal Table Metadata Views and Functions](../../relational-databases/tables/temporal-table-metadata-views-and-functions.md)
