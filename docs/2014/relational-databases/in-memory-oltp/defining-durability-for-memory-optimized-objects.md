---
title: "Defining Durability for Memory-Optimized Objects | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: 0fe85fbf-8e8d-4983-96fd-d04b3c7d6d65
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# Defining Durability for Memory-Optimized Objects
  In-Memory OLTP guarantees full atomicity, consistency, isolation, and full durability (ACID) properties. Durability in the context of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and memory-optimized tables provides following guarantees:  
  
 Transactional Durability  
 When you commit a fully durable transaction that made (DDL or DML) changes to a memory-optimized table, the changes made to a durable memory-optimized table are permanent.  
  
 When you commit a delayed durable transaction to a memory-optimized table, the transaction becomes durable only after the in-memory transaction log is saved to disk.  
  
 Restart Durability  
 When [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] restarts after a crash or planned shutdown, the memory-optimized durable tables are reinstantiated to restore them to the state before the shutdown or crash.  
  
 Media Failure Durability  
 If a failed or corrupt disk contains one or more persisted copies of durable memory-optimized objects, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backup and restore feature restores memory-optimized tables on the new media.  
  
 There are two durability options for memory-optimized tables:  
  
 SCHEMA_ONLY (non-durable table)  
 This option ensures durability of the table schema, including indexes. When [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is restarted, the non-durable table is recreated, but starts with no data. (This is unlike a table in tempdb, where both the table and its data are lost upon restart.) A typical scenario for creating a non-durable table is to store transient data, such as a staging table for an ETL process. A SCHEMA_ONLY durability avoids both transaction logging and checkpoint, which can significantly reduce I/O operations.  
  
 SCHEMA_AND_DATA (durable table)  
 This option provides durability of both schema and data. The level of data durability depends on whether you commit a transaction as fully durable or with delayed durability. Fully durable transactions provide the same durability guarantee for data and schema, similar to a disk-based table. Delayed durability will improve performance but can potentially result in data loss in case of a server crash or fail over. (For more information about delayed durability, see [Control Transaction Durability](../logs/control-transaction-durability.md).)  
  
 The schema of the memory-optimized table is persisted, similar to disk-based tables, in the primary file group of a database.  
  
 Data in durable memory-optimized tables is saved in data and delta file pairs.  
  
 The indexes defined in memory-optimized tables persist only in metadata, not in storage. Index structures are generated as part of loading memory-optimized tables.  
  
 Rows are deleted either explicitly by a DELETE statement or indirectly by an UPDATE statement. An UPDATE operation is executed as a delete followed by an insert. When a row is deleted, no change is made to a data file but a new row, identifying the deleted row, is appended to the corresponding delta file.  
  
 During recovery or restore operations, the In-Memory OLTP engine reads data and delta files for loading into physical memory. The load time is determined by:  
  
-   The amount of data to load.  
  
-   Sequential I/O bandwidth.  
  
-   Degree of parallelism, determined by number of file containers and processor cores.  
  
-   The amount of log records in the active portion of the log that need to be redone.  
  
## See Also  
 [Creating and Managing Storage for Memory-Optimized Objects](creating-and-managing-storage-for-memory-optimized-objects.md)  
  
  
