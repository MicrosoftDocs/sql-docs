---
title: "sys.dm_exec_query_plan_stats (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2019"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: conceptual
f1_keywords: 
  - "sys.dm_exec_query_plan_stats"
  - "sys.dm_exec_query_plan_stats_TSQL"
  - "dm_exec_query_plan_stats_TSQL"
  - "dm_exec_query_plan_stats"
helpviewer_keywords: 
  - "sys.dm_exec_query_plan_stats management view"
ms.assetid: fdc7659e-df41-488e-b2b5-0d79734dfacb
author: "pmasl"
ms.author: "pelopes"
manager: amitban
---
# sys.dm_exec_query_plan_stats (Transact-SQL)
[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

Returns the equivalent of the last known actual execution plan for a cached query plan. 

## Syntax

```
sys.dm_exec_query_plan_stats(plan_handle)  
``` 

## Arguments 
*plan_handle*  
Is a token that uniquely identifies a query execution plan for a batch that has executed and its plan resides in the plan cache, or is currently executing. *plan_handle* is **varbinary(64)**.   

The *plan_handle* can be obtained from the following dynamic management objects:  
  
-   [sys.dm_exec_cached_plans &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-cached-plans-transact-sql.md)  
  
-   [sys.dm_exec_query_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-stats-transact-sql.md)  
  
-   [sys.dm_exec_requests &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md)  

-   [sys.dm_exec_procedure_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-procedure-stats-transact-sql.md)  

-   [sys.dm_exec_trigger_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-trigger-stats-transact-sql.md)  

## Table Returned

|Column Name|Data Type|Description|  
|-----------------|---------------|-----------------|
|**dbid**|**smallint**|ID of the context database that was in effect when the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement corresponding to this plan was compiled. For ad hoc and prepared SQL statements, the ID of the database where the statements were compiled.<br /><br /> Column is nullable.|  
|**objectid**|**int**|ID of the object (for example, stored procedure or user-defined function) for this query plan. For ad hoc and prepared batches, this column is **null**.<br /><br /> Column is nullable.|  
|**number**|**smallint**|Numbered stored procedure integer. For example, a group of procedures for the **orders** application may be named **orderproc;1**, **orderproc;2**, and so on. For ad hoc and prepared batches, this column is **null**.<br /><br /> Column is nullable.|  
|**encrypted**|**bit**|Indicates whether the corresponding stored procedure is encrypted.<br /><br /> 0 = not encrypted<br /><br /> 1 = encrypted<br /><br /> Column is not nullable.|  
|**query_plan**|**xml**|Contains the last known runtime Showplan representation of the actual query execution plan that is specified with *plan_handle*. The Showplan is in XML format. One plan is generated for each batch that contains, for example ad hoc [!INCLUDE[tsql](../../includes/tsql-md.md)] statements, stored procedure calls, and user-defined function calls.<br /><br /> Column is nullable.| 

## Remarks
This system function is available starting with [!INCLUDE[sql-server-2019](../../includes/sssqlv15-md.md)] CTP 2.4.

This is an opt-in feature and requires [trace flag](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md) 2451 to be enabled.   

This system function works under the **lightweight** query execution statistics profiling infrastructure. For more information, see [Query Profiling Infrastructure](../../relational-databases/performance/query-profiling-infrastructure.md).  

Under the following conditions, a Showplan output equivalent to an actual execution plan **is returned** in the **query_plan** column of the returned table for **sys.dm_exec_query_plan_stats**:  

-   The plan is cacheable and can be found in the [sys.dm_exec_cached_plans](../../relational-databases/system-dynamic-management-views/sys-dm-exec-cached-plans-transact-sql.md) DMV.

    **AND**

-   The plan may be affected by runtime conditions such as available memory or available DOP. This typically includes plans executed in parallel **OR** include the Hash Match and Spool operators **OR** use dynamic memory grant. 

Under the following conditions, Showplan output **is not returned** in the **query_plan** column of the returned table for **sys.dm_exec_query_plan_stats**:  
  
-   If the query plan that is specified by using *plan_handle* has been evicted from the plan cache, the **query_plan** column of the returned table is null. For example, this condition may occur if there is a time delay between when the plan handle was captured and when it was used with **sys.dm_exec_query_plan_stats**.  
  
-   Some [!INCLUDE[tsql](../../includes/tsql-md.md)] statements are not cached, such as bulk operation statements or statements containing string literals larger than 8 KB in size. XML Showplans for such statements cannot be retrieved by using **sys.dm_exec_query_plan_stats**.  

-   If a [!INCLUDE[tsql](../../includes/tsql-md.md)] batch or stored procedure contains a call to a user-defined function or a call to dynamic SQL, for example using EXEC (*string*), the compiled XML Showplan for the user-defined function is not included in the table returned by **sys.dm_exec_query_plan_stats** for the batch or stored procedure. Instead, you must make a separate call to **sys.dm_exec_query_plan_stats** for the plan handle that corresponds to the user-defined function.  
  
When an ad hoc query uses simple or forced parameterization, the **query_plan** column will contain only the statement text and not the actual query plan. To return the query plan, call **sys.dm_exec_query_plan** for the plan handle of the prepared parameterized query. You can determine whether the query was parameterized by referencing the **sql** column of the [sys.syscacheobjects](../../relational-databases/system-compatibility-views/sys-syscacheobjects-transact-sql.md) view or the text column of the [sys.dm_exec_sql_text](../../relational-databases/system-dynamic-management-views/sys-dm-exec-sql-text-transact-sql.md) dynamic management view.  

In any other scenario not covered above, a **simplified<sup>1</sup>** Showplan output **is returned** in the **query_plan** column of the returned table for **sys.dm_exec_query_plan_stats**.

<sup>1</sup> This refers to a Showplan that only contains the root node operator (SELECT). 

> [!NOTE] 
> Due to a limitation in the number of nested levels allowed in the **xml** data type, **sys.dm_exec_query_plan** cannot return query plans that meet or exceed 128 levels of nested elements. In earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], this condition prevented the query plan from returning and generates error 6335. In [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] Service Pack 2 and later versions, the **query_plan** column returns NULL.  

## Permissions  
 Requires `VIEW SERVER STATE` permission on the server.  

## Examples  
  
### A. Looking at last known actual query execution plan for a specific cached plan  
 The following example queries **sys.dm_exec_cached_plans** to find the interesting plan and copy its `plan_handle` from the output.  
  
```sql  
SELECT * FROM sys.dm_exec_cached_plans;  
GO  
```  
  
Then, to obtain the last known actual query execution plan, use the copied `plan_handle` with system function **sys.dm_exec_query_plan_stats**.  
  
```sql  
SELECT * FROM sys.dm_exec_query_plan_stats(< copied plan_handle >);  
GO  
```   

### B. Looking at last known actual query execution plan for all cached plans
  
```sql  
SELECT *   
FROM sys.dm_exec_cached_plans AS cp
CROSS APPLY sys.dm_exec_sql_text(plan_handle) AS st
CROSS APPLY sys.dm_exec_query_plan_stats(plan_handle) AS qps;  
GO  
```   

### C. Looking at last known actual query execution plan for a specific cached plan and query text

```sql  
SELECT *   
FROM sys.dm_exec_cached_plans AS cp
CROSS APPLY sys.dm_exec_sql_text(plan_handle) AS st
CROSS APPLY sys.dm_exec_query_plan_stats(plan_handle) AS qps
WHERE st.text LIKE 'SELECT * FROM Person.Person%';  
GO  
```   

## See Also
  [Trace Flags](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md)  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Database Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/database-related-dynamic-management-views-transact-sql.md)  

