---
title: Install Azure Data CLI (azdata) with Windows Installer
titleSuffix:
description: Learn how to install the Azure Data CLI (azdata) tool with the installer. 
author: MikeRayMSFT 
ms.author: mikeray
ms.reviewer: mihaelab
ms.date: 09/30/2020
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Install [!INCLUDE [azure-data-cli-azdata](../../includes/azure-data-cli-azdata.md)] with Windows Installer

[!INCLUDE [azdata](../../includes/applies-to-version/azdata.md)]

This article describes how to install `azdata` on Windows with an installer. Use `azdata` to manage SQL Server Big Data Clusters or Azure Arc enabled data services.

## Steps to Install `azdata` with the Microsoft Windows Installer

To install `azdata` on with the Microsoft Windows Installer,

1. Remove `azdata` if it was installed using `pip`. If `azdata` was installed using Windows Installer, proceed to the next step.
1. Install `azdata` using the [Windows Installer](https://aka.ms/azdata-msi).

### Uninstall `azdata` with Windows Installer

To uninstall `azdata` with Windows Installer, follow the instructions for the appropriate operating system.

| Platform      | Instructions                                           |
| ------------- |--------------------------------------------------------|
| Windows 10| Start > Settings > Apps                                |
| Windows 8     | Start > Control Panel > Programs > Uninstall a program |

The program to uninstall is called `Azure Data CLI` . Select this application, then click the `Uninstall` button.

## Next steps

For more information about big data clusters, see [What are [!INCLUDE[big-data-clusters-2019](../../includes/ssbigdataclusters-ver15.md)]?](../../big-data-cluster/big-data-cluster-overview.md)

Use `azdata` with [Azure Arc enabled data services](/azure/azure-arc/data/)
