---
title: Cloud business continuity - disaster recovery
titleSuffix: Azure SQL  Managed Instance
description: Learn how Azure SQL Managed Instance supports cloud business continuity and disaster recovery to help keep mission-critical cloud applications running.
author: Stralle
ms.author: strrodic
ms.reviewer: mathoma
ms.date: 05/01/2023
ms.service: sql-managed-instance
ms.subservice: high-availability
ms.topic: conceptual
ms.custom: "azure-sql-split"
keywords:
  - "business continuity"
  - "cloud business continuity"
  - "database disaster recovery"
  - "database recovery"
monikerRange: "= azuresql || = azuresql-mi"
---
# Overview of business continuity with Azure SQL Managed Instance
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](../database/business-continuity-high-availability-disaster-recover-hadr-overview.md?view=azuresql-db&preserve-view=true)
> * [Azure SQL Managed Instance](business-continuity-high-availability-disaster-recover-hadr-overview.md?view=azuresql-mi&preserve-view=true)


**Business continuity** in Azure SQL Managed Instance refers to the mechanisms, policies, and procedures that enable your business to continue operating in the face of disruption, particularly to its computing infrastructure. In most cases, SQL Managed Instance handles the disruptive events that might happen in the cloud environment and keep your applications and business processes running. However, there are some disruptive events that can't be handled by SQL Managed Instance automatically, such as:

- User accidentally deleted or updated a row in a table.
- Malicious attacker succeeded to delete data or drop a database.
- Earthquake caused a power outage and temporarily disabled datacenter or any other catastrophic natural disaster event.

This overview describes the capabilities that SQL Managed Instance provides for business continuity and disaster recovery. Learn about options, recommendations, and tutorials for recovering from disruptive events that could cause data loss or cause your database and application to become unavailable. Learn what to do when a user or application error affects data integrity, an Azure region has an outage, or your application requires maintenance.

## Features that provide business continuity

From an instance perspective, there are four major potential disruption scenarios:

- Local hardware or software failures affecting the instance node such as a disk-drive failure.
- Data corruption or deletion typically caused by an application bug or human error. Such failures are application-specific and typically can't be detected by the database service.
- Datacenter outage, possibly caused by a natural disaster. This scenario requires some level of geo-redundancy with application failover to an alternate datacenter.
- Upgrade or maintenance errors, unanticipated issues that occur during planned infrastructure maintenance or upgrades may require rapid rollback to a prior database state.

To mitigate the local hardware and software failures, SQL Managed Instance includes a [high availability architecture](high-availability-sla.md), which guarantees automatic recovery from these failures with up to 99.995% availability SLA.  

