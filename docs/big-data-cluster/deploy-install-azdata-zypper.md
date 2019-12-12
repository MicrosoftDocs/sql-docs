---
title: Install azdata with zypper
titleSuffix: SQL Server big data clusters
description: Learn how to install the azdata tool for installing and managing Big Data Clusters with zypper.
author: MikeRayMSFT 
ms.author: mikeray
ms.reviewer: mihaelab
ms.date: 12/10/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Install `azdata` with zypper

For Linux distributions with `zypper` there is a package for the `azdata-cli`. The CLI package has been tested on Linux versions which use `zyper`:

- openSUSE 42.2 (leap) +
- SLES 12 SP 2 +

## Install with zypper
>[!IMPORTANT]
>The RPM package of the `azdata-cli` depends on the python3 package. On your system, this may be a Python version which predates the requirement of *Python 3.6.x*. If this poses an issue for you, find a replacement python3 package or follow the manual install instructions that use [`pip`](deploy-install-pip.md).

1. Install dependencies necessary to install `azdata-cli`

   ```bash
   sudo zypper install -y curl
   ```

1. Import the Microsoft repository key

   ```bash
   sudo rpm --import https://packages.microsoft.com/keys/microsoft.asc
   ```

1. Create local `azdata-cli` repository information

   ```bash
   sudo zypper addrepo --name 'azdata-cli' --check https://packages.microsoft.com/{{!!!!!_WE_STILL_NEED_TO_GET_THIS_URL_!!!!!}}} azdata-cli
   ```

1. Install

   ```bash
   sudo zypper install --from azdata-cli -y azdata-cli
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

Remove the package from your system

```bash
   sudo zypper removerepo azdata-cli
```
