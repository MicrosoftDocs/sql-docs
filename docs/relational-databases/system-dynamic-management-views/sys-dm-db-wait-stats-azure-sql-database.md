---
title: "sys.dm_db_wait_stats (Azure SQL Database) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: ""
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "dm_db_wait_stats_TSQL"
  - "dm_db_wait_stats"
  - "sys.dm_db_wait_stats"
  - "sys.dm_db_wait_stats_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_db_wait_stats dynamic management view"
  - "dm_db_wait_stats"
ms.assetid: 00abd0a5-bae0-4d71-b173-f7a14cddf795
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "= azuresqldb-current || = sqlallproducts-allversions"
---
# sys.dm_db_wait_stats (Azure SQL Database)
[!INCLUDE[tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md.md)]

  Returns information about all the waits encountered by threads that executed during operation. You can use this aggregated view to diagnose performance issues with [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and also with specific queries and batches.  
  
 Specific types of wait times during query execution can indicate bottlenecks or stall points within the query. Similarly, high wait times, or wait counts server wide can indicate bottlenecks or hot spots in interaction query interactions within the server instance. For example, lock waits indicate data contention by queries; page IO latch waits indicate slow IO response times; page latch update waits indicate incorrect file layout.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|wait_type|**nvarchar(60)**|Name of the wait type. For more information, see [Types of Waits](#WaitTypes), later in this topic.|  
|waiting_tasks_count|**bigint**|Number of waits on this wait type. This counter is incremented at the start of each wait.|  
|wait_time_ms|**bigint**|Total wait time for this wait type in milliseconds. This time is inclusive of signal_wait_time_ms.|  
|max_wait_time_ms|**bigint**|Maximum wait time on this wait type.|  
|signal_wait_time_ms|**bigint**|Difference between the time that the waiting thread was signaled and when it started running.|  
  
## Remarks  
  
-   This dynamic management view displays data only for the current database.  
  
-   This dynamic management view shows the time for waits that have completed. It does not show current waits.  
  
-   Counters are reset to zero any time the database is moved or taken offline.  
  
-   A SQL Server worker thread is not considered to be waiting if any of the following is true:  
  
    -   A resource becomes available.  
  
    -   A queue is nonempty.  
  
    -   An external process finishes.  
  
-   These statistics are not persisted across SQL Database failover events, and all data are cumulative since the last time the statistics were reset.  
  
## Permissions  
 Requires VIEW DATABASE STATE permission on the server.  
  
##  <a name="WaitTypes"></a> Types of Waits  
 Resource waits  
 Resource waits occur when a worker requests access to a resource that is not available because the resource is being used by some other worker or is not yet available. Examples of resource waits are locks, latches, network and disk I/O waits. Lock and latch waits are waits on synchronization objects.  
  
 Queue waits  
 Queue waits occur when a worker is idle, waiting for work to be assigned. Queue waits are most typically seen with system background tasks such as the deadlock monitor and deleted record cleanup tasks. These tasks will wait for work requests to be placed into a work queue. Queue waits may also periodically become active even if no new packets have been put on the queue.  
  
 External waits  
 External waits occur when a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] worker is waiting for an external event, such as an extended stored procedure call or a linked server query, to finish. When you diagnose blocking issues, remember that external waits do not always imply that the worker is idle, because the worker may actively be running some external code.  
  
 Although the thread is no longer waiting, the thread does not have to start running immediately. This is because such a thread is first put on the queue of runnable workers and must wait for a quantum to run on the scheduler.  
  
 In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] the wait-time counters are **bigint** values and therefore are not as prone to counter rollover as the equivalent counters in earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 The following table lists the wait types encountered by tasks.  
  
