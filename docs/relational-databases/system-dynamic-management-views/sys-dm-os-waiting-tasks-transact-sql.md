---
title: "sys.dm_os_waiting_tasks (Transact-SQL)"
description: sys.dm_os_waiting_tasks (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/13/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_os_waiting_tasks"
  - "sys.dm_os_waiting_tasks_TSQL"
  - "dm_os_waiting_tasks_TSQL"
  - "sys.dm_os_waiting_tasks"
helpviewer_keywords:
  - "sys.dm_os_waiting_tasks dynamic management view"
dev_langs:
  - "TSQL"
ms.assetid: ca5e6844-368c-42e2-b187-6e5f5afc8df3
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_os_waiting_tasks (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns information about the wait queue of tasks that are waiting on some resource. For more information about tasks, see the [Thread and Task Architecture Guide](../../relational-databases/thread-and-task-architecture-guide.md).
   
> [!NOTE]  
> To call this from [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the name **sys.dm_pdw_nodes_os_waiting_tasks**. [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**waiting_task_address**|**varbinary(8)**|Address of the waiting task.|  
|**session_id**|**smallint**|ID of the session associated with the task.|  
|**exec_context_id**|**int**|ID of the execution context associated with the task.|  
|**wait_duration_ms**|**bigint**|Total wait time for this wait type, in milliseconds. This time is inclusive of **signal_wait_time**.|  
|**wait_type**|**nvarchar(60)**|Name of the wait type.|  
|**resource_address**|**varbinary(8)**|Address of the resource for which the task is waiting.|  
|**blocking_task_address**|**varbinary(8)**|Task that is currently holding this resource|  
|**blocking_session_id**|**smallint**|ID of the session that is blocking the request. If this column is NULL, the request is not blocked, or the session information of the blocking session is not available (or cannot be identified).<br /><br /> -2 = The blocking resource is owned by an orphaned distributed transaction.<br /><br /> -3 = The blocking resource is owned by a deferred recovery transaction.<br /><br /> -4 = Session ID of the blocking latch owner could not be determined due to internal latch state transitions.|  
|**blocking_exec_context_id**|**int**|ID of the execution context of the blocking task.|  
|**resource_description**|**nvarchar(3072)**|Description of the resource that is being consumed. For more information, see the list below.|  
|**pdw_node_id**|**int**|**Applies to**: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]<br /><br /> The identifier for the node that this distribution is on.|  
  
## resource_description column  
 The resource_description column has the following possible values.  
  
 **Thread-pool resource owner:**  
  
-   threadpool id=scheduler\<hex-address>  
  
 **Parallel query resource owner:**  
  
-   exchangeEvent id={Port|Pipe}\<hex-address> WaitType=\<exchange-wait-type> nodeId=\<exchange-node-id>  
  
 **Exchange-wait-type:**  
  
-   e_waitNone  
  
-   e_waitPipeNewRow  
  
-   e_waitPipeGetRow  
  
-   e_waitSynchronizeConsumerOpen  
  
-   e_waitPortOpen  
  
-   e_waitPortClose  
  
-   e_waitRange  
  
 **Lock resource owner:**  
  
-   \<type-specific-description> id=lock\<lock-hex-address> mode=\<mode> associatedObjectId=\<associated-obj-id>  
  
     **\<type-specific-description> can be:**  
  
    -   For DATABASE: databaselock subresource=\<databaselock-subresource> dbid=\<db-id>  
  
    -   For FILE: filelock fileid=\<file-id> subresource=\<filelock-subresource> dbid=\<db-id>  
  
    -   For OBJECT: objectlock lockPartition=\<lock-partition-id> objid=\<obj-id> subresource=\<objectlock-subresource> dbid=\<db-id>  
  
    -   For PAGE: pagelock fileid=\<file-id> pageid=\<page-id> dbid=\<db-id> subresource=\<pagelock-subresource>  
  
    -   For Key: keylock  hobtid=\<hobt-id> dbid=\<db-id>  
  
    -   For EXTENT: extentlock fileid=\<file-id> pageid=\<page-id> dbid=\<db-id>  
  
    -   For RID: ridlock fileid=\<file-id> pageid=\<page-id> dbid=\<db-id>  
  
    -   For APPLICATION: applicationlock hash=\<hash> databasePrincipalId=\<role-id> dbid=\<db-id>  
  
    -   For METADATA: metadatalock subresource=\<metadata-subresource> classid=\<metadatalock-description> dbid=\<db-id>  
  
    -   For HOBT: hobtlock hobtid=\<hobt-id> subresource=\<hobt-subresource> dbid=\<db-id>  
  
    -   For ALLOCATION_UNIT: allocunitlock hobtid=\<hobt-id> subresource=\<alloc-unit-subresource> dbid=\<db-id>  
  
     **\<mode> can be:**  
  
     Sch-S, Sch-M, S, U, X, IS, IU, IX, SIU, SIX, UIX, BU, RangeS-S, RangeS-U, RangeI-N, RangeI-S, RangeI-U, RangeI-X, RangeX-, RangeX-U, RangeX-X  
  
 **External resource owner:**  
  
-   External ExternalResource=\<wait-type>  
  
 **Generic resource owner:**  
  
-   TransactionMutex TransactionInfo Workspace=\<workspace-id>  
  
-   Mutex  
  
-   CLRTaskJoin  
  
-   CLRMonitorEvent  
  
-   CLRRWLockEvent  
  
-   resourceWait  
  
 **Latch resource owner:**  
  
-   \<db-id>:\<file-id>:\<page-in-file>  
  
-   \<GUID>  
  
-   \<latch-class> (\<latch-address>)  
  
## Permissions

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] and SQL Managed Instance, requires `VIEW SERVER STATE` permission.

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Azure Active Directory admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.   
 
## Example
### A. Identify tasks from blocked sessions. 

```sql
SELECT * FROM sys.dm_os_waiting_tasks 
WHERE blocking_session_id IS NOT NULL; 
```   

### B. View waiting tasks per connection

```sql
SELECT st.text AS [SQL Text], c.connection_id, w.session_id, 
  w.wait_duration_ms, w.wait_type, w.resource_address, 
  w.blocking_session_id, w.resource_description, c.client_net_address, c.connect_time
FROM sys.dm_os_waiting_tasks AS w
INNER JOIN sys.dm_exec_connections AS c ON w.session_id = c.session_id 
CROSS APPLY (SELECT * FROM sys.dm_exec_sql_text(c.most_recent_sql_handle)) AS st 
              WHERE w.session_id > 50 AND w.wait_duration_ms > 0
ORDER BY c.connection_id, w.session_id
GO
```

### C. View waiting tasks for all user processes with additional information

```sql
SELECT 'Waiting_tasks' AS [Information], owt.session_id,
	owt.wait_duration_ms, owt.wait_type, owt.blocking_session_id,
	owt.resource_description, es.program_name, est.text,
	est.dbid, eqp.query_plan, er.database_id, es.cpu_time,
	es.memory_usage*8 AS memory_usage_KB
FROM sys.dm_os_waiting_tasks owt
INNER JOIN sys.dm_exec_sessions es ON owt.session_id = es.session_id
INNER JOIN sys.dm_exec_requests er ON es.session_id = er.session_id
OUTER APPLY sys.dm_exec_sql_text (er.sql_handle) est
OUTER APPLY sys.dm_exec_query_plan (er.plan_handle) eqp
WHERE es.is_user_process = 1
ORDER BY owt.session_id;
GO
```
  
## See Also  
[SQL Server Operating System Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-server-operating-system-related-dynamic-management-views-transact-sql.md)      
[Thread and Task Architecture Guide](../../relational-databases/thread-and-task-architecture-guide.md)     
   
