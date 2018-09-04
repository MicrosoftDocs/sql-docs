---
title: Data persistence with SQL Server Aris on Kubernetes | Microsoft Docs
description:
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 08/24/2018
ms.topic: conceptual
ms.prod: sql
---

# Data persistence with SQL Server Aris on Kubernetes

[Persistent Volumes](https://kubernetes.io/docs/concepts/storage/persistent-volumes/) provide a plugin model for storage in Kubernetes where how storage is provided is completed abstracted from how it is consumed. Therefore, you can bring your own highly available storage and plug it into the SQL Server Aris cluster. This gives you full control over the type of storage, availability, and performance that you require. Kubernetes supports various kinds of storage solutions including Azure disks/files, NFS, local storage, and more.

## Configure persistent volumes

The way SQL Server Aris consumes these persistent volumes is by using [Storage Classes](https://kubernetes.io/docs/concepts/storage/storage-classes/). You can create different storage classes for different kind of storage and specify them at the Aris cluster deployment time. You can configure which storage class to use for which purpose (pool). SQL Server Aris creates [persistent volume claims](https://kubernetes.io/docs/concepts/storage/persistent-volumes/#persistentvolumeclaims) with the specified storage class name for each pod that requires persistent volumes. It then mounts the corresponding persistent volume(s) in the pod.

> [!NOTE]
> For CTP 2.0, you can only have one storage class with static size and access mode for the whole cluster.

## Deployment settings

To use persistent storage during deployment, configure the **USE_PERSISTENT_VOLUME** and **STORAGE_CLASS_NAME** flags with mssqlctl. **USE_PERSISTENT_VOLUME** is set to false by default, and, in this case, SQL Server Aris uses emptyDir mounts. If you set the flag to true, you must also provide **STORAGE_CLASS_NAME** as a parameter at the deployment time.

## AKS/ACS storage classes

AKS and ACS both come with two built-in storage classes **default** and **premium-storage** along with dynamic provisioner for them. You can specify either of those or create their own storage class for deploying Aris cluster with persistent storage enabled.

## Minikube storage class

Minikube comes with a built-in storage class called **standard** along with a dynamic provisioner for it.

## Kubeadm

Kubeadm does not come with a built-in storage class; therefore, we have created scripts to set up persistent volumes and storage classes using local storage or [Rook](https://github.com/rook/rook) storage.

## On-premises cluster

On-premise clusters obviously do not come with any built-in storage class, therefore you must set up persistent volumes/provisioners beforehand and then use the corresponding storage classes during SQL Server Aris deployment.

## Next steps

For complete documentation about volumes in Kubernetes, see the [Kubernetes documentation on Volumes](https://kubernetes.io/docs/concepts/storage/volumes/).

For more information about deploying SQL Server Aris, see [How to deploy SQL Server Aris on Kubernetes](sql-server-aris-deployment-guidance.md).
