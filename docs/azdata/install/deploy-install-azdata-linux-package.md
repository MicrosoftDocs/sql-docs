---
title: Install azdata with installer on Linux
titleSuffix:
description: Learn how to install the azdata tool with the installer (Linux). 
author: MikeRayMSFT 
ms.author: mikeray
ms.reviewer: mihaelab
ms.date: 01/07/2020
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Install `azdata` with apt

[!INCLUDE[SQL Server 2019](../../includes/applies-to-version/sqlserver2019.md)]

This article describes how to install `azdata` on Linux. Before these package managers were available, the installation of `azdata` required `pip`.

[!INCLUDE [azdata-package-installation-remove-pip-install](../../includes/azdata-package-installation-remove-pip-install.md)]

## <a id="linux"></a>Install `azdata` for Linux

`azdata` installation package is available for Ubuntu with `apt`.

### <a id="azdata-apt"></a>Install `azdata` with apt (Ubuntu)

>[!NOTE]
>The `azdata` package does not use the system Python, rather installs its own Python interpreter.

1. Get packages needed for the install process:

    ```bash
    sudo apt-get update
    sudo apt-get install gnupg ca-certificates curl wget software-properties-common apt-transport-https lsb-release -y
    ```

2. Download and install the signing key:

    ```bash
    curl -sL https://packages.microsoft.com/keys/microsoft.asc |
    gpg --dearmor |
    sudo tee /etc/apt/trusted.gpg.d/microsoft.asc.gpg > /dev/null
    ```

3. Add the `azdata` repository information.

   For Ubuntu 16.04 client run:
    ```bash
    sudo add-apt-repository "$(wget -qO- https://packages.microsoft.com/config/ubuntu/16.04/prod.list)"
    ```

   For Ubuntu 18.04 client run:
    ```bash
    sudo add-apt-repository "$(wget -qO- https://packages.microsoft.com/config/ubuntu/18.04/prod.list)"
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

For more information about big data clusters, see [What are [!INCLUDE[big-data-clusters-2019](../../includes/ssbigdataclusters-ver15.md)]?](../../big-data-cluster/big-data-cluster-overview.md).

Use azdata with [Azure Arc enabled data services](/azure/azure-arc/data/)