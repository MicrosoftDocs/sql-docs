---
# required metadata

title: Overview of development for SQL Server on Linux | SQL Server vNext CTP1
description: 
author: sanagama 
ms.author: sanagama 
manager: jhubbard
ms.date: 11/12/2016
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: 758cb738-b018-465b-9ab0-59a24b892e66

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
# Develop applications for SQL Server on Linux

You can create applications that connect to and use SQL Server from a variety of programming languages such as C#, Java, Node.js, PHP, Python, Ruby and C++ and popular web frameworks and Object Relational Mapping (ORM) frameworks. Your applications can target SQL Server running on-premises or in the cloud, on Linux, Windows or Docker on macOS and also Azure SQL Database and Azure SQL Data Warehouse.

The best way to get started and build applications with SQL Server on Linux, Docker on macOS and Windows is to try it out for yourself.
- Browse to [Getting Started Tutorials](http://www.microsoft.com/sql-server/developer-get-started)
- Select your language and development platform
- Try the code samples

> [!TIP]
> Take a look at the **macOS** tutorials if you're interested in using SQL Server vNext CTP1 on Docker.

## Create New applications for SQL Server on Linux
You can use your favorite programming language, web or ORM framework, code editors, tools and IDEs to develop new database applications on Linux and macOS that connect to and use SQL Server vNext CTP1 on Linux or in Docker on macOS. Take a look at [Connectivity libraries](sql-server-linux-develop-connectivity-libraries.md) for a summary of the connectors and popular frameworks available for various programming languages.

## Use existing applications with SQL Server on Linux
If you have an existing database application, you can simply change it's connection string to target SQL Server vNext CTP1 on Linux. Make sure to read about the [Known Issues](sql-server-linux-release-notes.ms) in SQL Server vNext CTP1 on Linux.

## Use existing SQL Tools on Windows with SQL Server on Linux
Tools that you currently use on Windows such as SSMS, SSDT and PowerShell work with SQL Server vNext CTP1 on Linux. Do make sure that you are using the latest versions of these tools for the best experience.

See the topics below for more information:
- [SQL Server Management Studio (SSMS)](sql-server-linux-develop-use-ssms.md)
- [SQL Server Data Tools (SSDT)](sql-server-linux-develop-use-ssdt.md)
- [SQL PowerShell](sql-server-linux-manage-powershell.md)

## Use New SQL Tools on Linux and macOS
You can use the new [mssql extension](https://aka.ms/mssql-marketplace) for [Visual Studio Code](https://code.visualstudio.com) on Linux, macOS and Windows with SQL Server running on-premises or in the cloud, on Linux, Windows or Docker on macOS and also with Azure SQL Database and Azure SQL Data Warehouse. You can also use new command line tools that are natively for Linux.

See the topics below for more information:
- [Use Visual Studio Code](sql-server-linux-develop-use-vscode.md)
- [sqlcmd](sql-server-linux-connect-and-query-sqlcmd.md)
- [bcp](sql-server-linux-migrate-bcp.md)
- [sqlpackage](sql-server-linux-migrate-sqlpackage.md)
- [mssql-conf](sql-server-linux-configure-mssql-conf.md)
