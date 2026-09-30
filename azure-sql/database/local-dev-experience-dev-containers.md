---
title: Dev Container Templates for Azure SQL Database
titleSuffix: Azure SQL Database
description: Learn about the local development experience for Azure SQL Database with Dev Container Templates.
author: croblesm
ms.author: roblescarlos
ms.reviewer: wiassaf, randolphwest
ms.date: 09/16/2026
ms.service: azure-sql-database
ms.topic: overview
monikerRange: "=azuresql || =azuresql-db"
---

# Dev Container Templates for Azure SQL Database overview

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

Dev Container Templates give you a preconfigured development environment for Azure SQL Database. You can run Dev Container Templates on your local machine or in the cloud. Each template includes an application container for your language, a local SQL Server 2025 container, and a sample SQL Database project.

:::image type="content" source="media/local-dev-experience-dev-containers/azure-sql-db-dev-containers.png" alt-text="Diagram that shows the Azure SQL Database with dev containers local development experience." lightbox="media/local-dev-experience-dev-containers/azure-sql-db-dev-containers.png":::

Dev Container Templates support several popular languages and include the tools you need to build, test, and publish. These templates are compatible with [Visual Studio Code](https://visualstudio.microsoft.com/downloads/) and [GitHub Codespaces](https://github.com/features/codespaces), so you can move from local development to cloud environments.

The SQL Database project targets Azure SQL Database, so the project build tells you whether your schema works in Azure SQL Database. When you're ready to deploy, GitHub Actions can publish the application to Azure App Service or Azure Static Web Apps, and the database to Azure SQL Database.

> [!NOTE]  
> The local database engine is SQL Server 2025 (Developer edition), from the `mcr.microsoft.com/mssql/server:2025-latest` container image. Earlier versions of these templates used Azure SQL Edge, which retired on September 30, 2025.

> [!TIP]  
> A GitHub Action and an Azure DevOps Task are available in [devcontainers/ci](https://github.com/devcontainers/ci) for running a repository's dev container in continuous integration (CI) builds. This allows you to reuse the same setup that you use for local development to also build and test your code in CI.

The following video gives an overview of the Dev Container Templates for Azure SQL Database. The video was recorded against an earlier release, so some of the tools and options it shows differ from what this article describes.

<br />

> [!VIDEO https://learn-video.azurefd.net/vod/player?show=open-at-microsoft&ep=boost-your-local-development-with-dev-container-templates-for-azure-sql]

## How dev containers work

Dev containers are preconfigured, containerized environments that provide a consistent development experience no matter where you use them. They use the Development Container Specification (`devcontainer.json`) to define the tools, settings, and configurations for the development environment.

### Key components

- **Docker**: Provides the underlying container technology to create isolated environments.
- **Visual Studio Code**: Acts as the integrated development environment (IDE) that interacts with dev containers.
- **GitHub Codespaces**: Extends dev containers to the cloud, enabling development from any device with a browser.

### Get started steps

- **Initialization**: Developers start with a dev container template that includes all necessary configurations.
- **Environment setup**: Docker builds the container and sets up the environment based on the `devcontainer.json` specifications.
- **Coding and testing**: Developers write and test code within this consistent environment, ensuring compatibility with the final production setup.
- **Deployment**: After development and testing are complete, the application is deployed through CI/CD pipelines like GitHub Actions.

## Azure SQL Database and dev containers

Dev Containers benefit Azure SQL Database development by addressing common challenges and enhancing the overall workflow. Developers face several challenges when setting up local development environments for Azure SQL Database:

- **Lack of compatibility**: Discrepancies between local development and production environments.
- **Setup complexity**: Time-consuming manual installations and configurations.
- **Dependency on cloud resources**: Increased cloud costs and reliance on internet connectivity.
- **Limited integration**: Lack of integration with existing Azure development tools.

The specialized Dev Container Templates for Azure SQL Database help you bridge this gap, enhancing your development experience and streamlining workflows within the Azure ecosystem.

## Advantages of dev containers for Azure SQL Database

Dev containers give you a development environment that you don't assemble yourself, and that everyone on your team gets identically. You work against a local database instead of a database in Azure, so you don't pay for cloud resources while you develop, and you can work without a network connection after the first build.

Because you define the environment in files that you commit, the same definition runs on your workstation, in GitHub Codespaces, and in continuous integration.

### How compatibility with Azure SQL Database works

The database container runs SQL Server 2025, not Azure SQL Database. Compatibility comes from the SQL Database project, not from the local engine.

Each template includes a SQL Database project whose target platform is Azure SQL Database (`SqlAzureV12DatabaseSchemaProvider`). When you build the project, the build fails if the schema uses anything that Azure SQL Database doesn't support. Build the project after every schema change, and treat a build failure as a compatibility error rather than a tooling problem.

The build validates the schema. It doesn't validate application behavior, performance, or features that only exist in the service. Test your application against a database in Azure SQL Database before you deploy it. For more information, see [Target platform](/sql/tools/sql-database-projects/concepts/target-platform).

The following sections describe what each template gives you.

### Visual Studio Code extensions

Each template installs these extensions in the container:

- `ms-mssql.mssql`: connect to a database, run queries, and work with SQL Database projects. This extension is an extension pack, so it also installs the SQL Database Projects extension (`ms-mssql.sql-database-projects-vscode`). You don't list that extension separately.
- `ms-azuretools.vscode-docker`: manage containers and images from Visual Studio Code.
- `github.codespaces`: work with GitHub Codespaces.

Each template also installs the extensions for its language, such as the C# extensions in the .NET templates and the Python extensions in the Python template. For the full list, see `.devcontainer/devcontainer.json` in the template you use.

The templates don't install GitHub Copilot. Current versions of Visual Studio Code include GitHub Copilot Chat as a built-in extension, and a dev container that asks for the `github.copilot` extension fails to install it, because a built-in extension can't be replaced from the Marketplace. If you use an earlier version of Visual Studio Code, install GitHub Copilot yourself.

### Preconfigured environment

The dev container includes these tools. You don't need to install anything else.

- **Your language runtime**: .NET 10, Python 3.14, or Node.js 24, depending on the template. The .NET templates also offer .NET 8, which is supported until November 10, 2026. The .NET Aspire template includes the Aspire CLI, version 13.5.
- **.NET SDK**: present in every template, including the Python and Node.js templates, because the SQL Database project and SqlPackage need it.
- **SqlPackage**: publishes the SQL Database project to a database. It's installed as a .NET tool, so it runs on both x64 and Arm64 containers.
- **sqlcmd**: a command-line utility for running queries and scripts, from go-sqlcmd 1.10.0.
- **Azure CLI** and **Azure Developer CLI (`azd`)**: manage and deploy Azure resources.
- **Docker CLI**: work with the host's Docker engine from inside the container.
- **A sample database**: a `Library` database on the local SQL Server 2025 container, built from the SQL Database project and seeded with data when the container is created. It contains the `authors`, `books`, and `books_authors` tables, a view, and a stored procedure.

### Apple Silicon and other Arm64 hosts

On an Arm64 host, such as a Mac with Apple Silicon, the application container runs natively. Every tool in it, including the .NET SDK, SqlPackage, `sqlcmd`, the Azure CLI, and the Azure Developer CLI, has an Arm64 build.

SQL Server runs on x64 only. The database container is therefore pinned to `linux/amd64` and runs under emulation, such as Rosetta in Docker Desktop. Microsoft doesn't test or support SQL Server under emulation. For more information, see [SQL Server 2025 on Linux release notes](/sql/linux/sql-server-linux-release-notes).

SQL Server sometimes fails while it starts in the database container. When it does, the container build stops, because the database service never becomes healthy. Run **Dev Containers: Rebuild Container** to try again. This behavior isn't limited to emulation.

GitHub Codespaces and other x64 hosts run SQL Server natively, and aren't affected.

### Visual Studio Code tasks

Each template defines tasks for the actions you repeat. To run one, press <kbd>F1</kbd>, select **Tasks: Run Task**, and select the task.

- **1. Verify database schema and data**: opens a SQL script that queries the sample tables, so you can run it against the local database.
- **2. Build SQL Database project**: runs `dotnet build` on the SQL Database project and produces a `.dacpac` file. The build fails if the schema isn't compatible with Azure SQL Database.
- **3. Publish SQL Database project**: publishes the project to the local database with SqlPackage, which compares the project with the database and applies the differences.
- **4. Trust .NET HTTPS certificate**: runs `dotnet dev-certs https --trust` so that ASP.NET Core applications can serve HTTPS from the container. This task is in the .NET and .NET Aspire templates only.

## Available templates

The Dev Container Templates for Azure SQL Database are available for the following programming languages and frameworks:

- [.NET](https://github.com/microsoft/azuresql-devcontainers/tree/main/src/dotnet)
- [.NET Aspire](https://github.com/microsoft/azuresql-devcontainers/tree/main/src/dotnet-aspire)
- [Node.js](https://github.com/microsoft/azuresql-devcontainers/tree/main/src/javascript-node)
- [Python](https://github.com/microsoft/azuresql-devcontainers/tree/main/src/python)

To apply a template from the command line, use the Dev Container CLI:

```bash
devcontainer templates apply -t ghcr.io/microsoft/azuresql-devcontainers/python
devcontainer up --workspace-folder .
```

Replace `python` with `dotnet`, `dotnet-aspire`, or `javascript-node`.

> [!TIP]  
> Each template creates a `Library` sample database on the local SQL Server container when you build the container, so you can start querying immediately.

## Related content

- [What is the local development experience for Azure SQL Database?](local-dev-experience-overview.md)
- [Quickstart: Set up a development environment with Dev Container Templates for Azure SQL Database](local-dev-experience-dev-containers-quickstart.md)
- [Create a project for a local Azure SQL Database development environment](local-dev-experience-create-database-project.md)
- [What are SQL database projects?](/sql/tools/sql-database-projects/sql-database-projects)
