---
title: "Quickstart: Plan Mode for Database Design with GitHub Copilot"
titleSuffix: MSSQL Extension for Visual Studio Code
description: Learn how to use Visual Studio Code's Plan mode with GitHub Copilot and the MSSQL extension to reason through a data model before writing SQL data definition language.
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

# Quickstart: Use plan mode for spec-driven database design

Plan mode is a Visual Studio Code feature that lets GitHub Copilot reason through a request without making any changes. When you design a database, plan mode translates a natural-language product requirements document (PRD) into a reasoned data model (tables, relationships, junction tables, foreign key direction, and constraints) before you write a single `CREATE TABLE` statement. You then hand the plan to [agent mode](agent-mode.md) or [Schema Designer](schema-designer-scenarios.md) to build the actual schema.

> [!TIP]  
> Use plan mode when you need to think before you write SQL data definition language (DDL), especially for greenfield schemas or large refactors. For single-table changes, use [ask mode](chat-ask-mode.md) instead.

## Key takeaways

- Plan mode produces a written plan (often saved as `plan.md`) that you can review before execution.
- Plan mode is schema-aware when you use it alongside the `@mssql` chat participant or attach files with `#file:`.
- Pair plan mode with [custom instructions](custom-instructions.md) so the plan inherits your Transact-SQL (T-SQL) conventions automatically.
- Plan mode is a Visual Studio Code feature, not an MSSQL-specific one, but it fits database design well.

## Prerequisites

- Visual Studio Code with the [MSSQL extension](../mssql/mssql-extension-visual-studio-code.md) installed.
- An active GitHub Copilot subscription with plan mode available in the chat mode dropdown list.
- A workspace folder where you can author a `requirements.md` file.
- Optional: a target database connection for agent mode handoff.
- Optional: a [custom instructions](custom-instructions.md) file for your T-SQL conventions.

## What is plan mode?

Plan mode is one of the chat modes in Visual Studio Code, alongside ask mode, edit mode, and agent mode. In plan mode, GitHub Copilot:

- Reads the request and any attached context.
- Produces a written plan that breaks the work into reasoned steps.
- **Doesn't modify files or run tools.** The plan is a thinking artifact, not an execution.

You can then switch to agent mode (or Schema Designer) to execute the plan. For general documentation, see [Chat modes in Visual Studio Code](https://code.visualstudio.com/docs/copilot/customization/custom-agents).

## Why plan mode fits database design

Data model decisions compound. Junction tables, foreign key direction, audit columns, and normalization choices are easier to revise in a Markdown plan than in published DDL. Plan mode lets you:

- **Validate the design before building.** Review the proposed schema, push back on questionable choices, and iterate on the plan.
- **Expose hidden relationships.** Plan mode surfaces junction tables and many-to-many relationships you didn't explicitly ask for.
- **Separate specification from execution.** The PRD stays product-facing; the plan becomes the technical artifact; the DDL becomes the deliverable.

## End-to-end scenario: TaskManager schema

This quickstart walks through a full flow: PRD in natural language → plan in Markdown → tables built in Schema Designer.

### Step 1: Author a product requirements document

Create `requirements.md` at your workspace root. Describe the application in natural language, including domain entities, relationships, and business rules. Keep the document product-focused. Don't specify column types or constraints (that's what your [custom instructions](custom-instructions.md) file is for).

```markdown
# TaskManager - Product Specification

## Overview
A task management application for small development teams. Supports user
registration, role-based project membership, task tracking with multi-assignee
support, and team collaboration.

## User management
- Users register with email, username, first and last name, hashed password.
- Email and username must be unique across the system.
- Users can be deactivated (soft delete) rather than removed.
- A user can belong to multiple projects, each with a role: owner, admin, or member.

## Project management
- Projects have a name, description, and active/inactive status.
- Each project tracks who created it.
- Project membership is role-based - one entry per user per project.

## Task tracking
- Tasks belong to exactly one project.
- Required: title. Optional: description, due date.
- Status must be one of: todo, in-progress, done.
- Priority must be one of: low, medium, high, critical.
- Tasks can be assigned to multiple team members.

## Collaboration
- Team members can comment on tasks.
- Comments are text-based, tied to both a task and the author.

## Data integrity
- Every record must track when it was created and last updated.
- All relationships must enforce referential integrity.
- Deletes should be restricted (no orphan records).
- Status and priority fields must only accept valid values.
```

### Step 2: Switch GitHub Copilot Chat to plan mode

1. Open the **GitHub Copilot Chat** view.
1. In the chat mode dropdown list, select **Plan**.

### Step 3: Send the PRD to plan mode

Attach `requirements.md` as context, either by dragging the file into the chat input, using `#file:requirements.md`, or selecting the attach button.

```copilot-prompt
Based on the requirements document, think through the full data model:
identify all tables, relationships, junction tables, and constraints.
Save the plan so I can use it in the next step.
```

GitHub Copilot produces a reasoned plan and saves it as `plan.md` in your workspace. The plan typically includes:

- A list of tables with their purpose
- Relationships (one-to-many, many-to-many) and the junction tables that enable them
- Constraints (unique, check, foreign key) that enforce the business rules from the PRD
- Open questions or assumptions the plan relies on

Review the plan before proceeding. If a relationship is missing or a constraint is wrong, iterate by sending follow-up prompts in plan mode. Plan mode is stateless and cheap, so multiple rounds of refinement are normal.

### Step 4: Hand the plan to agent mode or Schema Designer

Once the plan reads correctly, switch to agent mode and build the schema.

1. In the chat mode dropdown list, select **Agent**.
1. Send a prompt that references the plan file and the target surface:

```copilot-prompt
Based on #file:plan.md, use Schema Designer to create all the tables
and relationships in my database. Focus on the database schema only -
skip any application layer items from the plan.
```

Agent mode opens Schema Designer and creates each table, asking for your approval at each step. Your [custom instructions](custom-instructions.md) apply automatically: column naming, audit columns, schema qualification, and constraint naming all follow your conventions.

## Tips

- **Pair with custom instructions.** The plan inherits your conventions automatically when you have a [custom instructions](custom-instructions.md) file for `**/*.sql`.
- **Keep the PRD product-focused.** Avoid specifying column types or constraints in `requirements.md`. Let the plan fill those in based on your conventions.
- **Iterate on the plan, not the DDL.** It's cheaper to revise a Markdown plan than to drop and recreate tables.
- **Save the plan in source control.** The plan is valuable documentation for future contributors and for revisiting design decisions.
- **Use Mermaid diagrams in the PRD.** Entity relationship diagrams in Mermaid help plan mode extract relationships accurately.

## Share your experience

[!INCLUDE [copilot-feedback](../includes/copilot-feedback.md)]

## Related content

- [How GitHub Copilot works with the MSSQL extension](how-it-works.md)
- [Quickstart: Use custom instructions to align GitHub Copilot with your T-SQL conventions](custom-instructions.md)
- [Quickstart: Use GitHub Copilot agent mode](agent-mode.md)
- [Quickstart: Design schemas visually with embedded GitHub Copilot scenarios](schema-designer-scenarios.md)
- [Chat modes in Visual Studio Code](https://code.visualstudio.com/docs/copilot/customization/custom-agents)
- [Library app sample with plan mode examples](https://github.com/croblesm/library-app)
