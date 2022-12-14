---
title: "sys.dm_os_schedulers (Transact-SQL)"
description: sys.dm_os_schedulers (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/13/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_os_schedulers"
  - "sys.dm_os_schedulers_TSQL"
  - "sys.dm_os_schedulers"
  - "dm_os_schedulers_TSQL"
helpviewer_keywords:
  - "sys.dm_os_schedulers dynamic management view"
dev_langs:
  - "TSQL"
ms.assetid: 3a09d81b-55d5-416f-9cda-1a3a5492abe0
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_os_schedulers (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns one row per scheduler in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] where each scheduler is mapped to an individual processor. Use this view to monitor the condition of a scheduler or to identify runaway tasks. For more information about schedulers, see the [Thread and Task Architecture Guide](../../relational-databases/thread-and-task-architecture-guide.md).  
  
> [!NOTE]  
>  To call this from [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the name **sys.dm_pdw_nodes_os_schedulers**. [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)] 
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|scheduler_address|**varbinary(8)**|Memory address of the scheduler. Is not nullable.|  
|parent_node_id|**int**|ID of the node that the scheduler belongs to, also known as the parent node. This represents a nonuniform memory access (NUMA) node. Is not nullable.|  
|scheduler_id|**int**|ID of the scheduler. All schedulers that are used to run regular queries have ID numbers less than 1048576. Those schedulers that have IDs greater than or equal to 1048576 are used internally by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], such as the dedicated administrator connection scheduler. Is not nullable.|  
|cpu_id|**smallint**|CPU ID assigned to the scheduler.<br /><br /> Is not nullable.<br /><br /> **Note:** 255 does not indicate no affinity as it did in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]. See [sys.dm_os_threads &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-threads-transact-sql.md) for additional affinity information.|  
|status|**nvarchar(60)**|Indicates the status of the scheduler. Can be one of the following values:<br /><br /> -   HIDDEN ONLINE<br />-   HIDDEN OFFLINE<br />-   VISIBLE ONLINE<br />-   VISIBLE OFFLINE<br />-   VISIBLE ONLINE (DAC)<br />-   HOT_ADDED<br /><br /> Is not nullable.<br /><br /> HIDDEN schedulers are used to process requests that are internal to the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. VISIBLE schedulers are used to process user requests.<br /><br /> OFFLINE schedulers map to processors that are offline in the affinity mask and are, therefore, not being used to process any requests. ONLINE schedulers map to processors that are online in the affinity mask and are available to process threads.<br /><br /> DAC indicates the scheduler is running under a dedicated administrator connection.<br /><br /> HOT ADDED indicates the schedulers were added in response to a hot add CPU event.|  
|is_online|**bit**|If [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is configured to use only some of the available processors on the server, this configuration can mean that some schedulers are mapped to processors that are not in the affinity mask. If that is the case, this column returns 0. This value means that the scheduler is not being used to process queries or batches.<br /><br /> Is not nullable.|  
|is_idle|**bit**|1 = Scheduler is idle. No workers are currently running. Is not nullable.|  
|preemptive_switches_count|**int**|Number of times that workers on this scheduler have switched to the preemptive mode.<br /><br /> To execute code that is outside [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (for example, extended stored procedures and distributed queries), a thread has to execute outside the control of the non-preemptive scheduler. To do this, a worker switches to preemptive mode.|  
|context_switches_count|**int**|Number of context switches that have occurred on this scheduler. Is not nullable.<br /><br /> To allow for other workers to run, the current running worker has to relinquish control of the scheduler or switch context.<br /><br /> **Note:** If a worker yields the scheduler and puts itself into the runnable queue and then finds no other workers, the worker will select itself. In this case, the context_switches_count is not updated, but the yield_count is updated.|  
|idle_switches_count|**int**|Number of times the scheduler has been waiting for an event while idle. This column is similar to context_switches_count. Is not nullable.|  
|current_tasks_count|**int**|Number of current tasks that are associated with this scheduler. This count includes the following:<br /><br /> -   Tasks that are waiting for a worker to execute them.<br />-   Tasks that are currently waiting or running (in SUSPENDED or RUNNABLE state).<br /><br /> When a task is completed, this count is decremented. Is not nullable.|  
|runnable_tasks_count|**int**|Number of workers, with tasks assigned to them, that are waiting to be scheduled on the runnable queue. Is not nullable.|  
|current_workers_count|**int**|Number of workers that are associated with this scheduler. This count includes workers that are not assigned any task. Is not nullable.|  
|active_workers_count|**int**|Number of workers that are active. An active worker is never preemptive, must have an associated task, and is either running, runnable, or suspended. Is not nullable.|  
|work_queue_count|**bigint**|Number of tasks in the pending queue. These tasks are waiting for a worker to pick them up. Is not nullable.|  
|pending_disk_io_count|**int**|Number of pending I/Os that are waiting to be completed. Each scheduler has a list of pending I/Os that are checked to determine whether they have been completed every time there is a context switch. The count is incremented when the request is inserted. This count is decremented when the request is completed. This number does not indicate the state of the I/Os. Is not nullable.|  
|load_factor|**int**|Internal value that indicates the perceived load on this scheduler. This value is used to determine whether a new task should be put on this scheduler or another scheduler. This value is useful for debugging purposes when it appears that schedulers are not evenly loaded. The routing decision is made based on the load on the scheduler. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] also uses a load factor of nodes and schedulers to help determine the best location to acquire resources. When a task is enqueued, the load factor is increased. When a task is completed, the load factor is decreased. Using the load factors helps [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] OS balance the work load better. Is not nullable.|  
|yield_count|**int**|Internal value that is used to indicate progress on this scheduler. This value is used by the Scheduler Monitor to determine whether a worker on the scheduler is not yielding to other workers on time. This value does not indicate that the worker or task transitioned to a new worker. Is not nullable.|  
|last_timer_activity|**bigint**|In CPU ticks, the last time that the scheduler timer queue was checked by the scheduler. Is not nullable.|  
|failed_to_create_worker|**bit**|Set to 1 if a new worker could not be created on this scheduler. This generally occurs because of memory constraints. Is nullable.|  
|active_worker_address|**varbinary(8)**|Memory address of the worker that is currently active. Is nullable. For more information, see [sys.dm_os_workers &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-workers-transact-sql.md).|  
|memory_object_address|**varbinary(8)**|Memory address of the scheduler memory object. Not NULLABLE.|  
|task_memory_object_address|**varbinary(8)**|Memory address of the task memory object. Is not nullable. For more information, see [sys.dm_os_memory_objects &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-memory-objects-transact-sql.md).|  
|quantum_length_us|**bigint**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)] Exposes the scheduler quantum used by SQLOS.|  
| total_cpu_usage_ms |**bigint**|**Applies to**: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later <br><br> Total CPU consumed by this scheduler as reported by non-preemptive workers. Is not nullable.|
|total_cpu_idle_capped_ms|**bigint**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)] Indicates throttling based on [Service Level Objective](/azure/sql-data-warehouse/what-is-a-data-warehouse-unit-dwu-cdwu#service-level-objective), will always be 0 for non-Azure versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Is nullable.|
|total_scheduler_delay_ms|**bigint**|**Applies to**: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later <br><br> The time between one worker switching out and another one switching in. Can be caused by preemptive workers delaying the scheduling of the next non-preemptive worker, or due to the OS scheduling threads from other processes. Is not nullable.|
|ideal_workers_limit|**int**|**Applies to**: [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] and later <br><br> How many workers should ideally be on the scheduler. If the current workers exceed the limit due to imbalanced task load, once they become idle they will be trimmed. Is not nullable.|
|pdw_node_id|**int**|**Applies to**: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]<br /><br /> The identifier for the node that this distribution is on.|  
  
