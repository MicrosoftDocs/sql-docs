---
title: "sys.dm_exec_query_plan (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/02/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "dm_exec_query_plan_TSQL"
  - "sys.dm_exec_query_plan"
  - "dm_exec_query_plan"
  - "sys.dm_exec_query_plan_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_exec_query_plan dynamic management function"
ms.assetid: e26f0867-9be3-4b2e-969e-7f2840230770
author: stevestein
ms.author: sstein
manager: craigg
---
# sys.dm_exec_query_plan (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Returns the Showplan in XML format for the batch specified by the plan handle. The plan specified by the plan handle can either be cached or currently executing.  
  
The XML schema for the Showplan is published and available at [this Microsoft Web site](https://go.microsoft.com/fwlink/?linkid=43100&clcid=0x409). It is also available in the directory where [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
sys.dm_exec_query_plan(plan_handle)  
```  
  
## Arguments  
*plan_handle*  
Uniquely identifies a query plan for a batch that is cached or is currently executing. *plan_handle* is **varbinary(64)**. 

The *plan_handle* can be obtained from the following dynamic management objects:  
  
-   [sys.dm_exec_cached_plans &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-cached-plans-transact-sql.md)  
  
-   [sys.dm_exec_query_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-stats-transact-sql.md)  
  
-   [sys.dm_exec_requests &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md)  

-   [sys.dm_exec_procedure_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-procedure-stats-transact-sql.md)  

-   [sys.dm_exec_trigger_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-trigger-stats-transact-sql.md)  
  
## Table Returned  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**dbid**|**smallint**|ID of the context database that was in effect when the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement corresponding to this plan was compiled. For ad hoc and prepared SQL statements, the ID of the database where the statements were compiled.<br /><br /> Column is nullable.|  
|**objectid**|**int**|ID of the object (for example, stored procedure or user-defined function) for this query plan. For ad hoc and prepared batches, this column is **null**.<br /><br /> Column is nullable.|  
|**number**|**smallint**|Numbered stored procedure integer. For example, a group of procedures for the **orders** application may be named **orderproc;1**, **orderproc;2**, and so on. For ad hoc and prepared batches, this column is **null**.<br /><br /> Column is nullable.|  
|**encrypted**|**bit**|Indicates whether the corresponding stored procedure is encrypted.<br /><br /> 0 = not encrypted<br /><br /> 1 = encrypted<br /><br /> Column is not nullable.|  
|**query_plan**|**xml**|Contains the compile-time Showplan representation of the query execution plan that is specified with *plan_handle*. The Showplan is in XML format. One plan is generated for each batch that contains, for example ad hoc [!INCLUDE[tsql](../../includes/tsql-md.md)] statements, stored procedure calls, and user-defined function calls.<br /><br /> Column is nullable.|  
  
## Remarks  
 Under the following conditions, no Showplan output is returned in the **query_plan** column of the returned table for **sys.dm_exec_query_plan**:  
  
-   If the query plan that is specified by using *plan_handle* has been evicted from the plan cache, the **query_plan** column of the returned table is null. For example, this condition may occur if there is a time delay between when the plan handle was captured and when it was used with **sys.dm_exec_query_plan**.  
  
-   Some [!INCLUDE[tsql](../../includes/tsql-md.md)] statements are not cached, such as bulk operation statements or statements containing string literals larger than 8 KB in size. XML Showplans for such statements cannot be retrieved by using **sys.dm_exec_query_plan** unless the batch is currently executing because they do not exist in the cache.  
  
-   If a [!INCLUDE[tsql](../../includes/tsql-md.md)] batch or stored procedure contains a call to a user-defined function or a call to dynamic SQL, for example using EXEC (*string*), the compiled XML Showplan for the user-defined function is not included in the table returned by **sys.dm_exec_query_plan** for the batch or stored procedure. Instead, you must make a separate call to **sys.dm_exec_query_plan** for the plan handle that corresponds to the user-defined function.  
  
 When an ad hoc query uses simple or forced parameterization, the **query_plan** column will contain only the statement text and not the actual query plan. To return the query plan, call **sys.dm_exec_query_plan** for the plan handle of the prepared parameterized query. You can determine whether the query was parameterized by referencing the **sql** column of the [sys.syscacheobjects](../../relational-databases/system-compatibility-views/sys-syscacheobjects-transact-sql.md) view or the text column of the [sys.dm_exec_sql_text](../../relational-databases/system-dynamic-management-views/sys-dm-exec-sql-text-transact-sql.md) dynamic management view.  
  
 Due to a limitation in the number of nested levels allowed in the **xml** data type, **sys.dm_exec_query_plan** cannot return query plans that meet or exceed 128 levels of nested elements. In earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], this condition prevented the query plan from returning and generates error 6335. In [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] Service Pack 2 and later versions, the **query_plan** column returns NULL. You can use the [sys.dm_exec_text_query_plan &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-text-query-plan-transact-sql.md) dynamic management function to return the output of the query plan in text format.  
  
## Permissions  
 To execute **sys.dm_exec_query_plan**, a user must be a member of the **sysadmin** fixed server role or have the VIEW SERVER STATE permission on the server.  
  
## Examples  
 The following examples show how to use the **sys.dm_exec_query_plan** dynamic management view.  
  
 To view the XML Showplans, execute the following queries in the Query Editor of [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], then click **ShowPlanXML** in the **query_plan** column of the table returned by **sys.dm_exec_query_plan**. The XML Showplan displays in the [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] summary pane. To save the XML Showplan to a file, right-click **ShowPlanXML** in the **query_plan** column, click **Save Results As**, name the file in the format \<*file_name*>.sqlplan; for example, MyXMLShowplan.sqlplan.  
  
### A. Retrieve the cached query plan for a slow-running Transact-SQL query or batch  
 Query plans for various types of [!INCLUDE[tsql](../../includes/tsql-md.md)] batches, such as ad hoc batches, stored procedures, and user-defined functions, are cached in an area of memory called the plan cache. Each cached query plan is identified by a unique identifier called a plan handle. You can specify this plan handle with the **sys.dm_exec_query_plan** dynamic management view to retrieve the execution plan for a particular [!INCLUDE[tsql](../../includes/tsql-md.md)] query or batch.  
  
 If a [!INCLUDE[tsql](../../includes/tsql-md.md)] query or batch runs a long time on a particular connection to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], retrieve the execution plan for that query or batch to discover what is causing the delay. The following example shows how to retrieve the XML Showplan for a slow-running query or batch.  
  
> [!NOTE]  
>  To run this example, replace the values for *session_id* and *plan_handle* with values specific to your server.  
  
 First, retrieve the server process ID (SPID) for the process that is executing the query or batch by using the `sp_who` stored procedure:  
  
```sql  
USE master;  
GO  
exec sp_who;  
GO  
```  
  
 The result set that is returned by `sp_who` indicates that the SPID is `54`. You can use the SPID with the `sys.dm_exec_requests` dynamic management view to retrieve the plan handle by using the following query:  
  
```sql  
USE master;  
GO  
SELECT * FROM sys.dm_exec_requests  
WHERE session_id = 54;  
GO  
```  
  
 The table that is returned by **sys.dm_exec_requests** indicates that the plan handle for the slow-running query or batch is `0x06000100A27E7C1FA821B10600`, which you can specify as the *plan_handle* argument with `sys.dm_exec_query_plan` to retrieve the execution plan in XML format as follows. The execution plan in XML format for the slow-running query or batch is contained in the **query_plan** column of the table returned by `sys.dm_exec_query_plan`.  
  
```sql  
USE master;  
GO  
SELECT * 
FROM sys.dm_exec_query_plan (0x06000100A27E7C1FA821B10600);  
GO  
```  
  
### B. Retrieve every query plan from the plan cache  
 To retrieve a snapshot of all query plans residing in the plan cache, retrieve the plan handles of all query plans in the cache by querying the `sys.dm_exec_cached_plans` dynamic management view. The plan handles are stored in the `plan_handle` column of `sys.dm_exec_cached_plans`. Then use the CROSS APPLY operator to pass the plan handles to `sys.dm_exec_query_plan` as follows. The XML Showplan output for each plan currently in the plan cache is in the `query_plan` column of the table that is returned.  
  
```sql  
USE master;  
GO  
SELECT * 
FROM sys.dm_exec_cached_plans AS cp 
CROSS APPLY sys.dm_exec_query_plan(cp.plan_handle);  
GO  
```  
  
### C. Retrieve every query plan for which the server has gathered query statistics from the plan cache  
 To retrieve a snapshot of all query plans for which the server has gathered statistics that currently reside in the plan cache, retrieve the plan handles of these plans in the cache by querying the `sys.dm_exec_query_stats` dynamic management view. The plan handles are stored in the `plan_handle` column of `sys.dm_exec_query_stats`. Then use the CROSS APPLY operator to pass the plan handles to `sys.dm_exec_query_plan` as follows. The XML Showplan output for each plan for which the server has gathered statistics currently in the plan cache is in the `query_plan` column of the table that is returned.  
  
```sql  
USE master;  
GO  
SELECT * 
FROM sys.dm_exec_query_stats AS qs 
CROSS APPLY sys.dm_exec_query_plan(qs.plan_handle);  
GO  
```  
  
### D. Retrieve information about the top five queries by average CPU time  
 The following example returns the plans and average CPU time for the top five queries.  
  
```sql  
SELECT TOP 5 total_worker_time/execution_count AS [Avg CPU Time],  
   plan_handle, query_plan   
FROM sys.dm_exec_query_stats AS qs  
CROSS APPLY sys.dm_exec_query_plan(qs.plan_handle)  
ORDER BY total_worker_time/execution_count DESC;  
GO  
```  
  
## See Also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [sys.dm_exec_cached_plans &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-cached-plans-transact-sql.md)   
 [sys.dm_exec_query_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-stats-transact-sql.md)   
 [sys.dm_exec_requests &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md)   
 [sp_who &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-who-transact-sql.md)   
 [Showplan Logical and Physical Operators Reference](../../relational-databases/showplan-logical-and-physical-operators-reference.md)   
 [sys.dm_exec_text_query_plan &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-text-query-plan-transact-sql.md)  
  
  
