---
title: Install azdata with installer on Linux
titleSuffix: SQL Server big data clusters
description: Learn how to install the azdata tool for installing and managing SQL Server Big Data Clusters with the installer (Linux). 
author: MikeRayMSFT 
ms.author: mikeray
ms.reviewer: mihaelab
ms.date: 11/04/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Install `azdata` to manage [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] on Linux

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

This article describes how to install `azdata` for SQL Server 2019 Big Data Clusters on Linux. Before these package managers were available, the installation of `azdata` required `pip`.

The package managers are designed for various operating systems and distributions.

- For Windows and Linux (Ubuntu distribution), you can install with a [package manager](./deploy-install-azdata-installer.md) for a simpler experience.
- For Linux (Ubuntu), [install `azdata` with `apt`](#azdata-apt)

At this time, there are no package managers to install `azdata` on other operating systems or distributions. For these platforms, see [Install `azdata` without package manager](./deploy-install-azdata.md).

## <a id="linux"></a>Install `azdata` for Linux

`azdata` installation package is available for Ubuntu with `apt`.

### <a id="azdata-apt"></a>Install `azdata` with apt (Ubuntu)

>[!NOTE]
>The `azdata` package does not use the system Python, rather installs its own Python interpreter.

1. Get packages needed for the install process:

    ```bash
    sudo apt-get update
    sudo apt-get install gnupg ca-certificates curl apt-transport-https lsb-release -y
    ```

2. Download and install the signing key:

    ```bash
    wget -qO- https://packages.microsoft.com/keys/microsoft.asc | sudo apt-key add -
    ```

3. Add the `azdata` repository information:

    ```bash
    sudo add-apt-repository "$(wget -qO- https://packages.microsoft.com/config/ubuntu/16.04/mssql-server-2019.list)"
    ```

4. Update repository information and install `azdata`:

    ```bash
    sudo apt-get update
    sudo apt-get install -y azdata-cli
    ```

5. Verify installation:

    ```bash
    azdata --version
    ```

### Update

Upgrade `azdata` only:

```bash
sudo apt-get update && sudo apt-get install --only-upgrade -y azdata-cli
```

### Uninstall

1. Uninstall with apt-get remove:

    ```bash
    sudo apt-get remove -y azdata-cli
    ```

2. Remove the `azdata` repository information:

    >[!NOTE]
    >This step is not needed if you plan on installing `azdata` in the future

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
