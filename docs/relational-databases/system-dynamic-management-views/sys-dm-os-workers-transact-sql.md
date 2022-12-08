---
title: "sys.dm_os_workers (Transact-SQL)"
description: sys.dm_os_workers (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/13/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom: event-tier1-build-2022
f1_keywords:
  - "dm_os_workers_TSQL"
  - "sys.dm_os_workers_TSQL"
  - "dm_os_workers"
  - "sys.dm_os_workers"
helpviewer_keywords:
  - "sys.dm_os_workers dynamic management view"
dev_langs:
  - "TSQL"
ms.assetid: 4d5d1e52-a574-4bdd-87ae-b932527235e8
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_os_workers (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns a row for every worker in the system. For more information about workers, see the [Thread and Task Architecture Guide](../../relational-databases/thread-and-task-architecture-guide.md). 
  
> [!NOTE]  
>  To call this from [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the name **sys.dm_pdw_nodes_os_workers**. [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|worker_address|**varbinary(8)**|Memory address of the worker.|  
|status|**int**|Internal use only.|  
|is_preemptive|**bit**|1 = Worker is running with preemptive scheduling. Any worker that is running external code is run under preemptive scheduling.|  
|is_fiber|**bit**|1 = Worker is running with lightweight pooling. For more information, see [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md).|  
|is_sick|**bit**|1 = Worker is stuck trying to obtain a spin lock. If this bit is set, this might indicate a problem with contention on a frequently accessed object.|  
|is_in_cc_exception|**bit**|1 = Worker is currently handling a non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] exception.|  
|is_fatal_exception|**bit**|Specifies whether this worker received a fatal exception.|  
|is_inside_catch|**bit**|1 = Worker is currently handling an exception.|  
|is_in_polling_io_completion_routine|**bit**|1 = Worker is currently running an I/O completion routine for a pending I/O. For more information, see [sys.dm_io_pending_io_requests &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-io-pending-io-requests-transact-sql.md).|  
|context_switch_count|**int**|Number of scheduler context switches that are performed by this worker.|  
|pending_io_count|**int**|Number of physical I/Os that are performed by this worker.|  
|pending_io_byte_count|**bigint**|Total number of bytes for all pending physical I/Os for this worker.|  
|pending_io_byte_average|**int**|Average number of bytes for physical I/Os for this worker.|  
|wait_started_ms_ticks|**bigint**|Point in time, in [ms_ticks](../../relational-databases/system-dynamic-management-views/sys-dm-os-sys-info-transact-sql.md), when this worker entered the SUSPENDED state. Subtracting this value from ms_ticks in [sys.dm_os_sys_info](../../relational-databases/system-dynamic-management-views/sys-dm-os-sys-info-transact-sql.md) returns the number of milliseconds that the worker has been waiting.|  
|wait_resumed_ms_ticks|**bigint**|Point in time, in [ms_ticks](../../relational-databases/system-dynamic-management-views/sys-dm-os-sys-info-transact-sql.md), when this worker entered the RUNNABLE state. Subtracting this value from ms_ticks in [sys.dm_os_sys_info](../../relational-databases/system-dynamic-management-views/sys-dm-os-sys-info-transact-sql.md) returns the number of milliseconds that the worker has been in the runnable queue.|  
|task_bound_ms_ticks|**bigint**|Point in time, in [ms_ticks](../../relational-databases/system-dynamic-management-views/sys-dm-os-sys-info-transact-sql.md), when a task is bound to this worker.|  
|worker_created_ms_ticks|**bigint**|Point in time, in [ms_ticks](../../relational-databases/system-dynamic-management-views/sys-dm-os-sys-info-transact-sql.md), when a worker is created.|  
|exception_num|**int**|Error number of the last exception that this worker encountered.|  
|exception_severity|**int**|Severity of the last exception that this worker encountered.|  
|exception_address|**varbinary(8)**|Code address that threw the exception|  
|affinity|**bigint**|The thread affinity of the worker. Matches the affinity of the thread in [sys.dm_os_threads &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-threads-transact-sql.md).|  
|state|**nvarchar(60)**|Worker state. Can be one of the following values:<br /><br /> INIT = Worker is currently being initialized.<br /><br /> RUNNING = Worker is currently running either nonpreemptively or preemptively.<br /><br /> RUNNABLE = The worker is ready to run on the scheduler.<br /><br /> SUSPENDED = The worker is currently suspended, waiting for an event to send it a signal.|  
|start_quantum|**bigint**|Time, in milliseconds, at the start of the current run of this worker.|  
|end_quantum|**bigint**|Time, in milliseconds, at the end of the current run of this worker.|  
|last_wait_type|**nvarchar(60)**|Type of last wait. For a list of wait types, see [sys.dm_os_wait_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-wait-stats-transact-sql.md).|  
|return_code|**int**|Return value from last wait. Can be one of the following values:<br /><br /> 0 =SUCCESS<br /><br /> 3 = DEADLOCK<br /><br /> 4 = PREMATURE_WAKEUP<br /><br /> 258 = TIMEOUT|  
|quantum_used|**bigint**|Internal use only.|  
|max_quantum|**bigint**|Internal use only.|  
|boost_count|**int**|Internal use only.|  
|tasks_processed_count|**int**|Number of tasks that this worker processed.|  
|fiber_address|**varbinary(8)**|Memory address of the fiber with which this worker is associated.<br /><br /> NULL = [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is not configured for lightweight pooling.|  
|task_address|**varbinary(8)**|Memory address of the current task. For more information, see [sys.dm_os_tasks &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-tasks-transact-sql.md).|  
|memory_object_address|**varbinary(8)**|Memory address of the worker memory object. For more information, see [sys.dm_os_memory_objects &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-memory-objects-transact-sql.md).|  
|thread_address|**varbinary(8)**|Memory address of the thread associated with this worker. For more information, see [sys.dm_os_threads &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-threads-transact-sql.md).|  
|signal_worker_address|**varbinary(8)**|Memory address of the worker that last signaled this object. For more information, see [sys.dm_os_workers](../../relational-databases/system-dynamic-management-views/sys-dm-os-workers-transact-sql.md).|  
|scheduler_address|**varbinary(8)**|Memory address of the scheduler. For more information, see [sys.dm_os_schedulers &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-schedulers-transact-sql.md).|  
|processor_group|**smallint**|Stores the processor group ID that is assigned to this thread.|  
|pdw_node_id|**int**|**Applies to**: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]<br /><br /> The identifier for the node that this distribution is on.|  
  
## Remarks  
 If the worker state is RUNNING and the worker is running nonpreemptively, the worker address matches the active_worker_address in sys.dm_os_schedulers.  
  
 When a worker that is waiting on an event is signaled, the worker is placed at the head of the runnable queue. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] allows for this to happen one thousand times in a row, after which the worker is placed at the end of the queue. Moving a worker to the end of the queue has some performance implications.  
  
## Permissions
On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)], requires `VIEW SERVER STATE` permission.   
On [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)] Premium Tiers, requires the `VIEW DATABASE STATE` permission in the database. On [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)] Standard and Basic Tiers, requires the  `Server Admin` role membership, or an `Azure Active Directory admin` account.   

