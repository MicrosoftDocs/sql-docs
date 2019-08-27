---
title: Install azdata with Windows Installer
titleSuffix: SQL Server big data clusters
description: Learn how to install the azdata tool for installing and managing [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)] (preview) with the installer. 
author: MikeRayMSFT 
ms.author: mikeray
ms.reviewer: mihaelab
ms.date: 08/26/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Install `azdata` to manage [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] with Windows Installer

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

This article describes how to install `azdata` for SQL Server 2019 Big Data Clusters release candidate on Windows. Before the Windows Installation was available, the installation of `azdata` required `pip`.

>For Linux (Ubuntu), [install `azdata` with `apt`](./deploy-install-azdata-linux-package.md).

At this time, there are no package managers to install `azdata` on other operating systems or distributions. For these platforms, see [Install `azdata` without package manager](./deploy-install-azdata.md).

## Install `azdata` with the Microsoft Windows Installer

To install `azdata` on with the Microsoft Windows Installer,

1. Remove `azdata` if it was installed using `pip`. If `azdata` was installed using Windows Installer, proceed to the next step.
1. Install `azdata` using the Windows Installer.

### Uninstall if previous installation done with `pip`

If you have an existing version of `azdata` installed using `pip`, first remove it:

```powershell
pip3 uninstall -y -r http://helsinki.redmond.corp.microsoft.com/browse/packages/python/aris-p-release-candidate-gb/azdata/requirements.txt
```

Once removed, [install `azdata` on Windows](#windows).

>[!NOTE]
>If your previous installation was done using the MSI, you will not need to uninstall any current versions before using the MSI installer.

### <a id="install-azdata-windows"></a>Install with Windows Installer

Use the Windows Installer to install or update `azdata` on Windows.

[Download the `azdata` Windows Installer]()

When the installer asks if it can make changes to your computer, click `Yes`.

### Uninstall `azdata` with Windows Installer

To uninstall `azdata` with Windows Installer, follow the instructions for the appropriate operating system.

| Platform      | Instructions                                           |
| ------------- |--------------------------------------------------------|
| Windows 10	| Start > Settings > Apps                                |
| Windows 8     | Start > Control Panel > Programs > Uninstall a program |

The program to uninstall is called **`Azdata CLI`** . Select this application, then click the `Uninstall` button.

## Next steps

For more information about big data clusters, see [What are [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]?](big-data-cluster-overview.md).
