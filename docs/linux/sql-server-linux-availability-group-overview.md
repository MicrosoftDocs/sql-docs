---
# required metadata

title: Always On availability group for SQL Server on Linux | Microsoft Docs
description: 
author: MikeRayMSFT 
ms.author: mikeray 
manager: jhubbard
ms.date: 05/17/2017
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: e37742d4-541c-4d43-9ec7-a5f9b2c0e5d1 

# optional metadata
# keywords: ""
# ROBOTS: ""
# audience: ""
# ms.devlang: ""
# ms.reviewer: ""
# ms.suite: ""
# ms.tgt_pltfrm: ""
# ms.custom: ""

---

# Availability groups for SQL Server on Linux

A SQL Server Always On availability group is a high-availability (HA), disaster-recovery (DR), and scale-out solution. It provides HA for groups of databases on direct attached storage. It supports multiple secondaries for integrated HA and DR, automatic failure detection, fast transparent failover, and read load balancing. This broad set of capabilities allows you to achieve optimal availability SLAs for your workloads.

SQL Server availability groups were first introduced in SQL Server 2012 and have been improved with each release. This feature is now available on Linux. To accommodate SQL Server workloads with rigorous business continuity requirements, availability groups run on all supported [Linux OS distributions](sql-server-linux-release-notes.md). Also, all capabilities that make availability groups a flexible, integrated and efficient HA DR solution are available on Linux as well. These include: 

- **Multi-database failover**
   An availability group supports a failover environment for a set of user databases, known as availability databases.
- **Fast failure detection and failover**
   As a resource in a highly available cluster, an availability group benefits from built-in cluster intelligence for immediate failover detection and failover action.
- **Transparent failover using virtual IP resource**
   Enables client to use single connection string to primary in case of failover. Requires integration with a cluster manager.
- **Multiple synchronous and asynchronous secondaries**
   An availability group supports up to eight secondary replicas. With synchronous replicas the primary replica waits to commit transaction the primary replica waits for transactions to be written to disk on the transaction log. The primary replica does not wait for writes on asynchronous synchronous replicas.  
- **Manual or automatic failover**
   Failover to a synchronous secondary replica can be triggered automatically by the cluster or on demand by the database administrator.
- **Active secondaries available for read and backup workloads**
   One or more secondary replicas can be configured to support read-only access to secondary databases and/or to permit backups on secondary databases.
- **Automatic seeding**
   SQL Server automatically creates the secondary replicas for every database in the availability group.
- **Read-only routing**
   SQL Server routes incoming connections to an availability group listener to a secondary replica that is configured to allow read-only workloads. 
- **Database level health monitoring and failover trigger**
   Enhanced database level monitoring and diagnostics. 
- **Disaster recovery configurations**
   With distributed availability groups or multi-subnet availability group setup. 
- **Read-scale capabilities**
   In SQL Server 2017 you can create an availability group with or without HA for scale-out read-only operations. 


For details about SQL Server availability groups, see [SQL Server Always On availability groups](http://msdn.microsoft.com/library/hh510230.aspx).

## Availability group terminology

An availability group supports a failover environment for a discrete set of user databases - known as availability databases - that fail over together. An availability group supports one set of read-write primary databases and one to eight sets of corresponding secondary databases. Optionally, secondary databases can be made available for read-only access and/or some backup operations. An availability group defines a set of two or more failover partners, known as availability replicas. Availability replicas are components of the availability group. For details see [Overview of Always On availability groups (SQL Server)](http://msdn.microsoft.com/library/ff877884.aspx).

The following terms describe the main parts of a SQL Server availability group solution:

 availability group  
 A container for a set of databases, *availability databases*, that fail over together.  
  
 availability database  
 A database that belongs to an availability group. For each availability database, the availability group maintains a single read-write copy (the *primary database*) and one to eight read-only copies (*secondary databases*).  
  
 primary database  
 The read-write copy of an availability database.  
  
 secondary database  
 A read-only copy of an availability database.  
  
 availability replica  
 An instantiation of an availability group that is hosted by a specific instance of SQL Server and maintains a local copy of each availability database that belongs to the availability group. Two types of availability replicas exist: a single *primary replica* and one to eight *secondary replicas*.  
  
 primary replica  
 The availability replica that makes the primary databases available for read-write connections from clients and, also, sends transaction log records for each primary database to every secondary replica.  
  
 secondary replica  
 An availability replica that maintains a secondary copy of each availability database, and serves as a potential failover targets for the availability group. Optionally, a secondary replica can support read-only access to secondary databases can support creating backups on secondary databases.  
  
 availability group listener  
 A server name to which clients can connect in order to access a database in a primary or secondary replica of an availability group. Availability group listeners direct incoming connections to the primary replica or to a read-only secondary replica.  


## New in SQL Server 2017 for availability groups

SQL Server 2017 introduces new features for availability groups.

**CLUSTER_TYPE**
Use with `CREATE AVAILABILITY GROUP`. Identifies the type of server cluster manager that manages an availability group. Can be one of the following types:

   - **WSFC**
      Winows server failover cluster. On Windows, it is the default value for CLUSTER_TYPE.
   - **EXTERNAL** 
      A cluster manager that is not Windows server failover cluster - for example, on Linux with Pacemaker.
   - **NONE**
      No cluster manager. Used for a read-scale availability group.

For more information about these options, see [CREATE AVAILABILITY GROUP](http://msdn.microsoft.com/library/ff878399.aspx) or [ALTER AVAILABILITY GROUP](http://msdn.microsoft.com/library/ff878601.aspx).

**Guarantee commits on synchronous secondary replicas**

Use `required_synchronized_secondaries_to_commit`with `CREATE AVAILABILITY GROUP` or `ALTER AVAILABILITY GROUP`. When REQUIRED_COPIES_TO_COMMIT is set to a value higher than 0, transactions at the primary replica databases will wait until the transaction is committed on the specified number of **synchronous secondary** replica database transaction logs. If enough synchronous secondary replicas are not online, all connections to primary replica will be rejected until communication with sufficient secondary replicas resume.

**Read-scale availability groups**

Create an availability group without a cluster to support read-scale workloads. See [Read-scale availability groups](../database-engine/availability-groups/windows/read-scale-availability-groups.md).

## Next steps

[Configure availability group for SQL Server on Linux](sql-server-linux-availability-group-configure-ha.md)

[Configure read-scale availability group for SQL Server on Linux](sql-server-linux-availability-group-configure-rs.md)

[Add availability group Cluster Resource on RHEL](sql-server-linux-availability-group-cluster-rhel.md)

[Add availability group Cluster Resource on SLES](sql-server-linux-availability-group-cluster-sles.md)

[Add availability group Cluster Resource on Ubuntu](sql-server-linux-availability-group-cluster-ubuntu.md)
