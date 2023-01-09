---
title: vCore purchasing model
description: The vCore purchasing model lets you independently scale compute and storage resources, match on-premises performance, and optimize price for Azure SQL Managed Instance.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: sashan, moslake
ms.date: 09/28/2022
ms.service: sql-managed-instance
ms.subservice: performance
ms.topic: conceptual
ms.custom: ignite-fall-2021
---
# vCore purchasing model - Azure SQL Managed Instance
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](../database/service-tiers-sql-database-vcore.md)
> * [Azure SQL Managed Instance](service-tiers-managed-instance-vcore.md)

This article reviews the [vCore purchasing model](../database/service-tiers-vcore.md) for [Azure SQL Managed Instance](sql-managed-instance-paas-overview.md). 

## Overview

[!INCLUDE [vcore-overview](../includes/vcore-overview.md)]

The virtual core (vCore) purchasing model used by Azure SQL Managed Instance provides the following benefits: 

- Control over hardware configuration to better match the compute and memory requirements of the workload.
- Pricing discounts for [Azure Hybrid Benefit (AHB)](../azure-hybrid-benefit.md) and [Reserved Instance (RI)](../database/reserved-capacity-overview.md).
- Greater transparency in the hardware details that power compute, helping facilitate planning for migrations from on-premises deployments.
- Higher scaling granularity with multiple compute sizes available.

## Compute cost 

The vCore-based purchasing model has a provisioned compute tier for both Azure SQL Database and Azure SQL Managed Instance, and a serverless compute tier for Azure SQL Database. 

In the provisioned compute tier, the compute cost reflects the total compute capacity continuously provisioned for the application independent of workload activity. Choose the resource allocation that best suits your business needs based on vCore and memory requirements, then scale resources up and down as needed by your workload.

In the serverless compute tier for Azure SQL database, compute resources are auto-scaled based on workload capacity and billed for the amount of compute used, per second. 

Since three additional replicas are automatically allocated in the Business Critical service tier, the price is approximately 2.7 times higher than it is in the General Purpose service tier. Likewise, the higher storage price per GB in the Business Critical service tier reflects the higher IO limits and lower latency of the local SSD storage.

## Data and log storage

The following factors affect the amount of storage used for data and log files, and apply to General Purpose and Business Critical tiers. 

- Each compute size supports a configurable maximum data size, with a default of 32 GB.
- When you configure maximum data size, an additional 30 percent of billable storage is automatically added for the log file.
- In the General Purpose service tier, `tempdb` uses local SSD storage, and this storage cost is included in the vCore price.
- In the Business Critical service tier, `tempdb` shares local SSD storage with data and log files, and `tempdb` storage cost is included in the vCore price.
- In the General Purpose and Business Critical tiers, you are charged for the maximum storage size configured for a database, elastic pool, or managed instance.
- For SQL Database, you can select any maximum data size between 1 GB and the supported storage size maximum, in 1 GB increments. For SQL Managed Instance, select data sizes in multiples of 32 GB up to the supported storage size maximum. 

