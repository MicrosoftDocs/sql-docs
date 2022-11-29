---
title: "sys.dm_os_memory_brokers (Transact-SQL)"
description: sys.dm_os_memory_brokers (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "08/18/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_os_memory_brokers"
  - "dm_os_memory_brokers_TSQL"
  - "sys.dm_os_memory_brokers_TSQL"
  - "dm_os_memory_brokers"
helpviewer_keywords:
  - "sys.dm_os_memory_brokers dynamic management view"
dev_langs:
  - "TSQL"
ms.assetid: 48dd6ad9-0d36-4370-8a12-4921d0df4b86
monikerRange: ">=sql-server-2016||>=sql-server-linux-2017||>=aps-pdw-2016||=azure-sqldw-latest"
---
# sys.dm_os_memory_brokers (Transact-SQL)
[!INCLUDE [sql-asa-pdw](../../includes/applies-to-version/sql-asa-pdw.md)]

  Allocations that are internal to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] use the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] memory manager. Tracking the difference between process memory counters from **sys.dm_os_process_memory** and internal counters can indicate memory use from external components in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] memory space.  
  
 Memory brokers fairly distribute memory allocations between various components within [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], based on current and projected usage. Memory brokers do not perform allocations. They only track allocations for computing distribution.  
  
 The following table provides information about memory brokers.  
  
> [!NOTE]  
>  To call this from [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the name **sys.dm_pdw_nodes_os_memory_brokers**. [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)] 
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**pool_id**|**int**|ID of the resource pool if it is associated with a Resource Governor pool.|  
|**memory_broker_type**|**nvarchar(60)**|Type of memory broker. There are currently three types of memory brokers in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], listed below with their descriptions.<br /><br /> **MEMORYBROKER_FOR_CACHE** : Memory that is allocated for use by cached objects (Not Buffer Pool cache).<br /><br /> **MEMORYBROKER_FOR_STEAL** : Memory that is stolen from the buffer pool. This memory is tracked by memory clerks and is not available for reuse by other components until it is freed by the current owner.<br /><br /> **MEMORYBROKER_FOR_RESERVE** : Memory reserved for future use by currently executing requests.|  
|**allocations_kb**|**bigint**|Amount of memory, in kilobytes (KB), that has been allocated to this type of broker.|  
|**allocations_kb_per_sec**|**bigint**|Rate of memory allocations in kilobytes (KB) per second. This value can be negative for memory deallocations.|  
|**predicted_allocations_kb**|**bigint**|Predicted amount of allocated memory by the broker. This is based on the memory usage pattern.|  
|**target_allocations_kb**|**bigint**|Recommended amount of allocated memory, in kilobytes (KB), that is based on current settings and the memory usage pattern. This broker should grow to or shrink to this number.|  
|**future_allocations_kb**|**bigint**|Projected number of allocations, in kilobytes (KB), that will be done in the next several seconds.|  
|**overall_limit_kb**|**bigint**|Maximum amount of memory, in kilobytes (KB), that the broker can allocate.|  
|**last_notification**|**nvarchar(60)**|Memory usage recommendation that is based on the current settings and usage pattern. Valid values are as follows:<br /><br /> grow<br /><br /> shrink<br /><br /> stable|  
|**pdw_node_id**|**int**|**Applies to**: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]<br /><br /> The identifier for the node that this distribution is on.|  
  
## Permissions  

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] and SQL Managed Instance, requires `VIEW SERVER STATE` permission.

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Azure Active Directory admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.   
  
## See Also  

  [SQL Server Operating System Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-server-operating-system-related-dynamic-management-views-transact-sql.md)  
  
