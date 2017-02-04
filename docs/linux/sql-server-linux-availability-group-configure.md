---
# required metadata

title: Configure availability group for SQL Server on Linux | Microsoft Docs
description: 
author: MikeRayMSFT 
ms.author: mikeray 
manager: jhubbard
ms.date: 02/03/2017
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: 150b0765-2c54-4bc4-b55a-7e57a5501a0f 

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

# Availability group for SQL Server on Linux

A SQL Server Always On Availability Group is a high-availability (HA) and disaster-recovery (DR) solution. It provides high availability for groups of databases on direct attached storage. It supports multiple secondaries for integrated HA and DR, automatic failure detection, fast transparent failover, and read load balancing. This broad set of capabilities allows you to achieve optimal availability SLAs for your workloads.

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

An availability group is one or more databases in a logical group, with replicas on more than one instance of SQL Server. 

## Next steps

