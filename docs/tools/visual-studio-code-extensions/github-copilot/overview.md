---
title: Overview of GitHub Copilot Integration
titleSuffix: MSSQL Extension for Visual Studio Code
description: Introduction to the GitHub Copilot integration with the MSSQL extension for Visual Studio Code that provides AI-assisted SQL development.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: roblescarlos
ms.date: 06/01/2026
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

The GitHub Copilot integration with the MSSQL extension for Visual Studio Code provides AI-assisted SQL development. You can use it to write and optimize database code, generate and modify schemas, understand existing logic, and work with code-first and data-first database development patterns.

This integration is designed for developers, with a focus on the following personas:

- **Modern application developer**: Builds feature-rich, scalable applications with frameworks like Node.js, Python, .NET, and Go.

- **AI / cloud-native developer**: Specializes in containerized, serverless, and microservices-based applications, often integrating AI-powered capabilities.

- **Solutions architect**: Designs end-to-end systems that align database solutions with broader architectural goals.

- **Database developer**: Focuses on Transact-SQL (T-SQL), database-specific concepts, and optimizing database workflows.

## What is the MSSQL extension for Visual Studio Code?

The MSSQL extension for Visual Studio Code supports SQL database in Fabric, Azure SQL Database, Azure SQL Managed Instance, and SQL Server.

For more information about the extension, see [MSSQL extension for Visual Studio Code](../mssql/mssql-extension-visual-studio-code.md).

## What is GitHub Copilot for the MSSQL extension for Visual Studio Code?

GitHub Copilot for the MSSQL extension provides AI assistance for SQL development in Visual Studio Code. It can help you:

- Explore, design, and modify database schemas using code-first and data-first approaches.
- Suggest SQL syntax, relationships, and constraints based on your schema context.
- Write, optimize, and troubleshoot SQL queries.
- Generate mock data and seed scripts for testing and development.
- Autogenerate object-relational mapping (ORM) migrations or T-SQL change scripts.
- Explain business logic in stored procedures, views, and functions.
- Identify security problems such as SQL injection risks or excessive permissions.
- Provide natural language explanations of T-SQL code.
- Describe existing database schemas and relationships.
- Scaffold data access layers and other backend components based on your database schema.

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

GitHub Copilot for the MSSQL extension is designed for developers who work with applications and SQL databases in Visual Studio Code.

| Persona | Description |
| --- | --- |
| **Modern application developer** | Build applications using frameworks like React, Angular, .NET, Django, and Node.js. GitHub Copilot assists with schema generation, query authoring, and integration patterns from your codebase. |
| **AI / cloud-native developer** | Build serverless, containerized, and microservices-based solutions. GitHub Copilot generates T-SQL queries, manages schema changes, and assists with data access patterns for cloud-native architectures, including vector search and hybrid retrieval scenarios. |
| **Solutions architect** | Design data-centric systems across services and environments. GitHub Copilot helps you visualize, validate, and prototype database interactions. |
| **Database engineer** | Manage schema evolution, write T-SQL queries, and optimize performance. GitHub Copilot offers suggestions, explains code, and identifies potential optimizations. |

## Choose a mode for your task

GitHub Copilot offers several interaction modes. Use this table to pick the right one.

| Task | Ask mode | Edit mode | Agent mode | Plan mode |
| --- | --- | --- | --- | --- |
| Explain a stored procedure | Recommended | No | Yes | No |
| Generate a query for a schema | Recommended | No | Yes | No |
| Refactor a query across multiple files | No | Recommended | Yes | No |
| Add audit columns to every table and update related procedures | No | Yes | Recommended | Yes |
| Design a full data model from a product requirements document | No | No | Yes | Recommended |
| Connect, switch database, run query in chat | [Slash commands](slash-commands.md) | No | Recommended | No |
| Design a schema visually with artificial intelligence assistance | [Schema Designer scenarios](schema-designer-scenarios.md) | No | No | No |

For a deeper explanation of each mode, see [How GitHub Copilot works with the MSSQL extension](how-it-works.md).

## Interaction surfaces

Each surface has different schema awareness. Know which surface to use when you need schema-aware suggestions.

| Surface | Provided by | Schema-aware? | Article |
| --- | --- | --- | --- |
| Chat participant (`@mssql`) | MSSQL extension | Yes (connected database) | [Chat with `@mssql` (ask mode)](chat-ask-mode.md) |
| Agent mode tools | MSSQL extension contributes tools | Yes (via tool calls) | [Agent mode](agent-mode.md) |
| Plan mode | Visual Studio Code | Yes (via `@mssql` context) | [Plan mode](plan-mode.md) |
| Slash commands | MSSQL extension | Yes | [Slash commands](slash-commands.md) |
| Inline completions (ghost text) | GitHub Copilot model directly | **No** | [Inline completions](inline-completions.md) |
| Schema Designer canvas | Embedded GitHub Copilot | Yes | [Schema Designer scenarios](schema-designer-scenarios.md) |
| Data API builder canvas | Embedded GitHub Copilot | Yes | [Data API builder](../mssql/mssql-data-api-builder.md) |

