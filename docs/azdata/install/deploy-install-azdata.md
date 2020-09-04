---
title: Install azdata
titleSuffix: 
description: Learn how to install the azdata tool.
author: MikeRayMSFT 
ms.author: mikeray
ms.reviewer: mihaelab
ms.date: 01/07/2020
ms.topic: conceptual
ms.prod: sql
---

# Install `azdata`

[!INCLUDE[SQL Server 2019](../../includes/applies-to-version/sqlserver2019.md)]

`azdata` is a command-line utility written in Python to bootstrap and manage the data services via REST APIs. 

## Find latest version

The list of files for the latest version is always available at [https://aka.ms/azdata](https://aka.ms/azdata).

To find your installed version and see if you need to update, run `azdata --version`.

## OS specific instructions

* [Install on Windows](../install/deploy-install-azdata-installer.md)
* [Install on macOS](../install/deploy-install-azdata-macos.md)
* Install on Linux or [Windows Subsystem for Linux (WSL)](/windows/wsl/about/)
   * [Install with apt on Debian or Ubuntu](../install/deploy-install-azdata-linux-package.md)
   * [Install with yum on RHEL, or CentOS](../install/deploy-install-azdata-yum.md)
   * [Install with Zypper on openSUSE or SLE](../install/deploy-install-azdata-zypper.md)
   * [Install from script](../install/deploy-install-azdata-pip.md)

[!INCLUDE [azdata-package-installation-remove-pip-install](../../includes/azdata-package-installation-remove-pip-install.md)]

## Next steps

Use azdata with Big Data Clusters, see [What are [!INCLUDE[big-data-clusters-2019](../../includes/ssbigdataclusters-ver15.md)]?](../../big-data-cluster/big-data-cluster-overview.md).

Use azdata with [Azure Arc enabled data services](/azure/azure-arc/data/)
