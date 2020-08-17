---
title: Monitoring applications with azdata and Grafana Dashboard
titleSuffix: SQL Server Big Data Clusters
description: Monitoring applications with azdata and Grafana on SQL Server 2019 big data cluster.
author: cloudmelon
ms.author: melqin
ms.reviewer: mikeray
ms.metadata: seo-lt-2019
ms.date: 13/08/2020
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

Grafana is one of the best cloud-native virtualization tools which can be used to provide various monitoring metrics of your application running in Kubernetes.  

This article describes how to monitor an application inside a SQL Server 2019 big data clusters.

## Prerequisites

- [SQL Server 2019 big data cluster](deployment-guidance.md)
- [azdata command-line utility](deploy-install-azdata.md)

## Capabilities

In SQL Server 2019 you can create, delete, describe, initialize, list run and update your application. The following table describes the application deployment commands that you can use with **azdata**.

|Command |Description |
|:---|:---|
|`azdata bdc endpoint list` | Lists the endpoints for the Big Data Cluster.. |


You can use the following example tol list the endpoint of **Grafana dashboard** :

```bash
azdata bdc endpoint list --endpoint-name metricsui 
```

The output will give you the endpoint which you can use your cluster username and password to login. 

![Grafana Dashboard](media/big-data-cluster-monitor-apps/grafana-dashboard-endpoint.png)


When you open the dashboard, go to the **Host Apps Metrics**, where you’ll get more insights about your application and keep in track.  

![Host apps metrics](media/big-data-cluster-monitor-apps/host-apps-metrics.png)


If you want to get more insight about a single pod of the application ( in case you have multiple copies of your application), you can also go to the **Host Pods Metrics** and choose the pod respect, you’ll get a view of metrics as the following:  

![Host pods metrics](media/big-data-cluster-monitor-apps/host-pods-metrics.png) 


## Next steps

You can check out additional samples at [App Deploy Samples](https://aka.ms/sql-app-deploy).

For more information about [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see [What are [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]?](big-data-cluster-overview.md).
