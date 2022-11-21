---
title: Configure Azure Kubernetes Service
titleSuffix: SQL Server Big Data Clusters
description: Learn how to configure Azure Kubernetes Service (AKS) for SQL Server 2019 big data cluster deployments.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 07/02/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
ms.metadata: seo-lt-2019
---

# Configure Azure Kubernetes Service for SQL Server big data cluster deployments

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

This article describes how to configure Azure Kubernetes Service (AKS) for [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)] deployments.

AKS makes it simple to create, configure, and manage a cluster of virtual machines that are preconfigured with a Kubernetes cluster to run containerized applications. This enables you to use your existing skills or draw upon a large and growing body of community expertise, to deploy and manage container-based applications on Microsoft Azure.

This article describes the steps to deploy Kubernetes on AKS using Azure CLI. If you don't have an Azure subscription, create a free account before you begin.

> [!TIP]
> You can also script the deployment of AKS and a big data cluster in one step. For more information, see how to do this in a [python script](quickstart-big-data-cluster-deploy.md) or an Azure Data Studio [notebook](notebooks-deploy.md).

## Prerequisites

- [Deploy the SQL Server 2019 big data tools](deploy-big-data-tools.md):
   - **Kubectl**
   - **Azure Data Studio**
   - **SQL Server 2019 extension**
   - **Azure CLI**

- Minimum 1.13 version for Kubernetes server. For AKS, you need to use `--kubernetes-version` parameter to specify a version different than the default.

- To ensure a successful deployment and an optimal experience while validating basic scenarios on AKS, you can use a single node or a multi-node AKS cluster, with these resources available:
   - 8 vCPUs across all nodes
   - 64 GB of memory per VM
   - 24 or more attached disks across all nodes

   > [!TIP]
   > Azure infrastructure offers multiple size options for VMs, see [here](/azure/virtual-machines/windows/sizes) for selections in the region you are planning to deploy.

## Create a resource group

An Azure resource group is a logical group in which Azure resources are deployed and managed. The following steps sign into Azure and create a resource group for the AKS cluster.

1. At the command prompt, run the following command and follow the prompts to login to your Azure subscription:

    ```azurecli
    az login
    ```

1. If you have multiple subscriptions you can view all of your subscriptions by running the following command:

   ```azurecli
   az account list
   ```

1. If you want to change to a different subscription you can run this command:

   ```azurecli
   az account set --subscription <subscription id>
   ```

1. Identify the Azure region where you want to deploy the cluster and the resources by using this command:

   ```azurecli
   az account list-locations -o table
   ```

1. Create a resource group with the **az group create** command. The following example creates a resource group named `sqlbdcgroup` in the `westus2` location.

   ```azurecli
   az group create --name sqlbdcgroup --location westus2
   ```

## Verify available Kubernetes versions

Use the latest available version of Kubernetes. The latest available version depends on the location where you are deploying the cluster. The following command returns Kubernetes versions available in a specific location.

Before you run the command, update the script. Replace `<Azure data center>` with the location of your cluster.

   **bash**

   ```bash
   az aks get-versions \
   --location <Azure data center> \
   --query orchestrators \
   --o table
   ```

   **PowerShell**

   ```powershell
   az aks get-versions `
   --location <Azure data center> `
   --query orchestrators `
   -o table
   ```

Choose the latest available version for your cluster. Record the version number. You will use it in the next step.

## Create a Kubernetes cluster

1. Create a Kubernetes cluster in AKS with the [az aks create](/cli/azure/aks) command. The following example creates a Kubernetes cluster named *kubcluster* with one Linux agent node of size **Standard_L8s**.

   Before you run the script, replace `<version number>` with the version number you identified in the previous step.

   Make sure you create the AKS cluster in the same resource group that you used in the previous sections.

   **bash:**

   ```bash
   az aks create --name kubcluster \
   --resource-group sqlbdcgroup \
   --generate-ssh-keys \
   --node-vm-size Standard_L8s \
   --node-count 1 \
   --kubernetes-version <version number>
   ```

   **PowerShell:**

   ```powershell
   az aks create --name kubcluster `
   --resource-group sqlbdcgroup `
   --generate-ssh-keys `
   --node-vm-size Standard_L8s `
   --node-count 1 `
   --kubernetes-version <version number>
   ```

   You can increase or decrease the number of Kubernetes agent nodes by changing the `--node-count <n>` where `<n>` is the number of agent nodes you want to use. This does not include the master Kubernetes node, which is managed behind the scenes by AKS. The previous example only uses a single node for evaluation purposes. You can also change the `--node-vm-size` to select an appropriate virtual machine size that matches your workload requirements. Use the `az vm list-sizes --location westus2 -o table` command to list available virtual machine sizes in your region.

   After several minutes, the command completes and returns JSON-formatted information about the cluster.

   > [!TIP]
   > If you get any errors creating the cluster in AKS, see the [troubleshooting section](#troubleshoot) of this article.

1. Save the JSON output from the previous command for later use.

## Connect to the cluster

1. To configure kubectl to connect to your Kubernetes cluster, run the [az aks get-credentials](/cli/azure/aks#az-aks-get-credentials) command. This step downloads credentials and configures the kubectl CLI to use them.

   ```azurecli
   az aks get-credentials --resource-group=sqlbdcgroup --name kubcluster
   ```

1. To verify the connection to your cluster, use the [kubectl get](https://kubernetes.io/docs/reference/generated/kubectl/kubectl-commands) command to return a list of the cluster nodes.  The example below shows the output if you were to have 1 master and 3 agent nodes.

   ```bash
   kubectl get nodes
   ```

## <a id="troubleshoot"></a> Troubleshooting

If you have any problems creating an Azure Kubernetes Service with the previous commands, try the following resolutions:

- Make sure that you have installed the [latest Azure CLI](/cli/azure/install-azure-cli).
- Try the same steps using a different resource group and cluster name.
- Refer to the detailed [troubleshooting documentation for AKS](/azure/aks/troubleshooting).

## Next steps

The steps in this article configured a Kubernetes cluster in AKS. The next step is to deploy a SQL Server 2019 big data cluster on the AKS Kubernetes cluster. For more information on how to deploy big data clusters, see the following article:

[How to deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] on Kubernetes](deployment-guidance.md)