To monitor the current allocated and used data storage size in SQL Database, use the *allocated_data_storage* and *storage* Azure Monitor [metrics](/azure/azure-monitor/essentials/metrics-supported#microsoftsqlserversdatabases) respectively. 

For both SQL Database and SQL Managed instance, to monitor the current allocated and used storage size of individual data and log files in a database by using T-SQL, use the [sys.database_files](/sql/relational-databases/system-catalog-views/sys-database-files-transact-sql) view and the [FILEPROPERTY(... , 'SpaceUsed')](/sql/t-sql/functions/fileproperty-transact-sql) function.

> [!TIP]
> Under some circumstances, you may need to shrink a database to reclaim unused space. For more information, see [Manage file space in Azure SQL Database](file-space-manage.md).

## Backup storage

Storage for database backups is allocated to support the [point-in-time restore (PITR)](recovery-using-backups.md) and [long-term retention (LTR)](long-term-retention-overview.md) capabilities of SQL Database and SQL Managed Instance. This storage is separate from data and log file storage, and is billed separately.

- **PITR**: In General Purpose and Business Critical tiers, individual database backups are copied to [Azure storage](automated-backups-overview.md#restore-capabilities) automatically. The storage size increases dynamically as new backups are created. The storage is used by full, differential, and transaction log backups. The storage consumption depends on the rate of change of the database and the retention period configured for backups. You can configure a separate retention period for each database between 1 and 35 days for SQL Database, and 0 to 35 days for SQL Managed Instance. A backup storage amount equal to the configured maximum data size is provided at no extra charge.
- **LTR**: You also have the option to configure long-term retention of full backups for up to 10 years. If you set up an LTR policy, these backups are stored in Azure Blob storage automatically, but you can control how often the backups are copied. To meet different compliance requirements, you can select different retention periods for weekly, monthly, and/or yearly backups. The configuration you choose determines how much storage will be used for LTR backups. For more information, see [Long-term backup retention](long-term-retention-overview.md).


## <a id="compute-tiers"></a>Service tiers

Service tier options in the vCore purchasing model include General Purpose and Business Critical. The service tier generally defines the storage architecture, space and I/O limits, and business continuity options related to availability and disaster recovery. 

For more details, review [resource limits](resource-limits.md). 

|**Category**|**General Purpose**|**Business Critical**|
|---|---|---|
|**Best for**|Most business workloads. Offers budget-oriented, balanced, and scalable compute and storage options. |Offers business applications the highest resilience to failures by using several isolated replicas, and provides the highest I/O performance.|
|**Read-only replicas**| 0 | 1 | 
|**Replicas for availability**|One replica for high availability| Three high availability replicas, 1 is also a [read-scale replica](../database/read-scale-out.md) |
|**Read-only replicas with [failover groups](auto-failover-group-sql-mi.md) enabled**| One additional read-only replica. Two total readable replicas, which includes the primary replica. | Two additional read-only replicas, three total read-only replicas. Four total readable replicas, which includes the primary replica. |
|**Pricing/billing**| [vCore, reserved storage, and backup storage](https://azure.microsoft.com/pricing/details/sql-database/managed/) is charged. <br/>IOPS is not charged| [vCore, reserved storage, and backup storage](https://azure.microsoft.com/pricing/details/sql-database/managed/) is charged. <br/>IOPS is not charged.
|**Discount models**| [Reserved instances](../database/reserved-capacity-overview.md)<br/>[Azure Hybrid Benefit](../azure-hybrid-benefit.md) (not available on dev/test subscriptions)<br/>[Enterprise](https://azure.microsoft.com/offers/ms-azr-0148p/) and [Pay-As-You-Go](https://azure.microsoft.com/offers/ms-azr-0023p/) Dev/Test subscriptions|[Reserved instances](../database/reserved-capacity-overview.md)<br/>[Azure Hybrid Benefit](../azure-hybrid-benefit.md) (not available on dev/test subscriptions)<br/>[Enterprise](https://azure.microsoft.com/offers/ms-azr-0148p/) and [Pay-As-You-Go](https://azure.microsoft.com/offers/ms-azr-0023p/) Dev/Test subscriptions|


> [!NOTE]
> For more information on the Service Level Agreement (SLA), see [SLA for Azure SQL Managed Instance](https://azure.microsoft.com/support/legal/sla/azure-sql-sql-managed-instance/). 

### General Purpose

The architectural model for the General Purpose service tier is based on a separation of compute and storage. This architectural model relies on high availability and reliability of Azure Blob storage that transparently replicates database files and guarantees no data loss if underlying infrastructure failure happens.

The following figure shows four nodes in standard architectural model with the separated compute and storage layers.

![Separation of compute and storage](./media/service-tier-general-purpose/general-purpose-service-tier.png)

In the architectural model for the General Purpose service tier, there are two layers:

- A stateless compute layer that is running the `sqlservr.exe` process and contains only transient and cached data (for example – plan cache, buffer pool, column store pool). This stateless node is operated by Azure Service Fabric that initializes process, controls health of the node, and performs failover to another place if necessary.
- A stateful data layer with database files (.mdf/.ldf) that are stored in Azure Blob storage. Azure Blob storage guarantees that there will be no data loss of any record that is placed in any database file. Azure Storage has built-in data availability/redundancy that ensures that every record in log file or page in data file will be preserved even if the process crashes.

Whenever the database engine or operating system is upgraded, some part of underlying infrastructure fails, or if some critical issue is detected in the `sqlservr.exe` process, Azure Service Fabric will move the stateless process to another stateless compute node. There is a set of spare nodes that is waiting to run new compute service if a failover of the primary node happens in order to minimize failover time. Data in Azure storage layer is not affected, and data/log files are attached to newly initialized process. This process guarantees 99.99% availability by default and 99.995% availability when [zone redundancy](high-availability-sla.md#general-purpose-service-tier-zone-redundant-availability) is enabled. There may be some performance impacts on heavy workloads that are running due to transition time and the fact the new node starts with cold cache.

#### When to choose this service tier

The General Purpose service tier is a default service tier in Azure SQL Managed Instance that is designed for most of generic workloads. If you need a fully managed database engine with a default SLA and storage latency between 5 and 10 ms, the General Purpose tier is the option for you.

### Business Critical 

The Business Critical service tier model is based on a cluster of database engine processes. This architectural model relies on a fact that there's always a quorum of available database engine nodes and has minimal performance impact on your workload even during maintenance activities. 

Azure upgrades and patches underlying operating system, drivers, and SQL Server database engine transparently with the minimal down-time for end users. 


In the Business Critical model, compute and storage is integrated on each node. High availability is achieved by replication of data between database engine processes on each node of a four node cluster, with each node using locally attached SSD as data storage. This technology is similar to SQL Server [Always On availability groups](/sql/database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server).

![Cluster of database engine nodes](./media/service-tier-business-critical/business-critical-service-tier.png)

Both the SQL Server database engine process and underlying .mdf/.ldf files are placed on the same node with locally attached SSD storage providing low latency to your workload. High availability is implemented using technology similar to SQL Server [Always On availability groups](/sql/database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server). Every database is a cluster of database nodes with one primary database that is accessible for customer workloads, and a three secondary processes containing copies of data. The primary node constantly pushes changes to the secondary nodes in order to ensure that the data is available on secondary replicas if the primary node fails for any reason. Failover is handled by the SQL Server database engine – one secondary replica becomes the primary node and a new secondary replica is created to ensure there are enough nodes in the cluster. The workload is automatically redirected to the new primary node.

In addition, the Business Critical cluster has built-in [Read Scale-Out](read-scale-out.md) capability that provides free-of charge built-in read-only replica that can be used to run read-only queries (for example reports) that shouldn't affect performance of your primary workload.

### When to choose this service tier

The Business Critical service tier is designed for applications that require low-latency responses from the underlying SSD storage (1-2 ms in average), fast recovery if the underlying infrastructure fails, or need to off-load reports, analytics, and read-only queries to the free of charge readable secondary replica of the primary database.

The key reasons why you should choose Business Critical service tier instead of General Purpose tier are:
-    **Low I/O latency requirements** – workloads that need a fast response from the storage layer (1-2 milliseconds in average) should use Business Critical tier. 
-    **Workload with reporting and analytic queries** that can be redirected to the free-of-charge secondary read-only replica.
- **Higher resiliency and faster recovery from failures**. In a case of system failure, the database on primary instance will be disabled and one of the secondary replicas will be immediately became new read-write primary database that is ready to process queries. The database engine doesn't need to analyze and redo transactions from the log file and load all data in the memory buffer.
- **Advanced data corruption protection**. The Business Critical tier leverages database replicas behind-the-scenes for business continuity purposes, and so the service also then leverages automatic page repair, which is the same technology used for SQL Server database [mirroring and availability groups](/sql/sql-server/failover-clusters/automatic-page-repair-availability-groups-database-mirroring). In the event that a replica can't read a page due to a data integrity issue, a fresh copy of the page will be retrieved from another replica, replacing the unreadable page without data loss or customer downtime. This functionality is applicable in General Purpose tier if the database has geo-secondary replica.
- **Higher availability** - The Business Critical tier in Multi-AZ configuration provides resiliency to zonal failures and a higher availability SLA.
- **Fast geo-recovery** - When [active geo-replication](active-geo-replication-overview.md) is configured, the Business Critical tier has a guaranteed Recovery Point Objective (RPO) of 5 seconds and Recovery Time Objective (RTO) of 30 seconds for 100% of deployed hours.



## Compute

SQL Managed Instance compute provides a specific amount of compute resources that are continuously provisioned independent of workload activity, and bills for the amount of compute provisioned at a fixed price per hour.

## Hardware configurations

Hardware configuration options in the vCore model include standard-series (Gen5), premium-series, and memory optimized premium-series. Hardware configuration generally defines the compute and memory limits and other characteristics that impact workload performance.

For more information on the hardware configuration specifics and limitations, see [Hardware configuration characteristics](resource-limits.md#hardware-configuration-characteristics).

In the [sys.dm_user_db_resource_governance](/sql/relational-databases/system-dynamic-management-views/sys-dm-user-db-resource-governor-azure-sql-database) dynamic management view, hardware generation for instances using Intel&reg; SP-8160 (Skylake) processors appears as Gen6, while hardware generation for instances using Intel&reg; 8272CL (Cascade Lake) appears as Gen7. The Intel&reg; 8370C (Ice Lake) CPUs used by premium-series and memory optimized premium-series hardware generations appear as Gen8. Resource limits for all standard-series (Gen5) instances are the same regardless of processor type (Broadwell, Skylake, or Cascade Lake).

### Selecting a hardware configuration

You can select hardware configuration at the time of instance creation, or you can change hardware of an existing instance.

**To select hardware configuration when creating a SQL Managed Instance**

For detailed information, see [Create a SQL Managed Instance](../managed-instance/instance-create-quickstart.md).

On the **Basics** tab, select the **Configure database** link in the **Compute + storage** section, and then select desired hardware:

:::image type="content" source="../database/media/service-tiers-vcore/configure-managed-instance.png" alt-text="configure SQL Managed Instance"  loc-scope="azure-portal":::
  
**To change hardware of an existing SQL Managed Instance**

#### [The Azure portal](#tab/azure-portal)

From the SQL Managed Instance page, select **Pricing tier** link placed under the Settings section

:::image type="content" source="../database/media/service-tiers-vcore/change-managed-instance-hardware.png" alt-text="change SQL Managed Instance hardware"  loc-scope="azure-portal":::

On the Pricing tier page, you will be able to change hardware as described in the previous steps.

#### [PowerShell](#tab/azure-powershell)

Use the following PowerShell script:

```powershell-interactive
Set-AzSqlInstance -Name "managedinstance1" -ResourceGroupName "ResourceGroup01" -ComputeGeneration Gen5
```

For more details, check [Set-AzSqlInstance](/powershell/module/az.sql/set-azsqlinstance) command.

#### [The Azure CLI](#tab/azure-cli)

Use the following CLI command:

```azurecli-interactive
az sql mi update -g mygroup -n myinstance --family Gen5
```

For more details, check [az sql mi update](/cli/azure/sql/mi#az-sql-mi-update) command.

---

When specifying hardware parameter in templates or scripts, hardware is provided by using its name. The following table applies:

|Hardware|Name|
|:-- |:-- |
|Standard-series (Gen5)|Gen5|
|Premium-series|G8IM|
|Memory optimized premium-series|G8IH|

### Hardware availability

#### <a id="gen4gen5-1"></a> Gen4

[!INCLUDE[azure-sql-gen4-hardware-retirement](../includes/azure-sql-gen4-hardware-retirement.md)]

#### Standard-series (Gen5) and premium-series

Standard-series (Gen5) and premium-series hardware is available in all public regions worldwide.
  
Memory optimized premium-series hardware is in preview, and has limited regional availability. For more information, see [Azure SQL Managed Instance resource limits](../managed-instance/resource-limits.md#hardware-configuration-characteristics).

## Next steps

- To get started, see [Creating a SQL Managed Instance using the Azure portal](instance-create-quickstart.md)
- For pricing details, see 
    - [Azure SQL Managed Instance single instance pricing page](https://azure.microsoft.com/pricing/details/azure-sql-managed-instance/single/)
    - [Azure SQL Managed Instance pools pricing page](https://azure.microsoft.com/pricing/details/azure-sql-managed-instance/pools/)
- For details about the specific compute and storage sizes available in the General Purpose and Business Critical service tiers, see [vCore-based resource limits for Azure SQL Managed Instance](resource-limits.md).
