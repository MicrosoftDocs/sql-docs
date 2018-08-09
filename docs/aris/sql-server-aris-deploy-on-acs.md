---
title: Configure Kubernetes on ACS for SQL Server vNext deployments | Microsoft Docs
description:
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 08/06/2018
ms.topic: conceptual
ms.prod: sql
---

# Configure Kubernetes on Azure Container Service (ACS) for SQL Server vNext

Azure Container Service makes it simple to create, configure, and manage a cluster of virtual machines that are preconfigured with a Kubernetes cluster to run containerized applications. 

This enables you to use your existing skills or draw upon a large and growing body of community expertise, to deploy and manage container-based applications on Microsoft Azure.

This article describes the steps to deploy Kubernetes on Azure Container Services using Azure CLI. If you don't have an Azure subscription, create a free account before you begin.

## Prerequisites

- For an ACS environment, the minimum VM requirement is at least two agent VMs of size Standard_DS3_V2 which contains 4 CPUs and 14 GB of memory or larger, especially in terms of memory.

- If not already installed, install git locally on [Windows](https://git-for-windows.github.io/), [Linux, or Mac](https://git-scm.com/book/en/v2/Getting-Started-Installing-Git).

- Install [kubectl](https://kubernetes.io/docs/tasks/tools/install-kubectl/).

- This section requires that you be running the Azure CLI version 2.0.4 or later. If you need to install or upgrade, see [Install Azure CLI 2.0](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli). Run `az --version` to find the version if needed.

## Create ssh keys

SSH keys are required for secure access to SSH into the VMs in Azure Container Service.  You will use the tool ssh-keygen to create password-protected SSH keys that will be used to access the VMs created in Azure Container Service.

### Windows

If you are on Windows, follow these steps to create SSH keys:

1. Open the **Git Bash shell** from the Start menu.

1. Run the following commands:

   ```bash
   mkdir %systemdrive%%homepath%\sqlvnextkeys
   cd %systemdrive%%homepath%\sqlvnextkeys
   ssh-keygen -t rsa -b 2048 -f ./id_rsa -N yourpassword
   ```

### Linux

If you are on Linux or MacOS, follow these steps to create SSH keys:

1. Open a terminal.

1. Run the following commands:

    ```bash
    mkdir ~/sqlvnextkeys
    cd ~/sqlvnextkeys
    ssh-keygen -t rsa -b 2048 -f ./id_rsa -N yourpassword
    ```

## Create a resource group

An Azure resource group is a logical group in which Azure resources are deployed and managed. The following steps sign into Azure and create a resource group for the ACS cluster.

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

1. Create a resource group with the **az group create** command. The following example creates a resource group named `sqlvnextgroup` in the `westus2` location.

   ```bash
   az group create --name sqlvnextgroup --location westus2
   ```

   > [!NOTE]
   > We recommend using only the following locations:  ukwest, uksouth, westcentralus, westus2, canadaeast, canadacentral, westindia, southindia, centralindia.

## Create a service principal

Create a service principal that Kubernetes will be used to interact with Azure APIs to manage resources within the service.

1. Find your subscription ID:

   ```bash
   az account show
   ```

   Your subscription ID is the **ID** property value in the JSON that is returned by the previous command.

1. Copy that and replace the `<subscriptionID>` placeholder in the following command to create the service principal with your subscription ID. Make sure you create the service principal in the same resource group that you previously created.

   ```bash
   az ad sp create-for-rbac --role="Contributor" --scopes="/subscriptions/<subscriptionID>/resourceGroups/sqlvnextgroup"
   ```

1. Save the JSON output from the previous command for later use.

> [!IMPORTANT]
> Wait for one to two minutes for the newly created service principal to be available for use in your resource group.

## Create a Kubernetes cluster

1. Create a Kubernetes cluster in Azure Container Service with the [az acs create](https://docs.microsoft.com/cli/azure/acs) command. The following example creates a Kubernetes cluster named *sqlvnextcluster* with one Linux master node and two Linux agent nodes. Make sure you create the ACS cluster in the same resource group that you used in the previous sections.

    ```bash
    az acs create --orchestrator-type kubernetes --resource-group sqlvnextgroup --name sqlvnextcluster --ssh-key-value=~/sqlvnextkeys/id_rsa.pub --agent-vm-size=Standard_DS3_v2 --master-vm-size=Standard_DS3_v2 --master-storage-profile=ManagedDisks --agent-storage-profile=ManagedDisks --agent-count=2 --orchestrator-version=1.10.6
    ```

    You can increase or decrease the default agent count by adding `--agent-count <n>` to the az acs create command where `<n>` is the number of agent nodes you want to have.

    After several minutes, the command completes and returns JSON-formatted information about the cluster.

1. Save the JSON output from the previous command for later use.

## Connect to the cluster

To manage a Kubernetes cluster, use [kubectl](https://kubernetes.io/docs/user-guide/kubectl/), the Kubernetes command-line client. If you haven’t already installed it, you can use the [az acs kubernetes install-cli](https://docs.microsoft.com/cli/azure/acs/kubernetes) command to install.

```bash
az acs kubernetes install-cli
```

1. To configure kubectl to connect to your Kubernetes cluster, run the [az acs kubernetes get-credentials](https://docs.microsoft.com/en-us/cli/azure/acs/kubernetes) command. This step downloads credentials and configures the kubectl CLI to use them.

   ```bash
   az acs kubernetes get-credentials --resource-group=sqlvnextgroup --name sqlvnextcluster --ssh-key-file=<path to the id_rsa key file from above>
   ```

   You will be asked to enter the password that you used when you created the SSH keys.

1. To verify the connection to your cluster, use the [kubectl get](https://kubernetes.io/docs/user-guide/kubectl/v1.6/) command to return a list of the cluster nodes.  The example below shows the output if you were to have 1 master and 3 agent nodes.

   ```bash
   kubectl get nodes
   ```

<!--Insert screenshot here when closer to public CTP-->

## Give cluster admin permissions

1. Before running the deploying SQL Server vNext, you must grant cluster admin permission to the default service account. Use the following kubectl command:

   ```bash
   kubectl create clusterrolebinding add-on-cluster-admin --clusterrole=cluster-admin --serviceaccount=default:default
   ```

1. Allow all services in your cluster to be a cluster-admin:

   ```bash
   kubectl create clusterrolebinding admin-binding --clusterrole=cluster-admin --group=system:serviceaccounts
   ```

   > [!NOTE]
   > This is a temporary work around that should only be used for test clusters.

## Next steps

The steps in this article configured a Kubernetes cluster in ACS. The next step is to deploy SQL Server vNext to the cluster.

[Deploy SQL Server vNext on Kubernetes](quickstart-sql-server-aris-get-started.md)