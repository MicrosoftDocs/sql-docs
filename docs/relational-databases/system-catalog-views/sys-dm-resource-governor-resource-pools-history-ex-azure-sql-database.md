---
title: "sys.dm_resource_governor_resource_pools_history_ex (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/27/2019"
ms.prod: 
ms.prod_service: sql-database
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.resource_governor"
  - "sys.resource_governor_TSQL"
  - "resource_governor"
  - "resource_governor_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.resource_governor catalog view"
ms.assetid: 
author: joesackmsft
ms.author: josack
manager: craigg
monikerRange: "=azuresqldb-current||=sqlallproducts-allversions"
---
# sys.dm_resource_governor_resource_pools_history_ex (Transact-SQL)

[!INCLUDE[appliesto-xx-asdb-xxxx-xxx-md](../../includes/appliesto-xx-asdb-xxxx-xxx-md.md)]

Returns snapshot at 15 seconds interval for last 30 minutes of resource pools stats for an Azure SQL Database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**pool_id**|int|The ID of the resource pool. Is not nullable.
|**name**|sysname|The name of the resource pool. Is not nullable.|
|**snapshot_time**|datetime2|Datetime of the resource pool stats snapshot taken|
|**duration_ms**|int|Duration between current and previous snapshot|
|**statistics_start_time**|datetime2|The time when statistics was reset for this pool. Is not nullable.|
|**active_session_count**|int|Total active sessions in current snapshot|
|**active_worker_count**|int|Total workers in current snapshot|
|**delta_cpu_usage_ms**|int|CPU usage in milliseconds since last snapshot. Is not nullable.|
|**delta_cpu_usage_preemptive_ms**|int|Preemptive win32 calls not govern by SQL CPU RG, since last snapshot|
|**used_data_space_kb**|bigint|total space used in user databases associated with user pool|
|**allocated_disk_space_kb**|bigint|total data file size of user databases in the associated with user pool|
|**target_memory_kb**|bigint|The target amount of memory, in kilobytes, the resource pool is trying to attain. This is based on the current settings and server state. Is not nullable.|
|**used_memory_kb**|bigint|The amount of memory used, in kilobytes, for the resource pool. Is not nullable.|
|**cache_memory_kb**|bigint|The current total cache memory usage in kilobytes. Is not nullable.|
|**compile_memory_kb**|bigint|The current total stolen memory usage in kilobytes (KB). The majority of this usage would be for compile and optimization, but it can also include other memory users. Is not nullable.|
|**active_memgrant_count**|bigint|The current count of memory grants. Is not nullable.|
|**active_memgrant_kb**|bigint|The sum, in kilobytes (KB), of current memory grants. Is not nullable.|
|**used_memgrant_kb**|bigint|The current total used (stolen) memory from memory grants. Is not nullable.|
|**delta_memgrant_timeout_count**|int|count of memory grant time-outs in this resource pool in this period. Is not nullable.|
|**delta_memgrant_waiter_count**|int|The count of queries currently pending on memory grants. Is not nullable.|
|**delta_out_of_memory_count**|int|The number of failed memory allocations in the pool since last snapshot. Is not nullable.|
|**delta_read_io_queued**|int|The total read IOs enqueued since last snapshot. Is nullable. Null if the resource pool is not governed for IO.|
|**delta_read_io_issued**|int|The total read IOs issued since last snapshot. Is nullable. Null if the resource pool is not governed for IO.|
|**delta_read_io_completed**|int|The total read IOs completed since last snapshot. Is not nullable.|
|**delta_read_io_throttled**|int|The total read IOs throttled since snapshot. Is nullable. Null if the resource pool is not governed for IO.|
|**delta_read_bytes**|bigint|The total number of bytes read since last snapshot. Is not nullable.|
|**delta_read_io_stall_ms**|int|Total time (in milliseconds) between read IO arrival and completion since last snapshot. Is not nullable.|
|**delta_read_io_stall_queued_ms**|int|Total time (in milliseconds) between read IO arrival and issue since last snapshot. Is nullable. Null if the resource pool is not governed for IO. Non-zero  delta_read_io_stall_queued_ms means IO is being affected by RG .|
|**delta_write_io_queued**|int|The total write IOs enqueued since last snapshot. Is nullable. Null if the resource pool is not governed for IO.|
|**delta_write_io_issued**|int|The total write IOs issued since last snapshot. Is nullable. Null if the resource pool is not governed for IO.|
|**delta_write_io_completed**|int|The total write IOs completed since last snapshot. Is not nullable|
|**delta_write_io_throttled**|int|The total write IOs throttled since last snapshot. Is not nullable|
|**delta_write_bytes**|bigint|The total number of bytes written since last snapshot. Is not nullable.|
|**delta_write_io_stall_ms**|int|Total time (in milliseconds) between write IO arrival and completion since last snapshot. Is not nullable.|
|**delta_write_io_stall_queued_ms**|int|Total time (in milliseconds) between write IO arrival and issue since last snapshot. Is nullable. Null if the resource pool is not governed for IO.|
|**delta_io_issue_delay_ms**|int|Total time (in milliseconds) between the scheduled issue and actual issue of IO since last snapshot. Is nullable. Null if the resource pool is not governed for IO.|
|**max_iops_per_volume**|int|The Maximum IO per second (IOPS) per disk volume setting for this Pool. Is nullable. Null if the resource pool is not governed for IO.|
|**max_memory_kb**|bigint|The maximum amount of memory, in kilobytes, that the resource pool can have. This is based on the current settings and server state. Is not nullable.
|**max_log_rate_kb**|bigint|Maximum log rate (kilo-bytes per sec) at resource pool level.|
|**max_data_space_kb**|bigint|Max elastic pool storage limit setting for this elastic pool in kilobytes.|
|**max_session**|int|Session limit for the pool|
|**max_worker**|int|Worker limit for the pool|
|**min_cpu_percent**|int|The current configuration for the guaranteed average CPU bandwidth for all requests in the resource pool when there is CPU contention. Is not nullable.|
|**max_cpu_percent**|int|The current configuration for the maximum average CPU bandwidth allowed for all requests in the resource pool when there is CPU contention. Is not nullable.|
|**cap_cpu_percent**|int|Hard cap on the CPU bandwidth that all requests in the resource pool will receive. Limits the maximum CPU bandwidth level to the specified level. The allowed range for value is from 1 through 100. Is not nullable.|
|**min_vcores**|decimal(5,2)|The current configuration for the guaranteed average CPU bandwidth for all requests in the resource pool when there is CPU contention.  In units of vCores|
|**max_vcores**|decimal(5,2)|The current configuration for the maximum average CPU bandwidth allowed for all requests in the resource pool when there is CPU contention.  In unit of vCores|
|**cap_vcores**|decimal(5,2)|Hard cap on the CPU bandwidth that all requests in the resource pool will receive.  In unit on vCores|
|**instance_cpu_count**|int|Number of CPU configured for the instance|
|**instance_cpu_percent|decimal(5,2)|CPU percent configured for the instance|
|**instance_vcores**|decimal(5,2)|Number of vCores configured for the instance|
|**delta_log_bytes_used**|decimal(5,2)|Total log generation (in bytes) at pool level since last snapshot|
|**avg_login_rate_percent**|decimal(5,2)|Number of Logins since last snapshot, compard against Login Limit|
|**delta_vcores_used**|decimal(5,2)|Compute utilization in count of vCores since last snapshot.|
|**cap_vcores_used_percent**|decimal(5,2)|Average compute utilization in percentage of the limit of the pool.|
|**instance_vcores_used_percent**|decimal(5,2)|Average compute utilization in percentage of the limit of the SQL instance.|
|**avg_data_io_percent**|decimal(5,2)|Average I/O utilization in percentage based on the limit of the pool.|
|**avg_log_write_percent**|decimal(5,2)|Average write resource utilization in percentage of the limit of the pool.|
|**avg_storage_percent**|decimal(5,2)|Average storage utilization in percentage of the storage limit of the pool.|
|**avg_allocated_storage_percent**|decimal(5,2)|The percentage of data space allocated by all databases in the elastic pool. This is the ratio of data space allocated to data max size for the elastic pool. For more information see: File space management in SQL DB|
|**max_worker_percent|decimal(5,2)|Maximum concurrent workers (requests) in percentage based on the limit of the pool.|
|**max_session_percent**|decimal(5,2)|Maximum concurrent sessions in percentage based on the limit of the pool.|

## Permissions

This view requires VIEW DATABASE STATE permission.

## Remarks

Users can access this dynamic management view to monitor near real time resource consumption for user workload pool as well as system internal pools of Azure SQL Database instance.   Most of the data surfaced by this DMV is intended for internal consumption and is subject to change.

## Examples

The following example returns  maximum log rate data and consumption at each snapshot by user pool  

```sql
select snapshot_time, name, max_log_rate_kb, delta_log_bytes_used from sys.dm_resource_governor_resource_pools_history_ex where name like 'UserPool%' order by snapshot_time desc
```

The following example returns similar info  as sys.elastic_pool_resource_stats without having to connect to Logical Master

```sql
select snapshot_time, name, cap_vcores_used_percent,
  avg_data_io_percent,  
  avg_log_write_percent,
  avg_storage_percent,
  avg_allocated_storage_percent,
  max_data_space_kb,
  max_worker_percent,
  max_session_percent
    from sys.dm_resource_governor_resource_pools_history_ex where name like 'UserPool%' order by snapshot_time desc
```

## See Also
