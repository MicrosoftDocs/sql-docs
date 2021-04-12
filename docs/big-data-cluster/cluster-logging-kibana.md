---
title: Check out cluster logs with Kibana Dashboard
titleSuffix: SQL Server Big Data Clusters
description: Monitoring cluster with Kibana Dashboard on SQL Server 2019 big data cluster.
author: cloudmelon
ms.author: melqin
ms.reviewer: mikeray
ms.metadata: seo-lt-2019
ms.date: 02/25/2021
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Check out cluster logs with Kibana Dashboard

This article describes how to virtualise the logs an application inside [!INCLUDE[ssbigdataclusters-ss-nover](../includes/ssbigdataclusters-ss-nover.md)]. SQL Server Big Data Cluster uses Fluent Bit which is an open source log processor and forwarder, it fetches the logs from BDC components in the cluster and stores them in ElasticSearch. From Kibana Dashboard, you can virtualise and search the log of your interest.

# BDC logs stored in Elastic Search

BDC related logs stored in Elastic Search are categorised as the following : 
- Kubernetes host related logs, including all the logs by pods, by namespace, per namespace.
- BDC workloads related logs : those metrics are the metrics related to SQL Server, Spark and HDFS are collected by CollectD, including such as SQL Server DMV metrics. 

## Prerequisites

- [[!INCLUDE[ssbigdataclusters-ss-nover](../includes/ssbigdataclusters-ss-nover.md)]](deployment-guidance.md)
- [azdata command-line utility](../azdata/install/deploy-install-azdata.md)

## Capabilities

In [!INCLUDE[sssql19-md](../includes/sssql19-md.md)] you can create, delete, describe, initialize, list run and update your application. The following table describes the application deployment commands that you can use with **azdata**.

|Command |Description |
|:---|:---|
|`azdata bdc endpoint list` | Lists the endpoints for the [!INCLUDE[ssbigdataclusters-ss-nover](../includes/ssbigdataclusters-ss-nover.md)]. |


You can use the following example to list the endpoint of **Kibana dashboard**:

```bash
azdata bdc endpoint list --endpoint-name logsui 
```

The output will give you the endpoint, which you can use your cluster username and password to log in. 

![Kibana Dashboard](media/big-data-cluster-monitor-cluster/kibana-dashboard-endpoint.png)


The link to a Kibana dashboard:

![Kibana dashboard](./media/view-cluster-status/kibana-dashboard.png)

> [!NOTE]
> The older Microsoft Edge browser is incompatible with Kibana, you must use the Edge chromium-based browser for the dashboard to display correctly. You will see a blank page when loading the dashboards using an unsupported browser, see [supported browsers for Kibana](https://www.elastic.co/support/matrix#matrix_browsers).


From Kibana Dashboard, you can use filters such as 'kubernetes_container_name', 'kubernetes_pod_name', 'log_filename' and 'service_name' to help you quickly virtualise all the logs such as logs from BDC controller, from SQL Server or any logs from different pods, services and more. 


## Next steps

For more information about [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see [What are [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]?](big-data-cluster-overview.md).


