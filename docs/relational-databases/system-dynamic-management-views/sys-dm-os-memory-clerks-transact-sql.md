---
description: "sys.dm_os_memory_clerks (Transact-SQL)"
title: "sys.dm_os_memory_clerks (Transact-SQL)"
ms.custom: ""
ms.date: "02/18/2021"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "reference"
f1_keywords: 
  - "dm_os_memory_clerks"
  - "sys.dm_os_memory_clerks"
  - "dm_os_memory_clerks_TSQL"
  - "sys.dm_os_memory_clerks_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_os_memory_clerks dynamic management view"
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_os_memory_clerks (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns the set of all memory clerks that are currently active in the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
> [!NOTE]  
>  To call this from [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the name **sys.dm_pdw_nodes_os_memory_clerks**.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**memory_clerk_address**|**varbinary(8)**|Specifies the unique memory address of the memory clerk. This is the primary key column. Is not nullable.|  
|**type**|**nvarchar(60)**|Specifies the type of memory clerk. Every clerk has a specific type, such as CLR Clerks MEMORYCLERK_SQLCLR. Is not nullable.|  
|**name**|**nvarchar(256)**|Specifies the internally assigned name of this memory clerk. A component can have several memory clerks of a specific type. A component might choose to use specific names to identify memory clerks of the same type. Is not nullable.|  
|**memory_node_id**|**smallint**|Specifies the ID of the memory node. Not nullable.|  
|**single_pages_kb**|**bigint**|**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)].|  
|**pages_kb**|**bigint**|**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later.<br /><br /> Specifies the amount of page memory allocated in kilobytes (KB) for this memory clerk. Is not nullable.|  
|**multi_pages_kb**|**bigint**|**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)].<br /><br /> Amount of multipage memory allocated in KB. This is the amount of memory allocated by using the multiple page allocator of the memory nodes. This memory is allocated outside the buffer pool and takes advantage of the virtual allocator of the memory nodes. Is not nullable.|  
|**virtual_memory_reserved_kb**|**bigint**|Specifies the amount of virtual memory that is reserved by a memory clerk. Is not nullable.|  
|**virtual_memory_committed_kb**|**bigint**|Specifies the amount of virtual memory that is committed by a memory clerk. The amount of committed memory should always be less than the amount of reserved memory. Is not nullable.|  
|**awe_allocated_kb**|**bigint**|Specifies the amount of memory in kilobytes (KB) locked in the physical memory and not paged out by the operating system. Is not nullable.|  
|**shared_memory_reserved_kb**|**bigint**|Specifies the amount of shared memory that is reserved by a memory clerk. The amount of memory reserved for use by shared memory and file mapping. Is not nullable.|  
|**shared_memory_committed_kb**|**bigint**|Specifies the amount of shared memory that is committed by the memory clerk. Is not nullable.|  
|**page_size_in_bytes**|**bigint**|Specifies the granularity of the page allocation for this memory clerk. Is not nullable.|  
|**page_allocator_address**|**varbinary(8)**|Specifies the address of the page allocator. This address is unique for a memory clerk and can be used in **sys.dm_os_memory_objects** to locate memory objects that are bound to this clerk. Is not nullable.|  
|**host_address**|**varbinary(8)**|Specifies the memory address of the host for this memory clerk. For more information, see [sys.dm_os_hosts &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-hosts-transact-sql.md). Components, such as [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client, access [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] memory resources through the host interface.<br /><br /> 0x00000000 = Memory clerk belongs to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br /> Is not nullable.|  
|**pdw_node_id**|**int**|**Applies to**: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]<br /><br /> The identifier for the node that this distribution is on.|  

