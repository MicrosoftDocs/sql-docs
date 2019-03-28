---
title: Configure Azure Kubernetes Service
titleSuffix: SQL Server 2019 big data clusters
description: Learn how to configure Azure Kubernetes Service (AKS) for SQL Server 2019 big data cluster (preview) deployments.
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 02/28/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
ms.custom: seodec18
---

# Configure Azure Kubernetes Service for SQL Server 2019 big data cluster (preview) deployments

This article describes how to configure Azure Kubernetes Service (AKS) for SQL Server 2019 big data cluster (preview) deployments.

AKS makes it simple to create, configure, and manage a cluster of virtual machines that are preconfigured with a Kubernetes cluster to run containerized applications. This enables you to use your existing skills or draw upon a large and growing body of community expertise, to deploy and manage container-based applications on Microsoft Azure.

This article describes the steps to deploy Kubernetes on AKS using Azure CLI. If you don't have an Azure subscription, create a free account before you begin.

> [!TIP] 
> For a sample python script that deploys both AKS and SQL Server big data cluster, see [Quickstart: Deploy SQL Server big data cluster on Azure Kubernetes Service (AKS)](quickstart-big-data-cluster-deploy.md).

## Prerequisites

- [Deploy the SQL Server 2019 big data tools](deploy-big-data-tools.md):
   - **Kubectl**
   - **Azure Data Studio**
   - **SQL Server 2019 extension**
   - **Azure CLI**

- Minimum 1.10 version for Kubernetes server. For AKS, you need to use `--kubernetes-version` parameter to specify a version different than the default.

- For an optimal experience while validating basic scenarios on AKS, use:
   - Minimum of 3 agent VMs
   - 4 vCPUs per VM
   - 32 GB of memory per VM

   > [!TIP]
   > Azure infrastructure offers multiple size options for VMs, see [here](https://docs.microsoft.com/azure/virtual-machines/windows/sizes) for selections in the region you are planning to deploy.

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

1. Create a resource group with the **az group create** command. The following example creates a resource group named `sqlbigdatagroup` in the `westus2` location.

   ```azurecli
   az group create --name sqlbigdatagroup --location westus2
   ```

## Create a Kubernetes cluster

1. Create a Kubernetes cluster in AKS with the [az aks create](https://docs.microsoft.com/cli/azure/aks) command. The following example creates a Kubernetes cluster named *kubcluster* with one Linux agent node of size **Standard_L8s**. Make sure you create the AKS cluster in the same resource group that you used in the previous sections.

    ```azurecli
   az aks create --name kubcluster \
    --resource-group sqlbigdatagroup \
    --generate-ssh-keys \
    --node-vm-size Standard_L8s \
    --node-count 1 \
    --kubernetes-version 1.10.9
    ```

   You can increase or decrease the number of Kubernetes agent nodes by changing the `--node-count <n>` where `<n>` is the number of agent nodes you want to use. This does not include the master Kubernetes node, which is managed behind the scenes by AKS. The previous example only uses a single node for evaluation purposes.

   After several minutes, the command completes and returns JSON-formatted information about the cluster.

1. Save the JSON output from the previous command for later use.

## Connect to the cluster

1. To configure kubectl to connect to your Kubernetes cluster, run the [az aks get-credentials](https://docs.microsoft.com/cli/azure/aks?view=azure-cli-latest#az-aks-get-credentials) command. This step downloads credentials and configures the kubectl CLI to use them.

   ```azurecli
   az aks get-credentials --resource-group=sqlbigdatagroup --name kubcluster
   ```

1. To verify the connection to your cluster, use the [kubectl get](https://kubernetes.io/docs/reference/generated/kubectl/kubectl-commands) command to return a list of the cluster nodes.  The example below shows the output if you were to have 1 master and 3 agent nodes.

   ```
   kubectl get nodes
   ```

## Next steps

The steps in this article configured a Kubernetes cluster in AKS. The next step is to deploy SQL Server 2019 big data to the cluster. For more information on how to deploy big data clusters, see the following article:

[How to deploy SQL Server big data clusters on Kubernetes](deployment-guidance.md)
