---
# required metadata

title: Get started with SQL Server on Linux | SQL Server vNext CTP1
description: 
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 10-26-2016
ms.topic: article
ms.prod: sql-non-specified
ms.service: 
ms.technology: 
ms.assetid: 

# optional metadata

# keywords: ""
# ROBOTS: ""
# audience: ""
# ms.devlang: ""
# ms.reviewer: ""
# ms.suite: ""
# ms.tgt_pltfrm: ""
# ms.custom: ""

---
# Get started with SQL Server on Linux

This topic is a guide for how to get started using SQL Server vNext CTP1 on Linux. It does not assume that you have experience with either Linux or SQL Server. If you do have a background in either area, you can scan this topic to find any additional information or links to other resources.

## Install Linux
If you do not already have a Linux machine, you must either install Linux on a physical server or a virtual machine (VM).

### Manual installation
To manually install Linux on a target machine, review the documentation provided for your target platform:

- [Ubuntu 16.04](https://www.ubuntu.com/download)
- [Red Hat Enterprise Linux 7.2](https://www.redhat.com/en/technologies/linux-platforms/enterprise-linux)
- [Docker Engine 1.8+](http://www.docker.com/products/docker#/linux)

> [!NOTE]
> Although the focus of this topic is on installing SQL Server vNext on Linux, you can also install it on the latest [Docker for Mac](http://www.docker.com/products/docker#/mac) and [Docker for Windows](http://www.docker.com/products/docker#/windows). 

### Azure VMs
Another option is to install Linux on a virtual machine. Use the following topics to create virtual machines in Azure with Linux already installed. 

- [Ubuntu 16.04 Azure VM](https://azure.microsoft.com/marketplace/partners/canonical/ubuntuserver1604lts/)
- [Red Hat Enterprise Linux 7.2 Azure VM](https://azure.microsoft.com/marketplace/partners/redhat/redhatenterpriselinux72/)

For a tutorial that walks you through the VM creation process, see [Create a Linux VM on Azure using the Portal](https://azure.microsoft.com/documentation/articles/virtual-machines-linux-quick-create-portal/). For all other Azure Linux VM resources, see [Linux Virtual Machines documentation](https://azure.microsoft.com/documentation/services/virtual-machines/linux/).

## Install SQL Server
Next, set up SQL Server vNext on your Linux machine using one of the following guides:

| Distribution | Installation |
|-----|-----|
| Ubuntu | [Installation guide](sql-server-linux-setup-ubuntu.md) |
| Red Hat Enterprise | [Installation guide](sql-server-linux-setup-red-hat.md) |
| Docker | [Installation guide](sql-server-linux-setup-docker.md) |

## Connect locally or remotely
TODO: provide links to the connection topics and talk about local and remote connectivity

## Create a database
TODO: provide the T-SQL for creating a database. Point to restore article that installs the AdventureWorks sample DB.

## Explore SQL Server capabilities on Linux
TODO: provide links to the main SQL Server documentation, maybe a table of the most popular ones with links and descriptions. Link to supportability topic here too for guidance on understanding how to read the main documentation set in relation to SQL Server.

## Next Steps
TODO: point to other topics in this content set.
