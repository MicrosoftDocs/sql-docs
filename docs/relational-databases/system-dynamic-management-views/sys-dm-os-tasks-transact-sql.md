---
title: "sys.dm_os_tasks (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/13/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_os_tasks"
  - "sys.dm_os_tasks_TSQL"
  - "dm_os_tasks_TSQL"
  - "dm_os_tasks"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_os_tasks dynamic management view"
ms.assetid: 180a3c41-e71b-4670-819d-85ea7ef98bac
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_os_tasks (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns one row for each task that is active in the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
> [!NOTE]  
>  To call this from [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the name **sys.dm_pdw_nodes_os_tasks**.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**task_address**|**varbinary(8)**|Memory address of the object.|  
|**task_state**|**nvarchar(60)**|State of the task. This can be one of the following:<br /><br /> PENDING: Waiting for a worker thread.<br /><br /> RUNNABLE: Runnable, but waiting to receive a quantum.<br /><br /> RUNNING: Currently running on the scheduler.<br /><br /> SUSPENDED: Has a worker, but is waiting for an event.<br /><br /> DONE: Completed.<br /><br /> SPINLOOP: Stuck in a spinlock.|  
|**context_switches_count**|**int**|Number of scheduler context switches that this task has completed.|  
|**pending_io_count**|**int**|Number of physical I/Os that are performed by this task.|  
|**pending_io_byte_count**|**bigint**|Total byte count of I/Os that are performed by this task.|  
|**pending_io_byte_average**|**int**|Average byte count of I/Os that are performed by this task.|  
|**scheduler_id**|**int**|ID of the parent scheduler. This is a handle to the scheduler information for this task. For more information, see [sys.dm_os_schedulers &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-schedulers-transact-sql.md).|  
|**session_id**|**smallint**|ID of the session that is associated with the task.|  
|**exec_context_id**|**int**|Execution context ID that is associated with the task.|  
|**request_id**|**int**|ID of the request of the task. For more information, see [sys.dm_exec_requests &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md).|  
|**worker_address**|**varbinary(8)**|Memory address of the worker that is running the task.<br /><br /> NULL = Task is either waiting for a worker to be able to run, or the task has just finished running.<br /><br /> For more information, see [sys.dm_os_workers &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-workers-transact-sql.md).|  
|**host_address**|**varbinary(8)**|Memory address of the host.<br /><br /> 0 = Hosting was not used to create the task. This helps identify the host that was used to create this task.<br /><br /> For more information, see [sys.dm_os_hosts &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-hosts-transact-sql.md).|  
|**parent_task_address**|**varbinary(8)**|Memory address of the task that is the parent of the object.|  
|**pdw_node_id**|**int**|**Applies to**: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]<br /><br /> The identifier for the node that this distribution is on.|  
  
## Permissions

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)], requires `VIEW SERVER STATE` permission.   
On [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)], requires the `VIEW DATABASE STATE` permission in the database.   

## Examples  
  
### A. Monitoring parallel requests  
 For requests that are executed in parallel, you will see multiple rows for the same combination of (\<**session_id**>, \<**request_id**>). Use the following query to find the [Configure the max degree of parallelism Server Configuration Option](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md) for all active requests.  
  
> [!NOTE]  
>  A **request_id** is unique within a session.  
  
```  
SELECT  
    task_address,  
    task_state,  
    context_switches_count,  
    pending_io_count,  
    pending_io_byte_count,  
    pending_io_byte_average,  
    scheduler_id,  
    session_id,  
    exec_context_id,  
    request_id,  
    worker_address,  
    host_address  
  FROM sys.dm_os_tasks  
  ORDER BY session_id, request_id;  
```  
  
### B. Associating session IDs with Windows threads  
 You can use the following query to associate a session ID value with a Windows thread ID. You can then monitor the performance of the thread in the Windows Performance Monitor. The following query does not return information for sessions that are sleeping.  
  
```  
SELECT STasks.session_id, SThreads.os_thread_id  
  FROM sys.dm_os_tasks AS STasks  
  INNER JOIN sys.dm_os_threads AS SThreads  
    ON STasks.worker_address = SThreads.worker_address  
  WHERE STasks.session_id IS NOT NULL  
  ORDER BY STasks.session_id;  
GO  
```  
  
## See Also  
  [SQL Server Operating System Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-server-operating-system-related-dynamic-management-views-transact-sql.md)  
  
  


