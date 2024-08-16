---
title: Work with memory-optimized system-versioned temporal tables
description: Work with memory-optimized system-versioned temporal tables
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/29/2024
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Work with memory-optimized system-versioned temporal tables

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

This article discusses how working with a memory-optimized system-versioned temporal table is different from working with a disk-based system-versioned temporal table.

> [!NOTE]  
> Memory-optimized temporal tables are only available in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)], and not [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

## Discover metadata

To discover metadata about a memory-optimized system-versioned temporal table, you need to combine information from [sys.tables](../system-catalog-views/sys-tables-transact-sql.md) and [sys.internal_tables](../system-catalog-views/sys-internal-tables-transact-sql.md). A system-versioned temporal table is presented as parent_object_id of the internal in-memory history table

This example shows how to query and join these tables.

```sql
SELECT SCHEMA_NAME(T1.schema_id) AS TemporalTableSchema,
    OBJECT_NAME(IT.parent_object_id) AS TemporalTableName,
    T1.object_id AS TemporalTableObjectId,
    IT.Name AS InternalHistoryStagingName,
    SCHEMA_NAME(T2.schema_id) AS HistoryTableSchema,
    OBJECT_NAME(T1.history_table_id) AS HistoryTableName
FROM sys.internal_tables IT
INNER JOIN sys.tables T1
    ON IT.parent_object_id = T1.object_id
INNER JOIN sys.tables T2
    ON T1.history_table_id = T2.object_id
WHERE T1.is_memory_optimized = 1
    AND T1.temporal_type = 2;
```

## Modify data

Memory-optimized temporal tables can be modified through natively compiled stored procedures, which enable you to convert non-temporal memory-optimized tables, and keep existing natively stored procedures.

This example how previously created table can be modified in natively compiled module.

```sql
CREATE PROCEDURE dbo.UpdateFXCurrencyPair (
    @ProviderID INT,
    @CurrencyID1 INT,
    @CurrencyID2 INT,
    @BidRate DECIMAL(8, 4),
    @AskRate DECIMAL(8, 4)
)
WITH NATIVE_COMPILATION, SCHEMABINDING,
EXECUTE AS OWNER
AS
BEGIN ATOMIC
   WITH (
      TRANSACTION ISOLATION LEVEL = SNAPSHOT,
      LANGUAGE = N'English'
   )
   UPDATE dbo.FXCurrencyPairs
   SET AskRate = @AskRate,
      BidRate = @BidRate
   WHERE ProviderID = @ProviderID
      AND CurrencyID1 = @CurrencyID1
      AND CurrencyID2 = @CurrencyID2
END
GO;
```

## Related content

- [System-versioned temporal tables with memory-optimized tables](system-versioned-temporal-tables-with-memory-optimized-tables.md)
- [Create a memory-optimized system-versioned temporal table](creating-a-memory-optimized-system-versioned-temporal-table.md)
- [Monitor memory-optimized system-versioned temporal tables](monitoring-memory-optimized-system-versioned-temporal-tables.md)
- [Memory-optimized system-versioned temporal table performance](memory-optimized-system-versioned-temporal-tables-performance.md)
- [Temporal tables](temporal-tables.md)
- [Temporal table system consistency checks](temporal-table-system-consistency-checks.md)
- [Manage retention of historical data in system-versioned temporal tables](manage-retention-of-historical-data-in-system-versioned-temporal-tables.md)
- [Temporal table metadata views and functions](temporal-table-metadata-views-and-functions.md)
