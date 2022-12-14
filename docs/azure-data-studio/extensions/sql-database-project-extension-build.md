---
title: Build and Publish a Project
description: Build and Publish with SQL Server Database Projects extension
author: dzsquared
ms.author: drskwier
ms.reviewer: maghan
ms.date: 10/27/2021
ms.service: azure-data-studio
ms.topic: conceptual
---

# Build and Publish a project

The build process in the SQL Database Projects extension (preview) for Azure Data Studio and VS Code allows for *dacpac* creation in Windows, macOS, and Linux environments. The project can be deployed to a local or cloud environment with the publish process.

## Prerequisites

- Install and configure [SQL Database Projects extension for Azure Data Studio or VS Code](sql-database-project-extension.md).

## Build a Database Project

 In the **Projects** viewlet right-click the *.sqlproj* root node and select **Build**.

 The output pane will automatically appear with the output from the build process.  A successful build will conclude with the message: 

 ``` ... exited with code: 0 ```

## Publish a Database Project

### Publish to an existing SQL instance 

*This functionality is available in Azure Data Studio and VS Code, however the interfaces will appear slightly different between the 2 applications.*

After a project is successfully compiled through the [build process](#build-a-database-project), the database can be published to a SQL Server instance. To publish a database project, in the **Projects** viewlet right-click the *.sqlproj* root node and select **Publish**.

In the **Publish Database** dialog that appears, specify a server connection and the database name to be created.

### Publish the SQL project and deploy to a local Container

*This functionality is currently only available in VS Code.*

After a project is successfully compiled through the [build process](#build-a-database-project), the database can be published to a new development instance of SQL Server in a local container. To publish a database project to a local container, in the **Projects** viewlet right-click the *.sqlproj* root node and select **Publish**. 

In VS Code, the options for publish appear as:

* Publish to existing server
* Publish to new server in a container

To create a new container with a development SQL instance and publish the SQL project contents to it, select the option "Publish to new server in a container".

Creating a new container exposes the following options:

* SQL Server port number: the port on which the SQL server's 1433 port will be forwarded to your workstation
* SQL Server admin password: the *sa* password for the new instance
* SQL Server docker image: the version base of the container

## Next steps

- [SQL Database Projects extension](sql-database-project-extension.md)
- [Build SQL database projects from command line](sql-database-project-extension-build-from-command-line.md)
