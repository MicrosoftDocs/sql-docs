---
# required metadata

title: Get started with SQL Server on Linux - SQL Server vNext CTP1 | Microsoft Docs
description: This topic provides a learning path for getting started with SQL Server vNext on Linux. It also includes links to other resources for each step.
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 11/07/2016
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
# ms.custom: ""

---
# Get started with SQL Server on Linux

This topic is a guide for how to get started using SQL Server vNext CTP1 on Linux. It contains a suggested learning plan with links to other resources for each step.

## 1: Install Linux
If you do not already have a Linux machine, install Linux on a physical server or a virtual machine (VM). Review the [Release notes](sql-server-linux-release-notes.md) on supported platforms and requirements.

> [!NOTE]
> One option is to create use a pre-configured Linux VM in Azure. In addition to OS-only VMs, there is also a VM image with SQL Server vNext CTP1 already installed. For more information, see [Provision a Linux VM in Azure for SQL Server](sql-server-linux-azure-virtual-machine.md). 

## 2: Install SQL Server
Next, set up SQL Server vNext on your Linux machine using one of the following guides:

| Platform | Installation |
|-----|-----|
| Ubuntu 16.04 | [Installation guide](sql-server-linux-setup-ubuntu.md) |
| Red Hat Enterprise 7.2 | [Installation guide](sql-server-linux-setup-red-hat.md) |

You can also run SQL Server on a Docker image that can run on platforms that support Docker, including Linux, Mac, and Windows. For more information, see the [Docker installation guide](sql-server-linux-setup-docker.md).

## 3: Connect locally or remotely
After installation, connect to the running SQL Server instance on your Linux machine. For a general discussion of connectivity, see [Connect and query SQL Server on Linux](sql-server-linux-connect-and-query.md). Then run some Transact-SQL queries using a client tool. Examples include:

| Tool | Tutorial |
|-----|-----|
| Sqlcmd | [Use the Sqlcmd command-line utility on Linux](sql-server-linux-connect-and-query-sqlcmd.md) |
| Visual Studio Code (VS Code) | [Use the multi-platform VS Code tool for SQL Server on Linux](sql-server-linux-connect-and-query-vs-code.md) |
| SQL Server Management Studio (SSMS) | [Use SSMS on Windows to connect to SQL Server on Linux](sql-server-linux-connect-and-query-ssms.md) |

## 4: Explore SQL Server capabilities on Linux
SQL Server vNext has the same underlying database engine on all supported platforms, including Linux. So many existing features and capabilities operate the same way on Linux.

If you are already familiar with SQL Server, you'll want to review the [Release notes](sql-server-linux-release-notes.md) for general guidelines and known issues for this release.

If you are new to SQL Server, there are a few additional resources to help you evaluate SQL Server:
 - [Manage SQL Server on Linux](sql-server-linux-management-overview.md)
 - [Get started with security features](sql-server-linux-security-get-started.md)
 - [Get started with performance features](sql-server-linux-performance-get-started.md)

## Next Steps
For more information about this release of SQL Server vNext, see [Overview of SQL Server on Linux](sql-server-linux-overview.md).

For the complete set of SQL Server documentation, see the [Microsoft SQL Server Documentation](https://msdn.microsoft.com/library/mt590198.aspx).
