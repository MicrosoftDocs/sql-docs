---
title: SQL Server Big Data Clusters post-deployment configuration overview
titleSuffix: SQL Server Big Data Clusters
description: Big data clusters post-deployment configuration overview
author: HugoMSFT
ms.author: hudequei
ms.reviewer: wiassaf
ms.date: 10/05/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: reference
---

# How to configure big data clusters settings post deployment

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

Cluster, service, and resource scoped settings for [!INCLUDE[big-data-clusters-nover](../includes/ssbigdataclusters-ss-nover.md)] can be configured post-deployment through the `azdata` CLI. This functionality allows [!INCLUDE[big-data-clusters-nover](../includes/ssbigdataclusters-ss-nover.md)] administrators to adjust configurations to always meet workload requirements. This article goes over example scenarios on how to configure timezone and Spark workload requirements. The post-deployment configuration functionality follows a set, diff, apply flow.

> [!NOTE]
> Post-deployment settings configuration is only available in [!INCLUDE[big-data-clusters-nover](../includes/ssbigdataclusters-ss-nover.md)] CU9 and later deployments. Settings configuration does not include scale, storage, or endpoint configuration. Options and instructions to configure [!INCLUDE[big-data-clusters-nover](../includes/ssbigdataclusters-ss-nover.md)] prior to CU9 can be found [here](configure-bdc-pre-configuration.md).

## Step by Step Scenario: Configure timezone on [!INCLUDE[big-data-clusters-nover](../includes/ssbigdataclusters-ss-nover.md)]

Starting on [!INCLUDE[big-data-clusters-nover](../includes/ssbigdataclusters-ss-nover.md)] CU13 it is possible to customize the cluster timezone configuration, so services timestamps align with the selected timezone. The setting does not apply to the big data cluster control plane, it sets the new timezone configuration for all SQL Server pools (master, compute, and data), Hadoop components, and Spark.

> [!NOTE]
> By default, [!INCLUDE[big-data-clusters-nover](../includes/ssbigdataclusters-ss-nover.md)] sets UTC as the timezone.

Use the following command to set the timezone configuration:

```bash
azdata bdc settings set --settings bdc.timezone=America/Los_Angeles
```

### Apply the pending settings to the cluster

The following command will apply the configuration and restart all services. Review the last sections of this article on how to track changes and control the configuration process.

```bash
azdata bdc settings apply
```

## Step by Step Scenario: Configure the cluster to meet your Spark workload requirements

### View the current configurations of the big data cluster Spark service

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

### Change the default number of cores and memory for the Spark driver 

Update the default number of cores to two and default memory to 7424 MB for the Spark service. This affects all resources with Spark, for the Spark service.

```bash
azdata bdc spark settings set --settings spark-defaults-conf.spark.driver.cores=2,spark-defaults-conf.spark.driver.memory=7424m
```

### Change the default number of cores and memory for the Spark executors in the Storage Pool

Update the default number of executor cores to 4 for the Storage Pool.

```bash
azdata bdc spark settings set --settings spark-defaults-conf.spark.executor.cores=4 --resource=storage-0
```

### Configure additional paths to the default classpath of Spark applications

The ```/opt/hadoop/share/hadoop/tools/lib/``` path contains several libraries to be used by your spark applications, but the referred path is not loaded by default in the classpath of Spark applications. To enable this setting, apply the following configuration pattern.

```bash
azdata bdc hdfs settings set --settings hadoop-env.HADOOP_CLASSPATH="/opt/hadoop/share/hadoop/tools/lib/*"
```

### View the pending settings changes staged in the big data cluster

View the pending settings changes for the Spark service only and across the entire big data cluster.

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

### Apply the pending settings to the big data cluster

```bash
azdata bdc settings apply
```

### Monitor the configuration update status

```bash
azdata bdc status show
```

## Optional steps

### Revert pending configuration settings

If you determine that you no longer want to change the pending configuration settings, you can un-stage these settings. This will revert the pending settings at all scopes.

```bash
azdata bdc settings revert
```

### Abort the configuration upgrade

If the configuration upgrade fails for any of the components, you can cancel the upgrade process and return the cluster back to its prior configurations. Settings that were staged for change during the upgrade will again be listed as pending settings.

```bash
azdata bdc settings cancel-apply
```

## Next steps

[Configure a SQL Server Big Data Cluster](configure-bdc-overview.md)
