---
title: "Quickstart: Chat / Inline GitHub Copilot Suggestions"
titleSuffix: MSSQL Extension for Visual Studio Code
description: Learn how to use GitHub Copilot's inline suggestions and chat experience to enhance your SQL development in Visual Studio Code.
author: croblesm
ms.author: roblescarlos
ms.reviewer: randolphwest
ms.date: 01/19/2026
ms.service: sql
ms.subservice: vs-code-sql-extensions
ms.topic: quickstart
ms.collection:
  - data-tools
  - ce-skilling-ai-copilot
ms.custom:
  - ignite-2025
ai-usage: ai-assisted
---

# Quickstart: Use chat and inline GitHub Copilot suggestions

GitHub Copilot provides both inline suggestions while typing in the code editor and an interactive chat experience. You can ask the chat participant questions or provide prompts by typing `@mssql` followed by your prompt.

## Get started

[!INCLUDE [get-started](../includes/get-started.md)]

## Chat with the MSSQL's chat participant in Visual Studio Code

Use the `@mssql` chat participant in GitHub Copilot Chat to bring intelligent, context-aware assistance into your SQL development workflow, all directly within Visual Studio Code. Whether you're writing queries, evolving your schema, or integrating with application code, GitHub Copilot can help you design and understand relational models, generate or optimize T-SQL code, create seed data, scaffold ORM migrations, and even explain business logic or security concerns using natural language, all tailored to your connected database context.

Here are common use cases and examples of what you can ask via the chat participant:

### List or explore objects in your database schema

Ask questions about tables, columns, schemas, and object metadata in your database.

#### Group objects by type

```copilot-prompt
Show all objects in the `SalesLT` schema of my current database, grouped by type.
```

#### List columns and properties of a table

```copilot-prompt
List the columns, data types, and nullability of the `SalesLT.Customer` table.
```

#### Count tables, views, and procedures in a database

```copilot-prompt
How many tables, views, and procedures are defined in my current database?
```

### Write simple queries

Get help writing common SQL queries for filtering, aggregation, and joins.

#### Return list of customers based on recent orders

```copilot-prompt
Write a T-SQL query to list all customers from `SalesLT.Customer` who placed an order in the last 30 days based on the latest order date.
```

#### Calculate average order total per customer

```copilot-prompt
Generate a query that calculates the average order total per customer from the `SalesLT.SalesOrderHeader` table, sorted descending.
```

#### Update query with another column

```copilot-prompt
Update the previous query to include the full name of each customer from the `SalesLT.Customer` table.
```

### Explain relationships or concepts

Ask for simplified explanations of schema relationships, query logic, or features relevant to development.

#### Describe foreign keys relationships between tables

```copilot-prompt
Describe the foreign key relationship between `SalesLT.SalesOrderHeader` and `SalesLT.Customer` tables in my current database.
```

#### Explain table relationships and keys involved

```copilot-prompt
I'm a developer new to T-SQL. Explain how `SalesLT.SalesOrderHeader` is related to `SalesLT.Customer`, and what keys are involved.
```

#### Explain vector data types and usage options

```copilot-prompt
Explain how vector data types work in SQL Server and when to use them for AI scenarios.
```

### Generate migration or integration code

Request help generating SQL or ORM-based migration scripts.

#### Add foreign key constraint to a table

```copilot-prompt
Create a T-SQL script to add a foreign key constraint on `SalesLT.SalesOrderDetail.ProductID` referencing `SalesLT.Product.ProductID`.
```

#### Generate migration script to add a foreign key

```copilot-prompt
Generate a Sequelize migration to add a foreign key from `SalesLT.SalesOrderDetail.ProductID` to `SalesLT.Product.ProductID`, assuming both exist.
```

## Use inline suggestions with GitHub Copilot

You can start by typing a T-SQL query in a new editor window, like `SELECT * FROM SalesLT.Customer`, and observe the inline suggestions provided by GitHub Copilot. The suggestions appear as you type, and you can accept them by pressing `Tab` or `Enter`.

Alternatively, you can type the same query, `SELECT * FROM SalesLT.Customer`, directly into the editor. Then, highlight it to reveal the ✨ **smart action** icon, which appears next to the highlighted query. This icon provides quick access to additional GitHub Copilot options, such as `Modify using Copilot` to adjust your query, `/doc` to generate documentation, or the ability to ask GitHub Copilot general questions related to the query.

:::image type="content" source="media/inline-copilot-suggestions/vscode-smart-action-icon.png" alt-text="Screenshot showing the smart action icon for modifying SQL queries using GitHub Copilot in Visual Studio Code.":::

When you select **Review using GitHub Copilot**, you see an inline recommendation like this:

:::image type="content" source="media/inline-copilot-suggestions/vscode-inline-recommendation.png" alt-text="Screenshot showing an inline recommendation from GitHub Copilot for optimizing a SQL query in Visual Studio Code." lightbox="media/inline-copilot-suggestions/vscode-inline-recommendation.png":::

You can also invoke GitHub Copilot using a shortcut (**Cmd**+**I** for macOS, or **Ctrl**+**I** for Windows and Linux) and ask questions or request modifications to your query.

:::image type="content" source="media/inline-copilot-suggestions/vscode-copilot-shortcut.png" alt-text="Screenshot demonstrating how to invoke GitHub Copilot using the keyboard shortcut in Visual Studio Code." lightbox="media/inline-copilot-suggestions/vscode-copilot-shortcut.png":::

## Share your experience

[!INCLUDE [copilot-feedback](../includes/copilot-feedback.md)]

## Related content

- [GitHub Copilot for MSSQL extension for Visual Studio Code](overview.md)
- [Quickstart: Generate code](code-generation.md)
- [Quickstart: Use the schema explorer and designer](schema-explorer-designer.md)
- [Quickstart: Use the smart query builder](smart-query-builder.md)
- [Quickstart: Query optimizer assistant](query-optimizer-assistant.md)
- [Quickstart: Use the business logic explainer](business-logic-explainer.md)
- [Quickstart: Security analyzer](security-analyzer.md)
- [Quickstart: Localization and formatting helper](localization-formatting-helper.md)
- [Quickstart: Generate data for testing and mocking](test-and-mocking-data-generator.md)
- [Limitations and known issues](limitations-and-known-issues.md)
