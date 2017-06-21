---
# required metadata

title: Disaster recovery for SQL Server on Linux | Microsoft Docs
description: 
author: mihaelab 
ms.author: mihaelab 
manager: jhubbard
ms.date: 03/17/2017
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: c75717c8-c677-4033-8ca6-d0ac93aee04d


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
# Business continuity and database recovery SQL Server on Linux

SQL Server on Linux allows organizations to achieve a wide array of service-level agreement goals to accommodate various business requirements.

The simplest solutions leverage virtualization technologies to achieve a high degree of resiliency against host-level failures, fault tolerance against hardware failures, as well as elasticity and resource maximization. These systems can run on-premises, in a private or public cloud, or hybrid environments. The simplest form of disaster recovery and protection is the database backup. Simple solutions available in SQL Server 2017 RC0 include:

- **VM Failover**
    - Resilience against guest and OS level failures
    - Unplanned and planned events
    - Minimum downtime for patching and upgrades
    - RTO in minutes


- [**Database backup and restore**](sql-server-linux-backup-and-restore-database.md) 
    - Protection against accidental or malicious data corruption
    - Disaster recovery protection
    - RTO in minutes to hours

Standard high-availability and disaster recovery techniques provide instance-level protection combined with a reliable shared storage infrastructure. For SQL Server 2017 RC0 standard high-availability includes:

- [**Failover Cluster**](sql-server-linux-shared-disk-cluster-configure.md)
    - Instance level protection
    - Automatic failure detection and failover
    - Resilience against OS and SQL Server failures
    - RTO in seconds to minutes


## Summary

SQL Server 2017 RC0 on Linux includes virtualization, backup and restore, and failover clusters to support high-availability and disaster recovery. 