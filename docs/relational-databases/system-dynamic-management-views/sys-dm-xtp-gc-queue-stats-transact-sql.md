---
title: "sys.dm_xtp_gc_queue_stats (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/02/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "dm_xtp_gc_stats"
  - "dm_xtp_gc_stats_TSQL"
  - "sys.dm_xtp_gc_stats_TSQL"
  - "sys.dm_xtp_gc_stats"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_xtp_gc_stats dynamic management view"
ms.assetid: addef774-318d-46a7-85df-f93168a800cb
author: stevestein
ms.author: sstein
manager: craigg
---
# sys.dm_xtp_gc_queue_stats (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2014-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2014-xxxx-xxxx-xxx-md.md)]

  Outputs information about each garbage collection worker queue on the server, and various statistics about each. There is one queue per logical CPU.  
  
 The main garbage collection thread (the Idle thread) tracks updated, deleted, and inserted rows for all transactions completed since the last invocation of the main garbage collection thread. When the garbage collection thread wakes, it determines if the timestamp of the oldest active transaction has changed. If the oldest active transaction has changed, then the idle thread enqueues work items (in chunks of 16 rows) for transactions whose write sets are no longer needed. For example, if you delete 1,024 rows, you will eventually see 64 garbage collection work items queued, each containing 16 deleted rows.  After a user transaction commits, it selects all enqueued items on its scheduler. If there are no enqueued items on its scheduler, the user transaction will search on any queue in the current NUMA node.  
  
 You can determine if garbage collection is freeing memory for deleted rows by executing sys.dm_xtp_gc_queue_stats to see if the enqueued work is being processed. If entries in the current_queue_depth are not being processed or if no new work items are being added to the current_queue_depth, this is an indication that garbage collection is not freeing memory. For example, garbage collection can't be done if there is a long running transaction.  
  
 For more information, see [In-Memory OLTP &#40;In-Memory Optimization&#41;](../../relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization.md).  
  

|Column name|Type|Description|  
|-----------------|----------|-----------------|  
|queue_id|**int**|The unique identifier of the queue.|  
|total_enqueues|**bigint**|The total number of garbage collection work items enqueued to this queue since the server started.|  
|total_dequeues|**bigint**|The total number of garbage collection work items dequeued from this queue since the server started.|  
|current_queue_depth|**bigint**|The current number of garbage collection work items present on this queue. This item may imply one or more to be garbage collected.|  
|maximum_queue_depth|**bigint**|The maximum depth this queue has seen.|  
|last_service_ticks|**bigint**|CPU ticks at the time the queue was last serviced.|  
  
## Permissions  
 Requires VIEW SERVER STATE permission.  
  
## User Scenario  
 This output shows that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is either running on 4 cores or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance has been affinitized to 4 cores:  
  
 This output shows that there are no work items in the queues to process. For queue 0, the total work items de-queued since SQL Startup are 15625 and the max queue depth has been 215625.  
  
```  
queue_id total_enqueues total_dequeues current_queue_depth  maximum_queue_depth  last_service_ticks  
----------------------------------------------------------------------------------------------------  
0        15625                15625    0                    15625                1233573168347  
1        15625                15625    0                    15625                1234123295566  
2        15625                15625    0                    15625                1233569418146  
3        15625                15625    0                    15625                1233571605761  
```  
  
## See Also  
 [Memory-Optimized Table Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/memory-optimized-table-dynamic-management-views-transact-sql.md)  
  
  
