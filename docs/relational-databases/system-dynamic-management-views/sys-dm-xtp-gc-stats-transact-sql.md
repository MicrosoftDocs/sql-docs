---
title: "sys.dm_xtp_gc_stats (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
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
ms.assetid: 8287d611-50e3-43e1-ba8d-3e3793d3ba0e
author: stevestein
ms.author: sstein
manager: craigg
---
# sys.dm_xtp_gc_stats (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2014-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2014-xxxx-xxxx-xxx-md.md)]

  Provides information (the overall statistics) about the current behavior of the [!INCLUDE[hek_2](../../includes/hek-2-md.md)] garbage-collection process.  
  
 Rows are garbage collected as part of regular transaction processing, or by the main garbage collection thread, which is referred to as the idle worker. When a user transaction commits, it dequeues one work item from the garbage collection queue ([sys.dm_xtp_gc_queue_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-xtp-gc-queue-stats-transact-sql.md)). Any rows that could be garbage collected but were not accessed by main user transaction are garbage collected by the idle worker, as part of the dusty corner scan (a scan for areas of the index that are less accessed).  
  
 For more information, see [In-Memory OLTP &#40;In-Memory Optimization&#41;](../../relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization.md).  
  
|Column name|Type|Description|  
|-----------------|----------|-----------------|  
|rows_examined|**bigint**|The number of rows examined by the garbage collection subsystem since the server was started.|  
|rows_no_sweep_needed|**bigint**|The number of rows that were removed without a dusty corner scan.|  
|rows_first_in_bucket|**bigint**|The number of rows examined by garbage collection that were the first row in the hash bucket.|  
|rows_first_in_bucket_removed|**bigint**|The number of rows examined by garbage collection that were the first row in the hash bucket that have been removed.|  
|rows_marked_for_unlink|**bigint**|The number of rows examined by garbage collection that were already marked as unlinked in their indexes with ref count =0.|  
|parallel_assist_count|**bigint**|The number of rows processed by user transactions.|  
|idle_worker_count|**bigint**|The number of garbage rows processed by the idle worker.|  
|sweep_scans_started|**bigint**|The number of dusty corner scans performed by garbage collection subsystem.|  
|sweep_scans_retries|**bigint**|The number of dusty corner scans performed by the garbage collection subsystem.|  
|sweep_rows_touched|**bigint**|Rows read by dusty corner processing.|  
|sweep_rows_expiring|**bigint**|Expiring rows read by dusty corner processing.|  
|sweep_rows_expired|**bigint**|Expired rows read by dusty corner processing.|  
|sweep_rows_expired_removed|**bigint**|Expired rows removed by dusty corner processing.|  
  
## Permissions  
 Requires VIEW SERVER STATE permission on the instance.  
  
## Usage Scenario  
 The following is sample output:  
  
```  
rows_examined        rows_no_sweep_needed rows_first_in_bucket rows_first_in_bucket_removed  
280085               209512               69905  
rows_first_in_bucket_removed rows_marked_for_unlink parallel_assist_count idle_worker_count  
69905                        0                      8953  
  
idle_worker_count    sweep_scans_started  sweep_scan_retries   sweep_rows_touched  
10306473             670                  0                    1343  
  
sweep_rows_expiring  sweep_rows_expired   sweep_rows_expired_removed  
               0                 673673  
```  
  
## See Also  
 [Memory-Optimized Table Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/memory-optimized-table-dynamic-management-views-transact-sql.md)  
  
  
