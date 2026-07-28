---
title: What is the Hyperscale service tier?
description: This article describes the Hyperscale service tier in the vCore-based purchasing model in Azure SQL Database and explains how it's different from the General Purpose and Business Critical service tiers.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: dfurman, mathoma, oslake, randolphwest, adbadram, malstewart
ms.date: 05/19/2026
ms.service: azure-sql-database
ms.subservice: service-overview
ms.topic: concept-article
ms.custom:
  - sqldbrb=1
  - build-2025
---

# Hyperscale service tier

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

Azure SQL Database Hyperscale is a cost-efficient, high-performance cloud database.

Azure SQL Database is based on the [SQL Database Engine](/sql/database-engine/sql-database-engine). Hyperscale differs from other Azure SQL Database service tiers:

 - Unlike other service tiers, Hyperscale has no SQL software license fee, which gives it a significant price advantage over other Azure SQL Database service tiers for high-performance databases.
 - [Hyperscale architecture](hyperscale-architecture.md) is distinct: it provides nearly instantaneous backups, fast restores, and high throughput for reads and writes.
 - Hyperscale provides fast compute scaling on demand without data movement.
 - Read scale-out strategies are easy, with up to 30 named replicas with independent configurable compute, plus built-in high-availability replicas and configurable geo-replicas around the globe.

The Hyperscale service tier is suitable for all workload types. Compute and storage resources in Hyperscale substantially exceed the resources available in the Azure SQL Database General Purpose and Business Critical tiers.

You can [convert an existing database in Azure SQL Database to Hyperscale](convert-to-hyperscale.md) easily, or migrate from any [SQL Server database to Hyperscale](/data-migration/sql-server/database/overview). To migrate other databases to Azure SQL Database, see [Azure Database Migration Guides](/data-migration/).

The Hyperscale service tier is currently only available for Azure SQL Database, and not for Azure SQL Managed Instance.

## What are the Hyperscale capabilities

The Hyperscale service tier in Azure SQL Database provides the following additional capabilities:

- Rapid scale up - scale up your compute resources to accommodate heavy workloads when needed, and then scale the compute resources back down when not needed.
- Rapid scale out - provision one or more [read-only replicas](service-tier-hyperscale-replicas.md) for offloading your read workload and for use as hot-standbys.
- Automatic scale-up, scale-down, and billing for compute based on usage with [serverless compute](serverless-tier-overview.md).
- Optimized price/performance for a group of Hyperscale databases with varying resource demands with [elastic pools](hyperscale-elastic-pool-overview.md).
- Autoscaling storage with support for up to 128 TB of database or 100 TB elastic pool size.
- Higher overall performance due to higher transaction log throughput and faster transaction commit times regardless of data volumes.
- Fast database backups (based on file snapshots) regardless of size with no I/O impact on compute resources.
- Fast database restores or copies (based on file snapshots) in minutes rather than hours or days.

The Hyperscale service tier removes many of the practical limits traditionally seen in cloud databases. Where most other databases are limited by the resources available in a single node, databases in the Hyperscale service tier have no such limits. With its flexible storage architecture, storage grows as needed. In fact, Hyperscale databases aren't created with a defined max size. A Hyperscale database grows as needed - and you're billed only for the storage capacity allocated. For read-intensive workloads, the Hyperscale service tier provides rapid scale-out by provisioning additional replicas as needed for offloading read workloads.

Additionally, the time required to create database backups or to scale up or down is no longer tied to the volume of data in the database. Hyperscale databases are backed up virtually instantaneously. You can also scale a database in the tens of terabytes up or down within minutes in the provisioned compute tier or use [serverless](serverless-tier-overview.md) to scale compute automatically. This capability frees you from concerns about being boxed in by your initial configuration choices. 

