---
title: Build a Project from the Command Line
description: Build a SQL Server Database Project from the command line
author: dzsquared
ms.author: drskwier
ms.reviewer: maghan
ms.date: 5/24/2022
ms.service: azure-data-studio
ms.topic: conceptual
---

# Build a database project from command line

While the SQL Database Project extension (preview) provides a graphical user interface to [build a database project](sql-database-project-extension-build.md), a command line build experience is also available for Windows, macOS, and Linux environments. The steps to build a project from the command line are different between [SDK-style SQL projects](sql-database-project-extension-sdk-style-projects.md) and the previous non-SDK-style SQL project format.  This article outlines the prerequisites and syntax needed to build a SQL project to dacpac from the command line for both SQL project types.

## SDK-style SQL projects

Using [Microsoft.Build.Sql](https://www.nuget.org/packages/Microsoft.Build.Sql) with [SDK-style SQL projects](sql-database-project-extension-sdk-style-projects.md) is the preferred method for working with SQL projects from the command line.

To build an SDK-style SQL project from the command line on Windows, macOS, or Linux, use the following command:

```bash
dotnet build /p:NetCoreBuild=true
```

> [!NOTE]
> The SDK-style SQL projects is currently in preview. 


## Non-SDK-style SQL projects

### Prerequisites

1. Install and configure [SQL Database Projects extension](sql-database-project-extension.md).

2. The following .NET Core dlls and the target file `Microsoft.Data.Tools.Schema.SqlTasks.targets` are required to build a SQL database project from the command line from all platforms supported by the Azure Data Studio extension for SQL Database Projects. These files are created by the extension during the first build completed in the Azure Data Studio interface and placed in the extension's folder under `BuildDirectory`.  For example, on Linux, these files are placed in `~\.azuredatastudio\extensions\microsoft.sql-database-projects-x.x.x\BuildDirectory\`.  Copy these 11 files to a new and accessible folder or note their location.  This location will be referred to as `DotNet Core build folder` in this document.

    - Microsoft.Data.SqlClient.dll
    - Microsoft.Data.Tools.Schema.Sql.dll
    - Microsoft.Data.Tools.Schema.SqlTasks.targets
    - Microsoft.Data.Tools.Schema.Tasks.Sql.dll
    - Microsoft.Data.Tools.Utilities.dll
    - Microsoft.SqlServer.Dac.dll
    - Microsoft.SqlServer.Dac.Extensions.dll
    - Microsoft.SqlServer.TransactSql.ScriptDom.dll
    - Microsoft.SqlServer.Types.dll
    - System.ComponentModel.Composition.dll
    - System.IO.Packaging.dll

3. If the project was created in Azure Data Studio - skip ahead to [Build the project from the command line](#build-the-project-from-the-command-line). If the project was created in SQL Server Data Tools (SSDT), open the project in the Azure Data Studio SQL Database project extension.  Opening the project in Azure Data Studio automatically updates the `sqlproj` file with three edits, noted below for your information:

    1. Import conditions

    ```console
    <Import Condition="'$(NetCoreBuild)' == 'true'" Project="$(NETCoreTargetsPath)\Microsoft.Data.Tools.Schema.SqlTasks.targets"/> 
    <Import Condition="'$(NetCoreBuild)' != 'true' AND '$(SQLDBExtensionsRefPath)' != ''" Project="$(SQLDBExtensionsRefPath)\Microsoft.Data.Tools.Schema.SqlTasks.targets"/>
    <Import Condition="'$(NetCoreBuild)' != 'true' AND '$(SQLDBExtensionsRefPath)' == ''" Project="$(MSBuildExtensionsPath)\Microsoft\VisualStudio\v$(VisualStudioVersion)\SSDT\Microsoft.Data.Tools.Schema.SqlTasks.targets"/>
    ```

    2. Package reference

    ```console
    <ItemGroup>
        <PackageReference Condition="'$(NetCoreBuild)' == 'true'" Include="Microsoft.NETFramework.ReferenceAssemblies" Version="1.0.0" PrivateAssets="All"/>
    </ItemGroup>
    ```

    3. Clean target, necessary for supporting dual editing in SQL Server Data Tools (SSDT) and Azure Data Studio

    ```console
    <Target Name="AfterClean">
        <Delete Files="$(BaseIntermediateOutputPath)\project.assets.json"/>
    </Target>
    ```

### Build the project from the command line

From the full .NET folder, use the following command:

```console
dotnet build "<sqlproj file path>" /p:NetCoreBuild=true /p:NETCoreTargetsPath="<DotNet Core build folder>"
```

For example, from `/usr/share/dotnet` on Linux:

```console
dotnet build "/home/myuser/Documents/DatabaseProject1/DatabaseProject1.sqlproj" /p:NetCoreBuild=true /p:NETCoreTargetsPath="/home/myuser/.azuredatastudio/extensions/microsoft.sql-database-projects-x.x.x/BuildDirectory"  
```

## Next steps

- [SQL Database Projects extension](sql-database-project-extension.md)
- [SDK-style SQL projects in SQL Database Projects extension](sql-database-project-extension-sdk-style-projects.md)
- [Publish SQL database projects](sql-database-project-extension-build.md#publish-a-database-project)
