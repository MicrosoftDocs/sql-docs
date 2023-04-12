---
title: Getting started with the SQL Database Projects extension
description: Getting started using the SQL Database Projects extension for Azure Data Studio or VS Code
author: dzsquared
ms.author: drskwier
ms.reviewer: maghan
ms.date: 4/12/2023
ms.service: azure-data-studio
ms.topic: conceptual
ms.custom: intro-get-started
---

# Getting started with the SQL Database Projects extension

This article describes three ways to get started with the SQL Database Projects extension:

1. [Create a new database project](#create-an-empty-database-project) by going to the **Database Projects** view or by searching for **Database Projects: New** in the command palette.
2. [Existing database projects](#open-an-existing-project) can be opened via **Database Projects: Open existing** in the command palette.
3. [Start from an existing database](#create-a-database-project-from-an-existing-database) by using **Database Projects: Create Project from Database** from the command palette or by selecting **Create Project from Database** in the **Connections** view.

    ![New viewlet](media/sql-database-projects-extension/projects-viewlet.png)

Once you've created or opened a SQL project, you're ready to start developing with SQL projects.  Some actions you might take are:

- [editing a table in table designer](../build-and-publish-changes-to-table-using-sql-projects.md)
- building and publishing the project
- using schema compare to visualize changes
- updating the project from changes made to a database

## Create an empty database project

In the **Database Projects** view select the **New Project** button and enter a project name in the text input that appears.  In the "Select a Folder" dialog that appears, select a directory for the project's folder, .sqlproj file, and other contents to reside in.
The empty project is opened and visible in the **Database Projects** view for editing.

## Open an existing project

In the **Database Projects** view, select the **Open Project** button and open an existing *.sqlproj* file from the file picker that appears. Existing projects can originate from Azure Data Studio, VS Code or [Visual Studio SQL Server Data Tools](../../ssdt/sql-server-data-tools.md).

The existing project is opened and its contents are visible in the **Database Projects** view for editing.

## Create a database project from an existing database

Instead of starting from an empty project, you can quickly populate a SQL Database Project with the existing objects from a database.

### In Object Explorer

In the **Connections** view, connect to the SQL instance that contains the database to be extracted.  Right-click on the database and select **Create Project from Database** from the context menu.

:::image type="content" source="media/sql-database-projects-extension/create-project-from-database.png" alt-text="Screenshot of create Project from Database dialog.":::

The folder structure setting is set to *Schema/Object Type* by default and offers different ways to automatically organize the existing objects when they are scripted out.  The options for the folder structure setting are:

- File: a single file is created for all the objects
- Flat: a single folder is created for all the objects in individual files
- Object Type: a folder is created per object type and each object is scripted out to a file
- Schema: a folder is created per schema and each object is scripted out to a file
- Schema/Object Type: a folder is created per schema and within the folder a folder is created per object type and each object is scripted out to a file

### In Database Projects view
In the **Project** view select the **Import Project from Database** button and connect to a SQL instance.  Once the connection is established, select a database from the list available databases and set the name of the project.

Finally, select a folder structure of the extraction.  The new project is opened and contains SQL scripts for the contents of the selected database.

## Further actions

### Build and publish

Deploying the database project is achieved in the SQL Database Projects extension by building the project into a [data-tier application file](../../relational-databases/data-tier-applications/data-tier-applications.md) (dacpac) and publishing to a supported platform. For more on this process, see [Build and Publish a Project](sql-database-project-extension-build.md).

### Schema compare

The SQL Database Projects extension interacts with the [Schema Compare extension](schema-compare-extension.md), if installed, to compare the contents of a project to a dacpac, existing database, or another project.  The resulting schema comparison can be used to view and apply the differences from source to target.

:::image type="content" source="media/sql-database-projects-extension/sql-project-schema-compare.png" alt-text="Screenshot of schema compare dialog comparing a SQL project to a database.":::

### Update project from database

If changes have been made to a database that are not yet made to the SQL project, the SQL project can be updated from the state of a database.  This is done by selecting **Update Project from Database** from the context menu of a database in the **Connections** view or from the context menu of a SQL project in the **Database Projects** view.

:::image type="content" source="media/sql-database-projects-extension/update-project-from-database.png" alt-text="Screenshot of update Project from Database dialog.":::


## Next steps

- [Build and Publish a project with SQL Database Projects extension](sql-database-project-extension-build.md)
- [Publish a project with GitHub sql-action](https://github.com/azure/sql-action)
