---
title: Install Azure Data CLI (azdata) using pip
description: Learn how to install the azdata tool with pip.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: danibunny
ms.date: 07/29/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
ms.custom: intro-installation
---

# Install [!INCLUDE [azure-data-cli-azdata](../../includes/azure-data-cli-azdata.md)] with `pip`

[!INCLUDE[azdata](../../includes/applies-to-version/azdata.md)]

This article describes how to install the [!INCLUDE [azure-data-cli-azdata](../../includes/azure-data-cli-azdata.md)] tool on Windows, Linux or macOS/OS X using `pip`.

> [!TIP]
> For a simpler experience, `azdata` can be installed with a [package manager](./deploy-install-azdata.md) for Windows, Linux (Ubuntu, Debian, RHEL, CentOS, openSUSE and SLE distributions) and macOS.

## <a id="prerequisites"></a> Prerequisites

`azdata` is a command-line utility written in Python that enables cluster administrators to bootstrap and manage data resources via REST APIs. The minimum Python version required is v3.6. `pip` is required to download and install the `azdata` tool. The instructions below provide examples for Windows, Linux (Ubuntu) and macOS/OS X. For installing Python on other platforms, see the [Python documentation](https://wiki.python.org/moin/BeginnersGuide/Download). In addition, install and update the latest version of `requests` Python package:

```bash
pip3 install -U requests
```

## <a id="windows"></a> Windows `azdata` installation

1. On a Windows client, download the necessary Python package from [https://www.python.org/downloads/](https://www.python.org/downloads/). For python 3.6 and later, pip3 is also installed when you install Python.

   > [!TIP]
   > When installing Python3, select to add Python to your `PATH`. If you do not, you can later find where pip3 is located and manually add it to your `PATH`.

1. Open a new Windows PowerShell session so that it gets the latest path with Python in it.

1. Starting with SQL Server 2019 CU5 release, azdata has an independent semantic version from the server. If you have any previous releases of `azdata` installed prior to this, it is important to first uninstall them before installing the latest version.

   For example, for 2019-cu4, run the following command:

   ```powershell
   pip3 uninstall -r https://azdatacli.blob.core.windows.net/python/azdata/2019-cu4/requirements.txt
   ```

  > [!NOTE]
  > In the preceding examples, replace `2019-cu6` with the version and CU of your installation of `azdata`. 

1. Install `azdata`.

   ```powershell
   pip3 install -r https://aka.ms/azdata
   ```

## <a id="linux"></a> Linux `azdata` installation

On Linux, you must install Python 3.6 and then upgrade pip. The following example shows the commands that would work for Ubuntu. For other Linux platforms, see the [Python documentation](https://wiki.python.org/moin/BeginnersGuide/Download).

1. Install the necessary Python packages:

   ```bash
   sudo apt-get update && \
   sudo apt-get install -y python3 && \
   sudo apt-get install -y python3-pip && \
   sudo apt-get install -y libkrb5-dev && \
   sudo apt-get install -y libsqlite3-dev && \
   sudo apt-get install -y unixodbc-dev
   ```

1. Upgrade pip3.

   ```bash
   sudo -H pip3 install --upgrade pip
   ```

1. Starting with SQL Server 2019 CU5 release, azdata has an independent semantic version from the server. If you have any previous releases of `azdata` installed prior to this, it is important to first uninstall them before installing the latest version.

   For example, for `2019-cu6`, run the following command:

   ```bash
   pip3 uninstall -r https://azdatacli.blob.core.windows.net/python/azdata/2019-cu6/requirements.txt
   ```

  > [!NOTE]
  > In the preceding examples, replace `2019-cu6` with the version and CU of your installation of `azdata`.

1. Install `azdata`.

   ```bash
   pip3 install -r https://aka.ms/azdata --user
   ```

   > [!NOTE]
   > The `--user` switch installs `azdata` to the Python user install directory. This is typically `~/.local/bin` on Linux. Either add this directory to your path or navigate to the user install directory and run `./azdata` from there.

## <a id="macOSX"></a> Install `azdata` on macOS or OS X

To install `azdata` on macOS or OS X complete these steps. For each step, run the example in Terminal.

1. On a macOS client, install [Homebrew](https://brew.sh) if you don't have it already:

   ```bash
   /usr/bin/ruby -e "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/master/install)"
   ```

1. Install Python and pip, minimum version 3.0:

   ```bash
   brew install python3
   ```

1. Install dependencies:

   ```bash
   pip3 install -U requests
   brew install freetds
   ```

1. Starting with SQL Server 2019 CU5 release, azdata has an independent semantic version from the server. If you have any previous releases of `azdata` installed prior to this, it is important to first uninstall them before installing the latest version. For example, the following command removes the RC1 version of `azdata`:

   ```bash
   pip3 uninstall -r https://azdatacli.blob.core.windows.net/python/azdata/2019-rc1/requirements.txt
   ```

1. Install Azure Data CLI.

   ```bash
   pip3 install -r https://aka.ms/azdata
   ```

## Next steps

For more information about big data clusters, see [What are [!INCLUDE[big-data-clusters-2019](../../includes/ssbigdataclusters-ver15.md)]?](../../big-data-cluster/big-data-cluster-overview.md).
