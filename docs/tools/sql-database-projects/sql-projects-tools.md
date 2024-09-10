---
title: "SQL projects tools"
description: "This overview reviews the tooling for SQL database projects."
author: dzsquared
ms.author: drskwier
ms.reviewer: maghan
ms.date: 09/10/2024
ms.service: sql
ms.topic: overview
---

# SQL projects tools

Tooling for SQL projects is available in several development environments and command line interfaces. The primary tools for SQL projects are the **SqlPackage** command line utility, **SQL Server Data Tools** (SSDT) in Visual Studio, and the **SQL Database Projects extension** for Azure Data Studio and Visual Studio Code. 

## Graphical tools

These tools provide a graphical interface for SQL projects, a T-SQL editor, and a build and publish process.

[SQL Server Data Tools](../../ssdt/sql-server-data-tools.md) (SSDT) is a **Visual Studio** component that provides a graphical interface for SQL projects. SSDT provides a visual designer for tables, a T-SQL editor, and a build and publish process.

[SQL Database Projects extension](https://aka.ms/azuredatastudio-sqlprojects) is an extension for **Azure Data Studio (ADS)** and **VS Code**. This extension provides a graphical interface for SQL projects, a T-SQL editor, and a build and publish process.

### Feature set comparison

| Feature | SDK-style SSDT | SSDT | ADS | VS Code |
| --- | --- | --- | --- | --- |
| [Create new empty project](get-started.md) | X | X | X | X |
| [Create new project from existing database](tutorials/start-from-existing-database.md) |  | X | X | X |
| Open existing Microsoft.Build.Sql projects | X<sup>1</sup> |  | X | X |
| Solution management and operations | X | X |  |  |
| Project run build | X | X | X | X |
| Publish project to existing server | X | X | X | X |
| Publish project to a local development instance | X<sup>2</sup> | X<sup>2</sup> | X<sup>3</sup> | X<sup>3</sup> |
| Publish options/properties | X | X | X |  |
| [Target platform](concepts/target-platform.md) can be updated | X | X | X | X |
| [SQLCMD variables](concepts/sqlcmd-variables.md) | X | X | X | X |
| [Project references](concepts/database-references.md) |  | X | X |  |
| [Dacpac references](concepts/database-references.md) |  | X | X | X |
| [Package references](concepts/package-references.md) |  |  | X |  |
| Publish profile creation | X | X | X |  |
| SQL files can be added by placing in project folder | X |  | X | X |
| SQL files can be excluded from build | X | X |  |  |
| [Pre/post deployment scripts](concepts/pre-post-deployment-scripts.md) | X | X | X | X |
| New object templates | X<sup>4</sup> | X | X<sup>4</sup> | X<sup>4</sup> |
| Project files can be organized into folders | X | X | X | X |
| [Schema comparison](concepts/schema-comparison.md) project to database |  | X | X |  |
| [Schema comparison](concepts/schema-comparison.md) database to project |  | X | X |  |
| Graphical table designer |  | X | X |  |
| [Code analysis](concepts/sql-code-analysis/sql-code-analysis.md) – enable/disable rules |  | X |  |  |
| Project properties – build output settings | X | X |  |  |
| Project properties – default schema |  | X |  |  |
| Project properties – database settings |  | X |  |  |
| Project run [code analysis](concepts/sql-code-analysis/sql-code-analysis.md) standalone |  | X |  |  |
| Object renaming and refactoring |  | X |  |  |
| Intellisense provided in database files from project model |  | X |  |  |
| SQL Server object explorer connectivity/view objects | X | X | X | X |
| SQL Server object explorer context menu items |  | X | X | X |
| SQL Server query editor connectivity |  | X | X | X |

1. In Visual Studio 2022 preview 2, SDK-style projects use the `.sqlprojx` extension instead of `.sqlproj`.
2. Local development instance is a SQL Server LocalDB instance.
3. Local development instance is a SQL Server container.
4. Limited subset of templates available

## Command line tools

[SqlPackage](../sqlpackage/sqlpackage.md) is the primary command line utility for the DacFx library, enabling automation of the database development tasks such as deploying a `.dacpac` to a database or extracting the objects of a database to a SQL project or `.dacpac`.

Custom console applications can be built using the DacFx .NET library to automate database development tasks. The [Microsoft.SqlServer.Dac](/dotnet/api/microsoft.sqlserver.dac) namespace contains classes for creating, deploying, and extracting database objects and is foundational to the rest of the DacFx library.

CI/CD pipelines can be built with command line execution or with tasks specific to `.dacpac` and SQL projects deployment. The [GitHub sql-action](https://github.com/azure/sql-action) and [SqlAzureDacpacDeployment in Azure DevOps](/azure/devops/pipelines/tasks/reference/sql-azure-dacpac-deployment-v1) are examples of tasks that use SqlPackage underneath a management layer to facilitate deploying database changes.

## Third-party tools

There are third-party tools available that provide functionality related to SQL projects and database deployment. Some tools are open source, such as [dbatools](https://docs.dbatools.io/Publish-DbaDacPackage.html).

Developers have shared their projects utilizing extensibility points around SQL projects, including [code analysis](concepts/sql-code-analysis/sql-code-analysis.md) rules and customizing deployment plans. Some of these projects are:

- https://github.com/tcartwright/SqlServer.Rules
- https://github.com/davebally/TSQL-Smells
- https://github.com/GoEddie/DeploymentContributorFilterer

## Related content

- [Project-Oriented Offline Database Development](../../ssdt/project-oriented-offline-database-development.md)
- [SQL Database Projects extension](/azure-data-studio/extensions/sql-database-project-extension)
- [SqlPackage](../sqlpackage/sqlpackage.md)
- [GitHub sql-action](https://github.com/azure/sql-action)
- [Azure DevOps SQL deployments](/azure/devops/pipelines/targets/azure-sqldb)
- [Data-tier applications (DAC)](../../relational-databases/data-tier-applications/data-tier-applications.md)
- [DacFx feedback repository](https://github.com/microsoft/dacfx)
- [Get started with SQL database projects](get-started.md)
- [Tutorial: Create and deploy a SQL project](tutorials/create-deploy-sql-project.md)
