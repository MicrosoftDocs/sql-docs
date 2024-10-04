---
title: Cloud business continuity - disaster recovery
titleSuffix: Azure SQL Managed Instance
description: Learn how Azure SQL Managed Instance supports cloud business continuity and disaster recovery to help keep mission-critical cloud applications running.
author: Stralle
ms.author: strrodic
ms.reviewer: mathoma
ms.date: 12/28/2023
ms.service: azure-sql-managed-instance
ms.subservice: high-availability
ms.topic: conceptual
ms.custom: azure-sql-split, ignite-2023
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

This article provides an overview of the business continuity and disaster recovery capabilities of Azure SQL Managed Instance, describing the options and recommendations for recovering from disruptive events that could lead to data loss or cause your instance and application to become unavailable. Learn what to do when a user or application error affects data integrity, an Azure availability zone or region has an outage, or your application requires maintenance.

## Overview

**Business continuity** in Azure SQL Managed Instance refers to the mechanisms, policies, and procedures that enable your business to continue operating in the face of disruption by providing availability, high availability, and disaster recovery. 

In most cases, SQL Managed Instance handles disruptive events that might happen in a cloud environment and keeps your applications and business processes running. However, there are some disruptive events where mitigation might take some time, such as:

- User accidentally deletes or updates a row in a table.
- Malicious attacker successfully deletes data or drops a database. 
- Catastrophic natural disaster event takes down a datacenter or availability zone or region. 
- Rare datacenter, availability zone or region-wide outage caused by a configuration change, software bug or hardware component failure.

### Availability

Azure SQL Managed Instance comes with a core resiliency and reliability promise that protects it against software or hardware failures. Database backups are automated to protect your data from corruption or accidental deletion. As a Platform-as-a-service (PaaS), the Azure SQL Managed Instance service provides availability as an off-the-shelf feature with an industry-leading availability SLA of 99.99%. 

### High Availability

