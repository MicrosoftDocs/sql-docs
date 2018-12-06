---
title: "sys.dm_os_wait_stats (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "12/04/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "dm_os_wait_stats_TSQL"
  - "dm_os_wait_stats"
  - "sys.dm_os_wait_stats"
  - "sys.dm_os_wait_stats_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_os_wait_stats dynamic management view"
ms.assetid: 568d89ed-2c96-4795-8a0c-2f3e375081da
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_os_wait_stats (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

Returns information about all the waits encountered by threads that executed. You can use this aggregated view to diagnose performance issues with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and also with specific queries and batches. [sys.dm_exec_session_wait_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-session-wait-stats-transact-sql.md) provides similar information by session.  
  
> [!NOTE] 
> To call this from **[!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]**, use the name **sys.dm_pdw_nodes_os_wait_stats**.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|wait_type|**nvarchar(60)**|Name of the wait type. For more information, see [Types of Waits](#WaitTypes), later in this topic.|  
|waiting_tasks_count|**bigint**|Number of waits on this wait type. This counter is incremented at the start of each wait.|  
|wait_time_ms|**bigint**|Total wait time for this wait type in milliseconds. This time is inclusive of signal_wait_time_ms.|  
|max_wait_time_ms|**bigint**|Maximum wait time on this wait type.|  
|signal_wait_time_ms|**bigint**|Difference between the time that the waiting thread was signaled and when it started running.|  
|pdw_node_id|**int**|The identifier for the node that this distribution is on. <br/> **Applies to**: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] |  
  
## Permissions

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)], requires `VIEW SERVER STATE` permission.   
On [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)], requires the `VIEW DATABASE STATE` permission in the database.   

##  <a name="WaitTypes"></a> Types of Waits  
 **Resource waits** 
 Resource waits occur when a worker requests access to a resource that is not available because the resource is being used by some other worker or is not yet available. Examples of resource waits are locks, latches, network and disk I/O waits. Lock and latch waits are waits on synchronization objects  
  
**Queue waits**  
 Queue waits occur when a worker is idle, waiting for work to be assigned. Queue waits are most typically seen with system background tasks such as the deadlock monitor and deleted record cleanup tasks. These tasks will wait for work requests to be placed into a work queue. Queue waits may also periodically become active even if no new packets have been put on the queue.  
  
 **External waits**  
 External waits occur when a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] worker is waiting for an external event, such as an extended stored procedure call or a linked server query, to finish. When you diagnose blocking issues, remember that external waits do not always imply that the worker is idle, because the worker may actively be running some external code.  
  
 `sys.dm_os_wait_stats` shows the time for waits that have completed. This dynamic management view does not show current waits.  
  
 A [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] worker thread is not considered to be waiting if any of the following is true:  
  
-   A resource becomes available.  
  
-   A queue is nonempty.  
  
-   An external process finishes.  
  
 Although the thread is no longer waiting, the thread does not have to start running immediately. This is because such a thread is first put on the queue of runnable workers and must wait for a quantum to run on the scheduler.  
  
 In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] the wait-time counters are **bigint** values and therefore are not as prone to counter rollover as the equivalent counters in earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 Specific types of wait times during query execution can indicate bottlenecks or stall points within the query. Similarly, high wait times, or wait counts server wide can indicate bottlenecks or hot spots in interaction query interactions within the server instance. For example, lock waits indicate data contention by queries; page IO latch waits indicate slow IO response times; page latch update waits indicate incorrect file layout.  
  
 The contents of this dynamic management view can be reset by running the following command:  
  
```sql  
DBCC SQLPERF ('sys.dm_os_wait_stats', CLEAR);  
GO  
```  
  
This command resets all counters to 0.  
  
> [!NOTE]
> These statistics are not persisted across [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] restarts, and all data is cumulative since the last time the statistics were reset or the server was started.  
  
 The following table lists the wait types encountered by tasks.  

