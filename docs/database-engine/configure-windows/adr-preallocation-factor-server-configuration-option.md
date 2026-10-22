---
title: "Server Configuration: ADR Preallocation Factor"
description: "Explains the SQL Server instance configuration setting for ADR preallocation factor."
author: adbadram
ms.author: adbadram
ms.reviewer: dfurman
ms.date: 11/25/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
helpviewer_keywords:
  - "ADR Preallocation Factor"
---
# Server configuration: ADR Preallocation Factor

[!INCLUDE [sqlserver2019-asdbmi](../../includes/applies-to-version/sqlserver2019-asdbmi.md)]

Starting in [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)], and in [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], this configuration setting is used by [accelerated database recovery](../../relational-databases/accelerated-database-recovery-concepts.md).

Accelerated database recovery (ADR) uses row versions for transaction management and database recovery purposes. These versions are generated as part of various data manipulation language (DML) operations. Versions are stored either in-row, or off-row in an internal table called the persistent version store (PVS).

## Remarks

Performance can degrade if pages are allocated for persistent version store (PVS) as part of foreground user DML operations. A background thread preallocates pages, and keeps them readily available for inserts into PVS. Performance is optimal when the background thread preallocates enough pages that the percentage of foreground PVS allocations is close to 0.

The number of pages the background thread preallocates is based on various workload heuristics. Commonly, the background thread allocates chunks of 512 pages. The ADR preallocation factor is a multiple of the chunk. By default, the factor is `4`, which means that 2048 pages are preallocated at once when required.

While the background thread takes workload patterns into consideration, this factor can be increased if necessary to improve performance. The `tx_version_optimized_insert_stats` extended event fires periodically when space allocations in PVS occur and provides space allocation statistics. If the value in the `foreground_allocation_percent` field is significantly higher than zero, consider increasing the `ADR Preallocation Factor` setting.

> [!WARNING]  
> If PVS preallocation factor is increased too much, it can contend with other allocations in the system and might actually reduce overall performance. Before you modify this setting, obtain a baseline of the system performance for tracking and comparison purposes.

::: moniker range="= sql-server-linux-ver15 || = sql-server-ver15"

## Known issue

For [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] CU 12 and previous versions, this value might be set to `0`. We recommend that you reset the value to `4`, which is the designed default, using the example in this article.

::: moniker-end

## Examples

The following example sets the preallocation factor to `4`.

```sql
EXEC sp_configure 'show advanced options', 1;
RECONFIGURE;
GO
EXEC sp_configure 'ADR Preallocation Factor', 4;
RECONFIGURE;
GO
```

## Related content

- [Server configuration options](server-configuration-options-sql-server.md)
- [Accelerated database recovery](../../relational-databases/accelerated-database-recovery-concepts.md)
- [Manage accelerated database recovery](../../relational-databases/accelerated-database-recovery-management.md)
