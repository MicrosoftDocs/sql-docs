---
title: Install azdata with yum
titleSuffix: SQL Server big data clusters
description: Learn how to install the azdata tool for installing and managing Big Data Clusters with yum.
author: MikeRayMSFT 
ms.author: mikeray
ms.reviewer: mihaelab
ms.date: 12/10/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Install `azdata` with yum

For Linux distributions with `yum` there is a package for the `azdata-cli`. The CLI package has been tested on Linux versions which use `yum`:

- RHEL 7, RHEL 8
- CentOS 7, CentOS 8
- Fedora 29+

## Install with yum

>[!IMPORTANT]
> The RPM package of the `azdata-cli` depends on the python3 package. On your system, this may be a Python version which predates the requirement of *Python 3.6.x*. If this poses an issue for you, find a replacement python3 package or follow the manual install instructions that use [`pip`](deploy-install-azdata-pip.md).

1. Import the Microsoft repository key

   ```bash
   sudo rpm --import https://packages.microsoft.com/keys/microsoft.asc
   ```

1. Create local `azdata-cli` repository information

   RHEL 7, RHEL 8, CentOS 7, CentOS 8:

   ```bash
   sudo sh -c 'echo -e "[azdata-cli]
   name=azdata-cli https://packages.microsoft.com/yumrepos/azure-cli
   baseurl=https://packages.microsoft.com/yum/el/{{!!!!!_WE_STILL_NEED_TO_GET_THIS_URL_!!!!!}}
   enabled=1
   gpgcheck=1
   gpgkey=https://packages.microsoft.com/keys/microsoft.asc" > /etc/yum.repos.d/azdata-cli.repo'
   ```

   Fedora 29+

   ```bash
   sudo sh -c 'echo -e "[azdata-cli]
   name=azdata-cli https://packages.microsoft.com/yumrepos/azure-cli
   baseurl=https://packages.microsoft.com/yum/fc/{{!!!!!_WE_STILL_NEED_TO_GET_THIS_URL_!!!!!}}
   enabled=1
   gpgcheck=1
   gpgkey=https://packages.microsoft.com/keys/microsoft.asc" > /etc/yum.repos.d/azdata-cli.repo'
   ```

1. Install with the `yum install` command

   ```bash
   sudo yum install azdata-cli
   ```

## Verify install

```
azdata
azdata --version
```

## Update

Update the `azdata-cli` with the `yum update` command.

```bash
sudo yum update azdata-cli
```

## Uninstall

1. Remove the package from your system

   ```bash
   sudo yum remove azdata-cli
   ```

1. Remove the repository information if you do not plan to plan to reinstall `azdata-cli`

   ```bash
   sudo rm /etc/yum.repos.d/azdata-cli.repo
   ```