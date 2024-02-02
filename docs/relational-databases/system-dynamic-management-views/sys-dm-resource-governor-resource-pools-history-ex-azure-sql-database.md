---
title: "sys.dm_resource_governor_resource_pools_history_ex (Transact-SQL)"
description: Each row in sys.dm_resource_governor_resource_pools_history_ex represents a periodic snapshot of resource pool statistics in Azure SQL Database.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: wiassaf, randolphwest
ms.date: 01/24/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.resource_governor"
  - "sys.resource_governor_TSQL"
  - "resource_governor"
  - "resource_governor_TSQL"
helpviewer_keywords:
  - "sys.resource_governor catalog view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current"
---
# sys.dm_resource_governor_resource_pools_history_ex (Transact-SQL)

[!INCLUDE [asdb-asdbmi](../../includes/applies-to-version/asdb-asdbmi.md)]

Each row represents a periodic snapshot of resource pool statistics in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)]. A snapshot is taken when the database engine starts, and every few seconds thereafter. The interval between the current and the previous snapshot can vary, and is provided in the `duration_ms` column. The latest available snapshots are returned, up to 128 snapshots for each resource pool.

> [!IMPORTANT]  
> Most of the data surfaced by this DMV is intended for internal consumption and is subject to change.

| Column name | Data type | Description |
| --- | --- | --- |
| `pool_id` | **int** | The ID of the resource pool. Not nullable. |
| `name` | **sysname** | The name of the resource pool. Not nullable. |
| `snapshot_time` | **datetime2** | Datetime of the resource pool stats snapshot taken. |
| `duration_ms` | **int** | Duration between current and previous snapshot. |
| `statistics_start_time` | **datetime2** | The time when statistics was reset for this pool. Not nullable. |
| `active_session_count` | **int** | Total active sessions in current snapshot. |
| `active_worker_count` | **int** | Total workers in current snapshot. |
| `delta_cpu_usage_ms` | **int** | CPU usage in milliseconds since last snapshot. Not nullable. |
| `delta_cpu_usage_preemptive_ms` | **int** | Preemptive Win32 calls not governed by SQL CPU RG, since last snapshot. |
| `used_data_space_kb` | **bigint** | Total space used in user databases associated with user pool. |
| `allocated_disk_space_kb` | **bigint** | Total data file size of user databases in the associated with user pool. |
| `target_memory_kb` | **bigint** | The target amount of memory, in kilobytes, the resource pool is trying to attain. This is based on the current settings and server state. Not nullable. |
| `used_memory_kb` | **bigint** | The amount of memory used, in kilobytes, for the resource pool. Not nullable. |
| `cache_memory_kb` | **bigint** | The current total cache memory usage in kilobytes. Not nullable. |
| `compile_memory_kb` | **bigint** | The current total stolen memory usage in kilobytes (KB). Most of this usage would be for compile and optimization, but it can also include other memory users. Not nullable. |
| `active_memgrant_count` | **bigint** | The current count of memory grants. Not nullable. |
| `active_memgrant_kb` | **bigint** | The sum, in kilobytes (KB), of current memory grants. Not nullable. |
| `used_memgrant_kb` | **bigint** | The current total used (stolen) memory from memory grants. Not nullable. |
| `delta_memgrant_timeout_count` | **int** | The count of memory grant time-outs in this resource pool in this period. Not nullable. |
| `delta_memgrant_waiter_count` | **int** | The count of queries currently pending on memory grants. Not nullable. |
| `delta_out_of_memory_count` | **int** | The number of failed memory allocations in the pool since last snapshot. Not nullable. |
| `delta_read_io_queued` | **int** | The total read IOs enqueued since last snapshot. Nullable. Null if the resource pool isn't governed for IO. |
| `delta_read_io_issued` | **int** | The total read IOs issued since last snapshot. Nullable. Null if the resource pool isn't governed for IO. |
| `delta_read_io_completed` | **int** | The total read IOs completed since last snapshot. Not nullable. |
| `delta_read_io_throttled` | **int** | The total read IOs throttled since snapshot. Nullable. Null if the resource pool isn't governed for IO. |
| `delta_read_bytes` | **bigint** | The total number of bytes read since last snapshot. Not nullable. |
| `delta_read_io_stall_ms` | **int** | Total time (in milliseconds) between read IO arrival and completion since last snapshot. Not nullable. |
| `delta_read_io_stall_queued_ms` | **int** | Total time (in milliseconds) between read IO arrival and issue since last snapshot. Nullable. Null if the resource pool isn't governed for IO. Non-zero `delta_read_io_stall_queued_ms` means IOs are being delayed by resource governance. |
| `delta_write_io_queued` | **int** | The total write IOs enqueued since last snapshot. Nullable. Null if the resource pool isn't governed for IO. |
| `delta_write_io_issued` | **int** | The total write IOs issued since last snapshot. Nullable. Null if the resource pool isn't governed for IO. |
| `delta_write_io_completed` | **int** | The total write IOs completed since last snapshot. Not nullable. |
| `delta_write_io_throttled` | **int** | The total write IOs throttled since last snapshot. Not nullable. |
| `delta_write_bytes` | **bigint** | The total number of bytes written since last snapshot. Not nullable. |
| `delta_write_io_stall_ms` | **int** | Total time (in milliseconds) between write IO arrival and completion since last snapshot. Not nullable. |
| `delta_write_io_stall_queued_ms` | **int** | Total time (in milliseconds) between write IO arrival and issue since last snapshot. Nullable. Null if the resource pool isn't governed for IO. |
| `delta_io_issue_delay_ms` | **int** | Total time (in milliseconds) between the scheduled issue and actual issue of IO since last snapshot. Nullable. Null if the resource pool isn't governed for IO. |
| `max_iops_per_volume` | **int** | The Maximum IO per second (IOPS) per disk volume setting for this Pool. Nullable. Null if the resource pool isn't governed for IO. |
| `max_memory_kb` | **bigint** | The maximum amount of memory, in kilobytes, that the resource pool can have. This is based on the current settings and server state. Not nullable. |
| `max_log_rate_kb` | **bigint** | Maximum log rate (kilo-bytes per sec) at resource pool level. |
| `max_data_space_kb` | **bigint** | Max elastic pool storage limit setting for this elastic pool in kilobytes. |
| `max_session` | **int** | Session limit for the pool. |
| `max_worker` | **int** | Worker limit for the pool. |
| `min_cpu_percent` | **int** | The current configuration for the guaranteed average CPU bandwidth for all requests in the resource pool when there's CPU contention. Not nullable. |
| `max_cpu_percent` | **int** | The current configuration for the maximum average CPU bandwidth allowed for all requests in the resource pool when there's CPU contention. Not nullable. |
| `cap_cpu_percent` | **int** | Hard cap on the CPU bandwidth that all requests in the resource pool receive. Limits the maximum CPU bandwidth level to the specified level. The allowed range for value is from 1 through 100. Not nullable. |
| `min_vcores` | **decimal(5,2)** | The current configuration for the guaranteed average CPU bandwidth for all requests in the resource pool when there's CPU contention. In units of vCores. |
| `max_vcores` | **decimal(5,2)** | The current configuration for the maximum average CPU bandwidth allowed for all requests in the resource pool when there's CPU contention. In unit of vCores. |
| `cap_vcores` | **decimal(5,2)** | Hard cap on the CPU bandwidth that all requests in the resource pool receive. In unit of vCores. |
| `instance_cpu_count` | **int** | Number of CPU configured for the instance. |
| `instance_cpu_percent` | **decimal(5,2)** | CPU percent configured for the instance. |
| `instance_vcores` | **decimal(5,2)** | Number of vCores configured for the instance. |
| `delta_log_bytes_used` | **decimal(5,2)** | Total log generation (in bytes) at pool level since last snapshot. |
| `avg_login_rate_percent` | **decimal(5,2)** | Number of logins since last snapshot, compared against login limit. |
| `delta_vcores_used` | **decimal(5,2)** | Compute utilization in count of vCores since last snapshot. |
| `cap_vcores_used_percent` | **decimal(5,2)** | Average compute utilization in percentage of the limit of the pool. |
| `instance_vcores_used_percent` | **decimal(5,2)** | Average compute utilization in percentage of the limit of the SQL instance. |
| `avg_data_io_percent` | **decimal(5,2)** | Average I/O utilization in percentage based on the limit of the pool. |
| `avg_log_write_percent` | **decimal(5,2)** | Average write resource utilization in percentage of the limit of the pool. |
| `avg_storage_percent` | **decimal(5,2)** | Average storage utilization in percentage of the storage limit of the pool. |
| `avg_allocated_storage_percent` | **decimal(5,2)** | The percentage of data space allocated by all databases in the elastic pool. This is the ratio of data space allocated to data max size for the elastic pool. For more information, visit [File space management in SQL Database](/azure/sql-database/sql-database-file-space-management). |
| `max_worker_percent` | **decimal(5,2)** | Maximum concurrent workers (requests) in percentage based on the limit of the pool. |
| `max_session_percent` | **decimal(5,2)** | Maximum concurrent sessions in percentage based on the limit of the pool. |
| `active_outbound_connection_worker_count` | **int** | Total outbound connection workers in current snapshot. |
| `max_outbound_connection_worker` | **int** | Outbound connection worker limit for the pool. |
| `max_outbound_connection_worker_percent` | **decimal(5,2)** | Maximum concurrent outbound connection workers (requests) in percentage based on the limit of the pool. |

