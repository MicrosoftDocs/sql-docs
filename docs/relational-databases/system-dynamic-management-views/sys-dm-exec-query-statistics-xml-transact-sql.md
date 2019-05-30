---
title: "sys.dm_exec_query_statistics_xml (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "11/16/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: conceptual
f1_keywords: 
  - "sys.dm_exec_query_statistics_xml"
  - "sys.dm_exec_query_statistics_xml_TSQL"
  - "dm_exec_query_statistics_xml_TSQL"
  - "dm_exec_query_statistics_xml"
helpviewer_keywords: 
  - "sys.dm_exec_query_statistics_xml management view"
ms.assetid: fdc7659e-df41-488e-b2b5-0d79734dfecb
author: "pmasl"
ms.author: "pelopes"
manager: craigg
---
# sys.dm_exec_query_statistics_xml (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

Returns query execution plan for in-flight requests. Use this DMV to retrieve showplan XML with transient statistics. 

## Syntax

```
sys.dm_exec_query_statistics_xml(session_id)  
``` 

## Arguments 
*session_id*  
 Is the session id executing the batch to be looked up. *session_id* is **smallint**. *session_id* can be obtained from the following dynamic management objects:  
  
-   [sys.dm_exec_requests](../../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md)  
  
-   [sys.dm_exec_sessions](../../relational-databases/system-dynamic-management-views/sys-dm-exec-sessions-transact-sql.md)  
  
-   [sys.dm_exec_connections](../../relational-databases/system-dynamic-management-views/sys-dm-exec-connections-transact-sql.md)  

## Table Returned

|Column Name|Data Type|Description|  
|-----------------|---------------|-----------------|
|session_id|**smallint**|ID of the session. Not nullable.|
|request_id|**int**|ID of the request. Not nullable.|
|sql_handle|**varbinary(64)**|Is a token that uniquely identifies the batch or stored procedure that the query is part of. Nullable.|
|plan_handle|**varbinary(64)**|Is a token that uniquely identifies a query execution plan for a batch that is currently executing. Nullable.|
|query_plan|**xml**|Contains the runtime Showplan representation of the query execution plan that is specified with *plan_handle* containing partial statistics. The Showplan is in XML format. One plan is generated for each batch that contains, for example ad hoc [!INCLUDE[tsql](../../includes/tsql-md.md)] statements, stored procedure calls, and user-defined function calls. Nullable.|

## Remarks
This system function is available starting with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP1. See KB [3190871](https://support.microsoft.com/en-us/help/3190871)

This system function works under both **standard** and **lightweight** query execution statistics profiling infrastructure. For more information, see [Query Profiling Infrastructure](../../relational-databases/performance/query-profiling-infrastructure.md).  

Under the following conditions, no Showplan output is returned in the **query_plan** column of the returned table for **sys.dm_exec_query_statistics_xml**:  
  
-   If the query plan that corresponds to the specified *session_id* is no longer executing, the **query_plan** column of the returned table is null. For example, this condition may occur if there is a time delay between when the plan handle was captured and when it was used with **sys.dm_exec_query_statistics_xml**.  
    
Due to a limitation in the number of nested levels allowed in the **xml** data type, **sys.dm_exec_query_statistics_xml** cannot return query plans that meet or exceed 128 levels of nested elements. In earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], this condition prevented the query plan from returning and generates error 6335. In [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] Service Pack 2 and later versions, the **query_plan** column returns NULL.   

## Permissions  
 Requires `VIEW SERVER STATE` permission on the server.  

## Examples  
  
### A. Looking at live query plan and execution statistics for a running batch  
 The following example queries **sys.dm_exec_requests** to find the interesting query and copy its `session_id` from the output.  
  
```sql  
SELECT * FROM sys.dm_exec_requests;  
GO  
```  
  
 Then, to obtain the live query plan and execution statistics, use the copied `session_id` with system function **sys.dm_exec_query_statistics_xml**.  
  
```sql  
--Run this in a different session than the session in which your query is running.
SELECT * FROM sys.dm_exec_query_statistics_xml(< copied session_id >);  
GO  
```   

 Or combined for all running requests.  
  
```sql  
--Run this in a different session than the session in which your query is running.
SELECT * FROM sys.dm_exec_requests
CROSS APPLY sys.dm_exec_query_statistics_xml(session_id);  
GO  
```   
  
## See Also
  [Trace Flags](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md)  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Database Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/database-related-dynamic-management-views-transact-sql.md)  