|Wait type|Description|  
|---------------|-----------------|  
|ABR|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|ASSEMBLY_LOAD|Occurs during exclusive access to assembly loading.|  
|ASYNC_DISKPOOL_LOCK|Occurs when there is an attempt to synchronize parallel threads that are performing tasks such as creating or initializing a file.|  
|ASYNC_IO_COMPLETION|Occurs when a task is waiting for I/Os to finish.|  
|ASYNC_NETWORK_IO|Occurs on network writes when the task is blocked behind the network. Verify that the client is processing data from the server.|  
|AUDIT_GROUPCACHE_LOCK|Occurs when there is a wait on a lock that controls access to a special cache. The cache contains information about which audits are being used to audit each audit action group.|  
|AUDIT_LOGINCACHE_LOCK|Occurs when there is a wait on a lock that controls access to a special cache. The cache contains information about which audits are being used to audit login audit action groups.|  
|AUDIT_ON_DEMAND_TARGET_LOCK|Occurs when there is a wait on a lock that is used to ensure single initialization of audit related Extended Event targets.|  
|AUDIT_XE_SESSION_MGR|Occurs when there is a wait on a lock that is used to synchronize the starting and stopping of audit related Extended Events sessions.|  
|BACKUP|Occurs when a task is blocked as part of backup processing.|  
|BACKUP_OPERATOR|Occurs when a task is waiting for a tape mount. To view the tape status, query [sys.dm_io_backup_tapes](../../relational-databases/system-dynamic-management-views/sys-dm-io-backup-tapes-transact-sql.md). If a mount operation is not pending, this wait type may indicate a hardware problem with the tape drive.|  
|BACKUPBUFFER|Occurs when a backup task is waiting for data, or is waiting for a buffer in which to store data. This type is not typical, except when a task is waiting for a tape mount.|  
|BACKUPIO|Occurs when a backup task is waiting for data, or is waiting for a buffer in which to store data. This type is not typical, except when a task is waiting for a tape mount.|  
|BACKUPTHREAD|Occurs when a task is waiting for a backup task to finish. Wait times may be long, from several minutes to several hours. If the task that is being waited on is in an I/O process, this type does not indicate a problem.|  
|BAD_PAGE_PROCESS|Occurs when the background suspect page logger is trying to avoid running more than every five seconds. Excessive suspect pages cause the logger to run frequently.|  
|BROKER_CONNECTION_RECEIVE_TASK|Occurs when waiting for access to receive a message on a connection endpoint. Receive access to the endpoint is serialized.|  
|BROKER_ENDPOINT_STATE_MUTEX|Occurs when there is contention to access the state of a [!INCLUDE[ssSB](../../includes/sssb-md.md)] connection endpoint. Access to the state for changes is serialized.|  
|BROKER_EVENTHANDLER|Occurs when a task is waiting in the primary event handler of the [!INCLUDE[ssSB](../../includes/sssb-md.md)]. This should occur very briefly.|  
|BROKER_INIT|Occurs when initializing [!INCLUDE[ssSB](../../includes/sssb-md.md)] in each active database. This should occur infrequently.|  
|BROKER_MASTERSTART|Occurs when a task is waiting for the primary event handler of the [!INCLUDE[ssSB](../../includes/sssb-md.md)] to start. This should occur very briefly.|  
|BROKER_RECEIVE_WAITFOR|Occurs when the RECEIVE WAITFOR is waiting. This is typical if no messages are ready to be received.|  
|BROKER_REGISTERALLENDPOINTS|Occurs during the initialization of a [!INCLUDE[ssSB](../../includes/sssb-md.md)] connection endpoint. This should occur very briefly.|  
|BROKER_SERVICE|Occurs when the [!INCLUDE[ssSB](../../includes/sssb-md.md)] destination list that is associated with a target service is updated or re-prioritized.|  
|BROKER_SHUTDOWN|Occurs when there is a planned shutdown of [!INCLUDE[ssSB](../../includes/sssb-md.md)]. This should occur very briefly, if at all.|  
|BROKER_TASK_STOP|Occurs when the [!INCLUDE[ssSB](../../includes/sssb-md.md)] queue task handler tries to shut down the task. The state check is serialized and must be in a running state beforehand.|  
|BROKER_TO_FLUSH|Occurs when the [!INCLUDE[ssSB](../../includes/sssb-md.md)] lazy flusher flushes the in-memory transmission objects to a work table.|  
|BROKER_TRANSMITTER|Occurs when the [!INCLUDE[ssSB](../../includes/sssb-md.md)] transmitter is waiting for work.|  
|BUILTIN_HASHKEY_MUTEX|May occur after startup of instance, while internal data structures are initializing. Will not recur once data structures have initialized.|  
|CHECK_PRINT_RECORD|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|CHECKPOINT_QUEUE|Occurs while the checkpoint task is waiting for the next checkpoint request.|  
|CHKPT|Occurs at server startup to tell the checkpoint thread that it can start.|  
|CLEAR_DB|Occurs during operations that change the state of a database, such as opening or closing a database.|  
|CLR_AUTO_EVENT|Occurs when a task is currently performing common language runtime (CLR) execution and is waiting for a particular autoevent to be initiated. Long waits are typical, and do not indicate a problem.|  
|CLR_CRST|Occurs when a task is currently performing CLR execution and is waiting to enter a critical section of the task that is currently being used by another task.|  
|CLR_JOIN|Occurs when a task is currently performing CLR execution and waiting for another task to end. This wait state occurs when there is a join between tasks.|  
|CLR_MANUAL_EVENT|Occurs when a task is currently performing CLR execution and is waiting for a specific manual event to be initiated.|  
|CLR_MEMORY_SPY|Occurs during a wait on lock acquisition for a data structure that is used to record all virtual memory allocations that come from CLR. The data structure is locked to maintain its integrity if there is parallel access.|  
|CLR_MONITOR|Occurs when a task is currently performing CLR execution and is waiting to obtain a lock on the monitor.|  
|CLR_RWLOCK_READER|Occurs when a task is currently performing CLR execution and is waiting for a reader lock.|  
|CLR_RWLOCK_WRITER|Occurs when a task is currently performing CLR execution and is waiting for a writer lock.|  
|CLR_SEMAPHORE|Occurs when a task is currently performing CLR execution and is waiting for a semaphore.|  
|CLR_TASK_START|Occurs while waiting for a CLR task to complete startup.|  
|CLRHOST_STATE_ACCESS|Occurs where there is a wait to acquire exclusive access to the CLR-hosting data structures. This wait type occurs while setting up or tearing down the CLR runtime.|  
|CMEMTHREAD|Occurs when a task is waiting on a thread-safe memory object. The wait time might increase when there is contention caused by multiple tasks trying to allocate memory from the same memory object.|  
|CXPACKET|Occurs when trying to synchronize the query processor exchange iterator. You may consider lowering the degree of parallelism if contention on this wait type becomes a problem.|  
|CXROWSET_SYNC|Occurs during a parallel range scan.|  
|DAC_INIT|Occurs while the dedicated administrator connection is initializing.|  
|DBMIRROR_DBM_EVENT|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|DBMIRROR_DBM_MUTEX|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|DBMIRROR_EVENTS_QUEUE|Occurs when database mirroring waits for events to process.|  
|DBMIRROR_SEND|Occurs when a task is waiting for a communications backlog at the network layer to clear to be able to send messages. Indicates that the communications layer is starting to become overloaded and affect the database mirroring data throughput.|  
|DBMIRROR_WORKER_QUEUE|Indicates that the database mirroring worker task is waiting for more work.|  
|DBMIRRORING_CMD|Occurs when a task is waiting for log records to be flushed to disk. This wait state is expected to be held for long periods of time.|  
|DEADLOCK_ENUM_MUTEX|Occurs when the deadlock monitor and sys.dm_os_waiting_tasks try to make sure that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is not running multiple deadlock searches at the same time.|  
|DEADLOCK_TASK_SEARCH|Large waiting time on this resource indicates that the server is executing queries on top of sys.dm_os_waiting_tasks, and these queries are blocking deadlock monitor from running deadlock search. This wait type is used by deadlock monitor only. Queries on top of sys.dm_os_waiting_tasks use DEADLOCK_ENUM_MUTEX.|  
|DEBUG|Occurs during [!INCLUDE[tsql](../../includes/tsql-md.md)] and CLR debugging for internal synchronization.|  
|DISABLE_VERSIONING|Occurs when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] polls the version transaction manager to see whether the timestamp of the earliest active transaction is later than the timestamp of when the state started changing. If this is this case, all the snapshot transactions that were started before the ALTER DATABASE statement was run have finished. This wait state is used when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] disables versioning by using the ALTER DATABASE statement.|  
|DISKIO_SUSPEND|Occurs when a task is waiting to access a file when an external backup is active. This is reported for each waiting user process. A count larger than five per user process may indicate that the external backup is taking too much time to finish.|  
|DISPATCHER_QUEUE_SEMAPHORE|Occurs when a thread from the dispatcher pool is waiting for more work to process. The wait time for this wait type is expected to increase when the dispatcher is idle.|  
|DLL_LOADING_MUTEX|Occurs once while waiting for the XML parser DLL to load.|  
|DROPTEMP|Occurs between attempts to drop a temporary object if the previous attempt failed. The wait duration grows exponentially with each failed drop attempt.|  
|DTC|Occurs when a task is waiting on an event that is used to manage state transition. This state controls when the recovery of [!INCLUDE[msCoName](../../includes/msconame-md.md)] Distributed Transaction Coordinator (MS DTC) transactions occurs after [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] receives notification that the MS DTC service has become unavailable.<br /><br /> This state also describes a task that is waiting when a commit of a MS DTC transaction is initiated by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is waiting for the MS DTC commit to finish.|  
|DTC_ABORT_REQUEST|Occurs in a MS DTC worker session when the session is waiting to take ownership of a MS DTC transaction. After MS DTC owns the transaction, the session can roll back the transaction. Generally, the session will wait for another session that is using the transaction.|  
|DTC_RESOLVE|Occurs when a recovery task is waiting for the master database in a cross-database transaction so that the task can query the outcome of the transaction.|  
|DTC_STATE|Occurs when a task is waiting on an event that protects changes to the internal MS DTC global state object. This state should be held for very short periods of time.|  
|DTC_TMDOWN_REQUEST|Occurs in a MS DTC worker session when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] receives notification that the MS DTC service is not available. First, the worker will wait for the MS DTC recovery process to start. Then, the worker waits to obtain the outcome of the distributed transaction that the worker is working on. This may continue until the connection with the MS DTC service has been reestablished.|  
|DTC_WAITFOR_OUTCOME|Occurs when recovery tasks wait for MS DTC to become active to enable the resolution of prepared transactions.|  
|DUMP_LOG_COORDINATOR|Occurs when a main task is waiting for a subtask to generate data. Ordinarily, this state does not occur. A long wait indicates an unexpected blockage. The subtask should be investigated.|  
|DUMPTRIGGER|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|EC|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|EE_PMOLOCK|Occurs during synchronization of certain types of memory allocations during statement execution.|  
|EE_SPECPROC_MAP_INIT|Occurs during synchronization of internal procedure hash table creation. This wait can only occur during the initial accessing of the hash table after the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance starts.|  
|ENABLE_VERSIONING|Occurs when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] waits for all update transactions in this database to finish before declaring the database ready to transition to snapshot isolation allowed state. This state is used when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] enables snapshot isolation by using the ALTER DATABASE statement.|  
|ERROR_REPORTING_MANAGER|Occurs during synchronization of multiple concurrent error log initializations.|  
|EXCHANGE|Occurs during synchronization in the query processor exchange iterator during parallel queries.|  
|EXECSYNC|Occurs during parallel queries while synchronizing in query processor in areas not related to the exchange iterator. Examples of such areas are bitmaps, large binary objects (LOBs), and the spool iterator. LOBs may frequently use this wait state.|  
|EXECUTION_PIPE_EVENT_INTERNAL|Occurs during synchronization between producer and consumer parts of batch execution that are submitted through the connection context.|  
|FAILPOINT|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|FCB_REPLICA_READ|Occurs when the reads of a snapshot (or a temporary snapshot created by DBCC) sparse file are synchronized.|  
|FCB_REPLICA_WRITE|Occurs when the pushing or pulling of a page to a snapshot (or a temporary snapshot created by DBCC) sparse file is synchronized.|  
|FS_FC_RWLOCK|Occurs when there is a wait by the FILESTREAM garbage collector to do either of the following:<br /><br /> Disable garbage collection (used by backup and restore).<br /><br /> Execute one cycle of the FILESTREAM garbage collector.|  
|FS_GARBAGE_COLLECTOR_SHUTDOWN|Occurs when the FILESTREAM garbage collector is waiting for cleanup tasks to be completed.|  
|FS_HEADER_RWLOCK|Occurs when there is a wait to acquire access to the FILESTREAM header of a FILESTREAM data container to either read or update contents in the FILESTREAM header file (Filestream.hdr).|  
|FS_LOGTRUNC_RWLOCK|Occurs when there is a wait to acquire access to FILESTREAM log truncation to do either of the following:<br /><br /> Temporarily disable FILESTREAM log (FSLOG) truncation (used by backup and restore).<br /><br /> Execute one cycle of FSLOG truncation.|  
|FSA_FORCE_OWN_XACT|Occurs when a FILESTREAM file I/O operation needs to bind to the associated transaction, but the transaction is currently owned by another session.|  
|FSAGENT|Occurs when a FILESTREAM file I/O operation is waiting for a FILESTREAM agent resource that is being used by another file I/O operation.|  
|FSTR_CONFIG_MUTEX|Occurs when there is a wait for another FILESTREAM feature reconfiguration to be completed.|  
|FSTR_CONFIG_RWLOCK|Occurs when there is a wait to serialize access to the FILESTREAM configuration parameters.|  
|FT_METADATA_MUTEX|Documented for informational purposes only. Not supported. Future compatibility is not guaranteed.|  
|FT_RESTART_CRAWL|Occurs when a full-text crawl needs to restart from a last known good point to recover from a transient failure. The wait lets the worker tasks currently working on that population to complete or exit the current step.|  
|FULLTEXT GATHERER|Occurs during synchronization of full-text operations.|  
|GUARDIAN|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|HTTP_ENUMERATION|Occurs at startup to enumerate the HTTP endpoints to start HTTP.|  
|HTTP_START|Occurs when a connection is waiting for HTTP to complete initialization.|  
|IMPPROV_IOWAIT|Occurs when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] waits for a bulkload I/O to finish.|  
|INTERNAL_TESTING|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|IO_AUDIT_MUTEX|Occurs during synchronization of trace event buffers.|  
|IO_COMPLETION|Occurs while waiting for I/O operations to complete. This wait type generally represents non-data page I/Os. Data page I/O completion waits appear as PAGEIOLATCH_* waits.|  
|IO_QUEUE_LIMIT|Occurs when the asynchronous IO queue for the [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] has too many IOs pending. Tasks trying to issue another IO are blocked on this wait type until the number of pending IOs drop below the threshold. The threshold is proportional to the DTUs assigned to the database.|  
|IO_RETRY|Occurs when an I/O operation such as a read or a write to disk fails because of insufficient resources, and is then retried.|  
|IOAFF_RANGE_QUEUE|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|KSOURCE_WAKEUP|Used by the service control task while waiting for requests from the Service Control Manager. Long waits are expected and do not indicate a problem.|  
|KTM_ENLISTMENT|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|KTM_RECOVERY_MANAGER|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|KTM_RECOVERY_RESOLUTION|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|LATCH_DT|Occurs when waiting for a DT (destroy) latch. This does not include buffer latches or transaction mark latches. A listing of LATCH_* waits is available in sys.dm_os_latch_stats. Note that sys.dm_os_latch_stats groups LATCH_NL, LATCH_SH, LATCH_UP, LATCH_EX, and LATCH_DT waits together.|  
|LATCH_EX|Occurs when waiting for an EX (exclusive) latch. This does not include buffer latches or transaction mark latches. A listing of LATCH_* waits is available in sys.dm_os_latch_stats. Note that sys.dm_os_latch_stats groups LATCH_NL, LATCH_SH, LATCH_UP, LATCH_EX, and LATCH_DT waits together.|  
|LATCH_KP|Occurs when waiting for a KP (keep) latch. This does not include buffer latches or transaction mark latches. A listing of LATCH_* waits is available in sys.dm_os_latch_stats. Note that sys.dm_os_latch_stats groups LATCH_NL, LATCH_SH, LATCH_UP, LATCH_EX, and LATCH_DT waits together.|  
|LATCH_NL|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|LATCH_SH|Occurs when waiting for an SH (share) latch. This does not include buffer latches or transaction mark latches. A listing of LATCH_* waits is available in sys.dm_os_latch_stats. Note that sys.dm_os_latch_stats groups LATCH_NL, LATCH_SH, LATCH_UP, LATCH_EX, and LATCH_DT waits together.|  
|LATCH_UP|Occurs when waiting for an UP (update) latch. This does not include buffer latches or transaction mark latches. A listing of LATCH_* waits is available in sys.dm_os_latch_stats. Note that sys.dm_os_latch_stats groups LATCH_NL, LATCH_SH, LATCH_UP, LATCH_EX, and LATCH_DT waits together.|  
|LAZYWRITER_SLEEP|Occurs when lazywriter tasks are suspended. This is a measure of the time spent by background tasks that are waiting. Do not consider this state when you are looking for user stalls.|  
|LCK_M_BU|Occurs when a task is waiting to acquire a Bulk Update (BU) lock. For a lock compatibility matrix, see [sys.dm_tran_locks &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-tran-locks-transact-sql.md).|  
|LCK_M_IS|Occurs when a task is waiting to acquire an Intent Shared (IS) lock. For a lock compatibility matrix, see [sys.dm_tran_locks &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-tran-locks-transact-sql.md).|  
|LCK_M_IU|Occurs when a task is waiting to acquire an Intent Update (IU) lock. For a lock compatibility matrix, see [sys.dm_tran_locks &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-tran-locks-transact-sql.md).|  
|LCK_M_IX|Occurs when a task is waiting to acquire an Intent Exclusive (IX) lock. For a lock compatibility matrix, see [sys.dm_tran_locks &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-tran-locks-transact-sql.md).|  
|LCK_M_RIn_NL|Occurs when a task is waiting to acquire a NULL lock on the current key value, and an Insert Range lock between the current and previous key. A NULL lock on the key is an instant release lock. For a lock compatibility matrix, see [sys.dm_tran_locks &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-tran-locks-transact-sql.md).|  
|LCK_M_RIn_S|Occurs when a task is waiting to acquire a shared lock on the current key value, and an Insert Range lock between the current and previous key. For a lock compatibility matrix, see [sys.dm_tran_locks &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-tran-locks-transact-sql.md).|  
|LCK_M_RIn_U|Task is waiting to acquire an Update lock on the current key value, and an Insert Range lock between the current and previous key. For a lock compatibility matrix, see [sys.dm_tran_locks &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-tran-locks-transact-sql.md).|  
|LCK_M_RIn_X|Occurs when a task is waiting to acquire an Exclusive lock on the current key value, and an Insert Range lock between the current and previous key. For a lock compatibility matrix, see [sys.dm_tran_locks &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-tran-locks-transact-sql.md).|  
|LCK_M_RS_S|Occurs when a task is waiting to acquire a Shared lock on the current key value, and a Shared Range lock between the current and previous key. For a lock compatibility matrix, see [sys.dm_tran_locks &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-tran-locks-transact-sql.md).|  
|LCK_M_RS_U|Occurs when a task is waiting to acquire an Update lock on the current key value, and an Update Range lock between the current and previous key. For a lock compatibility matrix, see [sys.dm_tran_locks &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-tran-locks-transact-sql.md).|  
|LCK_M_RX_S|Occurs when a task is waiting to acquire a Shared lock on the current key value, and an Exclusive Range lock between the current and previous key. For a lock compatibility matrix, see [sys.dm_tran_locks &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-tran-locks-transact-sql.md).|  
|LCK_M_RX_U|Occurs when a task is waiting to acquire an Update lock on the current key value, and an Exclusive range lock between the current and previous key. For a lock compatibility matrix, see [sys.dm_tran_locks &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-tran-locks-transact-sql.md).|  
|LCK_M_RX_X|Occurs when a task is waiting to acquire an Exclusive lock on the current key value, and an Exclusive Range lock between the current and previous key. For a lock compatibility matrix, see [sys.dm_tran_locks &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-tran-locks-transact-sql.md).|  
|LCK_M_S|Occurs when a task is waiting to acquire a Shared lock. For a lock compatibility matrix, see [sys.dm_tran_locks &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-tran-locks-transact-sql.md).|  
|LCK_M_SCH_M|Occurs when a task is waiting to acquire a Schema Modify lock. For a lock compatibility matrix, see [sys.dm_tran_locks &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-tran-locks-transact-sql.md).|  
|LCK_M_SCH_S|Occurs when a task is waiting to acquire a Schema Share lock. For a lock compatibility matrix, see [sys.dm_tran_locks &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-tran-locks-transact-sql.md).|  
|LCK_M_SIU|Occurs when a task is waiting to acquire a Shared With Intent Update lock. For a lock compatibility matrix, see [sys.dm_tran_locks &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-tran-locks-transact-sql.md).|  
|LCK_M_SIX|Occurs when a task is waiting to acquire a Shared With Intent Exclusive lock. For a lock compatibility matrix, see [sys.dm_tran_locks &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-tran-locks-transact-sql.md).|  
|LCK_M_U|Occurs when a task is waiting to acquire an Update lock. For a lock compatibility matrix, see [sys.dm_tran_locks &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-tran-locks-transact-sql.md).|  
|LCK_M_UIX|Occurs when a task is waiting to acquire an Update With Intent Exclusive lock. For a lock compatibility matrix, see [sys.dm_tran_locks &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-tran-locks-transact-sql.md).|  
|LCK_M_X|Occurs when a task is waiting to acquire an Exclusive lock. For a lock compatibility matrix, see [sys.dm_tran_locks &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-tran-locks-transact-sql.md).|  
|LOG_RATE_GOVERNOR|Occurs when DB is waiting for quota to write to the log.|  
|LOGBUFFER|Occurs when a task is waiting for space in the log buffer to store a log record. Consistently high values may indicate that the log devices cannot keep up with the amount of log being generated by the server.|  
|LOGGENERATION|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|LOGMGR|Occurs when a task is waiting for any outstanding log I/Os to finish before shutting down the log while closing the database.|  
|LOGMGR_FLUSH|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|LOGMGR_QUEUE|Occurs while the log writer task waits for work requests.|  
|LOGMGR_RESERVE_APPEND|Occurs when a task is waiting to see whether log truncation frees up log space to enable the task to write a new log record. Consider increasing the size of the log file(s) for the affected database to reduce this wait.|  
|LOWFAIL_MEMMGR_QUEUE|Occurs while waiting for memory to be available for use.|  
|MSQL_DQ|Occurs when a task is waiting for a distributed query operation to finish. This is used to detect potential Multiple Active Result Set (MARS) application deadlocks. The wait ends when the distributed query call finishes.|  
|MSQL_XACT_MGR_MUTEX|Occurs when a task is waiting to obtain ownership of the session transaction manager to perform a session level transaction operation.|  
|MSQL_XACT_MUTEX|Occurs during synchronization of transaction usage. A request must acquire the mutex before it can use the transaction.|  
|MSQL_XP|Occurs when a task is waiting for an extended stored procedure to end. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses this wait state to detect potential MARS application deadlocks. The wait stops when the extended stored procedure call ends.|  
|MSSEARCH|Occurs during Full-Text Search calls. This wait ends when the full-text operation completes. It does not indicate contention, but rather the duration of full-text operations.|  
|NET_WAITFOR_PACKET|Occurs when a connection is waiting for a network packet during a network read.|  
|OLEDB|Occurs when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] calls the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB Provider. This wait type is not used for synchronization. Instead, it indicates the duration of calls to the OLE DB provider.|  
|ONDEMAND_TASK_QUEUE|Occurs while a background task waits for high priority system task requests. Long wait times indicate that there have been no high priority requests to process, and should not cause concern.|  
|PAGEIOLATCH_DT|Occurs when a task is waiting on a latch for a buffer that is in an I/O request. The latch request is in Destroy mode. Long waits may indicate problems with the disk subsystem.|  
|PAGEIOLATCH_EX|Occurs when a task is waiting on a latch for a buffer that is in an I/O request. The latch request is in Exclusive mode. Long waits may indicate problems with the disk subsystem.|  
|PAGEIOLATCH_KP|Occurs when a task is waiting on a latch for a buffer that is in an I/O request. The latch request is in Keep mode. Long waits may indicate problems with the disk subsystem.|  
|PAGEIOLATCH_NL|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|PAGEIOLATCH_SH|Occurs when a task is waiting on a latch for a buffer that is in an I/O request. The latch request is in Shared mode. Long waits may indicate problems with the disk subsystem.|  
|PAGEIOLATCH_UP|Occurs when a task is waiting on a latch for a buffer that is in an I/O request. The latch request is in Update mode. Long waits may indicate problems with the disk subsystem.|  
|PAGELATCH_DT|Occurs when a task is waiting on a latch for a buffer that is not in an I/O request. The latch request is in Destroy mode.|  
|PAGELATCH_EX|Occurs when a task is waiting on a latch for a buffer that is not in an I/O request. The latch request is in Exclusive mode.|  
|PAGELATCH_KP|Occurs when a task is waiting on a latch for a buffer that is not in an I/O request. The latch request is in Keep mode.|  
|PAGELATCH_NL|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|PAGELATCH_SH|Occurs when a task is waiting on a latch for a buffer that is not in an I/O request. The latch request is in Shared mode.|  
|PAGELATCH_UP|Occurs when a task is waiting on a latch for a buffer that is not in an I/O request. The latch request is in Update mode.|  
|PARALLEL_BACKUP_QUEUE|Occurs when serializing output produced by RESTORE HEADERONLY, RESTORE FILELISTONLY, or RESTORE LABELONLY.|  
|PREEMPTIVE_ABR|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|PREEMPTIVE_AUDIT_ACCESS_EVENTLOG|Occurs when the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Operating System (SQLOS) scheduler switches to preemptive mode to write an audit event to the Windows event log.|  
|PREEMPTIVE_AUDIT_ACCESS_SECLOG|Occurs when the SQLOS scheduler switches to preemptive mode to write an audit event to the Windows Security log.|  
|PREEMPTIVE_CLOSEBACKUPMEDIA|Occurs when the SQLOS scheduler switches to preemptive mode to close backup media.|  
|PREEMPTIVE_CLOSEBACKUPTAPE|Occurs when the SQLOS scheduler switches to preemptive mode to close a tape backup device.|  
|PREEMPTIVE_CLOSEBACKUPVDIDEVICE|Occurs when the SQLOS scheduler switches to preemptive mode to close a virtual backup device.|  
|PREEMPTIVE_CLUSAPI_CLUSTERRESOURCECONTROL|Occurs when the SQLOS scheduler switches to preemptive mode to perform Windows failover cluster operations.|  
|PREEMPTIVE_COM_COCREATEINSTANCE|Occurs when the SQLOS scheduler switches to preemptive mode to create a COM object.|  
|PREEMPTIVE_HADR_LEASE_MECHANISM|Always On Availability Groups lease manager scheduling for CSS diagnostics.|  
|PREEMPTIVE_SOSTESTING|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|PREEMPTIVE_STRESSDRIVER|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|PREEMPTIVE_TESTING|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|PREEMPTIVE_XETESTING|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|PRINT_ROLLBACK_PROGRESS|Used to wait while user processes are ended in a database that has been transitioned by using the ALTER DATABASE termination clause. For more information, see [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md).|  
|PWAIT_HADR_CHANGE_NOTIFIER_TERMINATION_SYNC|Occurs when a background task is waiting for the termination of the background task that receives (via polling) Windows Server Failover Clustering notifications.  Internal use only.|  
|PWAIT_HADR_CLUSTER_INTEGRATION|An append, replace, and/or remove operation is waiting to grab a write lock on an Always On internal list (such as a list of networks, network addresses, or availability group listeners).  Internal use only.|  
|PWAIT_HADR_OFFLINE_COMPLETED|An Always On drop availability group operation is waiting for the target availability group to go offline before destroying Windows Server Failover Clustering objects.|  
|PWAIT_HADR_ONLINE_COMPLETED|An Always On create or failover availability group operation is waiting for the target availability group to come online.|  
|PWAIT_HADR_POST_ONLINE_COMPLETED|An Always On drop availability group operation is waiting for the termination of any background task that was scheduled as part of a previous command. For example, there may be a background task that is transitioning availability databases to the primary role. The DROP AVAILABILITY GROUP DDL must wait for this background task to terminate in order to avoid race conditions.|  
|PWAIT_HADR_WORKITEM_COMPLETED|Internal wait by a thread waiting for an async work task to complete. This is an expected wait and is for CSS use.|  
|PWAIT_MD_LOGIN_STATS|Occurs during internal synchronization in metadata on login stats.|  
|PWAIT_MD_RELATION_CACHE|Occurs during internal synchronization in metadata on table or index.|  
|PWAIT_MD_SERVER_CACHE|Occurs during internal synchronization in metadata on linked servers.|  
|PWAIT_MD_UPGRADE_CONFIG|Occurs during internal synchronization in upgrading server wide configurations.|  
|PWAIT_METADATA_LAZYCACHE_RWLOCk|Occurs during internal synchronization in metadata cache along with iterating index or stats in a table.|  
|QPJOB_KILL|Indicates that an asynchronous automatic statistics update was canceled by a call to KILL as the update was starting to run. The terminating thread is suspended, waiting for it to start listening for KILL commands. A good value is less than one second.|  
|QPJOB_WAITFOR_ABORT|Indicates that an asynchronous automatic statistics update was canceled by a call to KILL when it was running. The update has now completed but is suspended until the terminating thread message coordination is complete. This is an ordinary but rare state, and should be very short. A good value is less than one second.|  
|QRY_MEM_GRANT_INFO_MUTEX|Occurs when Query Execution memory management tries to control access to static grant information list. This state lists information about the current granted and waiting memory requests. This state is a simple access control state. There should never be a long wait on this state. If this mutex is not released, all new memory-using queries will stop responding.|  
|QUERY_ERRHDL_SERVICE_DONE|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|QUERY_EXECUTION_INDEX_SORT_EVENT_OPEN|Occurs in certain cases when offline create index build is run in parallel, and the different worker threads that are sorting synchronize access to the sort files.|  
|QUERY_NOTIFICATION_MGR_MUTEX|Occurs during synchronization of the garbage collection queue in the Query Notification Manager.|  
|QUERY_NOTIFICATION_SUBSCRIPTION_MUTEX|Occurs during state synchronization for transactions in Query Notifications.|  
|QUERY_NOTIFICATION_TABLE_MGR_MUTEX|Occurs during internal synchronization within the Query Notification Manager.|  
|QUERY_NOTIFICATION_UNITTEST_MUTEX|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|QUERY_OPTIMIZER_PRINT_MUTEX|Occurs during synchronization of query optimizer diagnostic output production. This wait type only occurs if diagnostic settings have been enabled under direction of [!INCLUDE[msCoName](../../includes/msconame-md.md)] Product Support.|  
|QUERY_TRACEOUT|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|QUERY_WAIT_ERRHDL_SERVICE|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|RECOVER_CHANGEDB|Occurs during synchronization of database status in warm standby database.|  
|REPL_CACHE_ACCESS|Occurs during synchronization on a replication article cache. During these waits, the replication log reader stalls, and data definition language (DDL) statements on a published table are blocked.|  
|REPL_SCHEMA_ACCESS|Occurs during synchronization of replication schema version information. This state exists when DDL statements are executed on the replicated object, and when the log reader builds or consumes versioned schema based on DDL occurrence.|  
|REPLICA_WRITES|Occurs while a task waits for completion of page writes to database snapshots or DBCC replicas.|  
|REQUEST_DISPENSER_PAUSE|Occurs when a task is waiting for all outstanding I/O to complete, so that I/O to a file can be frozen for snapshot backup.|  
|REQUEST_FOR_DEADLOCK_SEARCH|Occurs while the deadlock monitor waits to start the next deadlock search. This wait is expected between deadlock detections, and lengthy total waiting time on this resource does not indicate a problem.|  
|RESMGR_THROTTLED|Occurs when a new request comes in and is throttled based on the GROUP_MAX_REQUESTS setting.|  
|RESOURCE_QUEUE|Occurs during synchronization of various internal resource queues.|  
|RESOURCE_SEMAPHORE|Occurs when a query memory request cannot be granted immediately due to other concurrent queries. High waits and wait times may indicate excessive number of concurrent queries, or excessive memory request amounts.|  
|RESOURCE_SEMAPHORE_MUTEX|Occurs while a query waits for its request for a thread reservation to be fulfilled. It also occurs when synchronizing query compile and memory grant requests.|  
|RESOURCE_SEMAPHORE_QUERY_COMPILE|Occurs when the number of concurrent query compilations reaches a throttling limit. High waits and wait times may indicate excessive compilations, recompiles, or uncachable plans.|  
|RESOURCE_SEMAPHORE_SMALL_QUERY|Occurs when memory request by a small query cannot be granted immediately due to other concurrent queries. Wait time should not exceed more than a few seconds, because the server transfers the request to the main query memory pool if it fails to grant the requested memory within a few seconds. High waits may indicate an excessive number of concurrent small queries while the main memory pool is blocked by waiting queries.|  
|SE_REPL_CATCHUP_THROTTLE|Occurs when the transaction is waiting for one of the database secondaries to make progress.|  
|SE_REPL_COMMIT_ACK|Occurs when the transaction is waiting for quorum commit acknowledgement from secondary replicas.|  
|SE_REPL_COMMIT_TURN|Occurs when the transaction is waiting for commit after receiving quorum commit acknowledgements.|  
|SE_REPL_ROLLBACK_ACK|Occurs when the transaction is waiting for quorum rollback acknowledgement from secondary replicas.|  
|SE_REPL_SLOW_SECONDARY_THROTTLE|Occurs when the thread is waiting for one of the database secondary replicas.|  
|SEC_DROP_TEMP_KEY|Occurs after a failed attempt to drop a temporary security key before a retry attempt.|  
|SECURITY_MUTEX|Occurs when there is a wait for mutexes that control access to the global list of Extensible Key Management (EKM) cryptographic providers and the session-scoped list of EKM sessions.|  
|SEQUENTIAL_GUID|Occurs while a new sequential GUID is being obtained.|  
|SERVER_IDLE_CHECK|Occurs during synchronization of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance idle status when a resource monitor is attempting to declare a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance as idle or trying to wake up.|  
|SHUTDOWN|Occurs while a shutdown statement waits for active connections to exit.|  
|SLEEP_BPOOL_FLUSH|Occurs when a checkpoint is throttling the issuance of new I/Os in order to avoid flooding the disk subsystem.|  
|SLEEP_DBSTARTUP|Occurs during database startup while waiting for all databases to recover.|  
|SLEEP_DCOMSTARTUP|Occurs once at most during [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance startup while waiting for DCOM initialization to complete.|  
|SLEEP_MSDBSTARTUP|Occurs when SQL Trace waits for the msdb database to complete startup.|  
|SLEEP_SYSTEMTASK|Occurs during the start of a background task while waiting for tempdb to complete startup.|  
|SLEEP_TASK|Occurs when a task sleeps while waiting for a generic event to occur.|  
|SLEEP_TEMPDBSTARTUP|Occurs while a task waits for tempdb to complete startup.|  
|SNI_CRITICAL_SECTION|Occurs during internal synchronization within [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] networking components.|  
|SNI_HTTP_WAITFOR_0_DISCON|Occurs during [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] shutdown, while waiting for outstanding HTTP connections to exit.|  
|SNI_LISTENER_ACCESS|Occurs while waiting for non-uniform memory access (NUMA) nodes to update state change. Access to state change is serialized.|  
|SNI_TASK_COMPLETION|Occurs when there is a wait for all tasks to finish during a NUMA node state change.|  
|SOAP_READ|Occurs while waiting for an HTTP network read to complete.|  
|SOAP_WRITE|Occurs while waiting for an HTTP network write to complete.|  
|SOS_CALLBACK_REMOVAL|Occurs while performing synchronization on a callback list in order to remove a callback. It is not expected for this counter to change after server initialization is completed.|  
|SOS_DISPATCHER_MUTEX|Occurs during internal synchronization of the dispatcher pool. This includes when the pool is being adjusted.|  
|SOS_LOCALALLOCATORLIST|Occurs during internal synchronization in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] memory manager.|  
|SOS_MEMORY_USAGE_ADJUSTMENT|Occurs when memory usage is being adjusted among pools.|  
|SOS_OBJECT_STORE_DESTROY_MUTEX|Occurs during internal synchronization in memory pools when destroying objects from the pool.|  
|SOS_PROCESS_AFFINITY_MUTEX|Occurs during synchronizing of access to process affinity settings.|  
|SOS_RESERVEDMEMBLOCKLIST|Occurs during internal synchronization in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] memory manager.|  
|SOS_SCHEDULER_YIELD|Occurs when a task voluntarily yields the scheduler for other tasks to execute. During this wait the task is waiting for its quantum to be renewed.|  
|SOS_SMALL_PAGE_ALLOC|Occurs during the allocation and freeing of memory that is managed by some memory objects.|  
|SOS_STACKSTORE_INIT_MUTEX|Occurs during synchronization of internal store initialization.|  
|SOS_SYNC_TASK_ENQUEUE_EVENT|Occurs when a task is started in a synchronous manner. Most tasks in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are started in an asynchronous manner, in which control returns to the starter immediately after the task request has been placed on the work queue.|  
|SOS_VIRTUALMEMORY_LOW|Occurs when a memory allocation waits for a resource manager to free up virtual memory.|  
|SOSHOST_EVENT|Occurs when a hosted component, such as CLR, waits on a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] event synchronization object.|  
|SOSHOST_INTERNAL|Occurs during synchronization of memory manager callbacks used by hosted components, such as CLR.|  
|SOSHOST_MUTEX|Occurs when a hosted component, such as CLR, waits on a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] mutex synchronization object.|  
|SOSHOST_RWLOCK|Occurs when a hosted component, such as CLR, waits on a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] reader-writer synchronization object.|  
|SOSHOST_SEMAPHORE|Occurs when a hosted component, such as CLR, waits on a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] semaphore synchronization object.|  
|SOSHOST_SLEEP|Occurs when a hosted task sleeps while waiting for a generic event to occur. Hosted tasks are used by hosted components such as CLR.|  
|SOSHOST_TRACELOCK|Occurs during synchronization of access to trace streams.|  
|SOSHOST_WAITFORDONE|Occurs when a hosted component, such as CLR, waits for a task to complete.|  
|SQLCLR_APPDOMAIN|Occurs while CLR waits for an application domain to complete startup.|  
|SQLCLR_ASSEMBLY|Occurs while waiting for access to the loaded assembly list in the appdomain.|  
|SQLCLR_DEADLOCK_DETECTION|Occurs while CLR waits for deadlock detection to complete.|  
|SQLCLR_QUANTUM_PUNISHMENT|Occurs when a CLR task is throttled because it has exceeded its execution quantum. This throttling is done in order to reduce the effect of this resource-intensive task on other tasks.|  
|SQLSORT_NORMMUTEX|Occurs during internal synchronization, while initializing internal sorting structures.|  
|SQLSORT_SORTMUTEX|Occurs during internal synchronization, while initializing internal sorting structures.|  
|SQLTRACE_BUFFER_FLUSH|Occurs when a task is waiting for a background task to flush trace buffers to disk every four seconds.|  
|SQLTRACE_LOCK|Occurs during synchronization on trace buffers during a file trace.|  
|SQLTRACE_SHUTDOWN|Occurs while trace shutdown waits for outstanding trace events to complete.|  
|SQLTRACE_WAIT_ENTRIES|Occurs while a SQL Trace event queue waits for packets to arrive on the queue.|  
|SRVPROC_SHUTDOWN|Occurs while the shutdown process waits for internal resources to be released to shutdown cleanly.|  
|TEMPOBJ|Occurs when temporary object drops are synchronized. This wait is rare, and only occurs if a task has requested exclusive access for temp table drops.|  
|THREADPOOL|Occurs when a task is waiting for a worker to run on. This can indicate that the maximum worker setting is too low, or that batch executions are taking unusually long, thus reducing the number of workers available to satisfy other batches.|  
|TIMEPRIV_TIMEPERIOD|Occurs during internal synchronization of the Extended Events timer.|  
|TRACEWRITE|Occurs when the SQL Trace rowset trace provider waits for either a free buffer or a buffer with events to process.|  
|TRAN_MARKLATCH_DT|Occurs when waiting for a destroy mode latch on a transaction mark latch. Transaction mark latches are used for synchronization of commits with marked transactions.|  
|TRAN_MARKLATCH_EX|Occurs when waiting for an exclusive mode latch on a marked transaction. Transaction mark latches are used for synchronization of commits with marked transactions.|  
|TRAN_MARKLATCH_KP|Occurs when waiting for a keep mode latch on a marked transaction. Transaction mark latches are used for synchronization of commits with marked transactions.|  
|TRAN_MARKLATCH_NL|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|TRAN_MARKLATCH_SH|Occurs when waiting for a shared mode latch on a marked transaction. Transaction mark latches are used for synchronization of commits with marked transactions.|  
|TRAN_MARKLATCH_UP|Occurs when waiting for an update mode latch on a marked transaction. Transaction mark latches are used for synchronization of commits with marked transactions.|  
|TRANSACTION_MUTEX|Occurs during synchronization of access to a transaction by multiple batches.|  
|UTIL_PAGE_ALLOC|Occurs when transaction log scans wait for memory to be available during memory pressure.|  
|VIA_ACCEPT|Occurs when a Virtual Interface Adapter (VIA) provider connection is completed during startup.|  
|VIEW_DEFINITION_MUTEX|Occurs during synchronization on access to cached view definitions.|  
|WAIT_FOR_RESULTS|Occurs when waiting for a query notification to be triggered.|  
|WAITFOR|Occurs as a result of a WAITFOR [!INCLUDE[tsql](../../includes/tsql-md.md)] statement. The duration of the wait is determined by the parameters to the statement. This is a user-initiated wait.|  
|WAITFOR_TASKSHUTDOWN|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|WAITSTAT_MUTEX|Occurs during synchronization of access to the collection of statistics used to populate sys.dm_os_wait_stats.|  
|WCC|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|WORKTBL_DROP|Occurs while pausing before retrying, after a failed worktable drop.|  
|WRITE_COMPLETION|Occurs when a write operation is in progress.|  
|WRITELOG|Occurs while waiting for a log flush to complete. Common operations that cause log flushes are checkpoints and transaction commits.|  
|XACT_OWN_TRANSACTION|Occurs while waiting to acquire ownership of a transaction.|  
|XACT_RECLAIM_SESSION|Occurs while waiting for the current owner of a session to release ownership of the session.|  
|XACTLOCKINFO|Occurs during synchronization of access to the list of locks for a transaction. In addition to the transaction itself, the list of locks is accessed by operations such as deadlock detection and lock migration during page splits.|  
|XACTWORKSPACE_MUTEX|Occurs during synchronization of defections from a transaction, as well as the number of database locks between enlist members of a transaction.|  
|XE_BUFFERMGR_ALLPROCESSED_EVENT|Occurs when Extended Events session buffers are flushed to targets. This wait occurs on a background thread.|  
|XE_BUFFERMGR_FREEBUF_EVENT|Occurs when either of the following conditions is true:<br /><br /> An Extended Events session is configured for no event loss, and all buffers in the session are currently full. This can indicate that the buffers for an Extended Events session are too small, or should be partitioned.<br /><br /> Audits experience a delay. This can indicate a disk bottleneck on the drive where the audits are written.|  
|XE_DISPATCHER_CONFIG_SESSION_LIST|Occurs when an Extended Events session that is using asynchronous targets is started or stopped. This wait indicates either of the following:<br /><br /> An Extended Events session is registering with a background thread pool.<br /><br /> The background thread pool is calculating the required number of threads based on current load.|  
|XE_DISPATCHER_JOIN|Occurs when a background thread that is used for Extended Events sessions is terminating.|  
|XE_DISPATCHER_WAIT|Occurs when a background thread that is used for Extended Events sessions is waiting for event buffers to process.|  
|XE_MODULEMGR_SYNC|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|XE_OLS_LOCK|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|XE_PACKAGE_LOCK_BACKOFF|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|FT_COMPROWSET_RWLOCK|Full-text is waiting on fragment metadata operation. Documented for informational purposes only. Not supported. Future compatibility is not guaranteed.|  
|FT_IFTS_RWLOCK|Full-text is waiting on internal synchronization. Documented for informational purposes only. Not supported. Future compatibility is not guaranteed.|  
|FT_IFTS_SCHEDULER_IDLE_WAIT|Full-text scheduler sleep wait type. The scheduler is idle.|  
|FT_IFTSHC_MUTEX|Full-text is waiting on an fdhost control operation. Documented for informational purposes only. Not supported. Future compatibility is not guaranteed.|  
|FT_IFTSISM_MUTEX|Full-text is waiting on communication operation. Documented for informational purposes only. Not supported. Future compatibility is not guaranteed.|  
|FT_MASTER_MERGE|Full-text is waiting on master merge operation. Documented for informational purposes only. Not supported. Future compatibility is not guaranteed.|  
  
  
