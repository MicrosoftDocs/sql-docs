---
title: What are the Dev Container Templates for Azure SQL Database?
titleSuffix: Azure SQL Database
description: Learn about the local development experience for Azure SQL Database with Dev Container Templates.
author: croblesm
ms.author: roblescarlos
ms.reviewer: wiassaf, randolphwest
ms.date: 08/14/2024
ms.service: azure-sql-database
ms.topic: overview
---

# What are the Dev Container Templates for Azure SQL Database?

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

Dev containers provide a comprehensive solution for enhancing local development for Azure SQL Database. Dev Container Templates offer developers a seamless and efficient development environment, enabling them to build applications for Azure SQL Database with ease and confidence.

Dev containers can be utilized in any development environment whether is local or in the cloud, you can promote consistency across teams and workflows.

:::image type="content" source="media/local-dev-experience-dev-containers/azure-sql-db-dev-containers.png" alt-text="Diagram that shows the Azure SQL Database with dev containers local development experience." lightbox="media/local-dev-experience-dev-containers/azure-sql-db-dev-containers.png":::

The Dev Container Templates for Azure SQL Database simplify the development process by providing preconfigured environments that eliminate the need for manual setup. Developers can start coding immediately with all necessary tools and dependencies in place, using popular programming languages. These templates are compatible with [Visual Studio Code](https://visualstudio.microsoft.com/downloads/) and [GitHub Codespaces](https://github.com/features/codespaces), enabling a seamless transition from local development to cloud environments.

The local development environment mimics Azure SQL Database, allowing you to manage data and test applications efficiently. Once ready, **GitHub Actions** automate the deployment process, transitioning seamlessly your application to **Azure Static Web Apps** or **Azure Web App Service** and **Azure SQL Database**. This streamlined workflow enhances productivity, reduces setup time, and ensures consistency between local and production environments, helping you deliver high-quality applications faster.

> [!TIP]  
> A GitHub Action and an Azure DevOps Task are available in [devcontainers/ci](https://github.com/devcontainers/ci) for running a repository's dev container in continuous integration (CI) builds. This allows you to reuse the same setup that you use for local development to also build and test your code in CI.

## How dev containers work

Dev containers are preconfigured, containerized environments designed to provide a consistent development experience no matter where they're used. They utilize the Development Container Specification (`devcontainer.json`) to define necessary tools, settings, and configurations for the development environment.

### Key components

- **Docker**: Provides the underlying container technology to create isolated environments.
- **VS Code**: Acts as the integrated development environment (IDE) that interacts with dev containers.
- **GitHub Codespaces**: Extends dev containers to the cloud, enabling development from any device with a browser.

### Get started steps

- **Initialization**: Developers start with a dev container template that includes all necessary configurations.
- **Environment setup**: The container is built using Docker, setting up the environment based on the `devcontainer.json` specifications.
- **Coding and testing**: Developers write and test code within this consistent environment, ensuring compatibility with the final production setup.
- **Deployment**: Once development and testing are complete, the application can be seamlessly deployed using CI/CD pipelines like GitHub Actions.

## Azure SQL Database and Dev Containers

Dev Containers are beneficial for Azure SQL Database development by addressing common challenges and enhancing the overall workflow. As developers face significant challenges in setting up efficient local development environments for Azure SQL Database:

- **Lack of compatibility**: Discrepancies between local development and production environments.
- **Setup complexity**: Time-consuming manual installations and configurations.
- **Dependency on cloud resources**: Increased cloud costs and reliance on internet connectivity.
- **Limited integration**: Lack of integration with existing Azure development tools.

The specialized Dev Container Templates for Azure SQL Database can help you bridge this gap, enhancing your development experience and streamlining workflows within the Azure ecosystem.

## Advantages of Dev Containers for Azure SQL Database

Dev containers streamline the development lifecycle, enabling developers to focus on coding and testing without the hassle of environment setup. This efficiency leads to faster iterations, higher-quality applications, and a reduced time-to-market for applications built on Azure SQL Database, giving businesses a competitive edge.

Local development with dev containers reduces cloud costs associated with development and testing in Azure environments. This optimization of resources improves cost-efficiency and scalability. Developers can transition seamlessly from local development to Azure environments, using the scalability and reliability of Azure SQL Database for production deployments without incurring unnecessary costs.

Dev containers support cloud-native development scenarios, aligning with modern application architectures and frameworks. This ensures compatibility with Azure SQL Database and facilitates seamless deployment to Azure environments. By embracing cloud-native trends, we position Azure SQL Database as the platform of choice for modern, cloud-native applications, driving long-term adoption and revenue growth.

To fully appreciate the effect of dev containers on your Azure SQL Database projects, consider the following key features that enhance and simplify the development process:

### Visual Studio Code extensions

- `ms-mssql.mssql`: SQL Server extension for connecting and querying SQL databases.
- `ms-mssql.sql-database-projects`: Extension for managing SQL Database projects, allowing for streamlined schema changes and deployment.
- `github.copilot`: AI-powered code completion for enhanced productivity.
- `ms-azuretools.vscode-docker`: Docker extension for managing containers directly from Visual Studio Code.
- `github.codespaces`: Extension for working with GitHub Codespaces.
- `ms-azuretools.vscode-docker`: Docker extension for managing containers.

> [!TIP]  
> There are more extensions available, depending on the template you choose.

### Preconfigured environment

All of the below tools and utilities are preloaded in the dev container. You don't need to download or install anything else.

- **.NET / .NET Aspire / Node / Python**: The environment includes your preferred programming language/framework preinstalled and configured, ready for development.
- **Azure CLI**: Tools for managing Azure resources and deployments.
- **Azure Developer CLI:** A command-line interface providing a unified scripting experience for managing and developing Azure resources.
- **Docker CLI**: Allows building and managing Docker containers from within another container.
- **Azure SQL Database**: The `library` database was created and validated and ready for use. This database gives you full compatibility with Azure SQL Database.
- **SQLCMD**: A command-line utility you can use to interact with the database, run queries, and more.
- **SqlPackage**: Command-line utility for deploying database changes, including schema updates and data migrations.

> [!IMPORTANT]  
> While the container now uses the `mcr.microsoft.com/azure-sql-edge` image, designed for edge computing scenarios and offering a subset of SQL Server's features along with built-in AI, all database development within this dev container can still be validated for Azure SQL Database using the SQL Database Project. The SQL Database project is preconfigured with the target platform set as Azure SQL Database.

### Visual Studio Code tasks

A set of predefined tasks in Visual Studio Code to simplify common actions:

- **1. Verify database schema and data**: Opens and executes a SQL file to validate the database schema.
- **2. Build SQL Database project**: Builds the SQL Database project using `dotnet build`.
- **3. Publish SQL Database Project**: Publish the SQL Database project to the database container.

> [!TIP]  
> There are specific tasks available, depending on the template you choose.

## Available templates

The Dev Container Templates for Azure SQL Database, are available for the following programming languages / frameworks:

| Programming Language / Framework | Description |
| --- | --- |
| [.NET](https://github.com/microsoft/azuresql-devcontainers/tree/main/src/dotnet) | A development environment for .NET and Azure SQL, enabling streamlined local development and testing. |
| [.NET Aspire](https://github.com/microsoft/azuresql-devcontainers/tree/main/src/dotnet-aspire) | A development environment for .NET Aspire and Azure SQL, enabling streamlined local development and testing. |
| [Node.js](https://github.com/microsoft/azuresql-devcontainers/tree/main/src/javascript-node) | A development environment for Node.js (JavaScript) and Azure SQL, enabling streamlined local development and testing. |
| [Python](https://github.com/microsoft/azuresql-devcontainers/tree/main/src/python) | A development environment for Python and Azure SQL, enabling streamlined local development and testing. |

> [!TIP]  
> Each template comes with a pre-configured Azure SQL Database, making it easy to start developing right away!

## Related content

- [What is the local development experience for Azure SQL Database?](local-dev-experience-overview.md)
- [Quickstart: Set up a development environment with Dev Container Templates for Azure SQL Database](local-dev-experience-dev-containers-quickstart.md)
- [Quickstart: Create a local development environment for Azure SQL Database](local-dev-experience-quickstart.md)
