---
title: Install Azure Data CLI (azdata)
description: Learn how to install the Azure Data CLI azdata) tool.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: mihaelab, randolphwest
ms.date: 06/28/2024
ms.service: sql
ms.topic: conceptual
ms.custom:
  - intro-installation
---

# Install [!INCLUDE [azure-data-cli-azdata](../../includes/azure-data-cli-azdata.md)]

[!INCLUDE [azdata](../../includes/applies-to-version/azdata.md)]

[!INCLUDE [azure-data-cli-azdata](../../includes/azure-data-cli-azdata.md)] is a command-line utility written in Python to bootstrap and manage the data services via REST APIs.

## Find latest version

To find your installed version and see if you need to update, run `azdata --version`.

## OS specific instructions

- [Install on Windows](deploy-install-azdata-installer.md)
- [Install on macOS](deploy-install-azdata-macos.md)
- Install on Linux or [Windows Subsystem for Linux (WSL)](/windows/wsl/about/)
  - [Install with apt on Debian or Ubuntu](deploy-install-azdata-linux-package.md)
  - [Install with yum on Red Hat Enterprise Linux](deploy-install-azdata-yum.md)
  - [Install with Zypper on openSUSE or SLE](deploy-install-azdata-zypper.md)
  - [Install from script](deploy-install-azdata-pip.md)

[!INCLUDE [azdata-package-installation-remove-pip-install](../../includes/azdata-package-installation-remove-pip-install.md)]

## Related content

- [What are [!INCLUDE[big-data-clusters-2019](../../includes/ssbigdataclusters-ver15.md)]?](../../big-data-cluster/big-data-cluster-overview.md)