## Permissions

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)], requires `VIEW SERVER STATE` permission.
On SQL Database Basic, S0, and S1 service objectives, and for databases in elastic pools, the [server admin](https://docs.microsoft.com/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account or the [Azure Active Directory admin](https://docs.microsoft.com/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account is required. On all other SQL Database service objectives, the `VIEW DATABASE STATE` permission is required in the database.
  
## Remarks

 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] memory manager consists of a three-layer hierarchy. At the bottom of the hierarchy are memory nodes. The middle level consists of memory clerks, memory caches, and memory pools. The top layer consists of memory objects. These objects are generally used to allocate memory in an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 Memory nodes provide the interface and the implementation for low-level allocators. Inside [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], only memory clerks have access to memory nodes. Memory clerks access memory node interfaces to allocate memory. Memory nodes also track the memory allocated by using the clerk for diagnostics. Every component that allocates a significant amount of memory must create its own memory clerk and allocate all its memory by using the clerk interfaces. Frequently, components create their corresponding clerks at the time [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is started.  

### CACHESTORE and USERSTORE

CACHESTORE and USERSTORE are memory clerks but function as actual caches. Typically, caches keep allocations until a cache removal policy releases those allocations. To avoid re-creating a cached allocation, it is retained in cache as long as possible and is ordinarily removed from the cache when it is too old to be useful, or when the memory space is needed for new information (for more information see [clock hand sweep](sys-dm-os-memory-cache-clock-hands-transact-sql.md#remarks)). This is one of the two major controls for caches - lifetime control.

Cachestore and Userstore differ in the way they control lifetime of allocations. In the case of a Cache Store, the lifetime of entries is fully controlled by SQLOS's caching framework. In the case of a User Store, entries lifetime is only partially controlled by a store. The implementation of each userstore may be specific to the nature of memory allocations and therefore user stores participate in lifetime control of its entries. 

The second one is visibility control which manages visibility of an entry. An entry in a cache can exists but might not be visible. For example, if a cache entry is marked for single use only, the entry will not be visible after it is used. In addition, the cache entry might be marked as dirty; it will continue to live in the cache but won't be visible to any lookups. For both stores, entry visibility is controlled by the caching framework.

For more information, see [SQLOS Caching](https://docs.microsoft.com/archive/blogs/slavao/sqlos-caching).

### OBJECTSTORE

Object store is a simple pool. It is used to cache homogeneous type of data. All entries in the pools are considered equal.  Object stores implement a maximum cap to control size relative to other caches.

For more information, see [SQLOS Caching](https://docs.microsoft.com/archive/blogs/slavao/sqlos-caching).

## Types

The following table lists the Memory Clerk types:

|Type  |Description  |
|---------|---------|
|CACHESTORE_BROKERDSH     |         |
|CACHESTORE_BROKERKEK     |         |
|CACHESTORE_BROKERREADONLY     |         |
|CACHESTORE_BROKERRSB     |         |
|CACHESTORE_BROKERTBLACS     |         |
|CACHESTORE_BROKERTO     |         |
|CACHESTORE_BROKERUSERCERTLOOKUP     |         |
|CACHESTORE_COLUMNSTOREOBJECTPOOL     |         |
|CACHESTORE_CONVPRI     |         |
|CACHESTORE_EVENTS     |         |
|CACHESTORE_FULLTEXTSTOPLIST     |         |
|CACHESTORE_NOTIF     |         |
|CACHESTORE_OBJCP     |    This cachestore is used for caching objects with compiled plans (CP): stored procedures, functions, triggers. To illustrate, after a query plan for a stored procedure is created, its plan is stored in this cache.  |
|CACHESTORE_PHDR     |    CACHESTORE_PHDR is used for temporary memory caching during parsing for views, constraints and defaults algebrizer trees during compilation of a query. Once query is parsed, the memory should be released. Some examples include: many statements in one batch - a user can batch thousands of statement (insert or updates) into one batch, a T-SQL batch that contains a very large dynamically-generated query, very large number of values in IN clause.      |
|CACHESTORE_QDSRUNTIMESTATS     |         |
|CACHESTORE_SEARCHPROPERTYLIST     |         |
|CACHESTORE_SEHOBTCOLUMNATTRIBUTE     |         |
|CACHESTORE_SQLCP     |    CACHESTORE_SQLCP cachestore is used for Adhoc queries, prepared statements, and server side cursors. Ad-hoc queries are commonly language-event T-SQL statements submitted to the server without explicit parameterization. Prepared statements also use this cachestore - they are submitted by the application using API calls like [SQLPrepare()](../../odbc/reference/syntax/sqlprepare-function.md)/ [SQLExecute](../../odbc/reference/syntax/sqlexecute-function.md) (ODBC) or [SqlCommand.Prepare](https://docs.microsoft.com/dotnet/api/system.data.sqlclient.sqlcommand.prepare)/[SqlCommand.ExecuteNonQuery](https://docs.microsoft.com/dotnet/api/system.data.sqlclient.sqlcommand.executenonquery) (ADO.NET) and will appear on the server as [sp_prepare](../system-stored-procedures/sp-prepare-transact-sql.md)/[sp_execute](../system-stored-procedures/sp-execute-transact-sql.md) or [sp_prepexec](../system-stored-procedures/sp-prepexec-transact-sql.md) system procedure executions. Also, server-side cursors would consume from this cachestore ([sp_cursoropen](../system-stored-procedures/sp-cursoropen-transact-sql.md), [sp_cursorfetch](../system-stored-procedures/sp-cursorfetch-transact-sql.md), [sp_cursorclose](../system-stored-procedures/sp-cursorclose-transact-sql.md)).    |
|CACHESTORE_STACKFRAMES     |         |
|CACHESTORE_SYSTEMROWSET     |         |
|CACHESTORE_TEMPTABLES     |         |
|CACHESTORE_VIEWDEFINITIONS     |         |
|CACHESTORE_XML_SELECTIVE_DG     |         |
|CACHESTORE_XMLDBATTRIBUTE     |         |
|CACHESTORE_XMLDBELEMENT     |         |
|CACHESTORE_XMLDBTYPE     |         |
|CACHESTORE_XPROC     |         |
|MEMORYCLERK_BACKUP     |         |
|MEMORYCLERK_BHF     |         |
|MEMORYCLERK_BITMAP     |         |
|MEMORYCLERK_CSILOBCOMPRESSION     |         |
|MEMORYCLERK_DRTLHEAP     |       <br /><br />**Applies to**: [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] and later   |
|MEMORYCLERK_EXPOOL     |       <br /><br />**Applies to**: [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] and later   |
|MEMORYCLERK_EXTERNAL_EXTRACTORS     |       <br /><br />**Applies to**: [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] and later   |
|MEMORYCLERK_FILETABLE     |         |
|MEMORYCLERK_FSAGENT     |         |
|MEMORYCLERK_FSCHUNKER     |         |
|MEMORYCLERK_FULLTEXT     |     This memory clerk is used for allocations by Full-Text engine structures.   |
|MEMORYCLERK_FULLTEXT_SHMEM     |         |
|MEMORYCLERK_HADR     |     This memory clerk is used for memory allocations by AlwaysOn functionality     |
|MEMORYCLERK_HOST     |         |
|MEMORYCLERK_LANGSVC     |         |
|MEMORYCLERK_LWC     |         |
|MEMORYCLERK_POLYBASE     |    This memory clerk keeps track of memory allocations for Polybase-related functionalit inside SQL Server.     |
|MEMORYCLERK_QSRANGEPREFETCH     |         |
|MEMORYCLERK_QUERYDISKSTORE     |     Used by [Query Store](../performance/monitoring-performance-by-using-the-query-store.md) memory allocations inside SQL Server.    |
|MEMORYCLERK_QUERYDISKSTORE_HASHMAP     |   Used by [Query Store](../performance/monitoring-performance-by-using-the-query-store.md) memory allocations inside SQL Server.      |
|MEMORYCLERK_QUERYDISKSTORE_STATS     |   Used by [Query Store](../performance/monitoring-performance-by-using-the-query-store.md) memory allocations inside SQL Server.      |
|MEMORYCLERK_QUERYPROFILE     |      <br /><br />**Applies to**: [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] and later   |
|MEMORYCLERK_RTLHEAP     |      <br /><br />**Applies to**: [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] and later   |
|MEMORYCLERK_SECURITYAPI     |      <br /><br />**Applies to**: [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] and later   |
|MEMORYCLERK_SERIALIZATION     |         |
|MEMORYCLERK_SLOG     |      <br /><br />**Applies to**: [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] and later   |
|MEMORYCLERK_SNI     |     This memory clerk allocates memory for the SQL Server Network Interface (SNI) componets which manage connectivity and [TDS](https://docs.microsoft.com/openspecs/windows_protocols/ms-tds/893fcc7e-8a39-4b3c-815a-773b7b982c50) packets for SQL Server  |
|MEMORYCLERK_SOSMEMMANAGER     |   This memory clerk is used by SQLOS (SOS) scheduling, memory and I/O management for allocating memory structures.     |
|MEMORYCLERK_SOSNODE     |     This memory clerk is used by SQLOS (SOS) scheduling, memory and I/O management for allocating memory structures.    |
|MEMORYCLERK_SOSOS     |     This memory clerk is used by SQLOS (SOS) scheduling, memory and I/O management for allocating memory structures.    |
|MEMORYCLERK_SPATIAL     |    This memory clerk is used by [Spatial Data](../spatial/spatial-data-sql-server.md) components for memory allocations.     |
|MEMORYCLERK_SQLBUFFERPOOL     |    This memory clerk keeps track of the commonly the largest memory consumer inside SQL Server - data and index pages. Buffer Pool or data cache keeps data and index pages loaded in memory to provide fast access to data. See [Buffer Management](../memory-management-architecture-guide.md#buffer-management) for more information.     |
|MEMORYCLERK_SQLCLR     |         |
|MEMORYCLERK_SQLCLRASSEMBLY     |         |
|MEMORYCLERK_SQLCONNECTIONPOOL     |     This memory clerk caches information on the server that the client application may need the server to keep track of. One example is an applications that creates prepare handles via  [sp_prepexecrpc](../system-stored-procedures/sp-prepexecrpc-transact-sql.md). The application should properly unprepare (close) those handles after execution.  |
|MEMORYCLERK_SQLEXTENSIBILITY     |      <br /><br />**Applies to**: [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] and later   |
|MEMORYCLERK_SQLGENERAL     |   This memory clerk could be used by multiple consumers inside SQL engine. Examples include replication memory, internal debugging/diagnostics, some SQL Server startup functionality, some SQL parser functionality, building system indexes, initialize global memory objects, Create OLEDB connection inside the server and Linked Server queries, Server side Profiler tracing, creating showplan data, some security functionality, compilation of computed columns, memory for Parallelism structures, memory for some XML functionality     |
|MEMORYCLERK_SQLHTTP     |    Deprecated     |
|MEMORYCLERK_SQLLOGPOOL     |     Log Pool is a cache memory used to improve log cache utilization during multiple log reads, reduce disk I/O log reads and  share log scans. Primary consumers of log pool are AlwaysOn (Change Capture and Send), Redo Manager , Database Recovery - Analysis/Redo/Undo, Transaction Runtime Rollback, Replication/CDC , Backup/Restore.    |
|MEMORYCLERK_SQLOPTIMIZER     |     This memory clerk is used by query optimizer memory allocations during different phases of compiling a query. Some uses include query optimization, index statistics manager, view definitions compilation, histogram generation.   |
|MEMORYCLERK_SQLQERESERVATIONS     |     This memory clerk is used for Memory Grant allocations, that is memory allocated to queries to perform sort and hash operations during query execution. For more information on Query Exectuion reservations (memory grants), see [this blog](https://techcommunity.microsoft.com/t5/sql-server-support/memory-grants-the-mysterious-sql-server-memory-consumer-with/ba-p/333994)     |
|MEMORYCLERK_SQLQUERYCOMPILE     |    This memory clerk is used by Query optimizer for allocating memory during query compiling.   |
|MEMORYCLERK_SQLQUERYEXEC     |         |
|MEMORYCLERK_SQLQUERYPLAN     |         |
|MEMORYCLERK_SQLSERVICEBROKER     |   This memory clerk is used by [SQL Server Service Broker](../../database-engine/configure-windows/sql-server-service-broker.md) memory allocations.       |
|MEMORYCLERK_SQLSERVICEBROKERTRANSPORT     |     This memory clerk is used by [SQL Server Service Broker](../../database-engine/configure-windows/sql-server-service-broker.md) transport memory allocations.    |
|MEMORYCLERK_SQLSLO_OPERATIONS     |         |
|MEMORYCLERK_SQLSOAP     |     Deprecated    |
|MEMORYCLERK_SQLSOAPSESSIONSTORE     |    Deprecated     |
|MEMORYCLERK_SQLSTORENG     |   This memory clerks is used for allocation from multiple storage engine components. Examples include structures for database files, database snapshot replica file manager, deadlock monitor, DBTABLE structures, Logmanager structures, some tempdb versioning structures, some server startup functionality, execution context for child threads in parallel queries, and so on.      |
|MEMORYCLERK_SQLTRACE     |     This memory clerks is used for server-side [SQL Trace](../sql-trace/sql-trace.md) memory allocations.     |
|MEMORYCLERK_SQLUTILITIES     |    This memory clerk can be used by multiple allocators inside SQL Server. Examples include Backup and Restore, Log Shipping, Database Mirroring, DBCC commands, BCP code on the server side, some query parallelism work, Log Scan buffers    |
|MEMORYCLERK_SQLXML     |     This memory clerk is used for memory allocations when performing XML operations.    |
|MEMORYCLERK_SQLXP     |     This memory clerk is used for memory allocations when calling SQL Server [Extended Stored procedures](../extended-stored-procedures-reference/database-engine-extended-stored-procedures-reference.md).    |
|MEMORYCLERK_SVL     |    <br /><br />**Applies to**: [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] and later     |
|MEMORYCLERK_TEST     |       <br /><br />**Applies to**: [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] and later  |
|MEMORYCLERK_UNITTEST     |      <br /><br />**Applies to**: [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] and later   |
|MEMORYCLERK_WRITEPAGERECORDER     |       <br /><br />**Applies to**: [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] and later  |
|MEMORYCLERK_XE     |    This memory clerk is used for [Extended Events](../extended-events/extended-events.md) memory allocations      |
|MEMORYCLERK_XE_BUFFER     |      This memory clerk is used for [Extended Events](../extended-events/extended-events.md) memory allocations   |
|MEMORYCLERK_XLOG_SERVER     |      <br /><br />**Applies to**: [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] and later |
|MEMORYCLERK_XTP     |    This memory clerk is used for [In-Memory OLTP](../in-memory-oltp/in-memory-oltp-in-memory-optimization.md) memory allocations.     |
|OBJECTSTORE_LBSS     |    OBJECTSTORE_LBSS is used to store temporary LOBs - variables, parameters, intermediate results of expressions. An example that uses this store is [table-valued parameters](../../connect/ado-net/sql/table-valued-parameters.md) (TVP) . See the [KB article 4468102](https://support.microsoft.com/topic/kb4468102-fix-excessive-memory-usage-when-you-trace-rpc-events-that-involve-table-valued-parameters-in-sql-server-2016-and-2017-c68aa214-26f1-98de-6b4d-c7dcad82dbd4) and  [KB article 4051359](https://support.microsoft.com/topic/kb4051359-fix-sql-server-runs-out-of-memory-when-table-valued-parameters-are-captured-in-extended-events-sessions-in-sql-server-2016-even-if-collecting-statement-or-data-stream-isn-t-enabled-a3639efa-0618-82a8-f6b1-8cdcba29ce6d) for more information on fixes in this space.     |
|OBJECTSTORE_LOCK_MANAGER     |      This memory clerk keeps track of allocations made by the [Lock Manager](../sql-server-transaction-locking-and-row-versioning-guide.md#Lock_Engine) in SQL Server.   |
|OBJECTSTORE_SECAUDIT_EVENT_BUFFER     |   This object store memory clerk is used for [SQL Server Audit](../security/auditing/sql-server-audit-database-engine.md) memory allocations.        |
|OBJECTSTORE_SERVICE_BROKER     |         |
|OBJECTSTORE_SNI_PACKET     |         |
|OBJECTSTORE_XACT_CACHE     |         |
|USERSTORE_DBMETADATA     |         |
|USERSTORE_OBJPERM     |         |
|USERSTORE_QDSSTMT     |         |
|USERSTORE_SCHEMAMGR     |    Schema Manager cache stores different types of metadata information about the database objects in memory (e.g tables). A common user of this store could be the tempdb database with objects like tables, temp procedures, table variables, table valued parameters, worktables, workfiles, version store.  |
|USERSTORE_SXC     |    This user store is used for allocations to store all [RPC](https://docs.microsoft.com/openspecs/windows_protocols/ms-tds/619c43b6-9495-4a58-9e49-a4950db245b3) parameters.     |
|USERSTORE_TOKENPERM     |    TokenAndPermUserStore is a single SOS UserStore that keeps track of security entries for security context, login, user, permission, and audit. Multiple hash tables are allocated to store these objects.    |

## See Also  

 [SQL Server Operating System Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-server-operating-system-related-dynamic-management-views-transact-sql.md)   
 [sys.dm_os_sys_info &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-sys-info-transact-sql.md)   
 [sys.dm_exec_query_memory_grants &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-memory-grants-transact-sql.md)   
 [sys.dm_exec_requests &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md)   
 [sys.dm_exec_query_plan &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-plan-transact-sql.md)   
 [sys.dm_exec_sql_text &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-sql-text-transact-sql.md)  
  
  


