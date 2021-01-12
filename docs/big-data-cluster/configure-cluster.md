---
title: Configure cluster
titleSuffix: Configure a SQL Server big data cluster
description: Overview of how to configure a SQL Server big data cluster
author: MikeRayMSFT 
ms.author: mikeray
ms.reviewer: rahul.ajmera
ms.date: 08/04/2020
ms.topic: overview
ms.prod: sql
ms.technology: big-data-cluster
---

# Configure a SQL Server Big Data Cluster

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

You can configure properties for the SQL Server master instance, the Apache Spark, and Apache Hadoop in SQL Server 2019 Big Data Cluster at deployment time.

After deployment, Big Data Clusters does not support modifying the configuration properties.

## Configuration scopes
Big Data Clusters configuration has two scoping levels: `service`, and `resource`. The hierarchy of the settings follows in this order as well, from highest to lowest. BDC components will take the value of the setting defined at the lowest scope. If the setting is not defined at a given scope, it will inherit the value from its higher parent scope.

For example, you may want to define the default number of cores the Spark driver will use in the Storage Pool and Sparkhead `resources`. You can do this in two ways: 
- Specify a default cores value at the `Spark` service scope  
- Specify a default cores value at the `storage-0` and `sparkhead` resource scope

In the first scenario, all lower-scoped resources of the Spark service (Storage Pool and Sparkhead) will *inherit* the default number of cores from the Spark service default value.

In the second scenario, each resource will use the value defined at its respective scope.

If the default number of cores is configured at both service and resource scope, then the resource-scoped value will override the service-scoped value since this is the lowest **user configured** scope for the given setting.

For specific information about configuration, see the appropriate articles:

How to: 
- [Configure master instance of [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]](configure-sql-server-master-instance.md)
- [Configure Apache Spark and Apache Hadoop in Big Data Clusters](configure-spark-hdfs.md)

Reference: 
- [SQL Server master instance configuration properties](reference-config-master-instance.md)
- [Apache Spark & Apache Hadoop (HDFS) configuration properties](reference-config-spark-hadoop.md)
