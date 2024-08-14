---
title: High availability and disaster recovery checklist
description: Learn about the recommended user configurations that you can implement to maximize availability and ensure recovery for Azure SQL Managed Instance.
author: Stralle
ms.author: strrodic
ms.reviewer: urmilano, mathoma
ms.date: 06/15/2024
ms.service: azure-sql-managed-instance
ms.subservice: high-availability
ms.topic: conceptual
---
# High availability and disaster recovery checklist - Azure SQL Managed Instance
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](../database/high-availability-disaster-recovery-checklist.md?view=azuresql-db&preserve-view=true)
> * [Azure SQL Managed Instance](high-availability-disaster-recovery-checklist.md?view=azuresql-mi&preserve-view=true)

The Azure SQL Managed Instance service automatically ensures all the databases are online, healthy, and constantly strives to achieve [the published SLA](https://azure.microsoft.com/support/legal/sla/azure-sql-database/). 

This guide provides a detailed review of proactive steps you can take to maximize availability, ensure recovery, and prepare for Azure outages. This guidance applies to all service tiers of Azure SQL Managed Instance.

## Availability checklist

The following are recommended configurations to maximize availability:

* Incorporate [retry logic](../database/develop-overview.md#resiliency) in the application to handle transient errors.
* Use [maintenance windows](maintenance-window.md) to make impactful maintenance events predictable and less disruptive.
* Test [application fault resiliency](high-availability-sla-local-zone-redundancy.md#testing-application-fault-resiliency) by manually triggering a failover to see resiliency in action.


## High availability checklist

The following is the recommended configuration to achieve high availability:

* Enable [zone redundancy](instance-zone-redundancy-configure.md) where available for the SQL managed instance to ensure resiliency for zonal failures.

## Disaster recovery checklist

Although Azure SQL Managed Instance automatically maintains availability, there are instances when even having high availability (zone redundancy) might not guarantee resiliency as the impacting outage spans an entire region. A _regional_ Azure SQL Managed Instance outage may require you to initiate disaster recovery. 

To best prepare for disaster recovery, follow these recommendations:

* Enable [failover groups](failover-group-sql-mi.md) for an instance. 
    * Use the read-write and read-only listener endpoints in your application connection string so applications automatically connect to whichever instance is primary. 
    * Set the failover policy to [customer managed](failover-group-sql-mi.md#failover-policy).
* Ensure the geo-secondary instance is created with the same service tier, hardware generation, and compute size as the primary instance. 
* When scaling up, scale up the geo-secondary first, and then scale up the primary.
* When scaling down, reverse the order: scale down the primary first, and then scale down the secondary.
* Disaster recovery, by nature, is designed to make use of asynchronous data replication between the primary and secondary region. To prioritize data availability over higher commit latency, consider calling the [sp_wait_for_database_copy_sync](/sql/relational-databases/system-stored-procedures/sp-wait-for-database-copy-sync-transact-sql) stored procedure immediately after committing a transaction. Calling `sp_wait_for_database_copy_sync` blocks the calling thread until the last committed transaction has been transmitted and hardened in the transaction log of the secondary database.
* Monitor lag with respect to Recovery Point Objective (RPO) by using the `replication_lag_sec` column of the [sys.dm_geo_replication_link_status](/sql/relational-databases/system-dynamic-management-views/sys-dm-geo-replication-link-status-azure-sql-database?preserve-view=true&view=azuresqlmi-current) dynamic management view (DMV) on the primary database. The DMV shows lag in seconds between the transactions committed on the primary and hardened to the transaction log on the secondary. For example, assume the lag is one second at a point in time, if the primary is impacted by an outage and a geo-failover is initiated at that point in time, transactions committed in the last second will be lost.
* If enabling failover groups isn't possible, then consider setting the [backup storage redundancy option](automated-backups-change-settings.md?preserve-view=true&view=azuresqldb-current#configure-backup-storage-redundancy) to **Geo-redundant backup storage** to use the [geo-restore capability](recovery-using-backups.md#point-in-time-restore). 
    * This option isn't available in [regions with no region pair](/azure/reliability/cross-region-replication-azure#regions-with-availability-zones-and-no-region-pair). 
* Frequently plan and execute [disaster recovery drills](disaster-recovery-drills.md) so you're better prepared in the event of a real outage.

## Prepare secondary for an outage

To successfully recover to another data region using failover groups, or geo-restore, you need to prepare a secondary Azure SQL Managed Instance in another region. This secondary instance can become the new primary instance if needed. You should also have well-defined steps documented and tested to ensure a smooth recovery. These preparation steps include:

* For geo-restore, identify the instance in another region to become the new primary instance. This is generally an instance [in the paired region](/azure/availability-zones/cross-region-replication-azure) for the region in which your primary instance is located. Using an instance in a region paired to the primary region eliminates the cost of extra traffic during the geo-restore operations.
* Determine how you're going to redirect users to the new primary server. Redirecting users could be accomplished by manually changing application connection strings or DNS entries. If you've configured failover groups and use the read-write and read-only listener in application connection strings, no further action is needed - connections are automatically directed to new primary after failover.
* Identify, and optionally define, the [NSG and route table configuration](connectivity-architecture-overview.md#service-aided-subnet-configuration) that users need to access the new primary database on the new primary.
* Identify, and optionally create, the logins that must be present in the `master` database on the new primary server, and ensure these logins have appropriate permissions in the `master` database, if any. 
* Document the [auditing configuration](auditing-configure.md) on the current primary and make it identical on the secondary instance. 

## Related content

To learn more, review: 

- [High availability for Azure SQL Managed Instance](high-availability-sla-local-zone-redundancy.md)
- [Continuity scenarios](business-continuity-high-availability-disaster-recover-hadr-overview.md)
- [Disaster recovery guidance](disaster-recovery-guidance.md)
- [SLA for Azure SQL Managed Instance](https://azure.microsoft.com/support/legal/sla/azure-sql-sql-managed-instance/)
- [Automated backups](automated-backups-overview.md)
- [Restore a database from the service-initiated backups](recovery-using-backups.md)
- [Failover groups](failover-group-sql-mi.md)
- [Geo-restore](recovery-using-backups.md#point-in-time-restore)

