---
title: "sys.query_store_wait_stats (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "04/21/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.query_store_wait_stats"
  - "query_store_wait_stats"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "query_store_wait_stats catalog view"
  - "sys.query_store_wait_stats catalog view"
ms.assetid: ccf7a57c-314b-450c-bd34-70749a02784a
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2017||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.query_store_wait_stats (Transact-SQL)

[!INCLUDE[tsql-appliesto-ss2017-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2017-asdb-xxxx-xxx-md.md)]

  Contains  information about the wait information for the query.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**wait_stats_id**|**bigint**|Identifier of the row representing wait statistics for the plan_id, runtime_stats_interval_id, execution_type and wait_category. It is unique only for the past runtime statistics intervals. For the currently active interval, there may be multiple rows representing wait statistics for the plan referenced by plan_id, with the execution type represented by execution_type and the wait category represented by wait_category. Typically, one row represents wait statistics that are flushed to disk, while other(s) represent in-memory state. Hence, to get actual state for every interval you need to aggregate metrics, grouping by plan_id, runtime_stats_interval_id, execution_type and wait_category. |  
|**plan_id**|**bigint**|Foreign key. Joins to [sys.query_store_plan &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-query-store-plan-transact-sql.md).|  
|**runtime_stats_interval_id**|**bigint**|Foreign key. Joins to [sys.query_store_runtime_stats_interval &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-query-store-runtime-stats-interval-transact-sql.md).|  
|**wait_category**|**tinyint**|Wait types are categorized using the table below, and then wait time is aggregated across these wait categories. Different wait categories require a different follow-up analysis to resolve the issue, but wait types from the same category lead to similar troubleshooting experiences, and providing the affected query in addition to the waits is the missing piece to complete the majority of such investigations successfully.|
|**wait_category_desc**|**nvarchar(128)**|For textual description of the wait category field, review the table below.|
|**execution_type**|**tinyint**|Determines type of query execution:<br /><br /> 0 - Regular execution (successfully finished)<br /><br /> 3 - Client initiated aborted execution<br /><br /> 4 -  Exception aborted execution|  
|**execution_type_desc**|**nvarchar(128)**|Textual description of the execution type field:<br /><br /> 0 -  Regular<br /><br /> 3 -  Aborted<br /><br /> 4 -  Exception|  
|**total_query_wait_time_ms**|**bigint**|Total `CPU wait` time for the query plan within the aggregation interval and wait category (reported in milliseconds).|
|**avg_query_wait_time_ms**|**float**|Average wait duration for the query plan per execution within the aggregation interval and wait category (reported in milliseconds).|
|**last_query_wait_time_ms**|**bigint**|Last wait duration for the query plan within the aggregation interval and wait category (reported in milliseconds).|
|**min_query_wait_time_ms**|**bigint**|Minimum `CPU wait` time for the query plan within the aggregation interval and wait category (reported in milliseconds).|
|**max_query_wait_time_ms**|**bigint**|Maximum `CPU wait` time for the query plan within the aggregation interval and wait category (reported in milliseconds).|
|**stdev_query_wait_time_ms**|**float**|`Query wait` duration standard deviation for the query plan within the aggregation interval and wait category (reported in milliseconds).|

## Wait categories mapping table

"%" is used as a wildcard
  
