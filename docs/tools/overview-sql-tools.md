---
title: "SQL Tools and Utilities for SQL Server, Azure SQL Database, and Azure SQL Data Warehouse | Microsoft Docs"
ms.custom: ""
ms.date: "11/19/2018"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: tools-other
ms.topic: conceptual
ms.assetid: 
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017"
---
# SQL Tools and Utilities for SQL Server, Azure SQL Database, and Azure SQL Data Warehouse
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

To manage (query, monitor, etc.) your database you need a tool. While your databases can be running in the cloud, on Windows, or on [Linux](../linux/sql-server-linux-overview.md), your tool doesn't need to run on the same platform as the database. 

There are many database tools available, so this article provides descriptions and pointers to some of the available tools for working with your SQL databases. If you need help deciding which tool you need, see [Which tool should I use?](#which-tool-should-i-choose).


## GUI tools to manage databases  

The following are the main graphical user interface (GUI) tools:

| Tool | Description | Runs on |
|:--|:--|:--|
| [[!INCLUDE[name-sos](../includes/name-sos.md)]](../sql-operations-studio/download.md) | [!INCLUDE[name-sos](../includes/name-sos-short.md)] is a free, light-weight tool, for managing databases wherever they're running. This preview release provides database management features, including an extended Transact-SQL editor and customizable insights into the operational state of your databases. | **[!INCLUDE[name-sos](../includes/name-sos-short.md)] runs on Windows, macOS, and Linux**.|
| [SQL Server Management Studio (SSMS)](../ssms/download-sql-server-management-studio-ssms.md) | Use SQL Server Management Studio (SSMS) to query, design, and manage your SQL Server, Azure SQL Database, and Azure SQL Data Warehouse. | **SSMS runs on Windows**.|
| [SQL Server Data Tools (SSDT)](../ssdt/download-sql-server-data-tools-ssdt.md) | Turn Visual Studio into a powerful development environment for SQL Server, Azure SQL Database, and Azure SQL Data Warehouse.| **SSDT runs on Windows**.|
| [Visual Studio Code](https://code.visualstudio.com/)| After installing Visual Studio Code, install the [mssql extension](https://marketplace.visualstudio.com/items?itemName=ms-mssql.mssql) for developing Microsoft SQL Server, Azure SQL Database, and SQL Data Warehouse.| **Visual Studio Code runs on Windows, macOS, and Linux**.|


## Command line tools to manage databases

The following are the main command-line tools:

| Tool | Description | Runs on |
|:--|:--|:--|
|[**mssql-cli (preview)**](mssql-cli.md)|**mssql-cli** is an interactive command-line tool for querying SQL Server. | Windows, macOS, and Linux|
| [**sqlpackage**](sqlpackage.md) |**sqlpackage** is a command-line utility that automates several database development tasks. macOS and Linux versions of sqlpackage are currently in preview. | Windows, macOS, and Linux|
|[**SQL Server PowerShell**](../powershell/sql-server-powershell.md)| **SQL Server PowerShell** provides cmdlets for working with SQL| Windows, macOS, and Linux|
| [**sqlcmd**](sqlcmd-utility.md) |**sqlcmd** utility lets you enter Transact-SQL statements, system procedures, and script files at the command prompt. | Windows, macOS, and Linux|
|[**bcp**](https://docs.microsoft.com/sql/tools/bcp-utility?view=sql-server-2014)|The **b**ulk **c**opy **p**rogram utility (**bcp**) bulk copies data between an instance of [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] and a data file in a user-specified format.|Windows, macOS, and Linux|
|[**mssql-scripter (preview)**](https://github.com/Microsoft/mssql-scripter)|**mssql-scripter** is a multi-platform command line experience for scripting SQL Server databases|Windows, macOS, and Linux|
|[**mssql-conf**](../linux/sql-server-linux-configure-mssql-conf.md)|**mssql-conf** configures SQL Server running on Linux.|Linux|



## Which tool should I choose?

- Do you want to manage a SQL Server instance or database, in a light-weight editor on Windows, Linux or Mac? Choose [[!INCLUDE[name-sos](../includes/name-sos.md)]](../sql-operations-studio/download.md)
- Do you want to manage a SQL Server instance or database on Windows with full GUI support? Choose [SQL Server Management Studio (SSMS)](../ssms/download-sql-server-management-studio-ssms.md)
- Do you want to create or maintain database code, including compile time validation, refactoring and designer support on Windows? Choose [SQL Server Data Tools (SSDT)](../ssdt/download-sql-server-data-tools-ssdt.md)
- Do you want to query SQL Server with a command-line tool that features IntelliSense, syntax high-lighting, and more? Choose [mssql-cli](mssql-cli.md)
- Do you want to write T-SQL scripts in a light-weight editor on Windows, Linux or Mac? Choose [Visual Studio Code](https://code.visualstudio.com/) and the [mssql extension](https://marketplace.visualstudio.com/items?itemName=ms-mssql.mssql)



## Additional tools

| Tool | Description |
|:--|:--|
| [Configuration Manager](../tools/configuration-manager/sql-server-configuration-manager-help.md) | Use SQL Server Configuration Manager to configure SQL Server services and configure network connectivity. Configuration Manager runs on Windows|
| [SQL Server Migration Assistant](../ssma/sql-server-migration-assistant.md) | Use SQL Server Migration Assistant to automate database migration to SQL Server from Microsoft Access, DB2, MySQL, Oracle, and Sybase.|
| [Database Experimentation Assistant](../dea/database-experimentation-assistant-overview.md) | Use Database Experimentation Assistant to evaluate a targeted version of SQL for a given workload. |
| [Distributed Replay](../tools/distributed-replay/install-distributed-replay-overview.md) | Use the Distributed Replay feature to help you assess the impact of future SQL Server upgrades. Also use Distributed Replay to help assess the impact of hardware and operating system upgrades, and SQL Server tuning. |
| [ssbdiagnose](../tools/ssbdiagnose/ssbdiagnose-utility-service-broker.md) | The ssbdiagnose utility reports issues in Service Broker conversations or the configuration of Service Broker services. |

If you're looking for additional tools that are not mentioned on this page, see [SQL Command Prompt Utilities](command-prompt-utility-reference-database-engine.md).

