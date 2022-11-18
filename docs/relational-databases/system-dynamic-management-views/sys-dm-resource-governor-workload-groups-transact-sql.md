---
title: "sys.dm_resource_governor_workload_groups (Transact-SQL)"
description: sys.dm_resource_governor_workload_groups (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "05/04/2021"
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
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_resource_governor_workload_groups (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns workload group statistics and the current in-memory configuration of the workload group. This view can be joined with sys.dm_resource_governor_resource_pools to get the resource pool name.  
  
> [!NOTE]  
>  To call this from [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the name **sys.dm_pdw_nodes_resource_governor_workload_groups**. [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)] 
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|group_id|**int**|ID of the workload group. Is not nullable.|  
|name|**sysname**|Name of the workload group. Is not nullable.|  
|pool_id|**int**|ID of the resource pool. Is not nullable.|  
|external_pool_id|**int**|**Applies to**: Starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)].<br /><br /> ID of the external resource pool. Is not nullable.|  
|statistics_start_time|**datetime**|Time that statistics collection was reset for the workload group. Is not nullable.|  
|total_request_count|**bigint**|Cumulative count of completed requests in the workload group. Is not nullable.|  
|total_queued_request_count|**bigint**|Cumulative count of requests queued after the GROUP_MAX_REQUESTS limit was reached. Is not nullable.|  
|active_request_count|**int**|Current request count. Is not nullable.|  
|queued_request_count|**int**|Current queued request count. Is not nullable.|  
|total_cpu_limit_violation_count|**bigint**|Cumulative count of requests exceeding the CPU limit. Is not nullable.|  
|total_cpu_usage_ms|**bigint**|Cumulative CPU usage, in milliseconds, by this workload group. Is not nullable.|  
|max_request_cpu_time_ms|**bigint**|Maximum CPU usage, in milliseconds, for a single request. Is not nullable.<br /><br /> **Note:** This is a measured value, unlike request_max_cpu_time_sec, which is a configurable setting. For more information, see [CPU Threshold Exceeded Event Class](../../relational-databases/event-classes/cpu-threshold-exceeded-event-class.md).|  
|blocked_task_count|**int**|Current count of blocked tasks. Is not nullable.|  
|total_lock_wait_count|**bigint**|Cumulative count of lock waits that occurred. Is not nullable.|  
|total_lock_wait_time_ms|**bigint**|Cumulative sum of elapsed time, in milliseconds, a lock is held. Is not nullable.|  
|total_query_optimization_count|**bigint**|Cumulative count of query optimizations in this workload group. Is not nullable.|  
|total_suboptimal_plan_generation_count|**bigint**|Cumulative count of suboptimal plan generations that occurred in this workload group due to memory pressure. Is not nullable.|  
|total_reduced_memgrant_count|**bigint**|Cumulative count of memory grants that reached the maximum query size limit. Is not nullable.|  
|max_request_grant_memory_kb|**bigint**|Maximum memory grant size, in kilobytes, of a single request since the statistics were reset. Is not nullable.|  
|active_parallel_thread_count|**bigint**|Current count of parallel thread usage. Is not nullable.|  
|importance|**sysname**|Current configuration value for the relative importance of a request in this workload group. Importance is one of the following, with Medium being the default: Low, Medium, or High.<br /><br /> Is not nullable.|  
|request_max_memory_grant_percent|**int**|Current setting for the maximum memory grant, as a percentage, for a single request. Is not nullable.|  
|request_max_cpu_time_sec|**int**|Current setting for maximum CPU use limit, in seconds, for a single request. Is not nullable.|  
|request_memory_grant_timeout_sec|**int**|Current setting for memory grant time-out, in seconds, for a single request. Is not nullable.|  
|group_max_requests|**int**|Current setting for the maximum number of concurrent requests. Is not nullable.|  
|max_dop|**int**|Configured maximum degree of parallelism for the workload group. The default value, 0, uses global settings. Is not nullable.| 
|effective_max_dop|**int**|**Applies to**: Starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)].<br /><br />Effective maximum degree of parallelism for the workload group. Is not nullable.| 
|total_cpu_usage_preemptive_ms|**bigint**|**Applies to**: Starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)].<br /><br />Total CPU time used while in preemptive mode scheduling for the workload group, measured in ms. Is not nullable.<br /><br />To execute code that is outside [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (for example, extended stored procedures and distributed queries), a thread has to execute outside the control of the non-preemptive scheduler. To do this, a worker switches to preemptive mode.| 
|request_max_memory_grant_percent_numeric|**float**|**Applies to**: [!INCLUDE[ssazuremi_md](../../includes/ssazuremi_md.md)] and starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)].<br /><br />Current setting for the maximum memory grant, as a percentage, for a single request. Similar to **request_max_memory_grant_percent**, which returns an `integer`, **request_max_memory_grant_percent_numeric** returns a `float`. Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)], the parameter **REQUEST_MAX_MEMORY_GRANT_PERCENT** accepts values with a possible range of 0-100 and stores them as the `float` data type. Prior to [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)], **REQUEST_MAX_MEMORY_GRANT_PERCENT** is an `integer` with possible range of 1-100. For more information, see [CREATE WORKLOAD GROUP](../../t-sql/statements/create-workload-group-transact-sql.md).<BR><BR>Is not nullable.| 
|pdw_node_id|**int**|**Applies to**: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]<br /><br /> The identifier for the node that this distribution is on.|  
  
## Remarks  
 This dynamic management view shows the in-memory configuration. To see the stored configuration metadata, use the [sys.resource_governor_workload_groups &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-resource-governor-workload-groups-transact-sql.md) catalog view.  
  
 When `ALTER RESOURCE GOVERNOR RESET STATISTICS` is successfully executed, the following counters are reset: `statistics_start_time`, `total_request_count`, `total_queued_request_count`, `total_cpu_limit_violation_count`, `total_cpu_usage_ms`, `max_request_cpu_time_ms`, `total_lock_wait_count`, `total_lock_wait_time_ms`, `total_query_optimization_count`, `total_suboptimal_plan_generation_count`, `total_reduced_memgrant_count`, and `max_request_grant_memory_kb`. The counter `statistics_start_time` is set to the current system date and time, and the other counters are set to zero (0).  
  
## Permissions  
 Requires `VIEW SERVER STATE` permission.  
  
## See also  
 - [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 - [sys.dm_resource_governor_resource_pools &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-resource-governor-resource-pools-transact-sql.md)   
 - [sys.resource_governor_workload_groups &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-resource-governor-workload-groups-transact-sql.md)   
 - [ALTER RESOURCE GOVERNOR &#40;Transact-SQL&#41;](../../t-sql/statements/alter-resource-governor-transact-sql.md)  
 - [CREATE WORKLOAD GROUP](../../t-sql/statements/create-workload-group-transact-sql.md)
  
