---
title: Install Azure Data CLI (azdata) with apt
description: Learn how to install the Azure Data CLI (azdata) tool with apt.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: mihaelab
ms.date: 04/07/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
ms.custom: intro-installation
---

# Install [!INCLUDE [azure-data-cli-azdata](../../includes/azure-data-cli-azdata.md)] with apt

[!INCLUDE[azdata](../../includes/applies-to-version/azdata.md)]

For Linux distributions with `apt` there is a package for the `azdata-cli`. The CLI package has been tested on Linux versions which use `apt`:

- Ubuntu 16.04, Ubuntu 18.04, Ubuntu 20.04

[!INCLUDE [azdata-package-installation-remove-pip-install](../../includes/azdata-package-installation-remove-pip-install.md)]

## Install with apt

>[!IMPORTANT]
> The Debian package of `azdata-cli` depends on the `python3` package. On your system, this may be a Python version which predates the requirement of *Python 3.6.x*. If this poses an issue for you, find a replacement `python3` package or follow the manual installation instructions that use [`pip`](../install/deploy-install-azdata-pip.md).

1. Install dependencies necessary to install `azdata-cli`.

   ```bash
   sudo apt-get update
   sudo apt-get install gnupg ca-certificates curl wget software-properties-common apt-transport-https lsb-release -y
   ```

2. Import the Microsoft repository key.

   ```bash
   curl -sL https://packages.microsoft.com/keys/microsoft.asc |
   gpg --dearmor |
   sudo tee /etc/apt/trusted.gpg.d/microsoft.asc.gpg > /dev/null
   ```

3. Create local repository information.

   For Ubuntu 16.04 client run:

    ```bash
    sudo add-apt-repository "$(wget -qO- https://packages.microsoft.com/config/ubuntu/16.04/prod.list)"
    ```

   For Ubuntu 18.04 client run:

    ```bash
    sudo add-apt-repository "$(wget -qO- https://packages.microsoft.com/config/ubuntu/18.04/prod.list)"
    ```

   For Ubuntu 20.04 client run:

    ```bash
    sudo add-apt-repository "$(wget -qO- https://packages.microsoft.com/config/ubuntu/20.04/prod.list)"
    ```

4. Install `azdata-cli`.

   ```bash
   sudo apt-get update
   sudo apt-get install -y azdata-cli
   ```

## Verify install

```bash
azdata
azdata --version
```

## Update

Update the `azdata-cli` with the `apt-get update` and `apt-get install` commands.

```bash
sudo apt-get update && sudo apt-get install --only-upgrade -y azdata-cli
```

## Uninstall

1. Remove the package from your system.

   ```bash
   sudo apt-get remove -y azdata-cli
   ```

2. Remove the repository information if you do not plan to reinstall `azdata-cli`.

   ```bash
   sudo rm /etc/apt/sources.list.d/azdata-cli.list
   ```

3. Remove the repository key.

   ```bash
   sudo rm /etc/apt/trusted.gpg.d/dpgswdist.v1.asc.gpg
   ```

4. Remove dependencies no longer required.

   ```bash
   sudo apt autoremove
   ```

## Next steps

For more information about big data clusters, see [What are [!INCLUDE[big-data-clusters-2019](../../includes/ssbigdataclusters-ver15.md)]?](../../big-data-cluster/big-data-cluster-overview.md).
