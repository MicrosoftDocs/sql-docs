---
title: Cloud business continuity - disaster recovery
titleSuffix: Azure SQL Database
description: Learn how Azure SQL Database supports cloud business continuity and disaster recovery to help keep mission-critical cloud applications running.
author: rajeshsetlem
ms.author: rsetlem
ms.reviewer: wiassaf, mathoma
ms.date: 05/01/2023
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
[!INCLUDE[appliesto-sqldb-sqlmi](../includes/appliesto-sqldb.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](business-continuity-high-availability-disaster-recover-hadr-overview.md?view=azuresql-db&preserve-view=true)
> * [Azure SQL Managed Instance](../managed-instance/business-continuity-high-availability-disaster-recover-hadr-overview.md?view=azuresql-mi&preserve-view=true)


**Business continuity** in Azure SQL Database refers to the mechanisms, policies, and procedures that enable your business to continue operating in the face of disruption. In most cases, SQL Database handles the disruptive events that might happen in the cloud environment and keep your applications and business processes running. However, there are some disruptive events where mitigation might take some time such as:

- User accidentally deleted or updated a row in a table.
- Malicious attacker succeeded to delete data or drop a database.
- Catastrophic natural disaster event taking down a datacenter or availability zone or region. 
- Rare datacenter or availability zone or region wide outage caused by configuration change or software bug or hardware component failure.

This overview describes the capabilities that SQL Database provides for business continuity and disaster recovery. Learn about options, recommendations, and tutorials for recovering from disruptive events that could cause data loss or cause your database and application to become unavailable. Learn what to do when a user or application error affects data integrity, an Azure availability zone or region has an outage, or your application requires maintenance.

## Availability, High Availability & Disaster Recovery

### Availability

Every Azure SQL database comes with core resiliency and reliability promise that protects it against software or hardware failures. Database backups are automated to protect your data from corruption or accidental deletion. As a Platform As A Service (PaaS), Azure SQL database service provides availability as an off the shelf feature with an industry leading availability SLA of 99.99%. 

### High Availability

To achieve high availability in Azure cloud environment, enable [zone redundancy](high-availability-sla.md#zone-redundant-availability) for the SQL database or elastic pool to use [availability zones](/azure/reliability/availability-zones-overview) and ensure the database or elastic pool is resilient to zonal failures. Many Azure regions provide availability zones which are separated groups of data centers within a region. Availability zones have independent power, cooling, and networking infrastructure. They're designed so that if one zone experiences an outage, then regional services, capacity, and high availability are supported by the remaining zones. By enabling zone redundancy the database or elastic pool is resilient to zonal hardware and software failures and the recovery is transparent to applications.When high availability is enabled, Azure SQL database service provides an higher availability SLA of 99.995%. 

### Disaster recovery

To take business continuity for the database one notch higher than high availability and achieve redundancy across regions enable disaster recovery to quickly recover the database from a catastrophic regional failure. Options for disaster recovery are:
- [Active geo-replication](active-geo-replication-overview.md)
</br> Active geo-replication is a feature that lets you create a continuously synchronized readable secondary database in any region for a primary database.
- [Failover groups](auto-failover-group-sql-db.md#terminology-and-capabilities) (Recommended)
</br> Failover groups feature uses active geo-replication under the hood for continuos synchronization and allows you to manage the replication and failover of some or all databases on a logical server to a logical server in another region. Failover groups provide read-write and read-only listener end-points that remain unchanged in the event of failovers. 
- [Geo restore](recovery-using-backups.md#geo-restore)
</br> Geo-restore allows you to recover from a regional outage by restoring from geo replicated backups when you can't access your database in the primary region. It creates a new database on any existing server in any Azure region.

Active geo-replication and  failover groups are two continuous data synchronization disaster recovery options for Azure SQL database. Failover groups is the recommended option as it simplifies the deployment and management and add additional capabilities as described in the following table:

|  | Active Geo-replication | Failover groups |
|:--|:--|:--|
| **Fail over multiple databases simultaneously** | No | Yes |
| **Connection string remain unchanged after failover** | No | Yes |
| **Supports read-scale** | Yes | Yes |
| **Multiple replicas** | Yes | No |
| **Can be in same region as primary** | Yes | No |

## Features that provide business continuity

From a database perspective, there are four major potential disruption scenarios. Following table list the scenarios and the SQL database business continuity feature you can use to mitigate:

| Scenario | Business continuity feature |
|:--|:--|
| Local hardware or software failures affecting the database node. | To mitigate the local hardware and software failures, SQL Database includes a [availability architecture](high-availability-sla.md), which guarantees automatic recovery from these failures with up to 99.99% availability SLA. |
| Data corruption or deletion typically caused by an application bug or human error. Such failures are application-specific and typically cannot be detected by the database service. | To protect your business from data loss, SQL Database automatically creates full database backups weekly, differential database backups every 12 or 24 hours, and transaction log backups every 5 - 10 minutes. By default, backups are stored in [geo redundant storage](automated-backups-overview.md#backup-storage-redundancy) for seven days for all service tiers. All service tiers except Basic support configurable backup retention period for [Point in Time Restore](recovery-using-backups.md#point-in-time-restore) restore of up to 35 days. You can [restore a deleted database](recovery-using-backups.md#deleted-database-restore) to the point at which it was deleted if the server has not been deleted. |
| Rare datacenter or availability zone outage, possibly caused by natural disaster event or configuration change or software bug or hardware component failure. | To mitigate datacenter or availability zone level outage, enable [zone redundancy](high-availability-sla.md#zone-redundant-availability) for the SQL database or elastic pool which uses [Azure Availability Zones](/azure/reliability/availability-zones-overview) to provide redundancy across multiple physical zones within an Azure region. Enabling zone redundancy ensures the SQL database o elastic pool is resilient to zonal failures with up to 99.995% high availability SLA. |
| Rare region outage impacting all Availability Zones and the datacenters comprising it, possibly caused by catastrophic natural disaster event or configuration change or software bug or hardware component failure. | To mitigate region wide outage, enable disaster recovery using one of the options: </br> - Continuos data synchronization options like [Failover groups (recommended)](auto-failover-group-sql-db.md) or [Active geo-replication](active-geo-replication-overview.md) that allows you to create replicas in secondary region that can be used for failover. </br> - Setting backup storage redundancy to Geo-redundant backup storage to use [geo-restore](recovery-using-backups.md#geo-restore).  | 


## Recovery Time Objective (RTO) and Recovery Point Objective (RPO)

As you develop your business continuity plan, you need to understand the maximum acceptable time before the application fully recovers after the disruptive event. The time required for an application to fully recover is known as the Recovery Time Objective (RTO). You also need to understand the maximum period of recent data updates (time interval) the application can tolerate losing when recovering from an unplanned disruptive event. The potential data loss is known as Recovery Point Objective (RPO).

The following table compares RPO and RTO of each business continuity option:

| **Business continuity option** | **RTO (downtime)** | **RPO (dataloss)** |
| --- | --- | --- |
| High Availability </br>(Enabling zone redundancy) | Typically less than 30 seconds | 0 |
| Disaster Recovery </br>(Enabling Failover groups or Active geo-replication) | Typically less than 60 seconds | Equal to or greater than 0 </br> (Depends on data changes before the disruptive event that haven't been replicated) |
| Disaster Recovery </br>(Using geo-restore) | Typically minutes or hours | Typically minutes or hours |


## Availability & High Availability & Disaster Recovery checklist

For a prescriptive recommendation to maximize availability and achieve higher business continuity, refer to [Availability checklist](high-availability-disaster-recovery-checklist.md#availability-checklist), [High Availability checklist](high-availability-disaster-recovery-checklist.md#high-availability-checklist) and [Disaster Recovery checklist](high-availability-disaster-recovery-checklist.md#disaster-recovery-checklist). 

## Prepare for a region outage

Regardless of business continuity feature you use, you must prepare the secondary database in another region. If you do not prepare properly, bringing your applications online after a failover or recovery takes additional time and likely also requires troubleshooting delaying RTO. Follow the [checklist for preparing secondary for a region outage](high-availability-disaster-recovery-checklist.md#checklist-for-preparing-secondary-for-an-outage). 

## Restore a database within the same Azure region

You can use automatic database backups to restore a database to a point in time in the past. This way you can recover from data corruptions caused by human errors. Point-in-time restore allows you to create a new database in the same server that represents the state of data prior to the corrupting event. For most databases, restore operations take less than 12 hours. It may take longer to recover a very large or very active database. For more information about recovery time, see [database recovery time](recovery-using-backups.md#recovery-time).

If the maximum supported backup retention period for point-in-time restore (PITR) is not sufficient for your application, you can extend it by configuring a long-term retention (LTR) policy for the database(s). For more information, see [Long-term backup retention](long-term-retention-overview.md).


## Upgrade an application with minimal downtime

Sometimes an application must be taken offline because of maintenance such as an application upgrade. [Manage application upgrades](manage-application-rolling-upgrade.md) describes how to use active geo-replication to enable rolling upgrades of your cloud application to minimize downtime during upgrades and provide a recovery path if something goes wrong.

## Next steps

For a discussion of application design considerations for single databases and for elastic pools, see [Design an application for cloud disaster recovery](designing-cloud-solutions-for-disaster-recovery.md) and [Elastic pool disaster recovery strategies](disaster-recovery-strategies-for-applications-with-elastic-pool.md). 

Save on licensing costs by designating your secondary disaster recovery replica for [standby](standby-replica-how-to-configure.md).

Review the [Azure SQL Database disaster recovery guidance](disaster-recovery-guidance.md) and [Azure SQL Database high availability and disaster recovery checklist](high-availability-disaster-recovery-checklist.md).
