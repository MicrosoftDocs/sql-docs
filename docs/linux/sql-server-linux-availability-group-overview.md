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

For a comprehensive introduction, see [SQL Server Always On Availability Groups](http://msdn.microsoft.com/library/hh510230.aspx).

Configure SQL Server Always On Availability Groups on Linux server. In order to accommodate SQL Server workloads with rigorous business continuity requirements to run on Linux, Always On Availability Groups run on all supported [Linux OS distributions](sql-server-linux-release-notes.md). Also, all capabilities that make Availability Groups a flexible, integrated and efficient HA DR solution are available on Linux as well. These include: 

- Multi-database failover
- Fast failure detection and failover
- Multiple synchronous and asynchronous secondaries
- Manual or automatic failover
- Active secondaries available for read and backup workloads
- Direct seeding
- Read-only routing
- Database level health monitoring and failover trigger

## Next steps

[Configure Availability Group for SQL Server on Linux](sql-server-linux-availability-group-configure)

[Configure Red Hat Enterprise Linux Cluster for SQL Server Availability Group Cluster Resources](sql-server-linux-availability-group-cluster-rhel.md)

[Configure SUSE Enterprise Linux Cluster for SQL Server Availability Group Cluster Resources](sql-server-linux-availability-group-cluster-sles.md)

[Configure Ubuntu Cluster for SQL Server Availability Group Cluster Resources](sql-server-linux-availability-group-cluster-ubuntu.md)