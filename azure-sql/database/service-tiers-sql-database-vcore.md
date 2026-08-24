---
title: vCore purchasing model
description: The vCore purchasing model lets you independently scale compute and storage resources, match on-premises performance, and optimize price for Azure SQL Database
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: sashan, moslake, mathoma, dfurman, srinia
ms.date: 07/10/2026
ms.service: azure-sql-database
ms.subservice: performance
ms.topic: concept-article
ms.custom:
  - azure-sql-split
  - sfi-image-nochange
---
# vCore purchasing model - Azure SQL Database

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](service-tiers-sql-database-vcore.md?view=azuresql-db&preserve-view=true)
> * [Azure SQL Managed Instance](../managed-instance/service-tiers-managed-instance-vcore.md?view=azuresql-mi&preserve-view=true)

This article reviews the [vCore purchasing model](service-tiers-sql-database-vcore.md) for [Azure SQL Database](sql-database-paas-overview.md). 

## Overview

[!INCLUDE [vcore-overview](../includes/vcore-overview.md)]

> [!IMPORTANT]
> Compute resources, I/O, and data and log storage are charged per database or elastic pool. Backup storage is charged per each database. For pricing details, see the [Azure SQL Database pricing page](https://azure.microsoft.com/pricing/details/sql-database/single/).

## Compare vCore and DTU purchasing models

The vCore purchasing model used by Azure SQL Database provides several benefits over the [DTU-based purchasing model](service-tiers-dtu.md):

- Higher compute, memory, I/O, and storage limits.
- Choice of hardware configuration to better match compute and memory requirements of the workload.
- Pricing discounts with the [Azure Hybrid Benefit (AHB)](../azure-hybrid-benefit.md).
- Greater transparency in the hardware details, which facilitates planning for migrations from on-premises deployments.
- [Reserved instance pricing](reservations-discount-overview.md) is only available for vCore purchasing model. 
- Higher scaling granularity with multiple compute sizes available.

For help with choosing between the vCore and DTU purchasing models, see the [differences between the vCore and DTU-based purchasing models](purchasing-models.md).

## Compute

The vCore-based purchasing model has a provisioned compute tier and a [serverless](serverless-tier-overview.md) compute tier. In the provisioned compute tier, the compute cost reflects the total compute capacity continuously provisioned for the application independent of workload activity. Choose the resource allocation that best suits your business needs based on vCore and memory requirements, then scale resources up and down as needed by your workload. In the serverless compute tier for Azure SQL Database, compute resources are autoscaled based on workload capacity and billed for the amount of compute used, per second. 

Serverless is only supported on Standard-series (Gen5) hardware.

To summarize:

 - While the **provisioned compute tier** provides a specific amount of compute resources that are continuously provisioned independent of workload activity, the **serverless compute tier** autoscales compute resources based on workload activity.
 - While the **provisioned compute tier** bills for the amount of compute provisioned at a fixed price per hour, the **serverless compute tier** bills for the amount of compute used, per second.

Regardless of the compute tier, three additional high availability secondary replicas are automatically allocated in the Business Critical service tier to provide high resiliency to failures and fast failovers. These additional replicas make the cost approximately 2.7 times higher than it is in the General Purpose service tier. Likewise, the higher storage cost per GB in the Business Critical service tier reflects the higher IO limits and lower latency of the local SSD storage.

In Hyperscale, customers control the number of additional high availability replicas from 0 to 4 to get the level of resiliency required by their applications while controlling costs.

For more information on compute in Azure SQL Database, see [Compute resources (CPU and memory)](#compute-resources-cpu-and-memory).

## Resource limits

For vCore resource limits, review the available [Hardware configurations](#hardware-configuration), then review the resource limits for:
- [logical servers](resource-limits-logical-server.md)
- [single databases](resource-limits-vcore-single-databases.md)
- [databases in elastic pools](resource-limits-vcore-elastic-pools.md)

## Data and log storage

The following factors affect the amount of storage used for data and log files, and apply to General Purpose and Business Critical tiers. 

- Each compute size supports a configurable maximum data size, with a default of 32 GB.
- When you configure maximum data size, an extra 30 percent of billable storage is automatically added for the log file.
- In the General Purpose service tier, `tempdb` uses local SSD storage, and this storage cost is included in the vCore price.
- In the Business Critical service tier, `tempdb` shares local SSD storage with data and log files, and `tempdb` storage cost is included in the vCore price.
- In the General Purpose and Business Critical tiers, you're charged for the maximum storage size configured for a database or elastic pool.
- For SQL Database, you can select any maximum data size between 1 GB and the supported storage size maximum, in 1-GB increments. 

The following storage considerations apply to Hyperscale:

- Maximum data storage size is set to 128 TB and isn't configurable.
- You're charged only for the allocated data storage, not for maximum data storage, with a minimum of 10 GB.
- You aren't charged for log storage.
- `tempdb` uses local SSD storage, and its cost is included in the vCore price.
To monitor the current allocated and used data storage size in SQL Database, use the *allocated_data_storage* and *storage* Azure Monitor [metrics](/azure/azure-monitor/essentials/metrics-supported#microsoftsqlserversdatabases) respectively. 

To monitor the current allocated and used storage size of individual data and log files in a database by using T-SQL, use the [sys.database_files](/sql/relational-databases/system-catalog-views/sys-database-files-transact-sql) view and the [FILEPROPERTY(... , 'SpaceUsed')](/sql/t-sql/functions/fileproperty-transact-sql) function.

> [!TIP]
> Under some circumstances, you might need to shrink a database to reclaim unused space. For more information, see [Manage file space in Azure SQL Database](file-space-manage.md).

## Backup storage

Storage for database backups is allocated to support the [point-in-time restore (PITR)](recovery-using-backups.md) and [long-term retention (LTR)](long-term-retention-overview.md) capabilities of SQL Database. This storage is separate from data and log file storage, and is billed separately.

- **PITR**: In General Purpose and Business Critical tiers, individual database backups are copied to [Azure storage](automated-backups-overview.md#restore-capabilities) automatically. The storage size increases dynamically as new backups are created. The storage is used by full, differential, and transaction log backups. The storage consumption depends on the rate of change of the database and the retention period configured for backups. You can configure a separate retention period for each database between 1 and 35 days for SQL Database. A backup storage amount equal to the configured maximum data size is provided at no extra charge.
- **LTR**: You also can configure long-term retention of full backups for up to 10 years. If you set up an LTR policy, these backups are stored in Azure Blob storage automatically, but you can control how often the backups are copied. To meet different compliance requirements, you can select different retention periods for weekly, monthly, and/or yearly backups. The configuration you choose determines how much storage is used for LTR backups. For more information, see [Long-term backup retention](long-term-retention-overview.md).

For backup storage in Hyperscale, see [Automated backups for Hyperscale databases](hyperscale-automated-backups-overview.md).

## Service tiers

Service tier options in the vCore purchasing model include General Purpose, Business Critical, and Hyperscale. The service tier generally determines storage type and performance, high availability and disaster recovery options, and the availability of certain features such as In-Memory OLTP.

[!INCLUDE [service-tier-comparison-table](includes/service-tier-comparison-table.md)]

- For greater details, review resource limits for [logical server](resource-limits-logical-server.md), [single databases](resource-limits-vcore-single-databases.md), and [pooled databases](resource-limits-vcore-elastic-pools.md). 
- For more information on the Service Level Agreement (SLA), see [SLA for Azure SQL Database](https://azure.microsoft.com/support/legal/sla/azure-sql-database/).

### General Purpose

The architectural model for the General Purpose service tier is based on a separation of compute and storage. This architectural model relies on the high availability and reliability of Azure Blob storage that transparently replicates database files and guarantees no data loss if underlying infrastructure failure happens.

The following figure shows four nodes in standard architectural model with the separated compute and storage layers.

:::image type="content" source="media/service-tiers-sql-database-vcore/general-purpose-service-tier.png" alt-text="Diagram illustrating the separation of compute and storage.":::

In the architectural model for the General Purpose service tier, there are two layers:

- A stateless compute layer that is running the `sqlservr.exe` process and contains only transient and cached data (for example – plan cache, buffer pool, columnstore pool). This stateless node is operated by Azure Service Fabric that initializes process, controls health of the node, and performs failover to another place if necessary.
- A stateful data layer with database files (.mdf/.ldf) that are stored in Azure Blob storage. Azure Blob storage guarantees that there's no data loss of any record that is placed in any database file. Azure Storage has built-in data availability/redundancy that ensures that every record in log file or page in data file is preserved even if the process crashes.

Whenever the database engine or operating system is upgraded, some part of underlying infrastructure fails, or if some critical issue is detected in the `sqlservr.exe` process, Azure Service Fabric moves the stateless process to another stateless compute node. There's a set of spare nodes that is waiting to run new compute service if a failover of the primary node happens in order to minimize failover time. Data in Azure storage layer isn't affected, and data/log files are attached to newly initialized process. This process ensures an [enterprise class SLA availability](https://www.microsoft.com/licensing/docs/view/Service-Level-Agreements-SLA-for-Online-Services?lang=1) by default which increases when you implement [zone redundancy](high-availability-sla-local-zone-redundancy.md#zone-redundant-availability). There might be some performance impacts to heavy workloads that are in-flight due to transition time and the fact the new node starts with cold cache.

<a id="when-to-choose-this-service-tier"></a>

#### When to choose the General Purpose service tier

The General Purpose service tier is the default service tier in Azure SQL Database designed for most of generic workloads. If you need a fully managed database engine with a default SLA and storage latency between 5 ms and 10 ms, the General Purpose tier is the option for you.

### Business Critical

The Business Critical service tier model is based on a cluster of database engine processes. This architectural model relies on a quorum of database engine nodes to minimize performance impacts to your workload, even during maintenance activities. Upgrades and patches of the underlying operating system, drivers, and the database engine occur transparently, with minimal down-time for end users. 

In the Business Critical model, compute and storage is integrated on each node. Replication of data between database engine processes on each node of a four-node cluster achieves high availability, with each node using locally attached SSD as data storage. The following diagram shows how the Business Critical service tier organizes a cluster of database engine nodes in availability group replicas.

:::image type="content" source="media/service-tiers-sql-database-vcore/business-critical-service-tier.png" alt-text="Diagram showing how the Business Critical service tier organizes a cluster of database engine nodes in availability group replicas." lightbox="media/service-tiers-sql-database-vcore/business-critical-service-tier.png":::

Both the database engine process and underlying .mdf/.ldf files are placed on the same node with locally attached SSD storage, providing low latency to your workload. High availability is implemented using technology similar to SQL Server [Always On availability groups](/sql/database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server). Every database is a cluster of database nodes with one primary replica that is accessible for customer workloads, and three secondary replicas containing copies of data. The primary replica constantly pushes changes to the secondary replicas in order to ensure the data is available on secondary replicas if the primary fails for any reason. Failover is handled by the Service Fabric and the database engine – one secondary replica becomes the primary, and a new secondary replica is created to ensure there are enough nodes in the cluster. The workload is automatically redirected to the new primary replica.

In addition, the Business Critical cluster has a built-in [Read Scale-Out](read-scale-out.md) capability that provides a free-of charge read-only replica used to run read-only queries (such as reports) that won't affect the performance of the workload on your primary replica.

<a id="when-to-choose-this-service-tier-1"></a>

#### When to choose the Business Critical service tier

The Business Critical service tier is designed for applications that require low-latency responses from the underlying SSD storage (1-2 ms in average), faster recovery if the underlying infrastructure fails, or need to off-load reports, analytics, and read-only queries to the free of charge readable secondary replica of the primary database.

The key reasons why you should choose Business Critical service tier instead of General Purpose tier are:

- **Low I/O latency requirements** – workloads that need a consistently fast response from the storage layer (1-2 milliseconds on average) should use Business Critical tier. 
- **Workload with reporting and analytic queries** where a single free-of-charge secondary read-only replica is sufficient.
- **Higher resiliency and faster recovery from failures**. In case there's system failure, the database on primary instance is disabled and one of the secondary replicas immediately becomes the new read-write primary database, ready to process queries.
- **Higher availability** - The Business Critical tier in a multi-availability zone configuration provides resiliency to zonal failures and a higher availability SLA.
- **Fast geo-recovery** - When [active geo-replication](active-geo-replication-overview.md) is configured, the Business Critical tier has a guaranteed Recovery Point Objective (RPO) of 5 seconds and Recovery Time Objective (RTO) of 30 seconds for 100% of deployed hours.

### Hyperscale

The Hyperscale service tier is suitable for all workload types. Its cloud native architecture provides independently scalable compute and storage to support the widest variety of traditional and modern applications. Compute and storage resources in Hyperscale substantially exceed the resources available in the General Purpose and Business Critical tiers.

To learn more, review [Hyperscale service tier for Azure SQL Database](service-tier-hyperscale.md).

<a id="when-to-choose-this-service-tier-2"></a>

#### When to choose the Hyperscale service tier

The Hyperscale service tier removes many of the practical limits traditionally seen in cloud databases. Where most other databases are limited by the resources available in a single node, databases in the Hyperscale service tier have no such limits. With its flexible storage architecture, a Hyperscale database grows as needed - and you're billed only for the storage capacity you use.

Besides its advanced scaling capabilities, Hyperscale is a great option for any workload, not just for large databases. With Hyperscale, you can:

- Achieve **high resiliency and fast failure recovery** while controlling cost, by choosing the number of high availability replicas from 0 to 4.
- Improve **high availability** by enabling zone redundancy for compute and storage.
- Achieve **low I/O latency** (1-2 milliseconds on average) for the frequently accessed part of your database. For smaller databases, this might apply to the entire database.
- Implement a large variety of **read scale-out scenarios** with named replicas.
- Take advantage of **fast scaling**, without waiting for data to be copied to local storage on new nodes.
- Enjoy **zero-impact continuous database backup** and **fast restore**.
- Support **business continuity** requirements by using failover groups and geo-replication.

## Hardware configuration

Common hardware configurations in the vCore model include standard-series (Gen5), premium-series, premium-series memory optimized, and DC-series. Hyperscale also provides an option for premium-series and premium-series memory optimized hardware. Hardware configuration defines compute and memory limits and other characteristics that affect workload performance.

Certain hardware configurations such as standard-series (Gen5) can use more than one type of processor (CPU), as described in [Compute resources (CPU and memory)](#compute-resources-cpu-and-memory). While a given database or elastic pool tends to stay on the hardware with the same CPU type for a long time (commonly for multiple months), there are certain events that can cause a database or pool to be moved to hardware that uses a different CPU type. 

A database or pool could be moved for a variety of scenarios, including but not limited to when:

- The service objective is changed
- The current infrastructure in a datacenter is approaching capacity limits
- The currently used hardware is being decommissioned due to its end of life
- Zone-redundant configuration is enabled, moving to a different hardware due to available capacity

For some workloads, a move to a different CPU type can change performance. SQL Database configures hardware with the goal to provide predictable workload performance even if CPU type changes, keeping performance changes within a narrow band. However, across the wide spectrum of customer workloads in SQL Database, and as new types of CPUs become available, it's occasionally possible to see more noticeable changes in performance, if a database or pool moves to a different CPU type.

Regardless of CPU type used, resource limits for a database or elastic pool (such as the number of cores, memory, max data IOPS, max log rate, and max concurrent workers) remain the same as long as the database stays on the same service objective.

### Compute resources (CPU and memory)

The following table compares compute resources in different hardware configurations and compute tiers for Azure SQL Database. For Hyperscale, see [Hyperscale service tier](service-tier-hyperscale.md#compute-resources). 

|Hardware configuration  |CPU  |Memory  |
|:---------|:---------|:---------|
|Standard-series (Gen5) |**Provisioned compute**<br>- Intel&reg; E5-2673 v4 (Broadwell) 2.3 GHz, Intel&reg; SP-8160 (Skylake)\*, Intel&reg; 8272CL (Cascade Lake) 2.5 GHz\*, Intel&reg; Xeon&reg; Platinum 8370C (Ice Lake)\*, AMD EPYC&trade; 7763v (Milan)\*, AMD EPYC 9004 (Genoa)\*, Intel&reg; Xeon&reg; Platinum 8573C (Emerald Rapids)\* processors<br>- Provision up to 128 vCores (hyper-threaded)<br><br>**Serverless compute**<br>- Intel&reg; E5-2673 v4 (Broadwell) 2.3 GHz, Intel&reg; SP-8160 (Skylake)\*, Intel&reg; 8272CL (Cascade Lake) 2.5 GHz\*, Intel&reg; Xeon&reg; Platinum 8370C (Ice Lake)\*, AMD EPYC&trade; 7763v (Milan)\*, AMD EPYC 9004 (Genoa)\*, Intel&reg; Xeon&reg; Platinum 8573C (Emerald Rapids)\* processors<br>- Autoscale up to 80 vCores (hyper-threaded)<br>- The memory-to-vCore ratio dynamically adapts to memory and CPU usage based on workload demand and can be as high as 24 GB per vCore.  For example, at a given point in time a workload might use and be billed for 240-GB memory and only 10 vCores.|**Provisioned compute**<br>- 5.1 GB per vCore<br>- Provision up to 625 GB<br><br>**Serverless compute**<br>- Autoscale up to 24 GB per vCore<br>- Autoscale up to 240 GB max|
|DC-series     | - Intel&reg; Xeon&reg; E-2288G processors<br>- Featuring Intel Software Guard Extension (Intel SGX)<br>- Provision up to 8 vCores (physical) | 4.5 GB per vCore |
|Fsv2-series\*\*     |- Intel&reg; 8168 (Skylake) processors<br>- Featuring a sustained all core turbo clock speed of 3.4 GHz and a maximum single core turbo clock speed of 3.7 GHz.<br>- Provision up to 72 vCores (hyper-threaded)|- 1.9 GB per vCore<br>- Provision up to 136 GB|

\* For a given compute size and hardware configuration, resource limits are the same regardless of CPU type (Intel&reg; Broadwell, Skylake, Ice Lake, Cascade Lake, Emerald Rapid or AMD Milan, Genoa). In the [sys.dm_user_db_resource_governance](/sql/relational-databases/system-dynamic-management-views/sys-dm-user-db-resource-governor-azure-sql-database) dynamic management view, hardware generation for databases using:
  - Intel&reg; SP-8160 (Skylake) processors appears as Gen6
  - Intel&reg; 8272CL (Cascade Lake) appears as Gen7
  - Intel&reg; Xeon&reg; Platinum 8370C (Ice Lake) or AMD EPYC&trade; 7763v (Milan) appear as Gen8
  - AMD EPYC&trade; 9004 (Genoa) appear as Gen9 or Intel&reg; Xeon&reg; Platinum 8573C (Emerald Rapids) appear as Gen10

\*\* Fsv2-series hardware is no longer available to be created and will be retired October 1, 2026.

For more information, see resource limits for [single databases](resource-limits-vcore-single-databases.md) and [elastic pools](resource-limits-vcore-elastic-pools.md).

### Standard-series (Gen5)

Standard-series (Gen5) hardware provides balanced compute and memory resources, and is suitable for most database workloads.

Standard-series (Gen5) hardware is available in all public regions worldwide.

### Hyperscale premium-series 

Premium-series hardware options use the latest CPU and memory technology from Intel and AMD. Premium-series provides a boost to compute performance relative to standard-series hardware.

- The **Premium-series** option offers faster CPU performance compared to Standard-series and a higher number of maximum vCores.
- The **Premium-series memory optimized option** offers double the amount of memory relative to Standard-series.

Standard-series, premium-series, and premium-series memory optimized are available for [Hyperscale elastic pools](hyperscale-elastic-pool-overview.md).

For more information, see the [Hyperscale premium series blog announcement](https://aka.ms/AAiq28n).
 
For regions available, see [Hyperscale premium-series availability](region-availability.md#hyperscale-premium-series-availability).

### DC-series

- DC-series hardware uses Intel processors with Software Guard Extensions (Intel SGX) technology.
- DC-series is required for [Always Encrypted with secure enclaves](/sql/relational-databases/security/encryption/always-encrypted-enclaves) workloads that require stronger security protection of hardware enclaves, compared to Virtualization-based Security (VBS) enclaves.
- DC-series is designed for workloads that process sensitive data and demand confidential query processing capabilities, provided by Always Encrypted with secure enclaves.
- DC-series hardware provides balanced compute and memory resources.

DC-series is only supported for Provisioned compute (Serverless isn't supported) and doesn't support zone redundancy. For regions where DC-series is available, see [DC-series availability](region-availability.md#dc-series-availability).

#### Azure offer types supported by DC-series

To create databases or elastic pools on DC-series hardware, the subscription must be a paid offer type including Pay-As-You-Go or Enterprise Agreement (EA).  For a complete list of Azure offer types supported by DC-series, see [current offers without spending limits](https://azure.microsoft.com/support/legal/offer-details).

<a id="selecting-hardware-configuration"></a>

### Select hardware configuration

You can select hardware configuration for a database or elastic pool in SQL Database at the time of creation. You can also change hardware configuration of an existing database or elastic pool.

**To select a hardware configuration when creating a SQL Database or pool**

For detailed information, see [Create a SQL Database](single-database-create-quickstart.md).

On the **Basics** tab, select the **Configure database** link in the **Compute + storage** section, and then select the **Change configuration** link:

:::image type="content" source="media/service-tiers-sql-database-vcore/configure-sql-database.png" alt-text="Screenshot of the Azure portal Create SQL Database deployment, on the Configure page. The Change configuration button is highlighted." lightbox="media/service-tiers-sql-database-vcore/configure-sql-database.png":::

Select the desired hardware configuration:

:::image type="content" source="media/service-tiers-sql-database-vcore/select-hardware.png" alt-text="Screenshot of the Azure portal on the SQL hardware configuration page for an Azure SQL database." lightbox="media/service-tiers-sql-database-vcore/select-hardware.png":::

**To change hardware configuration of an existing SQL Database or pool**

For a database, on the Overview page, select the **Pricing tier** link:

:::image type="content" source="media/service-tiers-sql-database-vcore/change-hardware.png" alt-text="Screenshot of the Azure portal on the overview page of Azure SQL Database. The pricing tier 'General Purpose: Standard-series (Gen5), 2 vCores' is highlighted." lightbox="media/service-tiers-sql-database-vcore/change-hardware.png":::

For a pool, on the **Overview** page, select **Configure**.

Follow the steps to change configuration, and select hardware configuration as described in the previous steps.

## Hardware availability

For information on current generation hardware availability, see [Feature Availability by Region for Azure SQL Database](region-availability.md#vcore-purchasing-model-hardware-availability).

### Previous generation hardware

#### Fsv2-series

Fsv2-series hardware is no longer available to be created and will be retired October 1, 2026.

To minimize service disruption and maintain price-performance, transition to Hyperscale premium-series or Standard-series (Gen5) hardware. For more information, see [Retirement Notice: Azure SQL Database FSV2-series offer](https://azure.microsoft.com/updates?id=485030). For most databases and workloads, Hyperscale premium-series or Standard-series (Gen5) hardware provide similar or better price performance than Fsv2. To make sure, please validate this with your specific database and workloads.

- Fsv2 provides less memory and `tempdb` per vCore than other hardware, so workloads sensitive to those limits might perform better on standard-series (Gen5).
- Fsv2-series is only supported in the General Purpose tier.

#### Gen4

Gen4 hardware has been retired and isn't available for provisioning, upscaling, or downscaling. Migrate your database to a supported hardware generation for a wider range of vCore and storage scalability, accelerated networking, best IO performance, and minimal latency. Review [hardware options for single databases](resource-limits-vcore-single-databases.md) and [hardware options for elastic pools](resource-limits-vcore-elastic-pools.md). For more information, see [Support has ended for Gen 4 hardware on Azure SQL Database](https://azure.microsoft.com/updates/support-has-ended-for-gen-4-hardware-on-azure-sql-database/).

## Next step

> [!div class="nextstepaction"]
> [Quickstart: Create a single database - Azure SQL Database](single-database-create-quickstart.md)

## Related content

- [Azure SQL Database pricing page](https://azure.microsoft.com/pricing/details/sql-database/single/)
- [Resource limits for single databases using the vCore purchasing model](resource-limits-vcore-single-databases.md)
- [Resource limits for elastic pools using the vCore purchasing model](resource-limits-vcore-elastic-pools.md)
- [Modifiable configuration reference for Azure SQL Database](modifiable-configuration-reference.md)

