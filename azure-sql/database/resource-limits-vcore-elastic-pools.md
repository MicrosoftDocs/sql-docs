---
title: Elastic pool vCore resource limits
description: This page describes some common vCore resource limits for elastic pools in Azure SQL Database.
author: dimitri-furman
ms.author: dfurman
ms.reviewer: wiassaf, mathoma
ms.date: 10/23/2023
ms.service: sql-database
ms.subservice: elastic-pools
ms.topic: reference
ms.custom: sqldbrb=1, references_regions, ignite-2023
---
# Resource limits for elastic pools using the vCore purchasing model
[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

This article provides the detailed resource limits for Azure SQL Database elastic pools and pooled databases using the vCore purchasing model.

* For DTU purchasing model limits for single databases on a server, see [Overview of resource limits on a server](resource-limits-logical-server.md).
* For DTU purchasing model resource limits for Azure SQL Database, see [DTU resource limits single databases](resource-limits-dtu-single-databases.md) and [DTU resource limits elastic pools](resource-limits-dtu-elastic-pools.md).
* For vCore resource limits, see [vCore resource limits - Azure SQL Database](resource-limits-vcore-single-databases.md) and [vCore resource limits - elastic pools](resource-limits-vcore-elastic-pools.md).
* For more information regarding the different purchasing models, see [Purchasing models and service tiers](purchasing-models.md).

> [!IMPORTANT]
> Under some circumstances, you may need to shrink a database to reclaim unused space. For more information, see [Manage file space in Azure SQL Database](file-space-manage.md).

Each read-only replica of an elastic pool has its own resources, such as vCores, memory, data IOPS, `tempdb`, workers, and sessions. Each read-only replica is subject to elastic pool resource limits detailed later in this article.

You can set the service tier, compute size (service objective), and storage amount using:

* [Transact-SQL](elastic-pool-scale.md) via [ALTER DATABASE](/sql/t-sql/statements/alter-database-transact-sql?view=azuresqldb-current&preserve-view=true#overview-sql-database)
* [Azure portal](elastic-pool-manage.md#azure-portal)
* [PowerShell](elastic-pool-manage.md#powershell)
* [Azure CLI](elastic-pool-manage.md#azure-cli)
* [REST API](elastic-pool-manage.md#rest-api)

> [!IMPORTANT]
> For scaling guidance and considerations, see [Scale an elastic pool](elastic-pool-scale.md).

If all vCores of an elastic pool are busy, then each database in the pool receives an equal amount of compute resources to process queries. Azure SQL Database provides resource sharing fairness between databases by ensuring equal slices of compute time. Elastic pool resource sharing fairness is in addition to any amount of resource otherwise guaranteed to each database when the vCore min per database is set to a non-zero value.

For the same number of vCores, resources provided to an elastic pool may exceed the resources provided to a single database outside of an elastic pool. This means it's possible for the CPU, Data IO, and Log write utilization of an elastic pool to be less than the summation of CPU, Data IO, and Log write utilization across databases within the pool, depending on workload patterns. For example, in an extreme case with only one database in an elastic pool where database Data IO utilization is 100%, it's possible for pool Data IO utilization to be 50% for certain workload patterns. This can happen even if max vCores per database remains at the maximum supported value for the given pool size.

> [!NOTE]
> The Gen5 hardware in the vCore purchasing model has been renamed to **standard-series (Gen5)**.

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

## General Purpose - provisioned compute - standard-series (Gen5)

### General Purpose - standard-series (Gen5) (part 1 of 3)

Compute sizes (service level objectives, or SLOs) for General Purpose standard-series elastic pools follow the naming convention `GP_Gen5_` followed by the number of max vCores.

The following table covers these SLOs: `GP_Gen5_2`, `GP_Gen5_4`, `GP_Gen5_6`, `GP_Gen5_8`, and `GP_Gen5_10`:

| vCores | 2 | 4 | 6 | 8 | 10 |
|:-|-:|-:|-:|-:|-:|
| Hardware | Gen5 | Gen5 | Gen5 | Gen5 | Gen5 |
| Memory (GB) | 10.4 | 20.8 | 31.1 | 41.5 | 51.9 |
| Max number DBs per pool <sup>1</sup> | 100 | 200 | 500 | 500 | 500 |
| Columnstore support | Yes | Yes | Yes | Yes | Yes |
| In-memory OLTP storage (GB) | N/A | N/A | N/A | N/A | N/A |
| Max data size (GB) | 512 | 756 | 1536 | 2048 | 2048 |
| Max log size (GB) <sup>2</sup> | 154 | 227 | 461 | 461 | 461 |
| `tempdb` max data size (GB) | 64 | 128 | 192 | 256 | 320 |
| Storage type | Premium (Remote) Storage | Premium (Remote) Storage | Premium (Remote) Storage | Premium (Remote) Storage | Premium (Remote) Storage |
| Read IO latency (approximate) | 5-10 ms | 5-10 ms | 5-10 ms | 5-10 ms | 5-10 ms |
| Write IO latency (approximate) | 5-7 ms | 5-7 ms | 5-7 ms | 5-7 ms | 5-7 ms |
| Max data IOPS per pool <sup>3</sup> | 1,400 | 2,800 | 4,200 | 5,600 | 7,000 |
| Max log rate per pool (MBps) | 12 | 24 | 36 | 48 | 60 |
| Max concurrent workers per pool <sup>4</sup> | 210 | 420 | 630 | 840 | 1050 |
| Max concurrent logins per pool | 210 | 420 | 630 | 840 | 1050 |
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 |
| Max concurrent external connections per pool <sup>5</sup> | 21 | 42 | 63 | 84 | 105 |
| Min/max elastic pool vCore choices per database | 0, 0.25, 0.5, 1, 2 | 0, 0.25, 0.5, 1, 2, 4 | 0, 0.25, 0.5, 1, 2, 4, 6 | 0, 0.25, 0.5, 1, 2, 4, 6, 8 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10 |
| Number of replicas | 1 | 1 | 1 | 1 | 1 |
| Multi-AZ | Yes | Yes | Yes | Yes | Yes |
| Read Scale-out | N/A | N/A | N/A | N/A | N/A |
| Included backup storage | 1X DB size | 1X DB size | 1X DB size | 1X DB size | 1X DB size |

<sup>1</sup> See [Resource management in dense elastic pools](elastic-pool-resource-management.md) for additional considerations.

<sup>2</sup> For documented max data size values. Reducing max data size reduces max log size proportionally.

<sup>3</sup> The maximum value for IO sizes ranging between 8 KB and 64 KB. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance).

<sup>4</sup> For the max concurrent workers for any individual database, see [Single database resource limits](resource-limits-vcore-single-databases.md). For example, if the elastic pool is using Gen5 and the max vCore per database is set at 2, then the max concurrent workers value is 200.  If max vCore per database is set to 0.5, then the max concurrent workers value is 50 since on Gen5 there are a max of 100 concurrent workers per vCore. For other max vCore settings per database that are less 1 vCore or less, the number of max concurrent workers is similarly rescaled.

<sup>5</sup> For more information on what counts as an external connection, see [External Connections](resource-limits-logical-server.md#external-connections).

### General Purpose - standard-series (Gen5) (part 2 of 3)

Compute sizes (service level objectives, or SLOs) for General Purpose standard-series elastic pools follow the naming convention `GP_Gen5_` followed by the number of max vCores.

The following table covers these SLOs: `GP_Gen5_12`, `GP_Gen5_14`, `GP_Gen5_16`, `GP_Gen5_18`, and `GP_Gen5_20`:

| vCores | 12 | 14 | 16 | 18 | 20 |
|:-|-:|-:|-:|-:|-:|
| Hardware | Gen5 | Gen5 | Gen5 | Gen5 | Gen5 |
| Memory (GB) | 62.3 | 72.7 | 83 | 93.4 | 103.8 |
| Max number DBs per pool <sup>1</sup> | 500 | 500 | 500 | 500 | 500 |
| Columnstore support | Yes | Yes | Yes | Yes | Yes |
| In-memory OLTP storage (GB) | N/A | N/A | N/A | N/A | N/A |
| Max data size (GB) | 2048 | 2048 | 2048 | 3072 | 3072 |
| Max log size (GB) <sup>2</sup> | 614 | 614 | 614 | 922 | 922 |
| `tempdb` max data size (GB) | 384 | 448 | 512 | 576 | 640 |
| Storage type | Premium (Remote) Storage | Premium (Remote) Storage | Premium (Remote) Storage | Premium (Remote) Storage | Premium (Remote) Storage |
| Read IO latency (approximate) | 5-10 ms | 5-10 ms | 5-10 ms | 5-10 ms | 5-10 ms |
| Write IO latency (approximate) | 5-7 ms | 5-7 ms | 5-7 ms | 5-7 ms | 5-7 ms |
| Max data IOPS per pool <sup>3</sup> | 8,400 | 9,800 | 11,200 | 12,600 | 14,000 |
| Max log rate per pool (MBps) | 62.5 | 62.5 | 62.5 | 62.5 | 62.5 |
| Max concurrent workers per pool <sup>4</sup> | 1260 | 1470 | 1680 | 1890 | 2100 |
| Max concurrent logins per pool | 1260 | 1470 | 1680 | 1890 | 2100 |
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 |
| Max concurrent external connections per pool <sup>5</sup> | 126 | 147 | 150 | 150 | 150 |
| Min/max elastic pool vCore choices per database | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16, 18 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20 |
| Number of replicas | 1 | 1 | 1 | 1 | 1 |
| Multi-AZ | Yes | Yes | Yes | Yes | Yes |
| Read Scale-out | N/A | N/A | N/A | N/A | N/A |
| Included backup storage | 1X DB size | 1X DB size | 1X DB size | 1X DB size | 1X DB size |

<sup>1</sup> See [Resource management in dense elastic pools](elastic-pool-resource-management.md) for additional considerations.

<sup>2</sup> For documented max data size values. Reducing max data size reduces max log size proportionally.

<sup>3</sup> The maximum value for IO sizes ranging between 8 KB and 64 KB. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance).

<sup>4</sup> For the max concurrent workers for any individual database, see [Single database resource limits](resource-limits-vcore-single-databases.md). For example, if the elastic pool is using standard-series (Gen5) and the max vCore per database is set at 2, then the max concurrent workers value is 200.  If max vCore per database is set to 0.5, then the max concurrent workers value is 50 since on standard-series (Gen5) there are a max of 100 concurrent workers per vCore. For other max vCore settings per database that are less 1 vCore or less, the number of max concurrent workers is similarly rescaled.

<sup>5</sup> For more information on what counts as an external connection, see [External Connections](resource-limits-logical-server.md#external-connections).

### General Purpose - standard-series (Gen5) (part 3 of 3)

Compute sizes (service level objectives, or SLOs) for General Purpose standard-series elastic pools follow the naming convention `GP_Gen5_` followed by the number of max vCores.

The following table covers these SLOs: `GP_Gen5_24`, `GP_Gen5_32`, `GP_Gen5_40`, `GP_Gen5_80`, and `GP_Gen5_128`:

| vCores | 24 | 32 | 40 | 80 | 128 |
|:-|-:|-:|-:|-:|-:|
| Hardware | Gen5 | Gen5 | Gen5 | Gen5 | Gen5 |
| Memory (GB) | 124.6 | 166.1 | 207.6 | 415.2 | 625 |
| Max number DBs per pool <sup>1</sup> | 500 | 500 | 500 | 500 | 500 |
| Columnstore support | Yes | Yes | Yes | Yes | Yes |
| In-memory OLTP storage (GB) | N/A | N/A | N/A | N/A | N/A |
| Max data size (GB) | 3072 | 4096 | 4096 | 4096 | 4096 |
| Max log size (GB) <sup>2</sup> | 922 | 1229 | 1229 | 1229 | 1229 |
| `tempdb` max data size (GB) | 768 | 1024 | 1280 | 2560 | 2560 |
| Storage type | Premium (Remote) Storage | Premium (Remote) Storage | Premium (Remote) Storage | Premium (Remote) Storage | Premium (Remote) Storage |
| Read IO latency (approximate) | 5-10 ms | 5-10 ms | 5-10 ms | 5-10 ms | 5-10 ms |
| Write IO latency (approximate) | 5-7 ms | 5-7 ms | 5-7 ms | 5-7 ms | 5-7 ms |
| Max data IOPS per pool <sup>3</sup> | 16,800 | 22,400 | 28,000 | 32,000 | 40,000 |
| Max log rate per pool (MBps) | 62.5 | 62.5 | 62.5 | 62.5 | 62.5 |
| Max concurrent workers per pool <sup>4</sup> | 2520 | 3360 | 4200 | 8400 | 13,440 |
| Max concurrent logins per pool | 2520 | 3360 | 4200 | 8400 | 13,440 |
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 |
| Max concurrent external connections per pool <sup>5</sup> | 150 | 150 | 150 | 150 | 150 |
| Min/max elastic pool vCore choices per database | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 24 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 24, 32 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 24, 32, 40 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 24, 32, 40, 48, 80 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 24, 32, 40, 48, 80, 128 |
| Number of replicas | 1 | 1 | 1 | 1 | 1 |
| Multi-AZ | Yes | Yes | Yes | Yes | Yes |
| Read Scale-out | N/A | N/A | N/A | N/A | N/A |
| Included backup storage | 1X DB size | 1X DB size | 1X DB size | 1X DB size | 1X DB size |

<sup>1</sup> See [Resource management in dense elastic pools](elastic-pool-resource-management.md) for additional considerations.

<sup>2</sup> For documented max data size values. Reducing max data size reduces max log size proportionally.

<sup>3</sup> The maximum value for IO sizes ranging between 8 KB and 64 KB. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance).

<sup>4</sup> For the max concurrent workers for any individual database, see [Single database resource limits](resource-limits-vcore-single-databases.md). For example, if the elastic pool is using standard-series (Gen5) and the max vCore per database is set at 2, then the max concurrent workers value is 200.  If max vCore per database is set to 0.5, then the max concurrent workers value is 50 since on standard-series (Gen5) there are a max of 100 concurrent workers per vCore. For other max vCore settings per database that are less 1 vCore or less, the number of max concurrent workers is similarly rescaled.

<sup>5</sup> For more information on what counts as an external connection, see [External Connections](resource-limits-logical-server.md#external-connections).

## General Purpose - provisioned compute - Fsv2-series

### General Purpose - Fsv2-series (part 1 of 2)

Compute sizes (service level objectives, or SLOs) for General Purpose Fsv2-series elastic pools follow the naming convention `GP_Fsv2_` followed by the number of vCores. 

The following table covers these SLOs: `GP_Fsv2_8`, `GP_Fsv2_10`, `GP_Fsv2_12`, `GP_Fsv2_14`, and `GP_Fsv2_16`:

| vCores | 8 | 10 | 12 | 14 | 16 |
|:-|-:|-:|-:|-:|-:|
| Hardware | Fsv2-series | Fsv2-series | Fsv2-series | Fsv2-series | Fsv2-series |
| Memory (GB) | 15.1 | 18.9 | 22.7 | 26.5 | 30.2 |
| Max number DBs per pool <sup>1</sup> | 500 | 500 | 500 | 500 | 500 |
| Columnstore support | Yes | Yes | Yes | Yes | Yes |
| In-memory OLTP storage (GB) | N/A | N/A | N/A | N/A | N/A |
| Max data size (GB) | 1024 | 1024 | 1024 | 1024 | 1536 |
| Max log size (GB) <sup>2</sup> | 336 | 336 | 336 | 336 | 512 |
| `tempdb` max data size (GB) | 37 | 46 | 56 | 65 | 74 |
| Storage type | Remote SSD | Remote SSD | Remote SSD | Remote SSD | Remote SSD |
| Read IO latency (approximate) | 5-10 ms | 5-10 ms | 5-10 ms | 5-10 ms | 5-10 ms |
| Write IO latency (approximate) | 5-7 ms | 5-7 ms | 5-7 ms | 5-7 ms | 5-7 ms |
| Max data IOPS per pool <sup>3</sup> | 5,600 | 7,000 | 8,400 | 9,800 | 11,200 |
| Max log rate per pool (MBps) | 48 | 60 | 62.5 | 62.5 | 62.5 |
| Max concurrent workers per pool <sup>4</sup> | 400 | 500 | 600 | 700 | 800 |
| Max concurrent logins per pool | 400 | 500 | 600 | 700 | 800 |
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 |
| Max concurrent external connections per pool <sup>5</sup> | 40 | 50 | 60 | 70 | 80 |
| Min/max elastic pool vCore choices per database | 0, 8 | 0, 8, 10 | 0, 8, 10, 12 | 0, 8, 10, 12, 14 | 0, 8, 10, 12, 14, 16 |
| Number of replicas | 1 | 1 | 1 | 1 | 1 |
| Multi-AZ | N/A | N/A | N/A | N/A | N/A |
| Read Scale-out | N/A | N/A | N/A | N/A | N/A |
| Included backup storage | 1X DB size | 1X DB size | 1X DB size | 1X DB size | 1X DB size |

<sup>1</sup> See [Resource management in dense elastic pools](elastic-pool-resource-management.md) for additional considerations.

<sup>2</sup> For documented max data size values. Reducing max data size reduces max log size proportionally.

<sup>3</sup> The maximum value for IO sizes ranging between 8 KB and 64 KB. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance).

<sup>4</sup> For the max concurrent workers for any individual database, see [Single database resource limits](resource-limits-vcore-single-databases.md). For example, if the elastic pool is using standard-series (Gen5) and the max vCore per database is set at 2, then the max concurrent workers value is 200.  If max vCore per database is set to 0.5, then the max concurrent workers value is 50 since on standard-series (Gen5) there are a max of 100 concurrent workers per vCore. For other max vCore settings per database that are less 1 vCore or less, the number of max concurrent workers is similarly rescaled.

<sup>5</sup> For more information on what counts as an external connection, see [External Connections](resource-limits-logical-server.md#external-connections).

### General Purpose - Fsv2-series (part 2 of 2)

Compute sizes (service level objectives, or SLOs) for General Purpose Fsv2-series elastic pools follow the naming convention `GP_Fsv2_` followed by the number of vCores. 

The following table covers these SLOs: `GP_Fsv2_18`, `GP_Fsv2_20`, `GP_Fsv2_24`, `GP_Fsv2_32`, `GP_Fsv2_36`, and `GP_Fsv2_72`:

| vCores | 18 | 20 | 24 | 32 | 36 | 72 |
|:-|-:|-:|-:|-:|-:|-:|
| Hardware | Fsv2-series | Fsv2-series | Fsv2-series | Fsv2-series | Fsv2-series | Fsv2-series |
| Memory (GB) | 34.0 | 37.8 | 45.4 | 60.5 | 68.0 | 136.0 |
| Max number DBs per pool <sup>1</sup> | 500 | 500 | 500 | 500 | 500 |
| Columnstore support | Yes | Yes | Yes | Yes | Yes | Yes |
| In-memory OLTP storage (GB) | N/A | N/A | N/A | N/A | N/A | N/A |
| Max data size (GB) | 1536 | 1536 | 1536 | 3072 | 3072 | 4096 |
| Max log size (GB) <sup>2</sup> | 512 | 512 | 512 | 1024 | 1024 | 1024 |
| `tempdb` max data size (GB) | 83 | 93 | 111 | 148 | 167 | 333 |
| Storage type | Remote SSD | Remote SSD | Remote SSD | Remote SSD | Remote SSD | Remote SSD |
| Read IO latency (approximate) | 5-10 ms | 5-10 ms | 5-10 ms | 5-10 ms | 5-10 ms | 5-10 ms |
| Write IO latency (approximate) | 5-7 ms | 5-7 ms | 5-7 ms | 5-7 ms | 5-7 ms | 5-7 ms |
| Max data IOPS per pool <sup>3</sup> | 12,600 | 14,000 | 16,800 | 22,400 | 25,200 | 31,200 |
| Max log rate per pool (MBps) | 62.5 | 62.5 | 62.5 | 62.5 | 62.5 | 62.5 |
| Max concurrent workers per pool <sup>4</sup> | 900 | 1000 | 1200 | 1600 | 1800 | 3600 |
| Max concurrent logins per pool | 900 | 1000 | 1200 | 1600 | 1800 | 3600 |
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 |
| Max concurrent external connections per pool <sup>5</sup> | 90 | 100 | 120 | 150 | 150 | 150 |
| Min/max elastic pool vCore choices per database | 0, 8, 10, 12, 14, 16, 18 | 0, 8, 10, 12, 14, 16, 18, 20 | 0, 8, 10, 12, 14, 16, 18, 20, 24 | 0, 8, 10, 12, 14, 16, 18, 20, 24, 32 | 0, 8, 10, 12, 14, 16, 18, 20, 24, 32, 36 | 0, 8, 10, 12, 14, 16, 18, 20, 24, 32, 36, 40, 72 |
| Number of replicas | 1 | 1 | 1 | 1 | 1 | 1 |
| Multi-AZ | N/A | N/A | N/A | N/A | N/A | N/A |
| Read Scale-out | N/A | N/A | N/A | N/A | N/A | N/A |
| Included backup storage | 1X DB size | 1X DB size | 1X DB size | 1X DB size | 1X DB size | 1X DB size |

<sup>1</sup> See [Resource management in dense elastic pools](elastic-pool-resource-management.md) for additional considerations.

<sup>2</sup> For documented max data size values. Reducing max data size reduces max log size proportionally.

<sup>3</sup> The maximum value for IO sizes ranging between 8 KB and 64 KB. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance).

<sup>4</sup> For the max concurrent workers for any individual database, see [Single database resource limits](resource-limits-vcore-single-databases.md). For example, if the elastic pool is using standard-series (Gen5) and the max vCore per database is set at 2, then the max concurrent workers value is 200.  If max vCore per database is set to 0.5, then the max concurrent workers value is 50 since on standard-series (Gen5) there are a max of 100 concurrent workers per vCore. For other max vCore settings per database that are less 1 vCore or less, the number of max concurrent workers is similarly rescaled.

<sup>5</sup> For more information on what counts as an external connection, see [External Connections](resource-limits-logical-server.md#external-connections).

## General Purpose - provisioned compute - DC-series

### General Purpose - DC-series (part 1 of 2)

Compute sizes (service level objectives, or SLOs) for General Purpose DC-series elastic pools follow the naming convention `GP_DC_` followed by the number of vCores. 

The following table covers these SLOs: `GP_DC_2`, `GP_DC_4`, `GP_DC_6`, `GP_DC_8`, `GP_DC_10`, and `GP_DC_12`:

| vCores | 2 | 4 | 6 | 8 | 10<sup>6</sup> | 12<sup>6</sup> |
|:-|-:|-:|-:|-:|-:|-:|
| Hardware | DC | DC | DC | DC | DC | DC |
| Memory (GB) | 9 | 18 | 27 | 36 | 45 | 54 |
| Max number DBs per pool <sup>1</sup> | 100 | 400 | 400 | 400 | 500 | 500 |
| Columnstore support | Yes | Yes | Yes | Yes | Yes | Yes |
| In-memory OLTP storage (GB) | N/A | N/A | N/A | N/A | N/A | N/A |
| Max data size (GB) | 756 | 1536 | 2048 | 2048 | 3072 | 3072 |
| Max log size (GB) <sup>2</sup> | 227 | 461 | 614 | 614 | 614 | 922 |
| `tempdb` max data size (GB) | 64 | 128 | 192 | 256 | 320 | 384 |
| Storage type | Premium (Remote) Storage | Premium (Remote) Storage | Premium (Remote) Storage | Premium (Remote) Storage | Premium (Remote) Storage | Premium (Remote) Storage |
| Read IO latency (approximate) | 5-10 ms | 5-10 ms | 5-10 ms | 5-10 ms | 5-10 ms | 5-10 ms |
| Write IO latency (approximate) | 5-7 ms | 5-7 ms | 5-7 ms | 5-7 ms | 5-7 ms | 5-7 ms |
| Max data IOPS per pool <sup>3</sup> | 1,400 | 2,800 | 4,200 | 5,600 | 7,000 | 8,400 |
| Max log rate per pool (MBps) | 12 | 24 | 36 | 48 | 60 | 62.5|
| Max concurrent workers per pool <sup>4</sup> | 168 | 336 | 504 | 672 | 800 | 960 |
| Max concurrent logins per pool | 168 | 336 | 504 | 672 | 800 | 960 |
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 |
| Max concurrent external connections per pool <sup>5</sup> | 16 | 33 | 50 | 67 | 80 | 96 |
| Min/max elastic pool vCore choices per database | 0, 0.25, 0.5, 2 | 0, 0.25, 0.5, 2, 4 | 0, 0.25, 0.5, 2, 4, 6 | 0, 0.25, 0.5, 2, 4, 6, 8 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12 |
| Number of replicas | 1 | 1 | 1 | 1 |1 |1 |
| Multi-AZ | N/A | N/A | N/A | N/A |N/A |N/A |
| Read Scale-out | N/A | N/A | N/A | N/A |N/A |N/A |
| Included backup storage | 1X DB size | 1X DB size | 1X DB size | 1X DB size |1X DB size |1X DB size |

<sup>1</sup> See [Resource management in dense elastic pools](elastic-pool-resource-management.md) for additional considerations.

<sup>2</sup> For documented max data size values. Reducing max data size reduces max log size proportionally.

<sup>3</sup> The maximum value for IO sizes ranging between 8 KB and 64 KB. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance).

<sup>4</sup> For the max concurrent workers for any individual database, see [Single database resource limits](resource-limits-vcore-single-databases.md). For example, if the elastic pool is using standard-series (Gen5) and the max vCore per database is set at 2, then the max concurrent workers value is 200.  If max vCore per database is set to 0.5, then the max concurrent workers value is 50 since on standard-series (Gen5) there are a max of 100 concurrent workers per vCore. For other max vCore settings per database that are less 1 vCore or less, the number of max concurrent workers is similarly rescaled.

<sup>5</sup> For more information on what counts as an external connection, see [External Connections](resource-limits-logical-server.md#external-connections).

<sup>6</sup> DC hardware series vCore offerings from 10 to 40 are currently in Preview.

### General Purpose - DC-series (part 2 of 2)

Compute sizes (service level objectives, or SLOs) for General Purpose DC-series elastic pools follow the naming convention `GP_DC_` followed by the number of vCores. 

The following table covers these SLOs: `GP_DC_14`, `GP_DC_16`, `GP_DC_18`, `GP_DC_20`, `GP_DC_32`, and `GP_DC_40`:

| vCores | 14<sup>6</sup> | 16<sup>6</sup> | 18<sup>6</sup> | 20<sup>6</sup> | 32<sup>6</sup> | 40<sup>6</sup> |
|:-|-:|-:|-:|-:|-:|-:|
| Hardware | DC | DC | DC | DC | DC | DC |
| Memory (GB) | 63 | 72 | 81 | 90 | 144 | 180 |
| Max number DBs per pool <sup>1</sup> | 500 | 500 | 500 | 500 | 500 | 500 |
| Columnstore support | Yes | Yes | Yes | Yes | Yes | Yes |
| In-memory OLTP storage (GB) | N/A | N/A | N/A | N/A | N/A | N/A |
| Max data size (GB) | 3072 | 3072 | 3072 | 3072 | 3072 | 4096 |
| Max log size (GB) <sup>2</sup> | 922 | 922 | 922 | 922 | 1024 | 1024 |
| `tempdb` max data size (GB) | 384 | 512 | 576 | 640 | 768 | 1024 |
| Storage type | Premium (Remote) Storage | Premium (Remote) Storage | Premium (Remote) Storage | Premium (Remote) Storage | Premium (Remote) Storage | Premium (Remote) Storage |
| Read IO latency (approximate) | 5-10 ms | 5-10 ms | 5-10 ms | 5-10 ms | 5-10 ms | 5-10 ms |
| Write IO latency (approximate) | 5-7 ms | 5-7 ms | 5-7 ms | 5-7 ms | 5-7 ms | 5-7 ms |
| Max data IOPS per pool <sup>3</sup> | 9,800 | 11,200 | 12,600 | 14,000 | 22,400 | 25,200 |
| Max log rate per pool (MBps) | 62.5 | 62.5 | 62.5 | 62.5 | 62.550 | 62.550 |
| Max concurrent workers per pool <sup>4</sup> | 1120 | 1280 | 1440 | 1600 | 2560 | 3200 |
| Max concurrent logins per pool |  1120 | 1280 | 1440 | 1600 | 2560 | 3200 |
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 |
| Max concurrent external connections per pool <sup>5</sup> | 112 | 128 | 144 | 150 | 150 | 150 |
| Min/max elastic pool vCore choices per database | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16, 18 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 32 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 32, 40
| Number of replicas | 1 | 1 | 1 | 1 |1 |1 |
| Multi-AZ | N/A | N/A | N/A | N/A |N/A |N/A |
| Read Scale-out | N/A | N/A | N/A | N/A |N/A |N/A |
| Included backup storage | 1X DB size | 1X DB size | 1X DB size | 1X DB size |1X DB size |1X DB size |

<sup>1</sup> See [Resource management in dense elastic pools](elastic-pool-resource-management.md) for additional considerations.

<sup>2</sup> For documented max data size values. Reducing max data size reduces max log size proportionally.

<sup>3</sup> The maximum value for IO sizes ranging between 8 KB and 64 KB. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance).

<sup>4</sup> For the max concurrent workers for any individual database, see [Single database resource limits](resource-limits-vcore-single-databases.md). For example, if the elastic pool is using standard-series (Gen5) and the max vCore per database is set at 2, then the max concurrent workers value is 200.  If max vCore per database is set to 0.5, then the max concurrent workers value is 50 since on standard-series (Gen5) there are a max of 100 concurrent workers per vCore. For other max vCore settings per database that are less 1 vCore or less, the number of max concurrent workers is similarly rescaled.

<sup>5</sup> For more information on what counts as an external connection, see [External Connections](resource-limits-logical-server.md#external-connections).

<sup>6</sup> DC hardware series vCore offerings from 10 to 40 are currently in Preview.

## Business Critical - provisioned compute - standard-series (Gen5)

### Business Critical - standard-series (Gen5) (part 1 of 3)

Compute sizes (service level objectives, or SLOs) in the Business Critical standard-series elastic pools follow the naming convention `BC_Gen5_` followed by the number of vCores. 

The following table covers these SLOs: `BC_Gen5_4`, `BC_Gen5_6`, `BC_Gen5_8`, `BC_Gen5_10`, and `BC_Gen5_12`:

| vCores | 4 | 6 | 8 | 10 | 12 |
|:-|-:|-:|-:|-:|-:|
| Hardware | Gen5 | Gen5 | Gen5 | Gen5 | Gen5 |
| Memory (GB) | 20.8 | 31.1 | 41.5 | 51.9 | 62.3 |
| Max number DBs per pool <sup>1</sup> | 50 | 100 | 100 | 100 | 100 |
| Columnstore support | Yes | Yes | Yes | Yes | Yes |
| In-memory OLTP storage (GB) | 3.14 | 4.71 | 6.28 | 8.65 | 11.02 |
| Max data size (GB) | 1024 | 1536 | 2048 | 2048 | 3072 |
| Max log size (GB) <sup>2</sup> | 307 | 307 | 461 | 461 | 922 |
| `tempdb` max data size (GB) | 128 | 192 | 256 | 320 | 384 |
| [Max local storage size](resource-limits-logical-server.md#storage-space-governance) (GB) | 4829 | 4829 | 4829 | 4829 | 4829 |
| Storage type | Local SSD | Local SSD | Local SSD | Local SSD | Local SSD |
| Read IO latency (approximate) | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms |
| Write IO latency (approximate) | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms |
| Max data IOPS per pool <sup>3</sup> | 18,000 | 27,000 | 36,000 | 45,000 | 54,000 |
| Max log rate per pool (MBps) | 60 | 90 | 120 | 120 | 120 |
| Max concurrent workers per pool <sup>4</sup> | 420 | 630 | 840 | 1050 | 1260 |
| Max concurrent logins per pool | 420 | 630 | 840 | 1050 | 1260 |
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 |
| Max concurrent external connections per pool <sup>5</sup> | 42 | 63 | 84 | 105 | 126 |
| Min/max elastic pool vCore choices per database | 0, 0.25, 0.5, 1, 2, 4 | 0, 0.25, 0.5, 1, 2, 4, 6 | 0, 0.25, 0.5, 1, 2, 4, 6, 8 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12 |
| Number of replicas | 4 | 4 | 4 | 4 | 4 |
| Multi-AZ | Yes | Yes | Yes | Yes | Yes |
| Read Scale-out | Yes | Yes | Yes | Yes | Yes |
| Included backup storage | 1X DB size | 1X DB size | 1X DB size | 1X DB size | 1X DB size |

<sup>1</sup> See [Resource management in dense elastic pools](elastic-pool-resource-management.md) for additional considerations.

<sup>2</sup> For documented max data size values. Reducing max data size reduces max log size proportionally.

<sup>3</sup> The maximum value for IO sizes ranging between 8 KB and 64 KB. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance).

<sup>4</sup> For the max concurrent workers for any individual database, see [Single database resource limits](resource-limits-vcore-single-databases.md). For example, if the elastic pool is using standard-series (Gen5) and the max vCore per database is set at 2, then the max concurrent workers value is 200.  If max vCore per database is set to 0.5, then the max concurrent workers value is 50 since on standard-series (Gen5) there are a max of 100 concurrent workers per vCore. For other max vCore settings per database that are less 1 vCore or less, the number of max concurrent workers is similarly rescaled.

<sup>5</sup> For more information on what counts as an external connection, see [External Connections](resource-limits-logical-server.md#external-connections).

### Business Critical - standard-series (Gen5) (part 2 of 3)

Compute sizes (service level objectives, or SLOs) in the Business Critical standard-series elastic pools follow the naming convention `BC_Gen5_` followed by the number of vCores. 

The following table covers these SLOs: `BC_Gen5_14`, `BC_Gen5_16`, `BC_Gen5_18`, `BC_Gen5_20`, and `BC_Gen5_24`:

| vCores | 14 | 16 | 18 | 20 | 24 |
|:-|-:|-:|-:|-:|-:|
| Hardware | Gen5 | Gen5 | Gen5 | Gen5 | Gen5 |
| Memory (GB) | 72.7 | 83 | 93.4 | 103.8 | 124.6 |
| Max number DBs per pool <sup>1</sup> | 100 | 100 | 100 | 100 | 100 |
| Columnstore support | Yes | Yes | Yes | Yes | Yes |
| In-memory OLTP storage (GB) | 13.39 | 15.77 | 18.14 | 20.51 | 25.25 |
| Max data size (GB) | 3072 | 3072 | 3072 | 3072 | 4096 |
| Max log size (GB) <sup>2</sup> | 922 | 922 | 922 | 922 | 1229 |
| `tempdb` max data size (GB) | 448 | 512 | 576 | 640 | 768 |
| [Max local storage size](resource-limits-logical-server.md#storage-space-governance) (GB) | 4829 | 4829 | 4829 | 4829 | 4829 |
| Storage type | Local SSD | Local SSD | Local SSD | Local SSD | Local SSD |
| Read IO latency (approximate) | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms |
| Write IO latency (approximate) | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms |
| Max data IOPS per pool <sup>3</sup> | 63,000 | 72,000 | 81,000 | 90,000 | 108,000 |
| Max log rate per pool (MBps) | 120 | 120 | 120 | 120 | 120 |
| Max concurrent workers per pool <sup>4</sup> | 1470 | 1680 | 1890 | 2100 | 2520 |
| Max concurrent logins per pool | 1470 | 1680 | 1890 | 2100 | 2520 |
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 |
| Max concurrent external connections per pool <sup>5</sup> | 147 | 150 | 150 | 150 | 150 |
| Min/max elastic pool vCore choices per database | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16, 18 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 24 |
| Number of replicas | 4 | 4 | 4 | 4 | 4 |
| Multi-AZ | Yes | Yes | Yes | Yes | Yes |
| Read Scale-out | Yes | Yes | Yes | Yes | Yes |
| Included backup storage | 1X DB size | 1X DB size | 1X DB size | 1X DB size | 1X DB size |

<sup>1</sup> See [Resource management in dense elastic pools](elastic-pool-resource-management.md) for additional considerations.

<sup>2</sup> For documented max data size values. Reducing max data size reduces max log size proportionally.

<sup>3</sup> The maximum value for IO sizes ranging between 8 KB and 64 KB. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance).

<sup>4</sup> For the max concurrent workers for any individual database, see [Single database resource limits](resource-limits-vcore-single-databases.md). For example, if the elastic pool is using standard-series (Gen5) and the max vCore per database is set at 2, then the max concurrent workers value is 200.  If max vCore per database is set to 0.5, then the max concurrent workers value is 50 since on standard-series (Gen5) there are a max of 100 concurrent workers per vCore. For other max vCore settings per database that are less 1 vCore or less, the number of max concurrent workers is similarly rescaled.

### Business Critical - standard-series (Gen5) (part 3 of 3)

Compute sizes (service level objectives, or SLOs) in the Business Critical standard-series elastic pools follow the naming convention `BC_Gen5_` followed by the number of vCores. 

The following table covers these SLOs: `BC_Gen5_32`, `BC_Gen5_40`, `BC_Gen5_80`, and `BC_Gen5_128`:

| vCores | 32 | 40 | 80 | 128 |
|:-|-:|-:|-:|-:|
| Hardware | Gen5 | Gen5 | Gen5 | Gen5 |
| Memory (GB) | 166.1 | 207.6 | 415.2 | 625 |
| Max number DBs per pool <sup>1</sup> | 100 | 100 | 100 | 100 |
| Columnstore support | Yes | Yes | Yes | Yes |
| In-memory OLTP storage (GB) | 37.94 | 52.23 | 131.68 | 227.02 |
| Max data size (GB) | 4096 | 4096 | 4096 | 4096 |
| Max log size (GB) <sup>2</sup> | 1229 | 1229 | 1229 | 1229 |
| `tempdb` max data size (GB) | 1024 | 1280 | 2560 | 2560 |
| [Max local storage size](resource-limits-logical-server.md#storage-space-governance) (GB) | 4829 | 4829 | 4829 | 4829 |
| Storage type | Local SSD | Local SSD | Local SSD | Local SSD |
| Read IO latency (approximate) | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms |
| Write IO latency (approximate) | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms |
| Max data IOPS per pool <sup>3</sup> | 144,000 | 180,000 | 256,000 | 409,600 |
| Max log rate per pool (MBps) | 120 | 120 | 120 | 120 |
| Max concurrent workers per pool <sup>4</sup> | 3360 | 4200 | 8400 | 13,440 |
| Max concurrent logins per pool | 3360 | 4200 | 8400 | 13,440 |
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 30,000 |
| Max concurrent external connections per pool <sup>5</sup> | 150 | 150 | 150 | 150 |
| Min/max elastic pool vCore choices per database | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 24, 32 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 24, 32, 40 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 24, 32, 40, 48, 80 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 24, 32, 40, 48, 80, 128 |
| Number of replicas | 4 | 4 | 4 | 4 |
| Multi-AZ | Yes | Yes | Yes | Yes |
| Read Scale-out | Yes | Yes | Yes | Yes |
| Included backup storage | 1X DB size | 1X DB size | 1X DB size | 1X DB size |

<sup>1</sup> See [Resource management in dense elastic pools](elastic-pool-resource-management.md) for additional considerations.

<sup>2</sup> For documented max data size values. Reducing max data size reduces max log size proportionally.

<sup>3</sup> The maximum value for IO sizes ranging between 8 KB and 64 KB. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance).

<sup>4</sup> For the max concurrent workers for any individual database, see [Single database resource limits](resource-limits-vcore-single-databases.md). For example, if the elastic pool is using standard-series (Gen5) and the max vCore per database is set at 2, then the max concurrent workers value is 200.  If max vCore per database is set to 0.5, then the max concurrent workers value is 50 since on standard-series (Gen5) there are a max of 100 concurrent workers per vCore. For other max vCore settings per database that are less 1 vCore or less, the number of max concurrent workers is similarly rescaled.

<sup>5</sup> For more information on what counts as an external connection, see [External Connections](resource-limits-logical-server.md#external-connections).

## Business Critical - provisioned compute - DC-series

### Business Critical - DC-series (part 1 of 2)

Compute sizes (service level objectives, or SLOs) in the Business Critical DC-series elastic pools follow the naming convention `BC_DC_` followed by the number of vCores. 

The following table covers these SLOs: `BC_DC_2`, `BC_DC_4`, `BC_DC_6`, `BC_DC_8`, `BC_DC_10`, and `BC_DC_12`:

| vCores | 2 | 4 | 6 | 8 | 10<sup>6</sup> | 12<sup>6</sup> |
|:-|-:|-:|-:|-:|-:|-:|
| Hardware | DC | DC | DC | DC | DC | DC |
| Memory (GB) | 9 | 18 | 27 | 36 | 45 | 54 |
| Max number DBs per pool <sup>1</sup> | 50 | 100 | 100 | 100 | 100 | 100 |
| Columnstore support | Yes | Yes | Yes | Yes |Yes |Yes |
| In-memory OLTP storage (GB) | 1.7 | 3.7 | 5.9 | 8.2 | 10.65 | 11.02 |
| Max data size (GB) | 768 | 768 | 768 | 768 | 2048 | 3072 |
| Max log size (GB) <sup>2</sup> | 230 | 230 | 230 | 230 | 461 | 922 |
| `tempdb` max data size (GB) | 64 | 128 | 192 | 256 | 320 | 384 | 
| [Max local storage size](resource-limits-logical-server.md#storage-space-governance) (GB) | 1406 | 1406 | 1406 | 1406 | 4829 | 4829 |
| Storage type | Local SSD | Local SSD | Local SSD | Local SSD | Local SSD | Local SSD |
| Read IO latency (approximate) | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms |
| Write IO latency (approximate) | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms |
| Max data IOPS per pool <sup>3</sup> | 15,750 | 31,500 | 47,250 | 56,000 | 72,000 | 86,400 |
| Max log rate per pool (MBps) | 20 | 60 | 90 | 120 | 120 | 120 |
| Max concurrent workers per pool <sup>4</sup> | 168 | 336 | 504 | 672 | 804 | 1008 |
| Max concurrent logins per pool | 168 | 336 | 504 | 672 | 804 | 1008 |
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 |
| Max concurrent external connections per pool <sup>5</sup> | 16 | 33 | 50 | 67 | 84 | 100 |
| Min/max elastic pool vCore choices per database | 0, 0.25, 0.5, 2 | 0, 0.25, 0.5, 2, 4 | 0, 0.25, 0.5, 2, 4, 6 | 0, 0.25, 0.5, 2, 4, 6, 8 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12 |
| Number of replicas | 4 | 4 | 4 | 4 | 4 | 4 |
| Multi-AZ | No | No | No | No | No | No |
| Read Scale-out | Yes | Yes | Yes | Yes | Yes | Yes |
| Included backup storage | 1X DB size | 1X DB size | 1X DB size | 1X DB size | 1X DB size | 1X DB size |

<sup>1</sup> See [Resource management in dense elastic pools](elastic-pool-resource-management.md) for additional considerations.

<sup>2</sup> For documented max data size values. Reducing max data size reduces max log size proportionally.

<sup>3</sup> The maximum value for IO sizes ranging between 8 KB and 64 KB. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance).

<sup>4</sup> For the max concurrent workers for any individual database, see [Single database resource limits](resource-limits-vcore-single-databases.md). For example, if the elastic pool is using standard-series (Gen5) and the max vCore per database is set at 2, then the max concurrent workers value is 200.  If max vCore per database is set to 0.5, then the max concurrent workers value is 50 since on standard-series (Gen5) there are a max of 100 concurrent workers per vCore. For other max vCore settings per database that are less 1 vCore or less, the number of max concurrent workers is similarly rescaled.

<sup>5</sup> For more information on what counts as an external connection, see [External Connections](resource-limits-logical-server.md#external-connections).

<sup>6</sup> DC hardware series vCore offerings from 10 to 40 are currently in Preview.

### Business Critical - DC-series (part 2 of 2)

Compute sizes (service level objectives, or SLOs) in the Business Critical DC-series elastic pools follow the naming convention `BC_DC_` followed by the number of vCores. 

The following table covers these SLOs: `BC_DC_14`, `BC_DC_16`, `BC_DC_18`, `BC_DC_20`, `BC_DC_32`, and `BC_DC_40`:

| vCores | 14<sup>6</sup> | 16<sup>6</sup> | 18<sup>6</sup> | 20<sup>6</sup> | 32<sup>6</sup> | 40<sup>6</sup> |
|:-|-:|-:|-:|-:|-:|-:|
| Hardware | DC | DC | DC | DC | DC | DC |
| Memory (GB) | 63 | 72 | 81 | 90 | 144 | 180 |
| Max number DBs per pool <sup>1</sup> | 50 | 100 | 100 | 100 | 100 | 100 |
| Columnstore support | Yes | Yes | Yes | Yes |Yes |Yes |
| In-memory OLTP storage (GB) | 13.39 | 15.77 | 18.14 | 20.51 | 25.25 | 37.93 |
| Max data size (GB) | 3072 | 3072 | 3072 | 3072 | 4096 | 4096 |
| Max log size (GB) <sup>2</sup> | 922 | 922 | 922 | 922 | 1024 | 1024 |
| `tempdb` max data size (GB) | 448 | 512 | 576 | 640 | 768 | 1024 | 
| [Max local storage size](resource-limits-logical-server.md#storage-space-governance) (GB) | 4829 | 4829 | 4829 | 4829 | 4829 | 4829 |
| Storage type | Local SSD | Local SSD | Local SSD | Local SSD | Local SSD | Local SSD |
| Read IO latency (approximate) | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms |
| Write IO latency (approximate) | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms |
| Max data IOPS per pool <sup>3</sup> | 100,800 | 115,200 | 129,600 | 144,000 | 230,400 | 288,000 |
| Max log rate per pool (MBps) | 120 | 120 | 120 | 120 | 120 | 120 |
| Max concurrent workers per pool <sup>4</sup> | 1176 | 1344 | 1512 | 1680 | 2688 | 3360 |
| Max concurrent logins per pool | 1176 | 1344 | 1512 | 1680 | 2688 | 3360 | 
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 |
| Max concurrent external connections per pool <sup>5</sup> | 117 | 134 | 151 | 168 | 268 | 336 |
| Min/max elastic pool vCore choices per database | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16, 18 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 32 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 32, 40 |
| Number of replicas | 4 | 4 | 4 | 4 | 4 | 4 |
| Multi-AZ | No | No | No | No | No | No |
| Read Scale-out | Yes | Yes | Yes | Yes | Yes | Yes |
| Included backup storage | 1X DB size | 1X DB size | 1X DB size | 1X DB size | 1X DB size | 1X DB size |

<sup>1</sup> See [Resource management in dense elastic pools](elastic-pool-resource-management.md) for additional considerations.

<sup>2</sup> For documented max data size values. Reducing max data size reduces max log size proportionally.

<sup>3</sup> The maximum value for IO sizes ranging between 8 KB and 64 KB. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance).

<sup>4</sup> For the max concurrent workers for any individual database, see [Single database resource limits](resource-limits-vcore-single-databases.md). For example, if the elastic pool is using standard-series (Gen5) and the max vCore per database is set at 2, then the max concurrent workers value is 200.  If max vCore per database is set to 0.5, then the max concurrent workers value is 50 since on standard-series (Gen5) there are a max of 100 concurrent workers per vCore. For other max vCore settings per database that are less 1 vCore or less, the number of max concurrent workers is similarly rescaled.

<sup>5</sup> For more information on what counts as an external connection, see [External Connections](resource-limits-logical-server.md#external-connections).

<sup>6</sup> DC hardware series vCore offerings from 10 to 40 are currently in Preview.

## Hyperscale - provisioned compute - standard-series (Gen5)

Although the published Hyperscale resource limits for standard-series and premium-series are the same, premium-series offers faster CPU performance compared to standard-series, and scales up to 128 vCores, compared to 80 vCores for standard-series. Resources using premium-series are guaranteed to run on hardware with newer CPUs. Standard-series does not provide this guarantee and, depending on availability, resources may be placed on older hardware. There is no price difference between the two, but premium-series may not be available in all regions.

> [!NOTE]
> Elastic pools for Hyperscale databases are currently in preview.

### Hyperscale - standard-series (Gen5) (part 1 of 2)

Compute sizes (service level objectives, or SLOs) in the Hyperscale standard-series elastic pools follow the naming convention `HS_Gen5_` followed by the number of vCores. 

The following table covers these SLOs: `HS_Gen5_4`, `HS_Gen5_6`, `HS_Gen5_8`, `HS_Gen5_10`, `HS_Gen5_12`, and `HS_Gen5_14`:

| vCores | 4 | 6 | 8 | 10 | 12 | 14 |
|-:|-:|-:|-:|-:|-:|-:|
| Hardware | Gen5 | Gen5 | Gen5 | Gen5 | Gen5 | Gen5 |
| Memory (GB) | 20.8 | 31.1 | 41.5 | 51.9 | 62.3 | 72.7 |
| Max number DBs per pool <sup>1</sup> | 25 | 25 | 25 | 25 | 25 | 25 |
| Columnstore support | Yes | Yes | Yes | Yes | Yes | Yes |
| In-memory OLTP storage (GB) | N/A | N/A | N/A | N/A | N/A | N/A |
| Max data size per pool (TB) | 100 | 100 | 100 | 100 | 100 | 100 |
| `tempdb` max data size (GB) | 128 | 192 | 256 | 320 | 384 | 448 |
| Max local SSD IOPS per pool <sup>2</sup> | 18,000 | 27,000 | 36,000 | 45,000 | 54,000 | 63,000 |
| Max log rate per pool (MBps ) | 125 | 125 | 125 | 125 | 125 | 125 |
| Local read IO latency<sup>3</sup> | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms |
| Remote read IO latency<sup>3</sup> | 1-5 ms | 1-5 ms | 1-5 ms | 1-5 ms | 1-5 ms | 1-5 ms |
| Write IO latency<sup>3</sup> | 3-5 ms | 3-5 ms | 3-5 ms | 3-5 ms | 3-5 ms | 3-5 ms |
| Storage type | Multi-tiered<sup>4</sup> | Multi-tiered<sup>4</sup> | Multi-tiered<sup>4</sup> | Multi-tiered<sup>4</sup> | Multi-tiered<sup>4</sup> | Multi-tiered<sup>4</sup> |
| Max concurrent workers per pool <sup>5</sup> | 420 | 630 | 840 | 1050 | 1260 | 1470 |
| Max concurrent external connections per pool <sup>6</sup> | 42 | 63 | 84 | 105 | 126 | 147 |
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 |
| Min/max elastic pool vCore choices per database | 0, 0.25, 0.5, 1, 2, 4 | 0, 0.25, 0.5, 1, 2, 4, 6 | 0, 0.25, 0.5, 1, 2, 4, 6, 8 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14 |
| Secondary pool replicas | 0-4 | 0-4 | 0-4 | 0-4 | 0-4 | 0-4 |
| Multi-AZ | Not supported | Not supported | Not supported | Not supported | Not supported | Not supported |
| Read Scale-out | Yes | Yes | Yes | Yes | Yes | Yes |

<sup>1</sup> See [Resource management in dense elastic pools](elastic-pool-resource-management.md) for additional considerations.

<sup>2</sup> Hyperscale is a multi-tiered architecture with separate compute and storage components. Review [Hyperscale service tier](hyperscale-elastic-pool-overview.md#architecture) and [Hyperscale service tier](service-tier-hyperscale.md#distributed-functions-architecture) for more information.

<sup>3</sup> Besides local SSD IO, workloads use remote [page server](hyperscale-architecture.md#page-server) IO. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance), and [Data IO in resource utilization statistics](hyperscale-performance-diagnostics.md#data-io-in-resource-utilization-statistics).

<sup>4</sup> Latency is 1-2 ms for data on local compute replica SSD, which caches most used data pages. Higher latency for data retrieved from page servers.

<sup>5</sup> For the max concurrent workers for any individual database, see [Single database resource limits](resource-limits-vcore-single-databases.md). For example, if the elastic pool is using standard-series (Gen5) and the max vCore per database is set at 2, then the max concurrent workers value is 200.  If max vCore per database is set to 0.5, then the max concurrent workers value is 50 since on standard-series (Gen5) there are a max of 100 concurrent workers per vCore. For other max vCore settings per database that are less 1 vCore or less, the number of max concurrent workers is similarly rescaled.

<sup>6</sup> For more information on what counts as an external connection, see [External Connections](resource-limits-logical-server.md#external-connections).

### Hyperscale - standard-series (Gen5) (part 2 of 2)

Compute sizes (service level objectives, or SLOs) in the Hyperscale standard-series elastic pools follow the naming convention `HS_Gen5_` followed by the number of vCores. 

The following table covers these SLOs: `HS_Gen5_16`, `HS_Gen5_18`, `HS_Gen5_20`, `HS_Gen5_24`, `HS_Gen5_32`, `HS_Gen5_40`, and `HS_Gen5_80`:

| vCores | 16 | 18 | 20 | 24 | 32 | 40 | 80 |
|:-|-:|-:|-:|-:|-:|-:|-:|
| Hardware | Gen5 | Gen5 | Gen5 | Gen5 | Gen5 | Gen5 | Gen5 |
| Max number DBs per pool <sup>1</sup> | 25 | 25 | 25 | 25 | 25 | 25 | 25 |
| Memory (GB) | 83 | 93.4 | 103.8 | 124.6 | 166.1 | 207.6 | 415.2 |
| Columnstore support | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| In-memory OLTP storage (GB) | N/A | N/A | N/A | N/A | N/A | N/A | N/A |
| Max data size per pool (TB) | 100 | 100 | 100 | 100 | 100 | 100 | 100 |
| `tempdb` max data size (GB) | 512 | 576 | 640 | 768 | 1024 | 1280 | 2560 |
| Max local SSD IOPS per pool <sup>2</sup> | 72,000 | 81,000 | 90,000 | 108,000 | 144,000 | 180,000 | 256,000 |
| Max log rate per pool (MBps) | 125 | 125 | 125 | 125 | 125 | 125 | 125 |
| Local read IO latency<sup>3</sup> | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms |
| Remote read IO latency<sup>3</sup> | 1-5 ms | 1-5 ms | 1-5 ms | 1-5 ms | 1-5 ms | 1-5 ms | 1-5 ms |
| Write IO latency<sup>3</sup> | 3-5 ms | 3-5 ms | 3-5 ms | 3-5 ms | 3-5 ms | 3-5 ms | 3-5 ms |
| Storage type | Multi-tiered<sup>4</sup> | Multi-tiered<sup>4</sup> | Multi-tiered<sup>4</sup> | Multi-tiered<sup>4</sup> | Multi-tiered<sup>4</sup> | Multi-tiered<sup>4</sup> | Multi-tiered<sup>4</sup> |
| Max concurrent workers per pool <sup>5</sup> | 1680 | 1890 | 2100 | 2520 | 3360 | 4200 | 8400 |
| Max concurrent external connections per pool <sup>6</sup> | 150 | 150 | 150 | 150 | 150 | 150 | 150 |
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 |
| Min/max elastic pool vCore choices per database | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16, 18 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 24 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 24, 32 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 24, 32, 40 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 24, 32, 40, 80 |
| Secondary replicas | 0-4 | 0-4 | 0-4 | 0-4 | 0-4 | 0-4 | 0-4 |
| Multi-AZ | Not supported | Not supported | Not supported | Not supported | Not supported | Not supported | Not supported |
| Read Scale-out | Yes | Yes | Yes | Yes | Yes | Yes | Yes |

<sup>1</sup> See [Resource management in dense elastic pools](elastic-pool-resource-management.md) for additional considerations.

<sup>2</sup> Hyperscale is a multi-tiered architecture with separate compute and storage components. Review [Hyperscale service tier](hyperscale-elastic-pool-overview.md#architecture) and [Hyperscale service tier](service-tier-hyperscale.md#distributed-functions-architecture) for more information.

<sup>3</sup> Besides local SSD IO, workloads use remote [page server](hyperscale-architecture.md#page-server) IO. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance), and [Data IO in resource utilization statistics](hyperscale-performance-diagnostics.md#data-io-in-resource-utilization-statistics).

<sup>4</sup> Latency is 1-2 ms for data on local compute replica SSD, which caches most used data pages. Higher latency for data retrieved from page servers.

<sup>5</sup> For the max concurrent workers for any individual database, see [Single database resource limits](resource-limits-vcore-single-databases.md). For example, if the elastic pool is using standard-series (Gen5) and the max vCore per database is set at 2, then the max concurrent workers value is 200.  If max vCore per database is set to 0.5, then the max concurrent workers value is 50 since on standard-series (Gen5) there are a max of 100 concurrent workers per vCore. For other max vCore settings per database that are less 1 vCore or less, the number of max concurrent workers is similarly rescaled.

<sup>6</sup> For more information on what counts as an external connection, see [External Connections](resource-limits-logical-server.md#external-connections).

## Hyperscale - premium-series

### Hyperscale - premium-series (part 1 of 3)

Compute sizes (service level objectives, or SLOs) for Hyperscale premium-series elastic pools follow the naming convention `HS_PRMS_` followed by the number of max vCores.

The following table covers these SLOs: `HS_PRMS_4`, `HS_PRMS_6`, `HS_PRMS_8`, `HS_PRMS_10`, `HS_PRMS_12`, and `HS_PRMS_14`:
 
| vCores | 4 | 6 | 8 | 10 | 12 | 14 |
|-:|-:|-:|-:|-:|-:|-:|
| Hardware | Premium-series | Premium-series | Premium-series | Premium-series | Premium-series | Premium-series |
| Memory (GB) | 20.8 | 31.1 | 41.5 | 51.9 | 62.3 | 72.7 |
| Max number DBs per pool <sup>1</sup> | 25 | 25 | 25 | 25 | 25 | 25 |
| Columnstore support | Yes | Yes | Yes | Yes | Yes | Yes |
| In-memory OLTP storage (GB) | N/A | N/A | N/A | N/A | N/A | N/A |
| Max data size per pool (TB) | 100 | 100 | 100 | 100 | 100 | 100 |
| `tempdb` max data size (GB) | 128 | 192 | 256 | 320 | 384 | 448 |
| Max local SSD IOPS per pool <sup>2</sup> | 18,000 | 27,000 | 36,000 | 45,000 | 54,000 | 63,000 |
| Max log rate per pool (MBps ) | 125 | 125 | 125 | 125 | 125 | 125 |
| Local read IO latency<sup>3</sup> | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms |
| Remote read IO latency<sup>3</sup> | 1-5 ms | 1-5 ms | 1-5 ms | 1-5 ms | 1-5 ms | 1-5 ms |
| Write IO latency<sup>3</sup> | 3-5 ms | 3-5 ms | 3-5 ms | 3-5 ms | 3-5 ms | 3-5 ms |
| Storage type | Multi-tiered<sup>4</sup> | Multi-tiered<sup>4</sup> | Multi-tiered<sup>4</sup> | Multi-tiered<sup>4</sup> | Multi-tiered<sup>4</sup> | Multi-tiered<sup>4</sup> |
| Max concurrent workers per pool <sup>5</sup> | 420 | 630 | 840 | 1050 | 1260 | 1470 |
| Max concurrent external connections per pool <sup>6</sup> | 42 | 63 | 84 | 105 | 126 | 147 |
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 |
| Min/max elastic pool vCore choices per database | 0, 0.25, 0.5, 1, 2, 4 | 0, 0.25, 0.5, 1, 2, 4, 6 | 0, 0.25, 0.5, 1, 2, 4, 6, 8 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14 |
| Secondary pool replicas | 0-4 | 0-4 | 0-4 | 0-4 | 0-4 | 0-4 |
| Multi-AZ | Not supported | Not supported | Not supported | Not supported | Not supported | Not supported |
| Read Scale-out | Yes | Yes | Yes | Yes | Yes | Yes |

<sup>1</sup> See [Resource management in dense elastic pools](elastic-pool-resource-management.md) for additional considerations.

<sup>2</sup> Hyperscale is a multi-tiered architecture with separate compute and storage components. Review [Hyperscale service tier](hyperscale-elastic-pool-overview.md#architecture) and [Hyperscale service tier](service-tier-hyperscale.md#distributed-functions-architecture) for more information.

<sup>3</sup> Besides local SSD IO, workloads use remote [page server](hyperscale-architecture.md#page-server) IO. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance), and [Data IO in resource utilization statistics](hyperscale-performance-diagnostics.md#data-io-in-resource-utilization-statistics).

<sup>4</sup> Latency is 1-2 ms for data on local compute replica SSD, which caches most used data pages. Higher latency for data retrieved from page servers.

<sup>5</sup> For the max concurrent workers for any individual database, see [Single database resource limits](resource-limits-vcore-single-databases.md). For example, if the elastic pool is using standard-series (Gen5) and the max vCore per database is set at 2, then the max concurrent workers value is 200.  If max vCore per database is set to 0.5, then the max concurrent workers value is 50 since on standard-series (Gen5) there are a max of 100 concurrent workers per vCore. For other max vCore settings per database that are less 1 vCore or less, the number of max concurrent workers is similarly rescaled.

<sup>6</sup> For more information on what counts as an external connection, see [External Connections](resource-limits-logical-server.md#external-connections).

### Hyperscale - premium-series (part 2 of 3)

Compute sizes (service level objectives, or SLOs) for Hyperscale premium-series elastic pools follow the naming convention `HS_PRMS_` followed by the number of max vCores.

The following table covers these SLOs: `HS_PRMS_16`, `HS_PRMS_18`, `HS_PRMS_20`, `HS_PRMS_24`, `HS_PRMS_32`, and `HS_PRMS_40`:
 
| vCores | 16 | 18 | 20 | 24 | 32 | 40 | 
|:-|-:|-:|-:|-:|-:|-:|
| Hardware | Premium-series | Premium-series | Premium-series | Premium-series | Premium-series | Premium-series | 
| Max number DBs per pool <sup>1</sup> | 25 | 25 | 25 | 25 | 25 | 25 | 
| Memory (GB) | 83 | 93.4 | 103.8 | 124.6 | 166.1 | 207.6 | 
| Columnstore support | Yes | Yes | Yes | Yes | Yes | Yes |
| In-memory OLTP storage (GB) | N/A | N/A | N/A | N/A | N/A | N/A | 
| Max data size per pool (TB) | 100 | 100 | 100 | 100 | 100 | 100 | 
| `tempdb` max data size (GB) | 512 | 576 | 640 | 768 | 1024 | 1280 | 
| Max local SSD IOPS per pool <sup>2</sup> | 72,000 | 81,000 | 90,000 | 108,000 | 144,000 | 180,000 | 
| Max log rate per pool (MBps) | 131.25 | 131.25 | 131.25 | 131.25 | 131.25 | 131.25 | 
| Local read IO latency<sup>3</sup> | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 
| Remote read IO latency<sup>3</sup> | 1-5 ms | 1-5 ms | 1-5 ms | 1-5 ms | 1-5 ms | 1-5 ms | 
| Write IO latency<sup>3</sup> | 3-5 ms | 3-5 ms | 3-5 ms | 3-5 ms | 3-5 ms | 3-5 ms | 
| Storage type | Multi-tiered<sup>4</sup> | Multi-tiered<sup>4</sup> | Multi-tiered<sup>4</sup> | Multi-tiered<sup>4</sup> | Multi-tiered<sup>4</sup> | Multi-tiered<sup>4</sup> |
| Max concurrent workers per pool <sup>5</sup> | 1680 | 1890 | 2100 | 2520 | 3360 | 4200 | 
| Max concurrent external connections per pool <sup>6</sup> | 150 | 150 | 150 | 150 | 150 | 150 | 
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 | 
| Min/max elastic pool vCore choices per database | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16, 18 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 24 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 24, 32 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 24, 32, 40 |
| Secondary replicas | 0-4 | 0-4 | 0-4 | 0-4 | 0-4 | 0-4 | 
| Multi-AZ | Not supported | Not supported | Not supported | Not supported | Not supported | Not supported | 
| Read Scale-out | Yes | Yes | Yes | Yes | Yes | Yes | 

<sup>1</sup> See [Resource management in dense elastic pools](elastic-pool-resource-management.md) for additional considerations.

<sup>2</sup> Hyperscale is a multi-tiered architecture with separate compute and storage components. Review [Hyperscale service tier](hyperscale-elastic-pool-overview.md#architecture) and [Hyperscale service tier](service-tier-hyperscale.md#distributed-functions-architecture) for more information.

<sup>3</sup> Besides local SSD IO, workloads use remote [page server](hyperscale-architecture.md#page-server) IO. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance), and [Data IO in resource utilization statistics](hyperscale-performance-diagnostics.md#data-io-in-resource-utilization-statistics).

<sup>4</sup> Latency is 1-2 ms for data on local compute replica SSD, which caches most used data pages. Higher latency for data retrieved from page servers.

<sup>5</sup> For the max concurrent workers for any individual database, see [Single database resource limits](resource-limits-vcore-single-databases.md). For example, if the elastic pool is using standard-series (Gen5) and the max vCore per database is set at 2, then the max concurrent workers value is 200.  If max vCore per database is set to 0.5, then the max concurrent workers value is 50 since on standard-series (Gen5) there are a max of 100 concurrent workers per vCore. For other max vCore settings per database that are less 1 vCore or less, the number of max concurrent workers is similarly rescaled.

<sup>6</sup> For more information on what counts as an external connection, see [External Connections](resource-limits-logical-server.md#external-connections).

### Hyperscale - premium-series (part 3 of 3)

Compute sizes (service level objectives, or SLOs) for Hyperscale premium-series elastic pools follow the naming convention `HS_PRMS_` followed by the number of max vCores.

The following table covers these SLOs: `HS_PRMS_64`, `HS_PRMS_80`, `HS_PRMS_128`:
 
| vCores | 64 | 80 | 128 |
|:-|-:|-:|-:|-:|-:|-:|-:|
| Hardware | Premium-series | Premium-series | Premium-series | 
| Max number DBs per pool <sup>1</sup> | 25 | 25 | 25 | 
| Memory (GB) | 332.2 | 415.2 | 625 |
| Columnstore support | Yes | Yes | Yes | 
| In-memory OLTP storage (GB) | N/A | N/A |
| Max data size per pool (TB) | 100 | 100 | 100 | 
| `tempdb` max data size (GB) | 2,048 | 2,560 | 4,096 |
| Max local SSD IOPS per pool <sup>2</sup> | 288,000 | 360,000 | 409,600 |
| Max log rate per pool (MBps) | 125 | 125 | 125 | 
| Local read IO latency<sup>3</sup> | 1-2 ms | 1-2 ms | 1-2 ms | 
| Remote read IO latency<sup>3</sup> | 1-5 ms | 1-5 ms | 1-5 ms |
| Write IO latency<sup>3</sup> | 3-5 ms | 3-5 ms | 3-5 ms | 
| Storage type | Multi-tiered<sup>4</sup> | Multi-tiered<sup>4</sup> | Multi-tiered<sup>4</sup> | 
| Max concurrent workers per pool <sup>5</sup> | 6,720 | 8,400 | 13,440 |
| Max concurrent external connections per pool <sup>6</sup> | 150 | 150 | 150 |
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 
| Min/max elastic pool vCore choices per database | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 24, 32, 40, 64 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 24, 32, 40, 80 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 24, 32, 40, 64, 80, 128 |
| Secondary replicas | 0-4 | 0-4 | 0-4 | 
| Multi-AZ | Not supported | Not supported | Not supported |  
| Read Scale-out | Yes | Yes | Yes |  

<sup>1</sup> See [Resource management in dense elastic pools](elastic-pool-resource-management.md) for additional considerations.

<sup>2</sup> Hyperscale is a multi-tiered architecture with separate compute and storage components. Review [Hyperscale service tier](hyperscale-elastic-pool-overview.md#architecture) and [Hyperscale service tier](service-tier-hyperscale.md#distributed-functions-architecture) for more information.

<sup>3</sup> Besides local SSD IO, workloads use remote [page server](hyperscale-architecture.md#page-server) IO. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance), and [Data IO in resource utilization statistics](hyperscale-performance-diagnostics.md#data-io-in-resource-utilization-statistics).

<sup>4</sup> Latency is 1-2 ms for data on local compute replica SSD, which caches most used data pages. Higher latency for data retrieved from page servers.

<sup>5</sup> For the max concurrent workers for any individual database, see [Single database resource limits](resource-limits-vcore-single-databases.md). For example, if the elastic pool is using standard-series (Gen5) and the max vCore per database is set at 2, then the max concurrent workers value is 200.  If max vCore per database is set to 0.5, then the max concurrent workers value is 50 since on standard-series (Gen5) there are a max of 100 concurrent workers per vCore. For other max vCore settings per database that are less 1 vCore or less, the number of max concurrent workers is similarly rescaled.

<sup>6</sup> For more information on what counts as an external connection, see [External Connections](resource-limits-logical-server.md#external-connections).

## Hyperscale - premium-series memory optimized

### Hyperscale - premium-series memory optimized (part 1 of 3)

Compute sizes (service level objectives, or SLOs) for Hyperscale premium-series memory optimized elastic pools follow the naming convention `HS_MOPRMS_` followed by the number of max vCores.

The following table covers these SLOs: `HS_MOPRMS_4`, `HS_MOPRMS_6`, `HS_MOPRMS_8`, `HS_MOPRMS_10`, `HS_MOPRMS_12`, and `HS_MOPRMS_14`:
 
| vCores | 4 | 6 | 8 | 10 | 12 | 14 |
|-:|-:|-:|-:|-:|-:|-:|
| Hardware | Premium-series | Premium-series | Premium-series | Premium-series | Premium-series | Premium-series |
| Memory (GB) | 41.5 | 62.3 | 83 | 103.8 | 124.5 | 145.3 |
| Max number DBs per pool <sup>1</sup> | 25 | 25 | 25 | 25 | 25 | 25 |
| Columnstore support | Yes | Yes | Yes | Yes | Yes | Yes |
| In-memory OLTP storage (GB) | N/A | N/A | N/A | N/A | N/A | N/A |
| Max data size per pool (TB) | 100 | 100 | 100 | 100 | 100 | 100 |
| `tempdb` max data size (GB) | 128 | 192 | 256 | 320 | 384 | 448 |
| Max local SSD IOPS per pool <sup>2</sup> | 23,040 | 34,560 | 46,080 | 57,600 | 69,120 | 80,640 |
| Max log rate per pool (MBps ) | 125 | 125 | 125 | 125 | 125 | 125 |
| Local read IO latency<sup>3</sup> | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms |
| Remote read IO latency<sup>3</sup> | 1-5 ms | 1-5 ms | 1-5 ms | 1-5 ms | 1-5 ms | 1-5 ms |
| Write IO latency<sup>3</sup> | 3-5 ms | 3-5 ms | 3-5 ms | 3-5 ms | 3-5 ms | 3-5 ms |
| Storage type | Multi-tiered<sup>4</sup> | Multi-tiered<sup>4</sup> | Multi-tiered<sup>4</sup> | Multi-tiered<sup>4</sup> | Multi-tiered<sup>4</sup> | Multi-tiered<sup>4</sup> |
| Max concurrent workers per pool <sup>5</sup> | 420 | 630 | 840 | 1050 | 1260 | 1470 |
| Max concurrent external connections per pool <sup>6</sup> | 42 | 63 | 84 | 105 | 126 | 147 |
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 |
| Min/max elastic pool vCore choices per database | 0, 0.25, 0.5, 1, 2, 4 | 0, 0.25, 0.5, 1, 2, 4, 6 | 0, 0.25, 0.5, 1, 2, 4, 6, 8 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14 |
| Secondary pool replicas | 0-4 | 0-4 | 0-4 | 0-4 | 0-4 | 0-4 |
| Multi-AZ | Not supported | Not supported | Not supported | Not supported | Not supported | Not supported |
| Read Scale-out | Yes | Yes | Yes | Yes | Yes | Yes |

<sup>1</sup> See [Resource management in dense elastic pools](elastic-pool-resource-management.md) for additional considerations.

<sup>2</sup> Hyperscale is a multi-tiered architecture with separate compute and storage components. Review [Hyperscale service tier](hyperscale-elastic-pool-overview.md#architecture) and [Hyperscale service tier](service-tier-hyperscale.md#distributed-functions-architecture) for more information.

<sup>3</sup> Besides local SSD IO, workloads use remote [page server](hyperscale-architecture.md#page-server) IO. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance), and [Data IO in resource utilization statistics](hyperscale-performance-diagnostics.md#data-io-in-resource-utilization-statistics).

<sup>4</sup> Latency is 1-2 ms for data on local compute replica SSD, which caches most used data pages. Higher latency for data retrieved from page servers.

<sup>5</sup> For the max concurrent workers for any individual database, see [Single database resource limits](resource-limits-vcore-single-databases.md). For example, if the elastic pool is using standard-series (Gen5) and the max vCore per database is set at 2, then the max concurrent workers value is 200.  If max vCore per database is set to 0.5, then the max concurrent workers value is 50 since on standard-series (Gen5) there are a max of 100 concurrent workers per vCore. For other max vCore settings per database that are less 1 vCore or less, the number of max concurrent workers is similarly rescaled.

<sup>6</sup> For more information on what counts as an external connection, see [External Connections](resource-limits-logical-server.md#external-connections).

### Hyperscale - premium-series memory-optimized (part 2 of 3)

Compute sizes (service level objectives, or SLOs) for Hyperscale premium-series memory optimized elastic pools follow the naming convention `HS_MOPRMS_` followed by the number of max vCores.

The following table covers these SLOs: `HS_MOPRMS_16`, `HS_MOPRMS_18`, `HS_MOPRMS_20`, `HS_MOPRMS_24`, `HS_MOPRMS_32`, and `HS_MOPRMS_40`:

| vCores | 16 | 18 | 20 | 24 | 32 | 40 | 
|:-|-:|-:|-:|-:|-:|-:|
| Hardware | Premium-series | Premium-series | Premium-series | Premium-series | Premium-series | Premium-series | 
| Max number DBs per pool <sup>1</sup> | 25 | 25 | 25 | 25 | 25 | 25 | 
| Memory (GB) | 166.1 | 186.8 | 207.6 |249.1 | 332.1 | 415.2 | 
| Columnstore support | Yes | Yes | Yes | Yes | Yes | Yes |
| In-memory OLTP storage (GB) | N/A | N/A | N/A | N/A | N/A | N/A | 
| Max data size per pool (TB) | 100 | 100 | 100 | 100 | 100 | 100 | 
| `tempdb` max data size (GB) | 512 | 576 | 640 | 768 | 1024 | 1280 | 
| Max local SSD IOPS per pool <sup>2</sup> | 92,160 | 103,680 | 115,200 | 138,240 | 184,320 | 230,400 | 
| Max log rate per pool (MBps) | 125 | 125 | 125 | 125 | 125 | 125 | 
| Local read IO latency<sup>3</sup> | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 1-2 ms | 
| Remote read IO latency<sup>3</sup> | 1-5 ms | 1-5 ms | 1-5 ms | 1-5 ms | 1-5 ms | 1-5 ms | 
| Write IO latency<sup>3</sup> | 3-5 ms | 3-5 ms | 3-5 ms | 3-5 ms | 3-5 ms | 3-5 ms | 
| Storage type | Multi-tiered<sup>4</sup> | Multi-tiered<sup>4</sup> | Multi-tiered<sup>4</sup> | Multi-tiered<sup>4</sup> | Multi-tiered<sup>4</sup> | Multi-tiered<sup>4</sup> |
| Max concurrent workers per pool <sup>5</sup> | 1680 | 1890 | 2100 | 2520 | 3360 | 4200 | 
| Max concurrent external connections per pool <sup>6</sup> | 150 | 150 | 150 | 150 | 150 | 150 | 
| Max concurrent sessions | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 | 30,000 | 
| Min/max elastic pool vCore choices per database | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16, 18 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 24 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 24, 32 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 24, 32, 40 |
| Secondary replicas | 0-4 | 0-4 | 0-4 | 0-4 | 0-4 | 0-4 |
| Multi-AZ | Not supported | Not supported | Not supported | Not supported | Not supported | Not supported |
| Read Scale-out | Yes | Yes | Yes | Yes | Yes | Yes |

<sup>1</sup> See [Resource management in dense elastic pools](elastic-pool-resource-management.md) for additional considerations.

<sup>2</sup> Hyperscale is a multi-tiered architecture with separate compute and storage components. Review [Hyperscale service tier](hyperscale-elastic-pool-overview.md#architecture) and [Hyperscale service tier](service-tier-hyperscale.md#distributed-functions-architecture) for more information.

<sup>3</sup> Besides local SSD IO, workloads use remote [page server](hyperscale-architecture.md#page-server) IO. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance), and [Data IO in resource utilization statistics](hyperscale-performance-diagnostics.md#data-io-in-resource-utilization-statistics).

<sup>4</sup> Latency is 1-2 ms for data on local compute replica SSD, which caches most used data pages. Higher latency for data retrieved from page servers.

<sup>5</sup> For the max concurrent workers for any individual database, see [Single database resource limits](resource-limits-vcore-single-databases.md). For example, if the elastic pool is using standard-series (Gen5) and the max vCore per database is set at 2, then the max concurrent workers value is 200.  If max vCore per database is set to 0.5, then the max concurrent workers value is 50 since on standard-series (Gen5) there are a max of 100 concurrent workers per vCore. For other max vCore settings per database that are less 1 vCore or less, the number of max concurrent workers is similarly rescaled.

<sup>6</sup> For more information on what counts as an external connection, see [External Connections](resource-limits-logical-server.md#external-connections).

### Hyperscale - premium-series memory-optimized (part 3 of 3)

Compute sizes (service level objectives, or SLOs) for Hyperscale premium-series memory optimized elastic pools follow the naming convention `HS_MOPRMS_` followed by the number of max vCores.

The following table covers these SLOs: `HS_MOPRMS_64` and `HS_MOPRMS_80`:

| vCores | 64 | 80 | 
|:-|-:|-:|
| Hardware | Premium-series | Premium-series | 
| Max number DBs per pool <sup>1</sup> | 25 | 25 | 
| Memory (GB) | 664.4 | 830.5 | 
| Columnstore support | Yes | Yes | 
| In-memory OLTP storage (GB) | N/A | N/A |
| Max data size per pool (TB) | 100 | 100 |
| `tempdb` max data size (GB) | 2,048 | 2,560 | 
| Max local SSD IOPS per pool <sup>2</sup> | 368,640 | 409,600 |
| Max log rate per pool (MBps) | 125 | 125 | 
| Local read IO latency<sup>3</sup> | 1-2 ms | 1-2 ms | 
| Remote read IO latency<sup>3</sup> | 1-5 ms | 1-5 ms | 
| Write IO latency<sup>3</sup> | 3-5 ms | 3-5 ms | 
| Storage type | Multi-tiered<sup>4</sup> | Multi-tiered<sup>4</sup> | 
| Max concurrent workers per pool <sup>5</sup> | 6,720 | 8,400 | 
| Max concurrent external connections per pool <sup>6</sup> | 150 | 150 | 
| Max concurrent sessions | 30,000 | 30,000 | 
| Min/max elastic pool vCore choices per database | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 24, 32, 40, 64 | 0, 0.25, 0.5, 1, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 24, 32, 40, 80 | 
| Secondary replicas | 0-4 | 0-4 | 
| Multi-AZ | Not supported | Not supported | 
| Read Scale-out | Yes | Yes | 

<sup>1</sup> See [Resource management in dense elastic pools](elastic-pool-resource-management.md) for additional considerations.

<sup>2</sup> Hyperscale is a multi-tiered architecture with separate compute and storage components. Review [Hyperscale service tier](hyperscale-elastic-pool-overview.md#architecture) and [Hyperscale service tier](service-tier-hyperscale.md#distributed-functions-architecture) for more information.

<sup>3</sup> Besides local SSD IO, workloads use remote [page server](hyperscale-architecture.md#page-server) IO. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance), and [Data IO in resource utilization statistics](hyperscale-performance-diagnostics.md#data-io-in-resource-utilization-statistics).

<sup>4</sup> Latency is 1-2 ms for data on local compute replica SSD, which caches most used data pages. Higher latency for data retrieved from page servers.

<sup>5</sup> For the max concurrent workers for any individual database, see [Single database resource limits](resource-limits-vcore-single-databases.md). For example, if the elastic pool is using standard-series (Gen5) and the max vCore per database is set at 2, then the max concurrent workers value is 200.  If max vCore per database is set to 0.5, then the max concurrent workers value is 50 since on standard-series (Gen5) there are a max of 100 concurrent workers per vCore. For other max vCore settings per database that are less 1 vCore or less, the number of max concurrent workers is similarly rescaled.

<sup>6</sup> For more information on what counts as an external connection, see [External Connections](resource-limits-logical-server.md#external-connections).

## Database properties for pooled databases

For each elastic pool, you can optionally specify per database minimum and maximum vCores to modify resource consumption patterns within the pool. Specified min and max values apply to all databases in the pool. Customizing min and max vCores for individual databases in the pool isn't supported. 

You can also set maximum storage per database, for example to prevent a database from consuming all pool storage. This setting can be configured independently for each database.

The following table describes per database properties for pooled databases. 

| Property | Description |
|:--- |:--- |
| Max vCores per database |The maximum number of vCores that any database in the pool may use, if available based on utilization by other databases in the pool. Max vCores per database isn't a resource guarantee for a database. If the workload in each database doesn't need all available pool resources to perform adequately, consider setting max vCores per database to prevent a single database from monopolizing pool resources. Some degree of over-committing is expected since the pool generally assumes hot and cold usage patterns for databases, where all databases aren't simultaneously peaking. |
| Min vCores per database |The minimum number of vCores reserved for any database in the pool. Consider setting a min vCores per database when you want to guarantee resource availability for each database regardless of resource consumption by other databases in the pool. The min vCores per database may be set to 0, and is also the default value. This property is set to anywhere between 0 and the average vCores utilization per database.|
| Max storage per database |The maximum database size set by the user for a database in a pool. Pooled databases share allocated pool storage, so the size a database can reach is limited to the smaller of remaining pool storage and maximum database size. Maximum database size refers to the maximum size of the data files and doesn't include the space used by the log file. |

> [!IMPORTANT]
> Because resources in an elastic pool are finite, setting min vCores per database to a value greater than 0 implicitly limits resource utilization by each database. If, at a point in time, most databases in a pool are idle, resources reserved to satisfy the min vCores guarantee are not available to databases active at that point in time.
>
> Additionally, setting min vCores per database to a value greater than 0 implicitly limits the number of databases that can be added to the pool. For example, if you set the min vCores to 2 in a 20 vCore pool, it means that you will not be able to add more than 10 databases to the pool, because 2 vCores are reserved for each database.

Even though the per database properties are expressed in vCores, they also govern consumption of other resource types, such as data IO, log IO, buffer pool memory, and worker threads. As you adjust min and max per database vCore values, reservations and limits for all resource types are adjusted proportionally.

Min and max per database vCore values apply to resource consumption by user workloads, but not to resource consumption by internal processes. For example, for a database with a per database max vCores set to half of the pool vCores, user workload can't consume more than one half of the buffer pool memory. However, this database can still take advantage of pages in the buffer pool that were loaded by internal processes. For more information, see [Resource consumption by user workloads and internal processes](resource-limits-logical-server.md#resource-consumption-by-user-workloads-and-internal-processes).

> [!NOTE]
> The resource limits of individual databases in elastic pools are generally the same as for single databases outside of pools that have the same compute size (service objective). For example, the max concurrent workers for an GP_S_Gen5_10 database is 750 workers. So, the max concurrent workers for a database in a GP_Gen5_10 pool is also 750 workers. Note, the total number of concurrent workers in GP_Gen5_10 pool is 1050. For the max concurrent workers for any individual database, see [Single database resource limits](resource-limits-vcore-single-databases.md?view=azuresql&preserve-view=true).

## Previously available hardware

This section includes details on previously available hardware.

- Gen4 hardware has been retired and isn't available for provisioning, upscaling, or downscaling. Migrate your database to a supported hardware generation for a wider range of vCore and storage scalability, accelerated networking, best IO performance, and minimal latency. For more information, see [Support has ended for Gen 4 hardware on Azure SQL Database](https://azure.microsoft.com/updates/support-has-ended-for-gen-4-hardware-on-azure-sql-database/).

[!INCLUDE[identify-gen4-hardware](../includes/identify-gen4-hardware.md)]

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

## Related content

- For vCore resource limits for a single database, see [resource limits for single databases using the vCore purchasing model](resource-limits-vcore-single-databases.md)
- For DTU resource limits for a single database, see [resource limits for single databases using the DTU purchasing model](resource-limits-dtu-single-databases.md)
- For DTU resource limits for elastic pools, see [resource limits for elastic pools using the DTU purchasing model](resource-limits-dtu-elastic-pools.md)
- For resource limits for managed instances, see [managed instance resource limits](../managed-instance/resource-limits.md).
- For information about general Azure limits, see [Azure subscription and service limits, quotas, and constraints](/azure/azure-resource-manager/management/azure-subscription-service-limits).
- For information about resource limits on a logical SQL server, see [overview of resource limits on a logical SQL server](resource-limits-logical-server.md) for information about limits at the server and subscription levels.
