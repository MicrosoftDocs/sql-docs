---
title: Memory-optimized system-versioned temporal table performance
description: This article discusses some specific performance considerations when using system-versioned memory-optimized temporal tables.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/29/2024
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Memory-optimized system-versioned temporal table performance

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

This article discusses some specific performance considerations when using system-versioned memory-optimized temporal tables.

When you add system-versioning to an existing non-temporal table, expect a performance impact on update and delete operations, because the history table is updated automatically.

## Performance considerations

Every update and delete is recorded in an internal memory-optimized history table. You might experience unexpected memory consumption if your workload uses those two operations massively. Therefore we advise you the following considerations:

- Don't perform massive deletes from the current table in one step. Consider deleting data in multiple batches, with manually invoked data flush in between, with [sp_xtp_flush_temporal_history](../system-stored-procedures/temporal-table-sp-xtp-flush-temporal-history.md), or while `SYSTEM_VERSIONING = OFF`.

- Don't perform massive table updates at once, as it can result in memory consumption that is twice the amount of memory required to update a non-temporal memory-optimized table. This doubled memory consumption is temporary, because the data flush task works regularly to keep memory consumption of internal staging tables within projected boundaries in the steady state. The boundary is 10 percent of memory consumption of the current temporal table. Consider doing massive updates in multiple batches, or while `SYSTEM_VERSIONING = OFF`, such as using updates to set the defaults for newly added columns.

The period of activation for the data flush task isn't configurable, but you can manually execute [sp_xtp_flush_temporal_history](../system-stored-procedures/temporal-table-sp-xtp-flush-temporal-history.md) as needed.

Consider using clustered columnstore as a storage option for a disk-based history table, especially if you plan to run analytics queries on historical data that make use of aggregate or windowing functions. In that case, a clustered columnstore index is an optimal choice for your history table. Clustered columnstore indexes provide good data compression, and behave in an *insert-friendly* manner, aligning with how history data is generated.

## Related content

- [System-versioned temporal tables with memory-optimized tables](system-versioned-temporal-tables-with-memory-optimized-tables.md)
- [Create a memory-optimized system-versioned temporal table](creating-a-memory-optimized-system-versioned-temporal-table.md)
- [Work with memory-optimized system-versioned temporal tables](working-with-memory-optimized-system-versioned-temporal-tables.md)
- [Monitor memory-optimized system-versioned temporal tables](monitoring-memory-optimized-system-versioned-temporal-tables.md)
- [Temporal tables](temporal-tables.md)
- [Temporal table system consistency checks](temporal-table-system-consistency-checks.md)
- [Manage retention of historical data in system-versioned temporal tables](manage-retention-of-historical-data-in-system-versioned-temporal-tables.md)
- [Temporal table metadata views and functions](temporal-table-metadata-views-and-functions.md)
