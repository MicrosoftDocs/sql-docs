---
title: Elastic pool vCore resource limits
description: This page describes some common vCore resource limits for elastic pools in Azure SQL Database.
author: dimitri-furman
ms.author: dfurman
ms.reviewer: wiassaf, mathoma
ms.date: 11/04/2022
ms.service: sql-database
ms.subservice: elastic-pools
ms.topic: reference
ms.custom:
  - "sqldbrb=1"
  - "references_regions"

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

* [Transact-SQL](elastic-pool-scale.md) via [ALTER DATABASE](/sql/t-sql/statements/alter-database-transact-sql#overview-sql-database)
* [Azure portal](elastic-pool-manage.md#azure-portal)
* [PowerShell](elastic-pool-manage.md#powershell)
* [Azure CLI](elastic-pool-manage.md#azure-cli)
* [REST API](elastic-pool-manage.md#rest-api)

> [!IMPORTANT]
> For scaling guidance and considerations, see [Scale an elastic pool](elastic-pool-scale.md).

If all vCores of an elastic pool are busy, then each database in the pool receives an equal amount of compute resources to process queries. Azure SQL Database provides resource sharing fairness between databases by ensuring equal slices of compute time. Elastic pool resource sharing fairness is in addition to any amount of resource otherwise guaranteed to each database when the vCore min per database is set to a non-zero value.


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

> [!IMPORTANT]
> New Gen4 databases are no longer supported in the Australia East or Brazil South regions.

## General purpose - provisioned compute - standard-series (Gen5)

### General purpose service tier: standard-series (Gen5) (part 1 of 3)

|Compute size (service objective)|GP_Gen5_2|GP_Gen5_4|GP_Gen5_6|GP_Gen5_8|GP_Gen5_10|
|:--- | --: |--: |--: |--: |---: | 
|Compute generation|Gen5|Gen5|Gen5|Gen5|Gen5|
|vCores|2|4|6|8|10|
|Memory (GB)|10.4|20.8|31.1|41.5|51.9|
|Max number DBs per pool <sup>1</sup>|100|200|500|500|500|
|Columnstore support|Yes|Yes|Yes|Yes|Yes|
|In-memory OLTP storage (GB)|N/A|N/A|N/A|N/A|N/A|
|Max data size (GB)|512|756|1536|2048|2048|
|Max log size (GB) <sup>2</sup>|154|227|461|461|461|
|TempDB max data size (GB)|64|128|192|256|320|
|Storage type|Premium (Remote) Storage|Premium (Remote) Storage|Premium (Remote) Storage|Premium (Remote) Storage|Premium (Remote) Storage|
|Read IO latency (approximate)|5-10 ms|5-10 ms|5-10 ms|5-10 ms|5-10 ms|
|Write IO latency (approximate)|5-7 ms|5-7 ms|5-7 ms|5-7 ms|5-7 ms|
|Max data IOPS per pool <sup>3</sup>|1,400|2,800|4,200|5,600|7,000|
|Max log rate per pool (MBps)|12|24|36|48|60|
|Max concurrent workers per pool <sup>4</sup>|210|420|630|840|1050|
|Max concurrent logins per pool <sup>4</sup>|210|420|630|840|1050|
|Max concurrent external connections per pool <sup>5</sup>|21|42|63|84|105|
|Max concurrent sessions|30,000|30,000|30,000|30,000|30,000|
|Min/max elastic pool vCore choices per database|0, 0.25, 0.5, 1, 2|0, 0.25, 0.5, 1...4|0, 0.25, 0.5, 1...6|0, 0.25, 0.5, 1...8|0, 0.25, 0.5, 1...10|
|Number of replicas|1|1|1|1|1|
|Multi-AZ|Yes|Yes|Yes|Yes|Yes|
|Read Scale-out|N/A|N/A|N/A|N/A|N/A|
|Included backup storage|1X DB size|1X DB size|1X DB size|1X DB size|1X DB size|

<sup>1</sup> See [Resource management in dense elastic pools](elastic-pool-resource-management.md) for additional considerations.

<sup>2</sup> For documented max data size values. Reducing max data size reduces max log size proportionally.

<sup>3</sup> The maximum value for IO sizes ranging between 8 KB and 64 KB. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance).

<sup>4</sup> For the max concurrent workers for any individual database, see [Single database resource limits](resource-limits-vcore-single-databases.md). For example, if the elastic pool is using Gen5 and the max vCore per database is set at 2, then the max concurrent workers value is 200.  If max vCore per database is set to 0.5, then the max concurrent workers value is 50 since on Gen5 there are a max of 100 concurrent workers per vCore. For other max vCore settings per database that are less 1 vCore or less, the number of max concurrent workers is similarly rescaled.

<sup>5</sup> See [External Connections](resource-limits-logical-server.md#external-connections) for additional details on what counts as an external connection.

### General purpose service tier: standard-series (Gen5) (part 2 of 3)

|Compute size (service objective)|GP_Gen5_12|GP_Gen5_14|GP_Gen5_16|GP_Gen5_18|GP_Gen5_20|
|:--- | --: |--: |--: |--: |--: |
|Compute generation|Gen5|Gen5|Gen5|en5|Gen5|
|vCores|12|14|16|18|20|
|Memory (GB)|62.3|72.7|83|93.4|103.8|
|Max number DBs per pool <sup>1</sup>|500|500|500|500|500|
|Columnstore support|Yes|Yes|Yes|Yes|Yes|
|In-memory OLTP storage (GB)|N/A|N/A|N/A|N/A|N/A|
|Max data size (GB)|2048|2048|2048|3072|3072|
|Max log size (GB) <sup>2</sup>|614|614|614|922|922|
|TempDB max data size (GB)|384|448|512|576|640|
|Storage type|Premium (Remote) Storage|Premium (Remote) Storage|Premium (Remote) Storage|Premium (Remote) Storage|Premium (Remote) Storage|
|Read IO latency (approximate)|5-10 ms|5-10 ms|5-10 ms|5-10 ms|5-10 ms|
|Write IO latency (approximate)|5-7 ms|5-7 ms|5-7 ms|5-7 ms|5-7 ms|
|Max data IOPS per pool <sup>3</sup>|8,400|9,800|11,200|12,600|14,000|
|Max log rate per pool (MBps)|62.5|62.5|62.5|62.5|62.5|
|Max concurrent workers per pool <sup>4</sup>|1260|1470|1680|1890|2100|
|Max concurrent logins per pool <sup>4</sup>|1260|1470|1680|1890|2100|
|Max concurrent external connections per pool <sup>5</sup>|126|147|150|150|150|
|Max concurrent sessions|30,000|30,000|30,000|30,000|30,000|
|Min/max elastic pool vCore choices per database|0, 0.25, 0.5, 1...12|0, 0.25, 0.5, 1...14|0, 0.25, 0.5, 1...16|0, 0.25, 0.5, 1...18|0, 0.25, 0.5, 1...20|
|Number of replicas|1|1|1|1|1|
|Multi-AZ|Yes|Yes|Yes|Yes|Yes|
|Read Scale-out|N/A|N/A|N/A|N/A|N/A|
|Included backup storage|1X DB size|1X DB size|1X DB size|1X DB size|1X DB size|

<sup>1</sup> See [Resource management in dense elastic pools](elastic-pool-resource-management.md) for additional considerations.

<sup>2</sup> For documented max data size values. Reducing max data size reduces max log size proportionally.

<sup>3</sup> The maximum value for IO sizes ranging between 8 KB and 64 KB. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance).

<sup>4</sup> For the max concurrent workers for any individual database, see [Single database resource limits](resource-limits-vcore-single-databases.md). For example, if the elastic pool is using standard-series (Gen5) and the max vCore per database is set at 2, then the max concurrent workers value is 200.  If max vCore per database is set to 0.5, then the max concurrent workers value is 50 since on standard-series (Gen5) there are a max of 100 concurrent workers per vCore. For other max vCore settings per database that are less 1 vCore or less, the number of max concurrent workers is similarly rescaled.

<sup>5</sup> See [External Connections](resource-limits-logical-server.md#external-connections) for additional details on what counts as an external connection.

### General purpose service tier: standard-series (Gen5) (part 3 of 3)

|Compute size (service objective)|GP_Gen5_24|GP_Gen5_32|GP_Gen5_40|GP_Gen5_80|GP_Gen5_128|
|:--- | --: |--: | --: |--: |--: |
|Compute generation|Gen5|Gen5|Gen5|Gen5|Gen5|
|vCores|24|32|40|80|128<sup>6</sup>|
|Memory (GB)|124.6|166.1|207.6|415.2|625|
|Max number DBs per pool <sup>1</sup>|500|500|500|500|500|
|Columnstore support|Yes|Yes|Yes|Yes|Yes| 
|In-memory OLTP storage (GB)|N/A|N/A|N/A|N/A|N/A|
|Max data size (GB)|3072|4096|4096|4096|4096|
|Max log size (GB) <sup>2</sup>|922|1229|1229|1229|1229|
|TempDB max data size (GB)|768|1024|1280|2560|2560|
|Storage type|Premium (Remote) Storage|Premium (Remote) Storage|Premium (Remote) Storage|Premium (Remote) Storage|Premium (Remote) Storage|
|Read IO latency (approximate)|5-10 ms|5-10 ms|5-10 ms|5-10 ms|5-10 ms|
|Write IO latency (approximate)|5-7 ms|5-7 ms|5-7 ms|5-7 ms|5-7 ms|
|Max data IOPS per pool <sup>3</sup> |16,800|22,400|28,000|32,000|40,000|
|Max log rate per pool (MBps)|62.5|62.5|62.5|62.5|62.5|
|Max concurrent workers per pool <sup>4</sup>|2520|3360|4200|8400|13,440|
|Max concurrent logins per pool <sup>4</sup>|2520|3360|4200|8400|8400|
|Max concurrent external connections per pool <sup>5</sup>|150|150|150|150|150|
|Max concurrent sessions|30,000|30,000|30,000|30,000|30,000|
|Min/max elastic pool vCore choices per database|0, 0.25, 0.5, 1...20, 24|0, 0.25, 0.5, 1...20, 24, 32|0, 0.25, 0.5, 1...16, 24, 32, 40|0, 0.25, 0.5, 1...16, 24, 32, 40, 80|0, 0.25, 0.5, 1...16, 24, 32, 40, 80, 128|
|Number of replicas|1|1|1|1|1|
|Multi-AZ|Yes|Yes|Yes|Yes|Yes|
|Read Scale-out|N/A|N/A|N/A|N/A|N/A|
|Included backup storage|1X DB size|1X DB size|1X DB size|1X DB size|1X DB size|

<sup>1</sup> See [Resource management in dense elastic pools](elastic-pool-resource-management.md) for additional considerations.

<sup>2</sup> For documented max data size values. Reducing max data size reduces max log size proportionally.

<sup>3</sup> The maximum value for IO sizes ranging between 8 KB and 64 KB. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance).

<sup>4</sup> For the max concurrent workers for any individual database, see [Single database resource limits](resource-limits-vcore-single-databases.md). For example, if the elastic pool is using standard-series (Gen5) and the max vCore per database is set at 2, then the max concurrent workers value is 200.  If max vCore per database is set to 0.5, then the max concurrent workers value is 50 since on standard-series (Gen5) there are a max of 100 concurrent workers per vCore. For other max vCore settings per database that are less 1 vCore or less, the number of max concurrent workers is similarly rescaled.

<sup>5</sup> See [External Connections](resource-limits-logical-server.md#external-connections) for additional details on what counts as an external connection.

<sup>6</sup> The 128 vCore offering is currently in public preview.

## General purpose - provisioned compute - Fsv2-series

### Fsv2-series Hardware (part 1 of 2)

|Compute size (service objective)|GP_Fsv2_8|GP_Fsv2_10|GP_Fsv2_12|GP_Fsv2_14| GP_Fsv2_16|
|:---| ---:|---:|---:|---:|---:|
|Compute generation|Fsv2-series|Fsv2-series|Fsv2-series|Fsv2-series|Fsv2-series|
|vCores|8|10|12|14|16|
|Memory (GB)|15.1|18.9|22.7|26.5|30.2|
|Max number DBs per pool <sup>1</sup>|500|500|500|500|500|
|Columnstore support|Yes|Yes|Yes|Yes|Yes|
|In-memory OLTP storage (GB)|N/A|N/A|N/A|N/A|N/A|
|Max data size (GB)|1024|1024|1024|1024|1536|
|Max log size (GB) <sup>2</sup>|336|336|336|336|512|
|TempDB max data size (GB)|37|46|56|65|74|
|Storage type|Remote SSD|Remote SSD|Remote SSD|Remote SSD|Remote SSD|
|Read IO latency (approximate)|5-10 ms|5-10 ms|5-10 ms|5-10 ms|5-10 ms|
|Write IO latency (approximate)|5-7 ms|5-7 ms|5-7 ms|5-7 ms|5-7 ms|
|Max data IOPS per pool <sup>3</sup>|5,600|7,000|8,400|9,800|11,200|
|Max log rate per pool (MBps)|48|60|62.5|62.5|62.5|
|Max concurrent workers per pool <sup>4</sup>|400|500|600|700|800|
|Max concurrent logins per pool <sup>4</sup>|800|1000|1200|1400|1600|
|Max concurrent external connections per pool <sup>5</sup>|40|50|60|70|80|
|Max concurrent sessions|30,000|30,000|30,000|30,000|30,000|
|Min/max elastic pool vCore choices per database|0-8|0-10|0-12|0-14|0-16|
|Number of replicas|1|1|1|1|1|
|Multi-AZ|N/A|N/A|N/A|N/A|N/A|
|Read Scale-out|N/A|N/A|N/A|N/A|N/A|
|Included backup storage|1X DB size|1X DB size|1X DB size|1X DB size|1X DB size|

<sup>1</sup> See [Resource management in dense elastic pools](elastic-pool-resource-management.md) for additional considerations.

<sup>2</sup> For documented max data size values. Reducing max data size reduces max log size proportionally.

<sup>3</sup> The maximum value for IO sizes ranging between 8 KB and 64 KB. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance).

<sup>4</sup> For the max concurrent workers for any individual database, see [Single database resource limits](resource-limits-vcore-single-databases.md). For example, if the elastic pool is using standard-series (Gen5) and the max vCore per database is set at 2, then the max concurrent workers value is 200.  If max vCore per database is set to 0.5, then the max concurrent workers value is 50 since on standard-series (Gen5) there are a max of 100 concurrent workers per vCore. For other max vCore settings per database that are less 1 vCore or less, the number of max concurrent workers is similarly rescaled.

<sup>5</sup> See [External Connections](resource-limits-logical-server.md#external-connections) for additional details on what counts as an external connection.

### Fsv2-series Hardware (part 2 of 2)

|Compute size (service objective)|GP_Fsv2_18|GP_Fsv2_20|GP_Fsv2_24|GP_Fsv2_32| GP_Fsv2_36|GP_Fsv2_72|
|:---| ---:|---:|---:|---:|---:|---:|
|Compute generation|Fsv2-series|Fsv2-series|Fsv2-series|Fsv2-series|Fsv2-series|Fsv2-series|
|vCores|18|20|24|32|36|72|
|Memory (GB)|34.0|37.8|45.4|60.5|68.0|136.0|
|Max number DBs per pool <sup>1</sup>|500|500|500|500|500|
|Columnstore support|Yes|Yes|Yes|Yes|Yes|Yes|
|In-memory OLTP storage (GB)|N/A|N/A|N/A|N/A|N/A|N/A|
|Max data size (GB)|1536|1536|1536|3072|3072|4096|
|Max log size (GB) <sup>2</sup>|512|512|512|1024|1024|1024|
|TempDB max data size (GB)|83|93|111|148|167|333|
|Storage type|Remote SSD|Remote SSD|Remote SSD|Remote SSD|Remote SSD|Remote SSD|
|Read IO latency (approximate)|5-10 ms|5-10 ms|5-10 ms|5-10 ms|5-10 ms|5-10 ms|
|Write IO latency (approximate)|5-7 ms|5-7 ms|5-7 ms|5-7 ms|5-7 ms|5-7 ms|
|Max data IOPS per pool <sup>3</sup>|12,600|14,000|16,800|22,400|25,200|31,200|
|Max log rate per pool (MBps)|62.5|62.5|62.5|62.5|62.5|62.5|
|Max concurrent workers per pool <sup>4</sup>|900|1000|1200|1600|1800|3600|
|Max concurrent logins per pool <sup>4</sup>|1800|2000|2400|3200|3600|7200|
|Max concurrent external connections per pool <sup>5</sup>|90|100|120|150|150|150|
|Max concurrent sessions|30,000|30,000|30,000|30,000|30,000|30,000|
|Min/max elastic pool vCore choices per database|0-18|0-20|0-24|0-32|0-36|0-72|
|Number of replicas|1|1|1|1|1|1|
|Multi-AZ|N/A|N/A|N/A|N/A|N/A|N/A|
|Read Scale-out|N/A|N/A|N/A|N/A|N/A|N/A|
|Included backup storage|1X DB size|1X DB size|1X DB size|1X DB size|1X DB size|1X DB size|

<sup>1</sup> See [Resource management in dense elastic pools](elastic-pool-resource-management.md) for additional considerations.

<sup>2</sup> For documented max data size values. Reducing max data size reduces max log size proportionally.

<sup>3</sup> The maximum value for IO sizes ranging between 8 KB and 64 KB. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance).

<sup>4</sup> For the max concurrent workers for any individual database, see [Single database resource limits](resource-limits-vcore-single-databases.md). For example, if the elastic pool is using standard-series (Gen5) and the max vCore per database is set at 2, then the max concurrent workers value is 200.  If max vCore per database is set to 0.5, then the max concurrent workers value is 50 since on standard-series (Gen5) there are a max of 100 concurrent workers per vCore. For other max vCore settings per database that are less 1 vCore or less, the number of max concurrent workers is similarly rescaled.

<sup>5</sup> See [External Connections](resource-limits-logical-server.md#external-connections) for additional details on what counts as an external connection.

## General purpose - provisioned compute - DC-series

|Compute size (service objective)|GP_DC_2|GP_DC_4|GP_DC_6|GP_DC_8|
|:--- | --: |--: |--: |--: |
|Compute generation|DC|DC|DC|DC|
|vCores|2|4|6|8|
|Memory (GB)|9|18|27|36|
|Max number DBs per pool <sup>1</sup>|100|400|400|400|
|Columnstore support|Yes|Yes|Yes|Yes|
|In-memory OLTP storage (GB)|N/A|N/A|N/A|N/A|
|Max data size (GB)|756|1536|2048|2048|
|Max log size (GB) <sup>2</sup>|227|461|614|614|
|TempDB max data size (GB)|64|128|192|256|
|Storage type|Premium (Remote) Storage|Premium (Remote) Storage|Premium (Remote) Storage|Premium (Remote) Storage|
|Read IO latency (approximate)|5-10 ms|5-10 ms|5-10 ms|5-10 ms|
|Write IO latency (approximate)|5-7 ms|5-7 ms|5-7 ms|5-7 ms|
|Max data IOPS per pool <sup>3</sup>|1,400|2,800|4,200|5,600|
|Max log rate per pool (MBps)|12|24|36|48|
|Max concurrent workers per pool <sup>4</sup>|168|336|504|672|
|Max concurrent logins per pool <sup>4</sup>|168|336|504|672|
|Max concurrent external connections per pool <sup>5</sup>|16|33|50|67|
|Max concurrent sessions|30,000|30,000|30,000|30,000|
|Min/max elastic pool vCore choices per database|2|2...4|2...6|2...8|
|Number of replicas|1|1|1|1|
|Multi-AZ|N/A|N/A|N/A|N/A|
|Read Scale-out|N/A|N/A|N/A|N/A|
|Included backup storage|1X DB size|1X DB size|1X DB size|1X DB size|

<sup>1</sup> See [Resource management in dense elastic pools](elastic-pool-resource-management.md) for additional considerations.

<sup>2</sup> For documented max data size values. Reducing max data size reduces max log size proportionally.

<sup>3</sup> The maximum value for IO sizes ranging between 8 KB and 64 KB. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance).

<sup>4</sup> For the max concurrent workers for any individual database, see [Single database resource limits](resource-limits-vcore-single-databases.md). For example, if the elastic pool is using standard-series (Gen5) and the max vCore per database is set at 2, then the max concurrent workers value is 200.  If max vCore per database is set to 0.5, then the max concurrent workers value is 50 since on standard-series (Gen5) there are a max of 100 concurrent workers per vCore. For other max vCore settings per database that are less 1 vCore or less, the number of max concurrent workers is similarly rescaled.

<sup>5</sup> See [External Connections](resource-limits-logical-server.md#external-connections) for additional details on what counts as an external connection.

## Business Critical - provisioned compute - standard-series (Gen5)

### Business Critical service tier: standard-series (Gen5) (part 1 of 3)

|Compute size (service objective)|BC_Gen5_4|BC_Gen5_6|BC_Gen5_8|BC_Gen5_10|BC_Gen5_12|
|:--- | --: |--: |--: |--: |---: |
|Compute generation|Gen5|Gen5|Gen5|Gen5|Gen5|
|vCores|4|6|8|10|12|
|Memory (GB)|20.8|31.1|41.5|51.9|62.3|
|Max number DBs per pool <sup>1</sup>|50|100|100|100|100|
|Columnstore support|Yes|Yes|Yes|Yes|Yes|
|In-memory OLTP storage (GB)|3.14|4.71|6.28|8.65|11.02|
|Max data size (GB)|1024|1536|2048|2048|3072|
|Max log size (GB) <sup>2</sup>|307|307|461|461|922|
|TempDB max data size (GB)|128|192|256|320|384|
|[Max local storage size](resource-limits-logical-server.md#storage-space-governance) (GB)|4829|4829|4829|4829|4829|
|Storage type|Local SSD|Local SSD|Local SSD|Local SSD|Local SSD|
|Read IO latency (approximate)|1-2 ms|1-2 ms|1-2 ms|1-2 ms|1-2 ms|
|Write IO latency (approximate)|1-2 ms|1-2 ms|1-2 ms|1-2 ms|1-2 ms|
|Max data IOPS per pool <sup>3</sup>|18,000|27,000|36,000|45,000|54,000|
|Max log rate per pool (MBps)|60|90|120|120|120|
|Max concurrent workers per pool <sup>4</sup>|420|630|840|1050|1260|
|Max concurrent logins per pool <sup>4</sup>|420|630|840|1050|1260|
|Max concurrent external connections per pool <sup>5</sup>|42|63|84|105|126|
|Max concurrent sessions|30,000|30,000|30,000|30,000|30,000|
|Min/max elastic pool vCore choices per database|0, 0.25, 0.5, 1...4|0, 0.25, 0.5, 1...6|0, 0.25, 0.5, 1...8|0, 0.25, 0.5, 1...10|0, 0.25, 0.5, 1...12|
|Number of replicas|4|4|4|4|4|
|Multi-AZ|Yes|Yes|Yes|Yes|Yes|
|Read Scale-out|Yes|Yes|Yes|Yes|Yes|
|Included backup storage|1X DB size|1X DB size|1X DB size|1X DB size|1X DB size|

<sup>1</sup> See [Resource management in dense elastic pools](elastic-pool-resource-management.md) for additional considerations.

<sup>2</sup> For documented max data size values. Reducing max data size reduces max log size proportionally.

<sup>3</sup> The maximum value for IO sizes ranging between 8 KB and 64 KB. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance).

<sup>4</sup> For the max concurrent workers for any individual database, see [Single database resource limits](resource-limits-vcore-single-databases.md). For example, if the elastic pool is using standard-series (Gen5) and the max vCore per database is set at 2, then the max concurrent workers value is 200.  If max vCore per database is set to 0.5, then the max concurrent workers value is 50 since on standard-series (Gen5) there are a max of 100 concurrent workers per vCore. For other max vCore settings per database that are less 1 vCore or less, the number of max concurrent workers is similarly rescaled.

<sup>5</sup> See [External Connections](resource-limits-logical-server.md#external-connections) for additional details on what counts as an external connection.

### Business Critical service tier: standard-series (Gen5) (part 2 of 3)

|Compute size (service objective)|BC_Gen5_14|BC_Gen5_16|BC_Gen5_18|BC_Gen5_20|BC_Gen5_24|
|:--- | --: |--: |--: |--: | --: | 
|Compute generation|Gen5|Gen5|Gen5|Gen5|Gen5|
|vCores|14|16|18|20|24|
|Memory (GB)|72.7|83|93.4|103.8|124.6|
|Max number DBs per pool <sup>1</sup>|100|100|100|100|100|
|Columnstore support|Yes|Yes|Yes|Yes|Yes|
|In-memory OLTP storage (GB)|13.39|15.77|18.14|20.51|25.25|
|Max data size (GB)|3072|3072|3072|3072|4096|
|Max log size (GB) <sup>2</sup>|922|922|922|922|1229|
|TempDB max data size (GB)|448|512|576|640|768|
|[Max local storage size](resource-limits-logical-server.md#storage-space-governance) (GB)|4829|4829|4829|4829|4829|
|Storage type|Local SSD|Local SSD|Local SSD|Local SSD|Local SSD| 
|Read IO latency (approximate)|1-2 ms|1-2 ms|1-2 ms|1-2 ms|1-2 ms|
|Write IO latency (approximate)|1-2 ms|1-2 ms|1-2 ms|1-2 ms|1-2 ms|
|Max data IOPS per pool <sup>3</sup>|63,000|72,000|81,000|90,000|108,000|
|Max log rate per pool (MBps)|120|120|120|120|120|
|Max concurrent workers per pool <sup>4</sup>|1470|1680|1890|2100|2520|
|Max concurrent logins per pool <sup>4</sup>|1470|1680|1890|2100|2520|
|Max concurrent external connections per pool <sup>5</sup>|147|150|150|150|150|
|Max concurrent sessions|30,000|30,000|30,000|30,000|30,000|
|Min/max elastic pool vCore choices per database|0, 0.25, 0.5, 1...14|0, 0.25, 0.5, 1...16|0, 0.25, 0.5, 1...18|0, 0.25, 0.5, 1...20|0, 0.25, 0.5, 1...20, 24|
|Number of replicas|4|4|4|4|4|
|Multi-AZ|Yes|Yes|Yes|Yes|Yes|
|Read Scale-out|Yes|Yes|Yes|Yes|Yes|
|Included backup storage|1X DB size|1X DB size|1X DB size|1X DB size|1X DB size|

<sup>1</sup> See [Resource management in dense elastic pools](elastic-pool-resource-management.md) for additional considerations.

<sup>2</sup> For documented max data size values. Reducing max data size reduces max log size proportionally.

<sup>3</sup> The maximum value for IO sizes ranging between 8 KB and 64 KB. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance).

<sup>4</sup> For the max concurrent workers for any individual database, see [Single database resource limits](resource-limits-vcore-single-databases.md). For example, if the elastic pool is using standard-series (Gen5) and the max vCore per database is set at 2, then the max concurrent workers value is 200.  If max vCore per database is set to 0.5, then the max concurrent workers value is 50 since on standard-series (Gen5) there are a max of 100 concurrent workers per vCore. For other max vCore settings per database that are less 1 vCore or less, the number of max concurrent workers is similarly rescaled.

### Business Critical service tier: standard-series (Gen5) (part 3 of 3)

|Compute size (service objective)|BC_Gen5_32|BC_Gen5_40|BC_Gen5_80|BC_Gen5_128|
|:--- | --: | --: |--: |--: |
|Compute generation|Gen5|Gen5|Gen5|Gen5|
|vCores|32|40|80|128<sup>6</sup>|
|Memory (GB)|166.1|207.6|415.2|625|
|Max number DBs per pool <sup>1</sup>|100|100|100|100|
|Columnstore support|Yes|Yes|Yes|Yes|
|In-memory OLTP storage (GB)|37.94|52.23|131.68|227.02|
|Max data size (GB)|4096|4096|4096|4096|
|Max log size (GB) <sup>2</sup>|1229|1229|1229|1229|
|TempDB max data size (GB)|1024|1280|2560|2560|
|[Max local storage size](resource-limits-logical-server.md#storage-space-governance) (GB)|4829|4829|4829|4829|
|Storage type|Local SSD|Local SSD|Local SSD|Local SSD|
|Read IO latency (approximate)|1-2 ms|1-2 ms|1-2 ms|1-2 ms|
|Write IO latency (approximate)|1-2 ms|1-2 ms|1-2 ms|1-2 ms|
|Max data IOPS per pool <sup>3</sup>|144,000|180,000|256,000|409,600|
|Max log rate per pool (MBps)|120|120|120|120|
|Max concurrent workers per pool <sup>4</sup>|3360|4200|8400|13440|
|Max concurrent logins per pool <sup>4</sup>|3360|4200|8400|8400|
|Max concurrent external connections per pool <sup>5</sup>|150|150|150|150|
|Max concurrent sessions|30,000|30,000|30,000|30,000|
|Min/max elastic pool vCore choices per database|0, 0.25, 0.5, 1...20, 24, 32|0, 0.25, 0.5, 1...20, 24, 32, 40|0, 0.25, 0.5, 1...20, 24, 32, 40, 80|0, 0.25, 0.5, 1...20, 24, 32, 40, 80, 128|
|Number of replicas|4|4|4|4|
|Multi-AZ|Yes|Yes|Yes|Yes|
|Read Scale-out|Yes|Yes|Yes|Yes|
|Included backup storage|1X DB size|1X DB size|1X DB size|1X DB size|

<sup>1</sup> See [Resource management in dense elastic pools](elastic-pool-resource-management.md) for additional considerations.

<sup>2</sup> For documented max data size values. Reducing max data size reduces max log size proportionally.

<sup>3</sup> The maximum value for IO sizes ranging between 8 KB and 64 KB. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance).

<sup>4</sup> For the max concurrent workers for any individual database, see [Single database resource limits](resource-limits-vcore-single-databases.md). For example, if the elastic pool is using standard-series (Gen5) and the max vCore per database is set at 2, then the max concurrent workers value is 200.  If max vCore per database is set to 0.5, then the max concurrent workers value is 50 since on standard-series (Gen5) there are a max of 100 concurrent workers per vCore. For other max vCore settings per database that are less 1 vCore or less, the number of max concurrent workers is similarly rescaled.

<sup>5</sup> See [External Connections](resource-limits-logical-server.md#external-connections) for additional details on what counts as an external connection.

<sup>6</sup> The 128 vCore offering is currently in public preview.

## Business Critical - provisioned compute - M-series

For important information about M-series hardware availability, see [Azure offer types supported by M-series](service-tiers-sql-database-vcore.md#azure-offer-types-supported-by-m-series).

### M-series hardware (part 1 of 2)

|Compute size (service objective)|BC_M_8|BC_M_10|BC_M_12|BC_M_14|BC_M_16|BC_M_18|
|:---| ---:|---:|---:|---:|---:|---:|
|Compute generation|M-series|M-series|M-series|M-series|M-series|M-series|
|vCores|8|10|12|14|16|18|
|Memory (GB)|235.4|294.3|353.2|412.0|470.9|529.7|
|Max number DBs per pool <sup>1</sup>|100|100|100|100|100|100|
|Columnstore support|Yes|Yes|Yes|Yes|Yes|Yes|
|In-memory OLTP storage (GB)|64|80|96|112|128|150|
|Max data size (GB)|512|640|768|896|1024|1152|
|Max log size (GB) <sup>2</sup>|171|213|256|299|341|384|
|TempDB max data size (GB)|256|320|384|448|512|576|
|[Max local storage size](resource-limits-logical-server.md#storage-space-governance) (GB)|13,836|13,836|13,836|13,836|13,836|13,836|
|Storage type|Local SSD|Local SSD|Local SSD|Local SSD|Local SSD|Local SSD|
|Read IO latency (approximate)|1-2 ms|1-2 ms|1-2 ms|1-2 ms|1-2 ms|1-2 ms|
|Write IO latency (approximate)|1-2 ms|1-2 ms|1-2 ms|1-2 ms|1-2 ms|1-2 ms|
|Max data IOPS per pool <sup>3</sup>|12,499|15,624|18,748|21,873|24,998|28,123|
|Max log rate per pool (MBps)|48|60|72|84|96|108|
|Max concurrent workers per pool <sup>4</sup>|800|1,000|1,200|1,400|1,600|1,800|
|Max concurrent logins per pool <sup>4</sup>|800|1,000|1,200|1,400|1,600|1,800|
|Max concurrent sessions|30,000|30,000|30,000|30,000|30,000|30,000|
|Max concurrent external connections per pool <sup>5</sup>|80|100|120|140|150|150|
|Min/max elastic pool vCore choices per database|0-8|0-10|0-12|0-14|0-16|0-18|
|Number of replicas|4|4|4|4|4|4|
|Multi-AZ|No|No|No|No|No|No|
|Read Scale-out|Yes|Yes|Yes|Yes|Yes|Yes|
|Included backup storage|1X DB size|1X DB size|1X DB size|1X DB size|1X DB size|1X DB size|

<sup>1</sup> See [Resource management in dense elastic pools](elastic-pool-resource-management.md) for additional considerations.

<sup>2</sup> For documented max data size values. Reducing max data size reduces max log size proportionally.

<sup>3</sup> The maximum value for IO sizes ranging between 8 KB and 64 KB. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance).

<sup>4</sup> For the max concurrent workers for any individual database, see [Single database resource limits](resource-limits-vcore-single-databases.md). For example, if the elastic pool is using standard-series (Gen5) and the max vCore per database is set at 2, then the max concurrent workers value is 200.  If max vCore per database is set to 0.5, then the max concurrent workers value is 50 since on standard-series (Gen5) there are a max of 100 concurrent workers per vCore. For other max vCore settings per database that are less 1 vCore or less, the number of max concurrent workers is similarly rescaled.

<sup>5</sup> See [External Connections](resource-limits-logical-server.md#external-connections) for additional details on what counts as an external connection.

### M-series hardware (part 2 of 2)

|Compute size (service objective)|BC_M_20|BC_M_24|BC_M_32|BC_M_64|BC_M_128|
|:---| ---:|---:|---:|---:|---:|
|Compute generation|M-series|M-series|M-series|M-series|M-series|
|vCores|20|24|32|64|128|
|Memory (GB)|588.6|706.3|941.8|1883.5|3767.0|
|Max number DBs per pool <sup>1</sup>|100|100|100|100|100|
|Columnstore support|Yes|Yes|Yes|Yes|Yes|
|In-memory OLTP storage (GB)|172|216|304|704|1768|
|Max data size (GB)|1280|1536|2048|4096|4096|
|Max log size (GB) <sup>2</sup>|427|512|683|1024|1024|
|TempDB max data size (GB)|640|768|1024|2048|4096|
|[Max local storage size](resource-limits-logical-server.md#storage-space-governance) (GB)|13,836|13,836|13,836|13,836|13,836|
|Storage type|Local SSD|Local SSD|Local SSD|Local SSD|Local SSD|
|Read IO latency (approximate)|1-2 ms|1-2 ms|1-2 ms|1-2 ms|1-2 ms|
|Write IO latency (approximate)|1-2 ms|1-2 ms|1-2 ms|1-2 ms|1-2 ms|
|Max data IOPS per pool <sup>3</sup>|31,248|37,497|49,996|99,993|160,000|
|Max log rate per pool (MBps)|120|144|192|264|264|
|Max concurrent workers per pool <sup>4</sup>|2,000|2,400|3,200|6,400|12,800|
|Max concurrent logins per pool <sup>4</sup>|2,000|2,400|3,200|6,400|12,800|
|Max concurrent sessions|30,000|30,000|30,000|30,000|30,000|
|Max concurrent external connections per pool <sup>5</sup>|150|150|150|150|150|
|Number of replicas|4|4|4|4|4|
|Multi-AZ|No|No|No|No|No|
|Read Scale-out|Yes|Yes|Yes|Yes|Yes|
|Included backup storage|1X DB size|1X DB size|1X DB size|1X DB size|1X DB size|

<sup>1</sup> See [Resource management in dense elastic pools](elastic-pool-resource-management.md) for additional considerations.

<sup>2</sup> For documented max data size values. Reducing max data size reduces max log size proportionally.

<sup>3</sup> The maximum value for IO sizes ranging between 8 KB and 64 KB. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance).

<sup>4</sup> For the max concurrent workers for any individual database, see [Single database resource limits](resource-limits-vcore-single-databases.md). For example, if the elastic pool is using standard-series (Gen5) and the max vCore per database is set at 2, then the max concurrent workers value is 200.  If max vCore per database is set to 0.5, then the max concurrent workers value is 50 since on standard-series (Gen5) there are a max of 100 concurrent workers per vCore. For other max vCore settings per database that are less 1 vCore or less, the number of max concurrent workers is similarly rescaled.

<sup>5</sup> See [External Connections](resource-limits-logical-server.md#external-connections) for additional details on what counts as an external connection.

## Business Critical - provisioned compute - DC-series

|Compute size (service objective)|BC_DC_2|BC_DC_4|BC_DC_6|BC_DC_8|
|:--- | --: |--: |--: |--: |
|Compute generation|DC|DC|DC|DC|
|vCores|2|4|6|8|
|Memory (GB)|9|18|27|36|
|Max number DBs per pool <sup>1</sup>|50|100|100|100|
|Columnstore support|Yes|Yes|Yes|Yes|
|In-memory OLTP storage (GB)|1.7|3.7|5.9|8.2|
|Max data size (GB)|768|768|768|768|
|Max log size (GB) <sup>2</sup>|230|230|230|230|
|TempDB max data size (GB)|64|128|192|256|
|[Max local storage size](resource-limits-logical-server.md#storage-space-governance) (GB)|1406|1406|1406|1406|
|Storage type|Local SSD|Local SSD|Local SSD|Local SSD|
|Read IO latency (approximate)|1-2 ms|1-2 ms|1-2 ms|1-2 ms|
|Write IO latency (approximate)|1-2 ms|1-2 ms|1-2 ms|1-2 ms|
|Max data IOPS per pool <sup>3</sup>|15,750|31,500|47,250|56,000|
|Max log rate per pool (MBps)|20|60|90|120|
|Max concurrent workers per pool <sup>4</sup>|168|336|504|672|
|Max concurrent logins per pool <sup>4</sup>|168|336|504|672|
|Max concurrent external connections per pool <sup>5</sup>|16|33|50|67|
|Max concurrent sessions|30,000|30,000|30,000|30,000|
|Min/max elastic pool vCore choices per database|2|2...4|2...6|2...8|
|Number of replicas|4|4|4|4|
|Multi-AZ|No|No|No|No|
|Read Scale-out|Yes|Yes|Yes|Yes|
|Included backup storage|1X DB size|1X DB size|1X DB size|1X DB size|

<sup>1</sup> See [Resource management in dense elastic pools](elastic-pool-resource-management.md) for additional considerations.

<sup>2</sup> For documented max data size values. Reducing max data size reduces max log size proportionally.

<sup>3</sup> The maximum value for IO sizes ranging between 8 KB and 64 KB. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance).

<sup>4</sup> For the max concurrent workers for any individual database, see [Single database resource limits](resource-limits-vcore-single-databases.md). For example, if the elastic pool is using standard-series (Gen5) and the max vCore per database is set at 2, then the max concurrent workers value is 200.  If max vCore per database is set to 0.5, then the max concurrent workers value is 50 since on standard-series (Gen5) there are a max of 100 concurrent workers per vCore. For other max vCore settings per database that are less 1 vCore or less, the number of max concurrent workers is similarly rescaled.

<sup>5</sup> See [External Connections](resource-limits-logical-server.md#external-connections) for additional details on what counts as an external connection.

## Database properties for pooled databases

For each elastic pool, you can optionally specify per database minimum and maximum vCores to modify resource consumption patterns within the pool. Specified min and max values apply to all databases in the pool. Customizing min and max vCores for individual databases in the pool is not supported. 

You can also set maximum storage per database, for example to prevent a database from consuming all pool storage. This setting can be configured independently for each database.

The following table describes per database properties for pooled databases. 

| Property | Description |
|:--- |:--- |
| Max vCores per database |The maximum number of vCores that any database in the pool may use, if available based on utilization by other databases in the pool. Max vCores per database is not a resource guarantee for a database. If the workload in each database does not need all available pool resources to perform adequately, consider setting max vCores per database to prevent a single database from monopolizing pool resources. Some degree of over-committing is expected since the pool generally assumes hot and cold usage patterns for databases, where all databases are not simultaneously peaking. |
| Min vCores per database |The minimum number of vCores reserved for any database in the pool. Consider setting a min vCores per database when you want to guarantee resource availability for each database regardless of resource consumption by other databases in the pool. The min vCores per database may be set to 0, and is also the default value. This property is set to anywhere between 0 and the average vCores utilization per database.|
| Max storage per database |The maximum database size set by the user for a database in a pool. Pooled databases share allocated pool storage, so the size a database can reach is limited to the smaller of remaining pool storage and maximum database size. Maximum database size refers to the maximum size of the data files and does not include the space used by the log file. |

> [!IMPORTANT]
> Because resources in an elastic pool are finite, setting min vCores per database to a value greater than 0 implicitly limits resource utilization by each database. If, at a point in time, most databases in a pool are idle, resources reserved to satisfy the min vCores guarantee are not available to databases active at that point in time.
>
> Additionally, setting min vCores per database to a value greater than 0 implicitly limits the number of databases that can be added to the pool. For example, if you set the min vCores to 2 in a 20 vCore pool, it means that you will not be able to add more than 10 databases to the pool, because 2 vCores are reserved for each database.

Even though the per database properties are expressed in vCores, they also govern consumption of other resource types, such as data IO, log IO, buffer pool memory, and worker threads. As you adjust min and max per database vCore values, reservations and limits for all resource types are adjusted proportionally.

Min and max per database vCore values apply to resource consumption by user workloads, but not to resource consumption by internal processes. For example, for a database with a per database max vCores set to half of the pool vCores, user workload cannot consume more than one half of the buffer pool memory. However, this database can still take advantage of pages in the buffer pool that were loaded by internal processes. For more information, see [Resource consumption by user workloads and internal processes](resource-limits-logical-server.md#resource-consumption-by-user-workloads-and-internal-processes).

> [!NOTE]
> The resource limits of individual databases in elastic pools are generally the same as for single databases outside of pools that have the same compute size (service objective). For example, the max concurrent workers for an GP_S_Gen5_10 database is 750 workers. So, the max concurrent workers for a database in a GP_Gen5_10 pool is also 750 workers. Note, the total number of concurrent workers in GP_Gen5_10 pool is 1050. For the max concurrent workers for any individual database, see [Single database resource limits](resource-limits-vcore-single-databases.md?view=azuresql&preserve-view=true).


## Previously available hardware

This section includes details on previously available hardware.

[!INCLUDE[azure-sql-gen4-hardware-retirement](../includes/azure-sql-gen4-hardware-retirement.md)]

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

#### General purpose service tier: Generation 4 compute platform (part 1 of 2)

|Compute size (service objective)|GP_Gen4_1|GP_Gen4_2|GP_Gen4_3|GP_Gen4_4|GP_Gen4_5|GP_Gen4_6|
|:--- | --: |--: |--: |--: |--: |--: |
|Compute generation|Gen4|Gen4|Gen4|Gen4|Gen4|Gen4|
|vCores|1|2|3|4|5|6|
|Memory (GB)|7|14|21|28|35|42|
|Max number DBs per pool <sup>1</sup>|100|200|500|500|500|500|
|Columnstore support|Yes|Yes|Yes|Yes|Yes|Yes|
|In-memory OLTP storage (GB)|N/A|N/A|N/A|N/A|N/A|N/A|
|Max data size (GB)|512|756|1536|1536|1536|2048|
|Max log size <sup>2</sup>|154|227|461|461|461|614|
|TempDB max data size (GB)|32|64|96|128|160|192|
|Storage type|Premium (Remote) Storage|Premium (Remote) Storage|Premium (Remote) Storage|Premium (Remote) Storage|Premium (Remote) Storage|Premium (Remote) Storage|
|Read IO latency (approximate)|5-10 ms|5-10 ms|5-10 ms|5-10 ms|5-10 ms|5-10 ms|
|Write IO latency (approximate)|5-7 ms|5-7 ms|5-7 ms|5-7 ms|5-7 ms|5-7 ms|
|Max data IOPS per pool <sup>3</sup> |400|800|1200|1600|2000|2400|
|Max log rate per pool (MBps)|6|12|18|24|30|36|
|Max concurrent workers per pool<sup>4</sup> |210|420|630|840|1050|1260|
|Max concurrent logins per pool <sup>4</sup> |210|420|630|840|1050|1260|
|Max concurrent external connections per pool <sup>5</sup>|21|42|63|84|105|126|
|Max concurrent sessions|30,000|30,000|30,000|30,000|30,000|30,000|
|Min/max elastic pool vCore choices per database|0, 0.25, 0.5, 1|0, 0.25, 0.5, 1, 2|0, 0.25, 0.5, 1...3|0, 0.25, 0.5, 1...4|0, 0.25, 0.5, 1...5|0, 0.25, 0.5, 1...6|
|Number of replicas|1|1|1|1|1|1|
|Multi-AZ|N/A|N/A|N/A|N/A|N/A|N/A|
|Read Scale-out|N/A|N/A|N/A|N/A|N/A|N/A|
|Included backup storage|1X DB size|1X DB size|1X DB size|1X DB size|1X DB size|1X DB size|

<sup>1</sup> See [Resource management in dense elastic pools](elastic-pool-resource-management.md) for additional considerations.

<sup>2</sup> For documented max data size values. Reducing max data size reduces max log size proportionally.

<sup>3</sup> The maximum value for IO sizes ranging between 8 KB and 64 KB. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance).

<sup>4</sup> For the max concurrent workers for any individual database, see [Single database resource limits](resource-limits-vcore-single-databases.md). For example, if the elastic pool is using standard-series (Gen5) and the max vCore per database is set at 2, then the max concurrent workers value is 200.  If max vCore per database is set to 0.5, then the max concurrent workers value is 50 since on standard-series (Gen5) there are a max of 100 concurrent workers per vCore. For other max vCore settings per database that are less 1 vCore or less, the number of max concurrent workers is similarly rescaled.

<sup>5</sup> See [External Connections](resource-limits-logical-server.md#external-connections) for additional details on what counts as an external connection.

#### General purpose service tier: Generation 4 compute platform (part 2 of 2)

|Compute size (service objective)|GP_Gen4_7|GP_Gen4_8|GP_Gen4_9|GP_Gen4_10|GP_Gen4_16|GP_Gen4_24|
|:--- | --: |--: |--: |--: |--: |--: |
|Compute generation|Gen4|Gen4|Gen4|Gen4|Gen4|Gen4|
|vCores|7|8|9|10|16|24|
|Memory (GB)|49|56|63|70|112|159.5|
|Max number DBs per pool <sup>1</sup>|500|500|500|500|500|500|
|Columnstore support|Yes|Yes|Yes|Yes|Yes|Yes|
|In-memory OLTP storage (GB)|N/A|N/A|N/A|N/A|N/A|N/A|
|Max data size (GB)|2048|2048|2048|2048|3584|4096|
|Max log size (GB) <sup>2</sup>|614|614|614|614|1075|1229|
|TempDB max data size (GB)|224|256|288|320|512|768|
|Storage type|Premium (Remote) Storage|Premium (Remote) Storage|Premium (Remote) Storage|Premium (Remote) Storage|Premium (Remote) Storage|Premium (Remote) Storage|
|Read IO latency (approximate)|5-10 ms|5-10 ms|5-10 ms|5-10 ms|5-10 ms|5-10 ms|
|Write IO latency (approximate)|5-7 ms|5-7 ms|5-7 ms|5-7 ms|5-7 ms|5-7 ms|
|Max data IOPS per pool <sup>3</sup>|2800|3200|3600|4000|6400|9600|
|Max log rate per pool (MBps)|42|48|54|60|62.5|62.5|
|Max concurrent workers per pool <sup>4</sup>|1470|1680|1890|2100|3360|5040|
|Max concurrent logins pool <sup>4</sup>|1470|1680|1890|2100|3360|5040|
|Max concurrent external connections per pool <sup>5</sup>|147|150|150|150|150|150|
|Max concurrent sessions|30,000|30,000|30,000|30,000|30,000|30,000|
|Min/max elastic pool vCore choices per database|0, 0.25, 0.5, 1...7|0, 0.25, 0.5, 1...8|0, 0.25, 0.5, 1...9|0, 0.25, 0.5, 1...10|0, 0.25, 0.5, 1...10, 16|0, 0.25, 0.5, 1...10, 16, 24|
|Number of replicas|1|1|1|1|1|1|
|Multi-AZ|N/A|N/A|N/A|N/A|N/A|N/A|
|Read Scale-out|N/A|N/A|N/A|N/A|N/A|N/A|
|Included backup storage|1X DB size|1X DB size|1X DB size|1X DB size|1X DB size|1X DB size|

<sup>1</sup> See [Resource management in dense elastic pools](elastic-pool-resource-management.md) for additional considerations.

<sup>2</sup> For documented max data size values. Reducing max data size reduces max log size proportionally.

<sup>3</sup> The maximum value for IO sizes ranging between 8 KB and 64 KB. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance).    

<sup>4</sup> For the max concurrent workers for any individual database, see [Single database resource limits](resource-limits-vcore-single-databases.md). For example, if the elastic pool is using standard-series (Gen5) and the max vCore per database is set at 2, then the max concurrent workers value is 200.  If max vCore per database is set to 0.5, then the max concurrent workers value is 50 since on standard-series (Gen5) there are a max of 100 concurrent workers per vCore. For other max vCore settings per database that are less 1 vCore or less, the number of max concurrent workers is similarly rescaled.

<sup>5</sup> See [External Connections](resource-limits-logical-server.md#external-connections) for additional details on what counts as an external connection.

### Business critical - provisioned compute - Gen4

> [!IMPORTANT]
> New Gen4 databases are no longer supported in the Australia East or Brazil South regions.

#### Business critical service tier: Generation 4 compute platform (part 1 of 2)

|Compute size (service objective)|BC_Gen4_2|BC_Gen4_3|BC_Gen4_4|BC_Gen4_5|BC_Gen4_6|
|:--- | --: |--: |--: |--: |--: |--: |
|Compute generation|Gen4|Gen4|Gen4|Gen4|Gen4|
|vCores|2|3|4|5|6|
|Memory (GB)|14|21|28|35|42|
|Max number DBs per pool <sup>1</sup>|50|100|100|100|100|
|Columnstore support|Yes|Yes|Yes|Yes|Yes|
|In-memory OLTP storage (GB)|2|3|4|5|6|
|Storage type|Local SSD|Local SSD|Local SSD|Local SSD|Local SSD|
|Max data size (GB)|1024|1024|1024|1024|1024|
|Max log size (GB) <sup>2</sup>|307|307|307|307|307|
|TempDB max data size (GB)|64|96|128|160|192|
|[Max local storage size](resource-limits-logical-server.md#storage-space-governance) (GB)|1356|1356|1356|1356|1356|
|Read IO latency (approximate)|1-2 ms|1-2 ms|1-2 ms|1-2 ms|1-2 ms|
|Write IO latency (approximate)|1-2 ms|1-2 ms|1-2 ms|1-2 ms|1-2 ms|
|Max data IOPS per pool <sup>3</sup>|9,000|13,500|18,000|22,500|27,000|
|Max log rate per pool (MBps)|20|30|40|50|60|
|Max concurrent workers per pool <sup>4</sup>|420|630|840|1050|1260|
|Max concurrent logins per pool <sup>4</sup>|420|630|840|1050|1260|
|Max concurrent external connections per pool <sup>5</sup>|42|63|84|105|126|
|Max concurrent sessions|30,000|30,000|30,000|30,000|30,000|
|Min/max elastic pool vCore choices per database|0, 0.25, 0.5, 1, 2|0, 0.25, 0.5, 1...3|0, 0.25, 0.5, 1...4|0, 0.25, 0.5, 1...5|0, 0.25, 0.5, 1...6|
|Number of replicas|4|4|4|4|4|
|Multi-AZ|Yes|Yes|Yes|Yes|Yes|
|Read Scale-out|Yes|Yes|Yes|Yes|Yes|
|Included backup storage|1X DB size|1X DB size|1X DB size|1X DB size|1X DB size|

<sup>1</sup> See [Resource management in dense elastic pools](elastic-pool-resource-management.md) for additional considerations.

<sup>2</sup> For documented max data size values. Reducing max data size reduces max log size proportionally.

<sup>3</sup> The maximum value for IO sizes ranging between 8 KB and 64 KB. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance).

<sup>4</sup> For the max concurrent workers for any individual database, see [Single database resource limits](resource-limits-vcore-single-databases.md). For example, if the elastic pool is using standard-series (Gen5) and the max vCore per database is set at 2, then the max concurrent workers value is 200.  If max vCore per database is set to 0.5, then the max concurrent workers value is 50 since on standard-series (Gen5) there are a max of 100 concurrent workers per vCore. For other max vCore settings per database that are less 1 vCore or less, the number of max concurrent workers is similarly rescaled.

<sup>5</sup> See [External Connections](resource-limits-logical-server.md#external-connections) for additional details on what counts as an external connection.

#### Business critical service tier: Generation 4 compute platform (part 2 of 2)

|Compute size (service objective)|BC_Gen4_7|BC_Gen4_8|BC_Gen4_9|BC_Gen4_10|BC_Gen4_16|BC_Gen4_24|
|:--- | --: |--: |--: |--: |--: |--: |
|Compute generation|Gen4|Gen4|Gen4|Gen4|Gen4|Gen4|
|vCores|7|8|9|10|16|24|
|Memory (GB)|49|56|63|70|112|159.5|
|Max number DBs per pool <sup>1</sup>|100|100|100|100|100|100|
|Columnstore support|N/A|N/A|N/A|N/A|N/A|N/A|
|In-memory OLTP storage (GB)|7|8|9.5|11|20|36|
|Storage type|Local SSD|Local SSD|Local SSD|Local SSD|Local SSD|Local SSD|
|Max data size (GB)|1024|1024|1024|1024|1024|1024|
|Max log size (GB) <sup>2</sup>|307|307|307|307|307|307|
|TempDB max data size (GB)|224|256|288|320|512|768|
|[Max local storage size](resource-limits-logical-server.md#storage-space-governance) (GB)|1356|1356|1356|1356|1356|1356|
|Read IO latency (approximate)|1-2 ms|1-2 ms|1-2 ms|1-2 ms|1-2 ms|1-2 ms|
|Write IO latency (approximate)|1-2 ms|1-2 ms|1-2 ms|1-2 ms|1-2 ms|1-2 ms|
|Max data IOPS per pool <sup>3</sup>|31,500|36,000|40,500|45,000|72,000|96,000|
|Max log rate per pool (MBps)|70|80|80|80|80|80|
|Max concurrent workers per pool <sup>4</sup>|1470|1680|1890|2100|3360|5040|
|Max concurrent logins per pool <sup>4</sup>|1470|1680|1890|2100|3360|5040|
|Max concurrent external connections per pool <sup>5</sup>|147|150|150|150|150|150|
|Max concurrent sessions|30,000|30,000|30,000|30,000|30,000|30,000|
|Min/max elastic pool vCore choices per database|0, 0.25, 0.5, 1...7|0, 0.25, 0.5, 1...8|0, 0.25, 0.5, 1...9|0, 0.25, 0.5, 1...10|0, 0.25, 0.5, 1...10, 16|0, 0.25, 0.5, 1...10, 16, 24|
|Number of replicas|4|4|4|4|4|4|
|Multi-AZ|Yes|Yes|Yes|Yes|Yes|Yes|
|Read Scale-out|Yes|Yes|Yes|Yes|Yes|Yes|
|Included backup storage|1X DB size|1X DB size|1X DB size|1X DB size|1X DB size|1X DB size|

<sup>1</sup> See [Resource management in dense elastic pools](elastic-pool-resource-management.md) for additional considerations.

<sup>2</sup> For documented max data size values. Reducing max data size reduces max log size proportionally.

<sup>3</sup> The maximum value for IO sizes ranging between 8 KB and 64 KB. Actual IOPS are workload-dependent. For details, see [Data IO Governance](resource-limits-logical-server.md#resource-governance).

<sup>4</sup> For the max concurrent workers for any individual database, see [Single database resource limits](resource-limits-vcore-single-databases.md). For example, if the elastic pool is using standard-series (Gen5) and the max vCore per database is set at 2, then the max concurrent workers value is 200.  If max vCore per database is set to 0.5, then the max concurrent workers value is 50 since on standard-series (Gen5) there are a max of 100 concurrent workers per vCore. For other max vCore settings per database that are less 1 vCore or less, the number of max concurrent workers is similarly rescaled.

<sup>5</sup> See [External Connections](resource-limits-logical-server.md#external-connections) for additional details on what counts as an external connection.

## Next steps

- For vCore resource limits for a single database, see [resource limits for single databases using the vCore purchasing model](resource-limits-vcore-single-databases.md)
- For DTU resource limits for a single database, see [resource limits for single databases using the DTU purchasing model](resource-limits-dtu-single-databases.md)
- For DTU resource limits for elastic pools, see [resource limits for elastic pools using the DTU purchasing model](resource-limits-dtu-elastic-pools.md)
- For resource limits for managed instances, see [managed instance resource limits](../managed-instance/resource-limits.md).
- For information about general Azure limits, see [Azure subscription and service limits, quotas, and constraints](/azure/azure-resource-manager/management/azure-subscription-service-limits).
- For information about resource limits on a logical SQL server, see [overview of resource limits on a logical SQL server](resource-limits-logical-server.md) for information about limits at the server and subscription levels.
