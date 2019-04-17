---
title: "SQL Server, Access Methods Object | Microsoft Docs"
ms.custom: ""
ms.date: "03/20/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "Access Methods object"
  - "SQLServer:Access Methods"
ms.assetid: 27558585-e780-48bb-a042-30d664662ebc
author: julieMSFT
ms.author: jrasnick
manager: craigg
---
# SQL Server, Access Methods Object
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  The **Access Methods** object in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides counters to monitor how the logical data within the database is accessed. Physical access to the database pages on disk is monitored using the **Buffer Manager** counters. Monitoring the methods used to access data stored in the database can help you to determine whether query performance can be improved by adding or modifying indexes, adding or moving partitions, adding files or file groups, defragmenting indexes, or by rewriting queries. The **Access Methods** counters can also be used to monitor the amount of data, indexes, and free space within the database, thereby indicating data volume and fragmentation for each server instance. Excessive index fragmentation can impair performance.  
  
 For more detailed information about data volume, fragmentation and usage, use the following dynamic management views:  
  
-   [sys.dm_db_index_operational_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-index-operational-stats-transact-sql.md)  
  
-   [sys.dm_db_index_physical_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-index-physical-stats-transact-sql.md)  
  
-   [sys.dm_db_partition_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-partition-stats-transact-sql.md)  
  
-   [sys.dm_db_index_usage_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-index-usage-stats-transact-sql.md)  
  
 For space consumption in **tempdb** at the file, task and session level, use these dynamic management views:  
  
-   [sys.dm_db_file_space_usage &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-file-space-usage-transact-sql.md)  
  
-   [sys.dm_db_task_space_usage &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-task-space-usage-transact-sql.md)  
  
-   [sys.dm_db_session_space_usage &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-session-space-usage-transact-sql.md)  
  
 This table describes the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **Access Methods** counters.  
  
