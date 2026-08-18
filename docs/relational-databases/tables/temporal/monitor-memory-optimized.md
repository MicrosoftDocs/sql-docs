---
title: Monitor Memory-Optimized System-Versioned Temporal Tables
description: Learn how to use existing views to track detailed and summarized memory consumption for every system-versioned memory-optimized table.
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/18/2026
ms.service: sql
ms.subservice: table-view-index
ms.topic: how-to
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Monitor memory-optimized system-versioned temporal tables

[!INCLUDE [sqlserver2016-asdbmi](../../../includes/applies-to-version/sqlserver2016-asdbmi.md)]

You can use existing views to track detailed and summarized memory consumption for every system-versioned memory-optimized table.

## Monitor temporal tables

Use the following example code to monitor temporal tables that use in-memory OLTP. These examples use common table expressions (CTEs).

### Detailed memory consumption

The following query details the memory consumption, split per main system-versioned and internal history staging table.

```sql
WITH InMemoryTemporalTables
AS (SELECT SCHEMA_NAME(t1.schema_id) AS TemporalTableSchema,
           t1.object_id AS TemporalTableObjectId,
           t1.object_id AS InternalTableObjectId,
           OBJECT_NAME(t1.parent_object_id) AS TemporalTableName,
           t1.Name AS InternalHistoryStagingName
    FROM sys.internal_tables AS t1
         INNER JOIN sys.tables AS t1
             ON t1.parent_object_id = t1.object_id
    WHERE t1.is_memory_optimized = 1
          AND t1.temporal_type = 2)
SELECT TemporalTableSchema,
       t.TemporalTableName,
       t.InternalHistoryStagingName,
       CASE
           WHEN c.object_id = t.TemporalTableObjectId
           THEN 'Temporal Table Consumption'
           ELSE 'Internal Table Consumption'
       END AS ConsumedBy,
       c.*
FROM sys.dm_db_xtp_memory_consumers AS c
     INNER JOIN InMemoryTemporalTables AS t
         ON c.object_id = t.TemporalTableObjectId
         OR c.object_id = t.InternalTableObjectId
WHERE t.TemporalTableSchema = 'dbo'
      AND t.TemporalTableName = 'FXCurrencyPairs';
```

### Summary of memory consumption

The following query summarizes memory consumption, with a total for a system-versioned memory-optimized table.

```sql
;WITH InMemoryTemporalTables
AS (
    SELECT SCHEMA_NAME(t1.schema_id) AS TemporalTableSchema,
        t1.object_id AS TemporalTableObjectId,
        t1.object_id AS InternalTableObjectId,
        OBJECT_NAME(t1.parent_object_id) AS TemporalTableName,
        t1.Name AS InternalHistoryStagingName
    FROM sys.internal_tables t1
    INNER JOIN sys.tables t1
        ON t1.parent_object_id = t1.object_id
    WHERE t1.is_memory_optimized = 1
        AND t1.temporal_type = 2
    ),
DetailedConsumption
AS (
    SELECT TemporalTableSchema,
        t.TemporalTableName,
        t.InternalHistoryStagingName,
        CASE
            WHEN c.object_id = t.TemporalTableObjectId
            THEN 'Temporal Table Consumption'
            ELSE 'Internal Table Consumption'
        END AS ConsumedBy,
        c.*
    FROM sys.dm_db_xtp_memory_consumers c
    INNER JOIN InMemoryTemporalTables t
        ON c.object_id = t.TemporalTableObjectId
            OR c.object_id = t.InternalTableObjectId
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

- [System-versioned temporal tables with memory-optimized tables](memory-optimized.md)
- [Create a memory-optimized system-versioned temporal table](create-memory-optimized.md)
- [Work with memory-optimized system-versioned temporal tables](work-with-memory-optimized.md)
- [Memory-optimized system-versioned temporal table performance](memory-optimized-performance.md)
- [Temporal tables](overview.md)
- [Temporal table system consistency checks](consistency-checks.md)
- [Manage retention of historical data in system-versioned temporal tables](manage-retention.md)
- [Temporal table metadata views and functions](metadata-views-functions.md)
