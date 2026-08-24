---
title: "sys.resource_governor_resource_pools (Transact-SQL)"
description: sys.resource_governor_resource_pools (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: dfurman
ms.date: 02/11/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.resource_governor_resource_pools"
  - "resource_governor_resource_pools"
  - "sys.resource_governor_resource_pools_TSQL"
  - "resource_governor_resource_pools_TSQL"
helpviewer_keywords:
  - "sys.resource_governor_resource_pools catalog view"
dev_langs:
  - "TSQL"
---

# sys.resource_governor_resource_pools (Transact-SQL)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Returns the stored resource pool configuration. Each row represents a resource pool.

| Column name | Data type | Description |
|:--|:--|:--|
| `pool_id` | **int** | Unique ID of the resource pool. Not nullable.|
| `name` | **sysname** | Name of the resource pool. Not nullable.|
| `min_cpu_percent` | **int** | Guaranteed average CPU bandwidth for all requests in the resource pool when there is CPU contention. Not nullable. |
| `max_cpu_percent` | **int** | Maximum average CPU bandwidth allowed for all requests in the resource pool when there is CPU contention. Not nullable. |
| `min_memory_percent` | **int** | Guaranteed amount of query workspace memory for all requests in the resource pool. This is not shared with other resource pools. Not nullable. |
| `max_memory_percent` | **int** | Percentage of total query workspace memory that can be used by requests in this resource pool. Not nullable. The effective maximum depends on the other pool minimums. For example, `max_memory_percent` can be set to 100, but the effective maximum might be lower. Not nullable. |
| `cap_cpu_percent` | **int** | **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later.<br /><br /> Hard cap on the CPU bandwidth that all requests in the resource pool receive. Limits the maximum CPU bandwidth to the specified level. The allowed range for value is from 1 through 100. Not nullable. |
| `min_iops_per_volume` | **int** | **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and later.<br /><br /> The minimum I/O operations per second (IOPS) per volume setting for this pool. 0 = no reservation. Not nullable. |
| `max_iops_per_volume` | **int** | **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and later.<br /><br /> The maximum I/O operations per second (IOPS) per volume setting for this pool. 0 = unlimited. Not nullable. |

## Remarks

This catalog view displays the stored metadata. To see the currently effective resource governor configuration, use the corresponding dynamic management view, [sys.dm_resource_governor_resource_pools (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-resource-governor-resource-pools-transact-sql.md).

## Permissions

Requires the `VIEW ANY DEFINITION` permission.

## Related content

- [Resource governor catalog views (Transact-SQL)](resource-governor-catalog-views-transact-sql.md)
- [sys.dm_resource_governor_resource_pools (Transact-SQL)](../system-dynamic-management-objects/sys-dm-resource-governor-resource-pools-transact-sql.md)
- [Resource governor](../resource-governor/resource-governor.md)
- [sys.resource_governor_external_resource_pools (Transact-SQL)](sys-resource-governor-external-resource-pools-transact-sql.md)
