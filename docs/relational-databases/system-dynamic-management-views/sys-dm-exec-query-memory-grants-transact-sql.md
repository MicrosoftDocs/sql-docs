---
title: "sys.dm_exec_query_memory_grants (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "dm_exec_query_memory_grants_TSQL"
  - "sys.dm_exec_query_memory_grants"
  - "sys.dm_exec_query_memory_grants_TSQL"
  - "dm_exec_query_memory_grants"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_exec_query_memory_grants dynamic management view"
ms.assetid: 2c417747-2edd-4e0d-8a9c-e5f445985c1a
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_exec_query_memory_grants (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns information about all queries that have requested and are waiting for a memory grant or have been given a memory grant. Queries that do not require a memory grant will not appear in this view. For example, sort and hash join operations have memory grants for query execution, while queries without an **ORDER BY** clause will not have a memory grant.  
  
 In [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], dynamic management views cannot expose information that would impact database containment or expose information about other databases the user has access to. To avoid exposing this information, every row that contains data that doesn't belong to the connected tenant is filtered out. In addition, the values in the columns **scheduler_id**, **wait_order**, **pool_id**, **group_id** are filtered; the column value is set to NULL.  
  
> [!NOTE]  
> To call this from [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the name **sys.dm_pdw_nodes_exec_query_memory_grants**.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**session_id**|**smallint**|ID (SPID) of the session where this query is running.|  
|**request_id**|**int**|ID of the request. Unique in the context of the session.|  
|**scheduler_id**|**int**|ID of the scheduler that is scheduling this query.|  
|**dop**|**smallint**|Degree of parallelism of this query.|  
|**request_time**|**datetime**|Date and time when this query requested the memory grant.|  
|**grant_time**|**datetime**|Date and time when memory was granted for this query. NULL if memory is not granted yet.|  
|**requested_memory_kb**|**bigint**|Total requested amount of memory in kilobytes.|  
|**granted_memory_kb**|**bigint**|Total amount of memory actually granted in kilobytes. Can be NULL if the memory is not granted yet. For a typical situation, this value should be the same as **requested_memory_kb**. For index creation, the server may allow additional on-demand memory beyond initially granted memory.|  
|**required_memory_kb**|**bigint**|Minimum memory required to run this query in kilobytes. **requested_memory_kb** is the same or larger than this amount.|  
|**used_memory_kb**|**bigint**|Physical memory used at this moment in kilobytes.|  
|**max_used_memory_kb**|**bigint**|Maximum physical memory used up to this moment in kilobytes.|  
|**query_cost**|**float**|Estimated query cost.|  
|**timeout_sec**|**int**|Time-out in seconds before this query gives up the memory grant request.|  
|**resource_semaphore_id**|**smallint**|Non-unique ID of the resource semaphore on which this query is waiting.<br /><br /> **Note:** This ID is unique in versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that are earlier than [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)]. This change can affect troubleshooting query execution. For more information, see the "Remarks" section later in this topic.|  
|**queue_id**|**smallint**|ID of waiting queue where this query waits for memory grants. NULL if the memory is already granted.|  
|**wait_order**|**int**|Sequential order of waiting queries within the specified **queue_id**. This value can change for a given query if other queries get memory grants or time out. NULL if memory is already granted.|  
|**is_next_candidate**|**bit**|Candidate for next memory grant.<br /><br /> 1 = Yes<br /><br /> 0 = No<br /><br /> NULL = Memory is already granted.|  
|**wait_time_ms**|**bigint**|Wait time in milliseconds. NULL if the memory is already granted.|  
|**plan_handle**|**varbinary(64)**|Identifier for this query plan. Use **sys.dm_exec_query_plan** to extract the actual XML plan.|  
|**sql_handle**|**varbinary(64)**|Identifier for [!INCLUDE[tsql](../../includes/tsql-md.md)] text for this query. Use **sys.dm_exec_sql_text** to get the actual [!INCLUDE[tsql](../../includes/tsql-md.md)] text.|  
|**group_id**|**int**|ID for the workload group where this query is running.|  
|**pool_id**|**int**|ID of the resource pool that this workload group belongs to.|  
|**is_small**|**tinyint**|When set to 1, indicates that this grant uses the small resource semaphore. When set to 0, indicates that a regular semaphore is used.|  
|**ideal_memory_kb**|**bigint**|Size, in kilobytes (KB), of the memory grant to fit everything into physical memory. This is based on the cardinality estimate.|  
|**pdw_node_id**|**int**|**Applies to**: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]<br /><br /> The identifier for the node that this distribution is on.|  
  