|Integer value|Wait category|Wait types include in the category|  
|-----------------|---------------|-----------------|  
|**0**|**Unknown**|Unknown |  
|**1**|**CPU**|SOS_SCHEDULER_YIELD|
|**2**|**Worker Thread**|THREADPOOL|
|**3**|**Lock**|LCK_M_%|
|**4**|**Latch**|LATCH_%|
|**5**|**Buffer Latch**|PAGELATCH_%|
|**6**|**Buffer IO**|PAGEIOLATCH_%|
|**7**|**Compilation***|RESOURCE_SEMAPHORE_QUERY_COMPILE|
|**8**|**SQL CLR**|CLR%, SQLCLR%|
|**9**|**Mirroring**|DBMIRROR%|
|**10**|**Transaction**|XACT%, DTC%, TRAN_MARKLATCH_%, MSQL_XACT_%, TRANSACTION_MUTEX|
|**11**|**Idle**|SLEEP_%, LAZYWRITER_SLEEP, SQLTRACE_BUFFER_FLUSH, SQLTRACE_INCREMENTAL_FLUSH_SLEEP, SQLTRACE_WAIT_ENTRIES, FT_IFTS_SCHEDULER_IDLE_WAIT, XE_DISPATCHER_WAIT, REQUEST_FOR_DEADLOCK_SEARCH, LOGMGR_QUEUE, ONDEMAND_TASK_QUEUE, CHECKPOINT_QUEUE, XE_TIMER_EVENT|
|**12**|**Preemptive**|PREEMPTIVE_%|
|**13**|**Service Broker**|BROKER_% **(but not BROKER_RECEIVE_WAITFOR)**|
|**14**|**Tran Log IO**|LOGMGR, LOGBUFFER, LOGMGR_RESERVE_APPEND, LOGMGR_FLUSH, LOGMGR_PMM_LOG, CHKPT, WRITELOGF|
|**15**|**Network IO**|ASYNC_NETWORK_IO, NET_WAITFOR_PACKET, PROXY_NETWORK_IO, EXTERNAL_SCRIPT_NETWORK_IOF|
|**16**|**Parallelism**|CXPACKET, EXCHANGE|
|**17**|**Memory**|RESOURCE_SEMAPHORE, CMEMTHREAD, CMEMPARTITIONED, EE_PMOLOCK, MEMORY_ALLOCATION_EXT, RESERVED_MEMORY_ALLOCATION_EXT, MEMORY_GRANT_UPDATE|
|**18**|**User Wait**|WAITFOR, WAIT_FOR_RESULTS, BROKER_RECEIVE_WAITFOR|
|**19**|**Tracing**|TRACEWRITE, SQLTRACE_LOCK, SQLTRACE_FILE_BUFFER, SQLTRACE_FILE_WRITE_IO_COMPLETION, SQLTRACE_FILE_READ_IO_COMPLETION, SQLTRACE_PENDING_BUFFER_WRITERS, SQLTRACE_SHUTDOWN, QUERY_TRACEOUT, TRACE_EVTNOTIFF|
|**20**|**Full Text Search**|FT_RESTART_CRAWL, FULLTEXT GATHERER, MSSEARCH, FT_METADATA_MUTEX, FT_IFTSHC_MUTEX, FT_IFTSISM_MUTEX, FT_IFTS_RWLOCK, FT_COMPROWSET_RWLOCK, FT_MASTER_MERGE, FT_PROPERTYLIST_CACHE, FT_MASTER_MERGE_COORDINATOR, PWAIT_RESOURCE_SEMAPHORE_FT_PARALLEL_QUERY_SYNC|
|**21**|**Other Disk IO**|ASYNC_IO_COMPLETION, IO_COMPLETION, BACKUPIO, WRITE_COMPLETION, IO_QUEUE_LIMIT, IO_RETRY|
|**22**|**Replication**|SE_REPL_%, REPL_%, HADR_% **(but not HADR_THROTTLE_LOG_RATE_GOVERNOR)**, PWAIT_HADR_%, REPLICA_WRITES, FCB_REPLICA_WRITE, FCB_REPLICA_READ, PWAIT_HADRSIM|
|**23**|**Log Rate Governor**|LOG_RATE_GOVERNOR, POOL_LOG_RATE_GOVERNOR, HADR_THROTTLE_LOG_RATE_GOVERNOR, INSTANCE_LOG_RATE_GOVERNOR|

**Compilation** wait category is currently not supported.

## Permissions

 Requires the `VIEW DATABASE STATE` permission.  
  
## See Also

- [sys.database_query_store_options &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-query-store-options-transact-sql.md)
- [sys.query_context_settings &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-query-context-settings-transact-sql.md)
- [sys.query_store_plan &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-query-store-plan-transact-sql.md)
- [sys.query_store_query &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-query-store-query-transact-sql.md)
- [sys.query_store_query_text &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-query-store-query-text-transact-sql.md)
- [sys.query_store_runtime_stats_interval &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-query-store-runtime-stats-interval-transact-sql.md)
- [Monitoring Performance By Using the Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md)
- [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)
- [Query Store Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/query-store-stored-procedures-transact-sql.md)  
