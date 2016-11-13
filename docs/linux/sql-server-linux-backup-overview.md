---
# required metadata

title: Business continuity and database recovery SQL Server on Linux - SQL Server vNext CTP1 | Microsoft Docs
description: 
author: mihaelab 
ms.author: mihaelab 
manager: jhubbard
ms.date: 11/08/2016
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
# Business continuity and database recovery SQL Server on Linux - SQL Server vNext CTP1

SQL Server on Linux allows organizations to achieve a wide array of service-level agreement goals to accommodate various business requirements.

The simplest solutions leverage virtualization technologies to achieve a high degree of resiliency against host-level failures, fault tolerance against hardware failures, as well as elasticity and resource maximization. These systems can run on-premises, or in a private or public cloud, or hybrid environments. The simplest form of disaster recovery and protection is the database backup. Simple solutions available in SQL Server vNext CTP1 include:

- **VM Failover**
    - Resilience against guest and OS level failures
    - Unplanned and planned events
    - Minimum downtime for patching and upgrades
    - RTO in minutes


- **Backup and Restore**
    - Protection against accidental or malicious data corruption
    - Disaster recovery protection
    - RTO in minutes to hours

Standard high-availability and disaster recovery techniques provide instance-level protection combined with a reliable shared storage infrastructure. For SQL Server vNext CTP1 standard high-availability includes:

- **Failover Cluster**
    - Instance level protection
    - Automatic failure detection and failover
    - Resilience against OS and SQL Server failures
    - RTO in seconds to minutes

    Future releases of SQL Server vNext will include SQL server support for additional standard high availability capabilities, including database-level protection with basic Always On availability groups and disaster recovery with log shipping. The standard high availability features for SQL Server vNext will include:

- **Basic Availability Groups** (In development)
    - Availability Group with two replicas

- **Log Shipping** (In development)
    - Warm standby for disaster recovery

SQL Server on Linux vNext will meet mission critical high-availability and disaster recovery SLAs with Always On availability groups. 

- **Availability Groups** (In development)
    - Database level protection 
    - RTO in seconds
    - No data loss
    - Recover from unplanned outage
    - No downtime for planned maintenance
    - Offload read/backup workload to active secondaries
    - Failover to geographically distributed secondary site

## Summary

SQL Server vNext CTP1 on Linux includes virtualization, backup and restore, and failover clusters to support high-availability and disaster recovery. For more information about these capabilities, see:

- [Database backup and restore](sql-server-linux-backup-and-restore-database.md)
- [Virtualize server for high available](sql-server-linux-configure-high-availability-virtualize.md)
- [Shared-disk failover cluster](sql-server-linux-configure-high-availability-and-disaster-recovery.md)


## Next steps
