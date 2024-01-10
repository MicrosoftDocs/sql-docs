---
title: Cloud business continuity - disaster recovery
titleSuffix: Azure SQL Database
description: Learn how Azure SQL Database supports cloud business continuity and disaster recovery to help keep mission-critical cloud applications running.
author: rajeshsetlem
ms.author: rsetlem
ms.reviewer: wiassaf, mathoma
ms.date: 01/05/2024
ms.service: sql-database
ms.subservice: high-availability
ms.topic: conceptual
ms.custom: azure-sql-split, ignite-2023
keywords:
  - "business continuity"
  - "cloud business continuity"
  - "database disaster recovery"
  - "database recovery"
monikerRange: "= azuresql || = azuresql-db "
---
# Overview of business continuity with Azure SQL Database
[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](business-continuity-high-availability-disaster-recover-hadr-overview.md?view=azuresql-db&preserve-view=true)
> * [Azure SQL Managed Instance](../managed-instance/business-continuity-high-availability-disaster-recover-hadr-overview.md?view=azuresql-mi&preserve-view=true)

This article provides an overview of the business continuity and disaster recovery capabilities of Azure SQL Database, describing the options and recommendations to recover from disruptive events that could lead to data loss or cause your database and application to become unavailable. Learn what to do when a user or application error affects data integrity, an Azure availability zone or region has an outage, or your application requires maintenance.

<br/>


## Overview

**Business continuity** in Azure SQL Database refers to the mechanisms, policies, and procedures that enable your business to continue operating in the face of disruption by providing availability, high availability, and disaster recovery. 

In most cases, SQL Database handles disruptive events that might happen in a cloud environment and keeps your applications and business processes running. However, there are some disruptive events where mitigation might take some time, such as:

- User accidentally deletes or updates a row in a table.
- Malicious attacker successfully deletes data or drops a database. 
- Catastrophic natural disaster event takes down a datacenter or availability zone or region. 
- Rare datacenter, availability zone or region-wide outage caused by a configuration change, software bug or hardware component failure.

