---
title: SQL Server Big Data Clusters Configuration Overview
titleSuffix: SQL Server Big Data Clusters
description: Big Data Clusters Configuration Overview
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: rahul.ajmera
ms.date: 02/11/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: reference
---

# Configure a SQL Server Big Data Cluster

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

Configuration management enables administrators to ensure their big data cluster is always prepared for their workload needs. With this functionality, cluster administrators can alter or tune various parts of the big data cluster at deployment time or post-deployment and get deeper insight into the configurations running in their big data cluster. 

Configuration management allows an administrator to enable SQL Agent, define the baseline resources for their organization's Spark jobs, or even see what settings are configurable at each scope. At deployment time, configurations can be configured through the deployment `bdc.json` file and post-deployment through azdata CLI.

## Configuration Scopes
Big Data Clusters configuration has three scoping levels: `cluster`, `service`, and `resource`. The hierarchy of the settings follows in this order as well, from highest to lowest. BDC components will take the value of the setting defined at the lowest scope. If the setting is not defined at a given scope, it will inherit the value from its higher parent scope.

For example, you may want to define the default number of cores the Spark driver will use in the storage pool and `Sparkhead` resources. To define the default number of cores, you can do one of the following actions:

- Specify a default cores value at the `Spark` service scope

- Specify a default cores value at the `storage-0` and `sparkhead` resource scope

In the first scenario, all lower-scoped resources of the Spark service (storage pool and `Sparkhead`) will *inherit* the default number of cores from the Spark service default value.

In the second scenario, each resource will use the value defined at its respective scope.

If the default number of cores is configured at both service and resource scope, then the resource-scoped value will override the service-scoped value since this is the lowest **user configured** scope for the given setting.

## Next steps

For specific information about configuration, see the appropriate articles:

- [Customize Big Data Clusters deployment](deployment-custom-configuration.md)
- [Configure Big Data Clusters post-deployment](configure-bdc-postdeployment.md)
- [Configure Big Data Clusters - CU8 release and earlier](configure-bdc-pre-configuration.md)

Reference: 
- [SQL Server Big Data Clusters configuration properties](reference-config-bdc-overview.md)
- [Apache Spark & Apache Hadoop (HDFS) configuration properties](reference-config-spark-hadoop.md)
- [SQL Server Master Instance Configuration Properties - Pre CU9 Release](reference-config-master-instance.md)
- [azdata CLI](../azdata/reference/reference-azdata.md)