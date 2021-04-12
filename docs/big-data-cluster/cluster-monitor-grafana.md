---
title: Monitor cluster with Grafana Dashboard
titleSuffix: SQL Server Big Data Clusters
description: Monitoring cluster with Grafana Dashboard on SQL Server 2019 big data cluster.
author: cloudmelon
ms.author: melqin
ms.reviewer: mikeray
ms.metadata: seo-lt-2019
ms.date: 09/22/2020
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Monitor cluster with azdata and Grafana Dashboard

This article describes how to monitor an application inside a SQL Server Big Data Cluster. SQL Server Big Data Cluster exposes Grafana Dashboard for monitoring, those metrics are stored in influxDB. Those metrics are categorized as the following : 
- Kubernetes host related metrics, it's collected by Telegraf which is an agent for collecting, processing, aggregating, and writing metrics.
- BDC workloads related metrics : those metrics are the metrics related to SQL Server, Spark and HDFS are collected by CollectD, including such as SQL Server DMV metrics and [SQL server extended events (XEvents)](../relational-databases/extended-events/extended-events.md). 

# Available Metrics 

The following metrics are available in SQL Server Big Data Clusters :

|Categories |Description | metrics |
|---|---|---|
|Hosted Node Metrics|Metrics related to Kubernetes host | Those metrics including : CPU, RAM usage, Disk IOPS, Load averages etc.   |
|Pods and Containers Metrics|Metrics related to Kubernetes pods and containers, Grafana provides filter allows to filter those metrics by pods or even specific container. | Those metrics including : CPU, RAM, Disk and network usage.   |
|SQL Server Metrics|Metrics related to SQL Server | Those metrics including : Transaction/sec, Batch Requests/sec, Database Activity, SQL Server Activity etc, in particular, when ContainerAG is enabled, you can also monitor the alwaysOn from here.   |
|Spark Metrics |Metrics related to Spark apps. | Those metrics including : Executor hdfs writes, JVM GC time, JVM heap usage etc.   |
|Apps Metrics|Metrics related to the [Apps deployed](concept-application-deployment.md) on BDC cluster, Grafana provides filter allows to filter those metrics by specific app and app version. | Those metrics including : CPU, RAM usage and HTTP requests status   |

## Prerequisites

- [SQL Server 2019 big data cluster](deployment-guidance.md)
- [azdata command-line utility](../azdata/install/deploy-install-azdata.md)

## Capabilities

In SQL Server 2019 you can create, delete, describe, initialize, list run and update your application. The following table describes the application deployment commands that you can use with **azdata**.

|Command |Description |
|:---|:---|
|`azdata bdc endpoint list` | Lists the endpoints for the Big Data Cluster. |


You can use the following example to list the endpoint of **Grafana dashboard**:

```bash
azdata bdc endpoint list --endpoint-name metricsui 
```

The output will give you the endpoint, which you can use your cluster username and password to log in. 

![Grafana Dashboard](media/big-data-cluster-monitor-apps/grafana-dashboard-endpoint.png)

The `nodeMetricsUrl` and `sqlMetricsUrl` values link to a Grafana dashboard for monitoring Kubernetes node metrics and big data cluster service metrics:

![Grafana dashboard](./media/view-cluster-status/grafana-dashboard.png)

![SQL](./media/view-cluster-status/grafana-sql-status.png)



## Next steps

For more information about [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see [What are [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]?](big-data-cluster-overview.md).