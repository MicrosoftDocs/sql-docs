---
title: Overview of GitHub Copilot Integration
titleSuffix: MSSQL Extension for Visual Studio Code
description: Introduction to the GitHub Copilot integration with the MSSQL extension for Visual Studio Code that provides AI-assisted SQL development.
author: croblesm
ms.author: roblescarlos
ms.reviewer: randolphwest
ms.date: 01/19/2026
ms.service: sql
ms.subservice: vs-code-sql-extensions
ms.topic: overview
ms.collection:
  - data-tools
  - ce-skilling-ai-copilot
ms.custom:
  - ignite-2025
ai-usage: ai-assisted
---

# GitHub Copilot for MSSQL extension for Visual Studio Code

The GitHub Copilot integration with the MSSQL extension for Visual Studio Code brings AI-assisted development directly into the SQL development workflow.

This new feature empowers developers to write and optimize database code, generate and evolve schemas, understand existing logic, and work more confidently with code-first and data-first database development patterns using GitHub Copilot's intelligent, context-aware suggestions.

This integration is designed for developers, with a focus on the following key personas:

- **Modern application developer**: Builds feature-rich, scalable applications with frameworks like Node.js, Python, .NET, and Go.

- **AI / cloud-native developer**: Specializes in containerized, serverless, and microservices-based applications, often integrating AI-powered capabilities.

- **Solutions architect**: Designs end-to-end systems that align database solutions with broader architectural goals.

- **Database developer**: Focuses on T-SQL, database-specific concepts, and optimizing database workflows.

## What is the MSSQL extension for Visual Studio Code?

