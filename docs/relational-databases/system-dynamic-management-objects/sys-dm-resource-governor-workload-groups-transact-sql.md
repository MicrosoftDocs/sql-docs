---
title: "sys.dm_resource_governor_workload_groups (Transact-SQL)"
description: sys.dm_resource_governor_workload_groups (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: dfurman
ms.date: 04/15/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_resource_governor_workload_groups"
  - "sys.dm_resource_governor_workload_groups_TSQL"
  - "dm_resource_governor_workload_groups"
  - "dm_resource_governor_workload_groups_TSQL"
helpviewer_keywords:
  - "sys.dm_resource_governor_workload_groups dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
ms.custom:
  - build-2025
---

# sys.dm_resource_governor_workload_groups (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Returns workload group statistics and the current in-memory configuration of the workload group.

> [!NOTE]
>  To call this from [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the name **sys.dm_pdw_nodes_resource_governor_workload_groups**. [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]

| Column name | Data type | Description |
| --- | --- | --- |
| `group_id` | **int** | ID of the workload group. Not nullable. |
| `name` | **sysname** | Name of the workload group. Not nullable. |
| `pool_id` | **int** | ID of the resource pool. Not nullable. |
| `external_pool_id` | **int** | **Applies to**: Starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)].<br /><br /> ID of the external resource pool. Not nullable. |
| `statistics_start_time` | **datetime** | The time when statistics collection for the workload group started. Not nullable. |
| `total_request_count` | **bigint** | Cumulative count of completed requests in the workload group. Not nullable. |
| `total_queued_request_count` | **bigint** | Cumulative count of requests queued after the `GROUP_MAX_REQUESTS` limit was reached. Not nullable. |
| `active_request_count` | **int** | Current request count. Not nullable. |
| `queued_request_count` | **int** | Current queued request count. Not nullable. |
| `total_cpu_limit_violation_count` | **bigint** | Cumulative count of requests exceeding the CPU limit. Not nullable. |
| `total_cpu_usage_ms` | **bigint** | Cumulative CPU usage, in milliseconds, by this workload group. Not nullable. |
| `max_request_cpu_time_ms` | **bigint** | Maximum CPU usage, in milliseconds, for a single request. Not nullable.<br /><br /> **Note:** This is a measured value, unlike `request_max_cpu_time_sec`, which is a configurable setting. For more information, see [REQUEST_MAX_CPU_TIME_SEC](../../t-sql/statements/create-workload-group-transact-sql.md#request_max_cpu_time_sec--value). |
| `blocked_task_count` | **int** | Current count of blocked tasks. Not nullable. |
| `total_lock_wait_count` | **bigint** | Cumulative count of lock waits that occurred. Not nullable. |
| `total_lock_wait_time_ms` | **bigint** | Cumulative sum of elapsed time, in milliseconds, that a lock is held. Not nullable. |
| `total_query_optimization_count` | **bigint** | Cumulative count of query optimizations in this workload group. Not nullable. |
| `total_suboptimal_plan_generation_count` | **bigint** | Cumulative count of suboptimal plan generations that occurred in this workload group due to memory pressure. Not nullable. |
| `total_reduced_memgrant_count` | **bigint** | Cumulative count of memory grants that reached the maximum limit on the per-request memory grant size. Not nullable. |
| `max_request_grant_memory_kb` | **bigint** | Maximum memory grant size, in kilobytes, of a single request since the statistics were reset. Not nullable. |
| `active_parallel_thread_count` | **bigint** | Current count of parallel thread usage. Not nullable. |
| `importance` | **sysname** |Current configuration value for the relative importance of a request in this workload group. Importance is one of the following, with `Medium` being the default: `Low`, `Medium`, or `High`.<br /><br /> Not nullable.|
| `request_max_memory_grant_percent` | **int** | Current setting for the maximum memory grant, as a percentage, for a single request. Not nullable. |
| `request_max_cpu_time_sec` | **int** | Current setting for maximum CPU use limit, in seconds, for a single request. Not nullable. |
| `request_memory_grant_timeout_sec` | **int** | Current setting for memory grant time-out, in seconds, for a single request. Not nullable. |
| `group_max_requests` | **int** | Current setting for the maximum number of concurrent requests in the workload group. Not nullable. |
| `max_dop` | **int** | Configured maximum degree of parallelism for the workload group. The default value, 0, uses global settings. Not nullable. |
| `effective_max_dop` | **int** | **Applies to**: Starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)].<br /><br />Effective maximum degree of parallelism for the workload group. Not nullable. |
| `total_cpu_usage_preemptive_ms` | **bigint** | **Applies to**: Starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)].<br /><br />Total CPU time used while in preemptive mode scheduling for the workload group, measured in milliseconds. Not nullable.<br /><br />To execute code that is outside the [!INCLUDE [ssDE-md](../../includes/ssde-md.md)] (for example, extended stored procedures and distributed queries), a thread has to execute outside the control of the non-preemptive scheduler. To do this, a worker switches to preemptive mode. |
| `request_max_memory_grant_percent_numeric` | **float** |**Applies to**: [!INCLUDE[ssazuremi-md](../../includes/ssazuremi-md.md)] and starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)].<br /><br />Current setting for the maximum memory grant, as a percentage, for a single request. The value is similar to `request_max_memory_grant_percent`. However, unlike `request_max_memory_grant_percent` which returns an `integer` value, `request_max_memory_grant_percent_numeric` returns a `float` value. Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)], the parameter `REQUEST_MAX_MEMORY_GRANT_PERCENT` accepts values with a possible range of 0-100 and stores them as the `float` data type. Prior to [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)], `REQUEST_MAX_MEMORY_GRANT_PERCENT` is an `integer` with possible range of 1-100. For more information, see [CREATE WORKLOAD GROUP](../../t-sql/statements/create-workload-group-transact-sql.md).<br /><br />Not nullable. |
| `tempdb_data_space_kb` | **bigint** | **Applies to**: Starting with [!INCLUDE [sql-server-2025](../../includes/sssql25-md.md)]<br /><br />The current data space consumed in the `tempdb` data files by all sessions in the workload group, in kilobytes. Nullable. |
| `peak_tempdb_data_space_kb` | **bigint** | **Applies to**: Starting with [!INCLUDE [sql-server-2025](../../includes/sssql25-md.md)]<br /><br />The peak data space consumed in the `tempdb` data files by all sessions in the workload group since the server startup, or since resource governor statistics were reset, in kilobytes. Nullable.|
| `total_tempdb_data_limit_violation_count` | **bigint** | **Applies to**: Starting with [!INCLUDE [sql-server-2025](../../includes/sssql25-md.md)]<br /><br />The number of times a request was aborted with error 1138 because it would exceed the limit on tempdb data space consumption for the workload group. Nullable. |
| `pdw_node_id` | **int** | **Applies to**: [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]<br /><br /> The identifier for the node that this distribution is on. |

