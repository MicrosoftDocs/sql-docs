---
title: "sys.dm_os_latch_stats (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/18/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_os_latch_stats_TSQL"
  - "dm_os_latch_stats_TSQL"
  - "dm_os_latch_stats"
  - "sys.dm_os_latch_stats"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_os_latch_stats dynamic management view"
ms.assetid: 2085d9fc-828c-453e-82ec-b54ed8347ae5
author: stevestein
ms.author: sstein
manager: craigg
---
# sys.dm_os_latch_stats (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns information about all latch waits organized by class.  
  
> [!NOTE]  
>  To call this from [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the name **sys.dm_pdw_nodes_os_latch_stats**.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|latch_class|**nvarchar(120)**|Name of the latch class.|  
|waiting_requests_count|**bigint**|Number of waits on latches in this class. This counter is incremented at the start of a latch wait.|  
|wait_time_ms|**bigint**|Total wait time, in milliseconds, on latches in this class.<br /><br /> **Note:** This column is updated every five minutes during a latch wait and at the end of a latch wait.|  
|max_wait_time_ms|**bigint**|Maximum time a memory object has waited on this latch. If this value is unusually high, it might indicate an internal deadlock.|  
|pdw_node_id|**int**|**Applies to**: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]<br /><br /> The identifier for the node that this distribution is on.|  
  
## Permissions  

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)], requires `VIEW SERVER STATE` permission.   
On [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)], requires the `VIEW DATABASE STATE` permission in the database.   
  
## Remarks  
 sys.dm_os_latch_stats can be used to identify the source of latch contention by examining the relative wait numbers and wait times for the different latch classes. In some situations, you may be able to resolve or reduce latch contention. However, there might be situations that will require that you to contact [!INCLUDE[msCoName](../../includes/msconame-md.md)] Customer Support Services.  
  
 You can reset the contents of sys.dm_os_latch_stats by using `DBCC SQLPERF` as follows:  
  
```  
DBCC SQLPERF ('sys.dm_os_latch_stats', CLEAR);  
GO  
```  
  
 This resets all counters to 0.  
  
