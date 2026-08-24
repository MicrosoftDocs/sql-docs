---
title: "sys.resource_governor_external_resource_pools (Transact-SQL)"
description: sys.resource_governor_external_resource_pools (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: 02/11/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.resource_governor_external_resource_pools"
  - "sys.resource_governor_external_resource_pools_TSQL"
  - "resource_governor_external_resource_pools"
  - "resource_governor_external_resource_pools_TSQL"
helpviewer_keywords:
  - "sys.resource_governor_external_resource_pools"
  - "resource_governor_external_resource_pools"
dev_langs:
  - "TSQL"
---

# sys.resource_governor_external_resource_pools (Transact-SQL)

[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

**Applies to:** [!INCLUDE[sssql15-md](../../includes/sssql16-md.md)] [!INCLUDE[rsql-productname-md](../../includes/rsql-productname-md.md)] and [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)] [!INCLUDE[rsql-productnamenew-md](../../includes/rsql-productnamenew-md.md)]

Returns the stored external resource pool configuration in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Each row of the view determines the configuration of a pool.

| Column name | Data type | Description |
|:--|:--|:--|
| `external_pool_id` | **int** | Unique ID of the resource pool. Not nullable. |
| `name` | **sysname** | Name of the resource pool. Not nullable. |
| `max_cpu_percent` | **int** | Maximum average CPU bandwidth allowed for all requests in the resource pool when there is CPU contention. Not nullable. |
| `max_memory_percent` | **int** | Percentage of total server memory that can be used by requests in this resource pool. Not nullable. The effective maximum depends on the pool minimums. For example, max_memory_percent can be set to 100, but the effective maximum is lower. |
| `max_processes` | **int** | Maximum number of concurrent external processes. The default value, 0, specifies no limit. Not nullable. |
| `version` | **bigint** | Internal version number. |

## Permissions

Requires the `VIEW SERVER STATE` permission.

## Related content

- [Manage Python and R workloads with Resource Governor in SQL Server Machine Learning Services](../../machine-learning/administration/resource-governor.md)
- [Resource governor catalog views (Transact-SQL)](resource-governor-catalog-views-transact-sql.md)
- [sys.dm_resource_governor_resource_pools (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-resource-governor-resource-pools-transact-sql.md)
- [Resource governor](../resource-governor/resource-governor.md)
- [sys.dm_resource_governor_resource_pool_affinity (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-resource-governor-resource-pool-affinity-transact-sql.md)
- [Server configuration: external scripts enabled](../../database-engine/configure-windows/external-scripts-enabled-server-configuration-option.md)
- [ALTER EXTERNAL RESOURCE POOL (Transact-SQL)](../../t-sql/statements/alter-external-resource-pool-transact-sql.md)
