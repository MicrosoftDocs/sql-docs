---
title: Cloud Business Continuity - Disaster Recovery
titleSuffix: Azure SQL Database
description: Learn what to do when a user or application error affects data integrity, an Azure availability zone or region has an outage, or your application requires maintenance.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer:  mathoma
ms.date: 04/06/2025
ms.service: azure-sql-database
ms.subservice: high-availability
ms.topic: concept-article
ms.custom:
  - azure-sql-split
  - ignite-2023
  - ignite-2024
keywords:
  - "business continuity"
  - "cloud business continuity"
  - "database disaster recovery"
  - "database recovery"
monikerRange: "=azuresql || =azuresql-db "
---
# Business continuity in Azure SQL Database

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](business-continuity-high-availability-disaster-recover-hadr-overview.md?view=azuresql-db&preserve-view=true)
> * [Azure SQL Managed Instance](../managed-instance/business-continuity-high-availability-disaster-recover-hadr-overview.md?view=azuresql-mi&preserve-view=true)

**Business continuity** in Azure SQL Database refers to the mechanisms, policies, and procedures that enable your business to continue operating in the face of disruption by providing availability, high availability, and disaster recovery. 

<br/>

> [!VIDEO https://learn-video.azurefd.net/vod/player?show=data-exposed&ep=azure-sql-db-high-availability-disaster-recovery-overview-data-exposed]

For prescriptive recommendations to maximize availability and achieve higher business continuity, see: 

- [Availability checklist](high-availability-disaster-recovery-checklist.md#availability-checklist)
- [High availability checklist](high-availability-disaster-recovery-checklist.md#high-availability-checklist)
- [Disaster recovery checklist](high-availability-disaster-recovery-checklist.md#disaster-recovery-checklist)

In most cases, SQL Database handles disruptive events that might happen in a cloud environment and keeps your applications and business processes running. However, there are some disruptive events where mitigation might take some time, such as:

- User accidentally deletes or updates a row in a table.
- Malicious attacker successfully deletes data or drops a database. 
- Catastrophic natural disaster event takes down a datacenter or availability zone or region. 
- Rare datacenter, availability zone, or region-wide outage caused by a configuration change, software bug, or hardware failure.

## High Availability

Azure SQL Database comes with a core resiliency and reliability promise that protects it against software or hardware failures. Database backups are automated to protect your data from corruption or accidental deletion. As a Platform-as-a-service (PaaS), the Azure SQL Database service provides availability as an off-the-shelf feature with an [enterprise class availability SLA](https://www.microsoft.com/licensing/docs/view/Service-Level-Agreements-SLA-for-Online-Services?lang=1). 

To achieve high availability in the Azure cloud environment, enable [zone redundancy](high-availability-sla-local-zone-redundancy.md#zone-redundant-availability). With zone redundancy, the database or elastic pool uses [Azure availability zones](/azure/reliability/availability-zones-overview) to ensure resilience to zonal failures. 

- Many Azure regions provide availability zones, which are separated groups of data centers within a region that have independent power, cooling, and networking infrastructure. 
- Availability zones are designed to provide regional services, capacity, and high availability in the remaining zones if one zone experiences an outage. 

By enabling zone redundancy, the database or elastic pool is resilient to zonal hardware and software failures and the recovery is transparent to applications. When high availability is enabled, the Azure SQL Database service is able to provide a higher availability SLA. 

## Disaster recovery

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

## RTO and RPO

As you develop your business continuity plan, understand the maximum acceptable time before the application fully recovers after the disruptive event. Two common ways to quantify business requirements around disaster recovery are: 

- **Recovery Time Objective (RTO)**: The time required for an application to fully recover after an unplanned disruptive event. 
- **Recovery Point Objective (RPO)**: The time amount of data loss that can be tolerated from an unplanned disruptive event.

The following table compares RPO and RTO of each business continuity option:

| **Business continuity option** | **RTO (downtime)** | **RPO (data loss)** |
| --- | --- | --- |
| High Availability </br>(Using zone redundancy) | Typically less than 30 seconds | 0 |
| Disaster Recovery </br>(Using failover groups with [customer managed failover policy](failover-group-sql-db.md#failover-policy) or active geo-replication) | Typically less than 60 seconds | Equal to or greater than 0 </br> (Depends on data changes before the disruptive event that haven't been replicated) |
| Disaster Recovery </br>(Using geo-restore) | Typically minutes or hours, dependent on Azure storage replication | Typically minutes or hours, dependent on size of database backup |

## Features that provide business continuity

From a database perspective, there are four major potential disruption scenarios. The following table lists SQL Database business continuity features you can use to mitigate a potential business disruption scenario:  

| Business disruption scenario | Business continuity feature |
|:--|:--|
| Local hardware or software failures affecting the database node. | To mitigate local hardware and software failures, Azure SQL Database includes an [availability architecture](high-availability-sla-local-zone-redundancy.md), which guarantees automatic recovery from these failures with an [enterprise class SLA](https://www.microsoft.com/licensing/docs/view/Service-Level-Agreements-SLA-for-Online-Services?lang=1).|
| Data corruption or deletion typically caused by an application bug or human error. Such failures are application-specific and typically can't be detected by the database service. | To protect your business from data loss, SQL Database automatically creates full database backups weekly, differential database backups every 12 or 24 hours, and transaction log backups every 5 - 10 minutes. By default, backups are stored in [geo-redundant storage](automated-backups-overview.md#backup-storage-redundancy) for seven days for all service tiers. All service tiers except Basic support a configurable backup retention period for [point-in-time restore (PITR)](recovery-using-backups.md#point-in-time-restore) of up to 35 days. You can [restore a deleted database](recovery-using-backups.md#restore-deleted-database) to the point at which it was deleted if the server hasn't been deleted, or if you've configured [long-term retention (LTR)](long-term-retention-overview.md).|
| Rare datacenter or availability zone outage, possibly caused by a natural disaster event, configuration change, software bug, or hardware failure. | To mitigate datacenter or availability zone level outage, enable [zone redundancy](high-availability-sla-local-zone-redundancy.md#zone-redundant-availability) for the database or elastic pool to use [Azure Availability Zones](/azure/reliability/availability-zones-overview) and provide redundancy across multiple physical zones within an Azure region. Enabling zone redundancy ensures the database or elastic pool is resilient to zonal failures to increase beyond the default [enterprise class SLA](https://www.microsoft.com/licensing/docs/view/Service-Level-Agreements-SLA-for-Online-Services?lang=1).|
| Rare _regional outage_ affecting all availability zones and the datacenters comprising it, possibly caused by catastrophic natural disaster event. | To mitigate a region-wide outage, enable disaster recovery using one of the options: </br> - Continuous data synchronization options like [failover groups (recommended)](failover-group-sql-db.md) or [active geo-replication](active-geo-replication-overview.md) that allow you to create replicas in a secondary region for failover. </br> - Setting backup storage redundancy to geo-redundant backup storage to use [geo-restore](recovery-using-backups.md#geo-restore).| 


## Prepare for a region outage

Regardless of which business continuity features you use, you must prepare the secondary database in another region. If you don't prepare properly, bringing your applications online after a failover or recovery takes additional time and likely also requires troubleshooting, which can delay RTO. Follow the [checklist for preparing secondary for a region outage](high-availability-disaster-recovery-checklist.md#prepare-secondary-for-an-outage). 

## Restore a database within the same Azure region

You can use automatic database backups to restore a database to a point in time in the past. This way you can recover from data corruptions caused by human errors. Point-in-time restore (PITR) allows you to create a new database on the same server that represents the state of data before the corrupting event. For recovery times, see [RTO and RPO](#rto-and-rpo).

If the maximum supported backup retention period for point-in-time restore isn't sufficient for your application, you can extend it by configuring a long-term retention (LTR) policy. For more information, see [Long-term retention](long-term-retention-overview.md).

## Upgrade an application with minimal downtime

Sometimes an application must be taken offline because of maintenance such as an application upgrade. You can [manage rolling upgrades of cloud applications by using SQL Database active geo-replication](manage-application-rolling-upgrade.md). Geo-replication can also provide a recovery path if something goes wrong.

## Save on costs with a standby replica

If your secondary replica is used _only_ for disaster recovery (DR) and doesn't have any read or write workloads, you can save on licensing costs by designating the database for standby when you configure a new active geo-replication relationship. 

Review [license-free standby replica](standby-replica-how-to-configure.md) to learn more. 

## Next step

> [!div class="nextstepaction"]
> [High availability and disaster recovery checklist](high-availability-disaster-recovery-checklist.md)

## Related content

- [Designing globally available services using Azure SQL Database](designing-cloud-solutions-for-disaster-recovery.md)
- [Disaster recovery strategies for applications using Azure SQL Database elastic pools](disaster-recovery-strategies-for-applications-with-elastic-pool.md)
- [Disaster recovery guidance - Azure SQL Database](disaster-recovery-guidance.md)
- [High availability and disaster recovery checklist - Azure SQL Database](high-availability-disaster-recovery-checklist.md)
