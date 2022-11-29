---
title: "sys.dm_os_memory_cache_clock_hands (Transact-SQL)"
description: sys.dm_os_memory_cache_clock_hands (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "12/21/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_os_memory_cache_clock_hands_TSQL"
  - "dm_os_memory_cache_clock_hands"
  - "dm_os_memory_cache_clock_hands_TSQL"
  - "sys.dm_os_memory_cache_clock_hands"
helpviewer_keywords:
  - "sys.dm_os_memory_cache_clock_hands dynamic management view"
dev_langs:
  - "TSQL"
ms.assetid: 0660eddc-691c-425f-9d43-71151d644de7
monikerRange: ">=sql-server-2016||>=sql-server-linux-2017||>=aps-pdw-2016||=azure-sqldw-latest"
---
# sys.dm_os_memory_cache_clock_hands (Transact-SQL)
[!INCLUDE [sql-asa-pdw](../../includes/applies-to-version/sql-asa-pdw.md)]

  Returns the status of each hand for a specific cache clock.  
  
> [!NOTE]  
>  To call this from [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the name **sys.dm_pdw_nodes_os_memory_cache_clock_hands**. [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**cache_address**|**varbinary(8)**|Address of the cache associated with the clock. Is not nullable.|  
|**name**|**nvarchar(256)**|Name of the cache. Is not nullable.|  
|**type**|**nvarchar(60)**|Type of cache store. There can be several caches of the same type. Is not nullable.|  
|**clock_hand**|**nvarchar(60)**|Type of hand. Value is one of the following:<br /><br /> External<br /><br /> Internal<br /><br /> Is not nullable.|  
|**clock_status**|**nvarchar(60)**|Status of the clock. Value is one of the following:<br /><br /> Suspended<br /><br /> Running<br /><br /> Is not nullable.|  
|**rounds_count**|**bigint**|Number of sweeps made through the cache to remove entries. Is not nullable.|  
|**removed_all_rounds_count**|**bigint**|Number of entries removed by all sweeps. Is not nullable.|  
|**updated_last_round_count**|**bigint**|Number of entries updated during the last sweep. Is not nullable.|  
|**removed_last_round_count**|**bigint**|Number of entries removed during the last sweep. Is not nullable.|  
|**last_tick_time**|**bigint**|Last time, in milliseconds, that the clock hand moved. Is not nullable.|  
|**round_start_time**|**bigint**|Time, in milliseconds, of the previous sweep. Is not nullable.|  
|**last_round_start_time**|**bigint**|Total time, in milliseconds, taken by the clock to complete the previous round. Is not nullable.|  
|**pdw_node_id**|**int**|**Applies to**: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]<br /><br /> The identifier for the node that this distribution is on.|  
  
## Permissions  

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] and SQL Managed Instance, requires `VIEW SERVER STATE` permission.

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Azure Active Directory admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.   
  
## Remarks  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] stores information in memory in a structure called a memory cache. The information in the cache can be data, index entries, compiled procedure plans, and various other types of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] information. To avoid re-creating the information, it is retained the memory cache as long as possible and is ordinarily removed from the cache when it is too old to be useful, or when the memory space is needed for new information. The process that removes old information is called a memory sweep. The memory sweep is a frequent activity, but is not continuous. A clock algorithm controls the sweep of the memory cache. Each clock can control several memory sweeps, which are called hands. The memory-cache clock hand is the current location of one of the hands of a memory sweep.  

## See Also  
 [SQL Server Operating System Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-server-operating-system-related-dynamic-management-views-transact-sql.md)    
 [sys.dm_os_memory_cache_counters &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-memory-cache-counters-transact-sql.md)
