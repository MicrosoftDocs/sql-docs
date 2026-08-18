---
title: "Quickstart: Schema Designer with GitHub Copilot Scenarios"
titleSuffix: MSSQL Extension for Visual Studio Code
description: Learn how to use the GitHub Copilot embedded in Schema Designer to create tables from selected code, add relationships, generate test data, and import external artifacts in Visual Studio Code.
author: rwestMSFT
ms.author: randolphwest
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

# Quickstart: Design schemas visually with embedded GitHub Copilot scenarios

The MSSQL extension's Schema Designer has GitHub Copilot embedded directly in its canvas. You can describe what you need in natural language and watch tables, columns, and relationships materialize in the visual diagram while the Transact-SQL (T-SQL) script updates live. This quickstart walks through the scenarios that make the embedded experience uniquely useful: creating tables from selected code in other files, adding many-to-many relationships, generating themed test data, and importing external artifacts.

> [!TIP]  
> Use **Schema Designer with GitHub Copilot** when you want visual feedback alongside AI-driven schema design. Use [chat-based schema design with `@mssql`](schema-explorer-designer.md) when you prefer prompts-only or aren't ready to commit visual changes. For reference documentation of the feature, see [GitHub Copilot integration in Schema Designer](../mssql/mssql-schema-designer-copilot.md).

## Key takeaways

- Schema Designer's embedded GitHub Copilot is a separate pipeline from the `@mssql` chat participant. It operates directly on the visual canvas.
- GitHub Copilot in Schema Designer can pick up context from other editor selections (for example, a JSON structure in a TypeScript file).
- Every change is staged in the **Copilot Changes** panel for review before you publish to the database.
- Your [custom instructions](custom-instructions.md) apply here too.

## Prerequisites

- Visual Studio Code with the [MSSQL extension](../mssql/mssql-extension-visual-studio-code.md) installed (version 1.41 or later).
- An active GitHub Copilot subscription.
- A database connection in Object Explorer (local SQL Server, Azure SQL Database, or SQL database in Microsoft Fabric).

## Open Schema Designer

1. In Object Explorer, right-click the database.
1. Select **Open Schema Designer**.

The Schema Designer opens with a visual view of your existing tables. The GitHub Copilot chat panel appears alongside the canvas.

## Scenario 1: Create a table from selected code in another file

Schema Designer's embedded GitHub Copilot can read selections from other editor windows and use them as context for schema creation. This is useful when your frontend or application layer already has hardcoded data shapes that need to become real tables.

1. Open the source file in a separate editor tab. For example, a React component with a `MAGAZINES_DATA` array.
1. **Select** the data structure in the source file.
1. Switch to the Schema Designer chat panel and send:

```copilot-prompt
Using the selected JSON structure, create a new table called magazines.
```

GitHub Copilot:

- Picks up the cross-file selection as context.
- Infers appropriate T-SQL data types (`NVARCHAR`, `INT`, `DATETIME2`).
- Follows your [custom instructions](custom-instructions.md) for naming, constraints, and audit columns.
- Stages the new table in the **Copilot Changes** panel.

Review the proposed table in the canvas, then accept or undo the change.

## Scenario 2: Add a many-to-many relationship

If the new table needs to relate to existing tables, ask GitHub Copilot to create the junction table and foreign keys.

```copilot-prompt
Add a many-to-many relationship between the magazines table and
the existing authors table. Ensure the foreign key columns align
with the current database schema and reference the correct primary
key columns.
```

GitHub Copilot creates a `magazines_authors` junction table with the correct foreign keys pointing to both tables. The visual diagram updates to show the new relationship lines.

## Scenario 3: Generate themed test data

Once the schema is in place, GitHub Copilot can generate realistic seed data and execute it against your database.

```copilot-prompt
Seed the magazines and magazines_authors tables with test data.
- Tables already exist - do NOT create or alter them.
- Each magazine should be themed around science or technology.
- Include at least 5 magazines with creative titles, issues, and years.
- Look up existing authors in the database and link each magazine
  to one or more authors.
```

Before executing, review the generated `INSERT` statements. GitHub Copilot respects referential integrity by looking up existing primary keys rather than inventing IDs.

Verify the data landed correctly:

```copilot-prompt
Show me all the data in the magazines table and their linked authors.
```

## Scenario 4: Bootstrap a schema from scratch

On an empty database, you can build a full application schema from a single natural-language description.

```copilot-prompt
I'm building a task management app. Create a schema with users,
projects, tasks, and comments. Users can belong to multiple projects
with different roles. Tasks can have multiple assignees. Comments
belong to a task and an author.
```

GitHub Copilot creates all tables, relationships, junction tables, and constraints, then stages them for review. This flow pairs well with [plan mode](plan-mode.md), use plan mode to reason through the schema first, then hand the plan to Schema Designer for execution.

## Scenario 5: Import external artifacts

Schema Designer can generate schema elements from external inputs: JSON files, application code, documents, or images.

```copilot-prompt
Here's a JSON file describing our product catalog structure.
Create tables to represent this data, including appropriate
relationships between products, categories, and variants.
```

Attach the file via `#file:` or drag it into the chat. GitHub Copilot infers the schema, including the relationships that aren't explicit in the source data.

## Review changes before publishing

Every proposed change is staged in the **Copilot Changes** panel at the bottom of Schema Designer. You can:

- Review each added, modified, or removed object individually.
- See the exact T-SQL script that executes.
- **Accept** to apply the change to the canvas, or **Undo** to discard it.
- When ready, select **Publish Changes** to execute the DDL against your database.

## Validation and guardrails

GitHub Copilot in Schema Designer proactively flags issues before they reach your database:

- Tables without a primary key.
- Invalid data types for SQL Server.
- Normalization concerns (for example, repeating groups).
- Foreign keys that reference nonexistent columns.

Schema-qualified names (`schema.table`, `schema.column`) in the diff view make each proposed change unambiguous.

## When to use this vs. chat-based schema design

| If you want to... | Use |
| --- | --- |
| Design schemas visually with a drag-and-drop canvas + AI | **This article** (Schema Designer with GitHub Copilot) |
| Design schemas via prompts against a connected database, no canvas | [Chat-based schema explorer with `@mssql`](schema-explorer-designer.md) |
| Reason through a full data model before building | [Plan mode](plan-mode.md) |
| Manually design a schema with no AI | [Schema Designer](../mssql/mssql-schema-designer.md) |

## Share your experience

[!INCLUDE [copilot-feedback](../includes/copilot-feedback.md)]

## Related content

- [GitHub Copilot integration in Schema Designer](../mssql/mssql-schema-designer-copilot.md)
- [Schema Designer](../mssql/mssql-schema-designer.md)
- [Quickstart: Use the schema explorer and designer](schema-explorer-designer.md)
- [Quickstart: Use plan mode for spec-driven database design](plan-mode.md)
- [Quickstart: Use custom instructions to align GitHub Copilot with your T-SQL conventions](custom-instructions.md)
- [How GitHub Copilot works with the MSSQL extension](how-it-works.md)
