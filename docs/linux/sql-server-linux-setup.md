---
# required metadata

title: Install SQL Server on Linux | Microsoft Docs
description: SQL Server 2017 CTP 2.1 now runs on Linux. This topic provides an overview on how to install SQL Server on Linux with links to the guides for specific platforms. 
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 05/25/2017
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: 565156c3-7256-4e63-aaf0-884522ef2a52

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
# Install SQL Server on Linux

SQL Server 2017 CTP 2.1 is supported on Red Hat Enterprise Linux, SUSE Linux Enterprise Server, and Ubuntu. It is also available as a Docker image which can run on Docker Engine on Linux or Docker for Windows/Mac. The topics in this section provide tutorials and general guidance for installing SQL Server 2017 CTP 2.1 on Linux. 

## Supported platforms

SQL Server 2017 CTP 2.1 is supported on the following platforms:

| Platform | Supported version(s) | Get
|-----|-----|-----
| **Red Hat Enterprise Linux** | 7.3 | [Get RHEL 7.3](http://access.redhat.com/products/red-hat-enterprise-linux/evaluation)
| **SUSE Linux Enterprise Server** | v12 SP2 | [Get SLES v12 SP2](https://www.suse.com/products/server)
| **Ubuntu** | 16.04 and 16.10| [Get Ubuntu 16.04](http://www.ubuntu.com/download/server)
| **Docker Engine** | 1.8+ | [Get Docker](http://www.docker.com/products/overview)

## <a id="system"></a> System requirements

SQL Server 2017 has the following system requirements for Linux:

|||
|-----|-----|
| **Memory** | Minimum 3.25 GB |
| **File System** | **XFS** or **EXT4** (other file systems, such as **BTRFS**, are unsupported) |
| **Disk space** | Minimum 1 GB |
| **Processor speed** | Minimum 1.4 Ghz |
| **Processor cores** | 2 |
| **Processor type** | x86-64-compatible only |

> [!NOTE]
> SQL Server Engine has been tested up to 1 TB of memory at this time.

## <a id="platforms"></a> Install SQL Server

- [Install on Red Hat Enterprise Linux](sql-server-linux-setup-red-hat.md)
- [Install on SUSE Linux Enterprise Server](sql-server-linux-setup-suse-linux-enterprise-server.md)
- [Install on Ubuntu](sql-server-linux-setup-ubuntu.md)
- [Run on Docker](sql-server-linux-setup-docker.md)

## Next steps

After installation, connect to the SQL Server instance to begin creating and managing databases. To get started, see [Connect and query SQL Server on Linux](sql-server-linux-connect-and-query-sqlcmd.md).