Inline completions (ghost text in `.sql` files) **don't see your connected database schema**. For schema-aware SQL suggestions, use the [`@mssql` chat participant](chat-ask-mode.md). For the architectural reason, see [How GitHub Copilot works with the MSSQL extension](how-it-works.md).

## Features

| Feature | Status | Description |
| --- | --- | --- |
| [Chat with `@mssql` (ask mode)](chat-ask-mode.md) | GA | Natural-language conversations with the `@mssql` chat participant. Schema-aware suggestions based on your connected database and active files. |
| [Agent mode](agent-mode.md) | GA | Multi-step workflows where GitHub Copilot orchestrates MSSQL extension tools (connect, list databases, run query). Requires your approval on each tool invocation. |
| [Plan mode](plan-mode.md) | GA | Reason about a data model before writing SQL data definition language. Pair with product requirements documents for spec-driven design. |
| [Slash commands](slash-commands.md) | GA | Structured prompts for common tasks: connect, list databases, run query, show schema. Faster than typing a full natural-language prompt. |
| [Inline completions](inline-completions.md) | GA | Ghost text while typing. Useful for common SQL patterns. Doesn't see your database schema. |
| [Custom instructions](custom-instructions.md) | GA | Teach GitHub Copilot your team's Transact-SQL (T-SQL) conventions. Applies across ask, edit, agent, and inline completions. |
| [Smart query builder](smart-query-builder.md) | GA | Generate SQL and object-relational mapping (ORM) queries from natural language with schema awareness. |
| [Code generation](code-generation.md) | GA | Scaffold stored procedures, tables, and ORM data access code from your schema. |
| [Query optimizer assistant](query-optimizer-assistant.md) | GA | Performance suggestions, indexing strategies, execution plan analysis. |
| [Business logic explainer](business-logic-explainer.md) | GA | Natural-language explanations of stored procedures, views, and user-defined functions. |
| [Security analyzer](security-analyzer.md) | GA | Detect SQL injection patterns, overly permissive roles, and unencrypted sensitive data. |
| [Localization and formatting helper](localization-formatting-helper.md) | GA | Collation, Unicode, and region-specific query patterns. |
| [Test data generator](test-and-mocking-data-generator.md) | GA | Generate realistic seed data and `INSERT` statements. |
| [Schema Designer with GitHub Copilot](schema-designer-scenarios.md) | GA | Embedded artificial intelligence in the visual Schema Designer canvas. Create, evolve, and review schemas with live diagram updates. |
| [Data API builder with GitHub Copilot](../mssql/mssql-data-api-builder.md) | GA | Configure REST, GraphQL, and Model Context Protocol (MCP) endpoints using natural language. |
| [Schema explorer (chat-based)](schema-explorer-designer.md) | GA | Prompt-driven schema exploration, creation, and reverse engineering via `@mssql` chat. |
| [Object-relational mapping integrations](orm-integrations.md) | GA | Reference for Entity Framework, Prisma, Sequelize, SQLAlchemy, Django ORM, TypeORM, Drizzle, and Dapper. |

## Prerequisites

### Install Visual Studio Code

