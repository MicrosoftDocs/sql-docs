---
title: Configure deployments
titleSuffix: SQL Server Big Data Clusters
description: Learn how to customize a big data cluster deployment with configuration files that are built into the azdata management tool.
author: HugoMSFT
ms.author: hudequei
ms.reviewer: wiassaf
ms.date: 09/21/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
---

# Configure deployment settings for Big Data Cluster resources and services

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

Starting from a pre-defined set of configuration profiles that are built into the [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)] management tool, you can easily modify the default settings to better suit your BDC workload requirements. The structure of the configuration files enables you to granularly update settings for each service of the resource.

> [!Note]
> Big Data Clusters version CU9+ have support for configuration management functionality. This feature enables post-deployment configurations and provides increased visibility and configurability of the cluster. Versions CU8 and lower do not have this functionality and configurations can only be done at deployment time.

Watch this 13-minute video for an overview of big data cluster configuration:

> [!VIDEO https://channel9.msdn.com/Shows/Data-Exposed/Big-Data-Cluster-Configuration/player?WT.mc_id=dataexposed-c9-niner]

> [!TIP]
> Please reference the articles on how to configure **high availability** for mission critical components like [SQL Server master](deployment-high-availability.md) or [HDFS name node](deployment-high-availability-hdfs-spark.md),  for details on how to deploy highly available services.

> [!TIP]
> Reference the [SQL Server Big Data Clusters Configuration Properties](reference-config-bdc-overview.md) article to see what settings are configurable. For versions CU8 or lower, reference [SQL Server Master Instance Configuration Properties -  Pre CU9 Release](reference-config-master-instance.md) for configurations available for the SQL Server master instance and [Apache Spark & Apache Hadoop (HDFS) configuration properties](reference-config-spark-hadoop.md) for Apache Spark and Hadoop properties.

You can also set resource level configurations or update the configurations for all services in a resource. Here is a summary of the structure for `bdc.json`:

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

For updating resource level configurations like instances in a pool, you will update the resource spec. For example, to update the number of instances in the compute pool you will modify this section in `bdc.json` configuration file:

```json
"resources": {
    ...
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
    ...
}
``` 

Similarly for changing the settings of a single service within a specific resource. For example, if you want to change the Spark memory settings only for the Spark component in the Storage pool, you will update the `storage-0` resource with a `settings` section for `spark` service in the `bdc.json` configuration file.

```json
"resources":{
    ...
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
                    "spark-defaults-conf.spark.driver.memory": "2g",
                    "spark-defaults-conf.spark.driver.cores": "1",
                    "spark-defaults-conf.spark.executor.instances": "3",
                    "spark-defaults-conf.spark.executor.memory": "1536m",
                    "spark-defaults-conf.spark.executor.cores": "1",
                    "yarn-site.yarn.nodemanager.resource.memory-mb": "18432",
                    "yarn-site.yarn.nodemanager.resource.cpu-vcores": "6",
                    "yarn-site.yarn.scheduler.maximum-allocation-mb": "18432",
                    "yarn-site.yarn.scheduler.maximum-allocation-vcores": "6",
                    "yarn-site.yarn.scheduler.capacity.maximum-am-resource-percent": "0.3"
                }
            }
        }
    }
    ...
}
```

If you want to apply same configurations for a service associated with multiple resources, you will update the corresponding `settings` in the `services` section. For example, if you would like to set same settings for Spark across both storage pool and Spark pools, you will update the `settings` section in the `spark` service section in the `bdc.json` configuration file.

```json
"services": {
    ...
    "spark": {
        "resources": [
            "sparkhead",
            "storage-0"
        ],
        "settings": {
            "spark-defaults-conf.spark.driver.memory": "2g",
            "spark-defaults-conf.spark.driver.cores": "1",
            "spark-defaults-conf.spark.executor.instances": "3",
            "spark-defaults-conf.spark.executor.memory": "1536m",
            "spark-defaults-conf.spark.executor.cores": "1",
            "yarn-site.yarn.nodemanager.resource.memory-mb": "18432",
            "yarn-site.yarn.nodemanager.resource.cpu-vcores": "6",
            "yarn-site.yarn.scheduler.maximum-allocation-mb": "18432",
            "yarn-site.yarn.scheduler.maximum-allocation-vcores": "6",
            "yarn-site.yarn.scheduler.capacity.maximum-am-resource-percent": "0.3"
        }
    }
    ...
}
```

To customize your cluster deployment configuration files, you can use any JSON format editor, such as VSCode. For scripting these edits for automation purposes, use the `azdata bdc config` command. This article explains how to configure big data cluster deployments by modifying deployment configuration files. It provides examples for how to change the configuration for different scenarios. For more information about how configuration files are used in deployments, see the [deployment guidance](deployment-guidance.md#configfile).

## Prerequisites

- [Install azdata](../azdata/install/deploy-install-azdata.md).

- Each of the examples in this section assume that you have created a copy of one of the standard configurations. For more information, see [Create a custom configuration](deployment-guidance.md#customconfig). For example, the following command creates a directory called `custom-bdc` that contains two JSON deployment configuration files, `bdc.json` and `control.json`, based on the default `aks-dev-test` configuration:

   ```bash
   azdata bdc config init --source aks-dev-test --target custom-bdc
   ```

> [!WARNING]
> The parameter ```imagePullPolicy``` is required to be set as ```"Always"``` in the deployment profile control.json file.

## <a id="docker"></a> Change default Docker registry, repository, and images tag

The built-in configuration files, specifically control.json includes a `docker` section where container registry, repository, and images tag are pre-populated. By default, images required for big data clusters are in the Microsoft Container Registry (`mcr.microsoft.com`), in the `mssql/bdc` repository:

```json
{
    "apiVersion": "v1",
    "metadata": {
        "kind": "Cluster",
        "name": "mssql-cluster"
    },
    "spec": {
        "docker": {
            "registry": "mcr.microsoft.com",
            "repository": "mssql/bdc",
            "imageTag": "2019-GDR1-ubuntu-16.04",
            "imagePullPolicy": "Always"
        },
        ...
    }
}
```

Before deployment, you can customize the `docker` settings by either directly editing the `control.json` configuration file or using `azdata bdc config` commands. For example, following commands are updating a `custom-bdc` control.json configuration file with a different `<registry>`, `<repository>` and `<image_tag>`:

```bash
azdata bdc config replace -c custom-bdc/control.json -j "$.spec.docker.registry=<registry>"
azdata bdc config replace -c custom-bdc/control.json -j "$.spec.docker.repository=<repository>"
azdata bdc config replace -c custom-bdc/control.json -j "$.spec.docker.imageTag=<image_tag>"
```

> [!TIP]
> As a best practice, you must use a version specific image tag and avoid using `latest` image tag, as this can result in version mismatch that will cause cluster health issues.

> [!TIP]
> Big data clusters deployment must have access to the container registry and repository from which to pull container images. If your environment does not have access to the default Microsoft Container Registry, you can perform an offline installation where the required images are first placed into a private Docker repository. For more information about offline installations, see [Perform an offline deployment of a SQL Server big data cluster](deploy-offline.md). Note that you must set the `DOCKER_USERNAME` and `DOCKER_PASSWORD` [environment variables](deployment-guidance.md#env) before issuing the deployment to ensure the deployment workflow has acces to your private repository to pull the images from.

## <a id="clustername"></a> Change cluster name

The cluster name is both the name of the big data cluster and the Kubernetes namespace that will be created on deployment. It is specified in the following portion of the `bdc.json` deployment configuration file:

```json
"metadata": {
    "kind": "BigDataCluster",
    "name": "mssql-cluster"
},
```

The following command sends a key-value pair to the `--json-values` parameter to change the big data cluster name to `test-cluster`:

```bash
azdata bdc config replace --config-file custom-bdc/bdc.json --json-values "metadata.name=test-cluster"
```

> [!IMPORTANT]
> The name of your big data cluster must be only lower case alpha-numeric characters, no spaces. All Kubernetes artifacts (containers, pods, statefull sets, services) for the cluster will be created in a namespace with same name as the cluster name specified.

## <a id="ports"></a> Update endpoint ports

Endpoints are defined for the controller in the `control.json` and for gateway and SQL Server master instance in the corresponding sections in `bdc.json`. The following portion of the `control.json` configuration file shows the endpoint definitions for the controller:

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

The following example uses inline JSON to change the port for the `controller` endpoint:

```bash
azdata bdc config replace --config-file custom-bdc/control.json --json-values "$.spec.endpoints[?(@.name==""Controller"")].port=30000"
```

## <a id="replicas"></a> Configure scale

The configurations of each resource, such as the storage pool, is defined in the `bdc.json` configuration file. For example, the following portion of the `bdc.json` shows a `storage-0` resource definition:

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
                "spark-defaults-conf.spark.driver.memory": "2g",
                "spark-defaults-conf.spark.driver.cores": "1",
                "spark-defaults-conf.spark.executor.instances": "3",
                "spark-defaults-conf.spark.executor.memory": "1536m",
                "spark-defaults-conf.spark.executor.cores": "1",
                "yarn-site.yarn.nodemanager.resource.memory-mb": "18432",
                "yarn-site.yarn.nodemanager.resource.cpu-vcores": "6",
                "yarn-site.yarn.scheduler.maximum-allocation-mb": "18432",
                "yarn-site.yarn.scheduler.maximum-allocation-vcores": "6",
                "yarn-site.yarn.scheduler.capacity.maximum-am-resource-percent": "0.3"
            }
        }
    }
}
```

You can configure the number of instances in a storage, compute and/or data pool by modifying the `replicas` value for each pool. The following example uses inline JSON to change these values for the storage, compute and data pools to `10`, `4` and `4` respectively:

```bash
azdata bdc config replace --config-file custom-bdc/bdc.json --json-values "$.spec.resources.storage-0.spec.replicas=10"
azdata bdc config replace --config-file custom-bdc/bdc.json --json-values "$.spec.resources.compute-0.spec.replicas=4"
azdata bdc config replace --config-file custom-bdc/bdc.json --json-values "$.spec.resources.data-0.spec.replicas=4"
```

> [!NOTE]
> The maximum number of instances validated for compute and data pools is `8` each. There is no enforcement of this limit at deployment time, but we do not recommend configuring a higher scale in production deployments.

## <a id="storage"></a> Configure storage

You can also change the storage class and characteristics that are used for each pool. The following example assigns a custom storage class to the storage and data pools and updates the size of the persistent volume claim for storing data to 500 Gb for HDFS (storage pool) and 100 Gb for master and data pool. 

> [!TIP]
> For more information about storage configuration, see [Data persistence with SQL Server big data cluster on Kubernetes](concept-data-persistence.md).

First create a patch.json file as below that adjust the *storage* settings

```json
{
        "patch": [
                {
                        "op": "add",
                        "path": "spec.resources.storage-0.spec.storage",
                        "value": {
                                "data": {
                                        "size": "500Gi",
                                        "className": "default",
                                        "accessMode": "ReadWriteOnce"
                                },
                                "logs": {
                                        "size": "30Gi",
                                        "className": "default",
                                        "accessMode": "ReadWriteOnce"
                                }
                        }
                },
        {
                        "op": "add",
                        "path": "spec.resources.master.spec.storage",
                        "value": {
                                "data": {
                                        "size": "100Gi",
                                        "className": "default",
                                        "accessMode": "ReadWriteOnce"
                                },
                                "logs": {
                                        "size": "30Gi",
                                        "className": "default",
                                        "accessMode": "ReadWriteOnce"
                                }
                        }
                },
                {
                        "op": "add",
                        "path": "spec.resources.data-0.spec.storage",
                        "value": {
                                "data": {
                                        "size": "100Gi",
                                        "className": "default",
                                        "accessMode": "ReadWriteOnce"
                                },
                                "logs": {
                                        "size": "30Gi",
                                        "className": "default",
                                        "accessMode": "ReadWriteOnce"
                                }
                        }
                }
        ]
}

