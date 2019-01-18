---
title: Data persistence on Kubernetes
titleSuffix: SQL Server 2019 big data clusters
description: Learn about how data persistence works in a SQL Server 2019 big data cluster.
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 12/07/2018
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
ms.custom: seodec18
---

# Data persistence with SQL Server big data cluster on Kubernetes

[Persistent Volumes](https://kubernetes.io/docs/concepts/storage/persistent-volumes/) provide a plugin model for storage in Kubernetes where how storage is provided is completed abstracted from how it is consumed. Therefore, you can bring your own highly available storage and plug it into the SQL Server big data cluster cluster. This gives you full control over the type of storage, availability, and performance that you require. Kubernetes supports various kinds of storage solutions including Azure disks/files, NFS, local storage, and more.

## Configure persistent volumes

The way SQL Server big data cluster consumes these persistent volumes is by using [Storage Classes](https://kubernetes.io/docs/concepts/storage/storage-classes/). You can create different storage classes for different kind of storage and specify them at the big data cluster deployment time. You can configure which storage class to use for which purpose (pool). SQL Server big data cluster creates [persistent volume claims](https://kubernetes.io/docs/concepts/storage/persistent-volumes/#persistentvolumeclaims) with the specified storage class name for each pod that requires persistent volumes. It then mounts the corresponding persistent volume(s) in the pod.

> [!NOTE]
> For CTP 2.2, only `ReadWriteOnce` access mode for the whole cluster is supported.

## Deployment settings

To use persistent storage during deployment, configure the **USE_PERSISTENT_VOLUME** and **STORAGE_CLASS_NAME** environment variables before running  `mssqlctl create cluster` command. **USE_PERSISTENT_VOLUME** is set to `true` by default. You can override the default and set it to `false` and, in this case, SQL Server big data cluster uses emptyDir mounts. 

> [!WARNING]
> Running without persistent storage can work in a test environment, but it could result in a non-functional cluster. Upon pod restarts, cluster metadata and/or user data will be lost permanently.

If you set the flag to true, you must also provide **STORAGE_CLASS_NAME** as a parameter at the deployment time.

## AKS storage classes

AKS comes with [two built-in storage classes](https://docs.microsoft.com/azure/aks/azure-disks-dynamic-pv) **default** and **managed-premium** along with dynamic provisioner for them. You can specify either of those or create your own storage class  for deploying big data cluster with persistent storage enabled.

## Minikube storage class

Minikube comes with a built-in storage class called **standard** along with a dynamic provisioner for it. Note that on minikube, if `USE_PERSISTENT_VOLUME=true` (default), you must also override the default value for the **STORAGE_CLASS_NAME** environment variable because the default value is different. Set the value to `standard`: 

On Windows, use the following command:

```cmd
SET STORAGE_CLASS_NAME=standard
```

On Linux, use the following command:

```cmd
export STORAGE_CLASS_NAME=standard
```

Alternatively, you can suppress using persistent volumes on minikube by setting `USE_PERSISTENT_VOLUME=false`.

## Kubeadm

Kubeadm does not come with a built-in storage class. You can choose to create your own persistent volumes and storage classes using local storage or your preferred provisioner, such as [Rook](https://github.com/rook/rook). In that case, you would set the **STORAGE_CLASS_NAME** to the storage class you configured. Alternatively, you can set `USE_PERSISTENT_VOLUME=false` in test environments, but note the previous warning in the **Deployment settings** section of this article.  

## On-premises cluster

On-premise clusters obviously do not come with any built-in storage class, therefore you must set up [persistent volumes](https://kubernetes.io/docs/concepts/storage/persistent-volumes/)/[provisioners](https://kubernetes.io/docs/concepts/storage/dynamic-provisioning/) beforehand and then use the corresponding storage classes during SQL Server big data cluster deployment.

## Customize storage size for each pool
By default, the size of the persistent volume provisioned for each of the pods provisioned in the cluster is 6 GB. This is configurable by setting the environment variable `STORAGE_SIZE` to a different value. For example, you can run below command to set the value to 10 GB, before running the `mssqlctl create cluster command`.

```bash
export STORAGE_SIZE=10Gi
```

You can also have different configurations for persistent storage settings such storage class name and persistent volume sizes for different pools in the cluster . For example, you can configure the persistent volumes deployed for the storage pool to use a different storage class and have higher capacity by setting below environment variables before deploying the cluster:

```bash
export STORAGE_POOL_USE_PERSISTENT_VOLUME=true
export STORAGE_POOL_STORAGE_CLASS_NAME=managed-premium
export STORAGE_POOL_STORAGE_SIZE=100Gi
```

Here is a comprehensive list of the environment variables related to setting up persistent storage for the SQL Server big data cluster:

| Environment variable | Default value | Description |
|---|---|---|
| **USE_PERSISTENT_VOLUME** | true | `true` to use Kubernetes Persistent Volume Claims for pod storage. `false` to use ephemeral host storage for pod storage. |
| **STORAGE_CLASS_NAME** | default | If `USE_PERSISTENT_VOLUME` is `true` this indicates the name of the Kubernetes Storage Class to use. |
| **STORAGE_SIZE** | 6Gi | If `USE_PERSISTENT_VOLUME` is `true`, this indicates persistent volume size for each pod. |
| **DATA_POOL_USE_PERSISTENT_VOLUME** | USE_PERSISTENT_VOLUME | `true` to use Kubernetes Persistent Volume Claims for pods in the data pool. `false` to use ephemeral host storage for data pool pods. |
| **DATA_POOL_STORAGE_CLASS_NAME** | STORAGE_CLASS_NAME | Indicates the name of the Kubernetes Storage Class to use for the persistent volumes associated with data pool pods.|
| **DATA_POOL_STORAGE_SIZE** | STORAGE_SIZE |Indicates the persistent volume size for each pod in the data pool. |
| **STORAGE_POOL_USE_PERSISTENT_VOLUME** | USE_PERSISTENT_VOLUME | `true` to use Kubernetes Persistent Volume Claims for pods in the storage pool. `false` to use ephemeral host storage for storage pool pods.|
| **STORAGE_POOL_STORAGE_CLASS_NAME** | STORAGE_CLASS_NAME | TIndicates the name of the Kubernetes Storage Class to use for the persistent volumes associated with storage pool pods. |
| **STORAGE_POOL_STORAGE_SIZE** | STORAGE_SIZE | Indicates the persistent volume size for each pod in the storage pool. |

## Next steps

For complete documentation about volumes in Kubernetes, see the [Kubernetes documentation on Volumes](https://kubernetes.io/docs/concepts/storage/volumes/).

For more information about deploying SQL Server big data cluster, see [How to deploy SQL Server big data cluster on Kubernetes](deployment-guidance.md).

