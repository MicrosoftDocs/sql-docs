---
title: What is the local development experience for Azure SQL Database?
titleSuffix: Azure SQL Database
description: Learn about the local development experience for Azure SQL Database.
services: sql-database
ms.service: sql-database
author: scoriani
ms.author: scoriani
ms.reviewer: mathoma
ms.topic: overview 
ms.date: 05/05/2022
---

# What is the local development experience for Azure SQL Database?

The Azure SQL Database local development experience is a combination of tools and procedures that empowers application developers and database professionals to design, edit, build/validate, publish, and run database schemas for databases directly on their workstation using an Azure SQL Database containerized environment.

The Azure SQL Database local development experience consists of extensions for VSCode and Azure Data Studio that allow users to source control Database Projects and work offline when needed. The local development experience uses the [Azure SQL Database emulator](local-dev-experience-azure-sql-database-emulator.md), a containerized database with close fidelity with the Azure SQL Database public service, as runtime host for Database Projects that can be published and tested locally as part of developer's inner loop.

   > [!NOTE] 
   > We are planning to evolve this local development experience for Azure SQL Database over time to cover new scenarios and use cases, simplifying the process of developing and testing applications targeting Azure SQL Database. You can find out more about future plans in this space by reading [this blog post on Azure SQL Database Devs’ Corner](https://aka.ms/sql-db-local-dev-experience-plan).

Once a Database Project is ready to be deployed, the local development experience enables developers to publish the database directly to the global Azure SQL Database service, or push the Database Project to a Git repository where additional steps of a deployment pipeline can be triggered as part of the developer's outer loop.

A common example would be pushing the project to a GitHub repository that leverages GitHub Actions to automate database creation or apply schema changes to a database in Azure SQL Database. The Azure SQL Database emulator itself can also be used as part of Continuous Integration and Continuous Deployment (CI/CD) processes to automate database validation and testing.

![Diagram of the Azure SQL Database local development experience end-to-end workflow.](./media/local-dev-experience-overview/azure-sql-db-local-dev.png)

## VSCode and Azure Data Studio extensions

To use the Azure SQL Database local development experience, install the appropriate extension depending on whether you are using [VSCode](https://code.visualstudio.com/Docs) or [Azure Data Studio](/sql/azure-data-studio/download-azure-data-studio).


| Extension | Description | VSCode | Azure Data Studio |
|--|--|--|--|
| The [mssql extension for VSCode](https://marketplace.visualstudio.com/items?itemName=ms-mssql.mssql) | Enables you to connect and run queries and testing scripts against a database. The database may be running in the Azure SQL Database emulator locally, or it may be a database in the global Azure SQL Database service. | Install the mssql extension. | There is no need to install the mssql extension because this functionality is provided natively by Azure Data Studio. |
| [SQL Database Projects extension (Preview)](https://marketplace.visualstudio.com/items?itemName=ms-mssql.sql-database-projects-vscode) | Enables you to capture an existing database schema and/or design new database objects using a declarative database design model. You can commit database schema to version control. You can also publish database schema to a database running in the Azure SQL Database emulator, or to a database running in the global Azure SQL Database service. You may publish an entire database, or incremental changes to a database. | This extension is bundled into the mssql extension for VS Code and will be installed or updated automatically when that extension is updated or installed. | Install the SQL Database Projects extension. |

Learn more in [VSCode and Azure Data Studio extensions for the Azure SQL Database local development experience](local-dev-experience-extensions.md).

## Azure SQL Database emulator

The Azure SQL Database emulator is a containerized database with close fidelity with the Azure SQL Database public service. Application developers and database professionals can pull the Azure SQL Database emulator from an image in the Microsoft Container registry and run it on their own workstation. The Azure SQL Database emulator enables faster local and offline development workflows for Azure SQL Database.

The Azure SQL Database emulator can also be used as part of local or hosted CI/CD pipelines to support unit and integration testing, without the need to use the global Azure SQL Database cloud service.

Learn more in [Introducing the Azure SQL Database emulator](local-dev-experience-azure-sql-database-emulator.md).

## Next steps

Learn more about the local development experience for Azure SQL Database:

- [Set up a local development environment for Azure SQL Database](local-dev-experience-set-up-dev-environment.md)
- [Create a Database Project for a local Azure SQL Database development environment](local-dev-experience-create-database-project.md)
- [Publish a Database Project for Azure SQL Database to the local emulator](local-dev-experience-publish-emulator.md)
- [Quickstart: Create a local development environment for Azure SQL Database](local-dev-experience-quickstart.md)
- [VSCode and Azure Data Studio extensions for the Azure SQL Database local development experience](local-dev-experience-extensions.md)
- [Introducing the Azure SQL Database emulator](local-dev-experience-azure-sql-database-emulator.md)