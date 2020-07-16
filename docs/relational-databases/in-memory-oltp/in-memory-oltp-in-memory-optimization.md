---
title: "In-Memory OLTP (In-Memory Optimization) | Microsoft Docs"
description: Use these samples and resources for In-Memory OLTP, which can significantly improve performance in SQL Server.
ms.custom: ""
ms.date: "11/21/2019"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
helpviewer_keywords: 
  - "In-Memory OLTP"
  - "memory-optimized tables"
ms.assetid: e1d03d74-2572-4a55-afd6-7edf0bc28bdb
author: MightyPen
ms.author: genemi
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# In-Memory OLTP and Memory-Optimization

[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb.md)]

 [!INCLUDE[hek_2](../../includes/hek-2-md.md)] can significantly improve the performance of transaction processing, data ingestion and data load, and transient data scenarios.  To jump into the basic code and knowledge you need to quickly test your own memory-optimized table and natively compiled stored procedure, see
 -  [Quick Start 1: In-Memory OLTP Technologies for Faster Transact-SQL Performance](../../relational-databases/in-memory-oltp/survey-of-initial-areas-in-in-memory-oltp.md).  
 
We have uploaded to YouTube a [**17-minute video**](#anchorname-17minute-video) explaining In-Memory OLTP on SQL Server, and demonstrating the performance benefits.

For a more detailed overview of In-Memory OLTP and a review of scenarios that see performance benefits from the technology:

- [Overview and Usage Scenarios](../../relational-databases/in-memory-oltp/overview-and-usage-scenarios.md)
 
 Note that [!INCLUDE[hek_2](../../includes/hek-2-md.md)] is the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] technology for improving performance of transaction processing. For the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] technology that improves reporting and analytical query performance see [Columnstore Indexes Guide](../../relational-databases/indexes/columnstore-indexes-overview.md).
  
 Several improvements have been made to In-Memory OLTP in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] and [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)], as well as in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]. The Transact-SQL surface area has been increased to make it easier to migrate database applications. Support for performing ALTER operations for memory-optimized tables and natively compiled stored procedures has been added, to make it easier to maintain applications.
  