To protect your business from data loss, SQL Managed Instance automatically creates full database backups weekly, differential database backups every 12 hours, and transaction log backups every 10 minutes. By default, the backups are stored in [redundant storage](automated-backups-overview.md#backup-storage-redundancy) for seven days for both service tiers, with a configurable backup retention period for point-in-time restore of 1 to 35 days.

SQL Managed Instance also provides several business continuity features that you can use to mitigate various unplanned scenarios: 

- [Built-in automated backups](automated-backups-overview.md) and [Point in Time Restore](recovery-using-backups.md#point-in-time-restore) enables you to restore complete database to some point in time within the configured retention period of 1 to 35 days.
- You can [restore a deleted database](recovery-using-backups.md#deleted-database-restore) to the point at which it was deleted.
- [Long-term backup retention](../database/long-term-retention-overview.md) enables you to keep backups up to 10 years.  
- [Auto-failover group](auto-failover-group-sql-mi.md#terminology-and-capabilities) allows the application to automatically recover in the event of a regional outage.
- [Temporal tables](../temporal-tables.md) enable you to restore row versions from any point in time.


## Recover a database within the same Azure region

You can use automatic database backups to restore a database to a point in time in the past. This way you can recover from data corruptions caused by human errors. The point-in-time restore allows you to create a new database to the same instance, or a different instance, that represents the state of data prior to the corrupting event. The restore operation is a size of data operation that also depends on the current workload of the target server. It may take longer to recover a very large or very active database. For more information about recovery time, see [database recovery time](recovery-using-backups.md#recovery-time).

If the maximum supported backup retention period for point-in-time restore (PITR) isn't sufficient for your application, you can extend it by configuring a long-term retention (LTR) policy for the database(s). For more information, see [Long-term backup retention](../database/long-term-retention-overview.md).


## Recover a database to an existing instance

Although rare, an Azure datacenter can have an outage. When an outage occurs, it causes a business disruption that might only last a few minutes or might last for hours.

- One option is to wait for your database to come back online when the datacenter outage is over. This works for applications that can afford to have the database offline. For example, a development project or free trial you don't need to work on constantly. When a datacenter has an outage, you don't know how long the outage might last, so this option only works if you don't need your database for a while.
- If you're using geo-redundant (GRS), or geo-zone-redundant (GZRS) storage, another option is to restore a database to any SQL managed instance in any Azure region using [geo-redundant database backups](recovery-using-backups.md#geo-restore) (geo-restore). Geo-restore uses a geo-redundant backup as its source and can be used to recover a database to the last available point in time, even if the database or datacenter is inaccessible due to an outage. The available backup can be found in the paired region. 
- Finally, you can quickly recover from an outage if you've configured a geo-secondary using an [auto-failover group](auto-failover-group-sql-mi.md) for your instance, using either manual or automatic failover. While the failover itself takes only a few seconds, the service takes at least 1 hour to activate an automatic geo-failover, if configured. This is necessary to ensure that the failover is justified by the scale of the outage. Also, the failover may result in the loss of recently changed data due to the nature of asynchronous replication between the paired regions.

As you develop your business continuity plan, you need to understand the maximum acceptable time before the application fully recovers after the disruptive event. The time required for application to fully recover is known as Recovery Time Objective (RTO). You also need to understand the maximum period of recent data updates (time interval) the application can tolerate losing when recovering from an unplanned disruptive event. The potential data loss is known as Recovery Point Objective (RPO).

Different recovery methods offer different levels of RPO and RTO. You can choose a specific recovery method, or use a combination of methods to achieve full application recovery. 

The following table compares RPO and RTO of each recovery option when restoring from a geo-replicated backup, or using an auto-failover group: 

| **Recovery method** | **RTO** | **RPO** |
| --- | --- | --- |
| Geo-restore from geo-replicated backups | 12 h | 1 h |
| Auto-failover groups | 1 h | 5 s |


Use auto-failover groups if your application meets any of these criteria:

- Is mission critical.
- Has a service level agreement (SLA) that doesn't allow for 12 hours or more of downtime.
- Downtime may result in financial liability.
- Has a high rate of data change and 1 hour of data loss isn't acceptable.
- The additional cost of active geo-replication is lower than the potential financial liability and associated loss of business.

You may choose to use a combination of database backups and auto-failover groups depending upon your application requirements. 

The following sections provide an overview of the steps to recover using either database backups or auto-failover groups. 

### Prepare for an outage

Regardless of the business continuity feature you use, you must:

- Identify and prepare the target instance, including network IP firewall rules, logins, and `master` database level permissions.
- Determine how to redirect clients and client applications to the new instance
- Document other dependencies, such as auditing settings and alerts

If you don't prepare properly, bringing your applications online after a failover or a database recovery takes additional time, and likely also requires troubleshooting at a time of stress - a bad combination.

### Fail over to a geo-replicated secondary instance

If you're using auto-failover groups as your recovery mechanism, you can configure an automatic failover policy. Once initiated, the failover causes the secondary instance to become the new primary and ready to record new transactions and respond to queries - with minimal data loss for the data not yet replicated. 

> [!NOTE]
> When the datacenter comes back online the old primary automatically reconnects to the new primary to become the secondary instance. If you need to relocate the primary back to the original region, you can initiate a planned failover manually (failback).

### Perform a geo-restore

If you're using automated backups with geo-redundant storage (the default storage option when you create your instance), you can recover the database using [geo-restore](recovery-using-backups.md#geo-restore). Recovery usually takes place within 12 hours - with data loss of up to one hour determined by when the last log backup was taken and replicated. Until the recovery completes, the database is unable to record any transactions or respond to any queries. Note, geo-restore only restores the database to the last available point in time.

> [!NOTE]
> If the datacenter comes back online before you switch your application over to the recovered database, you can cancel the recovery.

### Perform post failover / recovery tasks

After recovery from either recovery mechanism, you must perform the following additional tasks before your users and applications are back up and running:

- Redirect clients and client applications to the new instance and restored database.
- Ensure appropriate network IP firewall rules are in place for users to connect. 
- Ensure appropriate logins and `master` database-level permissions are in place (or use [contained users](/sql/relational-databases/security/contained-database-users-making-your-database-portable)).
- Configure auditing, as appropriate.
- Configure alerts, as appropriate.


> [!NOTE]
> If you are using an auto-failover group and connect to the instance using the read-write listener, the redirection after failover will happen automatically and transparently to the application.


## Next steps

To learn more about business continuity features, see [Automated backups](automated-backups-overview.md), and [auto-failover groups](auto-failover-group-sql-mi.md#terminology-and-capabilities). In the event of a disaster, see [recover a database](recovery-using-backups.md). 
