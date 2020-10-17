---
title: Install Azure Data CLI with Windows Installer
titleSuffix:
description: Learn how to install the Azure Data CLI tool with the installer. 
author: MikeRayMSFT 
ms.author: mikeray
ms.reviewer: mihaelab
ms.date: 09/30/2020
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Install Azure Data CLI with Windows Installer

[!INCLUDE [azdata](../../includes/applies-to-version/azdata.md)]

This article describes how to install Azure Data CLI on Windows with an installer. Use Azure Data CLI to manage SQL Server Big Data Clusters or Azure Arc enabled data services.

## Steps to Install Azure Data CLI with the Microsoft Windows Installer

To install Azure Data CLI on with the Microsoft Windows Installer,

1. Remove Azure Data CLI if it was installed using `pip`. If Azure Data CLI was installed using Windows Installer, proceed to the next step.
1. Install Azure Data CLI using the [Windows Installer](https://aka.ms/azdata-msi).

### Uninstall Azure Data CLI with Windows Installer

To uninstall Azure Data CLI with Windows Installer, follow the instructions for the appropriate operating system.

| Platform      | Instructions                                           |
| ------------- |--------------------------------------------------------|
| Windows 10| Start > Settings > Apps                                |
| Windows 8     | Start > Control Panel > Programs > Uninstall a program |

The program to uninstall is called `Azdata CLI` . Select this application, then click the `Uninstall` button.

## Next steps

For more information about big data clusters, see [What are [!INCLUDE[big-data-clusters-2019](../../includes/ssbigdataclusters-ver15.md)]?](../../big-data-cluster/big-data-cluster-overview.md)

Use Azure Data CLI with [Azure Arc enabled data services](/azure/azure-arc/data/)
