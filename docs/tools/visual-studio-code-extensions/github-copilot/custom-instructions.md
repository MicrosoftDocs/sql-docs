---
title: "Quickstart: Custom Instructions for GitHub Copilot and T-SQL"
titleSuffix: MSSQL Extension for Visual Studio Code
description: Learn how to create a custom instructions file so GitHub Copilot follows your team's T-SQL conventions across ask, edit, agent, and inline completions in Visual Studio Code.
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

# Quickstart: Use custom instructions to align GitHub Copilot with your T-SQL conventions

Custom instructions teach GitHub Copilot your team's standards so every response, whether from ask mode, agent mode, or inline completions, follows your naming, formatting, and data type conventions. A single Markdown file scoped to `.sql` files changes generic Transact-SQL (T-SQL) output into aligned output that matches your project.

> [!TIP]  
> Custom instructions apply across **every** GitHub Copilot surface that matches the `applyTo` glob, including ask mode, edit mode, agent mode, and inline completions. Set them up once per language or domain.

## Key takeaways

- Custom instructions live in `.github/instructions/<name>.instructions.md` inside your workspace.
- The `applyTo` front matter key scopes each file to a glob pattern, such as `**/*.sql`.
- GitHub Copilot injects matching instructions into every request automatically. No slash command required.
- Without custom instructions, GitHub Copilot defaults to generic conventions (PascalCase, `INT` primary keys, no file templates).

## Prerequisites

- Visual Studio Code with the [MSSQL extension](../mssql/mssql-extension-visual-studio-code.md) installed.
- An active GitHub Copilot subscription.
- A workspace folder. This quickstart creates new files in `.github/instructions/`.

## What are custom instructions?

Custom instructions are Markdown files that GitHub Copilot reads and applies to every request that matches a glob pattern. Visual Studio Code supports them natively. The MSSQL extension doesn't require any extra configuration.

Every serious artificial intelligence (AI) coding tool now supports a version of this pattern: Cursor uses `.cursorrules`, OpenAI Codex uses `AGENTS.md`, Anthropic's Claude Code uses `CLAUDE.md`. GitHub Copilot in Visual Studio Code uses `.github/instructions/*.instructions.md`. The advantage is that you can have multiple instructions files scoped to different file types in the same repository.

