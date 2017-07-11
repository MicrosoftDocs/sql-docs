---
title: "sys.dm_exec_procedure_stats (Transact-SQL) | Microsoft Docs"
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
  - "sys.dm_exec_procedure_stats_TSQL"
  - "dm_exec_procedure_stats_TSQL"
  - "dm_exec_procedure_stats"
  - "sys.dm_exec_procedure_stats"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_exec_procedure_stats dynamic management view"
ms.assetid: ab8ddde8-1cea-4b41-a7e4-697e6ddd785a
caps.latest.revision: 24
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# sys.dm_exec_procedure_stats (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns aggregate performance statistics for cached stored procedures. The view returns one row for each cached stored procedure plan, and the lifetime of the row is as long as the stored procedure remains cached. When a stored procedure is removed from the cache, the corresponding row is eliminated from this view. At that time, a Performance Statistics SQL trace event is raised similar to **sys.dm_exec_query_stats**.  
  
 In [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], dynamic management views cannot expose information that would impact database containment or expose information about other databases the user has access to. To avoid exposing this information, every row that contains data that doesn’t belong to the connected tenant is filtered out.  
  
> **NOTE:** An initial query of **sys.dm_exec_procedure_stats** might produce inaccurate results if there is a workload currently executing on the server. More accurate results may be determined by rerunning the query.  
  
