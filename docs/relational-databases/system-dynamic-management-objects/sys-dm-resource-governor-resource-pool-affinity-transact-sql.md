---
title: "sys.dm_resource_governor_resource_pool_affinity (Transact-SQL)"
description: sys.dm_resource_governor_resource_pool_affinity tracks resource pool affinity.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: dfurman
ms.date: 02/11/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_resource_governor_resource_pool_affinity_TSQL"
  - "sys.dm_resource_governor_resource_pool_affinity"
  - "dm_resource_governor_resource_pool_affinity"
  - "dm_resource_governor_resource_pool_affinity_TSQL"
helpviewer_keywords:
  - "dm_resource_governor_resource_pool_affinity"
  - "sys.dm_resource_governor_resource_pool_affinity"
dev_langs:
  - "TSQL"
---

# sys.dm_resource_governor_resource_pool_affinity (Transact-SQL)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Returns the CPU and NUMA node affinity data for a resource pool.

| Column name | Data type | Description |
|:--|:--|:--|
| `pool_id` | **int** | The ID of the resource pool. Not nullable. |
| `processor_group` | **smallint** | The ID of the Windows logical processor group. Not nullable. |
| `scheduler_mask` | **bigint** | The binary mask representing the schedulers associated with this pool. Not nullable. |

## Remarks

Pools that are created with an affinity of AUTO won't appear in this view, because they have no affinity. For more information, see the [CREATE RESOURCE POOL (Transact-SQL)](../../t-sql/statements/create-resource-pool-transact-sql.md) and [ALTER RESOURCE POOL (Transact-SQL)](../../t-sql/statements/alter-resource-pool-transact-sql.md) statements.

## Related content

- [sys.dm_resource_governor_external_resource_pool_affinity (Transact-SQL)](sys-dm-resource-governor-external-resource-pool-affinity-transact-sql.md)
