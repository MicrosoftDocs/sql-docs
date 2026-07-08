---
title: "sys.dm_resource_governor_external_resource_pools (Transact-SQL)"
description: sys.dm_resource_governor_external_resource_pools (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: 02/11/2025
ms.service: sql
ms.subservice: machine-learning-services
ms.topic: "reference"
f1_keywords:
  - "sys.dm_resource_governor_external_resource_pools_TSQL"
  - "sys.dm_resource_governor_external_resource_pools"
  - "dm_resource_governor_external_resource_pools"
  - "dm_resource_governor_external_resource_pools_TSQL"
helpviewer_keywords:
  - "dm_resource_governor_external_resource_pools"
  - "sys.dm_resource_governor_external_resource_pools"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---

# sys.dm_resource_governor_external_resource_pools (Transact-SQL)

[!INCLUDE [SQL Server 2016 and later](../../includes/applies-to-version/sqlserver2016.md)]

Returns information about the current external resource pool state, the current configuration of resource pools, and resource pool statistics. 

| Column name | Data type | Description |
|:--|:--|:--|
| `external_pool_id` | **int** | The ID of the resource pool. Not nullable |
| `name` | **sysname** | The name of the resource pool. Not nullable |
| `pool_version` | **int** | Internal version number. |
| `max_cpu_percent` | **int** | The current configuration for the maximum average CPU bandwidth allowed for all requests in the resource pool when there is CPU contention. Not nullable. |
| `max_processes` | **int** | Maximum number of concurrent external processes. The default value, 0, specifies no limit. Not nullable. |
| `max_memory_percent` | **int** | The current configuration for the percentage of total server memory that can be used by requests in this resource pool. Not nullable. |
| `statistics_start_time` | **datetime** | The time when statistics was reset for this pool. Not nullable. |
| `peak_memory_kb` | **bigint** | The maximum amount of memory used, in kilobytes, for the resource pool. Not nullable. |
| `write_io_count` | **int** | The total write IOs issued since the Resource Governor statistics were reset. Not nullable. |
| `read_io_count` | **int** | The total read IOs issued since the Resource Governor statistics were reset. Not nullable. |
| `total_cpu_kernel_ms` | **bigint** | The cumulative CPU user kernel time in milliseconds since the Resource Governor statistics were reset. Not nullable. |
| `total_cpu_user_ms` | **bigint** | The cumulative CPU user time in milliseconds since the Resource Governor statistics were reset. Not nullable. |
| `active_processes_count` | **int** | The number of external processes running at the moment of the request. Not nullable. |

## Permissions

Requires the `VIEW SERVER STATE` permission.

### Permissions for SQL Server 2022 and later

Requires the `VIEW SERVER PERFORMANCE STATE` permission on the server.

## Related content

- [sys.dm_resource_governor_external_resource_pool_affinity (Transact-SQL)](sys-dm-resource-governor-external-resource-pool-affinity-transact-sql.md)
