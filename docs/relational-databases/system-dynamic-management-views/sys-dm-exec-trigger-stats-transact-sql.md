---
title: "sys.dm_exec_trigger_stats (Transact-SQL)"
description: sys.dm_exec_trigger_stats (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/03/2019"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_exec_trigger_stats"
  - "dm_exec_trigger_stats_TSQL"
  - "sys.dm_exec_trigger_stats_TSQL"
  - "sys.dm_exec_trigger_stats"
helpviewer_keywords:
  - "sys.dm_exec_trigger_stats dynamic management function"
dev_langs:
  - "TSQL"
ms.assetid: 863498b4-849c-434d-b748-837411458738
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_exec_trigger_stats (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns aggregate performance statistics for cached triggers. The view contains one row per trigger, and the lifetime of the row is as long as the trigger remains cached. When a trigger is removed from the cache, the corresponding row is eliminated from this view. At that time, a Performance Statistics SQL trace event is raised similar to **sys.dm_exec_query_stats**.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**database_id**|**int**|Database ID in which the trigger resides.|  
|**object_id**|**int**|Object identification number of the trigger.|  
|**type**|**char(2)**|Type of the object:<br /><br /> TA = Assembly (CLR) trigger<br /><br /> TR = SQL trigger|  
|**Type_desc**|**nvarchar(60)**|Description of the object type:<br /><br /> CLR_TRIGGER<br /><br /> SQL_TRIGGER|  
|**sql_handle**|**varbinary(64)**|This can be used to correlate with queries in **sys.dm_exec_query_stats** that were executed from within this trigger.|  
|**plan_handle**|**varbinary(64)**|Identifier for the in-memory plan. This identifier is transient and remains constant only while the plan remains in the cache. This value may be used with the **sys.dm_exec_cached_plans** dynamic management view.|  
|**cached_time**|**datetime**|Time at which the trigger was added to the cache.|  
|**last_execution_time**|**datetime**|Last time at which the trigger was executed.|  
|**execution_count**|**bigint**|The number of times that the trigger has been executed since it was last compiled.|  
|**total_worker_time**|**bigint**|The total amount of CPU time, in microseconds, that was consumed by executions of this trigger since it was compiled.|  
|**last_worker_time**|**bigint**|CPU time, in microseconds, that was consumed the last time the trigger was executed.|  
|**min_worker_time**|**bigint**|The maximum CPU time, in microseconds, that this trigger has ever consumed during a single execution.|  
|**max_worker_time**|**bigint**|The maximum CPU time, in microseconds, that this trigger has ever consumed during a single execution.|  
|**total_physical_reads**|**bigint**|The total number of physical reads performed by executions of this trigger since it was compiled.|  
|**last_physical_reads**|**bigint**|The number of physical reads performed the last time the trigger was executed.|  
|**min_physical_reads**|**bigint**|The minimum number of physical reads that this trigger has ever performed during a single execution.|  
|**max_physical_reads**|**bigint**|The maximum number of physical reads that this trigger has ever performed during a single execution.|  
|**total_logical_writes**|**bigint**|The total number of logical writes performed by executions of this trigger since it was compiled.|  
|**last_logical_writes**|**bigint**|The number of logical writes performed the last time the trigger was executed.|  
|**min_logical_writes**|**bigint**|The minimum number of logical writes that this trigger has ever performed during a single execution.|  
|**max_logical_writes**|**bigint**|The maximum number of logical writes that this trigger has ever performed during a single execution.|  
|**total_logical_reads**|**bigint**|The total number of logical reads performed by executions of this trigger since it was compiled.|  
|**last_logical_reads**|**bigint**|The number of logical reads performed the last time the trigger was executed.|  
|**min_logical_reads**|**bigint**|The minimum number of logical reads that this trigger has ever performed during a single execution.|  
|**max_logical_reads**|**bigint**|The maximum number of logical reads that this trigger has ever performed during a single execution.|  
|**total_elapsed_time**|**bigint**|The total elapsed time, in microseconds, for completed executions of this trigger.|  
|**last_elapsed_time**|**bigint**|Elapsed time, in microseconds, for the most recently completed execution of this trigger.|  
|**min_elapsed_time**|**bigint**|The minimum elapsed time, in microseconds, for any completed execution of this trigger.|  
|**max_elapsed_time**|**bigint**|The maximum elapsed time, in microseconds, for any completed execution of this trigger.| 
|**total_spills**|**bigint**|The total number of pages spilled by execution of this trigger since it was compiled.<br /><br /> **Applies to**: Starting with [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] CU3|  
|**last_spills**|**bigint**|The number of pages spilled the last time the trigger was executed.<br /><br /> **Applies to**: Starting with [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] CU3|  
|**min_spills**|**bigint**|The minimum number of pages that this trigger has ever spilled during a single execution.<br /><br /> **Applies to**: Starting with [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] CU3|  
|**max_spills**|**bigint**|The maximum number of pages that this trigger has ever spilled during a single execution.<br /><br /> **Applies to**: Starting with [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] CU3|  
|**total_page_server_reads**|**bigint**|The total number of page server reads performed by executions of this trigger since it was compiled.<br /><br /> **Applies to**: Azure SQL Database Hyperscale|  
|**last_page_server_reads**|**bigint**|The number of page server reads performed the last time the trigger was executed.<br /><br /> **Applies to**: Azure SQL Database Hyperscale|  
|**min_page_server_reads**|**bigint**|The minimum number of page server reads that this trigger has ever performed during a single execution.<br /><br /> **Applies to**: Azure SQL Database Hyperscale|  
|**max_page_server_reads**|**bigint**|The maximum number of page server reads that this trigger has ever performed during a single execution.<br /><br /> **Applies to**: Azure SQL Database Hyperscale|  

  
## Remarks  
 In [!INCLUDE[ssSDS](../../includes/sssds-md.md)], dynamic management views cannot expose information that would impact database containment or expose information about other databases the user has access to. To avoid exposing this information, every row that contains data that doesn't belong to the connected tenant is filtered out.  

Statistics in the view are updated when a query is completed.  
  
## Permissions  

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] and SQL Managed Instance, requires `VIEW SERVER STATE` permission.

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Azure Active Directory admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.   
  
## Examples  
 The following example returns information about the top five triggers identified by average elapsed time.  
  
```sql  
SELECT TOP 5 d.object_id, d.database_id, DB_NAME(database_id) AS 'database_name',   
    OBJECT_NAME(object_id, database_id) AS 'trigger_name', d.cached_time,  
    d.last_execution_time, d.total_elapsed_time,   
    d.total_elapsed_time/d.execution_count AS [avg_elapsed_time],   
    d.last_elapsed_time, d.execution_count  
FROM sys.dm_exec_trigger_stats AS d  
ORDER BY [total_worker_time] DESC;  
```  
  
## See Also  
[Execution Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/execution-related-dynamic-management-views-and-functions-transact-sql.md)   
[sys.dm_exec_sql_text &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-sql-text-transact-sql.md)   
[sys.dm_exec_query_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-stats-transact-sql.md)   
[sys.dm_exec_procedure_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-procedure-stats-transact-sql.md)   
[sys.dm_exec_cached_plans &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-cached-plans-transact-sql.md)  
  