> [!VIDEO https://learn-video.azurefd.net/vod/player?show=data-exposed&ep=azure-sql-db-high-availability-disaster-recovery-overview-data-exposed]

### Availability

Azure SQL Database comes with a core resiliency and reliability promise that protects it against software or hardware failures. Database backups are automated to protect your data from corruption or accidental deletion. As a Platform-as-a-service (PaaS), the Azure SQL Database service provides availability as an off-the-shelf feature with an industry-leading availability SLA of 99.99%. 

### High Availability

To achieve high availability in the Azure cloud environment, enable [zone redundancy](high-availability-sla.md#zone-redundant-availability) so the database, or elastic pool, uses [availability zones](/azure/reliability/availability-zones-overview) to ensure the database, or elastic pool, is resilient to zonal failures. Many Azure regions provide availability zones, which are separated groups of data centers within a region that have independent power, cooling, and networking infrastructure. Availability zones are designed to provide regional services, capacity, and high availability in the remaining zones if one zone experiences an outage. By enabling zone redundancy, the database or elastic pool is resilient to zonal hardware and software failures and the recovery is transparent to applications. When high availability is enabled, the Azure SQL Database service is able to provide a higher availability SLA of 99.995%. 

### Disaster recovery

To achieve higher availability and redundancy across regions, you can enable disaster recovery capabilities to quickly recover the database from a catastrophic regional failure. Options for disaster recovery with Azure SQL Database are:

- [Active geo-replication](active-geo-replication-overview.md) lets you create a continuously synchronized readable secondary database in any region for a primary database.
- [Failover groups](failover-group-sql-db.md#terminology-and-capabilities), in addition to providing continuous synchronization between a primary and secondary database, also allow you to manage the replication and failover of some, or all, databases on a logical server to a secondary logical server in another region. Failover groups provide read-write and read-only listener endpoints that remain unchanged so updating application connection strings after failover isn't necessary. 
- [Geo-restore](recovery-using-backups.md#geo-restore) allows you to recover from a regional outage by restoring from geo replicated backups when you can't access your database in the primary region by creating a new database on any existing server in any Azure region.

The following table compares active geo-replication and failover groups, two disaster recovery options for Azure SQL Database:

|  | Active Geo-replication | Failover groups |
|:--|:--|:--|
| **Continuous data synchronization between primary and secondary** | Yes | Yes |
| **Fail over multiple databases simultaneously** | No | Yes |
| **Connection string remains unchanged after failover** | No | Yes |
| **Supports read-scale** | Yes | Yes |
| **Multiple replicas** | Yes | No |
| **Can be in same region as primary** | Yes | No |

## Features that provide business continuity

From a database perspective, there are four major potential disruption scenarios. The following table lists SQL Database business continuity features you can use to mitigate a potential business disruption scenario:  

| Business disruption scenario | Business continuity feature |
|:--|:--|
| Local hardware or software failures affecting the database node. | To mitigate local hardware and software failures, SQL Database includes an [availability architecture](high-availability-sla.md), which guarantees automatic recovery from these failures with up to 99.99% availability SLA. |
| Data corruption or deletion typically caused by an application bug or human error. Such failures are application-specific and typically can't be detected by the database service. | To protect your business from data loss, SQL Database automatically creates full database backups weekly, differential database backups every 12 or 24 hours, and transaction log backups every 5 - 10 minutes. By default, backups are stored in [geo-redundant storage](automated-backups-overview.md#backup-storage-redundancy) for seven days for all service tiers. All service tiers except Basic support a configurable backup retention period for [point-in-time restore (PITR)](recovery-using-backups.md#point-in-time-restore) of up to 35 days. You can [restore a deleted database](recovery-using-backups.md#deleted-database-restore) to the point at which it was deleted if the server hasn't been deleted, or if you've configured [long-term retention (LTR)](long-term-retention-overview.md). |
| Rare datacenter or availability zone outage, possibly caused by a natural disaster event, configuration change, software bug or hardware component failure. | To mitigate datacenter or availability zone level outage, enable [zone redundancy](high-availability-sla.md#zone-redundant-availability) for the database or elastic pool to use [Azure Availability Zones](/azure/reliability/availability-zones-overview) and provide redundancy across multiple physical zones within an Azure region. Enabling zone redundancy ensures the database or elastic pool is resilient to zonal failures with up to 99.995% high availability SLA. |
| Rare _regional outage_ impacting all availability zones and the datacenters comprising it, possibly caused by catastrophic natural disaster event. | To mitigate a region-wide outage, enable disaster recovery using one of the options: </br> - Continuous data synchronization options like [failover groups (recommended)](failover-group-sql-db.md) or [active geo-replication](active-geo-replication-overview.md) that allow you to create replicas in a secondary region for failover. </br> - Setting backup storage redundancy to geo-redundant backup storage to use [geo-restore](recovery-using-backups.md#geo-restore).  | 


## RTO and RPO

As you develop your business continuity plan, understand the maximum acceptable time before the application fully recovers after the disruptive event. The time required for an application to fully recover is known as the Recovery Time Objective (RTO). Also understand the maximum period of recent data updates (time interval) the application can tolerate losing when recovering from an unplanned disruptive event. The potential data loss is known as Recovery Point Objective (RPO).

The following table compares RPO and RTO of each business continuity option:

| **Business continuity option** | **RTO (downtime)** | **RPO (data loss)** |
| --- | --- | --- |
| High Availability </br>(Enabling zone redundancy) | Typically less than 30 seconds | 0 |
| Disaster Recovery </br>(Enabling failover groups or active geo-replication) | Typically less than 60 seconds | Equal to or greater than 0 </br> (Depends on data changes before the disruptive event that haven't been replicated) |
| Disaster Recovery </br>(Using geo-restore) | Typically minutes or hours | Typically minutes or hours |


## Business continuity checklists

For prescriptive recommendations to maximize availability and achieve higher business continuity, refer to the: 
- [Availability checklist](high-availability-disaster-recovery-checklist.md#availability-checklist)
- [High availability checklist](high-availability-disaster-recovery-checklist.md#high-availability-checklist)
- [Disaster recovery checklist](high-availability-disaster-recovery-checklist.md#disaster-recovery-checklist)

## Prepare for a region outage

Regardless of which business continuity features you use, you must prepare the secondary database in another region. If you don't prepare properly, bringing your applications online after a failover or recovery takes additional time and likely also requires troubleshooting, which can delay RTO. Follow the [checklist for preparing secondary for a region outage](high-availability-disaster-recovery-checklist.md#prepare-secondary-for-an-outage). 

## Restore a database within the same Azure region

You can use automatic database backups to restore a database to a point in time in the past. This way you can recover from data corruptions caused by human errors. Point-in-time restore (PITR) allows you to create a new database on the same server that represents the state of data prior to the corrupting event. For most databases, restore operations take less than 12 hours. It can take longer to recover a very large or very active database. For more information, see [database recovery time](recovery-using-backups.md#recovery-time).

If the maximum supported backup retention period for point-in-time restore isn't sufficient for your application, you can extend it by configuring a long-term retention (LTR) policy for the database(s). For more information, see [Long-term backup retention](long-term-retention-overview.md).

## Upgrade an application with minimal downtime

Sometimes an application must be taken offline because of maintenance such as an application upgrade. [Manage application upgrades](manage-application-rolling-upgrade.md) describes how to use active geo-replication to enable rolling upgrades of your cloud application to minimize downtime during upgrades and provide a recovery path if something goes wrong.

## Save on costs with a standby replica 

If your secondary replica is used _only_ for disaster recovery (DR) and doesn't have any read or write workloads, you can save on licensing costs by designating the database for standby when you configure a new active geo-replication relationship. 

Review [license-free standby replica](standby-replica-how-to-configure.md) to learn more. 

## Next steps

For application design considerations, see [Design an application for cloud disaster recovery](designing-cloud-solutions-for-disaster-recovery.md) and [Elastic pool disaster recovery strategies](disaster-recovery-strategies-for-applications-with-elastic-pool.md). 

Review the [Azure SQL Database disaster recovery guidance](disaster-recovery-guidance.md) and [Azure SQL Database high availability and disaster recovery checklist](high-availability-disaster-recovery-checklist.md).
