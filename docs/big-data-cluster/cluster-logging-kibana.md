---
title: Check out cluster logs with Kibana Dashboard
titleSuffix: SQL Server Big Data Clusters
description: Monitoring cluster with Kibana Dashboard on SQL Server 2019 big data cluster.
author: cloudmelon
ms.author: melqin
ms.reviewer: mikeray
ms.metadata: seo-lt-2019
ms.date: 04/06/2021
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Check out cluster logs  with Kibana Dashboard

This article describes how to monitor an application inside [!INCLUDE[ssbigdataclusters-ss-nover](../includes/ssbigdataclusters-ss-nover.md)].

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

## Next steps

For more information about [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see [What are [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]?](big-data-cluster-overview.md).