## Permissions

This view requires `VIEW SERVER STATE` permission.

## Remarks

Users can access this dynamic management view to monitor near real time resource consumption for user workload pool and system internal pools of Azure SQL Database instance.

## Examples

The following example returns maximum log rate data and consumption at each snapshot by user pool:

```sql
SELECT snapshot_time,
    name,
    max_log_rate_kb,
    delta_log_bytes_used
FROM sys.dm_resource_governor_resource_pools_history_ex
WHERE name LIKE 'SloSharedPool1'
ORDER BY snapshot_time DESC;
```

## Related content

- [Translation log rate governance](/azure/sql-database/sql-database-resource-limits-database-server#transaction-log-rate-governance)
- [Elastic pool DTU resource limits](/azure/sql-database/sql-database-dtu-resource-limits-elastic-pools)
- [Elastic pool vCore resource limits](/azure/sql-database/sql-database-vcore-resource-limits-elastic-pools)
- [Manage elastic pools in Azure SQL Database](/azure/azure-sql/database/elastic-pool-manage)
- [sys.elastic_pool_resource_stats (Azure SQL Database)](../system-catalog-views/sys-elastic-pool-resource-stats-azure-sql-database.md)
- [sys.dm_elastic_pool_resource_stats (Azure SQL Database)](sys-dm-elastic-pool-resource-stats-azure-sql-database.md)
