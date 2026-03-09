---
title: Local SQL Server Container in Visual Studio Code with MSSQL
description: Learn how to use the MSSQL extension for Visual Studio Code to create, manage, and connect to SQL Server containers for local development, with no Docker CLI required.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: roblescarlos
ms.date: 02/21/2026
ms.service: sql
ms.subservice: vs-code-sql-extensions
ms.topic: overview
ms.collection:
  - data-tools
ai-usage: ai-assisted
---

# Local SQL Server container

With the Local SQL Server container feature in the MSSQL extension for Visual Studio Code, you can create and manage SQL Server containers without manually running Docker commands. Containers can be deployed, started, stopped, and removed directly from the **Connections** view, providing a straightforward way to prototype, develop, and test workloads using the same database engine available in production environments.

:::image type="content" source="media/mssql-local-container/local-container-wizard-1.gif" alt-text="Screenshot showing animation of Local Container wizard part one." lightbox="media/mssql-local-container/local-container-wizard-1.gif":::

By default, the container wizard uses [!INCLUDE [sssql25-md](../../../includes/sssql25-md.md)], which includes AI-ready capabilities such as vector data types and JSON functions. You can also choose from [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)], [!INCLUDE [sssql19-md](../../../includes/sssql19-md.md)], or [!INCLUDE [sssql17-md](../../../includes/sssql17-md.md)], depending on your testing needs.

:::image type="content" source="media/mssql-local-container/local-container-wizard-2.gif" alt-text="Screenshot showing animation of Local Container wizard part two." lightbox="media/mssql-local-container/local-container-wizard-2.gif":::

> [!NOTE]  
> The Local SQL Server container experience works on macOS, Windows, and Linux, as long as Docker Desktop (or an equivalent) is running in Linux container mode.

## Features

Local SQL Server container in the MSSQL extension provides the following capabilities:

- Create a local SQL Server container without writing Docker commands.
- Choose from multiple SQL Server versions ([!INCLUDE [sssql25-md](../../../includes/sssql25-md.md)] (default), [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)], [!INCLUDE [sssql19-md](../../../includes/sssql19-md.md)], and [!INCLUDE [sssql17-md](../../../includes/sssql17-md.md)]).
- Customize the container name, hostname, and port.
- Automatically check Docker installation and running status before deployment.
- Automatically assign an available port if 1433 is already in use.
- Autoconnect and persist connection settings across container restarts.
- Manage the container lifecycle (start, stop, delete) from the context menu in the Connections view.
- Use with all core MSSQL extension features, including Object Explorer, Table Designer, Schema Designer, Query Editor, and GitHub Copilot.

> [!IMPORTANT]  
> Local containers are intended for **development only**. Production deployments aren't supported.

## Create a local container

To create a local SQL Server container:

1. In the **Connections** view, select the **Create local SQL Container** option from the menu.

   :::image type="content" source="media/mssql-local-container/local-container-create.png" alt-text="Screenshot showing how to create local SQL Server container option in the MSSQL extension." lightbox="media/mssql-local-container/local-container-create.png":::

1. Review the **Overview screen**, which highlights what you can expect from the local SQL Server container experience.

   When you're ready, select **Get Started** to continue.

   :::image type="content" source="media/mssql-local-container/local-container-get-started.png" alt-text="Screenshot of the overview screen for local SQL Server container deployment with helpful links and Get Started button." lightbox="media/mssql-local-container/local-container-get-started.png":::

1. The MSSQL extension automatically checks for Docker before deployment:

   - If Docker isn't installed, a message with an install link appears. You must install it before continuing.
   - If Docker is installed but not running, you're prompted to start it. If starting Docker fails, retry or cancel.

   Once all prerequisites are met, select **Next** to continue with deployment.

   :::image type="content" source="media/mssql-local-container/local-container-prerequisites.png" alt-text="Screenshot of Docker prerequisite check-in MSSQL extension." lightbox="media/mssql-local-container/local-container-prerequisites.png":::

