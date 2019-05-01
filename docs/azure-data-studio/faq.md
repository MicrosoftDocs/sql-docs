---
title: FAQ
titleSuffix: Azure Data Studio
description: Frequently asked questions (FAQ) about Azure Data Studio.
ms.prod: sql
ms.technology: azure-data-studio
ms.topic: conceptual
author: "markingmyname"
ms.author: "maghan"
manager: craigg
ms.reviewer: "alayu; sstein"
ms.custom: "seodec18"
ms.date: "09/24/2018"
---

# [!INCLUDE[Azure Data Studio](../includes/name-sos.md)] FAQ

## What is Azure Data Studio?

Azure Data Studio is a new open source, cross-platform desktop environment for data professionals using the Azure Data family of on-premises and cloud data platforms on Windows, MacOS, and Linux. Previously released under the preview name SQL Operations Studio, Azure Data Studio offers a modern editor experience with lightning fast IntelliSense, code snippets, source control integration, and an integrated terminal. It is engineered with the data platform user in mind, with built in charting of query result sets and customizable dashboards.

Research has shown that users spend an order of magnitude more time working on query editing than on any other task with SQL Server Management Studio. For that reason, Azure Data Studio has been designed to focus deeply on the functionality that is used the most, with additional experiences made available as optional extensions into the product. This allows every user to customize their environment to the workflows that they use most often.


## How much does Azure Data Studio cost?

Azure Data Studio is free for private or commercial use.

## Who should use Azure Data Studio

Anyone can use Azure Data Studio. However, it is designed to simplify tasks performed by database developers, database administrators, system administrators, and independent software vendors.

## What can I do with Azure Data Studio?

Azure Data Studio is built on top of Visual Studio Code and offers a lightweight, keyboard focused modern code workflow experience when working with SQL Server, Azure SQL Database, Azure SQL DW. Azure Data Studio makes the core experiences that you rely on every day simple and easy with built-in features such as multiple tab windows, a rich SQL editor, IntelliSense, keyword completion, code snippets & code navigation and source control integration (Git and TFS). You can execute on-demand queries, view & save results as text, JSON, or Excel, edit data, organize & manage your favorite database connections, and browse database objects in a familiar object browsing experience.

Use your favorite command-line tools (for example, Bash, PowerShell, sqlcmd, bcp, psql, and ssh) in the Integrated Terminal window right within the Azure Data Studio user interface. Easily generate and execute CREATE and INSERT scripts for your database objects to create copies of your database for development or testing purposes. Boost your productivity with smart code snippets and rich graphical experiences that create new databases and database objects (such as tables, views, stored procedures, users, logins, roles, etc.) or update existing database objects. Use rich customizable dashboards to monitor and quickly troubleshoot performance bottlenecks in your databases on-premises, in Azure or any cloud.

Azure Data Studio offers a consistent experience to back up and restore your databases. With planned support for SQL Server Always-On Availability Groups, you can easily configure, monitor, and troubleshoot AGs for your mission-critical SQL Server databases and quickly failover to a secondary database during a disaster. Azure Data Studio has been designed to make you more productive in the DevOps lifecycle of your databases of choice on the operating systems of your choice. As a result, you are always in control, and you can reduce risks, solve problems faster, and continuously deliver value that exceeds customers' expectations.

## Is Azure Data Studio Open Source?

