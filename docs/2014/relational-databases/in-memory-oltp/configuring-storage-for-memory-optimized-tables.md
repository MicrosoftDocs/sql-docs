---
title: "Configuring Storage for Memory-Optimized Tables | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: 6e005de0-3a77-4b91-b497-14cc0f9f6605
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# Configuring Storage for Memory-Optimized Tables
  You need to configure storage capacity and input/output operations per second (IOPS).  
  
## Storage Capacity  
 Use the information in [Estimate Memory Requirements for Memory-Optimized Tables](memory-optimized-tables.md) to estimate the in-memory size of the database's durable memory-optimized tables. Because indexes are not persisted for memory-optimized tables, do not include the size of indexes. Once you determine the size, you need to provide disk space that is four times the size of durable, in-memory tables.  
  
## Storage Performance  
 [!INCLUDE[hek_2](../../includes/hek-2-md.md)] can significantly increase your workload throughput. Therefore, it is important to ensure that IO is not a bottleneck.  
  
-   When migrating disk-based tables to memory-optimized tables, make sure that the transaction log is on a storage media that can support increased transaction log activity. For example, if your storage media supports transaction log operations at 100 MB/sec, and memory-optimized tables result in five times greater performance, the transaction log's storage media must be able to also support five times performance improvement, to prevent the transaction log activity from becoming a performance bottleneck.  
  
-   Memory-optimized tables are persisted in files distributed across one or more containers. Each container should typically be mapped to its own spindle and is used both for increased storage capacity and improved performance. You need to ensure that sequential IOPS of the storage media can support a 3 times increase in transaction log throughput.  
  
     For example, if memory-optimized tables generate 500MB/sec of activity in the transaction log, the storage for memory-optimized tables must support 1.5GB/sec. The need to support a 3 times increase in transaction log throughput comes from the observation that the data and delta file pairs are first written with the initial data and then need to be read/re-written as part of a merge operation.  
  
     Another factor in estimating throughput for storage is the recovery time for memory-optimized tables. Data from durable tables must be read into memory before a database is made available to applications. Commonly, loading data into memory-optimized tables can be done at the speed of IOPS. So if the total storage for durable, memory-optimized tables is 60 GB and you want to be able to load this data in 1 minute, the IOPS for the storage must be set at 1 GB/sec.  
  
-   If you have even number of spindles, you should create two times the number of containers, each pair mapped to the same spindle. This is needed to spread the IOPS and the storage. For more information, see [The Memory Optimized Filegroup](the-memory-optimized-filegroup.md).  
  
## See Also  
 [Creating and Managing Storage for Memory-Optimized Objects](creating-and-managing-storage-for-memory-optimized-objects.md)  
  
  
