---
title: Use SDK-style projects with the SQL Database Projects extension
description: Getting started using SDK-style SQL projects with the SQL Database Projects extension for Azure Data Studio or Visual Studio Code
author: dzsquared
ms.author: drskwier
ms.reviewer: maghan
ms.date: 4/12/2023
ms.service: azure-data-studio
ms.topic: conceptual
ms.custom: intro-get-started
---

# Use SDK-style SQL projects with the SQL Database Projects extension (Preview)

This article introduces [Microsoft.Build.Sql](https://www.nuget.org/packages/Microsoft.Build.Sql) for SDK-style SQL projects in the SQL Database Projects extension in Azure Data Studio or Visual Studio Code. SDK-style SQL projects are especially advantageous for applications shipped through pipelines or built in cross-platform environments.  The initial announcement is available in [TechCommunity](https://techcommunity.microsoft.com/t5/azure-sql-blog/microsoft-build-sql-the-next-frontier-of-sql-projects/ba-p/3290628).

> [!NOTE]
> Microsoft.Build.Sql is currently in preview. 

## Create an SDK-style database project

You can create an SDK-style database project from a blank project, or from an existing database. 

### Blank project

In the **Database Projects** view, select the **New Project** button and enter a project name in the text input that appears.  In the **Select a Folder** dialog box that appears, choose a directory for the project's folder, `.sqlproj` file, and other contents to reside in.  

By default the selection for **SDK-style project (Preview)** is checked. When the dialog is completed, the empty project is opened and visible in the **Database Projects** view for editing.

### From an existing database

In the **Project** view, select the **Create Project from Database** button and connect to a SQL Server.  Once the connection is established, select a database from the list of available databases and set the name of the project.  Select a target structure of the extraction.  

By default the selection for **SDK-style project (Preview)** is checked. When the dialog is completed, the new project is opened and contains SQL scripts for the contents of the selected database.

## Build and publish

From the Azure Data Studio and Visual Studio Code interfaces, building and publishing an SDK-style SQL project is completed in the same way as the previous SQL project format. For more on this process, see [Build and Publish a Project](sql-database-project-extension-build.md).

To build an SDK-style SQL project from the command line on Windows, macOS, or Linux, use the following command:

```bash
dotnet build
```

The `.dacpac` file resulting from building an SDK-style SQL project is compatible with tooling associated with the data-tier application framework (`.dacpac`, `.bacpac`), including [SqlPackage](../../tools/sqlpackage/sqlpackage-publish.md) and GitHub [sql-action](https://github.com/azure/sql-action).

## Project capabilities
In SQL projects there are several capabilities that can be specified in the `.sqlproj` file that impact the database model either at project build or deployment.  The following sections describe some of these capabilities that are available for Microsoft.Build.Sql projects.

### Target platform
The target platform property is contained in the `DSP` tag in the `.sqlproj` file under the `<PropertyGroup>` item.  The target platform is used during project build to validate support for features included in the project and is added to the `.dacpac` file as a property.  By default, during deployment, the target platform is checked against the target database to ensure compatibility.  If the target platform is not supported by the target database, the deployment fails unless an override [publish option](../../tools/sqlpackage/sqlpackage-publish.md) is specified.

```xml
<Project DefaultTargets="Build">
  <Sdk Name="Microsoft.Build.Sql" Version="0.1.9-preview" />
  <PropertyGroup>
    <Name>AdventureWorks</Name>
    <DSP>Microsoft.Data.Tools.Schema.Sql.SqlAzureV12DatabaseSchemaProvider</DSP>
  </PropertyGroup>
```

Valid settings for the target platform are:
- `Microsoft.Data.Tools.Schema.Sql.Sql120DatabaseSchemaProvider`
- `Microsoft.Data.Tools.Schema.Sql.Sql130DatabaseSchemaProvider`
- `Microsoft.Data.Tools.Schema.Sql.Sql140DatabaseSchemaProvider`
- `Microsoft.Data.Tools.Schema.Sql.Sql150DatabaseSchemaProvider`
- `Microsoft.Data.Tools.Schema.Sql.Sql160DatabaseSchemaProvider`
- `Microsoft.Data.Tools.Schema.Sql.SqlAzureV12DatabaseSchemaProvider`
- `Microsoft.Data.Tools.Schema.Sql.SqlDwDatabaseSchemaProvider`

### Database references
The database model validation at build time can be extended past the contents of the SQL project through database references. Database references specified in the `.sqlproj` file can reference another SQL project or a `.dacpac` file, representing either another database or more components of the same database.

The following attributes are available for database references that represent another database:
- **DatabaseSqlCmdVariable:** the value is the name of the variable that is used to reference the database
    - Reference setting: `<DatabaseSqlCmdVariable>SomeOtherDatabase</DatabaseSqlCmdVariable>`
    - Usage example: `SELECT * FROM [$(SomeOtherDatabase)].dbo.Table1`
- **ServerSqlCmdVariable:** the value is the name of the variable that is used to reference the server where the database resides. used with DatabaseSqlCmdVariable, when the database is in another server.
    - Reference setting: `<ServerSqlCmdVariable>SomeOtherServer</ServerSqlCmdVariable>`
    - Usage example: `SELECT * FROM [$(SomeOtherServer)].[$(SomeOtherDatabase)].dbo.Table1`
- **DatabaseVariableLiteralValue:** the value is the literal name of the database as used in the SQL project, similar to `DatabaseSqlCmdVariable` but the reference to other database is a literal value
    - Reference setting: `<DatabaseVariableLiteralValue>SomeOtherDatabase</DatabaseVariableLiteralValue>`
    - Usage example: `SELECT * FROM [SomeOtherDatabase].dbo.Table1`

In a SQL project file, a database reference is specified as an `ArtifactReference` item with the `Include` attribute set to the path of the `.dacpac` file.

```xml
  <ItemGroup>
    <ArtifactReference Include="SampleA.dacpac">
      <DatabaseSqlCmdVariable>DatabaseA</DatabaseSqlCmdVariable>
    </ArtifactReference>
  </ItemGroup>
</Project>
```

### Package references
Package references are used to reference NuGet packages that contain a `.dacpac` file and are used to extend the database model at build time similarly as a [database reference](#database-references).

The following example from a SQL project file references the `Microsoft.SqlServer.Dacpacs` [package](https://www.nuget.org/packages/Microsoft.SqlServer.Dacpacs) for the `master` database.

```xml
  <ItemGroup>
    <PackageReference Include="Microsoft.SqlServer.Dacpacs" Version="160.0.0" />
  </ItemGroup>
</Project>
```

In addition to the attributes available for [database references](#database-references), the following `DacpacName` attribute can be specified to select a `.dacpac` from a package that contains multiple `.dacpac` files.

```xml
  <ItemGroup>
    <PackageReference Include="Microsoft.SqlServer.Dacpacs" Version="160.0.0" />
      <DacpacName>msdb</DacpacName>
    </PackageReference>
  </ItemGroup>
</Project>
```

### SqlCmd variables
SqlCmd variables can be defined in the `.sqlproj` file and are used to replace tokens in SQL objects and scripts during `.dacpac` [deployment](../../tools/sqlpackage/sqlpackage-publish.md#sqlcmd-variables). The following example from a SQL project file defines a variable named `EnvironmentName` that available for use in the project's objects and scripts.

```xml
  <ItemGroup>
    <SqlCmdVariable Include="EnvironmentName">
      <DefaultValue>testing</DefaultValue>
      <Value>$(SqlCmdVar__1)</Value>
    </SqlCmdVariable>
  </ItemGroup>
</Project>
```

```sql
IF '$(EnvironmentName)' = 'testing'
BEGIN
    -- do something
END
```

When a compiled SQL project (`.dacpac`) is deployed, the value of the variable is replaced with the value specified in the deployment command.  For example, the following command deploys the `AdventureWorks.dacpac` and sets the value of the `EnvironmentName` variable to `production`.

```bash
SqlPackage /Action:Publish /SourceFile:AdventureWorks.dacpac /TargetConnectionString:{connection_string_here} /v:EnvironmentName=production
```

### Pre/post-deployment scripts
Pre- and post-deployment scripts are SQL scripts that are included in the project to be executed during deployment. Pre/post-deployment scripts are included in the `.dacpac` but they are not compiled into or validated with database object model. A pre-deployment script is executed before the database model is applied and a post-deployment script is executed after the database model is applied.  The following example from a SQL project file adds the file `populate-app-settings.sql` as post-deployment script.

```xml
  <ItemGroup>
    <PostDeploy Include="populate-app-settings.sql" />
  </ItemGroup>
</Project>
```


## Next steps

- [Build and Publish a project with the SQL Database Projects extension](sql-database-project-extension-build.md)
- [Install SqlPackage for deployment from the CLI](../../tools/sqlpackage/sqlpackage-download.md)
