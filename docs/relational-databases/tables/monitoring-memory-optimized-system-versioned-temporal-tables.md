---
title: Monitor memory-optimized system-versioned temporal tables
description: Learn how to use existing views to track detailed and summarized memory consumption for every system-versioned memory-optimized table.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/29/2024
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Monitor memory-optimized system-versioned temporal tables

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

You can use existing views to track detailed and summarized memory consumption for every system-versioned memory-optimized table.

## Monitor temporal tables

Use the following example code to monitor temporal tables that use In-Memory OLTP. These examples make use of common table expressions (CTEs).

### Detailed memory consumption

The following query details the memory consumption, split per main system-versioned and internal history staging table.

```sql
WITH InMemoryTemporalTables
AS (
    SELECT SCHEMA_NAME(T1.schema_id) AS TemporalTableSchema,
        T1.object_id AS TemporalTableObjectId,
        IT.object_id AS InternalTableObjectId,
        OBJECT_NAME(IT.parent_object_id) AS TemporalTableName,
        IT.Name AS InternalHistoryStagingName
    FROM sys.internal_tables IT
    INNER JOIN sys.tables T1
        ON IT.parent_object_id = T1.object_id
    WHERE T1.is_memory_optimized = 1
        AND T1.temporal_type = 2
)
SELECT TemporalTableSchema,
    T.TemporalTableName,
    T.InternalHistoryStagingName,
    CASE 
        WHEN C.object_id = T.TemporalTableObjectId
            THEN 'Temporal Table Consumption'
        ELSE 'Internal Table Consumption'
        END ConsumedBy,
    C.*
FROM sys.dm_db_xtp_memory_consumers C
INNER JOIN InMemoryTemporalTables T
    ON C.object_id = T.TemporalTableObjectId
        OR C.object_id = T.InternalTableObjectId
WHERE T.TemporalTableSchema = 'dbo'
    AND T.TemporalTableName = 'FXCurrencyPairs';
```

### Summary of memory consumption

The following query summarizes memory consumption, with a total for a system-versioned memory-optimized table.

```sql
WITH InMemoryTemporalTables
AS (
    SELECT SCHEMA_NAME(T1.schema_id) AS TemporalTableSchema,
        T1.object_id AS TemporalTableObjectId,
        IT.object_id AS InternalTableObjectId,
        OBJECT_NAME(IT.parent_object_id) AS TemporalTableName,
        IT.Name AS InternalHistoryStagingName
    FROM sys.internal_tables IT
    INNER JOIN sys.tables T1
        ON IT.parent_object_id = T1.object_id
    WHERE T1.is_memory_optimized = 1
        AND T1.temporal_type = 2
    ),
DetailedConsumption
AS (
    SELECT TemporalTableSchema,
        T.TemporalTableName,
        T.InternalHistoryStagingName,
        CASE 
            WHEN C.object_id = T.TemporalTableObjectId
                THEN 'Temporal Table Consumption'
            ELSE 'Internal Table Consumption'
            END ConsumedBy,
        C.*
    FROM sys.dm_db_xtp_memory_consumers C
    INNER JOIN InMemoryTemporalTables T
        ON C.object_id = T.TemporalTableObjectId
            OR C.object_id = T.InternalTableObjectId
)
SELECT TemporalTableSchema TemporalTableName,
    sum(allocated_bytes) AS allocated_bytes,
    sum(used_bytes) AS used_bytes
FROM DetailedConsumption
WHERE TemporalTableSchema = 'dbo' ANDTemporalTableName = 'FXCurrencyPairs'
GROUP BY TemporalTableSchema,
    TemporalTableName;
```

## Related content

- [System-versioned temporal tables with memory-optimized tables](system-versioned-temporal-tables-with-memory-optimized-tables.md)
- [Create a memory-optimized system-versioned temporal table](creating-a-memory-optimized-system-versioned-temporal-table.md)
- [Work with memory-optimized system-versioned temporal tables](working-with-memory-optimized-system-versioned-temporal-tables.md)
- [Memory-optimized system-versioned temporal table performance](memory-optimized-system-versioned-temporal-tables-performance.md)
- [Temporal tables](temporal-tables.md)
- [Temporal table system consistency checks](temporal-table-system-consistency-checks.md)
- [Manage retention of historical data in system-versioned temporal tables](manage-retention-of-historical-data-in-system-versioned-temporal-tables.md)
- [Temporal table metadata views and functions](temporal-table-metadata-views-and-functions.md)