The source code for Azure Data Studio and its data providers is available on GitHub. The source code for the front-end Azure Data Studio (which is based on Visual Studio Code) is available under a source code EULA that provides rights to modify and use the software, but not to redistribute it or host it in a cloud service. The source code for the data providers is available under the MIT license at [https://github.com/Microsoft/sqltoolsservice](https://github.com/Microsoft/sqltoolsservice).

## Do we plan to open source SSMS?

No. However, next generation multi-OS CLI and GUI tools are open source. For example, the mssql extension for VS Code, mssql-scripter, and msql-CLI are all open source on GitHub. The source code for Azure Data Studio is available on GitHub.  

## Now that there is Azure Data Studio, does Microsoft plan to deprecate SSMS and SSDT? 

No. Investments in flagship Windows tools (SSMS, SSDT, PowerShell) will continue in addition to the next generation of multi-OS and multi-DB CLI and GUI tools. The goal is to offer customers the choice of using the tools they want on the platforms of their choice for their scenarios. Azure Data Studio is more tightly focused on the experiences around query editing and data development, which research has shown is the most heavily used capability in SQL Server Management Studio by an order of magnitude. Additional high-value administrative features such as backup, restore, agent job management, and server profiling are also available as extensions in Azure Data Studio. Azure Data Studio is also cross-platform, allowing users to work on their platform of choice. However, SQL Server Management Studio still offers the broadest range of administrative functions and remains the flagship tool for platform management tasks. 

## When Should I Use Azure Data Studio vs SQL Server Management Studio?

*Use Azure Data Studio if you:*

- Spend most of your time editing or executing queries.
- Need the ability to quickly chart and visualize resultsets.
- Can execute most administrative tasks via the integrated terminal using sqlcmd or Powershell.
- Have minimal need for wizard experiences.
- Do not need to do deep administrative or platform related configuration.
- Need to run on macOS or Linux.

*Use SQL Server Management Studio if you:*

- Spend most of your time on database administration tasks.
- Are doing complex administrative or platform configuration.
- Are doing security management, including user management, vulnerability assessment, and configuration of security features.
- Need to make use of performance tuning advisors and dashboards.
- Use database diagrams and table designers.
- Are doing import/export of DACPACs.
- Need access to Registered Servers.
- Make use of sqlcmd mode, live query stats, or client statistics.

## Feature Comparison

### Shell features

|Feature|Azure Data Studio|SSMS|
|:---|:---|:---|
|Azure Sign-In|Yes|Yes|
|Dashboard|Yes| |
|Extensions|Yes| |
|Integrated Terminal|Yes||
|Object Explorer|Yes|Yes|
|Object Scripting|Yes|Yes|
|Project System|Yes||
|Select from Table|Yes|Yes|
|Source Code Control|Yes||
|Task Pane|Yes||
|Theming|Yes||
|Dark Mode|Yes||
|Azure Resource Explorer|Preview||
|Generate Scripts Wizard||Yes
|Import\Export DACPAC||Yes|
|Object Properties||Yes|
|Table Designer||Yes|

### Query Editor

|Feature|Azure Data Studio|SSMS|
|:---|:---|:---|
|Chart Viewer|Yes||
|Export Results to CSV, JSON, XLSX|Yes||
|IntelliSense|Yes|Yes|
|Snippets|Yes|Yes|
|Show Plan|Preview|Yes|
|Client Statistics||Yes|
|Live Query Stats||Yes|
|Query Options||Yes|
|Results to File||Yes|
|Results to Text||Yes|
|Spatial Viewer||Yes|
|SQLCMD||Yes|
|T-SQL Debugger||Yes|

### Operating System Support

|Feature|Azure Data Studio|SSMS|
|:---|:---|:---|
|Windows|Yes|Yes|
|macOS|Yes||
|Linux|Yes||

### Data Engineering

|Feature|Azure Data Studio|SSMS|
|:---|:---|:---|
|External Data Wizard|Preview||
|HDFS Integration|Preview||
|Notebooks|Preview||

### Database Administration

|Feature|Azure Data Studio|SSMS|
|:---|:---|:---|
|Backup / Restore|Yes|Yes|
|Flat File Import|Preview|Yes|
|SQL Agent|Preview|Yes|
|SQL Profiler|Preview|Yes|
|Always On||Yes|
|Always Encrypted||Yes|
|Copy Data Wizard||Yes|
|Data Tuning Advisor||Yes|
|Database Diagrams||Yes|
|Error Log Viewer||Yes|
|Maintenance Plans||Yes|
|Multi-Server Query||Yes|
|Policy Based Management||Yes|
|PolyBase||Yes|
|Query Store||Yes|
|Registered Servers||Yes|
|Replication||Yes|
|Security Management||Yes|
|Service Broker||Yes|
|SQL Mail||Yes|
|Template Explorer||Yes|
|Vulnerability Assessment||Yes|
|XEvent Management||Yes|


## Azure Data Studio is missing a feature that is in SSMS/SSDT. Will you add it?

It depends on the scenario & customer/business need. To help prioritize, file a suggestion on [GitHub](https://github.com/microsoft/azuredatastudio/issues).

## I understand Azure Data Studio and the mssql extension for VS Code are powered by a new tools service that uses SMO APIs under the covers. Is SMO available on Linux and macOS?

The SMO APIs are not yet available on Linux or macOS in a consumable way. We ported over a subset of the SMO APIs to .NET Core that we needed for Azure Data Studio and we plan to expand as part of the roadmap. The SQL Tools Service is on GitHub: [https://github.com/Microsoft/sqltoolsservice](https://github.com/Microsoft/sqltoolsservice).

## Do you plan to port the DACFx APIs and/or sqlpackage.exe and/or SSDT to Linux and macOS?

It's on the longer-term roadmap. To help prioritize, file a suggestion on [GitHub](https://github.com/microsoft/azuredatastudio/issues).

## Will SQL PowerShell cmdlets be available on Linux and macOS?

SQL PowerShell is available today on the PowerShell gallery and you can use it on Windows to work with SQL Server running anywhere, including SQL on Linux. Offering the SQL PowerShell cmdlets on Linux & macOS is in the roadmap. To help prioritize, file a suggestion on [GitHub](https://github.com/microsoft/azuredatastudio/issues).

## Who usually uses Azure Data Studio?

Developers and DBAs are usually the users of Azure Data Studio.

## Does Azure Data Studio integrate with Azure SQL Data Warehouse?

Yes. Azure Data Studio support for Azure SQL Data Warehouse is currently in preview, together with Azure SQL Database Managed Instance, and SQL Server 2019 Big Data.

## Why is Azure Data Studio important for the new version of SQL Server?

As SQL Server extends its capabilities into the Big Data space, it needs new tooling to support those use cases. For that reason, Azure Data Studio is today shipping a new preview experience of support for SQL Server Big Data, including the first ever notebook experience in the SQL Server toolset and a new Create External Table wizard that makes accessing data from remote SQL Server and Oracle instances easy and fast.
