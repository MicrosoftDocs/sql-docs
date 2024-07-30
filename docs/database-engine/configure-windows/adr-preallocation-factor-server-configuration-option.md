---
title: "Server configuration: ADR Preallocation Factor"
description: "Explains the SQL Server instance configuration setting for ADR preallocation factor."
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 07/18/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "ADR Preallocation Factor"
---
# Server configuration: ADR Preallocation Factor

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

[!INCLUDE [sssql19-starting-md](../../includes/sssql19-starting-md.md)], this configuration setting is required for [accelerated database recovery](../../relational-databases/accelerated-database-recovery-concepts.md).

Accelerated database recovery (ADR) maintains versions of data for recovery purposes. These versions are generated as part of various data manipulation language (DML) operations. Versions are stored in an internal table called the persistent version store (PVS).

## Remarks

Performance can degrade if pages are allocated for the PVS as part of foreground user DML operations. A background thread preallocates pages, and keeps them readily available for DML transactions. Performance is best when the background thread preallocates enough pages and the percentage of foreground PVS allocations is close to 0. The error log contains entires with the tag `PreallocatePVS` if the percentage goes high and is affecting performance.

The number of pages the background thread preallocates, is based on various workload heuristics, but largely allocates pages in chunks of 512 pages. The ADR preallocation factor is a multiple of the chunk. By default, the factor is `4`, which means that it preallocates 2048 pages at once when required.

While the background thread takes workload patterns into consideration, this factor can be increased if necessary to improve performance.

> [!CAUTION]  
> If PVS preallocation is increased too much, it will contend with other allocations in the system and might actually reduce overall performance. Before you modify this setting, test the overall performance of the system.

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