To achieve high availability in the Azure cloud environment, enable [zone redundancy](high-availability-sla-local-zone-redundancy.md#zone-redundant-availability) so the instance uses [availability zones](/azure/reliability/availability-zones-overview) to ensure resilience to zonal failures. Many Azure regions provide availability zones, which are separated groups of data centers within a region that have independent power, cooling, and networking infrastructure. Availability zones are designed to provide regional services, capacity, and high availability in the remaining zones if one zone experiences an outage. By enabling zone redundancy, the instance is resilient to zonal hardware and software failures and the recovery is transparent to applications. When high availability is enabled, the Azure SQL Managed Instance service is able to provide a higher availability SLA of 99.99%. 

### Disaster recovery

To achieve higher availability and redundancy across regions, you can enable disaster recovery capabilities to quickly recover the instance from a catastrophic regional failure. Options for disaster recovery with Azure SQL Managed Instance are:

- [Failover groups](failover-group-sql-mi.md#terminology-and-capabilities) enable continuous synchronization between a primary and secondary instance. Failover groups provide read-write and read-only listener endpoints that remain unchanged so updating application connection strings after failover isn't necessary. 
- [Geo-restore](recovery-using-backups.md#geo-restore) allows you to recover from a regional outage by restoring from geo replicated backups when you can't access your database in the primary region by creating a new database on any existing instance in any Azure region.

## Features that provide business continuity

For an instance, there are four major potential disruption scenarios. The following table lists SQL Managed Instance business continuity features you can use to mitigate a potential business disruption scenario:  

| Business disruption scenario | Business continuity feature |
|:--|:--|
| Local hardware or software failures affecting the database node. | To mitigate local hardware and software failures, SQL Managed Instance includes an [availability architecture](high-availability-sla-local-zone-redundancy.md), which guarantees automatic recovery from these failures with up to 99.99% availability SLA. |
| Data corruption or deletion typically caused by an application bug or human error. Such failures are application-specific and typically can't be detected by the service. | To protect your business from data loss, SQL Managed Instance automatically creates full database backups weekly, differential database backups every 12 or 24 hours, and transaction log backups every 5 - 10 minutes. By default, backups are stored in [geo-redundant storage](automated-backups-overview.md#backup-storage-redundancy) for seven days, and support a configurable backup retention period for [point-in-time restore](recovery-using-backups.md#point-in-time-restore) of up to 35 days. You can [restore a deleted database](recovery-using-backups.md#deleted-database-restore) to the point at which it was deleted if the instance hasn't been deleted, or if you've configured [long-term retention](../database/long-term-retention-overview.md). |
| Rare datacenter or availability zone outage, possibly caused by a natural disaster event, configuration change, software bug or hardware component failure. | To mitigate datacenter or availability zone level outage, enable [zone redundancy](high-availability-sla-local-zone-redundancy.md#zone-redundant-availability) for the SQL Managed Instance to use [Azure Availability Zones](/azure/reliability/availability-zones-overview) and provide redundancy across multiple physical zones within an Azure region. Enabling zone redundancy ensures the managed instance is resilient to zonal failures with up to 99.99% high availability SLA. |
| Rare region outage impacting all availability zones and the datacenters comprising it, possibly caused by catastrophic natural disaster event. | To mitigate a region-wide outage, enable disaster recovery using one of the options: <br /> - Continuous data synchronization with [failover groups](failover-group-sql-mi.md) to replicas in a secondary region used for failover. <br /> - Setting backup storage redundancy to geo-redundant backup storage to use [geo-restore](recovery-using-backups.md#geo-restore).  | 

## RTO and RPO

As you develop your business continuity plan, understand the maximum acceptable time before the application fully recovers after the disruptive event. The time required for an application to fully recover is known as the Recovery Time Objective (RTO). Also understand the maximum period of recent data updates (time interval) the application can tolerate losing when recovering from an unplanned disruptive event. The potential data loss is known as Recovery Point Objective (RPO).

The following table compares RPO and RTO of each business continuity option:

| **Business continuity option** | **RTO (downtime)** | **RPO (data loss)** |
| --- | --- | --- |
| High Availability </br>(Using zone redundancy) | Typically less than 30 seconds | 0 |
| Disaster Recovery </br>(Using failover groups with [customer managed failover policy](failover-group-sql-mi.md#failover-policy)) | Typically less than 60 seconds | Equal to or greater than 0 </br> (Depends on data changes before the disruptive event that haven't been replicated) |
| Disaster Recovery </br>(Using geo-restore) | 12 hours | 1 hour|

## Business continuity checklists

For prescriptive recommendations to maximize availability and achieve higher business continuity, refer to the: 
- [Availability checklist](high-availability-disaster-recovery-checklist.md#availability-checklist)
- [High availability checklist](high-availability-disaster-recovery-checklist.md#high-availability-checklist)
- [Disaster recovery checklist](high-availability-disaster-recovery-checklist.md#disaster-recovery-checklist)

## Recover a database within the same Azure region

You can use automatic database backups to restore a database to a point in time in the past. This way you can recover from data corruptions caused by human errors. Point-in-time restore (PITR) allows you to create a new database to the same instance, or a different instance, that represents the state of data prior to the corrupting event. The restore operation is a size of data operation that also depends on the current workload of the target instance. It might take longer to recover a very large or very active database. For more information about recovery time, see [database recovery time](recovery-using-backups.md#recovery-time).

If the maximum supported backup retention period for point-in-time restore (PITR) isn't sufficient for your application, you can extend it by configuring a long-term retention (LTR) policy for the database(s). For more information, see [Long-term backup retention](../database/long-term-retention-overview.md).

## Recover a database to an existing instance

Although rare, an Azure datacenter can have an outage. When an outage occurs, it causes a business disruption that might only last a few minutes or might last for hours.

- One option is to wait for your instance to come back online when the datacenter outage is over. This works for applications that can afford to have their database offline. For example, a development project or free trial you don't need to work on constantly. When a datacenter has an outage, you don't know how long the outage might last, so this option only works if you don't need your database for some time. 
- If you're using geo-redundant (GRS), or geo-zone-redundant (GZRS) storage, another option is to restore a database to any SQL managed instance in any Azure region using [geo-redundant database backups](recovery-using-backups.md#geo-restore) (geo-restore). Geo-restore uses a geo-redundant backup as its source and can be used to recover a database to the last available point in time, even if the database or datacenter is inaccessible due to an outage. The available backup can be found in the paired region. 
- Finally, you can quickly recover from an outage if you've configured a geo-secondary using a [failover group](failover-group-sql-mi.md) for your instance, using either customer (recommended) or Microsoft-managed failover. While the failover itself takes only a few seconds, the service takes at least 1 hour to activate a Microsoft-managed geo-failover, if configured. This is necessary to ensure the failover is justified by the scale of the outage. Also, the failover might result in the loss of recently changed data due to the nature of asynchronous replication between the paired regions.

As you develop your business continuity plan, you need to understand the maximum acceptable time before the application fully recovers after the disruptive event. The time required for application to fully recover is known as Recovery Time Objective (RTO). You also need to understand the maximum period of recent data updates (time interval) the application can tolerate losing when recovering from an unplanned disruptive event. The potential data loss is known as Recovery Point Objective (RPO).

Different recovery methods offer different levels of RPO and RTO. You can choose a specific recovery method, or use a combination of methods to achieve full application recovery. 

Use failover groups if your application meets any of these criteria:

- Is mission critical.
- Has a service level agreement (SLA) that doesn't allow for 12 hours or more of downtime.
- Downtime might result in financial liability.
- Has a high rate of data change and 1 hour of data loss isn't acceptable.
- The additional cost of active geo-replication is lower than the potential financial liability and associated loss of business.

You might choose to use a combination of database backups and failover groups depending upon your application requirements. 

The following sections provide an overview of the steps to recover using either database backups or failover groups. 

### Prepare for an outage

Regardless of the business continuity feature you use, you must:

- Identify and prepare the target instance, including network IP firewall rules, logins, and `master` database level permissions.
- Determine how to redirect clients and client applications to the new instance
- Document other dependencies, such as auditing settings and alerts

If you don't prepare properly, bringing your applications online after a failover or a database recovery takes additional time, and likely also requires troubleshooting at a time of stress - a bad combination.

### Fail over to a geo-replicated secondary instance

If you're using failover groups as your recovery mechanism, you can configure an automatic failover policy. Once initiated, the failover causes the secondary instance to become the new primary, ready to record new transactions and respond to queries - with minimal data loss for the data not yet replicated. 

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
> If you are using a failover group and connect to the instance using the read-write listener, the redirection after failover will happen automatically and transparently to the application.

## License-free DR replicas 

You can save on licensing costs by configuring a secondary Azure SQL Managed Instance for only disaster recovery (DR). This benefit is available if you're using a failover group between two SQL managed instances, or you've configured a hybrid link between SQL Server and Azure SQL Managed Instance. As long as the secondary instance doesn't have any read or write workloads on it  and is only a passive DR standby, you aren't charged for the vCore licensing costs used by the secondary instance. 

When you designate a secondary instance for only disaster recovery, and no read or write workloads are running on the instance, Microsoft provides you with the number of vCores that are licensed to the primary instance at no extra charge under the failover rights benefit. You're still billed for the compute and storage that the secondary instance uses. For precise terms and conditions of the Hybrid failover rights benefit, see the SQL Server licensing terms online in the [“SQL Server – Fail-over Rights”](https://www.microsoft.com/licensing/terms/productoffering/SQLServer/EAEAS) section.

The name for the benefit depends on your scenario: 

- [Hybrid failover rights for a passive replica](managed-instance-link-feature-overview.md#license-free-passive-dr-replica): When you configure a [link](managed-instance-link-feature-overview.md) between SQL Server and Azure SQL Managed Instance, you can use the **Hybrid failover rights** benefit to save on vCore licensing costs for the passive secondary replica. 
- [Failover rights for a standby replica](failover-group-standby-replica-how-to-configure.md): When you configure a failover group between two managed instances, you can use the **Failover rights** benefit to save on vCore licensing costs for the standby secondary replica. 

The following diagram demonstrates the benefit for each scenario: 

:::image type="content" source="media/business-continuity-high-availability-disaster-recover-hadr-overview/failover-rights-diagram.png" alt-text="Diagram comparing the failover rights for Azure SQL Managed Instance. ":::

## Next steps

To learn more about business continuity features, see [Automated backups](automated-backups-overview.md), and [failover groups](failover-group-sql-mi.md#terminology-and-capabilities). For disaster recovery, see [recover a database](recovery-using-backups.md) and [enable zone redundancy](instance-zone-redundancy-configure.md) for Azure SQL Managed Instance. 
