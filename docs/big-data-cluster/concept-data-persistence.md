---
title: Data persistence on Kubernetes
titleSuffix: SQL Server Big Data Clusters
description: Learn how persistent volumes provide a plug-in model for storage in Kubernetes. Also learn how data persistence works in a SQL Server 2019 Big Data Cluster.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 07/16/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
---

# Data persistence in Kubernetes with [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

[Persistent volumes](https://kubernetes.io/docs/concepts/storage/persistent-volumes/) provide a plug-in model for storage in Kubernetes. In this model, the way that storage is provided is abstracted from how it's consumed. Therefore, you can bring your own highly available storage and plug it into the SQL Server big data cluster. This gives you full control over the type of storage, availability, and performance you require. Kubernetes supports [various kinds of storage solutions](https://kubernetes.io/docs/concepts/storage/storage-classes/#provisioner), including Azure disks and files, Network File System (NFS), and local storage.

> [!IMPORTANT]
> If you are deploying the big data cluster in Azure using one of the managed Kubernetes services (AKS or ARO), keep in mind there are limitations that you might need to accomodate, depending on your application workload requirements. For example, volumes that use Azure managed disks are currently not zone-redundant resources. Volumes cannot be attached across zones and must be co-located in the same zone as a given node hosting the target pod. In this specific case, you might want to use additional high availability solutions that are available with SQL Server Big Data Clusters. See [here](/azure/aks/availability-zones) for more details on Azure Kubernetes service and availability zones.

## Configure persistent volumes

A SQL Server big data cluster consumes these persistent volumes by using [storage classes](https://kubernetes.io/docs/concepts/storage/storage-classes/). You can create different storage classes for different kinds of storage and specify them at the time of big data cluster deployment. You can configure the storage class and the persistent volume claim size to use for a particular purpose at the pool level. A SQL Server big data cluster creates [persistent volume claims](https://kubernetes.io/docs/concepts/storage/persistent-volumes/#persistentvolumeclaims) by using the specified storage class name for each component that requires persistent volumes. It then mounts the corresponding persistent volume (or volumes) in the pod. 

Here are some important aspects to consider when you're planning storage configuration for your big data cluster:

- For a successful big data cluster deployment, make sure you have the required number of persistent volumes available. If you're deploying on an Azure Kubernetes Service (AKS) cluster and you're using a built-in storage class (`default` or `managed-premium`), this class supports dynamic provisioning for the persistent volumes. Therefore, you don't have to pre-create the persistent volumes, but you must make sure the available worker nodes in the AKS cluster can attach as many disks as the number of persistent volumes that are required for the deployment. Depending on the [VM size](/azure/virtual-machines/linux/sizes) specified for the worker nodes, each node can attach a certain number of disks. For a default size cluster (with no high availability), a minimum of 24 disks are required. If you're enabling high availability or scaling up any pool, make sure you have at least two persisted volumes per each additional replica, regardless of the resource you're scaling up.

- If the storage provisioner for the storage class you're providing in the configuration doesn't support dynamic provisioning, you must pre-create the persisted volumes. For example, the `local-storage` provisioner doesn't enable dynamic provisioning. See this [sample script](https://github.com/microsoft/sql-server-samples/tree/master/samples/features/sql-big-data-cluster/deployment/kubeadm/ubuntu) for guidance on how to proceed in a Kubernetes cluster deployed with `kubeadm`.

- When you deploy a big data cluster, you can configure the same storage class for use by all the components in the cluster. But as a best practice for a production deployment, various components will require different storage configurations to accommodate various workloads in terms of size or throughput. You can overwrite the default storage configuration specified in the controller for each of the SQL Server master instances, datasets, and storage pools. This article provides examples of how to do this.

- When computing the storage pool sizing requirements, you must consider the replication factor that HDFS is configured with.  Replication factor is configurable at deployment time in the cluster deployment configuration file. The default value for the dev-test profiles (i.e. `aks-dev-test` or `kubeadm-dev-test`) is 2, and for the profiles that we recommend for production deployments (i.e. `kubeadm-prod`) the default value is 3. As a best practice, we recommend you configure your production deployment of big data cluster with a replication factor for HDFS of at least 3. The value of the replication factor will impact the number of instances in the storage pool: at a minimum, you must deploy at least as many storage pool instances as the value of the replication factor. In addition, you must size the storage accordingly, and acount for data being replicated in HDFS as many times as the value of the replication factor. You can find more about data replication in HDFS [here](https://hadoop.apache.org/docs/r3.2.1/hadoop-project-dist/hadoop-hdfs/HdfsDesign.html#Data_Replication). 

- As of the SQL Server 2019 CU1 release, BDC does not enable an experience to update the storage configuration setting post-deployment. This constraint prevents you to use BDC operations to modify the size of the persistent volume claim for each instance or scale any pool post-deployment. Therefore, it's very important that you plan the storage layout before you deploy a big data cluster. However, you can expand the persistent volume size using Kubernetes APIs directly. In this case, BDC metadata will not be updated and you will see inconsistencies regarding volume sizes in the configuration cluster metadata.

- By deploying on Kubernetes as containerized applications, and by using features like stateful sets and persistent storage, Kubernetes ensures that pods are restarted in case of health issues and that they're attached to the same persistent storage. But in case there's a node failure and a pod must be restarted on another node, there's an increased risk of unavailability if the storage is local to the failed node. To reduce this risk, you must either configure additional redundancy and enable [high availability features](deployment-high-availability.md) or use remote redundant storage. Here's an overview of the storage options for various components in the big data clusters:

| Resources | Storage type for data | Storage type for log |  Notes |
|---|---|---|--|
| SQL Server master instance | Local (replicas>=3) or remote redundant storage (replica=1) | Local storage | Stateful set-based implementation in which pods that stick to the nodes will ensure restarts, and transient failures won't cause data loss. |
| Compute pool | Local storage | Local storage | No user data stored. |
| Data pool | Local/remote redundant storage | Local storage | Use remote redundant storage if data pool can't be rebuilt from other data sources.  |
| Storage pool (HDFS) | Local/remote redundant storage | Local storage | HDFS replication is enabled. |
| Spark pool | Local storage | Local storage | No user data stored. |
| Control (controldb, metricsdb, logsdb)| Remote redundant storage | Local storage | Critical to use storage level redundancy for big data cluster metadata. |

> [!IMPORTANT]
> Make sure that control-related components are on a remote redundant storage device. A `controldb-0` pod hosts a SQL Server instance with databases that store all the metadata related to the cluster service states, various settings, and other relevant info. If this instance becomes unavailable, it will cause health issues with other dependent services in the cluster.

## Configure big data cluster storage settings

As in other customizations, you can specify storage settings in the cluster configuration files at deployment time for each pool in the `bdc.json` configuration file and for the control services in the `control.json` file. If there are no storage configuration settings in the pool specifications, the control storage settings will be used for all other components, including SQL Server master (`master` resource), HDFS (`storage-0` resource), and data pool components. The following code sample represents the storage configuration section that you can include in the specification:

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

Deployment of the big data cluster uses persistent storage to store data, metadata, and logs for various components. You can customize the size of the persistent volume claims created as part of the deployment. As a best practice, we recommend that you use storage classes with a *Retain* [reclaim policy](https://kubernetes.io/docs/concepts/storage/storage-classes/#reclaim-policy).

> [!WARNING]
> Running without persistent storage can work in a test environment, but it could result in a non-functional cluster. When a pod restarts, cluster metadata and/or user data is lost permanently. We don't recommend that you run under this configuration.

The [Configure storage](#config-samples) section provides more examples of how to configure storage settings for your SQL Server big data cluster deployment.

## AKS storage classes

AKS comes with [two built-in storage classes](/azure/aks/azure-disks-dynamic-pv/), `default` and `managed-premium`, along with a dynamic provisioner for them. You can specify either of those or create your own storage class to deploy a big data cluster with persistent storage enabled. By default, the built-in cluster configuration file for AKS, `aks-dev-test`, comes with persistent storage configurations to use the `default` storage class.

> [!WARNING]
> Persistent volumes created with the built-in storage classes `default` and `managed-premium` have a reclaim policy of *Delete*. So, when you delete the SQL Server big data cluster, persistent volume claims are deleted as are the persistent volumes. You can create custom storage classes by using `azure-disk` provisioner with a `Retain` reclaim policy, as described in [Concepts storage](/azure/aks/concepts-storage/#storage-classes).

## Storage classes for `kubeadm` clusters 

Kubernetes clusters that are deployed by using `kubeadm` don't have a built-in storage class. You must create your own storage classes and persistent volumes by using local storage or your [preferred storage provisioner](https://kubernetes.io/docs/concepts/storage/storage-classes/#provisioner). In that situation, you set `className` to the storage class you configured.

> [!IMPORTANT]
>  In the built-in deployment configuration files for kubeadm (`kubeadm-dev-test` and `kubeadm-prod`), there's no storage class name specified for data and log storage. Before deployment, you must customize the configuration file and set the value for `className`. Otherwise, pre-deployment validations will fail. Deployment also has a validation step that checks for the existence of the storage class, but not for the necessary persistent volumes. Make sure you create enough volumes, depending on the scale of your cluster. For the default minimum cluster size (default scale, no high availability) you must create at least 24 persistent volumes. For an example of how to create persistent volumes by using a local provisioner, see [Create a Kubernetes cluster using Kubeadm](https://github.com/Microsoft/sql-server-samples/tree/master/samples/features/sql-big-data-cluster/deployment/kubeadm/ubuntu).

## Customize storage configurations for each pool

For all customizations, you must first create a copy of the built-in configuration file you want to use. For example, the following command creates a copy of the `aks-dev-test` deployment configuration files in a subdirectory named `custom`:

```bash
azdata bdc config init --source aks-dev-test --target custom
```

This process creates two files, `bdc.json` and `control.json`, which you can customize either by manually editing them or by using the `azdata bdc config` command. You can use a combination of jsonpath and jsonpatch libraries to provide ways to edit your config files.


### <a id="config-samples"></a> Configure storage class name and/or claims size

By default, the size of the persistent volume claims provisioned for each of the pods provisioned in the cluster is 10 gigabytes (GB). You can update this value to accommodate the workloads you're running in a custom configuration file before cluster deployment.

In the following example, the size of persistent volume claims is updated to 32 GB in the `control.json`. If it's not overridden at pool level, this setting will be applied to all pools.

```bash
azdata bdc config replace --config-file custom/control.json --json-values "$.spec.storage.data.size=100Gi"
```

The following example shows how to modify the storage class for the `control.json` file:

```bash
azdata bdc config replace --config-file custom/control.json --json-values "$.spec.storage.data.className=<yourStorageClassName>"
```

You also have the option to manually edit the custom configuration file or to use a .json patch that changes the storage class for Storage pool, as in the following example:

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

Apply the patch file. Use the `azdata bdc config patch` command to apply the changes in the .json patch file. The following example applies the `patch.json` file to a target deployment configuration file, `custom.json`:

```bash
azdata bdc config patch --config-file custom/bdc.json --patch-file ./patch.json
```

## Next steps

- For complete documentation about volumes in Kubernetes, see the [Kubernetes documentation on volumes](https://kubernetes.io/docs/concepts/storage/volumes/).

- For more information about deploying a SQL Server big data cluster, see [How to deploy SQL Server big data cluster on Kubernetes](deployment-guidance.md).
