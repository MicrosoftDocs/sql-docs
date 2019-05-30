---
title: "sys.dm_exec_query_stats (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "12/18/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "dm_exec_query_stats_TSQL"
  - "dm_exec_query_stats"
  - "sys.dm_exec_query_stats"
  - "sys.dm_exec_query_stats_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_exec_query_stats dynamic management view"
ms.assetid: eb7b58b8-3508-4114-97c2-d877bcb12964
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_exec_query_stats (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Returns aggregate performance statistics for cached query plans in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The view contains one row per query statement within the cached plan, and the lifetime of the rows are tied to the plan itself. When a plan is removed from the cache, the corresponding rows are eliminated from this view.  
  
> [!NOTE]
> An initial query of **sys.dm_exec_query_stats** might produce inaccurate results if there is a workload currently executing on the server. More accurate results may be determined by rerunning the query.  
  
> [!NOTE]
> To call this from [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the name **sys.dm_pdw_nodes_exec_query_stats**.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**sql_handle**|**varbinary(64)**  |Is a token that uniquely identifies the batch or stored procedure that the query is part of.<br /><br /> **sql_handle**, together with **statement_start_offset** and **statement_end_offset**, can be used to retrieve the SQL text of the query by calling the **sys.dm_exec_sql_text** dynamic management function.|  
|**statement_start_offset**|**int**|Indicates, in bytes, beginning with 0, the starting position of the query that the row describes within the text of its batch or persisted object.|  
|**statement_end_offset**|**int**|Indicates, in bytes, starting with 0, the ending position of the query that the row describes within the text of its batch or persisted object. For versions before [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)], a value of -1 indicates the end of the batch. Trailing comments are no longer include.|  
|**plan_generation_num**|**bigint**|A sequence number that can be used to distinguish between instances of plans after a recompile.|  
|**plan_handle**|**varbinary(64)**|Is a token that uniquely identifies a query execution plan for a batch that has executed and its plan resides in the plan cache, or is currently executing. This value can be passed to the [sys.dm_exec_query_plan](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-plan-transact-sql.md) dynamic management function to obtain the query plan.<br /><br /> Will always be 0x000 when a natively compiled stored procedure queries a memory-optimized table.|  
|**creation_time**|**datetime**|Time at which the plan was compiled.|  
|**last_execution_time**|**datetime**|Last time at which the plan started executing.|  
|**execution_count**|**bigint**|Number of times that the plan has been executed since it was last compiled.|  
|**total_worker_time**|**bigint**|Total amount of CPU time, reported in microseconds (but only accurate to milliseconds), that was consumed by executions of this plan since it was compiled.<br /><br /> For natively compiled stored procedures, **total_worker_time** may not be accurate if many executions take less than 1 millisecond.|  
|**last_worker_time**|**bigint**|CPU time, reported in microseconds (but only accurate to milliseconds), that was consumed the last time the plan was executed. <sup>1</sup>|  
|**min_worker_time**|**bigint**|Minimum CPU time, reported in microseconds (but only accurate to milliseconds), that this plan has ever consumed during a single execution. <sup>1</sup>|  
|**max_worker_time**|**bigint**|Maximum CPU time, reported in microseconds (but only accurate to milliseconds), that this plan has ever consumed during a single execution. <sup>1</sup>|  
|**total_physical_reads**|**bigint**|Total number of physical reads performed by executions of this plan since it was compiled.<br /><br /> Will always be 0 querying a memory-optimized table.|  
|**last_physical_reads**|**bigint**|Number of physical reads performed the last time the plan was executed.<br /><br /> Will always be 0 querying a memory-optimized table.|  
|**min_physical_reads**|**bigint**|Minimum number of physical reads that this plan has ever performed during a single execution.<br /><br /> Will always be 0 querying a memory-optimized table.|  
|**max_physical_reads**|**bigint**|Maximum number of physical reads that this plan has ever performed during a single execution.<br /><br /> Will always be 0 querying a memory-optimized table.|  
|**total_logical_writes**|**bigint**|Total number of logical writes performed by executions of this plan since it was compiled.<br /><br /> Will always be 0 querying a memory-optimized table.|  
|**last_logical_writes**|**bigint**|Number of buffer pool pages dirtied during the most recently completed execution of the plan.<br /><br />After a page is read, the page becomes dirty only the first time it is modified. When a page becomes dirty, this number is incremented. Subsequent modifications of an already dirty page do not affect this number.<br /><br />This number will always be 0 when querying a memory-optimized table.|  
|**min_logical_writes**|**bigint**|Minimum number of logical writes that this plan has ever performed during a single execution.<br /><br /> Will always be 0 querying a memory-optimized table.|  
|**max_logical_writes**|**bigint**|Maximum number of logical writes that this plan has ever performed during a single execution.<br /><br /> Will always be 0 querying a memory-optimized table.|  
|**total_logical_reads**|**bigint**|Total number of logical reads performed by executions of this plan since it was compiled.<br /><br /> Will always be 0 querying a memory-optimized table.|  
|**last_logical_reads**|**bigint**|Number of logical reads performed the last time the plan was executed.<br /><br /> Will always be 0 querying a memory-optimized table.|  
|**min_logical_reads**|**bigint**|Minimum number of logical reads that this plan has ever performed during a single execution.<br /><br /> Will always be 0 querying a memory-optimized table.|  
|**max_logical_reads**|**bigint**|Maximum number of logical reads that this plan has ever performed during a single execution.<br /><br /> Will always be 0 querying a memory-optimized table.|  
|**total_clr_time**|**bigint**|Time, reported in microseconds (but only accurate to milliseconds), consumed inside [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] common language runtime (CLR) objects by executions of this plan since it was compiled. The CLR objects can be stored procedures, functions, triggers, types, and aggregates.|  
|**last_clr_time**|**bigint**|Time, reported in microseconds (but only accurate to milliseconds) consumed by execution inside [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] CLR objects during the last execution of this plan. The CLR objects can be stored procedures, functions, triggers, types, and aggregates.|  
|**min_clr_time**|**bigint**|Minimum time, reported in microseconds (but only accurate to milliseconds), that this plan has ever consumed inside [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] CLR objects during a single execution. The CLR objects can be stored procedures, functions, triggers, types, and aggregates.|  
|**max_clr_time**|**bigint**|Maximum time, reported in microseconds (but only accurate to milliseconds), that this plan has ever consumed inside the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] CLR during a single execution. The CLR objects can be stored procedures, functions, triggers, types, and aggregates.|  
|**total_elapsed_time**|**bigint**|Total elapsed time, reported in microseconds (but only accurate to milliseconds), for completed executions of this plan.|  
|**last_elapsed_time**|**bigint**|Elapsed time, reported in microseconds (but only accurate to milliseconds), for the most recently completed execution of this plan.|  
|**min_elapsed_time**|**bigint**|Minimum elapsed time, reported in microseconds (but only accurate to milliseconds), for any completed execution of this plan.|  
|**max_elapsed_time**|**bigint**|Maximum elapsed time, reported in microseconds (but only accurate to milliseconds), for any completed execution of this plan.|  
|**query_hash**|**Binary(8)**|Binary hash value calculated on the query and used to identify queries with similar logic. You can use the query hash to determine the aggregate resource usage for queries that differ only by literal values.|  
|**query_plan_hash**|**binary(8)**|Binary hash value calculated on the query execution plan and used to identify similar query execution plans. You can use query plan hash to find the cumulative cost of queries with similar execution plans.<br /><br /> Will always be 0x000 when a natively compiled stored procedure queries a memory-optimized table.|  
|**total_rows**|**bigint**|Total number of rows returned by the query. Cannot be null.<br /><br /> Will always be 0 when a natively compiled stored procedure queries a memory-optimized table.|  
|**last_rows**|**bigint**|Number of rows returned by the last execution of the query. Cannot be null.<br /><br /> Will always be 0 when a natively compiled stored procedure queries a memory-optimized table.|  
|**min_rows**|**bigint**|Minimum number of rows ever returned by the query during one execution. Cannot be null.<br /><br /> Will always be 0 when a natively compiled stored procedure queries a memory-optimized table.|  
|**max_rows**|**bigint**|Maximum number of rows ever returned by the query during one execution. Cannot be null.<br /><br /> Will always be 0 when a natively compiled stored procedure queries a memory-optimized table.|  
|**statement_sql_handle**|**varbinary(64)**|**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> Populated with non-NULL values only if Query Store is turned on and collecting the stats for that particular query.|  
|**statement_context_id**|**bigint**|**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> Populated with non-NULL values only if Query Store is turned on and collecting the stats for that particular query.|  
|**total_dop**|**bigint**|The total sum of degree of parallelism this plan used since it was compiled. It will always be 0 for querying a memory-optimized table.<br /><br /> **Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].|  
|**last_dop**|**bigint**|The degree of parallelism when this plan executed last time. It will always be 0 for querying a memory-optimized table.<br /><br /> **Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].|  
|**min_dop**|**bigint**|The minimum degree of parallelism this plan ever used during one execution. It will always be 0 for querying a memory-optimized table.<br /><br /> **Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].|  
|**max_dop**|**bigint**|The maximum degree of parallelism this plan ever used during one execution. It will always be 0 for querying a memory-optimized table.<br /><br /> **Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].|  
|**total_grant_kb**|**bigint**|The total amount of reserved memory grant in KB this plan received since it was compiled. It will always be 0 for querying a memory-optimized table.<br /><br /> **Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].|  
|**last_grant_kb**|**bigint**|The amount of reserved memory grant in KB when this plan executed last time. It will always be 0 for querying a memory-optimized table.<br /><br /> **Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].|  
|**min_grant_kb**|**bigint**|The minimum amount of reserved memory grant in KB this plan ever received during one execution. It will always be 0 for querying a memory-optimized table.<br /><br /> **Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].|  
|**max_grant_kb**|**bigint**|The maximum amount of reserved memory grant in KB this plan ever received during one execution. It will always be 0 for querying a memory-optimized table.<br /><br /> **Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].|  
|**total_used_grant_kb**|**bigint**|The total amount of reserved memory grant in KB this plan used since it was compiled. It will always be 0 for querying a memory-optimized table.<br /><br /> **Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].|  
|**last_used_grant_kb**|**bigint**|The amount of used memory grant in KB when this plan executed last time. It will always be 0 for querying a memory-optimized table.<br /><br /> **Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].|  
|**min_used_grant_kb**|**bigint**|The minimum amount of used memory grant in KB this plan ever used during one execution. It will always be 0 for querying a memory-optimized table.<br /><br /> **Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].|  
|**max_used_grant_kb**|**bigint**|The maximum amount of used memory grant in KB this plan ever used during one execution. It will always be 0 for querying a memory-optimized table.<br /><br /> **Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].|  
|**total_ideal_grant_kb**|**bigint**|The total amount of ideal memory grant in KB this plan estimated since it was compiled. It will always be 0 for querying a memory-optimized table.<br /><br /> **Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].|  
|**last_ideal_grant_kb**|**bigint**|The amount of ideal memory grant in KB when this plan executed last time. It will always be 0 for querying a memory-optimized table.<br /><br /> **Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].|  
|**min_ideal_grant_kb**|**bigint**|The minimum amount of ideal memory grant in KB this plan ever estimated during one execution. It will always be 0 for querying a memory-optimized table.<br /><br /> **Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].|  
|**max_ideal_grant_kb**|**bigint**|The maximum amount of ideal memory grant in KB this plan ever estimated during one execution. It will always be 0 for querying a memory-optimized table.<br /><br /> **Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].|  
|**total_reserved_threads**|**bigint**|The total sum of reserved parallel threads this plan ever used since it was compiled. It will always be 0 for querying a memory-optimized table.<br /><br /> **Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].|  
|**last_reserved_threads**|**bigint**|The number of reserved parallel threads when this plan executed last time. It will always be 0 for querying a memory-optimized table.<br /><br /> **Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].|  
|**min_reserved_threads**|**bigint**|The minimum number of reserved parallel threads this plan ever used during one execution.  It will always be 0 for querying a memory-optimized table.<br /><br /> **Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].|  
|**max_reserved_threads**|**bigint**|The maximum number of reserved parallel threads this plan ever used during one execution. It will always be 0 for querying a memory-optimized table.<br /><br /> **Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].|  
|**total_used_threads**|**bigint**|The total sum of used parallel threads this plan ever used since it was compiled. It will always be 0 for querying a memory-optimized table.<br /><br /> **Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].|  
|**last_used_threads**|**bigint**|The number of used parallel threads when this plan executed last time. It will always be 0 for querying a memory-optimized table.<br /><br /> **Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].|  
|**min_used_threads**|**bigint**|The minimum number of used parallel threads this plan ever used during one execution. It will always be 0 for querying a memory-optimized table.<br /><br /> **Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].|  
|**max_used_threads**|**bigint**|The maximum number of used parallel threads this plan ever used during one execution. It will always be 0 for querying a memory-optimized table.<br /><br /> **Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].|  
|**total_columnstore_segment_reads**|**bigint**|The total sum of columnstore segments read by the query. Cannot be null.<br /><br /> **Applies to**: Starting with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP2 and [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] CU3|    
|**last_columnstore_segment_reads**|**bigint**|The number of columnstore segments read by the last execution of the query. Cannot be null.<br /><br /> **Applies to**: Starting with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP2 and [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] CU3|    
|**min_columnstore_segment_reads**|**bigint**|The minimum number of columnstore segments ever read by the query during one execution. Cannot be null.<br /><br /> **Applies to**: Starting with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP2 and [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] CU3|    
|**max_columnstore_segment_reads**|**bigint**|The maximum number of columnstore segments ever read by the query during one execution. Cannot be null.<br /><br /> **Applies to**: Starting with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP2 and [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] CU3|    
|**total_columnstore_segment_skips**|**bigint**|The total sum of columnstore segments skipped by the query. Cannot be null.<br /><br /> **Applies to**: Starting with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP2 and [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] CU3|    
|**last_columnstore_segment_skips**|**bigint**|The number of columnstore segments skipped by the last execution of the query. Cannot be null.<br /><br /> **Applies to**: Starting with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP2 and [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] CU3|    
|**min_columnstore_segment_skips**|**bigint**|The minimum number of columnstore segments ever skipped by the query during one execution. Cannot be null.<br /><br /> **Applies to**: Starting with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP2 and [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] CU3|    
|**max_columnstore_segment_skips**|**bigint**|The maximum number of columnstore segments ever skipped by the query during one execution. Cannot be null.<br /><br /> **Applies to**: Starting with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP2 and [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] CU3|
|**total_spills**|**bigint**|The total number of pages spilled by execution of this query since it was compiled.<br /><br /> **Applies to**: Starting with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP2 and [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] CU3|  
|**last_spills**|**bigint**|The number of pages spilled the last time the query was executed.<br /><br /> **Applies to**: Starting with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP2 and [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] CU3|  
|**min_spills**|**bigint**|The minimum number of pages that this query has ever spilled during a single execution.<br /><br /> **Applies to**: Starting with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP2 and [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] CU3|  
|**max_spills**|**bigint**|The maximum number of pages that this query has ever spilled during a single execution.<br /><br /> **Applies to**: Starting with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP2 and [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] CU3|  
|**pdw_node_id**|**int**|The identifier for the node that this distribution is on.<br /><br /> **Applies to**: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]| 