> [!NOTE]  
>  **Try it out**  
>   
>  In-Memory OLTP is available in Premium and Business Critical tier Azure SQL databases and elastic pools. To get started with In-Memory OLTP, as well as Columnstore in Azure SQL Database, see [Optimize Performance using In-Memory Technologies in SQL Database](https://azure.microsoft.com/documentation/articles/sql-database-in-memory/).  
  

## In this section  
 This section provides includes the following topics:  
  
|Topic|Description|  
|-----------|-----------------|  
|[Quick Start 1: In-Memory OLTP Technologies for Faster Transact-SQL Performance](../../relational-databases/in-memory-oltp/survey-of-initial-areas-in-in-memory-oltp.md)|Delve right into In-Memory OLTP|
|[Overview and Usage Scenarios](../../relational-databases/in-memory-oltp/overview-and-usage-scenarios.md)|Overview of what In-Memory OLTP is, and what are the scenarios that see performance benefits.|
|[Requirements for Using Memory-Optimized Tables](../../relational-databases/in-memory-oltp/requirements-for-using-memory-optimized-tables.md)|Discusses hardware and software requirements and guidelines for using memory-optimized tables.|  
|[In-Memory OLTP Code Samples](../../relational-databases/in-memory-oltp/in-memory-oltp-code-samples.md)|Contains code samples that show how to create and use a memory-optimized table.|  
|[Memory-Optimized Tables](../../relational-databases/in-memory-oltp/memory-optimized-tables.md)|Introduces memory-optimized tables.|  
|[Memory-Optimized Table Variables](https://msdn.microsoft.com/library/bd102e95-53e2-4da6-9b8b-0e4f02d286d3)|Code example showing how to use a memory-optimized table variable instead of a traditional table variable to reduce tempdb use.|  
|[Indexes on Memory-Optimized Tables](https://msdn.microsoft.com/library/86805eeb-6972-45d8-8369-16ededc535c7)|Introduces memory-optimized indexes.|  
|[Natively Compiled Stored Procedures](../../relational-databases/in-memory-oltp/natively-compiled-stored-procedures.md)|Introduces natively compiled stored procedures.|  
|[Managing Memory for In-Memory OLTP](https://msdn.microsoft.com/library/d82f21fa-6be1-4723-a72e-f2526fafd1b6)|Understanding and managing memory usage on your system.|  
|[Creating and Managing Storage for Memory-Optimized Objects](../../relational-databases/in-memory-oltp/creating-and-managing-storage-for-memory-optimized-objects.md)|Discusses data and delta files, which store information about transactions in memory-optimized tables.|  
|[Backup, Restore, and Recovery of Memory-Optimized Tables](https://msdn.microsoft.com/library/3f083347-0fbb-4b19-a6fb-1818d545e281)|Discusses backup, restore, and recovery for memory-optimized tables.|  
|[Transact-SQL Support for In-Memory OLTP](../../relational-databases/in-memory-oltp/transact-sql-support-for-in-memory-oltp.md)|Discusses [!INCLUDE[tsql](../../includes/tsql-md.md)] support for [!INCLUDE[hek_2](../../includes/hek-2-md.md)].|  
|[High Availability Support for In-Memory OLTP databases](../../relational-databases/in-memory-oltp/high-availability-support-for-in-memory-oltp-databases.md)|Discusses availability groups and failover clustering in [!INCLUDE[hek_2](../../includes/hek-2-md.md)].|  
|[SQL Server Support for In-Memory OLTP](../../relational-databases/in-memory-oltp/sql-server-support-for-in-memory-oltp.md)|Lists new and updated syntax and features supporting memory-optimized tables.|  
|[Migrating to In-Memory OLTP](../../relational-databases/in-memory-oltp/migrating-to-in-memory-oltp.md)|Discusses how to migrate disk-based tables to memory-optimized tables.|  
| &nbsp; | &nbsp; |

## Links to other websites

This section provides links to other websites that contain information about In-Memory OLTP on SQL Server.

- [**Video** explaining In-Memory OLTP, and demonstrating the performance benefits](#anchorname-17minute-video)

- [In-Memory OLTP Performance Demo v1.0](https://github.com/Microsoft/sql-server-samples/releases/tag/in-memory-oltp-demo-v1.0)

-   [SQL Server In-Memory OLTP Internals Technical Whitepaper](https://msdn.microsoft.com/library/mt764316.aspx)  

-   [SQL Server In-Memory OLTP and Columnstore Feature Comparison](https://download.microsoft.com/download/D/0/0/D0075580-6D72-403D-8B4D-C3BD88D58CE4/SQL_Server_2016_In_Memory_OLTP_and_Columnstore_Comparison_White_Paper.pdf)

-   What's new for In-Memory OLTP in SQL Server 2016, [Part 1](https://blogs.msdn.microsoft.com/sqlserverstorageengine/2015/11/12/in-memory-oltp-whats-new-in-sql2016-ctp3/) and [Part 2](https://blogs.msdn.microsoft.com/sqlserverstorageengine/2016/03/25/whats-new-for-in-memory-oltp-in-sql-server-2016-since-ctp3/)
  
-   [In-Memory OLTP - Common Workload Patterns and Migration Considerations](https://msdn.microsoft.com/library/dn673538.aspx)  
  
-   [In-Memory OLTP Blog](https://cloudblogs.microsoft.com/sqlserver/2013/06/26/sql-server-2014-in-memory-technologies-blog-series-introduction/)  

## <a name="anchorname-17minute-video"></a>17 minute video, indexed

- _Video title:_ &nbsp; **In-Memory OLTP in SQL Server 2016**
- _Published date:_ &nbsp; 2019-03-10, on `YouTube.com`.
- _Duration:_ &nbsp; 17:32 &nbsp; &nbsp; (See the following [**Index**](#anchorname-index-17minute-video) for links into the video.)
- _Hosted by:_ &nbsp; Jos de Bruijn, Senior Program Manager on SQL Server

### Demo can be downloaded

At the time mark 08:09, the video runs a demonstration twice. You can download the source code for runnable performance demo that is used in the video, from the following link:

- [In-Memory OLTP Performance Demo v1.0, source code](https://github.com/Microsoft/sql-server-samples/releases/tag/in-memory-oltp-demo-v1.0)

The general steps seen in the video are as follows:

1. First the demo is run with a regular table.
2. Next we see a memory-optimized edition of the table being created and populated by a few clicks in SQL Server Management Studio (SSMS.exe).
3. Then the demo is rerun with the memory-optimized table. An enormous speed improvement is measured.

### <a name="anchorname-index-17minute-video"></a>Index to each section in the video

| Time mark link | Section title |
| :------------- | :------------ |
| A.&nbsp; [00:00](https://www.youtube.com/watch?v=l5l5eophmK4&t=0) | The beginning. |
| <br/>B.&nbsp; [00:56](https://www.youtube.com/watch?v=l5l5eophmK4&t=56) | <br/>Why customers should care about In-Memory OLTP. |
| &nbsp; &nbsp; [01:03](https://www.youtube.com/watch?v=l5l5eophmK4&t=63) | Modern hardware requires modern architecture of database system. |
| &nbsp; &nbsp; [02:10](https://www.youtube.com/watch?v=l5l5eophmK4&t=130) | Explosion in data being generated; operations need to be instant (low latency). |
| &nbsp; &nbsp; [03:19](https://www.youtube.com/watch?v=l5l5eophmK4&t=199) | Reduce TCO - do more with the resources you have. |
| <br/>C.&nbsp; [03:33](https://www.youtube.com/watch?v=l5l5eophmK4&t=213) | <br/>What In-Memory OLTP is.<br/>Performance optimized using memory-optimized technology. |
| &nbsp; &nbsp; [05:03](https://www.youtube.com/watch?v=l5l5eophmK4&t=303) | Up to 30X faster transaction processing. |
| &nbsp; &nbsp; [05:22](https://www.youtube.com/watch?v=l5l5eophmK4&t=322) | Fully durable - data survives server failures. |
| &nbsp; &nbsp; [06:15](https://www.youtube.com/watch?v=l5l5eophmK4&t=375) | Fully integrated in SQL Server. Thus no new languages or tools to learn. |
| &nbsp; &nbsp; [07:22](https://www.youtube.com/watch?v=l5l5eophmK4&t=442) | First released in SQL Server 2014, but major improvements in 2016. |
| &nbsp; &nbsp; [07:58](https://www.youtube.com/watch?v=l5l5eophmK4&t=558) | Available in Azure SQL Database too (in the cloud). |
| <br/>D.&nbsp; [08:09](https://www.youtube.com/watch?v=l5l5eophmK4&t=489) | <br/>Performance demonstration.<br/> Run the demo with a regular table. |
| &nbsp; &nbsp; [09:11](https://www.youtube.com/watch?v=l5l5eophmK4&t=551) | SSMS context menu: **Reports** &gt; **Transaction Performance Analysis** |
| &nbsp; &nbsp; [10:38](https://www.youtube.com/watch?v=l5l5eophmK4&t=638) | SSMS context menu: **Memory Optimization Advisor**<br/> &nbsp; &nbsp; Actually create a memory-optimized table from a regular table, plus migrate the data. |
| &nbsp; &nbsp; [11:28](https://www.youtube.com/watch?v=l5l5eophmK4&t=688) | Rerun the demo, see 45X speed improvement. |
| <br/>E.&nbsp; [12:17](https://www.youtube.com/watch?v=l5l5eophmK4&t=737) | <br/>Easier to use In-Memory OLTP in SQL Server 2016 (compared to 2014). |
| &nbsp; &nbsp; [12:43](https://www.youtube.com/watch?v=l5l5eophmK4&t=763) | Simplified analysis to help with app migration. |
| &nbsp; &nbsp; [13:03](https://www.youtube.com/watch?v=l5l5eophmK4&t=783) | Reduced complexity of app migration through increased Transact-SQL language support (for example, with foreign keys and triggers). |
| &nbsp; &nbsp; [13:56](https://www.youtube.com/watch?v=l5l5eophmK4&t=836) | Improved manageability.<br/> &nbsp; &nbsp; For example, change schema and indexes, auto-update of statistics. |
| <br/>F.&nbsp; [14:46](https://www.youtube.com/watch?v=l5l5eophmK4&t=886) | <br/>Improved scalability. |
| &nbsp; &nbsp; [15:12](https://www.youtube.com/watch?v=l5l5eophmK4&t=912) | Large memory-optimized tables (up to 2TB per database). |
| &nbsp; &nbsp; [15:34](https://www.youtube.com/watch?v=l5l5eophmK4&t=934) | Even better scaling. |
| &nbsp; &nbsp; [16:41](https://www.youtube.com/watch?v=l5l5eophmK4&t=1001) | Do more with the resources you already have! |
| <br/>G.&nbsp; [16:53](https://www.youtube.com/watch?v=l5l5eophmK4&t=1013) | <br/>Final comments. (Ends at 17:32.) |
| &nbsp; | &nbsp; |

## See also  
 [Database Features](../../relational-databases/database-features.md)  
  
  
