---
title: "sys.dm_exec_cached_plan_dependent_objects (Transact-SQL)"
description: sys.dm_exec_cached_plan_dependent_objects returns a row for each execution plan, common language runtime (CLR) execution plan, and cursor associated with a plan.
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/03/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_exec_cached_plan_dependent_objects"
  - "dm_exec_cached_plan_dependent_objects_TSQL"
  - "sys.dm_exec_cached_plan_dependent_objects_TSQL"
  - "dm_exec_cached_plan_dependent_objects"
helpviewer_keywords:
  - "sys.dm_exec_cached_plan_dependent_objects dynamic management function"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_exec_cached_plan_dependent_objects (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns a row for each [!INCLUDE[tsql](../../includes/tsql-md.md)] execution plan, common language runtime (CLR) execution plan, and cursor associated with a plan.  
  
## Syntax  
  
```syntaxsql  
sys.dm_exec_cached_plan_dependent_objects(plan_handle)  
```  
  
## Arguments  

#### *plan_handle*  
Is a token that uniquely identifies a query execution plan for a batch that has executed and its plan resides in the plan cache. `plan_handle` is **varbinary(64)**.   

The `plan_handle` can be obtained from the following dynamic management objects:  
  
-   [sys.dm_exec_cached_plans &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-cached-plans-transact-sql.md)  
  
-   [sys.dm_exec_query_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-stats-transact-sql.md)  
  
-   [sys.dm_exec_requests &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md)  

-   [sys.dm_exec_procedure_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-procedure-stats-transact-sql.md)  

-   [sys.dm_exec_trigger_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-trigger-stats-transact-sql.md)  
  
## Table Returned  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**usecounts**|**int**|Number of times the execution context or cursor has been used.<br /><br /> Column is not nullable.|  
|**memory_object_address**|**varbinary(8)**|Memory address of the execution context or cursor.<br /><br /> Column is not nullable.|  
|**cacheobjtype**|**nvarchar(50)**|The Plan cache object type. Column is not nullable. Possible values are:<br /><br /> Executable plan<br /><br /> CLR compiled function<br /><br /> CLR compiled procedure<br /><br /> Cursor|  
  
## Permissions  
 Requires `VIEW SERVER STATE` permission on the server.  
  
## Physical joins  

:::image type="content" source="../../relational-databases/system-dynamic-management-views/media/join-dm-exec-cached-plan-dependent-objects.svg" alt-text="Diagram of physical joins for sys.dm_exec_cached_plan_dependent_objects.":::
  
## Relationship cardinalities  
  
|From|To|On|Relationship|  
|----------|--------|--------|------------------|  
|`dm_exec_cached_plan_dependent_objects`|`dm_os_memory_objects`|`memory_object_address`|One-to-one|  
  
## Next steps
 [Execution Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/execution-related-dynamic-management-views-and-functions-transact-sql.md)   
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [sys.syscacheobjects &#40;Transact-SQL&#41;](../../relational-databases/system-compatibility-views/sys-syscacheobjects-transact-sql.md)  
  
  
