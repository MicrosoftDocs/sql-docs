---
title: "In-Memory OLTP (In-Memory Optimization) | Microsoft Docs"
ms.custom: ""
ms.date: "07/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
helpviewer_keywords: 
  - "In-Memory OLTP"
  - "memory-optimized tables"
ms.assetid: e1d03d74-2572-4a55-afd6-7edf0bc28bdb
author: MightyPen
ms.author: genemi
manager: craigg
---
# In-Memory OLTP (In-Memory Optimization)
  New in [!INCLUDE[ssSQL14](../../../includes/sssql14-md.md)], [!INCLUDE[hek_2](../../../includes/hek-2-md.md)] can significantly improve OLTP database application performance. [!INCLUDE[hek_2](../../../includes/hek-2-md.md)] is a memory-optimized database engine integrated into the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] engine, optimized for OLTP.  
  
|||  
|-|-|  
|![Azure Virtual Machine](../../master-data-services/media/azure-virtual-machine.png "Azure Virtual Machine")|Do you want to try out SQL Server 2016? Sign up for Microsoft Azure, and then go **[Here](https://azure.microsoft.com/en-us/marketplace/partners/microsoft/sqlserver2016rtmenterprisewindowsserver2012r2/?wt.mc_id=sqL16_vm)** to spin up a Virtual Machine with  SQL Server 2016 already installed. You can delete the Virtual Machine when you're finished.|  
  
 To use [!INCLUDE[hek_2](../../../includes/hek-2-md.md)], you define a heavily accessed table as memory optimized. Memory-optimized-tables are fully transactional, durable, and are accessed using [!INCLUDE[tsql](../../../includes/tsql-md.md)] in the same way as disk-based tables. A query can reference both memory-optimized tables and disk-based tables. A transaction can update data in memory-optimized tables and disk-based tables. Stored procedures that only reference memory-optimized tables can be natively compiled into machine code for further performance improvements. The [!INCLUDE[hek_2](../../../includes/hek-2-md.md)] engine is designed for extremely high session concurrency for OLTP type of transactions driven from a highly scaled-out middle-tier. To achieve this, it uses latch-free data structures and optimistic, multi-version concurrency control. The result is predictable, sub-millisecond low latency and high throughput with linear scaling for database transactions. The actual performance gain depends on many factors, but 5-to-20 times performance improvements are common.  
  
 The following table summarizes the workload patterns that may benefit most by using [!INCLUDE[hek_2](../../../includes/hek-2-md.md)]:  
  
|Implementation Scenario|Implementation Scenario|Benefits of [!INCLUDE[hek_2](../../../includes/hek-2-md.md)]|  
|-----------------------------|-----------------------------|-------------------------------------|  
|High data insertion rate from multiple concurrent connections.|Primarily append-only store.<br /><br /> Unable to keep up with the insert workload.|Eliminate contention.<br /><br /> Reduce logging.|  
|Read performance and scale with periodic batch inserts and updates.|High performance read operations, especially when each server request has multiple read operations to perform.<br /><br /> Unable to meet scale-up requirements.|Eliminate contention when new data arrives.<br /><br /> Lower latency data retrieval.<br /><br /> Minimize code execution time.|  
|Intensive business logic processing in the database server.|Insert, update, and delete workload.<br /><br /> Intensive computation inside stored procedures.<br /><br /> Read and write contention.|Eliminate contention.<br /><br /> Minimize code execution time for reduced latency and improved throughput.|  
|Low latency.|Require low latency business transactions which typical database solutions cannot achieve.|Eliminate contention.<br /><br /> Minimize code execution time.<br /><br /> Low latency code execution.<br /><br /> Efficient data retrieval.|  
|Session state management.|Frequent insert, update and point lookups.<br /><br /> High scale load from numerous stateless web servers.|Eliminate contention.<br /><br /> Efficient data retrieval.<br /><br /> Optional IO reduction or removal, when using non-durable tables|  
  
 For more information about scenarios where [!INCLUDE[hek_2](../../../includes/hek-2-md.md)] will result in the greatest performance gains, see [In-Memory OLTP - Common Workload Patterns and Migration Considerations](https://msdn.microsoft.com/library/dn673538.aspx).  
  
 [!INCLUDE[hek_2](../../../includes/hek-2-md.md)] will improve performance best in OLTP with short-running transactions.  
  
 Programming patterns that [!INCLUDE[hek_2](../../../includes/hek-2-md.md)] will improve include concurrency scenarios, point lookups, workloads where there are many inserts and updates, and business logic in stored procedures.  
  
 Integration with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] means you can have both memory-optimized tables and disk-based tables in the same database, and query across both types of tables.  
  
 In [!INCLUDE[ssSQL14](../../../includes/sssql14-md.md)] there are limitations in [!INCLUDE[tsql](../../../includes/tsql-md.md)] surface area supported for [!INCLUDE[hek_2](../../../includes/hek-2-md.md)].  
  
 [!INCLUDE[hek_2](../../../includes/hek-2-md.md)] achieves significant performance and scalability gains by using:  
  
-   Algorithms that are optimized for accessing memory-resident data.  
  
-   Optimistic concurrency control that eliminates logical locks.  
  
-   Lock free objects that eliminate all physical locks and latches. Threads that perform transactional work don't use locks or latches for concurrency control.  
  
-   Natively compiled stored procedures, which have significantly better performance than interpreted stored procedures, when accessing a memory-optimized table.  
  
> [!IMPORTANT]  
>  Some syntax changes to tables and stored procedures will be required to use [!INCLUDE[hek_2](../../../includes/hek-2-md.md)]. For more information, see [Migrating to In-Memory OLTP](migrating-to-in-memory-oltp.md). Before you attempt to migrate a disk-based table to a memory-optimized table, read [Determining if a Table or Stored Procedure Should Be Ported to In-Memory OLTP](determining-if-a-table-or-stored-procedure-should-be-ported-to-in-memory-oltp.md) to see which tables and stored procedures will benefit from [!INCLUDE[hek_2](../../../includes/hek-2-md.md)].  
  
## In this section  
 This section provides information about the following concepts:  
  
|Topic|Description|  
|-----------|-----------------|  
|[Requirements for Using Memory-Optimized Tables](memory-optimized-tables.md)|Discusses hardware and software requirements and guidelines for using memory-optimized tables.|  
|[Using In-Memory OLTP in a VM Environment](../../database-engine/using-in-memory-oltp-in-a-vm-environment.md)|Covers using [!INCLUDE[hek_2](../../../includes/hek-2-md.md)] in a virtualized environment.|  
|[In-Memory OLTP Code Samples](in-memory-oltp-code-samples.md)|Contains code samples that show how to create and use a memory-optimized table.|  
|[Memory-Optimized Tables](memory-optimized-tables.md)|Introduces memory-optimized tables.|  
|[Memory-Optimized Table Variables](../../database-engine/memory-optimized-table-variables.md)|Code example showing how to use a memory-optimized table variable instead of a traditional table variable to reduce tempdb use.|  
|[Indexes on Memory-Optimized Tables](../../database-engine/indexes-on-memory-optimized-tables.md)|Introduces memory-optimized indexes.|  
|[Natively Compiled Stored Procedures](natively-compiled-stored-procedures.md)|Introduces natively compiled stored procedures.|  
|[Managing Memory for In-Memory OLTP](../../database-engine/managing-memory-for-in-memory-oltp.md)|Understanding and managing memory usage on your system.|  
|[Creating and Managing Storage for Memory-Optimized Objects](creating-and-managing-storage-for-memory-optimized-objects.md)|Discusses data and delta files, which store information about transactions in memory-optimized tables.|  
|[Backup, Restore, and Recovery of Memory-Optimized Tables](restore-and-recovery-of-memory-optimized-tables.md)|Discusses backup, restore, and recovery for memory-optimized tables.|  
|[Transact-SQL Support for In-Memory OLTP](transact-sql-support-for-in-memory-oltp.md)|Discusses [!INCLUDE[tsql](../../../includes/tsql-md.md)] support for [!INCLUDE[hek_2](../../../includes/hek-2-md.md)].|  
|[High Availability Support for In-Memory OLTP databases](high-availability-support-for-in-memory-oltp-databases.md)|Discusses availability groups and failover clustering in [!INCLUDE[hek_2](../../../includes/hek-2-md.md)].|  
|[SQL Server Support for In-Memory OLTP](sql-server-support-for-in-memory-oltp.md)|Lists new and updated syntax and features supporting memory-optimized tables.|  
|[Migrating to In-Memory OLTP](migrating-to-in-memory-oltp.md)|Discusses how to migrate disk-based tables to memory-optimized tables.|  
  
 More information about [!INCLUDE[hek_2](../../../includes/hek-2-md.md)] is available on:  
  
-   [Microsoft?? SQL Server?? 2014 Product Guide](https://www.microsoft.com/download/confirmation.aspx?id=39269)  
  
-   [In-Memory OLTP Blog](https://go.microsoft.com/fwlink/?LinkId=311696)  
  
-   [In-Memory OLTP - Common Workload Patterns and Migration Considerations](https://msdn.microsoft.com/library/dn673538.aspx)  
  
-   [SQL Server In-Memory OLTP Internals Overview](https://msdn.microsoft.com/library/dn720242.aspx)  
  
## See Also  
 [Database Features](../database-features.md)  
  
  
