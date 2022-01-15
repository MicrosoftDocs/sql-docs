---
description: "sys.dm_os_out_of_memory_events (Azure SQL Database)"
title: "sys.dm_os_out_of_memory_events (Azure SQL Database)"
ms.custom: ""
ms.date: "01/14/2022"
ms.service: sql-database
ms.topic: "reference"
f1_keywords: 
  - "sys.dm_os_out_of_memory_events"
  - "sys.dm_os_out_of_memory_events_TSQL"
  - "dm_os_out_of_memory_events"
  - "dm_os_out_of_memory_events_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_os_out_of_memory_events"
  - "dm_os_out_of_memory_events"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: 
monikerRange: "=azuresqldb-current"
---
# sys.dm_os_out_of_memory_events (Azure SQL Database and Azure SQL Managed Instance)
[!INCLUDE[Azure SQL Database Azure](../../includes/applies-to-version/asdb.md)]

  Returns a log of out of memory (OOM) events for an Azure SQL database.

  For more information on out of memory conditions, see [Troubleshoot out of memory errors with Azure SQL Database](/azure/azure-sql/database/troubleshoot-memory-errors-issues).
  
|Columns|Data Type|Description|  
|-------------|---------------|-----------------|  
|event_time | datetime2, not null  | OOM event time |
|oom_cause  | tinyint, not null | A value indicating OOM root cause |
|oom_cause_desc | nvarchar(60), not null | Description of `oom_cause`, one of:<BR>0. UNKNOWN -  OOM not known<BR>1. HEKATON_POOL_MEMORY_LOW -  OOM due to hekaton pool memory resource low<BR>2. MEMORY_LOW -  OOM due to memory resource low <BR>3. OS_MEMORY_PRESSURE -  OOM due to either machine memory pressure<BR>4. OS_MEMORY_PRESSURE_SQL -  OOM due to overbooking memory pressure <BR>5. NON_SOS_MEMORY_LEAK -  OOM due to leaks in non-SOS usages<BR>6. SERVERLESS_MEMORY_RECLAMATION) -  OOM due to serverless memory reclamation<BR>7. MEMORY_LEAK -  OOM due to SOS memory leak <BR>8. SLOW_BUFFER_POOL_SHRINK -  OOM due to slow buffer pool shrink<BR>9. INTERNAL_POOL -  OOM due to internal pool when OOM factor is AllocationPotential<BR>10. SYSTEM_POOL -  OOM due to our system pool when OOM factor is MemoryPool<BR>11. QUERY_MEMORY_GRANTS -  OOM due to large memory grants <BR>12. REPLICAS_AND_AVAILABILITY -  OOM due to workloads in SloSecSharedPool resource pool |
|available_physical_memory_mb|int, not null|Available physical memory, in megabytes|
|initial_job_object_memory_limit_mb |int, null |Job object memory limit on database engine startup, in megabytes |
|current_job_object_memory_limit_mb |int, null |Job object current memory limit, in megabytes |
|process_memory_usage_mb |int, not null |Total process memory usage in megabytes on the sql instance |
|non_sos_memory_usage_mb |int, not null | Non-SOS usage including SOS created threads, threads created by non-SOS components, loaded DLLs, etc.|
|committed_memory_target_mb |int, not null |SOS target memory in megabytes |
|committed_memory_mb |int, not null |SOS committed memory in megabytes |
|allocation_potential_memory_mb |int, not null |Memory available to the database engine instance for new allocations, in megabytes |
|oom_factor |tinyint, not null |A value indicating OOM cause, possible values are 0-12 |
|oom_factor_desc |nvarchar(60), not null |Description of `oom_factor`, one of:<BR>0 - UNDEFINED<BR>1 - ALLOCATION_POTENTIAL<BR>2 - BLOCK_ALLOCATOR<BR>3 - ESCAPE_TIMEOUT<BR>4 - FAIL_FAST<BR>5 - MEMORY_POOL<BR>6 - EMERGENCY_ALLOCATOR<BR>7 - VIRTUAL_ALLOC<BR>8 - SIMULATED<BR>9 - BUF_ALLOCATOR<BR>10 - QUERY_MEM_QUEUE<BR>11 - FRAGMENT<BR>12 - INIT_DESCRIPTOR<BR>13 - MEMORY_POOL_PRESSURE<BR>14 - DESCRIPTOR_ALLOCATOR<BR>15 - DESCRIPTOR_ALLOCATOR_ESCAPE |
|oom_resource_pools |nvarchar(max), null |OOM resource pools if any as a JSON string, including memory usage statistics for each pool |
|top_memory_clerks |nvarchar(max), not null |Top 5 (configurable) memory clerks as a JSON string, including memory usage statistics for each clerk |
|top_resource_pools |nvarchar(max), not null |Top 3 (configurable) resource pools as a JSON string, including memory usage statistics for each pool |
|possible_leaked_memory_clerks |nvarchar(max), null |Leaked memory clerks if any as a JSON string. Based on heuristics and provided with a finite degree of confidence. |
|possible_non_sos_leaked_memory_mb |int, null |Leaked non-SOS memory in megabytes, if any. Based on heuristics and provided with a finite degree of confidence. |
|||

## Permissions
 For Azure SQL Database, this view can only be accessed by the server administrator or Azure Active Directory admin.

## Remarks

Returns a row when an out of memory (OOM) event is detected, checking every 10 minutes. This DMV aligns to activity recorded in the `summarized_oom_snapshot` extended event, both introduced to Azure SQL Database in January 2022.

## Example  
  
The following example returns event data ordered by the most recent time for the currently connected database.
  
```sql  
SELECT * FROM sys.dm_os_out_of_memory_events ORDER BY event_time DESC;  
```  

## See also
 
 - [sys.resource_stats](../../relational-databases/system-catalog-views/sys-resource-stats-azure-sql-database.md)
 - [sys.server_resource_stats](../../relational-databases/system-catalog-views/sys-server-resource-stats-azure-sql-database.md)

## Next steps

 - [Troubleshoot out of memory errors with Azure SQL Database](/azure/azure-sql/database/troubleshoot-memory-errors-issues)