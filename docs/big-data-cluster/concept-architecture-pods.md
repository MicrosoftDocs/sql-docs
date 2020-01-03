---
title: Deployed pods
titleSuffix: SQL Server Big Data Clusters
description: A description of the pods typically deployed in a SQL Server Big Data Cluster.
author: mihaelablendea 
ms.author: mihaelab
ms.reviewer: mikeray
ms.date: 01/05/2020
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Pods deployed with Big Data Cluster

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

This article describes the pods deployed by a typical SQL Server Big Data Cluster deployment.

## Pods

The following table lists the pods that are typically deployed in a Big Data Cluster. 

|Name  |Count  |Type  |Description  |
|---------|---------|---------|---------|
|appproxy-\<*nnnn*\>|1         |         |Application proxy.|
|compute-\<*#*\>-\<*#*\>|1         |         |Compute.|
|control-\<*nnnn*\>|1         |         |Kubernetes control.|
|controldb-\<*#*\>|1         |         |         |
|controlwd-\<*nnnn*\>|1         |         |         |
|data-\<*#*\>-\<*#*\>|2         |         |         |
|gateway-\<*#*\>|1         |         |         |
|logsdb-\<*#*\>|1         |         |         |
|logsui-\<*nnnn*\>|1         |         |         |
|master-\<*#*\>|3         |         |Master SQL Server instance. 3 replicas provide HA.|
|metricsdb-\<*#*\>|1         |         |         |
|metricsdc-\<*nnnn*\>|5         |         |         |
|metricsui-\<*nnnn*\>|1         |         |         |
|mgmtproxy-\<*nnnn*\>|1         |         |         |
|nmnode-\<*#*\>-\<*#*\>|2         |         |         |
|operator-\<*nnnn*\>|1         |         |         |
|sparkhead-\<*#*\>|2         |         |         |
|storage-\<*#*\>-\<*#*\>|3         |         |         |
|zookeeper-\<*#*\>|3         |         |         |

## Next steps

To learn more about the [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see the following resources:

- [What are [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]?](big-data-cluster-overview.md)
- [Workshop: Microsoft [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] Architecture](https://github.com/Microsoft/sqlworkshops/tree/master/sqlserver2019bigdataclusters)
