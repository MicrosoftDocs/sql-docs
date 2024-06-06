---
title: What are the Azure SQL Database Dev Container templates?
titleSuffix: Azure SQL Database
description: Create a local development environment for Azure SQL Database using Dev Containers.
author: croblesm
ms.author: roblescarlos
ms.reviewer: wiassaf
ms.date: 06/06/2024
ms.service: sql-database
ms.topic: overview
---

# Quickstart: Set up a development environment with an Azure SQL Database Dev Container template

Dev Containers provide a comprehensive solution for enhancing local development for Azure SQL Database. Dev Container templates offer developers a seamless and efficient development environment, enabling them to build applications for Azure SQL Database with ease and confidence. Dev Containers can be utilized in any development environment whether is local or in the cloud, you can promote consistency across teams and workflows.

## Capabilities / Features

The Dev Container for Azure SQL Database is packed with powerful tools and configurations to streamline your development process. Here are the key features:

### Visual Studio Code Extensions

- **ms-mssql.mssql**: SQL Server extension for connecting and querying SQL databases.
- **ms-mssql.sql-database-projects**: Extension for managing SQL Database projects, allowing for streamlined schema changes and deployment.
- **github.copilot**: AI-powered code completion for enhanced productivity.
- **ms-azuretools.vscode-docker**: Docker extension for managing containers directly from VS Code.
- **ms-dotnettools.csdevkit** and **ms-dotnettools.csharp**: Essential extensions for .NET development.
- **ms-azuretools.vscode-bicep**: Bicep extension for managing Azure resources.
- **ms-azuretools.vscode-docker**: Docker extension for managing containers.
- **github.codespaces**: Extension for working with GitHub Codespaces.

### Pre-configured Environment

- **.NET Ready**: The environment includes .NET pre-installed and configured, ready for development.
- **Azure CLI and azd CLI**: Tools for managing Azure resources and deployments.
- **Pre-created Database**: A sample SQL Server database, pre-configured and ready for use.
- **SQL Server 2022**: The latest developer edition of Microsoft SQL Server is included.

> [!IMPORTANT]
> While the SQL Server container employs a standard version of SQL Server, all database development within this Dev Container can be validated for Azure SQL Database using the SQL Database Project. The SQL Database project is preconfigured with the target platform set as Azure SQL Database.

### VS Code Tasks

A set of predefined tasks in VS Code to simplify common actions:

- **Execute SQL Query**: Opens and executes a SQL file to validate the database schema.
- **Build SQL Database Project**: Builds the SQL Database project using `dotnet build`.
- **Deploy SQL Database Project**: Deploys the SQL Database project to the database container.
- **Update .NET SDK**: Updates the .NET SDK to ensure the latest features and compatibility.
- **Trust .NET HTTPS Certificate**: Trusts the HTTPS certificate for secure development with .NET Aspire.

### Automation and Efficiency

- **Automated Database Deployment**: The Dev Container initialization process handles the deployment of the sample database using a DAC package.
- **Container Configuration**: Includes Docker configurations for setting up the development environment with minimal effort.

### Customization and Flexibility

- **Forwarded Ports**: Pre-configured to forward necessary ports for seamless local development.
- **Docker Compose**: Uses Docker Compose to manage multiple services and ensure they work together efficiently.

## Prerequisites

Before you begin, ensure you have the following installed on your local machine:

- **Docker**: Required for running containers. [Download Docker](https://www.docker.com/get-started)
- **Visual Studio Code**: The primary IDE for this quickstart. [Download VS Code](https://code.visualstudio.com/)
- **Dev Containers extension for VS Code**: Enables working with Dev Containers. [Install the extension](https://marketplace.visualstudio.com/items?itemName=ms-vscode-remote.remote-containers)
- **Git**: For version control. [Download Git](https://git-scm.com/)

Additionally, you will need:

- An Azure account: If you don't have one, you can create a free account at [Azure Free Account](https://azure.microsoft.com/free/).

# Steps to Set Up the Development Environment

1. Clone the Dev Container Repository

    ```bash
    git clone https://github.com/your-repository/azure-sql-dev-container-template.git
    cd azure-sql-dev-container-template
    ```  

2. Open the Project in VS Code  

    - Open Visual Studio Code.
    - Use the File > Open Folder menu to open the cloned repository folder.

3. Reopen in Container
 
    - Press `F1` to open the command palette.
    - Type Remote-Containers: Reopen in Container and select the command.

4. Wait for the Container to Build

    - VS Code will build the container based on the `devcontainer.json` configuration.
    - This might take a few minutes the first time.

5. Verify the Setup

    - Once the container is ready, you can start using the pre-installed tools and extensions.
    - Open a terminal in VS Code and run `sqlcmd --version`, `dotnet --version`, `az --version` to verify the installations.

6. Run the Predefined Tasks

    - To run any of the predefined tasks, open the command palette (`F1`), type Run Task, and select the desired task (e.g., Execute SQL Query, Build SQL Database Project).

## Next steps

Learn more about the local development experience for Azure SQL Database:

- [Overview - Azure SQL Database Dev Container templates](local-dev-experience-dev-container)
- [Quickstart: Set up a development environment with an Azure SQL Database Dev Container template](local-dev-experience-dev-container-quickstart)
- [Quickstart: Create a local development environment for Azure SQL Database](local-dev-experience-quickstart)
