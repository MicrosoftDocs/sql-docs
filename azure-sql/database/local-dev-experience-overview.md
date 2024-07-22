---
title: What is the local development experience for Azure SQL Database?
titleSuffix: Azure SQL Database
description: Learn about the local development experience for Azure SQL Database.
author: croblesm
ms.author: roblescarlos
ms.reviewer: wiassaf
ms.date: 07/22/2024
ms.service: sql-database
ms.topic: overview
---

# What is the local development experience for Azure SQL Database?
[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

This article provides an overview of the local development experience tailored for Azure SQL Database.

## Overview

The Azure SQL Database local development experience is a combination of tools and services that empowers application developers and database professionals to design, build, validate, and publish database schemas for databases while working offline.

The following diagram illustrates the inner and outer loop processes in the development lifecycle, highlighting how developers interact with Azure SQL Database both locally and in the cloud:

![Diagram of the Azure SQL Database local development experience end-to-end workflow.](./media/local-dev-experience-overview/azure-sql-db-local-dev.png)

Let's understand both the inner and outer loop in detail:

### Inner Loop

- The inner loop represents the local development cycle. It's where developers write code, test it, and debug it on their local machines.

- During this phase, developers interact with a local instance of SQL Database. This allows them to quickly iterate on their code with immediate feedback, without needing to deploy changes to the cloud.

- The goal of the inner loop is to enable rapid development and testing in an isolated environment that closely mimics the production setup.

### Outer Loop

- The outer loop encompasses the broader development lifecycle, including collaboration, continuous integration, and deployment to production.  

- Changes made in the inner loop are pushed to a shared repository, such as a GitHub repository, where they can be reviewed, tested, and merged.

- From the repository, automated workflows (for example, through GitHub Actions or GitHub Codespaces) build and deploy the application, including any database changes, to Azure services.

- In the outer loop, the application and database are deployed to Azure, where they can be accessed and used in a live environment.

## Tools and extensions

To streamline the local development experience, several tools and extensions are available. The following table provides a high-level overview of these options, each tailored for different aspects of the development lifecycle.

| Tool/Extension | Description | Compatible with |
|--|--|--|
| [**Azure SQL Database Dev Container Templates**](https://aka.ms/azuresql-devcontainers-repo) | Provides preconfigured development environments, eliminating manual setup and ensuring consistency. Supports multiple popular languages. | [Visual Studio Code](https://marketplace.visualstudio.com/items?itemName=ms-vscode-remote.remote-containers)<br>[GitHub Codespaces](https://docs.github.com/en/codespaces/setting-up-your-project-for-codespaces/adding-a-dev-container-configuration/introduction-to-dev-containers) |
| [**MSSQL Extension**](/sql/tools/visual-studio-code/sql-server-develop-use-vscode) | Enables connection, query execution, and script testing against a database, whether local or in Azure SQL Database. | [Visual Studio Code](https://marketplace.visualstudio.com/items?itemName=ms-mssql.mssql)<br>GitHub Codespaces |
| [**SQL Database Projects Extension**](/azure-data-studio/extensions/sql-database-project-extension) | Allows capturing existing database schemas, designing new objects using a declarative model, committing schemas to version control, and publishing to databases. | [Visual Studio Code](https://marketplace.visualstudio.com/items?itemName=ms-mssql.sql-database-projects-vscode)<br>[Azure Data Studio](https://github.com/microsoft/azuredatastudio/tree/main/extensions/sql-database-projects#readme)<br>GitHub Codespaces |

## Azure SQL Database Dev Container Templates

Dev Containers offer a streamlined, preconfigured development environment for Azure SQL Database, eliminating the need for manual setup. These containers enhance productivity by ensuring that all necessary tools and dependencies are available right from the start.

Developers can utilize Dev Containers to quickly start coding in environments that mimic the Azure SQL Database setup, promoting consistency across local and cloud development. This approach not only accelerates the development process but also reduces errors and ensures a smooth transition from local development to production deployment. 

> [!NOTE]
> Dev Containers can run both locally in VS Code using the [Dev Containers extension](https://marketplace.visualstudio.com/items?itemName=ms-vscode-remote.remote-containers) and in the cloud using [GitHub Codespaces](https://docs.github.com/en/codespaces/setting-up-your-project-for-codespaces/adding-a-dev-container-configuration/introduction-to-dev-containers).

To learn more about the Azure SQL Database Dev Container templates, see [Azure SQL Database Dev Container templates](./local-dev-experience-dev-containers.md).

## Related content

Learn more about the local development experience for Azure SQL Database:

- [Overview - Azure SQL Database Dev Container templates](./local-dev-experience-dev-containers.md)
- [Quickstart: Set up a development environment with an Azure SQL Database Dev Container template](./local-dev-experience-dev-containers-quickstart.md)
- [Quickstart: Create a local development environment for Azure SQL Database](./local-dev-experience-quickstart.md)
