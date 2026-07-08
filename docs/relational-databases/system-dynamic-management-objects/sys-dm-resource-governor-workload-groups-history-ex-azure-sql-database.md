---
title: "sys.dm_resource_governor_workload_groups_history_ex"
description: Each row in sys.dm_resource_governor_workload_groups_history_ex represents a periodic snapshot of workload group statistics.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: dfurman
ms.date: 02/11/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_resource_governor_workload_groups_history_ex_TSQL"
  - "sys.dm_resource_governor_workload_groups_history_ex"
  - "dm_resource_governor_workload_groups_history_ex"
  - "dm_resource_governor_workload_groups_history_ex_TSQL"
helpviewer_keywords:
  - "Availability Groups [SQL Server], monitoring"
  - "sys.dm_resource_governor_workload_groups_history_ex dynamic management view"
dev_langs:
  - "TSQL"
---

# sys.dm_resource_governor_workload_groups_history_ex

[!INCLUDE[Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/asdb-asdbmi.md)]

Each row represents a periodic snapshot of workload group statistics in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)]. A snapshot is taken when the database engine starts, and every few seconds thereafter. The interval between the current and the previous snapshot may vary, and is provided in the `duration_ms` column. The latest available snapshots are returned, up to 128 snapshots for each workload group.

| Column name | Data type | Description |
|:--|:-|:--|
| `pool_id` | **int** | ID of the resource pool. Not nullable. |
| `group_id` | **int** | ID of the workload group. Not nullable. |
| `name` | **nvarchar(256)** | Name of the workload group. Not nullable. |
| `snapshot_time` | **datetime** | The time when the workload group statistics snapshot is taken. |
| `duration_ms` | **int** | Duration between the current and the previous snapshot. |
| `active_worker_count` | **int** | Total workers in current snapshot. |
| `active_request_count` | **int** | Current request count. Not nullable. |
| `active_session_count` | **int** | Total active sessions in current snapshot. |
| `total_request_count` | **bigint** | Cumulative count of completed requests in the workload group. Not nullable. |
| `delta_request_count` | **int** | Count of completed requests in the workload group since last snapshot. Not nullable. |
| `total_cpu_usage_ms` | **bigint** | Cumulative CPU usage, in milliseconds, by this workload group. Not nullable. |
| `delta_cpu_usage_ms` | **int** | CPU usage in milliseconds since last snapshot. Not nullable. |
| `delta_cpu_usage_preemptive_ms` | **int** | Preemptive win32 calls not governed by the SQL CPU resource governance, since last snapshot. |
| `delta_reads_reduced_memgrant_count` | **int** | The count of memory grants that reached the maximum query size limit since last snapshot. Not nullable. |
| `reads_throttled` | **int** | Total number of read IOs throttled. |
| `delta_reads_queued` | **int** | The total read IOs enqueued since last snapshot. Is nullable. Null if the workload group is not governed for IO. |
| `delta_reads_issued` | **int** | The total read IOs issued since last snapshot. Is nullable. Null if the workload group is not governed for IO. |
| `delta_reads_completed` | **int** | The total read IOs completed since last snapshot. Not nullable. |
| `delta_read_bytes` | **bigint** | The total number of bytes read since last snapshot. Not nullable. |
| `delta_read_stall_ms` | **int** | Total time (in milliseconds) between read IO arrival and completion since last snapshot. Not nullable. |
| `delta_read_stall_queued_ms` | **int** | Total time (in milliseconds) between read IO arrival and issue since last snapshot. Is nullable. Null if the workload group is not governed for IO. Non-zero  delta_read_stall_queued_ms means IOs are being delayed by resource governance. |
| `delta_writes_queued` | **int** | The total write IOs enqueued since last snapshot. Is nullable. Null if the workload group is not governed for IO. |
| `delta_writes_issued` | **int** | The total write IOs issued since last snapshot. Is nullable. Null if the workload group is not governed for IO. |
| `delta_writes_completed` | **int** | The total write IOs completed since last snapshot. Not nullable. |
| `delta_writes_bytes` | **bigint** | The total number of bytes written since last snapshot. Not nullable. |
| `delta_write_stall_ms` | **int** | Total time (in milliseconds) between write IO arrival and completion since last snapshot. Not nullable. |
| `delta_background_writes` | **int** | The total writes performed by background tasks since last snapshot. |
| `delta_background_write_bytes` | **bigint** | The total write size performed by background tasks since last snapshot, in bytes. |
| `delta_log_bytes_used` | **bigint** | Transaction log space used since last snapshot in bytes. |
| `delta_log_temp_db_bytes_used` | **bigint** | Tempdb transaction log space used since last snapshot in bytes. |
| `delta_query_optimizations` | **bigint** | The count of query optimizations in this workload group since last snapshot. Not nullable. |
| `delta_suboptimal_plan_generations` | **bigint** | The count of suboptimal plan generations that occurred in this workload group due to memory pressure since last snapshot. Not nullable. |
| `max_memory_grant_kb` | **bigint** | Maximum size of a memory grant for a request executing in the group in kilobytes. |
| `max_request_cpu_msec` | **bigint** | Maximum CPU usage, in milliseconds, for a single request. Not nullable. |
| `max_concurrent_request` | **int** | Current setting for the maximum number of concurrent requests. Not nullable. |
| `max_io` | **int** | Maximum IO limit for the group. |
| `max_global_io` | **int** | Identified for informational purposes only. Not supported. Future compatibility is not guaranteed. |
| `max_queued_io` | **int** | Identified for informational purposes only. Not supported. Future compatibility is not guaranteed. |
| `max_log_rate_kb` | **bigint** | Maximum log rate in kilobytes per second for the workload group. |
| `max_session` | **int** | Session limit for the workload group. |
| `max_worker` | **int** | Worker limit for the workload group. |
| `active_outbound_connection_worker_count` | **int** | Total outbound connection workers in current snapshot. |
| `max_outbound_connection_worker` | **int** | Outbound connection worker limit for the group. |
| `max_outbound_connection_worker_percent` | **decimal(5,2)** | Maximum concurrent outbound connection workers (requests) in percentage based on the limit of the group. |

## Permissions

Requires the `VIEW SERVER PERFORMANCE STATE` permission.

## Remarks

Users can access this dynamic management view to monitor near real time resource consumption for user workload group as well as system internal workload groups in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)].

> [!IMPORTANT]
> Most of the data in this DMV is intended for internal consumption and is subject to change.

## Examples

The following example the returns maximum log rate data and consumption at each snapshot by the database, or by all databases in an elastic pool.

```sql
SELECT snapshot_time,
       name,
       max_log_rate_kb,
       delta_log_bytes_used
FROM sys.dm_resource_governor_workload_groups_history_ex
WHERE name LIKE 'UserPrimaryGroup.DBId%'
ORDER BY snapshot_time DESC;
```

## Related content

- [sys.dm_resource_governor_resource_pools_history_ex](sys-dm-resource-governor-resource-pools-history-ex-azure-sql-database.md)
- [Translation log rate governance](/azure/sql-database/sql-database-resource-limits-database-server#transaction-log-rate-governance)
- [Elastic pool DTU resource limits](/azure/sql-database/sql-database-dtu-resource-limits-elastic-pools)
- [Elastic pool vCore resource limits](/azure/sql-database/sql-database-vcore-resource-limits-elastic-pools)