## Examples  
 You can use the following query to find out how long a worker has been running in a SUSPENDED or RUNNABLE state.  
  
```sql
SELECT   
    t1.session_id,  
    CONVERT(varchar(10), t1.status) AS status,  
    CONVERT(varchar(15), t1.command) AS command,  
    CONVERT(varchar(10), t2.state) AS worker_state,  
    w_suspended =   
      CASE t2.wait_started_ms_ticks  
        WHEN 0 THEN 0  
        ELSE   
          t3.ms_ticks - t2.wait_started_ms_ticks  
      END,  
    w_runnable =   
      CASE t2.wait_resumed_ms_ticks  
        WHEN 0 THEN 0  
        ELSE   
          t3.ms_ticks - t2.wait_resumed_ms_ticks  
      END  
  FROM sys.dm_exec_requests AS t1  
  INNER JOIN sys.dm_os_workers AS t2  
    ON t2.task_address = t1.task_address  
  CROSS JOIN sys.dm_os_sys_info AS t3  
  WHERE t1.scheduler_id IS NOT NULL;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  

```
 session_id status     command         worker_state w_suspended w_runnable  
 ---------- ---------- --------------- ------------ ----------- --------------------  
 4          background LAZY WRITER     SUSPENDED    688         688  
 6          background LOCK MONITOR    SUSPENDED    4657        4657
 19         background BRKR TASK       SUSPENDED    603820344   603820344  
 14         background BRKR EVENT HNDL SUSPENDED    63583641    63583641  
 51         running    SELECT          RUNNING      0           0  
 2          background RESOURCE MONITO RUNNING      0           603825954  
 3          background LAZY WRITER     SUSPENDED    422         422  
 7          background SIGNAL HANDLER  SUSPENDED    603820485   603820485  
 13         background TASK MANAGER    SUSPENDED    603824704   603824704  
 18         background BRKR TASK       SUSPENDED    603820407   603820407  
 9          background TRACE QUEUE TAS SUSPENDED    454         454  
 52         suspended  SELECT          SUSPENDED    35094       35094  
 1          background RESOURCE MONITO RUNNING      0           603825954  
```

 In the output, when `w_runnable` and `w_suspended` are equal, this represents the time that the worker is in the SUSPENDED state. Otherwise, `w_runnable` represents the time that is spent by the worker in the RUNNABLE state. In the output, session `52` is `SUSPENDED` for `35,094` milliseconds.  
  
## See Also  
[SQL Server Operating System Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-server-operating-system-related-dynamic-management-views-transact-sql.md)       
[Query Processing Architecture Guide](../../relational-databases/query-processing-architecture-guide.md#degree-of-parallelism-dop)       
[Thread and Task Architecture Guide](../../relational-databases/thread-and-task-architecture-guide.md)    