> [!NOTE]  
>  These statistics are not persisted if [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is restarted. All data is cumulative since the last time the statistics were reset, or since [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] was started.  
  
## Latches  
 A latch is a lightweight synchronization object that is used by various [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components. A latch is primarily used to synchronize database pages. Each latch is associated with a single allocation unit.  
  
 A latch wait occurs when a latch request cannot be granted immediately, because the latch is held by another thread in a conflicting mode. Unlike locks, a latch is released immediately after the operation, even in write operations.  
  
 Latches are grouped into classes based on components and usage. Zero or more latches of a particular class can exist at any point in time in an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
> [!NOTE]  
>  sys.dm_os_latch_stats does not track latch requests that were granted immediately, or that failed without waiting.  
  
 The following table contains brief descriptions of the various latch classes.  
  
|Latch class|Description|  
|-----------------|-----------------|  
|ALLOC_CREATE_RINGBUF|Used internally by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to initialize the synchronization of the creation of an allocation ring buffer.|  
|ALLOC_CREATE_FREESPACE_CACHE|Used to initialize the synchronization of internal freespace caches for heaps.|  
|ALLOC_CACHE_MANAGER|Used to synchronize internal coherency tests.|  
|ALLOC_FREESPACE_CACHE|Used to synchronize the access to a cache of pages with available space for heaps and binary large objects (BLOBs). Contention on latches of this class can occur when multiple connections try to insert rows into a heap or BLOB at the same time. You can reduce this contention by partitioning the object. Each partition has its own latch. Partitioning will distribute the inserts across multiple latches.|  
|ALLOC_EXTENT_CACHE|Used to synchronize the access to a cache of extents that contains pages that are not allocated. Contention on latches of this class can occur when multiple connections try to allocate data pages in the same allocation unit at the same time. This contention can be reduced by partitioning the object of which this allocation unit is a part.|  
|ACCESS_METHODS_DATASET_PARENT|Used to synchronize child dataset access to the parent dataset during parallel operations.|  
|ACCESS_METHODS_HOBT_FACTORY|Used to synchronize access to an internal hash table.|  
|ACCESS_METHODS_HOBT|Used to synchronize access to the in-memory representation of a HoBt.|  
|ACCESS_METHODS_HOBT_COUNT|Used to synchronize access to a HoBt page and row counters.|  
|ACCESS_METHODS_HOBT_VIRTUAL_ROOT|Used to synchronize access to the root page abstraction of an internal B-tree.|  
|ACCESS_METHODS_CACHE_ONLY_HOBT_ALLOC|Used to synchronize worktable access.|  
|ACCESS_METHODS_BULK_ALLOC|Used to synchronize access within bulk allocators.|  
|ACCESS_METHODS_SCAN_RANGE_GENERATOR|Used to synchronize access to a range generator during parallel scans.|  
|ACCESS_METHODS_KEY_RANGE_GENERATOR|Used to synchronize access to read-ahead operations during key range parallel scans.|  
|APPEND_ONLY_STORAGE_INSERT_POINT|Used to synchronize inserts in fast append-only storage units.|  
|APPEND_ONLY_STORAGE_FIRST_ALLOC|Used to synchronize the first allocation for an append-only storage unit.|  
|APPEND_ONLY_STORAGE_UNIT_MANAGER|Used for internal data structure access synchronization within the fast append-only storage unit manager.|  
|APPEND_ONLY_STORAGE_MANAGER|Used to synchronize shrink operations in the fast append-only storage unit manager.|  
|BACKUP_RESULT_SET|Used to synchronize parallel backup result sets.|  
|BACKUP_TAPE_POOL|Used to synchronize backup tape pools.|  
|BACKUP_LOG_REDO|Used to synchronize backup log redo operations.|  
|BACKUP_INSTANCE_ID|Used to synchronize the generation of instance IDs for backup performance monitor counters.|  
|BACKUP_MANAGER|Used to synchronize the internal backup manager.|  
|BACKUP_MANAGER_DIFFERENTIAL|Used to synchronize differential backup operations with DBCC.|  
|BACKUP_OPERATION|Used for internal data structure synchronization within a backup operation, such as database, log, or file backup.|  
|BACKUP_FILE_HANDLE|Used to synchronize file open operations during a restore operation.|  
|BUFFER|Used to synchronize short term access to database pages. A buffer latch is required before reading or modifying any database page. Buffer latch contention can indicate several issues, including hot pages and slow I/Os.<br /><br /> This latch class covers all possible uses of page latches. sys.dm_os_wait_stats makes a difference between page latch waits that are caused by I/O operations and read and write operations on the page.|  
|BUFFER_POOL_GROW|Used for internal buffer manager synchronization during buffer pool grow operations.|  
|DATABASE_CHECKPOINT|Used to serialize checkpoints within a database.|  
|CLR_PROCEDURE_HASHTABLE|Internal use only.|  
|CLR_UDX_STORE|Internal use only.|  
|CLR_DATAT_ACCESS|Internal use only.|  
|CLR_XVAR_PROXY_LIST|Internal use only.|  
|DBCC_CHECK_AGGREGATE|Internal use only.|  
|DBCC_CHECK_RESULTSET|Internal use only.|  
|DBCC_CHECK_TABLE|Internal use only.|  
|DBCC_CHECK_TABLE_INIT|Internal use only.|  
|DBCC_CHECK_TRACE_LIST|Internal use only.|  
|DBCC_FILE_CHECK_OBJECT|Internal use only.|  
|DBCC_PERF|Used to synchronize internal performance monitor counters.|  
|DBCC_PFS_STATUS|Internal use only.|  
|DBCC_OBJECT_METADATA|Internal use only.|  
|DBCC_HASH_DLL|Internal use only.|  
|EVENTING_CACHE|Internal use only.|  
|FCB|Used to synchronize access to the file control block.|  
|FCB_REPLICA|Internal use only.|  
|FGCB_ALLOC|Use to synchronize access to round robin allocation information within a filegroup.|  
|FGCB_ADD_REMOVE|Use to synchronize access to filegroups for add, drop, grow, and shrink file operations.|  
|FILEGROUP_MANAGER|Internal use only.|  
|FILE_MANAGER|Internal use only.|  
|FILESTREAM_FCB|Internal use only.|  
|FILESTREAM_FILE_MANAGER|Internal use only.|  
|FILESTREAM_GHOST_FILES|Internal use only.|  
|FILESTREAM_DFS_ROOT|Internal use only.|  
|LOG_MANAGER|Internal use only.|  
|FULLTEXT_DOCUMENT_ID|Internal use only.|  
|FULLTEXT_DOCUMENT_ID_TRANSACTION|Internal use only.|  
|FULLTEXT_DOCUMENT_ID_NOTIFY|Internal use only.|  
|FULLTEXT_LOGS|Internal use only.|  
|FULLTEXT_CRAWL_LOG|Internal use only.|  
|FULLTEXT_ADMIN|Internal use only.|  
|FULLTEXT_AMDIN_COMMAND_CACHE|Internal use only.|  
|FULLTEXT_LANGUAGE_TABLE|Internal use only.|  
|FULLTEXT_CRAWL_DM_LIST|Internal use only.|  
|FULLTEXT_CRAWL_CATALOG|Internal use only.|  
|FULLTEXT_FILE_MANAGER|Internal use only.|  
|DATABASE_MIRRORING_REDO|Internal use only.|  
|DATABASE_MIRRORING_SERVER|Internal use only.|  
|DATABASE_MIRRORING_CONNECTION|Internal use only.|  
|DATABASE_MIRRORING_STREAM|Internal use only.|  
|QUERY_OPTIMIZER_VD_MANAGER|Internal use only.|  
|QUERY_OPTIMIZER_ID_MANAGER|Internal use only.|  
|QUERY_OPTIMIZER_VIEW_REP|Internal use only.|  
|RECOVERY_BAD_PAGE_TABLE|Internal use only.|  
|RECOVERY_MANAGER|Internal use only.|  
|SECURITY_OPERATION_RULE_TABLE|Internal use only.|  
|SECURITY_OBJPERM_CACHE|Internal use only.|  
|SECURITY_CRYPTO|Internal use only.|  
|SECURITY_KEY_RING|Internal use only.|  
|SECURITY_KEY_LIST|Internal use only.|  
|SERVICE_BROKER_CONNECTION_RECEIVE|Internal use only.|  
|SERVICE_BROKER_TRANSMISSION|Internal use only.|  
|SERVICE_BROKER_TRANSMISSION_UPDATE|Internal use only.|  
|SERVICE_BROKER_TRANSMISSION_STATE|Internal use only.|  
|SERVICE_BROKER_TRANSMISSION_ERRORS|Internal use only.|  
|SSBXmitWork|Internal use only.|  
|SERVICE_BROKER_MESSAGE_TRANSMISSION|Internal use only.|  
|SERVICE_BROKER_MAP_MANAGER|Internal use only.|  
|SERVICE_BROKER_HOST_NAME|Internal use only.|  
|SERVICE_BROKER_READ_CACHE|Internal use only.|  
|SERVICE_BROKER_WAITFOR_MANAGER| Used to synchronize an instance level map of waiter queues. One queue exists per database ID, Database Version, and Queue ID tuple. Contention on latches of this class can occur when many connections are: In a WAITFOR(RECEIVE) wait state; calling WAITFOR(RECEIVE); exceeding the WAITFOR timeout; receiving a message; committing or rolling back the transaction that contains the WAITFOR(RECEIVE); You can reduce the contention by reducing the number of threads in a WAITFOR(RECEIVE) wait state. |  
|SERVICE_BROKER_WAITFOR_TRANSACTION_DATA|Internal use only.|  
|SERVICE_BROKER_TRANSMISSION_TRANSACTION_DATA|Internal use only.|  
|SERVICE_BROKER_TRANSPORT|Internal use only.|  
|SERVICE_BROKER_MIRROR_ROUTE|Internal use only.|  
|TRACE_ID|Internal use only.|  
|TRACE_AUDIT_ID|Internal use only.|  
|TRACE|Internal use only.|  
|TRACE_CONTROLLER|Internal use only.|  
|TRACE_EVENT_QUEUE|Internal use only.|  
|TRANSACTION_DISTRIBUTED_MARK|Internal use only.|  
|TRANSACTION_OUTCOME|Internal use only.|  
|NESTING_TRANSACTION_READONLY|Internal use only.|  
|NESTING_TRANSACTION_FULL|Internal use only.|  
|MSQL_TRANSACTION_MANAGER|Internal use only.|  
|DATABASE_AUTONAME_MANAGER|Internal use only.|  
|UTILITY_DYNAMIC_VECTOR|Internal use only.|  
|UTILITY_SPARSE_BITMAP|Internal use only.|  
|UTILITY_DATABASE_DROP|Internal use only.|  
|UTILITY_DYNAMIC_MANAGER_VIEW|Internal use only.|  
|UTILITY_DEBUG_FILESTREAM|Internal use only.|  
|UTILITY_LOCK_INFORMATION|Internal use only.|  
|VERSIONING_TRANSACTION|Internal use only.|  
|VERSIONING_TRANSACTION_LIST|Internal use only.|  
|VERSIONING_TRANSACTION_CHAIN|Internal use only.|  
|VERSIONING_STATE|Internal use only.|  
|VERSIONING_STATE_CHANGE|Internal use only.|  
|KTM_VIRTUAL_CLOCK|Internal use only.|  
  
## See Also  
 
 [DBCC SQLPERF &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-sqlperf-transact-sql.md)   
 
 [SQL Server Operating System Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-server-operating-system-related-dynamic-management-views-transact-sql.md)  
  
  


