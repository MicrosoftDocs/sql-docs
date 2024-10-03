---
title: "What are SQL database projects?"
description: "This overview introduces SQL database projects, which enable database development and CI/CD workflows."
author: dzsquared
ms.author: drskwier
ms.reviewer: maghan, randolphwest
ms.date: 08/30/2024
ms.service: sql
ms.topic: overview
---

# What are SQL database projects?

[!INCLUDE [SQL Server 2022 Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sqlserver2022-asdb-asmi.md)]

A SQL database project is a local representation of SQL objects that comprise the schema for a single database, such as tables, stored procedures, or functions. The development cycle of a SQL database project enables database development to be integrated into a continuous integration and continuous deployment (CI/CD) workflows familiar as a development best practice.

## Overview

SQL projects are based in declarative T-SQL statements. In your SQL database project code, you create each object once. If you need to change something about that object, such as adding columns or changing a data type, you modify the singular file that declares the object for the first and only time.

When a SQL database project is built, the output artifact is a `.dacpac` file. New and existing databases can be updated to match the contents of the `.dacpac` by publishing the `.dacpac` to a target database.

The SQL database projects framework around your database code that adds two foundational capabilities to that set of files with its build process:

- [validation](#validation) of references between objects and the syntax against a specific version of SQL
- [deployment](#deployment) of the build artifact to new or existing databases

:::image type="content" source="media/sql-database-projects/sqlproj-summary.png" alt-text="Screenshot of Summary of SQL Database Projects containing pre-deployment and post-deployment scripts as well as database objects." lightbox="media/sql-database-projects/sqlproj-summary.png":::

The functionality for SQL database projects is provided by the [Microsoft.SqlServer.DacFx](https://www.nuget.org/packages/Microsoft.SqlServer.DacFx/) .NET library and is surfaced in several [tools for SQL development](sql-projects-tools.md). DacFx has multiple extensibility points, such as modification of deployment steps and the ability to create custom rules for code analysis. The project SDK for SQL projects is [Microsoft.Build.Sql](https://www.nuget.org/packages/Microsoft.Build.Sql/), currently available in preview and [advised for new development](#original-projects-vs-sdk-style-projects-preview).

### Validation

When a SQL project is built, the relationships between objects are validated. For example, a view definition can't contain a table or columns that don't exist in the SQL project.

Additionally, a SQL project contains a property in its `.sqlproj` file called the "[target platform](concepts/target-platform.md)". This information is used during the build process to validate that the functions and T-SQL syntax exists in that version of SQL. For example, the JSON functions added in SQL Server 2022 can't be used in a SQL project set to the Sql140 (SQL Server 2017) target platform.

To build a SQL project, we run dotnet build from the command line. In graphical tools that support SQL projects (Azure Data Studio, VS Code, and Visual Studio), there's a menu item to build the SQL project.

The console output of the build process might contain errors (build failure) or warnings. Build warnings can include inconsistent casing in object names and other [customizable best practices](concepts/sql-code-analysis/sql-code-analysis.md), but don't fail the build.

The artifact output of the build process is a `.dacpac` file, which can be found for a build with default settings in the `bin/Debug` folder.

### Deployment

The output file, the `.dacpac`, is powerful, reusable, and declarative artifact. With this file, we can use SqlPackage or other [tools](sql-projects-tools.md) to apply our database code to a database. The SqlPackage command to deploy a `.dacpac` is the **[publish](../sqlpackage/sqlpackage-publish.md)** command.

:::image type="content" source="media/sql-database-projects/build-deploy.png" alt-text="Screenshot of Overview of process from SQL project build to dacpac and deploy to database." lightbox="media/sql-database-projects/build-deploy.png":::

For example, `sqlpackage /Action:Publish /SourceFile:yourfile.dacpac /TargetConnectionString:{yourconnectionstring}`.

#### New databases

SqlPackage navigates the object relationships to create each object in the right order when publishing a dacpac to a new database. For example, SqlPackage creates Table_A before Table_B when Table_B has a foreign key to Table_A.

You don't want to be executing a whole folder of SQL scripts, especially when you could be using SQL projects which automatically executes each T-SQL section in the right order based on object relationships.

#### Existing databases

In addition to navigating the object hierarchy when publishing to new databases, the `.dacpac` publish process also calculates the difference between a source `.dacpac` and a target database before determining what steps it needs to take to update that database. For example, if Table_C is missing two columns in the database that it has in the SQL project and StoredProcedure_A was changed, SqlPackage creates an `ALTER TABLE` statement and an `ALTER PROCEDURE` statement instead of blindly trying to create a bunch of objects.

:::image type="content" source="media/sql-database-projects/existing-deploy.png" alt-text="Screenshot of Example alter table statement calculated by deployment." lightbox="media/sql-database-projects/existing-deploy.png":::

The flexibility provided by the publish command to existing databases isn't limited to a single database. One `.dacpac` can be deployed multiple times, such as when upgrading a fleet of a hundred databases.

## When to use

SQL database projects are a great fit for teams that are looking to integrate database development into a CI/CD workflow. The declarative nature of SQL projects allows for a single source of truth for the database schema, and the build and publish process provides a repeatable and reliable way to deploy changes to databases.

SQL database projects are used to track the source of truth for database state, including development with an object-relational mapper (ORM) such as EF Core. Either a graphical tool or the command line can be used to extract the schema of a database to a SQL project, regardless of the ORM used to create the database.

:::image type="content" source="media/sql-database-projects/project-files.png" alt-text="Screenshot of VS Code with AdventureWorks SQL project open, displaying a table in the editor." lightbox="media/sql-database-projects/project-files.png":::

SQL database projects support the SQL Server and Azure SQL family of databases, including Azure SQL Database and Azure Synapse Analytics. Whether you're developing an application or a data warehouse, SQL database projects can be used to manage the schema of your database. SQL projects can be developed from [tools](sql-projects-tools.md) in Visual Studio, VS Code, and Azure Data Studio.

## Original projects vs SDK-style projects (preview)

The original SQL project format is based on MSBuild (.NET Framework) and is the format used by SQL Server Data Tools in Visual Studio. The SDK-style project format is based on the new SDK-style projects introduced in .NET Core and is the format used by the SQL Database Projects extension for Azure Data Studio and VS Code. Support for SDK-style SQL projects in Visual Studio is on the [roadmap](https://github.com/microsoft/DacFx/issues/180).

New development work should consider using the SDK-style project format, as it's the format that will be supported in the future. The SDK-style project format is more flexible and contains new features not available with the original SQL projects:

- .NET 8 support (cross platform)
- NuGet [package references](concepts/package-references.md) for database references
- Default globbing pattern for .sql files in the project

SDK-style projects have a superset of functionality from original SQL projects and existing SQL projects can be converted to SDK-style projects through [modification of the project file](howto/convert-original-sql-project.md). The exception to the functionality coverage is support for SQLCLR objects, which require .NET Framework and aren't supported in SDK-style projects.

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
