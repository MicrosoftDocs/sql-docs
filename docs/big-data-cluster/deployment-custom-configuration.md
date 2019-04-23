---
title: Configure deployments
titleSuffix: SQL Server big data clusters
description: Learn how to customize a big data cluster deployment with configuration files.
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 04/24/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Configure deployment settings for big data clusters

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

To customize your cluster deployment configuration file, you can use any json format editor like VSCode. For scripting these edits for automation purposes, we provide a **mssqlctl cluster config section** command. This article explains how to configure big data cluster deployments by modifying deployment configuration files. It provides examples for how to change the configuration for different scenarios. For more information about how configuration files are used in deployments, see the [deployment guidance](deployment-guidance.md#configfile).

## Prerequisites

- [Install mssqlctl](deploy-install-mssqlctl.md).

- Each of the examples in this section assume that you have created a copy of one of the standard configuration files. For more information, see [Create a custom configuration file](deployment-guidance.md#customconfig). For example, the following command creates a **custom.json** file based on the default **aks-dev-test.json** configuration:

   ```bash
   mssqlctl cluster config init --src aks-dev-test.json --target custom.json
   ```

## <a id="clustername"></a> Change cluster name

The cluster name is both the name of the big data cluster and the Kubernetes namespace that will be created on deployment. It is specified in the following portion of the deployment configuration file:

```json
"metadata": {
    "kind": "Cluster",
    "name": "mssql-cluster"
},
```

The following command sends a key-value pair to the **--json-values** parameter to change the big data cluster name to **test-cluster**:

```bash
mssqlctl cluster config section set -f custom.json -j ".metadata.name=test-cluster"
```

> [!IMPORTANT]
> The name of your cluster must be only lower case alpha-numeric characters, no spaces. All Kubernetes artifacts (containers, pods, statefull sets, services) for the cluster will be created in a namespace with same name as the cluster name specified.

## <a id="ports"></a> Update endpoint ports

Endpoints are defined for the control plane as well as for individual pools. The following portion of the configuration file shows the endpoint definitions for the control plane:

```json
"endpoints": [
    {
        "name": "Controller",
        "serviceType": "LoadBalancer",
        "port": 30080
    },
    {
        "name": "ServiceProxy",
        "serviceType": "LoadBalancer",
        "port": 30777
    },
    {
        "name": "AppServiceProxy",
        "serviceType": "LoadBalancer",
        "port": 30778
    },
    {
        "name": "Knox",
        "serviceType": "LoadBalancer",
        "port": 30443
    }
]
```

The following example uses inline JSON to change the port for the **Controller** endpoint:

```bash
mssqlctl cluster config section set -f custom.json -j "$.spec.controlPlane.spec.endpoints[?(@.name==""Controller"")].port=30000"
```

## <a id="replicas"></a> Configure pool replicas

The characteristics of each pool, such as the storage pool, is defined in the configuration file. For example, the following portion shows a storage pool definition:

```json
"pools": [
    {
        "metadata": {
            "kind": "Pool",
            "name": "default"
        },
        "spec": {
            "type": "Storage",
            "replicas": 2,
            "storage": {
                "usePersistentVolume": true,
                "className": "managed-premium",
                "accessMode": "ReadWriteOnce",
                "size": "10Gi"
            }
        }
    }
]
```

You can configure the number of instances in a pool by modifying the **replicas** value for each pool. The following example uses inline JSON to change these values for the storage and data pools to `10` and `4` respectively:

```bash
mssqlctl cluster config section set -f custom.json -j "$.spec.pools[?(@.spec.type == ""Storage"")].spec.replicas=10"
mssqlctl cluster config section set -f custom.json -j "$.spec.pools[?(@.spec.type == ""Data"")].spec.replicas=4'
```

> [!IMPORTANT]
> In this release, you cannot change the number of instances in the compute pool.

## <a id="storage"></a> Configure storage

You can also change the storage class and characteristics that are used for each pool. The following example assigns a custom storage class to the storage pool:

```bash
mssqlctl cluster config section set -f custom.json -j "$.spec.pools[?(@.spec.type == ""Storage"")].spec={""replicas"": 2,""storage"": {""className"": ""newStorageClass"",""size"": ""20Gi"",""accessMode"": ""ReadWriteOnce"",""usePersistentVolume"": true},""type"": ""Storage""}"
```

The following example only updates the size of the storage pool to `32Gi`:

```bash
mssqlctl cluster config section set -f custom.json -j "$.spec.pools[?(@.spec.type == ""Storage"")].spec.storage.size=32Gi"
```

The following example updates the size of all pools to `32Gi`:

```bash
mssqlctl cluster config section set -f custom.json -j "$.spec.pools[?(@.spec.type[*])].spec.storage.size=32Gi"
```

> [!NOTE]
> A configuration file based on **kubeadm-dev-test.json** does not have a storage definition for each pool, but this can be added manually if needed.

For more information about storage configuration, see [Data persistence with SQL Server big data cluster on Kubernetes](concept-data-persistence.md).

## <a id="podplacement"></a> Configure pod placement using Kubernetes labels

You can control pod placement on Kubernetes nodes that have specific resources to accommodate various types of workload requirements. For example, you might want to ensure the storage pool pods are placed on nodes with more storage, or SQL Server master instances are placed on nodes that have higher CPU and memory resources. In this case, you will first build a heterogeneous Kubernetes cluster with different types of hardware and then [assign node labels](https://kubernetes.io/docs/concepts/overview/working-with-objects/labels/) accordingly. At the time of deploying big data cluster, you can specify same labels at pool level in the cluster deployment configuration file. Kubernetes will then take care of  affinitizing the pods on nodes that match the specified labels.

The following example shows how to edit a custom configuration file to include a node label setting for the SQL Server master instance. Note that there is no *nodeLabel* key in the built in configurations so you will need to either edit a custom configuration file manually or create a patch file and apply it to the custom configuration file.

Create a file named **patch.json** in your current directory with the following contents:

```json
{
  "patch": [
     {
      "op": "add",
      "path": "$.spec.pools[?(@.spec.type == 'Master')].spec",
      "value": {
      "nodeLabel": "<yourNodeLabel>"
       }
    }
  ]
}
```

```bash
mssqlctl cluster config section set -f custom.json -p ./patch.json
```

## <a id="jsonpatch"></a> JSON patch files

JSON patch files configure multiple settings at once. For more information about JSON patches, see [JSON Patches in Python](https://github.com/stefankoegl/python-json-patch) and the [JSONPath Online Evaluator](https://jsonpath.com/).

The following **patch.json** file performs the following changes:

- Updates the port of single endpoint.
- Updates all endpoints (**port** and **serviceType**).
- Updates the control plane storage.
- Updates the storage class name in control plane storage.
- Updates pool storage, including replicas (storage pool).
- Updates Spark settings for a specific pool (storage pool).

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

> [!TIP]
> For more information about the structure and options for changing a deployment configuration file, see [Deployment configuration file reference for big data clusters](reference-deployment-config.md).

Use **mssqlctl cluster config section set** to apply the changes in the JSON patch file. The following example applies the **patch.json** file to a target deployment configuration file **custom.json**.

```bash
mssqlctl cluster config section set -f custom.json -p ./patch.json
```

## Next steps

For more information about using configuration files in big data cluster deployments, see [How to deploy SQL Server big data clusters on Kubernetes](deployment-guidance.md#configfile).
