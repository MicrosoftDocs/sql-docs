---
title: "sys.dm_os_schedulers (Transact-SQL)"
description: Monitor scheduler status, worker availability, runnable tasks, and queued tasks with the sys.dm_os_schedulers dynamic management view.
author: dimitri-furman
ms.author: dfurman
ms.date: 11/12/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ai-usage: ai-assisted
ms.custom:
  - ignite-2025
f1_keywords:
  - "dm_os_schedulers"
  - "sys.dm_os_schedulers_TSQL"
  - "sys.dm_os_schedulers"
  - "dm_os_schedulers_TSQL"
helpviewer_keywords:
  - "sys.dm_os_schedulers dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---

# sys.dm_os_schedulers (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-fabricsqldb.md)]

Returns one row per SQLOS scheduler. The view includes schedulers that process user queries, the dedicated administrator connection (DAC) scheduler when present, and hidden schedulers that process internal tasks. Use this view to monitor scheduler status, worker availability, and task queues. For more information, see the [Thread and task architecture guide](../thread-and-task-architecture-guide.md).
  
> [!NOTE]  
>  To call this from [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)], use the name **sys.dm_pdw_nodes_os_schedulers**. [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)] 
  
| Column name | Data type | Description |
| --- | --- | --- |
| `scheduler_address` | **varbinary(8)** | Memory address of the scheduler. Is not nullable. |
| `parent_node_id` | **int** | ID of the SQLOS node that the scheduler belongs to. The DAC scheduler belongs to node `64`. For information about SQLOS nodes, processor locality, and soft-NUMA, see [sys.dm_os_nodes](sys-dm-os-nodes-transact-sql.md). Is not nullable. |
| `scheduler_id` | **int** | ID of the scheduler. Schedulers with IDs less than `1048576` are used to process user queries when online. The DAC scheduler has ID `1048576`. Hidden scheduler IDs start at `1048578`; these schedulers process internal Database Engine tasks. Is not nullable. |
| `cpu_id` | **int** | ID of the logical processor assigned to the scheduler. Is not nullable. |
| `status` | **nvarchar(60)** | Scheduler status, composed of a visibility value (`HIDDEN` or `VISIBLE`) and an availability value (`ONLINE` or `OFFLINE`). For example, `VISIBLE ONLINE` or `HIDDEN ONLINE`.<br /><br />`HIDDEN` schedulers process internal Database Engine tasks. `VISIBLE` schedulers process user requests. The DAC scheduler is reserved for DAC requests and reports `VISIBLE ONLINE (DAC)`.<br /><br />`ONLINE` indicates that the scheduler is available to process work. `OFFLINE` indicates that it isn't available. [Processor affinity configuration](../../t-sql/statements/alter-server-configuration-transact-sql.md#arguments) is one of the factors that determine which schedulers are online. Is not nullable. |
| `is_online` | **bit** | `1` = The scheduler is online and available to process work; `0` = The scheduler is offline. Is not nullable. |
| `is_idle` | **bit** | `1` = The scheduler is idle, waiting for work to execute; otherwise, `0`. Is not nullable. |
| `preemptive_switches_count` | **int** | Number of times that workers on this scheduler have switched to preemptive mode.<br /><br />To execute code outside the control of a scheduler, such as an extended stored procedure or a distributed query, a worker switches to preemptive mode. Is not nullable. |
| `context_switches_count` | **int** | Number of context switches performed by the scheduler. A worker relinquishes control of the scheduler to allow another worker to run. Is not nullable. |
| `idle_switches_count` | **int** | Number of times the scheduler entered an idle wait because no worker was ready to run. Is not nullable. |
| `current_tasks_count` | **int** | Number of tasks associated with this scheduler. Includes tasks waiting for a worker and tasks with an assigned worker that are running, runnable, or suspended. The count decreases when a task completes. Is not nullable. |
| `runnable_tasks_count` | **int** | Number of workers, with tasks assigned to them, that are waiting to be scheduled on the runnable queue. Is not nullable. |
| `current_workers_count` | **int** | Number of workers that are associated with this scheduler. This count includes workers that aren't assigned any task. Is not nullable. |
| `active_workers_count` | **int** | Number of nonpreemptive workers with assigned tasks. These workers can be running, runnable, or suspended. Is not nullable. |
| `work_queue_count` | **bigint** | Number of tasks in the pending queue. These tasks are waiting for a worker to pick them up. Is not nullable. |
| `pending_disk_io_count` | **int** | Number of I/O requests in the scheduler's pending disk I/O queue. The scheduler checks pending requests for completion during context switches. Is not nullable. |
| `queued_disk_io_count` | **int** | **Applies to**: [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions.<br /><br />Number of I/O requests waiting in the scheduler's queue to be issued to the storage subsystem. Resource governor controls issuance according to applicable I/O limits, such as a resource pool `MAX_IOPS_PER_VOLUME` configuration. Is not nullable. |
| `load_factor` | **int** | Internal value that indicates the perceived load on this scheduler. This value is used to determine whether a new task should be put on this scheduler or another scheduler. This value is useful for debugging purposes when it appears that schedulers aren't evenly loaded. The routing decision is based on the load on the scheduler. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] also uses a load factor of nodes and schedulers to help determine the best location to acquire resources. When a task is enqueued, the load factor increases. When a task is completed, the load factor decreases. Using the load factors helps [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] OS balance the workload better. Is not nullable. |
| `yield_count` | **int** | Number of yields on this scheduler. The scheduler monitor uses this counter to track scheduler progress and detect nonyielding workers. The count increases when a worker yields, including when the same worker resumes execution. Is not nullable. |
| `last_timer_activity` | **bigint** | Relative timestamp of the most recent scheduler timer activity, in milliseconds, using the same clock as `ms_ticks` in [sys.dm_os_sys_info](sys-dm-os-sys-info-transact-sql.md). Is not nullable. |
| `failed_to_create_worker` | **bit** | `1` = An attempt to obtain a new worker failed, for example because of memory pressure or a worker limit. The flag is cleared when a subsequent attempt succeeds. Is not nullable. |
| `active_worker_address` | **varbinary(8)** | Memory address of the scheduler's current worker. Is nullable. For more information, see [sys.dm_os_workers](sys-dm-os-workers-transact-sql.md). |
| `memory_object_address` | **varbinary(8)** | Memory address of the scheduler memory object. Is not nullable. |
| `task_memory_object_address` | **varbinary(8)** | Memory address of the task memory object. Is not nullable. For more information, see [sys.dm_os_memory_objects &#40;Transact-SQL&#41;](sys-dm-os-memory-objects-transact-sql.md). |
| `quantum_length_us` | **bigint** | [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]<br />Exposes the scheduler quantum used by SQLOS. Is not nullable. |
| `total_cpu_usage_ms` | **bigint** | Total CPU time, in milliseconds, consumed by this scheduler as reported by nonpreemptive workers. Is not nullable. |
| `total_cpu_idle_capped_ms` | **bigint** | **Applies to**: [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)]<br /><br />[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]<br />Total time, in milliseconds, that the scheduler spends idle while enforcing an instance-level CPU cap. Is nullable. |
| `total_scheduler_delay_ms` | **bigint** | Total accumulated delay, in milliseconds, between a worker switching out and the next nonpreemptive worker starting to execute. The operating system scheduling preemptive workers or threads from other processes can cause this delay. Is not nullable. |
| `ideal_workers_limit` | **int** | **Applies to**: [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] and later <br><br> How many workers should ideally be on the scheduler. If the current workers exceed the limit due to imbalanced task load, once they become idle they will be trimmed. Is not nullable. |
| `total_signal_time` | **bigint** | **Applies to**: [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions.<br /><br />Accumulated signal wait time, in milliseconds, for completed SQLOS waits recorded on this scheduler. Includes waits by user and internal tasks. Timer-driven internal tasks can contribute to this value even when no workers are waiting on the runnable queue. Is not nullable. |
| `total_waits_completed` | **bigint** | **Applies to**: [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions.<br /><br />Number of completed SQLOS waits recorded on this scheduler, including scheduler yield waits. Excludes elapsed-time measurements recorded around operations such as memory allocation (`MEMORY_ALLOCATION_EXT`), rather than through the SQLOS wait/signal/resume mechanism. Is not nullable. |
| `total_enqueued_tasks` | **int** | **Applies to**: [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions.<br /><br />Cumulative number of tasks enqueued on the scheduler since it was created. Is not nullable. |
| `total_completed_tasks` | **int** | **Applies to**: [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions.<br /><br />Cumulative number of tasks removed from the scheduler's task count since it was created. Includes task completions, task transfers to another scheduler, and task removals from the pending queue. Is not nullable. |
| `spinlock_wait_time_ms` | **bigint** | **Applies to**: [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions.<br /><br />Accumulated time, in milliseconds, spent acquiring spinlocks by workers associated with this scheduler. Includes spinning and sleeping time. Is not nullable. For more information, see [Spinlock wait statistics](#spinlock-wait-statistics). |
| `spinlock_max_wait_time_ms` | **bigint** | **Applies to**: [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions.<br /><br />Largest accumulated spinlock wait-time sample, in milliseconds, reported by a worker on this scheduler. Is not nullable. |
| `spinlock_wait_count` | **bigint** | **Applies to**: [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions.<br /><br />Number of nonzero spinlock wait-time samples reported by workers on this scheduler. Is not nullable. |
| `pdw_node_id` | **int** | **Applies to**: [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)]<br /><br /> The identifier for the node that this distribution is on. |

## Remarks

Task and worker counts include user requests and internal Database Engine work. Tasks waiting for an available worker contribute to `work_queue_count`. Workers with assigned tasks waiting on the scheduler's runnable queue contribute to `runnable_tasks_count`.

SQLOS creates or reuses hidden schedulers as internal work requires them. The number of hidden schedulers can vary with instance configuration and activity since startup.

The view samples values while scheduler activity continues. Counts from different columns and rows can reflect scheduler activity at different instants.

Enqueuing a task increments both `current_tasks_count` and `total_enqueued_tasks`. Completing a task decrements `current_tasks_count` and increments `total_completed_tasks`. Task transfers update these counters on both the source and destination schedulers. Compare changes in the cumulative task counters over a measurement interval to measure task scheduling activity on each scheduler.

The `total_signal_time` and `total_waits_completed` counters aggregate waits from user and internal tasks. Dividing the change in `total_signal_time` by the change in `total_waits_completed` over the same measurement interval gives the average recorded signal wait time per completed wait, in milliseconds, when the completed-wait count increases. Evaluate this average together with runnable queue counts and CPU utilization.

### Spinlock wait statistics

To collect spinlock wait statistics in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions, enable [trace flag 8134](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf8134) globally. The `spinlock_wait_time_ms`, `spinlock_max_wait_time_ms`, and `spinlock_wait_count` columns report the scheduler's [SPINLOCK_EXT](sys-dm-os-wait-stats-transact-sql.md#spinlock_ext) waits.

Workers accumulate spinlock acquisition time during each execution quantum and report the accumulated time to the scheduler. A nonzero report constitutes one sample, which can include multiple spinlock acquisitions. The maximum and count columns describe these samples.

Collecting these statistics can have a minor performance impact on high-throughput, highly concurrent workloads. For guidance on using spinlock diagnostics, see [Diagnose and resolve spinlock contention](../diagnose-resolve-spinlock-contention.md#diagnose-spinlock-contention).

## Permissions
On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] and SQL Managed Instance, requires `VIEW SERVER STATE` permission.

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Microsoft Entra admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.   

### Permissions for SQL Server 2022 and later

Requires `VIEW SERVER PERFORMANCE STATE` permission on the server.

## Examples

### A. View scheduler status, workers, and tasks

The following query shows scheduler status and the associated worker and task counts across all schedulers. Results depend on the instance configuration and workload.

```sql
SELECT scheduler_id,
       cpu_id,
       parent_node_id,
       status,
       current_tasks_count,
       runnable_tasks_count,
       current_workers_count,
       active_workers_count,
       work_queue_count
FROM sys.dm_os_schedulers
ORDER BY scheduler_id;
```

### B. Monitor worker and task counts

The following query reports worker and task counts for schedulers with `scheduler_id` values less than `1048576`. This set excludes hidden schedulers and the scheduler reserved for DAC requests.

```sql
SELECT scheduler_id,
       cpu_id,
       status,
       current_tasks_count,
       runnable_tasks_count,
       current_workers_count,
       active_workers_count,
       work_queue_count
FROM sys.dm_os_schedulers
WHERE scheduler_id < 1048576
ORDER BY scheduler_id;
```

Interpret the queue counts as follows:

- A `work_queue_count` greater than `0` indicates that tasks are waiting for an available worker.
- A `runnable_tasks_count` greater than `0` indicates that workers with assigned tasks are ready to execute and are waiting on a scheduler's runnable queue.
- A `runnable_tasks_count` greater than `0` with `work_queue_count` equal to `0` indicates that workers are waiting to execute, and no tasks are waiting for an available worker on that scheduler.

Use [sys.dm_os_tasks](sys-dm-os-tasks-transact-sql.md), [sys.dm_os_workers](sys-dm-os-workers-transact-sql.md), and [sys.dm_os_waiting_tasks](sys-dm-os-waiting-tasks-transact-sql.md) to investigate task states, worker states, and waits.
  
## Related content

- [SQL Server Operating System related dynamic management views (Transact-SQL)](sql-server-operating-system-related-dynamic-management-views-transact-sql.md)
- [Thread and task architecture guide](../thread-and-task-architecture-guide.md)
- [sys.dm_os_nodes (Transact-SQL)](sys-dm-os-nodes-transact-sql.md)
- [sys.dm_os_workers (Transact-SQL)](sys-dm-os-workers-transact-sql.md)