> [!NOTE]
> <sup>1</sup> For natively compiled stored procedures when statistics collection is enabled, worker time is collected in milliseconds. If the query executes in less than one millisecond, the value will be 0.  
  
## Permissions  

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)], requires `VIEW SERVER STATE` permission.   
On [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)], requires the `VIEW DATABASE STATE` permission in the database.   
   
## Remarks  
 Statistics in the view are updated when a query is completed.  
  
## Examples  
  
### A. Finding the TOP N queries  
 The following example returns information about the top five queries ranked by average CPU time. This example aggregates the queries according to their query hash so that logically equivalent queries are grouped by their cumulative resource consumption.  
  
```sql  
SELECT TOP 5 query_stats.query_hash AS "Query Hash",   
    SUM(query_stats.total_worker_time) / SUM(query_stats.execution_count) AS "Avg CPU Time",  
    MIN(query_stats.statement_text) AS "Statement Text"  
FROM   
    (SELECT QS.*,   
    SUBSTRING(ST.text, (QS.statement_start_offset/2) + 1,  
    ((CASE statement_end_offset   
        WHEN -1 THEN DATALENGTH(ST.text)  
        ELSE QS.statement_end_offset END   
            - QS.statement_start_offset)/2) + 1) AS statement_text  
     FROM sys.dm_exec_query_stats AS QS  
     CROSS APPLY sys.dm_exec_sql_text(QS.sql_handle) as ST) as query_stats  
GROUP BY query_stats.query_hash  
ORDER BY 2 DESC;  
```  
  
