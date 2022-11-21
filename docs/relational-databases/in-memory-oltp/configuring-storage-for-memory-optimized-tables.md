---
title: "Configuring Storage for Memory-Optimized Tables"
description: Learn how to configure storage capacity and input/output operations per second (IOPS) for memory-optimized tables in SQL Server.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "1/15/2020"
ms.service: sql
ms.subservice: in-memory-oltp
ms.topic: conceptual
ms.assetid: 6e005de0-3a77-4b91-b497-14cc0f9f6605
---
# Configuring Storage for Memory-Optimized Tables
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  You need to configure storage capacity and input/output operations per second (IOPS).  
  
## Storage Capacity  

Use the information in [Estimate Memory Requirements for Memory-Optimized Tables](../../relational-databases/in-memory-oltp/estimate-memory-requirements-for-memory-optimized-tables.md) to estimate the in-memory size of the database's durable memory-optimized tables. Because indexes aren't persisted for memory-optimized tables, don't include the size of indexes. 
 
After you determine the size, you must provide disk space sufficient to hold the checkpoint files, which are used to store newly changed data. The data that's stored contains not only the contents of new rows that are added to the in-memory tables, but also new versions of existing rows. This storage grows when rows are inserted or updated. Row versions are merged and storage is reclaimed when log truncation occurs. If log truncation is delayed for any reason, the in-memory OLTP store will grow.

A good starting point for sizing storage for this area is to reserve four times the size of durable, in-memory tables. Monitor the space usage and be prepared to expand the storage that's available to it if necessary.
  
## Storage IOPS  
 [!INCLUDE[inmemory](../../includes/inmemory-md.md)] can significantly increase your workload throughput. Therefore, it is important to ensure that IO is not a bottleneck.  
  
-   When migrating disk-based tables to memory-optimized tables, make sure that the transaction log is on a storage media that can support increased transaction log activity. For example, if your storage media supports transaction log operations at 100 MB/sec, and memory-optimized tables result in five times greater performance, the transaction log's storage media must be able to also support five times performance improvement, to prevent the transaction log activity from becoming a performance bottleneck.  
  
-   Memory-optimized tables are persisted in checkpoint files, which are distributed across one or more containers. Each container should typically be mapped to its own storage device and is used both for increased storage capacity and improved IOPS. You need to ensure that the sequential IOPS of the storage media can support up to 3 times the sustained transaction log throughput. Writes to checkpoint files are 256 KB for data files and 4 KB for delta files.
  
     - For example, if memory-optimized tables generate a sustained 500 MB/sec of activity in the transaction log, the storage for memory-optimized tables must support 1.5 GB/sec IOPS. The need to support 3 times the sustained transaction log throughput comes from the observation that the data and delta file pairs are first written with the initial data and then need to be read/re-written as part of a merge operation.  
  
- Another factor in estimating the IOPS for storage is the recovery time for memory-optimized tables. Data from durable tables must be read into memory before a database is made available to applications. Commonly, loading data into memory-optimized tables can be done at the speed of IOPS. So if the total storage for durable, memory-optimized tables is 60 GB and you want to be able to load this data in 1 minute, the IOPS for the storage must be set at 1 GB/sec.  
  
-   Checkpoint files are usually distributed uniformly across all containers, space permitting. With SQL Server 2014 you need to provision an odd number of  containers in order to achieve a uniform distribution - starting 2016, both odd and even numbers of containers lead to a uniform distribution.
  
## Encryption  
 In [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later versions, storage for memory-optimized tables will be encrypted at rest as part of enabling Transparent Data Encryption (TDE) on the database. For more information, see [Transparent Data Encryption](../../relational-databases/security/encryption/transparent-data-encryption.md). In [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] checkpoint files are not encrypted even if TDE is enabled on the database.

 Data in [non-durable](../../relational-databases/in-memory-oltp/defining-durability-for-memory-optimized-objects.md) (SCHEMA_ONLY) memory-optimized tables is not written to disk at any time. Therefore, TDE does not apply to such tables.
  
## See Also  
 [Creating and Managing Storage for Memory-Optimized Objects](../../relational-databases/in-memory-oltp/creating-and-managing-storage-for-memory-optimized-objects.md)  
  
  
