---
title: Work with Memory-Optimized System-Versioned Temporal Tables
description: Work with memory-optimized system-versioned temporal tables
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/18/2026
ms.service: sql
ms.subservice: table-view-index
ms.topic: how-to
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Work with memory-optimized system-versioned temporal tables

[!INCLUDE [sqlserver2016-asdbmi](../../../includes/applies-to-version/sqlserver2016-asdbmi.md)]

This article describes how working with a memory-optimized system-versioned temporal table differs from working with a disk-based system-versioned temporal table.

> [!NOTE]  
> Memory-optimized temporal tables are only available in [!INCLUDE [ssnoversion](../../../includes/ssnoversion-md.md)] and [!INCLUDE [ssazuremi-md](../../../includes/ssazuremi-md.md)]. Memory-optimized tables and temporal tables are independently available in [!INCLUDE [ssazure-sqldb](../../../includes/ssazure-sqldb.md)].

## Discover metadata

To discover metadata about a memory-optimized system-versioned temporal table, combine information from [sys.tables](../../system-catalog-views/sys-tables-transact-sql.md) and [sys.internal_tables](../../system-catalog-views/sys-internal-tables-transact-sql.md). A system-versioned temporal table appears as the `parent_object_id` column of the internal in-memory history table.

This example shows how to query and join these tables.

```sql
SELECT SCHEMA_NAME(T1.schema_id) AS TemporalTableSchema,
       OBJECT_NAME(IT.parent_object_id) AS TemporalTableName,
       T1.object_id AS TemporalTableObjectId,
       IT.Name AS InternalHistoryStagingName,
       SCHEMA_NAME(T2.schema_id) AS HistoryTableSchema,
       OBJECT_NAME(T1.history_table_id) AS HistoryTableName
FROM sys.internal_tables AS IT
     INNER JOIN sys.tables AS T1
         ON IT.parent_object_id = T1.object_id
     INNER JOIN sys.tables AS T2
         ON T1.history_table_id = T2.object_id
WHERE T1.is_memory_optimized = 1
      AND T1.temporal_type = 2;
```

## Modify data

You can modify memory-optimized temporal tables through natively compiled stored procedures. These procedures let you convert non-temporal memory-optimized tables and keep existing natively stored procedures.

This example shows how to modify an existing table in a natively compiled module.

```sql
CREATE PROCEDURE dbo.UpdateFXCurrencyPair (
    @ProviderID INT,
    @CurrencyID1 INT,
    @CurrencyID2 INT,
    @BidRate DECIMAL (8, 4),
    @AskRate DECIMAL (8, 4)
)
WITH NATIVE_COMPILATION,
     SCHEMABINDING,
     EXECUTE AS OWNER
AS
BEGIN ATOMIC
WITH (TRANSACTION ISOLATION LEVEL = SNAPSHOT, LANGUAGE = N'English')
    UPDATE dbo.FXCurrencyPairs
        SET AskRate = @AskRate,
            BidRate = @BidRate
    WHERE ProviderID = @ProviderID
          AND CurrencyID1 = @CurrencyID1
          AND CurrencyID2 = @CurrencyID2;
END;
```

## Related content

- [System-versioned temporal tables with memory-optimized tables](memory-optimized.md)
- [Create a memory-optimized system-versioned temporal table](create-memory-optimized.md)
- [Monitor memory-optimized system-versioned temporal tables](monitor-memory-optimized.md)
- [Memory-optimized system-versioned temporal table performance](memory-optimized-performance.md)
- [Temporal tables](overview.md)
- [Temporal table system consistency checks](consistency-checks.md)
- [Manage retention of historical data in system-versioned temporal tables](manage-retention.md)
- [Temporal table metadata views and functions](metadata-views-functions.md)
