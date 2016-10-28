---
# required metadata

title: Install SQL Server on Linux | SQL Server vNext CTP1
description: 
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 10-27-2016
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
# Install SQL Server on Linux

SQL Server vNext CTP1 is supported on several distribution of Linux. It is also available as a Docker image which can run on Docker for Linux, Windows, and Mac. The topics in this section provide tutorials and general guidance for installing SQL Server vNext CTP1 on Linux. 

## Supported distributions
SQL Server vNext CTP1 is supported on the following distributions:

| Distribution | Supported version |
|-----|-----|
| Red Hat Enterprise Linux | 7 |
| Ubuntu | 16.04 |
| Docker Engine | 1.8+ |

If you want to go straight to a step-by-step installation guide, select your Linux distribution from the following list: 
> [!div class="op_single_selector"]
- [Ubuntu](sql-server-linux-setup-ubuntu.md)
- [Red Hat Enterprise Linux](sql-server-linux-setup-red-hat.md)
- [Docker](sql-server-linux-setup-docker.md)

> [!TIP] 
> In addition to the support for the Docker Engine running on Linux, you can also use the latest version of Docker for Mac/Windows.

## <a id="tools"> </a> Command-line tools and ODBC drivers
You can optionally install the SQL Server command-line tools. It also installs the Microsoft ODBC drivers and dependencies. 

The following tools are installed:

| Tool | Description |
|-----|-----|
| [sqlcmd](https://msdn.microsoft.com/library/ms162773.aspx) | Run Transact-SQL queries or scripts on any SQL Server instance. |
| [bcp](https://msdn.microsoft.com/library/ms162802.aspx) | Bulk copies data between an instance of Microsoft SQL Server and a data file in a user-specified format. |

For the specific steps for installing the tools, see the installation guides in the next section.

## Installation guides

The following guides provide step-by-step installation guides for the supported Linux distributions.

- [Install SQL Server on Linux (Ubuntu)](sql-server-linux-setup-ubuntu.md)
- [Install SQL Server on Linux (Red Hat Enterprise Linux)](sql-server-linux-setup-red-hat.md)
- [Install SQL Server on Linux (Docker)](sql-server-linux-setup-docker.md)

## Next Steps
After installation, connect to the SQL Server instance to create and manage databases. To get started, see [Connect and query SQL Server on Linux](sql-server-linux-connect-and-query.md).


