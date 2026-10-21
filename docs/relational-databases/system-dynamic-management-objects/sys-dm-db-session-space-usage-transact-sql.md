---
title: "sys.dm_db_session_space_usage (Transact-SQL)"
description: sys.dm_db_session_space_usage (Transact-SQL) returns the number of pages allocated and deallocated by each session for the tempdb system database
author: dimitri-furman
ms.author: dfurman
ms.date: "10/21/2026"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "dm_db_session_space_usage_TSQL"
  - "dm_db_session_space_usage"
  - "sys.dm_db_session_space_usage"
  - "sys.dm_db_session_space_usage_TSQL"
helpviewer_keywords:
  - "sys.dm_db_session_space_usage dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---

# sys.dm_db_session_space_usage (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-fabricsqldb.md)]

Returns the number of pages allocated and deallocated by each session in the `tempdb` database during the session's lifetime.
 
> [!NOTE]  
> To obtain this data in [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)], use the name `sys.dm_pdw_nodes_db_session_space_usage`. [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]  

| Column name | Data type | Description |
| --- | --- | --- |
| `session_id` | **smallint** | Session ID.<br /><br /> **session_id** maps to **session_id** in [sys.dm_exec_sessions](sys-dm-exec-sessions-transact-sql.md). |
| `database_id` | **smallint** | Database ID. <br /><br />In [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], the values are unique within a single database or an elastic pool, but not within a logical server. |
| `user_objects_alloc_page_count` | **bigint** | Number of pages reserved or allocated for user objects by this session during its lifetime. |
| `user_objects_dealloc_page_count` | **bigint** | Number of pages deallocated and no longer reserved for user objects by this session during its lifetime. |
| `internal_objects_alloc_page_count` | **bigint** | Number of pages reserved or allocated for internal objects by this session during its lifetime. |
| `internal_objects_dealloc_page_count` | **bigint** | Number of pages deallocated and no longer reserved for internal objects by this session during its lifetime. |
| `user_objects_deferred_dealloc_page_count` | **bigint** | Number of pages queued for deferred deallocation by this session during its lifetime. |
| `pdw_node_id` | **int** | **Applies to**: [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)]<br /><br /> The identifier for the node that this distribution is on. |
  
## Permissions  

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] and SQL Managed Instance, requires `VIEW SERVER STATE` permission.

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Microsoft Entra admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.   

### Permissions for SQL Server 2022 and later

Requires VIEW SERVER PERFORMANCE STATE permission on the server.

## Remarks  

IAM pages aren't included in any of the allocation or deallocation counts that this view reports.  
  
Page counters initialize to zero at the start of a session and increment during the session lifetime. The counters track the total number of pages that are allocated or deallocated for tasks that are already completed in the session. The counters update only when a task ends. They don't reflect running tasks.  

When deferred deallocation is used, the `user_objects_deferred_dealloc_page_count` column reports the pages queued for a later deallocation by a background process. When the queue is long, the actual deallocation can be delayed. As a consequence, the user session `tempdb` space usage reported by the `user_objects_alloc_page_count - user_objects_dealloc_page_count - user_objects_deferred_dealloc_page_count` calculation might be temporarily lower than the actual usage.

A session can have multiple requests active at the same time. A request can start multiple threads or tasks if it's a parallel query.  
  
For more information about the sessions, requests, and tasks, see [sys.dm_exec_sessions (Transact-SQL)](sys-dm-exec-sessions-transact-sql.md), [sys.dm_exec_requests (Transact-SQL)](sys-dm-exec-requests-transact-sql.md), and [sys.dm_os_tasks (Transact-SQL)](sys-dm-os-tasks-transact-sql.md).  
  
## User objects  

The user object page counters include the following objects:  
  
-   User-defined tables and indexes  
  
-   System tables and indexes  
  
-   Global temporary tables and indexes  
  
-   Local temporary tables and indexes  
  
-   Table variables  
  
-   Tables returned in the table-valued functions  
  
## Internal objects  

The internal object page counters include the following objects:  
  
-   Work tables for cursor or spool operations and temporary large object (LOB) storage  
  
-   Work files for operations such as a hash join  
  
-   Sort runs  
  
## Physical joins  

:::image type="content" source="../../relational-databases/system-dynamic-management-objects/media/join-dm-db-session-space-usage.svg" alt-text="Diagram of physical joins for sys.dm_db_session_space_usage.":::

## Relationship cardinalities  
  
|From|To|Relationship|  
|----------|--------|------------------|  
|`dm_db_session_space_usage`.`session_id`|`dm_exec_sessions`.`session_id`|One-to-one|  
  
## Related content

- [System dynamic management views and functions](system-dynamic-management-objects.md)
- [Database related dynamic management views (Transact-SQL)](database-related-dynamic-management-views-transact-sql.md)
- [sys.dm_exec_sessions (Transact-SQL)](sys-dm-exec-sessions-transact-sql.md)
- [sys.dm_exec_requests (Transact-SQL)](sys-dm-exec-requests-transact-sql.md)
- [sys.dm_os_tasks (Transact-SQL)](sys-dm-os-tasks-transact-sql.md)
- [sys.dm_db_task_space_usage (Transact-SQL)](sys-dm-db-task-space-usage-transact-sql.md)
- [sys.dm_db_file_space_usage (Transact-SQL)](sys-dm-db-file-space-usage-transact-sql.md)
