---
# required metadata

title: Install SQL Server on Linux | SQL Server vNext CTP1
description: 
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 10-20-2016
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

It also has support for the Docker Engine 1.8+ running on any supported Linux distribution. In addition, you can use the latest version of Docker for Mac/Windows.

## Command-line tools and ODBC drivers
You can optionally install the SQL Server command-line tools. It also installs the Microsoft ODBC drivers and dependencies. 

The following tools are installed:

| Tool | Description |
|-----|-----|
| [sqlcmd](https://msdn.microsoft.com/library/ms162773.aspx) | Run Transact-SQL queries or scripts on any SQL Server instance. |
| [bcp](https://msdn.microsoft.com/library/ms162802.aspx) | Bulk copies data between an instance of Microsoft SQL Server and a data file in a user-specified format. |

## Installation guides

The following guides provide step-by-step installation guides for the supported Linux distributions.

- [Install SQL Server on Linux (Ubuntu)](sql-server-linux-setup-ubuntu.md)
- [Install SQL Server on Linux (Red Hat Enterprise Linux)](sql-server-linux-setup-red-hat.md)
- [Install SQL Server on Linux (Docker)](sql-server-linux-setup-docker.md)

## Next Steps
After installing SQL Server on Linux, next see [how to connect to the server and run basic Transact-SQL queries](sql-server-linux-connect-and-query.md).


