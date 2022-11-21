---
title: "sys.dm_os_memory_cache_counters (Transact-SQL)"
description: sys.dm_os_memory_cache_counters (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "08/18/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_os_memory_cache_counters_TSQL"
  - "dm_os_memory_cache_counters_TSQL"
  - "sys.dm_os_memory_cache_counters"
  - "dm_os_memory_cache_counters"
helpviewer_keywords:
  - "sys.dm_os_memory_cache_counters dynamic management view"
dev_langs:
  - "TSQL"
ms.assetid: ca7bd036-d661-4c17-b00a-e1a975bd8932
monikerRange: ">=sql-server-2016||>=sql-server-linux-2017||>=aps-pdw-2016||=azure-sqldw-latest"
---
# sys.dm_os_memory_cache_counters (Transact-SQL)
[!INCLUDE [sql-asa-pdw](../../includes/applies-to-version/sql-asa-pdw.md)]

  Returns a snapshot of the health of a cache in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. **sys.dm_os_memory_cache_counters** provides run-time information about the cache entries allocated, their use, and the source of memory for the cache entries.  
  
> [!NOTE]  
> To call this from [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the name **sys.dm_pdw_nodes_os_memory_cache_counters**. [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)] 
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**cache_address**|**varbinary(8)**|Indicates the address (primary key) of the counters associated with a specific cache. Is not nullable.|  
|**name**|**nvarchar(256)**|Specifies the name of the cache. Is not nullable.|  
|**type**|**nvarchar(60)**|Indicates the type of cache that is associated with this entry. Is not nullable.|  
|**single_pages_kb**|**bigint**|**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)].<br /><br /> Amount, in kilobytes, of the single-page memory allocated. This is the amount of memory allocated by using the single-page allocator. This refers to the 8-KB pages that are taken directly from the buffer pool for this cache. Is not nullable.|  
|**pages_kb**|**bigint**|**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later.<br /><br /> Specifies the amount, in kilobytes, of the memory allocated in the cache. Is not nullable.|  
|**multi_pages_kb**|**bigint**|**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)].<br /><br /> Amount, in kilobytes, of the multipage memory allocated. This is the amount of memory allocated by using the multiple-page allocator of the memory node. This memory is allocated outside the buffer pool and takes advantage of the virtual allocator of the memory nodes. Is not nullable.|  
|**pages_in_use_kb**|**bigint**|**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later.<br /><br /> Specifies the amount, in kilobytes, of the memory that is allocated and in use in the cache. Is nullable.  Values for objects of type `USERSTORE_<*>` are not tracked.  NULL is reported for them.|  
|**single_pages_in_use_kb**|**bigint**|**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)].<br /><br /> Amount, in kilobytes, of the single-page memory that is being used. Is nullable. This information is not tracked for objects of type USERSTORE_\<*> and these values will be NULL.|  
|**multi_pages_in_use_kb**|**bigint**|**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)].<br /><br /> Amount, in kilobytes, of the multipage memory that is being used. NULLABLE. This information is not tracked for objects of type USERSTORE_\<*>, and these values will be NULL.|  
|**entries_count**|**bigint**|Indicates the number of entries in the cache. Is not nullable.|  
|**entries_in_use_count**|**bigint**|Indicates the number of entries in the cache that is being used. Is not nullable.|  
|**pdw_node_id**|**int**|**Applies to**: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]<br /><br /> The identifier for the node that this distribution is on.|  
  
## Permissions 

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] and SQL Managed Instance, requires `VIEW SERVER STATE` permission.

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Azure Active Directory admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.   

## See Also  
  [SQL Server Operating System Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-server-operating-system-related-dynamic-management-views-transact-sql.md)  
  
