---
title: SQL Database Projects extension
description: Install and use the SQL Database Projects extension for Azure Data Studio and Visual Studio Code.
author: dzsquared
ms.author: drskwier
ms.reviewer: maghan
ms.date: 4/12/2023
ms.service: azure-data-studio
ms.topic: conceptual
---

# SQL Database Projects extension

The SQL Database Projects extension is an Azure Data Studio and Visual Studio Code extension for developing SQL databases in a project-based development environment. Compatible databases include SQL Server, Azure SQL Database, Azure SQL Managed Instance, and Azure Synapse SQL.  A SQL project is a local representation of SQL objects that comprise the schema for a single database, such as tables, stored procedures, or functions. When a SQL Database project is built, the output artifact is a *.dacpac* file. New and existing databases can be updated to match the contents of the *.dacpac* by publishing the SQL Database project with the SQL Database Projects extension or by publishing the *.dacpac* with the command line interface [SqlPackage](../../tools/sqlpackage/sqlpackage-publish.md).

:::image type="content" source="media/sql-database-projects-extension/sqlproj-summary.png" alt-text="Summary of SQL Database Projects containing pre-deployment and post-deployment scripts as well as database objects." border="true":::


## Extension features

The SQL Database Projects extension provides the following features: 

- Create a new blank project.
- Create a new project from a connected database.
- Open a project previously created in [Azure Data Studio, Visual Studio Code](sql-database-project-extension-getting-started.md) or in [SQL Server Data Tools](../../ssdt/sql-server-data-tools.md).
- Edit a project by adding or removing objects (tables, views, stored procedures) or custom scripts in the project.
- Organize files/scripts in folders.
- Add references to system databases or a user dacpac.
- Build a single project.
- Deploy a single project.
- Load connection details (SQL Windows authentication) and SQLCMD variables from deployment profile.

The following features in the SQL Database Projects extension are currently in preview:

- Create new projects from an [OpenAPI](https://github.com/OAI/OpenAPI-Specification) specification file.
- SDK-style SQL projects ([Microsoft.Build.Sql](https://www.nuget.org/packages/Microsoft.Build.Sql/)).

Watch this short 10-minute video for an introduction to the SQL Database Projects extension in Azure Data Studio:

> [!VIDEO https://channel9.msdn.com/Shows/Data-Exposed/Build-SQL-Database-Projects-Easily-in-Azure-Data-Studio/player?WT.mc_id=dataexposed-c9-niner]

## Install

You can install the SQL Database Project extension in Azure Data Studio and Visual Studio Code. 

### Azure Data Studio

To install the SQL Database Project extension in Azure Data Studio, follow these steps: 

1. Open the extensions manager to access the available extensions.  To do so, either select the extensions icon or select **Extensions** in the **View** menu.
1. Identify the *SQL Database Projects* extension by typing all or part of the name in the extension search box. Select an available extension to view its details.

   ![Screenshot of Azure Data Studio, Install extension.](media/sql-database-projects-extension/install-database-projects.png)

1. Select the extension you want and choose to **Install** it.
1. Select **Reload** to enable the extension (only required the first time you install an extension).
1. Select the **Projects** icon from the activity bar.


> [!NOTE]
> - It is recommended to install the [Schema Compare extension](schema-compare-extension.md) alongside the SQL Database Projects extension for full functionality.



### Visual Studio Code

The SQL Database Projects extension is installed with the [mssql](../../tools/visual-studio-code/sql-server-develop-use-vscode.md) extension for Visual Studio Code.

## Dependencies

The SQL Database Projects extension has a dependency on the .NET SDK (required) and AutoRest.Sql (optional).

### .NET SDK

The .NET SDK is required for project build functionality and you are prompted to install the .NET SDK if a supported version can't be detected by the extension.  The .NET SDK can be downloaded and installed for [Windows, macOS, and Linux](https://aka.ms/sqlprojects-dotnet). 

If you would like to [check currently installed versions](/dotnet/core/install/how-to-detect-installed-versions) of the dotnet SDK, open a terminal and run the following command:

```dotnetcli
dotnet --list-sdks
```

After installing the .NET SDK, your environment is ready to use the SQL Database Projects extension.

#### Common issues

**Nuget.org missing from the list of sources may result in error messages such as:**
- `error MSB4236: The SDK 'Microsoft.Build.Sql/0.1.9-preview' specified could not be found.`
- `Unable to find package Microsoft.Build.Sql. No packages exist with this id in source(s): Microsoft Visual Studio Offline Packages`

To check if nuget.org is registered as a source, run `dotnet nuget list source` from the command line and review the results for an `[Enabled]` item referencing nuget.org. If nuget.org is not registered as a source, run `dotnet nuget add source https://api.nuget.org/v3/index.json -n nuget.org`.

**Unsupported .NET SDK versions may result in error messages such as:**
- `error MSB4018: The "SqlBuildTask" task failed unexpectedly.`
- `error MSB4018: System.TypeInitializationException: The type initializer for 'SqlSchemaModelStaticState' threw an exception. ---> System.IO.FileNotFoundException: Could not load file or assembly 'System.Runtime, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'. The system cannot find the file specified. [c:\Users\ .sqlproj]_` (where the linked non-existing file has an unmatched closing square bracket). 

To force the SQL Database Projects extension to use the v6.x version of the .NET SDK when multiple versions are installed, add a [global.json](/dotnet/core/tools/global-json) file to the folder that contains the SQL project. 


### AutoRest.Sql

The SQL extension for [AutoRest](https://github.com/Azure/autorest) is automatically downloaded and used by the SQL Database Projects extension when a SQL project is generated from an OpenAPI specification file.


## Limitations

Currently, the SQL Database Project extension has the following limitations: 

- Tasks (build/publish) aren't user-defined.
- SQLCLR objects in projects aren't supported.
- Code analysis rules on projects aren't supported at this time.

## Workspace

SQL database projects are contained within a logical workspace in Azure Data Studio and Visual Studio Code. A workspace manages the folder(s) visible in the Explorer pane. **All SQL projects within the folders open in the current workspace are available in the SQL Database Projects view by default.**  

You can manually add and remove projects from a workspace through the interface in the **Projects** pane. The settings for a workspace can be manually edited in the `.code-workspace` file,  if necessary.

In the following example `.code-workspace` file, the `folders` array lists all folders included in the Explorer pane and the `dataworkspace.excludedProjects` array within `settings` lists all the SQL projects excluded from the **Projects** pane.

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
