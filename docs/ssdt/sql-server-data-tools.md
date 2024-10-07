---
title: SQL Server Data Tools
description: View resources on database development tasks that you can accomplish with SQL Server Data Tools, such as designing tables and creating feature extensions.
author: markingmyname
ms.author: maghan
ms.date: 09/10/2024
ms.service: sql
ms.subservice: ssdt
ms.topic: conceptual
f1_keywords:
  - "sql.data.tools.errortask.generichelp"
  - "sql.data.tools.sqlserverobjectexplorer"
  - "sql.data.tools.datatoolsoperations"
---

# SQL Server Data Tools

**SQL Server Data Tools (SSDT)** is a set of development tools in Visual Studio with focus on building SQL Server databases and Azure SQL databases. SSDT can be extended to Analysis Services (AS) data models, Integration Services (IS) packages, and Reporting Services (RS) reports with their corresponding extensions. SSDT allows you to design and deploy SQL objects with the same project concept as other application development tools. The **SQL projects** capability extends to CI/CD pipelines, enabling you to automate the build and deployment of your database projects with the [SqlPackage CLI](../tools/sqlpackage/sqlpackage.md).

:::image type="content" source="media/sql-server-data-tools/install-layout.png" alt-text="Screenshot of graphic with SQL Server Data Tools component and three extensions.":::

The core of SQL Server Data Tools functionality is available as a workload component with Visual Studio. The Visual Studio extensions are available from the Visual Studio Marketplace and more information on installing SSDT can be found in [Download SQL Server Data Tools](download-sql-server-data-tools-ssdt.md).

> [!NOTE]
> SDK-style SQL projects in Visual Studio are available as part of the **SQL Server Data Tools, SDK-style (preview)** feature for Visual Studio 2022, separate from the original SSDT. The SDK-style project format is based on the new SDK-style projects introduced in .NET Core and is the format used by the SQL Database Projects extension for Azure Data Studio and VS Code. For more information, see [SQL Server Data Tools, SDK-style (preview)](sql-server-data-tools-sdk-style.md).

## Release notes

The latest release notes for SQL Server Data Tools with Visual Studio 2022 can be found in the following locations:

- SQL Server Data Tools (SSDT) release notes are listed with the [release notes for Visual Studio 2022](/visualstudio/releases/2022/release-notes)
- Analysis Services (SSAS) extension release notes are listed on the [extension marketplace](https://marketplace.visualstudio.com/items?itemName=ProBITools.MicrosoftAnalysisServicesModelingProjects2022)
- Integration Services (SSIS) extension release notes are listed on the [extension marketplace](https://marketplace.visualstudio.com/items?itemName=SSIS.MicrosoftDataToolsIntegrationServices)
- Reporting Services (SSRS) extension release notes are listed on the [extension marketplace](https://marketplace.visualstudio.com/items?itemName=ProBITools.MicrosoftReportProjectsforVisualStudio2022)

The release notes for SQL Server Data Tools with Visual Studio 2019 can be found in the following locations:

- SQL Server Data Tools (SSDT) release notes are listed with the [release notes for Visual Studio 2019](/visualstudio/releases/2019/release-notes)
- Analysis Services (SSAS) extension release notes are listed on the [extension marketplace](https://marketplace.visualstudio.com/items?itemName=ProBITools.MicrosoftAnalysisServicesModelingProjects)
- Integration Services (SSIS) extension release notes are listed on the [extension marketplace](https://marketplace.visualstudio.com/items?itemName=SSIS.SqlServerIntegrationServicesProjects)
- Reporting Services (SSRS) extension release notes are listed on the [extension marketplace](https://marketplace.visualstudio.com/items?itemName=ProBITools.MicrosoftReportProjectsforVisualStudio)

For information about SQL Server Data Tools with Visual Studio 2017, see [Previous releases of SQL Server Data Tools (SSDT and SSDT-BI)](previous-releases-of-sql-server-data-tools-ssdt-and-ssdt-bi.md).

## Core SQL Server Data Tools

SQL Server Data Tools (SSDT) transforms database development by introducing a ubiquitous, declarative model (SQL database projects) that spans all the phases of database development inside Visual Studio. SSDT Transact-SQL design capabilities can be used to build, debug, maintain, and refactor databases. You can work with a database project or directly connect to a database instance on or off-premises.

Developers can use the familiar Visual Studio environment for comprehensive database development. The suite of tools includes code navigation, IntelliSense, and language support akin to what is available for C# and Visual Basic, along with specialized validation, debugging, and declarative editing within the Transact-SQL editor. SQL Server Data Tools (SSDT) also offers a visual table designer for streamlined creation and modification of tables in database projects or connected instances. In team-based settings, version control is available for all project files, enhancing collaboration. When it's time to deploy, projects can be published across all supported SQL platforms, such as SQL Database and SQL Server.

The SQL Server Object Explorer in Visual Studio offers a view of your database objects like SQL Server Management Studio. SQL Server Object Explorer lets you do light-duty database administration and design work. You can easily create, edit, rename, and delete tables, stored procedures, types, and functions. You can also edit table data, compare schemas, or execute queries using contextual menus from the SQL Server Object Explorer.

Learn more about SQL projects and database development tasks that you can accomplish with SQL Server Data Tools in the [SQL database projects documentation](../tools/sql-database-projects/sql-database-projects.md).

## SDK-style SQL projects (preview)

Support for the Microsoft.Build.Sql project SDK is available in preview in Visual Studio as the next generation of SQL projects. The SDK-style SQL projects are based on the .NET SDK-style project format and are designed to be more flexible and extensible than the original SQL projects. The SDK-style SQL projects are recommended for new development and are available in Visual Studio 2022 as an optional component "SQL Server Data Tools, SDK-style (preview)." Learn more about the SDK-style SQL projects and Visual Studio in the [SDK-style SQL Server Data Tools documentation](sql-server-data-tools-sdk-style.md).

## Related content

- [SQL database projects](../tools/sql-database-projects/sql-database-projects.md)
- [SQL Server Analysis Services tutorials](/analysis-services/analysis-services-tutorials-ssas)
- [SQL Server Reporting Services tools](../reporting-services/tools/reporting-services-tools.md)
- [SQL Server Integration Services Projects and solutions](../integration-services/integration-services-ssis-projects-and-solutions.md)
