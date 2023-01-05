---
title: Install azdata with yum
description: Learn how to install the azdata tool with yum.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: mihaelab
ms.date: 09/30/2020
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
ms.custom: intro-installation
---

# Install [!INCLUDE [azure-data-cli-azdata](../../includes/azure-data-cli-azdata.md)] with yum

[!INCLUDE[azdata](../../includes/applies-to-version/azdata.md)]

For Linux distributions with `yum` there is a package for the `azdata-cli`. The CLI package has been tested on Linux versions which use `yum`:

- RHEL 7, RHEL 8

[!INCLUDE [azdata-package-installation-remove-pip-install](../../includes/azdata-package-installation-remove-pip-install.md)]

## Install with yum

>[!IMPORTANT]
> The RPM package of the `azdata-cli` depends on the python3 package. On your system, this may be a Python version which predates the requirement of *Python 3.6.x*. If this poses an issue for you, find a replacement python3 package or follow the manual install instructions that use [`pip`](../install/deploy-install-azdata-pip.md).

1. Install dependencies necessary to install `azdata-cli`.

   ```bash
   sudo yum install -y curl
   ```

1. Import the Microsoft repository key.

   ```bash
   sudo rpm --import https://packages.microsoft.com/keys/microsoft.asc
   ```

1. Create local repository information.

   For a RHEL 7 client run:

   ```bash
   sudo curl -o /etc/yum.repos.d/mssql-server.repo https://packages.microsoft.com/config/rhel/7/prod.repo
   ```
  
   For a RHEL 8 client run:

   ```bash
   sudo curl -o /etc/yum.repos.d/mssql-server.repo https://packages.microsoft.com/config/rhel/8/prod.repo
   ```

1. Install `azdata-cli`.

   ```bash
   sudo yum install azdata-cli
   ```

## Verify install

```bash
azdata
azdata --version
```

## Update

Update the `azdata-cli` with the `yum update` command.

```bash
sudo yum update azdata-cli
```

## Uninstall

1. Remove the package from your system.

   ```bash
   sudo yum remove azdata-cli
   ```

1. Remove the repository information if you do not plan to reinstall `azdata-cli`.

   ```bash
   sudo rm /etc/yum.repos.d/azdata-cli.repo
   ```

## Next steps

For more information about big data clusters, see [What are [!INCLUDE[big-data-clusters-2019](../../includes/ssbigdataclusters-ver15.md)]?](../../big-data-cluster/big-data-cluster-overview.md).
