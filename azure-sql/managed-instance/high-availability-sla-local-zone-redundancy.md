---
title: Availability through local and zone redundancy
titleSuffix: Azure SQL Managed Instance
description: Learn about the architecture of Azure SQL Managed Instance that achieves availability through local redundancy, and high availability through zone redundancy. 
author: Stralle
ms.author: strrodic
ms.reviewer: mathoma, randolphwest
ms.date: 06/25/2024
ms.service: azure-sql-managed-instance
ms.subservice: high-availability
ms.topic: conceptual
ms.custom: references_regions, azure-sql-split, ignite-2023
monikerRange: "= azuresql || = azuresql-mi"
---

# Availability through local and zone redundancy - Azure SQL Managed Instance
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](../database/high-availability-sla-local-zone-redundancy.md?view=azuresql-db&preserve-view=true)
> * [Azure SQL Managed Instance](high-availability-sla-local-zone-redundancy.md?view=azuresql-mi&preserve-view=true)

This article describes the architecture of Azure SQL Managed Instance that achieves availability through local redundancy, and high availability through zone redundancy. 

> [!IMPORTANT]
> Zone-redundant configuration is in public preview for the General Purpose service tier and generally available for the Business Critical service tier.

## Overview

SQL Managed Instance runs on the latest stable version of the SQL Server database engine on the Windows operating system with all applicable patches. SQL Managed Instance automatically handles critical servicing tasks, such as patching, backups, Windows and SQL database engine upgrades, and unplanned events such as underlying hardware, software, or network failures. When an instance is patched or fails over, the downtime isn't impactful if you [employ retry logic](../database/develop-overview.md#resiliency) in your app. SQL Managed Instance can quickly recover even in the most critical circumstances, ensuring that your data is always available. Most users don't notice that upgrades are performed continuously.

By default, Azure SQL Managed Instance achieves *availability* through local redundancy, making your instance available during:

- Customer initiated [management operations](management-operations-overview.md) that result in a brief downtime
- Service maintenance operations
- Issues and datacenter outages with the:
    - rack where the machines that power your service are running
    - physical machine that hosts the VM that runs the SQL database engine
    - virtual machine that runs the SQL database engine
- Other problems with the SQL database engine
- Other potential unplanned local outages

The default availability solution is designed to ensure that committed data is never lost due to failures, that maintenance operations don't affect your workload, and that the instance isn't a single point of failure in your software architecture.

