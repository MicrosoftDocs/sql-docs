---
title: SQL Server Big Data Clusters Configuration Overview
titleSuffix: SQL Server big data clusters
description: Big Data Clusters Configuration Overview
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: rahul.ajmera
ms.date: 08/04/2020
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# Configure a SQL Server Big Data Cluster

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]
Configuration management enables administrators to ensure their Big Data Cluster is always prepared for their workload needs. With this functionality, cluster administrators can alter or tune various parts of the Big Data Cluster at deployment time or post-deployment and get deeper insight into the configurations running in their BDC. 

Configuration management allows an adminstrator to enable SQL Agent, define the baseline resources for their organization’s Spark jobs, or even see what settings are configurable at each scope. At deployment time, configurations can be configured through the deployment `bdc.json` file and post-deployment through azdata CLI.

## Configuration Scopes
Big Data Clusters configuration has three scoping levels: `cluster`, `service`, and `resource`. The hierarchy of the settings follows in this order as well, from highest to lowest. BDC components will take the value of the setting defined at the lowest scope. If the setting is not defined at a given scope, it will inherit the value from its higher parent scope.

For example, you may want to define the default number of cores the Spark driver will use in the Storage Pool and Sparkhead `resources`. You can do this in two ways: 
    1) Specify a default cores value at the `Spark` service scope 
    2) Specify a default cores value at the `storage-0` and `sparkhead` resource scope

In the first scenario, all lower-scoped resources of the Spark service (Storage Pool and Sparkhead) will *inherit* the default number of cores from the Spark service default value.

In the second scenario, each resource will use the value defined at its respective scope.

If the default number of cores is configured at both service and resource scope, then the resource-scoped value will override the service-scoped value since this is the lowest **user configured** scope for the given setting.

For specific information about configuration, see the appropriate articles:

How to: 
<!-- - [Configure master instance of [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]](configure-sql-server-master-instance.md)
- [Configure Apache Spark and Apache Hadoop in Big Data Clusters](configure-spark-hdfs.md) -->

- [Configure Big Data Clusters at deplyoment time](configure-bdc-deployment.md)
- [Configure Big Data Clusters post-deployment](configure-bdc-postdeployment.md)
- [Configure Big Data Clusters - CU8 release and earlier](configure-bdc-pre-configuration.md)

Reference: 
- [SQL Server Big Data Clusters configuration properties](reference-config-bdc-overview.md)
- [Apache Spark & Apache Hadoop (HDFS) configuration properties](reference-config-spark-hadoop.md)
- [SQL Server Master Instance Configuration Properties - Pre CU9 Release](reference-config-master-instance.md)