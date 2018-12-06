---
title: "sys.dm_os_memory_cache_entries (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/18/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "dm_os_memory_cache_entries"
  - "sys.dm_os_memory_cache_entries"
  - "dm_os_memory_cache_entries_TSQL"
  - "sys.dm_os_memory_cache_entries_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_os_memory_cache_entries dynamic management view"
ms.assetid: dd32be6b-10d1-4059-b4fd-0bf817f40d54
author: stevestein
ms.author: sstein
manager: craigg
---
# sys.dm_os_memory_cache_entries (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns information about all entries in caches in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Use this view to trace cache entries to their associated objects. You can also use this view to obtain statistics on cache entries.  
  
> [!NOTE]  
>  To call this from [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the name **sys.dm_pdw_nodes_os_memory_cache_entries**.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**cache_address**|**varbinary(8)**|Address of the cache. Is not nullable.|  
|**name**|**nvarchar(256)**|Name of the cache. Is not nullable.|  
|**type**|**varchar(60)**|Type of cache. Is not nullable.|  
|**entry_address**|**varbinary(8)**|Address of the descriptor of the cache entry. Is not nullable.|  
|**entry_data_address**|**varbinary(8)**|Address of the user data in the cache entry.<br /><br /> 0x00000000 = Entry data address is not available.<br /><br /> Is not nullable.|  
|**in_use_count**|**int**|Number of concurrent users of this cache entry. Is not nullable.|  
|**is_dirty**|**bit**|Indicates whether this cache entry is marked for removal. 1 = marked for removal. Is not nullable.|  
|**disk_ios_count**|**int**|Number of I/Os incurred while this entry was created. Is not nullable.|  
|**context_switches_count**|**int**|Number of context switches incurred while this entry was created. Is not nullable.|  
|**original_cost**|**int**|Original cost of the entry. This value is an approximation of the number of I/Os incurred, CPU instruction cost, and the amount of memory consumed by entry. The greater the cost, the lower the chance that the item will be removed from the cache. Is not nullable.|  
|**current_cost**|**int**|Current cost of the cache entry. This value is updated during the process of entry purging. Current cost is reset to its original value on entry reuse. Is not nullable.|  
|**memory_object_address**|**varbinary(8)**|Address of the associated memory object. Is nullable.|  
|**pages_allocated_count**|**bigint**|**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)].<br /><br /> Number of 8-KB pages to store this cache entry. Is not nullable.|  
|**pages_kb**|**bigint**|**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> Amount of memory in kilobytes (KB) used by this cache entry.  Is not nullable.|  
|**entry_data**|**nvarchar(2048)**|Serialized representation of the cached entry. This information is cache store dependant. Is nullable.|  
|**pool_id**|**int**|**Applies to**: [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> Resource pool id associated with entry. Is nullable.<br /><br /> not katmai|  
|**pdw_node_id**|**int**|**Applies to**: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]<br /><br /> The identifier for the node that this distribution is on.|  
  
## Permissions 

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)], requires `VIEW SERVER STATE` permission.   
On [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)], requires the `VIEW DATABASE STATE` permission in the database.   

## See Also  
 
  [SQL Server Operating System Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-server-operating-system-related-dynamic-management-views-transact-sql.md)  
  
  


