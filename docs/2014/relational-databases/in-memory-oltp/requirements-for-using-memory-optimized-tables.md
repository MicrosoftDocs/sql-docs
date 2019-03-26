---
title: "Requirements for Using Memory-Optimized Tables | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: 47d9a7e8-c597-4b95-a58a-dcf66df8e572
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# Requirements for Using Memory-Optimized Tables
  In addition to the [Hardware and Software Requirements for Installing SQL Server 2014](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md), the following are requirements to use In-Memory OLTP:  
  
-   64-bit Enterprise, Developer, or Evaluation edition of [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)].  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] needs enough memory to hold the data in memory-optimized tables and indexes. To account for row versions, you should provide an amount of memory that is two times the expected size of memory-optimized tables and indexes. But the actual amount of memory needed will depend on your workload. You should monitor your memory usage and make adjustments as needed. The size of data in memory-optimized tables must not exceed the allowed percentage of the pool. To discover the size of a memory-optimized table, see [sys.dm_db_xtp_table_memory_stats &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-db-xtp-table-memory-stats-transact-sql).  
  
     If you have disk-based tables in the database, you need to provide enough memory for the buffer pool and query processing on those tables.  
  
     It is important to know how much memory your In-Memory OLTP application will require. See [Estimate Memory Requirements for Memory-Optimized Tables](memory-optimized-tables.md) for more information.  
  
-   Free disk space for that is two times the size of your durable memory-optimized tables.  
  
-   A processor needs to support the instruction **cmpxchg16b** to use In-Memory OLTP. All modern 64-bit processors support **cmpxchg16b**.  
  
     If you are using a VM host application and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] displays an error caused by an older processor, see if the application has a configuration option to allow **cmpxchg16b**. If not, you could use Hyper-V, which supports **cmpxchg16b** without needing to modify a configuration option.  
  
-   To install In-Memory OLTP, select **Database Engine Services** when you install [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)].  
  
     To install report generation ([Determining if a Table or Stored Procedure Should Be Ported to In-Memory OLTP](determining-if-a-table-or-stored-procedure-should-be-ported-to-in-memory-oltp.md)) and [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] (to manage In-Memory OLTP via [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] Object Explorer), select **Management Tools-Basic** or **Management Tools-Advanced** when you install [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)].  
  
## Important Notes on Using [!INCLUDE[hek_2](../../../includes/hek-2-md.md)]  
  
-   The total in-memory size of all durable tables in a database should not exceed 250 GB. For more information, see [Durability for Memory-Optimized Tables](durability-for-memory-optimized-tables.md).  
  
-   This release of [!INCLUDE[hek_2](../../../includes/hek-2-md.md)] is targeted to perform optimally on systems with 2 or 4 sockets and fewer than 60 cores.  
  
-   Checkpoint files must not be manually deleted. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] automatically performs garbage collection on unneeded checkpoint files. For more information, see the discussion on merging data and delta files in [Durability for Memory-Optimized Tables](durability-for-memory-optimized-tables.md).  
  
-   In this first release of In-Memory OLTP (in [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]), the only way to remove a memory-optimized filegroup is to drop the database.  
  
-   If you attempt to delete a large batch of rows while there is a concurrent insert or update workload affecting the range of rows you are trying to delete, the delete will likely fail. The workaround is to stop the insert or update workload before doing the delete. Alternatively, you could configure the transaction into smaller transactions, which would be less likely to be disrupted by a concurrent workload. As with all write operations on memory-optimized tables, use retry logic ([Guidelines for Retry Logic for Transactions on Memory-Optimized Tables](../../database-engine/guidelines-for-retry-logic-for-transactions-on-memory-optimized-tables.md)).  
  
-   If you create one or more databases with memory-optimized tables, you should enable Instant File Initialization (grant the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service startup account the SE_MANAGE_VOLUME_NAME user right) for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. Without Instant File Initialization, memory-optimized storage files (data and delta files) will be initialized upon creation, which can have negative impact on the performance of your workload. For more information about Instant File Initialization, see [Database File Initialization](../databases/database-instant-file-initialization.md). For information on how to enable Instant File Initialization, see [How and Why to Enable Instant File Initialization](https://blogs.msdn.com/b/sql_pfe_blog/archive/2009/12/23/how-and-why-to-enable-instant-file-initialization.aspx).  
  
## Did this Article Help You? We're Listening  
 What information are you looking for, and did you find it? We're listening to your feedback to improve the content. Please submit your comments to [sqlfeedback@microsoft.com](mailto:sqlfeedback@microsoft.com?subject=Your%20feedback%20about%20the%20Requirements%20for%20Using%20Memory-Optimized%20Tables%20page).  
  
## See Also  
 [In-Memory OLTP &#40;In-Memory Optimization&#41;](in-memory-oltp-in-memory-optimization.md)  
  
  
