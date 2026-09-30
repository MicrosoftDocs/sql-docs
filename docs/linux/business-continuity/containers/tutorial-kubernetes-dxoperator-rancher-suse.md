---
title: Deploy Availability Groups on AKS with DH2i DxOperator and Rancher by SUSE
description: Set up an availability group in SQL Server on Kubernetes using DH2i DxOperator and Rancher by SUSE.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: amitkh, atsingh
ms.date: 01/02/2026
ms.service: sql
ms.subservice: linux
ms.topic: tutorial
ms.custom:
  - intro-deployment
  - linux-related-content
---
# Deploy SQL Server containers and availability group with DH2i DxOperator on Azure Kubernetes Service via Rancher

[!INCLUDE [SQL Server - Linux](../../../includes/applies-to-version/sql-linux.md)]

This tutorial provides instructions on setting up [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] configured with Always On availability groups (AGs) in an Azure Kubernetes Service (AKS) cluster. It utilizes the DH2i DxOperator and Rancher Prime from SUSE for deployment.

> [!NOTE]  
> Starting in [!INCLUDE [sssql25-md](../../../includes/sssql25-md.md)], SUSE Linux Enterprise Server (SLES) isn't supported.

Microsoft supports data movement, AG, and [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] components. DH2i is responsible for support of the DxEnterprise product, which includes cluster and quorum management.

