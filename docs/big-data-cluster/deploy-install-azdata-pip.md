---
title: Install azdata using pip
titleSuffix: SQL Server big data clusters
description: Learn how to install the azdata tool for installing and managing Big Data Clusters with pip.
author: MikeRayMSFT 
ms.author: mikeray
ms.reviewer: mihaelab
ms.date: 01/07/2020
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Install `azdata` with `pip`

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

This article describes how to install the `azdata` tool Windows or Linux using `pip`.

For Windows and Linux (Ubuntu distribution), you can install with a [package manager](./deploy-install-azdata-installer.md) for a simpler experience.

## <a id="prerequisites"></a> Prerequisites

`azdata` is a command-line utility written in Python that enables cluster administrators to bootstrap and manage the big data cluster via REST APIs. The minimum Python version required is v3.5. `pip` is required to download and install `azdata` tool. The instructions below provide examples for Windows and Ubuntu. For installing Python on other platforms, see the [Python documentation](https://wiki.python.org/moin/BeginnersGuide/Download).
In addition, install and update the latest version of `requests` Python package:

```bash
pip3 install -U requests
```

> [!IMPORTANT]
> If you are installing a newer version of big data clusters, back up your data and delete the old cluster upgrading `azdata` and installing the new release. For more information, see [Upgrading to a new release](deployment-upgrade.md).

## <a id="windows"></a> Windows `azdata` installation

1. On a Windows client, download the necessary Python package from [https://www.python.org/downloads/](https://www.python.org/downloads/). For python3.5.3 and later, pip3 is also installed when you install Python. 

   > [!TIP] 
   > When installing Python3, select to add Python to your `PATH`. If you do not, you can later find where pip3 is located and manually add it to your `PATH`.

1. Open a new Windows PowerShell session so that it gets the latest path with Python in it.

1. If you have any previous releases of `azdata` installed, it is important to uninstall it first before installing the latest version.

   For CTP 3.2 or RC1, run the following command.

   ```bash
   pip3 uninstall -r https://azdatacli.blob.core.windows.net/python/azdata/2019-ctp3.2/requirements.txt
   ```
   or
   ```bash
   pip3 uninstall -r https://azdatacli.blob.core.windows.net/python/azdata/2019-rc1/requirements.txt
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
   sudo apt-get install -y python3-pip && \
   sudo apt-get install -y libkrb5-dev && \
   sudo apt-get install -y libsqlite3-dev && \
   sudo apt-get install -y unixodbc-dev
   ```

1. Upgrade pip3:

   ```bash
   sudo -H pip3 install --upgrade pip
   ```

1. If you have any previous releases of `azdata` installed, it is important to uninstall it first before installing the latest version.

   For CTP 3.2 or RC1, run the following command.

   ```bash
   pip3 uninstall -r https://azdatacli.blob.core.windows.net/python/azdata/2019-ctp3.2/requirements.txt
   ```
   or
   ```bash
   pip3 uninstall -r https://azdatacli.blob.core.windows.net/python/azdata/2019-rc1/requirements.txt
   ```

1. Install `azdata` with the following command:

   ```bash
   pip3 install -r https://aka.ms/azdata --user
   ```

   > [!NOTE]
   > The `--user` switch installs `azdata` to the Python user install directory. This is typically `~/.local/bin` on Linux. Either add this directory to your path or navigate to the user install directory and run `./azdata` from there.

## <a id="macOSX"></a> Install `azdata` on macOS or OS X

To install `azdata` on macOS or OS X complete these steps. For each step, run the example in Terminal.

1. On a macOS client, install [Homebrew](https://brew.sh) if you don't have it already:

   ```
   /usr/bin/ruby -e "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/master/install)"
   ```

1. Install Python and pip, minimum version 3.0:

   ```
   brew install python3
   ```

1. Install dependencies:

   ```
   pip3 install -U requests
   brew install freetds
   ```

1. If you have any previous releases of the tool installed, it is important to uninstall it first before installing the latest version of `azdata`. The following command removes the version of `azdata`.

   ```
   pip3 uninstall -r https://azdatacli.blob.core.windows.net/python/azdata/2019-rc1/requirements.txt
   ```

1. Install `azdata` with the following command:

   ```
   pip3 install -r https://aka.ms/azdata
   ```

## Next steps

For more information about big data clusters, see [What are [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]?](big-data-cluster-overview.md).
