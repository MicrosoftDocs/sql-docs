---
# required metadata

title: Always On Availability Group for SQL Server on Linux | Microsoft Docs
description: 
author: MikeRayMSFT 
ms.author: mikeray 
manager: jhubbard
ms.date: 02/09/2017
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

# Always On Availability Group for SQL Server on Linux

A SQL Server Always On Availability Group is a high-availability (HA) and disaster-recovery (DR) solution. It provides HA for groups of databases on direct attached storage. It supports multiple secondaries for integrated HA and DR, automatic failure detection, fast transparent failover, and read load balancing. This broad set of capabilities allows you to achieve optimal availability SLAs for your workloads.

SQL Server Always On Availability Groups were first introduced in SQL Server 2012 and have been improved with each release. This feature is now available on Linux. To accommodate SQL Server workloads with rigorous business continuity requirements, Always On Availability Groups run on all supported [Linux OS distributions](sql-server-linux-release-notes.md). Also, all capabilities that make Availability Groups a flexible, integrated and efficient HA DR solution are available on Linux as well. These include: 

- **Multi-database failover**
   An availability group supports a failover environment for a set of user databases, known as availability databases.
- **Fast failure detection and failover**
   As a resource in a highly available cluster, an availability group benefits from built-in cluster intelligence for immediate failover detection and failover action.
- **Transparent failover using availability group listener**
   Enables client to use single connection string to primary or secondary databases that does not change in case of failover. 
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

For details about SQL Server Availability Groups, see [SQL Server Always On Availability Groups](http://msdn.microsoft.com/library/hh510230.aspx).

## Overview of Steps to Configure SQL Server Availability Group on Linux

To configure a SQL Server Availability Group on Linux, first configure the availability group and then add it as a resource to a Linux cluster. The steps to configure the availability group are the same across Linux distributions. The tools to manage the cluster resources may be different, depending on the distribution. For specific steps, see the following links. 

## New in SQL Server vNext for Availability Groups

SQL Server vNext introduces two new features for availability groups.

**REQUIRED_COPIES_TO_COMMIT**

Use with `CREATE AVAILABILITY GROUP` or `ALTER AVAILABILITY GROUP`. When REQUIRED_COPIES_TO_COMMIT is set, transactions at the primary replica databases will wait until the transaction is committed on the required number of synchronous secondary replica database transaction logs. If enough synchronous secondary replicas are not online, transactions will stop until communication with sufficient secondary replicas resume.

**CLUSTER_TYPE**
Use with `CREATE AVAILABILITY GROUP`. Denotes an availability group on a server that is not a member of a Windows Server Failover Cluster (WSFC).

For more information about these options, see [CREATE AVAILABILITY GROUP](http://msdn.microsoft.com/library/ff878399.aspx) or [ALTER AVAILABILITY GROUP](http://msdn.microsoft.com/library/ff878601.aspx).


## Next steps

[Configure Availability Group for SQL Server on Linux](sql-server-linux-availability-group-configure.md)

[Add Availability Group Cluster Resource on RHEL](sql-server-linux-availability-group-cluster-rhel.md)

[Add Availability Group Cluster Resource on SLES](sql-server-linux-availability-group-cluster-sles.md)

[Add Availability Group Cluster Resource on Ubuntu](sql-server-linux-availability-group-cluster-ubuntu.md)
