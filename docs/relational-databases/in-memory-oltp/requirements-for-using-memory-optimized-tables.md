---
title: "Requirements for Using Memory-Optimized Tables | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "11/16/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine-imoltp"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 47d9a7e8-c597-4b95-a58a-dcf66df8e572
caps.latest.revision: 65
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Requirements for Using Memory-Optimized Tables
[!INCLUDE[tsql-appliesto-ss2014-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2014-xxxx-xxxx-xxx-md.md)]

  For using In-Memory OLTP in Azure DB see [Get started with In-Memory in SQL Database](http://azure.microsoft.com/documentation/articles/sql-database-in-memory/).  
  
 In addition to the [Hardware and Software Requirements for Installing SQL Server 2016](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md), the following are requirements to use In-Memory OLTP:  
  
-   SQL Server 2016 SP1 (or later), any edition. For SQL Server 2014 and SQL Server 2016 RTM (pre-SP1) you need  Enterprise, Developer, or Evaluation edition.
    - Note: In-Memory OLTP requires the 64-bit version of SQL Server.  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] needs enough memory to hold the data in memory-optimized tables and indexes, as well as additional memory to support the online workload. See [Estimate Memory Requirements for Memory-Optimized Tables](../../relational-databases/in-memory-oltp/estimate-memory-requirements-for-memory-optimized-tables.md) for more information.  

-   When running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in a Virtual Machine (VM), ensure there is enough memory allocated to the VM to support the memory needed for memory-optimized tables and indexes. Depending on the VM host application, the configuration option to guarantee memory allocation for the VM could be called Memory Reservation or, when using Dynamic Memory, Minimum RAM. Make sure these settings are sufficient for the needs of the databases in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].
  
-   Free disk space that is two times the size of your durable memory-optimized tables.  
  
-   A processor needs to support the instruction **cmpxchg16b** to use In-Memory OLTP. All modern 64-bit processors support **cmpxchg16b**.  
  
     If you are using a Virtual Machine and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] displays an error caused by an older processor, see if the VM host application has a configuration option to allow **cmpxchg16b**. If not, you could use Hyper-V, which supports **cmpxchg16b** without needing to modify a configuration option.  
  
-   In-Memory OLTP is installed as part of **Database Engine Services**.  
  
     To install report generation ([Determining if a Table or Stored Procedure Should Be Ported to In-Memory OLTP](../../relational-databases/in-memory-oltp/determining-if-a-table-or-stored-procedure-should-be-ported-to-in-memory-oltp.md)) and [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (to manage In-Memory OLTP via [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] Object Explorer), [Download SQL Server Management Studio (SSMS)](https://msdn.microsoft.com/library/mt238290.aspx).  
  
## Important Notes on Using [!INCLUDE[hek_2](../../includes/hek-2-md.md)]  
  
-   Starting SQL Server 2016 there is no limit on the size of memory-optimized tables, other than available memory. In SQL Server 2014 the total in-memory size of all durable tables in a database should not exceed 250 GB for SQL Server 2014 databases. For more information, see [Estimate Memory Requirements for Memory-Optimized Tables](../../relational-databases/in-memory-oltp/estimate-memory-requirements-for-memory-optimized-tables.md).  
    - Note: Starting SQL Server 2016 SP1, Standard and Express Editions support In-Memory OLTP, but they impose quotas on the amount of memory you can use for memory-optimized tables in a given database. In Standard edition this is 32GB per database; in Express edition this is 352MB per database. 
  
-   If you create one or more databases with memory-optimized tables, you should enable Instant File Initialization (grant the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service startup account the SE_MANAGE_VOLUME_NAME user right) for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. Without Instant File Initialization, memory-optimized storage files (data and delta files) will be initialized upon creation, which can have negative impact on the performance of your workload. For more information about Instant File Initialization, see [Database File Initialization](http://msdn.microsoft.com/library/ms175935\(SQL.105\).aspx). For information on how to enable Instant File Initialization, see [How and Why to Enable Instant File Initialization](http://blogs.msdn.com/b/sql_pfe_blog/archive/2009/12/23/how-and-why-to-enable-instant-file-initialization.aspx).  
  
## See Also  
 [In-Memory OLTP &#40;In-Memory Optimization&#41;](../../relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization.md)  
  
  
