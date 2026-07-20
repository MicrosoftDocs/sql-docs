---
title: SQL Tools Overview
description: SQL query and management tools for SQL Server, Azure SQL (Azure SQL database, Azure SQL managed instance, SQL virtual machines), and Azure Synapse Analytics.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: erinstellato, iqrashaikh, mbarickman, drskwier, roblescarlos
ms.date: 03/09/2026
ms.service: sql
ms.subservice: tools-other
ms.topic: overview
ms.collection:
  - data-tools
ms.custom:
  - ignite-2025
ai-usage: ai-assisted
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =fabric-sqldb"
---

# SQL tools overview

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Azure Synapse Analytics PDW FabricSQLDB](../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

To manage your database, you need a tool. Whether your databases run in the cloud, on Windows, on macOS, or on [Linux](../linux/sql-server-linux-overview.md), your tool doesn't need to run on the same platform as the database.

## Free tools for your business case

[!INCLUDE [msconame-md](../includes/msconame-md.md)] provides the following free tools and extensions to work with our [[!INCLUDE [ssdenoversion-md](../includes/ssdenoversion-md.md)]](../database-engine/sql-database-engine.md) products, based on your business role and function.

[!INCLUDE [tools](../includes/tools.md)]

## Description and use case examples

The following table lists available tools and extensions.

| Tool | Description | Operating system | Feedback |
| --- | --- | --- | --- |
| **Graphical&nbsp;tools** | | | 
| <a id="ssms"></a> **[SQL Server Management Studio (SSMS)](/ssms/install/install)** | Manage SQL Server and Azure SQL databases with full GUI support. Access, configure, manage, administer, and develop all components of the [SQL Database Engine](../database-engine/sql-database-engine.md) on-premises and the cloud, including Azure Synapse Analytics and SQL database for Microsoft Fabric. SSMS is a comprehensive application that combines a broad group of graphical tools and a rich script editor to provide access to SQL for database administrators and developers of all skill levels. | Windows only | [Feedback](https://aka.ms/ssms-feedback) |
| <a id="ssdt"></a> **[SQL Server Data Tools (SSDT)](../ssdt/download-sql-server-data-tools-ssdt.md)** | A modern development tool for building SQL Server relational databases, Azure SQL databases, Analysis Services (AS) data models, Integration Services (IS) packages, and Reporting Services (RS) reports. With SQL Server Data tools (SSDT), you can design and deploy any SQL Server content type with the same ease as you would develop an application in **[Visual Studio](https://visualstudio.microsoft.com/downloads/)**. | Windows only | [Feedback](https://aka.ms/vs-feedback) |
| <a id="mssql"></a> **[MSSQL extension for Visual Studio Code](visual-studio-code-extensions/mssql/mssql-extension-visual-studio-code.md)** | The official SQL Server extension that supports connections to SQL Server and Azure SQL, and a rich editing experience for Transact-SQL (T-SQL). Write T-SQL scripts in a lightweight editor. | Windows, macOS, Linux | [Feedback](https://github.com/microsoft/vscode-mssql) |
| <a id="projects"></a> **[SQL Database Projects extension for Visual Studio Code](visual-studio-code-extensions/sql-database-projects/sql-database-projects-extension.md)** | Manage and develop databases as projects in source control in Visual Studio Code. The SQL Database Projects extension uses the [DacFx (Data-Tier Application Framework) package](/sql/tools/sqlpackage/sqlpackage) to build and publish database projects, compare schemas, script changes, and extract or deploy `.dacpac` files. | Windows, macOS, Linux | [Feedback](https://github.com/microsoft/vscode-mssql) |
| <a id="ads"></a> **[Azure Data Studio](/azure-data-studio/download-azure-data-studio)** | **Azure Data Studio is retiring on February 28, 2026.** | Windows, macOS, Linux | |
| **Command-line&nbsp;utilities** | | | |
| <a id="bcp"></a> **[bcp utility](bcp-utility.md)** | The **b**ulk **c**opy **p**rogram utility (**bcp**) bulk copies data between an instance of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] and a data file in a user-specified format. | Windows, macOS, Linux | |
| <a id="mssql-conf"></a> **[mssql-conf](../linux/sql-server-linux-configure-mssql-conf.md)** | **mssql-conf** configures SQL Server running on Linux or Linux containers. | Linux only | |
| <a id="sqlcmd"></a> **[sqlcmd utility](sqlcmd/sqlcmd-utility.md)** | **sqlcmd** lets you enter Transact-SQL statements, system procedures, and script files at the command prompt. With sqlcmd (Go), you can also deploy Linux containers for development purposes. | Windows, macOS, Linux | [Feedback](https://github.com/microsoft/go-sqlcmd) |
| <a id="sqlpackage"></a> **[SqlPackage](sqlpackage/sqlpackage.md)** | **sqlpackage** is a command-line utility that automates several database development tasks. | Windows, macOS, Linux | [Feedback](https://github.com/microsoft/dacfx) |
| <a id="powershell"></a> **[SQL Server PowerShell](/powershell/sql-server/sql-server-powershell)** | **SQL Server PowerShell** provides cmdlets for working with SQL. | Windows, macOS, Linux | [Feedback](https://github.com/microsoft/SqlServerPSModule) |

## Migration, configuration, and other tools

The following table lists tools that are used to migrate, configure, and provide other features for SQL databases.

These tools are available for Windows only.

| Tool | Description |
| --- | --- |
| <a id="sscm"></a> **[SQL Server Configuration Manager](configuration-manager/sql-server-configuration-manager-help.md)** | Use SQL Server Configuration Manager to configure SQL Server services and configure network connectivity. |
| <a id="dreplay"></a> **[Distributed Replay](distributed-replay/install-distributed-replay.md)** <sup>1</sup> | Use the Distributed Replay feature to help you assess the impact of future SQL Server upgrades. Also use Distributed Replay to help assess the impact of hardware and operating system upgrades, and SQL Server tuning. |
| <a id="ssbdiagnose"></a> **[ssbdiagnose](ssbdiagnose/ssbdiagnose-utility-service-broker.md)** | **ssbdiagnose** reports issues in Service Broker conversations or the configuration of Service Broker services. |
| <a id="ssma"></a> **[SQL Server Migration Assistant (SSMA)](../ssma/sql-server-migration-assistant.md)** | Use SQL Server Migration Assistant to automate database migration to SQL Server and Azure SQL from Microsoft Access, Db2, MySQL, Oracle, and Sybase. |

<sup>1</sup> Distributed Replay is supported on [!INCLUDE [sssql16-md](../includes/sssql16-md.md)] through [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] only.

## Product roadmaps and feedback

- [Feedback: SQL database in Microsoft Fabric](https://community.fabric.microsoft.com/t5/Fabric-Ideas/idb-p/fbc_ideas/label-name/databases%20%7C%20sql%20database)
- [Feedback: SQL Server Management Studio](https://aka.ms/ssms-feedback)
- [Feedback: SQL Server](https://feedback.azure.com/d365community/forum/04fe6ee0-3b25-ec11-b6e6-000d3a4f0da0)
- [Feedback: SqlPackage and DacFx](https://github.com/microsoft/dacfx)
- [Feedback: sql-action GitHub action](https://github.com/azure/sql-action)
- [Roadmap: MSSQL extension in Visual Studio Code](https://github.com/microsoft/vscode-mssql/wiki/roadmap)
- [Roadmap: SQL Server Management Studio](/ssms/roadmap)
- [What's happening with Azure Data Studio](whats-happening-azure-data-studio.md)

## Additional tools

If you're looking for other tools that aren't mentioned in this article, see:

- [SQL command-line utilities (Database Engine)](command-prompt-utility-reference-database-engine.md)
- [Download SQL Server extended features and tools](download-sql-feature-packs.md)

## Related content

- [SQL Server](/sql/sql-server/)
- [Azure SQL Database](/azure/azure-sql/database/)
- [Azure Database for PostgreSQL](/azure/postgresql/)
- [Azure Database for MySQL](/azure/mysql/)
- [Azure Cosmos DB](/azure/cosmos-db/introduction)
- [SQL database in Microsoft Fabric](/fabric/database/sql/overview)
