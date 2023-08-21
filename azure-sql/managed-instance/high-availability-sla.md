---
title: High availability
titleSuffix: Azure SQL Managed Instance
description: Learn about the Azure SQL Managed Instance service high availability capabilities and features.
author: Stralle
ms.author: strrodic
ms.reviewer: mathoma, randolphwest
ms.date: 05/01/2023
ms.service: sql-managed-instance
ms.subservice: high-availability
ms.topic: conceptual
ms.custom:
  - "references_regions"
  - "azure-sql-split"
monikerRange: "= azuresql || = azuresql-mi"
---

# High availability for Azure SQL Managed Instance
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](../database/high-availability-sla.md?view=azuresql-db&preserve-view=true)
> * [Azure SQL Managed Instance](high-availability-sla.md?view=azuresql-mi&preserve-view=true)

This article describes high availability in Azure SQL Managed Instance.

Zone-redundant configuration is currently in preview for SQL Managed Instance, and only available for the Business Critical service tier.

## Overview

The goal of the high availability architecture in Azure SQL Managed Instance is to minimize impact on customer workloads from service maintenance operations and outages. For more information regarding specific SLAs for different service tiers, review [Azure SQL Managed Instance](https://azure.microsoft.com/support/legal/sla/azure-sql-sql-managed-instance/).

SQL Managed Instance runs on the latest stable version of the SQL Server Database Engine on the Windows operating system with all applicable patches. SQL Managed Instance automatically handles critical servicing tasks, such as patching, backups, Windows and SQL engine upgrades, and unplanned events such as underlying hardware, software, or network failures. When an instance is patched or fails over, the downtime isn't impactful if you [employ retry logic](../database/develop-overview.md#resiliency) in your app. SQL Managed Instance can quickly recover even in the most critical circumstances, ensuring that your data is always available. Most users do not notice that upgrades are performed continuously.

The high availability solution is designed to ensure that committed data is never lost due to failures, that maintenance operations don't affect your workload, and that the instance won't be a single point of failure in your software architecture.

There are two high availability architectural models:

- **Remote storage model** that is based on a separation of compute and storage.  It relies on high availability and reliability of the remote storage tier. This architecture targets budget-oriented business applications that can tolerate some performance degradation during maintenance activities.
- **Local storage model** that is based on a cluster of database engine processes. It relies on the fact that there is always a quorum of available database engine nodes. This architecture targets mission-critical applications with high IO performance, high transaction rate and guarantees minimal performance impact on your workload during maintenance activities.


## Locally redundant availability 

Locally redundant availability is based on storing your compute node and data on [locally redundant storage (LRS)](/azure/storage/common/storage-redundancy#locally-redundant-storage) which copies your data three times within a single datacenter in the primary region and protects your data in the event of local failure, such as a small-scale network or power failure. LRS is the lowest-cost redundancy option and offers the least durability compared to other options. If a large-scale disaster such as fire or flooding occurs within a region, all replicas of a storage account using LRS may be lost or unrecoverable. As such, to further protect your data when using the locally redundant availability option, consider using a more resilient storage option for your [database backups](automated-backups-overview.md#backup-storage-redundancy). 

Locally redundant availability is available to instances in either service tier. 

### General Purpose service tier

The General Purpose service tier uses the remote storage availability architecture. The following figure shows four different nodes with the separated compute and storage layers.

:::image type="content" source="../database/media/high-availability-sla/general-purpose-service-tier.png" alt-text="Diagram showing separation of compute and storage.":::

The remote storage availability model includes two layers:

- A stateless compute layer that runs the database engine process and contains only transient and cached data, such as the `tempdb` and `model` databases on the attached SSD, and plan cache, buffer pool, and columnstore pool in memory. This stateless node is operated by Azure Service Fabric that initializes database engine, controls health of the node, and performs failover to another node if necessary.
- A stateful data layer with the database files (`.mdf` and `.ldf`) stored in Azure Blob Storage. Azure Blob Storage has built-in data availability and redundancy features. It guarantees that every record in the log file or page in the data file will be preserved even if database engine process crashes.

Whenever the database engine or the operating system is upgraded, or a failure is detected, Azure Service Fabric will move the stateless database engine process to another stateless compute node with sufficient free capacity. Data in Azure Blob storage isn't affected by the move, and the data/log files are attached to the newly initialized database engine process. This process guarantees high availability, but a heavy workload may experience some performance degradation during the transition since the new database engine process starts with cold cache.

### Business Critical service tier 

The Business Critical service tier uses the local storage availability model, which integrates compute resources (database engine process) and storage (locally attached SSD) on a single node. High availability is achieved by replicating both compute and storage to additional nodes. 

:::image type="content" source="../database/media/high-availability-sla/business-critical-service-tier.png" alt-text="Diagram of a cluster of database engine nodes.":::

The underlying database files (.mdf/.ldf) are placed on the attached SSD storage to provide very low latency IO to your workload. High availability is implemented using a technology similar to SQL Server [Always On availability groups](/sql/database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server). The cluster includes a single primary replica that is accessible for read-write customer workloads, and up to three secondary replicas (compute and storage) containing copies of data. The primary replica constantly pushes changes to the secondary replicas in order and ensures that the data is persisted on a sufficient number of secondary replicas before committing each transaction. This process guarantees that if the primary replica or a readable secondary replica crash for any reason, there is always a fully synchronized replica to fail over to. The failover is initiated by the Azure Service Fabric. Once a secondary replica becomes the new primary replica, another secondary replica is created to ensure the cluster has a sufficient number of replicas to maintain quorum. Once a failover is complete, Azure SQL connections are automatically redirected to the new primary replica or readable secondary replica.

As an extra benefit, the local storage availability model includes the ability to redirect read-only Azure SQL connections to one of the secondary replicas. This feature is called [Read Scale-Out](../database/read-scale-out.md). It provides 100% additional compute capacity at no extra charge to off-load read-only operations, such as analytical workloads, from the primary replica.


## Zone-redundant availability

Zone-redundant configuration is currently in preview for SQL Managed Instance, and only available for the Business Critical service tier. 

Zone-redundant availability is based on storing your compute node and data to [zone-redundant storage (ZRS)](/azure/storage/common/storage-redundancy#zone-redundant-storage), which copies your data across three Azure availability zones in the primary region. Each availability zone is a separate physical location with independent power, cooling, and networking.

By default, the cluster of nodes for the local storage availability model is created in the same datacenter. With the introduction of [Azure Availability Zones](/azure/availability-zones/az-overview), SQL Managed Instance can place different replicas of a Business Critical instance in different availability zones in the same region. To eliminate a single point of failure, the control ring is also duplicated across multiple zones as three gateway rings (GW). The routing to a specific gateway ring is controlled by [Azure Traffic Manager](/azure/traffic-manager/traffic-manager-overview) (ATM). Because the zone-redundant configuration in the Business Critical service tier doesn't create additional database redundancy, you can enable it at no extra cost. By selecting a zone-redundant configuration, you can make your Business Critical instances resilient to a much larger set of failures, including catastrophic datacenter outages, without any changes to the application logic. You can also convert any existing Business Critical instances to zone-redundant configuration.

Because zone-redundant instances have replicas in different datacenters with some distance between them, the increased network latency may increase the transaction commit time, and thus impact the performance of some OLTP workloads. You can always return to the single-zone configuration by disabling the zone-redundancy setting. This process is an online operation similar to the regular service tier objective upgrade. At the end of the process, the instance is migrated from a zone-redundant ring to a single-zone ring or vice versa.

The zone-redundant version of the high availability architecture is illustrated by the following diagram:

:::image type="content" source="../database/media/high-availability-sla/zone-redundant-business-critical-service-tier.png" alt-text="Diagram of the zone-redundant high availability architecture.":::


Consider the following when using zone-redundancy: 

- During preview, zone redundancy for SQL Managed Instance is available in the Business Critical service tier. 
- For up to date information about the regions that support zone-redundant instances, see [Services support by region](/azure/availability-zones/az-region).
- For zone redundant availability, choosing a [maintenance window](../database/maintenance-window.md) other than the default is currently available in [select regions](../database/maintenance-window.md?preserve-view=true&view=azuresql#azure-sql-managed-instance-region-support-for-maintenance-windows).

During preview, zone redundancy for SQL Managed Instance is available in the Business Critical service tier and supported in the following regions:

| Americas | Europe | Middle East | Africa | Asia Pacific |
|---|---|---|---|---|
| Brazil South | North Europe | Qatar Central | South Africa North | Australia East |
| Canada Central | Norway East | UAE North | | Central India |
| Central US | UK South | | | Japan East |
| East US | West Europe | | | Korea Central |
| East US 2 | Sweden Central | | | East Asia |
| South Central US | Switzerland North | | | |
| West US 2 | | | | |
| West US 3 | | | | |



## <a id="testing-application-fault-resiliency"></a> Test application fault resiliency

High availability is a fundamental part of the SQL Managed Instance platform that works transparently for your database application. However, we recognize that you may want to test how the automatic failover operations initiated during planned or unplanned events would impact an application before you deploy it to production. You can manually trigger a failover by calling a special API to restart a managed instance. In the case of a zone-redundant instance, the API call would result in redirecting client connections to the new primary in an Availability Zone different from the Availability Zone of the old primary. So in addition to testing how failover impacts existing database sessions, you can also verify if it changes the end-to-end performance due to changes in network latency. Because the restart operation is intrusive and a large number of them could stress the platform, only one failover call is allowed every 15 minutes for each managed instance.

A failover can be initiated using PowerShell, REST API, or Azure CLI:

| PowerShell | REST API | Azure CLI |
 :--- | :--- | :--- |
 [Invoke-AzSqlInstanceFailover](/powershell/module/az.sql/Invoke-AzSqlInstanceFailover/) | [SQL Managed Instance - Failover](/rest/api/sql/managed%20instances%20-%20failover/failover) | [az sql mi failover](/cli/azure/sql/mi/#az-sql-mi-failover) may be used to invoke a REST API call from Azure CLI |


## Conclusion

Azure SQL Managed Instance features a built-in high availability solution that is deeply integrated with the Azure platform. It is dependent on Service Fabric for failure detection and recovery, on Azure Blob storage for data protection, and on Availability Zones for higher fault tolerance (currently in Preview for the Business Critical service tier). In addition,  SQL Managed Instance uses the Always On availability group technology from the SQL Server instance for replication and failover. The combination of these technologies enables applications to fully realize the benefits of a mixed storage model and supports the most demanding SLAs.

## Next steps

- Learn about [Azure Availability Zones](/azure/availability-zones/az-overview)
- Learn about [Service Fabric](/azure/service-fabric/service-fabric-overview)
- Learn about [Azure Traffic Manager](/azure/traffic-manager/traffic-manager-overview)
- Learn [How to initiate a manual failover on SQL Managed Instance](user-initiated-failover.md)
- For more options for high availability and disaster recovery, see [Business Continuity](business-continuity-high-availability-disaster-recover-hadr-overview.md)
