---
title: SQL Projects Tools
description: Compare SQL project tools available in Visual Studio Code, SSMS, Visual Studio, and the command line.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: drskwier
ms.date: 07/14/2026
ms.service: sql
ms.subservice: sql-database-projects
ms.topic: overview
ms.collection:
  - data-tools
---

# SQL projects tools

Tooling for SQL projects is available in several development environments and command line interfaces. The primary tools for SQL projects are the **SqlPackage** command line utility, **SQL Server Data Tools** (SSDT) in Visual Studio, and the **SQL Database Projects extension** for Visual Studio Code.

Tools included in this article:

- [Graphical tools](#graphical-tools)
  - [SQL Database Projects extension](../visual-studio-code-extensions/sql-database-projects/sql-database-projects-extension.md)
  - [Database DevOps in SQL Server Management Studio](/ssms/database-devops)
  - [SQL Server Data Tools](../../ssdt/sql-server-data-tools.md)
- [Command line tools](#command-line-tools)
  - [SqlPackage](../sqlpackage/sqlpackage.md)

## Graphical tools

These tools provide a graphical interface for SQL projects, a Transact-SQL (T-SQL) editor, and a build and publish process.

[SQL Database Projects extension](../visual-studio-code-extensions/sql-database-projects/sql-database-projects-extension.md) is an extension for **Visual Studio Code**. This extension provides a graphical interface for SQL projects, a T-SQL editor, and a build and publish process.

[Database DevOps](/ssms/database-devops) in **SQL Server Management Studio (SSMS)** provides a graphical interface for SQL projects, a T-SQL editor, and a build and publish process.

[SQL Server Data Tools](../../ssdt/sql-server-data-tools.md) (SSDT) is a **Visual Studio** component that provides a graphical interface for SQL projects. SSDT provides a visual designer for tables, a T-SQL editor, and a build and publish process.

### Feature set comparison

| Feature | Visual Studio Code | SSMS | SSDT (VS2022-2026) | SDK-style SSDT (preview) (VS2022) |
| --- | --- | --- | --- | --- |
| [Create new empty project](get-started.md) | Yes | Yes | Yes | Yes |
| [Create new project from existing database](tutorials/start-from-existing-database.md) | Yes | Yes | Yes | Yes |
| Open existing Microsoft.Build.Sql projects | Yes | Yes | No | Yes |
| Open original-style (SSDT) projects | Yes | No | Yes | No |
| Solution management and operations | No | Yes | Yes | Yes |
| Project run build | Yes | Yes | Yes | Yes |
| Publish project to existing server | Yes | Yes | Yes | Yes |
| Publish project to a local development instance | Yes<sup>1</sup> | Yes<sup>2</sup> | Yes<sup>3</sup> | Yes<sup>3</sup> |
| Publish options/properties | Yes | Yes | Yes | Yes |
| [Target platform](concepts/target-platform.md) can be updated | Yes | Yes | Yes | Yes |
| [SQLCMD variables](concepts/sqlcmd-variables.md) | Yes | Yes | Yes | Yes |
| [Project references](concepts/project-references.md) | Yes | Yes | Yes | Yes |
| [DACPAC references](concepts/database-references.md) | Yes | Yes | Yes | Yes |
| [Package references](concepts/package-references.md) | Yes | Yes | No | No |
| Publish profile creation | Yes | No | Yes | Yes |
| SQL files can be added by placing in project folder | Yes | Yes | No | Yes |
| SQL files can be excluded from build | Yes | Yes | Yes | No |
| [Pre-deployment and post-deployment scripts](concepts/pre-post-deployment-scripts.md) | Yes | Yes | Yes | Yes |
| New object templates | Yes<sup>4</sup> | Yes | Yes | Yes<sup>4</sup> |
| Project files can be organized into folders | Yes | Yes | Yes | Yes |
| [Schema comparison](concepts/schema-comparison.md) project to database | Yes | Yes | Yes | Yes |
| [Schema comparison](concepts/schema-comparison.md) database to project | Yes | Yes | Yes | No |
| Graphical table designer | No | No | Yes | Yes |
| [Code analysis](concepts/sql-code-analysis/sql-code-analysis.md) - enable/disable rules GUI | Yes | Yes | Yes | No |
| Project properties - build output settings | No | No | Yes | Yes |
| Project properties - database settings GUI | No | No | Yes | No |
| Project run [code analysis](concepts/sql-code-analysis/sql-code-analysis.md) | Yes | Yes | Yes | No |
| Object renaming and refactoring | Yes | No | Yes | No |
| Intellisense provided in database files from project model | Yes | No | Yes | No |

<sup>1</sup> Local development instance is a SQL Server container.  
<sup>2</sup> Any pre-installed Microsoft SQL database can be used as a local development instance.  
<sup>3</sup> Local development instance is a SQL Server LocalDB instance.  
<sup>4</sup> Limited subset of templates available.

## Command line tools

[SqlPackage](../sqlpackage/sqlpackage.md) is the primary command line utility for the DacFx library, enabling automation of the database development tasks such as deploying a `.dacpac` to a database or extracting the objects of a database to a SQL project or `.dacpac`.

Custom console applications can be built using the DacFx .NET library to automate database development tasks. The [Microsoft.SqlServer.Dac](/dotnet/api/microsoft.sqlserver.dac) namespace contains classes for creating, deploying, and extracting database objects and is foundational to the rest of the DacFx library.

CI/CD pipelines can be built with command line execution or with tasks specific to `.dacpac` and SQL projects deployment. The [GitHub sql-action](https://github.com/azure/sql-action) and [SqlAzureDacpacDeployment in Azure DevOps](/azure/devops/pipelines/tasks/reference/sql-azure-dacpac-deployment-v1) are examples of tasks that use SqlPackage underneath a management layer to facilitate deploying database changes.

### Conversion tools

The process of converting an [existing SQL project to an SDK-style project](howto/convert-original-sql-project.md) is done by manually editing the `.sqlproj` file to include the new SDK-style project format. Before beginning the process, it's recommended to both back up the project file and archive a `.dacpac` of the project. By comparing a "before" and "after" `.dacpac` built from the project, you can ensure that the conversion process has correctly completed.

### Project/solution management

Multiple SQL projects (and other projects) can be logically grouped together in a solution file. The solution file is a container for one or more projects and is used to manage the projects as a group, including the build action. Large solutions can be broken down into smaller solutions to improve performance and manageability, or dynamically generated for the appropriate task at hand. The [slngen solution file generator](https://github.com/microsoft/slngen) is available for Microsoft.Build.Sql projects and can be used to create a solution file for a set of projects programmatically and on-demand.

## Roadmap

A quarterly roadmap for SQL projects related capabilities is available at <https://aka.ms/sqlprojects-roadmap>. Customer feedback heavily influences the roadmap, which incorporates both modernizing the capabilities of Microsoft.Build.Sql projects and improvements to the tooling surfaces associated with SQL projects.

## Third-party tools

There are third-party tools available that provide functionality related to SQL projects and database deployment. Some tools are open source, such as [dbatools](https://dbatools.io/Publish-DbaDacPackage).

Developers have shared their projects utilizing extensibility points around SQL projects, including [code analysis](concepts/sql-code-analysis/sql-code-analysis.md) rules and customizing deployment plans. Some of these projects are:

- <https://github.com/tcartwright/SqlServer.Rules>
- <https://github.com/davebally/TSQL-Smells>
- <https://github.com/ErikEJ/SqlServer.Rules>
- <https://github.com/GoEddie/DeploymentContributorFilterer>

## Related content

- [Project-Oriented Offline Database Development](../../ssdt/project-oriented-offline-database-development.md)
- [SQL Database Projects extension](../visual-studio-code-extensions/sql-database-projects/sql-database-projects-extension.md)
- [SqlPackage](../sqlpackage/sqlpackage.md)
- [GitHub sql-action](https://github.com/azure/sql-action)
- [Azure DevOps SQL deployments](/azure/devops/pipelines/targets/azure-sqldb)
- [Data-tier applications (DAC)](../../relational-databases/data-tier-applications/data-tier-applications.md)
- [DacFx feedback repository](https://github.com/microsoft/dacfx)
- [Get started with SQL database projects](get-started.md)
- [Tutorial: Create and deploy a SQL project](tutorials/create-deploy-sql-project.md)
