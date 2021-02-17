---
description: "sys.dm_os_memory_clerks (Transact-SQL)"
title: "sys.dm_os_memory_clerks (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/13/2017"
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
ms.assetid: 1d556c67-5c12-46d5-aa8c-7ec1bb858df7
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

## Types

### OBJECTSTORE

### CACHESTORE



The following table lists the Memory Clerk types.




|Type  |Description  |
|---------|---------|
|CACHESTORE_BROKERDSH     |     To be documented in the future   |
|CACHESTORE_BROKERKEK     |     To be documented in the future   |
|CACHESTORE_BROKERREADONLY     |   To be documented in the future     |
|CACHESTORE_BROKERRSB     |   To be documented in the future     |
|CACHESTORE_BROKERTBLACS     |     To be documented in the future   |
|CACHESTORE_BROKERTO     |     To be documented in the future   |
|CACHESTORE_BROKERUSERCERTLOOKUP     |     To be documented in the future   |
|CACHESTORE_COLUMNSTOREOBJECTPOOL     |     To be documented in the future   |
|CACHESTORE_CONVPRI     |     To be documented in the future   |
|CACHESTORE_EVENTS     |     To be documented in the future   |
|CACHESTORE_FULLTEXTSTOPLIST     |    To be documented in the future    |
|CACHESTORE_NOTIF     |    To be documented in the future    |
|CACHESTORE_OBJCP     |    To be documented in the future    |
|CACHESTORE_PHDR     |    To be documented in the future    |
|CACHESTORE_QDSRUNTIMESTATS     |     To be documented in the future   |
|CACHESTORE_SEARCHPROPERTYLIST     |     To be documented in the future   |
|CACHESTORE_SEHOBTCOLUMNATTRIBUTE     |    To be documented in the future    |
|CACHESTORE_SQLCP     |    To be documented in the future    |
|CACHESTORE_STACKFRAMES     |     To be documented in the future   |
|CACHESTORE_SYSTEMROWSET     |    To be documented in the future    |
|CACHESTORE_TEMPTABLES     |     To be documented in the future   |
|CACHESTORE_VIEWDEFINITIONS     |     To be documented in the future   |
|CACHESTORE_XML_SELECTIVE_DG     |     To be documented in the future   |
|CACHESTORE_XMLDBATTRIBUTE     |     To be documented in the future   |
|CACHESTORE_XMLDBELEMENT     |     To be documented in the future   |
|CACHESTORE_XMLDBTYPE     |     To be documented in the future   |
|CACHESTORE_XPROC     |     To be documented in the future   |
|MEMORYCLERK_BACKUP     |     To be documented in the future   |
|MEMORYCLERK_BHF     |     To be documented in the future   |
|MEMORYCLERK_BITMAP     |      To be documented in the future  |
|MEMORYCLERK_CSILOBCOMPRESSION     |      To be documented in the future  |
|MEMORYCLERK_DRTLHEAP     |     To be documented in the future <br /><br />**Applies to**: [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] and later   |
|MEMORYCLERK_EXPOOL     |     To be documented in the future <br /><br />**Applies to**: [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] and later   |
|MEMORYCLERK_EXTERNAL_EXTRACTORS     |     To be documented in the future <br /><br />**Applies to**: [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] and later   |
|MEMORYCLERK_FILETABLE     |   To be documented in the future     |
|MEMORYCLERK_FSAGENT     |    To be documented in the future    |
|MEMORYCLERK_FSCHUNKER     |     To be documented in the future   |
|MEMORYCLERK_FULLTEXT     |     To be documented in the future   |
|MEMORYCLERK_FULLTEXT_SHMEM     |     To be documented in the future   |
|MEMORYCLERK_HADR     |     To be documented in the future   |
|MEMORYCLERK_HOST     |     To be documented in the future   |
|MEMORYCLERK_LANGSVC     |     To be documented in the future   |
|MEMORYCLERK_LWC     |     To be documented in the future   |
|MEMORYCLERK_POLYBASE     |     To be documented in the future   |
|MEMORYCLERK_QSRANGEPREFETCH     |     To be documented in the future   |
|MEMORYCLERK_QUERYDISKSTORE     |     To be documented in the future   |
|MEMORYCLERK_QUERYDISKSTORE_HASHMAP     |     To be documented in the future   |
|MEMORYCLERK_QUERYDISKSTORE_STATS     |     To be documented in the future   |
|MEMORYCLERK_QUERYPROFILE     |    To be documented in the future <br /><br />**Applies to**: [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] and later   |
|MEMORYCLERK_RTLHEAP     |    To be documented in the future <br /><br />**Applies to**: [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] and later   |
|MEMORYCLERK_SECURITYAPI     |    To be documented in the future <br /><br />**Applies to**: [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] and later   |
|MEMORYCLERK_SERIALIZATION     |     To be documented in the future   |
|MEMORYCLERK_SLOG     |    To be documented in the future <br /><br />**Applies to**: [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] and later   |
|MEMORYCLERK_SNI     |     To be documented in the future   |
|MEMORYCLERK_SOSMEMMANAGER     |     To be documented in the future   |
|MEMORYCLERK_SOSNODE     |    To be documented in the future    |
|MEMORYCLERK_SOSOS     |     To be documented in the future   |
|MEMORYCLERK_SPATIAL     |     To be documented in the future   |
|MEMORYCLERK_SQLBUFFERPOOL     |    To be documented in the future    |
|MEMORYCLERK_SQLCLR     |    To be documented in the future    |
|MEMORYCLERK_SQLCLRASSEMBLY     |     To be documented in the future   |
|MEMORYCLERK_SQLCONNECTIONPOOL     |     To be documented in the future   |
|MEMORYCLERK_SQLEXTENSIBILITY     |    To be documented in the future <br /><br />**Applies to**: [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] and later   |
|MEMORYCLERK_SQLGENERAL     |   To be documented in the future     |
|MEMORYCLERK_SQLHTTP     |   To be documented in the future     |
|MEMORYCLERK_SQLLOGPOOL     |     To be documented in the future   |
|MEMORYCLERK_SQLOPTIMIZER     |     To be documented in the future   |
|MEMORYCLERK_SQLQERESERVATIONS     |     To be documented in the future   |
|MEMORYCLERK_SQLQUERYCOMPILE     |    To be documented in the future    |
|MEMORYCLERK_SQLQUERYEXEC     |    To be documented in the future    |
|MEMORYCLERK_SQLQUERYPLAN     |    To be documented in the future    |
|MEMORYCLERK_SQLSERVICEBROKER     |     To be documented in the future   |
|MEMORYCLERK_SQLSERVICEBROKERTRANSPORT     |     To be documented in the future   |
|MEMORYCLERK_SQLSLO_OPERATIONS     |     To be documented in the future   |
|MEMORYCLERK_SQLSOAP     |     To be documented in the future   |
|MEMORYCLERK_SQLSOAPSESSIONSTORE     |     To be documented in the future   |
|MEMORYCLERK_SQLSTORENG     |   To be documented in the future     |
|MEMORYCLERK_SQLTRACE     |     To be documented in the future   |
|MEMORYCLERK_SQLUTILITIES     |    To be documented in the future    |
|MEMORYCLERK_SQLXML     |     To be documented in the future   |
|MEMORYCLERK_SQLXP     |    To be documented in the future    |
|MEMORYCLERK_SVL     |  To be documented in the future <br /><br />**Applies to**: [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] and later     |
|MEMORYCLERK_TEST     |    To be documented in the future  <br /><br />**Applies to**: [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] and later  |
|MEMORYCLERK_UNITTEST     |    To be documented in the future <br /><br />**Applies to**: [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] and later   |
|MEMORYCLERK_WRITEPAGERECORDER     |    To be documented in the future  <br /><br />**Applies to**: [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] and later  |
|MEMORYCLERK_XE     |    To be documented in the future    |
|MEMORYCLERK_XE_BUFFER     |     To be documented in the future   |
|MEMORYCLERK_XLOG_SERVER     |    To be documented in the future <br /><br />**Applies to**: [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] and later |
|MEMORYCLERK_XTP     |    To be documented in the future    |
|OBJECTSTORE_LBSS     |    To be documented in the future    |
|OBJECTSTORE_LOCK_MANAGER     |     To be documented in the future   |
|OBJECTSTORE_SECAUDIT_EVENT_BUFFER     |     To be documented in the future   |
|OBJECTSTORE_SERVICE_BROKER     |    To be documented in the future    |
|OBJECTSTORE_SNI_PACKET     |    To be documented in the future    |
|OBJECTSTORE_XACT_CACHE     |     To be documented in the future   |
|USERSTORE_DBMETADATA     |    To be documented in the future    |
|USERSTORE_OBJPERM     |    To be documented in the future    |
|USERSTORE_QDSSTMT     |    To be documented in the future    |
|USERSTORE_SCHEMAMGR     |    To be documented in the future    |
|USERSTORE_SXC     |    To be documented in the future    |
|USERSTORE_TOKENPERM     |    To be documented in the future    |

## See Also  

 [SQL Server Operating System Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-server-operating-system-related-dynamic-management-views-transact-sql.md)   
 [sys.dm_os_sys_info &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-sys-info-transact-sql.md)   
 [sys.dm_exec_query_memory_grants &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-memory-grants-transact-sql.md)   
 [sys.dm_exec_requests &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md)   
 [sys.dm_exec_query_plan &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-plan-transact-sql.md)   
 [sys.dm_exec_sql_text &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-sql-text-transact-sql.md)  
  
  


