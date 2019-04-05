---
title: Get started
titleSuffix: SQL Server big data clusters
description: Learn the steps and resources for deploying SQL Server 2019 big data clusters (preview).
author: rothja
ms.author: jroth
manager: craigg
ms.date: 03/18/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Get started with SQL Server big data clusters

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

This article provides an overview of how to deploy a [SQL Server 2019 big data cluster (preview)](big-data-cluster-overview.md). It is meant to orient you to the concepts and provide a framework for understanding the other deployment articles in this section. Your specific deployment steps vary based on your platform choices for the client and server.

## <a id="tools"></a> Client tools

Big data clusters require a specific set of client tools. Before you deploy a big data cluster to Kubernetes, you should install the following tools:

| Tool | Description |
|---|---|
| **mssqlctl** | Deploys and manages big data clusters. |
| **kubectl** | Creates and manages the underlying Kubernetes cluster. |
| **Azure Data Studio** | Graphical interface for using the big data cluster. |
| **SQL Server 2019 extension** | Azure Data Studio extension that enables big data cluster features. |

Other tools are required for different scenarios. Each article should explain the prerequisite tools for performing a specific task. For a full list of tools and installation links, see [Install SQL Server 2019 big data tools](deploy-big-data-tools.md).

## Kubernetes

Big data clusters are deployed as a series of interrelated containers that are managed in [Kubernetes](https://kubernetes.io/docs/home). You can host Kubernetes in a variety of ways. Even if you already have an existing Kubernetes environment, you should review the related requirements for big data clusters.

- **Azure Kubernetes Service (AKS)**: AKS allows you to deploy a managed Kubernetes cluster in Azure. You only manage and maintain the agent nodes. With AKS, you don't have to provision your own hardware for the cluster. It is also easy to use a big data cluster [deployment script](quickstart-big-data-cluster-deploy.md) to create the AKS cluster and deploy the big data cluster in one step. For more information about using AKS with big data clusters, see [Configure Azure Kubernetes Service for SQL Server 2019 big data cluster (preview) deployments](deploy-on-aks.md).

- **Multiple machines**: You can also deploy Kubernetes to multiple Linux machines, which could be physical servers or virtual machines. The [kubeadm](https://kubernetes.io/docs/setup/independent/create-cluster-kubeadm/) tool can be used to create the Kubernetes cluster. This method works well if you already have existing infrastructure that you want to use for your big data cluster. For more information about using **kubeadm** deployments with big data clusters, see [Configure Kubernetes on multiple machines for SQL Server 2019 big data cluster (preview) deployments](deploy-with-kubeadm.md).

- **Minikube**: Minikube allows you to run Kubernetes locally on a single server. It is a good option if you are trying out big data clusters or need to use it in a testing or development scenario. For more information about using Minikube, see the [Minikube documentation](https://kubernetes.io/docs/setup/minikube/). For specific requirements for using Minikube with big data clusters, see [Configure minikube for SQL Server 2019 big data cluster deployments](deploy-on-minikube.md).

## Deployment scripts

Deployment scripts can help deploy both Kubernetes and big data clusters in a single step. They also often provide default values for the required environment variables. For an example of a deployment script for big data cluster on Azure Kubernetes Service (AKS), see [Deploy a SQL Server 2019 big data cluster with a deployment script (AKS)](quickstart-big-data-cluster-deploy.md).

You can customize any deployment script by creating your own version that configures the big data cluster environment variables differently.

## Deploy a big data cluster

To deploy Kubernetes and a big data cluster to AKS with a single script, see the following example:

- [Deploy a SQL Server 2019 big data cluster with a deployment script (AKS)](quickstart-big-data-cluster-deploy.md)

For detailed deployment guidance for deploying big data clusters using AKS, kubeadm, and MiniKube, see the following article:

- [How to deploy SQL Server big data clusters on Kubernetes](deployment-guidance.md)

## Next steps

After you successfully deploy a big data cluster, [connect to the cluster](connect-to-big-data-cluster.md) and consider [loading sample data](tutorial-load-sample-data.md) for use with several walkthroughs.