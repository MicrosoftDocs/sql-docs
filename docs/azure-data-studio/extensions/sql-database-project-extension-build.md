---
title: Build and Publish a Project
description: Build and Publish with SQL Server Database Projects extension
author: dzsquared
ms.author: drskwier
ms.reviewer: maghan
ms.date: 4/12/2023
ms.service: azure-data-studio
ms.topic: conceptual
---

# Build and Publish a project

The build process in the SQL Database Projects extension for Azure Data Studio and VS Code allows for *dacpac* creation in Windows, macOS, and Linux environments. The project can be deployed to a local or cloud environment with the publish process.

## Prerequisites

- Install and configure [SQL Database Projects extension for Azure Data Studio or VS Code](sql-database-project-extension.md).

## Build a Database Project

 In the **Database Projects** view, right-click the database project's root node and select **Build**.

 The output pane automatically appears with the output from the build process.  A successful build concludes with the message: 

 ``` ... exited with code: 0 ```

## Publish a Database Project

### Publish to an existing SQL instance 

*This functionality is available in Azure Data Studio and VS Code, however the interfaces are slightly different between the two applications.*

After a project is successfully compiled through the [build process](#build-a-database-project), the database can be published to a SQL instance. Platform compatibility is determined by the [target platform](sql-database-project-extension-sdk-style-projects.md#target-platform) of a SQL project, and includes SQL Server and Azure SQL options.

To publish a database project, in the **Database Projects** view right-click the database project's root node and select **Publish**.

In the **Publish Database** dialog that appears, specify a server connection and the database name to be created.

### Publish the SQL project and deploy to a local Container

After a project is successfully compiled through the [build process](#build-a-database-project), the database can be published to a new development instance of SQL Server in a local container. To publish a database project to a local container, in the **Database Projects** view right-click the database project's root node and select **Publish**. 

In projects targeting SQL Server, the options for publish appear as:

* Publish to existing SQL server
* Publish to new SQL server local development container

In projects targeting Azure SQL Database, the options for publish appear as:

* Publish to an existing Azure SQL server
* Publish to new Azure SQL server local development container (Preview)

To create a new container with a development SQL instance and publish the SQL project contents to it, select the option "Publish to new server in a container", or "Publish to new Azure SQL server local development container (Preview)".

:::image type="content" source="media/sql-database-projects-extension/publish-to-container.png" alt-text="Screenshot of publish to container dialog in Azure Data Studio.":::

Creating a new container exposes the following options:

* SQL Server port number: the port on which the SQL server's 1433 port is forwarded to your workstation
* SQL Server admin password: the *sa* password for the new instance
* SQL Server docker image: the version base of the container

For more information on the Azure SQL local development container, see [Azure SQL Database Emulator](/azure/azure-sql/database/local-dev-experience-sql-database-emulator).

For more information on SQL Server in containers, see [Configure and customize SQL Server Docker containers](../../linux/sql-server-linux-docker-container-configure.md).

## Next steps

- [SQL Database Projects extension](sql-database-project-extension.md)
- [Build SQL database projects from command line](sql-database-project-extension-build-from-command-line.md)
