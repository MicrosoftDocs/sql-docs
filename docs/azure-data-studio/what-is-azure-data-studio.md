---
title: What is Azure Data Studio
description: Azure Data Studio is a free, light-weight tool, that runs on Windows, macOS, and Linux, for managing SQL Server, Azure SQL Database, and Azure Synapse Analytics.
author: "markingmyname"
ms.author: "maghan"
ms.reviewer: erinstellato
ms.date: 7/11/2022
ms.service: azure-data-studio
ms.topic: "overview"
ms.custom:
  - seodec18
  - sqlfreshmay19
  - intro-overview
---

# What is Azure Data Studio?

Azure Data Studio is a cross-platform database tool for data professionals using on-premises and cloud data platforms on Windows, macOS, and Linux.

Azure Data Studio offers a modern editor experience with IntelliSense, code snippets, source control integration, and an integrated terminal. It's engineered with the data platform user in mind, with built-in charting of query result sets and customizable dashboards.

The source code for Azure Data Studio and its data providers is available on GitHub under a source code EULA that provides rights to modify and use the software, but not to redistribute it or host it in a cloud service. For more information, see [Azure Data Studio FAQ](faq.yml).

**[Download and Install Azure Data Studio](./download-azure-data-studio.md)**

## SQL code editor with IntelliSense

Azure Data Studio offers a modern, keyboard-focused SQL coding experience that makes your everyday tasks easier with built-in features, such as multiple tab windows, a rich SQL editor, IntelliSense, keyword completion, code snippets, code navigation, and source control integration (Git). Run on-demand SQL queries, view and save results as text, JSON, or Excel. Edit data, organize your favorite database connections, and browse database objects in a familiar object browsing experience. To learn how to use the SQL editor, see [Use the SQL editor to create database objects](tutorial-sql-editor.md).

## Smart SQL code snippets

SQL code snippets generate the proper SQL syntax to create databases, tables, views, stored procedures, users, logins, roles, and to update existing database objects. Use smart snippets to quickly create copies of your database for development or testing purposes, and to generate and execute CREATE and INSERT scripts.

Azure Data Studio also provides functionality to create custom SQL code snippets. To learn more, see [Create and use code snippets](code-snippets.md).

## Customizable Server and Database Dashboards

Create rich customizable dashboards to monitor and quickly troubleshoot performance bottlenecks in your databases. To learn about insight widgets, and database (and server) dashboards, see [Manage servers and databases with insight widgets](insight-widgets.md).

## Connection management (server groups)

Server groups provide a way to organize connection information for the servers and databases you work with. For details, see [Server groups](server-groups.md).

## Integrated Terminal

Use your favorite command-line tools (for example, Bash, PowerShell, sqlcmd, bcp, and ssh) in the Integrated Terminal window right within the Azure Data Studio user interface. To learn about the integrated terminal, see [Integrated terminal](integrated-terminal.md).

## Extensibility and extension authoring

Enhance the Azure Data Studio experience by extending the functionality of the base installation. Azure Data Studio provides extensibility points for data management activities, and support for extension authoring.

To learn about extensibility in Azure Data Studio, see [Extensibility](extensibility.md).
To learn about authoring extensions, see [Extension authoring](extensions/extension-authoring.md).

## Feature comparison with SQL Server Management Studio (SSMS)

**Use Azure Data Studio if you:**

- Are mostly editing or executing queries.
- Need the ability to quickly chart and visualize result sets.
- Can execute most administrative tasks via the integrated terminal using sqlcmd or PowerShell.
- Have minimal need for wizard experiences.
- Do not need to do deep administrative or platform related configuration.
- Need to run on macOS or Linux.

**Use SQL Server Management Studio if you:**

- Are doing complex administrative or platform configuration.
- Are doing security management, including user management, vulnerability assessment, and configuration of security features.
- Need to make use of performance tuning advisors and dashboards.
- Use database diagrams and table designers.
- Need access to Registered Servers.
- Make use of live query stats or client statistics.

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
|Themes, including Dark Mode|Yes||
|Azure Resource Explorer|Preview||
|Generate Scripts Wizard||Yes|
|Object Properties||Yes|
|Table Designer|Preview|Yes|

### Query Editor

|Feature|Azure Data Studio|SSMS|
|:---|:---|:---|
|Chart Viewer|Yes||
|Export Results to CSV, JSON, XLSX|Yes||
|Results to File||Yes|
|Results to Text||Yes|
|IntelliSense|Yes|Yes|
|Snippets|Yes|Yes|
|Show Plan|Preview|Yes|
|Client Statistics||Yes|
|Live Query Stats||Yes|
|Query Options||Yes|
|Spatial Viewer||Yes|
|SQLCMD|Yes|Yes|

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
|Flat File Import|Yes|Yes|
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
|SQL Assessment|Preview|Yes|
|SQL Mail||Yes|
|Template Explorer||Yes|
|Vulnerability Assessment||Yes|
|XEvent Management||Yes|

### Database Development
|Feature|Azure Data Studio|SSMS|
|:---|:---|:---|
|Import\Export DACPAC|Yes|Yes|
|SQL Projects|Preview||
|Schema Compare|Yes||

## SQL Tools Service

Azure Data Studio uses the [SqlToolsService](https://github.com/microsoft/sqltoolsservice) as the SQL API layer to the application.  SQL Tools Service is .NET-based and is open source under the MIT license. For SQL connectivity, SQL Tools Service uses [Microsoft.Data.SqlClient](https://github.com/dotnet/SqlClient) as the SQL driver.

## Next steps

- [Download and Install Azure Data Studio](./download-azure-data-studio.md)
- [Azure Data Studio FAQ](faq.yml)
- [Connect and query SQL Server](quickstart-sql-server.md)
- [Connect and query Azure SQL Database](quickstart-sql-database.md)
- [Download SQL Server Management Studio (SSMS)](../ssms/download-sql-server-management-studio-ssms.md)

[!INCLUDE[get-help-sql-tools](../includes/paragraph-content/get-help-sql-tools.md)]

[!INCLUDE[contribute-to-content](../includes/paragraph-content/contribute-to-content.md)]
