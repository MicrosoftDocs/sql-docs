---
title: Configure Azure Kubernetes Service for SQL Server 2019 CTP 2.0 deployments | Microsoft Docs
description:
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 10/01/2018
ms.topic: conceptual
ms.prod: sql
---

# Configure Azure Kubernetes Service for SQL Server 2019 CTP 2.0

Azure Kubernetes Service (AKS) makes it simple to create, configure, and manage a cluster of virtual machines that are preconfigured with a Kubernetes cluster to run containerized applications. 

This enables you to use your existing skills or draw upon a large and growing body of community expertise, to deploy and manage container-based applications on Microsoft Azure.

This article describes the steps to deploy Kubernetes on AKS using Azure CLI. If you don't have an Azure subscription, create a free account before you begin.

## Prerequisites

- For an AKS environment, the minimum VM requirement is at least two agent VMs (in addition to master) of a minimum size [Standard_DS3_V2](https://docs.microsoft.com/azure/virtual-machines/windows/sizes-general#dsv2-series). Minimum resources required per VM are 4 CPUs and 14 GB of memory.
  
   > [!NOTE]
   > If you plan to run big data jobs or multiple Spark applications, the minimum size is [Standard_D8_v3](https://docs.microsoft.com/azure/virtual-machines/windows/sizes-general#dv3-series-sup1sup), and the minimum resources required per VM are 8 CPUs and 32 GB of memory.

- This section requires that you be running the Azure CLI version 2.0.4 or later. If you need to install or upgrade, see [Install Azure CLI 2.0](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli). Run `az --version` to find the version if needed.

- Install [kubectl](https://kubernetes.io/docs/tasks/tools/install-kubectl/). SQL Server Big Data cluster requires any minor version within the 1.10 version range for Kubernetes, for both server and client. To install a specific version on kubectl client, see [Install kubectl binary via curl](https://kubernetes.io/docs/tasks/tools/install-kubectl/#install-kubectl). For AKS you will need to use `--kubernetes-version` parameter to specify a version different than default. Note that at the CTP2.0 release timeframe, AKS only supports 1.10.7 and 1.10.8 versions. 


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

2. If you have multiple subscriptions you can view all of your subscriptions by running the following command:

   ```bash
   az account list
   ```

3. If you want to change to a different subscription you can run this command:

   ```bash
   az account set --subscription <subscription id>
   ```

4. Create a resource group with the **az group create** command. The following example creates a resource group named `sqlbigdatagroup` in the `westus2` location.

   ```bash
   az group create --name sqlbigdatagroup --location westus2
   ```

## Create a Kubernetes cluster

1. Create a Kubernetes cluster in AKS with the [az aks create](https://docs.microsoft.com/cli/azure/aks) command. The following example creates a Kubernetes cluster named *kubcluster* with one Linux master node and four Linux agent nodes. Make sure you create the AKS cluster in the same resource group that you used in the previous sections.

    ```bash
   az aks create --name kubcluster \
    --resource-group sqlbigdatagroup \
    --generate-ssh-keys \
    --node-vm-size Standard_DS4_v2 \
    --node-count 4 \
    --kubernetes-version 1.10.7
    ```

    You can increase or decrease the default agent count by changing the `--node-count <n>` where `<n>` is the number of agent nodes you want to have.

    After several minutes, the command completes and returns JSON-formatted information about the cluster.

1. Save the JSON output from the previous command for later use.

## Connect to the cluster

1. To configure kubectl to connect to your Kubernetes cluster, run the [az aks get-credentials](https://docs.microsoft.com/en-us/cli/azure/aks?view=azure-cli-latest#az-aks-get-credentials) command. This step downloads credentials and configures the kubectl CLI to use them.

   ```bash
   az aks get-credentials --resource-group=sqlbigdatagroup --name kubcluster
   ```

1. To verify the connection to your cluster, use the [kubectl get](https://kubernetes.io/docs/reference/generated/kubectl/kubectl-commands) command to return a list of the cluster nodes.  The example below shows the output if you were to have 1 master and 3 agent nodes.

   ```bash
   kubectl get nodes
   ```

## Next steps

The steps in this article configured a Kubernetes cluster in AKS. The next step is to deploy SQL Server 2019 Big Data to the cluster.

[Deploy SQL Server 2019 Big Data cluster on Kubernetes](quickstart-big-data-cluster-deploy.md)
