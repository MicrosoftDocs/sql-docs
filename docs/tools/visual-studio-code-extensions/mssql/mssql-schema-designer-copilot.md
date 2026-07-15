---
title: GitHub Copilot Integration in Schema Designer
titleSuffix: MSSQL Extension for Visual Studio Code
description: Learn how to use GitHub Copilot within the Schema Designer in the MSSQL extension for Visual Studio Code, to design and evolve database schemas using natural language.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: yoleichen, roblescarlos
ms.date: 06/01/2026
ms.service: sql
ms.subservice: vs-code-sql-extensions
ms.topic: overview
ms.collection:
  - data-tools
  - ce-skilling-ai-copilot
ai-usage: ai-assisted
---

# GitHub Copilot integration in Schema Designer

The Schema Designer in the MSSQL extension for Visual Studio Code includes GitHub Copilot integration. You can design, modify, and validate database schemas using natural language. Describe what you need in the chat pane, and GitHub Copilot translates your requests into schema actions reflected in the visual diagram canvas, generated Transact-SQL (T-SQL) or ORM code, and change highlights.

:::image type="content" source="media/mssql-schema-designer-copilot/schema-designer-copilot-chat.png" alt-text="Screenshot of the Schema Designer with the GitHub Copilot chat panel open in Visual Studio Code." lightbox="media/mssql-schema-designer-copilot/schema-designer-copilot-chat.png":::

## Features

GitHub Copilot integration in Schema Designer offers these capabilities:

- Create database schemas from natural language descriptions, with tables, columns, and relationships generated automatically.
- Evolve existing schemas by adding, modifying, or removing tables and columns through conversational prompts.
- Generate migration-ready object-relational mapping (ORM) scripts from your visual schema changes, with support for Prisma, Sequelize, TypeORM, Drizzle, SQLAlchemy, and Entity Framework Core.
- Review AI-proposed changes individually through a guided change review flow, with the ability to accept or undo each edit.
- View a schema diff that shows all pending changes before they're applied to the database.
- Bootstrap application schemas on an empty database using a single natural language prompt.
- Import external artifacts such as JSON data, documents, or images and generate schema elements from them.
- Validate schema changes with guardrails for missing primary keys, invalid data types, and normalization issues.

## Prerequisites

Before you use GitHub Copilot in the Schema Designer, ensure the following requirements are met:

