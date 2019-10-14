---
title: Data persistence on Kubernetes
titleSuffix: SQL Server big data clusters
description: Learn about how data persistence works in a SQL Server 2019 big data cluster.
author: mihaelablendea 
ms.author: mihaelab
ms.reviewer: mikeray
ms.date: 08/28/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Data persistence with SQL Server big data cluster on Kubernetes

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

[Persistent Volumes](https://kubernetes.io/docs/concepts/storage/persistent-volumes/) provide a plugin model for storage in Kubernetes. How storage is provided is abstracted from how it is consumed. Therefore, you can bring your own highly available storage and plug it into the SQL Server big data cluster. This gives you full control over the type of storage, availability, and performance that you require. Kubernetes supports various kinds of storage solutions including Azure disks/files, NFS, local storage, and more.

## Configure persistent volumes

The way a SQL Server big data cluster consumes these persistent volumes is by using [Storage Classes](https://kubernetes.io/docs/concepts/storage/storage-classes/). You can create different storage classes for different kind of storage and specify them at the big data cluster deployment time. You can configure which storage class and the persistent volume claim size to use for which purpose at the pool level. A SQL Server big data cluster creates [persistent volume claims](https://kubernetes.io/docs/concepts/storage/persistent-volumes/#persistentvolumeclaims) with the specified storage class name for each component that requires persistent volumes. It then mounts the corresponding persistent volume(s) in the pod. 

Here are some important aspects to consider when you are planning storage configuration for your big data cluster:

- For a successful big data cluster deployment, you must ensure the required number of persistent volumes are available. If you are deploying on an AKS cluster and you are using one of the built-in storage classes (`default` or `managed-premium`) - these support dynamic provisioning for the persistent volumes. This means that you do not have to pre-create the persistent volumes, but you must ensure the worker nodes available in the AKS cluster can attach as many disks as persistent volumes are necessary for the deployment. Depending on the [VM size](https://docs.microsoft.com/en-us/azure/virtual-machines/linux/sizes) specified for the worker nodes, each node can attach a certain number of disks. For a default size cluster(with no high availability), a minimum of 24 disks are required. If you are enabling high availability or scaling up any pool, you must ensure at a minimum two persisted volumes per each additional replica, irrespective of the resource you are scaling up.

- If the storage provisioner for the storage class you are providing in the configuration does not support dynamic provisioning, you must pre-create the persisted volumes. For example, the `local-storage` provisioner does not enable dynamic provisioning. See this [sample script](https://github.com/microsoft/sql-server-samples/tree/cu1-bdc/samples/features/sql-big-data-cluster/deployment/kubeadm/ubuntu) on how to do so in a Kubernetes cluster deployed with `kubeadm`.

- When you deploy a big data cluster, you can configure same storage class to be used by all components in the cluster. But as a best practice for a production deployment, various components will require different storage configurations to accommodate various workloads in terms of size or throughput. You can overwrite the default storage configuration specified in the controller for each of the SQL Server master instance, data and storage pools. This article provides examples how to do this.

- As of SQL Server 2019 CU1 release, you can't modify storage configuration setting post deployment. This includes not all modifying the size of the persistent volume claim for each instance, but also scaling operations post deployment are not supported. So planning the storage layout before big data cluster is very important.

- By the nature of deploying on Kubernetes as containerized applications, and using features like stateful sets and persistent storage, Kubernetes ensures that pods are restarted in case of health issues and attached to the same persistent storage. But in case there is a node failure and pod must be restarted on another node, there is increased risk of unavailability if the storage is local to the failed node. To overcome this risk, you must either configure additional redundancy and enable [high availability features](deployment-high-availability.md) or use remote redundant storage. Here is an overview of the storage options for various components in the big data clusters.

| Resources | Storage type for data | Storage type for log |  Notes |
|---|---|---|--|
| SQL Server master instance | Local (replicas>=3) or Remote redundant storage (replica=1) | Local storage | Stateful set based implementation where pods stick to the nodes will ensure restarts and transient failures won’t cause data loss. |
| Compute pool | Local storage* | Local storage | No user data stored. |
| Data pool | Local/Remote redundant storage | Local storage | Use remote redundant storage if data pool can not be rebuilt from other data sources.  |
| Storage pool (HDFS) | Local/Remote redundant storage | Local storage | HDFS replication is enabled. |
| Spark pool | Local storage* | Local storage | No user data stored. |
| Control (controldb, metricsdb, logsdb)| Remote redundant storage | Local storage | Critical to use storage level redundancy for big data cluster metadata. |

> [!IMPORTANT]
> You must ensure control related components are on remote redundant storage. `controldb-0` pod hosts a SQL Server instance with databases that store all metadata about the cluster service states,  various settings etc. Unavailability of this instance will cause health issues with other dependent services in the cluster.

## Configure big data cluster storage settings

Similar to other customizations, you can specify storage settings in the cluster configuration files at deployment time for each pool in the `bdc.json` configuration file and for the control services in the `control.json` file. If there are no storage configuration settings in the pool specifications, then the control storage settings will be used for all other components, including SQL Server master (`master` resource), HDFS (`storage-0` resource) or data pool. This is a sample of the storage configuration section that you can include in the spec:

```json
    "storage": 
    {
      "data": {
        "className": "default",
        "accessMode": "ReadWriteOnce",
        "size": "15Gi"
      },
      "logs": {
        "className": "default",
        "accessMode": "ReadWriteOnce",
        "size": "10Gi"
    }
```

Deployment of big data cluster will use persistent storage to store data, metadata and logs for various components. You can customize the size of the persistent volume claims created as part of the deployment. As a best practice, we recommend to use storage classes with a *Retain* [reclaim policy](https://kubernetes.io/docs/concepts/storage/storage-classes/#reclaim-policy).

> [!WARNING]
> Running without persistent storage can work in a test environment, but it could result in a non-functional cluster. Upon pod restarts, cluster metadata and/or user data will be lost permanently. We do not recommend to run in this configuration. 

[Configure storage](#config-samples) section provides more examples on how to configure storage settings for your SQL Server big data cluster deployment.

## AKS storage classes

AKS comes with [two built-in storage classes](/azure/aks/azure-disks-dynamic-pv/) `default` and `managed-premium` along with dynamic provisioner for them. You can specify either of those or create your own storage class  for deploying big data cluster with persistent storage enabled. By default, the built in cluster configuration file for aks `aks-dev-test`comes with persistent storage configurations to use `default` storage class.

> [!WARNING]
> Persistent volumes created with the built-in storage classes `default` and `managed-premium` have a reclaim policy of *Delete*. So at the time the you delete the SQL Server big data cluster, persistent volume claims get deleted and then persistent volumes as well. You can create custom storage classes using `azure-disk` provisioner with a `Retain` reclaim policy as shown in [this](/azure/aks/concepts-storage/#storage-classes) article.

## `kubeadm` storage classes

`kubeadm` does not come with a built-in storage class. You must create your own storage classes and persistent volumes using local storage or your preferred provisioner, such as [Rook](https://github.com/rook/rook). In that case, you would set the `className` to the storage class you configured. 

> [!NOTE]
>  In the built in deployment configuration file for `kubeadm kubeadm-dev-test` there is no storage class name specified for the data and log storage. Before deployment, you must customize the configuration file and set the value for className otherwise the pre-deployment validations will fail. Deployment also has a validation step that checks for the existence of the storage class, but not for the necessary persistent volumes. You must ensure you create enough volumes depending on the scale of your cluster. In CTP 3.1, for the default cluster size you must create at least 23 volumes. [Here](https://github.com/Microsoft/sql-server-samples/tree/master/samples/features/sql-big-data-cluster/deployment/kubeadm/ubuntu) is an example on how to create persistent volumes using local provisioner.


## Customize storage configurations for each pool

For all customizations, you must first create a copy of the built in configuration file you want to use. For example, the following command creates a copy of the `aks-dev-test` deployment configuration files in a subdirectory named `custom`:

```bash
azdata bdc config init --source aks-dev-test --target custom
```

This creates two files, `bdc.json` and `control.json` that can be customized by either editing them manually, or you can use `azdata bdc config` command. You can use a combination of jsonpath and jsonpatch libraries to provide ways to edit your config files.


### <a id="config-samples"></a> Configure storage class name and/or claims size

By default, the size of the persistent volume claims provisioned for each of the pods provisioned in the cluster is 10 GB. You can update this value to accommodate the workloads you are running in a custom configuration file before cluster deployment.

The following example updates the size of persistent volume claims size to 32Gi in the `control.json`. If not overridden at pool level, this setting will be applied to all pools:

```bash
azdata bdc config replace --config-file custom/control.json --json-values "$.spec.storage.data.size=100Gi"
```

Following example shows how to modify the storage class for the `control.json` file:

```bash
azdata bdc config replace --config-file custom/control.json --json-values "$.spec.storage.data.className=<yourStorageClassName>"
```

Another option is to manually edit the custom configuration file or to use json patch like in the following example that changes the storage class for Storage pool. Create a `patch.json` file with this content:

```json
{
  "patch": [
    {
      "op": "replace",
      "path": "$.spec.resources.storage-0.spec",
      "value": {
        "type":"Storage",
        "replicas":2,
        "storage":{
            "data":{
                    "size": "100Gi",
                    "className": "myStorageClass",
                    "accessMode":"ReadWriteOnce"
                    },
            "logs":{
                    "size":"32Gi",
                    "className":"myStorageClass",
                    "accessMode":"ReadWriteOnce"
                    }
                }
            }
        }
    ]
}
```

Apply the patch file. Use `azdata bdc config patch` command to apply the changes in the JSON patch file. The following example applies the `patch.json` file to a target deployment configuration file `custom.json`.

```bash
azdata bdc config patch --config-file custom/bdc.json --patch-file ./patch.json
```

## Next steps

For complete documentation about volumes in Kubernetes, see the [Kubernetes documentation on Volumes](https://kubernetes.io/docs/concepts/storage/volumes/).

For more information about deploying a SQL Server big data cluster, see [How to deploy SQL Server big data cluster on Kubernetes](deployment-guidance.md).