1. Download [Visual Studio Code](https://code.visualstudio.com/download).
1. Complete the installation by following the wizard.

## Install the MSSQL extension for Visual Studio Code

To get started with SQL development in Visual Studio Code, install the **[MSSQL extension](../mssql/mssql-extension-visual-studio-code.md)**:

1. Open **Visual Studio Code**.

1. Select the **Extensions** icon in the Activity Bar (**Cmd**+**Shift**+**X** on macOS, or **Ctrl**+**Shift**+**X** on Windows and Linux).

1. In the **search bar**, type `mssql`.

1. Find **SQL Server (mssql)** in the results and select it.

1. Select the **Install** button.

   :::image type="content" source="media/overview/mssql-extension-vscode.png" alt-text="Screenshot of the MSSQL extension for Visual Studio Code." lightbox="media/overview/mssql-extension-vscode.png":::

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
> For step-by-step instructions on how to create a new connection profile and connect to a database, see [Connect to a database with the MSSQL extension for Visual Studio Code](../mssql/mssql-database-connections.md).

## Start chatting with your database

You can start using GitHub Copilot by initiating a chat session with your database:

1. Go to the **Connections** view in the MSSQL extension.

1. Right-click on a connected database.

1. Select **Chat with this database** from the context menu.

   :::image type="content" source="media/overview/vscode-chat-database-context-menu.png" alt-text="Screenshot showing the database context menu, selecting the Chat with this database option.":::

After selecting this option, you need to grant the MSSQL extension access to the language models provided by GitHub Copilot Chat.  
This access is required to enable contextual conversations about your database.

:::image type="content" source="media/overview/vscode-copilot-access-prompt.png" alt-text="Screenshot of placeholder for Copilot access notification.":::

Once you approve access, a GitHub Copilot chat window opens in the context of the selected database. You're now ready to ask questions, generate Transact-SQL, and explore schema insights using natural language.

## Manage database context

GitHub Copilot uses your current database connection to provide schema-aware suggestions. The `@mssql` chat participant automatically detects your connection status and adapts its behavior accordingly.

### Connect to a database

When you start an Ask mode session with `@mssql` without an active database connection, the chat participant detects this condition and provides helpful guidance to establish a connection:

:::image type="content" source="media/overview/vscode-copilot-chat-not-connected.png" alt-text="Screenshot showing the @mssql chat participant detecting no database connection and prompting the user to connect.":::

You can connect to a database in multiple ways:

1. **Use GitHub Copilot's chat interface**: When prompted by the `@mssql` participant, select the **Open SQL editor and connect** button to launch the connection dialog.

1. **Use the MSSQL extension**: Use the **Connect** command from the MSSQL extension's **Connections** view to create or select a connection profile.

1. **Use slash commands**: Type `@mssql /connect` in the GitHub Copilot chat to quickly open the connection dialog. For more connection-related slash commands, see [Connection management slash commands](slash-commands.md#connection-management).

1. **Use Agent Mode**: If you're using [GitHub Copilot agent mode](agent-mode.md), you can connect directly through natural language prompts without requiring a pre-established connection. For more information on how Agent Mode handles connections, see [How connection logic works](agent-mode.md#how-connection-logic-works).

:::image type="content" source="media/overview/vscode-copilot-connection-flow.gif" alt-text="Animation showing the complete flow of connecting to a database through the @mssql chat participant." lightbox="media/overview/vscode-copilot-connection-flow.gif":::

### Welcome message and database context

Once connected, the `@mssql` chat participant displays a welcome message with your current connection details:

:::image type="content" source="media/overview/vscode-copilot-chat-welcome.png" alt-text="Screenshot of the @mssql chat participant welcome message showing the connected database details.":::

The welcome message displays:

- Your connected server
- Your current database name
- Available capabilities and assistance options

### Switch database contexts

To switch to a different database while working, use one of the following options:

- **Change Database button**: Use the **Change Database** button in the MSSQL extension sidebar.

  :::image type="content" source="media/overview/vscode-copilot-change-database-1.png" alt-text="Screenshot of the GitHub Copilot chat window and MSSQL extension changing database context using the change database option in Visual Studio Code." lightbox="media/overview/vscode-copilot-change-database-1.png":::

- **Status bar**: Select the status bar panel that displays the current connection (server, database, user). This action opens a dropdown list where you can select a different database from your configured profiles.

  :::image type="content" source="media/overview/vscode-copilot-change-database-2.png" alt-text="Screenshot of the GitHub Copilot chat window and MSSQL extension changing database context using the status bar in Visual Studio Code." lightbox="media/overview/vscode-copilot-change-database-2.png":::

  > [!IMPORTANT]  
  > GitHub Copilot requires an active database connection to provide meaningful, schema-aware suggestions. Without a connection, the `@mssql` participant guides you to establish one before proceeding with database-related tasks.

- **Slash commands**: Type `@mssql /changeDatabase` in the GitHub Copilot chat to quickly switch to a different database. For more information, see [Connection management slash commands](slash-commands.md#connection-management).

- **Agent Mode tools**: Use natural language prompts with Agent Mode to switch databases. For more information, see [Connection management in Agent Mode](agent-mode.md#connection-management).

  > [!NOTE]  
  > When using [GitHub Copilot agent mode](agent-mode.md), you can connect to databases without requiring a pre-established connection. Agent Mode uses tools contributed by the MSSQL extension to handle connections through natural language prompts or chat variables like `#mssql_connect`. For details, see [How connection logic works](agent-mode.md#how-connection-logic-works).

GitHub Copilot detects your connection state and provides context-aware assistance whether you're connecting for the first time or switching between environments.

## Share your experience

[!INCLUDE [copilot-feedback](../includes/copilot-feedback.md)]

## Related content

- [Quickstart: Use GitHub Copilot slash commands](slash-commands.md)
- [Quickstart: Use GitHub Copilot agent mode](agent-mode.md)
- [Quickstart: Chat with the `@mssql` participant (ask mode)](chat-ask-mode.md)
- [Quickstart: Generate code](code-generation.md)
- [Quickstart: Use the schema explorer and designer](schema-explorer-designer.md)
- [Quickstart: Use the smart query builder](smart-query-builder.md)
- [Quickstart: Query optimizer assistant](query-optimizer-assistant.md)
- [Quickstart: Use the business logic explainer](business-logic-explainer.md)
- [Quickstart: Security analyzer](security-analyzer.md)
- [Quickstart: Localization and formatting helper](localization-formatting-helper.md)
- [Quickstart: Generate data for testing and mocking](test-and-mocking-data-generator.md)
- [GitHub Copilot integration in Schema Designer](../mssql/mssql-schema-designer-copilot.md)
- [Data API builder](../mssql/mssql-data-api-builder.md)
- [Limitations and known issues](limitations-and-known-issues.md)
