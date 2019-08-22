---
title: Configure deployments
titleSuffix: SQL Server big data clusters
description: Learn how to customize a big data cluster deployment with configuration files.
author: MikeRayMSFT 
ms.author: mikeray
ms.reviewer: mihaelab
ms.date: 08/21/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---


# Configure deployment settings for cluster resources and services

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

Starting from a pre-defined set of configuration profiles that are built-in in the azdata management tool, you can easily modify the default settings to better suit your BDC workload requirements. Starting with the release candidate release, the structure of the configuration files was updated to enable you to granularly update settings per each service of the resource. 

You can also set resource level configurations or update the configurations for all services in a resource. Here is a summary of the structure for **bdc.json**:

```json
{
    "apiVersion": "v1",
    "metadata": {
        "kind": "BigDataCluster",
        "name": "mssql-cluster"
    },
    "spec": {
        "resources": {
            "nmnode-0": {...
            },
            "sparkhead": {...
            },
            "zookeeper": {...
            },
            "gateway": {...
            },
            "appproxy": {...
            },
            "master": {...
            },
            "compute-0": {...
            },
            "data-0": {...
            },
            "storage-0": {...
        },
        "services": {
            "sql": {
                "resources": [
                    "master",
                    "compute-0",
                    "data-0",
                    "storage-0"
                ]
            },
            "hdfs": {
                "resources": [
                    "nmnode-0",
                    "zookeeper",
                    "storage-0",
                    "sparkhead"
                ],
                "settings": {...
            },
            "spark": {
                "resources": [
                    "sparkhead",
                    "storage-0"
                ],
                "settings": {...
            }
        }
    }
}
```

For updating resource level configurations like instances in a pool, you will update the resource spec. For example:
```json
"resources": {
    "compute-0": {
        "metadata": {
            "kind": "Pool",
            "name": "default"
        },
        "spec": {
            "type": "Compute",
            "replicas": 4
        }
    }
}
``` 

Similarly for changing the default settings of a service only for a specific resource. For example, if you want to change the Spark memory settings only for the Spark component in the Storage pool, you will udpate the **storage-0** resource with a **settings** section for **spark** service.
```json
"resources":{
     "storage-0": {
        "metadata": {
            "kind": "Pool",
            "name": "default"
        },
        "spec": {
            "type": "Storage",
            "replicas": 2,
            "settings": {
                "spark": {
                    "driverMemory": "2g",
                    "driverCores": "1",
                    "executorInstances": "3",
                    "executorMemory": "1536m",
                    "executorCores": "1"
                }
            }
        }
    }
}
```

If you want to apply same configurations for a service associated with multiple resources, you will update the corresponding **settings** in the **services** section. For example, if you would like to set same same settings for Spark across both storage pool and Spark pools, you will update the **settings** section in the **spark** service section.

```json
"services": {
    "spark": {
        "resources": [
            "sparkhead",
            "storage-0"
        ],
        "settings": {
            "driverMemory": "2g",
            "driverCores": "1",
            "executorInstances": "3",
            "executorMemory": "1536m",
            "executorCores": "1"
        }
    }
}
```


