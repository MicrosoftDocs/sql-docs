---
title: "SQL Server, Buffer Manager object"
description: Learn about the Buffer Manager object, which provides counters to monitor memory for pages, counters to monitor physical I/O, and buffer pool extensions.
ms.custom: ""
ms.date: "07/12/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "Buffer Manager object"
  - "SQLServer:Buffer Manager"
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# SQL Server, Buffer Manager object
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  The **Buffer Manager** object provides counters to monitor how [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses:  
  
-   Memory to store data pages.  
  
-   Counters to monitor the physical I/O as [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] reads and writes database pages.  
  
-   Buffer pool extension to extend the buffer cache by using fast non-volatile storage such as solid-state drives (SSD).  
  
 Monitoring the memory and the counters used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] helps you determine:  
  
-   If bottlenecks exist from inadequate physical memory. If it cannot store frequently accessed data in cache, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must retrieve the data from disk.   
  
-   If query performance can be improved by adding more memory, or by making more memory available to the data cache or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] internal structures.  
  
-   How often [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] needs to read data from disk. Compared with other operations, such as memory access, physical I/O consumes a lot of time. Minimizing physical I/O can improve query performance.  
  
## Buffer Manager Performance Objects  
 This table describes the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **Buffer Manager** performance objects.  
  
|**SQL Server Buffer Manager** counters|Description|  
|----------------------------------------|-----------------|  
|**Background writer pages/sec**|Number of pages flushed to enforce the recovery interval settings.| 
|**Buffer cache hit ratio**|Indicates the percentage of pages found in the buffer cache without having to read from disk. The ratio is the total number of cache hits divided by the total number of cache lookups over the last few thousand page accesses. After a long period of time, the ratio moves very little. Because reading from the cache is much less expensive than reading from disk, you want this ratio to be high. Generally, you can increase the buffer cache hit ratio by increasing the amount of memory available to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or by using the buffer pool extension feature.|  
|**Buffer cache hit ratio base**|For internal use only.|
|**Checkpoint pages/sec**|Indicates the number of pages flushed to disk per second by a checkpoint or other operation that require all dirty pages to be flushed.|  
|**Database pages**|Indicates the number of pages in the buffer pool with database content.|  
|**Extension allocated pages**|Total number of non-free cache pages in the buffer pool extension file.|  
|**Extension free pages**|Total number of free cache pages in the buffer pool extension file.|  
|**Extension in use as percentage**|Percentage of the buffer pool extension paging file occupied by buffer manager pages.|  
|**Extension outstanding IO counter**|I/O queue length for the buffer pool extension file.|  
|**Extension page evictions/sec**|Number of pages evicted from the buffer pool extension file per second.|  
|**Extension page reads/sec**|Number of pages read from the buffer pool extension file per second.|  
|**Extension page unreferenced time**|Average seconds a page will stay in the buffer pool extension without references to it.|  
|**Extension pages writes/sec**|Number of pages written to the buffer pool extension file per second.|  
|**Free list stalls/sec**|Indicates the number of requests per second that had to wait for a free page.|  
|**Integral Controller Slope**|The slope that integral controller for the buffer pool last used, times -10 billion.| 
|**Lazy writes/sec**|Indicates the number of buffers written per second by the buffer manager's lazy writer. The *lazy writer* is a system process that flushes out batches of dirty, aged buffers (buffers that contain changes that must be written back to disk before the buffer can be reused for a different page) and makes them available to user processes. The lazy writer eliminates the need to perform frequent checkpoints in order to create available buffers.|  
|**Page life expectancy**|Indicates the number of seconds a page will stay in the buffer pool without references.|  
|**Page lookups/sec**|Indicates the number of requests per second to find a page in the buffer pool.|  
|**Page reads/sec**|Indicates the number of physical database page reads that are issued per second. This statistic displays the total number of physical page reads across all databases. Because physical I/O is expensive, you may be able to minimize the cost, either by using a larger data cache, intelligent indexes, and more efficient queries, or by changing the database design.|  
|**Page writes/sec**|Indicates the number of physical database page writes that are issued per second.|  
|**Readahead pages/sec**|Indicates the number of pages read per second in anticipation of use.|  
|**Readahead time/sec**|Time (microseconds) spent issuing readahead.|
|**Target pages**|Ideal number of pages in the buffer pool.|

  
## Example

You begin to explore the query performance counters in this object using this T-SQL query on the [sys.dm_os_performance_counters](../system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md) dynamic management view:

```sql
SELECT * FROM sys.dm_os_performance_counters
WHERE object_name LIKE '%Buffer Manager%';
```    


## See also  
 - [SQL Server:Buffer Node](../../relational-databases/performance-monitor/sql-server-buffer-node.md)   
 - [Server Memory Server Configuration Options](../../database-engine/configure-windows/server-memory-server-configuration-options.md)   
 - [SQL Server, Plan Cache Object](../../relational-databases/performance-monitor/sql-server-plan-cache-object.md)   
 - [Monitor Resource Usage &#40;System Monitor&#41;](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)   
 - [sys.dm_os_performance_counters &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md)   
 - [Buffer Pool Extension](../../database-engine/configure-windows/buffer-pool-extension.md)  
  
  
