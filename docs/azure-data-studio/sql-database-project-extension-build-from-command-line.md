---
title: Build a Project from the Command Line
description: Build a SQL Server Database Projects from the command line
ms.custom: "seodec18"
ms.date: "08/07/2020"
ms.reviewer: "drskwier, maghan, sstein"
ms.prod: sql
ms.technology: azure-data-studio
ms.topic: conceptual
author: "dzsquared"
ms.author: "drskwier"
---
# Build a Database Project from Command Line

The build process in the SQL Database Projects extension for Azure Data Studio allows for *dacpac* creation in Windows, macOS, and Linux environments. 

## Prerequisites
1. Install and configure [SQL Database Projects extension for Azure Data Studio](sql-database-project-extension.md).

2. The following .NET Core dlls and the target file `Microsoft.Data.Tools.Schema.SqlTasts.targets` are required to build a SQL database project from the command line from all platforms supported by the Azure Data Studio extension for SQL Database Projects.
- Microsoft.Data.Tools.Schema.Sql.dll

- Microsoft.Data.Tools.Schema.Tasks.Sql.dll

- Microsoft.Data.Tools.Utilities.dll 

- Microsoft.SqlServer.Dac.dll 

- Microsoft.SqlServer.Dac.Extensions.dll 

- Microsoft.SqlServer.TransactSql.ScriptDom.dll 

- Microsoft.SqlServer.Types.dll 

- Microsoft.Data.Tools.Schema.SqlTasks.targets 

- System.ComponentModel.Composition.dll 

- Microsoft.Data.SqlClient.dll 

These files are created by the extension during the first build completed in the Azure Data Studio interface and placed in the extension's folder under `BuildDirectory`.  For example, on Linux, these files are placed in `~\.azuredatastudio\extensions\microsoft.sql-database-projects-x.x.x\BuildDirectory\`.

Copy these ten files to a new and accesible folder or note their location.  This location will be referred to as `DotNet Core build folder` in this document.

3. If the project was created in Azure Data Studio - skip ahead to [Build the project from the command line](#Build-the-project-from-the-command-line). If the project was created in SSDT, open the project in the Azure Data Studio SQL Database project extension.  This automatically updates the `sqlproj` file with 3 edits, noted below for your information:
    1. Import conditions 
    ```
    <Import Condition="'$(NetCoreBuild)' == 'true'" Project="$(NETCoreTargetsPath)\Microsoft.Data.Tools.Schema.SqlTasks.targets"/> 
    <Import Condition="'$(NetCoreBuild)' != 'true' AND '$(SQLDBExtensionsRefPath)' != ''" Project="$(SQLDBExtensionsRefPath)\Microsoft.Data.Tools.Schema.SqlTasks.targets"/> 
    <Import Condition="'$(NetCoreBuild)' != 'true' AND '$(SQLDBExtensionsRefPath)' == ''" Project="$(MSBuildExtensionsPath)\Microsoft\VisualStudio\v$(VisualStudioVersion)\SSDT\Microsoft.Data.Tools.Schema.SqlTasks.targets"/> 
    ```
    2. Package reference
    ```
    <ItemGroup> 
        <PackageReference Condition="'$(NetCoreBuild)' == 'true'" Include="Microsoft.NETFramework.ReferenceAssemblies" Version="1.0.0" PrivateAssets="All"/> 
    </ItemGroup> 
    ```
    3. Clean target, necessary for supporting dual editing in SQL Server Data Tools (SSDT) and Azure Data Studio
    ```
    <Target Name="AfterClean"> 
        <Delete Files="$(BaseIntermediateOutputPath)\project.assets.json"/> 
    </Target> 
    ```

## Build the project from the command line
From the full .Net folder, use the following command:
```
dotnet build "<sqlproj file path>" /p:NetCoreBuild=true /p:NETCoreTargetsPath="<DotNet Core build folder>"
```
For example, from `/usr/share/dotnet` on Linux:
```
dotnet build "/home/myuser/Documents/DatabaseProject1/DatabaseProject1.sqlproj" /p:NetCoreBuild=true /p:NETCoreTargetsPath="/home/myuser/.azuredatastudio-insiders/extensions/microsoft.sql-database-projects-0.1.2/BuildDirectory"  
```
## Next steps

- [SQL Database Projects extension for Azure Data Studio](sql-database-project-extension.md)
- [Publish SQL database projects](sql-database-project-extension-build.md#publish-a-database-project)
