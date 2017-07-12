---
# required metadata

title: Overview of SQL Server on Linux | Microsoft Docs
description: This topic describes how SQL Server runs on Linux and provides information on how to learn more.
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 07/17/2017
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: 9dcc6a90-0add-42c2-815b-862e4e2a21ac

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
# SQL Server on Linux

SQL Server now runs on Linux! This latest release, SQL Server 2017 RC1, runs on Linux and is in
many ways simply SQL Server. It’s the same SQL Server database engine, with many similar features and services regardless of your operating system.

## Install

To get started, install SQL Server on Linux using one of the following quick start tutorials:

- [Install on Red Hat Enterprise Linux](quickstart-install-connect-red-hat.md)
- [Install on SUSE Linux Enterprise Server](quickstart-install-connect-suse.md)
- [Install on Ubuntu](quickstart-install-connect-ubuntu.md)
- [Run on Docker](quickstart-install-connect-ubuntu.md)

> [!NOTE]
> Docker itself runs on multiple platforms, which means that you can run the Docker image on Linux, Mac, and Windows.

## Connect

After installation, connect to the SQL Server instance on your Linux machine. You can connect locally or remotely and with a variety of tools and drivers. The quick start tutorials demonstrate how to use the [sqlcmd](sql-server-linux-setup-tools.md) command-line tool. Other tools include the following:

| Tool | Tutorial |
|-----|-----|
| Visual Studio Code (VS Code) | [Use VS Code with SQL Server on Linux](sql-server-linux-develop-use-vscode.md) |
| SQL Server Management Studio (SSMS) | [Use SSMS on Windows to connect to SQL Server on Linux](sql-server-linux-develop-use-ssms.md) |
| SQL Server Data Tools (SSDT) | [Use SSDT with SQL Server on Linux](sql-server-linux-develop-use-ssdt.md) |

## Explore

SQL Server 2017 has the same underlying database engine on all supported platforms, including Linux. So many existing features and capabilities operate the same way on Linux. This area of the documentation exposes some of these features from a Linux perspective. It also calls out areas that have unique requirements on Linux.

If you are already familiar with SQL Server, review the [Release notes](sql-server-linux-release-notes.md) for general guidelines and known issues for this release. Then look at [what's new for SQL Server on Linux](sql-server-linux-whats-new.md) as well as [what's new for SQL Server 2017 overall](../sql-server/what-s-new-in-sql-server-2017.md).

##  ![info_tip](./media/general/info_tip.png) Engage with the SQL Server engineering team 
- [Stack Overflow (tag sql-server) - ask technical questions](http://stackoverflow.com/questions/tagged/sql-server)
- [MSDN Forums - ask technical questions](https://social.msdn.microsoft.com/Forums/en-US/home?category=sqlserver)
- [Microsoft Connect - report bugs and request features](https://connect.microsoft.com/SQLServer/Feedback)
- [Reddit - discuss SQL Server](https://www.reddit.com/r/SQLServer/)