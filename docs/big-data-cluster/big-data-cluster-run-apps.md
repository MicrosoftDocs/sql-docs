---
title: Running applications with azdata 
titleSuffix: SQL Server Big Data Clusters
description: Running applications with azdata on SQL Server 2019 big data clusters.
author: cloudmelon
ms.author: melqin
ms.reviewer: mikeray
ms.metadata: seo-lt-2019
ms.date: 13/08/2020
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

This article describes how to run an application inside a SQL Server 2019 big data clusters.

## Prerequisites

- [SQL Server 2019 big data cluster](deployment-guidance.md)
- [azdata command-line utility](deploy-install-azdata.md)

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

If the run was successful, you should see your output as specified when you created the app. The following is an example.

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

The describe command provides detailed information about the app including the end point in your cluster. This is typically used by an app developer to build an app using the swagger client and using the webservice to interact with the app in a RESTful manner. See [Consume applications on big data clusters](big-data-cluster-consume-apps.md) for more information.

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

Explore how to integrate apps deployed on [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] in your own applications at [Consume applications on big data clusters](big-data-cluster-consume-apps.md) for more information. You can also check out additional samples at [App Deploy Samples](https://aka.ms/sql-app-deploy).

For more information about [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see [What are [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]?](big-data-cluster-overview.md).
