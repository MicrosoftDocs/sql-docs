---
title: SQL Database Projects extension
description: Install and use the SQL Database Projects extension for Azure Data Studio and VS Code.
ms.prod: azure-data-studio
ms.technology: azure-data-studio
ms.topic: conceptual
author: dzsquared
ms.author: drskwier
ms.reviewer: maghan
ms.custom: 
ms.date: 10/27/2021
---

# SQL Database Projects extension (Preview)

The SQL Database Projects extension (preview) is an Azure Data Studio and VS Code extension for developing SQL databases in a project-based development environment.  A SQL project is a local representation of SQL objects that comprise a single database's schema, such as tables, stored procedures, or functions.


## Extension features

- Create a new blank project.
- Create a new project from a connected database or from an [OpenAPI](https://github.com/OAI/OpenAPI-Specification) specification file.
- Open a Project previously created in [Azure Data Studio, VS Code](sql-database-project-extension-getting-started.md) or in [SQL Server Data Tools](../../ssdt/sql-server-data-tools.md).
- Edit project by adding or removing objects (tables, views, stored procedures) or custom scripts in the project.
- Organize files/scripts in folders.
- Add references to system databases or user dacpac.
- Build single project.
- Deploy single project.
- Load connection details (SQL Windows authentication) and SQLCMD variables from deployment profile.

Watch this short 10-minute video for an introduction to the SQL Database Projects extension in Azure Data Studio:

> [!VIDEO https://channel9.msdn.com/Shows/Data-Exposed/Build-SQL-Database-Projects-Easily-in-Azure-Data-Studio/player?WT.mc_id=dataexposed-c9-niner]

## Installation

### Azure Data Studio

1. Open the extensions manager to access the available extensions.  To do so, either select the extensions icon or select **Extensions** in the **View** menu.
2. Identify the *SQL Database Projects* extension by typing all or part of the name in the extension search box. Select an available extension to view its details.

   ![Install extension](media/sql-database-projects-extension/install-database-projects.png)

3. Select the extension you want and **Install** it.
4. Select **Reload** to enable the extension (only required the first time you install an extension).
5. Select the **Projects** icon from the activity bar.

   > [!NOTE]
   > The .NET Core SDK is required for project build functionality and you will be prompted to install the .NET Core SDK if it cannot be detected by the extension.  The .NET Core SDK (v3.1 or higher) can be downloaded and installed from [https://dotnet.microsoft.com/download/dotnet-core/3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1).

   > [!NOTE]
   > It is recommended to install the [Schema Compare extension](schema-compare-extension.md) alongside the SQL Database Projects extension for full functionality.

### VS Code

The SQL Database Projects extension is installed with the [mssql](../../tools/visual-studio-code/sql-server-develop-use-vscode.md) extension for VS Code.

## Dependencies

### .NET Core SDK
The .NET Core SDK is required for project build functionality and you will be prompted to install the .NET Core SDK if it cannot be detected by the extension.  The .NET Core SDK (v3.1.x) can be downloaded and installed from [https://dotnet.microsoft.com/download/dotnet-core/3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1). If you would like to [check currently installed versions](/dotnet/core/install/how-to-detect-installed-versions) of the dotnet SDK, open a terminal and run the following command.

```dotnetcli
dotnet --list-sdks
```

To force the SQL Database Projects extension to use the v3.1.x version of the .NET Core SDK when multiple versions are installed, add a [package.json](/dotnet/core/tools/global-json?tabs=netcore3x) file to a containing folder for the SQL project.

Unsupported .NET Core SDK versions may result in error messages such as:
- `error MSB4018: The "SqlBuildTask" task failed unexpectedly.`
- ` error MSB4018: System.TypeInitializationException: The type initializer for 'SqlSchemaModelStaticState' threw an exception. ---> System.IO.FileNotFoundException: Could not load file or assembly 'System.Runtime, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'. The system cannot find the file specified. [c:\Users\ .sqlproj]_` (where the linked non-existing file has an unmatched closing square bracket)


### AutoRest.Sql

The SQL extension for [AutoRest](https://github.com/Azure/autorest) is automatically downloaded and used by the SQL Database Projects extension when a SQL project is generated from an OpenAPI specification file.


## Known limitations

- Loading files as link is not supported in Azure Data Studio view today, however the files will be loaded at the top level in tree and build will incorporate these files as expected.
- SQLCLR objects in project are not supported in .NET Core version of DacFx.
- Tasks (build/publish) are not user-defined.
- Publish targets defined by DacFx.
- WSL environment support is limited.

## Workspace
SQL database projects are contained within a logical workspace in Azure Data Studio and VS Code. A workspace manages the folder(s) visible in the Explorer pane. **All SQL projects within the folders open in the current workspace are available in the SQL Database Projects view by default.**  Manually adding and removing projects from a workspace can be accomplished through the interface in the Projects pane. However, the settings for a workspace can be manually edited in the `.code-workspace` file if necessary.

In the example `.code-workspace` file below, the `folders` array lists all folders included in the Explorer pane and the `dataworkspace.excludedProjects` array within `settings` lists all the SQL projects included in the Projects pane.

```json
{
	"folders": [
		{
			"path": "."
		},
		{
			"name": "WideWorldImportersDW",
			"path": "..\\WideWorldImportersDW"
		}
	],
	"settings": {
		"dataworkspace.excludedProjects": [
			"AdventureWorksLT.sqlproj"
		]
	}
}
```

## Next steps

- [Getting Started with the SQL Database Projects extension](sql-database-project-extension-getting-started.md)
- [Build and Publish a project with SQL Database Projects extension](sql-database-project-extension-build.md)