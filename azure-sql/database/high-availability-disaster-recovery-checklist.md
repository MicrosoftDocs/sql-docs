---
title: Azure SQL Database high availability and disaster recovery checklist
description: Learn about the recommended user configurations that you can implement to maximize availability and ensure recovery for Azure SQL Database.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: rsetlem
ms.date: 02/08/2023
ms.service: sql-database
ms.subservice: high-availability
ms.topic: conceptual
---
# Azure SQL Database high availability and disaster recovery checklist
[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

Azure SQL Database service automatically ensures all the databases are online, healthy, and constantly strives to achieve [the published SLA](https://azure.microsoft.com/support/legal/sla/azure-sql-database/). 

This guide provides a detailed review of proactive steps you can take to maximize availability, ensure recovery, and prepare for Azure outages. This guidance applies to all purchasing models and service tiers of Azure SQL Database.

## High availability checklist

The following are recommended configurations that you can implement to maximize availability:

* [Enable zone redundancy](high-availability-sla.md) where available for the database or elastic pool to ensure resiliency from a larger set of failures in a region.
* [Incorporate retry logic](develop-overview.md#resiliency) in the application to handle transient errors.
* [Leverage maintenance windows](maintenance-window.md) to make impactful maintenance events predictable and less disruptive.
* [Test application fault resiliency](high-availability-sla.md#testing-application-fault-resiliency) by manually triggering a failover to see built in High Availability in action.

## Disaster recovery checklist

Although Azure SQL Database automatically maintains high availability, there are instances when having zone redundancy, retry logic, and maintenance windows might not guarantee availability as the impacting outage spans an entire region. An Azure SQL Database outage may require you to initiate disaster recovery. 

The following are recommended configurations to be best prepared for disaster recovery:

* Enable [active geo-replication](active-geo-replication-overview.md) to have a readable secondary database in a different Azure region. 
* Enable [auto-failover groups](auto-failover-group-sql-db.md) for a group of databases. Leverage the read-write and read-only listener endpoints that remain unchanged after failover. With auto-failover groups, you can set the failover mode to [automatic (default) or manual](auto-failover-group-sql-db.md#terminology-and-capabilities).
* Ensure the secondary database is created with the same service tier and service level objective (SLO) as primary database. When scaling up, we recommend that you scale up the geo-secondary first, and then scale up the primary. When scaling down, reverse the order: scale down the primary first, and then scale down the secondary.
* Disaster recovery by nature is designed to make use of asynchronous replication of data between primary and secondary region. To prioritize data availability over higher commit latency, consider calling the [sp_wait_for_database_copy_sync](/sql/relational-databases/system-stored-procedures/active-geo-replication-sp-wait-for-database-copy-sync?preserve-view=true&view=azuresqldb-current) stored procedure immediately after committing the transaction. Calling `sp_wait_for_database_copy_sync` blocks the calling thread until the last committed transaction has been transmitted and hardened in the transaction log of the secondary database.
* Monitor lag with respect to Recovery Point Objective (RPO), using the `replication_lag_sec` column of the [sys.dm_geo_replication_link_status](/sql/relational-databases/system-dynamic-management-views/sys-dm-geo-replication-link-status-azure-sql-database?preserve-view=true&view=azuresqldb-current) dynamic management view (DMV) on the primary database. It shows lag in seconds between the transactions committed on the primary and hardened to the transaction log on the secondary. For example, if the lag is one second, it means that if the primary is impacted by an outage at this moment and a geo-failover is initiated, transactions committed in the last second will be lost.
* [Configure the backup storage redundancy option](automated-backups-change-settings.md?preserve-view=true&view=azuresqldb-current#configure-backup-storage-redundancy) to **Geo-redundant backup storage** to leverage [geo-restore capability](recovery-using-backups.md#point-in-time-restore). 
    * This option is not available in [regions with no region pair](/azure/reliability/cross-region-replication-azure#regions-with-availability-zones-and-no-region-pair). 
* Frequently plan and execute [disaster recovery drills](disaster-recovery-drills.md) so that you are better prepared in the event of a real outage.

## Checklist for preparing secondary for an outage

For success with recovery to another data region using either active geo-replication, auto-failover groups, or geo-restore, you need to prepare a secondary Azure SQL Database logical server in another region. This secondary server can become the new primary server should the need arise. You should also have well-defined steps documented and tested to ensure a smooth recovery. These preparation steps include:

* For geo-restore, identify the server in another region to become the new primary server. This is generally a server [in the paired region](/azure/availability-zones/cross-region-replication-azure) for the region in which your database is located. This eliminates the additional traffic cost during the geo-restoring operations.
* Determine how you are going to redirect users to the new primary server. This could be accomplished by manually changing application connection strings or DNS entries. If you have enabled auto-failover groups and use the read-write and read-only listener in application connection strings, no further action is needed as connections will be automatically directed to new primary.
* Identify, and optionally define, the [firewall rules](firewall-configure.md) needed for users to access the new primary database.
* Identify, and optionally create the logins that must be present in the `master` database on the new primary server, and ensure these logins have appropriate permissions in the `master` database, if any. For more information, see [Azure SQL Database security after disaster recovery](active-geo-replication-security-configure.md).
* Identify alert rules that need to be updated to map to the new primary.
* Document the auditing configuration on the current primary.

## Next steps

- Review [Azure SQL Database disaster recovery guidance](disaster-recovery-guidance.md).
- Review the [SLA for Azure SQL Database](https://azure.microsoft.com/support/legal/sla/azure-sql-database/).
- To learn about Azure SQL Database automated backups, see [SQL Database automated backups](automated-backups-overview.md).
- To learn about business continuity design and recovery scenarios, see [Continuity scenarios](business-continuity-high-availability-disaster-recover-hadr-overview.md).
- To learn about using automated backups for recovery, see [restore a database from the service-initiated backups](recovery-using-backups.md).
- Learn more about [active geo-replication](active-geo-replication-overview.md).
- Learn more about [auto-failover groups](auto-failover-group-sql-db.md).
- Learn more about [geo-restore](recovery-using-backups.md#point-in-time-restore).
- Learn more about [zone-redundant databases](high-availability-sla.md).
