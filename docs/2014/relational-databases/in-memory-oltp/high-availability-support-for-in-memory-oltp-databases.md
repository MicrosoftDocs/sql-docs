---
title: "High Availability Support for In-Memory OLTP databases | Microsoft Docs"
ms.custom: ""
ms.date: "10/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: 2113a916-3b1e-496c-8650-7f495e492510
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# High Availability Support for In-Memory OLTP databases
  Databases containing memory-optimized tables, with or without native compiled stored procedures, are fully supported with AlwaysOn Availability Groups.  There is no difference in the configuration and support for databases which contain [!INCLUDE[hek_2](../../includes/hek-2-md.md)] objects as compared to those without,  
  
## AlwaysOn Availability Groups and In-Memory OLTP Databases  
 Configuring databases with [!INCLUDE[hek_2](../../includes/hek-2-md.md)] components provides the following:  
  
-   **A fully integrated experience**   
    You can configure your databases containing memory-optimized tables using the same wizard with the same level of support for both synchronous and asynchronous secondary replicas. Additionally, health monitoring is provided using the familiar AlwaysOn dashboard in SQL Server Management Studio.  
  
-   **Comparable Failover time**   
    Secondary replicas maintain the in-memory state of the durable memory-optimized tables. In the event of automatic or forced failover, the time to failover to the new primary is comparable to disk-bases tables as no recovery is needed. Memory-optimized tables created as SCHEMA_ONLY are supported in this configuration. However changes to these tables are not logged and therefore no data will exist in these tables on the secondary replica.  
  
-   **Readable Secondary**   
    You can access and query memory-optimized tables on the secondary replica if it has been configured for read access. For more information see [Active Secondaries: Readable Secondary Replicas (AlwaysOn Availability Groups)](../../database-engine/availability-groups/windows/active-secondaries-readable-secondary-replicas-always-on-availability-groups.md).  
  
## Failover Clustering Instance (FCI) and In-Memory OLTP Databases  
 To achieve high-availability in a shared-storage configuration, you can setup failover clustering on instances with one or more database with memory-optimized tables. You need to consider the following factors as part of setting up an FCI.  
  
-   **Recovery Time Objective**   
    Failover time will likely to be higher as the memory-optimized tables must be loaded into memory before the database is made available.  
  
-   **SCHEMA_ONLY tables**   
    Be aware that SCHEMA_ONLY tables will be empty with no rows after the failover. This is as designed and defined by the application. This is exactly the same behavior when you restart an [!INCLUDE[hek_2](../../includes/hek-2-md.md)] database with one or more SCHEMA_ONLY tables.  
  
## Support for transaction replication in In-Memory OLTP  
 Tables acting as transactional replication subscribers, excluding Peer-to-peer transactional replication, can be configured as memory-optimized tables. Other replication configurations are not compatible with memory-optimized tables.  For more information see [Replication to Memory-Optimized Table Subscribers](../replication/replication-to-memory-optimized-table-subscribers.md).  
  
## See Also  
 [AlwaysOn Availability Groups (SQL Server)](../../database-engine/availability-groups/windows/always-on-availability-groups-sql-server.md)   
 [Overview of AlwaysOn Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Active Secondaries: Readable Secondary Replicas &#40;AlwaysOn Availability Groups&#41;](../../database-engine/availability-groups/windows/active-secondaries-readable-secondary-replicas-always-on-availability-groups.md)   
 [Replication to Memory-Optimized Table Subscribers](../replication/replication-to-memory-optimized-table-subscribers.md)  
  
  
