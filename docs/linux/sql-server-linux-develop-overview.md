---
title: Develop applications for SQL Server on Linux | Microsoft Docs
description: 
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 11/17/2017
ms.topic: conceptual
ms.prod: sql
ms.custom: "sql-linux"
ms.technology: linux
ms.assetid: 758cb738-b018-465b-9ab0-59a24b892e66
---
# How to get started developing applications for SQL Server on Linux

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

You can create applications that connect to and use SQL Server on Linux from a variety of programming languages, such as C#, Java, Node.js, PHP, Python, Ruby, and C++. You can also use popular web frameworks and Object Relational Mapping (ORM) frameworks.

> [!VIDEO https://channel9.msdn.com/events/Connect/2017/T153/player]

> [!TIP]
> These same development options also enable you to target SQL Server on other platforms. Applications can target SQL Server running on-premises or in the cloud, on Linux, Windows, or Docker on macOS. Or you can target Azure SQL Database and Azure SQL Data Warehouse.

## Try the tutorials

The best way to get started and build applications with SQL Server is to try it out for yourself.

- Browse to [Getting Started Tutorials](https://aka.ms/sqldev).
- Select your language and development platform.
- Try the code samples.

> [!TIP]
> If you want to develop for SQL Server on Docker, take a look at the **macOS** tutorials.

## Create new applications

If you're creating a new application, take a look at a list of the [Connectivity libraries](sql-server-linux-develop-connectivity-libraries.md) for a summary of the connectors and popular frameworks available for various programming languages.

## Use existing applications

If you have an existing database application, you can simply change its connection string to target SQL Server on Linux. Make sure to read about the [Known Issues](sql-server-linux-release-notes.md) in SQL Server on Linux.

## Use existing SQL tools on Windows with SQL Server on Linux

Tools that currently run on Windows such as SSMS, SSDT, and PowerShell, also work with SQL Server on Linux. Although they do not run natively on Linux, you can still manage remote SQL Server instances on Linux. 

See the following topics for more information:

- [SQL Server Management Studio (SSMS)](sql-server-linux-manage-ssms.md)
- [SQL Server Data Tools (SSDT)](sql-server-linux-develop-use-ssdt.md)
- [SQL PowerShell](sql-server-linux-manage-powershell.md)

> [!Note]
> Make sure that you are using the latest versions of these tools for the best experience.

## Use new SQL tools for Linux

You can use the new [mssql extension](https://aka.ms/mssql-marketplace) for [Visual Studio Code](https://code.visualstudio.com) on Linux, macOS, and Windows. For a step-by-step walkthrough, see the following tutorial:

- [Use Visual Studio Code](sql-server-linux-develop-use-vscode.md)

You can also use new command-line tools that are native for Linux. These tools include the following:

- [sqlcmd](../tools/sqlcmd-utility.md)
- [bcp](sql-server-linux-migrate-bcp.md)
- [mssql-conf](sql-server-linux-configure-mssql-conf.md)

## Next steps

To get started, install SQL Server on Linux using one of the following quickstarts:

- [Install on Red Hat Enterprise Linux](quickstart-install-connect-red-hat.md)
- [Install on SUSE Linux Enterprise Server](quickstart-install-connect-suse.md)
- [Install on Ubuntu](quickstart-install-connect-ubuntu.md)
- [Run on Docker](quickstart-install-connect-ubuntu.md)
