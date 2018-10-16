---
title: "sys.dm_resource_governor_resource_pools (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "04/24/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_resource_governor_resource_pools_TSQL"
  - "dm_resource_governor_resource_pools_TSQL"
  - "sys.dm_resource_governor_resource_pools"
  - "dm_resource_governor_resource_pools"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_resource_governor_resource_pools dynamic management view"
ms.assetid: 9bfc926e-d8bc-40f8-9229-ab1f8a1e69c5
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_resource_governor_resource_pools (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-asdw-pdw-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns information about the current resource pool state, the current configuration of resource pools, and resource pool statistics.  
  
> [!NOTE]  
>  To call this from [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the name **sys.dm_pdw_nodes_resource_governor_resource_pools**.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|pool_id|**int**|The ID of the resource pool. Is not nullable.|  
|name|**sysname**|The name of the resource pool. Is not nullable.|  
|statistics_start_time|**datetime**|The time when statistics was reset for this pool. Is not nullable.|  
|total_cpu_usage_ms|**bigint**|The cumulative CPU usage in milliseconds since the Resource Govenor statistics were reset. Is not nullable.|  
|cache_memory_kb|**bigint**|The current total cache memory usage in kilobytes. Is not nullable.|  
|compile_memory_kb|**bigint**|The current total stolen memory usage in kilobytes (KB). The majority of this usage would be for compile and optimization, but it can also include other memory users. Is not nullable.|  
|used_memgrant_kb|**bigint**|The current total used (stolen) memory from memory grants. Is not nullable.|  
|total_memgrant_count|**bigint**|The cumulative count of memory grants in this resource pool. Is not nullable.|  
|total_memgrant_timeout_count|**bigint**|The cumulative count of memory grant time-outs in this resource pool. Is not nullable.|  
|active_memgrant_count|**int**|The current count of memory grants. Is not nullable.|  
|active_memgrant_kb|**bigint**|The sum, in kilobytes (KB), of current memory grants. Is not nullable.|  
|memgrant_waiter_count|**int**|The count of queries currently pending on memory grants. Is not nullable.|  
|max_memory_kb|**bigint**|The maximum amount of memory, in kilobytes, that the resource pool can have. This is based on the current settings and server state. Is not nullable.|  
|used_memory_kb|**bigint**|The amount of memory used, in kilobytes, for the resource pool. Is not nullable.|  
|target_memory_kb|**bigint**|The target amount of memory, in kilobytes, the resource pool is trying to attain. This is based on the current settings and server state. Is not nullable.|  
|out_of_memory_count|**bigint**|The number of failed memory allocations in the pool since the Resource Govenor statistics were reset. Is not nullable.|  
|min_cpu_percent|**int**|The current configuration for the guaranteed average CPU bandwidth for all requests in the resource pool when there is CPU contention. Is not nullable.|  
|max_cpu_percent|**int**|The current configuration for the maximum average CPU bandwidth allowed for all requests in the resource pool when there is CPU contention. Is not nullable.|  
|min_memory_percent|**int**|The current configuration for the guaranteed amount of memory for all requests in the resource pool when there is memory contention. This is not shared with other resource pools. Is not nullable.|  
|max_memory_percent|**int**|The current configuration for the percentage of total server memory that can be used by requests in this resource pool. Is not nullable.|  
|cap_cpu_percent|**int**|**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> Hard cap on the CPU bandwidth that all requests in the resource pool will receive. Limits the maximum CPU bandwidth level to the specified level. The allowed range for value is from 1 through 100. Is not nullable.|  
|min_iops_per_volume|**int**|**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> The minimum IO per second (IOPS) per disk volume setting for this Pool. Is nullable. Null if the resource pool is not governed for IO. That is, the Resource Pool MIN_IOPS_PER_VOLUME and MAX_IOPS_PER_VOLUME settings are 0.|  
|max_iops_per_volume|**int**|**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> The maximum IO per second (IOPS) per disk volume setting for this Pool. Is nullable. Null if the resource pool is not governed for IO. That is, the Resource Pool MIN_IOPS_PER_VOLUME and MAX_IOPS_PER_VOLUME settings are 0..|  
|read_io_queued_total|**int**|**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> The total read IOs enqueued since the Resource Govenor was reset. Is nullable. Null if the resource pool is not governed for IO. That is, the Resource Pool MIN_IOPS_PER_VOLUME and MAX_IOPS_PER_VOLUME settings are 0.|  
|read_io_issued_total|**int**|**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> The total read IOs issued since the Resource Govenor statistics were reset. Is nullable. Null if the resource pool is not governed for IO. That is, the Resource Pool MIN_IOPS_PER_VOLUME and MAX_IOPS_PER_VOLUME settings are 0.|  
|read_io_completed_total|**int**|**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> The total read IOs completed since the Resource Govenor statistics were reset. Is not nullable.|  
|read_io_throttled_total|**int**|The total read IOs throttled since the Resource Govenor statistics were reset. Is nullable. Null if the resource pool is not governed for IO. That is, the Resource Pool MIN_IOPS_PER_VOLUME and MAX_IOPS_PER_VOLUME settings are 0.|  
|read_bytes_total|**bigint**|**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> The total number of bytes read since the Resource Govenor statistics were reset. Is not nullable.|  
|read_io_stall_total_ms|**bigint**|**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> Total time (in milliseconds) between read IO arrival and completion. Is not nullable. |  
|read_io_stall_queued_ms|**bigint**|**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> Total time (in milliseconds) between read IO arrival and issue. Is nullable. Null if the resource pool is not governed for IO. That is, the Resource Pool MIN_IOPS_PER_VOLUME and MAX_IOPS_PER_VOLUME settings are 0.<br /><br /> To determine if the IO setting for the pool is causing latency, subtract **read_io_stall_queued_ms** from **read_io_stall_total_ms**.|  
|write_io_queued_total|**int**|**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> The total write IOs enqueued since the Resource Govenor statistics were reset. Is nullable. Null if the resource pool is not governed for IO. That is, the Resource Pool MIN_IOPS_PER_VOLUME and MAX_IOPS_PER_VOLUME settings are 0.|  
|write_io_issued_total|**int**|**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> The total write IOs issued since the Resource Govenor statistics were reset. Is nullable. Null if the resource pool is not governed for IO. That is, the Resource Pool MIN_IOPS_PER_VOLUME and MAX_IOPS_PER_VOLUME settings are 0.|  
|write_io_completed_total|**int**|**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> The total write IOs completed since the Resource Govenor statistics were reset. Is not nullable|  
|write_io_throttled_total|**int**|**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> The total write IOs throttled since the Resource Govenor statistics were reset. Is not nullable|  
|write_bytes_total|**bigint**|**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> The total number of bytes written since the Resource Govenor statistics were reset. Is not nullable.|  
|write_io_stall_total_ms|**bigint**|**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> Total time (in milliseconds) between write IO arrival and completion. Is not nullable. |  
|write_io_stall_queued_ms|**bigint**|**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> Total time (in milliseconds) between write IO arrival and issue. Is nullable. Null if the resource pool is not governed for IO. That is, the Resource Pool MIN_IOPS_PER_VOLUME and MAX_IOPS_PER_VOLUME settings are 0.<br /><br /> This is the delay introduced by IO Resource Governance.|  
|io_issue_violations_total|**int**|**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> Total IO issue violations. That is, the number of times when the rate of IO issue was lower than the reserved rate. Is nullable. Null if the resource pool is not governed for IO. That is, the Resource Pool MIN_IOPS_PER_VOLUME and MAX_IOPS_PER_VOLUME settings are 0.|  
|io_issue_delay_total_ms|**bigint**|**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> Total time (in milliseconds) between the scheduled issue and actual issue of IO. Is nullable. Null if the resource pool is not governed for IO. That is, the Resource Pool MIN_IOPS_PER_VOLUME and MAX_IOPS_PER_VOLUME settings are 0.|  
|pdw_node_id|**int**|**Applies to**: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]<br /><br /> The identifier for the node that this distribution is on.|  
  
## Remarks  
 Resource Governor workload groups and Resource Governor resource pools have a many-to-one mapping. As a result, many of the resource pool statistics are derived from the workload group statistics.  
  
 This dynamic management view shows the in-memory configuration. To see the stored configuration metadata, use the sys.resource_governor_resource_pools catalog view.  
  
## Permissions  
 Requires VIEW SERVER STATE permission.  
  
## See Also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [sys.dm_resource_governor_workload_groups &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-resource-governor-workload-groups-transact-sql.md)   
 [sys.resource_governor_resource_pools &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-resource-governor-resource-pools-transact-sql.md)   
 [ALTER RESOURCE GOVERNOR &#40;Transact-SQL&#41;](../../t-sql/statements/alter-resource-governor-transact-sql.md)  
  
  



