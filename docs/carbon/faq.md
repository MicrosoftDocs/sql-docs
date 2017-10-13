---
title: Carbon FAQ | Microsoft Docs
description: Carbon is a lightweight, open source, multi-OS and multi-database tool, designed from the ground-up for DBAs and developers.
services: sql-database
author: sanagama
ms.author: sanagama
manager: craigg
ms.reviewer: achatter, alayu, erickang, sanagama, sstein
ms.service: data-tools
ms.workload: data-tools
ms.prod: NEEDED
ms.topic: article
ms.date: 10/11/2017
---
# Carbon FAQ

## What is Carbon?

Carbon is a free lightweight database development and operations tool which runs on your desktop and is available for Windows, macOS and Linux. It has built-in support for Azure SQL Database, Azure SQL Data Warehouse and SQL Server and PostgreSQL running on-premises or in any cloud and offers a consistent experience across databases of your choice on your favorite operating systems.

## Where can I get Carbon?

Download Carbon for Windows, macOS and Linux from http://aka.ms/vsdata

## How much does Carbon cost?

Carbon is free for private or commercial use.


## When will Carbon be generally available?

We are currently targeting H1 CY2018 for a GA release.


## Who should use Carbon?

Anyone can use Carbon. However, we created it to simplify tasks performed by database developers, database administrators, system administrators and independent software vendors.


## What can I do with Carbon? 

Carbon is built on top of Visual Studio Code and offers a lightweight, keyboard focused modern code workflow experience when working with SQL and PostgreSQL. 

We’ve made the core experiences that you rely on every day simple and easy with built-in features such as multiple tab windows, a rich SQL editor, IntelliSense, keyword completion, code snippets & code navigation and source control integration (Git and TFS). You can execute ad-hoc queries, view & save results as text, JSON or Excel, edit data, organize & manage your favorite database connections and browse database objects in a familiar object browsing experience.

Use your favorite command line tools (e.g. Bash, PowerShell, sqlcmd, bcp, psql and ssh) in the Integrated Terminal window right within the Carbon user interface. Easily generate and execute CREATE and INSERT scripts for your database objects to create copies of your database for development or testing purposes. Boost your productivity with smart code snippets and rich graphical experiences to create new databases and database objects (such as tables, views, stored procedures, users, logins, roles, etc.) or update existing database objects. Use rich customizable dashboards to monitor and quickly troubleshoot performance bottlenecks in your databases on-premises, in Azure or any cloud.

Carbon offers a consistent experience to backup and restore your databases. With planned support for SQL Server Always On Availability Groups, you can easily configure, monitor and troubleshoot AGs for your mission-critical SQL Server databases and quickly failover to a secondary database in the event of a disaster.
Carbon has been designed from the ground up to make you more productive in the DevOps lifecycle of your databases of choice on the operating systems of your choice. As a result, you are always in control, and you can reduce risks, solve problems faster, and continuously deliver value that exceeds customers’ expectations.


## Is Carbon Open Source? 

The source code for Carbon and its data providers is available on Github. The source code for the front-end of Carbon (which is based on Visual Studio Code) is available under a source code EULA that provides rights to modify and use the software, but not to redistribute it or host it in a cloud service. The source code for the data providers is available under the MIT license.


## What is the difference between the mssql extension for VS Code and Carbon? When should I use one versus the other?

TBD


## Is Carbon the new SQL Server Manager Studio (SSMS) or SQL Server Data Tools (SSDT)?

TBD


## What is the difference between SQL Server Management Studio (SSMS) and Carbon? When would I use one versus the other?

TBD


## Which SSMS features are missing from Carbon?

TBD


## Can we create extensions for Carbon similar to Visual Studio Code?

TBD


## Now that there is Carbon, does Microsoft plan to deprecate SSMS and SSDT?

No. We will continue to invest in our flagship Windows tools (SSMS, SSDT, PowerShell) in addition to the next generation of multi-OS and multi-DB CLI and GUI tools.
Our goal is to offer customers the choice of using the tools they want on the platforms of their choice for their scenarios.


## Do you plan to open source SSMS?

No, we do not. However, our next generation multi-OS CLI and GUI tools are open source. For example, the mssql extension for VS Code, mssql-scripter are all open source on Github. The source code for Carbon is available on Github.


## Carbon is missing a feature that is in SSMS/SSDT. Will you add it?
This depends on the scenario & customer/business need. Please file a suggestion on Github to help us prioritize: https://github.com/microsoft/vsdata/issues


## I understand Carbon and the mssql extension for VS Code are powered by a new “tools service” that uses SMO APIs under the covers. Is SMO available on Linux and macOS?

The SMO APIs are not yet available on Linux & macOS in a publicly consumable way. We ported over a subset of the SMO APIs to .NET Core that we needed for Carbon, and we plan to expand this as part of our roadmap.
The SQL Tools Service is on Github: https://github.com/Microsoft/sqltoolsservice


## I use SSDT and I am familiar with the DACFx APIs. Do you plan to port the DACFx APIs and/or sqlpackage.exe and/or SSDT to Linux and macOS?

This is on our longer-term roadmap. Please file a suggestion on Github to help us prioritize: https://github.com/microsoft/vsdata/issues


## Will SQL PowerShell cmdlets be available on Linux and macOS?

SQL PowerShell is available today on the PowerShell gallery and you can use it on Windows to work with SQL Server running anywhere, including SQL on Linux. Offering the SQL PowerShell cmdlets on Linux & macOS is in our roadmap. Please file a suggestion on Github to help us prioritize: https://github.com/microsoft/vsdata/issues