```

You can then use the `azdata bdc config patch` command to update the `bdc.json` configuration file.
```bash
azdata bdc config patch --config-file custom-bdc/bdc.json --patch ./patch.json
```

> [!NOTE]
> A configuration file based on `kubeadm-dev-test` does not have a storage definition for each pool, but you can use above process to added if needed.

## <a id="sparkstorage"></a> Configure storage pool without spark

You can also configure the storage pools to run without spark and create a separate spark pool. This configuration enables you to scale spark compute power independent of storage. To see how to configure the spark pool, see the [Create a spark pool](#sparkpool) section in this article.

> [!NOTE]
> Deploying a big data cluster without Spark is not supported. So you must either have `includeSpark` set to `true` or you must create a separate [spark pool](#sparkpool) with at least one instance. You can also have Spark running both in storage pool (`includeSpark` is `true`) and have a separate Spark pool.

By default, the `includeSpark` setting for the storage pool resource is set to true, so you must edit the `includeSpark` field into the storage configuration in order to make changes. The following command shows how to edit this value using inline json.

```bash
azdata bdc config replace --config-file custom-bdc/bdc.json --json-values "$.spec.resources.storage-0.spec.settings.spark.includeSpark=false"
```

## <a id="sparkpool"></a> Create a spark pool

You can create a Spark pool in addition, or instead of Spark instances running in the storage pool. Following example shows how to create a spark pool with two instances by patching the `bdc.json` configuration file. 

First, create a `spark-pool-patch.json` file as below:

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
        }
    ]
}
```

