---
title: "sys.dm_os_memory_cache_hash_tables (Transact-SQL)"
description: sys.dm_os_memory_cache_hash_tables (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/13/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_os_memory_cache_hash_tables_TSQL"
  - "sys.dm_os_memory_cache_hash_tables"
  - "dm_os_memory_cache_hash_tables"
  - "dm_os_memory_cache_hash_tables_TSQL"
helpviewer_keywords:
  - "sys.dm_os_memory_cache_hash_tables dynamic management view"
dev_langs:
  - "TSQL"
ms.assetid: 68b94f35-8f80-4d2b-bcde-7a21934219af
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_os_memory_cache_hash_tables (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns a row for each active cache in the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
> [!NOTE]  
>  To call this from [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the name **sys.dm_pdw_nodes_os_memory_cache_hash_tables**. [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)] 
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**cache_address**|**varbinary(8)**|Address (primary key) of the cache entry. Is not nullable.|  
|**name**|**nvarchar(256)**|Name of the cache. Is not nullable.|  
|**type**|**nvarchar(60)**|Type of cache. Is not nullable.|  
|**table_level**|**int**|Hash table number. A particular cache may have multiple hash tables that correspond to different hash functions. Is not nullable.|  
|**buckets_count**|**int**|Number of buckets in the hash table. Is not nullable.|  
|**buckets_in_use_count**|**int**|Number of buckets that are currently being used. Is not nullable.|  
|**buckets_min_length**|**int**|Minimum number of cache entries in a bucket. Is not nullable.|  
|**buckets_max_length**|**int**|Maximum number of cache entries in a bucket. Is not nullable.|  
|**buckets_avg_length**|**int**|Average number of cache entries in each bucket. Is not nullable.|  
|**buckets_max_length_ever**|**int**|Maximum number of cached entries in a hash bucket for this hash table since the server was started. Is not nullable.|  
|**hits_count**|**bigint**|Number of cache hits. Is not nullable.|  
|**misses_count**|**bigint**|Number of cache misses. Is not nullable.|  
|**buckets_avg_scan_hit_length**|**int**|Average number of examined entries in a bucket before the searched for an item was found. Is not nullable.|  
|**buckets_avg_scan_miss_length**|**int**|Average number of examined entries in a bucket before the search ended unsuccessfully. Is not nullable.|  
|**pdw_node_id**|**int**|The identifier for the node that this distribution is on.<br /><br /> **Applies to**: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]|  
  
## Permissions 

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] and SQL Managed Instance, requires `VIEW SERVER STATE` permission.

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Azure Active Directory admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.   

## See Also  
 
  [SQL Server Operating System Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-server-operating-system-related-dynamic-management-views-transact-sql.md)  
  
