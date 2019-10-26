---
title: Deploy SQL Server big data cluster with Azure Data Studio notebooks
titleSuffix: Deploy SQL Server big data cluster cluster with Azure Data Studio notebooks
description: Use a notebook from Azure Data Studio to deploy a big data cluster.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: mihaelab
ms.date: 11/04/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Deploy SQL Server big data cluster with Azure Data Studio notebooks

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

[!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)] provides an extension for Azure Data Studio that includes deployment notebooks. A deployment notebook includes documentation and code that you can use in Azure Data Studio to create a SQL Server big data cluster.

Originally implemented as an open source project, [notebooks](notebooks-guidance.md) have been implemented into [Azure Data Studio](https://docs.microsoft.com/sql/azure-data-studio/download). You can use markdown for text in the text cells and one of the available kernels to write code in the code cells.

You can use notebooks to deploy big data clusters for [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)].

## Prerequisites

Following prerequisites are required to be able to launch the notebook:

- Latest version of Azure Data Studio installed
- SQL Server 2019 (15.x) extension installed in Azure Data Studio

In addition to above, deploying SQL Server 2019 big data cluster also requires:

- `azdata`
- `kubectl`
- Azure CLI

## Launch the notebook

1. Launch Azure Data Studio.
2. On the **Connections** tab, click **...** and select **Deploy SQL Server**

   :::image type="content" source="media/deploy-notebooks/deploy-notebooks1.png" alt-text="deploy-notebooks1":::

3. From the deployment options, choose **SQL Server Big Data Cluster**.
4. From the **Deployment Target**, under **Options**, select either **New Azure Kubernetes Cluster** or **Existing Azure Kubernetes Service cluster**.
5. Accept the privacy and license terms.
   This dialog also checks whether the required tools for the chosen type of SQL deployment on the host. The **Select** button is not enabled until the tools check is successful.  
7. Click **Select** button. This will launch the deployment experience.

## Set deployment configuration template

:::image type="content" source="media/deploy-notebooks/deploy-notebooks2.png" alt-text="deploy-notebooks2.png":::

Select the target configuration template from the available templates. The settings of the deployment profile can be customized in subsequent steps.

> [!NOTE]
> The available profiles are filtered depending on the type of deployment target chosen in the previous dialog.

## Set Azure settings

If the deployment target is a new Azure Kubernetes Service (AKS), additional information such as Azure subscription ID, resource group, AKS cluster name, VM count, size etc. will be required to create the AKS cluster.

:::image type="content" source="media/deploy-notebooks/deploy-notebooks3.png" alt-text="deploy-notebooks3.png":::

If the deployment target is an existing Kubernetes cluster, the wizard prompts for the path to the kube-config file to import the Kubernetes cluster settings. Ensure the appropriate cluster context is selected where the SQL Server 2019 Big Data Cluster will be deployed.

:::image type="content" source="media/deploy-notebooks/deploy-notebooks4.png" alt-text="deploy-notebooks4.png":::

## Set cluster, Docker and AD settings

1. Enter the cluster name for the SQL Server 2019 big data cluster (BDC), administrator username, and password.

   > [!NOTE]
   > The same account will be used for controller and SQL Server.

   :::image type="content" source="media/deploy-notebooks/deploy-notebooks5.png" alt-text="deploy-notebooks5.png":::

1. Enter the Docker settings as appropriate

   :::image type="content" source="media/deploy-notebooks/deploy-notebooks6.png" alt-text="deploy-notebooks6.png":::

1. If AD authentication is available, enter the AD settings

   :::image type="content" source="media/deploy-notebooks/deploy-notebooks7.png" alt-text="deploy-notebooks7.png":::

## Set service settings

This screen has inputs for various settings such as scale, endpoints, storage, and other advanced storage settings. Enter the appropriate values and select **Next**.

### Scale settings

- Enter the number of instances of each of the components in the big data cluster.
- Spark Instance can be included along with HDFS in the storage pool or on its own in the Spark pool.
- For additional information on each of these components refer Master instance, Data Pool, Storage pool, Compute pool.

:::image type="content" source="media/deploy-notebooks/deploy-notebooks8.png" alt-text="deploy-notebooks8.png":::

### Endpoint settings

The default endpoints have been pre-filled. However, they can be changed as appropriate. More on endpoints.

:::image type="content" source="media/deploy-notebooks/deploy-notebooks9.png" alt-text="deploy-notebooks9.png":::

### Storage settings

:::image type="content" source="media/deploy-notebooks/deploy-notebooks10.png" alt-text="deploy-notebooks10.png":::

The storage settings include storage class and claim size for data and logs. The settings can be applied across storage, data, and SQL Server master pool.

### Advanced Storage settings

:::image type="content" source="media/deploy-notebooks/deploy-notebooks11.png" alt-text="deploy-notebooks11.png":::

## Review and save or deploy

Review all the input that was provided to deploy BDC. You can download the config files via **Save config files**. Additionally, you can script the entire deployment to a Notebook and to deploy  BDC with the settings at a later time or even share with another user. Select **Deploy** to initiate a silent deployment of BDC.

:::image type="content" source="media/deploy-notebooks/deploy-notebooks12.png" alt-text="deploy-notebooks12.png":::

## Next steps

For more information about deployment, see [deployment guidance for SQL Server big data clusters](deployment-guidance.md).
