---
title: Publish SQL Database Projects from Visual Studio Code
description: Learn how to publish SQL database projects from Visual Studio Code using the Publish dialog.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: iqrashaikh, drskwier
ms.date: 02/04/2026
ms.service: sql
ms.subservice: sql-database-projects
ms.topic: how-to
ms.collection:
  - data-tools
---
# Publish SQL database projects from Visual Studio Code

Use the SQL Database Projects extension for Visual Studio Code to deploy database schema changes directly from a SQL project.

A SQL project deployment takes the schema you define in the project, compares it to the target database, and applies only the necessary changes to bring the database into the desired state through a dynamically generated plan.

You can review changes, generate a deployment script, and publish updates to a target database without leaving the editor.

## Prerequisites

Before you begin, make sure you have:

- Visual Studio Code installed
- The MSSQL extension for Visual Studio Code
- The SQL Database Projects extension
- An existing SQL database project (`.sqlproj`)
- Access to a SQL Server or Azure SQL Database target

## Open the Publish dialog

You can open the Publish dialog from the **Database Projects** view.

1. Open the **Database Projects** view.
1. Right-click your SQL project.
1. Select **Publish**.

The Publish dialog opens in a new editor tab.

:::image type="content" source="media/publish-database-project/publish-dialog.png" alt-text="Screenshot of Publish Project dialog showing publish target, server, and database fields."

## Configure the publish target

Use the Publish dialog to select where to deploy the project.

1. Select a **Publish target**, such as SQL Server or Azure SQL.

1. Select a **Server** connection.

   - If you're not connected, the connection dialog opens.
   - You can select an existing connection or create a new one.

1. Select the target **Database**.

After you establish a connection, the dialog shows more actions.

> [!NOTE]  
> Make sure that the database type you're publishing to matches the project's **Target platform** setting. If they don't match, either change the project's target platform to align with the database, or, if you're confident that the project is compatible, use the advanced option in the Publish dialog that allows publishing to an incompatible platform.

## Generate a deployment script

Before publishing, you can generate a deployment script to review the changes that the process makes to the database.

1. In the Publish dialog, select **Generate Script**.
1. Wait for the script to generate.

The editor opens the generated script for review.

> [!NOTE]  
> The deployment script includes SQLCMD variables for the connection and other deployment settings. Make sure SQLCMD mode is enabled in the editor, so that the variables resolve correctly when you run the script as a query.

:::image type="content" source="media/publish-database-project/deployment-script.png" alt-text="Screenshot of generated deployment script opened for review in the editor.":::

## Publish changes

After reviewing the deployment script, publish the changes.

1. Go back to the Publish dialog.
1. Select **Publish**.
1. If prompted, confirm or select a database connection.

The project is deployed to the selected target database.

## Related content

- [What are SQL database projects?](../../sql-database-projects/sql-database-projects.md)
- [Get started with the SQL Database Projects extension](getting-started-sql-database-projects-extension.md)
- [Tutorial: Create and deploy a SQL project](../../sql-database-projects/tutorials/create-deploy-sql-project.md)
- [MSSQL extension for Visual Studio Code](../mssql/mssql-extension-visual-studio-code.md)
