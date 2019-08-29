---
title: Manage SQL Server big data cluster with Azure Data Studio notebooks
titleSuffix: Manage SQL Server big data cluster cluster with Azure Data Studio notebooks
description: Use a notebook from Azure Data Studio to manage and troubleshoot a big data cluster.
author: yualan
ms.author: alanyu
ms.reviewer: mikeray
ms.date: 07/24/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Manage big data clusters for SQL Server with Azure Data Studio notebooks

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

[!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)] provides an extension for Azure Data Studio that includes notebooks. A notebook includes documentation and code that you can use in Azure Data Studio to manage big data clusters for SQL Server.

Originally implemented as an open source project, [notebooks](notebooks-guidance.md) have been implemented into [Azure Data Studio](https://docs.microsoft.com/sql/azure-data-studio/download). You can use markdown for text in the text cells and one of the available kernels to write code in the code cells.

You can use notebooks to deploy big data clusters for [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)].

In addition to notebooks, users can view a collection of notebooks, which are called Jupyter Books. A Jupyter Book provides a table of contents to navigate a collection of notebooks so a user can easily find the notebook they need, whether to troubleshoot SQL Server or view cluster status.

## Prerequisites

The following prerequisites are required to be able to launch the notebook:

* Latest version of [Azure Data Studio Insiders build](https://github.com/microsoft/azuredatastudio#try-out-the-latest-insiders-build-from-master)
* [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)] extension installed in Azure Data Studio

In addition to above, deploying SQL Server 2019 big data cluster also requires:

* [azdata](deploy-install-azdata.md)
* [kubectl](https://kubernetes.io/docs/tasks/tools/install-kubectl/#install-kubectl-binary-using-native-package-management)
* [Azure CLI](/cli/azure/install-azure-cli)

## Accessing troubleshooting notebooks

1. After installing Azure Data Studio Insiders, connect to a SQL Server big data cluster instance.
2. After successfully connecting, right-click on your server name in the Connections viewlet and click **Manage.**
3. On the dashboard, click **SQL Server Big Data Cluster.** Click **SQL Server 2019 guide** to open the Jupyter Book with the notebooks you need.
    ![button](media/manage-notebooks/jupyter-book-button.png)

1. On the folder picker window, choose a location to save your Jupyter Book.
2. Click **Reload** to reload Azure Data Studio to see your Jupyter Book. Click **Open new instance** to open a new instance of Azure Data Studio with the Jupyter Book.
3. In the Explorer view, you should see a section called **Books.** If it is not expanded, click on it to view the notebooks.
4. Click on the notebook for the task you need to complete.

## Next Steps
For more information about notebooks in Azure Data Studio, see [How to use notebooks in SQL Server 2019 preview](notebooks-guidance.md).
