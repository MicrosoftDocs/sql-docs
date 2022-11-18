---
title: Run applications with azdata
titleSuffix: SQL Server Big Data Clusters
description: Running applications with azdata on SQL Server 2019 big data clusters.
author: HugoMSFT
ms.author: hudequei
ms.reviewer: wiassaf
ms.date: 08/16/2020
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
ms.metadata: seo-lt-2019
---

# Run apps with azdata - SQL Server Big Data Clusters

This article describes how to run an application inside a SQL Server Big Data Clusters.

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

## Prerequisites

- [SQL Server 2019 big data cluster](deployment-guidance.md)
- [[!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)]](../azdata/install/deploy-install-azdata.md)

## Capabilities

In SQL Server 2019 you can create, delete, describe, initialize, list run and update your application. The following table describes the application deployment commands that you can use with **azdata**.

|Command |Description |
|:---|:---|
|`azdata app describe` | Describe application. |
|`azdata app run` | Run application. |


The following sections describe these commands in more detail.


## Run an app

If the app is in a `Ready` state, you can use it by running it with your specified input parameters. Use the following syntax to run an app:

```bash
azdata app run --name <app_name> --version <app_version> --inputs <inputs_params>
```

The following example command demonstrates the run command:

```bash
azdata app run --name add-app --version v1 --inputs x=1,y=2
```

If the run was successful, you should see your output as specified when you created the app. The following output is an example.

```json
{
  "changedFiles": [],
  "consoleOutput": "",
  "errorMessage": "",
  "outputFiles": {},
  "outputParameters": {
    "result": 3
  },
  "success": true
}
```


## Describe an app

The describe command provides detailed information about the app including the end point in your cluster. This is typically used by an app developer to build an app using the swagger client and using the webservice to interact with the app in a RESTful manner. For more information, see [Consume applications on big data clusters](app-consume.md) for more information.

```json
{
  "input_param_defs": [
    {
      "name": "x",
      "type": "int"
    },
    {
      "name": "y",
      "type": "int"
    }
  ],
  "links": {
    "app": "https://10.1.1.3:30080/api/app/add-app/v1",
    "swagger": "https://10.1.1.3:30080/api/app/add-app/v1/swagger.json"
  },
  "name": "add-app",
  "output_param_defs": [
    {
      "name": "result",
      "type": "int"
    }
  ],
  "state": "Ready",
  "version": "v1"
}
```

Explore how to integrate apps deployed on [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] in your own applications at [Consume applications on big data clusters](app-consume.md) for more information. You can also check out additional samples at [App Deploy Samples](https://aka.ms/sql-app-deploy).

For more information about [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see [Introducing [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]](big-data-cluster-overview.md).