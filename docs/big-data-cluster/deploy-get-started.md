---
title: Get started
titleSuffix: SQL Server Big Data Clusters
description: Learn the steps and resources for deploying SQL Server Big Data Clusters.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: wiassaf
ms.date: 06/22/2020
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
ms.custom: intro-deployment
---

# Get started with [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] deployment

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

This article provides an overview of how to deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]. The article introduces you to concepts and provides a framework for understanding the deployment scenarios. Your specific deployment steps vary based on your platform choices for the client and server. For introduction on [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see [[!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]](big-data-cluster-overview.md)

For other SQL Server deployment scenarios, see:

- [Windows](../database-engine/install-windows/install-sql-server.md)
- [Linux](../linux/sql-server-linux-setup.md)
- [Docker containers](../linux/sql-server-linux-docker-container-deployment.md)

## Quick Introduction 

Watch this 9-minute video for an overview of how to deploy big data clusters:

> [!VIDEO https://channel9.msdn.com/Shows/Data-Exposed/Big-Data-Clusters-deployment-overview/player?WT.mc_id=dataexposed-c9-niner]


> [!TIP]
> To quickly get an environment with Kubernetes and big data cluster deployed to help you ramp up on its capabilities, use one of the sample scripts pointed to in [the scripts section](#scripts). After deployment, to manage the cluster use the [client tools](#tools) in the following section.


## <a id="tools"></a> Client tools

Big data clusters require a specific set of client tools. Before you deploy a big data cluster to Kubernetes, you should install the tools required for your deployment. Specific tools are required for different scenarios. Each article should explain the prerequisite tools for performing a specific task. For a full list of tools and installation links, see [Install SQL Server 2019 big data tools](deploy-big-data-tools.md).

## Kubernetes

Big data clusters are deployed as a series of interrelated containers that are managed in [Kubernetes](https://kubernetes.io/docs/home). You can host Kubernetes in a variety of ways. Even if you already have an existing Kubernetes environment, you should review the related requirements for big data clusters.

- **Azure Kubernetes Service (AKS)**: AKS allows you to deploy a managed Kubernetes cluster in Azure. You only manage and maintain the agent nodes. With AKS, you don't have to provision your own hardware for the cluster. It is also easy to use a [python script](quickstart-big-data-cluster-deploy.md) or a [deployment notebook](notebooks-deploy.md) to create the AKS cluster and deploy the big data cluster in one step. For more information about configuring AKS for a big data cluster deployment, see [Configure Azure Kubernetes Service for [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)] deployments](deploy-on-aks.md).

- **Azure Red Hat OpenShift (ARO)**: ARO allows you to deploy a managed Red Hat OpenShift cluster in Azure. You only manage and maintain the agent nodes. With ARO, you don't have to provision your own hardware for the cluster. It is also easy to use a [python script](quickstart-big-data-cluster-deploy-aro.md) to create the ARO cluster and deploy the big data cluster in one step. This deployment model is introduced in SQL Server 2019 CU5. 

- **Multiple machines**: You can also deploy Kubernetes to multiple Linux machines, which could be physical servers or virtual machines. The [kubeadm](https://kubernetes.io/docs/setup/independent/create-cluster-kubeadm/) tool can be used to create the Kubernetes cluster. You can use a [bash script](deployment-script-single-node-kubeadm.md) to automate this type of deployment. This method works well if you already have existing infrastructure that you want to use for your big data cluster. For more information about using **kubeadm** deployments with big data clusters, see [Configure Kubernetes on multiple machines for [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)] deployments](deploy-with-kubeadm.md).

- **Red Hat OpenShift**: Deploy to your own Red Hat OpenShift cluster. For information, see [Deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] on OpenShift on-premises and Azure Red Hat OpenShift](deploy-openshift.md). This deployment model is introduced in SQL Server 2019 CU5.

## Deploy a big data cluster

After configuring Kubernetes, you deploy a big data cluster with the `azdata bdc create` command. When deploying, you can take several different approaches.

- If you are deploying to a dev-test environment, you can choose to use one of the [default configurations](deployment-guidance.md#deploy) provided by **azdata**.

- To customize your deployment, you can create and use your own [deployment configuration files](deployment-guidance.md#configfile).

- For a completely unattended installation, you can pass all other settings in  environment variables. For more information, see [unattended deployments](deployment-guidance.md#unattended).


## <a id="scripts"></a> Deployment scripts

Deployment scripts can help deploy both Kubernetes and big data clusters in a single step. They also often provide default values for big data cluster settings. You can customize any deployment script by creating your own version that configures the big data cluster deployment differently.

The following deployment scripts are currently available:

- [Python script -- Deploy a big data cluster on Azure Kubernetes Service (AKS)](quickstart-big-data-cluster-deploy.md)
- [Bash script -- Deploy a big data cluster to a single node kubeadm cluster](deployment-script-single-node-kubeadm.md)

## Deployment notebooks

You can also deploy a big data cluster by running an Azure Data Studio notebook. For more information on how to use a notebook to deploy on AKS, see the following article:

- [Deploy a big data cluster with Azure Data Studio Notebooks](notebooks-deploy.md).

## Next steps

After you successfully deploy a big data cluster, [connect to the cluster](connect-to-big-data-cluster.md) and consider [loading sample data](tutorial-load-sample-data.md) for use with several walkthroughs.