> **NOTE for Azure SQL Data Warehouse!** To call this from [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the name **sys.dm_pdw_nodes_exec_procedure_stats**.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**database_id**|**int**|Database ID in which the stored procedure resides.|  
|**object_id**|**int**|Object identification number of the stored procedure.|  
|**type**|**char(2)**|Type of the object:<br /><br /> P = SQL stored procedure<br /><br /> PC = Assembly (CLR) stored procedure<br /><br /> X = Extended stored procedure|  
|**type_desc**|**nvarchar(60)**|Description of the object type:<br /><br /> SQL_STORED_PROCEDURE<br /><br /> CLR_STORED_PROCEDURE<br /><br /> EXTENDED_STORED_PROCEDURE|  
|**sql_handle**|**varbinary(64)**|This can be used to correlate with queries in **sys.dm_exec_query_stats** that were executed from within this stored procedure.|  
|**plan_handle**|**varbinary(64)**|Identifier for the in-memory plan. This identifier is transient and remains constant only while the plan remains in the cache. This value may be used with the **sys.dm_exec_cached_plans** dynamic management view.<br /><br /> Will always be 0x000 when a natively compiled stored procedure queries a memory-optimized table.|  
|**cached_time**|**datetime**|Time at which the stored procedure was added to the cache.|  
|**last_execution_time**|**datetime**|Last time at which the stored procedure was executed.|  
|**execution_count**|**bigint**|Number of times that the stored procedure has been executed since it was last compiled.|  
|**total_worker_time**|**bigint**|Total amount of CPU time, in microseconds, that was consumed by executions of this stored procedure since it was compiled.<br /><br /> For natively compiled stored procedures, **total_worker_time** may not be accurate if many executions take less than 1 millisecond.|  
|**last_worker_time**|**bigint**|CPU time, in microseconds, that was consumed the last time the stored procedure was executed. <sup>1</sup>|  
|**min_worker_time**|**bigint**|Minimum CPU time, in microseconds, that this stored procedure has ever consumed during a single execution. <sup>1</sup>|  
|**max_worker_time**|**bigint**|Maximum CPU time, in microseconds, that this stored procedure has ever consumed during a single execution. <sup>1</sup>|  
|**total_physical_reads**|**bigint**|Total number of physical reads performed by executions of this stored procedure since it was compiled.<br /><br /> Will always be 0 querying a memory-optimized table.|  
|**last_physical_reads**|**bigint**|Number of physical reads performed the last time the stored procedure was executed.<br /><br /> Will always be 0 querying a memory-optimized table.|  
|**min_physical_reads**|**bigint**|Minimum number of physical reads that this stored procedure has ever performed during a single execution.<br /><br /> Will always be 0 querying a memory-optimized table.|  
|**max_physical_reads**|**bigint**|Maximum number of physical reads that this stored procedure has ever performed during a single execution.<br /><br /> Will always be 0 querying a memory-optimized table.|  
|**total_logical_writes**|**bigint**|Total number of logical writes performed by executions of this stored procedure since it was compiled.<br /><br /> Will always be 0 querying a memory-optimized table.|  
|**last_logical_writes**|**bigint**|Number of the number of buffer pool pages dirtied the last time the plan was executed. If a page is already dirty (modified) no writes are counted.<br /><br /> Will always be 0 querying a memory-optimized table.|  
|**min_logical_writes**|**bigint**|Minimum number of logical writes that this stored procedure has ever performed during a single execution.<br /><br /> Will always be 0 querying a memory-optimized table.|  
|**max_logical_writes**|**bigint**|Maximum number of logical writes that this stored procedure has ever performed during a single execution.<br /><br /> Will always be 0 querying a memory-optimized table.|  
|**total_logical_reads**|**bigint**|Total number of logical reads performed by executions of this stored procedure since it was compiled.<br /><br /> Will always be 0 querying a memory-optimized table.|  
|**last_logical_reads**|**bigint**|Number of logical reads performed the last time the stored procedure was executed.<br /><br /> Will always be 0 querying a memory-optimized table.|  
|**min_logical_reads**|**bigint**|Minimum number of logical reads that this stored procedure has ever performed during a single execution.<br /><br /> Will always be 0 querying a memory-optimized table.|  
|**max_logical_reads**|**bigint**|Maximum number of logical reads that this stored procedure has ever performed during a single execution.<br /><br /> Will always be 0 querying a memory-optimized table.|  
|**total_elapsed_time**|**bigint**|Total elapsed time, in microseconds, for completed executions of this stored procedure.|  
|**last_elapsed_time**|**bigint**|Elapsed time, in microseconds, for the most recently completed execution of this stored procedure.|  
|**min_elapsed_time**|**bigint**|Minimum elapsed time, in microseconds, for any completed execution of this stored procedure.|  
|**max_elapsed_time**|**bigint**|Maximum elapsed time, in microseconds, for any completed execution of this stored procedure.|  
|**pdw_node_id**|**int**|**Applies to**: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]<br /><br /> The identifier for the node that this distribution is on.|  
  
 <sup>1</sup> For natively compiled stored procedures when statistics collection is enabled, worker time is collected in milliseconds. If the query executes in less than a millisecond, the value will be 0.  
  
## Permissions  
On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)], requires `VIEW SERVER STATE` permission.   
On [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)] Premium Tiers, requires the `VIEW DATABASE STATE` permission in the database. On [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)] Standard and Basic Tiers, requires the  **Server admin** or an **Azure Active Directory admin** account. 
  
## Remarks  
 Statistics in the view are updated when a stored procedure execution completes.  
  
## Examples  
 The following example returns information about the top ten stored procedures identified by average elapsed time.  
  
```  
SELECT TOP 10 d.object_id, d.database_id, OBJECT_NAME(object_id, database_id) 'proc name',   
    d.cached_time, d.last_execution_time, d.total_elapsed_time,  
    d.total_elapsed_time/d.execution_count AS [avg_elapsed_time],  
    d.last_elapsed_time, d.execution_count  
FROM sys.dm_exec_procedure_stats AS d  
ORDER BY [total_worker_time] DESC;  
```  
  
## See Also  
 [Execution Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/execution-related-dynamic-management-views-and-functions-transact-sql.md)   
 [sys.dm_exec_sql_text &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-sql-text-transact-sql.md)   
 [sys.dm_exec_query_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-stats-transact-sql.md)   
 
 [sys.dm_exec_trigger_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-trigger-stats-transact-sql.md)  
  
  


