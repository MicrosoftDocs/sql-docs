---
title: SQL Server Big Data Clusters Post-Deployment Configuration Overview
titleSuffix: SQL Server big data clusters
description: Big Data Clusters Post-Deployment Configuration Overview
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: rahul.ajmera
ms.date: 08/04/2020
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---


# Configure BDC settings post-deployments

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

> [!NOTE]
> Post-deplyoment settings configuration is only available in BDC CU9 and later deployments. Settings configuration **does not** include scale, storage, or endpoint configuration.

Cluster, service, and resource scoped settings for Big Data Clusters can be configured post-deployment through the azdata CLI. This functionality allows BDC adminstrators to adjust configurations to always meet workload requirements. This article goes over example steps to configure a BDC to meet Spark workload requirements. The post-deployment configuration functionality follows a set, diff, apply flow.

## Update BDC to meet your Spark workload requirements

### View the current configurations of the Big Data Cluster Spark service
The following example shows how to view the user configured settings of the Spark service. You can view all possible configurable settings, system-managed and all configurable settings, and pending settings through optional parameters. Visit the [azdata reference]() for more information.

```bash
azdata bdc spark config settings show
```
Sample output
```json

```

### Change the default number of cores and memory for the Spark driver across all resources with Spark (i.e. for the Spark servcice)
Update the default number of cores to 2 and default memory to 7424m for the Spark service.

```bash
azdata bdc spark config settings set spark-defaults-conf.spark.driver.cores=2, spark-defaults-conf.spark.driver.memory=7424m
```
Sample output
```json

```

### View the pending settings changes staged in the BDC
View the pending settings changes for the Spark service only and across the entire BDC cluster.

```bash
azdata bdc spark config settings show --filter-option=pending --include-details
```
Sample output
```json

```

```bash
azdata bdc config settings show --filter-option=pending --include-details --recursive
```
Sample output
```json

```

### Upgrade the BDC with the pending configuration settings
```bash
azdata bdc config upgrade
```
Sample output
```json

```

### Monitor the status of the BDC upgrade


## Additional steps

### Revert pending configuration settings
If you determine that you no longer want to change the pending configuration settings, you can unstage these settings. This will revert the pending settings at all scopes.

```bash
azdata bdc config settings revert
```
Sample output
```json

```
### Abort the configuration upgrade
If the configuration upgrade fails for any of the components, you can abort the upgrade process and return the BDC back to its prior configurations. Settings that were staged for change during the upgrade will again be listed as pending settings.

```bash
azdata bdc config abort-upgrade
```
Sample output
```json

```