However, to minimize impact to your data in the event of an outage to an entire zone, you can achieve *high availability* by enabling zone redundancy. Without zone redundancy, failovers happen locally within the same data center, which might result in your instance being unavailable until the outage is resolved - the only way to recover is through a disaster recovery solution, such as through a [failover group](failover-group-sql-mi.md), or a [geo-restore](recovery-using-backups.md#geo-restore) of a geo-redundant backup. To learn more, review the [overview of business continuity](business-continuity-high-availability-disaster-recover-hadr-overview.md).

High availability increases the reliability of your service by protecting you from impact on the: 

- Availability zone that forms the datacenter


There are two different availability architectural models based on the service tier: 

- The **remote storage model** is based on a separation of compute and storage in the [General Purpose](service-tiers-managed-instance-vcore.md#general-purpose) and [Next-gen General Purpose](service-tiers-next-gen-general-purpose-use.md) service tiers that relies on the availability and reliability of the [remote storage](/azure/storage/common/storage-redundancy) and the availability of compute clusters managed by [Azure Service Fabric](/azure/service-fabric/service-fabric-azure-clusters-overview). This availability model targets budget-oriented business applications that can tolerate some performance degradation during maintenance activities. 
- The **local storage model** is based on a cluster of database engine processes that rely on a quorum of available database engine nodes in the [Business Critical service tier](service-tiers-managed-instance-vcore.md#business-critical) that have local storage. This local storage model targets mission-critical applications that have a high transaction rate and require high IO performance. The high availability architecture guarantees minimal performance impact on your workload during maintenance activities. 

For more information regarding specific SLAs for different service tiers, review [SLA for Azure SQL Managed Instance](https://azure.microsoft.com/support/legal/sla/azure-sql-sql-managed-instance/). 

## <a id="locally-redundant-availability"></a>Availability through local redundancy 

Locally redundant availability is based on storing your compute nodes and data on within a single datacenter in the primary region and protects your data in the event of local failure, such as a small-scale network or power failure. If a large-scale disaster such as fire or flooding occurs within a region, all replicas of a storage account or data on the compute nodes might be lost or unrecoverable. As such, to further protect your data when using the locally redundant availability option, consider using a more resilient storage option for your [database backups](automated-backups-overview.md#backup-storage-redundancy).

### General Purpose service tier

The General Purpose service tier uses the remote storage availability architecture. The following figure shows four different nodes with the separated compute and storage layers.

:::image type="content" source="media/high-availability-sla-local-zone-redundancy/general-purpose-service-tier.png" alt-text="Diagram showing separation of compute and storage.":::

The remote storage availability model includes two layers:

- A stateless compute layer that runs the database engine process and contains only transient and cached data, such as the `tempdb` and `model` databases on the attached SSD, and plan cache, buffer pool, and columnstore pool in memory. This stateless node is operated by [Azure Service Fabric](/azure/service-fabric/service-fabric-azure-clusters-overview) that initializes database engine, controls health of the node, and performs failover to another node if necessary.
- A stateful data layer with the database files (`.mdf` and `.ldf`) stored in Azure Blob Storage. Azure Blob Storage has built-in data availability and redundancy features. Locally redundant availability is based on storing your data on [locally redundant storage (LRS)](/azure/storage/common/storage-redundancy#locally-redundant-storage) which copies your data three times within a single datacenter in the primary region. It guarantees that every record in the log file or page in the data file will be preserved even if database engine process crashes. 

Whenever the database engine or the operating system is upgraded, or a failure is detected, Azure Service Fabric will move the stateless database engine process to another stateless compute node with sufficient free capacity. Data in Azure Blob storage isn't affected by the move, and the data/log files are attached to the newly initialized database engine process. This process guarantees high availability, but a heavy workload might experience some performance degradation during the transition since the new database engine process starts with cold cache.

### Next-gen General Purpose service tier 

> [!NOTE]
> The [Next-gen General Purpose service tier upgrade](service-tiers-next-gen-general-purpose-use.md) is currently in preview. 

Next-gen General Purpose is an architectural upgrade to the existing General Purpose service tier that uses an upgraded remote storage layer that stores instance data and log files on managed disks instead of page blobs and maintains it locally. 

### Business Critical service tier 

The Business Critical service tier uses the local storage availability model, which integrates compute resources (database engine process) and storage (locally attached SSD) on a single node. High availability is achieved by replicating both compute and storage to additional nodes. 

:::image type="content" source="media/high-availability-sla-local-zone-redundancy/business-critical-service-tier.png" alt-text="Diagram of a cluster of database engine nodes.":::

The underlying database files (.mdf/.ldf) are placed on attached SSD storage to provide very low latency IO for your workload. High availability is implemented using a technology similar to SQL Server [Always On availability groups](/sql/database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server). The cluster includes a single primary replica that is accessible for read-write customer workloads, and up to three secondary replicas (compute and storage) that contain copies of data. The primary replica constantly pushes changes to the secondary replicas sequentially to ensure that data is persisted on a sufficient number of secondary replicas before committing each transaction. This process guarantees that, if the primary replica or a readable secondary replica become unavailable for any reason, a fully synchronized replica is always available to fail over to. Failover is initiated by [Azure Service Fabric](/azure/service-fabric/service-fabric-azure-clusters-overview). Once a secondary replica becomes the new primary replica, another secondary replica is created to ensure the cluster has a sufficient number of replicas to maintain quorum. Once failover completes, Azure SQL connections are automatically redirected to the new primary replica (or readable secondary replica based on the connection string).

As an extra benefit, the local storage availability model includes the ability to redirect read-only Azure SQL connections to one of the secondary replicas. This feature is called [Read Scale-Out](../database/read-scale-out.md). It provides 100% additional compute capacity at no extra charge to off-load read-only operations, such as analytical workloads, from the primary replica.


## <a id="zone-redundant-availability"></a>High availability through zone-redundancy

Zone-redundant availability is based on placing replicas across three Azure availability zones in the primary region. Each availability zone is a separate physical location with independent power, cooling, and networking.

By default, the cluster of nodes for the local storage availability model are created in the same datacenter. With the introduction of [Azure Availability Zones](/azure/availability-zones/az-overview), SQL Managed Instance places different replicas in different availability zones in the same region. To eliminate a single point of failure, the control ring is also duplicated across multiple zones. The control plane traffic is then routed to a load balancer that is also deployed across availability zones. Traffic routing from the control plane to the load balancer is controlled by [Azure Traffic Manager (ATM)](/azure/traffic-manager/traffic-manager-overview). 

By using a zone-redundant configuration, you can make your Business Critical or General Purpose instances resilient to a much larger set of failures, including catastrophic datacenter outages, without any changes to the application logic. You can convert any existing Business Critical or General Purpose instances to the zone-redundant configuration.

Because zone-redundant instances have replicas in different datacenters with some distance between them, the increased network latency might increase the transaction commit time, and thus impact the performance of some OLTP workloads. You can always return to the single-zone configuration by disabling the zone-redundancy setting. This process is an online operation similar to the regular service tier objective upgrade. At the end of the process, the instance is migrated from a zone-redundant ring to a single-zone ring or vice versa.

To get started with zone redundancy for your SQL managed instance, review [Configure zone redundancy](instance-zone-redundancy-configure.md). 

### General Purpose service tier

In the General Purpose service tier, zone redundancy is achieved by placing stateless compute nodes in different availability zones and then relies on a stateful [zone redundant storage (ZRS)](/azure/storage/common/storage-redundancy#zone-redundant-storage) that is attached to whichever node currently contains the active SQL Database Engine process. In the event of an outage, the SQL Database Engine process becomes active on one of the stateless nodes, which then accesses the data in the stateful storage. 

The following diagram demonstrates the zone redundancy architecture for the General Purpose service tier: 

:::image type="content" source="media/high-availability-sla-local-zone-redundancy/zone-redundant-general-purpose-service-tier.png" alt-text="Diagram of the zone redundancy architecture in the General Purpose service tier.":::

> [!NOTE]
> Zone redundancy is currently in preview for the General Purpose service tier. 

### Business Critical service tier 

In the Business Critical service tier, zone redundancy is achieved by placing compute and storage replicas in different availability zones and then using underlying Always On availability group technology to replicate data changes from the primary instance to standby replicas in other availability zones. In the event of an outage, there's an automatic failover that seamlessly transitions one of the standby replicas to be primary. 

The following diagram demonstrates the zone redundancy architecture for the Business Critical service tier: 

:::image type="content" source="media/high-availability-sla-local-zone-redundancy/zone-redundant-business-critical-service-tier.png" alt-text="Diagram of the zone redundancy architecture in the Business Critical service tier.":::

## <a id="testing-application-fault-resiliency"></a> Test application fault resiliency

Availability is a fundamental part of the SQL Managed Instance platform that works transparently for your database application. However, we recognize that you might want to test how the automatic failover operations initiated during planned or unplanned events would impact an application before you deploy it to production. You can manually trigger a failover by calling a special API to restart a managed instance. Because the restart operation is intrusive and a large number of them could stress the platform, only one failover call is allowed every 15 minutes for each managed instance.

During a true failover, connections to the instance fail while the SQL service becomes primary on a different node. To simulate a failover, invoke the command that restarts the SQL process to simulate starting the service as if there was a failover. However, connections may fail for a longer period during a true failover compared to a simulated failover, since during a true failover, the SQL process becomes the primary on another virtual machine within the cluster (either locally, or in another zone if zone-redundancy is enabled) and during a simulated failover, the SQL process is restarted on the existing virtual machine.  

The manual failover command in this section behaves the same way in both locally redundant, and zone-redundant configurations - it only restarts the SQL process locally, and does not initiate a failover to another node. This local failover is different to a failover that occurs for a failover group. 

A local failover can be initiated using PowerShell, REST API, or Azure CLI:

| PowerShell | REST API | Azure CLI |
| :--- | :--- | :--- |
| [Invoke-AzSqlInstanceFailover](/powershell/module/az.sql/Invoke-AzSqlInstanceFailover/) | [SQL Managed Instance - Failover](/rest/api/sql/managed-instances/failover) | [az sql mi failover](/cli/azure/sql/mi/#az-sql-mi-failover) can be used to invoke a REST API call from Azure CLI |


## Conclusion

Azure SQL Managed Instance features a built-in high availability solution that is deeply integrated with the Azure platform. The service depends on Service Fabric to detect failure and recover, Azure Blob storage to protect data, and on Availability Zones for higher fault tolerance. And for the Business Critical service tier, SQL Managed Instance uses SQL Server Always On availability group technology for database replication and failover. The combination of these technologies enables applications to fully realize the benefits of a mixed storage model and supports the most demanding SLAs.

## Next steps

- [Enable zone redundancy](instance-zone-redundancy-configure.md) for Azure SQL Managed Instance. 
- Learn about [Azure Availability Zones](/azure/availability-zones/az-overview)
- Learn about [Service Fabric](/azure/service-fabric/service-fabric-overview)
- Learn about [Azure Traffic Manager](/azure/traffic-manager/traffic-manager-overview)
- Learn [How to initiate a manual failover on SQL Managed Instance](user-initiated-failover.md)
- For more options for high availability and disaster recovery, see [Business Continuity](business-continuity-high-availability-disaster-recover-hadr-overview.md)