|SQL Server Access Methods counters|Description|  
|----------------------------------------|-----------------|  
|**AU cleanup batches/sec**|The number of batches per second that were completed successfully by the background task that cleans up deferred dropped allocation units.|  
|**AU cleanups/sec**|The number of allocation units per second that were successfully dropped the background task that cleans up deferred dropped allocation units. Each allocation unit drop requires multiple batches.|  
|**By-reference Lob Create Count**|Count of large object (lob) values that were passed by reference. By-reference lobs are used in certain bulk operations to avoid the cost of passing them by value.|  
|**By-reference Lob Use Count**|Count of by-reference lob values that were used. By-reference lobs are used in certain bulk operations to avoid the cost of passing them by-value.|  
|**Count Lob Readahead**|Count of lob pages on which readahead was issued.|  
|**Count Pull In Row**|Count of column values that were pulled in-row from off-row.|  
|**Count Push Off Row**|Count of column values that were pushed from in-row to off-row.|  
|**Deferred Dropped Aus**|The number of allocation units waiting to be dropped by the background task that cleans up deferred dropped allocation units.|  
|**Deferred Dropped rowsets**|The number of rowsets created as a result of aborted online index build operations that are waiting to be dropped by the background task that cleans up deferred dropped rowsets.|  
|**Dropped rowset cleanups/sec**|The number of rowsets per second created as a result of aborted online index build operations that were successfully dropped by the background task that cleans up deferred dropped rowsets.|  
|**Dropped rowsets skipped/sec**|The number of rowsets per second created as a result of aborted online index build operations that were skipped by the background task that cleans up deferred dropped rowsets created.|  
|**Extent Deallocations/sec**|Number of extents deallocated per second in all databases in this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|**Extents Allocated/sec**|Number of extents allocated per second in all databases in this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|**Failed AU cleanup batches/sec**|The number of batches per second that failed and required retry, by the background task that cleans up deferred dropped allocation units. Failure could be due to lack of memory or disk space, hardware failure and other reasons.|  
|**Failed leaf page cookie**|The number of times that a leaf page cookie could not be used during an index search since changes happened on the leaf page. The cookie is used to speed up index search.|  
|**Failed tree page cookie**|The number of times that a tree page cookie could not be used during an index search since changes happened on the parent pages of those tree pages. The cookie is used to speed up index search.|  
|**Forwarded Records/sec**|Number of records per second fetched through forwarded record pointers.|  
|**FreeSpace Page Fetches/sec**|Number of pages fetched per second by free space scans. These scans search for free space within pages already allocated to an allocation unit, to satisfy requests to insert or modify record fragments.|  
|**FreeSpace Scans/sec**|Number of scans per second that were initiated to search for free space within pages already allocated to an allocation unit to insert or modify record fragment. Each scan may find multiple pages.|  
|**Full Scans/sec**|Number of unrestricted full scans per second. These can be either base-table or full-index scans.|  
|**Index Searches/sec**|Number of index searches per second. These are used to start a range scan, reposition a range scan, revalidate a scan point, fetch a single index record, and search down the index to locate where to insert a new row.|  
|**InSysXact waits/sec**|Number of times a reader needs to wait for a page because the InSysXact bit is set.|  
|**LobHandle Create Count**|Count of temporary lobs created.|  
|**LobHandle Destroy Count**|Count of temporary lobs destroyed.|  
|**LobSS Provider Create Count**|Count of LOB Storage Service Providers (LobSSP) created. One worktable created per LobSSP.|  
|**LobSS Provider Destroy Count**|Count of LobSSP destroyed.|  
|**LobSS Provider Truncation Count**|Count of LobSSP truncated.|  
|**Mixed page allocations/sec**|Number of pages allocated per second from mixed extents. These could be used for storing the IAM pages and the first eight pages that are allocated to an allocation unit.|  
|**Page compression attempts/sec**|Number of pages evaluated for page-level compression. Includes pages that were not compressed because significant savings could be achieved. Includes all objects in the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For information about specific objects, see [sys.dm_db_index_operational_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-index-operational-stats-transact-sql.md).|  
|**Page Deallocations/sec**|Number of pages deallocated per second in all databases in this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. These include pages from mixed extents and uniform extents.|  
|**Page Splits/sec**|Number of page splits per second that occur as the result of overflowing index pages.|  
|**Pages Allocated/sec**|Number of pages allocated per second in all databases in this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. These include pages allocations from both mixed extents and uniform extents.|  
|**Pages compressed/sec**|Number of data pages that are compressed by using PAGE compression. Includes all objects in the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For information about specific objects, see [sys.dm_db_index_operational_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-index-operational-stats-transact-sql.md).|  
|**Probe Scans/sec**|Number of probe scans per second that are used to find at most one single qualified row in an index or base table directly.|  
|**Range Scans/sec**|Number of qualified range scans through indexes per second.|  
|**Scan Point Revalidations/sec**|Number of times per second that the scan point had to be revalidated to continue the scan.|  
|**Skipped Ghosted Records/sec**|Number of ghosted records per second skipped during scans.|  
|**Table Lock Escalations/sec**|Number of times locks on a table were escalated to the TABLE or HoBT granularity.|  
|**Used leaf page cookie**|Number of times a leaf page cookie is used successfully during an index search since no change happened on the leaf page. The cookie is used to speed up index search.|  
|**Used tree page cookie**|Number of times a tree page cookie is used successfully during an index search since no change happened on the parent page of the tree page. The cookie is used to speed up index search.|  
|**Workfiles Created/sec**|Number of work files created per second. For example, work files could be used to store temporary results for hash joins and hash aggregates.|  
|**Worktables Created/sec**|Number of work tables created per second. For example, work tables could be used to store temporary results for query spool, lob variables, XML variables, and cursors.|  
|**Worktables From Cache Base**|For internal use only.|  
|**Worktables From Cache Ratio**|Percentage of work tables created where the initial two pages of the work table were not allocated but were immediately available from the work table cache. (When a work table is dropped, two pages may remain allocated and they are returned to the work table cache. This increases performance.)|  
  
## See Also  
 [Monitor Resource Usage &#40;System Monitor&#41;](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)  
  
  
