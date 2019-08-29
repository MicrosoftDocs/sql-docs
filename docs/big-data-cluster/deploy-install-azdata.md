---
title: Install azdata using pip
titleSuffix: SQL Server big data clusters
description: Learn how to install the azdata tool for installing and managing [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)] (preview) with pip.
author: MikeRayMSFT 
ms.author: mikeray
ms.reviewer: mihaelab
ms.date: 08/28/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Install `azdata` for [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] using `pip`

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

This article describes how to install the `azdata` tool for the release candidate on Windows or Linux using `pip`.

For Windows and Linux (Ubuntu distribution), you can install with a [package manager](./deploy-install-azdata-installer.md) for a simpler experience.

## <a id="prerequisites"></a> Prerequisites

`azdata` is a command-line utility written in Python that enables cluster administrators to bootstrap and manage the big data cluster via REST APIs. The minimum Python version required is v3.5. You must also have `pip` that is used to download and install `azdata` tool. The instructions below provide examples for Windows and Ubuntu. For installing Python on other platforms, see the [Python documentation](https://wiki.python.org/moin/BeginnersGuide/Download).
In addition, you must also install and update the latest version of *requests* Python package:
```bash
pip3 install -U requests
```

> [!IMPORTANT]
> If you are installing a newer version of big data clusters, you must backup your data and delete the old cluster *before* upgrading `azdata` and installing the new release. For more information, see [Upgrading to a new release](deployment-upgrade.md).

## <a id="windows"></a> Windows `azdata` installation

1. On a Windows client, download the necessary Python package from [https://www.python.org/downloads/](https://www.python.org/downloads/). For python3.5.3 and later, pip3 is also installed when you install Python. 

   > [!TIP] 
   > When installing Python3, select to add Python to your **PATH**. If you do not, you can later find where pip3 is located and manually add it to your **PATH**.

1. Open a new Windows PowerShell session so that it gets the latest path with Python in it.

1. If you have any previous releases of the tool installed (previously named **mssqlctl**), it is important to uninstall it first before installing the latest version of `azdata`.

   ```powershell
   pip3 uninstall -r https://aka.ms/azdata
   ```

1. If you have any previous releases of `azdata` installed, it is important to uninstall it first before installing the latest version.

   For CTP 3.2 or higher, run the following command.

   ```powershell
   pip3 uninstall -r https://aka.ms/azdata
   ```

1. Install `azdata` with the following command:

   ```powershell
   pip3 install -r https://aka.ms/azdata
   ```

## <a id="linux"></a> Linux `azdata` installation

On Linux, you must install Python 3.5 and then upgrade pip. The following example shows the commands that would work for Ubuntu. For other Linux platforms, see the [Python documentation](https://wiki.python.org/moin/BeginnersGuide/Download).

1. Install the necessary Python packages:

   ```bash
   sudo apt-get update && \
   sudo apt-get install -y python3 && \
   sudo apt-get install -y python3-pip
   ```

1. Upgrade pip3:

   ```bash
   sudo -H pip3 install --upgrade pip
   ```

1. If you have any previous releases of the tool installed (previously named **mssqlctl**), it is important to uninstall it first before installing the latest version of `azdata`.

   For CTP 3.1 or lower, run the following command. Replace `ctp3.1` in the command with the version of **mssqlctl** that you are uninstalling. 

   ```powershell
   pip3 uninstall -r https://aka.ms/azdata
   ```

1. If you have any previous releases of `azdata` installed, it is important to uninstall it first before installing the latest version.

   For CTP 3.2 or higher, run the following command.

   ```powershell
   pip3 uninstall -r https://aka.ms/azdata
   ```

1. Install `azdata` with the following command:

   ```bash
   pip3 install -r https://aka.ms/azdata --user
   ```

   > [!NOTE]
   > The `--user` switch installs `azdata` to the Python user install directory. This is typically `~/.local/bin` on Linux. Either add this directory to your path or navigate to the user install directory and run `./azdata` from there.

## Next steps

For more information about big data clusters, see [What are [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]?](big-data-cluster-overview.md).