|type |Description| 
|-------------------------- |--------------------------| 
|ABR |Identified for informational purposes only. Not supported. Future compatibility is not guaranteed.| | 
|AM_INDBUILD_ALLOCATION |TBD <br />**Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|AM_SCHEMAMGR_UNSHARED_CACHE |TBD <br />**Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|ASSEMBLY_FILTER_HASHTABLE |TBD <br />**Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|ASSEMBLY_LOAD |Occurs during exclusive access to assembly loading.| 
|ASYNC_DISKPOOL_LOCK |Occurs when there is an attempt to synchronize parallel threads that are performing tasks such as creating or initializing a file.| 
|ASYNC_IO_COMPLETION |Occurs when a task is waiting for I/Os to finish.| 
|ASYNC_NETWORK_IO |Occurs on network writes when the task is blocked behind the network. Verify that the client is processing data from the server.| 
|ASYNC_OP_COMPLETION |TBD <br />**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|ASYNC_OP_CONTEXT_READ |TBD <br />**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|ASYNC_OP_CONTEXT_WRITE |TBD <br />**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|ASYNC_SOCKETDUP_IO |TBD <br />**Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|AUDIT_GROUPCACHE_LOCK |Occurs when there is a wait on a lock that controls access to a special cache. The cache contains information about which audits are being used to audit each audit action group.| 
|AUDIT_LOGINCACHE_LOCK |Occurs when there is a wait on a lock that controls access to a special cache. The cache contains information about which audits are being used to audit login audit action groups.| 
|AUDIT_ON_DEMAND_TARGET_LOCK |Occurs when there is a wait on a lock that is used to ensure single initialization of audit related Extended Event targets.| 
|AUDIT_XE_SESSION_MGR |Occurs when there is a wait on a lock that is used to synchronize the starting and stopping of audit related Extended Events sessions.| 
|BACKUP |Occurs when a task is blocked as part of backup processing.| 
|BACKUP_OPERATOR |Occurs when a task is waiting for a tape mount. To view the tape status, query sys.dm_io_backup_tapes. If a mount operation is not pending, this wait type may indicate a hardware problem with the tape drive.| 
|BACKUPBUFFER |Occurs when a backup task is waiting for data, or is waiting for a buffer in which to store data. This type is not typical, except when a task is waiting for a tape mount.| 
|BACKUPIO |Occurs when a backup task is waiting for data, or is waiting for a buffer in which to store data. This type is not typical, except when a task is waiting for a tape mount.| 
|BACKUPTHREAD |Occurs when a task is waiting for a backup task to finish. Wait times may be long, from several minutes to several hours. If the task that is being waited on is in an I/O process, this type does not indicate a problem.| 
|BAD_PAGE_PROCESS |Occurs when the background suspect page logger is trying to avoid running more than every five seconds. Excessive suspect pages cause the logger to run frequently.| 
|BLOB_METADATA |TBD <br />**Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|BMPALLOCATION |TBD <br />**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|BMPBUILD |TBD <br />**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|BMPREPARTITION |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|BMPREPLICATION |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|BPSORT |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|BROKER_CONNECTION_RECEIVE_TASK |Occurs when waiting for access to receive a message on a connection endpoint. Receive access to the endpoint is serialized.| 
|BROKER_DISPATCHER |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|BROKER_ENDPOINT_STATE_MUTEX |Occurs when there is contention to access the state of a Service Broker connection endpoint. Access to the state for changes is serialized.| 
|BROKER_EVENTHANDLER |Occurs when a task is waiting in the primary event handler of the Service Broker. This should occur very briefly.| 
|BROKER_FORWARDER |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|BROKER_INIT |Occurs when initializing Service Broker in each active database. This should occur infrequently.| 
|BROKER_MASTERSTART |Occurs when a task is waiting for the primary event handler of the Service Broker to start. This should occur very briefly.| 
|BROKER_RECEIVE_WAITFOR |Occurs when the RECEIVE WAITFOR is waiting. This may mean that either no messages are ready to be received in the queue or a lock contention is preventing it from receiving messages from the queue.| 
|BROKER_REGISTERALLENDPOINTS |Occurs during the initialization of a Service Broker connection endpoint. This should occur very briefly.| 
|BROKER_SERVICE |Occurs when the Service Broker destination list that is associated with a target service is updated or re-prioritized.| 
|BROKER_SHUTDOWN |Occurs when there is a planned shutdown of Service Broker. This should occur very briefly, if at all.| 
|BROKER_START |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|BROKER_TASK_SHUTDOWN |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|BROKER_TASK_STOP |Occurs when the Service Broker queue task handler tries to shut down the task. The state check is serialized and must be in a running state beforehand.| 
|BROKER_TASK_SUBMIT |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|BROKER_TO_FLUSH |Occurs when the Service Broker lazy flusher flushes the in-memory transmission objects to a work table.| 
|BROKER_TRANSMISSION_OBJECT |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|BROKER_TRANSMISSION_TABLE |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|BROKER_TRANSMISSION_WORK |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|BROKER_TRANSMITTER |Occurs when the Service Broker transmitter is waiting for work. Service Broker has a component known as the Transmitter which schedules messages from multiple dialogs to be sent across the wire over one or more connection endpoints. The transmitter has 2 dedicated threads for this purpose. This wait type is charged when these transmitter threads are waiting for dialog messages to be sent using the transport connections. High values of waiting_tasks_count for this wait type point to intermittent work for these transmitter threads and are not indications of any performance problem. If service broker is not used at all, waiting_tasks_count should be 2 (for the 2 transmitter threads) and wait_time_ms should be twice the duration since instance startup. See [Service broker wait stats](https://blogs.msdn.microsoft.com/sql_service_broker/2008/12/01/service-broker-wait-types).|
|BUILTIN_HASHKEY_MUTEX |May occur after startup of instance, while internal data structures are initializing. Will not recur once data structures have initialized.| 
|CHANGE_TRACKING_WAITFORCHANGES |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|CHECK_PRINT_RECORD |Identified for informational purposes only. Not supported. Future compatibility is not guaranteed.| 
|CHECK_SCANNER_MUTEX |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|CHECK_TABLES_INITIALIZATION |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|CHECK_TABLES_SINGLE_SCAN |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|CHECK_TABLES_THREAD_BARRIER |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|CHECKPOINT_QUEUE |Occurs while the checkpoint task is waiting for the next checkpoint request.| 
|CHKPT |Occurs at server startup to tell the checkpoint thread that it can start.| 
|CLEAR_DB |Occurs during operations that change the state of a database, such as opening or closing a database.| 
|CLR_AUTO_EVENT |Occurs when a task is currently performing common language runtime (CLR) execution and is waiting for a particular autoevent to be initiated. Long waits are typical, and do not indicate a problem.| 
|CLR_CRST |Occurs when a task is currently performing CLR execution and is waiting to enter a critical section of the task that is currently being used by another task.| 
|CLR_JOIN |Occurs when a task is currently performing CLR execution and waiting for another task to end. This wait state occurs when there is a join between tasks.| 
|CLR_MANUAL_EVENT |Occurs when a task is currently performing CLR execution and is waiting for a specific manual event to be initiated.| 
|CLR_MEMORY_SPY |Occurs during a wait on lock acquisition for a data structure that is used to record all virtual memory allocations that come from CLR. The data structure is locked to maintain its integrity if there is parallel access.| 
|CLR_MONITOR |Occurs when a task is currently performing CLR execution and is waiting to obtain a lock on the monitor.| 
|CLR_RWLOCK_READER |Occurs when a task is currently performing CLR execution and is waiting for a reader lock.| 
|CLR_RWLOCK_WRITER |Occurs when a task is currently performing CLR execution and is waiting for a writer lock.| 
|CLR_SEMAPHORE |Occurs when a task is currently performing CLR execution and is waiting for a semaphore.| 
|CLR_TASK_START |Occurs while waiting for a CLR task to complete startup.| 
|CLRHOST_STATE_ACCESS |Occurs where there is a wait to acquire exclusive access to the CLR-hosting data structures. This wait type occurs while setting up or tearing down the CLR runtime.| 
|CMEMPARTITIONED |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|CMEMTHREAD |Occurs when a task is waiting on a thread-safe memory object. The wait time might increase when there is contention caused by multiple tasks trying to allocate memory from the same memory object.| 
|COLUMNSTORE_BUILD_THROTTLE |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|COLUMNSTORE_COLUMNDATASET_SESSION_LIST |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|COMMIT_TABLE |TBD| 
|CONNECTION_ENDPOINT_LOCK |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|COUNTRECOVERYMGR |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|CREATE_DATINISERVICE |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|CXCONSUMER |Occurs with parallel query plans when a consumer thread waits for a producer thread to send rows. This is a normal part of parallel query execution. <br /> **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP2, [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] CU3), [!INCLUDE[ssSDS](../../includes/sssds-md.md)]|
|CXPACKET |Occurs with parallel query plans when synchronizing the query processor exchange iterator, and when producing and consuming rows. If waiting is excessive and cannot be reduced by tuning the query (such as adding indexes), consider adjusting the cost threshold for parallelism or lowering the degree of parallelism.<br /> **Note:** Starting with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP2, [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] CU3, and [!INCLUDE[ssSDS](../../includes/sssds-md.md)], CXPACKET only refers to synchronizing the query processor exchange iterator, and to producing rows for consumer threads. Consumer threads are tracked separately in the CXCONSUMER wait type.| 
|CXROWSET_SYNC |Occurs during a parallel range scan.| 
|DAC_INIT |Occurs while the dedicated administrator connection is initializing.| 
|DBCC_SCALE_OUT_EXPR_CACHE |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|DBMIRROR_DBM_EVENT |Identified for informational purposes only. Not supported. Future compatibility is not guaranteed.| 
|DBMIRROR_DBM_MUTEX |Identified for informational purposes only. Not supported. Future compatibility is not guaranteed.| 
|DBMIRROR_EVENTS_QUEUE |Occurs when database mirroring waits for events to process.| 
|DBMIRROR_SEND |Occurs when a task is waiting for a communications backlog at the network layer to clear to be able to send messages. Indicates that the communications layer is starting to become overloaded and affect the database mirroring data throughput.| 
|DBMIRROR_WORKER_QUEUE |Indicates that the database mirroring worker task is waiting for more work.| 
|DBMIRRORING_CMD |Occurs when a task is waiting for log records to be flushed to disk. This wait state is expected to be held for long periods of time.| 
|DBSEEDING_FLOWCONTROL |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|DBSEEDING_OPERATION |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|DEADLOCK_ENUM_MUTEX |Occurs when the deadlock monitor and sys.dm_os_waiting_tasks try to make sure that SQL Server is not running multiple deadlock searches at the same time.| 
|DEADLOCK_TASK_SEARCH |Large waiting time on this resource indicates that the server is executing queries on top of sys.dm_os_waiting_tasks, and these queries are blocking deadlock monitor from running deadlock search. This wait type is used by deadlock monitor only. Queries on top of sys.dm_os_waiting_tasks use DEADLOCK_ENUM_MUTEX.| 
|DEBUG |Occurs during Transact-SQL and CLR debugging for internal synchronization.| 
|DIRECTLOGCONSUMER_LIST |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|DIRTY_PAGE_POLL |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|DIRTY_PAGE_SYNC |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|DIRTY_PAGE_TABLE_LOCK |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|DISABLE_VERSIONING |Occurs when SQL Server polls the version transaction manager to see whether the timestamp of the earliest active transaction is later than the timestamp of when the state started changing. If this is this case, all the snapshot transactions that were started before the ALTER DATABASE statement was run have finished. This wait state is used when SQL Server disables versioning by using the ALTER DATABASE statement.| 
|DISKIO_SUSPEND |Occurs when a task is waiting to access a file when an external backup is active. This is reported for each waiting user process. A count larger than five per user process may indicate that the external backup is taking too much time to finish.| 
|DISPATCHER_PRIORITY_QUEUE_SEMAPHORE |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|DISPATCHER_QUEUE_SEMAPHORE |Occurs when a thread from the dispatcher pool is waiting for more work to process. The wait time for this wait type is expected to increase when the dispatcher is idle.| 
|DLL_LOADING_MUTEX |Occurs once while waiting for the XML parser DLL to load.| 
|DPT_ENTRY_LOCK |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|DROP_DATABASE_TIMER_TASK |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|DROPTEMP |Occurs between attempts to drop a temporary object if the previous attempt failed. The wait duration grows exponentially with each failed drop attempt.| 
|DTC |Occurs when a task is waiting on an event that is used to manage state transition. This state controls when the recovery of Microsoft Distributed Transaction Coordinator (MS DTC) transactions occurs after SQL Server receives notification that the MS DTC service has become unavailable.| 
|DTC_ABORT_REQUEST |Occurs in a MS DTC worker session when the session is waiting to take ownership of a MS DTC transaction. After MS DTC owns the transaction, the session can roll back the transaction. Generally, the session will wait for another session that is using the transaction.| 
|DTC_RESOLVE |Occurs when a recovery task is waiting for the master database in a cross-database transaction so that the task can query the outcome of the transaction.| 
|DTC_STATE |Occurs when a task is waiting on an event that protects changes to the internal MS DTC global state object. This state should be held for very short periods of time.| 
|DTC_TMDOWN_REQUEST |Occurs in a MS DTC worker session when SQL Server receives notification that the MS DTC service is not available. First, the worker will wait for the MS DTC recovery process to start. Then, the worker waits to obtain the outcome of the distributed transaction that the worker is working on. This may continue until the connection with the MS DTC service has been reestablished.| 
|DTC_WAITFOR_OUTCOME |Occurs when recovery tasks wait for MS DTC to become active to enable the resolution of prepared transactions.| 
|DTCNEW_ENLIST |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|DTCNEW_PREPARE |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|DTCNEW_RECOVERY |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|DTCNEW_TM |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|DTCNEW_TRANSACTION_ENLISTMENT |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|DTCPNTSYNC |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|DUMP_LOG_COORDINATOR |Occurs when a main task is waiting for a subtask to generate data. Ordinarily, this state does not occur. A long wait indicates an unexpected blockage. The subtask should be investigated.| 
|DUMP_LOG_COORDINATOR_QUEUE |TBD| 
|DUMPTRIGGER |Identified for informational purposes only. Not supported. Future compatibility is not guaranteed.| 
|EC |Identified for informational purposes only. Not supported. Future compatibility is not guaranteed.| 
|EE_PMOLOCK |Occurs during synchronization of certain types of memory allocations during statement execution.| 
|EE_SPECPROC_MAP_INIT |Occurs during synchronization of internal procedure hash table creation. This wait can only occur during the initial accessing of the hash table after the SQL Server instance starts.| 
|ENABLE_EMPTY_VERSIONING |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|ENABLE_VERSIONING |Occurs when SQL Server waits for all update transactions in this database to finish before declaring the database ready to transition to snapshot isolation allowed state. This state is used when SQL Server enables snapshot isolation by using the ALTER DATABASE statement.| 
|ERROR_REPORTING_MANAGER |Occurs during synchronization of multiple concurrent error log initializations.| 
|EXCHANGE |Occurs during synchronization in the query processor exchange iterator during parallel queries.| 
|EXECSYNC |Occurs during parallel queries while synchronizing in query processor in areas not related to the exchange iterator. Examples of such areas are bitmaps, large binary objects (LOBs), and the spool iterator. LOBs may frequently use this wait state.| 
|EXECUTION_PIPE_EVENT_INTERNAL |Occurs during synchronization between producer and consumer parts of batch execution that are submitted through the connection context.| 
|EXTERNAL_RG_UPDATE |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|EXTERNAL_SCRIPT_NETWORK_IO |TBD <br /> **Applies to**: [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] through current.| 
|EXTERNAL_SCRIPT_PREPARE_SERVICE |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|EXTERNAL_SCRIPT_SHUTDOWN |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|EXTERNAL_WAIT_ON_LAUNCHER, |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|FABRIC_HADR_TRANSPORT_CONNECTION |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|FABRIC_REPLICA_CONTROLLER_LIST |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|FABRIC_REPLICA_CONTROLLER_STATE_AND_CONFIG |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|FABRIC_REPLICA_PUBLISHER_EVENT_PUBLISH |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|FABRIC_REPLICA_PUBLISHER_SUBSCRIBER_LIST |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|FABRIC_WAIT_FOR_BUILD_REPLICA_EVENT_PROCESSING |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|FAILPOINT |Identified for informational purposes only. Not supported. Future compatibility is not guaranteed.| 
|FCB_REPLICA_READ |Occurs when the reads of a snapshot (or a temporary snapshot created by DBCC) sparse file are synchronized.| 
|FCB_REPLICA_WRITE |Occurs when the pushing or pulling of a page to a snapshot (or a temporary snapshot created by DBCC) sparse file is synchronized.| 
|FEATURE_SWITCHES_UPDATE |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|FFT_NSO_DB_KILL_FLAG |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|FFT_NSO_DB_LIST |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|FFT_NSO_FCB |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|FFT_NSO_FCB_FIND |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|FFT_NSO_FCB_PARENT |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|FFT_NSO_FCB_RELEASE_CACHED_ENTRIES |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|FFT_NSO_FCB_STATE |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|FFT_NSO_FILEOBJECT |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|FFT_NSO_TABLE_LIST |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|FFT_NTFS_STORE |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|FFT_RECOVERY |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|FFT_RSFX_COMM |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|FFT_RSFX_WAIT_FOR_MEMORY |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|FFT_STARTUP_SHUTDOWN |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|FFT_STORE_DB |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|FFT_STORE_ROWSET_LIST |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|FFT_STORE_TABLE |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|FILE_VALIDATION_THREADS |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|FILESTREAM_CACHE |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|FILESTREAM_CHUNKER |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|FILESTREAM_CHUNKER_INIT |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|FILESTREAM_FCB |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|FILESTREAM_FILE_OBJECT |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|FILESTREAM_WORKITEM_QUEUE |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|FILETABLE_SHUTDOWN |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|FOREIGN_REDO |TBD <br /> **Applies to**: [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] through current.| 
|FORWARDER_TRANSITION |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|FS_FC_RWLOCK |Occurs when there is a wait by the FILESTREAM garbage collector to do either of the following:| 
|FS_GARBAGE_COLLECTOR_SHUTDOWN |Occurs when the FILESTREAM garbage collector is waiting for cleanup tasks to be completed.| 
|FS_HEADER_RWLOCK |Occurs when there is a wait to acquire access to the FILESTREAM header of a FILESTREAM data container to either read or update contents in the FILESTREAM header file (Filestream.hdr).| 
|FS_LOGTRUNC_RWLOCK |Occurs when there is a wait to acquire access to FILESTREAM log truncation to do either of the following:| 
|FSA_FORCE_OWN_XACT |Occurs when a FILESTREAM file I/O operation needs to bind to the associated transaction, but the transaction is currently owned by another session.| 
|FSAGENT |Occurs when a FILESTREAM file I/O operation is waiting for a FILESTREAM agent resource that is being used by another file I/O operation.| 
|FSTR_CONFIG_MUTEX |Occurs when there is a wait for another FILESTREAM feature reconfiguration to be completed.| 
|FSTR_CONFIG_RWLOCK |Occurs when there is a wait to serialize access to the FILESTREAM configuration parameters.| 
|FT_COMPROWSET_RWLOCK |Full-text is waiting on fragment metadata operation. Documented for informational purposes only. Not supported. Future compatibility is not guaranteed.| 
|FT_IFTS_RWLOCK |Full-text is waiting on internal synchronization. Documented for informational purposes only. Not supported. Future compatibility is not guaranteed.| 
|FT_IFTS_SCHEDULER_IDLE_WAIT |Full-text scheduler sleep wait type. The scheduler is idle.| 
|FT_IFTSHC_MUTEX |Full-text is waiting on an fdhost control operation. Documented for informational purposes only. Not supported. Future compatibility is not guaranteed.| 
|FT_IFTSISM_MUTEX |Full-text is waiting on communication operation. Documented for informational purposes only. Not supported. Future compatibility is not guaranteed.| 
|FT_MASTER_MERGE |Full-text is waiting on master merge operation. Documented for informational purposes only. Not supported. Future compatibility is not guaranteed.| 
|FT_MASTER_MERGE_COORDINATOR |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|FT_METADATA_MUTEX |Documented for informational purposes only. Not supported. Future compatibility is not guaranteed.| 
|FT_PROPERTYLIST_CACHE |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|FT_RESTART_CRAWL |Occurs when a full-text crawl needs to restart from a last known good point to recover from a transient failure. The wait lets the worker tasks currently working on that population to complete or exit the current step.| 
|FULLTEXT GATHERER |Occurs during synchronization of full-text operations.| 
|GDMA_GET_RESOURCE_OWNER |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|GHOSTCLEANUP_UPDATE_STATS |TBD <br /> **Applies to**: [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|GHOSTCLEANUPSYNCMGR |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|GLOBAL_QUERY_CANCEL |TBD <br /> **Applies to**: [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|GLOBAL_QUERY_CLOSE |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|GLOBAL_QUERY_CONSUMER |TBD <br /> **Applies to**: [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|GLOBAL_QUERY_PRODUCER |TBD <br /> **Applies to**: [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|GLOBAL_TRAN_CREATE |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|GLOBAL_TRAN_UCS_SESSION |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|GUARDIAN |Identified for informational purposes only. Not supported. Future compatibility is not guaranteed.| 
|HADR_AG_MUTEX |Occurs when an Always On DDL statement or Windows Server Failover Clustering command is waiting for exclusive read/write access to the configuration of an availability group., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_AR_CRITICAL_SECTION_ENTRY |Occurs when an Always On DDL statement or Windows Server Failover Clustering command is waiting for exclusive read/write access to the runtime state of the local replica of the associated availability group., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_AR_MANAGER_MUTEX |Occurs when an availability replica shutdown is waiting for startup to complete or an availability replica startup is waiting for shutdown to complete. Internal use only., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_AR_UNLOAD_COMPLETED |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_ARCONTROLLER_NOTIFICATIONS_SUBSCRIBER_LIST |The publisher for an availability replica event (such as a state change or configuration change) is waiting for exclusive read/write access to the list of event subscribers. Internal use only., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_BACKUP_BULK_LOCK |The Always On primary database received a backup request from a secondary database and is waiting for the background thread to finish processing the request on acquiring or releasing the BulkOp lock., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_BACKUP_QUEUE |The backup background thread of the Always On primary database is waiting for a new work request from the secondary database. (typically, this occurs when the primary database is holding the BulkOp log and is waiting for the secondary database to indicate that the primary database can release the lock)., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_CLUSAPI_CALL |A SQL Server thread is waiting to switch from non-preemptive mode (scheduled by SQL Server) to preemptive mode (scheduled by the operating system) in order to invoke Windows Server Failover Clustering APIs., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_COMPRESSED_CACHE_SYNC |Waiting for access to the cache of compressed log blocks that is used to avoid redundant compression of the log blocks sent to multiple secondary databases., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_CONNECTIVITY_INFO |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_DATABASE_FLOW_CONTROL |Waiting for messages to be sent to the partner when the maximum number of queued messages has been reached. Indicates that the log scans are running faster than the network sends. This is an issue only if network sends are slower than expected., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_DATABASE_VERSIONING_STATE |Occurs on the versioning state change of an Always On secondary database. This wait is for internal data structures and is usually is very short with no direct effect on data access., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_DATABASE_WAIT_FOR_RECOVERY |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_DATABASE_WAIT_FOR_RESTART |Waiting for the database to restart under Always On Availability Groups control. Under normal conditions, this is not a customer issue because waits are expected here., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_DATABASE_WAIT_FOR_TRANSITION_TO_VERSIONING |A query on object(s) in a readable secondary database of an Always On availability group is blocked on row versioning while waiting for commit or rollback of all transactions that were in-flight when the secondary replica was enabled for read workloads. This wait type guarantees that row versions are available before execution of a query under snapshot isolation., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_DB_COMMAND |Waiting for responses to conversational messages (which require an explicit response from the other side, using the Always On conversational message infrastructure). A number of different message types use this wait type., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_DB_OP_COMPLETION_SYNC |Waiting for responses to conversational messages (which require an explicit response from the other side, using the Always On conversational message infrastructure). A number of different message types use this wait type., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_DB_OP_START_SYNC |An Always On DDL statement or a Windows Server Failover Clustering command is waiting for serialized access to an availability database and its runtime state., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_DBR_SUBSCRIBER |The publisher for an availability replica event (such as a state change or configuration change) is waiting for exclusive read/write access to the runtime state of an event subscriber that corresponds to an availability database. Internal use only., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_DBR_SUBSCRIBER_FILTER_LIST |The publisher for an availability replica event (such as a state change or configuration change) is waiting for exclusive read/write access to the list of event subscribers that correspond to availability databases. Internal use only., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_DBSEEDING |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_DBSEEDING_LIST |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_DBSTATECHANGE_SYNC |Concurrency control wait for updating the internal state of the database replica., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_FABRIC_CALLBACK |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_FILESTREAM_BLOCK_FLUSH |The FILESTREAM Always On transport manager is waiting until processing of a log block is finished., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_FILESTREAM_FILE_CLOSE |The FILESTREAM Always On transport manager is waiting until the next FILESTREAM file gets processed and its handle gets closed., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_FILESTREAM_FILE_REQUEST |An Always On secondary replica is waiting for the primary replica to send all requested FILESTREAM files during UNDO., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_FILESTREAM_IOMGR |The FILESTREAM Always On transport manager is waiting for R/W lock that protects the FILESTREAM Always On I/O manager during startup or shutdown., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_FILESTREAM_IOMGR_IOCOMPLETION |The FILESTREAM Always On I/O manager is waiting for I/O completion., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_FILESTREAM_MANAGER |The FILESTREAM Always On transport manager is waiting for the R/W lock that protects the FILESTREAM Always On transport manager during startup or shutdown., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_FILESTREAM_PREPROC |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_GROUP_COMMIT |Transaction commit processing is waiting to allow a group commit so that multiple commit log records can be put into a single log block. This wait is an expected condition that optimizes the log I/O, capture, and send operations., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_LOGCAPTURE_SYNC |Concurrency control around the log capture or apply object when creating or destroying scans. This is an expected wait when partners change state or connection status., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_LOGCAPTURE_WAIT |Waiting for log records to become available. Can occur either when waiting for new log records to be generated by connections or for I/O completion when reading log not in the cache. This is an expected wait if the log scan is caught up to the end of log or is reading from disk., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_LOGPROGRESS_SYNC |Concurrency control wait when updating the log progress status of database replicas., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_NOTIFICATION_DEQUEUE |A background task that processes Windows Server Failover Clustering notifications is waiting for the next notification. Internal use only., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_NOTIFICATION_WORKER_EXCLUSIVE_ACCESS |The Always On availability replica manager is waiting for serialized access to the runtime state of a background task that processes Windows Server Failover Clustering notifications. Internal use only., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_NOTIFICATION_WORKER_STARTUP_SYNC |A background task is waiting for the completion of the startup of a background task that processes Windows Server Failover Clustering notifications. Internal use only., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_NOTIFICATION_WORKER_TERMINATION_SYNC |A background task is waiting for the termination of a background task that processes Windows Server Failover Clustering notifications. Internal use only., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_PARTNER_SYNC |Concurrency control wait on the partner list., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_READ_ALL_NETWORKS |Waiting to get read or write access to the list of WSFC networks. Internal use only. Note: The engine keeps a list of WSFC networks that is used in dynamic management views (such as sys.dm_hadr_cluster_networks) or to validate Always On Transact-SQL statements that reference WSFC network information. This list is updated upon engine startup, WSFC related notifications, and internal Always On restart (for example, losing and regaining of WSFC quorum). Tasks will usually be blocked when an update in that list is in progress. , <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_RECOVERY_WAIT_FOR_CONNECTION |Waiting for the secondary database to connect to the primary database before running recovery. This is an expected wait, which can lengthen if the connection to the primary is slow to establish., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_RECOVERY_WAIT_FOR_UNDO |Database recovery is waiting for the secondary database to finish the reverting and initializing phase to bring it back to the common log point with the primary database. This is an expected wait after failovers.Undo progress can be tracked through the Windows System Monitor (perfmon.exe) and dynamic management views., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_REPLICAINFO_SYNC |Waiting for concurrency control to update the current replica state., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_SEEDING_CANCELLATION |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_SEEDING_FILE_LIST |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_SEEDING_LIMIT_BACKUPS |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_SEEDING_SYNC_COMPLETION |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_SEEDING_TIMEOUT_TASK |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_SEEDING_WAIT_FOR_COMPLETION |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_SYNC_COMMIT |Waiting for transaction commit processing for the synchronized secondary databases to harden the log. This wait is also reflected by the Transaction Delay performance counter. This wait type is expected for synchronized availability groups and indicates the time to send, write, and acknowledge log to the secondary databases., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_SYNCHRONIZING_THROTTLE |Waiting for transaction commit processing to allow a synchronizing secondary database to catch up to the primary end of log in order to transition to the synchronized state. This is an expected wait when a secondary database is catching up., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_TDS_LISTENER_SYNC |Either the internal Always On system or the WSFC cluster will request that listeners are started or stopped. The processing of this request is always asynchronous, and there is a mechanism to remove redundant requests. There are also moments that this process is suspended because of configuration changes. All waits related with this listener synchronization mechanism use this wait type. Internal use only., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_TDS_LISTENER_SYNC_PROCESSING |Used at the end of an Always On Transact-SQL statement that requires starting and/or stopping anavailability group listener. Since the start/stop operation is done asynchronously, the user thread will block using this wait type until the situation of the listener is known., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_THROTTLE_LOG_RATE_GOVERNOR |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_THROTTLE_LOG_RATE_LOG_SIZE |TBD <br /> **Applies to**: [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_THROTTLE_LOG_RATE_SEEDING |TBD <br /> **Applies to**: [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_THROTTLE_LOG_RATE_SEND_RECV_QUEUE_SIZE |TBD <br /> **Applies to**: [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_TIMER_TASK |Waiting to get the lock on the timer task object and is also used for the actual waits between times that work is being performed. For example, for a task that runs every 10 seconds, after one execution, Always On Availability Groups waits about 10 seconds to reschedule the task, and the wait is included here., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_TRANSPORT_DBRLIST |Waiting for access to the transport layer's database replica list. Used for the spinlock that grants access to it., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_TRANSPORT_FLOW_CONTROL |Waiting when the number of outstanding unacknowledged Always On messages is over the out flow control threshold. This is on an availability replica-to-replica basis (not on a database-to-database basis)., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_TRANSPORT_SESSION |Always On Availability Groups is waiting while changing or accessing the underlying transport state., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_WORK_POOL |Concurrency control wait on the Always On Availability Groups background work task object., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_WORK_QUEUE |Always On Availability Groups background worker thread waiting for new work to be assigned. This is an expected wait when there are ready workers waiting for new work, which is the normal state., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HADR_XRF_STACK_ACCESS |Accessing (look up, add, and delete) the extended recovery fork stack for an Always On availability database., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HCCO_CACHE |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HK_RESTORE_FILEMAP |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HKCS_PARALLEL_MIGRATION |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HKCS_PARALLEL_RECOVERY |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HTBUILD |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HTDELETE |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HTMEMO |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HTREINIT |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HTREPARTITION |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|HTTP_ENUMERATION |Occurs at startup to enumerate the HTTP endpoints to start HTTP.| 
|HTTP_START |Occurs when a connection is waiting for HTTP to complete initialization.| 
|HTTP_STORAGE_CONNECTION |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|IMPPROV_IOWAIT |Occurs when SQL Server waits for a bulkload I/O to finish.| 
|INSTANCE_LOG_RATE_GOVERNOR |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|INTERNAL_TESTING |Identified for informational purposes only. Not supported. Future compatibility is not guaranteed.| 
|IO_AUDIT_MUTEX |Occurs during synchronization of trace event buffers.| 
|IO_COMPLETION |Occurs while waiting for I/O operations to complete. This wait type generally represents non-data page I/Os. Data page I/O completion waits appear as PAGEIOLATCH\_\* waits.| 
|IO_QUEUE_LIMIT |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|IO_RETRY |Occurs when an I/O operation such as a read or a write to disk fails because of insufficient resources, and is then retried.| 
|IOAFF_RANGE_QUEUE |Identified for informational purposes only. Not supported. Future compatibility is not guaranteed.| 
|KSOURCE_WAKEUP |Used by the service control task while waiting for requests from the Service Control Manager. Long waits are expected and do not indicate a problem.| 
|KTM_ENLISTMENT |Identified for informational purposes only. Not supported. Future compatibility is not guaranteed.| 
|KTM_RECOVERY_MANAGER |Identified for informational purposes only. Not supported. Future compatibility is not guaranteed.| 
|KTM_RECOVERY_RESOLUTION |Identified for informational purposes only. Not supported. Future compatibility is not guaranteed.| 
|LATCH_DT |Occurs when waiting for a DT (destroy) latch. This does not include buffer latches or transaction mark latches. A listing of LATCH\_\* waits is available in sys.dm_os_latch_stats. Note that sys.dm_os_latch_stats groups LATCH_NL, LATCH_SH, LATCH_UP, LATCH_EX, and LATCH_DT waits together.| 
|LATCH_EX |Occurs when waiting for an EX (exclusive) latch. This does not include buffer latches or transaction mark latches. A listing of LATCH\_\* waits is available in sys.dm_os_latch_stats. Note that sys.dm_os_latch_stats groups LATCH_NL, LATCH_SH, LATCH_UP, LATCH_EX, and LATCH_DT waits together.| 
|LATCH_KP |Occurs when waiting for a KP (keep) latch. This does not include buffer latches or transaction mark latches. A listing of LATCH\_\* waits is available in sys.dm_os_latch_stats. Note that sys.dm_os_latch_stats groups LATCH_NL, LATCH_SH, LATCH_UP, LATCH_EX, and LATCH_DT waits together.| 
|LATCH_NL |Identified for informational purposes only. Not supported. Future compatibility is not guaranteed.| 
|LATCH_SH |Occurs when waiting for an SH (share) latch. This does not include buffer latches or transaction mark latches. A listing of LATCH\_\* waits is available in sys.dm_os_latch_stats. Note that sys.dm_os_latch_stats groups LATCH_NL, LATCH_SH, LATCH_UP, LATCH_EX, and LATCH_DT waits together.| 
|LATCH_UP |Occurs when waiting for an UP (update) latch. This does not include buffer latches or transaction mark latches. A listing of LATCH\_\* waits is available in sys.dm_os_latch_stats. Note that sys.dm_os_latch_stats groups LATCH_NL, LATCH_SH, LATCH_UP, LATCH_EX, and LATCH_DT waits together.| 
|LAZYWRITER_SLEEP |Occurs when lazywriter tasks are suspended. This is a measure of the time spent by background tasks that are waiting. Do not consider this state when you are looking for user stalls.| 
|LCK_M_BU |Occurs when a task is waiting to acquire a Bulk Update (BU) lock.| 
|LCK_M_BU_ABORT_BLOCKERS |Occurs when a task is waiting to acquire a Bulk Update (BU) lock with Abort Blockers. (Related to the low priority wait option of ALTER TABLE and ALTER INDEX.), <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LCK_M_BU_LOW_PRIORITY |Occurs when a task is waiting to acquire a Bulk Update (BU) lock with Low Priority. (Related to the low priority wait option of ALTER TABLE and ALTER INDEX.), <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LCK_M_IS |Occurs when a task is waiting to acquire an Intent Shared (IS) lock.| 
|LCK_M_IS_ABORT_BLOCKERS |Occurs when a task is waiting to acquire an Intent Shared (IS) lock with Abort Blockers. (Related to the low priority wait option of ALTER TABLE and ALTER INDEX.), <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LCK_M_IS_LOW_PRIORITY |Occurs when a task is waiting to acquire an Intent Shared (IS) lock with Low Priority. (Related to the low priority wait option of ALTER TABLE and ALTER INDEX.), <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LCK_M_IU |Occurs when a task is waiting to acquire an Intent Update (IU) lock.| 
|LCK_M_IU_ABORT_BLOCKERS |Occurs when a task is waiting to acquire an Intent Update (IU) lock with Abort Blockers. (Related to the low priority wait option of ALTER TABLE and ALTER INDEX.), <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LCK_M_IU_LOW_PRIORITY |Occurs when a task is waiting to acquire an Intent Update (IU) lock with Low Priority. (Related to the low priority wait option of ALTER TABLE and ALTER INDEX.), <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LCK_M_IX |Occurs when a task is waiting to acquire an Intent Exclusive (IX) lock.| 
|LCK_M_IX_ABORT_BLOCKERS |Occurs when a task is waiting to acquire an Intent Exclusive (IX) lock with Abort Blockers. (Related to the low priority wait option of ALTER TABLE and ALTER INDEX.), <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LCK_M_IX_LOW_PRIORITY |Occurs when a task is waiting to acquire an Intent Exclusive (IX) lock with Low Priority. (Related to the low priority wait option of ALTER TABLE and ALTER INDEX.), <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LCK_M_RIn_NL |Occurs when a task is waiting to acquire a NULL lock on the current key value, and an Insert Range lock between the current and previous key. A NULL lock on the key is an instant release lock.| 
|LCK_M_RIn_NL_ABORT_BLOCKERS |Occurs when a task is waiting to acquire a NULL lock with Abort Blockers on the current key value, and an Insert Range lock with Abort Blockers between the current and previous key. A NULL lock on the key is an instant release lock. (Related to the low priority wait option of ALTER TABLE and ALTER INDEX.), <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LCK_M_RIn_NL_LOW_PRIORITY |Occurs when a task is waiting to acquire a NULL lock with Low Priority on the current key value, and an Insert Range lock with Low Priority between the current and previous key. A NULL lock on the key is an instant release lock. (Related to the low priority wait option of ALTER TABLE and ALTER INDEX.), <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LCK_M_RIn_S |Occurs when a task is waiting to acquire a shared lock on the current key value, and an Insert Range lock between the current and previous key.| 
|LCK_M_RIn_S_ABORT_BLOCKERS |Occurs when a task is waiting to acquire a shared lock with Abort Blockers on the current key value, and an Insert Range lock with Abort Blockers between the current and previous key. (Related to the low priority wait option of ALTER TABLE and ALTER INDEX.), <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LCK_M_RIn_S_LOW_PRIORITY |Occurs when a task is waiting to acquire a shared lock with Low Priority on the current key value, and an Insert Range lock with Low Priority between the current and previous key. (Related to the low priority wait option of ALTER TABLE and ALTER INDEX.), <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LCK_M_RIn_U |Task is waiting to acquire an Update lock on the current key value, and an Insert Range lock between the current and previous key.| 
|LCK_M_RIn_U_ABORT_BLOCKERS |Task is waiting to acquire an Update lock with Abort Blockers on the current key value, and an Insert Range lock with Abort Blockers between the current and previous key. (Related to the low priority wait option of ALTER TABLE and ALTER INDEX.), <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LCK_M_RIn_U_LOW_PRIORITY |Task is waiting to acquire an Update lock with Low Priority on the current key value, and an Insert Range lock with Low Priority between the current and previous key. (Related to the low priority wait option of ALTER TABLE and ALTER INDEX.), <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LCK_M_RIn_X |Occurs when a task is waiting to acquire an Exclusive lock on the current key value, and an Insert Range lock between the current and previous key.| 
|LCK_M_RIn_X_ABORT_BLOCKERS |Occurs when a task is waiting to acquire an Exclusive lock with Abort Blockers on the current key value, and an Insert Range lock with Abort Blockers between the current and previous key. (Related to the low priority wait option of ALTER TABLE and ALTER INDEX.), <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LCK_M_RIn_X_LOW_PRIORITY |Occurs when a task is waiting to acquire an Exclusive lock with Low Priority on the current key value, and an Insert Range lock with Low Priority between the current and previous key. (Related to the low priority wait option of ALTER TABLE and ALTER INDEX.), <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LCK_M_RS_S |Occurs when a task is waiting to acquire a Shared lock on the current key value, and a Shared Range lock between the current and previous key.| 
|LCK_M_RS_S_ABORT_BLOCKERS |Occurs when a task is waiting to acquire a Shared lock with Abort Blockers on the current key value, and a Shared Range lock with Abort Blockers between the current and previous key. (Related to the low priority wait option of ALTER TABLE and ALTER INDEX.), <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LCK_M_RS_S_LOW_PRIORITY |Occurs when a task is waiting to acquire a Shared lock with Low Priority on the current key value, and a Shared Range lock with Low Priority between the current and previous key. (Related to the low priority wait option of ALTER TABLE and ALTER INDEX.), <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LCK_M_RS_U |Occurs when a task is waiting to acquire an Update lock on the current key value, and an Update Range lock between the current and previous key.| 
|LCK_M_RS_U_ABORT_BLOCKERS |Occurs when a task is waiting to acquire an Update lock with Abort Blockers on the current key value, and an Update Range lock with Abort Blockers between the current and previous key. (Related to the low priority wait option of ALTER TABLE and ALTER INDEX.), <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LCK_M_RS_U_LOW_PRIORITY |Occurs when a task is waiting to acquire an Update lock with Low Priority on the current key value, and an Update Range lock with Low Priority between the current and previous key. (Related to the low priority wait option of ALTER TABLE and ALTER INDEX.), <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LCK_M_RX_S |Occurs when a task is waiting to acquire a Shared lock on the current key value, and an Exclusive Range lock between the current and previous key.| 
|LCK_M_RX_S_ABORT_BLOCKERS |Occurs when a task is waiting to acquire a Shared lock with Abort Blockers on the current key value, and an Exclusive Range with Abort Blockers lock between the current and previous key. (Related to the low priority wait option of ALTER TABLE and ALTER INDEX.), <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LCK_M_RX_S_LOW_PRIORITY |Occurs when a task is waiting to acquire a Shared lock with Low Priority on the current key value, and an Exclusive Range with Low Priority lock between the current and previous key. (Related to the low priority wait option of ALTER TABLE and ALTER INDEX.), <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LCK_M_RX_U |Occurs when a task is waiting to acquire an Update lock on the current key value, and an Exclusive range lock between the current and previous key.| 
|LCK_M_RX_U_ABORT_BLOCKERS |Occurs when a task is waiting to acquire an Update lock with Abort Blockers on the current key value, and an Exclusive range lock with Abort Blockers between the current and previous key. (Related to the low priority wait option of ALTER TABLE and ALTER INDEX.), <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LCK_M_RX_U_LOW_PRIORITY |Occurs when a task is waiting to acquire an Update lock with Low Priority on the current key value, and an Exclusive range lock with Low Priority between the current and previous key. (Related to the low priority wait option of ALTER TABLE and ALTER INDEX.), <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LCK_M_RX_X |Occurs when a task is waiting to acquire an Exclusive lock on the current key value, and an Exclusive Range lock between the current and previous key.| 
|LCK_M_RX_X_ABORT_BLOCKERS |Occurs when a task is waiting to acquire an Exclusive lock with Abort Blockers on the current key value, and an Exclusive Range lock with Abort Blockers between the current and previous key. (Related to the low priority wait option of ALTER TABLE and ALTER INDEX.), <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LCK_M_RX_X_LOW_PRIORITY |Occurs when a task is waiting to acquire an Exclusive lock with Low Priority on the current key value, and an Exclusive Range lock with Low Priority between the current and previous key. (Related to the low priority wait option of ALTER TABLE and ALTER INDEX.), <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LCK_M_S |Occurs when a task is waiting to acquire a Shared lock.| 
|LCK_M_S_ABORT_BLOCKERS |Occurs when a task is waiting to acquire a Shared lock with Abort Blockers. (Related to the low priority wait option of ALTER TABLE and ALTER INDEX.), <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LCK_M_S_LOW_PRIORITY |Occurs when a task is waiting to acquire a Shared lock with Low Priority. (Related to the low priority wait option of ALTER TABLE and ALTER INDEX.), <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LCK_M_SCH_M |Occurs when a task is waiting to acquire a Schema Modify lock.| 
|LCK_M_SCH_M_ABORT_BLOCKERS |Occurs when a task is waiting to acquire a Schema Modify lock with Abort Blockers. (Related to the low priority wait option of ALTER TABLE and ALTER INDEX.), <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LCK_M_SCH_M_LOW_PRIORITY |Occurs when a task is waiting to acquire a Schema Modify lock with Low Priority. (Related to the low priority wait option of ALTER TABLE and ALTER INDEX.), <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LCK_M_SCH_S |Occurs when a task is waiting to acquire a Schema Share lock.| 
|LCK_M_SCH_S_ABORT_BLOCKERS |Occurs when a task is waiting to acquire a Schema Share lock with Abort Blockers. (Related to the low priority wait option of ALTER TABLE and ALTER INDEX.), <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LCK_M_SCH_S_LOW_PRIORITY |Occurs when a task is waiting to acquire a Schema Share lock with Low Priority. (Related to the low priority wait option of ALTER TABLE and ALTER INDEX.), <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LCK_M_SIU |Occurs when a task is waiting to acquire a Shared With Intent Update lock.| 
|LCK_M_SIU_ABORT_BLOCKERS |Occurs when a task is waiting to acquire a Shared With Intent Update lock with Abort Blockers. (Related to the low priority wait option of ALTER TABLE and ALTER INDEX.), <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LCK_M_SIU_LOW_PRIORITY |Occurs when a task is waiting to acquire a Shared With Intent Update lock with Low Priority. (Related to the low priority wait option of ALTER TABLE and ALTER INDEX.), <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LCK_M_SIX |Occurs when a task is waiting to acquire a Shared With Intent Exclusive lock.| 
|LCK_M_SIX_ABORT_BLOCKERS |Occurs when a task is waiting to acquire a Shared With Intent Exclusive lock with Abort Blockers. (Related to the low priority wait option of ALTER TABLE and ALTER INDEX.), <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LCK_M_SIX_LOW_PRIORITY |Occurs when a task is waiting to acquire a Shared With Intent Exclusive lock with Low Priority. (Related to the low priority wait option of ALTER TABLE and ALTER INDEX.), <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LCK_M_U |Occurs when a task is waiting to acquire an Update lock.| 
|LCK_M_U_ABORT_BLOCKERS |Occurs when a task is waiting to acquire an Update lock with Abort Blockers. (Related to the low priority wait option of ALTER TABLE and ALTER INDEX.), <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LCK_M_U_LOW_PRIORITY |Occurs when a task is waiting to acquire an Update lock with Low Priority. (Related to the low priority wait option of ALTER TABLE and ALTER INDEX.), <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LCK_M_UIX |Occurs when a task is waiting to acquire an Update With Intent Exclusive lock.| 
|LCK_M_UIX_ABORT_BLOCKERS |Occurs when a task is waiting to acquire an Update With Intent Exclusive lock with Abort Blockers. (Related to the low priority wait option of ALTER TABLE and ALTER INDEX.), <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LCK_M_UIX_LOW_PRIORITY |Occurs when a task is waiting to acquire an Update With Intent Exclusive lock with Low Priority. (Related to the low priority wait option of ALTER TABLE and ALTER INDEX.), <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LCK_M_X |Occurs when a task is waiting to acquire an Exclusive lock.| 
|LCK_M_X_ABORT_BLOCKERS |Occurs when a task is waiting to acquire an Exclusive lock with Abort Blockers. (Related to the low priority wait option of ALTER TABLE and ALTER INDEX.), <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LCK_M_X_LOW_PRIORITY |Occurs when a task is waiting to acquire an Exclusive lock with Low Priority. (Related to the low priority wait option of ALTER TABLE and ALTER INDEX.), <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LOG_POOL_SCAN |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LOG_RATE_GOVERNOR |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LOGBUFFER |Occurs when a task is waiting for space in the log buffer to store a log record. Consistently high values may indicate that the log devices cannot keep up with the amount of log being generated by the server.| 
|LOGCAPTURE_LOGPOOLTRUNCPOINT |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LOGGENERATION |Identified for informational purposes only. Not supported. Future compatibility is not guaranteed.| 
|LOGMGR |Occurs when a task is waiting for any outstanding log I/Os to finish before shutting down the log while closing the database.| 
|LOGMGR_FLUSH |Identified for informational purposes only. Not supported. Future compatibility is not guaranteed.| 
|LOGMGR_PMM_LOG |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LOGMGR_QUEUE |Occurs while the log writer task waits for work requests.| 
|LOGMGR_RESERVE_APPEND |Occurs when a task is waiting to see whether log truncation frees up log space to enable the task to write a new log record. Consider increasing the size of the log file(s) for the affected database to reduce this wait.| 
|LOGPOOL_CACHESIZE |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LOGPOOL_CONSUMER |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LOGPOOL_CONSUMERSET |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LOGPOOL_FREEPOOLS |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LOGPOOL_MGRSET |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LOGPOOL_REPLACEMENTSET |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LOGPOOLREFCOUNTEDOBJECT_REFDONE |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|LOWFAIL_MEMMGR_QUEUE |Occurs while waiting for memory to be available for use.| 
|MD_AGENT_YIELD |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|MD_LAZYCACHE_RWLOCK |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|MEMORY_ALLOCATION_EXT |Occurs while allocating memory from either the internal SQL Server memory pool or the operation system., <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|MEMORY_GRANT_UPDATE |TBD <br /> **Applies to**: [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|METADATA_LAZYCACHE_RWLOCK |TBD <br /> **Applies to**: [!INCLUDE[ssKilimanjaro_md](../../includes/sskilimanjaro-md.md)] only. |  
|MIGRATIONBUFFER |TBD <br /> **Applies to**: [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|MISCELLANEOUS |Identified for informational purposes only. Not supported. Future compatibility is not guaranteed.| 
|MISCELLANEOUS |Identified for informational purposes only. Not supported. Future compatibility is not guaranteed.| 
|MSQL_DQ |Occurs when a task is waiting for a distributed query operation to finish. This is used to detect potential Multiple Active Result Set (MARS) application deadlocks. The wait ends when the distributed query call finishes.| 
|MSQL_XACT_MGR_MUTEX |Occurs when a task is waiting to obtain ownership of the session transaction manager to perform a session level transaction operation.| 
|MSQL_XACT_MUTEX |Occurs during synchronization of transaction usage. A request must acquire the mutex before it can use the transaction.| 
|MSQL_XP |Occurs when a task is waiting for an extended stored procedure to end. SQL Server uses this wait state to detect potential MARS application deadlocks. The wait stops when the extended stored procedure call ends.| 
|MSSEARCH |Occurs during Full-Text Search calls. This wait ends when the full-text operation completes. It does not indicate contention, but rather the duration of full-text operations.| 
|NET_WAITFOR_PACKET |Occurs when a connection is waiting for a network packet during a network read.| 
|NETWORKSXMLMGRLOAD |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|NODE_CACHE_MUTEX |TBD| 
|OLEDB |Occurs when SQL Server calls the SQL Server Native Client OLE DB Provider. This wait type is not used for synchronization. Instead, it indicates the duration of calls to the OLE DB provider.| 
|ONDEMAND_TASK_QUEUE |Occurs while a background task waits for high priority system task requests. Long wait times indicate that there have been no high priority requests to process, and should not cause concern.| 
|PAGEIOLATCH_DT |Occurs when a task is waiting on a latch for a buffer that is in an I/O request. The latch request is in Destroy mode. Long waits may indicate problems with the disk subsystem.| 
|PAGEIOLATCH_EX |Occurs when a task is waiting on a latch for a buffer that is in an I/O request. The latch request is in Exclusive mode. Long waits may indicate problems with the disk subsystem.| 
|PAGEIOLATCH_KP |Occurs when a task is waiting on a latch for a buffer that is in an I/O request. The latch request is in Keep mode. Long waits may indicate problems with the disk subsystem.| 
|PAGEIOLATCH_NL |Identified for informational purposes only. Not supported. Future compatibility is not guaranteed.| 
|PAGEIOLATCH_SH |Occurs when a task is waiting on a latch for a buffer that is in an I/O request. The latch request is in Shared mode. Long waits may indicate problems with the disk subsystem.| 
|PAGEIOLATCH_UP |Occurs when a task is waiting on a latch for a buffer that is in an I/O request. The latch request is in Update mode. Long waits may indicate problems with the disk subsystem.| 
|PAGELATCH_DT |Occurs when a task is waiting on a latch for a buffer that is not in an I/O request. The latch request is in Destroy mode.| 
|PAGELATCH_EX |Occurs when a task is waiting on a latch for a buffer that is not in an I/O request. The latch request is in Exclusive mode.| 
|PAGELATCH_KP |Occurs when a task is waiting on a latch for a buffer that is not in an I/O request. The latch request is in Keep mode.| 
|PAGELATCH_NL |Identified for informational purposes only. Not supported. Future compatibility is not guaranteed.| 
|PAGELATCH_SH |Occurs when a task is waiting on a latch for a buffer that is not in an I/O request. The latch request is in Shared mode.| 
|PAGELATCH_UP |Occurs when a task is waiting on a latch for a buffer that is not in an I/O request. The latch request is in Update mode.| 
|PARALLEL_BACKUP_QUEUE |Occurs when serializing output produced by RESTORE HEADERONLY, RESTORE FILELISTONLY, or RESTORE LABELONLY.| 
|PARALLEL_REDO_DRAIN_WORKER |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PARALLEL_REDO_FLOW_CONTROL |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PARALLEL_REDO_LOG_CACHE |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PARALLEL_REDO_TRAN_LIST |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PARALLEL_REDO_TRAN_TURN |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PARALLEL_REDO_WORKER_SYNC |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PARALLEL_REDO_WORKER_WAIT_WORK |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PERFORMANCE_COUNTERS_RWLOCK |TBD| 
|PHYSICAL_SEEDING_DMV |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|POOL_LOG_RATE_GOVERNOR |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PREEMPTIVE_ABR |Identified for informational purposes only. Not supported. Future compatibility is not guaranteed.| 
|PREEMPTIVE_AUDIT_ACCESS_EVENTLOG |Occurs when the [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] Operating System (SQLOS) scheduler switches to preemptive mode to write an audit event to the Windows event log. <br /> **Applies to**: [!INCLUDE[ssKilimanjaro_md](../../includes/sskilimanjaro-md.md)] only. |  
|PREEMPTIVE_AUDIT_ACCESS_SECLOG |Occurs when the SQLOS scheduler switches to preemptive mode to write an audit event to the Windows Security log. <br /> **Applies to**: [!INCLUDE[ssKilimanjaro_md](../../includes/sskilimanjaro-md.md)] only. |  
|PREEMPTIVE_CLOSEBACKUPMEDIA |Occurs when the SQLOS scheduler switches to preemptive mode to close backup media.| 
|PREEMPTIVE_CLOSEBACKUPTAPE |Occurs when the SQLOS scheduler switches to preemptive mode to close a tape backup device.| 
|PREEMPTIVE_CLOSEBACKUPVDIDEVICE |Occurs when the SQLOS scheduler switches to preemptive mode to close a virtual backup device.| 
|PREEMPTIVE_CLUSAPI_CLUSTERRESOURCECONTROL |Occurs when the SQLOS scheduler switches to preemptive mode to perform Windows failover cluster operations.| 
|PREEMPTIVE_COM_COCREATEINSTANCE |Occurs when the SQLOS scheduler switches to preemptive mode to create a COM object.| 
|PREEMPTIVE_COM_COGETCLASSOBJECT |TBD| 
|PREEMPTIVE_COM_CREATEACCESSOR |TBD| 
|PREEMPTIVE_COM_DELETEROWS |TBD| 
|PREEMPTIVE_COM_GETCOMMANDTEXT |TBD| 
|PREEMPTIVE_COM_GETDATA |TBD| 
|PREEMPTIVE_COM_GETNEXTROWS |TBD| 
|PREEMPTIVE_COM_GETRESULT |TBD| 
|PREEMPTIVE_COM_GETROWSBYBOOKMARK |TBD| 
|PREEMPTIVE_COM_LBFLUSH |TBD| 
|PREEMPTIVE_COM_LBLOCKREGION |TBD| 
|PREEMPTIVE_COM_LBREADAT |TBD| 
|PREEMPTIVE_COM_LBSETSIZE |TBD| 
|PREEMPTIVE_COM_LBSTAT |TBD| 
|PREEMPTIVE_COM_LBUNLOCKREGION |TBD| 
|PREEMPTIVE_COM_LBWRITEAT |TBD| 
|PREEMPTIVE_COM_QUERYINTERFACE |TBD| 
|PREEMPTIVE_COM_RELEASE |TBD| 
|PREEMPTIVE_COM_RELEASEACCESSOR |TBD| 
|PREEMPTIVE_COM_RELEASEROWS |TBD| 
|PREEMPTIVE_COM_RELEASESESSION |TBD| 
|PREEMPTIVE_COM_RESTARTPOSITION |TBD| 
|PREEMPTIVE_COM_SEQSTRMREAD |TBD| 
|PREEMPTIVE_COM_SEQSTRMREADANDWRITE |TBD| 
|PREEMPTIVE_COM_SETDATAFAILURE |TBD| 
|PREEMPTIVE_COM_SETPARAMETERINFO |TBD| 
|PREEMPTIVE_COM_SETPARAMETERPROPERTIES |TBD| 
|PREEMPTIVE_COM_STRMLOCKREGION |TBD| 
|PREEMPTIVE_COM_STRMSEEKANDREAD |TBD| 
|PREEMPTIVE_COM_STRMSEEKANDWRITE |TBD| 
|PREEMPTIVE_COM_STRMSETSIZE |TBD| 
|PREEMPTIVE_COM_STRMSTAT |TBD| 
|PREEMPTIVE_COM_STRMUNLOCKREGION |TBD| 
|PREEMPTIVE_CONSOLEWRITE |TBD| 
|PREEMPTIVE_CREATEPARAM |TBD| 
|PREEMPTIVE_DEBUG |TBD| 
|PREEMPTIVE_DFSADDLINK |TBD| 
|PREEMPTIVE_DFSLINKEXISTCHECK |TBD| 
|PREEMPTIVE_DFSLINKHEALTHCHECK |TBD| 
|PREEMPTIVE_DFSREMOVELINK |TBD| 
|PREEMPTIVE_DFSREMOVEROOT |TBD| 
|PREEMPTIVE_DFSROOTFOLDERCHECK |TBD| 
|PREEMPTIVE_DFSROOTINIT |TBD| 
|PREEMPTIVE_DFSROOTSHARECHECK |TBD| 
|PREEMPTIVE_DTC_ABORT |TBD| 
|PREEMPTIVE_DTC_ABORTREQUESTDONE |TBD| 
|PREEMPTIVE_DTC_BEGINTRANSACTION |TBD| 
|PREEMPTIVE_DTC_COMMITREQUESTDONE |TBD| 
|PREEMPTIVE_DTC_ENLIST |TBD| 
|PREEMPTIVE_DTC_PREPAREREQUESTDONE |TBD| 
|PREEMPTIVE_FILESIZEGET |TBD| 
|PREEMPTIVE_FSAOLEDB_ABORTTRANSACTION |TBD| 
|PREEMPTIVE_FSAOLEDB_COMMITTRANSACTION |TBD| 
|PREEMPTIVE_FSAOLEDB_STARTTRANSACTION |TBD| 
|PREEMPTIVE_FSRECOVER_UNCONDITIONALUNDO |TBD| 
|PREEMPTIVE_GETRMINFO |TBD| 
|PREEMPTIVE_HADR_LEASE_MECHANISM |Always On Availability Groups lease manager scheduling for CSS diagnostics., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PREEMPTIVE_HTTP_EVENT_WAIT |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PREEMPTIVE_HTTP_REQUEST |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PREEMPTIVE_LOCKMONITOR |TBD| 
|PREEMPTIVE_MSS_RELEASE |TBD| 
|PREEMPTIVE_ODBCOPS |TBD| 
|PREEMPTIVE_OLE_UNINIT |TBD| 
|PREEMPTIVE_OLEDB_ABORTORCOMMITTRAN |TBD| 
|PREEMPTIVE_OLEDB_ABORTTRAN |TBD| 
|PREEMPTIVE_OLEDB_GETDATASOURCE |TBD| 
|PREEMPTIVE_OLEDB_GETLITERALINFO |TBD| 
|PREEMPTIVE_OLEDB_GETPROPERTIES |TBD| 
|PREEMPTIVE_OLEDB_GETPROPERTYINFO |TBD| 
|PREEMPTIVE_OLEDB_GETSCHEMALOCK |TBD| 
|PREEMPTIVE_OLEDB_JOINTRANSACTION |TBD| 
|PREEMPTIVE_OLEDB_RELEASE |TBD| 
|PREEMPTIVE_OLEDB_SETPROPERTIES |TBD| 
|PREEMPTIVE_OLEDBOPS |TBD| 
|PREEMPTIVE_OS_ACCEPTSECURITYCONTEXT |TBD| 
|PREEMPTIVE_OS_ACQUIRECREDENTIALSHANDLE |TBD| 
|PREEMPTIVE_OS_AUTHENTICATIONOPS |TBD| 
|PREEMPTIVE_OS_AUTHORIZATIONOPS |TBD| 
|PREEMPTIVE_OS_AUTHZGETINFORMATIONFROMCONTEXT |TBD| 
|PREEMPTIVE_OS_AUTHZINITIALIZECONTEXTFROMSID |TBD| 
|PREEMPTIVE_OS_AUTHZINITIALIZERESOURCEMANAGER |TBD| 
|PREEMPTIVE_OS_BACKUPREAD |TBD| 
|PREEMPTIVE_OS_CLOSEHANDLE |TBD| 
|PREEMPTIVE_OS_CLUSTEROPS |TBD| 
|PREEMPTIVE_OS_COMOPS |TBD| 
|PREEMPTIVE_OS_COMPLETEAUTHTOKEN |TBD| 
|PREEMPTIVE_OS_COPYFILE |TBD| 
|PREEMPTIVE_OS_CREATEDIRECTORY |TBD| 
|PREEMPTIVE_OS_CREATEFILE |TBD| 
|PREEMPTIVE_OS_CRYPTACQUIRECONTEXT |TBD| 
|PREEMPTIVE_OS_CRYPTIMPORTKEY |TBD| 
|PREEMPTIVE_OS_CRYPTOPS |TBD| 
|PREEMPTIVE_OS_DECRYPTMESSAGE |TBD| 
|PREEMPTIVE_OS_DELETEFILE |TBD| 
|PREEMPTIVE_OS_DELETESECURITYCONTEXT |TBD| 
|PREEMPTIVE_OS_DEVICEIOCONTROL |TBD| 
|PREEMPTIVE_OS_DEVICEOPS |TBD| 
|PREEMPTIVE_OS_DIRSVC_NETWORKOPS |TBD| 
|PREEMPTIVE_OS_DISCONNECTNAMEDPIPE |TBD| 
|PREEMPTIVE_OS_DOMAINSERVICESOPS |TBD| 
|PREEMPTIVE_OS_DSGETDCNAME |TBD| 
|PREEMPTIVE_OS_DTCOPS |TBD| 
|PREEMPTIVE_OS_ENCRYPTMESSAGE |TBD| 
|PREEMPTIVE_OS_FILEOPS |TBD| 
|PREEMPTIVE_OS_FINDFILE |TBD| 
|PREEMPTIVE_OS_FLUSHFILEBUFFERS |TBD| 
|PREEMPTIVE_OS_FORMATMESSAGE |TBD| 
|PREEMPTIVE_OS_FREECREDENTIALSHANDLE |TBD| 
|PREEMPTIVE_OS_FREELIBRARY |TBD| 
|PREEMPTIVE_OS_GENERICOPS |TBD| 
|PREEMPTIVE_OS_GETADDRINFO |TBD| 
|PREEMPTIVE_OS_GETCOMPRESSEDFILESIZE |TBD| 
|PREEMPTIVE_OS_GETDISKFREESPACE |TBD| 
|PREEMPTIVE_OS_GETFILEATTRIBUTES |TBD| 
|PREEMPTIVE_OS_GETFILESIZE |TBD| 
|PREEMPTIVE_OS_GETFINALFILEPATHBYHANDLE |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PREEMPTIVE_OS_GETLONGPATHNAME |TBD| 
|PREEMPTIVE_OS_GETPROCADDRESS |TBD| 
|PREEMPTIVE_OS_GETVOLUMENAMEFORVOLUMEMOUNTPOINT |TBD| 
|PREEMPTIVE_OS_GETVOLUMEPATHNAME |TBD| 
|PREEMPTIVE_OS_INITIALIZESECURITYCONTEXT |TBD| 
|PREEMPTIVE_OS_LIBRARYOPS |TBD| 
|PREEMPTIVE_OS_LOADLIBRARY |TBD| 
|PREEMPTIVE_OS_LOGONUSER |TBD| 
|PREEMPTIVE_OS_LOOKUPACCOUNTSID |TBD| 
|PREEMPTIVE_OS_MESSAGEQUEUEOPS |TBD| 
|PREEMPTIVE_OS_MOVEFILE |TBD| 
|PREEMPTIVE_OS_NETGROUPGETUSERS |TBD| 
|PREEMPTIVE_OS_NETLOCALGROUPGETMEMBERS |TBD| 
|PREEMPTIVE_OS_NETUSERGETGROUPS |TBD| 
|PREEMPTIVE_OS_NETUSERGETLOCALGROUPS |TBD| 
|PREEMPTIVE_OS_NETUSERMODALSGET |TBD| 
|PREEMPTIVE_OS_NETVALIDATEPASSWORDPOLICY |TBD| 
|PREEMPTIVE_OS_NETVALIDATEPASSWORDPOLICYFREE |TBD| 
|PREEMPTIVE_OS_OPENDIRECTORY |TBD| 
|PREEMPTIVE_OS_PDH_WMI_INIT |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PREEMPTIVE_OS_PIPEOPS |TBD| 
|PREEMPTIVE_OS_PROCESSOPS |TBD| 
|PREEMPTIVE_OS_QUERYCONTEXTATTRIBUTES |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PREEMPTIVE_OS_QUERYREGISTRY |TBD| 
|PREEMPTIVE_OS_QUERYSECURITYCONTEXTTOKEN |TBD| 
|PREEMPTIVE_OS_REMOVEDIRECTORY |TBD| 
|PREEMPTIVE_OS_REPORTEVENT |TBD| 
|PREEMPTIVE_OS_REVERTTOSELF |TBD| 
|PREEMPTIVE_OS_RSFXDEVICEOPS |TBD| 
|PREEMPTIVE_OS_SECURITYOPS |TBD| 
|PREEMPTIVE_OS_SERVICEOPS |TBD| 
|PREEMPTIVE_OS_SETENDOFFILE |TBD| 
|PREEMPTIVE_OS_SETFILEPOINTER |TBD| 
|PREEMPTIVE_OS_SETFILEVALIDDATA |TBD| 
|PREEMPTIVE_OS_SETNAMEDSECURITYINFO |TBD| 
|PREEMPTIVE_OS_SQLCLROPS |TBD| 
|PREEMPTIVE_OS_SQMLAUNCH |TBD <br /> **Applies to**: [!INCLUDE[ssKilimanjaro_md](../../includes/sskilimanjaro-md.md)] through [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)]. |  
|PREEMPTIVE_OS_VERIFYSIGNATURE |TBD| 
|PREEMPTIVE_OS_VERIFYTRUST |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PREEMPTIVE_OS_VSSOPS |TBD| 
|PREEMPTIVE_OS_WAITFORSINGLEOBJECT |TBD| 
|PREEMPTIVE_OS_WINSOCKOPS |TBD| 
|PREEMPTIVE_OS_WRITEFILE |TBD| 
|PREEMPTIVE_OS_WRITEFILEGATHER |TBD| 
|PREEMPTIVE_OS_WSASETLASTERROR |TBD| 
|PREEMPTIVE_REENLIST |TBD| 
|PREEMPTIVE_RESIZELOG |TBD| 
|PREEMPTIVE_ROLLFORWARDREDO |TBD| 
|PREEMPTIVE_ROLLFORWARDUNDO |TBD| 
|PREEMPTIVE_SB_STOPENDPOINT |TBD| 
|PREEMPTIVE_SERVER_STARTUP |TBD| 
|PREEMPTIVE_SETRMINFO |TBD| 
|PREEMPTIVE_SHAREDMEM_GETDATA |TBD| 
|PREEMPTIVE_SNIOPEN |TBD| 
|PREEMPTIVE_SOSHOST |TBD| 
|PREEMPTIVE_SOSTESTING |Identified for informational purposes only. Not supported. Future compatibility is not guaranteed.| 
|PREEMPTIVE_SP_SERVER_DIAGNOSTICS |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PREEMPTIVE_STARTRM |TBD| 
|PREEMPTIVE_STREAMFCB_CHECKPOINT |TBD| 
|PREEMPTIVE_STREAMFCB_RECOVER |TBD| 
|PREEMPTIVE_STRESSDRIVER |Identified for informational purposes only. Not supported. Future compatibility is not guaranteed.| 
|PREEMPTIVE_TESTING |Identified for informational purposes only. Not supported. Future compatibility is not guaranteed.| 
|PREEMPTIVE_TRANSIMPORT |TBD| 
|PREEMPTIVE_UNMARSHALPROPAGATIONTOKEN |TBD| 
|PREEMPTIVE_VSS_CREATESNAPSHOT |TBD| 
|PREEMPTIVE_VSS_CREATEVOLUMESNAPSHOT |TBD| 
|PREEMPTIVE_XE_CALLBACKEXECUTE |TBD| 
|PREEMPTIVE_XE_CX_FILE_OPEN |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PREEMPTIVE_XE_CX_HTTP_CALL |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PREEMPTIVE_XE_DISPATCHER |TBD| 
|PREEMPTIVE_XE_ENGINEINIT |TBD| 
|PREEMPTIVE_XE_GETTARGETSTATE |TBD| 
|PREEMPTIVE_XE_SESSIONCOMMIT |TBD| 
|PREEMPTIVE_XE_TARGETFINALIZE |TBD| 
|PREEMPTIVE_XE_TARGETINIT |TBD| 
|PREEMPTIVE_XE_TIMERRUN |TBD| 
|PREEMPTIVE_XETESTING |Identified for informational purposes only. Not supported. Future compatibility is not guaranteed.| 
|PRINT_ROLLBACK_PROGRESS |Used to wait while user processes are ended in a database that has been transitioned by using the ALTER DATABASE termination clause. For more information, see ALTER DATABASE (Transact-SQL).| 
|PRU_ROLLBACK_DEFERRED |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PWAIT_ALL_COMPONENTS_INITIALIZED |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PWAIT_COOP_SCAN |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PWAIT_DIRECTLOGCONSUMER_GETNEXT |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PWAIT_EVENT_SESSION_INIT_MUTEX |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PWAIT_FABRIC_REPLICA_CONTROLLER_DATA_LOSS |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PWAIT_HADR_ACTION_COMPLETED |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PWAIT_HADR_CHANGE_NOTIFIER_TERMINATION_SYNC |Occurs when a background task is waiting for the termination of the background task that receives (via polling) Windows Server Failover Clustering notifications., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PWAIT_HADR_CLUSTER_INTEGRATION |An append, replace, and/or remove operation is waiting to grab a write lock on an Always On internal list (such as a list of networks, network addresses, or availability group listeners). Internal use only, <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PWAIT_HADR_FAILOVER_COMPLETED |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PWAIT_HADR_JOIN |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PWAIT_HADR_OFFLINE_COMPLETED |An Always On drop availability group operation is waiting for the target availability group to go offline before destroying Windows Server Failover Clustering objects., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PWAIT_HADR_ONLINE_COMPLETED |An Always On create or failover availability group operation is waiting for the target availability group to come online., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PWAIT_HADR_POST_ONLINE_COMPLETED |An Always On drop availability group operation is waiting for the termination of any background task that was scheduled as part of a previous command. For example, there may be a background task that is transitioning availability databases to the primary role. The DROP AVAILABILITY GROUP DDL must wait for this background task to terminate in order to avoid race conditions., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PWAIT_HADR_SERVER_READY_CONNECTIONS |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PWAIT_HADR_WORKITEM_COMPLETED |Internal wait by a thread waiting for an async work task to complete. This is an expected wait and is for CSS use., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PWAIT_HADRSIM |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PWAIT_LOG_CONSOLIDATION_IO |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PWAIT_LOG_CONSOLIDATION_POLL |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PWAIT_MD_LOGIN_STATS |Occurs during internal synchronization in metadata on login stats., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PWAIT_MD_RELATION_CACHE |Occurs during internal synchronization in metadata on table or index., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PWAIT_MD_SERVER_CACHE |Occurs during internal synchronization in metadata on linked servers., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PWAIT_MD_UPGRADE_CONFIG |Occurs during internal synchronization in upgrading server wide configurations., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PWAIT_PREEMPTIVE_APP_USAGE_TIMER |TBD <br /> **Applies to**: [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PWAIT_PREEMPTIVE_AUDIT_ACCESS_WINDOWSLOG |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PWAIT_QRY_BPMEMORY |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PWAIT_REPLICA_ONLINE_INIT_MUTEX |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PWAIT_RESOURCE_SEMAPHORE_FT_PARALLEL_QUERY_SYNC |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PWAIT_SBS_FILE_OPERATION |TBD <br /> **Applies to**: [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PWAIT_XTP_FSSTORAGE_MAINTENANCE |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|PWAIT_XTP_HOST_STORAGE_WAIT |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|QDS_ASYNC_CHECK_CONSISTENCY_TASK |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|QDS_ASYNC_PERSIST_TASK |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|QDS_ASYNC_PERSIST_TASK_START |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|QDS_ASYNC_QUEUE |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|QDS_BCKG_TASK |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|QDS_BLOOM_FILTER |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|QDS_CLEANUP_STALE_QUERIES_TASK_MAIN_LOOP_SLEEP |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|QDS_CTXS |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|QDS_DB_DISK |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|QDS_DYN_VECTOR |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|QDS_EXCLUSIVE_ACCESS |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|QDS_HOST_INIT |TBD <br /> **Applies to**: [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|QDS_LOADDB |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|QDS_PERSIST_TASK_MAIN_LOOP_SLEEP |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|QDS_QDS_CAPTURE_INIT |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|QDS_SHUTDOWN_QUEUE |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|QDS_STMT |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|QDS_STMT_DISK |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|QDS_TASK_SHUTDOWN |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|QDS_TASK_START |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|QE_WARN_LIST_SYNC |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|QPJOB_KILL |Indicates that an asynchronous automatic statistics update was canceled by a call to KILL as the update was starting to run. The terminating thread is suspended, waiting for it to start listening for KILL commands. A good value is less than one second.| 
|QPJOB_WAITFOR_ABORT |Indicates that an asynchronous automatic statistics update was canceled by a call to KILL when it was running. The update has now completed but is suspended until the terminating thread message coordination is complete. This is an ordinary but rare state, and should be very short. A good value is less than one second.| 
|QRY_MEM_GRANT_INFO_MUTEX |Occurs when Query Execution memory management tries to control access to static grant information list. This state lists information about the current granted and waiting memory requests. This state is a simple access control state. There should never be a long wait on this state. If this mutex is not released, all new memory-using queries will stop responding.| 
|QRY_PARALLEL_THREAD_MUTEX |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|QRY_PROFILE_LIST_MUTEX |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|QUERY_ERRHDL_SERVICE_DONE |Identified for informational purposes only. Not supported. <br /> **Applies to**: [!INCLUDE[ssKilimanjaro_md](../../includes/sskilimanjaro-md.md)] only. |  
|QUERY_WAIT_ERRHDL_SERVICE |Identified for informational purposes only.  Not supported. <br /> **Applies to**: [!INCLUDE[ssKilimanjaro_md](../../includes/sskilimanjaro-md.md)] only.  |  
|QUERY_EXECUTION_INDEX_SORT_EVENT_OPEN |Occurs in certain cases when offline create index build is run in parallel, and the different worker threads that are sorting synchronize access to the sort files.| 
|QUERY_NOTIFICATION_MGR_MUTEX |Occurs during synchronization of the garbage collection queue in the Query Notification Manager.| 
|QUERY_NOTIFICATION_SUBSCRIPTION_MUTEX |Occurs during state synchronization for transactions in Query Notifications.| 
|QUERY_NOTIFICATION_TABLE_MGR_MUTEX |Occurs during internal synchronization within the Query Notification Manager.| 
|QUERY_NOTIFICATION_UNITTEST_MUTEX |Identified for informational purposes only. Not supported. Future compatibility is not guaranteed.| 
|QUERY_OPTIMIZER_PRINT_MUTEX |Occurs during synchronization of query optimizer diagnostic output production. This wait type only occurs if diagnostic settings have been enabled under direction of Microsoft Product Support.| 
|QUERY_TASK_ENQUEUE_MUTEX |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|QUERY_TRACEOUT |Identified for informational purposes only. Not supported. Future compatibility is not guaranteed.| 
|RBIO_WAIT_VLF |TBD <br /> **Applies to**: [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|RECOVER_CHANGEDB |Occurs during synchronization of database status in warm standby database.| 
|RECOVERY_MGR_LOCK |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|REDO_THREAD_PENDING_WORK |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|REDO_THREAD_SYNC |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|REMOTE_BLOCK_IO |TBD <br /> **Applies to**: [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|REMOTE_DATA_ARCHIVE_MIGRATION_DMV |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|REMOTE_DATA_ARCHIVE_SCHEMA_DMV |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|REMOTE_DATA_ARCHIVE_SCHEMA_TASK_QUEUE |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|REPL_CACHE_ACCESS |Occurs during synchronization on a replication article cache. During these waits, the replication log reader stalls, and data definition language (DDL) statements on a published table are blocked.| 
|REPL_HISTORYCACHE_ACCESS |TBD| 
|REPL_SCHEMA_ACCESS |Occurs during synchronization of replication schema version information. This state exists when DDL statements are executed on the replicated object, and when the log reader builds or consumes versioned schema based on DDL occurrence. Contention can be seen on this wait type if you have many published databases on a single publisher with transactional replication and the published databases are very active.| 
|REPL_TRANFSINFO_ACCESS |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|REPL_TRANHASHTABLE_ACCESS |TBD| 
|REPL_TRANTEXTINFO_ACCESS |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|REPLICA_WRITES |Occurs while a task waits for completion of page writes to database snapshots or DBCC replicas.| 
|REQUEST_DISPENSER_PAUSE |Occurs when a task is waiting for all outstanding I/O to complete, so that I/O to a file can be frozen for snapshot backup.| 
|REQUEST_FOR_DEADLOCK_SEARCH |Occurs while the deadlock monitor waits to start the next deadlock search. This wait is expected between deadlock detections, and lengthy total waiting time on this resource does not indicate a problem.| 
|RESERVED_MEMORY_ALLOCATION_EXT |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|RESMGR_THROTTLED |Occurs when a new request comes in and is throttled based on the GROUP_MAX_REQUESTS setting.| 
|RESOURCE_GOVERNOR_IDLE |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|RESOURCE_QUEUE |Occurs during synchronization of various internal resource queues.| 
|RESOURCE_SEMAPHORE |Occurs when a query memory request cannot be granted immediately due to other concurrent queries. High waits and wait times may indicate excessive number of concurrent queries, or excessive memory request amounts.| 
|RESOURCE_SEMAPHORE_MUTEX |Occurs while a query waits for its request for a thread reservation to be fulfilled. It also occurs when synchronizing query compile and memory grant requests.| 
|RESOURCE_SEMAPHORE_QUERY_COMPILE |Occurs when the number of concurrent query compilations reaches a throttling limit. High waits and wait times may indicate excessive compilations, recompiles, or uncachable plans.| 
|RESOURCE_SEMAPHORE_SMALL_QUERY |Occurs when memory request by a small query cannot be granted immediately due to other concurrent queries. Wait time should not exceed more than a few seconds, because the server transfers the request to the main query memory pool if it fails to grant the requested memory within a few seconds. High waits may indicate an excessive number of concurrent small queries while the main memory pool is blocked by waiting queries. <br /> **Applies to**: [!INCLUDE[ssKilimanjaro_md](../../includes/sskilimanjaro-md.md)] only. |  
|RESTORE_FILEHANDLECACHE_ENTRYLOCK |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|RESTORE_FILEHANDLECACHE_LOCK |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|RG_RECONFIG |TBD| 
|ROWGROUP_OP_STATS |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|ROWGROUP_VERSION |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|RTDATA_LIST |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|SATELLITE_CARGO |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|SATELLITE_SERVICE_SETUP |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|SATELLITE_TASK |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|SBS_DISPATCH |TBD <br /> **Applies to**: [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|SBS_RECEIVE_TRANSPORT |TBD <br /> **Applies to**: [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|SBS_TRANSPORT |TBD <br /> **Applies to**: [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|SCAN_CHAR_HASH_ARRAY_INITIALIZATION |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|SEC_DROP_TEMP_KEY |Occurs after a failed attempt to drop a temporary security key before a retry attempt.| 
|SECURITY_CNG_PROVIDER_MUTEX |TBD <br /> **Applies to**: [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|SECURITY_CRYPTO_CONTEXT_MUTEX |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|SECURITY_DBE_STATE_MUTEX |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|SECURITY_KEYRING_RWLOCK |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|SECURITY_MUTEX |Occurs when there is a wait for mutexes that control access to the global list of Extensible Key Management (EKM) cryptographic providers and the session-scoped list of EKM sessions.| 
|SECURITY_RULETABLE_MUTEX |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|SEMPLAT_DSI_BUILD |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|SEQUENCE_GENERATION |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|SEQUENTIAL_GUID |Occurs while a new sequential GUID is being obtained.| 
|SERVER_IDLE_CHECK |Occurs during synchronization of SQL Server instance idle status when a resource monitor is attempting to declare a SQL Server instance as idle or trying to wake up.| 
|SERVER_RECONFIGURE |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|SESSION_WAIT_STATS_CHILDREN |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|SHARED_DELTASTORE_CREATION |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|SHUTDOWN |Occurs while a shutdown statement waits for active connections to exit.| 
|SLEEP_BPOOL_FLUSH |Occurs when a checkpoint is throttling the issuance of new I/Os in order to avoid flooding the disk subsystem.| 
|SLEEP_BUFFERPOOL_HELPLW |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|SLEEP_DBSTARTUP |Occurs during database startup while waiting for all databases to recover.| 
|SLEEP_DCOMSTARTUP |Occurs once at most during SQL Server instance startup while waiting for DCOM initialization to complete.| 
|SLEEP_MASTERDBREADY |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|SLEEP_MASTERMDREADY |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|SLEEP_MASTERUPGRADED |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|SLEEP_MEMORYPOOL_ALLOCATEPAGES |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|SLEEP_MSDBSTARTUP |Occurs when SQL Trace waits for the msdb database to complete startup.| 
|SLEEP_RETRY_VIRTUALALLOC |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|SLEEP_SYSTEMTASK |Occurs during the start of a background task while waiting for tempdb to complete startup.| 
|SLEEP_TASK |Occurs when a task sleeps while waiting for a generic event to occur.| 
|SLEEP_TEMPDBSTARTUP |Occurs while a task waits for tempdb to complete startup.| 
|SLEEP_WORKSPACE_ALLOCATEPAGE |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|SLO_UPDATE |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|SMSYNC |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|SNI_CONN_DUP |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|SNI_CRITICAL_SECTION |Occurs during internal synchronization within SQL Server networking components.| 
|SNI_HTTP_WAITFOR_0_DISCON |Occurs during SQL Server shutdown, while waiting for outstanding HTTP connections to exit.| 
|SNI_LISTENER_ACCESS |Occurs while waiting for non-uniform memory access (NUMA) nodes to update state change. Access to state change is serialized.| 
|SNI_TASK_COMPLETION |Occurs when there is a wait for all tasks to finish during a NUMA node state change.| 
|SNI_WRITE_ASYNC |TBD <br /> **Applies to**: [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|SOAP_READ |Occurs while waiting for an HTTP network read to complete.| 
|SOAP_WRITE |Occurs while waiting for an HTTP network write to complete.| 
|SOCKETDUPLICATEQUEUE_CLEANUP |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|SOS_CALLBACK_REMOVAL |Occurs while performing synchronization on a callback list in order to remove a callback. It is not expected for this counter to change after server initialization is completed.| 
|SOS_DISPATCHER_MUTEX |Occurs during internal synchronization of the dispatcher pool. This includes when the pool is being adjusted.| 
|SOS_LOCALALLOCATORLIST |Occurs during internal synchronization in the [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] memory manager. <br /> **Applies to**: [!INCLUDE[ssKilimanjaro_md](../../includes/sskilimanjaro-md.md)] only. |  
|SOS_MEMORY_TOPLEVELBLOCKALLOCATOR |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|SOS_MEMORY_USAGE_ADJUSTMENT |Occurs when memory usage is being adjusted among pools.| 
|SOS_OBJECT_STORE_DESTROY_MUTEX |Occurs during internal synchronization in memory pools when destroying objects from the pool.| 
|SOS_PHYS_PAGE_CACHE |Accounts for the time a thread waits to acquire the mutex it must acquire before it allocates physical pages or before it returns those pages to the operating system. Waits on this type only appear if the instance of SQL Server uses AWE memory., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|SOS_PROCESS_AFFINITY_MUTEX |Occurs during synchronizing of access to process affinity settings.| 
|SOS_RESERVEDMEMBLOCKLIST |Occurs during internal synchronization in the SQL Server memory manager. <br /> **Applies to**: [!INCLUDE[ssKilimanjaro_md](../../includes/sskilimanjaro-md.md)] only. |  
|SOS_SCHEDULER_YIELD |Occurs when a task voluntarily yields the scheduler for other tasks to execute. During this wait the task is waiting for its quantum to be renewed.| 
|SOS_SMALL_PAGE_ALLOC |Occurs during the allocation and freeing of memory that is managed by some memory objects.| 
|SOS_STACKSTORE_INIT_MUTEX |Occurs during synchronization of internal store initialization.| 
|SOS_SYNC_TASK_ENQUEUE_EVENT |Occurs when a task is started in a synchronous manner. Most tasks in SQL Server are started in an asynchronous manner, in which control returns to the starter immediately after the task request has been placed on the work queue.| 
|SOS_VIRTUALMEMORY_LOW |Occurs when a memory allocation waits for a resource manager to free up virtual memory.| 
|SOSHOST_EVENT |Occurs when a hosted component, such as CLR, waits on a SQL Server event synchronization object.| 
|SOSHOST_INTERNAL |Occurs during synchronization of memory manager callbacks used by hosted components, such as CLR.| 
|SOSHOST_MUTEX |Occurs when a hosted component, such as CLR, waits on a SQL Server mutex synchronization object.| 
|SOSHOST_RWLOCK |Occurs when a hosted component, such as CLR, waits on a SQL Server reader-writer synchronization object.| 
|SOSHOST_SEMAPHORE |Occurs when a hosted component, such as CLR, waits on a SQL Server semaphore synchronization object.| 
|SOSHOST_SLEEP |Occurs when a hosted task sleeps while waiting for a generic event to occur. Hosted tasks are used by hosted components such as CLR.| 
|SOSHOST_TRACELOCK |Occurs during synchronization of access to trace streams.| 
|SOSHOST_WAITFORDONE |Occurs when a hosted component, such as CLR, waits for a task to complete.| 
|SP_PREEMPTIVE_SERVER_DIAGNOSTICS_SLEEP |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|SP_SERVER_DIAGNOSTICS_BUFFER_ACCESS |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|SP_SERVER_DIAGNOSTICS_INIT_MUTEX |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|SP_SERVER_DIAGNOSTICS_SLEEP |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|SQLCLR_APPDOMAIN |Occurs while CLR waits for an application domain to complete startup.| 
|SQLCLR_ASSEMBLY |Occurs while waiting for access to the loaded assembly list in the appdomain.| 
|SQLCLR_DEADLOCK_DETECTION |Occurs while CLR waits for deadlock detection to complete.| 
|SQLCLR_QUANTUM_PUNISHMENT |Occurs when a CLR task is throttled because it has exceeded its execution quantum. This throttling is done in order to reduce the effect of this resource-intensive task on other tasks.| 
|SQLSORT_NORMMUTEX |Occurs during internal synchronization, while initializing internal sorting structures.| 
|SQLSORT_SORTMUTEX |Occurs during internal synchronization, while initializing internal sorting structures.| 
|SQLTRACE_BUFFER_FLUSH |Occurs when a task is waiting for a background task to flush trace buffers to disk every four seconds. <br /> **Applies to**: [!INCLUDE[ssKilimanjaro_md](../../includes/sskilimanjaro-md.md)] only. |  
|SQLTRACE_FILE_BUFFER |Occurs during synchronization on trace buffers during a file trace., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|SQLTRACE_FILE_READ_IO_COMPLETION |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|SQLTRACE_FILE_WRITE_IO_COMPLETION |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|SQLTRACE_INCREMENTAL_FLUSH_SLEEP |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|SQLTRACE_LOCK |TBD <br /> **APPLIES TO**: [!INCLUDE[ssKilimanjaro_md](../../includes/sskilimanjaro-md.md)] only. |  
|SQLTRACE_PENDING_BUFFER_WRITERS |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|SQLTRACE_SHUTDOWN |Occurs while trace shutdown waits for outstanding trace events to complete.| 
|SQLTRACE_WAIT_ENTRIES |Occurs while a SQL Trace event queue waits for packets to arrive on the queue.| 
|SRVPROC_SHUTDOWN |Occurs while the shutdown process waits for internal resources to be released to shutdown cleanly.| 
|STARTUP_DEPENDENCY_MANAGER |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|TDS_BANDWIDTH_STATE |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|TDS_INIT |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|TDS_PROXY_CONTAINER |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|TEMPOBJ |Occurs when temporary object drops are synchronized. This wait is rare, and only occurs if a task has requested exclusive access for temp table drops.| 
|TEMPORAL_BACKGROUND_PROCEED_CLEANUP |TBD <br /> **Applies to**: [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|TERMINATE_LISTENER |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|THREADPOOL |Occurs when a task is waiting for a worker to run on. This can indicate that the maximum worker setting is too low, or that batch executions are taking unusually long, thus reducing the number of workers available to satisfy other batches.| 
|TIMEPRIV_TIMEPERIOD |Occurs during internal synchronization of the Extended Events timer.| 
|TRACE_EVTNOTIF |TBD| 
|TRACEWRITE |Occurs when the SQL Trace rowset trace provider waits for either a free buffer or a buffer with events to process.| 
|TRAN_MARKLATCH_DT |Occurs when waiting for a destroy mode latch on a transaction mark latch. Transaction mark latches are used for synchronization of commits with marked transactions.| 
|TRAN_MARKLATCH_EX |Occurs when waiting for an exclusive mode latch on a marked transaction. Transaction mark latches are used for synchronization of commits with marked transactions.| 
|TRAN_MARKLATCH_KP |Occurs when waiting for a keep mode latch on a marked transaction. Transaction mark latches are used for synchronization of commits with marked transactions.| 
|TRAN_MARKLATCH_NL |Identified for informational purposes only. Not supported. Future compatibility is not guaranteed.| 
|TRAN_MARKLATCH_SH |Occurs when waiting for a shared mode latch on a marked transaction. Transaction mark latches are used for synchronization of commits with marked transactions.| 
|TRAN_MARKLATCH_UP |Occurs when waiting for an update mode latch on a marked transaction. Transaction mark latches are used for synchronization of commits with marked transactions.| 
|TRANSACTION_MUTEX |Occurs during synchronization of access to a transaction by multiple batches.| 
|UCS_ENDPOINT_CHANGE |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|UCS_MANAGER |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|UCS_MEMORY_NOTIFICATION |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|UCS_SESSION_REGISTRATION |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|UCS_TRANSPORT |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|UCS_TRANSPORT_STREAM_CHANGE |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|UTIL_PAGE_ALLOC |Occurs when transaction log scans wait for memory to be available during memory pressure.| 
|VDI_CLIENT_COMPLETECOMMAND |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|VDI_CLIENT_GETCOMMAND |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|VDI_CLIENT_OPERATION |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|VDI_CLIENT_OTHER |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|VERSIONING_COMMITTING |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|VIA_ACCEPT |Occurs when a Virtual Interface Adapter (VIA) provider connection is completed during startup.| 
|VIEW_DEFINITION_MUTEX |Occurs during synchronization on access to cached view definitions.| 
|WAIT_FOR_RESULTS |Occurs when waiting for a query notification to be triggered.| 
|WAIT_ON_SYNC_STATISTICS_REFRESH |Occurs when waiting for synchronous statistics update to complete before query compilation and execution can resume.<br /> **Applies to**: Starting with [!INCLUDE[sql-server-2019](../../includes/sssqlv15-md.md)]|
|WAIT_SCRIPTDEPLOYMENT_REQUEST |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|WAIT_SCRIPTDEPLOYMENT_WORKER |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|WAIT_XLOGREAD_SIGNAL |TBD <br /> **Applies to**: [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|WAIT_XTP_ASYNC_TX_COMPLETION |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|WAIT_XTP_CKPT_AGENT_WAKEUP |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|WAIT_XTP_CKPT_CLOSE |Occurs when waiting for a checkpoint to complete., <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|WAIT_XTP_CKPT_ENABLED |Occurs when checkpointing is disabled, and waiting for checkpointing to be enabled., <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|WAIT_XTP_CKPT_STATE_LOCK |Occurs when synchronizing checking of checkpoint state., <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|WAIT_XTP_COMPILE_WAIT |TBD <br /> **APPLIES TO**: [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|WAIT_XTP_GUEST |Occurs when the database memory allocator needs to stop receiving low-memory notifications., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|WAIT_XTP_HOST_WAIT |Occurs when waits are triggered by the database engine and implemented by the host., <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|WAIT_XTP_OFFLINE_CKPT_BEFORE_REDO |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|WAIT_XTP_OFFLINE_CKPT_LOG_IO |Occurs when offline checkpoint is waiting for a log read IO to complete., <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|WAIT_XTP_OFFLINE_CKPT_NEW_LOG |Occurs when offline checkpoint is waiting for new log records to scan., <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|WAIT_XTP_PROCEDURE_ENTRY |Occurs when a drop procedure is waiting for all current executions of that procedure to complete., <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|WAIT_XTP_RECOVERY |Occurs when database recovery is waiting for recovery of memory-optimized objects to finish., <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|WAIT_XTP_SERIAL_RECOVERY |TBD <br /> **Applies to**: [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|WAIT_XTP_SWITCH_TO_INACTIVE |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|WAIT_XTP_TASK_SHUTDOWN |Occurs when waiting for an In-Memory OLTP thread to complete., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|WAIT_XTP_TRAN_DEPENDENCY |Occurs when waiting for transaction dependencies., <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|WAITFOR |Occurs as a result of a WAITFOR Transact-SQL statement. The duration of the wait is determined by the parameters to the statement. This is a user-initiated wait.| 
|WAITFOR_PER_QUEUE |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|WAITFOR_TASKSHUTDOWN |Identified for informational purposes only. Not supported. Future compatibility is not guaranteed.| 
|WAITSTAT_MUTEX |Occurs during synchronization of access to the collection of statistics used to populate sys.dm_os_wait_stats.| 
|WCC |Identified for informational purposes only. Not supported. Future compatibility is not guaranteed.| 
|WINDOW_AGGREGATES_MULTIPASS |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|WINFAB_API_CALL |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|WINFAB_REPLICA_BUILD_OPERATION |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|WINFAB_REPORT_FAULT |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|WORKTBL_DROP |Occurs while pausing before retrying, after a failed worktable drop.| 
|WRITE_COMPLETION |Occurs when a write operation is in progress.| 
|WRITELOG |Occurs while waiting for a log flush to complete. Common operations that cause log flushes are checkpoints and transaction commits.| 
|XACT_OWN_TRANSACTION |Occurs while waiting to acquire ownership of a transaction.| 
|XACT_RECLAIM_SESSION |Occurs while waiting for the current owner of a session to release ownership of the session.| 
|XACTLOCKINFO |Occurs during synchronization of access to the list of locks for a transaction. In addition to the transaction itself, the list of locks is accessed by operations such as deadlock detection and lock migration during page splits.| 
|XACTWORKSPACE_MUTEX |Occurs during synchronization of defections from a transaction, as well as the number of database locks between enlist members of a transaction.| 
|XDB_CONN_DUP_HASH |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|XDES_HISTORY |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|XDES_OUT_OF_ORDER_LIST |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|XDES_SNAPSHOT |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|XDESTSVERMGR |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|XE_BUFFERMGR_ALLPROCESSED_EVENT |Occurs when Extended Events session buffers are flushed to targets. This wait occurs on a background thread.| 
|XE_BUFFERMGR_FREEBUF_EVENT |Occurs when either of the following conditions is true:| 
|XE_CALLBACK_LIST |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|XE_CX_FILE_READ |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|XE_DISPATCHER_CONFIG_SESSION_LIST |Occurs when an Extended Events session that is using asynchronous targets is started or stopped. This wait indicates either of the following:| 
|XE_DISPATCHER_JOIN |Occurs when a background thread that is used for Extended Events sessions is terminating.| 
|XE_DISPATCHER_WAIT |Occurs when a background thread that is used for Extended Events sessions is waiting for event buffers to process.| 
|XE_FILE_TARGET_TVF |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|XE_LIVE_TARGET_TVF |TBD <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|XE_MODULEMGR_SYNC |Identified for informational purposes only. Not supported. Future compatibility is not guaranteed.| 
|XE_OLS_LOCK |Identified for informational purposes only. Not supported. Future compatibility is not guaranteed.| 
|XE_PACKAGE_LOCK_BACKOFF |Identified for informational purposes only. Not supported. <br /> **Applies to**: [!INCLUDE[ssKilimanjaro_md](../../includes/sskilimanjaro-md.md)] only. |  
|XE_SERVICES_EVENTMANUAL |TBD| 
|XE_SERVICES_MUTEX |TBD| 
|XE_SERVICES_RWLOCK |TBD| 
|XE_SESSION_CREATE_SYNC |TBD| 
|XE_SESSION_FLUSH |TBD| 
|XE_SESSION_SYNC |TBD| 
|XE_STM_CREATE |TBD| 
|XE_TIMER_EVENT |TBD| 
|XE_TIMER_MUTEX |TBD| 
|XE_TIMER_TASK_DONE |TBD| 
|XIO_CREDENTIAL_MGR_RWLOCK |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|XIO_CREDENTIAL_RWLOCK |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|XIO_EDS_MGR_RWLOCK |TBD <br /> **Applies to**: [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|XIO_EDS_RWLOCK |TBD <br /> **Applies to**: [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|XIO_IOSTATS_BLOBLIST_RWLOCK |TBD <br /> **Applies to**: [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|XIO_IOSTATS_FCBLIST_RWLOCK |TBD <br /> **Applies to**: [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|XIO_LEASE_RENEW_MGR_RWLOCK |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|XTP_HOST_DB_COLLECTION |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|XTP_HOST_LOG_ACTIVITY |TBD <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|XTP_HOST_PARALLEL_RECOVERY |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|XTP_PREEMPTIVE_TASK |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|XTP_TRUNCATION_LSN |TBD <br /> **Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|XTPPROC_CACHE_ACCESS |Occurs when for accessing all natively compiled stored procedure cache objects., <br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].| 
|XTPPROC_PARTITIONED_STACK_CREATE |Occurs when allocating per-NUMA node natively compiled stored procedure cache structures (must be done single threaded) for a given procedure., <br /> **Applies to**: [!INCLUDE[ssSQL12](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].|

  
 The following XEvents are related to partition **SWITCH** and online index rebuild. For information about syntax, see [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md) and [ALTER INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-index-transact-sql.md).  
  
-   lock_request_priority_state  
  
-   process_killed_by_abort_blockers  
  
-   ddl_with_wait_at_low_priority  
  
 For a lock compatibility matrix, see [sys.dm_tran_locks &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-tran-locks-transact-sql.md).  
  
## See also  
    
 [SQL Server Operating System Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-server-operating-system-related-dynamic-management-views-transact-sql.md)   
 [sys.dm_exec_session_wait_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-session-wait-stats-transact-sql.md)   
 [sys.dm_db_wait_stats &#40;Azure SQL Database&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-wait-stats-azure-sql-database.md)  
  
  


