---
title: Resource Limits
titleSuffix: Azure SQL Managed Instance
description: This article provides an overview of the resource limits for Azure SQL Managed Instance.
author: vladai78
ms.author: vladiv
ms.reviewer: mathoma, sachinp, wiassaf, randolphwest
ms.date: 08/10/2026
ms.service: azure-sql-managed-instance
ms.subservice: service-overview
ms.topic: reference
ms.custom:
  - references_regions
  - ignite-2025
ai-usage: ai-assisted
---
# Overview of Azure SQL Managed Instance resource limits

[!INCLUDE [appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

> [!div class="op_single_selector"]
> - [Azure SQL Database](../database/resource-limits-logical-server.md?view=azuresql-db&preserve-view=true)
> - [Azure SQL Managed Instance](resource-limits.md?view=azuresql-mi&preserve-view=true)

This article provides an overview of the technical characteristics and resource limits for Azure SQL Managed Instance, and provides information about how to request an increase to these limits.

> [!NOTE]
> For differences in supported features and T-SQL statements see [Feature differences](../database/features-comparison.md) and [T-SQL statement support](transact-sql-tsql-differences-sql-server.md). For general differences between service tiers for Azure SQL Database and SQL Managed Instance review [General Purpose](../database/service-tiers-sql-database-vcore.md#general-purpose) and [Business Critical](../database/service-tiers-sql-database-vcore.md#business-critical) service tiers.

## Hardware configuration characteristics

In this section:

- [Regional supports for memory optimized premium-series hardware and for premium-series hardware with 16-TB storage](#regional-supports-for-memory-optimized-premium-series-hardware-and-for-premium-series-hardware-with-16-tb-storage)
- [In-memory OLTP available space](#in-memory-oltp-available-space)

SQL Managed Instance has characteristics and resource limits that depend on the underlying infrastructure and architecture. SQL Managed Instance can be deployed on multiple hardware generations.

Hardware generations have different characteristics, as described in the following table:

| Feature | Standard-series (Gen5) | Premium-series | Memory optimized premium-series |
| :-- | :-- | :-- | :-- |
| **CPU** | Intel&reg; E5-2673 v4 (Broadwell) 2.3 GHz, Intel&reg; SP-8160 (Skylake), and Intel&reg; 8272CL (Cascade Lake) 2.5-GHz processors | Intel&reg; 8370C (Ice Lake) 2.8-GHz processors | Intel&reg; 8370C (Ice Lake) 2.8-GHz processors |
| **Number of vCores**<br />vCore=1 LP (hyper-thread) | 2<sup>1</sup> -80 vCores | 2<sup>1</sup> -128 vCores | 4-128 vCores |
| **Max memory (memory/vCore ratio)** | 5.1 GB per vCore - 408 GB maximum<br />Add more vCores to get more memory. | 7 GB per vCore up to 80 vCores - 560 GB maximum | 13.6 GB per vCore up to 64 vCores - 870.4 GB maximum |
| **Max In-Memory OLTP memory** | Instance limit: 0.8 - 1.65 GB per vCore | Instance limit: 1.1 - 2.3 GB per vCore | Instance limit: 2.2 - 4.5 GB per vCore |
| **Max instance reserved storage** <sup>2</sup> | **General Purpose:** up to 32 TB<sup>4</sup><br />**Business Critical:** up to 4 TB | **General Purpose:** up to 32 TB<sup>4</sup><br />**Business Critical:** up to 16 TB<sup>3</sup> | **General Purpose:** up to 32 TB<sup>4</sup><br />**Business Critical:** up to 16 TB |

<sup>1</sup> Deploying a 2-vCore instance is only possible inside an [instance pool](instance-pools-overview.md).   
<sup>2</sup> Dependent on [the number of vCores](#service-tier-characteristics).   
<sup>3</sup> The [following regions](#regional-supports-for-memory-optimized-premium-series-hardware-and-for-premium-series-hardware-with-16-tb-storage) can provide 16 TB of storage, while other regions limit available storage to 5.5 TB.   
<sup>4</sup> 16 TB for classic General Purpose. 32 TB only for [Next-gen General Purpose service tier](service-tiers-next-gen-general-purpose-use.md)

> [!NOTE]  
> If your workload requires storage sizes greater than the available resource limits for Azure SQL Managed Instance, consider the Azure SQL Database [Hyperscale service tier](../database/service-tier-hyperscale.md).

### Regional supports for memory optimized premium-series hardware and for premium-series hardware with 16-TB storage

Support for the premium-series hardware with 16-TB storage has the same availability as support for the memory optimized premium-series hardware. To learn more, review [Regional supports for memory optimized premium-series hardware and for premium-series hardware with 16-TB storage](region-availability.md#regional-supports-for-memory-optimized-premium-series-hardware-and-for-premium-series-hardware-with-16-tb-storage).

### In-memory OLTP available space

The amount of In-memory OLTP space in [Business Critical](../database/service-tiers-sql-database-vcore.md#business-critical) service tier depends on the number of vCores and hardware configuration. The following table lists the limits of memory that can be used for In-memory OLTP objects.

| vCores | Standard-series (Gen5) | Premium-series | Memory optimized premium-series |
| --- | --- | --- | --- |
| 4 vCores | 3.14 GB | 4.39 GB | 8.79 GB |
| 6 vCores | - | 6.59 GB | 15.32 GB |
| 8 vCores | 6.28 GB | 8.79 GB | 22.06 GB |
| 10 vCores | - | 12.11 GB | 30.94 GB |
| 12 vCores | - | 15.43 GB | 39.82 GB |
| 16 vCores | 15.77 GB | 22.06 GB | 57.58 GB |
| 20 vCores | - | 28.70 GB | 75.34 GB |
| 24 vCores | 25.25 GB | 35.34 GB | 93.09 GB |
| 32 vCores | 37.94 GB | 53.09 GB | 128.61 GB |
| 40 vCores | 52.23 GB | 73.09 GB | 164.13 GB |
| 48 vCores | - | 95.34 GB | 199.64 GB |
| 56 vCores | - | 117.58 GB | 244.13 GB |
| 64 vCores | 99.9 GB | 139.82 GB | 288.61 GB |
| 80 vCores | 131.68 GB | 184.30 GB | 288.61 GB |
| 96 vCores | N/A | 184.30 GB | 288.61 GB |
| 128 vCores | N/A | 184.30 GB | 288.61 GB |

## Service tier characteristics

SQL Managed Instance has two [service tiers](service-tiers-managed-instance-vcore.md#compute-tiers): General Purpose and Business Critical. You can choose to use the upgraded [Next-gen General Purpose service tier](service-tiers-next-gen-general-purpose-use.md).

> [!IMPORTANT]  
> The Business Critical service tier provides an additional built-in copy of the SQL Managed Instance (secondary replica) that can be used for read-only workload. If you can separate read-write queries and read-only/analytic/reporting queries, you're getting twice the vCores and memory for the same price. The secondary replica might lag a few seconds behind the primary instance, so it's designed to offload reporting/analytic workloads that don't need exact current state of data. In the following table, **read-only queries** are the queries that are executed on secondary replica.

In this section:

- [Number of vCores](#number-of-vcores)
- [Max memory](#max-memory)
- [Maximum instance storage size](#maximum-instance-storage-size-reserved)
- [Service tier characteristics comparison](#service-tier-characteristics-comparison)
- [IOPS and throughput](#iops-and-throughput)
- [Data and log storage](#data-and-log-storage)
- [Backups and storage](#backups-and-storage)
- [Additional characteristics considerations](#additional-characteristics-considerations)


### Number of vCores 

| Hardware generation | General Purpose | Next-gen General Purpose | Business Critical |
| --- | --- | --- | --- |
| **Standard-series (Gen5)** | 2<sup>1</sup>, 4, 8, 16, 24, 32, 40, 64, 80 | 4, 8, 16, 24, 32, 40, 64, 80 | 4, 8, 16, 24, 32, 40, 64, 80 |
| **Premium-series** | 2<sup>1</sup>, 4, 8, 16, 24, 32, 40, 64, 80 | 4, 6, 8, 10, 12, 16, 20, 24, 32, 40, 48, 56, 64, 80, 96, 128 | 4, 6, 8, 10, 12, 16, 20, 24, 32, 40, 48, 56, 64, 80, 96, 128 |
| **Memory optimized premium-series** | 4, 8, 16, 24, 32, 40, 64, 80 | 4, 6, 8, 10, 12, 16, 20, 24, 32, 40, 48, 56, 64, 80, 96, 128 | 4, 6, 8, 10, 12, 16, 20, 24, 32, 40, 48, 56, 64, 80, 96, 128 |

<sup>1</sup> Deploying a 2-vCore instance is only possible inside an [instance pool](instance-pools-overview.md).

### Max memory

| Hardware generation | General Purpose | Next-gen General Purpose | Business Critical |
| --- | --- | --- | --- |
| **Standard-series (Gen5)** | 20.4 GB - 408 GB<br />5.1 GB/vCore | 20.4 GB - 408 GB<br />5.1 GB/vCore | 20.4 GB - 408 GB<br />5.1 GB/vCore on each replica |
| **Premium-series** | 28 GB - 560 GB<br />7 GB/vCore | 28 GB - 560 GB<br />7 GB/vCore | 28 GB - 560 GB<br />7 GB/vCore up to 80 vCores<sup>1</sup> on each replica |
| **Memory optimized premium-series** | 54.4 GB - 870.4 GB<br />13.6 GB/vCore | 54.4 GB - 870.4 GB<br />13.6 GB/vCore | 54.4 GB - 870.4 GB<br />13.6 GB/vCore up to 64 vCores<sup>1</sup> on each replica |

<sup>1</sup> The memory-to-vCore ratio is only available up to 80 vCores for premium-series hardware, and 64 vCores for memory optimized premium-series. Maximum memory is capped at 560 GB for premium-series vCores above 80, and 870.4 GB for memory optimized premium-series vCores above 64.

### Maximum instance storage size (reserved)

The following table lists the maximum storage size based on the number of vCores for each hardware generation and service tier: 

| Hardware generation | General Purpose<sup>1</sup> | Next-gen General Purpose | Business Critical<sup>2</sup> |
| --- | --- | --- | --- |
| **Standard-series (Gen5)** | - 2 TB for 4 vCores<br />- 8 TB for 8 vCores<br />- 16 TB for other sizes | - 2 TB for 4 vCores<br />- 8 TB for 8 vCores<br />- 16 TB for 16, 24 vCores<br />- 32 TB for 32, 40, 64, 80 vCores | - 1 TB for 4, 8, 16 vCores<br />- 2 TB for 24 vCores<br />- 4 TB for 32, 40, 64, 80 vCores |
| **Premium-series** | - 2 TB for 4 vCores<br />- 8 TB for 8 vCores<br />- 16 TB for other sizes | - 2 TB for 4, 6 vCores<br />- 8 TB for 8, 10, 12 vCores<br />- 16 TB for 16, 20, 24 vCores<br />- 32 TB for 32, 40, 48, 56, 64, 80, 96, 128 vCores | - 1 TB for 4, 6 vCores<br />- 2 TB for 8, 10, 12 vCores<br />- 4 TB for 16, 20 vCores<br />- 5.5 TB for 24, 32, 40, 48, 56 vCores<br />- 5.5 TB or 16 TB (depending on the region) for 64, 80, 96, 128 vCores<sup>3</sup> |
| **Memory optimized premium-series** | - 2 TB for 4 vCores<br />- 8 TB for 8 vCores<br />- 16 TB for other sizes | - 2 TB for 4, 6 vCores<br />- 8 TB for 8, 10, 12 vCores<br />- 16 TB for 16, 20, 24 vCores<br />- 32 TB for 32, 40, 48, 56, 64, 80, 96, 128 vCores | - 1 TB for 4, 6 vCores<br />- 2 TB for 8, 10, 12 vCores<br />- 4 TB for 16, 20 vCores<br />- 5.5 TB for 24 vCores<br />- 5.5 TB or 8 TB (depending on the region) for 32, 40 vCores<sup>4</sup><br />- 12 TB for 48, 56 vCores<br />- 16 TB for 64, 80, 96, 128 vCores |

<sup>1</sup> In the General Purpose service tier, `tempdb` uses local SSD storage.

<sup>2</sup> In the Business Critical service tier, `tempdb` shares local SSD storage with data and log files. The size of `tempdb` can grow up to the currently available instance storage size. 

<sup>3</sup> Only [the major regions](#regional-supports-for-memory-optimized-premium-series-hardware-and-for-premium-series-hardware-with-16-tb-storage) can provide 16 TB of storage for the premium-series hardware for these CPU vCore numbers. Smaller regions limit available storage to 5.5 TB.

<sup>4</sup> Only [the major regions](#regional-supports-for-memory-optimized-premium-series-hardware-and-for-premium-series-hardware-with-16-tb-storage) can provide 8 TB of storage for the premium-series memory optimized hardware for these CPU vCore numbers. Smaller regions limit available storage to 5.5 TB.

### Service tier characteristics comparison

| Feature | General Purpose | Next-gen General Purpose | Business Critical |
| --- | --- | --- | --- |
| Max database size | Up to currently available instance size (depending on the number of vCores). | Up to currently available instance size (depending on the number of vCores). | Up to currently available instance size (depending on the number of vCores). |
| Max `tempdb` database size | Limited to 24 GB/vCore (96 - 1,920 GB) and currently available instance storage size.<br />Add more vCores to get more `tempdb` space.<br />Log file size is limited to 120 GB. | Limited to 24 GB/vCore (96 - 1,920 GB) and currently available instance storage size.<br />Add more vCores to get more `tempdb` space.<br />Log file size is limited to 120 GB. | Up to currently available instance storage size. |
| Max number of `tempdb` files | 128 | 128 | 128 |
| Max number of databases per instance | 100 user databases, unless the instance storage size limit has been reached. | 500 user databases | 100 user databases, unless the instance storage size limit has been reached. |
| Max number of database files | 280 per instance, unless the instance storage size or [Azure Premium Disk storage allocation space](doc-changes-updates-known-issues.md#exceeding-storage-space-with-small-database-files) limit has been reached. | 4,096 files per database | 32,767 files per database, unless the instance storage size limit has been reached. |
| Max data file size | Maximum size of each data file is 8 TB. Use at least two data files for databases larger than 8 TB. | Up to currently available instance size (depending on the number of vCores). | Up to currently available instance size (depending on the number of vCores). |
| Max log file size | Limited to 2 TB and currently available instance storage size. | Limited to 2 TB and currently available instance storage size. | Limited to 2 TB and currently available instance storage size. |
| Data/Log IOPS (approximate) | 500 - 7500 per file<br />\*[Increase file size to get more IOPS](#iops-and-throughput) | Reserved storage * 3 - up to the VM limit. 300 in case of 32 GB, 64 GB, and 96 GB of reserved storage.<br />VM limit depends on the number of vCores<br />6400 IOPS for a VM with 4 vCores - 80 K IOPS for a VM with 128 vCores | 16 K - 320 K (4000 IOPS/vCore)<br />Add more vCores to get better IO performance. |
| Data throughput (approximate) | 100 - 250 MiB/s per file<br />\*[Increase the file size to get better IO performance](#iops-and-throughput) | IOPS / 30 MBps - up to the VM limit. 75 MBps in case of 32 GB, 64 GB, and 96 GB of reserved storage. | Not limited. |
| Log write throughput limit (per instance) | 4.5 MiB/s per vCore<br />Max 120 MiB/s per instance<br />22 - 65 MiB/s per DB (depending on log file size)<br />\*[Increase the file size to get better IO performance](#iops-and-throughput) | 4.5 MiB/s per vCore<br />Max 192 MiB/s | Standard-series:<br />4.5 MiB/s per vCore<br />Max 96 MiB/s<br /><br />Premium-series and Memory optimized premium-series:<br />12 MiB/s per vCore<br />Max 192 MiB/s |
| Storage IO latency (approximate<sup>1</sup>) | 5-10 ms | 3-5 ms | 1-2 ms |
| In-memory OLTP | Not supported | Not supported | Available, [size depends on number of vCore](#in-memory-oltp-available-space) |
| Max sessions | 30000 | 30000 | 30000 |
| Max concurrent workers | 105 * number of vCores + 800 | 105 * number of vCores + 800 | 105 * number of vCores + 800 |
| [Use read-only replicas to offload read-only query workloads](../database/read-scale-out.md) | 0 | 0 | 1 (included in price) |
| Compute isolation | Not supported as General Purpose instances might share physical hardware with other instances | Not supported as Next-gen General Purpose instances might share physical hardware with other instances | **Standard-series (Gen5)**:<br />Supported for configurations with 64 or more vCores<br />**Premium-series**: Supported for configurations with 64 or more vCores<br />**Memory optimized premium-series**: Supported for configurations with 64 or more vCores |
| Replicas for availability | Stand by nodes for high availability | Stand by nodes for high availability | Four high availability replicas, 1 is also a [read-scale replica](../database/read-scale-out.md) |
| Read-only replicas with [failover groups](failover-group-sql-mi.md) enabled | One additional read-only replica. Two total readable replicas, which include the primary replica. | One additional read-only replica. Two total readable replicas, which include the primary replica. | Two additional read-only replicas, three total read-only replicas. Four total readable replicas, which include the primary replica. |
| Pricing/billing | [vCore, reserved storage, and backup storage](https://azure.microsoft.com/pricing/details/sql-database/managed/) is charged.<br />IOPS aren't charged | vCore, reserved storage, backup storage and IOPS (over the free quota) are charged. | [vCore, reserved storage, and backup storage](https://azure.microsoft.com/pricing/details/sql-database/managed/) is charged.<br />IOPS aren't charged. |
| Discount models | [Azure Reservations](../database/reservations-discount-overview.md)<br />[Azure Hybrid Benefit - Azure SQL Database & SQL Managed Instance](../azure-hybrid-benefit.md) (not available on dev/test subscriptions)<br />[Enterprise](https://azure.microsoft.com/offers/ms-azr-0148p/) and [pay-as-you-go Dev/Test](https://azure.microsoft.com/offers/ms-azr-0023p/) subscriptions | [Azure Reservations](../database/reservations-discount-overview.md)<br />[Azure Hybrid Benefit - Azure SQL Database & SQL Managed Instance](../azure-hybrid-benefit.md) (not available on dev/test subscriptions)<br />[Enterprise](https://azure.microsoft.com/offers/ms-azr-0148p/) and [pay-as-you-go Dev/Test](https://azure.microsoft.com/offers/ms-azr-0023p/) subscriptions |[Azure Reservations](../database/reservations-discount-overview.md)<br />[Azure Hybrid Benefit - Azure SQL Database & SQL Managed Instance](../azure-hybrid-benefit.md) (not available on dev/test subscriptions)<br />[Enterprise](https://azure.microsoft.com/offers/ms-azr-0148p/) and [pay-as-you-go Dev/Test](https://azure.microsoft.com/offers/ms-azr-0023p/) subscriptions |

<sup>1</sup> This is an average range. Although the vast majority of IO request durations will fall under the top of the range, outliers which exceed the range are possible.

### IOPS and throughput

IOPS and throughput are characteristics of the IO file system. 

IOPS and throughput differ by service tier in the following ways: 

- In the **Business Critical** service tier, throughput is unlimited.
- In the **General Purpose** service tier, throughput depends on the file size.
- In the **Next-gen General Purpose** service tier, throughput scales proportionally with the number of IOPS allocated to the instance.

The following sections describe IO characteristics by service tier in more detail. 

#### IOPS and throughput in the General Purpose service tier

In the General Purpose service tier, every database file gets dedicated IOPS and throughput that depend on the file size. Larger files get more IOPS and throughput. 

The following table shows IO characteristics of database files in the **General Purpose** service tier:

| File size | >=0 and <=129 GiB | >129 and <=513 GiB | >513 and <=1025 GiB | >1025 and <=2049 GiB | >2049 and <=4097 GiB | >4097 GiB and <=8 TiB |
| --- | --- | --- | --- | --- | --- | --- |
| **IOPS per file** | 500 | 2300 | 5000 | 7500 | 7500 | 7500 |
| **Throughput per file** | 100 MiB/s | 150 MiB/s | 200 MiB/s | 250 MiB/s | 250 MiB/s | 250 MiB/s |

If you notice high IO latency on some database file or you see that IOPS/throughput is reaching the limit, you might improve performance by [increasing the file size](https://techcommunity.microsoft.com/blog/azuresqlblog/increase-data-file-size-to-improve-hammerdb-workload-performance-on-general-purp/823337).

There's also an instance-level limit on the max log write throughput (see the previous table for values, for example 22 MiB/s), so you might not be able to reach the max file throughout on the log file because you're hitting the instance throughput limit.

#### IOPS and throughput in the Next-gen General Purpose service tier

The **Next-gen General Purpose** service tier has the following IOPS characteristics: 
- **Minimum guaranteed IOPS**: Every instance is guaranteed a minimum of 300 IOPS, regardless of the reserved storage.
- **IOPS scales with reserved storage**: For every 1GB of reserved storage, you get 3 built-in IOPS. For example, if you have 1024 GB of reserved storage, you get 3072 IOPS assigned.
- **Extra IOPS**: You can increase the amount of IOPS above the built-in value up to the cap.
- **Maximum IOPS cap per vCore count**: Each vCore configuration has a maximum IOPS limit, as the max IOPS per vCore table shows.

The **Next-gen General Purpose** service tier has the following throughput characteristics: 

- **Minimum guaranteed throughput**: Every instance is guaranteed a minimum of 75 MB/s of throughput, regardless of the number of vCores.
- **Throughput scales with IOPS**: Throughput scales proportionally with the number of IOPS allocated to the instance, using the formula `throughput (MB/s) = IOPS / 30`. For example, an instance allocated with 3,000 IOPS receives 100 MB/s of throughput.
- **Maximum throughput cap per vCore count**: Each vCore configuration has a maximum throughput limit, as the max throughput per vCore table shows. Once the required IOPS are provisioned to reach this cap, allocating additional IOPS increases IOPS capacity but doesn't increase throughput further. For example, a 4 vCore instance (max 6,400 IOPS) reaches its throughput cap of 145 MB/s at 4,350 IOPS. Scaling IOPS from 4,350 to 6,400 provides more IOPS to the instance, but throughput remains capped at 145 MB/s. To increase the maximum throughput, add more vCores.

The following table shows the max IOPS and throughput per number of vCores allocated to an instance in the Next-gen General Purpose service tier: 

| **Number of vCores** | **Max IOPS** | **Max throughput (MB/s)** |
|:--|:--|:--|
| 4 | 6,400 | 145 |
| 6 | 9,600 | 217 |
| 8 | 12,800 | 290 |
| 10 | 16,000 | 367 |
| 12 | 19,200 | 445 |
| 16 | 25,600 | 600 |
| 20 | 32,000 | 665 |
| 24 | 38,400 | 730 |
| 32 | 51,200 | 865 |
| 40 | 64,000 | 1,152 |
| 48 | 76,800 | 1,152 |
| 56 - 128 | 80,000 | 1,200 |

#### IOPS pricing in Next-gen General Purpose

When using the additional IOPS, consider the following:
- Additional IOPS is charged per IOPS/month
- Billable IOPS is calculated by the following formula: `Billable IOPS = Total IOPS - default IOPS`.

For example, if you have a pay-as-you-go 4 vCore instance with 6400 IOPS, you are charged for: 
  - 4 vCores
  - SQL license for 4 vCores
  - 1024 GB of reserved storage
  - 3328 of billable IOPS `(6400 - (1024*3) = 3328 IOPS)`.

#### IOPS and throughput in the Business Critical service tier

In the Business Critical service tier, available IOPS are dictated by the number of vCores using the formula `4000 IOPS/vCore`. Since the Business Critical service tier uses local storage for data and log files, the throughput is unlimited.

The following table lists the max IOPS available in the Business Critical service tier based on the number of vCores:

| Number of vCores | Business Critical |
| --- | --- |
| 4 | 16,000 |
| 6 | 24,000 |
| 8 | 32,000 |
| 10 | 40,000 |
| 12 | 48,000 |
| 16 | 64,000 |
| 20 | 80,000 |
| 24 | 96,000 |
| 32 | 128,000 |
| 40 | 160,000 |
| 48 | 192,000 |
| 56 | 224,000 |
| 64 | 256,000 |
| 80 | 320,000 |
| 96 | 320,000 |
| 128 | 320,000 |


### Data and log storage

The following factors affect the amount of storage used for data and log files, and apply to General Purpose and Business Critical tiers.

- In the General Purpose service tier, `tempdb` uses local SSD storage, and this storage cost is included in the vCore price.
- In the Business Critical service tier, `tempdb` shares local SSD storage with data and log files, and `tempdb` storage cost is included in the vCore price.  You can size `tempdb` up to the currently available instance storage size.
- The maximum storage size for a SQL Managed Instance must be specified in multiples of 32 GB.

> [!IMPORTANT]  
> In both service tiers, you're charged for the maximum storage size configured for a SQL managed instance.

To monitor total consumed instance storage size for SQL Managed Instance, use the *storage_space_used_mb* [metric](/azure/azure-monitor/essentials/metrics-supported#microsoftsqlmanagedinstances). To monitor the current allocated and used storage size of individual data and log files in a database using T-SQL, use the [sys.database_files](/sql/relational-databases/system-catalog-views/sys-database-files-transact-sql) view and the [FILEPROPERTY(... , 'SpaceUsed')](/sql/t-sql/functions/fileproperty-transact-sql) function.

> [!TIP]  
> Under some circumstances, you might need to shrink a database to reclaim unused space. For more information, see [DBCC SHRINKFILE](/sql/t-sql/database-console-commands/dbcc-shrinkfile-transact-sql).

### Backups and storage

Storage for database backups is allocated to support the [point-in-time restore (PITR)](../database/recovery-using-backups.md) and [long-term retention (LTR)](../database/long-term-retention-overview.md) capabilities of SQL Managed Instance. This storage is separate from data and log file storage, and is billed separately.

- **PITR**: In General Purpose and Business Critical tiers, individual database backups are copied to [read-access geo-redundant (RA-GRS) storage](/azure/storage/common/geo-redundant-design) automatically. The storage size increases dynamically as new backups are created. The storage is used by full, differential, and transaction log backups. The storage consumption depends on the rate of change of the database and the retention period configured for backups. You can configure a separate retention period for each database between 1 to 35 days for SQL Managed Instance. A backup storage amount equal to the configured maximum data size is provided at no extra charge.

- **LTR**: You also have the option to configure long-term retention of full backups for up to 10 years. If you set up an LTR policy, these backups are stored in RA-GRS storage automatically, but you can control how often the backups are copied. To meet different compliance requirements, you can select different retention periods for weekly, monthly, and/or yearly backups. The configuration you choose determines how much storage is used for LTR backups. For more information, see [Long-term retention - Azure SQL Database and Azure SQL Managed Instance](../database/long-term-retention-overview.md).

### Additional characteristics considerations

- **Currently available instance storage size** is the difference between reserved instance size and the used storage space.

- Both data and log file size in the user and system databases are included in the instance storage size that is compared with the max storage size limit. Use the [sys.master_files](/sql/relational-databases/system-catalog-views/sys-master-files-transact-sql) system view to determine the total used space by databases. Error logs aren't persisted and not included in the size. Backups aren't included in storage size.

- Throughput and IOPS in the General Purpose tier also depends on the [file size](#iops-and-throughput), and isn't explicitly limited by the SQL Managed Instance.

- Max instance IOPS depend on the file layout and distribution of workload. As an example, if you create 7 x 1-TB files with max 5 K IOPS each and seven small files (smaller than 128 GB) with 500 IOPS each, you can get 38500 IOPS per instance (7x5000+7x500) if your workload can use all files. Some IOPS are also used for autobackups.

- You can create another readable replica in a different Azure region using [failover groups](failover-group-configure-sql-mi.md)

- Names of `tempdb`files can't have more than 16 characters.

Find more information about the [resource limits in SQL Managed Instance pools in this article](instance-pools-overview.md#resource-limits).

## Flexible memory

By default, the amount of memory allocated to Azure SQL Managed Instance is a static value determined by the selected number of vCores. The flexible memory feature allows you to change the amount of memory allocated to your SQL managed instance without changing the number of vCores. This feature is useful for workloads that require more memory than the default allocation for a given number of vCores.

The flexible memory feature is available in the following service tiers and deployment configurations on [Premium-series](#hardware-configuration-characteristics) hardware:

- **[Next-gen General Purpose](service-tiers-next-gen-general-purpose-use.md)**: [locally redundant](high-availability-sla-local-zone-redundancy.md#locally-redundant-availability) and [zone-redundant](high-availability-sla-local-zone-redundancy.md#zone-redundant-availability) instances. Flexible memory is generally available (GA) for locally redundant instances and is currently in preview for zone-redundant instances.
- **[Business Critical](../database/service-tiers-sql-database-vcore.md#business-critical)**: [locally redundant](high-availability-sla-local-zone-redundancy.md#locally-redundant-availability) and [zone-redundant](high-availability-sla-local-zone-redundancy.md#zone-redundant-availability) instances. Flexible memory is currently in preview for the Business Critical service tier.

You can change the amount of memory allocated to your SQL managed instance at any time for new and existing instances by using the Azure portal, or the REST API. The memory allocation change is applied to all databases in the instance and performs a failover of the instance as the final operation step. Check [management operations duration](management-operations-duration.md#management-operation-duration) to determine the estimated time for the operation to complete.

Use the **Compute + storage** blade in the Azure portal to change the memory allocation, or the `properties.memorySizeInGB` value of the [Managed Instance - Create or Update](/rest/api/sql/managed-instances/create-or-update) REST API call starting with the **2024-08-01-preview** version. The memory allocation is specified in gigabytes (GB).

When allocating memory, you can choose between a minimum and maximum value, and also determine the ratio of memory to vCores. The minimum and maximum values are determined by the number of vCores selected for the instance. The memory-to-vCore ratio is a percentage of the total memory allocated to the instance.

The following table shows the minimum and maximum memory values for the flexible memory feature:

| vCores | Min RAM (GB) | Max RAM (GB) | Supported API RAM values | Min ratio | Max ratio | Supported ratios |
|--|--|--|--|--|--|--|
| 4 | 28 | 48 | 28, 32, 40, 48 | 7 | 12 | 7, 8, 10, 12 |
| 6 | 42 | 72 | 42, 48, 60, 72 | 7 | 12 | 7, 8, 10, 12 |
| 8 | 56 | 96 | 56, 64, 80, 96 | 7 | 12 | 7, 8, 10, 12 |
| 10 | 70 | 120 | 70, 80, 100, 120 | 7 | 12 | 7, 8, 10, 12 |
| 12 | 84 | 144 | 84, 96, 120, 144 | 7 | 12 | 7, 8, 10, 12 |
| 16 | 112 | 192 | 112, 128, 160, 192 | 7 | 12 | 7, 8, 10, 12 |
| 20 | 140 | 240 | 140, 160, 200, 240 | 7 | 12 | 7, 8, 10, 12 |
| 24 | 168 | 288 | 168, 192, 240, 288 | 7 | 12 | 7, 8, 10, 12 |
| 32 | 224 | 384 | 224, 256, 320, 384 | 7 | 12 | 7, 8, 10, 12 |
| 40 | 280 | 480 | 280, 320, 400, 480 | 7 | 12 | 7, 8, 10, 12 |
| 48 | 336 | 480 | 336, 384, 480 | 7 | 10 | 7, 8, 10 |
| 56 | 392 | 448 | 392, 448 | 7 | 8 | 7, 8 |
| 64 | 448 | 448 | 448 | 7 | 7 | 8 |
| 80 | 560 | 560 | 560 | 7 | 7 | 8 |
| 96 | 560 | 560 | 560 | 5.83 | 5.83 | 5.83 |
| 128 | 560 | 560 | 560 | 4.38 | 4.38 | 4.38 |  

### Pricing

When using the flexible memory feature, consider the following:
- Additional memory is charged per GB/hour
- Billable memory is calculated by the following formula: `Billable memory = Total memory - default memory`.

For example, if you have a pay-as-you-go 4 vCore instance with 40 GB of memory, you are charged for: 
  - 4 vCores
  - SQL license for 4 vCores 
  - 12 GB of billable memory (40 GB - (4*7) = 12 GB).

## Supported regions

SQL Managed Instance can be created only in [supported regions](https://azure.microsoft.com/global-infrastructure/services/?products=sql-database&regions=all). To create a SQL Managed Instance in a region that is currently not supported, you can [send a support request via the Azure portal](../database/quota-increase-request.md).

## Supported subscription types

SQL Managed Instance currently supports deployment only on the following types of subscriptions:

- [Enterprise Agreement (EA)](https://azure.microsoft.com/pricing/enterprise-agreement/)
- [Pay-as-you-go](https://azure.microsoft.com/pricing/offers/ms-azr-0003p?cid=msft_learn)
- [Cloud Service Provider (CSP)](/partner-center/csp-documents-and-learning-resources)
- [Enterprise Dev/Test](https://azure.microsoft.com/offers/ms-azr-0148p/)
- [Pay-as-you-go Dev/Test](https://azure.microsoft.com/offers/ms-azr-0023p/)
- [Subscriptions with monthly Azure credit for Visual Studio subscribers](https://azure.microsoft.com/pricing/member-offers/credit-for-visual-studio-subscribers/)
- [Free Trial](https://azure.microsoft.com/pricing/purchase-options/azure-account?cid=msft_learn)
- [Azure For Students](https://azure.microsoft.com/pricing/offers/ms-azr-0170p?cid=msft_learn)
- [Azure In Open](https://azure.microsoft.com/pricing/offers/ms-azr-0111p?cid=msft_learn)
- [Azure plan](https://azure.microsoft.com/pricing/offers/ms-azr-0017g?cid=msft_learn)

## Regional resource limitations

> [!NOTE]  
> For the latest information on region availability for subscriptions, first check [select a region](../capacity-errors-troubleshoot.md).

Supported subscription types can contain a limited number of resources per region. SQL Managed Instance has one default limit per Azure region (that can be increased on-demand by creating a special [support request in the Azure portal](../database/quota-increase-request.md)) depending on a type of subscription type:

- **vCore unit limit**: The maximum number of vCore units per hardware generation that can be deployed across all instances in a single region. One GP vCore uses one vCore unit and one BC vCore takes four vCore units. The total number of instances isn't limited as long as it's within the vCore unit limit.

> [!NOTE]  
> These limits are default settings and not technical limitations. The limits can be increased on-demand by creating a special [support request in the Azure portal](../database/quota-increase-request.md) if you need more instances in the current region. As an alternative, you can create new instances of SQL Managed Instance in another Azure region without sending support requests.

The following table shows the **default regional limits** for supported subscription types (default limits can be extended using [a support request](#request-a-quota-increase)):

| Subscription type | Default limit for vCore units <sup>1</sup> |
| --- | --- |
| CSP | 960 (1440 in some regions<sup>2</sup>) |
| EA | 960 (1440 in some regions<sup>2</sup>) |
| Enterprise Dev/Test | 320 |
| Pay-as-you-go | 320 |
| Pay-as-you-go Dev/Test | 320 |
| Azure Pass | 64 |
| BizSpark | 64 |
| BizSpark Plus | 64 |
| Microsoft Azure Sponsorship | 64 |
| Microsoft Partner Network | 64 |
| Visual Studio Enterprise (MPN) | 64 |
| Visual Studio Enterprise | 32 |
| Visual Studio Enterprise (BizSpark) | 32 |
| Visual Studio Professional | 32 |
| MSDN Platforms | 32 |

<sup>1</sup> In planning deployments, please take into consideration that Business Critical (BC) service tier requires four (4) times more vCore capacity than General Purpose (GP) service tier. For example: 1 GP vCore = 1 vCore unit and 1 BC vCore = 4 vCore. To simplify your consumption analysis against the default limits, summarize the vCore units per hardware generation across all instances in the region where SQL Managed Instance is deployed and compare the results with the vCore unit limits for your subscription type. The **max number of vCore units** limit applies to each subscription per hardware generation in a region.

<sup>2</sup> Larger vCore limits are available in the following regions: Australia East, East US, East US 2, North Europe, South Central US, Southeast Asia, UK South, West Europe, West US 2.

> [!IMPORTANT]  
> In case your vCore limit is 0, it means that default regional limit for your subscription type isn't set. You can also use quota increase request for getting subscription access in specific region following the same procedure - providing required vCore values.

## Request a quota increase

If you need more instances in your current regions, send a support request to extend the quota using the Azure portal. For more information, see [Request quota increases for Azure SQL Database and SQL Managed Instance](../database/quota-increase-request.md).

> [!NOTE]
> Starting August 2026, SQL Managed Instance quota request types changed. Subnet quotas no longer apply, and vCore quotas are now scoped per hardware generation. For more information, see [SQL Managed Instance quota request types](../database/quota-increase-request.md#sql-managed-instance-quota-request-types).

## Related content

- [What is Azure SQL Managed Instance?](sql-managed-instance-paas-overview.md)
- [SQL Managed Instance pricing](https://azure.microsoft.com/pricing/details/sql-database/managed/)
- [the quickstart guide](instance-create-quickstart.md)
- [SLA for Azure SQL Managed Instance](https://azure.microsoft.com/support/legal/sla/azure-sql-sql-managed-instance/)
- [Modifiable configuration reference for Azure SQL Managed Instance](modifiable-configuration-reference.md)
