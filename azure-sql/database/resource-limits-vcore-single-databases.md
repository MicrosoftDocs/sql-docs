---
title: Single database vCore resource limits
description: This page describes common vCore resource limits for a single database in Azure SQL Database.
author: dimitri-furman
ms.author: dfurman
ms.reviewer: wiassaf, mathoma
ms.date: 07/02/2024
ms.service: azure-sql-database
ms.subservice: service-overview
ms.topic: reference
ms.custom:
  - sqldbrb=1
  - references_regions
---
# Resource limits for single databases using the vCore purchasing model
[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database logical server](resource-limits-logical-server.md?view=azuresql-db&preserve-view=true)
> * [Azure SQL Database single database](resource-limits-vcore-single-databases.md?view=azuresql-db&preserve-view=true)
> * [Azure SQL Managed Instance](../managed-instance/resource-limits.md?view=azuresql-mi&preserve-view=true)

This article provides the detailed resource limits for single databases in Azure SQL Database using the vCore purchasing model.


* For elastic pool vCore resource limits, [vCore resource limits - elastic pools](resource-limits-vcore-elastic-pools.md).
* For limits of the logical server in Azure, see [Overview of resource limits on a server](resource-limits-logical-server.md).
* For DTU purchasing model resource limits, see [DTU resource limits single databases](resource-limits-dtu-single-databases.md) and [DTU resource limits elastic pools](resource-limits-dtu-elastic-pools.md).
* For more information regarding the different purchasing models, see [Purchasing models and service tiers](purchasing-models.md).

> [!IMPORTANT]
> Shrink operations should not be considered a regular maintenance operation. Data and log files that grow due to regular, recurring business operations do not require shrink operations.
> - Under some circumstances, you might need to shrink a database to reclaim unused space. For more information, see [Manage file space in Azure SQL Database](file-space-manage.md).
> - For scaling guidance and considerations, see [Scale a single database](single-database-scale.md).

Each read-only replica of a database has its own resources, such as vCores, memory, data IOPS, `tempdb`, workers, and sessions. Each read-only replica is subject to the resource limits detailed later in this article.

You can set the service tier, compute size (service objective), and storage amount for a single database using:

