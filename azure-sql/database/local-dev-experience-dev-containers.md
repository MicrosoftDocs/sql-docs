---
title: What are the Azure SQL Database Dev Container templates?
titleSuffix: Azure SQL Database
description: Learn about the local development experience for Azure SQL Database with Dev Container templates.
author: croblesm
ms.author: roblescarlos
ms.reviewer: wiassaf
ms.date: 06/06/2024
ms.service: sql-database
ms.topic: overview
---

# What are the Azure SQL Database Dev Container templates?
[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

Dev Containers provide a comprehensive solution for enhancing local development for Azure SQL Database. Dev Container templates offer developers a seamless and efficient development environment, enabling them to build applications for Azure SQL Database with ease and confidence. Dev Containers can be utilized in any development environment whether is local or in the cloud, you can promote consistency across teams and workflows.

:::image type="content" source="media/local-dev-experience-dev-container/azure-sql-db-dev-containers.png" border="false" alt-text="Diagram that shows the Azure SQL Database with Dev Containers local development experience" lightbox="media/local-dev-experience-dev-container/azure-sql-db-dev-containers.png":::

The Azure SQL Database Dev Container templates simplify the development process by providing preconfigured environments that eliminate the need for manual setup. Developers can start coding immediately with all necessary tools and dependencies in place, using popular programming languages. These templates are compatible with Visual Studio Code and GitHub Codespaces, enabling a seamless transition from local development to cloud environments.

The local development environment mimics Azure SQL Database, allowing you to manage data and test applications efficiently. Once ready, **GitHub Actions** automate the deployment process, transitioning seamlessly your application to **Azure Static Web Apps** or **Azure Web App Service** and **Azure SQL Database**. This streamlined workflow enhances productivity, reduces setup time, and ensures consistency between local and production environments, helping you deliver high-quality applications faster.

> [!TIP]
> A GitHub Action and an Azure DevOps Task are available in [devcontainers/ci](https://github.com/devcontainers/ci) for running a repository’s dev container in continuous integration (CI) builds. This allows you to reuse the same setup that you are using for local development to also build and test your code in CI.

## How Dev Containers Work

Dev Containers are preconfigured, containerized environments designed to provide a consistent development experience no matter where they're used. They utilize the Development Container Specification (`devcontainer.json`) to define necessary tools, settings, and configurations for the development environment.

### Key components

- **Docker**: Provides the underlying container technology to create isolated environments.
- **VS Code**: Acts as the integrated development environment (IDE) that interacts with Dev Containers.
- **GitHub Codespaces**: Extends Dev Containers to the cloud, enabling development from any device with a browser.

### How Dev Containers work

1. **Initialization**: Developers start with a Dev Container template that includes all necessary configurations.
2. **Environment Setup**: The container is built using Docker, setting up the environment based on the `devcontainer.json` specifications.
3. **Coding and Testing**: Developers write and test code within this consistent environment, ensuring compatibility with the final production setup.
4. **Deployment**: Once development and testing are complete, the application can be seamlessly deployed using CI/CD pipelines like GitHub Actions.

## Azure SQL Database and Dev Containers

Dev Containers are beneficial for Azure SQL Database development by addressing common challenges and enhancing the overall workflow. As developers face significant challenges in setting up efficient local development environments for Azure SQL Database:

- **Lack of Compatibility**: Discrepancies between local development and production environments.
- **Setup Complexity**: Time-consuming manual installations and configurations.
- **Dependency on Cloud Resources**: Increased cloud costs and reliance on internet connectivity.
- **Limited Integration**: Lack of integration with existing Azure development tools.

The specialized Dev Container templates for Azure SQL Database can help you bridge this gap, enhancing your development experience and streamlining workflows within the Azure ecosystem.

## Advantages of Dev Containers for Azure SQL Database

Dev Containers streamline the development lifecycle, enabling developers to focus on coding and testing without the hassle of environment setup. This efficiency leads to faster iterations, higher-quality applications, and a reduced time-to-market for applications built on Azure SQL Database, giving businesses a competitive edge.

Local development with Dev Containers reduces cloud costs associated with development and testing in Azure environments. This optimization of resources improves cost-efficiency and scalability. Developers can transition seamlessly from local development to Azure environments, using the scalability and reliability of Azure SQL Database for production deployments without incurring unnecessary costs.

Dev Containers support cloud-native development scenarios, aligning with modern application architectures and frameworks. This ensures compatibility with Azure SQL Database and facilitates seamless deployment to Azure environments. By embracing cloud-native trends, we position Azure SQL Database as the platform of choice for modern, cloud-native applications, driving long-term adoption and revenue growth.

## Available Templates

The Azure SQL Database Dev Container Templates are available for the following programming languages / frameworks:

| Programming Language / Framework | Description |
|----------------------------------|-------------|
| [.NET](https://github.com/microsoft/azuresql-devcontainers/tree/main/src/dotnet)                             | A development environment for .NET and Azure SQL, enabling streamlined local development and testing. |
| [.NET Aspire](https://github.com/microsoft/azuresql-devcontainers/tree/main/src/dotnet-aspire)                      | A development environment for .NET Aspire and Azure SQL, enabling streamlined local development and testing. |

> [!TIP]
> Each template comes with a pre-configured Azure SQL Database, making it easy to start developing right away!

## Next steps

Learn more about the local development experience for Azure SQL Database:

- [Overview - Local development experience for Azure SQL Database](local-dev-experience-overview)
- [Quickstart: Set up a development environment with an Azure SQL Database Dev Container template](local-dev-experience-dev-container-quickstart)
- [Quickstart: Create a local development environment for Azure SQL Database](local-dev-experience-quickstart)
