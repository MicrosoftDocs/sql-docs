---
title: SQL Server Big Data Clusters Post-Deployment Configuration Overview
titleSuffix: SQL Server big data clusters
description: Big Data Clusters Post-Deployment Configuration Overview
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: rahul.ajmera
ms.date: 02/11/2021
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---


# How to configure BDC settings post deployment

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

> [!NOTE]
> Post-deplyoment settings configuration is only available in BDC CU9 and later deployments. Settings configuration does not include scale, storage, or endpoint configuration. Options and instructions to configure BDC prior to CU9 can be found [here](configure-bdc-pre-configuration.md).

Cluster, service, and resource scoped settings for Big Data Clusters can be configured post-deployment through the azdata CLI. This functionality allows BDC administrators to adjust configurations to always meet workload requirements. This article goes over example steps to configure a BDC to meet Spark workload requirements. The post-deployment configuration functionality follows a set, diff, apply flow.

## Step by Step: Configure BDC to meet your Spark workload requirements

### View the current configurations of the Big Data Cluster Spark service
The following example shows how to view the user configured settings of the Spark service. You can view all possible configurable settings, system-managed and all configurable settings, and pending settings through optional parameters. Visit [`azdata bdc spark` statement](../azdata/reference/reference-azdata-bdc-spark-statement.md) for more information.

```bash
azdata bdc spark settings show
```
#### Sample output
Spark Service 

|Setting|Running Value|
| --- | --- |
|`spark-defaults-conf.spark.driver.cores`|`1` |
|`spark-defaults-conf.spark.driver.memory`|`1664m` |

### Change the default number of cores and memory for the Spark driver across all resources with Spark (i.e. for the Spark service)
Update the default number of cores to 2 and default memory to 7424m for the Spark service.

```bash
azdata bdc spark settings set spark-defaults-conf.spark.driver.cores=2, spark-defaults-conf.spark.driver.memory=7424m
```

### Change the default number of cores and memory for the Spark executors in the Storage Pool
Update the default number of executor cores to 4 for the Storage Pool.

```bash
azdata bdc spark settings set spark-defaults-conf.spark.executor.cores=4 --resource=storage-0
```

### View the pending settings changes staged in the BDC
View the pending settings changes for the Spark service only and across the entire BDC cluster.

#### Pending Spark Service Settings
```bash
azdata bdc spark settings show --filter-option=pending --include-details
```

### Spark Service

|Setting|Running Value|Configured Value|Configurable|Configured |Last Updated Time|
| --- | --- | --- | --- | --- | --- |
|`spark-defaults-conf.spark.driver.cores`|`1`| `2` | `true` | `true` |
|`spark-defaults-conf.spark.driver.memory`|`1664m`| `7424m` | `true` | `true` |

#### All Pending Settings
```bash
azdata bdc settings show --filter-option=pending --include-details --recursive
```

Spark Service Settings - Pending

|Setting|Running Value|Configured Value|Configurable|Configured|Last Updated Time|
| --- | --- | --- | --- | --- | --- |
|`spark-defaults-conf.spark.driver.cores`|`1`| `2` | `true` | `true` |
|`spark-defaults-conf.spark.driver.memory`|`1664m`| `7424m` | `true` | `true` |

Storage-0 Resource Spark Settings - Pending

|Setting|Running Value|Configured Value|Configurable|Configured|Last Updated Time|
| --- | --- | --- | --- | --- | --- |
|`spark-defaults-conf.spark.executor.cores`|`1`| `4` | `true` | `true` |

### Apply the pending settings to the BDC

```bash
azdata bdc settings apply
```

### Monitor the status of the BDC configuration update

```bash
azdata bdc status show -all
```

## Optional steps

### Revert pending configuration settings

If you determine that you no longer want to change the pending configuration settings, you can un-stage these settings. This will revert the pending settings at all scopes.

```bash
azdata bdc settings revert
```

### Abort the configuration upgrade

If the configuration upgrade fails for any of the components, you can cancel the upgrade process and return the BDC back to its prior configurations. Settings that were staged for change during the upgrade will again be listed as pending settings.

```bash
azdata bdc settings cancel-apply
```

## Next steps

[Configure a SQL Server Big Data Cluster](configure-bdc-overview.md)