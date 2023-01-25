---
title: Use SDK-style projects with the SQL Database Projects extension
description: Getting started using SDK-style SQL projects with the SQL Database Projects extension for Azure Data Studio or VS Code
author: dzsquared
ms.author: drskwier
ms.reviewer: maghan
ms.date: 5/24/2022
ms.service: azure-data-studio
ms.topic: conceptual
ms.custom: intro-get-started
---

# Use SDK-style SQL projects with the SQL Database Projects extension (Preview)

This article introduces [Microsoft.Build.Sql](https://www.nuget.org/packages/Microsoft.Build.Sql) for SDK-style SQL projects  in the SQL Database Projects extension in Azure Data Studio or VS Code. SDK-style SQL projects are especially advantageous for applications shipped through pipelines or built in cross-platform environments.  The initial announcement is available in [TechCommunity](https://techcommunity.microsoft.com/t5/azure-sql-blog/microsoft-build-sql-the-next-frontier-of-sql-projects/ba-p/3290628).

> [!NOTE]
> The SDK-style SQL projects is currently in preview. 

## Create an SDK-style database project

You can create an SDK-style database project from a blank project, or from an existing database. 

### Blank project

In the **Projects** view, select the **New Project** button and enter a project name in the text input that appears.  In the **Select a Folder** dialog box that appears, choose a directory for the project's folder, `.sqlproj` file, and other contents to reside in.  

By default the selection for **SDK-style project (Preview)** is checked. When the dialog is completed, the empty project is opened and visible in the **Projects** view for editing.

### From an existing database

In the **Project** view, select the **Import Project from Database** button and connect to a SQL Server.  Once the connection is established, select a database from the list of available databases and set the name of the project.  Select a target structure of the extraction.  

By default the selection for **SDK-style project (Preview)** is checked. When the dialog is completed, the new project is opened and contains SQL scripts for the contents of the selected database.

## Build and publish

From the Azure Data Studio and VS Code interfaces, building and publishing an SDK-style SQL project is completed in the same way as the previous SQL project format. For more on this process, see [Build and Publish a Project](sql-database-project-extension-build.md).

To build an SDK-style SQL project from the command line on Windows, macOS, or Linux, use the following command:

```bash
dotnet build /p:NetCoreBuild=true
```

The `.dacpac` resulting from building an SDK-style SQL project is compatible with tooling associated with the data-tier application framework (`.dacpac`, `.bacpac`), including [SqlPackage](../../tools/sqlpackage/sqlpackage-publish.md).


## Next steps

- [Build and Publish a project with the SQL Database Projects extension](sql-database-project-extension-build.md)
