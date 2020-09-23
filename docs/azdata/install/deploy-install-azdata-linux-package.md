---
title: Install azdata with apt
titleSuffix: SQL Server big data clusters
description: Learn how to install the azdata tool for installing and managing SQL Server Big Data Clusters with apt. 
author: MikeRayMSFT 
ms.author: mikeray
ms.reviewer: mihaelab
ms.date: 09/30/2020
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Install `azdata` with apt

[!INCLUDE[SQL Server 2019](../../includes/applies-to-version/azdata.md)]

For Linux distributions with `apt` there is a package for the `azdata-cli`. The CLI package has been tested on Linux versions which use `apt`:

- Ubuntu 16.04, Ubuntu 18.04

[!INCLUDE [azdata-package-installation-remove-pip-install](../../includes/azdata-package-installation-remove-pip-install.md)]

## Install with apt

>[!IMPORTANT]
> The RPM package of the `azdata-cli` depends on the python3 package. On your system, this may be a Python version which predates the requirement of *Python 3.6.x*. If this poses an issue for you, find a replacement python3 package or follow the manual install instructions that use [`pip`](../install/deploy-install-azdata-pip.md).

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

Use azdata with [Azure Arc enabled data services](/azure/azure-arc/data/)