For more information about the compute sizes for the Hyperscale service tier, see [Service tier characteristics](service-tiers-vcore.md#service-tiers).

For details on the General Purpose and Business Critical service tiers in the vCore-based purchasing model, see [General Purpose](service-tiers-sql-database-vcore.md#general-purpose) and [Business Critical](service-tiers-sql-database-vcore.md#business-critical) service tiers. For a comparison of the vCore-based purchasing model with the DTU-based purchasing model, see [Compare vCore and DTU-based purchasing models of Azure SQL Database](purchasing-models.md).

## Who should consider the Hyperscale service tier

The Hyperscale service tier is intended for all customers who require higher performance and availability, fast backup and restore, and fast storage and compute scalability. Hyperscale is ideal for customers who are moving to the cloud to modernize their applications, or for customers who are already using other service tiers in Azure SQL Database. The Hyperscale service tier supports a broad range of database workloads, from pure OLTP to pure analytics. It's optimized for OLTP and hybrid transaction and analytical processing (HTAP) workloads.

## Hyperscale pricing model

For high performance databases, Hyperscale offers a significant price advantage over other Azure SQL Database service tiers. For more information, see [Blog: Azure SQL Database Hyperscale pricing announcement from Ignite 2023](https://aka.ms/hsignite2023). For pricing change details, see [Blog: Azure SQL Database Hyperscale - lower, simplified pricing!](https://techcommunity.microsoft.com/blog/azuresqlblog/azure-sql-database-hyperscale-–-lower-simplified-pricing/3982209)

The Hyperscale service tier is available only in the [vCore model](service-tiers-sql-database-vcore.md) and comes in two compute tiers. Billing for Hyperscale is based on the provisioned or serverless compute tier:

- **Provisioned** compute tier:

   The [vCore compute cost](service-tiers-sql-database-vcore.md#compute) reflects the total compute capacity that's continuously provisioned for the application. The Hyperscale compute unit price is per replica.

- **Serverless** compute tier:

   Serverless compute billing is based on usage. For more information, see [Serverless compute tier for Azure SQL Database](serverless-tier-overview.md).

You don't specify the max data size when configuring a Hyperscale database. In the Hyperscale tier, you pay for storage based on actual allocation. Storage is automatically allocated between 10 GB and 128 TB, and grows as needed. For more information, see [In what increments does my database size grow?](service-tier-hyperscale-frequently-asked-questions-faq.yml#in-what-increments-does-my-database-size-grow-)

<a id="distributed-functions-architecture"></a>

## Scale and performance advantages

Hyperscale separates the main database engine from the components that provide long-term storage and durability for the data. This architecture allows you to scale compute resources rapidly, without data movement, and to scale storage (up to 128 TB) independently of compute. For more details, including an architecture diagram, see [Hyperscale architecture](hyperscale-architecture.md#hyperscale-architecture-overview).

- With the ability to rapidly spin up/down additional read-only compute nodes, the Hyperscale architecture allows significant read scale capabilities and can also free up the primary compute node to serve more requests.
- You can provision the compute for secondary nodes or use [serverless compute](serverless-tier-overview.md). In either case, you can scale them up or down quickly because of the shared-storage architecture of Hyperscale.  
   - Secondary high-availability compute node replicas in Hyperscale follow the compute tier of the primary, which results in low-impact failovers.
- When you use serverless, primary or secondary compute nodes automatically scale based on your workload demand.

The primary Azure SQL Database Hyperscale database handles both read and write workloads, but you can easily create read-only replicas as part of your application strategy:

- You can adjust the total number of [high-availability secondary replicas](service-tier-hyperscale-replicas.md#high-availability-replica) from 0 to 4, depending on availability and scalability requirements.
- You can create up to 30 [named replicas](service-tier-hyperscale-replicas.md#named-replica) to support read scale-out workloads. 
- You can achieve geo-distributed read scale-out across Azure global data centers by using [geo-replicas](service-tier-hyperscale-replicas.md#geo-replica).


### Database high availability in Hyperscale

As in all other service tiers, Hyperscale guarantees data durability for committed transactions regardless of compute replica availability. The extent of downtime due to the primary replica becoming unavailable depends on the type of failover (planned vs. unplanned), [whether zone redundancy is configured](high-availability-sla-local-zone-redundancy.md#hyperscale-service-tier-zone-redundant-availability), and on the presence of at least one high-availability replica. In a planned failover (such as a maintenance event), the system either creates the new primary replica before initiating a failover or uses an existing high-availability replica as the failover target. In an unplanned failover (such as a hardware failure on the primary replica), the system uses a high-availability replica as a failover target if one exists, or creates a new primary replica from the pool of available compute capacity. In the latter case, downtime duration is longer due to extra steps required to create the new primary replica.

You can [choose a maintenance window](maintenance-window.md?view=azuresql-db&preserve-view=true) to make impactful maintenance events predictable and less disruptive for your workload.

For Hyperscale SLA, see [SLA for Azure SQL Database](https://azure.microsoft.com/support/legal/sla/azure-sql-database).

<a id="buffer-pool-resilient-buffer-pool-extension-and-continuous-priming"></a>
<a id="buffer-pool-resilient-buffer-pool-extension"></a>

### Buffer pool and resilient buffer pool extension

In Azure Database Hyperscale, there is a distinct separation between compute and storage. Storage contains all the database pages in one database, and can be allocated over multiple machines as the database grows. The compute node, however, only caches what is being used recently. The hottest pages in compute are maintained in memory in a structure called buffer pool (BP). It is also stored in the local SSD, the resilient buffer pool extension (RBPEX), so data can be retrieved faster in case the compute process restarts.

In a cloud system, compute can move to different machines as needed. The compute layer can have multiple replicas. One replica is primary and receives all updates, while the others are secondary replicas. If the primary fails, the system promotes one of the high availability secondary replicas to primary in a process called failover. The secondary replica might not have a cache in its BP and RBPEX that's optimized for the primary workload.

### Continuous priming

Continuous priming is a process that collects information about which pages are the most frequently accessed (hottest) in all compute replicas. The process aggregates this information, and high availability secondary replicas use the list of hottest pages that correspond to the typical customer workload. This process continuously fills both the BP and RBPEX with the hottest pages to keep up with changes in the customer workload.

Without continuous priming, both BP and RBPEX are not inherited by new high availability replicas, and are only reconstructed during the user workload. Continuous priming saves time and prevents inconsistent performance, as there is no wait before the caches are fully hydrated again. With continuous priming, new high availability secondary replicas will immediately start priming their BP and RBPEX. This will help maintain performance more consistently as failovers happen.

Continuous priming works both ways: high availability secondary replicas will cache pages being used in the primary replica, and the primary will cache pages with the workload from the secondary replicas.

Continuous priming is currently available on the Hyperscale provisioned compute tier.

### Back up and restore

Back up and restore operations for Hyperscale databases are file-snapshot based. This approach makes these operations nearly instantaneous. Because the Hyperscale architecture uses the storage layer for backup and restore, it reduces the processing burden and performance impact on compute replicas. For more information, see [Hyperscale backups and storage redundancy](hyperscale-automated-backups-overview.md#data-and-backup-storage-redundancy).

### Disaster recovery for Hyperscale databases

To restore a Hyperscale database in Azure SQL Database to a region other than the one it's currently hosted in, perform a [geo-restore](recovery-using-backups.md#geo-restore). This method works for disaster recovery operations, drills, relocation, or any other reason. Geo-restore is only available when you choose geo-redundant storage (RA-GRS) for storage redundancy.

For more information, see [restoring a Hyperscale database to a different region](hyperscale-automated-backups-overview.md#restore-a-hyperscale-database-to-a-different-region).

## Compare resource limits

<!---
vCore resource limits are listed in the following articles, please be sure to update all of them:  
/database/resource-limits-vcore-single-databases.md
/database/resource-limits-vcore-elastic-pools.md
/database/resource-limits-logical-server.md
/database/service-tiers-sql-database-vcore.md
/database/service-tier-hyperscale.md
/managed-instance/resource-limits.md
--->

The vCore-based service tiers differ in database availability, storage type, performance, and maximum storage size. The following table describes these differences:

[!INCLUDE [service-tier-comparison-table](includes/service-tier-comparison-table.md)]

## Compute resources

The following table compares compute resources in different hardware configurations and compute tiers for Azure SQL Database Hyperscale. For non-Hyperscale Azure SQL Database, see [vCore purchasing model - Azure SQL Database](service-tiers-sql-database-vcore.md#compute-resources-cpu-and-memory).

| Hardware configuration | CPU | Memory |
| :--- | :--- | :--- |
|Standard-series (Gen5) |**Provisioned compute**<br>- Intel&reg; E5-2673 v4 (Broadwell) 2.3 GHz, Intel&reg; SP-8160 (Skylake)\*, Intel&reg; 8272CL (Cascade Lake) 2.5 GHz\*, Intel&reg; Xeon&reg; Platinum 8370C (Ice Lake)\*, AMD EPYC&trade; 7763v (Milan)\*, AMD EPYC 9004 (Genoa)\*, Intel&reg; Xeon&reg; Platinum 8573C (Emerald Rapids)\* processors<br>- Provision up to 128 vCores (hyper-threaded)<br><br>**Serverless compute**<br>- Intel&reg; E5-2673 v4 (Broadwell) 2.3 GHz, Intel&reg; SP-8160 (Skylake)\*, Intel&reg; 8272CL (Cascade Lake) 2.5 GHz\*, Intel&reg; Xeon&reg; Platinum 8370C (Ice Lake)\*, AMD EPYC&trade; 7763v (Milan)\*, AMD EPYC 9004 (Genoa)\*, Intel&reg; Xeon&reg; Platinum 8573C (Emerald Rapids)\* processors<br>- Autoscale up to 80 vCores (hyper-threaded)<br>- The memory-to-vCore ratio dynamically adapts to memory and CPU usage based on workload demand and can be as high as 24 GB per vCore.  For example, at a given point in time a workload might use and be billed for 240-GB memory and only 10 vCores.|**Provisioned compute**<br>- 5.1 GB per vCore<br>- Provision up to 625 GB<br><br>**Serverless compute**<br>- Autoscale up to 24 GB per vCore<br>- Autoscale up to 240 GB max|
|Premium-series |**Provisioned compute**<br>- Intel&reg; Xeon&reg; Platinum 8370C (Ice Lake)\*, AMD EPYC&trade; 7763v (Milan)\*, AMD EPYC 9004 (Genoa)\*, Intel&reg; Xeon&reg; Platinum 8573C (Emerald Rapids)\* processors<br>- Provision up to 192 vCores (hyper-threaded). | 5.2 GB per vCore |
|Premium-series memory optimized  |**Provisioned compute**<br>- Intel&reg; Xeon&reg; Platinum 8370C (Ice Lake)\*, AMD EPYC&trade; 7763v (Milan)\*, AMD EPYC 9004 (Genoa)\*, Intel&reg; Xeon&reg; Platinum 8573C (Emerald Rapids)\* processors<br>- Provision up to 80 vCores (hyper-threaded).| 10.2 GB per vCore |

\* For a given compute size and hardware configuration, resource limits are the same regardless of CPU type (Intel&reg; Broadwell, Skylake, Ice Lake, Cascade Lake, Emerald Rapid, or AMD Milan, Genoa). In the [sys.dm_user_db_resource_governance](/sql/relational-databases/system-dynamic-management-views/sys-dm-user-db-resource-governor-azure-sql-database) dynamic management view, hardware generation for databases using:
  - Intel&reg; SP-8160 (Skylake) processors appears as Gen6
  - Intel&reg; 8272CL (Cascade Lake) appears as Gen7
  - Intel&reg; Xeon&reg; Platinum 8370C (Ice Lake) or AMD EPYC&trade; 7763v (Milan) appear as Gen8
  - AMD EPYC&trade; 9004 (Genoa) appear as Gen9 or Intel&reg; Xeon&reg; Platinum 8573C (Emerald Rapids) appear as Gen10

For more information, see resource limits for [single databases](resource-limits-vcore-single-databases.md) and [elastic pools](resource-limits-vcore-elastic-pools.md).

## Create and manage Hyperscale databases

You can create and manage Hyperscale databases by using the Azure portal, Transact-SQL, PowerShell, and the Azure CLI. For more information, see [Quickstart: Create a Hyperscale database](hyperscale-database-create-quickstart.md#create-a-hyperscale-database).

| **Operation** | **Details** | **Learn more** |
| :--- | :--- | :--- |
| **Create a Hyperscale database** | Hyperscale databases are available only by using the [vCore-based purchasing model](service-tiers-vcore.md). | Find examples to create a Hyperscale database in [Quickstart: Create a Hyperscale database in Azure SQL Database](hyperscale-database-create-quickstart.md). |
| **Convert an existing database to Hyperscale** | You can convert an existing database to the Azure SQL Database Hyperscale tier. The conversion duration depends on the size of data. | For more information, see [Convert an existing database to Hyperscale](convert-to-hyperscale.md). |
| **Reverse migrate a Hyperscale database to the General Purpose service tier** | If you previously migrated an existing Azure SQL Database to Hyperscale, you can reverse migrate the database to the General Purpose service tier within 45 days of the original migration to Hyperscale.<br /><br />If you want to migrate the database to another service tier, such as Business Critical, first reverse migrate to the General Purpose service tier, then change the service tier. | Learn [how to reverse migrate from Hyperscale](reverse-migrate-from-hyperscale.md), including the [limitations for reverse migration](reverse-migrate-from-hyperscale.md#limitations-for-reverse-migration). |

<a id="known-limitations"></a>

## Limitations

These limitations currently apply to the Hyperscale service tier. The product team is actively working to remove as many of these limitations as possible.

| Issue | Description |
| :--- | :--- |
| Shrink is blocked when TDE is disabled | Currently, Azure SQL Database Hyperscale doesn't support database and file shrink operations when Transparent Data Encryption (TDE) is disabled.|
| Restore database from other service tiers | You can't restore a non-Hyperscale database as a Hyperscale database. You also can't restore a Hyperscale database as a non-Hyperscale database.<br /><br />For databases migrated to Hyperscale from other Azure SQL Database service tiers, pre-migration backups are kept for the duration of the [backup retention](automated-backups-overview.md#backup-retention) period of the source database, including long-term retention policies. You can restore a pre-migration backup within the backup retention period of the database [via the command line](recovery-using-backups.md#point-in-time-restore). You can restore these backups to any non-Hyperscale service tier. |
| Migration of databases with In-Memory OLTP objects | Hyperscale supports a subset of In-Memory OLTP objects, including memory-optimized table types, table variables, and natively compiled modules. However, when any In-Memory OLTP objects are present in the database being migrated, migration from Premium and Business Critical service tiers to Hyperscale isn't supported. To migrate such a database to Hyperscale, you must drop all In-Memory OLTP objects and their dependencies. After the database is migrated, you can recreate these objects. Durable and non-durable memory-optimized tables aren't currently supported in Hyperscale and must be changed to disk tables. |
| Database integrity check | `DBCC CHECKDB` and `DBCC CHECKFILEGROUP` aren't currently supported for Azure SQL Database Hyperscale databases. As a workaround, use `DBCC CHECKTABLE ('TableName') WITH TABLOCK`. For details on data integrity management in Azure SQL Database, see [Data Integrity in Azure SQL Database](data-integrity.md). |
| Elastic Jobs | Using a Hyperscale database as the Job database isn't supported. However, elastic jobs can target Hyperscale databases in the same way as any other database in Azure SQL Database. |
| Data Sync | Using a Hyperscale database as a Hub or Sync Metadata database isn't supported. However, a Hyperscale database can be a member database in a Data Sync topology. |
| Hyperscale service tier premium-series hardware | Premium-series and memory-optimized premium-series hardware doesn't currently support the serverless compute tier. Serverless is only supported on Standard-series (Gen5) hardware. |
| Regional availability | Hyperscale service tier premium-series and premium-series memory-optimized hardware is available in limited Azure regions. For a list, see [Hyperscale premium-series availability](region-availability.md#hyperscale-premium-series-availability). |

## Related content

- [Frequently asked questions about Hyperscale](service-tier-hyperscale-frequently-asked-questions-faq.yml)
- [Compare vCore and DTU-based purchasing models of Azure SQL Database](purchasing-models.md)
- [Resource management in Azure SQL Database](resource-limits-logical-server.md)
- [Resource limits for single databases using the vCore purchasing model](resource-limits-vcore-single-databases.md)
- [Features comparison: Azure SQL Database and Azure SQL Managed Instance](features-comparison.md)
- [Hyperscale distributed functions architecture](hyperscale-architecture.md)
- [How to manage a Hyperscale database](manage-hyperscale-database.md)
- [Modifiable configuration reference for Azure SQL Database](modifiable-configuration-reference.md)
