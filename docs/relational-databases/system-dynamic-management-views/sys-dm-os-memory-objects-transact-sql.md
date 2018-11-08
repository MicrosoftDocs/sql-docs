---
title: "sys.dm_os_memory_objects (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/13/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "dm_os_memory_objects"
  - "sys.dm_os_memory_objects"
  - "sys.dm_os_memory_objects_TSQL"
  - "dm_os_memory_objects_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_os_memory_objects dynamic management view"
ms.assetid: 5688bcf8-5da9-4ff9-960b-742b671d7096
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_os_memory_objects (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns memory objects that are currently allocated by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. You can use **sys.dm_os_memory_objects** to analyze memory use and to identify possible memory leaks.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**memory_object_address**|**varbinary(8)**|Address of the memory object. Is not nullable.|  
|**parent_address**|**varbinary(8)**|Address of the parent memory object. Is nullable.|  
|**pages_allocated_count**|**int**|**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)].<br /><br /> Number of pages that are allocated by this object. Is not nullable.|  
|**pages_in_bytes**|**bigint**|**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> Amount of memory in bytes that is allocated by this instance of the memory object. Is not nullable.|  
|**creation_options**|**int**|Internal use only. Is nullable.|  
|**bytes_used**|**bigint**|Internal use only. Is nullable.|  
|**type**|**nvarchar(60)**|Type of memory object.<br /><br /> This indicates some component that this memory object belongs to, or the function of the memory object. Is nullable.|  
|**name**|**varchar(128)**|Internal use only. Nullable.|  
|**memory_node_id**|**smallint**|ID of a memory node that is being used by this memory object. Is not nullable.|  
|**creation_time**|**datetime**|Internal use only. Is nullable.|  
|**max_pages_allocated_count**|**int**|**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)].<br /><br /> Maximum number of pages allocated by this memory object. Is not nullable.|  
|**page_size_in_bytes**|**int**|**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> Size of pages in bytes allocated by this object. Is not nullable.|  
|**max_pages_in_bytes**|**bigint**|Maximum amount of memory  ever used by this memory object. Is not nullable.|  
|**page_allocator_address**|**varbinary(8)**|Memory address of page allocator. Is not nullable. For more information, see [sys.dm_os_memory_clerks &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-memory-clerks-transact-sql.md).|  
|**creation_stack_address**|**varbinary(8)**|Internal use only. Is nullable.|  
|**sequence_num**|**int**|Internal use only. Is nullable.|  
|**partition_type**|**int**|The type of partition:<br /><br /> 0 - Non-partitionable memory object<br /><br /> 1 - Partitionable memory object, currently not partitioned<br /><br /> 2 - Partitionable memory object, partitioned by NUMA node. In an environment with a single NUMA node this is equivalent to 1.<br /><br /> 3 - Partitionable memory object, partitioned by CPU.|  
|**contention_factor**|**real**|A value specifying contention on this memory object, with 0 meaning no contention. The value is updated whenever a specified number of memory allocations were made reflecting contention during that period. Applies only to thread-safe memory objects.|  
|**waiting_tasks_count**|**bigint**|Number of waits on this memory object. This counter is incremented whenever memory is allocated from this memory object. The increment is the number of tasks currently waiting for access to this memory object. Applies only to thread-safe memory objects. This is a best effort value without a correctness guarantee.|  
|**exclusive_access_count**|**bigint**|Specifies how often this memory object was accessed exclusively. Applies only to thread-safe memory objects.  This is a best effort value without a correctness guarantee.|  
|**pdw_node_id**|**int**|**Applies to**: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]<br /><br /> The identifier for the node that this distribution is on.|  
  
 **partition_type**, **contention_factor**, **waiting_tasks_count**, and **exclusive_access_count** are not yet implemented in [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)].  
  
## Permissions

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)], requires `VIEW SERVER STATE` permission.   
On [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)], requires the `VIEW DATABASE STATE` permission in the database.   

## Remarks  
 Memory objects are heaps. They provide allocations that have a finer granularity than those provided by memory clerks. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components use memory objects instead of memory clerks. Memory objects use the page allocator interface of the memory clerk to allocate pages. Memory objects do not use virtual or shared memory interfaces. Depending on the allocation patterns, components can create different types of memory objects to allocate regions of arbitrary size.  
  
 The typical page size for a memory object is 8 KB. However, incremental memory objects can have page sizes that range from 512 bytes to 8 KB.  
  
> [!NOTE]  
>  Page size is not a maximum allocation. Instead, page size is allocation granularity that is supported by a page allocator and that is implemented by a memory clerk. You can request allocations greater than 8 KB from memory objects.  
  
## Examples  
 The following example returns the amount of memory allocated by each memory object type.  
  
```  
SELECT SUM (pages_in_bytes) as 'Bytes Used', type   
FROM sys.dm_os_memory_objects  
GROUP BY type   
ORDER BY 'Bytes Used' DESC;  
GO  
```  
  
## See Also  
  [SQL Server Operating System Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-server-operating-system-related-dynamic-management-views-transact-sql.md)   
 [sys.dm_os_memory_clerks &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-memory-clerks-transact-sql.md)  
  
  


