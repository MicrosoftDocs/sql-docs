---
title: "sys.dm_os_spinlock_stats (Transact-SQL)"
description: Returns information about all spinlock waits organized by type.
author: "bluefooted"
ms.author: "pamela"
ms.reviewer: wiassaf, randolphwest
ms.date: 12/02/2022
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_os_spinlock_stats_TSQL"
  - "dm_os_spinlock_stats_TSQL"
  - "dm_os_spinlock_stats"
  - "sys.dm_os_spinlock_stats"
helpviewer_keywords:
  - "sys.dm_os_spinlock_stats dynamic management view"
dev_langs:
  - "TSQL"
---
# sys.dm_os_spinlock_stats (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns information about all spinlock waits organized by type.

| Column name | Data type | Description |
| --- | --- | --- |
| name | **nvarchar(256)** | Name of the spinlock type. |
| collisions | **bigint** | The number of times a thread attempts to acquire the spinlock and is blocked because another thread currently holds the spinlock. |
| spins | **bigint** | The number of times a thread executes a loop while attempting to acquire the spinlock. |
| spins_per_collision | **real** | Ratio of spins per collision. |
| sleep_time | **bigint** | The amount of time in milliseconds that threads spent sleeping in the event of a backoff. |
| backoffs | **bigint** | The number of times a thread that is "spinning" fails to acquire the spinlock and yields the scheduler. |

## Permissions

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] and SQL Managed Instance, requires `VIEW SERVER STATE` permission.

On Azure SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Azure Active Directory admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.

## Remarks

`sys.dm_os_spinlock_stats` can be used to identify the source of spinlock contention. In some situations, you may be able to resolve or reduce spinlock contention. However, there might be situations that will require that you to contact [!INCLUDE[msCoName](../../includes/msconame-md.md)] Customer Support Services.

You can reset the contents of `sys.dm_os_spinlock_stats` by using `DBCC SQLPERF` as follows:

```sql
DBCC SQLPERF ('sys.dm_os_spinlock_stats', CLEAR);
GO
```

This resets all counters to 0.

