---
title: "In-Memory OLTP Garbage Collection | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: 940140a7-4785-46fc-8bf4-151435dccd3c
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# In-Memory OLTP Garbage Collection
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  A data row is considered stale if it was deleted by a transaction that is no longer active. A stale row is eligible for garbage collection. The following are characteristics of garbage collection in [!INCLUDE[hek_2](../../includes/hek-2-md.md)]:  
  
-   Non-blocking. Garbage collection is distributed over time with minimal impact on the workload.  
  
-   Cooperative. User transactions participate in garbage collection with main garbage-collection thread.  
  
-   Efficient. User transactions delink stale rows in the access path (the index) being used. This reduces the work required when the row is finally removed.  
  
-   Responsive. Memory pressure leads to aggressive garbage collection.  
  
-   Scalable. After commit, user transactions do part of the work of garbage collection. The more transaction activity, the more the transactions delink stale rows.  
  
 Garbage collection is controlled by the main garbage collection thread. The main garbage collection thread runs every minute, or when the number of committed transactions exceeds an internal threshold. The task of the garbage collector is to:  
  
-   Identify transactions that have deleted or updated a set of rows and have committed before the oldest active transaction.  
  
-   Identity row versions created by these old transactions.  
  
-   Group old rows into one or more units of 16 rows each. This is done to distribute the work of the garbage collector into smaller units.  
  
-   Move these work units into the garbage collection queue, one for each scheduler. Refer to the garbage collector DMVs for the details: [sys.dm_xtp_gc_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-xtp-gc-stats-transact-sql.md), [sys.dm_db_xtp_gc_cycle_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-xtp-gc-cycle-stats-transact-sql.md), and [sys.dm_xtp_gc_queue_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-xtp-gc-queue-stats-transact-sql.md).  
  
 After a user transaction commits, it identifies all queued items associated with the scheduler it ran on and then releases the memory. If the garbage collection queue on the scheduler is empty, it searches for any non-empty queue in the current NUMA node. If there is low transactional activity and there is memory pressure, the main garbage-collection thread can access garbage collect rows from any queue. If there is no transactional activity after (for example) deleting a large number of rows and there is no memory pressure, the deleted rows will not be garbage collected until the transactional activity resumes or there is memory pressure.  
  
## See Also  
 [Managing Memory for In-Memory OLTP](https://msdn.microsoft.com/library/d82f21fa-6be1-4723-a72e-f2526fafd1b6)  
  
  
