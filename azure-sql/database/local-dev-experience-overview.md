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
ms.date: 04/29/2022
---

# What is the local development experience for Azure SQL Database?

The Azure SQL Database local development experience is a combination of tools and procedures that empowers application developers and database professionals to design, edit, build/validate, publish, and run database schemas for databases directly on their workstation using an Azure SQL Database containerized environment. The Azure SQL Database local development experience consists of extensions for VSCode and Azure Data Studio that allow users to source control database projects and work offline when needed. The local development experience uses the Azure SQL Database emulator, a containerized database with close fidelity with the Azure SQL Database public service, as runtime host for database projects that can be published and tested locally as part of developer's inner loop. Once database project is ready to be deployed, the local development experience enables developers to publish database directly to the public Azure SQL Database service, or push the database project to a Git repository where additional steps of a deployment pipeline can be triggered as part of developer's outer loop. A common example would be pushing the project to a GitHub repository that leverages GitHub Actions to automate the database creation or schema changes to a Azure SQL Database. The Azure SQL Database emulator itself can also be used as part of Continuous Integration and Continuous Deployment (CI/CD) processes to automate database validation and testing.

![](./media/local-dev-experience-overview/azure-sql-db-local-dev.png)

## VSCode and Azure Data Studio extensions

The Azure SQL Database local development experience utilizes two extensions. The extensions are available for both [VSCode](https://code.visualstudio.com/Docs) and [Azure Data Studio](/sql/azure-data-studio).

The **Azure SQL Database extension** enables you to connect and run queries and testing scripts against a database. The database may be running in the Azure SQL Database emulator locally, or it may be a database in the global Azure SQL Database service.

The **SQL Database Projects extension** enables you to capture an existing database schema and/or design new database objects using a declarative database design model. You can commit database schema to version control. You can also publish database schema to a database running in the Azure SQL Database emulator, or to a database running in the global Azure SQL Database service. You may publish an entire database, or incremental changes to a database.

Learn more in [VSCode and Azure Data Studio extensions for the Azure SQL Database local development experience](local-dev-experience-extensions.md).

## Azure SQL Database emulator

The Azure SQL Database emulator is a containerized database with close fidelity with the Azure SQL Database public service. Application developers and database professionals can pull the Azure SQL Database emulator from an image in the Microsoft Container registry and run it on their own workstation. The Azure SQL Database emulator enables faster local and offline development workflows for Azure SQL Database.

The Azure SQL Database emulator can also be used as part of local or hosted CI/CD pipelines to support unit and integration testing, without the need to use the global Azure SQL Database cloud service.

Learn more in [Introducing the Azure SQL Database emulator](local-dev-experience-azure-sql-database-emulator.md).

## Next steps

Learn more about the local development experience for Azure SQL Database:

- [Set up a local development environment for Azure SQL Database](local-dev-experience-set-up-dev-environment.md)
- [Create a database project for a local Azure SQL Database development environment](local-dev-experience-create-database-project.md)
- [Publish a database project for Azure SQL Database to the local emulator](local-dev-experience-publish-emulator.md)
- [Quickstart: Create a local development environment for Azure SQL Database](local-dev-experience-quickstart.md)
- [VSCode and Azure Data Studio extensions for the Azure SQL Database local development experience](local-dev-experience-extensions.md)
- [Introducing the Azure SQL Database emulator](local-dev-experience-azure-sql-database-emulator.md)
- [Plan for the Azure SQL Database local development experience](local-dev-experience-plan.md)