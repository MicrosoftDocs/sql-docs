---
title: "Quickstart: Chat with @mssql (Ask Mode)"
titleSuffix: MSSQL Extension for Visual Studio Code
description: Learn how to use the @mssql chat participant in GitHub Copilot ask mode to write schema-aware SQL queries, explain relationships, and generate migrations in Visual Studio Code.
author: cwebb-msft
ms.author: chweb
ms.reviewer: roblescarlos
ms.date: 06/01/2026
ms.service: sql
ms.subservice: vs-code-sql-extensions
ms.topic: quickstart
ms.collection:
  - data-tools
  - ce-skilling-ai-copilot
ai-usage: ai-assisted
---

# Quickstart: Chat with the `@mssql` participant (ask mode)

The `@mssql` chat participant brings schema-aware SQL assistance into your GitHub Copilot Chat conversations. In ask mode, you have a natural-language conversation with `@mssql` about your connected database: explore tables and relationships, write Transact-SQL (T-SQL) queries, generate migrations, and get explanations of existing objects. Ask mode is read-only. It answers questions and proposes code, but never modifies files.

> [!TIP]  
> Use ask mode when you need an answer or a one-shot query. Use [agent mode](agent-mode.md) for multi-step workflows that involve tool execution. Use [edit mode](https://code.visualstudio.com/docs/copilot/chat/copilot-chat) when you need targeted changes to specific files.

## Key takeaways

- `@mssql` in ask mode is **schema-aware** when you have an active database connection.
- The chat participant reads schema metadata (tables, columns, relationships) and includes it in each request.
- Ask mode is conversational and stateless. Each message is a new question.
- Your [custom instructions](custom-instructions.md) apply to every ask mode response.

## Prerequisites

[!INCLUDE [get-started](../includes/get-started.md)]

## Chat with the `@mssql` participant

Use `@mssql` in GitHub Copilot Chat to bring intelligent, context-aware assistance into your SQL development workflow. Whether you're writing queries, exploring schema, or generating migration scripts, GitHub Copilot tailors responses to your connected database.

Here are common use cases and examples.

### List or explore objects in your database schema

Ask questions about tables, columns, schemas, and object metadata.

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

### Write queries

Get help writing common SQL queries for filtering, aggregation, and joins.

#### Return a list of customers based on recent orders

```copilot-prompt
Write a T-SQL query to list all customers from `SalesLT.Customer` who placed
an order in the last 30 days based on the latest order date.
```

#### Calculate average order total per customer

```copilot-prompt
Generate a query that calculates the average order total per customer
from the `SalesLT.SalesOrderHeader` table, sorted descending.
```

#### Update a query with another column

```copilot-prompt
Update the previous query to include the full name of each customer
from the `SalesLT.Customer` table.
```

### Explain relationships or concepts

Ask for simplified explanations of schema relationships, query logic, or T-SQL features.

#### Describe foreign key relationships between tables

```copilot-prompt
Describe the foreign key relationship between `SalesLT.SalesOrderHeader`
and `SalesLT.Customer` tables in my current database.
```

#### Explain table relationships and keys involved

```copilot-prompt
I'm a developer new to T-SQL. Explain how `SalesLT.SalesOrderHeader` is
related to `SalesLT.Customer`, and what keys are involved.
```

#### Explain vector data types and usage options

```copilot-prompt
Explain how vector data types work in SQL Server and when to use them
for artificial intelligence (AI) scenarios.
```

### Generate migration or integration code

Request help generating SQL or object-relational mapping (ORM) migration scripts.

#### Add a foreign key constraint to a table

```copilot-prompt
Create a T-SQL script to add a foreign key constraint on
`SalesLT.SalesOrderDetail.ProductID` referencing `SalesLT.Product.ProductID`.
```

#### Generate a migration script to add a foreign key

```copilot-prompt
Generate a Sequelize migration to add a foreign key from
`SalesLT.SalesOrderDetail.ProductID` to `SalesLT.Product.ProductID`,
assuming both columns exist.
```

For object-relational mapping (ORM) examples across Entity Framework, Prisma, SQLAlchemy, and other frameworks, see [Object-relational mapping integrations with GitHub Copilot](orm-integrations.md).

## Ask mode vs. agent mode: which to use

| Scenario | Best mode |
| --- | --- |
| "What does this stored procedure do?" | Ask mode |
| "Write a query to find orders over $500" | Ask mode |
| "Connect to my LocalDev profile and list databases" | [Agent mode](agent-mode.md) |
| "Add audit columns to every table in the Sales schema" | [Agent mode](agent-mode.md) |
| "Design a full data model from my PRD" | [Plan mode](plan-mode.md) |

## Share your experience

[!INCLUDE [copilot-feedback](../includes/copilot-feedback.md)]

## Related content

- [How GitHub Copilot works with the MSSQL extension](how-it-works.md)
- [Quickstart: Use GitHub Copilot agent mode](agent-mode.md)
- [Quickstart: Use GitHub Copilot slash commands](slash-commands.md)
- [Quickstart: Use plan mode for spec-driven database design](plan-mode.md)
- [Quickstart: Use inline GitHub Copilot completions in SQL files](inline-completions.md)
- [Quickstart: Use custom instructions to align GitHub Copilot with your T-SQL conventions](custom-instructions.md)
- [Object-relational mapping integrations with GitHub Copilot](orm-integrations.md)
- [Limitations and known issues](limitations-and-known-issues.md)
