---
title: "The Memory Optimized Filegroup | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: 14106cc9-816b-493a-bcb9-fe66a1cd4630
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
---
# The Memory Optimized Filegroup
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  To create memory-optimized tables, you must first create a memory-optimized filegroup. The memory-optimized filegroup holds one or more containers. Each container contains data files or delta files or both.  
  
 Even though data rows from `SCHEMA_ONLY` tables are not persisted and the metadata for memory-optimized tables and natively compiled stored procedures is stored in the traditional catalogs, the [!INCLUDE[hek_2](../../includes/hek-2-md.md)] engine still requires a memory-optimized filegroup for `SCHEMA_ONLY` memory-optimized tables to provide a uniform experience for databases with memory-optimized tables.  
  
 The memory-optimized filegroup is based on filestream filegroup, with the following differences:  
  
-   You can only create one memory-optimized filegroup per database. You need to explicitly mark the filegroup as containing memory_optimized_data. You can create the filegroup when you create the database or you can add it later:  
  
    ```sql  
    ALTER DATABASE imoltp ADD FILEGROUP imoltp_mod CONTAINS MEMORY_OPTIMIZED_DATA  
    ```  
  
-   You need to add one or more containers to the `MEMORY_OPTIMIZED_DATA` filegroup. For example:  
  
    ```sql  
    ALTER DATABASE imoltp ADD FILE (name='imoltp_mod1', filename='c:\data\imoltp_mod1') TO FILEGROUP imoltp_mod  
    ```  
  
-   You do not need to enable filestream ([Enable and Configure FILESTREAM](../../relational-databases/blob/enable-and-configure-filestream.md)) to create a memory-optimized filegroup. The mapping to filestream is done by the [!INCLUDE[hek_2](../../includes/hek-2-md.md)] engine.  
  
-   You can add new containers to a memory-optimized filegroup. You may need a new container to expand the storage needed for durable memory-optimized table and also to distribute IO across multiple containers.  
  
-   Data movement with a memory-optimized filegroup is optimized in an Always On Availability Group configuration. Unlike filestream files that are sent to secondary replicas, the checkpoint files (both data and delta) within the memory-optimized filegroup are not sent to secondary replicas. The data and delta files are constructed using the transaction log on the secondary replica.  
  
The following limitations apply to a memory-optimized filegroup:  
  
-   Once you use a memory-optimized filegroup, you can only remove it by dropping the database. In a production environment, it is unlikely that you will need to remove the memory-optimized filegroup.  
  
-   You cannot drop a non-empty container or move data and delta file pairs to another container in the memory-optimized filegroup.    
  
## Configuring a Memory-Optimized Filegroup  
Consider creating multiple containers in the memory-optimized filegroup and distribute them on different drives to achieve more bandwidth to stream the data into memory. 
 
In a multiple container, multiple drive scenario, data and delta files are allocated in a round-robin fashion into containers. The first data file is allocated from the first container and the delta file is allocated from the next container and this allocation pattern repeats. This allocation scheme distributes data and delta files evenly across containers if you have an odd number of drives, each mapped to one container. However, if you have an even number of drives, each mapped to a container, it can result in imbalanced storage with data files mapped to odd drives and delta files mapped to even drives. To obtain a balanced stream of I/O on recovery, consider placing pairs of data and delta files on the same spindles/storage.
  
When configuring storage, you must provide free disk space that is four times the size of durable memory-optimized tables. Also ensure that your I/O subsystem supports the required IOPS for your workload. If data and delta file pairs are populated at a given IOPS, you need three times that IOPS to account for storing and merge operations. You can add storage capacity and IOPS by adding one or more containers to the memory-optimized filegroup.  
 
> [!CAUTION]
> If a `MAXSIZE` value is set for the memory-optimized filegroup, and checkpoint files exceed the max size of the container, then the database will become SUSPECT.   
> In this case do not attempt to set the database OFFLINE and ONLINE, causing the database to stay in RECOVERY_PENDING state.
  
## See Also  
[Creating and Managing Storage for Memory-Optimized Objects](../../relational-databases/in-memory-oltp/creating-and-managing-storage-for-memory-optimized-objects.md)  
[Database Files and Filegroups](../../relational-databases/databases/database-files-and-filegroups.md)    
[ALTER DATABASE File and Filegroup Options (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql-file-and-filegroup-options.md) 

