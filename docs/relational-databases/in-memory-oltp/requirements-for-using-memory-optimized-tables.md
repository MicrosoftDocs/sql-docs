---
title: "Requirements for Using Memory-Optimized Tables | Microsoft Docs"
ms.custom: ""
ms.date: "11/24/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: 47d9a7e8-c597-4b95-a58a-dcf66df8e572
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
---
# Requirements for Using Memory-Optimized Tables
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  For using In-Memory OLTP in Azure DB see [Get started with In-Memory in SQL Database](https://azure.microsoft.com/documentation/articles/sql-database-in-memory/).  
  
 In addition to the [Hardware and Software Requirements for Installing SQL Server](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md), the following are requirements to use In-Memory OLTP:  
  
-   [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP1 (or later), any edition. For [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] RTM (pre-SP1) you need Enterprise, Developer, or Evaluation edition.
    
    > [!NOTE]
    > In-Memory OLTP requires the 64-bit version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] needs enough memory to hold the data in memory-optimized tables and indexes, as well as additional memory to support the online workload. See [Estimate Memory Requirements for Memory-Optimized Tables](../../relational-databases/in-memory-oltp/estimate-memory-requirements-for-memory-optimized-tables.md) for more information.  

-   When running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in a Virtual Machine (VM), ensure there is enough memory allocated to the VM to support the memory needed for memory-optimized tables and indexes. Depending on the VM host application, the configuration option to guarantee memory allocation for the VM could be called Memory Reservation or, when using Dynamic Memory, Minimum RAM. Make sure these settings are sufficient for the needs of the databases in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].
  
-   Free disk space that is two times the size of your durable memory-optimized tables.  
  
-   A processor needs to support the instruction **cmpxchg16b** to use In-Memory OLTP. All modern 64-bit processors support **cmpxchg16b**.  
  
     If you are using a Virtual Machine and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] displays an error caused by an older processor, see if the VM host application has a configuration option to allow **cmpxchg16b**. If not, you could use Hyper-V, which supports **cmpxchg16b** without needing to modify a configuration option.  
  
-   In-Memory OLTP is installed as part of **Database Engine Services**.  
  
     To install report generation ([Determining if a Table or Stored Procedure Should Be Ported to In-Memory OLTP](../../relational-databases/in-memory-oltp/determining-if-a-table-or-stored-procedure-should-be-ported-to-in-memory-oltp.md)) and [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (to manage In-Memory OLTP via [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] Object Explorer), [download SQL Server Management Studio (SSMS)](../../ssms/download-sql-server-management-studio-ssms.md).   
  
## Important Notes on using [!INCLUDE[hek_2](../../includes/hek-2-md.md)]  
  
-   Starting [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)], there is no limit on the size of memory-optimized tables, other than available memory. 

-   In [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)], the total in-memory size of all durable tables in a database should not exceed 250 GB. For more information, see [Estimate Memory Requirements for Memory-Optimized Tables](../../relational-databases/in-memory-oltp/estimate-memory-requirements-for-memory-optimized-tables.md).  

> [!NOTE]
> Starting [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP1, Standard and Express Editions support In-Memory OLTP, but they impose quotas on the amount of memory you can use for memory-optimized tables in a given database. In Standard edition this is 32GB per database; in Express edition this is 352MB per database. 
  
-   If you create one or more databases with memory-optimized tables, you should enable Instant File Initialization (IFI) by granting the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service startup account the *SE_MANAGE_VOLUME_NAME* user right. Without IFI, memory-optimized storage files (data and delta files) will be initialized upon creation, which can have negative impact on the performance of your workload. For more information about IFI, including how to enable it, see [Database Instant File Initialization](../../relational-databases/databases/database-instant-file-initialization.md).
  
## See Also  
 [In-Memory OLTP &#40;In-Memory Optimization&#41;](../../relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization.md)  
 [Database Instant File Initialization](../../relational-databases/databases/database-instant-file-initialization.md)  
 [Memory Architecture guide](../../relational-databases/memory-management-architecture-guide.md)
  