Then run the `azdata bdc config patch` command:

```bash
azdata bdc config patch -c custom-bdc/bdc.json -p spark-pool-patch.json
```

## <a id="podplacement"></a> Configure pod placement using Kubernetes labels

You can control pod placement on Kubernetes nodes that have specific resources to accommodate various types of workload requirements. Using Kubernetes labels, you can customize which are the nodes in your Kubernetes cluster will be used for deploying big data cluster resources, but also restrict which nodes are used for specific resources.
For example, you might want to ensure the storage pool resource pods are placed on nodes with more storage, while SQL Server master instances are placed on nodes that have higher CPU and memory resources. In this case, you will first build a heterogeneous Kubernetes cluster with different types of hardware and then [assign node labels](https://kubernetes.io/docs/concepts/overview/working-with-objects/labels/) accordingly. At the time of deploying big data cluster, you can specify same labels at cluster level to indicate which nodes are used for big data cluster using the `clusterLabel` attribute in the `control.json` file. Then, different labels will be used for pool level placement. These labels can be specified in the big data cluster deployment configuration files using `nodeLabel` attribute. Kubernetes assigns the pods on nodes that match the specified labels. The specific label keys that needs to be added to the nodes in the kubernetes cluster are `mssql-cluster` (for indicating which nodes are used for big data cluster) and `mssql-resource` (to indicate which specific nodes the pods are placed on for various resources). The values of these labels can be any string that you choose.

> [!NOTE]
> Due to the nature of the pods that do node level metrics collection, `metricsdc` pods are deployed on all nodes with the `mssql-cluster` label, and the `mssql-resource` will not apply to these pods.

The following example shows how to edit a custom configuration file to include a node label `bdc` for the entire big data cluster, a label `bdc-master` for placing SQL Server master instance pods on a specific node, `bdc-storage-pool` for storage pool resources, `bdc-compute-pool` for compute pool and data pool pods, and `bdc-shared` for rest of the resources. 

First label the Kubernetes nodes:

```bash
kubectl label node <kubernetesNodeName1> mssql-cluster=bdc mssql-resource=bdc-shared --overwrite=true
kubectl label node <kubernetesNodeName2> mssql-cluster=bdc mssql-resource=bdc-master --overwrite=true
kubectl label node <kubernetesNodeName3> mssql-cluster=bdc mssql-resource=bdc-compute-pool --overwrite=true
kubectl label node <kubernetesNodeName4> mssql-cluster=bdc mssql-resource=bdc-compute-pool --overwrite=true
kubectl label node <kubernetesNodeName5> mssql-cluster=bdc mssql-resource=bdc-storage-pool --overwrite=true
kubectl label node <kubernetesNodeName6> mssql-cluster=bdc mssql-resource=bdc-storage-pool --overwrite=true
kubectl label node <kubernetesNodeName7> mssql-cluster=bdc mssql-resource=bdc-storage-pool --overwrite=true
kubectl label node <kubernetesNodeName8> mssql-cluster=bdc mssql-resource=bdc-storage-pool --overwrite=true
```

Then update the cluster deployment configuration files to include the label values. This example assumes that you are customizing configuration files in a `custom-bdc` profile. By default, there are no `nodeLabel` and `clusterLabel` keys in the built-in configurations so you will need to either edit a custom configuration file manually or use the `azdata bdc config add` commands to make the necessary edits.

```bash
azdata bdc config add -c custom-bdc/control.json -j "$.spec.clusterLabel=bdc"
azdata bdc config add -c custom-bdc/control.json -j "$.spec.nodeLabel=bdc-shared"

azdata bdc config add -c custom-bdc/bdc.json -j "$.spec.resources.master.spec.nodeLabel=bdc-master"
azdata bdc config add -c custom-bdc/bdc.json -j "$.spec.resources.compute-0.spec.nodeLabel=bdc-compute-pool"
azdata bdc config add -c custom-bdc/bdc.json -j "$.spec.resources.data-0.spec.nodeLabel=bdc-compute-pool"
azdata bdc config add -c custom-bdc/bdc.json -j "$.spec.resources.storage-0.spec.nodeLabel=bdc-storage-pool"

# below can be omitted in which case we will take the node label default from the control.json
azdata bdc config add -c custom-bdc/bdc.json -j "$.spec.resources.nmnode-0.spec.nodeLabel=bdc-shared"
azdata bdc config add -c custom-bdc/bdc.json -j "$.spec.resources.sparkhead.spec.nodeLabel=bdc-shared"
azdata bdc config add -c custom-bdc/bdc.json -j "$.spec.resources.zookeeper.spec.nodeLabel=bdc-shared"
azdata bdc config add -c custom-bdc/bdc.json -j "$.spec.resources.gateway.spec.nodeLabel=bdc-shared"
azdata bdc config add -c custom-bdc/bdc.json -j "$.spec.resources.appproxy.spec.nodeLabel=bdc-shared"
```
>[!NOTE]
> Best practice avoids giving the Kubernetes master any of the above BDC roles. If you plan on assigning these roles to the Kubernetes master node anyway, you'll need to [remove its ``master:NoSchedule`` taint.](https://kubernetes.io/docs/concepts/configuration/taint-and-toleration/) Be aware that this could overload the master node and inhibit its ability to perform its Kubernetes management duties on larger clusters. It's normal to see some pods scheduled to the master on any deployment: they already tolerate the ``master:NoSchedule`` taint, and they're mostly used to help manage the cluster. 

## <a id="jsonpatch"></a> Other customizations using JSON patch files

JSON patch files configure multiple settings at once. For more information about JSON patches, see [JSON Patches in Python](https://github.com/stefankoegl/python-json-patch) and the [JSONPath Online Evaluator](https://jsonpath.com/).

The following `patch.json` files perform the following changes:

- Update the port of single endpoint in `control.json`.

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

- Update all endpoints (`port` and `serviceType`) in `control.json`.

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

- Update the controller storage settings in `control.json`. These settings are applicable to all cluster components, unless overridden at pool level.

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

- Update the storage class name in `control.json`.

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

- Update pool storage settings for storage pool in `bdc.json`.

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

- Update Spark settings for storage pool in `bdc.json`.

```json
{
  "patch": [
    {
      "op": "replace",
      "path": "spec.services.spark.settings",
      "value": {
            "spark-defaults-conf.spark.driver.memory": "2g",
            "spark-defaults-conf.spark.driver.cores": "1",
            "spark-defaults-conf.spark.executor.instances": "3",
            "spark-defaults-conf.spark.executor.memory": "1536m",
            "spark-defaults-conf.spark.executor.cores": "1",
            "yarn-site.yarn.nodemanager.resource.memory-mb": "18432",
            "yarn-site.yarn.nodemanager.resource.cpu-vcores": "6",
            "yarn-site.yarn.scheduler.maximum-allocation-mb": "18432",
            "yarn-site.yarn.scheduler.maximum-allocation-vcores": "6",
            "yarn-site.yarn.scheduler.capacity.maximum-am-resource-percent": "0.3"
      }
    }
  ]
}
```

> [!TIP]
> For more information about the structure and options for changing a deployment configuration file, see [Deployment configuration file reference for big data clusters](reference-deployment-config.md).

Use `azdata bdc config` commands to apply the changes in the JSON patch file. The following example applies the `patch.json` file to a target deployment configuration file `custom-bdc/bdc.json`.

```bash
azdata bdc config patch --config-file custom-bdc/bdc.json --patch-file ./patch.json
```

## Disable ElasticSearch to run in privileged mode

By default, ElasticSearch container runs in privilege mode in big data cluster. This setting ensures that at container initialization time, the container has enough permissions to update a setting on the host required when ElasticSearch processes higher amount of logs. You can find more information about this topic in [this article](https://www.elastic.co/guide/en/elasticsearch/reference/current/vm-max-map-count.html). 

For disabling the container that runs ElasticSearch to run in privileged mode, you  must updated the `settings` section in the `control.json` and specify the value of `vm.max_map_count` to `-1`. Here is a sample of how this section would look like:

```json
{
    "apiVersion": "v1",
    "metadata": {...},
    "spec": {
        "docker": {...},
        "storage": {...},
        "endpoints": [...],
        "settings": {
            "ElasticSearch": {
                "vm.max_map_count": "-1"
            }
        }
    }
}
```

You can manually edit the `control.json` and add the above section to the `spec`, or you can create a patch file `elasticsearch-patch.json` like below and use [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)] to patch the `control.json` file:

```json
{
  "patch": [
    {
      "op": "add",
      "path": "spec.settings",
      "value": {
            "ElasticSearch": {
                "vm.max_map_count": "-1"
        }
      }
    }
  ]
}
```

Run this command to patch the configuration file:

```bash
azdata bdc config patch --config-file custom-bdc/control.json --patch-file elasticsearch-patch.json
```

> [!IMPORTANT]
> We recommend as a best practice to manually update the `max_map_count` setting manually on each host in the Kubernetes cluster as per instructions in [this article](https://www.elastic.co/guide/en/elasticsearch/reference/current/vm-max-map-count.html).

## Turn pods and nodes metrics collection on/off

SQL Server 2019 CU5 enabled two feature switches to control the collection of pods and nodes metrics. In case you are using different solutions for monitoring your Kubernetes infrastructure, you can turn off the built-in metrics collection for pods and host nodes by setting *allowNodeMetricsCollection* and *allowPodMetricsCollection* to *false* in *control.json* deployment configuration file. For OpenShift environments, these settings are set to *false* by default in the built-in deployment profiles, since collecting pod and node metrics requires privileged capabilities.
Run this command to update the values of these settings in your custom configuration file using *azdata* CLI:

```bash
 azdata bdc config replace -c custom-bdc/control.json -j "$.security.allowNodeMetricsCollection=false"
 azdata bdc config replace -c custom-bdc/control.json -j "$.security.allowPodMetricsCollection=false"
 ```

## Next steps

For more information about using configuration files in big data cluster deployments, see [How to deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] on Kubernetes](deployment-guidance.md#configfile).