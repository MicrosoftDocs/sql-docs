---
title: "sys.dm_db_session_space_usage (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "11/16/2015"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "dm_db_session_space_usage_TSQL"
  - "dm_db_session_space_usage"
  - "sys.dm_db_session_space_usage"
  - "sys.dm_db_session_space_usage_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_db_session_space_usage dynamic management view"
ms.assetid: a67a6045-8e14-460a-9fe3-912b846c08c1
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_db_session_space_usage (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns the number of pages allocated and deallocated by each session for the database.  
  
> [!NOTE]  
>  This view is applicable only to the [tempdb database](../../relational-databases/databases/tempdb-database.md).  
  
> [!NOTE]  
>  To call this from [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the name **sys.dm_pdw_nodes_db_session_space_usage**.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**session_id**|**smallint**|Session ID.<br /><br /> **session_id** maps to **session_id** in [sys.dm_exec_sessions](../../relational-databases/system-dynamic-management-views/sys-dm-exec-sessions-transact-sql.md).|  
|**database_id**|**smallint**|Database ID.|  
|**user_objects_alloc_page_count**|**bigint**|Number of pages reserved or allocated for user objects by this session.|  
|**user_objects_dealloc_page_count**|**bigint**|Number of pages deallocated and no longer reserved for user objects by this session.|  
|**internal_objects_alloc_page_count**|**bigint**|Number of pages reserved or allocated for internal objects by this session.|  
|**internal_objects_dealloc_page_count**|**bigint**|Number of pages deallocated and no longer reserved for internal objects by this session.|  
|**user_objects_deferred_dealloc_page_count**|**bigint**|Number of pages which have been marked for deferred deallocation.<br /><br /> **Note:** Introduced in service packs for [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)].|  
|**pdw_node_id**|**int**|**Applies to**: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]<br /><br /> The identifier for the node that this distribution is on.|  
  
## Permissions  

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)], requires `VIEW SERVER STATE` permission.   
On [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)], requires the `VIEW DATABASE STATE` permission in the database.   

## Remarks  
 IAM pages are not included in any of the allocation or deallocation counts reported by this view.  
  
 Page counters are initialized to zero (0) at the start of a session. The counters track the total number of pages that have been allocated or deallocated for tasks that are already completed in the session. The counters are updated only when a task ends; they do not reflect running tasks.  
  
 A session can have multiple requests active at the same time. A request can start multiple threads, tasks, if it is a parallel query.  
  
 For more information about the sessions, requests, and tasks, see [sys.dm_exec_sessions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-sessions-transact-sql.md), [sys.dm_exec_requests &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md), and [sys.dm_os_tasks &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-tasks-transact-sql.md).  
  
## User Objects  
 The following objects are included in the user object page counters:  
  
-   User-defined tables and indexes  
  
-   System tables and indexes  
  
-   Global temporary tables and indexes  
  
-   Local temporary tables and indexes  
  
-   Table variables  
  
-   Tables returned in the table-valued functions  
  
## Internal Objects  
 Internal objects are only in **tempdb**. The following objects are included in the internal object page counters:  
  
-   Work tables for cursor or spool operations and temporary large object (LOB) storage  
  
-   Work files for operations such as a hash join  
  
-   Sort runs  
  
## Physical Joins  
 ![Physical joins for sys.dm_db_session_space_usage](../../relational-databases/system-dynamic-management-views/media/join-dm-db-session-space-usage-1.gif "Physical joins for sys.dm_db_session_space_usage")  
  
## Relationship Cardinalities  
  
|From|To|Relationship|  
|----------|--------|------------------|  
|dm_db_session_space_usage.session_id|dm_exec_sessions.session_id|One-to-one|  
  
## See Also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Database Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/database-related-dynamic-management-views-transact-sql.md)   
 [sys.dm_exec_sessions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-sessions-transact-sql.md)   
 [sys.dm_exec_requests &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md)   
 [sys.dm_os_tasks &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-tasks-transact-sql.md)   
 [sys.dm_db_task_space_usage &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-task-space-usage-transact-sql.md)   
 [sys.dm_db_file_space_usage &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-file-space-usage-transact-sql.md)  
  
  



