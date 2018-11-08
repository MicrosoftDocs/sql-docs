---
title: "sys.dm_os_memory_nodes (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/13/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "dm_os_memory_nodes_TSQL"
  - "sys.dm_os_memory_nodes"
  - "sys.dm_os_memory_nodes_TSQL"
  - "dm_os_memory_nodes"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_os_memory_nodes dynamic management view"
ms.assetid: bf4032fe-7db1-40e9-a62e-d69cebff4b44
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_os_memory_nodes (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Allocations that are internal to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] use the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] memory manager. Tracking the difference between process memory counters from **sys.dm_os_process_memory** and internal counters can indicate memory use from external components in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] memory space.  
  
 Nodes are created per physical NUMA memory nodes. These might be different from the CPU nodes in **sys.dm_os_nodes**.  
  
 No allocations done directly through Windows memory allocations routines are tracked. The following table provides information about memory allocations done only by using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] memory manager interfaces.  
  
> [!NOTE]  
>  To call this from [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the name **sys.dm_pdw_nodes_os_memory_nodes**.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**memory_node_id**|**smallint**|Specifies the ID of the memory node. Related to **memory_node_id** of **sys.dm_os_memory_clerks**. Not nullable.|  
|**virtual_address_space_reserved_kb**|**bigint**|Indicates the number of virtual address reservations, in kilobytes (KB), which are neither committed nor mapped to physical pages. Not nullable.|  
|**virtual_address_space_committed_kb**|**bigint**|Specifies the amount of virtual address, in KB, that has been committed or mapped to physical pages. Not nullable.|  
|**locked_page_allocations_kb**|**bigint**|Specifies the amount of physical memory, in KB, that has been locked by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Not nullable.|  
|**single_pages_kb**|**bigint**|**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)].<br /><br /> Amount of committed memory, in KB, that is allocated by using the single page allocator by threads running on this node. This memory is allocated from the buffer pool. This value indicates the node where allocations request occurred, not the physical location where the allocation request was satisfied.|  
|**pages_kb**|**bigint**|**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> Specifies the amount of committed memory, in KB, which is allocated from this NUMA node by Memory Manager Page Allocator. Not nullable.|  
|**multi_pages_kb**|**bigint**|**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)].<br /><br /> Amount of committed memory, in KB, that is allocated by using the multipage allocator by threads running on this node. This memory is from outside the buffer pool. This value indicates the node where the allocation requests occurred, not the physical location where the allocation request was satisfied.|  
|**shared_memory_reserved_kb**|**bigint**|Specifies the amount of shared memory, in KB, that has been reserved from this node. Not nullable.|  
|**shared_memory_committed_kb**|**bigint**|Specifies the amount of shared memory, in KB, that has been committed on this node. Not nullable.|  
|**cpu_affinity_mask**|**bigint**|**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> Internal use only. Not nullable.|  
|**online_scheduler_mask**|**bigint**|**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> Internal use only. Not nullable.|  
|**processor_group**|**smallint**|**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> Internal use only. Not nullable.|  
|**foreign_committed_kb**|**bigint**|**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> Specifies the amount of committed memory, in KB, from other memory nodes. Not nullable.|  
|**target_kb** |**bigint** |**Applies to**: [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)].<br /><br /> Specifies the memory goal for the memory node, in KB. |   
|**pdw_node_id**|**int**|**Applies to**: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]<br /><br /> The identifier for the node that this distribution is on.|  
  
## Permissions

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)], requires `VIEW SERVER STATE` permission.   
On [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)], requires the `VIEW DATABASE STATE` permission in the database.   

## See Also  
  [SQL Server Operating System Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-server-operating-system-related-dynamic-management-views-transact-sql.md)  
  
  


