---
title: Compare a database and a project
description: "Compare a project and a database and with different approaches."
author: dzsquared
ms.author: drskwier
ms.reviewer: maghan, randolphwest
ms.date: 08/30/2024
ms.service: sql
ms.topic: how-to
zone_pivot_groups: sq1-sql-projects-tools
---

# Compare a database and a project

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Understanding the object definition differences between a database and a SQL project can provide valuable insights into the state of your database and project, including during ongoing development or regression troubleshooting. SQL projects includes tooling for visualizing differences, analyzing changes required to update a database, importing changes from a database into a SQL project file set, and reviewing T-SQL scripts that would be executed to update a database to match the project.

This article reviews methods for comparing a database and a SQL project using different approaches:

- You can use **schema compare** to [visualize the differences](#schema-compare-visualize-differences) between databases and/or projects. This comparison can help you identify changes that need to be synchronized between the database and the project.
- You can use a **deploy report** to [summarize and automate reviews](#deploy-report-review-changes) of the changes required to update a database.
- You can use **schema compare** or SqlPackage **extract** to [import changes from a database](#import-changes-from-a-database) into a SQL project file set.
- You can use SqlPackage **deploy script** or **schema compare** to [review the T-SQL statements](#review-deployment-t-sql-scripts) that are executed to update a database to match the project.

## Schema compare: visualize differences

### Prerequisites

::: zone pivot="sq1-visual-studio"

- [Visual Studio 2022 Community, Professional, or Enterprise](https://visualstudio.microsoft.com/downloads/)
- [SQL Server Data Tools (SSDT) installed in Visual Studio 2022](../../../ssdt/download-sql-server-data-tools-ssdt.md)

::: zone-end

<!-- ::: zone pivot="sq1-visual-studio-sdk"

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022 Community, Professional, or Enterprise](https://visualstudio.microsoft.com/downloads/)
- [SQL Server Data Tools, SDK-style (preview) installed in Visual Studio 2022](../../sql/ssdt/download-sql-server-data-tools-ssdt-sdk.md)

::: zone-end -->

::: zone pivot="sq1-visual-studio-code"

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Database Projects extension for Azure Data Studio](/azure-data-studio/extensions/sql-database-project-extension)

::: zone-end

::: zone pivot="sq1-command-line"

[!INCLUDE [schema-compare-command-line](../includes/schema-compare-command-line.md)]

::: zone-end

### Summary

Schema compare provides the visually richest interface to understanding the differences between a database and a project. A key capability with schema compare is the directionality of the comparison is reversible. As a result, you can use schema comparison to understand changes from a project to be deployed to a database or changes from a database to add to a project. You can use schema compare to identify differences in object definitions, such as tables, views, stored procedures, and functions.

The complete set of differences or a selected subset can be used to apply the changes to the database or project. Schema compare can also generate a deployment script that, when run, effectively applies the changes to a database.

Learn more about schema compare in the [schema comparison overview](../concepts/schema-comparison.md).

## Deploy report: review changes

Deploy reports require the SqlPackage CLI.

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SqlPackage CLI](../../sqlpackage/sqlpackage-download.md)

```bash
dotnet tool install -g Microsoft.SqlPackage
```

### Summary

A deploy report provides a summary of the changes required to update a database to match a project. The SqlPackage CLI [generates a deploy report](../../sqlpackage/sqlpackage-deploy-drift-report.md) by comparing a source model (`.dacpac` SQL project build artifact or database) with a target database. For example, the following command generates a deploy report for a database named `MyDatabase` from a SQL project named `MyProject`:

```bash
dotnet build MyProject.sqlproj
sqlpackage /Action:deployreport /SourceFile:bin/Debug/MyProject.dacpac /TargetConnectionString:{connection string for MyDatabase} /OutputPath:deployreport.xml
```

The produced XML is a simplified form of the deployment plan, summarizing the operations that would be performed if a database deployment is run. The following list of operations is non-exhaustive:

- Create
- Alter
- Drop
- Refresh
- UnbindSchemaBinding
- UnbindFulltextIndex
- TableDataMotion
- SPRename
- EnableChangeTrackingDatabase
- DisableChangeTrackingDatabase

A deploy report can be reviewed in a text editor or in Visual Studio and would look similar to the following:

```xml
<?xml version="1.0" encoding="utf-8"?>
<DeploymentReport xmlns="http://schemas.microsoft.com/sqlserver/dac/DeployReport/2012/02">
    <Alerts />
    <Operations>
        <Operation Name="Create">
            <Item Value="[CO].[Products].[IX_Products_CategorySlug]" Type="SqlIndex" />
        </Operation>
        <Operation Name="Alter">
            <Item Value="[CO].[Brands]" Type="SqlTable" />
            <Item Value="[CO].[AddProductImage]" Type="SqlProcedure" />
        </Operation>
        <Operation Name="Refresh">
            <Item Value="[CO].[SelectStarView]" Type="SqlView" />
        </Operation>
    </Operations>
</DeploymentReport>
```

A deploy report can be used to review the changes as well as monitor for potentially high impact events, such as data motion or clustered index create/drop. These events would be listed in the deployment report under the `Alerts` element.

An advantage of the deploy report XML operation is that it can be used to automate the review of changes required to update a database. The XML can be parsed and used to generate a report or to trigger alerts based on the operations or object names listed.

## Import changes from a database

As mentioned in the [schema compare section](#schema-compare-visualize-differences), schema compare can be used to apply changes from a database into a SQL project file set. Applying changes to a SQL project is a common scenario when you have a database that is actively developed in directly and a SQL project is used to manage the database objects in source control. Manually completing this operation through Visual Studio or Azure Data Studio can be time-consuming, especially when the database has many objects or sporadic changes. In this section, we review how to automate the extract of object definitions from a database into a SQL project file set.

### Prerequisites

With a focus on automation, we utilize the SqlPackage CLI to extract object definitions from a database into a SQL project file set. The Microsoft.Build.Sql.Templates .NET templates are used to create a SQL project file, an optional step.

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SqlPackage CLI](../../sqlpackage/sqlpackage-download.md)
- [Microsoft.Build.Sql.Templates .NET templates](https://www.nuget.org/packages/Microsoft.Build.Sql.Templates/)

```bash
# install SqlPackage CLI
dotnet tool install -g Microsoft.SqlPackage

# install Microsoft.Build.Sql.Templates
dotnet new install Microsoft.Build.Sql.Templates
```

### Summary

The SqlPackage extract command takes a source database and generates an output database model, either as a `.dacpac` file or as a set of SQL scripts. [SqlPackage](../../sqlpackage/sqlpackage-extract.md) defaults to generating a `.dacpac` file, but the `/p:ExtractTarget=` property can be used to specify a set of SQL scripts. The following command extracts the database `MyDatabase` into a SQL project file set in the folder `MyDatabaseProject`:

```bash
sqlpackage /Action:Extract /SourceConnectionString:{connection string for MyDatabase} /TargetFile:MyDatabaseProject /p:ExtractTarget=SchemaObjectType
```

When a folder is under source control, the extracted object definitions would show differences in source control tooling. By using SqlPackage to generate the files and check for differences in source control, you can automate the process of importing changes from a database into a SQL project file set.

In a series of three commands, we can remove the previous set of files, extract the database, and check for differences in the source control tooling:

```bash
rm -rf MyDatabaseProject
sqlpackage /Action:Extract /SourceConnectionString:{connection string for MyDatabase} /TargetFile:MyDatabaseProject /p:ExtractTarget=SchemaObjectType
git status --porcelain | wc -l
```

Our output is the number of files that were changed by the latest SqlPackage extract. The output from the `git status` command can be used to trigger other automation steps. If we'd like to use this set of files as a SQL project, we can use the Microsoft.Build.Sql.Templates .NET templates to create a SQL project file in the folder `MyDatabaseProject`:

```bash
dotnet new sqlproj -n MyDatabaseProject -o MyDatabaseProject
```

## Review deployment T-SQL scripts

As mentioned in the [schema compare section](#schema-compare-visualize-differences), schema compare can be used to generate the T-SQL scripts necessary to update a database to match a SQL project. In this section, we review how to use SqlPackage automate the generation of the T-SQL scripts necessary to update a database to match a SQL project such that they can be stored as a pipeline artifact for review and approval.

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SqlPackage CLI](../../sqlpackage/sqlpackage-download.md)

```bash
dotnet tool install -g Microsoft.SqlPackage
```

### Summary

Running a SQL project deployment with SqlPackage uses the [publish](../../sqlpackage/sqlpackage-publish.md) action, but if we're looking to review the T-SQL scripts that are executed, we can use the [script](../../sqlpackage/sqlpackage-script.md) action. The following command generates the T-SQL scripts necessary to update a database named `MyDatabase` to match a SQL project named `MyProject`:

```bash
dotnet build MyProject.sqlproj
sqlpackage /Action:Script /SourceFile:bin/Debug/MyProject.dacpac /TargetConnectionString:{connection string for MyDatabase} /DeployScriptPath:Deployment.sql
```

## Related content

- [Schema comparison overview](../concepts/schema-comparison.md)
- [SqlPackage](../../sqlpackage/sqlpackage.md)
