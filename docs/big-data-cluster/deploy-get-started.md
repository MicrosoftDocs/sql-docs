---
title: Get started
titleSuffix: SQL Server 2019 big data clusters
description: Learn the steps and resources for deploying SQL Server 2019 big data clusters (preview).
author: rothja
ms.author: jroth
manager: craigg
ms.date: 03/05/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Get started with SQL Server 2019 big data clusters

This article provides an overview of how to deploy a [SQL Server 2019 big data cluster (preview)](big-data-cluster-overview.md). It is meant to orient you to the concepts and provide a framework for understanding the other deployment articles in this section. Your specific deployment steps vary based on your platform choices for the client and server.

## <a id="tools"></a> Client tools

Big data clusters require a specific set of client tools. Before you deploy a big data cluster to Kubernetes, you should at a minimum install the following tools:

| Tool | Description |
|---|---|
| **mssqlctl** | Deploys and manages big data clusters. |
| **kubectl** | Creates and manages the underlying Kubernetes cluster. |
| **Azure Data Studio** | Graphical interface for using the big data cluster. |
| **SQL Server 2019 extension** | Azure Data Studio extension that enables big data cluster features. |

Other tools are required for different scenarios. Each article should explain the prerequisite tools for performing a specific task. For a full list of tools and installation links, see [Install SQL Server 2019 big data tools](deploy-big-data-tools.md).

## Kubernetes

Big data clusters are deployed as a series of interrelated containers that are managed in [Kubernetes](https://kubernetes.io/docs/home). You can host Kubernetes in a variety of ways:

- [Azure Kubernetes Services (AKS)](deploy-on-aks.md)
- [Multiple machines](deploy-with-kubeadm.md)
- [Minikube](deploy-on-minikube.md)

Even if you already have an existing Kubernetes environment, you should review the related information in the previous links for big data cluster prerequisites.

## Environment variables

The actual deployment is configured by a series of environment variables before running the **mssqlctl** command to create the big data cluster. You can customize your deployment by configuring these variables. For a full list of big data cluster environment variables, see [Define environment variables](deployment-guidance.md#env).

## Deployment scripts

Deployment scripts can help deploy both Kubernetes and big data clusters in a single step. They also often provide default values for the required environment variables. For an example of a deployment script for big data cluster on Azure Kubernetes Service (AKS), see [Deploy a SQL Server 2019 big data cluster with a deployment script (AKS)](quickstart-big-data-cluster-deploy.md).

You can customize any deployment script by creating your own version that configures the big data cluster environment variables differently.

## Deploy a big data cluster

To deploy Kubernetes and a big data cluster with a single script, see the following example:

- [Deploy a SQL Server 2019 big data cluster with a deployment script (AKS)](quickstart-big-data-cluster-deploy.md)

To manually deploy a big data cluster to Kubernetes, see the following article:

- [How to deploy SQL Server big data clusters on Kubernetes](deployment-guidance.md)

## Next steps

After you successfully deploy a big data cluster, [connect to the cluster](connect-to-big-data-cluster.md) and try out the [tutorials](tutorial-load-sample-data.md).