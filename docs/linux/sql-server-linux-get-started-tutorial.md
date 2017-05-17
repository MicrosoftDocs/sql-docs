---
# required metadata

title: Get started with SQL Server on Linux | Microsoft Docs
description: This topic provides a learning path for getting started with SQL Server 2017 on Linux. It also includes links to other resources for each step.
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 03/17/2017
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: 66d96e59-2ded-4460-b350-fda80d93d79b

# optional metadata

# keywords: ""
# ROBOTS: ""
# audience: ""
# ms.devlang: ""
# ms.reviewer: ""
# ms.suite: ""
# ms.tgt_pltfrm: ""
ms.custom: H1Hack27Feb2017

---
# Find resources for installing and using SQL Server on Linux

Get started using SQL Server 2017 CTP 2.1 on Linux. Here are basic steps with links to how-to information.

## 1: Install Linux
If you do not already have a Linux machine, install Linux on a physical server or a virtual machine (VM). Review the [Release notes](sql-server-linux-release-notes.md) on supported platforms and requirements.

> [!NOTE]
> One option is to create use a pre-configured Linux VM in Azure. In addition to OS-only VMs, there is also a VM image with SQL Server 2017 CTP 2.1 already installed. For more information, see [Provision a Linux VM in Azure for SQL Server](sql-server-linux-azure-virtual-machine.md). 

## 2: Install SQL Server
Next, set up SQL Server 2017 on your Linux machine, or run the Docker image, using one of the following guides:

| Platform | Installation |
|-----|-----|
| Red Hat Enterprise | [Installation guide](sql-server-linux-setup-red-hat.md) |
| SUSE Linux Enterprise Server v12 SP2 | [Installation guide](sql-server-linux-setup-suse-linux-enterprise-server.md) |
| Ubuntu 16.04 and 16.10 | [Installation guide](sql-server-linux-setup-ubuntu.md) |
| Docker | [Installation guide](sql-server-linux-setup-docker.md) |

Note that Docker itself runs on multiple platforms, which means that you can run the Docker image on Linux, Mac, and Windows.

> [!NOTE] 
> You need at least 3.25GB of memory to run SQL Server on Linux.
> SQL Server Engine has been tested up to 1 TB of memory at this time.

## 3: Connect locally or remotely
After installation, connect to the running SQL Server instance on your Linux machine. For a general discussion of connectivity, see [Connect and query SQL Server on Linux](sql-server-linux-connect-and-query-sqlcmd.md). Then run some Transact-SQL queries using a client tool. Examples include:

| Tool | Tutorial |
|-----|-----|
| Sqlcmd | [Use the Sqlcmd command-line utility on Linux](sql-server-linux-connect-and-query-sqlcmd.md) |
| Visual Studio Code (VS Code) | [Use VS Code with SQL Server on Linux](sql-server-linux-develop-use-vscode.md) |
| SQL Server Management Studio (SSMS) | [Use SSMS on Windows to connect to SQL Server on Linux](sql-server-linux-develop-use-ssms.md) |
| SQL Server Data Tools (SSDT) | [Use SSDT with SQL Server on Linux](sql-server-linux-develop-use-ssdt.md) |

## 4: Explore SQL Server capabilities on Linux
SQL Server 2017 has the same underlying database engine on all supported platforms, including Linux. So many existing features and capabilities operate the same way on Linux.

If you are already familiar with SQL Server, you'll want to review the [Release notes](sql-server-linux-release-notes.md) for general guidelines and known issues for this release.

If you are new to SQL Server, you might find it helpful to quickly explore some of the security and performance capabilities in the following two guides:
 - [Get started with security features](sql-server-linux-security-get-started.md)
 - [Get started with performance features](sql-server-linux-performance-get-started.md)

Then learn how to develop and manage SQL Server:
 - [Develop for SQL Server on Linux](sql-server-linux-develop-overview.md)
 - [Manage SQL Server on Linux](sql-server-linux-management-overview.md)
 - [Configure high availability](sql-server-linux-business-continuity-dr.md)

## Next steps

For the complete set of SQL Server documentation, see the [Microsoft SQL Server Documentation](https://msdn.microsoft.com/library/mt590198.aspx).
