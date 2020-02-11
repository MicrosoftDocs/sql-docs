---
title: "Creating and Managing Storage for Memory-Optimized Objects | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: 622aabe6-95c7-42cc-8768-ac2e679c5089
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# Creating and Managing Storage for Memory-Optimized Objects
  The [!INCLUDE[hek_2](../../includes/hek-2-md.md)] engine is integrated into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], which lets you have both memory-optimized tables and (traditional) disk-based tables in the same database. However, the storage structure for memory-optimized tables is different from disk-based tables.  
  
 Storage for disk-based table has following key attributes:  
  
-   Mapped to a filegroup and the filegroup contains one or more files.  
  
-   Each file is divided into extents of 8 pages and each page is of size 8K bytes.  
  
-   An extent can be shared across multiple tables, but a there is 1-to-1 mapping between an allocated page and the table or index. In other words, a page can't have rows from two or more tables or index.  
  
-   The data is moved into memory (the buffer pool) as needed and the modified or newly created pages are asynchronously written to the disk generating mostly random IO.  
  
 Storage for memory-optimized tables has following key attributes:  
  
-   All memory-optimized tables are mapped to a memory-optimized filegroup. This filegroup is built using the filestream filegroup.  
  
-   There are no pages and the data is persisted as a row.  
  
-   All changes to memory-optimized tables are stored by appending to active files. Both reading and writing to files is sequential.  
  
-   An update is implemented as a delete followed by an insert. The deleted rows are not immediately removed from the storage. The deleted rows are removed by a background process, called MERGE, based on a policy as described in [Durability for Memory-Optimized Tables](memory-optimized-tables.md).  
  
-   Unlike disk-based tables, storage for memory-optimized tables is not compressed. When migrating a compressed (ROW or PAGE) disk-based table to memory-optimized table, you will need to account for the change in size.  
  
-   A memory-optimized table can be durable or can be non-durable. You only need to configure storage for durable memory-optimize tables.  
  
 This section describes checkpoint file pairs and other aspects of how data in memory-optimized tables is stored.  
  
 Topics in this section:  
  
-   [Configuring Storage for Memory-Optimized Tables](configuring-storage-for-memory-optimized-tables.md)  
  
-   [The Memory Optimized Filegroup](the-memory-optimized-filegroup.md)  
  
-   [Durability for Memory-Optimized Tables](memory-optimized-tables.md)  
  
-   [Checkpoint Operation for Memory-Optimized Tables](checkpoint-operation-for-memory-optimized-tables.md)  
  
-   [Defining Durability for Memory-Optimized Objects](defining-durability-for-memory-optimized-objects.md)  
  
-   [Comparing Disk-Based Table Storage to Memory-Optimized Table Storage](comparing-disk-based-table-storage-to-memory-optimized-table-storage.md)  
  
-   [Monitoring and Troubleshooting Merge for Data and Delta File Pairs](../../database-engine/monitoring-and-troubleshooting-merge-for-data-and-delta-file-pairs.md)  
  
## See Also  
 [In-Memory OLTP &#40;In-Memory Optimization&#41;](in-memory-oltp-in-memory-optimization.md)  
  
  
