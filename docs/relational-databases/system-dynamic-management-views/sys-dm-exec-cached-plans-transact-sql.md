---
title: "sys.dm_exec_cached_plans (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/18/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_exec_cached_plans"
  - "dm_exec_cached_plans"
  - "dm_exec_cached_plans_TSQL"
  - "sys.dm_exec_cached_plans_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_exec_cached_plans dynamic management view"
ms.assetid: 95b707d3-3a93-407f-8e88-4515d4f2039d
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_exec_cached_plans (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns a row for each query plan that is cached by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] for faster query execution. You can use this dynamic management view to find cached query plans, cached query text, the amount of memory taken by cached plans, and the reuse count of the cached plans.  
  
 In [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], dynamic management views cannot expose information that would impact database containment or expose information about other databases the user has access to. To avoid exposing this information, every row that contains data that doesn't belong to the connected tenant is filtered out. In addition, the values in the columns **memory_object_address** and **pool_id** are filtered; the column value is set to NULL.  
  
> [!NOTE]  
>  To call this from [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the name **sys.dm_pdw_nodes_exec_cached_plans**.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|bucketid|**int**|ID of the hash bucket in which the entry is cached. The value indicates a range from 0 through the hash table size for the type of cache.<br /><br /> For the SQL Plans and Object Plans caches, the hash table size can be up to 10007 on 32-bit systems and up to 40009 on 64-bit systems. For the Bound Trees cache, the hash table size can be up to 1009 on 32-bit systems and up to 4001 on 64-bit systems. For the Extended Stored Procedures cache the hash table size can be up to 127 on 32-bit and 64-bit systems.|  
|refcounts|**int**|Number of cache objects that are referencing this cache object. **Refcounts** must be at least 1 for an entry to be in the cache.|  
|usecounts|**int**|Number of times the cache object has been looked up. Not incremented when parameterized queries find a plan in the cache. Can be incremented multiple times when using showplan.|  
|size_in_bytes|**int**|Number of bytes consumed by the cache object.|  
|memory_object_address|**varbinary(8)**|Memory address of the cached entry. This value can be used with [sys.dm_os_memory_objects](../../relational-databases/system-dynamic-management-views/sys-dm-os-memory-objects-transact-sql.md) to get the memory breakdown of the cached plan and with [sys.dm_os_memory_cache_entries](../../relational-databases/system-dynamic-management-views/sys-dm-os-memory-cache-entries-transact-sql.md)_entries to obtain the cost of caching the entry.|  
|cacheobjtype|**nvarchar(34)**|Type of object in the cache. The value can be one of the following:<br /><br /> Compiled Plan<br /><br /> Compiled Plan Stub<br /><br /> Parse Tree<br /><br /> Extended Proc<br /><br /> CLR Compiled Func<br /><br /> CLR Compiled Proc|  
|objtype|**nvarchar(16)**|Type of object. Below are the possible values and their corresponding descriptions.<br /><br /> Proc: Stored procedure<br />Prepared: Prepared statement<br />Adhoc: Ad hoc query. Refers to [!INCLUDE[tsql](../../includes/tsql-md.md)] submitted as language events by using **osql** or **sqlcmd** instead of as remote procedure calls.<br />ReplProc: Replication-filter-procedure<br />Trigger: Trigger<br />View: View<br />Default: Default<br />UsrTab: User table<br />SysTab: System table<br />Check: CHECK constraint<br />Rule: Rule|  
|plan_handle|**varbinary(64)**|Identifier for the in-memory plan. This identifier is transient and remains constant only while the plan remains in the cache. This value may be used with the following dynamic management functions:<br /><br /> [sys.dm_exec_sql_text](../../relational-databases/system-dynamic-management-views/sys-dm-exec-sql-text-transact-sql.md)<br /><br /> [sys.dm_exec_query_plan](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-plan-transact-sql.md)<br /><br /> [sys.dm_exec_plan_attributes](../../relational-databases/system-dynamic-management-views/sys-dm-exec-plan-attributes-transact-sql.md)|  
|pool_id|**int**|The ID of the resource pool against which this plan memory usage is accounted for.|  
|pdw_node_id|**int**|**Applies to**: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]<br /><br /> The identifier for the node that this distribution is on.|  
  
 <sup>1</sup>  
  
## Permissions

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)], requires `VIEW SERVER STATE` permission.   
On [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)], requires the `VIEW DATABASE STATE` permission in the database.   

## Examples  
  
### A. Returning the batch text of cached entries that are reused  
 The following example returns the SQL text of all cached entries that have been used more than once.  
  
```sql  
SELECT usecounts, cacheobjtype, objtype, text   
FROM sys.dm_exec_cached_plans   
CROSS APPLY sys.dm_exec_sql_text(plan_handle)   
WHERE usecounts > 1   
ORDER BY usecounts DESC;  
GO  
```  
  
### B. Returning query plans for all cached triggers  
 The following example returns the query plans of all cached triggers.  
  
```sql  
SELECT plan_handle, query_plan, objtype   
FROM sys.dm_exec_cached_plans   
CROSS APPLY sys.dm_exec_query_plan(plan_handle)   
WHERE objtype ='Trigger';  
GO  
```  
  
### C. Returning the SET options with which the plan was compiled  
 The following example returns the SET options with which the plan was compiled. The `sql_handle` for the plan is also returned. The PIVOT operator is used to output the `set_options` and `sql_handle` attributes as columns rather than as rows. For more information about the value returned in `set_options`, see [sys.dm_exec_plan_attributes &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-plan-attributes-transact-sql.md).  
  
```sql  
SELECT plan_handle, pvt.set_options, pvt.sql_handle  
FROM (  
      SELECT plan_handle, epa.attribute, epa.value   
      FROM sys.dm_exec_cached_plans   
      OUTER APPLY sys.dm_exec_plan_attributes(plan_handle) AS epa  
      WHERE cacheobjtype = 'Compiled Plan'  
      ) AS ecpa   
PIVOT (MAX(ecpa.value) FOR ecpa.attribute IN ("set_options", "sql_handle")) AS pvt;  
GO  
```  
  
### D. Returning the memory breakdown of all cached compiled plans  
 The following example returns a breakdown of the memory used by all compiled plans in the cache.  
  
```sql  
SELECT plan_handle, ecp.memory_object_address AS CompiledPlan_MemoryObject,   
    omo.memory_object_address, type, page_size_in_bytes   
FROM sys.dm_exec_cached_plans AS ecp   
JOIN sys.dm_os_memory_objects AS omo   
    ON ecp.memory_object_address = omo.memory_object_address   
    OR ecp.memory_object_address = omo.parent_address  
WHERE cacheobjtype = 'Compiled Plan';  
GO  
```  
  
## See Also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Execution Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/execution-related-dynamic-management-views-and-functions-transact-sql.md)   
 [sys.dm_exec_query_plan &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-plan-transact-sql.md)   
 [sys.dm_exec_plan_attributes &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-plan-attributes-transact-sql.md)   
 [sys.dm_exec_sql_text &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-sql-text-transact-sql.md)   
 [sys.dm_os_memory_objects &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-memory-objects-transact-sql.md)   
 [sys.dm_os_memory_cache_entries &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-memory-cache-entries-transact-sql.md)   
 [FROM &#40;Transact-SQL&#41;](../../t-sql/queries/from-transact-sql.md)  
  
  