1. In the **Deployment settings** panel:

   1. Select the SQL Server version ([!INCLUDE [sssql25-md](../../../includes/sssql25-md.md)] is selected by default).
   1. Enter a password for the `sa` account.
   1. Enter a name for your connection profile (optional).
   1. Optionally complete the advanced options:
      - Container name
      - Port
      - Hostname
   1. Accept the license terms.
   1. Select **Create Container**.

   :::image type="content" source="media/mssql-local-container/local-container-settings.png" alt-text="Screenshot of container setup configuration form." lightbox="media/mssql-local-container/local-container-settings.png":::

### How SQL container deployment works

After you select **Create Container**, the extension handles everything for you:

- Pulls the selected SQL Server image (if not already cached).
- Creates a SQL Server container with the chosen settings.
- Monitors the logs to verify that all databases are fully recovered and ready.
- Creates a connection profile and connects to your container.

When deployment finishes, the container starts, and the MSSQL extension autoconnects to the new database.

## Manage your container

To manage the container, right-click the connection profile name in the **Connections** view. From the context menu, you can:

### Start a stopped container

The extension checks if Docker is running before starting the container. If Docker isn't running, you're prompted to start it.

### Stop a running container

This option shuts down the container while preserving your database state. You can restart it anytime from the same menu.

### Delete the container

This option permanently removes the container and its data. You're asked to confirm before proceeding.

:::image type="content" source="media/mssql-local-container/local-container-stop-delete.png" alt-text="Screenshot showing how to manage container lifecycle in the MSSQL extension." lightbox="media/mssql-local-container/local-container-stop-delete.png":::

> [!IMPORTANT]  
> Deleting a container also removes its associated connection profile.

## Automatic reconnect experience

If your container or Docker isn't running when you reconnect, the extension prompts you to start them. You don't need to troubleshoot manually.

## Supported scenarios

You can use a local SQL Server container with all core features of the MSSQL extension:

- Query Editor and IntelliSense
- Table Designer and Schema Designer
- GitHub Copilot
- Object Explorer and connection management

This feature makes a local SQL Server container ideal for:

- Prototyping new features
- Testing schema changes
- Running automated tests in isolated environments
- Experimenting with [!INCLUDE [sssql25-md](../../../includes/sssql25-md.md)] features

## Limitations

- Requires Docker Desktop (or equivalent) to be installed and running.
- Only supports Linux-based SQL Server containers ([!INCLUDE [sssql17-md](../../../includes/sssql17-md.md)] and later versions).
- No support for Podman.
- The built-in wizard doesn't support backup and restore, clustering, script initialization, or Docker Compose. You can perform these tasks manually using external tools or commands.
- Designed strictly for local development. Not for production use.
- You must resolve any issues with Docker installation, configuration, or runtime.
- Make sure your computer has sufficient resources, or adjust Docker's memory allocation and the container's resource limits through Docker settings to ensure stable performance.
- Memory requirements:
  - SQL Server needs at least 2 GB of memory to start a Linux-based container.
  - By default, SQL Server on Linux uses approximately 80% of the memory available to the container.

## Feedback and support

[!INCLUDE [feedback](../includes/feedback.md)]

## Related content

- [Quickstart: Connect to and query a database with the MSSQL extension for Visual Studio Code](connect-database-visual-studio-code.md)
- [GitHub Copilot for MSSQL extension for Visual Studio Code](../github-copilot/overview.md)
- [Database operations (Preview)](mssql-database-operations.md)
- [Schema Designer](mssql-schema-designer.md)
- [Schema Compare](mssql-schema-compare.md)
- [Visual Studio Code documentation](https://code.visualstudio.com/docs)
- [MSSQL extension for Visual Studio Code repository on GitHub](https://github.com/Microsoft/vscode-mssql)
