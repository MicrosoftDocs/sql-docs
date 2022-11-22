---
title: "SQL Server XTP Databases object"
description: Learn about the SQL Server XTP Databases performance object, which provides In-Memory OLTP database-specific counters.
ms.custom: ""
ms.date: "07/13/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server 2016 XTP Databases"
  - "SQL Server 2017 XTP Databases"
  - "SQL Server XTP Databases"
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# SQL Server XTP Databases object
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The **SQL Server XTP Databases** performance object provides In-Memory OLTP database-specific counters.

> [!NOTE]
>  The SQL Server XTP Databases counters are not currently visible from `sys.dm_os_performance_counters`. On Windows, the counters can be viewed from [System Monitor](../../relational-databases/performance/start-system-monitor-windows.md).

This table describes the **SQL Server XTP Databases** counters.

|Counter|Description| 
|-------------|-----------------|  
|**Avg Transaction Segment Large Data Size**|Average size of transaction segment large data payload. This is a very low-level counter, not intended for customer use.|
|**Avg Transaction Segment Size**|Average size of transaction segment payload. If this value goes to zero, more pages are allocated from the backend allocator. This is a very low-level counter, not intended for customer use.|
|**Flush Thread 256K Queue Depth**|Flush Thread queue depth for 256K IO requests.|
|**Flush Thread 4K Queue Depth**|Flush Thread queue depth for 4K IO requests.|
|**Flush Thread 64K Queue Depth**|Flush Thread queue depth for 64K IO requests.|
|**Flush Thread Frozen IOs/sec (256K)**|The number of 256K IO requests encountered during flush page processing that are above the freeze threshold and thus cannot be issued.|
|**Flush Thread Frozen IOs/sec (4K)**|The number of 4K IO requests encountered during flush page processing that are above the freeze threshold and thus cannot be issued.|
|**Flush Thread Frozen IOs/sec (64K)**|The number of 64K IO requests encountered during flush page processing that are above the freeze threshold and thus cannot be issued.|
|**IoPagePool256K Free List Count**|Number of pages in the 256K IO page pool free list. If this value goes to zero, more pages are allocated from the backend allocator. This is a very low-level counter, not intended for customer use.|
|**IoPagePool256K Total Allocated**|Total number of pages allocated and held by the 256K IO page pool from the backend allocator. This is a very low-level counter, not intended for customer use.|
|**IoPagePool4K Free List Count**|Number of pages in the 4K IO page pool free list. If this value goes to zero, more pages are allocated from the backend allocator. This is a very low-level counter, not intended for customer use.|
|**IoPagePool4K Total Allocated**|Total number of pages allocated and held by the 4K IO page pool from the backend allocator. This is a very low-level counter, not intended for customer use.|
|**IoPagePool64K Free List Count**|Number of pages in the 64K IO page pool free list. If this value goes to zero, more pages are allocated from the backend allocator. This is a very low-level counter, not intended for customer use.|
|**IoPagePool64K Total Allocated**|Total number of pages allocated and held by the 64K IO page pool from the backend allocator. This is a very low-level counter, not intended for customer use.|
|**MtLog 256K Expand Count**|Number of times a 256K MtLog was expanded. This is a very low-level counter, not intended for customer use.|
|**MtLog 256K IOs Outstanding**|The number of outstanding 256K IO requests issued by MtLog.|
|**MtLog 256K Page Fill %/Page Flushed**|Average fill percentage of each 256K MtLog page flushed. This is a very low-level counter, not intended for customer use.|
|**MtLog 256K Write Bytes/sec**|Write bytes per second on 256K MtLog objects. This is a very low-level counter, not intended for customer use.|
|**MtLog 4K Expand Count**|Number of times a 4K MtLog was expanded. This is a very low-level counter, not intended for customer use.|
|**MtLog 4K IOs Outstanding**|The number of outstanding 4K IO requests issued by MtLog.|
|**MtLog 4K Page Fill %/Page Flushed**|Average fill percentage of each 4K MtLog page flushed. This is a very low-level counter, not intended for customer use.|
|**MtLog 4K Write Bytes/sec**|Write bytes per second on 4K MtLog objects. This is a very low-level counter, not intended for customer use.|
|**MtLog 64K Expand Count**|Number of times a 64K MtLog was expanded. This is a very low-level counter, not intended for customer use.|
|**MtLog 64K IOs Outstanding**|The number of outstanding 64K IO requests issued by MtLog.|
|**MtLog 64K Page Fill %/Page Flushed**|Average fill percentage of each 64K MtLog page flushed. This is a very low-level counter, not intended for customer use.|
|**MtLog 64K Write Bytes/sec**|Write bytes per second on 64K MtLog objects. This is a very low-level counter, not intended for customer use.|
|**Num Merges**|The number of merges in flight.|
|**Num Merges/sec**|The number of merges created per second (on average).|
|**Num Serializations**|The number of serializations in flight.|
|**Num Serializations/sec**|The number of serializations created per second (on average).|
|**Redo Active Worker**|The number of active redo workers.|
|**Redo Log Processed Bytes/sec**|Log bytes per second that Redo controller has processed.|
|**Redo Segment Definition/Sec**|The number segments definitions seen per second. This is a very low-level counter, not intended for customer use.|
|**Tail Cache Page Count**|Number of pages allocated in the Tail Cache. This is a very low-level counter, not intended for customer use.|
|**Tail Cache Page Count Peak**|Highest number of pages allocated in the Tail Cache. This is a very low-level counter, not intended for customer use.|


## See also  
- [In-Memory OLTP and Memory-Optimization](../in-memory-oltp/overview-and-usage-scenarios.md)
- [SQL Server XTP &#40;In-Memory OLTP&#41; Performance Counters](../../relational-databases/performance-monitor/sql-server-xtp-in-memory-oltp-performance-counters.md)