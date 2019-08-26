---
title: Install azdata with installer
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

# Install `azdata` to manage [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] with installer

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

This article describes how to install `azdata` for SQL Server 2019 Big Data Clusters release candidate on Windows or Linux with installer packages.

- For Windows, [install `azdata` CLI with the Microsoft Windows Installer](#windows)
- For Linux (Ubuntu or Debian), [install `azdata` CLI with `apt`](#azdata-ap)

<!--
## <a id="prerequisites"></a> Prerequisites

`azdata` is a command-line utility written in Python that enables cluster administrators to bootstrap and manage the big data cluster via REST APIs. The minimum Python version required is v3.5. You must also have `pip` that is used to download and install `azdata` tool. The instructions below provide examples for Windows and Ubuntu. For installing Python on other platforms, see the [Python documentation](https://wiki.python.org/moin/BeginnersGuide/Download).
In addition, you must also install and update the latest version of *requests* Python package:
```bash
pip3 install -U requests
```

> [!IMPORTANT]
> If you are installing a newer version of big data clusters, you must backup your data and delete the old cluster *before* upgrading `azdata` and installing the new release. For more information, see [Upgrading to a new release](deployment-upgrade.md).
-->
## <a id="windows"></a>Install `azdata` CLI with the Microsoft Windows Installer

To install `azdata` CLI on with the Microsoft Windows Installer,

1. Remove `azdata` CLI if it was installed using `pip`. If `azdata` was installed using Windows Installer, proceed to the next step.
1. Install `azdata` CLI using the Windows Installer.

## Uninstall if previous installation done with `pip`

If you have an existing version of `azdata` CLI installed using `pip` please first remove it:

```bash
$ pip3 uninstall -y -r http://helsinki.redmond.corp.microsoft.com/browse/packages/python/aris-p-release-candidate-gb/azdata/requirements.txt
```

Once removed, [install `azdata` CLI on Windows](#windows).

>[!NOTE]
>If your previous installation was done using the MSI, you will not need to uninstall any current versions before using the MSI installer.

## <a id="install-azdata-windows"></a>Install `azdata` with Windows Installer

Use the Windows Installer to install or update `azdata` CLI on Windows.

[Download the `azdata` Windows Installer]()

When the installer asks if it can make changes to your computer, click `Yes`.

## Uninstall `azdata` with Windows Installer

To uninstall `azdata` with Windows Installer, follow the instructions for the appropriate operating system.

| Platform      | Instructions                                           |
| ------------- |--------------------------------------------------------|
| Windows 10	| Start > Settings > Apps                                |
| Windows 8     | Start > Control Panel > Programs > Uninstall a program |

The program to uninstall is called **`Azdata CLI`** . Select this application, then click the `Uninstall` button.

## <a id="linux"></a>Linux `azdata` installation

`azdata CLI` installation package is available for Ubuntu or Debian with `apt`.

### <a id="azdata-apt"></a>Install `azdata` with apt (Ubuntu or Debian)

> **Note:** The Azdata CLI package does not use the system Python, rather installs its own Python interpreter.

1. Get packages needed for the install process:

    ```bash
    sudo apt-get update
    sudo apt-get install gnupg ca-certificates curl apt-transport-https lsb-release
    ```

2. Download and install the signing key:

    ```bash
    sudo curl -sL http://repo.corp.microsoft.com/browse/repo/ubuntu/dpgswdist.v1.asc | gpg --dearmor | tee /etc/apt/trusted.gpg.d/dpgswdist.v1.asc.gpg > /dev/null
    ```

3. Add the Azdata CLI repository information:

    ```bash
    sudo echo "deb [trusted=yes arch=amd64] http://repo.corp.microsoft.com/browse/repo/ubuntu/azdata-test mssql main" | tee /etc/apt/sources.list.d/azdata-cli.list
    ```

4. Update repository information and install `azdata`:

    ```bash
    sudo apt-get update
    sudo apt-get install azdata-cli
    ```

5. Verify installation:

    ```bash
    azdata --version
    ```

## Update

Upgrade the Azdata CLI only:

```bash
sudo apt-get update && sudo apt-get install --only-upgrade -y azdata-cli
```

## Uninstall

1. Uninstall with apt-get remove:

    ```bash
    sudo apt-get remove -y azdata-cli
    ```

2. Remove the Azdata CLI repository information:

    >[!NOTE]
    >This step is not needed if you plan on installing Azdata CLI in the future

    ```bash
    sudo rm /etc/apt/sources.list.d/azdata-cli.list
    ```

3. Remove the signing key:

    ```bash
    sudo rm /etc/apt/trusted.gpg.d/dpgswdist.v1.asc.gpg
    ```

4. Remove any unneeded dependencies that were installed with Azdata CLI:

    ```bash
    sudo apt autoremove
    ```

## Next steps

For more information about big data clusters, see [What are [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]?](big-data-cluster-overview.md).
