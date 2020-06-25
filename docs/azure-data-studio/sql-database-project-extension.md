---
title: SQL Database Projects extension
description: Install and use the SQL Database Projects extension (preview) for Azure Data Studio
ms.custom: "seodec18"
ms.date: "06/25/2020"
ms.reviewer: "drskwier, maghan, sstein"
ms.prod: sql
ms.technology: azure-data-studio
ms.topic: conceptual
author: "dzsquared"
ms.author: "drskwier"
---
# SQL Database Projects extension (preview)

The SQL Database Projects extension (preview) is an extension for developing SQL databases in a project-based development environment. This extension is currently in preview and is available in the [Azure Data Studio Insiders Build](https://github.com/microsoft/azuredatastudio#try-out-the-latest-insiders-build-from-main).


## Install the SQL Database Projects extension

1. To open the extensions manager and access the available extensions, select the extensions icon, or select **Extensions** in the **View** menu.
2. Identify the *SQL Database Projects* extension by typing all or part of the name in the extension search box. Select an available extension to view its details.

   ![Install extension](media/extensions/sql-database-projects-extension/install-database-projects.png)

3. Select the extension you want and **Install** it.
4. Select **Reload** to enable the extension (only required the first time you install an extension).
5. Select the files icon from the activity bar or select **Explorer** from the **View** menu. A new viewlet for **Projects** is now available.


## Getting Started with Database Projects

* Create a new database project by going to the **Projects** viewlet under Explorer, or by searching for **New Database Project** in the command palette.
* Existing database projects can be opened via **Open Database Project** in the command palette.
* Start from an existing database by using **Import New Database Project** from the command palette.

   ![New viewlet](media/extensions/sql-database-projects-extension/projects-viewlet.png)


### Create an Empty Database Project

 In the **Projects** viewlet under **Explorer**, click the **New Project** button and enter a project name in the text input that appears.  In the "Select a Folder" dialog that appears, select a directory for the project's folder, .sqlproj file, and other contents to reside in.
 The empty project is opened and visible in the **Projects** viewlet for editing.

### Open an Existing Project

In the **Projects** viewlet, click the **Open Project** button and open an existing *.sqlproj* file from the file picker that appears. Existing projects can originate from Azure Data Studio or [Visual Studio SQL Server Data Tools](../ssdt/sql-server-data-tools.md).

The existing project is opened and its contents are visible in the **Projects** viewlet for editing.

### Create a Database Project from an Existing Database

In the **Project** viewlet, click the **Import Project from Database** button and connect to a SQL Server.  Once the connection is established, select a database from the list available databases and set the name of the project.

Finally, select a target structure of the extraction.  The new project is opened and contains the Data Definition Language (DDL) contents of the selected database.

