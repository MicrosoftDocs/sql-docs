---
title: Set up a development environment with Dev Container Templates for Azure SQL Database
titleSuffix: Azure SQL Database
description: Create a local development environment for Azure SQL Database using dev containers.
author: croblesm
ms.author: roblescarlos
ms.reviewer: wiassaf, randolphwest
ms.date: 08/14/2024
ms.service: sql-database
ms.topic: quickstart
---

# Quickstart: Set up a development environment with Dev Container Templates for Azure SQL Database

Dev containers provide a comprehensive solution for enhancing local development for Azure SQL Database. Dev Container Templates offer developers a seamless and efficient development environment, enabling them to build applications for Azure SQL Database with ease and confidence. Dev containers can be utilized in any development environment whether is local or in the cloud, you can promote consistency across teams and workflows.

## Prerequisites

Before you begin, ensure you have the following installed on your local machine:

- **Git**: For version control. [Download Git](https://git-scm.com/)
- **Docker**: Required for running containers. [Download Docker](https://www.docker.com/get-started)
- **Visual Studio Code**: The primary IDE for this quickstart. [Download Visual Studio Code](https://code.visualstudio.com/)
- **Dev Containers extension for Visual Studio Code**: Enables working with dev containers. [Install the extension](https://marketplace.visualstudio.com/items?itemName=ms-vscode-remote.remote-containers)

## Steps to set up the development environment

1. Begin by either opening a local folder containing your application project or cloning an existing repository into Visual Studio Code. This initial step sets the stage for integrating your project with a development container, whether you're starting from scratch or working on an existing application.

1. In Visual Studio Code, press `F1` or `Ctrl+Shift+P` to open the command palette. Select the **Dev Containers: Add Dev Container Configuration Files...** command.

   :::image type="content" source="media/local-dev-experience-dev-containers-quickstart/vscode-command-palette.png" alt-text="Screenshot of Visual Studio Code command palette for adding Dev Container configuration files." lightbox="media/local-dev-experience-dev-containers-quickstart/vscode-command-palette.png":::

1. Select the **Add configuration file to workspace** option if you want to add the dev container configuration file to your current local repository.
   - Alternatively, choose the **Add configuration file to user data folder** option.
   - For this quickstart, select the **Add configuration file to workspace** option.

   :::image type="content" source="media/local-dev-experience-dev-containers-quickstart/vscode-command-palette-configuration-file-1.png" alt-text="Screenshot of Visual Studio Code command palette showing the option to add configuration file to workspace." lightbox="media/local-dev-experience-dev-containers-quickstart/vscode-command-palette-configuration-file-1.png":::

   Visual Studio Code prompts you to select a Dev Container Template. The available templates are based on the tools and dependencies required for the specific development environment. Select **Show All Definitions** to view all available templates.

   :::image type="content" source="media/local-dev-experience-dev-containers-quickstart/vscode-command-palette-configuration-file-2.png" alt-text="Screenshot of Visual Studio Code command palette showing the option to show all Dev Container definitions." lightbox="media/local-dev-experience-dev-containers-quickstart/vscode-command-palette-configuration-file-2.png":::

1. Select the desired Dev Container Template for Azure SQL Database by typing **Azure SQL** into the command palette. This action displays a list of available templates designed for Azure SQL development.

   :::image type="content" source="media/local-dev-experience-dev-containers-quickstart/vscode-azure-sql-devcontainers.png" alt-text="Screenshot of Visual Studio Code showing available Dev Container Templates for Azure SQL." lightbox="media/local-dev-experience-dev-containers-quickstart/vscode-azure-sql-devcontainers.png":::

   Upon selection, Visual Studio Code automatically generates the necessary configuration files tailored to the chosen template. These files include settings for the development environment, extensions to install, and Docker configuration details. They're stored in a `.devcontainer` folder within your project directory, ensuring a consistent and reproducible development environment.

   :::image type="content" source="media/local-dev-experience-dev-containers-quickstart/vscode-azure-sql-devcontainers-create-1.png" alt-text="Screenshot of Visual Studio Code generating configuration files for Azure SQL Dev Containers." lightbox="media/local-dev-experience-dev-containers-quickstart/vscode-azure-sql-devcontainers-create-1.png":::

   Following the configuration file generation, Visual Studio Code prompts you to transition your project into the newly created Dev Container environment. You can do it by selecting **Reopen in Container**. This step is crucial as it moves your development inside the container, applying the predefined environment settings for Azure SQL development.

   If you haven't already, you can also initiate this transition manually at any time using the Dev Containers extension. Use the **Reopen in Container** command from the command palette or select the blue icon at the bottom left corner of Visual Studio Code and select **Reopen in Container**.

   :::image type="content" source="media/local-dev-experience-dev-containers-quickstart/vscode-azure-sql-devcontainers-create-2.png" alt-text="Screenshot of Visual Studio Code prompt to reopen project in container." lightbox="media/local-dev-experience-dev-containers-quickstart/vscode-azure-sql-devcontainers-create-2.png":::

   This screenshot shows the Dev Containers extension icon in Visual Studio Code.

   :::image type="content" source="media/local-dev-experience-dev-containers-quickstart/vscode-azure-sql-devcontainers-create-3.png" alt-text="Screenshot of Dev Containers extension icon in Visual Studio Code." lightbox="media/local-dev-experience-dev-containers-quickstart/vscode-azure-sql-devcontainers-create-3.png":::

1. This action initiates the setup process, where Visual Studio Code generates the necessary configuration files and builds the development container based on the selected template. The process ensures that your development environment is precisely configured for Azure SQL Database development.

   :::image type="content" source="media/local-dev-experience-dev-containers-quickstart/vscode-azure-sql-devcontainers-build.png" alt-text="Screenshot of Visual Studio Code showing Dev Container build log." lightbox="media/local-dev-experience-dev-containers-quickstart/vscode-azure-sql-devcontainers-build.png":::

1. Visual Studio Code builds the container based on the selected configuration. The build process might take a few minutes the first time.

   :::image type="content" source="media/local-dev-experience-dev-containers-quickstart/vscode-azure-sql-devcontainers-features.png" alt-text="Screenshot of Visual Studio Code showing Dev Container build log with MSSQL feature." lightbox="media/local-dev-experience-dev-containers-quickstart/vscode-azure-sql-devcontainers-features.png":::

1. Once the dev container is built, you can start exploring and verifying the setup. Open a terminal within Visual Studio Code, then check that all necessary tools are installed and working correctly.

   :::image type="content" source="media/local-dev-experience-dev-containers-quickstart/vscode-azure-sql-devcontainers-terminal.png" alt-text="Screenshot of Terminal in Visual Studio Code for verifying Dev Container setup." lightbox="media/local-dev-experience-dev-containers-quickstart/vscode-azure-sql-devcontainers-terminal.png":::

1. As an optional step, you can also run predefined tasks directly from the command palette, streamlining your development workflow and allowing you to focus on writing code.

   This screenshot shows the Visual Studio Code command palette, with the option to run predefined tasks.

   :::image type="content" source="media/local-dev-experience-dev-containers-quickstart/vscode-azure-sql-devcontainers-tasks.png" alt-text="Screenshot of Visual Studio Code command palette showing option to run predefined tasks." lightbox="media/local-dev-experience-dev-containers-quickstart/vscode-azure-sql-devcontainers-tasks.png":::

   This screenshot shows the list of predefined tasks in Visual Studio Code for Dev Containers.

   :::image type="content" source="media/local-dev-experience-dev-containers-quickstart/vscode-azure-sql-devcontainers-task-list.png" alt-text="Screenshot of list of predefined tasks in Visual Studio Code for Dev Containers." lightbox="media/local-dev-experience-dev-containers-quickstart/vscode-azure-sql-devcontainers-task-list.png":::

For more information about a specific template, see [GitHub - azuresql-devcontainers](https://aka.ms/azuresql-devcontainers-repo).

## Related content

- [What is the local development experience for Azure SQL Database?](local-dev-experience-overview.md)
- [What are the Dev Container Templates for Azure SQL Database?](local-dev-experience-dev-containers.md)
- [Quickstart: Create a local development environment for Azure SQL Database](local-dev-experience-quickstart.md)
