---
title: "sys.dm_resource_governor_resource_pools (Transact-SQL)"
description: Returns information about the current resource pool state, the current configuration of resource pools, and resource pool statistics.
author: rwestMSFT
ms.author: randolphwest
ms.date: 01/24/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_resource_governor_resource_pools_TSQL"
  - "dm_resource_governor_resource_pools_TSQL"
  - "sys.dm_resource_governor_resource_pools"
  - "dm_resource_governor_resource_pools"
helpviewer_keywords:
  - "sys.dm_resource_governor_resource_pools dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sys.dm_resource_governor_resource_pools (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Returns information about the current resource pool state, the current configuration of resource pools, and resource pool statistics.

> [!NOTE]  
> To call this from [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] or [!INCLUDE [ssPDW](../../includes/sspdw-md.md)], use the name `sys.dm_pdw_nodes_resource_governor_resource_pools`. [!INCLUDE [synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]

| Column name | Data type | Description |
| --- | --- | --- |
| `pool_id` | **int** | The ID of the resource pool. Not nullable. |
| `name` | **sysname** | The name of the resource pool. Not nullable. |
| `statistics_start_time` | **datetime** | The time when statistics was reset for this pool. Not nullable. |
| `total_cpu_usage_ms` | **bigint** | The cumulative CPU usage in milliseconds since the Resource Governor statistics were reset. Not nullable. |
| `cache_memory_kb` | **bigint** | The current total cache memory usage in kilobytes. Not nullable. |
| `compile_memory_kb` | **bigint** | The current total stolen memory usage in kilobytes (KB). Most this usage would be for compile and optimization, but it can also include other memory users. Not nullable. |
| `used_memgrant_kb` | **bigint** | The current total used (stolen) memory from memory grants. Not nullable. |
| `total_memgrant_count` | **bigint** | The cumulative count of memory grants in this resource pool. Not nullable. |
| `total_memgrant_timeout_count` | **bigint** | The cumulative count of memory grant time-outs in this resource pool. Not nullable. |
| `active_memgrant_count` | **int** | The current count of memory grants. Not nullable. |
| `active_memgrant_kb` | **bigint** | The sum, in kilobytes (KB), of current memory grants. Not nullable. |
| `memgrant_waiter_count` | **int** | The count of queries currently pending on memory grants. Not nullable. |
| `max_memory_kb` | **bigint** | The maximum amount of memory, in kilobytes, that the resource pool can have. This is based on the current settings and server state. Not nullable. |
| `used_memory_kb` | **bigint** | The amount of memory used, in kilobytes, for the resource pool. Not nullable. |
| `target_memory_kb` | **bigint** | The target amount of memory, in kilobytes, the resource pool is trying to attain. This is based on the current settings and server state. Not nullable. |
| `out_of_memory_count` | **bigint** | The number of failed memory allocations in the pool since the Resource Governor statistics were reset. Not nullable. |
| `min_cpu_percent` | **int** | The current configuration for the guaranteed average CPU bandwidth for all requests in the resource pool when there's CPU contention. Not nullable. |
| `max_cpu_percent` | **int** | The current configuration for the maximum average CPU bandwidth allowed for all requests in the resource pool when there's CPU contention. Not nullable. |
| `min_memory_percent` | **int** | The current configuration for the guaranteed amount of memory for all requests in the resource pool when there's memory contention. This isn't shared with other resource pools. Not nullable. |
| `max_memory_percent` | **int** | The current configuration for the percentage of total server memory that can be used by requests in this resource pool. Not nullable. |
| `cap_cpu_percent` | **int** | Hard cap on the CPU bandwidth that all requests in the resource pool receive. Limits the maximum CPU bandwidth level to the specified level. The allowed range for value is from 1 through 100. Not nullable.<br /><br />**Applies to**: [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] and later versions |
| `min_iops_per_volume` | **int** | The minimum I/O per second (IOPS) per disk volume setting for this Pool. Nullable. `NULL` if the resource pool isn't governed for I/O. That is, the Resource Pool MIN_IOPS_PER_VOLUME and MAX_IOPS_PER_VOLUME settings are 0.<br /><br />**Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions |
| `max_iops_per_volume` | **int** | The maximum I/O per second (IOPS) per disk volume setting for this Pool. Nullable. `NULL` if the resource pool isn't governed for I/O. That is, the Resource Pool MIN_IOPS_PER_VOLUME and MAX_IOPS_PER_VOLUME settings are 0.<br /><br />**Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions |
| `read_io_queued_total` | **int** | The total read I/Os enqueued since the Resource Governor was reset. Nullable. `NULL` if the resource pool isn't governed for I/O. That is, the Resource Pool MIN_IOPS_PER_VOLUME and MAX_IOPS_PER_VOLUME settings are 0.<br /><br />**Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions |
| `read_io_issued_total` | **int** | The total read I/Os issued since the Resource Governor statistics were reset. Nullable. `NULL` if the resource pool isn't governed for I/O. That is, the Resource Pool MIN_IOPS_PER_VOLUME and MAX_IOPS_PER_VOLUME settings are 0.<br /><br />**Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions |
| `read_io_completed_total` | **int** | The total read I/Os completed since the Resource Governor statistics were reset. Not nullable. |
| `read_io_throttled_total` | **int** | The total read I/Os throttled since the Resource Governor statistics were reset. Nullable. `NULL` if the resource pool isn't governed for I/O. That is, the Resource Pool MIN_IOPS_PER_VOLUME and MAX_IOPS_PER_VOLUME settings are 0.<br /><br />**Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions |
| `read_bytes_total` | **bigint** | The total number of bytes read since the Resource Governor statistics were reset. Not nullable.<br /><br />**Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions |
| `read_io_stall_total_ms` | **bigint** | Total time (in milliseconds) between read I/O arrival and completion. Not nullable.<br /><br />**Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions |
| `read_io_stall_queued_ms` | **bigint** | Total time (in milliseconds) between read I/O arrival and issue. Nullable. `NULL` if the resource pool isn't governed for I/O. That is, the Resource Pool MIN_IOPS_PER_VOLUME and MAX_IOPS_PER_VOLUME settings are 0.<br /><br />To determine if the I/O setting for the pool is causing latency, subtract **read_io_stall_queued_ms** from **read_io_stall_total_ms**.<br /><br />**Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions |
| `write_io_queued_total` | **int** | The total write I/Os enqueued since the Resource Governor statistics were reset. Nullable. `NULL` if the resource pool isn't governed for I/O. That is, the Resource Pool MIN_IOPS_PER_VOLUME and MAX_IOPS_PER_VOLUME settings are 0.<br /><br />**Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions |
| `write_io_issued_total` | **int** | The total write I/Os issued since the Resource Governor statistics were reset. Nullable. `NULL` if the resource pool isn't governed for I/O. That is, the Resource Pool MIN_IOPS_PER_VOLUME and MAX_IOPS_PER_VOLUME settings are 0.<br /><br />**Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions |
| `write_io_completed_total` | **int** | The total write I/Os completed since the Resource Governor statistics were reset. Not nullable.<br /><br />**Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions |
| `write_io_throttled_total` | **int** | The total write I/Os throttled since the Resource Governor statistics were reset. Not nullable.<br /><br />**Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions |
| `write_bytes_total` | **bigint** | The total number of bytes written since the Resource Governor statistics were reset. Not nullable.<br /><br />**Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions |
| `write_io_stall_total_ms` | **bigint** | Total time (in milliseconds) between write I/O arrival and completion. Not nullable.<br /><br />**Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions |
| `write_io_stall_queued_ms` | **bigint** | Total time (in milliseconds) between write I/O arrival and issue. Nullable. `NULL` if the resource pool isn't governed for I/O. That is, the Resource Pool MIN_IOPS_PER_VOLUME and MAX_IOPS_PER_VOLUME settings are 0.<br /><br />This is the delay introduced by I/O Resource Governance.<br /><br />**Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions |
| `io_issue_violations_total` | **int** | Total I/O issue violations. That is, the number of times when the rate of I/O issue was lower than the reserved rate. Nullable. `NULL` if the resource pool isn't governed for I/O. That is, the Resource Pool MIN_IOPS_PER_VOLUME and MAX_IOPS_PER_VOLUME settings are 0.<br /><br />**Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions |
| `io_issue_delay_total_ms` | **bigint** | Total time (in milliseconds) between the scheduled issue and actual issue of I/O. Nullable. `NULL` if the resource pool isn't governed for I/O. That is, the Resource Pool MIN_IOPS_PER_VOLUME and MAX_IOPS_PER_VOLUME settings are 0.<br /><br />**Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions |
| `io_issue_ahead_total_ms` | **bigint** | Internal use only.<br /><br />**Applies to:** [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions |
| `reserved_io_limited_by_volume_total` | **bigint** | Internal use only.<br /><br />**Applies to:** [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions |
| `io_issue_delay_non_throttled_total_ms` | **bigint** | Total time (in milliseconds) between the scheduled issue and actual issue of a non-throttled I/O.<br /><br />**Applies to:** [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions |
| `total_cpu_delayed_ms` | **bigint** | Total time (in milliseconds) between a runnable worker yields, and the operating system gives back control to another runnable worker in the [!INCLUDE [ssDE-md](../../includes/ssde-md.md)]. This could be the Idle worker.<br /><br />**Applies to:** [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions |
| `total_cpu_active_ms` | **bigint** | Total active CPU time (in milliseconds).<br /><br />**Applies to:** [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions |
| `total_cpu_violation_delay_ms` | **bigint** | Total CPU violation delays (in milliseconds). That is, total CPU time delay that was lower than the minimum guaranteed delay between a runnable worker yields, and the operating system gives back control to another runnable worker in the [!INCLUDE [ssDE-md](../../includes/ssde-md.md)].<br /><br />**Applies to:** [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions |
| `total_cpu_violation_sec` | **bigint** | Total CPU violations (in seconds). That is, total time accrued when a CPU time violation was in-flight.<br /><br />**Applies to:** [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions |
| `total_cpu_usage_preemptive_ms` | **bigint** | Total CPU time used while in preemptive mode scheduling for the workload group (in milliseconds). Not nullable.<br /><br />To execute code that is outside the [!INCLUDE [ssDE-md](../../includes/ssde-md.md)] (for example, extended stored procedures and distributed queries), a thread has to execute outside the control of the non-preemptive scheduler. To do this, a worker switches to preemptive mode.<br /><br />**Applies to:** [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions |
| `max_vcores` | **decimal(5,2)** | The current configuration for the maximum average CPU bandwidth allowed for all requests in the resource pool when there's CPU contention. In unit of vCores.<br /><br />**Applies to**: [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)] |
| `total_cpu_usage_actual_ms` | **bigint** | [!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)] |
| `pdw_node_id` | **int**` | The identifier for the node that this distribution is on.<br /><br />**Applies to**: [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)], [!INCLUDE [ssPDW](../../includes/sspdw-md.md)] |

## Remarks

Resource Governor workload groups and Resource Governor resource pools have a many-to-one mapping. As a result, many of the resource pool statistics are derived from the workload group statistics.

This dynamic management view shows the in-memory configuration. To see the stored configuration metadata, use the `sys.resource_governor_resource_pools` catalog view.

## Permissions

Requires `VIEW SERVER STATE` permission.

### Permissions for SQL Server 2022 and later

Requires `VIEW SERVER PERFORMANCE STATE` permission on the server.

## Related content

- [Dynamic Management Views and Functions (Transact-SQL)](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)
- [sys.dm_resource_governor_workload_groups (Transact-SQL)](sys-dm-resource-governor-workload-groups-transact-sql.md)
- [sys.resource_governor_resource_pools (Transact-SQL)](../system-catalog-views/sys-resource-governor-resource-pools-transact-sql.md)
- [ALTER RESOURCE GOVERNOR (Transact-SQL)](../../t-sql/statements/alter-resource-governor-transact-sql.md)
