---
title: "High availability - in-memory OLTP databases"
description: SQL Server Databases with memory-optimized tables, with or without native compiled stored procedures, are fully supported with Always On Availability Groups.
author: dimitri-furman
ms.author: dfurman
ms.date: 11/24/2026
ms.service: sql
ms.subservice: in-memory-oltp
ms.topic: concept-article
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
    Secondary replicas maintain the in-memory state of the durable memory-optimized tables. In an automatic or forced failover that takes the hot path, failover time is comparable to disk-based tables because the memory-optimized data doesn't need to be reloaded. For more information, see [Failover time](#failover-time).
  
-   **Readable Secondary**   
    You can access and query memory-optimized tables on the secondary replica if2 it has been configured for read access. In [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)], the read timestamp on the secondary replica is in close synchronization with the read timestamp on the primary replica, which means that changes on the primary become visible on the secondary quickly. This close synchronization behavior is different from [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] In-Memory OLTP.  

### Parallel redo

SQL Server 2019 introduced parallel redo for memory-optimized availability group databases. In SQL Server 2016 and 2017, disk-based tables don't use parallel redo if a database in an availability group is also memory-optimized.
  
### Failover time

In a failover, the *old primary* is the replica that was primary before the failover, and the *new primary* is the secondary replica that becomes primary. On the new primary, each database that contains memory-optimized data takes one of two paths to make the memory-optimized tables ready when a failover occurs.

- **Hot path:** While it was a secondary, the new primary kept its memory-optimized tables in memory and current. The database finishes applying the log and switches to the primary role in place, usually within seconds. This path is used most commonly.
- **Cold path:** The database is restarted on the new primary. Durable memory-optimized (XTP) data is reloaded from checkpoint files, and transaction log records since the last durable checkpoint are replayed before the database becomes available. Recovery time depends on the volume of memory-optimized data, the number of log records to replay, and storage and CPU throughput.

A failover takes the hot path only when all of the following are true for the database on the new primary:

- **The database on the new primary finished starting up before the failover.** When a secondary replica restarts, or becomes a secondary after a previous failover, it reloads its memory-optimized data before it resumes applying log. A failover to that replica within this window, which is about as long as the XTP data reload itself, takes the cold path. This condition matters when failovers happen in quick succession or shortly after a restart.
- **The new primary's Resource database version matches the version the database was last synchronized with.** Each database records the version of the [Resource database](../databases/resource-database.md) on the replica where it last ran as primary. If the new primary's Resource database version is different, the database is restarted so it can be synchronized, and the failover takes the cold path. A cumulative update (CU) might change the Resource database version, so this condition can happen when you apply a CU to a secondary replica first and then fail over to it. To check before a failover, compare the output from `SELECT SERVERPROPERTY('ResourceVersion')` on the old primary and on the secondary replica that will become the new primary. Once the database has been synchronized, later failovers between replicas with the same Resource database version take the hot path.
- **Change tracking is not enabled on the database.** If it's enabled, every failover takes the cold path.

When the old primary becomes a secondary, its copy of the database always takes the cold path. That condition affects when the old primary is ready to serve as a secondary again.

## Failover Clustering Instance (FCI) and In-Memory OLTP Databases  
 To achieve high-availability in a shared-storage configuration, you can set up a failover cluster instance with databases using memory-optimized tables. Consider the following factors as part of setting up an FCI:  
  
-   **Recovery Time Objective**   
    Failover time is likely to be higher as the memory-optimized tables must be loaded into memory before the database is made available.  
  
-   **SCHEMA_ONLY tables**   
    Be aware that SCHEMA_ONLY tables will be empty with no rows after the failover. This is as designed and defined by the application. This is exactly the same behavior when you restart an [!INCLUDE[inmemory](../../includes/inmemory-md.md)] database with one or more SCHEMA_ONLY tables.  
  
## Support for transaction replication in In-Memory OLTP  
 Tables acting as transactional replication subscribers, excluding Peer-to-peer transactional replication, can be configured as memory-optimized tables. Other replication configurations aren't compatible with memory-optimized tables.  For more information, see [Replication to Memory-Optimized Table Subscribers](../../relational-databases/replication/replication-to-memory-optimized-table-subscribers.md).  
  
## Related content

- [What is an Always On availability group?](../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)
- [Offload read-only workload to secondary replica of an Always On availability group](../../database-engine/availability-groups/windows/active-secondaries-readable-secondary-replicas-always-on-availability-groups.md)
- [Replication to Memory-Optimized Table Subscribers](../replication/replication-to-memory-optimized-table-subscribers.md)