The [MSSQL extension in Visual Studio Code](https://aka.ms/vscode-mssql-marketplace) helps developers seamlessly work with their databases. It simplifies the use of SQL database in Fabric, all Azure SQL offerings, and SQL Server as the backend for their applications.

For more information about the extension, visit the [GitHub repository](https://github.com/microsoft/vscode-mssql).

## What is GitHub Copilot for the MSSQL extension for Visual Studio Code?

GitHub Copilot for the MSSQL extension brings AI-driven assistance directly into your SQL development workflow in Visual Studio Code. It helps developers to:

- Explore, design, and evolve database schemas using intelligent, code-first, and data-first guidance.
- Apply contextual suggestions for SQL syntax, relationships, and constraints.
- Write, optimize, and troubleshoot SQL queries with AI-recommended improvements.
- Generate mock data and seed scripts to support testing and development environments.
- Accelerate schema evolution by autogenerating object-relational mapping (ORM) migrations or T-SQL change scripts.
- Understand and document business logic embedded in stored procedures, views, and functions.
- Get security-related recommendations, such as avoiding SQL injection or excessive permissions.
- Receive natural language explanations to help developers unfamiliar with T-SQL write and understand code confidently.
- Reverse-engineer existing databases by explaining SQL schemas and relationships.
- Scaffold backend components, such as data access layers, based on your current database context.

## Supported SQL Server platforms

GitHub Copilot for the MSSQL extension works with all of these SQL Server platforms:

### SQL Server

- [!INCLUDE [sssql19-md](../../../includes/sssql19-md.md)]
- [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)]
- [!INCLUDE [sssql25-md](../../../includes/sssql25-md.md)]
- SQL Server running on any platform:
  - Windows
  - Linux
  - Containers (local and Kubernetes deployments)

### Azure SQL

- Azure SQL Database
- Azure SQL Managed Instance
- SQL Server on Azure Virtual Machines

### Microsoft Fabric

- SQL database in Fabric
- Fabric Data Warehouse
- Fabric Lakehouse (SQL analytics endpoint)

## Target audience

GitHub Copilot for the MSSQL extension is designed for developers who work with applications and SQL databases in Visual Studio Code. It provides intelligent assistance to help you write, optimize, and understand database code more effectively.

| Persona | Description |
| --- | --- |
| **Modern application developer** | Build robust, scalable applications using frameworks like React, Angular, .NET, Django, and Node.js, often following a code-first approach where application logic drives database design. GitHub Copilot streamlines database interactions by assisting with schema generation, query authoring, and integration patterns directly from your codebase, boosting productivity and minimizing context switching. |
| **AI / cloud-native developer** | Build serverless, containerized, and microservices-based solutions that integrate AI capabilities, both within the application and the database layer. GitHub Copilot accelerates development by generating intelligent T-SQL queries, managing schema changes, and assisting with data access patterns common in cloud-native architectures. It also helps developers use AI features built into the database, such as vector search and hybrid retrieval scenarios, to build intelligent, AI-ready applications. |
| **Solutions architect** | Design data-centric systems and ensure consistency across services and environments. GitHub Copilot helps you visualize, validate, and prototype database interactions, making it easier to align database design with system architecture goals. |
| **Database engineer** | Manage schema evolution, write complex T-SQL queries, and optimize performance. GitHub Copilot speeds up development by offering suggestions, explaining code, and identifying potential optimizations, boosting both speed and confidence. |

## Features

| Feature | Description |
| --- | --- |
| **Chat / inline Copilot suggestions** | Engage in natural language conversations with the `@mssql` chat participant or use inline completions for T-SQL or ORM code. Suggestions adapt based on your database schema and active files. |
| **Schema explorer and designer** | Understand, design, and evolve your database schema using AI assistance. Supports object creation, relationships, and reverse engineering. |
| **Smart query builder** | Generate SQL and object-relational mapping (ORM) queries using filters, joins, groupings, and conditions, based on schema awareness and natural language prompts. |
| **Code generation** | Quickly scaffold database code, stored procedures, or ORM-based data access layers based on your current database schema and active files. GitHub Copilot understands your context and can generate repeatable patterns such as CRUD operations or access methods aligned with your development stack. |
| **Query optimizer assistant** | Get AI-generated tips for improving the performance of your SQL queries. GitHub Copilot might suggest better indexing strategies, refactor joins, or spot inefficiencies in WHERE clauses, helpful for developers who aren't experts in performance tuning. Additionally, it supports analysis of execution plans for in-depth analysis, enabling more precise recommendations tailored to your query's actual execution context. |
| **Business logic explainer** | Ask GitHub Copilot to explain in simple terms what a stored procedure, view, or user-defined function does. This feature is especially useful for onboarding new developers who need to understand how business rules are implemented without reading hundreds of lines of T-SQL code. |
| **Security analyzer** | GitHub Copilot can analyze patterns that might expose your code to SQL injection, overly permissive roles, or unencrypted sensitive data. It can also recommend safer ways to handle credentials, user input, and authentication flows, directly in your context. |
| **Localization and formatting helper** | Whether you're building multilingual apps or just ensuring proper sorting and encoding, GitHub Copilot can suggest appropriate collation settings, Unicode usage, and query patterns that support language-specific and region-specific requirements. |
| **Test data generator** | Generate close to realistic, schema-aware sample data (via SQL INSERTs or ORM seeders) to populate your development environment. GitHub Copilot can even extrapolate schema from an existing sample (JSON, CSV, TXT) or autogenerate themed data to support prototyping or testing. |

## Prerequisites

### Install Visual Studio Code

1. Download [Visual Studio Code](https://code.visualstudio.com/download).
1. Complete the installation following the wizard.

## Install the MSSQL extension in Visual Studio Code

To get started with SQL development in Visual Studio Code, install the **MSSQL extension**:

1. Open **Visual Studio Code**.

1. Select the **Extensions** icon in the Activity Bar (**Cmd**+**Shift**+**X** on macOS,  or **Ctrl**+**Shift**+**X** on Windows and Linux).

1. In the **search bar**, type `mssql`.

1. Find **SQL Server (mssql)** in the results and select it.

1. Select the **Install** button.

   :::image type="content" source="media/overview/mssql-extension-vscode.png" alt-text="Screenshot of the MSSQL extension in Visual Studio Code." lightbox="media/overview/mssql-extension-vscode.png":::

> [!TIP]  
> If you're unfamiliar with the MSSQL extension for Visual Studio Code, see the [MSSQL extension for Visual Studio Code](../mssql/mssql-extension-visual-studio-code.md).

### Set up GitHub Copilot in Visual Studio Code

1. Make sure you have a GitHub account. If you don't have one, sign up for free at [GitHub](https://github.com).

1. Ensure you have an active GitHub Copilot subscription. You can start a free trial or purchase a subscription at [GitHub Copilot](https://docs.github.com/copilot/get-started/plans).

1. In Visual Studio Code, open the **Extensions** view (**Cmd**+**Shift**+**X** on macOS, or **Ctrl**+**Shift**+**X** on Windows and Linux).

1. Search for and install both **GitHub Copilot** and **GitHub Copilot Chat** extensions.

   :::image type="content" source="media/overview/github-copilot-extension-vscode.png" alt-text="Screenshot of the GitHub Copilot extension in Visual Studio Code.":::

1. After installation, sign in to your GitHub account. Use the Visual Studio Code command palette `GitHub Copilot: Sign in` or sign in using the GitHub Copilot icon from the status bar at the bottom of the window.

   :::image type="content" source="media/overview/github-copilot-signin-vscode.png" alt-text="Screenshot of the GitHub Copilot sign-in in Visual Studio Code." lightbox="media/overview/github-copilot-signin-vscode.png":::

1. After signing in, you might need to authorize the GitHub Copilot extension to access your GitHub account. Follow the prompts to complete the authorization process.

1. When you sign in, GitHub Copilot is active and ready to assist as you write code in your editor.

For more information, see the official [Quickstart for GitHub Copilot in Visual Studio Code](https://docs.github.com/copilot/get-started/quickstart?tool=visualstudio).

### Connect to a database

To get started with GitHub Copilot for the MSSQL extension, connect to a supported SQL Server or Azure SQL database from the **Connections** view in Visual Studio Code.

> [!NOTE]  
> For step-by-step instructions on how to create a new connection profile and connect to a database, see [Quickstart: Connect to and query a database with the MSSQL extension for Visual Studio Code](../mssql/connect-database-visual-studio-code.md).

## Start chatting with your database

You can start using GitHub Copilot by initiating a chat session with your database:

1. Go to the **Connections** view in the MSSQL extension.

1. Right-click on a connected database.

1. Select **Chat with this database** from the context menu.

   :::image type="content" source="media/overview/vscode-chat-database-context-menu.png" alt-text="Screenshot showing the database context menu, selecting the Chat with this database option.":::

After selecting this option, you need to grant the MSSQL extension access to the language models provided by GitHub Copilot Chat.  
This access is required to enable contextual conversations about your database.

:::image type="content" source="media/overview/vscode-copilot-access-prompt.png" alt-text="Screenshot of Placeholder for Copilot access notification.":::

Once approved, a GitHub Copilot chat window opens in the context of the selected database. You're now ready to ask questions, generate Transact-SQL, and explore schema insights using natural language.

## Manage database context

GitHub Copilot uses your current database connection to provide schema-aware suggestions. The `@mssql` chat participant automatically detects your connection status and adapts its behavior accordingly.

### Connect to a database

When you start an Ask mode session with `@mssql` without an active database connection, the chat participant detects this condition and provides helpful guidance to establish a connection:

:::image type="content" source="media/overview/vscode-copilot-chat-not-connected.png" alt-text="Screenshot showing the @mssql chat participant detecting no database connection and prompting the user to connect.":::

You can connect to a database in multiple ways:

1. **Use GitHub Copilot's chat interface**: When prompted by the `@mssql` participant, select the **Open SQL editor and connect** button to launch the connection dialog.

1. **Use the MSSQL extension**: Use the **Connect** command from the MSSQL extension's **Connections** view to create or select a connection profile.

1. **Use slash commands**: Type `@mssql /connect` in the GitHub Copilot chat to quickly open the connection dialog. For more connection-related slash commands, see [Connection management slash commands](slash-commands.md#connection-management).

1. **Use Agent Mode**: If you're using [Quickstart: Use GitHub Copilot Agent Mode](agent-mode.md), you can connect directly through natural language prompts without requiring a pre-established connection. For more information on how Agent Mode handles connections, see [How connection logic works](agent-mode.md#how-connection-logic-works).

:::image type="content" source="media/overview/vscode-copilot-connection-flow.gif" alt-text="Animation showing the complete flow of connecting to a database through the @mssql chat participant." lightbox="media/overview/vscode-copilot-connection-flow.gif":::

### Welcome message and database context

Once connected, the `@mssql` chat participant greets you with a personalized welcome message that shows your current connection details:

:::image type="content" source="media/overview/vscode-copilot-chat-welcome.png" alt-text="Screenshot of the @mssql chat participant welcome message showing the connected database details.":::

The welcome message displays:

- Your connected server
- Your current database name
- Available capabilities and assistance options

### Switch database contexts

If you want to switch to a different database while working, you have several options:

- **Use the Change Database button**: Use the **Change Database** button in the MSSQL extension sidebar.

  :::image type="content" source="media/overview/vscode-copilot-change-database-1.png" alt-text="Screenshot of the GitHub Copilot chat window and MSSQL extension changing database context using the change database option in Visual Studio Code." lightbox="media/overview/vscode-copilot-change-database-1.png":::

- **Use the status bar**: Select the status bar panel that displays the current connection (server, database, user). This action opens a dropdown list where you can select a different database from your configured profiles.

  :::image type="content" source="media/overview/vscode-copilot-change-database-2.png" alt-text="Screenshot of the GitHub Copilot chat window and MSSQL extension changing database context using the status bar in Visual Studio Code." lightbox="media/overview/vscode-copilot-change-database-2.png":::

  > [!IMPORTANT]  
  > GitHub Copilot requires an active database connection to provide meaningful, schema-aware suggestions. Without a connection, the `@mssql` participant guides you to establish one before proceeding with database-related tasks.

- **Use slash commands**: Type `@mssql /changeDatabase` in the GitHub Copilot chat to quickly switch to a different database. For more information, see [Connection management slash commands](slash-commands.md#connection-management).

- **Use Agent Mode tools**: Use natural language prompts with Agent Mode to switch databases. For more information, see [Connection management in Agent Mode](agent-mode.md#connection-management).

  > [!NOTE]  
  > When using [GitHub Copilot Agent Mode](agent-mode.md), you can connect to databases without requiring a pre-established connection. Agent Mode uses tools contributed by the MSSQL extension to handle connections through natural language prompts or chat variables like `#mssql_connect`. For details, see [How connection logic works](agent-mode.md#how-connection-logic-works).

This intelligent connection detection gives you a seamless experience, whether you're connecting for the first time or switching between environments while retaining context-aware assistance from GitHub Copilot.

## Share your experience

[!INCLUDE [copilot-feedback](../includes/copilot-feedback.md)]

## Related content

- [Quickstart: Use GitHub Copilot slash commands](slash-commands.md)
- [Quickstart: Use GitHub Copilot Agent Mode](agent-mode.md)
- [Quickstart: Use chat and inline GitHub Copilot suggestions](inline-copilot-suggestions.md)
- [Quickstart: Generate code](code-generation.md)
- [Quickstart: Use the schema explorer and designer](schema-explorer-designer.md)
- [Quickstart: Use the smart query builder](smart-query-builder.md)
- [Quickstart: Query optimizer assistant](query-optimizer-assistant.md)
- [Quickstart: Use the business logic explainer](business-logic-explainer.md)
- [Quickstart: Security analyzer](security-analyzer.md)
- [Quickstart: Localization and formatting helper](localization-formatting-helper.md)
- [Quickstart: Generate data for testing and mocking](test-and-mocking-data-generator.md)
- [Limitations and known issues](limitations-and-known-issues.md)
