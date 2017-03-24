---
title: "sys.dm_exec_trigger_stats (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "dm_exec_trigger_stats"
  - "dm_exec_trigger_stats_TSQL"
  - "sys.dm_exec_trigger_stats_TSQL"
  - "sys.dm_exec_trigger_stats"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_exec_trigger_stats dynamic management function"
ms.assetid: 863498b4-849c-434d-b748-837411458738
caps.latest.revision: 14
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# sys.dm_exec_trigger_stats (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

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
|**execution_count**|**bigint**|Number of times that the trigger has been executed since it was last compiled.|  
|**total_worker_time**|**bigint**|Total amount of CPU time, in microseconds, that was consumed by executions of this trigger since it was compiled.|  
|**last_worker_time**|**bigint**|CPU time, in microseconds, that was consumed the last time the trigger was executed.|  
|**min_worker_time**|**bigint**|Maximum CPU time, in microseconds, that this trigger has ever consumed during a single execution.|  
|**max_worker_time**|**bigint**|Maximum CPU time, in microseconds, that this trigger has ever consumed during a single execution.|  
|**total_physical_reads**|**bigint**|Total number of physical reads performed by executions of this trigger since it was compiled.|  
|**last_physical_reads**|**bigint**|Number of physical reads performed the last time the trigger was executed.|  
|**min_physical_reads**|**bigint**|Minimum number of physical reads that this trigger has ever performed during a single execution.|  
|**max_physical_reads**|**bigint**|Maximum number of physical reads that this trigger has ever performed during a single execution.|  
|**total_logical_writes**|**bigint**|Total number of logical writes performed by executions of this trigger since it was compiled.|  
|**last_logical_writes**|**bigint**|**total_physical_reads**Number of logical writes performed the last time the trigger was executed.|  
|**min_logical_writes**|**bigint**|Minimum number of logical writes that this trigger has ever performed during a single execution.|  
|**max_logical_writes**|**bigint**|Maximum number of logical writes that this trigger has ever performed during a single execution.|  
|**total_logical_reads**|**bigint**|Total number of logical reads performed by executions of this trigger since it was compiled.|  
|**last_logical_reads**|**bigint**|Number of logical reads performed the last time the trigger was executed.|  
|**min_logical_reads**|**bigint**|Minimum number of logical reads that this trigger has ever performed during a single execution.|  
|**max_logical_reads**|**bigint**|Maximum number of logical reads that this trigger has ever performed during a single execution.|  
|**total_elapsed_time**|**bigint**|Total elapsed time, in microseconds, for completed executions of this trigger.|  
|**last_elapsed_time**|**bigint**|Elapsed time, in microseconds, for the most recently completed execution of this trigger.|  
|**min_elapsed_time**|**bigint**|Minimum elapsed time, in microseconds, for any completed execution of this trigger.|  
|**max_elapsed_time**|**bigint**|Maximum elapsed time, in microseconds, for any completed execution of this trigger.|  
  
## Remarks  
 In Windows Azure SQL Database, dynamic management views cannot expose information that would impact database containment or expose information about other databases the user has access to. To avoid exposing this information, every row that contains data that doesn’t belong to the connected tenant is filtered out.  

Statistics in the view are updated when a query is completed.  
  
## Permissions  
On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)], requires `VIEW SERVER STATE` permission.   
On [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)] Premium Tiers, requires the `VIEW DATABASE STATE` permission in the database. On [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)] Standard and Basic Tiers, requires the  **Server admin** or an **Azure Active Directory admin** account.
  
  
## Examples  
 The following example returns information about the top five triggers identified by average elapsed time.  
  
```sql  
PRINT '--top 5 CPU consuming triggers '  
  
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
  
  