## Remarks

This dynamic management view shows the in-memory configuration. To see the stored configuration metadata, use the [sys.resource_governor_workload_groups](../system-catalog-views/sys-resource-governor-workload-groups-transact-sql.md) catalog view.

This view can be joined with [sys.dm_resource_governor_resource_pools](sys-dm-resource-governor-resource-pools-transact-sql.md) to get the resource pool name.

Statistics are tracked since the last start of the [!INCLUDE [ssDE-md](../../includes/ssde-md.md)]. When `ALTER RESOURCE GOVERNOR RESET STATISTICS` is executed, the following counters are reset: `statistics_start_time`, `total_request_count`, `total_queued_request_count`, `total_cpu_limit_violation_count`, `total_cpu_usage_ms`, `max_request_cpu_time_ms`, `total_lock_wait_count`, `total_lock_wait_time_ms`, `total_query_optimization_count`, `total_suboptimal_plan_generation_count`, `total_reduced_memgrant_count`, `max_request_grant_memory_kb`, `peak_tempdb_data_space_kb`, and `total_tempdb_data_limit_violation_count`. The counter `statistics_start_time` is set to the current system date and time, and the other counters are set to zero (`0`).

## Permissions

Requires `VIEW SERVER STATE` permission.

### Permissions for SQL Server 2022 and later

Requires `VIEW SERVER PERFORMANCE STATE` permission on the server.

## Related content

- [System dynamic management views and functions](system-dynamic-management-objects.md)
- [sys.dm_resource_governor_resource_pools (Transact-SQL)](sys-dm-resource-governor-resource-pools-transact-sql.md)
- [sys.resource_governor_workload_groups (Transact-SQL)](../system-catalog-views/sys-resource-governor-workload-groups-transact-sql.md)
- [ALTER RESOURCE GOVERNOR (Transact-SQL)](../../t-sql/statements/alter-resource-governor-transact-sql.md)
- [CREATE WORKLOAD GROUP (Transact-SQL)](../../t-sql/statements/create-workload-group-transact-sql.md)