## Permissions
On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] and SQL Managed Instance, requires `VIEW SERVER STATE` permission.

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Azure Active Directory admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.   

## Examples  
  
### A. Monitoring hidden and nonhidden schedulers  
 The following query outputs the state of workers and tasks in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] across all schedulers. This query was executed on a computer system that has the following:  
  
-   Two processors (CPUs)  
  
-   Two (NUMA) nodes  
  
-   One CPU per NUMA node  
  
-   Affinity mask set to `0x03`.  
  
```sql  
SELECT  
    scheduler_id,  
    cpu_id,  
    parent_node_id,  
    current_tasks_count,  
    runnable_tasks_count,  
    current_workers_count,  
    active_workers_count,  
    work_queue_count  
  FROM sys.dm_os_schedulers;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
scheduler_id cpu_id parent_node_id current_tasks_count  
------------ ------ -------------- -------------------  
0            1      0              9                    
257          255    0              1                    
1            0      1              10                   
258          255    1              1                    
255          255    32             2                    
  
runnable_tasks_count current_workers_count  
-------------------- ---------------------  
0                    11                     
0                    1                      
0                    18                     
0                    1                      
0                    3                      
  
active_workers_count work_queue_count  
-------------------- --------------------  
6                    0  
1                    0  
8                    0  
1                    0  
1                    0  
```  
  
 The output provides the following information:  
  
