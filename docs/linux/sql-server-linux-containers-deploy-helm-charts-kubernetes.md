---
title: "Quickstart: Deploy a SQL Server Linux container to Kubernetes using Helm charts"
description: Learn how to deploy a SQL Server on Linux container to Azure Kubernetes Service (AKS) using Helm charts.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: amitkh
ms.date: 08/17/2022
ms.service: sql
ms.subservice: linux
ms.topic: quickstart
---

# Quickstart: Deploy a SQL Server Linux container to Kubernetes using Helm charts

[!INCLUDE [sql-linux](../includes/applies-to-version/sql-linux.md)]

This quickstart takes you through the steps to deploy SQL Server on Linux containers to [Azure Kubernetes Service](/azure/aks/) (AKS) with [Helm charts](/azure/aks/quickstart-helm), from a Windows client machine.

AKS is a managed Kubernetes service for deploying and managing container clusters. [Helm](https://helm.sh/) is an open-source packaging tool that helps you install and manage the lifecycle of Kubernetes applications.

## Prerequisites

- An Azure subscription. If you don't have an Azure subscription, you can create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F).

- [Create an AKS cluster](/azure/aks/kubernetes-walkthrough#create-aks-cluster).

- Download and review the sample [Helm chart](https://github.com/microsoft/mssql-docker/tree/master/linux/sample-helm-chart) for this quickstart. The sample chart contains many configuration options for customizing your SQL Server deployment.

## Install client tools

On your Windows client machine, you'll need the following tools.

- [Azure CLI](/cli/azure/install-azure-cli) for Windows.
- [Helm](https://github.com/helm/helm/releases) for Windows.

If you prefer to use a different client operating system, you'll need to select the appropriate packages for that platform.

### Install kubectl using the Az PowerShell module

You'll use **kubectl** to interact with the Kubernetes cluster. For more information, see [az aks install-cli](/cli/azure/aks#az-aks-install-cli).

To install **kubectl**, run the following command from your Windows command prompt:

```cmd
az aks install-cli
```

> [!TIP]  
> You can add **kubectl** to your local `PATH` environment variable, so that you don't have to type in the full path each time.

## Connect kubectl to the AKS cluster

1. You need to merge the *context* of the AKS cluster, so that **kubectl** or **helm** commands run on that specific AKS cluster.

   To merge, run the command as described in the [Connect to AKS cluster](/azure/aks/kubernetes-walkthrough#connect-to-the-cluster) article:

   ```cmd
   az aks get-credentials --resource-group <resourcegroupname> --name <aks clustername>
   ```

   You should see the following output, where `<clustername>` is the cluster you provided, and `<username>` is your local Windows user account:

   ```output
   Merged "<clustername>" as current context in C:\Users\<username>\.kube.config
   ```

1. Confirm that the merge was successful by running `kubectl get nodes`. The output should show the nodes in the context of your AKS cluster.

   ```output
   NAME                    STATUS   ROLES   AGE    VERSION
   <aks-node>-vmss000000   Ready    agent   141d   v1.16.13
   <aks-node>-vmss000001   Ready    agent   141d   v1.16.13
   ```

## Review the sample Helm chart

You're now ready to deploy the SQL Server on AKS cluster via the Helm chart.

This quickstart provides a sample "as-is" [sample "as-is" Helm chart](https://github.com/microsoft/mssql-docker/tree/master/linux/sample-helm-chart). The sample is for reference only. Remember to review the `readme` file to understand the configuration values that match your configuration requirements.

If you want to deploy SQL Server in StatefulSet mode, which is the recommended mode for SQL Server deployments, you can view a [sample "as-is" StatefulSet-based Helm chart](https://github.com/microsoft/mssql-docker/tree/master/linux/sample-helm-chart-statefulset-deployment) deployment instead.

1. Download the sample [Helm chart](https://github.com/microsoft/mssql-docker/tree/master/linux/sample-helm-chart).

1. Switch to the directory where you've downloaded the sample chart, and modify the `values.yaml` file if needed.

## Deploy SQL Server to the AKS cluster

Deploy SQL Server using the command below. The deployment name is customizable, so you can change `mssql-latest-deploy` to anything that you'd like.

```cmd
helm install mssql-latest-deploy . --set ACCEPT_EULA.value=Y --set MSSQL_PID.value=Developer
```

In the above example, the chart and its files are in the current directory, represented by the period (`.`). You can specify the path of the chart if you prefer.

If successful, you'll see similar output:

```output
NAME: mssql-latest-deploy
LAST DEPLOYED: Wed Apr 06 21:36:19 2022
NAMESPACE: default
STATUS: deployed
REVISION: 1
```

## Verify SQL Server deployment

Deploying to a Kubernetes cluster can take a few minutes. To verify that your deployment was successful, run the following command:

```cmd
kubectl get all
```

If successful, you'll see similar output:

```output
NAME                                      READY   STATUS    RESTARTS   AGE
pod/mssql-latest-deploy-7f8c7f5bc-9grmg   1/1     Running   0          2m56s

NAME                          TYPE           CLUSTER-IP     EXTERNAL-IP   PORT(S)          AGE
service/kubernetes            ClusterIP      10.0.0.1       <none>        443/TCP          141d
service/mssql-latest-deploy   LoadBalancer   10.0.247.220   20.40.0.145   1433:30780/TCP   2m56s

NAME                                  READY   UP-TO-DATE   AVAILABLE   AGE
deployment.apps/mssql-latest-deploy   1/1     1            1           2m56s

NAME                                            DESIRED   CURRENT   READY   AGE
replicaset.apps/mssql-latest-deploy-7f8c7f5bc   1         1         1       2m56s
```

## Connect to the SQL Server running on AKS

You can connect to the SQL Server instance using any familiar SQL Server client tool, such as SQL Server Management Studio (SSMS), Azure Data Studio, or **sqlcmd**.

For example, if you connect to the SQL Server instance using SSMS, you can use the following settings:

- **Server name:** Use the `External-IP` address for the `mssql-latest-deploy` service. In this example, it's `20.40.0.145`.
- **Authentication:** Select **SQL Server Authentication** from the dropdown.
- **Login:** Use **sa**, which is the system administrator account.
- **Password:** The **sa** password matches the value you provided in the `sa_password` configuration option, in the `values.yaml` file of the Helm chart.

Once you've connected, you'll be able to expand the SQL Server instance in **Object Explorer**.

:::image type="content" source="media/sql-server-linux-containers-deploy-helm-charts-kubernetes/expand-object-explorer.png" alt-text="Screenshot showing the Object Explorer connected to the database instance.":::

## Clean up resources

If you're not going to continue using your AKS cluster, remember to [delete your cluster](/azure/aks/kubernetes-walkthrough#delete-the-cluster).

## See also

- [Quickstart: Deploy SQL Server on Linux using an Ansible playbook](sql-server-linux-deploy-ansible.md)

## Next steps

- [Configure Active Directory authentication with SQL Server on Linux-based containers using adutil](sql-server-linux-containers-ad-auth-adutil-tutorial.md)
- [Secure SQL Server Docker containers](sql-server-linux-docker-container-security.md)
- [Troubleshooting SQL Server Docker containers](sql-server-linux-docker-container-troubleshooting.md)