> [!NOTE]  
> DxOperator is a software extension to Kubernetes that uses custom resource definitions to automate the deployment of DxEnterprise clusters. DxEnterprise then provides all of the instrumentation to create, configure, manage, and provide automatic failover for [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] AG workloads in Kubernetes. You can register for a [free DxEnterprise software license](https://dh2i.com/dxoperator-sql-server-operator-for-kubernetes). For more information, see the [DxOperator Quick Start Guide](https://support.dh2i.com/dxoperator/guides/dxoperator-qsg). For support issues, contact SUSE directly.

This tutorial consists of the following steps:

> [!div class="checklist"]
> - Configure Rancher Prime on AKS
> - Install DxOperator
> - Deploy SQL Server containers and configure the Always On availability groups using the DH2i DxOperator
> - Connect and manage [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] containers using SQL Server Management Studio (SSMS).

## Prerequisites

- An [Azure Kubernetes Service](/azure/aks/tutorial-kubernetes-deploy-cluster) (AKS) or Kubernetes cluster.

- A valid DxEnterprise license with AG features and tunnels enabled. For more information, see the [developer edition](https://dh2i.com/trial/) for nonproduction usage, or [DxEnterprise software](https://dh2i.com/DxEnterprise-high-availability/) for production workloads.

## Configure Rancher Prime on Azure Kubernetes Service

Follow the instructions provided in the [Quickstart: Deploy an Azure Kubernetes Service (AKS) cluster using Azure CLI](/azure/aks/learn/quick-kubernetes-deploy-cli) article.

After you install your Kubernetes cluster, follow the instructions in [Installing Rancher on Azure Kubernetes Service](https://ranchermanager.docs.rancher.com/getting-started/installation-and-upgrade/install-upgrade-on-a-kubernetes-cluster/rancher-on-aks) from Step 4, where you access the credentials of the Kubernetes cluster and install Rancher.

## Install DxOperator

1. Sign in to the Rancher portal via the web UI.

   :::image type="content" source="media/tutorial-kubernetes-dxoperator-rancher-suse/rancher-prime.png" alt-text="Screenshot of Rancher web UI." lightbox="media/tutorial-kubernetes-dxoperator-rancher-suse/rancher-prime.png":::

1. To proceed, select the Kubernetes cluster where you want to install DxOperator. In this example, choose a local cluster that also has Rancher deployed. However, you should import or add another Kubernetes cluster and use that cluster for deploying SQL Server, to avoid Rancher and SQL Server pods running on same Kubernetes cluster.

1. From the left hand menu, Select **App** > **Charts**.

   :::image type="content" source="media/tutorial-kubernetes-dxoperator-rancher-suse/rancher-charts.png" alt-text="Screenshot showing the available chart options." lightbox="media/tutorial-kubernetes-dxoperator-rancher-suse/rancher-charts.png":::

1. Search for DxOperator and select **Install**.
   1. Select **Next** and then **Install**.
   1. Wait for the installation to complete before proceeding.

## Install SQL Server AGs using DxEnterprise DxOperator

1. Add secrets for SQL Server and the DxEnterprise cluster passkeys.

   1. Navigate to **Storage** > **Secrets** > **Create**.
   1. Choose **Opaque** as your secret option, and enter the secret name `dxe`.
   1. Provide the key name as `DX_PASSKEY`, and value of the DxEnterprise cluster passkey.
   1. Select **Add**.
   1. Add the key name as `DX_LICENSE`, with value of the DxEnterprise license key.
   1. Select **Create** to finish creating the secret.

      :::image type="content" source="media/tutorial-kubernetes-dxoperator-rancher-suse/rancher-secrets.png" alt-text="Screenshot of the license and passkey secrets." lightbox="media/tutorial-kubernetes-dxoperator-rancher-suse/rancher-secrets.png":::

   1. Select **Create** to create a new secret, and then **Opaque**.
   1. Enter name of secret as `mssql`, set the key name as `MSSQL_SA_PASSWORD` with a value of a strong `sa` password.
   1. Select **Create** to finish creating the secret.

      :::image type="content" source="media/tutorial-kubernetes-dxoperator-rancher-suse/rancher-password.png" alt-text="Screenshot of the password secret." lightbox="media/tutorial-kubernetes-dxoperator-rancher-suse/rancher-password.png":::

1. Select **Apps** > **Charts**.

1. From the list of available charts, search for **DxOperator - DxE + SQL Server AG**.

   :::image type="content" source="media/tutorial-kubernetes-dxoperator-rancher-suse/rancher-search-chart.png" alt-text="Screenshot of the search option." lightbox="media/tutorial-kubernetes-dxoperator-rancher-suse/rancher-search-chart.png":::

1. Install the chart.

   1. Select **Install**.
   1. Once you select **Install**, provide the AG details as shown.

      :::image type="content" source="media/tutorial-kubernetes-dxoperator-rancher-suse/rancher-availability-group.png" alt-text="Screenshot of the availability group settings." lightbox="media/tutorial-kubernetes-dxoperator-rancher-suse/rancher-availability-group.png":::

   1. Select **Next**.
   1. Check the **Create Load Balancers** option to allow external access.
   1. On the DxEnterprise options, check the **Accept EULA** option.
   1. Select `dxe` as cluster secret.

      :::image type="content" source="media/tutorial-kubernetes-dxoperator-rancher-suse/rancher-cluster-secret.png" alt-text="Screenshot of cluster secret." lightbox="media/tutorial-kubernetes-dxoperator-rancher-suse/rancher-cluster-secret.png":::

   1. On the SQL Server options, check **Accept EULA**.
   1. Select `mssql` as SQL Server secret.
   1. Select **Next** and **Install**.

   :::image type="content" source="media/tutorial-kubernetes-dxoperator-rancher-suse/rancher-cluster-options.png" alt-text="Screenshot of the cluster options." lightbox="media/tutorial-kubernetes-dxoperator-rancher-suse/rancher-cluster-options.png":::

## Connect to SQL Server

Once the installation is complete from the previous steps, get the external IP address to connect using [!INCLUDE [ssmanstudiofull-md](../../../includes/ssmanstudiofull-md.md)] (SSMS).

1. Navigate to **Services**, select `DxEnterprisesqlag-0-lb`, and note the external IP (LB) address.

1. Use the external IP address in SSMS to connect to the AG.

   :::image type="content" source="media/tutorial-kubernetes-dxoperator-rancher-suse/rancher-external.png" alt-text="Screenshot of external IP address." lightbox="media/tutorial-kubernetes-dxoperator-rancher-suse/rancher-external.png":::

1. In SSMS Object Explorer, expand **Always On High Availability** > **Availability Groups**.

1. Select `AG1` > **Show dashboard**.

   :::image type="content" source="media/tutorial-kubernetes-dxoperator-rancher-suse/validation.png" alt-text="Screenshot of SSMS validation." lightbox="media/tutorial-kubernetes-dxoperator-rancher-suse/validation.png":::

Simulate the failover to verify.

1. From the Rancher web UI, select **Workloads** > **Pods**
1. Select the `DxEnterprisesqlag-0` pod
1. Select **Delete**, then select **Delete** again when prompted

Fetch the external IP address of the surviving pod.

1. Select **Services** on Rancher web UI
1. Select `DxEnterprisesqlag-1-lb`
1. Fetch the external/load balancer IP address
1. Connect to SSMS and view the AG dashboard
1. Observe that the role changed to Primary for `DxEnterprisesqlag-1`

   :::image type="content" source="media/tutorial-kubernetes-dxoperator-rancher-suse/post-failover.png" alt-text="Screenshot of post failover." lightbox="media/tutorial-kubernetes-dxoperator-rancher-suse/post-failover.png":::

## Related content

- [Deploy availability groups on Kubernetes with DH2i DxOperator on Azure Kubernetes Service](tutorial-kubernetes-dxoperator.md)
- [Deploy availability groups with DH2i DxEnterprise on Kubernetes](tutorial-kubernetes-dh2i.md)
- [Quickstart: Deploy a SQL Server container cluster on Azure or Red Hat OpenShift](../../install-upgrade/quickstart-containers-azure.md)
- [Deploy SQL Server Linux containers on Kubernetes with StatefulSets](../../containers/kubernetes-best-practices-statefulsets.md)
