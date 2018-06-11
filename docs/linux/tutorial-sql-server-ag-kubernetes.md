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

## Prerequisites

Before starting this tutorial, 

* [Create an Azure Kubernetes Service(AKS) cluster](http://docs.microsoft.com/azure/aks/create-cluster.md) in a resource group named `myResourceGroup`. Name the AKS cluster `myAKSCluster`.

   ```azurecli-interactive
   az aks create --resource-group myResourceGroup --name myAKSCluster
   ```

## Create storage


## Create the deployment


## Verify failure and recovery

## Summary

## Next steps

> [!div class="nextstepaction"]
>[Introduction to Kubernetes](http://docs.microsoft.com/en-us/azure/aks/intro-kubernetes)


