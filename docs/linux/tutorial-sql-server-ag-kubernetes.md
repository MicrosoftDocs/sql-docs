---
title: Configure a SQL Server Always On availability group in Kubernetes for high availability | Microsoft Docs
description: This tutorial shows how to deploy a SQL Server always on availability group with Kubernetes on Azure Container Service.
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
ms.date: 07/16/2018
ms.topic: tutorial
ms.prod: sql
ms.component: ""
ms.suite: "sql"
ms.custom: "sql-linux,mvc"
ms.technology: linux
---
# Configure a SQL Server Always On availability group in Kubernetes for high availability

Learn how to configure a SQL Server Always On availability group on [Kubernetes](http://kubernetes.io) in [Azure Kubernetes Service (AKS)](http://docs.microsoft.com/azure/aks/). This solution provides the high availability (HA) and read-scale benefits of a [SQL Server Always On availability group](../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md) with the container orchestration benefits of Kubernetes.

This tutorial demonstrates how to configure a highly available SQL Server instance in containers that use AKS. 

> [!div class="checklist"] 
> * Create an SA password
> * Create storage
> * Create the deployment
> * Connect with SQL Server Management Studio (SSMS)
> * Verify failure and recovery

## Configure storage

## Create the operator deployment 

1. Configure storage for SQL Server files

2. Deploy the operator, and agents

3. 

## Create storage

### Define the storage

Create a manifest file to define the storage class and the persistent volume claim for each of the 3 SQL Server instances that will be part of the AG. The Kubernetes cluster uses this manifest to create the persistent storage.

The following yaml file example, defines a storage class and persistent volume claims. The storage class provisioner is `azure-disk` because this Kubernetes cluster is in Azure. The persistent volume claims are `mssql-data-1`, `mssql-data-2`, `mssql-data-3`. The persistent volume claim metadata includes an annotation connecting it back to the storage class.

```yaml
kind: StorageClass
apiVersion: storage.k8s.io/v1beta1
metadata:
     name: azure-disk
provisioner: kubernetes.io/azure-disk
parameters:
  storageaccounttype: Standard_LRS
  kind: Managed
---
kind: PersistentVolumeClaim
apiVersion: v1
metadata:
  name: mssql-data-1
  annotations:
    volume.beta.kubernetes.io/storage-class: azure-disk
spec:
  accessModes:
  - ReadWriteOnce
  resources:
    requests:
      storage: 8Gi
---
kind: PersistentVolumeClaim
apiVersion: v1
metadata:
  name: mssql-data-2
  annotations:
    volume.beta.kubernetes.io/storage-class: azure-disk
spec:
  accessModes:
  - ReadWriteOnce
  resources:
    requests:
      storage: 8Gi
---
kind: PersistentVolumeClaim
apiVersion: v1
metadata:
  name: mssql-data-3
  annotations:
    volume.beta.kubernetes.io/storage-class: azure-disk
spec:
  accessModes:
  - ReadWriteOnce
  resources:
    requests:
      storage: 8Gi
```

Save the file `pvc.yaml`.

### Create the persistent volume claims in Kubernetes

```azurecli
kubectl apply -f <Path to pvc.yaml file>
```

Kubernetes creates the persistent volumes automatically as Azure managed storage accounts, and binds them to the persistent volume claims. 

### Verify the persistent volume claim

```azurecli
kubectl describe pvc <PersistentVolumeClaim>
```

In the preceding step, the persistent volume claim is named `mssql-data-<x>` where `<x>` is a number. For example, `mssql-data-1`. To see 
metadata about the persistent volume claim, run the following command: 

```azurecli
kubectl describe pvc mssql-data-<x>
```

### Verify the persistent volumes

```azurecli
kubectl describe pv
```

`kubectl` returns metadata about the persistent volumes that were automatically created and bound to the persistent volume claims.

## Deploy the operator and agents

In these steps you will:

1. Create an 


## Verify failure and recovery

## Summary

## Next steps

> [!div class="nextstepaction"]
>[Introduction to Kubernetes](http://docs.microsoft.com/en-us/azure/aks/intro-kubernetes)


