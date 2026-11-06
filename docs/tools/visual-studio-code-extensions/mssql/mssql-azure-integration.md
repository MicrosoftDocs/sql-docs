---
title: Create an Azure SQL Database (Free Tier) in Visual Studio Code
titleSuffix: MSSQL Extension for Visual Studio Code
description: Learn how to use the MSSQL extension for Visual Studio Code to connect to Azure and provision Azure SQL databases directly from your editor.
author: cwebb-msft
ms.author: chweb
ms.reviewer: roblescarlos, tsiddique
ms.date: 08/17/2026
ms.service: sql
ms.subservice: vs-code-sql-extensions
ms.topic: overview
ms.collection:
  - data-tools
ai-usage: ai-assisted
---

# Create an Azure SQL Database

This article shows you how to quickly provision an Azure SQL Database (free tier) using the [MSSQL extension for Visual Studio Code](mssql-extension-visual-studio-code.md).

This experience provides free tier provisioning only that you can't customize. You can't change compute, storage, or pricing options.

To configure advanced settings or use paid tiers, deploy your database through the [Azure portal](https://portal.azure.com).

The MSSQL extension for Visual Studio Code integrates with Azure so you can provision Azure SQL databases and connect to them without leaving the editor. A guided wizard on the **Deployments** page authenticates with your Azure account and creates a fully managed Azure SQL database in minutes. The provisioning experience uses the free tier's monthly allowances. Review [eligibility, limits, and pause or paid-overage behavior](/azure/azure-sql/database/free-tier) before creating resources. The free tier is intended primarily for development and proof-of-concept scenarios and has no SLA; supporting resources can have separate charges.

## Features

Azure integration in the MSSQL extension offers the following capabilities:

- Provision new Azure SQL databases directly from Visual Studio Code with the free tier's monthly allowances.
- Authenticate with your Azure account using Microsoft Entra ID with persistent sign-in.
- Configure subscription, resource group, server, and database on a single page in a guided wizard.
- Choose what happens when the free monthly limit is reached: autopause until the next calendar month, or continue at standard serverless rates.
- Autoconnect to the new database the moment provisioning completes, with the connection added to your saved profiles.

## Prerequisites

Before you provision an Azure SQL database from Visual Studio Code, make sure you meet the following requirements:

- Install the MSSQL extension for Visual Studio Code. For installation steps, see the [MSSQL extension for Visual Studio Code](mssql-extension-visual-studio-code.md).
- Have an active Azure account. If you don't have one, [create a free Azure account](https://azure.microsoft.com/free/).
- Have permissions to create resources in the target Azure subscription and resource group.

## Provision an Azure SQL database

You can provision a new Azure SQL database from the MSSQL extension's **Deployments** page.

1. In the MSSQL extension Activity Bar, select the **Deployments** view.

1. Select **Create an Azure SQL Database**.

   :::image type="content" source="media/mssql-azure-integration/deployment.png" alt-text="Screenshot showing the deployment options.":::

1. Sign in with your Azure account when prompted.

1. Configure the database on a single page:

   - **Subscription**: Select the Azure subscription that owns the new database.
   - **Resource group**: Select an existing resource group or create a new one.
   - **Server**: Select an existing Azure SQL logical server or create a new one. New servers prompt for an admin sign-in and password.
   - **Database name**: Enter a name for the new database.
   - **Optional Settings**: Add a profile name, connection group, data source, or tags.

   :::image type="content" source="media/mssql-azure-integration/create-new-database.png" alt-text="Screenshot showing the Create screen.":::

1. For the free tier, choose what happens when the monthly limit is reached:

   - **Auto-pause** the database until the next calendar month.
   - **Continue at standard serverless rates**.

1. Select **Create Database**. The extension creates the database and adds the connection to your saved profiles. The new connection autoconnects when provisioning completes.

## After provisioning

Use the database you just provisioned rather than creating another database for a tutorial.

1. [Verify the deployment](/azure/azure-sql/database/free-tier#verify-the-deployment), including the intended subscription, resource group, server, free tier configuration, and monthly-limit behavior. If provisioning fails or its status is uncertain, inspect the existing deployment before trying again; don't create another database or switch to a paid configuration as a fallback.
1. Find the new saved connection in **Object Explorer**. If it isn't connected, use the saved connection to reconnect. For connection or authentication help, see [Connect to a database](mssql-database-connections.md).
1. Open a query editor connected to the new database and run `SELECT DB_NAME() AS current_database;`. Confirm that the result matches the database name you provisioned before running sample statements.
1. Follow [Use an existing database](mssql-run-first-query.md#use-an-existing-database) in the first-query quickstart, then create its sample table, insert rows, and query the results. Skip the generic **Create a database** step: `CREATE DATABASE Library` doesn't provision a free tier database. The sample table steps don't require Adventure Works or an imported database.
1. When you finish, follow the quickstart's [sample-object cleanup](mssql-run-first-query.md#clean-up-resources) instructions. Keep your database if you plan to continue the project.

[Track your remaining allowance](/azure/azure-sql/database/free-tier#track-free-usage) and review [pause and overage behavior](/azure/azure-sql/database/free-tier#considerations-and-limitations). Disconnect query editors and the Object Explorer connection when you're done. Open connections can prevent idle auto-pause; disconnecting doesn't restore vCore seconds already consumed.

After you run your first query, you can continue your workflow in the same database. Review permissions, generated changes, and costs for any additional services before using these optional tools:

- Open the [Schema Designer](mssql-schema-designer.md) to design and evolve your data model. With [GitHub Copilot integration in Schema Designer](mssql-schema-designer-copilot.md), you can describe your schema in natural language and let GitHub Copilot generate tables and relationships.
- Use [Data API builder](mssql-data-api-builder.md) to generate REST, GraphQL, and Model Context Protocol (MCP) endpoints from your schema. It works with the same Azure SQL connection.
- Use [SQL Notebooks](mssql-sql-notebooks.md) or the standard query editor to build queries and explore data.

Together, these experiences give you an end-to-end path from a provisioned Azure SQL database to a working back end, all inside Visual Studio Code.

## Limitations

- Only Azure SQL Database is currently supported. Azure SQL Managed Instance and SQL Server on Azure Virtual Machines aren't supported.
- The free tier follows standard Azure SQL Database free tier limits. After you reach the monthly limit, your chosen behavior (autopause or continue) applies.
- You need Microsoft Entra ID authentication for provisioning.

## Feedback and support

[!INCLUDE [feedback](../includes/feedback.md)]

## Related content

- [Connect to a database with the MSSQL extension for Visual Studio Code](mssql-database-connections.md)
- [Quickstart: Run your first query with the MSSQL extension for Visual Studio Code](mssql-run-first-query.md)
- [Schema Designer](mssql-schema-designer.md)
- [GitHub Copilot integration in Schema Designer](mssql-schema-designer-copilot.md)
- [Data API builder](mssql-data-api-builder.md)
- [SQL Notebooks](mssql-sql-notebooks.md)
- [MSSQL extension for Visual Studio Code](mssql-extension-visual-studio-code.md)
- [Deploy Azure SQL Database for free](/azure/azure-sql/database/free-tier)