To customize your cluster deployment configuration files, you can use any JSON format editor, such as VSCode. For scripting these edits for automation purposes, use the **azdata bdc config** command. This article explains how to configure big data cluster deployments by modifying deployment configuration files. It provides examples for how to change the configuration for different scenarios. For more information about how configuration files are used in deployments, see the [deployment guidance](deployment-guidance.md#configfile).

## Prerequisites

- [Install azdata](deploy-install-azdata.md).

- Each of the examples in this section assume that you have created a copy of one of the standard configurations. For more information, see [Create a custom configuration](deployment-guidance.md#customconfig). For example, the following command creates a directory called `custom` that contains two JSON deployment configuration files, **bdc.json** and **control.json**, based on the default **aks-dev-test** configuration:

   ```bash
   azdata bdc config init --source aks-dev-test --target custom
   ```

## <a id="clustername"></a> Change cluster name

The cluster name is both the name of the big data cluster and the Kubernetes namespace that will be created on deployment. It is specified in the following portion of the **bdc.json** deployment configuration file:

```json
"metadata": {
    "kind": "BigDataCluster",
    "name": "mssql-cluster"
},
```

The following command sends a key-value pair to the **--json-values** parameter to change the big data cluster name to **test-cluster**:

```bash
azdata bdc config replace --config-file custom/bdc.json --json-values "metadata.name=test-cluster"
```

> [!IMPORTANT]
> The name of your big data cluster must be only lower case alpha-numeric characters, no spaces. All Kubernetes artifacts (containers, pods, statefull sets, services) for the cluster will be created in a namespace with same name as the cluster name specified.

## <a id="ports"></a> Update endpoint ports

Endpoints are defined for the controller in the **control.json** and for gateway and SQL Server master instance in the corresponding sections in **bdc.json**. The following portion of the **control.json** configuration file shows the endpoint definitions for the controller:

```json
{
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
    }
  ]
}
```

The following example uses inline JSON to change the port for the **controller** endpoint:

```bash
azdata bdc config replace --config-file custom/control.json --json-values "$.spec.endpoints[?(@.name==""Controller"")].port=30000"
```

## <a id="replicas"></a> Configure pool replicas

The configurations of each resource, such as the storage pool, is defined in the **bdc.json** configuration file. For example, the following portion of the **bdc.json** shows a **storage-0** resource definition:

```json
"storage-0": {
    "metadata": {
        "kind": "Pool",
        "name": "default"
    },
    "spec": {
        "type": "Storage",
        "replicas": 2,
        "settings": {
            "spark": {
                "driverMemory": "2g",
                "driverCores": "1",
                "executorInstances": "3",
                "executorMemory": "1536m",
                "executorCores": "1"
            }
        }
    }
}
```

You can configure the number of instances in a pool by modifying the **replicas** value for each pool. The following example uses inline JSON to change these values for the storage and data pools to `10` and `4` respectively:

```bash
azdata bdc config replace --config-file custom/bdc.json --json-values "$.spec.resources.storage-0.spec.replicas=10"
azdata bdc config replace --config-file custom/bdc.json --json-values "$.spec.resources.data-0.spec.replicas=4"
```

## <a id="storage"></a> Configure storage

You can also change the storage class and characteristics that are used for each pool. The following example assigns a custom storage class to the storage pool and updates the size of the persistent volume claim for storing data to 100Gb. 
First create a patch.json file as below that includes the new *storage* section, in addition to *type* and *replicas*

```json
{
  "patch": [
    {
      "op": "replace",
      "path": "spec.resources.storage-0.spec",
      "value": {
        "type": "Storage",
        "replicas": 2,
        "storage": {
          "data": {
            "size": "100Gi",
            "className": "myStorageClass",
            "accessMode": "ReadWriteOnce"
          },
          "logs": {
            "size": "32Gi",
            "className": "myStorageClass",
            "accessMode": "ReadWriteOnce"
          }
        }
      }
    }
  ]
}
```

You can then use the **azdata bdc config patch** command to update the **bdc.json** configuration file.
```bash
azdata bdc config patch --config-file custom/bdc.json --patch ./patch.json
```

> [!NOTE]
> A configuration file based on **kubeadm-dev-test** does not have a storage definition for each pool, but you can use above process to added if needed.

For more information about storage configuration, see [Data persistence with SQL Server big data cluster on Kubernetes](concept-data-persistence.md).

## <a id="sparkstorage"></a> Configure storage pool without spark

You can also configure the storage pools to run without spark and create a separate spark pool. This enables you to scale spark compute power independent of storage. To see how to configure the spark pool, see the [JSON patch file example](#jsonpatch) at the end of this article.


By default, the **includeSpark** setting for the storage pool resource is set to true, so you must add the **includeSpark** field into the storage configuration in order to make changes. The following JSON patch file shows how to add this.

```json
{
  "patch": [
    {
      "op": "replace",
      "path": "spec.resources.storage-0.spec",
      "value": {
        "type": "Storage",
        "replicas": 2,
        "includeSpark": false
      }
    }
  ]
}
```

```bash
azdata bdc config patch --config-file custom/bdc.json --patch ./patch.json
```

## <a id="podplacement"></a> Configure pod placement using Kubernetes labels

You can control pod placement on Kubernetes nodes that have specific resources to accommodate various types of workload requirements. For example, you might want to ensure the storage pool resource pods are placed on nodes with more storage, or SQL Server master instances are placed on nodes that have higher CPU and memory resources. In this case, you will first build a heterogeneous Kubernetes cluster with different types of hardware and then [assign node labels](https://kubernetes.io/docs/concepts/overview/working-with-objects/labels/) accordingly. At the time of deploying big data cluster, you can specify same labels at pool level in the cluster deployment configuration file. Kubernetes will then take care of  affinitizing the pods on nodes that match the specified labels. The specific label key that needs to be added to the nodes in the kubernetes cluster is **mssql-cluster-wide**. The value of this label itself can be any string that you choose.

The following example shows how to edit a custom configuration file to include a node label setting for the SQL Server master instance, Compute Pool, Data Pool & Storage Pool. Note that there is no *nodeLabel* key in the built in configurations so you will need to either edit a custom configuration file manually or create a patch file and apply it to the custom configuration file. The SQL Server Master instance pod will be deployed on a node that contain a label **mssql-cluster-wide** with value **bdc-master**. The Compute Pool and Data Pool pods will be deployed on nodes that contain a label **mssql-cluster-wide** with value **bdc-sql**. The Storage Pool pods will be deployed on nodes that contain a label **mssql-cluster-wide** with value **bdc-storage**.

Create a file named **patch.json** in your current directory with the following contents:

```json
{
  "patch": [
    {
      "op": "replace",
      "path": "spec.resources.master.spec",
      "value": {
        "type": "Master",
        "replicas": 1,
        "endpoints": [
          {
            "name": "Master",
            "serviceType": "NodePort",
            "port": 31433
          }
        ],
        "settings": {
          "sql": {
            "hadr.enabled": "false",
            "hadr.headless.service.name": "master-svc"
          }
        },
        "nodeLabel": "bdc-master"
      }
    },
    {
      "op": "replace",
      "path": "spec.resources.compute-0.spec",
      "value": {
        "type": "Compute",
        "replicas": 1,
        "nodeLabel": "bdc-sql"
      }
    },
    {
      "op": "replace",
      "path": "spec.resources.data-0.spec",
      "value": {
        "type": "Data",
        "replicas": 2,
        "nodeLabel": "bdc-sql"
      }
    },
    {
      "op": "replace",
      "path": "spec.resources.storage-0.spec",
      "value": {
        "type": "Storage",
        "replicas": 3,
        "nodeLabel": "bdc-storage",
        "settings": {
          "spark": {
            "includeSpark": "true"
          }
        }
      }
    }
  ]
}
```

```bash
azdata bdc config patch --config-file custom/bdc.json --patch-file ./patch.json
```

## <a id="jsonpatch"></a> JSON patch files

JSON patch files configure multiple settings at once. For more information about JSON patches, see [JSON Patches in Python](https://github.com/stefankoegl/python-json-patch) and the [JSONPath Online Evaluator](https://jsonpath.com/).

The following **patch.json** file performs the following changes:

- Updates the port of single endpoint in **control.json**.
```json
{
  "patch": [
    {
      "op": "replace",
      "path": "$.spec.endpoints[?(@.name=='Controller')].port",
      "value": 30000
    }
  ]
}
```

- Updates all endpoints (**port** and **serviceType**) in **control.json**.
```json
{
  "patch": [
    {
      "op": "replace",
      "path": "spec.endpoints",
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
        }
      ]
    }
  ]
}
```

- Updates the controller storage settings in **control.json**. These settings are applicable to all cluster components, unless overridden at pool level.
```json
{
  "patch": [
    {
      "op": "replace",
      "path": "spec.storage",
      "value": {
        "data": {
          "className": "managed-premium",
          "accessMode": "ReadWriteOnce",
          "size": "100Gi"
        },
        "logs": {
          "className": "managed-premium",
          "accessMode": "ReadWriteOnce",
          "size": "32Gi"
        }
      }
    }
  ]
}
```

- Updates the storage class name in **control.json**.
```json
{
  "patch": [
    {
      "op": "replace",
      "path": "spec.storage.data.className",
      "value": "managed-premium"
    }
  ]
}
```

- Updates pool storage settings for storage pool in **bdc.json**.
```json
{
  "patch": [
    {
      "op": "replace",
      "path": "spec.resources.storage-0.spec",
      "value": {
        "type": "Storage",
        "replicas": 2,
        "storage": {
          "data": {
            "size": "100Gi",
            "className": "myStorageClass",
            "accessMode": "ReadWriteOnce"
          },
          "logs": {
            "size": "32Gi",
            "className": "myStorageClass",
            "accessMode": "ReadWriteOnce"
          }
        }
      }
    }
  ]
}
```

- Updates Spark settings for storage pool in **bdc.json**.
```json
{
  "patch": [
    {
      "op": "replace",
      "path": "spec.services.spark.settings",
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

- Creates a spark pool with 2 instances in **bdc.json**.
```json
{
  "patch": [
    {
      "op": "add",
      "path": "spec.resources.spark-0",
      "value": {
        "metadata": {
          "kind": "Pool",
          "name": "default"
        },
        "spec": {
          "type": "Spark",
          "replicas": 2
        }
      }
    },
    {
      "op": "add",
      "path": "spec.services.spark.resources/-",
      "value": "spark-0"
    },
    {
      "op": "add",
      "path": "spec.services.hdfs.resources/-",
      "value": "spark-0"
    },
    {
      "op": "add",
      "path": "spec.services.spark.settings",
      "value": {
        "DriverMemory": "2g",
        "DriverCores": "1",
        "ExecutorInstances": "2",
        "ExecutorMemory": "2g",
        "ExecutorCores": "1"
      }
    }
  ]
}
```

> [!TIP]
> For more information about the structure and options for changing a deployment configuration file, see [Deployment configuration file reference for big data clusters](reference-deployment-config.md).

Use **azdata bdc config** commands to apply the changes in the JSON patch file. The following example applies the **patch.json** file to a target deployment configuration file **custom/bdc.json**.

```bash
azdata bdc config patch --config-file custom/bdc.json --patch-file ./patch.json
```

## Next steps

For more information about using configuration files in big data cluster deployments, see [How to deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] on Kubernetes](deployment-guidance.md#configfile).
