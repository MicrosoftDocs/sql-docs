---
title: Deploy SQL Server big data cluster with Azure Data Studio notebooks
titleSuffix: Deploy SQL Server big data cluster cluster with Azure Data Studio notebooks
description: Use a notebook from Azure Data Studio to deploy a big data cluster.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: mihaelab
ms.date: 07/24/2019
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

* Latest version of [Azure Data Studio Insiders build](https://github.com/microsoft/azuredatastudio#try-out-the-latest-insiders-build-from-master) installed
* [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)] extension installed in Azure Data Studio

In addition to above, deploying SQL Server 2019 big data cluster also requires:

* [azdata](deploy-install-azdata.md)
* [kubectl](https://kubernetes.io/docs/tasks/tools/install-kubectl/#install-kubectl-binary-using-native-package-management)
* [Azure CLI](/cli/azure/install-azure-cli)

## Launch the notebook

1. Launch the Azure Data Studio Insiders.

1. On the **Connections** tab, click **...** and select **Deploy SQL Server big data cluster...**.

   ![AI and ML](media/deploy-notebooks/deploy-notebooks-1.png)

1. From the **Deployment Target**, under **Options**, select either **New Azure Kubernetes Cluster** or **Existing Azure Kubernetes Service cluster**.

1. Click **Select** button.

1. This action launches a dialog to collect the user input, provide the required information and review the default values.

1. Click **Open Notebook** button.
This action launches the appropriate notebook. To complete the deployment, follow the instructions in the notebook to deploy a big data cluster for [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)] on an existing or new Azure Kubernetes Service cluster.

## Next steps

For more information about deployment, see [deployment guidance for SQL Server big data clusters](deployment-guidance.md).