For general Visual Studio Code documentation on this feature, see [Customize AI responses in Visual Studio Code](https://code.visualstudio.com/docs/copilot/customization/overview).

## Why custom instructions matter for T-SQL

Modern GitHub Copilot generates impressive T-SQL out of the box: `NVARCHAR` columns, `DATETIME2` timestamps, sensible constraint naming. But it defaults to conventions your team might not use.

Without custom instructions, GitHub Copilot typically:

- Uses **PascalCase** for table and column names (`UserID`, `CreatedAt`) instead of your team's `camelCase` or `snake_case`.
- Uses `INT` primary keys instead of `BIGINT`.
- **Omits** file header templates, `SET ANSI_NULLS ON`, and `SET QUOTED_IDENTIFIER ON`.
- **Skips schema qualification** (`CREATE TABLE users` instead of `CREATE TABLE dbo.users`).
- Generates **auto-named constraints** instead of following your `PK_tableName` / `FK_child_parent` pattern.

None of these defaults are wrong. They just aren't yours. Custom instructions fix all of this with a single file.

## Create a T-SQL conventions file

This example creates an instructions file that enforces camelCase naming, schema qualification, audit columns, and a file header template.

### Step 1: Use GitHub Copilot to create the file

1. In Visual Studio Code, open the **GitHub Copilot Chat** view.
1. Select the settings (gear) icon, then choose **Instructions & Rules** > **New instruction file**.
1. When prompted for a location, select `.github/instructions`.
1. When prompted for a filename, enter `tsql-conventions`.

Visual Studio Code creates `.github/instructions/tsql-conventions.instructions.md` with scaffolded front matter.

### Step 2: Add your conventions

Replace the scaffolded content with your team's T-SQL standards. The following template covers naming, data types, audit columns, schema qualification, constraint naming, and prohibited patterns.

```markdown
---
applyTo: "**/*.sql"
---

# T-SQL conventions

## Database environment
- Local development: [!INCLUDE [sssql25-md](../../../includes/sssql25-md.md)] running in a Docker container
- Cloud / production: Azure SQL Database
- All T-SQL must be compatible with both environments

## File template
Every .sql file MUST begin with this header block:

-- ================================================================
-- Author:      <your name>
-- Created:     <YYYY-MM-DD>
-- Purpose:     <brief description>
-- ================================================================
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

## T-SQL conventions
- Use camelCase for ALL identifiers: table names, column names, parameters
- Use NVARCHAR for all text columns - never VARCHAR
- Use BIGINT for all primary keys and foreign keys - never INT
- Every table MUST include these audit columns:
    createdAt  DATETIME2(7) NOT NULL DEFAULT GETUTCDATE()
    updatedAt  DATETIME2(7) NOT NULL DEFAULT GETUTCDATE()
- Always schema-qualify all objects: dbo.tableName
- Use clustered primary keys on all tables
- Foreign key column names follow the pattern: [referencedTable]Id
- Never use SELECT * - always name columns explicitly

## Constraint naming conventions
- Primary keys: PK_tableName
- Foreign keys: FK_childTable_parentTable
- Unique constraints: UQ_tableName_columnName
- Check constraints: CK_tableName_columnName
- Default constraints: DF_tableName_columnName

## What to avoid
- Do NOT generate stored procedures unless I explicitly ask for one
- Do NOT use deprecated T-SQL syntax (no *= for joins, no non-ANSI joins)
- Do NOT generate object-relational mapping (ORM) models or application code
```

### Step 3: Save the file

GitHub Copilot picks up instructions files as soon as you save. There's no reload or configuration step.

## See the before/after contrast

To see the impact of custom instructions, run the same prompt in GitHub Copilot Chat before and after adding the file.

### Before: no custom instructions

In a workspace with no `.github/instructions/` folder, ask GitHub Copilot:

```copilot-prompt
Create a users table and a projects table for a task management app.
```

Typical output:

- `CREATE TABLE Users` (no schema qualification, PascalCase)
- `UserID INT IDENTITY(1,1) PRIMARY KEY`
- No file header block, no `SET` statements
- Autonamed or inconsistent constraints

### After: with custom instructions

In the workspace where you created `tsql-conventions.instructions.md`, ask the same prompt:

```copilot-prompt
Create a users table and a projects table for a task management app.
```

Expected output:

- Full file header with author, date, purpose, `SET ANSI_NULLS ON`, `SET QUOTED_IDENTIFIER ON`
- `CREATE TABLE dbo.users` (schema-qualified, camelCase)
- `userId BIGINT IDENTITY(1,1) PRIMARY KEY`
- `createdAt` and `updatedAt` audit columns
- Named constraints: `PK_users`, `UQ_users_email`, `FK_projects_users`, `DF_users_createdAt`

Same model, same prompt, same GitHub Copilot. The instructions file did the work.

## Verify custom instructions are being applied

To confirm GitHub Copilot is reading your instructions file, inspect the debug output.

1. In Visual Studio Code, select **View** > **Output**.
1. In the output channel dropdown list, select **GitHub Copilot** or **GitHub Copilot Chat**.
1. Send a prompt and watch the output channel. The full request payload, including your custom instructions, appears in the log.

This debug view is the source of truth. If the output doesn't contain your instructions, check that:

- The file lives at `.github/instructions/<name>.instructions.md` (the `.instructions.md` suffix is required).
- The `applyTo` glob matches the file you're working on.
- The file is saved.

## Patterns and best practices

- **One file per language or domain.** Use separate files for T-SQL, TypeScript, Python, and so on, each with its own `applyTo` glob. Don't mix languages in one file.
- **Keep it declarative.** Bullet points and short rules work better than prose. GitHub Copilot follows instructions more reliably when they're scannable.
- **Version control the files.** Commit `.github/instructions/` to your repository so every contributor benefits automatically.
- **Pair with plan mode.** When you use [plan mode](plan-mode.md) for database design, conventions are inherited automatically. You don't need to restate them in the prompt.
- **Test after changes.** Use the debug view to confirm the instructions are applied whenever you update the file.

## Share your experience

[!INCLUDE [copilot-feedback](../includes/copilot-feedback.md)]

## Related content

- [How GitHub Copilot works with the MSSQL extension](how-it-works.md)
- [Quickstart: Use plan mode for spec-driven database design](plan-mode.md)
- [Quickstart: Chat with the `@mssql` participant (ask mode)](chat-ask-mode.md)
- [Quickstart: Use GitHub Copilot agent mode](agent-mode.md)
- [Customize AI responses in Visual Studio Code](https://code.visualstudio.com/docs/copilot/customization/overview)
- [Limitations and known issues](limitations-and-known-issues.md)
