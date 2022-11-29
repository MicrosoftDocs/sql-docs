---
title: SQL tools overview
description: SQL query and management tools for SQL Server, Azure SQL (Azure SQL database, Azure SQL managed instance, SQL virtual machines), and Azure Synapse Analytics.
ms.service: sql
ms.subservice: tools-other
ms.topic: conceptual
ms.assetid: 
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: seo-lt-2019
ms.date: 02/04/2020
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017"
---

# SQL tools overview

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

To manage your database, you need a tool. Whether your databases run in the cloud, on Windows, on macOS, or on [Linux](../linux/sql-server-linux-overview.md), your tool doesn't need to run on the same platform as the database.

You can view the links to the different SQL tools in the following tables.

> [!Note]
> To download SQL Server, see [Install SQL Server](../database-engine/install-windows/install-sql-server.md).

## Recommended tools

The following tools provide a graphical user interface (GUI).

| Tool | Description | Operating system |
|:--|:--|:--|
| [**![ADS image](../tools/media/overview-sql-tools/azure-data-studio.svg)</br></br>Azure Data Studio**](../azure-data-studio/download.md) | A light-weight editor that can run on-demand SQL queries, view and save results as text, JSON, or Excel. Edit data, organize your favorite database connections, and browse database objects in a familiar object browsing experience. | **Windows</br>macOS</br>Linux** |
| [**![SSMS image](../tools/media/overview-sql-tools/ssms.svg)</br></br>SQL Server Management Studio (SSMS)**](../ssms/download-sql-server-management-studio-ssms.md) | Manage a SQL Server instance or database with full GUI support. Access, configure, manage, administer, and develop all components of SQL Server, Azure SQL Database, and Azure Synapse Analytics. Provides a single comprehensive utility that combines a broad group of graphical tools with a number of rich script editors to provide access to SQL for developers and database administrators of all skill levels. | **Windows** |
| [**![SSDT image](../tools/media/overview-sql-tools/ssdt.svg)</br>SQL Server Data Tools (SSDT)**](../ssdt/download-sql-server-data-tools-ssdt.md) | A modern development tool for building SQL Server relational databases, Azure SQL databases, Analysis Services (AS) data models, Integration Services (IS) packages, and Reporting Services (RS) reports. With SSDT, you can design and deploy any SQL Server content type with the same ease as you would develop an application in **[Visual Studio](https://visualstudio.microsoft.com/downloads/)**. | **Windows** |
| [**![VS Code image](../tools/media/overview-sql-tools/visual-studio-code.svg)</br></br>Visual Studio Code**](https://code.visualstudio.com/) | The **[mssql extension](https://marketplace.visualstudio.com/items?itemName=ms-mssql.mssql)** for Visual Studio Code is the official SQL Server extension that supports connections to SQL Server and rich editing experience for T-SQL in Visual Studio Code. Write T-SQL scripts in a light-weight editor. | **Windows</br>macOS</br>Linux** |

## Command-line tools

The tools below are the main command-line tools.

| Tool | Description | Operating system |
|:--|:--|:--|
|[**bcp**](bcp-utility.md)|The **b**ulk **c**opy **p**rogram utility (**bcp**) bulk copies data between an instance of [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] and a data file in a user-specified format.| **Windows</br>macOS</br>Linux** |
|[**mssql-cli (preview)**](mssql-cli.md)|**mssql-cli** is an interactive command-line tool for querying SQL Server. Also, query SQL Server with a command-line tool that features IntelliSense, syntax high-lighting, and more. | **Windows</br>macOS</br>Linux** |
|[**mssql-conf**](../linux/sql-server-linux-configure-mssql-conf.md) | **mssql-conf** configures SQL Server running on Linux. | **Linux** |
|[**mssql-scripter (preview)**](https://github.com/Microsoft/mssql-scripter) | **mssql-scripter** is a multi-platform command-line experience for scripting SQL Server databases. | **Windows</br>macOS</br>Linux** |
| [**sqlcmd**](sqlcmd-utility.md) |**sqlcmd** utility lets you enter Transact-SQL statements, system procedures, and script files at the command prompt. | **Windows</br>macOS</br>Linux** |
| [**sqlpackage**](sqlpackage/sqlpackage.md) |**sqlpackage** is a command-line utility that automates several database development tasks. |**Windows</br>macOS</br>Linux** |
|[**SQL Server PowerShell**](../powershell/sql-server-powershell.md)| **SQL Server PowerShell** provides cmdlets for working with SQL. | **Windows</br>macOS</br>Linux** |

## Migration and other tools

These tools are used to migrate, configure, and provide other features for SQL databases.

| Tool | Description |
|:--|:--|
| **[Configuration Manager](../tools/configuration-manager/sql-server-configuration-manager-help.md)** | Use SQL Server Configuration Manager to configure SQL Server services and configure network connectivity. Configuration Manager runs on Windows|
| **[Database Experimentation Assistant](../dea/database-experimentation-assistant-overview.md)** | Use Database Experimentation Assistant to evaluate a targeted version of SQL for a given workload. |
| **[Data Migration Assistant](../dma/dma-overview.md)** | The Data Migration Assistant tool helps you upgrade to a modern data platform by detecting compatibility issues that can impact database functionality in your new version of SQL Server or Azure SQL Database. |
| **[Distributed Replay](./distributed-replay/install-distributed-replay.md)** | Use the Distributed Replay feature to help you assess the impact of future SQL Server upgrades. Also use Distributed Replay to help assess the impact of hardware and operating system upgrades, and SQL Server tuning. |
| **[ssbdiagnose](../tools/ssbdiagnose/ssbdiagnose-utility-service-broker.md)** | The ssbdiagnose utility reports issues in Service Broker conversations or the configuration of Service Broker services. |
| **[SQL Server Migration Assistant](../ssma/sql-server-migration-assistant.md)** | Use SQL Server Migration Assistant to automate database migration to SQL Server from Microsoft Access, DB2, MySQL, Oracle, and Sybase.|

If you're looking for additional tools that aren't mentioned on this page, see [SQL Command Prompt Utilities](command-prompt-utility-reference-database-engine.md) and [Download SQL Server extended features and tools](download-sql-feature-packs.md)