* [Transact-SQL](single-database-manage.md#transact-sql-t-sql) via [ALTER DATABASE](/sql/t-sql/statements/alter-database-transact-sql?view=azuresqldb-current&preserve-view=true#overview-sql-database)
* [Azure portal](single-database-manage.md#the-azure-portal)
* [PowerShell](single-database-manage.md#powershell)
* [Azure CLI](single-database-manage.md#azure-cli)
* [REST API](single-database-manage.md#rest-api)

> [!NOTE]
> The Gen5 hardware in the vCore purchasing model has been renamed to **standard-series (Gen5)**.

## <a id="general-purpose---serverless-compute---gen5"></a>General Purpose - serverless compute - standard-series (Gen5)

<!---
vCore resource limits are listed in the following articles, please be sure to update all of them: 
/database/resource-limits-vcore-single-databases.md
/database/resource-limits-vcore-elastic-pools.md
/database/resource-limits-logical-server.md
/database/service-tier-general-purpose.md
/database/service-tier-business-critical.md
/database/service-tier-hyperscale.md
/managed-instance/resource-limits.md
--->

The [serverless compute tier](serverless-tier-overview.md) is currently available on standard-series (Gen5) hardware only.

### <a id="gen5-hardware-part-1-1"></a>General Purpose - serverless compute - standard-series (Gen5) (part 1 of 3)

Compute sizes (service level objectives, or SLOs) for General Purpose serverless standard-series databases follow the naming convention `GP_S_Gen5_` followed by the number of max vCores. 

The following table covers these SLOs: `GP_S_Gen5_1`, `GP_S_Gen5_2`, `GP_S_Gen5_4`, `GP_S_Gen5_6`, and `GP_S_Gen5_8`:

| Min-max vCores | 0.5 - 1 | 0.5 - 2 | 0.5 - 4 | 0.75 - 6 | 1.0 - 8 |
|:-|-:|-:|-:|-:|-:|
| Hardware | Gen5 | Gen5 | Gen5 | Gen5 | Gen5 |
| Min-max memory (GB) | 2.02 - 3 | 2.05 - 6 | 2.10 - 12 | 2.25 - 18 | 3.00 - 24 |
| Min-max auto-pause delay (minutes) | 15 - 10,080 | 15 - 10,080 | 15 - 10,080 | 15 - 10,080 | 15 - 10,080 |
| Columnstore support | Yes <sup>1</sup> | Yes | Yes | Yes | Yes |
| In-memory OLTP storage (GB) | N/A | N/A | N/A | N/A | N/A |
| Max data size (GB) | 512 | 1024 | 1024 | 1024 | 2048 |
| Max log size (GB) <sup>2</sup> | 154 | 307 | 307 | 307 | 614 |
| Tempdb max data size (GB) | 32 | 64 | 128 | 192 | 256 |
| Storage type | Remote SSD | Remote SSD | Remote SSD | Remote SSD | Remote SSD |
| Read IO latency (approximate) | 5-10 ms | 5-10 ms | 5-10 ms | 5-10 ms | 5-10 ms |
| Write IO latency (approximate) | 5-7 ms | 5-7 ms | 5-7 ms | 5-7 ms | 5-7 ms |
| Max data IOPS <sup>3</sup> | 320 | 640 | 1280 | 1920 | 2560 |
| Max log rate (MBps) | 4.5 | 9 | 18 | 27 | 36 |
| Max concurrent workers | 75 | 150 | 300 | 450 | 600 |
| Max concurrent logins | 75 | 150 | 300 | 450 | 600 |
| Max concurrent external connections <sup>4</sup> | 7 | 15 | 30 | 45 | 60 |
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 |
| Number of replicas | 1 | 1 | 1 | 1 | 1 |
| Multi-AZ | Yes | Yes | Yes | Yes | Yes |
| Read Scale-out | N/A | N/A | N/A | N/A | N/A |
| Included backup storage | 1X DB size | 1X DB size | 1X DB size | 1X DB size | 1X DB size |

<sup>1</sup> Service objectives with smaller max vCore configurations can have insufficient memory for creating and using columnstore indexes.  If encountering performance problems with columnstore, increase the max vCore configuration to increase the max memory available.  

<sup>2</sup> For documented max data size values. Reducing max data size reduces max log size proportionally.

<sup>3</sup> The maximum value for IO sizes ranging between 8 KB and 64 KB. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance).

<sup>4</sup> For more information on what counts as an external connection, see [External Connections](resource-limits-logical-server.md#external-connections).

### <a id="gen5-hardware-part-2-1"></a>General Purpose - serverless compute - standard-series (Gen5) (part 2 of 3)

Compute sizes (service level objectives, or SLOs) for General Purpose serverless standard-series databases follow the naming convention `GP_S_Gen5_` followed by the number of max vCores. 

The following table covers these SLOs: `GP_S_Gen5_10`, `GP_S_Gen5_12`, `GP_S_Gen5_14`, `GP_S_Gen5_16`, and `GP_S_Gen5_18`:

| Min-max vCores | 1.25-10 | 1.50-12 | 1.75-14 | 2.00-16 | 2.25-18 |
|:-|-:|-:|-:|-:|-:|
| Hardware | Gen5 | Gen5 | Gen5 | Gen5 | Gen5 |
| Min-max memory (GB) | 3.75-30 | 4.50-36 | 5.25-42 | 6.00-48 | 6.75-54 |
| Min-max auto-pause delay (minutes) | 15-10,080 | 15-10,080 | 15-10,080 | 15-10,080 | 15-10,080 |
| Columnstore support | Yes | Yes | Yes | Yes | Yes |
| In-memory OLTP storage (GB) | N/A | N/A | N/A | N/A | N/A |
| Max data size (GB) | 2048 | 3072 | 3072 | 3072 | 3072 |
| Max log size (GB) <sup>1</sup> | 614 | 922 | 922 | 922 | 922 |
| Tempdb max data size (GB) | 320 | 384 | 448 | 512 | 576 |
| Storage type | Remote SSD | Remote SSD | Remote SSD | Remote SSD | Remote SSD |
| Read IO latency (approximate) | 5-10 ms | 5-10 ms | 5-10 ms | 5-10 ms | 5-10 ms |
| Write IO latency (approximate) | 5-7 ms | 5-7 ms | 5-7 ms | 5-7 ms | 5-7 ms |
| Max data IOPS <sup>2</sup> | 3200 | 3840 | 4480 | 5120 | 5760 |
| Max log rate (MBps) | 45 | 50 | 50 | 50 | 50 |
| Max concurrent workers | 750 | 900 | 1050 | 1200 | 1350 |
| Max concurrent logins | 750 | 900 | 1050 | 1200 | 1350 |
| Max concurrent external connections <sup>3</sup> | 75 | 90 | 105 | 120 | 135 |
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 |
| Number of replicas | 1 | 1 | 1 | 1 | 1 |
| Multi-AZ | Yes | Yes | Yes | Yes | Yes |
| Read Scale-out | N/A | N/A | N/A | N/A | N/A |
| Included backup storage | 1X DB size | 1X DB size | 1X DB size | 1X DB size | 1X DB size |

<sup>1</sup> For documented max data size values. Reducing max data size reduces max log size proportionally.

<sup>2</sup> The maximum value for IO sizes ranging between 8 KB and 64 KB. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance).

<sup>3</sup> For more information on what counts as an external connection, see [External Connections](resource-limits-logical-server.md#external-connections).

### <a id="gen5-hardware-part-3-1"></a>General Purpose - serverless compute - standard-series (Gen5) (part 3 of 3)

Compute sizes (service level objectives, or SLOs) for General Purpose serverless standard-series databases follow the naming convention `GP_S_Gen5_` followed by the number of max vCores. 

The following table covers these SLOs: `GP_S_Gen5_20`, `GP_S_Gen5_24`, `GP_S_Gen5_32`, `GP_S_Gen5_40`, and `GP_S_Gen5_80`:

| Min-max vCores | 2.5-20 | 3-24 | 4-32 | 5-40 | 10-80 <sup>3</sup> |
|:-|-:|-:|-:|-:|-:|
| Hardware | Gen5 | Gen5 | Gen5 | Gen5 | Gen5 |
| Min-max memory (GB) | 7.5-60 | 9-72 | 12-96 | 15-120 | 30-240 |
| Min-max auto-pause delay (minutes) | 15-10,080 | 15-10,080 | 15-10,080 | 15-10,080 | 15-10,080 |
| Columnstore support | Yes | Yes | Yes | Yes | Yes |
| In-memory OLTP storage (GB) | N/A | N/A | N/A | N/A | N/A |
| Max data size (GB) | 3072 | 4096 | 4096 | 4096 | 4096 |
| Max log size (GB) <sup>1</sup> | 922 | 1024 | 1024 | 1024 | 1024 |
| Tempdb max data size (GB) | 640 | 768 | 1024 | 1280 | 2560 |
| Storage type | Remote SSD | Remote SSD | Remote SSD | Remote SSD | Remote SSD |
| Read IO latency (approximate) | 5-10 ms | 5-10 ms | 5-10 ms | 5-10 ms | 5-10 ms |
| Write IO latency (approximate) | 5-7 ms | 5-7 ms | 5-7 ms | 5-7 ms | 5-7 ms |
| Max data IOPS <sup>2</sup> | 6,400 | 7,680 | 10,240 | 12,800 | 12,800 |
| Max log rate (MBps) | 50 | 50 | 50 | 50 | 50 |
| Max concurrent workers | 1500 | 1800 | 2400 | 3000 | 6000 |
| Max concurrent logins | 1500 | 1800 | 2400 | 3000 | 6000 |
| Max concurrent external connections <sup>4</sup> | 150 | 150 | 150 | 150 | 150 |
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 |
| Number of replicas | 1 | 1 | 1 | 1 | 1 |
| Multi-AZ | Yes | Yes | Yes | Yes | Yes |
| Read Scale-out | N/A | N/A | N/A | N/A | N/A |
| Included backup storage | 1X DB size | 1X DB size | 1X DB size | 1X DB size | 1X DB size |

<sup>1</sup> For documented max data size values. Reducing max data size reduces max log size proportionally.

<sup>2</sup> The maximum value for IO sizes ranging between 8 KB and 64 KB. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance).

<sup>3</sup> For specific regions where 80 vCores in serverless is available, see [Available regions](serverless-tier-overview.md#available-regions).

<sup>4</sup> For more information on what counts as an external connection, see [External Connections](resource-limits-logical-server.md#external-connections).

## Hyperscale - serverless compute - standard-series (Gen5)

The [serverless compute tier](serverless-tier-overview.md) is currently available on standard-series (Gen5) hardware only.

### Hyperscale - serverless compute - standard-series (Gen5) (part 1 of 2)

Compute sizes (service level objectives, or SLOs) for Hyperscale serverless standard-series databases follow the naming convention `HS_S_Gen5_` followed by the number of max vCores. 

The following table covers these SLOs: `HS_S_Gen5_2`, `HS_S_Gen5_4`, `HS_S_Gen5_6`, `HS_S_Gen5_8`, `HS_S_Gen5_10`, `HS_S_Gen5_12`, and `HS_S_Gen5_14`:

| Min-max vCores | 0.5-2 | 0.5-4 | 0.75-6 | 1-8 | 1.25-10 | 1.5-12 | 1.75-14 |
|--|--|--|--|--|--|--|--|
| Hardware | Gen5 | Gen5 | Gen5 | Gen5 | Gen5 | Gen5 | Gen5 |
| Min-max memory (GB) | 2.05-6 | 2.10-12 | 2.25-18 | 3.00-24 | 3.75-30 | 4.50-36 | 5.25-42 |
| Columnstore support | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| In-memory OLTP storage (GB) | N/A | N/A | N/A | N/A | N/A | N/A | N/A |
| Max data size (TB) | 100 | 100 | 100 | 100 | 100 | 100 | 100 |
| Max log size (TB) | Unlimited | Unlimited | Unlimited | Unlimited | Unlimited | Unlimited | Unlimited |
| Tempdb max data size (GB) | 64 | 128 | 192 | 256 | 320 | 384 | 448 |
| Storage type | Multi-tiered <sup>1</sup>| Multi-tiered <sup>1</sup>| Multi-tiered <sup>1</sup>| Multi-tiered <sup>1</sup>| Multi-tiered <sup>1</sup>| Multi-tiered <sup>1</sup>| Multi-tiered <sup>1</sup>|
| Max local SSD IOPS <sup>2</sup> | 8000 | 16000 | 24000 | 32000 | 40000 | 48000 | 56000 |
| Max log rate (MBps) | 100 | 100 | 100 | 100 | 100 | 100 | 100 |
| IO latency (approximate) | Variable <sup>3</sup> | Variable <sup>3</sup> | Variable <sup>3</sup> | Variable <sup>3</sup>| Variable <sup>3</sup> | Variable <sup>3</sup> | Variable <sup>3</sup> |
| Max concurrent workers | 150 | 300 | 450 | 600 | 750 | 900 | 1050 |
| Max concurrent logins | 150 | 300 | 450 | 600 | 750 | 900 | 1050 |
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 |
| Secondary replicas | 0-4 | 0-4 | 0-4 | 0-4 | 0-4 | 0-4 | 0-4 |
| Multi-AZ | [Yes](high-availability-sla-local-zone-redundancy.md#hyperscale-service-tier) | [Yes](high-availability-sla-local-zone-redundancy.md#hyperscale-service-tier) | [Yes](high-availability-sla-local-zone-redundancy.md#hyperscale-service-tier) | [Yes](high-availability-sla-local-zone-redundancy.md#hyperscale-service-tier) | [Yes](high-availability-sla-local-zone-redundancy.md#hyperscale-service-tier) | [Yes](high-availability-sla-local-zone-redundancy.md#hyperscale-service-tier) | [Yes](high-availability-sla-local-zone-redundancy.md#hyperscale-service-tier) |
| Read Scale-out | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| Backup storage retention | 7 days | 7 days | 7 days | 7 days | 7 days | 7 days | 7 days |

<sup>1</sup> Hyperscale is a multi-tiered architecture with separate compute and storage components. Review [Hyperscale service tier](service-tier-hyperscale.md#distributed-functions-architecture) for more information.   
<sup>2</sup> Besides local SSD IO, workloads use remote [page server](hyperscale-architecture.md#page-server) IO. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance), and [Data IO in resource utilization statistics](hyperscale-performance-diagnostics.md#data-io-in-resource-utilization-statistics).   
<sup>3</sup> Latency is 1-2 ms for data on local compute replica SSD, which caches most used data pages. Higher latency for data retrieved from page servers. 



### Hyperscale - serverless compute - standard-series (Gen5) (part 2 of 2)

Compute sizes (service level objectives, or SLOs) for Hyperscale serverless standard-series databases follow the naming convention `HS_S_Gen5_` followed by the number of max vCores. 

The following table covers these SLOs: `HS_S_Gen5_16`, `HS_S_Gen5_18`, `HS_S_Gen5_20`, `HS_S_Gen5_24`, `HS_S_Gen5_32`, `HS_S_Gen5_40`, and `HS_S_Gen5_80`:

| Min-max vCores | 2-16 | 2.25-18 | 2.25-20 | 3-24 | 4-32 | 5-40 | 10-80 <sup>4</sup> |
|--|--|--|--|--|--|--|--|
| Hardware | Gen5 | Gen5 | Gen5 | Gen5 | Gen5 | Gen5 | Gen5 |
| Min-max memory (GB) | 6.00-48 | 6.75-54 | 7.5-60 | 9-72 | 12-96 | 15-120 | 30-240 |
| Columnstore support | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| In-memory OLTP storage (GB) | N/A | N/A | N/A | N/A | N/A | N/A | N/A |
| Max data size (TB) | 100 | 100 | 100 | 100 | 100 | 100 | 100 |
| Max log size (TB) | Unlimited | Unlimited | Unlimited | Unlimited | Unlimited | Unlimited | Unlimited |
| Tempdb max data size (GB) | 512 | 576 | 640 | 768 | 1024 | 1280 | 2560 |
| Storage type | Multi-tiered <sup>1</sup> | Multi-tiered <sup>1</sup> | Multi-tiered <sup>1</sup> | Multi-tiered <sup>1</sup> | Multi-tiered <sup>1</sup> | Multi-tiered <sup>1</sup> | Multi-tiered <sup>1</sup> |
| Max local SSD IOPS <sup>2</sup> | 64000 | 72000 | 80000 | 96000 | 128000 | 160000 | 204800 |
| Max log rate (MBps) | 100 | 100 | 100 | 100 | 100 | 100 | 100 |
| IO latency (approximate) | Variable <sup>3</sup> | Variable <sup>3</sup> | Variable <sup>3</sup> | Variable <sup>3</sup> | Variable <sup>3</sup> | Variable <sup>3</sup> | Variable <sup>3</sup> |
| Max concurrent workers | 1200 | 1350 | 1500 | 1800 | 2400 | 3000 | 6000 |
| Max concurrent logins | 1200 | 1350 | 1500 | 1800 | 2400 | 3000 | 6000 |
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 |
| Secondary replicas | 0-4 | 0-4 | 0-4 | 0-4 | 0-4 | 0-4 | 0-4 |
| Multi-AZ | [Yes](high-availability-sla-local-zone-redundancy.md#hyperscale-service-tier) | [Yes](high-availability-sla-local-zone-redundancy.md#hyperscale-service-tier) | [Yes](high-availability-sla-local-zone-redundancy.md#hyperscale-service-tier) | [Yes](high-availability-sla-local-zone-redundancy.md#hyperscale-service-tier) | [Yes](high-availability-sla-local-zone-redundancy.md#hyperscale-service-tier) | [Yes](high-availability-sla-local-zone-redundancy.md#hyperscale-service-tier) | [Yes](high-availability-sla-local-zone-redundancy.md#hyperscale-service-tier) |
| Read Scale-out | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| Backup storage retention | 7 days | 7 days | 7 days | 7 days | 7 days | 7 days | 7 days |

<sup>1</sup> Hyperscale is a multi-tiered architecture with separate compute and storage components. Review [Hyperscale service tier](service-tier-hyperscale.md#distributed-functions-architecture) for more information.   
<sup>2</sup> Besides local SSD IO, workloads use remote [page server](hyperscale-architecture.md#page-server) IO. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance), and [Data IO in resource utilization statistics](hyperscale-performance-diagnostics.md#data-io-in-resource-utilization-statistics).   
<sup>3</sup> Latency is 1-2 ms for data on local compute replica SSD, which caches most used data pages. Higher latency for data retrieved from page servers. 
<sup>4</sup> For specific regions where 80 vCores in serverless is available, see [Available regions](serverless-tier-overview.md#available-regions).


## <a id="hyperscale---provisioned-compute---gen5"></a>Hyperscale - provisioned compute - standard-series (Gen5)

### <a id="gen5-hardware-part-1-2"></a>Hyperscale standard-series (Gen5) (part 1 of 2)

Compute sizes (service level objectives, or SLOs) for Hyperscale standard-series databases follow the naming convention `HS_Gen5_` followed by the number of vCores. 

The following table covers these SLOs: `HS_Gen5_2`, `HS_Gen5_4`, `HS_Gen5_6`, `HS_Gen5_8`, `HS_Gen5_10`, `HS_Gen5_12`, and `HS_Gen5_14`:

| vCores | 2 | 4 | 6 | 8 | 10 | 12 | 14 |
|:-|-:|-:|-:|-:|-:|-:|-:|
| Hardware | Gen5 | Gen5 | Gen5 | Gen5 | Gen5 | Gen5 | Gen5 |
| Memory (GB) | 10.4 | 20.8 | 31.1 | 41.5 | 51.9 | 62.3 | 72.7 |
| Columnstore support | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| In-memory OLTP storage (GB) | N/A | N/A | N/A | N/A | N/A | N/A | N/A |
| Max data size (TB) | 100 | 100 | 100 | 100 | 100 | 100 | 100 |
| Max log size (TB) | Unlimited | Unlimited | Unlimited | Unlimited | Unlimited | Unlimited | Unlimited |
| `Tempdb` max data size (GB) | 64 | 128 | 192 | 256 | 320 | 384 | 448 |
| Max local SSD IOPS <sup>1</sup> | 8000 | 16,000 | 24,000 | 32,000 | 40,000 | 48,000 | 56,000 |
| Max log rate (MBps) | 100 | 100 | 100 | 100 | 100 | 100 | 100 |
| Local read IO latency<sup>2</sup> | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms |
| Remote read IO latency<sup>2</sup> | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms |
| Write IO latency<sup>2</sup> | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms |
| Storage type | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> |
| Max concurrent workers | 200 | 400 | 600 | 800 | 1000 | 1200 | 1400 |
| Max concurrent logins | 200 | 400 | 600 | 800 | 1000 | 1200 | 1400 |
| Max concurrent external connections <sup>4</sup> | 20 | 40 | 60 | 80 | 100 | 120 | 140 |
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 |
| Secondary replicas | 0-4 | 0-4 | 0-4 | 0-4 | 0-4 | 0-4 | 0-4 |
| Multi-AZ | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| Read Scale-out | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| Backup storage retention | 7 days | 7 days | 7 days | 7 days | 7 days | 7 days | 7 days |

<sup>1</sup> Besides local SSD IO, workloads use remote [page server](hyperscale-architecture.md#page-server) IO. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance), and [Data IO in resource utilization statistics](hyperscale-performance-diagnostics.md#data-io-in-resource-utilization-statistics).

<sup>2</sup> Latency numbers are approximate and representative for typical workloads at steady state, but aren't guaranteed. 

<sup>3</sup> Hyperscale is a multi-tiered architecture with separate compute and storage components. For more information, see [Hyperscale service tier architecture](hyperscale-architecture.md).

<sup>4</sup> For more information on what counts as an external connection, see [External Connections](resource-limits-logical-server.md#external-connections).

### <a id="gen5-hardware-part-2-2"></a>Hyperscale standard-series (Gen5) (part 2 of 2)

Compute sizes (service level objectives, or SLOs) for Hyperscale standard-series databases follow the naming convention `HS_Gen5_` followed by the number of vCores. 

The following table covers these SLOs: `HS_Gen5_16`, `HS_Gen5_18`, `HS_Gen5_20`, `HS_Gen5_24`, `HS_Gen5_32`, `HS_Gen5_40`, and `HS_Gen5_80`:

| vCores | 16 | 18 | 20 | 24 | 32 | 40 | 80 |
|:-|-:|-:|-:|-:|-:|-:|-:|
| Hardware | Gen5 | Gen5 | Gen5 | Gen5 | Gen5 | Gen5 | Gen5 |
| Memory (GB) | 83 | 93.4 | 103.8 | 124.6 | 166.1 | 207.6 | 415.2 |
| Columnstore support | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| In-memory OLTP storage (GB) | N/A | N/A | N/A | N/A | N/A | N/A | N/A |
| Max data size (TB) | 100 | 100 | 100 | 100 | 100 | 100 | 100 |
| Max log size (TB) | Unlimited | Unlimited | Unlimited | Unlimited | Unlimited | Unlimited | Unlimited |
| Tempdb max data size (GB) | 512 | 576 | 640 | 768 | 1024 | 1280 | 2560 |
| Max local SSD IOPS <sup>1</sup> | 64,000 | 72,000 | 80,000 | 96,000 | 128,000 | 160,000 | 204,800 |
| Max log rate (MBps) | 100 | 100 | 100 | 100 | 100 | 100 | 100 |
| Local read IO latency<sup>2</sup> | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms |
| Remote read IO latency<sup>2</sup> | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms |
| Write IO latency<sup>2</sup> | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms |
| Storage type | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> |
| Max concurrent workers | 1600 | 1800 | 2000 | 2400 | 3200 | 4000 | 8000 |
| Max concurrent logins | 1600 | 1800 | 2000 | 2400 | 3200 | 4000 | 8000 |
| Max concurrent external connections <sup>4</sup> | 150 | 150 | 150 | 150 | 150 | 150 | 150 |
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 |
| Secondary replicas | 0-4 | 0-4 | 0-4 | 0-4 | 0-4 | 0-4 | 0-4 |
| Multi-AZ | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| Read Scale-out | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| Backup storage retention | 7 days | 7 days | 7 days | 7 days | 7 days | 7 days | 7 days |

<sup>1</sup> Besides local SSD IO, workloads use remote [page server](hyperscale-architecture.md#page-server) IO. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance), and [Data IO in resource utilization statistics](hyperscale-performance-diagnostics.md#data-io-in-resource-utilization-statistics).

<sup>2</sup> Latency numbers are representative for typical workloads at steady state, but aren't guaranteed. 

<sup>3</sup> Hyperscale is a multi-tiered architecture with separate compute and storage components. For more information, see [Hyperscale service tier architecture](hyperscale-architecture.md).

<sup>4</sup> For more information on what counts as an external connection, see [External Connections](resource-limits-logical-server.md#external-connections).

## Hyperscale - provisioned compute - DC-series
### Hyperscale DC-series (part 1 of 2)

Compute sizes (service level objectives, or SLOs) for Hyperscale DC-series databases follow the naming convention `HS_DC_` followed by the number of vCores. 

The following table covers these SLOs: `HS_DC_2`, `HS_DC_4`, `HS_DC_6`, `HS_DC_8`, `HS_DC_10`, and `HS_DC_12`:

| vCores | 2 | 4 | 6 | 8 | 10 | 12 |
|:-|-:|-:|-:|-:|-:|-:|
| Hardware | DC-series | DC-series | DC-series | DC-series | DC-series | DC-series |
| Memory (GB) | 9 | 18 | 27 | 36 | 45 | 54 |
| Columnstore support | Yes | Yes | Yes | Yes | Yes | Yes |
| In-memory OLTP storage (GB) | N/A | N/A | N/A | N/A | N/A | N/A |
| Max data size (TB) | 100 | 100 | 100 | 100 | 100 | 100 |
| Max log size (TB) | Unlimited | Unlimited | Unlimited | Unlimited | Unlimited | Unlimited |
| Tempdb max data size (GB) | 64 | 128 | 192 | 256 | 320 | 384 |
| Max local SSD IOPS <sup>1</sup> | 14,000 | 28,000 | 42,000 | 44,800 | 64,000 | 76,800 |
| Max log rate (MBps) | 100 | 100 | 100 | 100 | 100 | 100 |
| Local read IO latency<sup>2</sup> | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms |
| Remote read IO latency<sup>2</sup> | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms |
| Write IO latency<sup>2</sup> | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms |
| Storage type | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> |
| Max concurrent workers | 160 | 320 | 480 | 640 | 800 | 960 |
| Max concurrent logins | 160 | 320 | 480 | 640 | 800 | 960 |
| Max concurrent external connections <sup>4</sup> | 16 | 32 | 48 | 64 | 80 | 150 |
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 |
| Secondary replicas | 0-4 | 0-4 | 0-4 | 0-4 | 0-4 | 0-4 |
| Multi-AZ | N/A | N/A | N/A | N/A | N/A | N/A |
| Read Scale-out | Yes | Yes | Yes | Yes | Yes | Yes |
| Backup storage retention | 7 days | 7 days | 7 days | 7 days | 7 days | 7 days |

<sup>1</sup> Besides local SSD IO, workloads use remote [page server](hyperscale-architecture.md#page-server) IO. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance), and [Data IO in resource utilization statistics](hyperscale-performance-diagnostics.md#data-io-in-resource-utilization-statistics).

<sup>2</sup> Latency numbers are representative for typical workloads at steady state, but aren't guaranteed. 

<sup>3</sup> Hyperscale is a multi-tiered architecture with separate compute and storage components. For more information, see [Hyperscale service tier architecture](hyperscale-architecture.md).

<sup>4</sup> For more information on what counts as an external connection, see [External Connections](resource-limits-logical-server.md#external-connections).



### Hyperscale DC-series (part 2 of 2)

Compute sizes (service level objectives, or SLOs) for Hyperscale DC-series databases follow the naming convention `HS_DC_` followed by the number of vCores. 

The following table covers these SLOs: `HS_DC_14`, `HS_DC_16`, `HS_DC_18`, `HS_DC_20`, `HS_DC_32`, and `HS_DC_40`:

| vCores | 14 | 16 | 18 | 20 | 32 | 40 |
|:-|-:|-:|-:|-:|-:|-:|
| Hardware | DC-series | DC-series | DC-series | DC-series | DC-series | DC-series |
| Memory (GB) | 63 | 72 | 81 | 90 | 144 | 180 |
| Columnstore support | Yes | Yes | Yes | Yes | Yes | Yes |
| In-memory OLTP storage (GB) | N/A | N/A | N/A | N/A | N/A | N/A |
| Max data size (TB) | 100 | 100 | 100 | 100 | 100 | 100 |
| Max log size (TB) | Unlimited | Unlimited | Unlimited | Unlimited | Unlimited | Unlimited |
| Tempdb max data size (GB) | 448 | 512 | 576 | 640 | 1024 | 1280 | 
| Max local SSD IOPS <sup>1</sup> | 89,600 | 102,400 | 115,200 | 128,000 | 204,800 | 256,000 |
| Max log rate (MBps) | 100 | 100 | 100 | 100 | 100 | 100 |
| Local read IO latency<sup>2</sup> | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms |
| Remote read IO latency<sup>2</sup> | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms |
| Write IO latency<sup>2</sup> | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms |
| Storage type | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> |
| Max concurrent workers | 1120 | 1280 | 1440 | 1600 | 2560 | 3200 |
| Max concurrent logins | 1120 | 1280 | 1440 | 1600 | 2560 | 3200 |
| Max concurrent external connections <sup>4</sup> | 150 | 150 | 150 | 150 | 150 | 150 |
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 |
| Secondary replicas | 0-4 | 0-4 | 0-4 | 0-4 | 0-4 | 0-4 |
| Multi-AZ | N/A | N/A | N/A | N/A | N/A | N/A |
| Read Scale-out | Yes | Yes | Yes | Yes | Yes | Yes |
| Backup storage retention | 7 days | 7 days | 7 days | 7 days | 7 days | 7 days |

<sup>1</sup> Besides local SSD IO, workloads use remote [page server](hyperscale-architecture.md#page-server) IO. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance), and [Data IO in resource utilization statistics](hyperscale-performance-diagnostics.md#data-io-in-resource-utilization-statistics).

<sup>2</sup> Latency numbers are representative for typical workloads at steady state, but aren't guaranteed. 

<sup>3</sup> Hyperscale is a multi-tiered architecture with separate compute and storage components. For more information, see [Hyperscale service tier architecture](hyperscale-architecture.md).

<sup>4</sup> For more information on what counts as an external connection, see [External Connections](resource-limits-logical-server.md#external-connections).

## Hyperscale - provisioned compute - premium-series

Although the published Hyperscale resource limits for standard-series and premium-series are the same, premium-series offers faster CPU performance compared to standard-series, and scales up to 128 vCores, compared to 80 vCores for standard-series. Resources using premium-series are guaranteed to run on hardware with newer CPUs. Standard-series does not provide this guarantee and, depending on availability, resources can be placed on older hardware. There is no price difference between the two, but premium-series might not be available in all regions.

### Hyperscale premium-series (part 1 of 3)

Compute sizes (service level objectives, or SLOs) for Hyperscale premium-series databases follow the naming convention `HS_PRMS_` followed by the number of vCores. 

The following table covers these SLOs: `HS_PRMS_2`, `HS_PRMS_4`, `HS_PRMS_6`, `HS_PRMS_8`, and `HS_PRMS_10`:

| vCores | 2 | 4 | 6 | 8 | 10 |
|:-|-:|-:|-:|-:|-:|
| Hardware | Premium-series | Premium-series | Premium-series | Premium-series | Premium-series |
| Memory (GB) | 10.4 | 20.8 | 31.1 | 41.5 | 51.9 |
| Columnstore support | Yes | Yes | Yes | Yes | Yes |
| In-memory OLTP storage (GB) | N/A | N/A | N/A | N/A | N/A |
| Max data size (TB) | 100 | 100 | 100 | 100 | 100 |
| Max log size (TB) | Unlimited | Unlimited | Unlimited | Unlimited | Unlimited |
| Tempdb max data size (GB) | 64 | 128 | 192 | 256 | 320 |
| Max local SSD IOPS <sup>1</sup> | 8,000 | 16,000 | 24,000 | 32,000 | 40,000 |
| Max log rate (MBps) | 100 | 100 | 100 | 100 | 100 |
| Local read IO latency<sup>2</sup> | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms |
| Remote read IO latency<sup>2</sup> | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms |
| Write IO latency<sup>2</sup> | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms |
| Storage type | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> |
| Max concurrent workers | 200 | 400 | 600 | 800 | 1000 |
| Max concurrent logins | 200 | 400 | 600 | 800 | 1000 |
| Max concurrent external connections <sup>4</sup> | 20 | 40 | 60 | 80 | 100 |
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 |
| Secondary replicas | 0-4 | 0-4 | 0-4 | 0-4 | 0-4 |
| Multi-AZ | N/A | N/A | N/A | N/A | N/A |
| Read Scale-out | Yes | Yes | Yes | Yes | Yes |
| Backup storage retention | 7 days | 7 days | 7 days | 7 days | 7 days |

<sup>1</sup> Besides local SSD IO, workloads use remote [page server](hyperscale-architecture.md#page-server) IO. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance), and [Data IO in resource utilization statistics](hyperscale-performance-diagnostics.md#data-io-in-resource-utilization-statistics).

<sup>2</sup> Latency numbers are representative for typical workloads at steady state, but aren't guaranteed. 

<sup>3</sup> Hyperscale is a multi-tiered architecture with separate compute and storage components. For more information, see [Hyperscale service tier architecture](hyperscale-architecture.md).

<sup>4</sup> For more information on what counts as an external connection, see [External Connections](resource-limits-logical-server.md#external-connections).


### Hyperscale premium-series (part 2 of 3)

Compute sizes (service level objectives, or SLOs) for Hyperscale premium-series databases follow the naming convention `HS_PRMS_` followed by the number of vCores. 

The following table covers these SLOs: `HS_PRMS_12`, `HS_PRMS_14`, `HS_PRMS_16`, `HS_PRMS_18`, and `HS_PRMS_20`:

| vCores | 12 | 14 | 16 | 18 | 20 |
|:-|-:|-:|-:|-:|-:|
| Hardware | Premium-series | Premium-series | Premium-series | Premium-series | Premium-series |
| Memory (GB) | 62.3 | 72.7 | 83.0 | 93.4 | 103.8 |
| Columnstore support | Yes | Yes | Yes | Yes | Yes |
| In-memory OLTP storage (GB) | N/A | N/A | N/A | N/A | N/A |
| Max data size (TB) | 100 | 100 | 100 | 100 | 100 |
| Max log size (TB) | Unlimited | Unlimited | Unlimited | Unlimited | Unlimited |
| Tempdb max data size (GB) | 384 | 448 | 512 | 576 | 640 |
| Max local SSD IOPS <sup>1</sup> | 48,000 | 56,000 | 64,000 | 72,000 | 80,000 |
| Max log rate (MBps) | 100 | 100 | 100 | 100 | 100 |
| Local read IO latency<sup>2</sup> | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms |
| Remote read IO latency<sup>2</sup> | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms |
| Write IO latency<sup>2</sup> | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms |
| Storage type | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> |
| Max concurrent workers | 1,200 | 1,400 | 1,600 | 1,800 | 2,000 |
| Max concurrent logins | 1,200 | 1,400 | 1,600 | 1,800 | 2,000 |
| Max concurrent external connections <sup>4</sup> | 120 | 140 | 150 | 150 | 150 |
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 |
| Secondary replicas | 0-4 | 0-4 | 0-4 | 0-4 | 0-4 |
| Multi-AZ | N/A | N/A | N/A | N/A | N/A |
| Read Scale-out | Yes | Yes | Yes | Yes | Yes |
| Backup storage retention | 7 days | 7 days | 7 days | 7 days | 7 days |

<sup>1</sup> Besides local SSD IO, workloads use remote [page server](hyperscale-architecture.md#page-server) IO. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance), and [Data IO in resource utilization statistics](hyperscale-performance-diagnostics.md#data-io-in-resource-utilization-statistics).

<sup>2</sup> Latency numbers are representative for typical workloads at steady state, but aren't guaranteed. 

<sup>3</sup> Hyperscale is a multi-tiered architecture with separate compute and storage components. For more information, see [Hyperscale service tier architecture](hyperscale-architecture.md).

<sup>4</sup> For more information on what counts as an external connection, see [External Connections](resource-limits-logical-server.md#external-connections).

### Hyperscale premium-series (part 3 of 3)

Compute sizes (service level objectives, or SLOs) for Hyperscale premium-series databases follow the naming convention `HS_PRMS_` followed by the number of vCores. 

The following table covers these SLOs: `HS_PRMS_24`, `HS_PRMS_32`, `HS_PRMS_40`, `HS_PRMS_64`, `HS_PRMS_80`, and `HS_PRMS_128`:

| vCores | 24 | 32 | 40 | 64 | 80 | 128 |
|:-|-:|-:|-:|-:|-:|-:|
| Hardware | Premium-series | Premium-series | Premium-series | Premium-series | Premium-series | Premium-series |
| Memory (GB) | 124.6 | 166.1 | 207.6 | 332.2 | 415.2 | 625 |
| Columnstore support | Yes | Yes | Yes | Yes | Yes | Yes |
| In-memory OLTP storage (GB) | N/A | N/A | N/A | N/A | N/A | N/A |
| Max data size (TB) | 100 | 100 | 100 | 100 | 100 | 100 |
| Max log size (TB) | Unlimited | Unlimited | Unlimited | Unlimited | Unlimited | Unlimited |
| Tempdb max data size (GB) | 768 | 1,024 | 1,280 | 2,048 | 2,560 | 4,096 |
| Max local SSD IOPS <sup>1</sup> | 96,000 | 128,000 | 160,000 | 256,000 | 320,000 | 327,680 |
| Max log rate (MBps) | 100 | 100 | 100 | 100 | 100 | 100 |
| Local read IO latency<sup>2</sup> | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms |
| Remote read IO latency<sup>2</sup> | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms |
| Write IO latency<sup>2</sup> | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms |
| Storage type | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> |
| Max concurrent workers | 2,400 | 3,200 | 4,000 | 6,400 | 8,000 | 12,800 |
| Max concurrent logins | 2,400 | 3,200 | 4,000 | 6,400 | 8,000 | 12,800 |
| Max concurrent external connections <sup>4</sup> | 150 | 150 | 150 | 150 | 150 | 150 |
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 |
| Secondary replicas | 0-4 | 0-4 | 0-4 | 0-4 | 0-4 | 0-4 |
| Multi-AZ | N/A | N/A | N/A | N/A | N/A | N/A |
| Read Scale-out | Yes | Yes | Yes | Yes | Yes | Yes |
| Backup storage retention | 7 days | 7 days | 7 days | 7 days | 7 days | 7 days |

<sup>1</sup> Besides local SSD IO, workloads use remote [page server](hyperscale-architecture.md#page-server) IO. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance), and [Data IO in resource utilization statistics](hyperscale-performance-diagnostics.md#data-io-in-resource-utilization-statistics).

<sup>2</sup> Latency numbers are representative for typical workloads at steady state, but aren't guaranteed. 

<sup>3</sup> Hyperscale is a multi-tiered architecture with separate compute and storage components. For more information, see [Hyperscale service tier architecture](hyperscale-architecture.md).

<sup>4</sup> For more information on what counts as an external connection, see [External Connections](resource-limits-logical-server.md#external-connections).
 
## Hyperscale - provisioned compute - premium-series memory optimized

### Hyperscale premium-series memory optimized (part 1 of 3)

Compute sizes (service level objectives, or SLOs) for Hyperscale premium-series memory optimized databases follow the naming convention `HS_MOPRMS_` followed by the number of vCores. 

The following table covers these SLOs: `HS_MOPRMS_2`, `HS_MOPRMS_4`, `HS_MOPRMS_6`, `HS_MOPRMS_8`, and `HS_MOPRMS_10`:

| vCores | 2 | 4 | 6 | 8 | 10 |
|:-|-:|-:|-:|-:|-:|
| Hardware | Premium-series | Premium-series | Premium-series | Premium-series | Premium-series |
| Memory (GB) | 20.8 | 41.5 | 62.3 | 83 | 103.8 |
| Columnstore support | Yes | Yes | Yes | Yes | Yes |
| In-memory OLTP storage (GB) | N/A | N/A | N/A | N/A | N/A |
| Max data size (TB) | 100 | 100 | 100 | 100 | 100 |
| Max log size (TB) | Unlimited | Unlimited | Unlimited | Unlimited | Unlimited |
| Tempdb max data size (GB) | 64 | 128 | 192 | 256 | 320 |
| Max local SSD IOPS <sup>1</sup> | 10,240 | 20,480 | 30,720 | 40,960 | 51,200 |
| Max log rate (MBps) | 100 | 100 | 100 | 100 | 100 |
| Local read IO latency<sup>2</sup> | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms |
| Remote read IO latency<sup>2</sup> | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms |
| Write IO latency<sup>2</sup> | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms |
| Storage type | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> |
| Max concurrent workers | 200 | 400 | 600 | 800 | 1000 |
| Max concurrent logins | 200 | 400 | 600 | 800 | 1000 |
| Max concurrent external connections <sup>4</sup> | 20 | 40 | 60 | 80 | 100 |
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 |
| Secondary replicas | 0-4 | 0-4 | 0-4 | 0-4 | 0-4 |
| Multi-AZ | N/A | N/A | N/A | N/A | N/A |
| Read Scale-out | Yes | Yes | Yes | Yes | Yes |
| Backup storage retention | 7 days | 7 days | 7 days | 7 days | 7 days |

<sup>1</sup> Besides local SSD IO, workloads use remote [page server](hyperscale-architecture.md#page-server) IO. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance), and [Data IO in resource utilization statistics](hyperscale-performance-diagnostics.md#data-io-in-resource-utilization-statistics).

<sup>2</sup> Latency numbers are representative for typical workloads at steady state, but aren't guaranteed. 

<sup>3</sup> Hyperscale is a multi-tiered architecture with separate compute and storage components. For more information, see [Hyperscale service tier architecture](hyperscale-architecture.md).

<sup>4</sup> For more information on what counts as an external connection, see [External Connections](resource-limits-logical-server.md#external-connections).


### Hyperscale premium-series memory optimized (part 2 of 3)

Compute sizes (service level objectives, or SLOs) for Hyperscale premium-series memory optimized databases follow the naming convention `HS_MOPRMS_` followed by the number of vCores. 

The following table covers these SLOs: `HS_MOPRMS_12`, `HS_MOPRMS_14`, `HS_MOPRMS_16`, `HS_MOPRMS_18`, and `HS_MOPRMS_20`:

| vCores | 12 | 14 | 16 | 18 | 20 |
|:-|-:|-:|-:|-:|-:|
| Hardware | Premium-series | Premium-series | Premium-series | Premium-series | Premium-series |
| Memory (GB) | 124.6 | 145.3 | 166.1 | 186.9 | 207.6 |
| Columnstore support | Yes | Yes | Yes | Yes | Yes |
| In-memory OLTP storage (GB) | N/A | N/A | N/A | N/A | N/A |
| Max data size (TB) | 100 | 100 | 100 | 100 | 100 |
| Max log size (TB) | Unlimited | Unlimited | Unlimited | Unlimited | Unlimited |
| Tempdb max data size (GB) | 384 | 448 | 512 | 576 | 640 |
| Max local SSD IOPS <sup>1</sup> | 61,440 | 71,680 | 81,920 | 92,160 | 102,400 |
| Max log rate (MBps) | 100 | 100 | 100 | 100 | 100 |
| Local read IO latency<sup>2</sup> | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms |
| Remote read IO latency<sup>2</sup> | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms |
| Write IO latency<sup>2</sup> | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms |
| Storage type | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> |
| Max concurrent workers | 1,200 | 1,400 | 1,600 | 1,800 | 2,000 |
| Max concurrent logins | 1,200 | 1,400 | 1,600 | 1,800 | 2,000 |
| Max concurrent external connections <sup>4</sup> | 120 | 140 | 150 | 150 | 150 |
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 |
| Secondary replicas | 0-4 | 0-4 | 0-4 | 0-4 | 0-4 |
| Multi-AZ | N/A | N/A | N/A | N/A | N/A |
| Read Scale-out | Yes | Yes | Yes | Yes | Yes |
| Backup storage retention | 7 days | 7 days | 7 days | 7 days | 7 days |

<sup>1</sup> Besides local SSD IO, workloads use remote [page server](hyperscale-architecture.md#page-server) IO. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance), and [Data IO in resource utilization statistics](hyperscale-performance-diagnostics.md#data-io-in-resource-utilization-statistics).

<sup>2</sup> Latency numbers are representative for typical workloads at steady state, but aren't guaranteed. 

<sup>3</sup> Hyperscale is a multi-tiered architecture with separate compute and storage components. For more information, see [Hyperscale service tier architecture](hyperscale-architecture.md).

<sup>4</sup> For more information on what counts as an external connection, see [External Connections](resource-limits-logical-server.md#external-connections).


### Hyperscale premium-series memory optimized (part 3 of 3)

Compute sizes (service level objectives, or SLOs) for Hyperscale premium-series memory optimized databases follow the naming convention `HS_MOPRMS_` followed by the number of vCores. 

The following table covers these SLOs: `HS_MOPRMS_24`, `HS_MOPRMS_32`, `HS_MOPRMS_40`, `HS_MOPRMS_64`, and `HS_MOPRMS_80`:

| vCores | 24 | 32 | 40 | 64 | 80 |
|:-|-:|-:|-:|-:|-:|
| Hardware | Premium-series | Premium-series | Premium-series | Premium-series | Premium-series |
| Memory (GB) | 249.1 | 332.2 | 415.2 | 664.4 | 830.5 |
| Columnstore support | Yes | Yes | Yes | Yes | Yes |
| In-memory OLTP storage (GB) | N/A | N/A | N/A | N/A | N/A |
| Max data size (TB) | 100 | 100 | 100 | 100 | 100 |
| Max log size (TB) | Unlimited | Unlimited | Unlimited | Unlimited | Unlimited |
| Tempdb max data size (GB) | 768 | 1,024 | 1,280 | 2,048 | 2,560 |
| Max local SSD IOPS <sup>1</sup> | 122,880 | 163,840 | 204,800 | 327,680 | 327,680 |
| Max log rate (MBps) | 100 | 100 | 100 | 100 | 100 |
| Local read IO latency<sup>2</sup> | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms |
| Remote read IO latency<sup>2</sup> | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms |
| Write IO latency<sup>2</sup> | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms | 1-4 ms |
| Storage type | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> | Multi-tiered<sup>3</sup> |
| Max concurrent workers | 2,400 | 3,200 | 4,000 | 6,400 | 8,000 |
| Max concurrent logins | 2,400 | 3,200 | 4,000 | 6,400 | 8,000 |
| Max concurrent external connections <sup>4</sup> | 150 | 150 | 150 | 150 | 150 |
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 |
| Secondary replicas | 0-4 | 0-4 | 0-4 | 0-4 | 0-4 |
| Multi-AZ | N/A | N/A | N/A | N/A | N/A |
| Read Scale-out | Yes | Yes | Yes | Yes | Yes |
| Backup storage retention | 7 days | 7 days | 7 days | 7 days | 7 days |

<sup>1</sup> Besides local SSD IO, workloads use remote [page server](hyperscale-architecture.md#page-server) IO. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance), and [Data IO in resource utilization statistics](hyperscale-performance-diagnostics.md#data-io-in-resource-utilization-statistics).

<sup>2</sup> Latency numbers are representative for typical workloads at steady state, but aren't guaranteed. 

<sup>3</sup> Hyperscale is a multi-tiered architecture with separate compute and storage components. For more information, see [Hyperscale service tier architecture](hyperscale-architecture.md).

<sup>4</sup> For more information on what counts as an external connection, see [External Connections](resource-limits-logical-server.md#external-connections).
 
## <a id="general-purpose---provisioned-compute---gen5"></a>General Purpose - provisioned compute - standard-series (Gen5)

### <a id="gen5-hardware-part-1-3"></a>General Purpose standard-series (Gen5) (part 1 of 3)

Compute sizes (service level objectives, or SLOs) for General Purpose standard-series databases follow the naming convention `GP_Gen5_` followed by the number of vCores. 

The following table covers these SLOs: `GP_Gen5_2`, `GP_Gen5_4`, `GP_Gen5_6`, `GP_Gen5_8`, and `GP_Gen5_10`:

| vCores | 2 | 4 | 6 | 8 | 10 |
|:-|-:|-:|-:|-:|-:|
| Hardware | Gen5 | Gen5 | Gen5 | Gen5 | Gen5 |
| Memory (GB) | 10.4 | 20.8 | 31.1 | 41.5 | 51.9 |
| Columnstore support | Yes | Yes | Yes | Yes | Yes |
| In-memory OLTP storage (GB) | N/A | N/A | N/A | N/A | N/A |
| Max data size (GB) | 1024 | 1024 | 1536 | 2048 | 2048 |
| Max log size (GB) <sup>1</sup> | 307 | 307 | 461 | 461 | 461 |
| Tempdb max data size (GB) | 64 | 128 | 192 | 256 | 320 |
| Storage type | Remote SSD | Remote SSD | Remote SSD | Remote SSD | Remote SSD |
| Read IO latency (approximate) | 5-10 ms | 5-10 ms | 5-10 ms | 5-10 ms | 5-10 ms |
| Write IO latency (approximate) | 5-7 ms | 5-7 ms | 5-7 ms | 5-7 ms | 5-7 ms |
| Max data IOPS <sup>2</sup> | 640 | 1280 | 1920 | 2560 | 3200 |
| Max log rate (MBps) | 9 | 18 | 27 | 36 | 45 |
| Max concurrent workers | 200 | 400 | 600 | 800 | 1000 |
| Max concurrent logins | 200 | 400 | 600 | 800 | 1000 |
| Max concurrent external connections <sup>3</sup> | 20 | 40 | 60 | 80 | 100 |
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 |
| Number of replicas | 1 | 1 | 1 | 1 | 1 |
| Multi-AZ | Yes | Yes | Yes | Yes | Yes |
| Read Scale-out | N/A | N/A | N/A | N/A | N/A |
| Included backup storage | 1X DB size | 1X DB size | 1X DB size | 1X DB size | 1X DB size |

<sup>1</sup> For documented max data size values. Reducing max data size reduces max log size proportionally.

<sup>2</sup> The maximum value for IO sizes ranging between 8 KB and 64 KB. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance).

<sup>3</sup> For more information on what counts as an external connection, see [External Connections](resource-limits-logical-server.md#external-connections).


### <a id="gen5-hardware-part-2-3"></a>General Purpose standard-series (Gen5) (part 2 of 3)

Compute sizes (service level objectives, or SLOs) for General Purpose standard-series databases follow the naming convention `GP_Gen5_` followed by the number of vCores. 

The following table covers these SLOs: `GP_Gen5_12`, `GP_Gen5_14`, `GP_Gen5_16`, `GP_Gen5_18`, and `GP_Gen5_20`:

| vCores | 12 | 14 | 16 | 18 | 20 |
|:-|-:|-:|-:|-:|-:|
| Hardware | Gen5 | Gen5 | Gen5 | Gen5 | Gen5 |
| Memory (GB) | 62.3 | 72.7 | 83 | 93.4 | 103.8 |
| Columnstore support | Yes | Yes | Yes | Yes | Yes |
| In-memory OLTP storage (GB) | N/A | N/A | N/A | N/A | N/A |
| Max data size (GB) | 3072 | 3072 | 3072 | 3072 | 3072 |
| Max log size (GB) <sup>1</sup> | 922 | 922 | 922 | 922 | 922 |
| Tempdb max data size (GB) | 384 | 384 | 512 | 576 | 640 |
| Storage type | Remote SSD | Remote SSD | Remote SSD | Remote SSD | Remote SSD |
| Read IO latency (approximate) | 5-10 ms | 5-10 ms | 5-10 ms | 5-10 ms | 5-10 ms |
| Write IO latency (approximate) | 5-7 ms | 5-7 ms | 5-7 ms | 5-7 ms | 5-7 ms |
| Max data IOPS <sup>2</sup> | 3,840 | 4,480 | 5,120 | 5,760 | 6,400 |
| Max log rate (MBps) | 50 | 50 | 50 | 50 | 50 |
| Max concurrent workers | 1,200 | 1,400 | 1,600 | 1,800 | 2,000 |
| Max concurrent logins | 1,200 | 1,400 | 1,600 | 1,800 | 2,000 |
| Max concurrent external connections <sup>3</sup> | 120 | 140 | 150 | 150 | 150 |
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 |
| Number of replicas | 1 | 1 | 1 | 1 | 1 |
| Multi-AZ | Yes | Yes | Yes | Yes | Yes |
| Read Scale-out | N/A | N/A | N/A | N/A | N/A |
| Included backup storage | 1X DB size | 1X DB size | 1X DB size | 1X DB size | 1X DB size |

<sup>1</sup> For documented max data size values. Reducing max data size reduces max log size proportionally.

<sup>2</sup> The maximum value for IO sizes ranging between 8 KB and 64 KB. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance).

<sup>3</sup> For more information on what counts as an external connection, see [External Connections](resource-limits-logical-server.md#external-connections).


### <a id="gen5-hardware-part-3-3"></a>General Purpose standard-series (Gen5) (part 3 of 3)

Compute sizes (service level objectives, or SLOs) for General Purpose standard-series databases follow the naming convention `GP_Gen5_` followed by the number of vCores. 

The following table covers these SLOs: `GP_Gen5_24`, `GP_Gen5_32`, `GP_Gen5_40`, `GP_Gen5_80`, and `GP_Gen5_128`:

| vCores | 24 | 32 | 40 | 80 | 128 |
|:-|-:|-:|-:|-:|-:|
| Hardware | Gen5 | Gen5 | Gen5 | Gen5 | Gen5 |
| Memory (GB) | 124.6 | 166.1 | 207.6 | 415.2 | 625 |
| Columnstore support | Yes | Yes | Yes | Yes | Yes |
| In-memory OLTP storage (GB) | N/A | N/A | N/A | N/A | N/A |
| Max data size (GB) | 4096 | 4096 | 4096 | 4096 | 4096 |
| Max log size (GB) <sup>1</sup> | 1024 | 1024 | 1024 | 1024 | 1024 |
| Tempdb max data size (GB) | 768 | 1024 | 1280 | 2560 | 2560 |
| Storage type | Remote SSD | Remote SSD | Remote SSD | Remote SSD | Remote SSD |
| Read IO latency (approximate) | 5-10 ms | 5-10 ms | 5-10 ms | 5-10 ms | 5-10 ms |
| Write IO latency (approximate) | 5-7 ms | 5-7 ms | 5-7 ms | 5-7 ms | 5-7 ms |
| Max data IOPS <sup>2</sup> | 7,680 | 10,240 | 12,800 | 12,800 | 16,000 |
| Max log rate (MBps) | 50 | 50 | 50 | 50 | 50 |
| Max concurrent workers | 2,400 | 3,200 | 4,000 | 8,000 | 12,800 |
| Max concurrent logins | 2,400 | 3,200 | 4,000 | 8,000 | 12,800 |
| Max concurrent external connections <sup>3</sup> | 150 | 150 | 150 | 150 | 150 |
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 |
| Number of replicas | 1 | 1 | 1 | 1 | 1 |
| Multi-AZ | Yes | Yes | Yes | Yes | Yes |
| Read Scale-out | N/A | N/A | N/A | N/A | N/A |
| Included backup storage | 1X DB size | 1X DB size | 1X DB size | 1X DB size | 1X DB size |

<sup>1</sup> For documented max data size values. Reducing max data size reduces max log size proportionally.

<sup>2</sup> The maximum value for IO sizes ranging between 8 KB and 64 KB. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance).

<sup>3</sup> For more information on what counts as an external connection, see [External Connections](resource-limits-logical-server.md#external-connections).

## General Purpose - provisioned compute - Fsv2-series

### General Purpose Fsv2-series hardware (part 1 of 2)

Compute sizes (service level objectives, or SLOs) for General Purpose Fsv2-series databases follow the naming convention `GP_Fsv2_` followed by the number of vCores. 

The following table covers these SLOs: `GP_Fsv2_8`, `GP_Fsv2_10`, `GP_Fsv2_12`, `GP_Fsv2_14`, and `GP_Fsv2_16`:

| vCores | 8 | 10 | 12 | 14 | 16 |
|:-|-:|-:|-:|-:|-:|
| Hardware | Fsv2-series | Fsv2-series | Fsv2-series | Fsv2-series | Fsv2-series |
| Memory (GB) | 15.1 | 18.9 | 22.7 | 26.5 | 30.2 |
| Columnstore support | Yes | Yes | Yes | Yes | Yes |
| In-memory OLTP storage (GB) | N/A | N/A | N/A | N/A | N/A |
| Max data size (GB) | 1024 | 1024 | 1024 | 1024 | 1536 |
| Max log size (GB) <sup>1</sup> | 336 | 336 | 336 | 336 | 512 |
| Tempdb max data size (GB) | 37 | 46 | 56 | 65 | 74 |
| Storage type | Remote SSD | Remote SSD | Remote SSD | Remote SSD | Remote SSD |
| Read IO latency (approximate) | 5-10 ms | 5-10 ms | 5-10 ms | 5-10 ms | 5-10 ms |
| Write IO latency (approximate) | 5-7 ms | 5-7 ms | 5-7 ms | 5-7 ms | 5-7 ms |
| Max data IOPS <sup>2</sup> | 2560 | 3200 | 3840 | 4480 | 5120 |
| Max log rate (MBps) | 36 | 45 | 50 | 50 | 50 |
| Max concurrent workers | 400 | 500 | 600 | 700 | 800 |
| Max concurrent logins | 400 | 500 | 600 | 700 | 800 |
| Max concurrent external connections <sup>3</sup> | 40 | 50 | 60 | 70 | 80 |
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 |
| Number of replicas | 1 | 1 | 1 | 1 | 1 |
| Multi-AZ | N/A | N/A | N/A | N/A | N/A |
| Read Scale-out | N/A | N/A | N/A | N/A | N/A |
| Included backup storage | 1X DB size | 1X DB size | 1X DB size | 1X DB size | 1X DB size |

<sup>1</sup> For documented max data size values. Reducing max data size reduces max log size proportionally.

<sup>2</sup> The maximum value for IO sizes ranging between 8 KB and 64 KB. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance).

<sup>3</sup> For more information on what counts as an external connection, see [External Connections](resource-limits-logical-server.md#external-connections).

### General Purpose Fsv2-series hardware (part 2 of 2)

Compute sizes (service level objectives, or SLOs) for General Purpose Fsv2-series databases follow the naming convention `GP_Fsv2_` followed by the number of vCores. 

The following table covers these SLOs: `GP_Fsv2_18`, `GP_Fsv2_20`, `GP_Fsv2_24`, `GP_Fsv2_32`, `GP_Fsv2_36`, and `GP_Fsv2_72`:

| vCores | 18 | 20 | 24 | 32 | 36 | 72 |
|:-|-:|-:|-:|-:|-:|-:|
| Hardware | Fsv2-series | Fsv2-series | Fsv2-series | Fsv2-series | Fsv2-series | Fsv2-series |
| Memory (GB) | 34.0 | 37.8 | 45.4 | 60.5 | 68.0 | 136.0 |
| Columnstore support | Yes | Yes | Yes | Yes | Yes | Yes |
| In-memory OLTP storage (GB) | N/A | N/A | N/A | N/A | N/A | N/A |
| Max data size (GB) | 1536 | 1536 | 1536 | 3072 | 3072 | 4096 |
| Max log size (GB) <sup>1</sup> | 512 | 512 | 512 | 1024 | 1024 | 1024 |
| Tempdb max data size (GB) | 83 | 93 | 111 | 148 | 167 | 333 |
| Storage type | Remote SSD | Remote SSD | Remote SSD | Remote SSD | Remote SSD | Remote SSD |
| Read IO latency (approximate) | 5-10 ms | 5-10 ms | 5-10 ms | 5-10 ms | 5-10 ms | 5-10 ms |
| Write IO latency (approximate) | 5-7 ms | 5-7 ms | 5-7 ms | 5-7 ms | 5-7 ms | 5-7 ms |
| Max data IOPS <sup>2</sup> | 5760 | 6400 | 7680 | 10,240 | 11,520 | 12,800 |
| Max log rate (MBps) | 50 | 50 | 50 | 50 | 50 | 50 |
| Max concurrent workers | 900 | 1000 | 1200 | 1600 | 1800 | 3600 |
| Max concurrent logins | 900 | 1000 | 1200 | 1600 | 1800 | 3600 |
| Max concurrent external connections <sup>3</sup> | 90 | 100 | 120 | 150 | 150 | 150 |
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 |
| Number of replicas | 1 | 1 | 1 | 1 | 1 | 1 |
| Multi-AZ | N/A | N/A | N/A | N/A | N/A | N/A |
| Read Scale-out | N/A | N/A | N/A | N/A | N/A | N/A |
| Included backup storage | 1X DB size | 1X DB size | 1X DB size | 1X DB size | 1X DB size | 1X DB size |

<sup>1</sup> For documented max data size values. Reducing max data size reduces max log size proportionally.

<sup>2</sup> The maximum value for IO sizes ranging between 8 KB and 64 KB. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance).

<sup>3</sup> For more information on what counts as an external connection, see [External Connections](resource-limits-logical-server.md#external-connections).

## General Purpose - provisioned compute - DC-series
### General Purpose DC-series hardware (part 1 of 2)

Compute sizes (service level objectives, or SLOs) for General Purpose DC-series databases follow the naming convention `GP_DC_` followed by the number of vCores. 

The following table covers these SLOs: `GP_DC_2`, `GP_DC_4`, `GP_DC_6`, `GP_DC_8`, `GP_DC_10`, and `GP_DC_12`:

| vCores | 2 | 4 | 6 | 8 | 10<sup>4</sup> | 12<sup>4</sup> |
|:-|-:|-:|-:|-:|-:|-:|
| Hardware | DC-series | DC-series | DC-series | DC-series | DC-series | DC-series |
| Memory (GB) | 9 | 18 | 27 | 36 | 45 | 54 |
| Columnstore support | Yes | Yes | Yes | Yes | Yes | Yes |
| In-memory OLTP storage (GB) | N/A | N/A | N/A | N/A | N/A | N/A |
| Max data size (GB) | 1024 | 1536 | 3072 | 3072 | 3072 | 3072 |
| Max log size (GB) <sup>1</sup> | 307 | 461 | 922 | 922 | 922 | 922 |
| Tempdb max data size (GB) | 64 | 128 | 192 | 256 | 320 | 384 |
| Storage type | Remote SSD | Remote SSD | Remote SSD | Remote SSD | Remote SSD | Remote SSD |
| Read IO latency (approximate) | 5-10 ms | 5-10 ms | 5-10 ms | 5-10 ms | 5-10 ms | 5-10 ms |
| Write IO latency (approximate) | 5-7 ms | 5-7 ms | 5-7 ms | 5-7 ms | 5-7 ms | 5-7 ms |
| Max data IOPS <sup>2</sup> | 640 | 1280 | 1920 | 2560 | 3200 | 3840 |
| Max log rate (MBps) | 9 | 18 | 27 | 36 | 45 | 50 |
| Max concurrent workers | 160 | 320 | 480 | 640 | 800 | 960 |
| Max concurrent logins | 160 | 320 | 480 | 640 | 800 | 960 |
| Max concurrent external connections <sup>3</sup> | 16 | 32 | 48 | 64 | 80 | 96 |
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 |
| Number of replicas | 1 | 1 | 1 | 1 | 1 | 1 |
| Multi-AZ | N/A | N/A | N/A | N/A | N/A | N/A |
| Read Scale-out | N/A | N/A | N/A | N/A | N/A | N/A |
| Included backup storage | 1X DB size | 1X DB size | 1X DB size | 1X DB size | 1X DB size | 1X DB size |

<sup>1</sup> For documented max data size values. Reducing max data size reduces max log size proportionally.

<sup>2</sup> The maximum value for IO sizes ranging between 8 KB and 64 KB. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance).

<sup>3</sup> For more information on what counts as an external connection, see [External Connections](resource-limits-logical-server.md#external-connections).

<sup>4</sup> DC hardware series vCore offerings from 10 to 40 are currently in Preview.

### General Purpose DC-series hardware (part 2 of 2)

Compute sizes (service level objectives, or SLOs) for General Purpose DC-series databases follow the naming convention `GP_DC_` followed by the number of vCores. 

The following table covers these SLOs: `GP_DC_14`, `GP_DC_16`, `GP_DC_18`, `GP_DC_20`, `GP_DC_32`, and `GP_DC_40`:

| vCores | 14<sup>4</sup> | 16<sup>4</sup> | 18<sup>4</sup> | 20<sup>4</sup> | 32<sup>4</sup> | 40<sup>4</sup> |
|:-|-:|-:|-:|-:|-:|-:|
| Hardware | DC-series | DC-series | DC-series | DC-series | DC-series | DC-series |
| Memory (GB) | 63 | 72 | 81 | 90 | 144 | 180 |
| Columnstore support | Yes | Yes | Yes | Yes | Yes | Yes |
| In-memory OLTP storage (GB) | N/A | N/A | N/A | N/A | N/A | N/A |
| Max data size (GB) | 1024 | 1536 | 3072 | 3072 | 3072 | 3072 |
| Max log size (GB) <sup>1</sup> | 922 | 922 | 922 | 922 | 1024 | 1024 |
| Tempdb max data size (GB) | 384 | 512 | 576 | 640 | 768 | 1024 |
| Storage type | Remote SSD | Remote SSD | Remote SSD | Remote SSD | Remote SSD | Remote SSD |
| Read IO latency (approximate) | 5-10 ms | 5-10 ms | 5-10 ms | 5-10 ms | 5-10 ms | 5-10 ms |
| Write IO latency (approximate) | 5-7 ms | 5-7 ms | 5-7 ms | 5-7 ms | 5-7 ms | 5-7 ms |
| Max data IOPS <sup>2</sup> | 4480 | 5120 | 5760 | 6400 | 7680 | 10240 |
| Max log rate (MBps) | 50 | 50 | 50 | 50 | 50 | 50 |
| Max concurrent workers | 1120 | 1280 | 1440 | 1600 | 2560 | 3200 |
| Max concurrent logins | 1120 | 1280 | 1440 | 1600 | 2560 | 3200 |
| Max concurrent external connections <sup>3</sup> | 112 | 128 | 144 | 150 | 150 | 320 |
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 |
| Number of replicas | 1 | 1 | 1 | 1 | 1 | 1 |
| Multi-AZ | N/A | N/A | N/A | N/A | N/A | N/A |
| Read Scale-out | N/A | N/A | N/A | N/A | N/A | N/A |
| Included backup storage | 1X DB size | 1X DB size | 1X DB size | 1X DB size | 1X DB size | 1X DB size |

<sup>1</sup> For documented max data size values. Reducing max data size reduces max log size proportionally.

<sup>2</sup> The maximum value for IO sizes ranging between 8 KB and 64 KB. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance).

<sup>3</sup> For more information on what counts as an external connection, see [External Connections](resource-limits-logical-server.md#external-connections).

<sup>4</sup> DC hardware series vCore offerings from 10 to 40 are currently in Preview.

## <a id="business-critical---provisioned-compute---gen5"></a>Business Critical - provisioned compute - standard-series (Gen5)

### <a id="gen5-hardware-part-1-4"></a>Business Critical standard-series (Gen5) (part 1 of 3)

Compute sizes (service level objectives, or SLOs) in the Business Critical standard-series databases follow the naming convention `BC_Gen5_` followed by the number of vCores. 

The following table covers these SLOs: `BC_Gen5_2`, `BC_Gen5_4`, `BC_Gen5_6`, `BC_Gen5_8`, and `BC_Gen5_10`:

| vCores | 2 | 4 | 6 | 8 | 10 |
|:-|-:|-:|-:|-:|-:|--|
| Hardware | Gen5 | Gen5 | Gen5 | Gen5 | Gen5 |
| Memory (GB) | 10.4 | 20.8 | 31.1 | 41.5 | 51.9 |
| Columnstore support | Yes | Yes | Yes | Yes | Yes |
| In-memory OLTP storage (GB) | 1.57 | 3.14 | 4.71 | 6.28 | 8.65 |
| Max data size (GB) | 1024 | 1024 | 1536 | 2048 | 2048 |
| Max log size (GB) <sup>1</sup> | 307 | 307 | 461 | 461 | 461 |
| Tempdb max data size (GB) | 64 | 128 | 192 | 256 | 320 |
| [Max local storage size](resource-limits-logical-server.md#storage-space-governance) (GB) | 4829 | 4829 | 4829 | 4829 | 4829 |
| Storage type | Local SSD | Local SSD | Local SSD | Local SSD | Local SSD |
| Read IO latency (approximate) | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms |
| Write IO latency (approximate) | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms |
| Max data IOPS <sup>2</sup> | 8000 | 16,000 | 24,000 | 32,000 | 40,000 |
| Max log rate (MBps) | 24 | 48 | 72 | 96 | 96 |
| Max concurrent workers | 200 | 400 | 600 | 800 | 1000 |
| Max concurrent logins | 200 | 400 | 600 | 800 | 1000 |
| Max concurrent external connections <sup>3</sup> | 20 | 40 | 60 | 80 | 100 |
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 |
| Number of replicas | 4 | 4 | 4 | 4 | 4 |
| Multi-AZ | Yes | Yes | Yes | Yes | Yes |
| Read Scale-out | Yes | Yes | Yes | Yes | Yes |
| Included backup storage | 1X DB size | 1X DB size | 1X DB size | 1X DB size | 1X DB size |

<sup>1</sup> For documented max data size values. Reducing max data size reduces max log size proportionally.

<sup>2</sup> The maximum value for IO sizes ranging between 8 KB and 64 KB. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance).

<sup>3</sup> For more information on what counts as an external connection, see [External Connections](resource-limits-logical-server.md#external-connections).


### <a id="gen5-hardware-part-2-4"></a>Business Critical standard-series (Gen5) (part 2 of 3)

Compute sizes (service level objectives, or SLOs) in the Business Critical standard-series databases follow the naming convention `BC_Gen5_` followed by the number of vCores. 

The following table covers these SLOs: `BC_Gen5_12`, `BC_Gen5_14`, `BC_Gen5_16`, `BC_Gen5_18`, and `BC_Gen5_20`:

| vCores | 12 | 14 | 16 | 18 | 20 |
|:-|-:|-:|-:|-:|-:|
| Hardware | Gen5 | Gen5 | Gen5 | Gen5 | Gen5 |
| Memory (GB) | 62.3 | 72.7 | 83 | 93.4 | 103.8 |
| Columnstore support | Yes | Yes | Yes | Yes | Yes |
| In-memory OLTP storage (GB) | 11.02 | 13.39 | 15.77 | 18.14 | 20.51 |
| Max data size (GB) | 3072 | 3072 | 3072 | 3072 | 3072 |
| Max log size (GB) <sup>1</sup> | 922 | 922 | 922 | 922 | 922 |
| Tempdb max data size (GB) | 384 | 448 | 512 | 576 | 640 |
| [Max local storage size](resource-limits-logical-server.md#storage-space-governance) (GB) | 4829 | 4829 | 4829 | 4829 | 4829 |
| Storage type | Local SSD | Local SSD | Local SSD | Local SSD | Local SSD |
| Read IO latency (approximate) | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms |
| Write IO latency (approximate) | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms |
| Max data IOPS <sup>2</sup> | 48,000 | 56,000 | 64,000 | 72,000 | 80,000 |
| Max log rate (MBps) | 96 | 96 | 96 | 96 | 96 |
| Max concurrent workers | 1200 | 1400 | 1600 | 1800 | 2000 |
| Max concurrent logins | 1200 | 1400 | 1600 | 1800 | 2000 |
| Max concurrent external connections <sup>3</sup> | 120 | 140 | 150 | 150 | 150 |
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 |
| Number of replicas | 4 | 4 | 4 | 4 | 4 |
| Multi-AZ | Yes | Yes | Yes | Yes | Yes |
| Read Scale-out | Yes | Yes | Yes | Yes | Yes |
| Included backup storage | 1X DB size | 1X DB size | 1X DB size | 1X DB size | 1X DB size |

<sup>1</sup> For documented max data size values. Reducing max data size reduces max log size proportionally.

<sup>2</sup> The maximum value for IO sizes ranging between 8 KB and 64 KB. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance).

<sup>3</sup> For more information on what counts as an external connection, see [External Connections](resource-limits-logical-server.md#external-connections).

### <a id="gen5-hardware-part-3-4"></a>Business Critical standard-series (Gen5) (part 3 of 3)

Compute sizes (service level objectives, or SLOs) in the Business Critical standard-series databases follow the naming convention `BC_Gen5_` followed by the number of vCores. 

The following table covers these SLOs: `BC_Gen5_24`, `BC_Gen5_32`, `BC_Gen5_40`, `BC_Gen5_80`, and `BC_Gen5_128`:

| vCores | 24 | 32 | 40 | 80 | 128 |
|:-|-:|-:|-:|-:|-:|--|
| Hardware | Gen5 | Gen5 | Gen5 | Gen5 | Gen5 |
| Memory (GB) | 124.6 | 166.1 | 207.6 | 415.2 | 625 |
| Columnstore support | Yes | Yes | Yes | Yes | Yes |
| In-memory OLTP storage (GB) | 25.25 | 37.94 | 52.23 | 131.64 | 227.02 |
| Max data size (GB) | 4096 | 4096 | 4096 | 4096 | 4096 |
| Max log size (GB) <sup>1</sup> | 1024 | 1024 | 1024 | 1024 | 1024 |
| Tempdb max data size (GB) | 768 | 1024 | 1280 | 2560 | 2560 |
| [Max local storage size](resource-limits-logical-server.md#storage-space-governance) (GB) | 4829 | 4829 | 4829 | 4829 | 4829 |
| Storage type | Local SSD | Local SSD | Local SSD | Local SSD | Local SSD |
| Read IO latency (approximate) | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms |
| Write IO latency (approximate) | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms |
| Max data IOPS <sup>2</sup> | 96,000 | 128,000 | 160,000 | 204,800 | 327,680 |
| Max log rate (MBps) | 96 | 96 | 96 | 96 | 96 |
| Max concurrent workers | 2400 | 3200 | 4000 | 8000 | 12,800 |
| Max concurrent logins | 2400 | 3200 | 4000 | 8000 | 12,800 |
| Max concurrent external connections <sup>3</sup> | 150 | 150 | 150 | 150 | 150 |
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 |
| Number of replicas | 4 | 4 | 4 | 4 | 4 |
| Multi-AZ | Yes | Yes | Yes | Yes | Yes |
| Read Scale-out | Yes | Yes | Yes | Yes | Yes |
| Included backup storage | 1X DB size | 1X DB size | 1X DB size | 1X DB size | 1X DB size |

<sup>1</sup> For documented max data size values. Reducing max data size reduces max log size proportionally.

<sup>2</sup> The maximum value for IO sizes ranging between 8 KB and 64 KB. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance).

<sup>3</sup> For more information on what counts as an external connection, see [External Connections](resource-limits-logical-server.md#external-connections).

## Business Critical - provisioned compute - DC-series

### Business Critical DC-series hardware (part 1 of 2)

Compute sizes (service level objectives, or SLOs) in the Business Critical DC-series databases follow the naming convention `BC_DC_` followed by the number of vCores. 

The following table covers these SLOs: `BC_DC_2`, `BC_DC_4`, `BC_DC_6`, `BC_DC_8`, `BC_DC_10`, and `BC_DC_12`:

| vCores | 2 | 4 | 6 | 8 | 10<sup>4</sup> | 12<sup>4</sup> |
|:-|-:|-:|-:|-:|-:|-:|
| Hardware | DC-series | DC-series | DC-series | DC-series | DC-series | DC-series |
| Memory (GB) | 9 | 18 | 27 | 36 | 45 | 54 |
| Columnstore support | Yes | Yes | Yes | Yes | Yes | Yes |
| In-memory OLTP storage (GB) | 1.7 | 3.7 | 5.9 | 8.2 | 10.65 | 13.13 |
| Max data size (GB) | 768 | 768 | 768 | 768 | 2048 | 3072 |
| Max log size (GB) <sup>1</sup> | 230 | 230 | 230 | 230 | 461 | 922 |
| Tempdb max data size (GB) | 64 | 128 | 192 | 256 | 320 | 384 |
| [Max local storage size](resource-limits-logical-server.md#storage-space-governance) (GB) | 1406 | 1406 | 1406 | 1406 | 4829 | 4829 |
| Storage type | Local SSD | Local SSD | Local SSD | Local SSD | Local SSD | Local SSD |
| Read IO latency (approximate) | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms |
| Write IO latency (approximate) | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms |
| Max data IOPS <sup>2</sup> | 14,000 | 28,000 | 42,000 | 44,800 | 64,000 | 76,800 |
| Max log rate (MBps) | 24 | 48 | 72 | 96 | 96 | 96 |
| Max concurrent workers | 200 | 400 | 600 | 800 | 800 | 960 |
| Max concurrent logins | 200 | 400 | 600 | 800 | 800 | 960 |
| Max concurrent external connections <sup>3</sup> | 20 | 40 | 60 | 80 | 80 | 120 |
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 |
| Number of replicas | 4 | 4 | 4 | 4 | 4 | 4 |
| Multi-AZ | No | No | No | No | No | No |
| Read Scale-out | No | No | No | No | No | No |
| Included backup storage | 1X DB size | 1X DB size | 1X DB size | 1X DB size | 1X DB size | 1X DB size |

<sup>1</sup> For documented max data size values. Reducing max data size reduces max log size proportionally.

<sup>2</sup> The maximum value for IO sizes ranging between 8 KB and 64 KB. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance).

<sup>3</sup> For more information on what counts as an external connection, see [External Connections](resource-limits-logical-server.md#external-connections).

<sup>4</sup> DC hardware series vCore offerings from 10 to 40 are currently in Preview.

### Business Critical DC-series hardware (part 2 of 2)

Compute sizes (service level objectives, or SLOs) in the Business Critical DC-series databases follow the naming convention `BC_DC_` followed by the number of vCores. 

The following table covers these SLOs: `BC_DC_14`, `BC_DC_16`, `BC_DC_18`, `BC_DC_20`, `BC_DC_32`, and `BC_DC_40`:

| vCores | 14<sup>4</sup> | 16<sup>4</sup> | 18<sup>4</sup> | 20<sup>4</sup> | 32<sup>4</sup> | 40<sup>4</sup> |
|:-|-:|-:|-:|-:|-:|-:|
| Hardware | DC-series | DC-series | DC-series | DC-series | DC-series | DC-series |
| Memory (GB) | 63 | 72 | 81 | 90 | 144 | 180 |
| Columnstore support | Yes | Yes | Yes | Yes | Yes | Yes |
| In-memory OLTP storage (GB) | 15.61 | 18.09 | 20.57 | 23.05 | 37.93 | 47.86 |
| Max data size (GB) | 3072 | 3072 | 3072 | 3072 | 4096 | 4096 |
| Max log size (GB) <sup>1</sup> | 922 | 922 | 922 | 922 | 1024 | 1024 |
| Tempdb max data size (GB) | 448 | 512 | 576 | 640 | 768 | 1024 |
| [Max local storage size](resource-limits-logical-server.md#storage-space-governance) (GB) | 4829 | 4829 | 4829 | 4829 | 4829 | 4829 |
| Storage type | Local SSD | Local SSD | Local SSD | Local SSD | Local SSD | Local SSD |
| Read IO latency (approximate) | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms |
| Write IO latency (approximate) | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms |
| Max data IOPS <sup>2</sup> | 89,600 | 102,400 | 115,200 | 128,000 | 204,800 | 256,000 |
| Max log rate (MBps) | 96 | 96 | 96 | 96 | 96 | 96 |
| Max concurrent workers | 1120 | 1280 | 1440 | 1600 | 2560 | 3200 |
| Max concurrent logins | 1120 | 1280 | 1440 | 1600 | 2560 | 3200 |
| Max concurrent external connections <sup>3</sup> | 122 | 128 | 144 | 150 | 150 | 150 |
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 |
| Number of replicas | 4 | 4 | 4 | 4 | 4 | 4 |
| Multi-AZ | No | No | No | No | No | No |
| Read Scale-out | No | No | No | No | No | No |
| Included backup storage | 1X DB size | 1X DB size | 1X DB size | 1X DB size | 1X DB size | 1X DB size |

<sup>1</sup> For documented max data size values. Reducing max data size reduces max log size proportionally.

<sup>2</sup> The maximum value for IO sizes ranging between 8 KB and 64 KB. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance).

<sup>3</sup> For more information on what counts as an external connection, see [External Connections](resource-limits-logical-server.md#external-connections).

<sup>4</sup> DC hardware series vCore offerings from 10 to 40 are currently in Preview.

## Previously available hardware

This section includes details on previously available hardware.

Gen4 hardware has been retired and isn't available for provisioning, upscaling, or downscaling. [Migrate your database to a supported hardware generation](service-tiers-sql-database-vcore.md) for a wider range of vCore and storage scalability, accelerated networking, best IO performance, and minimal latency. For more information, see [Azure SQL Database Gen 4 hardware approaching end of life](https://azure.microsoft.com/updates/gen-4-hardware-on-azure-sql-database-approaching-end-of-life-in-2020/).
 
## Next steps

- For DTU resource limits for a single database, see [resource limits for single databases using the DTU purchasing model](resource-limits-dtu-single-databases.md)
- For vCore resource limits for elastic pools, see [resource limits for elastic pools using the vCore purchasing model](resource-limits-vcore-elastic-pools.md)
- For DTU resource limits for elastic pools, see [resource limits for elastic pools using the DTU purchasing model](resource-limits-dtu-elastic-pools.md)
- For resource limits for SQL Managed Instance, see [SQL Managed Instance resource limits](../managed-instance/resource-limits.md).
- For information about general Azure limits, see [Azure subscription and service limits, quotas, and constraints](/azure/azure-resource-manager/management/azure-subscription-service-limits).
- For information about resource limits at the server and subscription levels, see [overview of resource limits on a server](resource-limits-logical-server.md).
- [ALTER DATABASE - Azure SQL Database](/sql/t-sql/statements/alter-database-transact-sql?view=azuresqldb-current&preserve-view=true)
- [CREATE DATABASE - Azure SQL Database](/sql/t-sql/statements/create-database-transact-sql?view=azuresqldb-current&preserve-view=true)
