---
title: Disaster recovery for SQL Server on Linux | Microsoft Docs
description: 
author: mihaelab 
ms.author: mihaelab 
manager: jhubbard
ms.date: 09/14/2017
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: c75717c8-c677-4033-8ca6-d0ac93aee04d
---
# Business continuity and database recovery SQL Server on Linux

[!INCLUDE[tsql-appliesto-sslinux-only](../includes/tsql-appliesto-sslinux-only.md)]

SQL Server on Linux allows organizations to achieve a wide array of service-level agreement goals to accommodate various business requirements.

The simplest solutions leverage virtualization technologies to achieve a high degree of resiliency against host-level failures, fault tolerance against hardware failures, as well as elasticity and resource maximization. These systems can run on-premises, in a private or public cloud, or hybrid environments. The simplest form of disaster recovery and protection is the database backup. Basic high availability and disaster recovery solutions in SQL Server 2017 for Linux include:

- **VM Failover**
    - Resilience against guest and OS level failures
    - Unplanned and planned events
    - Minimum downtime for patching and upgrades
    - RTO in minutes


- [**Database backup and restore**](sql-server-linux-backup-and-restore-database.md) 
    - Protection against accidental or malicious data corruption
    - Disaster recovery protection
    - RTO in minutes to hours

Advanced high-availability and disaster recovery techniques provide instance-level protection combined with a reliable shared storage infrastructure. Advanced high availability and disaster recovery solutions in SQL Server 2017 for Linux include:

- [**Always On failover cluster instances**](sql-server-linux-shared-disk-cluster-configure.md)
    - Instance level protection
    - Automatic failure detection and failover
    - Resilience against OS and SQL Server failures
    - RTO in seconds to minutes

- [**Always On availability groups**](sql-server-linux-availability-group-overview.md)
    - Database level protection
    - Data redundancy
    - Distribute read only workloads
    - RTO in seconds

[!INCLUDE[HA-Story](../includes/sql-server-ha-story.md)]

## Summary

SQL Server 2017 on Linux includes virtualization, backup and restore, and failover cluster instances, and availability groups to support high-availability and disaster recovery. 