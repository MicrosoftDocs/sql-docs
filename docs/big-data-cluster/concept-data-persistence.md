---
title: Data persistence on Kubernetes
titleSuffix: SQL Server big data clusters
description: Learn about how data persistence works in a SQL Server 2019 big data cluster.
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 04/23/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
ms.custom: seodec18
---

# Data persistence with SQL Server big data cluster on Kubernetes

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

[Persistent Volumes](https://kubernetes.io/docs/concepts/storage/persistent-volumes/) provide a plugin model for storage in Kubernetes. How storage is provided is abstracted from how it is consumed. Therefore, you can bring your own highly available storage and plug it into the SQL Server big data cluster. This gives you full control over the type of storage, availability, and performance that you require. Kubernetes supports various kinds of storage solutions including Azure disks/files, NFS, local storage, and more.

## Configure persistent volumes

The way a SQL Server big data cluster consumes these persistent volumes is by using [Storage Classes](https://kubernetes.io/docs/concepts/storage/storage-classes/). You can create different storage classes for different kind of storage and specify them at the big data cluster deployment time. You can configure which storage class and the persistent volume claim size to use for which purpose at the pool level. A SQL Server big data cluster creates [persistent volume claims](https://kubernetes.io/docs/concepts/storage/persistent-volumes/#persistentvolumeclaims) with the specified storage class name for each component that requires persistent volumes. It then mounts the corresponding persistent volume(s) in the pod. 

## Configure big data cluster storage settings

Similar to other customizations, you can specify storage settings in the cluster configuration files at deployment time for each pool and the control plane. If there are no storage configuration settings in the pool specifications, then the control plane storage settings will be used. This is a sample of the storage configuration section that you can include in the spec:

```json
    "storage": 
    {
        "usePersistentVolume": true,
        "className": "managed-premium",
        "accessMode": "ReadWriteOnce",
        "size": "10Gi"
    }
```

To use persistent storage during deployment, set the values of **usePersistentVolume** key to *true* and **className** key to the name of the storage class you want to use for the respective pool. You can also customize the size of the persistent volume claims created as part of the deployment. As a best practice, we recommend to use storage classes with a *Retain* [reclaim policy](https://kubernetes.io/docs/concepts/storage/storage-classes/#reclaim-policy).

> [!NOTE]
> In CTP 2.5, you can't modify storage configuration setting post deployment. Also, only `ReadWriteOnce` access mode for the whole cluster is supported.

> [!WARNING]
> Running without persistent storage can work in a test environment, but it could result in a non-functional cluster. Upon pod restarts, cluster metadata and/or user data will be lost permanently. We do not recommend to run in this configuration. 

[Configure storage](#config-samples) section provides more examples on how to configure storage settings for your SQL Server big data cluster deployment.

## AKS storage classes

AKS comes with [two built-in storage classes](https://docs.microsoft.com/azure/aks/azure-disks-dynamic-pv) **default** and **managed-premium** along with dynamic provisioner for them. You can specify either of those or create your own storage class  for deploying big data cluster with persistent storage enabled. By default, the built in cluster configuration file for aks *aks-dev-test.json* comes with persistent storage configurations to use **managed-premium** storage class.

> [!WARNING]
> Persistent volumes created with the built-in storage classes **default** and **managed-premium** have a reclaim policy of *Delete*. So at the time the you delete the SQL Server big data cluster, persistent volume claims get deleted and then persistent volumes as well. You can create custom storage classes using **azure-disk** privioner with a *Retain* reclaim policy as shown in  [this](https://docs.microsoft.com/en-us/azure/aks/concepts-storage#storage-classes) article.


## Minikube storage class

Minikube comes with a built-in storage class called **standard** along with a dynamic provisioner for it. The built in configuration file for minikube *minikube-dev-test.json* has the storage configuration settings in the control plane spec. The same settings will be applied to all pools specs. You can also customize a copy of this file and use it for your big data cluster deployment on minikube. You can manually edit the custom file and change the size of the persistent volumes claims for specific pools to accommodate the workloads you want to run. Or, see [Configure storage](#config-samples) section for examples on how to do edits using *mssqlctl* commands.

## Kubeadm storage classes

Kubeadm does not come with a built-in storage class. You must create your own storage classes and persistent volumes using local storage or your preferred provisioner, such as [Rook](https://github.com/rook/rook). In that case, you would set the **className** to the storage class you configured. 

> [!NOTE]
> In the built in deployment configuration file for kubeadm *kubeadm-dev-test.json*, the default value for **usePersistentVolume** key is *true*, so you must set the value for **className** otherwise the pre-deployment validations will fail. Deployment also has a validation step that checks for the existence of the storage class, but not for the necessary persistent volumes. You must ensure you create enough volumes depending on the scale of your cluster. In CTP2.5, for the default cluster size you must create at least 23 volumes. [Here](https://github.com/Microsoft/sql-server-samples/tree/master/samples/features/sql-big-data-cluster/deployment/kubeadm/ubuntu) is an example on how to create persistent volumes using local provisioner.


## Customize storage configurations for each pool

For all customizations, you must first create a copy of the built in configuration file you want to use. For example, the following command creates a copy of the *aks-dev-test.json* deployment configuration file in the current directory:

```bash
mssqlctl cluster config init --src aks-dev-test.json --target custom.json
```

Then, you can customize your config file either by editing it manually, or you can use *mssqlctl cluster config section set* command. This set command uses a combination of jsonpath and jsonpatch libraries to provide ways to edit your config file.

### Configure size

By default, the size of the persistent volume claims provisioned for each of the pods provisioned in the cluster is 10 GB. You can update this value to accommodate the workloads you are running in a custom configuration file before cluster deployment.

The following example only updates the size of persistent volume claims in the storage pool to 32Gi:

```bash
mssqlctl cluster config section set -f custom.json -j "$.spec.pools[?(@.spec.type == ""Storage"")].spec.storage.size=32Gi"
```

The following example updates the size of persistent volume claims for all pools to 32Gi:

```bash
mssqlctl cluster config section set -f custom.json -j "$.spec.pools[?(@.spec.type[*])].spec.storage.size=32Gi"
```

### <a id="config-samples"></a> Configure storage class

Following example shows how to modify the storage class for the control plane:

```bash
mssqlctl cluster config section set -f custom.json -j "$.spec.controlPlace.spec.storage.className=<yourStorageClassName>"
```

Another option is to manually edit the custom configuration file or to use jsonpatch like in the following example that changes the storage class for Storage pool. Create a *patch.json*
 file with this content:

```json
{
  "patch": [
    {
      "op": "replace",
      "path": "$.spec.pools[?(@.spec.type == 'Storage')].spec",
      "value": {
        "replicas": 2,
        "type": "Storage",
        "storage": {
          "usePersistentVolume": true,
          "accessMode": "ReadWriteOnce",
          "className": "<yourStorageClassName>",
          "size": "32Gi"
        }
      }
    }
  ]
}
```

Apply the patch file. Use *mssqlctl cluster config section set* command to apply the changes in the JSON patch file. The following example applies the patch.json file to a target deployment configuration file custom.json.

```bash
mssqlctl cluster config section set -f custom.json -p ./patch.json
```

## Next steps

For complete documentation about volumes in Kubernetes, see the [Kubernetes documentation on Volumes](https://kubernetes.io/docs/concepts/storage/volumes/).

For more information about deploying a SQL Server big data cluster, see [How to deploy SQL Server big data cluster on Kubernetes](deployment-guidance.md).

