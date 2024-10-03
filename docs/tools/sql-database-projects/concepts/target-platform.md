---
title: SQL projects target platform
description: "Specify SQL version compatibility for SQL database projects."
author: dzsquared
ms.author: drskwier
ms.reviewer: maghan, randolphwest
ms.date: 08/30/2024
ms.service: sql
ms.topic: concept-article
f1_keywords:
  - "sql.data.tools.publish.dialog"
  - "sql.data.tools.publishdacproject"
zone_pivot_groups: sq1-sql-projects-tools
---

# Target platform overview

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

The target platform setting is a project property that is used during project build to validate support for features included in the project, such as T-SQL syntax and system functions. The target platform setting is incorporated into the `.dacpac` build artifact and during deployment the target platform setting is checked against the target database to ensure compatibility. If the target platform doesn't match the database, the deployment doesn't begin unless the [publish property](../../sqlpackage/sqlpackage-publish.md#properties-specific-to-the-publish-action) `/p:AllowIncompatiblePlatform=true` is specified.

## SQL project file sample and syntax

The target platform project property is contained in the `DSP` tag in the `.sqlproj` file under the `<PropertyGroup>` item:

```xml
<Project DefaultTargets="Build">
  <Sdk Name="Microsoft.Build.Sql" Version="0.2.0-preview" />
  <PropertyGroup>
    <Name>AdventureWorks</Name>
    <DSP>Microsoft.Data.Tools.Schema.Sql.SqlAzureV12DatabaseSchemaProvider</DSP>
  </PropertyGroup>
...
```

Valid values for the target platform in the `DSP` tag include:

- `Microsoft.Data.Tools.Schema.Sql.Sql120DatabaseSchemaProvider` (SQL Server 2014)
- `Microsoft.Data.Tools.Schema.Sql.Sql130DatabaseSchemaProvider` (SQL Server 2016)
- `Microsoft.Data.Tools.Schema.Sql.Sql140DatabaseSchemaProvider` (SQL Server 2017)
- `Microsoft.Data.Tools.Schema.Sql.Sql150DatabaseSchemaProvider` (SQL Server 2019)
- `Microsoft.Data.Tools.Schema.Sql.Sql160DatabaseSchemaProvider` (SQL Server 2022)
- `Microsoft.Data.Tools.Schema.Sql.SqlAzureV12DatabaseSchemaProvider` (Azure SQL Database)
- `Microsoft.Data.Tools.Schema.Sql.SqlDbFabricDatabaseSchemaProvider` (Fabric Mirrored SQL Database, preview)
- `Microsoft.Data.Tools.Schema.Sql.SqlDwDatabaseSchemaProvider` (Azure Synapse SQL Pool)
- `Microsoft.Data.Tools.Schema.Sql.SqlServerlessDatabaseSchemaProvider` (Azure Synapse Serverless SQL Pool)
- `Microsoft.Data.Tools.Schema.Sql.SqlDwUnifiedDatabaseSchemaProvider` (Synapse Data Warehouse in Microsoft Fabric)

## Alter the target platform

::: zone pivot="sq1-visual-studio"

To change the target platform of a SQL project in Visual Studio, right-click the project in **Solution Explorer** and select **Properties**. In the **Project Settings** tab of the properties window, select the desired target platform from the **Target platform** dropdown list.

::: zone-end

::: zone pivot="sq1-visual-studio-sdk"

To change the target platform of a SQL project in Visual Studio, right-click the project in **Solution Explorer** and select **Properties**. In the **Project Settings** tab of the properties window, select the desired target platform from the **Target platform** dropdown list.

::: zone-end

::: zone pivot="sq1-visual-studio-code"

To change the target platform of a SQL project in the SQL Database Projects extension, right-click the project in the **Database Projects** view and select **Change Target Platform**. Select the new target platform from the dropdown list.

Alternatively, you can edit the `.sqlproj` file directly to change the target platform. Open the `.sqlproj` file from the **Explorer** view or by right-clicking on the project in the **Database Projects** view and selecting **Edit .sqlproj File**. From the text editor, change the value in the DSP tag to the desired target platform.

::: zone-end

::: zone pivot="sq1-command-line"

To build a SQL project for a target platform different than the target platform specified in the `.sqlproj` file, use the `/p:DSP=` command line argument. For example, to build a SQL project for SQL Server 2019 compatibility:

```bash
dotnet build /p:DSP=Microsoft.Data.Tools.Schema.Sql.Sql150DatabaseSchemaProvider
```

::: zone-end

## Publish to a different target platform

When you publish a SQL project, the target platform of the project must match the target platform of the database. If the target platforms don't match, the deployment exits before applying any changes with an error. To publish a project to a database with a different target platform, use the `/p:AllowIncompatiblePlatform=true` [publish property](../../sqlpackage/sqlpackage-publish.md#properties-specific-to-the-publish-action).

## Related content

- [SqlPackage Publish parameters, properties, and SQLCMD variables](../../sqlpackage/sqlpackage-publish.md)
- [Tutorial: Create and deploy a SQL project](../tutorials/create-deploy-sql-project.md)
- [Database Project Settings](../../../ssdt/database-project-settings.md)
