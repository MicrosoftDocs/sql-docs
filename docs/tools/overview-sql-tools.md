---
title: SQL query and management tools
description: SQL query and management tools for SQL Server, Azure SQL (Azure SQL database, Azure SQL managed instance, SQL virtual machines), and Azure SQL data warehouse.
ms.prod: sql
ms.prod_service: "sql-tools"
ms.technology: tools-other
ms.topic: conceptual
ms.assetid: 
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: seo-lt-2019
ms.date: 11/23/2019
monikerRange: ">=aps-pdw-2014||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017"
---

# SQL query and management tools

[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

To manage your database, you need a tool. While your databases run in the cloud, on Windows, or [Linux](../linux/sql-server-linux-overview.md), your tool doesn't need to run on the same platform as the database.

There are many database tools available, so this article provides descriptions and pointers to some of the available tools for working with your SQL databases. If you need help with deciding which tool you need, see [Which tool should I use?](#which-tool-should-i-choose).

## Which tool should I choose?

If you choose to manage a SQL Server instance or database, in a light-weight editor on Windows, Mac, or Linux? then you can choose [Azure Data Studio](../azure-data-studio/download.md).

If you want to manage a SQL Server instance or database on Windows with full GUI support, then choose [SQL Server Management Studio (SSMS)](../ssms/download-sql-server-management-studio-ssms.md).

If you want to create or maintain database code, including compile-time validation, refactoring, and designer support on Windows then choose [SQL Server Data Tools (SSDT)](../ssdt/download-sql-server-data-tools-ssdt.md).

If you want to write T-SQL scripts in a light-weight editor on Windows, Mac, or Linux, then choose the [mssql extension](https://marketplace.visualstudio.com/items?itemName=ms-mssql.mssql) that runs on [Visual Studio Code](https://code.visualstudio.com/).

If you want to query SQL Server with a command-line tool that features IntelliSense, syntax high-lighting, and more? then choose [mssql-cli](mssql-cli.md).

You can view the links to the different SQL tools column in the following tables. To download SQL Server, see [Install SQL Server](../database-engine/install-windows/install-sql-server.md).

## Recommended tools

The following tools provide a graphical user interface (GUI).

| Tool | Description | Operating system |
|:--|:--|:--|
| [**![ADS image](../tools/media/overview-sql-tools/azure-data-studio.svg) Azure Data Studio**](../azure-data-studio/download.md) | Run on-demand SQL queries, view and save results as text, JSON, or Excel. Edit data, organize your favorite database connections, and browse database objects in a familiar object browsing experience. | **Windows</br>macOS</br>Linux** |
| [**![SSMS image](../tools/media/overview-sql-tools/ssms.svg) SQL Server Management Studio (SSMS)**](../ssms/download-sql-server-management-studio-ssms.md) | Access, configure, manage, administer, and develop all components of SQL Server, Azure SQL Database, and SQL Data Warehouse. Provides a single comprehensive utility that combines a broad group of graphical tools with a number of rich script editors to provide access to SQl for developers and database administrators of all skill levels. | **Windows** |
| [**![SSDT image](../tools/media/overview-sql-tools/ssdt.svg) SQL Server Data Tools (SSDT)**](../ssdt/download-sql-server-data-tools-ssdt.md) | A modern development tool for building SQL Server relational databases, Azure SQL databases, Analysis Services (AS) data models, Integration Services (IS) packages, and Reporting Services (RS) reports. With SSDT, you can design and deploy any SQL Server content type with the same ease as you would develop an application in **[Visual Studio](https://visualstudio.microsoft.com/downloads/)**. | **Windows** |
| [**![VS Code image](../tools/media/overview-sql-tools/visual-studio-code.svg) Visual Studio Code**](https://code.visualstudio.com/) | The [mssql extension](https://marketplace.visualstudio.com/items?itemName=ms-mssql.mssql) for Visual Studio Code is the official SQL Server extension that supports connections to SQL Server and rich editing experience for T-SQL in Visual Studio Code. | **Windows</br>macOS</br>Linux** |

## Command-line tools

The following are the main command-line tools:

| Tool | Description | Runs on |
|:--|:--|:--|
|[**mssql-cli**](mssql-cli.md)|**mssql-cli** is an interactive command-line tool for querying SQL Server. | Windows, macOS, and Linux|
| [**sqlpackage**](sqlpackage.md) |**sqlpackage** is a command-line utility that automates several database development tasks. macOS and Linux versions of sqlpackage are currently in preview. | Windows, macOS, and Linux|
|[**SQL Server PowerShell**](../powershell/sql-server-powershell.md)| **SQL Server PowerShell** provides cmdlets for working with SQL| Windows, macOS, and Linux|
| [**sqlcmd**](sqlcmd-utility.md) |**sqlcmd** utility lets you enter Transact-SQL statements, system procedures, and script files at the command prompt. | Windows, macOS, and Linux|
|[**bcp**](bcp-utility.md)|The **b**ulk **c**opy **p**rogram utility (**bcp**) bulk copies data between an instance of [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] and a data file in a user-specified format.|Windows, macOS, and Linux|
|[**mssql-scripter (preview)**](https://github.com/Microsoft/mssql-scripter)|**mssql-scripter** is a multi-platform command-line experience for scripting SQL Server databases|Windows, macOS, and Linux|
|[**mssql-conf**](../linux/sql-server-linux-configure-mssql-conf.md)|**mssql-conf** configures SQL Server running on Linux.|Linux|

## Migration and other tools

| Tool | Description |
|:--|:--|
| [Configuration Manager](../tools/configuration-manager/sql-server-configuration-manager-help.md) | Use SQL Server Configuration Manager to configure SQL Server services and configure network connectivity. Configuration Manager runs on Windows|
| [SQL Server Migration Assistant](../ssma/sql-server-migration-assistant.md) | Use SQL Server Migration Assistant to automate database migration to SQL Server from Microsoft Access, DB2, MySQL, Oracle, and Sybase.|
| [Database Experimentation Assistant](../dea/database-experimentation-assistant-overview.md) | Use Database Experimentation Assistant to evaluate a targeted version of SQL for a given workload. |
| [Distributed Replay](../tools/distributed-replay/install-distributed-replay-overview.md) | Use the Distributed Replay feature to help you assess the impact of future SQL Server upgrades. Also use Distributed Replay to help assess the impact of hardware and operating system upgrades, and SQL Server tuning. |
| [ssbdiagnose](../tools/ssbdiagnose/ssbdiagnose-utility-service-broker.md) | The ssbdiagnose utility reports issues in Service Broker conversations or the configuration of Service Broker services. |

If you're looking for additional tools that aren't mentioned on this page, see [SQL Command Prompt Utilities](command-prompt-utility-reference-database-engine.md).