### B. Returning row count aggregates for a query  
 The following example returns row count aggregate information (total rows, minimum rows, maximum rows and last rows) for queries.  
  
```sql  
SELECT qs.execution_count,  
    SUBSTRING(qt.text,qs.statement_start_offset/2 +1,   
                 (CASE WHEN qs.statement_end_offset = -1   
                       THEN LEN(CONVERT(nvarchar(max), qt.text)) * 2   
                       ELSE qs.statement_end_offset end -  
                            qs.statement_start_offset  
                 )/2  
             ) AS query_text,   
     qt.dbid, dbname= DB_NAME (qt.dbid), qt.objectid,   
     qs.total_rows, qs.last_rows, qs.min_rows, qs.max_rows  
FROM sys.dm_exec_query_stats AS qs   
CROSS APPLY sys.dm_exec_sql_text(qs.sql_handle) AS qt   
WHERE qt.text like '%SELECT%'   
ORDER BY qs.execution_count DESC;  
```  
  
## See also  
[Execution Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/execution-related-dynamic-management-views-and-functions-transact-sql.md)    
[sys.dm_exec_sql_text &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-sql-text-transact-sql.md)    
[sys.dm_exec_query_plan &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-plan-transact-sql.md)    
[sys.dm_exec_procedure_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-procedure-stats-transact-sql.md)     
[sys.dm_exec_trigger_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-trigger-stats-transact-sql.md)     
[sys.dm_exec_cached_plans &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-cached-plans-transact-sql.md)    
  


