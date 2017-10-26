---
title: Get started with SQL Server 2017 in the Cloud | Microsoft Docs
description: This quick start tutorial shows how to run the SQL Server 2017 on Linux in the cloud of your choice.
author: anshrest
ms.author: anshrest
manager: jhubbard
ms.date: 10/25/2017
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid:
---
# Run the SQL Server 2017 in the cloud

[!INCLUDE[tsql-appliesto-sslinux-only](../includes/tsql-appliesto-sslinux-only.md)]

In this quick start tutorial, you will install SQL Server 2017 on Red Hat Enterprise Linux (RHEL), SUSE Linux Enterprise Server (SLES), or Ubuntu in the cloud of your choice. Go to [Provision a Linux SQL Server virtual machine in the Azure portal](https://docs.microsoft.com/en-us/azure/virtual-machines/linux/sql/provision-sql-server-linux-virtual-machine?toc=%2fsql%2flinux%2ftoc.json) to run SQL Server on Linux in Azure.

## Amazon Web Services
## Digital Ocean
## Google Cloud Platform
1.	Create a Linux VM from the Gallery 
    a.	Ubuntu 16.04, RHEL 7.3+, SLES v12 SP2
    b.	Size at least 3.75 GB (n1-standard-1)
2.	Connect to the new VM with ssh
3.	Follow the quick start for your Linux distro 
a.	Red Hat
b.	SUSE
c.	Ubuntu
4.	Configure for remote connections 
a.	Go to the Firewall Rules page
b.	Add a rule to allow traffic on the port on which SQL Server listens (default of 1433) (tcp:1433)

## Heroku
## IBM
## Rackspace
## VMWare