- The MSSQL extension for Visual Studio Code is installed. For installation steps, see the [MSSQL extension for Visual Studio Code](mssql-extension-visual-studio-code.md) overview.
- GitHub Copilot and GitHub Copilot Chat extensions are installed and signed in. For setup instructions, see [Set up GitHub Copilot](../github-copilot/overview.md#set-up-github-copilot-in-visual-studio-code).
- An active database connection is established through the MSSQL extension. For connection steps, see [Connect to a database with the MSSQL extension for Visual Studio Code](mssql-database-connections.md).

## Open Schema Designer with GitHub Copilot

You can open the Schema Designer with GitHub Copilot from two entry points:

1. In the MSSQL extension's **Object Explorer**, right-click on a database node.

1. Select **Schema Designer** from the context menu.

   :::image type="content" source="media/mssql-schema-designer-copilot/schema-designer-copilot-context-menu.png" alt-text="Screenshot of the right-click context menu on a database node showing the Schema Designer option." lightbox="media/mssql-schema-designer-copilot/schema-designer-copilot-context-menu.png":::

1. When the Schema Designer canvas opens with your database schema loaded, select the **Chat** button (with the GitHub Copilot icon) in the Schema Designer toolbar to open a GitHub Copilot chat session scoped to the current schema context.

> [!TIP]  
> You can also right-click a database node and select **Open in Copilot Agent mode** to start a GitHub Copilot agent chat session. In agent mode, you can ask GitHub Copilot to open the Schema Designer for you, for example: `"Open schema designer for AdventureWorksLT2022"`. For more information, see [Quickstart: Use GitHub Copilot agent mode](../github-copilot/agent-mode.md).

## Create a schema with natural language

When you open the GitHub Copilot chat panel in the Schema Designer, you can describe a schema in plain English and see it rendered in the visual diagram.

1. Type a natural language description of the schema you want to create in the chat pane.

1. GitHub Copilot generates the tables, columns, primary keys, and foreign key relationships based on your description.

1. Watch the visual diagram canvas as it updates in real time to reflect the generated schema.

Here are some prompt examples you can try:

- `"Build a library management system with tables for Books, Authors, Members, and BookLoans"`
- `"Design an e-commerce schema with Products, Categories, Orders, OrderItems, and Customers"`
- `"Create a blog platform with Users, Posts, Comments, and Tags with many-to-many relationships"`

:::image type="content" source="media/mssql-schema-designer-copilot/schema-designer-copilot-make-changes.png" alt-text="Screenshot of GitHub Copilot generating schema changes in the Schema Designer with the visual diagram updating." lightbox="media/mssql-schema-designer-copilot/schema-designer-copilot-make-changes.png":::

## Evolve an existing schema

Starting from an existing schema, use GitHub Copilot to evolve it by adding, modifying, or removing tables and columns. The diagram, code, and diff views all reflect these changes live.

Here are some prompt examples for schema evolution:

- `"Add a description column of type NVARCHAR(500) to the Books table"`
- `"Remove the ratings table from the schema"`
- `"Rename the 'NumberOfPages' column in the Books table to 'PageCount'"`
- `"Add booking/reservation support to this app schema"`
- `"Change the data type of the Price column in Products from INT to DECIMAL(10,2)"`

The visual diagram updates when you apply a change. Column additions appear in the correct table card. Removed elements disappear from the diagram. The process preserves all relationships.

## Review schema changes

After making schema edits through GitHub Copilot or the UI, review all pending changes before applying them to the database.

### Schema diff view

Select the **Show Changes** button in the Schema Designer toolbar to open the diff view. The diff view displays:

- Added objects, such as tables, columns, and foreign keys, clearly marked as **Added**.
- Removed objects marked as **Removed**.
- Modified objects showing before and after values for data type, nullability, identity, default values, and key flags.
- Schema-qualified names for all objects, such as `schema.table` and `schema.column`.
- An **Undo** option that appears when you hover over individual changes.

:::image type="content" source="media/mssql-schema-designer-copilot/schema-designer-copilot-diff-view.png" alt-text="Screenshot of the schema diff view showing added, removed, and modified schema objects." lightbox="media/mssql-schema-designer-copilot/schema-designer-copilot-diff-view.png":::

### GitHub Copilot change review

When GitHub Copilot applies multiple schema edits, it automatically starts the **Copilot Changes** review. This guided flow shows you each AI-proposed edit so you can review them one by one:

1. Go forward and backward between changes.
1. Select **Accept** to keep a change, or **Undo** to revert it.
1. Select the property badge on a change card to view detailed before and after values.

:::image type="content" source="media/mssql-schema-designer-copilot/schema-designer-copilot-change-review.png" alt-text="Screenshot of the GitHub Copilot change review flow with Accept and Undo buttons for each schema change." lightbox="media/mssql-schema-designer-copilot/schema-designer-copilot-change-review.png":::

Select the property badge on a change card to view detailed before and after values for each modified property:

:::image type="content" source="media/mssql-schema-designer-copilot/schema-designer-copilot-change-detail.png" alt-text="Screenshot of a change card detail showing before and after values for a modified schema property." lightbox="media/mssql-schema-designer-copilot/schema-designer-copilot-change-detail.png":::

## Bootstrap a schema from scratch

GitHub Copilot can generate complete application schemas. On an empty database, open the Schema Designer with the GitHub Copilot chat panel and describe an application concept. GitHub Copilot builds the data model, including tables, columns, primary keys, foreign keys, and relationships.

Here are some prompt examples for schema bootstrapping:

- `"Build a Twitter clone schema from scratch"`
- `"Create a blog platform schema with users, posts, comments, and tags"`
- `"Design an inventory management system with warehouses, products, stock levels, and suppliers"`
- `"Generate a restaurant reservation system with tables for restaurants, menus, customers, and reservations"`

After GitHub Copilot generates the initial schema, you can continue evolving it with follow-up prompts such as `"Add a comments feature to this blog platform"` or `"Add direct messaging to this Twitter clone"`.

## Import external artifacts

GitHub Copilot can accept external context to generate or inform schema design. Supported input includes JSON data, documents, images, or any format that GitHub Copilot supports.

Here are some prompt examples for importing external artifacts:

- `"Import this JSON and model it as tables"` followed by a JSON payload
- `"I have this API response format, create tables to store this data"` followed by a sample JSON response
- Attach a document or image and ask: `"Create a schema based on this document"`

Nested objects in JSON are modeled as separate related tables. Schema Designer infers data types from the input and creates foreign key relationships for nested structures.

:::image type="content" source="media/mssql-schema-designer-copilot/schema-designer-copilot-import-artifacts.png" alt-text="Screenshot of GitHub Copilot importing a JSON payload and generating tables in the Schema Designer diagram." lightbox="media/mssql-schema-designer-copilot/schema-designer-copilot-import-artifacts.png":::

## Generate ORM migration scripts

After you design or evolve a schema with GitHub Copilot, you can export your changes as migration-ready object-relational mapping (ORM) code instead of plain Transact-SQL. The Schema Designer Copilot panel includes a dedicated ORM output view that turns your pending schema changes into framework-specific migration files you can drop into an existing application project.

The following ORMs are supported:

- **Prisma** (Node.js / TypeScript)
- **Sequelize** (Node.js)
- **TypeORM** (Node.js / TypeScript)
- **Drizzle** (TypeScript)
- **SQLAlchemy** (Python)
- **Entity Framework Core** (.NET)

To generate ORM code, pick your target ORM from the framework selector in the Schema Designer Copilot panel. GitHub Copilot regenerates the migration script to match the conventions of the selected framework, including model definitions, relationship mappings, and migration metadata. You can copy the generated script directly into your project's migrations folder.

:::image type="content" source="media/mssql-schema-designer-copilot/schema-designer-copilot-sequelize-example.png" alt-text="Screenshot of the Schema Designer Copilot panel showing a Sequelize migration script generated from visual schema changes, with the framework selector at the top of the panel." lightbox="media/mssql-schema-designer-copilot/schema-designer-copilot-sequelize-example.png":::

> [!TIP]  
> Use ORM script generation when you want your database schema changes tracked alongside your application code. The generated migration files follow each framework's standard format, so they integrate cleanly with existing CI/CD pipelines and code review workflows.

## Validation and guardrails

As schemas evolve, GitHub Copilot validates changes and raises potential problems inline. Validation checks include:

- Missing primary keys on tables.
- Invalid or unsupported data types for the target SQL Server platform.
- Normalization issues such as repeating groups stored in a single column.
- Duplicate column names within the same table.
- Foreign key references to nonexistent tables or columns.

GitHub Copilot explains detected problems inline and suggests corrective actions before proceeding with the requested changes.

## Limitations

- **Chat session state**: Chat sessions don't keep history when you switch database context. A new context resets the chat memory.
- **Active database connection required**: You need an active database connection through the MSSQL extension to load and modify schemas. When you use GitHub Copilot in agent mode, the agent can set up the connection for you.
- **Review AI-generated output**: GitHub Copilot might suggest incorrect or suboptimal schema recommendations. Always review generated SQL and schema changes before publishing to your database.

## Feedback and support

[!INCLUDE [feedback](../includes/feedback.md)]

## Related content

- [Schema Designer](mssql-schema-designer.md)
- [Quickstart: Use the schema explorer and designer](../github-copilot/schema-explorer-designer.md)
- [GitHub Copilot for MSSQL extension for Visual Studio Code](../github-copilot/overview.md)
- [Quickstart: Use GitHub Copilot agent mode](../github-copilot/agent-mode.md)
- [Schema Compare](mssql-schema-compare.md)
- [Data-tier Application (DACPAC and BACPAC) import and export](mssql-data-tier-application.md)
- [Quickstart: Run your first query with the MSSQL extension for Visual Studio Code](mssql-run-first-query.md)
- [Visual Studio Code documentation](https://code.visualstudio.com/docs)
- [MSSQL extension for Visual Studio Code repository on GitHub](https://github.com/Microsoft/vscode-mssql)