## Permissions  

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)], requires `VIEW SERVER STATE` permission.   
On [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], requires the `VIEW DATABASE STATE` permission in the database.   
   
## Remarks  
 A typical debugging scenario for query time-out may look like the following:  
  
-   Check overall system memory status using [sys.dm_os_memory_clerks](../../relational-databases/system-dynamic-management-views/sys-dm-os-memory-clerks-transact-sql.md), [sys.dm_os_sys_info](../../relational-databases/system-dynamic-management-views/sys-dm-os-sys-info-transact-sql.md), and various performance counters.  
  
-   Check for query-execution memory reservations in **sys.dm_os_memory_clerks** where `type = 'MEMORYCLERK_SQLQERESERVATIONS'`.  
  
-   Check for queries waiting<sup>1</sup> for grants using **sys.dm_exec_query_memory_grants**.  
  
    ```sql  
    --Find all queries waiting in the memory queue  
    SELECT * FROM sys.dm_exec_query_memory_grants where grant_time is null  
    ```  
    
    <sup>1</sup> In this scenario, the wait type is typically RESOURCE_SEMAPHORE. For more information, see [sys.dm_os_wait_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-wait-stats-transact-sql.md). 
  
-   Search cache for queries with memory grants using [sys.dm_exec_cached_plans &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-cached-plans-transact-sql.md) and [sys.dm_exec_query_plan &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-plan-transact-sql.md)  
  
    ```sql  
    -- retrieve every query plan from the plan cache  
    USE master;  
    GO  
    SELECT * FROM sys.dm_exec_cached_plans cp CROSS APPLY sys.dm_exec_query_plan(cp.plan_handle);  
    GO  
    ```  
  
-   Further examine memory-intensive queries using [sys.dm_exec_requests](../../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md).  
  
    ```sql  
    --Find top 5 queries by average CPU time  
    SELECT TOP 5 total_worker_time/execution_count AS [Avg CPU Time],  
      plan_handle, query_plan   
    FROM sys.dm_exec_query_stats AS qs  
    CROSS APPLY sys.dm_exec_query_plan(qs.plan_handle)  
    ORDER BY total_worker_time/execution_count DESC;  
    GO  
    ```  
  
-   If a runaway query is suspected, examine the Showplan from [sys.dm_exec_query_plan](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-plan-transact-sql.md) and batch text from [sys.dm_exec_sql_text](../../relational-databases/system-dynamic-management-views/sys-dm-exec-sql-text-transact-sql.md).  
  
 Queries that use dynamic management views that include `ORDER BY` or aggregates may increase memory consumption and thus contribute to the problem they are troubleshooting.  
  
 The Resource Governor feature enables a database administrator to distribute server resources among resource pools, up to a maximum of 64 pools. Beginning with [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], each pool behaves like a small independent server instance and requires 2 semaphores. The number of rows that are returned from **sys.dm_exec_query_resource_semaphores** can be up to 20 times more than the rows that are returned in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)].  
  
## See Also  
 [sys.dm_exec_query_resource_semaphores &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-resource-semaphores-transact-sql.md)     
 [sys.dm_os_wait_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-wait-stats-transact-sql.md)     
 [Execution Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/execution-related-dynamic-management-views-and-functions-transact-sql.md)  
  
  
