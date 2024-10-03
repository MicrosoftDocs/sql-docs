---
title: SQL tools overview
description: SQL query and management tools for SQL Server, Azure SQL (Azure SQL database, Azure SQL managed instance, SQL virtual machines), and Azure Synapse Analytics.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 09/19/2024
ms.service: sql
ms.subservice: tools-other
ms.topic: overview
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017"
---

# SQL tools overview

[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW](../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

To manage your database, you need a tool. Whether your databases run in the cloud, on Windows, on macOS, or on [Linux](../linux/sql-server-linux-overview.md), your tool doesn't need to run on the same platform as the database.

## Recommended tools

The following tools provide a graphical user interface (GUI).

You can view the links to the different SQL tools in the following tables.

| Tool | Description | Operating system |
| :-- | :-- | :-- |
| [:::image type="content" source="media/overview-sql-tools/azure-data-studio.svg" alt-text="Screenshot of Azure Data Studio.":::<br />**Azure Data Studio](/azure-data-studio/download-azure-data-studio)** | A light-weight editor that can run on-demand SQL queries, view and save results as text, JSON, or Excel. Edit data, organize your favorite database connections, and browse database objects in a familiar object browsing experience. | **Windows**<br />**macOS**<br />**Linux** |
| [:::image type="icon" source="media/overview-sql-tools/ssms.svg" border="false":::<br />**SQL Server Management Studio (SSMS)**](../ssms/download-sql-server-management-studio-ssms.md) | Manage a SQL Server instance or database with full GUI support. Access, configure, manage, administer, and develop all components of SQL Server, Azure SQL Database, and Azure Synapse Analytics. Provides a single comprehensive utility that combines a broad group of graphical tools with many rich script editors to provide access to SQL for developers and database administrators of all skill levels. | **Windows** |
| [:::image type="icon" source="media/overview-sql-tools/ssdt.svg" border="false":::<br />**SQL Server Data Tools (SSDT)**](../ssdt/download-sql-server-data-tools-ssdt.md) | A modern development tool for building SQL Server relational databases, Azure SQL databases, Analysis Services (AS) data models, Integration Services (IS) packages, and Reporting Services (RS) reports. With SQL Server Data tools (SSDT), you can design and deploy any SQL Server content type with the same ease as you would develop an application in **[Visual Studio](https://visualstudio.microsoft.com/downloads/)**. | **Windows** |
| [:::image type="icon" source="media/overview-sql-tools/visual-studio-code.svg" border="false":::<br />**Visual Studio Code**](https://code.visualstudio.com/) | The **[mssql extension](https://marketplace.visualstudio.com/items?itemName=ms-mssql.mssql)** for Visual Studio Code is the official SQL Server extension that supports connections to SQL Server and rich editing experience for T-SQL in Visual Studio Code. Write T-SQL scripts in a light-weight editor. | **Windows**<br />**macOS**<br />**Linux** |

## Command-line tools

The following tools are the main command-line tools.

| Tool | Description | Operating system |
| :-- | :-- | :-- |
| [**bcp**](bcp-utility.md) | The **b**ulk **c**opy **p**rogram utility (**bcp**) bulk copies data between an instance of [!INCLUDE [msCoName](../includes/msconame-md.md)] [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] and a data file in a user-specified format. | **Windows<br />macOS<br />Linux** |
| [**mssql-conf**](../linux/sql-server-linux-configure-mssql-conf.md) | **mssql-conf** configures SQL Server running on Linux. | **Linux** |
| [**sqlcmd**](sqlcmd/sqlcmd-utility.md) | The **sqlcmd** utility lets you enter Transact-SQL statements, system procedures, and script files at the command prompt. | **Windows<br />macOS<br />Linux** |
| [**sqlpackage**](sqlpackage/sqlpackage.md) | **sqlpackage** is a command-line utility that automates several database development tasks. | **Windows<br />macOS<br />Linux** |
| [**SQL Server PowerShell**](/powershell/sql-server/sql-server-powershell) | **SQL Server PowerShell** provides cmdlets for working with SQL. | **Windows<br />macOS<br />Linux** |

## Migration and other tools

These tools are used to migrate, configure, and provide other features for SQL databases.

| Tool | Description |
| :-- | :-- |
| **[Configuration Manager](../tools/configuration-manager/sql-server-configuration-manager-help.md)** | Use SQL Server Configuration Manager to configure SQL Server services and configure network connectivity. Configuration Manager runs on Windows |
| **[Database Experimentation Assistant](../dea/database-experimentation-assistant-overview.md)** | Use Database Experimentation Assistant to evaluate a targeted version of SQL for a given workload. |
| **[Data Migration Assistant](../dma/dma-overview.md)** | The Data Migration Assistant tool helps you upgrade to a modern data platform by detecting compatibility issues that can affect database functionality in your new version of SQL Server or Azure SQL Database. |
| **[Distributed Replay](distributed-replay/install-distributed-replay.md)** | Use the Distributed Replay feature to help you assess the impact of future SQL Server upgrades. Also use Distributed Replay to help assess the impact of hardware and operating system upgrades, and SQL Server tuning. |
| **[ssbdiagnose](../tools/ssbdiagnose/ssbdiagnose-utility-service-broker.md)** | The ssbdiagnose utility reports issues in Service Broker conversations or the configuration of Service Broker services. |
| **[SQL Server Migration Assistant](../ssma/sql-server-migration-assistant.md)** | Use SQL Server Migration Assistant to automate database migration to SQL Server from Microsoft Access, DB2, MySQL, Oracle, and Sybase. |

If you're looking for other tools that aren't mentioned on this page, see [SQL Command Prompt Utilities](command-prompt-utility-reference-database-engine.md) and [Download SQL Server extended features and tools](download-sql-feature-packs.md)

## Related content

- [SQL Server](/sql/sql-server/)
- [Azure SQL Database](/azure/azure-sql/database/)
- [Azure Database for PostgreSQL](/azure/postgresql/)
- [Azure Database for MySQL](/azure/mysql/)
- [Azure Cosmos DB](/azure/cosmos-db/introduction)
