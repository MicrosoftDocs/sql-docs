---
title: "sys.dm_resource_governor_workload_groups_history_ex (Azure SQL Database) | Microsoft Docs"
ms.custom: ""
ms.date: "05/22/2019"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_resource_governor_workload_groups_history_ex_TSQL"
  - "sys.dm_resource_governor_workload_groups_history_ex"
  - "dm_resource_governor_workload_groups_history_ex"
  - "dm_resource_governor_workload_groups_history_ex_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "Availability Groups [SQL Server], monitoring"
  - "sys.dm_resource_governor_workload_groups_history_ex dynamic management view"
author: stevestein
ms.author: sstein
manager: craigg
---
# sys.dm_resource_governor_workload_groups_history_ex (Transact-SQL)
[!INCLUDE[tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md.md)]

Returns snapshot at 15 seconds interval for last 30 minutes of resource pools stats for an Azure SQL Database.
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**pool_id**| int |ID of the resource pool. Is not nullable.|
|**group_id**| int |ID of the workload group. Is not nullable.|
|**name**| sysname |Name of the workload group. Is not nullable.|
|**snapshot_time**| datetime2 |Datetime of the resource group stats snapshot taken.|
|**duration_ms**| int |Duration between current and previous snapshot.|
|**active_worker_count**| int |Total workers in current snapshot.|
|**active_request_count**| int |Current request count. Is not nullable.|
|**active_session_count**| int |Total active sessions in current snapshot.|
|**total_request_count**| int |Cumulative count of completed requests in the workload group. Is not nullable.|
|**delta_request_count**| int |Count of completed requests in the workload group since last snapshot. Is not nullable.|
|**total_cpu_usage_ms**| int |Cumulative CPU usage, in milliseconds, by this workload group. Is not nullable.|
|**delta_cpu_usage_ms**| int |CPU usage in milliseconds since last snapshot. Is not nullable.|
|**delta_cpu_active_ms**| int |Will remove from sys.dm_resource_governor_workload_groups_history_ex|
|**delta_cpu_delayed_ms**| int |Will remove from sys.dm_resource_governor_workload_groups_history_ex|
|**delta_cpu_usage_preemptive_ms**| int |Preemptive win32 calls not govern by SQL CPU RG, since last snapshot.|
|**delta_reads_reduced_memgrant_count**| int |The count of memory grants that reached the maximum query size limit since last snapshot. Is not nullable.|
|**reads_throttled**| int |Total number of reads throttled.|
|**delta_reads_queued**| int |The total read IOs enqueued since last snapshot. Is nullable. Null if the resource group is not governed for IO.|
|**delta_reads_issued**| int |The total read IOs issued since last snapshot. Is nullable. Null if the resource group is not governed for IO.|
|**delta_reads_completed**| int |The total read IOs completed since last snapshot. Is not nullable.|
|**delta_read_bytes**| bigint |The total number of bytes read since last snapshot. Is not nullable.|
|**delta_read_stall_ms**| int |Total time (in milliseconds) between read IO arrival and completion since last snapshot. Is not nullable.|
|**delta_read_stall_queued_ms**| int |Total time (in milliseconds) between read IO arrival and issue since last snapshot. Is nullable. Null if the resource group is not governed for IO. Non-zero  delta_read_stall_queued_ms means IO is being affected by RG .|
|**delta_writes_queued**| int |The total write IOs enqueued since last snapshot. Is nullable. Null if the resource group is not governed for IO.|
|**delta_writes_issued**| int |The total write IOs issued since last snapshot. Is nullable. Null if the resource group is not governed for IO.|
|**delta_writes_completed**| int |The total write IOs completed since last snapshot. Is not nullable.|
|**delta_writes_bytes**| int |The total number of bytes written since last snapshot. Is not nullable.|
|**delta_write_stall_ms**| int |Total time (in milliseconds) between write IO arrival and completion since last snapshot. Is not nullable.|
|**delta_background_writes**| int |The total writes performed by background tasks since last snapshot.|
|**delta_background_write_bytes**| int |The total write size performed by background tasks since last snapshot, in bytes.|
|**delta_io_active_time_ms**| int |Will remove from sys.dm_resource_governor_workload_groups_history_ex|
|**delta_log_bytes_used**| int |Log used since last snapshot in bytes.|
|**delta_log_commits_tracked**| int |Will remove from sys.dm_resource_governor_workload_groups_history_ex|
|**delta_log_operations_tracked**| int |Will remove from sys.dm_resource_governor_workload_groups_history_ex|
|**delta_log_time_waits_for_allotment**| int |Will remove from sys.dm_resource_governor_workload_groups_history_ex|
|**delta_log_time_waits_for_allotment_ms**| int |Will remove from sys.dm_resource_governor_workload_groups_history_ex|
|**delta_log_temp_db_bytes_used**| int |Tempdb log used since last snapshot in bytes.|
|**delta_query_optimizations**| int |The count of query optimizations in this workload group since last snapshot. Is not nullable.|
|**delta_suboptimal_plan_generations**| int |The count of suboptimal plan generations that occurred in this workload group due to memory pressure since last snapshot. Is not nullable.
|**max_memory_grant_kb**| bigint |Maximum memory grant for the group in KB.|
|**max_request_cpu_msec**| int |Maximum CPU usage, in milliseconds, for a single request. Is not nullable.|
|**max_concurrent_request**| int |Current setting for the maximum number of concurrent requests. Is not nullable.|
|**max_io**| int |Maximum IO limit for the group.|
|**max_global_io**| int |pResourceGroupData->SetMaxGlobalIo((UINT32)(pResourceGroupData->GetMaxGlobalIo() * ((double)pMdGroupData->bMaxIopsPercent / 100.0))); Max global io is to a degree artifact of sawa v1 and box io rg v1.<br>Identified for informational purposes only. Not supported. Future compatibility is not guaranteed.
|**max_queued_io**| int |pResourceGroupData->SetMaxGlobalIo((UINT32)(pResourceGroupData->GetMaxGlobalIo() * ((double)pMdGroupData->bMaxIopsPercent / 100.0))); Identified for informational purposes only. Not supported. Future compatibility is not guaranteed.|
|**max_log_rate_kb**| int |Maximum log rate (kilo-bytes per sec) at resource group level.|
|**max_session**| int |Session limit for the group.|
|**max_worker**| int |Worker limit for the group.|
|||

