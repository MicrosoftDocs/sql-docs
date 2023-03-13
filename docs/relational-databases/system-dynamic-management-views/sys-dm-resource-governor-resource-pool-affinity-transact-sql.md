---
title: "sys.dm_resource_governor_resource_pool_affinity (Transact-SQL)"
description: sys.dm_resource_governor_resource_pool_affinity tracks resource pool affinity.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/28/2022
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

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Tracks resource pool affinity.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
| Column name | Data type | Description |
| --- | --- | --- |
| pool_id | **int** | The ID of the resource pool. Not nullable. |
| processor_group | **smallint** | The ID of the Windows logical processor group. Not nullable. |
| scheduler_mask | **bigint** | The binary mask representing the schedulers associated with this pool. Not nullable. |

## Remarks

Pools that are created with an affinity of AUTO won't appear in this view, because they have no affinity. For more information, see the [CREATE RESOURCE POOL (Transact-SQL)](../../t-sql/statements/create-resource-pool-transact-sql.md) and [ALTER RESOURCE POOL (Transact-SQL)](../../t-sql/statements/alter-resource-pool-transact-sql.md) statements.

## See also

- [sys.dm_resource_governor_external_resource_pool_affinity (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-resource-governor-external-resource-pool-affinity-transact-sql.md)
