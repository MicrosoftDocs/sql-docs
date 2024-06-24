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

- **Docker**: Required for running containers. [Download Docker](https://www.docker.com/get-started)
- **Visual Studio Code**: The primary IDE for this quickstart. [Download Visual Studio Code](https://code.visualstudio.com/)
- **Dev Containers extension for Visual Studio Code**: Enables working with Dev Containers. [Install the extension](https://marketplace.visualstudio.com/items?itemName=ms-vscode-remote.remote-containers)
- **Git**: For version control. [Download Git](https://git-scm.com/)

## Steps to set up the development environment

1. Open Visual Studio Code.

1. Press `F1` or `Ctrl+Shift+P` to open the command palette.

1. Select the **Dev Containers: New Dev Container** command.

1. Select the desired Dev Container template for Azure SQL Database, typing `*Azure SQL*`.

1. Select one of the following templates:
        - .NET with Aspire and Azure SQL (dotnet-aspire)
        - .NET and Azure SQL (dotnet)
        - Python and Azure SQL (python)
        - Node.js and Azure SQL (node)

1. Wait for the container to build.
    - Visual Studio Code will build the container based on the selected configuration.
    - The build process might take a few minutes the first time.

1. Verify the setup.
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

    - For .NET projects, verify the build and restore process:

        ```bash
        # Restore NuGet packages
        dotnet restore

        # Build the project    
        dotnet build      
        ```

    - For Python projects, verify the virtual environment setup:

        ```bash
        # Verify installed Python packages
        pip list   
        ```

    - For Node.js projects, verify package installations:

        ```bash
        # Verify installed Node.js packages
        npm list   
        ```

6. Run predefined tasks.

    - To run any of the predefined tasks, open the command palette (`F1`), type `Run Task`, and select the desired task. For example, **Execute SQL Query**, **Build SQL Database Project**.

For more information about a specific template, see [Azure SQL Database Dev Container templates](./local-dev-experience-dev-containers.md).

## Related content

Learn more about the local development experience for Azure SQL Database:

- [Overview - Azure SQL Database Dev Container templates](./local-dev-experience-dev-containers.md)
- [Quickstart: Set up a development environment with an Azure SQL Database Dev Container template](./local-dev-experience-dev-containers-quickstart.md)
- [Quickstart: Create a local development environment for Azure SQL Database](./local-dev-experience-quickstart.md)
