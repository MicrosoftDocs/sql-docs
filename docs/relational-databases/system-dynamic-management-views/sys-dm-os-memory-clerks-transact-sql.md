---
title: "sys.dm_os_memory_clerks (Transact-SQL)"
description: sys.dm_os_memory_clerks (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "02/18/2021"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_os_memory_clerks"
  - "sys.dm_os_memory_clerks"
  - "dm_os_memory_clerks_TSQL"
  - "sys.dm_os_memory_clerks_TSQL"
helpviewer_keywords:
  - "sys.dm_os_memory_clerks dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_os_memory_clerks (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns the set of all memory clerks that are currently active in the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
> [!NOTE]  
>  To call this from [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the name **sys.dm_pdw_nodes_os_memory_clerks**. [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]
 
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**memory_clerk_address**|**varbinary(8)**|Specifies the unique memory address of the memory clerk. This is the primary key column. Is not nullable.|  
|**type**|**nvarchar(60)**|Specifies the type of memory clerk. Every clerk has a specific type, such as CLR Clerks MEMORYCLERK_SQLCLR. Is not nullable.|  
|**name**|**nvarchar(256)**|Specifies the internally assigned name of this memory clerk. A component can have several memory clerks of a specific type. A component might choose to use specific names to identify memory clerks of the same type. Is not nullable.|  
|**memory_node_id**|**smallint**|Specifies the ID of the memory node. Not nullable.|  
|**single_pages_kb**|**bigint**|**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)]. For more information, see [Changes to Memory Management starting with SQL Server 2012 (11.x)](../memory-management-architecture-guide.md#changes-to-memory-management-starting-with-).|  
|**pages_kb**|**bigint**|**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later.<br /><br /> Specifies the amount of page memory allocated in kilobytes (KB) for this memory clerk. Is not nullable.|  
|**multi_pages_kb**|**bigint**|**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)]. For more information, see [Changes to Memory Management starting with SQL Server 2012 (11.x)](../memory-management-architecture-guide.md#changes-to-memory-management-starting-with-).<br /><br /> Amount of multipage memory allocated in KB. This is the amount of memory allocated by using the multiple page allocator of the memory nodes. This memory is allocated outside the buffer pool and takes advantage of the virtual allocator of the memory nodes. Is not nullable.|  
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
On [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] Basic, S0, and S1 service objectives, and for databases in elastic pools, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account or the [Azure Active Directory admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account is required. On all other [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] service objectives, the `VIEW DATABASE STATE` permission is required in the database.   
  
## Remarks

 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] memory manager consists of a three-layer hierarchy. At the bottom of the hierarchy are memory nodes. The middle level consists of memory clerks, memory caches, and memory pools. The top layer consists of memory objects. These objects are used to allocate memory in an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 Memory nodes provide the interface and the implementation for low-level allocators. Inside [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], only memory clerks have access to memory nodes. Memory clerks access memory node interfaces to allocate memory. Memory nodes also track the memory allocated by using the clerk for diagnostics. Every component that allocates a significant amount of memory must create its own memory clerk and allocate all its memory by using the clerk interfaces. Frequently, components create their corresponding clerks at the time [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is started.  

### CACHESTORE and USERSTORE

CACHESTORE and USERSTORE are memory clerks but function as actual caches. Typically, caches keep allocations until a cache removal policy releases those allocations. To avoid re-creating it, a cached allocation is retained in cache as long as possible and is ordinarily removed from the cache when it is too old to be useful, or when the memory space is needed for new information (for more information, see [clock hand sweep](sys-dm-os-memory-cache-clock-hands-transact-sql.md#remarks)). This is one of the two major controls for caches - lifetime control and visibility control.

Cache store and user store differ in the way they control lifetime of allocations. In the case of a cache store, the lifetime of entries is fully controlled by SQLOS's caching framework. With user store, entries lifetime is only partially controlled by a store. The implementation of each user store may be specific to the nature of memory allocations and therefore user stores participate in lifetime control of its entries. 

Visibility control manages visibility of an entry. An entry in a cache can exist but might not be visible. For example, if a cache entry is marked for single use only, the entry will not be visible after it is used. In addition, the cache entry might be marked as dirty; it will continue to live in the cache but won't be visible to any lookups. For both stores, entry visibility is controlled by the caching framework.

For more information, see [SQLOS Caching](/archive/blogs/slavao/sqlos-caching).

### OBJECTSTORE

Object store is a simple pool. It is used to cache homogeneous data. All entries in the pools are considered equal.  Object stores implement a maximum cap to control size relative to other caches.

For more information, see [SQLOS Caching](/archive/blogs/slavao/sqlos-caching).

## Types

The following table lists the memory clerk types:

|Type  |Description  |
|---------|---------|
|CACHESTORE_BROKERDSH     |     This cache store is used to store allocations by [Service Broker](../../database-engine/configure-windows/sql-server-service-broker.md) Dialog Security Header Cache    |
|CACHESTORE_BROKERKEK     |   This cache store is used to store allocations by [Service Broker](../../database-engine/configure-windows/sql-server-service-broker.md)  Key Exchange Key Cache    |
|CACHESTORE_BROKERREADONLY     |     This cache store is used to store allocations by [Service Broker](../../database-engine/configure-windows/sql-server-service-broker.md) Read Only Cache    |
|CACHESTORE_BROKERRSB     |  This cache store is used to store allocations by [Service Broker](../../database-engine/configure-windows/sql-server-service-broker.md) [Remote Service Binding](../../t-sql/statements/create-remote-service-binding-transact-sql.md) Cache.    |
|CACHESTORE_BROKERTBLACS     |     This cache store is used to store allocations by [Service Broker](../../database-engine/configure-windows/sql-server-service-broker.md) for security access structures.    |
|CACHESTORE_BROKERTO     |  This cache store is used to store allocations by [Service Broker](../../database-engine/configure-windows/sql-server-service-broker.md) [Transmission Object](../performance-monitor/sql-server-broker-to-statistics-object.md) Cache   |
|CACHESTORE_BROKERUSERCERTLOOKUP     |    This cache store is used to store allocations by [Service Broker](../../database-engine/configure-windows/sql-server-service-broker.md)  user certificates lookup cache  |
|CACHESTORE_COLUMNSTOREOBJECTPOOL     |     This cache store is used for allocations by [Columnstore Indexes](../indexes/columnstore-indexes-overview.md) for [segments](../system-catalog-views/sys-column-store-segments-transact-sql.md) and [dictionaries](../system-catalog-views/sys-column-store-dictionaries-transact-sql.md)   |
|CACHESTORE_CONVPRI     |  This cache store is used to store allocations by [Service Broker](../../database-engine/configure-windows/sql-server-service-broker.md) to keep track of   [Conversations priorities](../system-catalog-views/sys-conversation-priorities-transact-sql.md)     |
|CACHESTORE_EVENTS     |     This cache store is used to store allocations by [Service Broker](../../database-engine/configure-windows/sql-server-service-broker.md) [Event Notifications](../service-broker/event-notifications.md) |
|CACHESTORE_FULLTEXTSTOPLIST     |    This memory clerk is used for allocations by Full-Text engine for [stoplist](../search/configure-and-manage-stopwords-and-stoplists-for-full-text-search.md) functionality.       |
|CACHESTORE_NOTIF     |    This cache store is used for allocations by [Query Notification](../../connect/ado-net/sql/query-notifications-sql-server.md) functionality    |
|CACHESTORE_OBJCP     |    This cache store is used for caching objects with compiled plans (CP): stored procedures, functions, triggers. To illustrate, after a query plan for a stored procedure is created, its plan is stored in this cache.  |
|CACHESTORE_PHDR     |    This cache store is used for temporary memory caching during parsing for views, constraints, and defaults algebrizer trees during compilation of a query. Once query is parsed, the memory should be released. Some examples include: many statements in one batch - thousands of inserts or updates into one batch, a T-SQL batch that contains a large dynamically generated query, a large number of values in an IN clause.      |
|CACHESTORE_QDSRUNTIMESTATS     |   This cache store is used to cache [Query Store](../performance/monitoring-performance-by-using-the-query-store.md)  runtime statistics |
|CACHESTORE_SEARCHPROPERTYLIST     |     This cache store is used for allocations by Full-Text engine for [Property List](../search/search-document-properties-with-search-property-lists.md) Cache  |
|CACHESTORE_SEHOBTCOLUMNATTRIBUTE     |  This cache store is used by storage engine for caching Heap or B-Tree (HoBT) column metadata structures.      |
|CACHESTORE_SQLCP     |    This cache store is used for caching ad hoc queries, prepared statements, and server-side cursors in plan cache. Ad hoc queries are commonly language-event T-SQL statements submitted to the server without explicit parameterization. Prepared statements also use this cache store - they are submitted by the application using API calls like [SQLPrepare()](../../odbc/reference/syntax/sqlprepare-function.md)/ [SQLExecute](../../odbc/reference/syntax/sqlexecute-function.md) (ODBC) or [SqlCommand.Prepare](/dotnet/api/system.data.sqlclient.sqlcommand.prepare)/[SqlCommand.ExecuteNonQuery](/dotnet/api/system.data.sqlclient.sqlcommand.executenonquery) (ADO.NET) and will appear on the server as [sp_prepare](../system-stored-procedures/sp-prepare-transact-sql.md)/[sp_execute](../system-stored-procedures/sp-execute-transact-sql.md) or [sp_prepexec](../system-stored-procedures/sp-prepexec-transact-sql.md) system procedure executions. Also, server-side cursors would consume from this cache store ([sp_cursoropen](../system-stored-procedures/sp-cursoropen-transact-sql.md), [sp_cursorfetch](../system-stored-procedures/sp-cursorfetch-transact-sql.md), [sp_cursorclose](../system-stored-procedures/sp-cursorclose-transact-sql.md)).    |
|CACHESTORE_STACKFRAMES     |    This cache store is used for allocations of internal SQL OS structures related to stack frames.     |
|CACHESTORE_SYSTEMROWSET     |   This cache store is used for allocations of internal structures related to transaction logging and recovery.      |
|CACHESTORE_TEMPTABLES     |     This cache store is used for allocations related to [temporary tables and table variables caching](../databases/tempdb-database.md#performance-improvements-in-tempdb-for-sql-server) - part of plan cache.    |
|CACHESTORE_VIEWDEFINITIONS     |     This cache store is used for caching view definitions as part of query optimization.    |
|CACHESTORE_XML_SELECTIVE_DG     |     This cache store is used to cache XML structures for XML processing.    |
|CACHESTORE_XMLDBATTRIBUTE     |     This cache store is used to cache XML attribute structures for XML activity like [XQuery](../../xquery/xquery-language-reference-sql-server.md).    |
|CACHESTORE_XMLDBELEMENT     |   This cache store is used to cache XML element structures for XML activity like [XQuery](../../xquery/xquery-language-reference-sql-server.md).      |
|CACHESTORE_XMLDBTYPE     |    This cache store is used to cache XML structures for XML activity like XQuery.     |
|CACHESTORE_XPROC     |   This cache store is used for caching structures for [Extended Stored procedures (Xprocs)](../extended-stored-procedures-programming/database-engine-extended-stored-procedures-programming.md) in plan cache.     |
|MEMORYCLERK_BACKUP     |    This memory clerk is used for various allocations by [Backup](../../t-sql/statements/backup-transact-sql.md) functionality    |
|MEMORYCLERK_BHF     |      This memory clerk is used for allocations for binary large objects (BLOB) management during query execution (Blob Handle support)  |
|MEMORYCLERK_BITMAP     |     This memory clerk is used for allocations by SQL OS functionality for bitmap filtering    |
|MEMORYCLERK_CSILOBCOMPRESSION     |  This memory clerk is used for allocations by [Columnstore Index binary large objects (BLOB) Compression](../data-compression/data-compression.md#columnstore-and-columnstore-archive-compression)    |
|MEMORYCLERK_DRTLHEAP     |    This memory clerk is used for allocations by SQL OS functionality   <br /><br />**Applies to**: [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] and later   |
|MEMORYCLERK_EXPOOL     |    This memory clerk is used for allocations by SQL OS functionality   <br /><br />**Applies to**: [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] and later   |
|MEMORYCLERK_EXTERNAL_EXTRACTORS     |   This memory clerk is used for allocations by query execution engine for [batch mode](../query-processing-architecture-guide.md#batch-mode-execution) operations   <br /><br />**Applies to**: [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] and later   |
|MEMORYCLERK_FILETABLE     |      This memory clerk is used for various allocations by [FileTables](../blob/filetables-sql-server.md) functionality.   |
|MEMORYCLERK_FSAGENT     |      This memory clerk is used for various allocations by [FILESTREAM](../blob/filestream-sql-server.md) functionality.    |
|MEMORYCLERK_FSCHUNKER     |      This memory clerk is used for various allocations by [FILESTREAM](../blob/filestream-sql-server.md) functionality for creating filestream chunks.   |
|MEMORYCLERK_FULLTEXT     |     This memory clerk is used for allocations by Full-Text engine structures.   |
|MEMORYCLERK_FULLTEXT_SHMEM     |   This memory clerk is used for allocations by Full-Text engine structures related to Shared memory connectivity with the Full Text Daemon process.      |
|MEMORYCLERK_HADR     |     This memory clerk is used for memory allocations by Always On functionality     |
|MEMORYCLERK_HOST     |    This memory clerk is used for allocations by SQL OS functionality.     |
|MEMORYCLERK_LANGSVC     |     This memory clerk is used for allocations by SQL T-SQL statements and commands (parser, algebrizer, etc.)    |
|MEMORYCLERK_LWC     |   This memory clerk is used for allocations by Full-Text [Semantic Search](../search/semantic-search-sql-server.md) engine       |
|MEMORYCLERK_POLYBASE     |    This memory clerk keeps track of memory allocations for [PolyBase](../polybase/polybase-guide.md) functionality inside SQL Server.     |
|MEMORYCLERK_QSRANGEPREFETCH     |  This memory clerk is used for allocations during query execution for query scan range prefetch.      |
|MEMORYCLERK_QUERYDISKSTORE     |     This memory clerk is used by [Query Store](../performance/monitoring-performance-by-using-the-query-store.md) memory allocations inside SQL Server.    |
|MEMORYCLERK_QUERYDISKSTORE_HASHMAP     |   This memory clerk is used by [Query Store](../performance/monitoring-performance-by-using-the-query-store.md) memory allocations inside SQL Server.      |
|MEMORYCLERK_QUERYDISKSTORE_STATS     |  This memory clerk is used by [Query Store](../performance/monitoring-performance-by-using-the-query-store.md) memory allocations inside SQL Server.      |
|MEMORYCLERK_QUERYPROFILE     |  This memory clerk is used for during server startup to enable query profiling    <br /><br />**Applies to**: [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] and later   |
|MEMORYCLERK_RTLHEAP     |    This memory clerk is used for allocations by SQL OS functionality.  <br /><br />**Applies to**: [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] and later   |
|MEMORYCLERK_SECURITYAPI     |    This memory clerk is used for allocations by SQL OS functionality.  <br /><br />**Applies to**: [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] and later   |
|MEMORYCLERK_SERIALIZATION     |     Internal use only    |
|MEMORYCLERK_SLOG     |     This memory clerk is used for allocations by sLog (secondary in-memory log stream) in [Accelerated Database Recovery](../accelerated-database-recovery-concepts.md) <br /><br />**Applies to**: [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] and later   |
|MEMORYCLERK_SNI     |     This memory clerk allocates memory for the Server Network Interface (SNI) components. SNI manages connectivity and [TDS](/openspecs/windows_protocols/ms-tds/893fcc7e-8a39-4b3c-815a-773b7b982c50) packets for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  |
|MEMORYCLERK_SOSMEMMANAGER     |   This memory clerk allocates structures for  SQLOS (SOS) thread scheduling and memory and I/O management..     |
|MEMORYCLERK_SOSNODE     |     This memory clerk allocates structures for SQLOS (SOS) thread scheduling and memory and I/O management.    |
|MEMORYCLERK_SOSOS     |     This memory clerk allocates structures for  SQLOS (SOS) thread scheduling and memory and I/O management..    |
|MEMORYCLERK_SPATIAL     |    This memory clerk is used by [Spatial Data](../spatial/spatial-data-sql-server.md) components for memory allocations.     |
|MEMORYCLERK_SQLBUFFERPOOL     |    This memory clerk keeps track of commonly the largest memory consumer inside [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] - data and index pages. Buffer Pool or data cache keeps data and index pages loaded in memory to provide fast access to data. For more information, see [Buffer Management](../memory-management-architecture-guide.md#buffer-management).     |
|MEMORYCLERK_SQLCLR     |     This memory clerk is used for allocations by [SQLCLR ](../clr-integration/clr-integration-overview.md).     |
|MEMORYCLERK_SQLCLRASSEMBLY     |     This memory clerk is used for allocations for [SQLCLR ](../clr-integration/clr-integration-overview.md) assemblies.     |
|MEMORYCLERK_SQLCONNECTIONPOOL     |     This memory clerk caches information on the server that the client application may need the server to keep track of. One example is an application that creates prepare handles via  [sp_prepexecrpc](../system-stored-procedures/sp-prepexecrpc-transact-sql.md). The application should properly unprepare (close) those handles after execution.  |
|MEMORYCLERK_SQLEXTENSIBILITY     |    This memory clerk is used for allocations by the [Extensibility Framework](../../machine-learning/concepts/extensibility-framework.md) for running external Python or R scripts on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  <br /><br />**Applies to**: [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] and later   |
|MEMORYCLERK_SQLGENERAL     |   This memory clerk could be used by multiple consumers inside SQL engine. Examples include replication memory, internal debugging/diagnostics, some [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] startup functionality, some SQL parser functionality, building system indexes, initialize global memory objects, Create OLEDB connection inside the server and Linked Server queries, Server-side Profiler tracing, creating showplan data, some security functionality, compilation of computed columns, memory for Parallelism structures, memory for some XML functionality     |
|MEMORYCLERK_SQLHTTP     |    Deprecated     |
|MEMORYCLERK_SQLLOGPOOL     |   This memory clerk is used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Log Pool.  Log Pool is a cache  used to improve performance when reading the transaction log. Specifically it improves log cache utilization during multiple log reads, reduces disk I/O log reads and allows sharing of log scans. Primary consumers of log pool are Always On (Change Capture and Send), Redo Manager, Database Recovery - Analysis/Redo/Undo, Transaction Runtime Rollback, Replication/CDC, Backup/Restore.    |
|MEMORYCLERK_SQLOPTIMIZER     |     This memory clerk is used for memory allocations during different phases of compiling a query. Some uses include query optimization, index statistics manager, view definitions compilation, histogram generation.   |
|MEMORYCLERK_SQLQERESERVATIONS     |     This memory clerk is used for Memory Grant allocations, that is memory allocated to queries to perform sort and hash operations during query execution. For more information on Query Execution reservations (memory grants), see [this blog](https://techcommunity.microsoft.com/t5/sql-server-support/memory-grants-the-mysterious-sql-server-memory-consumer-with/ba-p/333994)     |
|MEMORYCLERK_SQLQUERYCOMPILE     |    This memory clerk is used by Query optimizer for allocating memory during query compiling.   |
|MEMORYCLERK_SQLQUERYEXEC     |    This memory clerk is used for allocations in the following areas: [Batch mode processing](../query-processing-architecture-guide.md#batch-mode-execution), [Parallel query](../query-processing-architecture-guide.md#parallel-query-processing) execution, query execution context, [spatial index tessellation](../spatial/spatial-indexes-overview.md#tessellation), sort and hash operations (sort tables, hash tables), some DVM processing, [update statistics](../statistics/update-statistics.md) execution    |
|MEMORYCLERK_SQLQUERYPLAN     |     This memory clerk is used for allocations by [Heap](../indexes/heaps-tables-without-clustered-indexes.md) page management, [DBCC CHECKTABLE](../../t-sql/database-console-commands/dbcc-checktable-transact-sql.md) allocations, and [sp_cursor* stored procedure](../system-stored-procedures/cursor-stored-procedures-transact-sql.md) allocations   |
|MEMORYCLERK_SQLSERVICEBROKER     |   This memory clerk is used by [SQL Server Service Broker](../../database-engine/configure-windows/sql-server-service-broker.md) memory allocations.       |
|MEMORYCLERK_SQLSERVICEBROKERTRANSPORT     |     This memory clerk is used by [SQL Server Service Broker](../../database-engine/configure-windows/sql-server-service-broker.md) transport memory allocations.    |
|MEMORYCLERK_SQLSLO_OPERATIONS     |      This memory clerk is used to gather performance statistics <br /><br />**Applies to**:  [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]   |
|MEMORYCLERK_SQLSOAP     |     Deprecated    |
|MEMORYCLERK_SQLSOAPSESSIONSTORE     |    Deprecated     |
|MEMORYCLERK_SQLSTORENG     |   This memory clerk is used for allocations by multiple storage engine components. Examples of components include structures for database files, database snapshot replica file manager, deadlock monitor, DBTABLE structures, Log manager structures, some tempdb versioning structures, some server startup functionality, execution context for child threads in parallel queries.      |
|MEMORYCLERK_SQLTRACE     |     This memory clerk is used for server-side [SQL Trace](../sql-trace/sql-trace.md) memory allocations.     |
|MEMORYCLERK_SQLUTILITIES     |    This memory clerk can be used by multiple allocators inside SQL Server. Examples include Backup and Restore, Log Shipping, Database Mirroring, DBCC commands, BCP code on the server side, some query parallelism work, Log Scan buffers.    |
|MEMORYCLERK_SQLXML     |     This memory clerk is used for memory allocations when performing XML operations.    |
|MEMORYCLERK_SQLXP     |     This memory clerk is used for memory allocations when calling [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [Extended Stored procedures](../extended-stored-procedures-reference/database-engine-extended-stored-procedures-reference.md).    |
|MEMORYCLERK_SVL     |    This memory clerk is used used for allocations of internal SQL OS structures |
|MEMORYCLERK_TEST     |    Internal use only   |
|MEMORYCLERK_UNITTEST     |      Internal use only  |
|MEMORYCLERK_WRITEPAGERECORDER     |    This memory clerk is used for allocations by Write Page Recorder.   |
|MEMORYCLERK_XE     |    This memory clerk is used for [Extended Events](../extended-events/extended-events.md) memory allocations      |
|MEMORYCLERK_XE_BUFFER     |      This memory clerk is used for [Extended Events](../extended-events/extended-events.md) memory allocations   |
|MEMORYCLERK_XLOG_SERVER     |   This memory clerk is used for allocations by Xlog used for log file management in SQL Azure Database   <br /><br />**Applies to**:  [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] |
|MEMORYCLERK_XTP     |    This memory clerk is used for [In-Memory OLTP](../in-memory-oltp/overview-and-usage-scenarios.md) memory allocations.     |
|OBJECTSTORE_LBSS     |    This object store is used to allocate temporary LOBs - variables, parameters, and intermediate results for expressions. An example that uses this store is [table-valued parameters](../../connect/ado-net/sql/table-valued-parameters.md) (TVP) . See the [KB article 4468102](https://support.microsoft.com/topic/kb4468102-fix-excessive-memory-usage-when-you-trace-rpc-events-that-involve-table-valued-parameters-in-sql-server-2016-and-2017-c68aa214-26f1-98de-6b4d-c7dcad82dbd4) and  [KB article 4051359](https://support.microsoft.com/topic/kb4051359-fix-sql-server-runs-out-of-memory-when-table-valued-parameters-are-captured-in-extended-events-sessions-in-sql-server-2016-even-if-collecting-statement-or-data-stream-isn-t-enabled-a3639efa-0618-82a8-f6b1-8cdcba29ce6d) for more information on fixes in this space.     |
|OBJECTSTORE_LOCK_MANAGER     |      This memory clerk keeps track of allocations made by the [Lock Manager](../sql-server-transaction-locking-and-row-versioning-guide.md#Lock_Engine) in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].   |
|OBJECTSTORE_SECAUDIT_EVENT_BUFFER     |   This object store is used for [SQL Server Audit](../security/auditing/sql-server-audit-database-engine.md) memory allocations.        |
|OBJECTSTORE_SERVICE_BROKER     |     This object store is used by [Service Broker](../../database-engine/configure-windows/sql-server-service-broker.md)    |
|OBJECTSTORE_SNI_PACKET     |     This object store is used by Server Network Interface (SNI) components which manage connectivity    |
|OBJECTSTORE_XACT_CACHE     |    This object store is used to cache transactions information     |
|USERSTORE_DBMETADATA     |      This object store is used for metadata structures     |
|USERSTORE_OBJPERM     |     This store is used for structures keeping track of object security/permission     |
|USERSTORE_QDSSTMT     |  This cache store is used to cache [Query Store](../performance/monitoring-performance-by-using-the-query-store.md)  statements       |
|USERSTORE_SCHEMAMGR     |    Schema manager cache stores different types of metadata information about the database objects in memory (e.g tables). A common user of this store could be the tempdb database with objects like tables, temp procedures, table variables, table-valued parameters, worktables, workfiles, version store.  |
|USERSTORE_SXC     |    This user store is used for allocations to store all [RPC](/openspecs/windows_protocols/ms-tds/619c43b6-9495-4a58-9e49-a4950db245b3) parameters.     |
|USERSTORE_TOKENPERM     |    TokenAndPermUserStore is a single SOS user store that keeps track of security entries for security context, login, user, permission, and audit. Multiple hash tables are allocated to store these objects.    |

[!INCLUDE [sql-b-tree](../../includes/sql-b-tree.md)]

## See Also  

 [SQL Server Operating System Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-server-operating-system-related-dynamic-management-views-transact-sql.md)   
 [sys.dm_os_sys_info &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-sys-info-transact-sql.md)   
 [sys.dm_exec_query_memory_grants &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-memory-grants-transact-sql.md)   
 [sys.dm_exec_requests &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md)   
 [sys.dm_exec_query_plan &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-plan-transact-sql.md)   
 [sys.dm_exec_sql_text &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-sql-text-transact-sql.md)
