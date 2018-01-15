---
title: SQL Operations Studio (preview) FAQ | Microsoft Docs
description: Frequently asked questions (FAQ) for SQL Operations Studio (preview).
ms.custom: "tools|sos"
ms.date: "11/15/2017"
ms.prod: "sql-non-specified"
ms.reviewer: "alayu; erickang; sstein"
ms.suite: "sql"
ms.prod_service: sql-tools
ms.component: sos
ms.tgt_pltfrm: ""
ms.topic: "article"
author: "stevestein"
ms.author: "sstein"
manager: craigg
ms.workload: "Inactive"
---
# [!INCLUDE[name-sos](../includes/name-sos.md)] FAQ

## What is [!INCLUDE[name-sos](../includes/name-sos-short.md)]?

[!INCLUDE[name-sos](../includes/name-sos-short.md)] is a free lightweight database development and operations tool that runs on your desktop and is available for Windows, macOS, and Linux. [!INCLUDE[name-sos](../includes/name-sos-short.md)] has built-in support for Azure SQL Database, Azure SQL Data Warehouse, and SQL Server running on-premises or in any cloud. [!INCLUDE[name-sos](../includes/name-sos-short.md)] offers a consistent experience across databases of your choice on your favorite operating systems.

## Where can I get [!INCLUDE[name-sos](../includes/name-sos-short.md)]?

Download [!INCLUDE[name-sos](../includes/name-sos-short.md)] for Windows, macOS, and Linux from [http://aka.ms/sqlopsstudio](download.md)

## How much does [!INCLUDE[name-sos](../includes/name-sos-short.md)] cost?

[!INCLUDE[name-sos](../includes/name-sos-short.md)] is free for private or commercial use.

## Who should use [!INCLUDE[name-sos](../includes/name-sos-short.md)]?

Anyone can use [!INCLUDE[name-sos](../includes/name-sos-short.md)]. However, it is designed to simplify tasks performed by database developers, database administrators, system administrators, and independent software vendors.


## What can I do with [!INCLUDE[name-sos](../includes/name-sos-short.md)]? 

[!INCLUDE[name-sos](../includes/name-sos-short.md)] is built on top of Visual Studio Code and offers a lightweight, keyboard focused modern code workflow experience when working with SQL. 

[!INCLUDE[name-sos](../includes/name-sos-short.md)] makes the core experiences that you rely on every day simple and easy with built-in features such as multiple tab windows, a rich SQL editor, IntelliSense, keyword completion, code snippets & code navigation and source control integration (Git and TFS). You can execute on-demand queries, view & save results as text, JSON, or Excel, edit data, organize & manage your favorite database connections, and browse database objects in a familiar object browsing experience.

Use your favorite command-line tools (for example, Bash, PowerShell, sqlcmd, bcp, psql, and ssh) in the Integrated Terminal window right within the [!INCLUDE[name-sos](../includes/name-sos-short.md)] user interface. Easily generate and execute CREATE and INSERT scripts for your database objects to create copies of your database for development or testing purposes. Boost your productivity with smart code snippets and rich graphical experiences that create new databases and database objects (such as tables, views, stored procedures, users, logins, roles, etc.) or update existing database objects. Use rich customizable dashboards to monitor and quickly troubleshoot performance bottlenecks in your databases on-premises, in Azure or any cloud.

[!INCLUDE[name-sos](../includes/name-sos-short.md)] offers a consistent experience to back up and restore your databases. With planned support for SQL Server Always On Availability Groups, you can easily configure, monitor, and troubleshoot AGs for your mission-critical SQL Server databases and quickly failover to a secondary database during a disaster.
[!INCLUDE[name-sos](../includes/name-sos-short.md)] has been designed to make you more productive in the DevOps lifecycle of your databases of choice on the operating systems of your choice. As a result, you are always in control, and you can reduce risks, solve problems faster, and continuously deliver value that exceeds customers’ expectations.


## Is [!INCLUDE[name-sos](../includes/name-sos-short.md)] Open Source? 

The source code for [!INCLUDE[name-sos](../includes/name-sos-short.md)] and its data providers is available on GitHub. The source code for the front end of [!INCLUDE[name-sos](../includes/name-sos-short.md)] (which is based on Visual Studio Code) is available under a source code EULA that provides rights to modify and use the software, but not to redistribute it or host it in a cloud service. The source code for the data providers is available under the MIT license at [https://github.com/Microsoft/sqltoolsservice](https://github.com/Microsoft/sqltoolsservice).

## Do you plan to open source SSMS?

No. However, next generation multi-OS CLI and GUI tools are open source. For example, the mssql extension for VS Code, mssql-scripter, and msql-CLI are all open source on GitHub. The source code for [!INCLUDE[name-sos](../includes/name-sos-short.md)] is available on GitHub.


## Now that there is [!INCLUDE[name-sos](../includes/name-sos-short.md)], does Microsoft plan to deprecate SSMS and SSDT?

No. Investments in flagship Windows tools (SSMS, SSDT, PowerShell) will continue in addition to the next generation of multi-OS and multi-DB CLI and GUI tools.
The goal is to offer customers the choice of using the tools they want on the platforms of their choice for their scenarios.


## [!INCLUDE[name-sos](../includes/name-sos-short.md)] is missing a feature that is in SSMS/SSDT. Will you add it?
It depends on the scenario & customer/business need. To help prioritize, file a suggestion on [GitHub](https://github.com/microsoft/sqlopsstudio/issues).


## I understand [!INCLUDE[name-sos](../includes/name-sos-short.md)] and the mssql extension for VS Code are powered by a new *tools service* that uses SMO APIs under the covers. Is SMO available on Linux and macOS?

The SMO APIs are not yet available on Linux or macOS in a consumable way. We ported over a subset of the SMO APIs to .NET Core that we needed for [!INCLUDE[name-sos](../includes/name-sos-short.md)], and we plan to expand as part of the roadmap.
The SQL Tools Service is on GitHub: [https://github.com/Microsoft/sqltoolsservice](https://github.com/Microsoft/sqltoolsservice).


## Do you plan to port the DACFx APIs and/or sqlpackage.exe and/or SSDT to Linux and macOS?

It's on the longer-term roadmap. To help prioritize, file a suggestion on [GitHub](https://github.com/microsoft/sqlopsstudio/issues).


## Will SQL PowerShell cmdlets be available on Linux and macOS?

SQL PowerShell is available today on the PowerShell gallery and you can use it on Windows to work with SQL Server running anywhere, including SQL on Linux. Offering the SQL PowerShell cmdlets on Linux & macOS is in the roadmap. To help prioritize, file a suggestion on [GitHub](https://github.com/microsoft/sqlopsstudio/issues).