## Permissions

This view requires VIEW DATABASE STATE permission.

## Remarks

Users can access this dynamic management view to monitor near real time resource consumption for user workload pool as well as system internal pools of Azure SQL Database instance.

> [!IMPORTANT]
> Most of the data surfaced by this DMV is intended for internal consumption and is subject to change.

## Examples

The following example returns maximum log rate data and consumption at each snapshot by user pool:

```sql
select snapshot_time, name, max_log_rate_kb, delta_log_bytes_used from sys.dm_resource_governor_workload_groups_history_ex where name like 'UserPool%' order by snapshot_time desc
```

The following example returns similar info as sys.elastic_pool_resource_stats without having to connect to Logical Master

```sql
select snapshot_time, name, cap_vcores_used_percent,
  avg_data_io_percent,  
  avg_log_write_percent,
  avg_storage_percent,
  avg_allocated_storage_percent,
  max_data_space_kb,
  max_worker_percent,
  max_session_percent
    from sys.dm_resource_governor_workload_groups_history_ex where name like 'UserPool%' order by snapshot_time desc
```

## See Also

- [Translation log rate governance](https://docs.microsoft.com/azure/sql-database/sql-database-resource-limits-database-server#transaction-log-rate-governance)
- [Elastic pool DTU resource limits](https://docs.microsoft.com/azure/sql-database/sql-database-dtu-resource-limits-elastic-pools)
- [Elastic pool vCore resource limits](https://docs.microsoft.com/azure/sql-database/sql-database-vcore-resource-limits-elastic-pools)