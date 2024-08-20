---
title: SQL Server Data Tools
description: View resources on database development tasks that you can accomplish with SQL Server Data Tools, such as designing tables and creating feature extensions.
author: markingmyname
ms.author: maghan
ms.date: 08/19/2024
ms.service: sql
ms.subservice: ssdt
ms.topic: conceptual
f1_keywords:
  - "sql.data.tools.errortask.generichelp"
  - "sql.data.tools.sqlserverobjectexplorer"
---

# SQL Server Data Tools

**SQL Server Data Tools (SSDT)** is a set of development tools for building SQL Server databases, Azure SQL databases, Analysis Services (AS) data models, Integration Services (IS) packages, and Reporting Services (RS) reports. SSDT allows you to design and deploy SQL objects with the same project concept as other application development tools. The **SQL projects** capability extends to CI/CD pipelines, enabling you to automate the build and deployment of your database projects with the [SqlPackage CLI](../tools/sqlpackage/sqlpackage.md).

The core of SQL Server Data Tools functionality is available as a workload component with Visual Studio, enabling database development. Additional functionality for developing AS, IS, and RS projects is available as Visual Studio extensions for installation in addition to the SSDT workload. The Visual Studio extensions are available from the Visual Studio Marketplace and more information on installing SSDT can be found in [Download SQL Server Data Tools](download-sql-server-data-tools-ssdt.md).

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

The following topics and sections discuss how SSDT can help you develop a database. How-to topics are included to help guide you through completing tasks for your database project. These tasks, written like a tutorial and completed in order, use Northwind Traders, a fictitious company that imports and exports specialty foods.

| Topics/Section | Description |
| --- | --- |
| [Project-Oriented Offline Database Development](../ssdt/project-oriented-offline-database-development.md) | Topics in this section describe SQL Server Data Tools features for authoring, building, debugging, and publishing a database project. |
| [Project-Oriented Database Development using Command-Line Tools](../ssdt/project-oriented-database-development-using-command-line-tools.md) | Topics in this section describe command-line tools that enable several project-oriented database development scenarios. |
| [Compare and Synchronize Data in One or More Tables with Data in a Reference Database](../ssdt/compare-and-synchronize-data-in-tables-with-data-in-reference-database.md) | Discusses how to compare data in a source database and a target database, specify which values should match, and then either update the target to synchronize the databases or export the update script to the Transact-SQL editor or a file. |
| [Use Transact-SQL Editor to Edit and Execute Scripts](../ssdt/use-transact-sql-editor-to-edit-and-execute-scripts.md) | Topics in this section describe how to use the Transact-SQL Editor, which provides a rich editing and debugging experience when working with scripts. |
| [Manage Tables, Relationships, and Fix Errors](../ssdt/manage-tables-relationships-and-fix-errors.md) | Topics in this section describe how to:<br /><br />- Use the Table Designer to design tables and manage table relationships.<br />- Fix common syntax or semantic errors. |
| [Verifying Database Code by Using SQL Server Unit Tests](../ssdt/verifying-database-code-by-using-sql-server-unit-tests.md) | Discusses how you can use SQL Server unit tests to establish a baseline state for your database and then to verify any subsequent changes that you make to database objects. |
| [Extending the Database Features](../ssdt/extending-the-database-features.md) | You can create feature extensions that extend features such as unit testing and database code analysis. |
| [Required Permissions for SQL Server Data Tools](../ssdt/required-permissions-for-sql-server-data-tools.md) | Discusses required access permission to use SQL Server Data Tools. |
| [DAC Framework Compatibility](../ssdt/dac-framework-compatibility.md) | Describes compatibility issues with DAC framework. |