-   There are five schedulers. Two schedulers have an ID value < 1048576. Schedulers with ID >= 1048576 are known as hidden schedulers. Scheduler `255` represents the dedicated administrator connection (DAC). There is one DAC scheduler per instance. Resource monitors that coordinate memory pressure use scheduler `257` and scheduler `258`, one per NUMA node  
  
-   There are 23 active tasks in the output. These tasks include user requests in addition to resource management tasks that have been started by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Examples of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tasks are RESOURCE MONITOR (one per NUMA node), LAZY WRITER (one per NUMA node), LOCK MONITOR, CHECKPOINT, and LOG WRITER.  
  
-   NUMA node `0` is mapped to CPU `1` and NUMA node `1` is mapped to CPU `0`. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] typically starts on a NUMA node other than node 0.  
  
-   With `runnable_tasks_count` returning `0`, there are no actively running tasks. However, active sessions may exist.  
  
-   Scheduler `255` representing DAC has `3` workers associated with it. These workers are allocated at [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] startup and do not change. These workers are used to process DAC queries only. The two tasks on this scheduler represent a connection manager and an idle worker.  
  
-   `active_workers_count` represents all workers that have associated tasks and are running under non-preemptive mode. Some tasks, such as network listeners, run under preemptive scheduling.  
  
-   Hidden schedulers do not process typical user requests. The DAC scheduler is the exception. This DAC scheduler has one thread to process requests.  
  
### B. Monitoring nonhidden schedulers in a busy system  
 The following query shows the state of heavily loaded nonhidden schedulers, where more requests exist than can be handled by available workers. In this example, 256 workers are assigned tasks. Some tasks are waiting for an assignment to a worker. Lower runnable count implies that multiple tasks are waiting for a resource.  
  
> [!NOTE]  
>  You can find the state of workers by querying sys.dm_os_workers. For more information, see [sys.dm_os_workers &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-workers-transact-sql.md).  
  
 Here is the query:  
  
```sql  
SELECT  
    scheduler_id,  
    cpu_id,  
    current_tasks_count,  
    runnable_tasks_count,  
    current_workers_count,  
    active_workers_count,  
    work_queue_count  
  FROM sys.dm_os_schedulers  
  WHERE scheduler_id < 255;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
scheduler_id current_tasks_count runnable_tasks_count  
------------ ------------------- --------------------  
0            144                 0                     
1            147                 1                     
  
current_workers_count active_workers_count work_queue_count  
--------------------- -------------------- --------------------  
128                   125                  16  
128                   126                  19  
```  
  
 By comparison, the following result shows multiple runnable tasks where no task is waiting to obtain a worker. The `work_queue_count` is `0` for both schedulers.  
  
```  
scheduler_id current_tasks_count runnable_tasks_count  
------------ ------------------- --------------------  
0            107                 98                    
1            110                 100                   
  
current_workers_count active_workers_count work_queue_count  
--------------------- -------------------- --------------------  
128                   104                  0  
128                   108                  0  
```  
  
## See Also  
 [SQL Server Operating System Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-server-operating-system-related-dynamic-management-views-transact-sql.md)
