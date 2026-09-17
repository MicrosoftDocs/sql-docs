---
title: What Is the Local Development Experience?
titleSuffix: Azure SQL Database
description: Learn about the local development experience for Azure SQL Database.
author: croblesm
ms.author: roblescarlos
ms.reviewer: wiassaf, randolphwest
ms.date: 09/16/2026
ms.service: azure-sql-database
ms.topic: overview
monikerRange: "=azuresql || =azuresql-db"
---

# What is the local development experience for Azure SQL Database?

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

This article provides an overview of the local development experience tailored for Azure SQL Database.

## Overview

The Azure SQL Database local development experience is a combination of tools and services that empowers application developers and database professionals to design, build, validate, and publish database schemas while working offline.

The following diagram illustrates the inner and outer loop processes in the development lifecycle, highlighting how developers interact with Azure SQL Database both locally and in the cloud:

:::image type="content" source="media/local-dev-experience-overview/azure-sql-db-local-dev.png" alt-text="Diagram of the Azure SQL Database local development experience end-to-end workflow.":::

The following sections describe both the inner and outer loop in detail:

### Inner loop

- The inner loop is the local development cycle, where you write, run, and debug code on your own machine.

- You work against a local database engine that runs in a container, so you get immediate feedback without deploying to Azure.

- You define the database schema in a SQL Database project whose target platform is Azure SQL Database. Building the project fails if the schema uses anything that Azure SQL Database doesn't support, which is how you catch incompatibilities before you deploy. The local engine doesn't enforce that; the project build does.

### Outer loop

- The outer loop encompasses the broader development lifecycle, including collaboration, continuous integration, and deployment to production.

- You push changes made in the inner loop to a shared repository, such as a GitHub repository, where you can review, test, and merge them.

- From the repository, automated workflows (for example, through GitHub Actions or GitHub Codespaces) build and deploy the application, including any database changes, to Azure services.

- In the outer loop, you deploy the application and database to Azure, where they run in a live environment.

## Tools and extensions

Several tools and extensions streamline the local development experience. The following table provides a high-level overview of these options, each addressing different aspects of the development lifecycle.

| Tool or extension | Description | Compatible with |
| --- | --- | --- |
| **[Azure SQL Database Dev Container Templates](https://aka.ms/azuresql-devcontainers-repo)** | Provides preconfigured development environments, eliminating manual setup and ensuring consistency. Supports multiple popular languages. | [Visual Studio Code](https://marketplace.visualstudio.com/items?itemName=ms-vscode-remote.remote-containers)<br />[GitHub Codespaces](https://docs.github.com/codespaces/setting-up-your-project-for-codespaces/adding-a-dev-container-configuration/introduction-to-dev-containers) |
| **[MSSQL extension for Visual Studio Code](/sql/tools/visual-studio-code-extensions/mssql/mssql-extension-visual-studio-code)** | Enables connection, query execution, and script testing against a database, whether local or in Azure SQL Database. | [Visual Studio Code](https://marketplace.visualstudio.com/items?itemName=ms-mssql.mssql)<br />GitHub Codespaces |
| **[SQL Database Projects extension](/sql/tools/sql-database-projects/sql-database-projects#original-projects-vs-sdk-style-projects)** | Capture an existing schema, design objects declaratively, commit the schema to version control, and publish it to a database. Set the project's target platform to Azure SQL Database so that the build fails on anything the service doesn't support. Included in the SQL Server extension pack. | [Visual Studio Code](https://marketplace.visualstudio.com/items?itemName=ms-mssql.mssql)<br />GitHub Codespaces |

## Azure SQL Database Dev Container templates

Dev containers give you a preconfigured development environment, so you don't need to install and configure the tools yourself. Each template pairs an application container for your language with a local SQL Server container and a sample database defined as a SQL Database project.

The project's target platform is Azure SQL Database, so the project build tells you whether your schema works in Azure SQL Database. Build the project after every schema change, and test your application against a database in Azure SQL Database before you deploy.

> [!NOTE]  
> Dev containers can run both locally in Visual Studio Code using the [Dev Containers extension](https://marketplace.visualstudio.com/items?itemName=ms-vscode-remote.remote-containers) and in the cloud using [GitHub Codespaces](https://docs.github.com/codespaces/setting-up-your-project-for-codespaces/adding-a-dev-container-configuration/introduction-to-dev-containers).

To learn more about the Azure SQL Database Dev Container Templates, see [Dev Container Templates for Azure SQL Database overview](local-dev-experience-dev-containers.md)

## Related content

- [Quickstart: Set up a development environment with Dev Container Templates for Azure SQL Database](local-dev-experience-dev-containers-quickstart.md)
