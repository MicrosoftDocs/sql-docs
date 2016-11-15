---
# required metadata

title: Install SQL Server on Linux - SQL Server vNext CTP1 | Microsoft Docs
description: SQL Server vNext CTP1 now runs on Linux. This topic provides an overview on how to install SQL Server on Linux with links to the guides for specific platforms. 
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 11/15/2016
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

SQL Server vNext CTP1 is supported on several distribution of Linux. It is also available as a Docker image which can run on Docker for Linux, Windows, and Mac. The topics in this section provide tutorials and general guidance for installing SQL Server vNext CTP1 on Linux. 

## Supported distributions
SQL Server vNext CTP1 is supported on the following distributions:

| Distribution | Supported version |
|-----|-----|
| **Red Hat Enterprise Linux** | 7.2 |
| **Ubuntu** | 16.04 |
| **Docker Engine** | 1.8+ |

> [!TIP] 
> In addition to the support for the Docker Engine running on Linux, you can also use the latest version of Docker for Mac/Windows.

If you want to go straight to a step-by-step installation guide, select your Linux distribution from the following list: 
> [!div class="op_single_selector"]
- [Red Hat Enterprise Linux](sql-server-linux-setup-red-hat.md)
- [Ubuntu](sql-server-linux-setup-ubuntu.md)
- [Docker](sql-server-linux-setup-docker.md)

## <a id="tools"> </a> Command-line tools and ODBC drivers
You can optionally install the SQL Server command-line tools. It also installs the Microsoft ODBC drivers and dependencies. 

The following tools are installed:

| Tool | Description |
|-----|-----|
| [sqlcmd](https://msdn.microsoft.com/library/ms162773.aspx) | Run Transact-SQL queries or scripts on any SQL Server instance. |
| [bcp](https://msdn.microsoft.com/library/ms162802.aspx) | Bulk copies data between an instance of Microsoft SQL Server and a data file in a user-specified format. |

For the specific steps for installing the tools, see the links to the installation guides in the next section.

## Next steps
Use one of the following installation guides to install SQL Server on Linux:

- [Install SQL Server on Linux (Ubuntu)](sql-server-linux-setup-ubuntu.md)
- [Install SQL Server on Linux (Red Hat Enterprise Linux)](sql-server-linux-setup-red-hat.md)
- [Install SQL Server on Linux (Docker)](sql-server-linux-setup-docker.md)

After installation, connect to the SQL Server instance to begin creating and managing databases. To get started, see [Connect and query SQL Server on Linux](sql-server-linux-connect-and-query.md).
