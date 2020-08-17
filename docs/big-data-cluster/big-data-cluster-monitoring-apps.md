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

Grafana is one of the best cloud-native virtualization tools which can be used to provide various monitoring metrics of your application running in Kubernetes.  You can use the following command which will give you the endpoint of **Grafana** Dashboard.  

```bash
azdata bdc endpoint list --endpoint-name metricsui 
```

The output will give you the endpoint which you can use your cluster username and password to login. 

![Grafana Dashboard](media/big-data-cluster-monitor-apps/grafana-dashboard-endpoint.png)


When you open the dashboard, go to the **Host Apps Metrics**, where you’ll get more insights about your application and keep in track.  

![Host apps metrics](media/big-data-cluster-monitor-apps/host-apps-metrics.png)


If you want to get more insight about a single pod of the application ( in case you have multiple copies of your application), you can also go to the **Host Pods Metrics** and choose the pod respect, you’ll get a view of metrics as the following:  

![Host pods metrics](media/big-data-cluster-monitor-apps/host-pods-metrics.png) 