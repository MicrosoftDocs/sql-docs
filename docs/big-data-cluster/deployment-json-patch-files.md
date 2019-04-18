---
title: Customize deployment with a JSON patch file
titleSuffix: SQL Server big data clusters
description: Learn how to customize a big data cluster deployment configuration file by applying a JSON patch file.
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 04/24/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Customize a big data cluster deployment with a JSON patch file

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

This article explains how to use a JSON patch file to modify a big data cluster [deployment configuration file](deployment-guidance.md#configfile). A JSON patch file contains 

## Prerequisites

[Install mssqlctl](deploy-install-mssqlctl.md).

## Create the JSON patch file

Create a file named **patch.json** in your current directory with the following contents:

```json
{
  "patch": [
    {
      "op": "replace",
      "path": "$.spec.controlPlane.spec.endpoints[?(@.name=='Controller')].port",
      "value": 30000
    },
    {
      "op": "replace",
      "path": "spec.controlPlane.spec.endpoints",
      "value": [
        {
          "serviceType": "LoadBalancer",
          "port": 30001,
          "name": "Controller"
        },
        {
            "serviceType": "LoadBalancer",
            "port": 30778,
            "name": "ServiceProxy"
        },
        {
            "serviceType": "LoadBalancer",
            "port": 30778,
            "name": "AppServiceProxy"
        },
        {
            "serviceType": "LoadBalancer",
            "port": 30443,
            "name": "Knox"
        }
      ]
    },
    {
      "op": "replace",
      "path": "spec.controlPlane.spec.storage",
      "value": {
        "usePersistentVolume":true,
        "accessMode":"ReadWriteMany",
        "className":"managed-premium",
        "size":"10Gi"
      }
    },
    {
      "op": "replace",
      "path": "spec.controlPlane.spec.storage.className",
      "value": "default"
    },
    {
      "op": "replace",
      "path": "$.spec.pools[?(@.spec.type == 'Storage')].spec",
      "value": {
        "replicas": 2,
        "type": "Storage",
        "storage": {
          "usePersistentVolume": true,
          "accessMode": "ReadWriteOnce",
          "className": "managed-premium",
          "size": "10Gi"
        }
      }
    },
    {
      "op": "replace",
      "path": "$.spec.pools[?(@.spec.type == 'Storage')].hadoop.spark",
      "value": {
        "driverMemory": "2g",
        "driverCores": 1,
        "executorInstances": 3,
        "executorCores": 1,
        "executorMemory": "1536m"
      }
    }
  ]
}
```

This example JSON patch file makes the following changes:

- Updates the port of single endpoint.

- Updates all endpoints (**port** and **serviceType**).

   > [!NOTE]
   > You must update the entire array to update all the endpoints. Note that this overrides the first port change, but this example is meant to understand your options.

- Updates the control plane storage.

- Updates the storage class name in control plane storage.

- Updates pool storage, including replicas (storage pool).

- Updates Spark settings for a specific pool (storage pool).

For more information about the structure and options for changing a deployment configuration file, see [Deployment configuration file reference for big data clusters](reference-deployment-config.md).

## Apply the patch file

Use **mssqlctl cluster config section set** to apply the changes in the JSON patch file. The following example applies the **patch.json** file to a target deployment configuration file **custom.json**.

```bash
mssqlctl cluster config section set -f custom.json -p ./patch.json
```

## Next steps

For more information about JSON patches, see [JSON Patches in Python](https://github.com/stefankoegl/python-json-patch) and the [JSONPath Online Evaluator](https://jsonpath.com/).

For more information about using configuration files in big data cluster deployments, see [How to deploy SQL Server big data clusters on Kubernetes](deployment-guidance.md#configfile).