> [!NOTE]  
> These statistics are not persisted if [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is restarted. All data is cumulative since the last time the statistics were reset, or since [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] was started.

## Spinlocks

A spinlock is a lightweight synchronization object used to serialize access to data structures which are typically held for a short period of time. When a thread attempts to access a resource protected by a spinlock which is being held by another thread, the thread will execute a loop, or "spin" and try accessing the resource again, rather than immediately yielding the scheduler as with a latch or other resource wait. The thread will continue spinning until the resource is available, or the loop completes, at which point the thread will yield the scheduler and go back into the runnable queue. This practice helps reduce excessive thread context switching, but when contention for a spinlock is high, significant CPU utilization may be observed.

Internal adjustments to the Database Engine introduced in [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] make spinlocks more efficient.

> [!NOTE]  
> If you have a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installed on Intel Skylake processors please review [KB4538688](https://support.microsoft.com/help/4538688) to apply the required update and enable [Trace Flag 8101](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf8101).

The following table contains brief descriptions of some of the most common spinlock types.

| Spinlock type | Description |
| --- | --- |
| ABR | Internal use only. |
| ADB_CACHE | Internal use only. |
| ALLOC_CACHES_HASH | Internal use only. |
| APPENDONLY_STORAGE | Internal use only. |
| APRC_BACK_OFF_STATS | Internal use only. |
| APRC_EVENT_LIST | Internal use only. |
| APRC_QUEUE_LIST | Internal use only. |
| APRC_VALIDATION_QUEUE_LIST | Internal use only. |
| ASYNC_OP_ADMIN_CLIENT_REGISTRATION_LIST | Internal use only. |
| ASYNC_OP_ADMIN_WORK_REGISTRATION_HASH_TABLE | Internal use only. |
| ASYNCSTATSLIST | Internal use only. |
| BACKUP | Internal use only. |
| BACKUP_COPY_CONTEXT | Internal use only. |
| BACKUP_CTX | Protects access to list of pages involved in I/O while a backup is happening on that particular database. High spins could be observed when long checkpoints or lazwriter activity happen during backup operations. You can obtain relief using one of the following methods:<br /><br />- a) Use [indirect checkpoint](../../relational-databases/logs/database-checkpoints-sql-server.md#IndirectChkpt) instead of [automatic checkpoint](../../relational-databases/logs/database-checkpoints-sql-server.md#AutomaticChkpt)<br /><br />- b) Minimize lazywriter activity by properly allocating memory required for this instance<br /><br />- c) Avoid too many concurrent backups for databases on the instance |
| BASE_XACT_HASH | Internal use only. |
| BLOCKER_ENUM | Internal use only. |
| BPREPARTITION | Internal use only. |
| BPWORKFILE | Internal use only. |
| BUF_HASH | Internal use only. |
| BUF_LINK | Internal use only. |
| BUF_WRITE_LOG | Internal use only. |
| CACHEOBJ_DBG | Internal use only. |
| CHANNELFORCECLOSEMANAGER | Internal use only. |
| CHECK_AGGREGATE_STATE | Internal use only. |
| CLR_HOSTTASK | Internal use only. |
| CLR_SPIN_LOCK | Internal use only. |
| CMED_DATABASE | Internal use only. |
| CMED_HASH_SET | Internal use only.<br /><br />**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] CU 1)<br /><br />**Note:** this spinlock name changes to LOCK_RW_CMED_HASH_SET after you apply [SQL Server 2016 CU 2](https://support.microsoft.com/help/3195888). |
| COLUMNDATASETSESSIONLIST | Internal use only. |
| COLUMNSTORE_HASHTABLE | Internal use only. |
| COLUMNSTOREBUILDSTATE_LIST | Internal use only. |
| COM_INIT | Internal use only. |
| COMMITTABLE | Internal use only. |
| COMPPLAN_SKELETON | Internal use only. |
| CONNECTION_MANAGER | Internal use only. |
| CONNECTS | Internal use only. |
| CSIBUILDMEM | Internal use only. |
| CURSOR | Internal use only. |
| CURSQL | Internal use only. |
| DATAPORTCONSUMER | Internal use only. |
| DATAPORTSOURCEINFOCREDIT | Internal use only. |
| DATAPORTSOURCEINFOQUEUE | Internal use only. |
| DATASET_FREELIST | Internal use only. |
| DBCC_CHECK | Internal use only. |
| DBSEEDING_OPERATION | Internal use only. |
| DBT_HASH | Internal use only. |
| DBT_IO_LIST | Internal use only. |
| DBTABLE | Controls access to an in-memory data structure for every database in a [!INCLUDE[ssde_md](../../includes/ssde_md.md)] that contains the properties of that database. For more information, see [Improving Concurrency and Scalability of SQL Server workload by optimizing database containment check in SQL 2014 and SQL 2016](https://techcommunity.microsoft.com/t5/SQL-Server/Improving-Concurrency-Scalability-of-SQL-Server-workload-by/ba-p/384789). |
| DEFERRED_WF_EXT_DROP | Internal use only. |
| DEK_INSTANCE | Internal use only. |
| DELAYED_PARTITIONED_STACK | Internal use only. |
| DELETEBITMAP | Internal use only. |
| DIAG_MANAGER | Internal use only. |
| DIAG_OBJECT | Internal use only. |
| DIGEST_CACHE | Internal use only. |
| DINPBUF | Internal use only. |
| DIRECTLOGCONSUMER | Internal use only. |
| DP_LIST | Controls access to the list of dirty pages for a database that has indirect checkpoint turned on. Apply fixes from [KB4497928](https://support.microsoft.com/help/4497928), [KB4040276](https://support.microsoft.com/help/4040276), or use [Trace Flag 3468](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf3468). For more information, see [Indirect Checkpoint and tempdb - the good, the bad and the non-yielding scheduler](https://techcommunity.microsoft.com/t5/SQL-Server/Indirect-Checkpoint-and-tempdb-8211-the-good-the-bad-and-the-non/ba-p/385510). |
| DROP | Internal use only. |
| DROP_TEMPO | Internal use only. |
| DROPPED_ALLOC_UNIT | Internal use only. |
| DTC_HASHTABLE | Internal use only. |
| DTT_LIST | Internal use only. |
| ENDD_LIST | Internal use only. |
| EXT_CACHE | Internal use only. |
| EXTENT_ACTIVATION | Internal use only. |
| FABRIC_DB_MGR_PTR | Internal use only. |
| FABRIC_LOG_MANAGEMENT_INPUT_VALUE | Internal use only. |
| FABRIC_REPLICA_TRANSPORT | Internal use only. |
| FABRIC_TVF_DATA_CONSUMER_LIST | Internal use only. |
| FABRIC_TVF_LOAD_LIB | Internal use only. |
| FCB_REPLICA_SYNC | Internal use only. |
| FGCB_PRP_FILL | Internal use only. |
| FILE_HANDLE_CACHE | Internal use only. |
| FILE_TABLE | Internal use only. |
| FILESTREAM_CHUNKER | Internal use only. |
| FREE_SPACE_CACHE_ENTRY | Internal use only. |
| FS_CONTAINER_LIST_WITH_DELETE | Internal use only. |
| FS_DELETED_FOLDER_CLEANUP | Internal use only. |
| FSAGENT | Internal use only. |
| FSGHOST_STATUS | Internal use only. |
| FT_INIT | Internal use only. |
| GHOST_FREE | Internal use only. |
| GHOST_HASH | Internal use only. |
| GLOBAL_SCHEDULER_LIST | Internal use only. |
| GLOBAL_TRACE_FLAGS | Internal use only. |
| GLOBALTRANS | Internal use only. |
| GROUP_COMMIT_FEEDBACK_LOOP | Internal use only. |
| GUARDIAN | Internal use only. |
| HADR_AGH_X_ACCESS | Internal use only. |
| HADR_AR_CONTROLLER_COLLECTION | Internal use only. |
| HADR_AR_DB_MGR | Internal use only. |
| HADR_AR_TRANSPORT | Internal use only. |
| HADR_COMPRESSION_MGR_POOL | Internal use only. |
| HADR_FABRIC_FACTORY | Internal use only. |
| HADR_PRIORITY_QUEUE | Internal use only. |
| HADR_TRANSPORT_CONTROL | Internal use only. |
| HADR_TRANSPORT_LIST | Internal use only. |
| HADRSEEDINGLIST | Internal use only. |
| HOBT_DROPPED | Internal use only. |
| HOBT_HASH | Internal use only. |
| HTTP | Internal use only. |
| HTTP_CONNCACHE | Internal use only. |
| HTTP_ENDPOINT | Internal use only. |
| IDENTITY | Internal use only. |
| INDEX_CREATE | Internal use only. |
| IO_DISPENSER_PAUSE | Internal use only. |
| IO_RG_VOLUME_HASHTABLE | Internal use only. |
| IOREQ | Internal use only. |
| ISSRESOURCE | Internal use only. |
| KTM_ENLISTMENT | Internal use only. |
| LANG_RES_LOAD | Internal use only. |
| LIVE_TARGET_TVF | Internal use only. |
| LOCK_FREE_LIST | Internal use only. |
| LOCK_HASH | Protects access to the lock manager hash table that stores information about the locks being held in a database. For more information, see [KB2926217](https://support.microsoft.com/help/2926217) and the [Transaction Locking and Row Versioning Guide](../../relational-databases/sql-server-transaction-locking-and-row-versioning-guide.md#Lock_Engine). |
| LOCK_NOTIFICATION | Internal use only. |
| LOCK_RESOURCE_ID | Internal use only. |
| LOCK_RW_ABTX_HASH_SET | Internal use only. |
| LOCK_RW_AGDB_HEALTH_DIAG | Internal use only. |
| LOCK_RW_CMED_HASH_SET | Internal use only.<br /><br />**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] CU 2), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], and [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)] |
| LOCK_RW_DPT_TABLE | Internal use only. |
| LOCK_RW_IN_ROW_TRACKER | Internal use only. |
| LOCK_RW_LOGIN_RATE_STATS | Internal use only. |
| LOCK_RW_PVS_PAGE_TRACKER | Internal use only. |
| LOCK_RW_RBIO_REQ | Internal use only. |
| LOCK_RW_SECURITY_CACHE | Protects the cache entries related to security tokens and access checks.<br /><br />**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] CU 2), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], and [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)]<br /><br />If the entries in TokenAndPermUserStore cache store grows continuously, you might notice large spins for this spinlock. Evaluate using Trace Flags [4610](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf4610) and [4618](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf4618) to limit entries. For more information, see [access check cache Server Configuration Options](../../database-engine/configure-windows/access-check-cache-server-configuration-options.md), [Queries take longer to finish when the size of the TokenAndPermUserStore cache grows in SQL Server](/troubleshoot/sql/performance/token-and-perm-user-store-perf-issue), and [Query Performance issues associated with a large sized security cache](https://techcommunity.microsoft.com/t5/sql-server-support/query-performance-issues-associated-with-a-large-sized-security/ba-p/315494). |
| LOCK_RW_TEST | Internal use only. |
| LOCK_RW_WPR_BUCKET | Internal use only. |
| LOCK_SORT_STREAM | Internal use only. |
| LOCK_SQLSATELLITE_MESSAGE | Internal use only. |
| LOG_CONSOLIDATION | Internal use only. |
| LOG_RG_GOVERNOR | Internal use only. |
| LOGCACHE_ACCESS | Internal use only. |
| LOGFLUSHQ | Internal use only. |
| LOGIOSEQ | Internal use only. |
| LOGIOSEQMAPPENDINGMESSAGEQUEUE | Internal use only. |
| LOGLC | Internal use only. |
| LOGLFM | Internal use only. |
| LOGON_TRIGGER_CACHE | Internal use only. |
| LOGPOOL_HASHBUCKET | Internal use only. |
| LOGPOOL_REFCOUNTEDOBJECT | Internal use only. |
| LOGPOOL_SHAREDCACHEBUFFER | Internal use only. |
| LOGPOOL_SIZEPERRESOURCEPOOL | Internal use only. |
| LPE_BATCH | Internal use only. |
| LPE_SESSION | Internal use only. |
| LPE_SXTP | Internal use only. |
| LSID | Internal use only. |
| LSLIST | Internal use only. |
| LSNREFLIST | Internal use only. |
| LSS_SYNC_DTC | Internal use only. |
| MD_CHANGE_NOTIFICATION | Internal use only. |
| MDB_REMOTE_BATCH_STATS_HASH_TABLE | Internal use only. |
| MDB_REMOTE_SESSION_HASH_TABLE | Internal use only. |
| MEM_MGR | Internal use only. |
| MGR_CACHE | Internal use only. |
| MIGRATION_BUF_LIST | Internal use only. |
| MUTEX | Protects the cache entries related to security tokens and access checks.<br /><br />**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Up to [!INCLUDE[sssql11-md](../../includes/sssql11-md.md)])<br /><br />If the entries in TokenAndPermUserStore cache store grows continuously, you might notice large spins for this spinlock. Evaluate using Trace Flags [4610](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf4610) and [4618](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf4618) to limit entries. For more information, see [access check cache Server Configuration Options](../../database-engine/configure-windows/access-check-cache-server-configuration-options.md), [Queries take longer to finish when the size of the TokenAndPermUserStore cache grows in SQL Server](/troubleshoot/sql/performance/token-and-perm-user-store-perf-issue), and [Query Performance issues associated with a large sized security cache](https://techcommunity.microsoft.com/t5/sql-server-support/query-performance-issues-associated-with-a-large-sized-security/ba-p/315494). |
| NETCONN_ADDRESS | Internal use only. |
| ONDEMAND_TASK | Internal use only. |
| ONE_PROC_SIM_NODE_CONTEXT | Internal use only. |
| ONE_PROC_SIM_NODE_CONTEXT_LIST | Internal use only. |
| ONE_PROC_SIM_REPLICA_CONTEXT | Internal use only. |
| ONE_PROC_SIM_SERVICE_PARTITION | Internal use only. |
| OPT_IDX_MISS_ID | Internal use only. |
| OPT_IDX_MISS_KEY | Internal use only. |
| OPT_IDX_STATS | Internal use only. |
| OPT_INFO_MGR | Internal use only. |
| PAGE_WORKITEMLIST | Internal use only. |
| PAGECOPIER | Internal use only. |
| PARALLELREDOCACHE | Internal use only. |
| PARTITIONED_HEAP_FREE_LIST | Internal use only. |
| PROGRESS_REPORT | Internal use only. |
| QE_SHUTDOWN | Internal use only. |
| QSCAN_CACHE | Internal use only. |
| QUERY_EXEC_STATS | Internal use only. |
| QUERY_STORE_ASYNC_PERSIST | Internal use only. |
| QUERY_STORE_ASYNC_QUEUE_TLIST | Internal use only. |
| QUERY_STORE_CAPTURE_POLICY_INTERVAL | Internal use only. |
| QUERY_STORE_CAPTURE_POLICY_STATS | Internal use only. |
| QUERY_STORE_CAPTURE_POLICY_THRESHOLD | Internal use only. |
| QUERY_STORE_CURRENT_INTERVAL | Internal use only. |
| QUERY_STORE_HT_CACHE | Internal use only. |
| QUERY_STORE_LIST | Internal use only. |
| QUERY_STORE_PLAN_COMP_AGG | Internal use only. |
| QUERY_STORE_PLAN_LIST | Internal use only. |
| QUERY_STORE_READ_ONLY_FLAGS | Internal use only. |
| QUERY_STORE_SELF_AGG | Internal use only. |
| QUERY_STORE_STMT_COMP_AGG | Internal use only. |
| QUERYEXEC | Internal use only. |
| QUERYSCAN | Internal use only. |
| RANGE_GENERATION | Internal use only. |
| READ_AHEAD | Internal use only. |
| REDOMGRSTATE | Internal use only. |
| REMOTE_SESSION_CACHE | Internal use only. |
| REMOTEBLOCKIO | Internal use only. |
| REMOTEOP | Internal use only. |
| REPL_LOGREADER_HISTORY_CACHE | Internal use only. |
| REPL_LOGREADER_PERDB_HISTORY_CACHE | Internal use only. |
| RESMANAGER | Internal use only. |
| RESOURCE | Internal use only. |
| RESQUEUE | Internal use only. |
| RFS_THREAD_QUEUE | Internal use only. |
| RG_TIMER | Internal use only. |
| ROWGROUP_VERSIONS | Internal use only. |
| RPCCHANNELPOOL | Internal use only. |
| RPCPACKAGE | Internal use only. |
| RPCREQUESTORCONTEXT | Internal use only. |
| RWLOCK_LAST | Internal use only. |
| SATELLITE_CONNECTION | Internal use only. |
| SBS_CLIENT_ENDPOINTS | Internal use only. |
| SBS_CLIENT_REQUESTS | Internal use only. |
| SBS_DISPATCH | Internal use only. |
| SBS_PENDING | Internal use only. |
| SBS_SERVER_XACT_TASK_PROXY | Internal use only. |
| SBS_TRANSPORT | Internal use only. |
| SBS_UCS_DISPATCH | Internal use only. |
| SECURITY | Internal use only. |
| SECURITY_CACHE | Protects the cache entries related to security tokens and access checks.<br /><br />**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] CU 1)<br /><br />If the entries in TokenAndPermUserStore cache store grows continuously, you might notice large spins for this spinlock. Evaluate using Trace Flags [4610](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf4610) and [4618](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf4618) to limit entries. For more information, see [access check cache Server Configuration Options](../../database-engine/configure-windows/access-check-cache-server-configuration-options.md), [Queries take longer to finish when the size of the TokenAndPermUserStore cache grows in SQL Server](/troubleshoot/sql/performance/token-and-perm-user-store-perf-issue), and [Query Performance issues associated with a large sized security cache](https://techcommunity.microsoft.com/t5/sql-server-support/query-performance-issues-associated-with-a-large-sized-security/ba-p/315494).<br /><br />**Note:** this spinlock name changes to LOCK_RW_SECURITY_CACHE after you apply [SQL Server 2016 CU 2](https://support.microsoft.com/help/3195888). |
| SECURITY_FEDAUTH_AAD_BECWSCONNS | Internal use only. |
| SEMANTIC_TICACHE | Internal use only. |
| SEQUENCED_OBJECT | Internal use only. |
| SEQUEUE_SIZED_THREADSAFE | Internal use only. |
| SESSION_KILLER | Internal use only. |
| SESSION_MANAGER | Internal use only. |
| SESSION_SEC_CONTEXT | Internal use only. |
| SETRANGE_SYNC | Internal use only. |
| SHARABLE_SESSION_OBJECTS | Internal use only. |
| SLO_INFO_LIST | Internal use only. |
| SNI | Internal use only. |
| SNI_NODE_PENDING_IO_QUEUE | Internal use only. |
| SOAPSESSIONS | Internal use only. |
| SOS_ABORT_TASK | Internal use only. |
| SOS_ACTIVEDESCRIPTOR | Internal use only. |
| SOS_BLOCKALLOCPARTIALLIST | Internal use only. |
| SOS_BLOCKDESCRIPTORBUCKET | Internal use only. |
| SOS_CACHESTORE | Synchronizes access to various in-memory caches in the [!INCLUDE[ssde_md](../../includes/ssde_md.md)], such as the plan cache or temp table cache. Heavy contention on this spinlock type can mean many different things depending on the specific cache that is in contention. Contact [!INCLUDE[msCoName](../../includes/msconame-md.md)] Customer Support Services for help troubleshooting this spinlock type. |
| SOS_CACHESTORE_CLOCK | Internal use only. |
| SOS_CLOCKALG_INTERNODE_SYNC | Internal use only. |
| SOS_DEBUG_HOOK | Internal use only. |
| SOS_DESCDATABUFFERLIST | Internal use only. |
| SOS_LARGEPAGE_ALLOCATOR | Internal use only. |
| SOS_MINITHREAD | Internal use only. |
| SOS_NODE | Internal use only. |
| SOS_OBJECT_POOL | Internal use only. |
| SOS_OBJECT_STORE | Internal use only. |
| SOS_OOM_CHECK | Internal use only. |
| SOS_PHYS_PAGE_CACHE | Internal use only. |
| SOS_RESOURCE_CLERK_LIST | Internal use only. |
| SOS_RINGBUFFER_RECORD | Internal use only. |
| SOS_RW | Internal use only. |
| SOS_SATELLITE_USER_POOL | Internal use only. |
| SOS_SCHEDULER | Internal use only. |
| SOS_SELIST_SIZED_SLOCK | Internal use only. |
| SOS_SUSPEND_QUEUE | Internal use only. |
| SOS_SYSTHREAD | Internal use only. |
| SOS_SYSTHREAD_DISPATCHER | Internal use only. |
| SOS_TASK | Internal use only. |
| SOS_TLIST | Internal use only. |
| SOS_VM_LOW | Internal use only. |
| SOS_WAIT_STATS | Internal use only. |
| SOS_WAITABLE_ADDRESS_HASHBUCKET | Internal use only. |
| SPIN_EVENT_MUTEX | Internal use only. |
| SPL_DISPATCHER_LIST | Internal use only. |
| SPL_DISPATCHER_QUEUE | Internal use only. |
| SPL_NONYIELD_ANALYSIS | Internal use only. |
| SPL_QUERY_STORE_CTX_INITIALIZED | Internal use only. |
| SPL_QUERY_STORE_EXEC_STATS_AGG | Internal use only. |
| SPL_QUERY_STORE_EXEC_STATS_READ | Internal use only. |
| SPL_QUERY_STORE_STATS_COOKIE_CACHE | Internal use only. |
| SPL_SOS_DISPATCHER | Internal use only. |
| SPL_TDS_PKT_QUEUE | Internal use only. |
| SPL_XE_BUFFER_MGR | Internal use only. |
| SPL_XE_DISPATCHER_QUEUE | Internal use only. |
| SPL_XE_NOTIFICATION_CALLBACK_LIST | Internal use only. |
| SPL_XE_SESSION_EVENT_MGR | Internal use only. |
| SPL_XE_SESSION_MGR | Internal use only. |
| SPL_XE_SESSION_TARGET_MGR | Internal use only. |
| SPT_PROFILE | Internal use only. |
| SQL_MGR | Internal use only. |
| SQL_NORM | Internal use only. |
| SQLTRACE_FILE_BUFFER | Internal use only. |
| SRVPROC | Internal use only. |
| STACK_HASHER | Internal use only. |
| SUBLATCH | Internal use only. |
| SUBPDESC | Internal use only. |
| SUBPDESC_LIST | Internal use only. |
| SVC_BROKER_CTRL | Internal use only. |
| SVC_BROKER_DEBUG_LIST | Internal use only. |
| SVC_BROKER_LIST | Internal use only. |
| SVC_BROKER_OBJECT | Internal use only. |
| SYNCPOINT_RESOURCE | Internal use only. |
| TaskElapsedExecutionMonitor | Internal use only. |
| TDS_TVP | Internal use only. |
| TESTTEAM | Internal use only. |
| TESTTEAMEXPONENTIAL | Internal use only. |
| TESTTEAMEXPONENTIALTASTAS | Internal use only. |
| TESTTEAMTASTAS | Internal use only. |
| TMP_SESS_KEY | Internal use only. |
| TSQL_DEBUG | Internal use only. |
| TXFRM_REPL | Internal use only. |
| VDI_OPERATION | Internal use only. |
| WINFAB_REPORT_FAULT | Internal use only. |
| WRITE_PAGE_RECORDER | Internal use only. |
| X_PACKET_LIST | Internal use only. |
| X_PIPE | Internal use only. |
| X_PIPE_DEMAND | Internal use only. |
| X_PORT | Internal use only. |
| XACT_LOCK_INFO | Internal use only. |
| XACT_LOCKINFO_TASK | Internal use only. |
| XACT_WORKSPACE | Internal use only. |
| XCB | Internal use only. |
| XCB_FREE_LIST | Internal use only. |
| XCB_HASH | Internal use only. |
| XCHNG_TRACE | Internal use only. |
| XDES | Internal use only. |
| XDES_HASH | Internal use only. |
| XDESMGR | Internal use only. |
| XDESTABLELIST | Internal use only. |
| XE_RATE_LIMITER_STRETCHDB | Internal use only. |
| XE_SESSION_STORAGE | Internal use only. |
| XID_ARRAY | Internal use only. |
| XIO_BLOCKLIST | Internal use only. |
| XIO_REQSTR | Internal use only. |
| XIO_SEQNUMBUMP | Internal use only. |
| XIOSTATS | Internal use only. |
| XTP_RT_DATA_LIST | Internal use only. |
| XTS_MGR | Internal use only. |
| XVB_CSN | Internal use only. |
| XVB_LIST | Internal use only. |

## See also

- [DBCC SQLPERF (Transact-SQL)](../../t-sql/database-console-commands/dbcc-sqlperf-transact-sql.md)
- [SQL Server Operating System Related Dynamic Management Views (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sql-server-operating-system-related-dynamic-management-views-transact-sql.md)
- [When is Spinlock a Significant Driver of CPU utilization in SQL Server?](https://techcommunity.microsoft.com/t5/SQL-Server-Support/When-is-Spinlock-a-Significant-Driver-of-CPU-utilization-in-SQL/ba-p/530142)
- [Diagnosing and Resolving Spinlock Contention on SQL Server](../diagnose-resolve-spinlock-contention.md)
