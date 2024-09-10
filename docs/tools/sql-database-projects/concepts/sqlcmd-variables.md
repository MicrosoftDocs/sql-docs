---
title: SQLCMD variables in SQL projects
description: "Dynamically set values in a SQL project at deployment."
author: dzsquared
ms.author: drskwier
ms.reviewer: maghan, randolphwest
ms.date: 08/30/2024
ms.service: sql
ms.topic: concept-article
zone_pivot_groups: sq1-sql-projects-tools
---

# SQLCMD variables overview

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

SQLCMD variables are used in SQL projects to create dynamically replaceable tokens in SQL objects and scripts. The values of these variables are set at deployment time and can be used to dynamically set values in a SQL project. Values for SQLCMD variables can be set in the publish action or through a publish profile.

## SQL project file sample and syntax

SQLCMD variables are defined in the `.sqlproj` file under an `<ItemGroup>` item. In this example, the variable `EnvironmentName` is defined with a default value of `testing`:

```xml
...
  <ItemGroup>
    <SqlCmdVariable Include="EnvironmentName">
      <DefaultValue>testing</DefaultValue>
      <Value>$(SqlCmdVar__1)</Value>
    </SqlCmdVariable>
  </ItemGroup>
</Project>
```

The `DefaultValue` element is optional. When a default value is provided, it's only used to load in the publish dialog of graphical tools for SQL projects. The default value isn't compiled into the `.dacpac` file and a command line deployment without the values specified by a publish profile or the `/v` option to specify values will use empty values for the SQLCMD variables.

### Use SQLCMD variables in SQL objects

SQLCMD variables can be used in SQL objects and scripts by wrapping the variable name in `$(variableName)` syntax. For example, the following SQL script uses the `$(EnvironmentName)` variable to control script behavior:

```sql
IF '$(EnvironmentName)' = 'testing'
BEGIN
    -- do something
END
```

### Use SQLCMD variables in publish actions

SQLCMD variables can be set at deployment time using the `/v` option in the [SqlPackage](../../sqlpackage/sqlpackage-publish.md#sqlcmd-variables) command line tool. For example, the following command sets the `EnvironmentName` variable to `production`:

```bash
sqlpackage /Action:Publish /SourceFile:AdventureWorks.dacpac /TargetConnectionString:{connection_string_here} /v:EnvironmentName=production
```

## Add and use SQLCMD variables

::: zone pivot="sq1-visual-studio"

To add a SQLCMD variable to a SQL project in Visual Studio, right-click the project in **Solution Explorer** and select **Properties**. In the **SQLCMD Variables** tab of the properties window, specify the variable name and optionally a default value.

Once the variable is defined, it can be used in SQL scripts by wrapping the variable name in `$(variableName)` syntax.

When publishing the project from Visual Studio, SQLCMD variables are set in the publish dialog. Use the **Load Values** button to load the default values from the SQL project into the dialog.

::: zone-end

::: zone pivot="sq1-visual-studio-sdk"

To add a SQLCMD variable to a SQL project in Visual Studio, right-click the project in **Solution Explorer** and select **Properties**. In the **SQLCMD Variables** section of the properties window, specify the variable name and optionally a default value.

Once the variable is defined, it can be used in SQL scripts by wrapping the variable name in `$(variableName)` syntax.

When publishing the project from Visual Studio, SQLCMD variables are set in the publish dialog. Use the **Load Values** button to load the default values from the SQL project into the dialog.

::: zone-end

::: zone pivot="sq1-visual-studio-code"

To add a SQLCMD variable to a SQL project in the SQL Database Projects extension, right-click the **SQLCMD Variables** node under the project in the **Database Projects** view and select **Add SQLCMD Variable**. Specify the variable name and optionally a default value.

Once the variable is defined, it can be used in SQL scripts by wrapping the variable name in `$(variableName)` syntax.

When publishing the project from the SQL Database Projects extension, SQLCMD variables values are automatically loaded from the default values. You're able to provide alternative values when prompted to modify the values during the publish process.

::: zone-end

::: zone pivot="sq1-command-line"

To add a SQLCMD variable to a SQL project, add an `<ItemGroup>` item to the `.sqlproj` file with a `<SqlCmdVariable>` item for each variable. The `<SqlCmdVariable>` item includes the variable name, a default value, and a value that can be set at deployment time.

```xml
<ItemGroup>
    <SqlCmdVariable Include="EnvironmentName">
        <DefaultValue>testing</DefaultValue>
        <Value>$(SqlCmdVar__1)</Value>
    </SqlCmdVariable>
</ItemGroup>
```

Once the variable is defined, it can be used in SQL scripts by wrapping the variable name in `$(variableName)` syntax.

When deploying the project from the command line, SQLCMD variables can be set using the `/v` option in the [SqlPackage](../../sqlpackage/sqlpackage-publish.md#sqlcmd-variables) command line tool.

For example:

```bash
SqlPackage /Action:Publish /SourceFile:AdventureWorks.dacpac /TargetConnectionString:{connection_string_here} /v:EnvironmentName=production
```

::: zone-end

## Related content

- [SqlPackage Publish parameters, properties, and SQLCMD variables](../../sqlpackage/sqlpackage-publish.md)
- [Tutorial: Create and deploy a SQL project](../tutorials/create-deploy-sql-project.md)
- [Database Project Settings](../../../ssdt/database-project-settings.md)
