---
title: Configure Azure Kubernetes Service
titleSuffix: SQL Server 2019 big data clusters
description: Learn how to configure Azure Kubernetes Service (AKS) for SQL Server 2019 big data cluster (preview) deployments.
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 12/06/2018
ms.topic: conceptual
ms.prod: sql
ms.custom: seodec18
---

# Configure Azure Kubernetes Service for SQL Server 2019 big data cluster (preview) deployments

This article describes how to configure Azure Kubernetes Service (AKS) for SQL Server 2019 big data cluster (preview) deployments.

AKS makes it simple to create, configure, and manage a cluster of virtual machines that are preconfigured with a Kubernetes cluster to run containerized applications. This enables you to use your existing skills or draw upon a large and growing body of community expertise, to deploy and manage container-based applications on Microsoft Azure.

This article describes the steps to deploy Kubernetes on AKS using Azure CLI. If you don't have an Azure subscription, create a free account before you begin.

> [!TIP] 
> For a sample python script that deploys both AKS and SQL Server big data cluster, see [Deploy a SQL Server big data cluster on Azure Kubernetes Service (AKS)](https://github.com/Microsoft/sql-server-samples/tree/master/samples/features/sql-big-data-cluster/deployment/aks).

## Prerequisites

- For an AKS environment, for an optimal experience while validating basic scenarios, we recommend  at least three agent VMs (in addition to master), with at least 4 vCPUs and 32 GB of memory each. Azure infrastructure offers multiple size options for VMs, see [here](https://docs.microsoft.com/azure/virtual-machines/windows/sizes) for selections in the region you are planning to deploy.
  
- This section requires that you be running the Azure CLI version 2.0.4 or later. If you need to install or upgrade, see [Install Azure CLI 2.0](https://docs.microsoft.com/cli/azure/install-azure-cli). Run `az --version` to find the version if needed.

- Install [kubectl](https://kubernetes.io/docs/tasks/tools/install-kubectl/) with a minimum of version 1.10. If you want to install a specific version on kubectl client, see [Install kubectl binary via curl](https://kubernetes.io/docs/tasks/tools/install-kubectl/#install-kubectl). 

- Same 1.10 minimum version applies to Kubernetes server. For AKS, you need to use `--kubernetes-version` parameter to specify a version different than the default.

> [!NOTE]
Note that the client/server version skew that is supported is +/-1 minor version. The Kubernetes documentation states that  "a client should be skewed no more than one minor version from the master, but may lead the master by up to one minor version. For example, a v1.3 master should work with v1.1, v1.2, and v1.3 nodes, and should work with v1.2, v1.3, and v1.4 clients." For more information, see [Kubernetes supported releases and component skew](https://github.com/kubernetes/community/blob/master/contributors/design-proposals/release/versioning.md#supported-releases-and-component-skew).

Also, note that `az aks kubernetes install-cli` will install kubectl client with a version lower that the required 1.10. Follow above instructions to install the right version of kubectl client.

## Create a resource group

An Azure resource group is a logical group in which Azure resources are deployed and managed. The following steps sign into Azure and create a resource group for the AKS cluster.

> [!TIP]
> If you are using Windows, use PowerShell for the remainder of the steps.

1. At the command prompt, run the following command and follow the prompts to login to your Azure subscription:

    ```bash
    az login
    ```

1. If you have multiple subscriptions you can view all of your subscriptions by running the following command:

   ```bash
   az account list
   ```

1. If you want to change to a different subscription you can run this command:

   ```bash
   az account set --subscription <subscription id>
   ```

1. Create a resource group with the **az group create** command. The following example creates a resource group named `sqlbigdatagroup` in the `westus2` location.

   ```bash
   az group create --name sqlbigdatagroup --location westus2
   ```

## Create a Kubernetes cluster

1. Create a Kubernetes cluster in AKS with the [az aks create](https://docs.microsoft.com/cli/azure/aks) command. The following example creates a Kubernetes cluster named *kubcluster* with three Linux agent nodes. Make sure you create the AKS cluster in the same resource group that you used in the previous sections.

    ```bash
   az aks create --name kubcluster \
    --resource-group sqlbigdatagroup \
    --generate-ssh-keys \
    --node-vm-size Standard_L4s \
    --node-count 3 \
    --kubernetes-version 1.10.8
    ```

   You can increase or decrease the number of Kubernetes agent nodes by changing the `--node-count <n>` where `<n>` is the number of agent nodes you want to use. This does not include the master Kubernetes node, which is managed behind the scenes by AKS. So in the example above, there are **3** VMs of size **Standard_L4s** used for the agent nodes of your AKS cluster.

   After several minutes, the command completes and returns JSON-formatted information about the cluster.

1. Save the JSON output from the previous command for later use.

## Connect to the cluster

1. To configure kubectl to connect to your Kubernetes cluster, run the [az aks get-credentials](https://docs.microsoft.com/cli/azure/aks?view=azure-cli-latest#az-aks-get-credentials) command. This step downloads credentials and configures the kubectl CLI to use them.

   ```bash
   az aks get-credentials --resource-group=sqlbigdatagroup --name kubcluster
   ```

1. To verify the connection to your cluster, use the [kubectl get](https://kubernetes.io/docs/reference/generated/kubectl/kubectl-commands) command to return a list of the cluster nodes.  The example below shows the output if you were to have 1 master and 3 agent nodes.

   ```bash
   kubectl get nodes
   ```

## Next steps

The steps in this article configured a Kubernetes cluster in AKS. The next step is to deploy SQL Server 2019 big data to the cluster.

[Quickstart: Deploy SQL Server big data cluster on Azure Kubernetes Service (AKS)](quickstart-big-data-cluster-deploy.md)
