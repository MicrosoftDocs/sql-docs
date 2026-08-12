---
title: Publish Profiles Overview
description: Learn about publish profiles for SQL projects, including the deployment settings, SQLCMD variables, and connection information they store.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: drskwier
ms.date: 04/10/2026
ms.service: sql
ms.subservice: sql-database-projects
ms.topic: concept-article
ms.collection:
  - data-tools
zone_pivot_groups: sq1-sql-projects-tools
---

# Publish profiles overview

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

A publish profile is a file that stores deployment configuration for a SQL project. Publish profiles can encapsulate target connection information, deployment options, and SQLCMD variable values in a reusable `.publish.xml` file. Publish profiles are especially useful when your deployment requires specific settings, like excluding certain object types or ignoring target platform differences. Instead of specifying these options each time you deploy, save them in a publish profile and reuse it.

When working with SQL projects, you can create and store multiple publish profiles. During deployment, select a publish profile to apply consistent settings without manually selecting multiple options.

## Publish profile file format

A publish profile is an XML file with the `.publish.xml` extension. The file contains properties and items that define deployment behavior in the XML path `/Project/PropertyGroup`.

A publish profile can include the following information:

- **Target connection string** - the connection string for the target database server
- **Target database name** - the name of the database to deploy to
- **Deployment options** - settings that control deployment behavior, such as whether to drop objects not in the source or block on possible data loss
- **SQLCMD variable values** - values for SQLCMD variables defined in the SQL project, applied at deployment time

### Sample publish profile

The following example shows a publish profile that targets a local SQL Server instance. It sets two deployment options and provides a value for one SQLCMD variable:

```xml
<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="4.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <PropertyGroup>
    <IncludeCompositeObjects>True</IncludeCompositeObjects>
    <TargetDatabaseName>AdventureWorks</TargetDatabaseName>
    <AllowIncompatiblePlatform>True</AllowIncompatiblePlatform>
    <ProfileVersionNumber>1</ProfileVersionNumber>
  </PropertyGroup>
  <ItemGroup>
    <SqlCmdVariable Include="EnvironmentName">
      <Value>staging</Value>
    </SqlCmdVariable>
  </ItemGroup>
</Project>
```

### Publish profile entry in project file

When you add a publish profile to a SQL project, it adds an entry to the project file (`.sqlproj`) that includes the path to the publish profile file:

```xml
  <ItemGroup>
    <None Include="Staging.publish.xml" />
  </ItemGroup>
```

While you can specify any publish profile using the `/Profile:` parameter with SqlPackage, publish profiles included in the project file appear in the solution explorer or database projects view of your SQL project tool. This feature makes them easier to manage and select during deployment.

## Deployment options in publish profiles

Deployment options control the behavior of the deployment engine when applying changes to a target database. These options correspond to the properties available in the [SqlPackage Publish](../../sqlpackage/sqlpackage-publish.md) action and the `DacDeployOptions` class in the DacFx API.

Commonly used deployment options include:

| Option | Default | Description |
| --- | --- | --- |
| `AllowIncompatiblePlatform` | `False` | Attempts deployment even if there are platform compatibility differences. |
| `BlockOnPossibleDataLoss` | `True` | Stops deployment if a change could result in data loss. |
| `DropObjectsNotInSource` | `False` | Drops objects in the target database that don't exist in the source project. |
| `IgnoreColumnOrder` | `False` | Ignores differences in column order between source and target. |
| `ScriptDatabaseOptions` | `True` | Scripts database-level options such as compatibility level during deployment. |

For a complete list of deployment options and their default values, see [SqlPackage Publish properties](../../sqlpackage/sqlpackage-publish.md#properties-specific-to-the-publish-action).

## SQLCMD variables in publish profiles

[SQLCMD variables](sqlcmd-variables.md) defined in a SQL project can have their values set in a publish profile. When the publish profile is used for deployment, the SQLCMD variable values from the profile override any default values in the project.

The publish profile stores SQLCMD variables as `<SqlCmdVariable>` items:

```xml
<ItemGroup>
  <SqlCmdVariable Include="EnvironmentName">
    <Value>staging</Value>
  </SqlCmdVariable>
  <SqlCmdVariable Include="FirstHistoricalYear">
    <Value>2005</Value>
  </SqlCmdVariable>
</ItemGroup>
```

Values specified on the command line with `/v:` override both the project default and publish profile values.

## Use publish profiles with tools

Publish profiles are supported across SQL project tools and the SqlPackage command-line interface.

::: zone pivot="sq1-visual-studio"

In Visual Studio (SSDT), create and load publish profiles from the **Publish** dialog. When you configure deployment settings and select **Save Profile As**, the settings are saved to a `.publish.xml` file. You can load a previously saved profile by selecting **Load Profile** in the **Publish** dialog. Publish profiles are stored as items in the SQL project and appear in **Solution Explorer**.

Add new publish profiles to the project with the **Add New Item** dialog, and selecting the **Publish Profile** item template. Add existing publish profile files to the project using **Add > Existing Item**.

When you double-click a publish profile from **Solution Explorer**, it opens the **Publish** dialog with the settings from the profile applied. You can review or modify these settings before deployment.

::: zone-end

::: zone pivot="sq1-visual-studio-sdk"

In SDK-style SQL projects in Visual Studio, create and load publish profiles from the **Publish** dialog. When you configure deployment settings and select **Save Profile As**, the settings are saved to a `.publish.xml` file. You can load a previously saved profile by selecting **Load Profile** in the **Publish** dialog.

::: zone-end

::: zone pivot="sq1-visual-studio-code"

In Visual Studio Code with the SQL Database Projects extension, create publish profiles from the **Publish** dialog using the **Save As** option. The publish profile is saved as a `.publish.xml` file that you can add to the project. You can load a previously saved profile by selecting it from **Select Profile** in the **Publish** dialog.

Add new publish profiles to the project by selecting the **Add Publish Profile** option from the project context menu. Add existing publish profile files to the project with **Add Existing Item...**.

In Visual Studio Code with the SQL Database Projects extension, publish profiles appear under the project node in the **Database Projects** view. When you publish a project, you can select an existing publish profile or proceed without one.

::: zone-end

::: zone pivot="sq1-sql-server-management-studio"

Publish profiles aren't currently supported in SQL Server Management Studio (SSMS).

::: zone-end

::: zone pivot="sq1-command-line"

With the SqlPackage command-line interface, specify a publish profile with the `/Profile:` (or `/pr:`) parameter:

```bash
sqlpackage /Action:Publish /SourceFile:AdventureWorks.dacpac /Profile:production.publish.xml
```

Properties and SQLCMD variable values that you specify on the command line override values in the publish profile. For more information, see [SqlPackage Publish](../../sqlpackage/sqlpackage-publish.md).

::: zone-end

## Related content

- [SQLCMD variables overview](sqlcmd-variables.md)
- [SqlPackage Publish parameters, properties, and SQLCMD variables](../../sqlpackage/sqlpackage-publish.md)
- [SQL projects properties](project-properties.md)
- [Tutorial: Create and deploy a SQL project](../tutorials/create-deploy-sql-project.md)
