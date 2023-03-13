---
title: Install Azure Data CLI (azdata) with zypper
description: Learn how to install the Azure Data CLI (azdata) tool with zypper.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: mihaelab
ms.date: 09/30/2020
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
ms.custom: intro-installation
---

# Install [!INCLUDE [azure-data-cli-azdata](../../includes/azure-data-cli-azdata.md)] with zypper

[!INCLUDE[azdata](../../includes/applies-to-version/azdata.md)]

For Linux distributions with `zypper` there is a package for the `azdata-cli`. The CLI package has been tested on Linux versions which use `zypper`:

- openSUSE 42.2 (leap) +
- SLES 12 SP 2 +

[!INCLUDE [azdata-package-installation-remove-pip-install](../../includes/azdata-package-installation-remove-pip-install.md)]

## Install with zypper

>[!IMPORTANT]
>The RPM package of the `azdata-cli` depends on the python3 package. On your system, this may be a Python version which predates the requirement of *Python 3.6.x*. If this poses an issue for you, find a replacement python3 package or follow the manual install instructions that use [`pip`](../install/deploy-install-azdata-pip.md).

1. Install dependencies necessary to install `azdata-cli`.

   ```bash
   sudo zypper install -y curl
   ```

1. Import the Microsoft repository key.

   ```bash
   sudo rpm --import https://packages.microsoft.com/keys/microsoft.asc
   ```

1. Create local repository information.

   ```bash
   sudo zypper addrepo -fc https://packages.microsoft.com/config/sles/12/prod.repo
   ```

1. Install `azdata-cli`.

   ```bash
   sudo zypper install --from packages-microsoft-com-mssql-server-2019 -y azdata-cli
   ```

## Verify install

```bash
azdata
azdata --version
```

## Update

Update the `azdata-cli` with the `zypper update` command.

```bash
sudo zypper refresh
sudo zypper update azdata-cli
```

## Uninstall

Remove the package from your system.

```bash
sudo zypper removerepo azdata-cli
```

## Next steps

For more information about big data clusters, see [What are [!INCLUDE[big-data-clusters-2019](../../includes/ssbigdataclusters-ver15.md)]?](../../big-data-cluster/big-data-cluster-overview.md).
