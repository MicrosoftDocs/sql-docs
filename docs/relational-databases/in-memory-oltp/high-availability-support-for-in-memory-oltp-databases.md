---
title: "High availability - in-memory OLTP databases"
description: SQL Server Databases with memory-optimized tables, with or without native compiled stored procedures, are fully supported with Always On Availability Groups.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "08/31/2016"
ms.service: sql
ms.subservice: in-memory-oltp
ms.topic: conceptual
ms.custom: seo-dt-2019
ms.assetid: 2113a916-3b1e-496c-8650-7f495e492510
---
# High Availability Support for In-Memory OLTP databases
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Databases containing memory-optimized tables, with or without native compiled stored procedures, are fully supported with Always On Availability Groups.  There's no difference in the configuration and support for databases that contain [!INCLUDE[inmemory](../../includes/inmemory-md.md)] objects as compared to those without.  

 Changes to memory-optimized tables on the primary replica are applied to the tables on the secondary replica during redo. This allows for rapid failover to the secondary replica since the data is already in memory. Tables are available for read queries on the secondary for replicas that have been configured for read access.  

  
## Always On Availability Groups and In-Memory OLTP Databases  
 Configuring databases with [!INCLUDE[inmemory](../../includes/inmemory-md.md)] components provides the following benefits:  
  
-   **A fully integrated experience**   
    You can configure your databases containing memory-optimized tables using the same wizard with the same level of support for both synchronous and asynchronous secondary replicas. Additionally, health monitoring is provided using the familiar Always On dashboard in SQL Server Management Studio.  
  
-   **Comparable Failover time**   
    Secondary replicas maintain the in-memory state of the durable memory-optimized tables. In the event of automatic or forced failover, the time to fail over to the new primary is comparable to disk-bases tables as no recovery is needed. Memory-optimized tables created as SCHEMA_ONLY are supported in this configuration. However, changes to these tables aren't logged so no data will exist in these tables on the secondary replica.  
  
-   **Readable Secondary**   
    You can access and query memory-optimized tables on the secondary replica if2 it has been configured for read access. In [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)], the read timestamp on the secondary replica is in close synchronization with the read timestamp on the primary replica, which means that changes on the primary become visible on the secondary quickly. This close synchronization behavior is different from [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] In-Memory OLTP.  

### Considerations

- SQL Server 2019 introduced parallel redo for memory optimized availability group databases. In SQL Server 2016 and 2017, disk-based tables do not use parallel redo if a database in an availability group is also memory optimized. 
  
## Failover Clustering Instance (FCI) and In-Memory OLTP Databases  
 To achieve high-availability in a shared-storage configuration, you can set up a failover cluster instance with databases using memory-optimized tables. Consider the following factors as part of setting up an FCI:  
  
-   **Recovery Time Objective**   
    Failover time is likely to be higher as the memory-optimized tables must be loaded into memory before the database is made available.  
  
-   **SCHEMA_ONLY tables**   
    Be aware that SCHEMA_ONLY tables will be empty with no rows after the failover. This is as designed and defined by the application. This is exactly the same behavior when you restart an [!INCLUDE[inmemory](../../includes/inmemory-md.md)] database with one or more SCHEMA_ONLY tables.  
  
## Support for transaction replication in In-Memory OLTP  
 Tables acting as transactional replication subscribers, excluding Peer-to-peer transactional replication, can be configured as memory-optimized tables. Other replication configurations aren't compatible with memory-optimized tables.  For more information, see [Replication to Memory-Optimized Table Subscribers](../../relational-databases/replication/replication-to-memory-optimized-table-subscribers.md).  
  
## See Also  
 [Always On Availability Groups (SQL Server)](../../database-engine/availability-groups/windows/always-on-availability-groups-sql-server.md)   
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Active Secondaries: Readable Secondary Replicas (Always On Availability Groups)](../../database-engine/availability-groups/windows/active-secondaries-readable-secondary-replicas-always-on-availability-groups.md)   
 [Replication to Memory-Optimized Table Subscribers](../../relational-databases/replication/replication-to-memory-optimized-table-subscribers.md)  
  
  
