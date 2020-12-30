---
title: SQL Database Projects extension
description: Install and use the SQL Database Projects extension for Azure Data Studio.
ms.prod: azure-data-studio
ms.technology: azure-data-studio
ms.topic: conceptual
author: dzsquared
ms.author: drskwier
ms.reviewer: maghan
ms.custom: 
ms.date: 12/15/2020
---

# SQL Database Projects extension (Preview)

The SQL Database Projects extension (preview) is an extension for developing SQL databases in a project-based development environment. 


## Features

1. Create project from a connected database.
2. Create a new blank project.
3. Open a Project previously created in [Azure Data Studio](sql-database-project-extension-getting-started.md) or in [SQL Server Data Tools](../../ssdt/sql-server-data-tools.md).
4. Edit project by adding or removing Table, View, Stored Procedure, or custom scripts in the project.
5. Organize files/scripts in folders.
6. Add references to system databases or user dacpac.
7. Build single project.
8. Deploy single project.
9. Load connection details (SQL Windows authentication) and SQLCMD variables from deployment profile.

Watch this short 10-minute video for an introduction to the SQL Database Projects extension in Azure Data Studio:

> [!VIDEO https://channel9.msdn.com/Shows/Data-Exposed/Build-SQL-Database-Projects-Easily-in-Azure-Data-Studio/player?WT.mc_id=dataexposed-c9-niner]

## Install the SQL Database Projects extension

1. Open the extensions manager to access the available extensions.  To do so, either select the extensions icon or select **Extensions** in the **View** menu.
2. Identify the *SQL Database Projects* extension by typing all or part of the name in the extension search box. Select an available extension to view its details.

   ![Install extension](media/sql-database-projects-extension/install-database-projects.png)

3. Select the extension you want and **Install** it.
4. Select **Reload** to enable the extension (only required the first time you install an extension).
5. Select the files icon from the activity bar or select **Explorer** from the **View** menu. A new viewlet for **Projects** is now available.

   > [!NOTE]
   > The .NET Core SDK is required for project build functionality and you will be prompted to install the .NET Core SDK if it cannot be detected by the extension.  The .NET Core SDK (v3.1 or higher) can be downloaded and installed from [https://dotnet.microsoft.com/download/dotnet-core/3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1).

   > [!NOTE]
   > It is recommended to install the [Schema Compare extension](schema-compare-extension.md) alongside the SQL Database Projects extension for full functionality.

## Known limitations

- Loading files as link is not supported in Azure Data Studio viewlet today, however the files will be loaded at the top level in tree and build will incorporate these files as expected.
- SQLCLR objects in project are not supported in .NET Core version of DacFx.
- Tasks (build/publish) are not user-defined.
- Publish targets defined by DacFx.
- WSL environment support is limited.

## Workspace
SQL database projects in Azure Data Studio are contained within a logical workspace.  A workspace manages the folder(s) visible in the Explorer pane as well as the project(s) visible in the Project pane. Adding and removing projects from a workspace can be accomplished through the Azure Data Studio interface in the Projects pane. However, the settings for a workspace can be manually edited in the `.code-workspace` file if necessary.

In the example `.code-workspace` file below, the `folders` array lists all folders included in the Explorer pane and the `dataworkspace.projects` array within `settings` lists all the SQL projects included in the Projects pane.

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
		"dataworkspace.projects": [
			"AdventureWorksLT.sqlproj",
			"..\\WideWorldImportersDW\\WideWorldImportersDW.sqlproj"
		]
	}
}
```

## Next steps

- [Getting Started with the SQL Database Projects extension](sql-database-project-extension-getting-started.md)
- [Build and Publish a project with SQL Database Projects extension for Azure Data Studio](sql-database-project-extension-build.md)
