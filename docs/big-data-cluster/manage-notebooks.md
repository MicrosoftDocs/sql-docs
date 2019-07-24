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

[!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)] provides an extension for Azure Data Studio that includes deployment notebooks. A deployment notebook includes documentation and code that you can use in Azure Data Studio to manage big data clusters for SQL Server.

Originally implemented as an open source project, [notebooks](notebooks-guidance.md) have been implemented into [Azure Data Studio](http://docs.microsoft.com/sql/azure-data-studio/download). You can use markdown for text in the text cells and one of the available kernels to write code in the code cells.

You can use notebooks to deploy big data clusters for [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)].

## Prerequisites

Following prerequisites are required to be able to launch the notebook:

* Latest version of [Azure Data Studio Insiders build](https://github.com/microsoft/azuredatastudio#try-out-the-latest-insiders-build-from-master)
* [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)] extension installed in Azure Data Studio

In addition to above, deploying SQL Server 2019 big data cluster also requires:

* [azdata](deploy-install-azdata.md)
* [kubectl](https://kubernetes.io/docs/tasks/tools/install-kubectl/#install-kubectl-binary-using-native-package-management)
* [Azure CLI](/cli/azure/install-azure-cli)
