---
title: Set up a development environment with an Azure SQL Database Dev Container template
titleSuffix: Azure SQL Database
description: Create a local development environment for Azure SQL Database using Dev Containers.
author: croblesm
ms.author: roblescarlos
ms.reviewer: wiassaf
ms.date: 06/06/2024
ms.service: sql-database
ms.topic: quickstart
---

# Quickstart: Set up a development environment with an Azure SQL Database Dev Container template

Dev Containers provide a comprehensive solution for enhancing local development for Azure SQL Database. Dev Container templates offer developers a seamless and efficient development environment, enabling them to build applications for Azure SQL Database with ease and confidence. Dev Containers can be utilized in any development environment whether is local or in the cloud, you can promote consistency across teams and workflows.

## Prerequisites

Before you begin, ensure you have the following installed on your local machine:

- **Git**: For version control. [Download Git](https://git-scm.com/)
- **Docker**: Required for running containers. [Download Docker](https://www.docker.com/get-started)
- **Visual Studio Code**: The primary IDE for this quickstart. [Download Visual Studio Code](https://code.visualstudio.com/)
- **Dev Containers extension for Visual Studio Code**: Enables working with Dev Containers. [Install the extension](https://marketplace.visualstudio.com/items?itemName=ms-vscode-remote.remote-containers)


## Steps to set up the development environment

1. Open Visual Studio Code.

2. Press `F1` or `Ctrl+Shift+P` to open the command palette. Select the **Dev Containers: New Dev Container** command.
:::image type="content" source="media/local-dev-experience-dev-containers-quickstart/vscode-command-palette.png" border="false" alt-text="Visual Studio Code command palette" lightbox="media/local-dev-experience-dev-container/azure-sql-db-dev-containers.png":::

3. Select the desired Dev Container template for Azure SQL Database, typing **Azure SQL**.
:::image type="content" source="media/local-dev-experience-dev-containers-quickstart/vscode-azure-sql-devcontainers.png" border="false" alt-text="Select one of the available templates for Azure SQL" lightbox="media/local-dev-experience-dev-container/azure-sql-db-dev-containers.png":::

4. After choosing a template, proceed by selecting **Create Dev Container**. Visual Studio Code handles the heavy lifting of building the container.
:::image type="content" source="media/local-dev-experience-dev-containers-quickstart/vscode-azure-sql-devcontainers-create.png" border="false" alt-text="Create Dev Container" lightbox="media/local-dev-experience-dev-container/azure-sql-db-dev-containers.png":::

    This process sets up the environment based on the chosen configuration and ensures that all required tools and dependencies are included. So, the build process may take a few minutes, especially the first time, as it downloads and configures everything.

    :::image type="content" source="media/local-dev-experience-dev-containers-quickstart/vscode-azure-sql-devcontainers-build.png" border="false" alt-text="Dev Container creating log" lightbox="media/local-dev-experience-dev-container/azure-sql-db-dev-containers.png":::

    Visual Studio Code will build the container based on the selected configuration. The build process might take a few minutes the first time.

    :::image type="content" source="media/local-dev-experience-dev-containers-quickstart/vscode-azure-sql-devcontainers-features.png" border="false" alt-text="Dev Container creating log - MSSSQL feature" lightbox="media/local-dev-experience-dev-container/azure-sql-db-dev-containers.png":::

5. Once the dev container is built, you can start exploring and verifying the setup. Open a terminal within Visual Studio Code to check that all necessary tools are installed and working correctly.
:::image type="content" source="media/local-dev-experience-dev-containers-quickstart/vscode-azure-sql-devcontainers-terminal.png" border="false" alt-text="Dev Container verification" lightbox="media/local-dev-experience-dev-container/azure-sql-db-dev-containers.png":::

6. Verify the setup.
    - Once the container is ready, you can start using the preinstalled tools and extensions.
    - Open a terminal in Visual Studio Code and run the following commands to verify the installations:

        ```bash
        # Verify SQL Command Line Tool
        sqlcmd --version   
        
        # Verify Azure CLI
        az --version       
        
        # Verify .NET SDK
        dotnet --version   
        
        # Verify Python installation (if applicable)
        python --version   
        
        # Verify Node.js installation (if applicable)
        node --version     
        ```

7. As an optional step, you can also run predefined tasks directly from the command palette, streamlining your development workflow and allowing you to focus on writing code.

    :::image type="content" source="media/local-dev-experience-dev-containers-quickstart/vscode-azure-sql-devcontainers-tasks.png" border="false" alt-text="Dev Container run a VS Code task" lightbox="media/local-dev-experience-dev-container/azure-sql-db-dev-containers.png":::
    
    :::image type="content" source="media/local-dev-experience-dev-containers-quickstart/vscode-azure-sql-devcontainers-task-list.png" border="false" alt-text="List of VS Code tasks" lightbox="media/local-dev-experience-dev-container/azure-sql-db-dev-containers.png":::
    
For more information about a specific template, see [GitHub - azuresql-devcontainers](https://aka.ms/azuresql-devcontainers-repo).

## Related content

Learn more about the local development experience for Azure SQL Database:

- [Overview - Azure SQL Database Dev Container templates](./local-dev-experience-dev-containers.md)
- [Quickstart: Set up a development environment with an Azure SQL Database Dev Container template](./local-dev-experience-dev-containers-quickstart.md)
- [Quickstart: Create a local development environment for Azure SQL Database](./local-dev-experience-quickstart.md)
