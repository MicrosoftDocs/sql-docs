---
title: "sys.dm_os_out_of_memory_events"
description: sys.dm_os_out_of_memory_events returns a log of out of memory (OOM) events, including a predicted out of memory cause.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "03/10/2022"
ms.service: sql-database
ms.topic: "reference"
f1_keywords:
  - "sys.dm_os_out_of_memory_events"
  - "sys.dm_os_out_of_memory_events_TSQL"
  - "dm_os_out_of_memory_events"
  - "dm_os_out_of_memory_events_TSQL"
helpviewer_keywords:
  - "sys.dm_os_out_of_memory_events"
  - "dm_os_out_of_memory_events"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||=azuresqldb-mi-current"
---
# sys.dm_os_out_of_memory_events

[!INCLUDE[Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/asdb-asdbmi.md)]

  Returns a log of out of memory (OOM) events.

  For more information on out of memory conditions in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], see [Troubleshoot out of memory errors in Azure SQL Database](/azure/azure-sql/database/troubleshoot-memory-errors-issues).
  
|Column Name|Data Type|Description|  
|-------------|---------------|-----------------|  
|event_time | datetime2, not null  | OOM event time |
|oom_cause  | tinyint, not null | A numeric value indicating OOM root cause. OOM cause is determined by a heuristic algorithm and is provided with a finite degree of confidence. |
|oom_cause_desc | nvarchar(60), not null | Description of `oom_cause`, one of:<BR>0. UNKNOWN -  OOM cause could not be determined<BR>1. HEKATON_POOL_MEMORY_LOW - Insufficient memory in the resource pool used for In-Memory OLTP. For more information, see [Monitor In-Memory OLTP](/azure/azure-sql/in-memory-oltp-monitor-space).<BR>2. MEMORY_LOW - Insufficient memory available to the database engine process<BR>3. OS_MEMORY_PRESSURE - OOM due to external memory pressure from the operating system<BR>4. OS_MEMORY_PRESSURE_SQL - OOM due to external memory pressure from other database engine instance(s)<BR>5. NON_SOS_MEMORY_LEAK - OOM due to a leak in non-SOS memory, for example, loaded modules<BR>6. SERVERLESS_MEMORY_RECLAMATION - OOM related to memory reclamation in a serverless database<BR>7. MEMORY_LEAK - OOM due to a leak in SOS memory<BR>8. SLOW_BUFFER_POOL_SHRINK - OOM due to the buffer pool not releasing memory fast enough under memory pressure<BR>9. INTERNAL_POOL - Insufficient memory in the internal resource pool<BR>10. SYSTEM_POOL - Insufficient memory in a system resource pool<BR>11. QUERY_MEMORY_GRANTS - OOM due to large memory grants held by queries<BR>12. REPLICAS_AND_AVAILABILITY - OOM due to workloads in SloSecSharedPool resource pool |
|available_physical_memory_mb|int, not null|Available physical memory, in megabytes|
|initial_job_object_memory_limit_mb |int, null |Job object memory limit on database engine startup, in megabytes. For more information on Job Objects, see [Resource governance](/azure/azure-sql/database/resource-limits-logical-server#resource-governance). |
|current_job_object_memory_limit_mb |int, null |Job object current memory limit, in megabytes |
|process_memory_usage_mb |int, not null |Total process memory usage in megabytes by the instance |
|non_sos_memory_usage_mb |int, not null | Non-SOS usage in megabytes, including SOS created threads, threads created by non-SOS components, loaded DLLs, etc.|
|committed_memory_target_mb |int, not null |SOS target memory in megabytes |
|committed_memory_mb |int, not null |SOS committed memory in megabytes |
|allocation_potential_memory_mb |int, not null |Memory available to the database engine instance for new allocations, in megabytes |
|oom_factor |tinyint, not null | A value that provides additional information related to the OOM event, for internal use only|
|oom_factor_desc |nvarchar(60), not null |Description of `oom_factor`. For internal use only. One of:<BR>0 - UNDEFINED<BR>1 - ALLOCATION_POTENTIAL<BR>2 - BLOCK_ALLOCATOR<BR>3 - ESCAPE_TIMEOUT<BR>4 - FAIL_FAST<BR>5 - MEMORY_POOL<BR>6 - EMERGENCY_ALLOCATOR<BR>7 - VIRTUAL_ALLOC<BR>8 - SIMULATED<BR>9 - BUF_ALLOCATOR<BR>10 - QUERY_MEM_QUEUE<BR>11 - FRAGMENT<BR>12 - INIT_DESCRIPTOR<BR>13 - MEMORY_POOL_PRESSURE<BR>14 - DESCRIPTOR_ALLOCATOR<BR>15 - DESCRIPTOR_ALLOCATOR_ESCAPE |
|oom_resource_pools |nvarchar(max), null | Resource pools that are out of memory, including memory usage statistics for each pool. This information is provided as a JSON value. |
|top_memory_clerks |nvarchar(max), not null | Top memory clerks by memory consumption, including memory usage statistics for each clerk. This information is provided as a JSON value. |
|top_resource_pools |nvarchar(max), not null | Top resource pools by memory consumption, including memory usage statistics for each resource pool. This information is provided as a JSON value. |
|possible_leaked_memory_clerks |nvarchar(max), null |Memory clerks that have leaked memory. Based on heuristics and provided with a finite degree of confidence. This information is provided as a JSON value. |
|possible_non_sos_leaked_memory_mb |int, null |Leaked non-SOS memory in megabytes, if any. Based on heuristics and provided with a finite degree of confidence. |

## Permissions
On [!INCLUDE[ssazuremi_md](../../includes/ssazuremi_md.md)], requires `VIEW SERVER STATE` permission.

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Azure Active Directory admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.

## Remarks

Each row in this view represents an out of memory (OOM) event that has occurred in the database engine. Not all OOM events may be captured. Older OOM events may disappear from the result set as more recent OOM events occur. Result set is not persisted across restarts of the database engine.

## Example  
  
The following example returns event data ordered by the most recent time for the currently connected database.
  
```sql  
SELECT * FROM sys.dm_os_out_of_memory_events ORDER BY event_time DESC;  
```  

## See also

 - [sys.resource_stats](../../relational-databases/system-catalog-views/sys-resource-stats-azure-sql-database.md)
 - [sys.server_resource_stats](../../relational-databases/system-catalog-views/sys-server-resource-stats-azure-sql-database.md)
 - [sys.dm_db_resource_stats](sys-dm-db-resource-stats-azure-sql-database.md)

## Next steps

 - [Monitor In-Memory OLTP storage in Azure SQL Database and Azure SQL Managed Instance](/azure/azure-sql/in-memory-oltp-monitor-space)
 - [Troubleshoot out of memory errors with Azure SQL Database](/azure/azure-sql/database/troubleshoot-memory-errors-issues)
