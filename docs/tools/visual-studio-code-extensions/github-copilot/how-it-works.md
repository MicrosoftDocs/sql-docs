---
title: How GitHub Copilot Works with the MSSQL Extension
titleSuffix: MSSQL Extension for Visual Studio Code
description: Understand the architecture behind the GitHub Copilot integration with the MSSQL extension for Visual Studio Code, including chat, agent, inline completions, and embedded experiences.
author: cwebb-msft
ms.author: chweb
ms.reviewer: roblescarlos
ms.date: 06/01/2026
ms.service: sql
ms.subservice: vs-code-sql-extensions
ms.topic: concept-article
ms.collection:
  - data-tools
  - ce-skilling-ai-copilot
ai-usage: ai-assisted
---

# How GitHub Copilot works with the MSSQL extension

The GitHub Copilot integration with the MSSQL extension for Visual Studio Code exposes several *surfaces* where artificial intelligence (AI) assists your SQL development. Each surface has different capabilities, different access to your database schema, and different ideal use cases. This article explains which surface handles which scenario, so you can choose the right tool for each task.

## Key takeaways

- The `@mssql` chat participant, agent mode tools, Schema Designer, and Data API builder all have *schema awareness* when connected to a database.
- *Inline completions* (ghost text while typing in a `.sql` file) come from GitHub Copilot's model directly and don't see your connected database schema.
- For schema-aware suggestions while writing SQL, use the `@mssql` chat participant in chat instead of inline ghost text.

## GitHub Copilot surfaces at a glance

| Surface | Provided by | Schema-aware? | Best for |
| --- | --- | --- | --- |
| [Chat participant (`@mssql`)](chat-ask-mode.md) | MSSQL extension | Yes (connected database) | Questions, explanations, query authoring |
| [Agent mode tools](agent-mode.md) | MSSQL extension contributes tools | Yes (via tool calls) | Multi-step workflows, delegated changes |
| [Plan mode](plan-mode.md) | Visual Studio Code | Yes (via `@mssql` context) | Reasoning before writing SQL data definition language (DDL) |
| [Slash commands](slash-commands.md) | MSSQL extension | Yes | Structured prompts for common tasks |
| [Inline completions](inline-completions.md) | GitHub Copilot model directly | No | Generic ghost text while typing |
| [Schema Designer with GitHub Copilot](schema-designer-scenarios.md) | Embedded in MSSQL Schema Designer | Yes | Visual schema design with AI assistance |
| [Data API builder with GitHub Copilot](../mssql/mssql-data-api-builder.md) | Embedded in MSSQL Data API builder | Yes | Entity configuration for REST, GraphQL, and Model Context Protocol (MCP) endpoints |

## The chat participant: `@mssql`

The `@mssql` chat participant is contributed by the MSSQL extension. When you type `@mssql` in the GitHub Copilot Chat view, the extension receives your prompt along with metadata about your active database connection. The extension can:

- Read schema information (tables, columns, relationships, stored procedures)
- Include schema context in the prompt sent to the model
- Return responses that reference real objects in your database

The chat participant is the primary way to get *schema-aware* AI assistance. It powers ask mode and edit mode interactions when you address `@mssql` in chat.

For a scenario-driven walkthrough, see [Quickstart: Chat with the `@mssql` participant (ask mode)](chat-ask-mode.md).

## Agent mode tools

Agent mode lets GitHub Copilot autonomously plan and execute work. The MSSQL extension contributes a set of *tools* (for example, `connect`, `list_databases`, `run_query`) that agent mode can call on your behalf, always with your approval.

Unlike the chat participant (which needs an explicit `@mssql` mention), agent mode picks up the MSSQL extension's tools automatically when the extension is active. You write natural-language prompts like "Connect to my LocalDev profile and show me the tables in AdventureWorks," and agent mode chooses which tools to invoke.

Agent mode is *schema-aware* through its tools. Each tool call returns real data from your connected database.

For the full tool reference and example prompts, see [Quickstart: Use GitHub Copilot agent mode](agent-mode.md).

## Plan mode

Plan mode is a Visual Studio Code feature that lets GitHub Copilot reason through a request without making changes. When you switch chat to plan mode, GitHub Copilot produces a written plan (often saved as `plan.md`) that you can review before handing off to agent mode or Schema Designer for execution.

Plan mode is a strong fit for database design. A natural-language product requirements document becomes a reasoned data model that includes tables, junction tables, foreign key direction, and constraints before any SQL DDL is written.

For a walkthrough that pairs plan mode with a product requirements document (PRD), see [Quickstart: Use plan mode for spec-driven database design](plan-mode.md).

## Inline completions

Inline completions are the *ghost text* that appears as you type in the editor. In `.sql` files, these suggestions come from GitHub Copilot's model directly.

Inline completions **don't see your connected database schema**. Visual Studio Code's inline completion application programming interface (API) is effectively single-provider: when GitHub Copilot is enabled, third-party extensions (including the MSSQL extension) can't contribute schema-aware ghost text. For schema-aware SQL suggestions, use the [`@mssql` chat participant](chat-ask-mode.md) instead.

This is a platform behavior, not an MSSQL extension limitation. It applies to every third-party extension that wants to contribute inline SQL completions. For a deeper look at what inline completions do offer, see [Quickstart: Use inline GitHub Copilot completions in SQL files](inline-completions.md).

## Schema Designer with embedded GitHub Copilot

The MSSQL extension's Schema Designer has GitHub Copilot embedded directly in its canvas. This is a *separate pipeline* from chat and agent mode. The Schema Designer has its own prompt infrastructure and its own way of applying schema context. You can ask GitHub Copilot to create tables from selected code, add relationships, generate test data, or import external artifacts, and watch the visual diagram update live.

Schema Designer with GitHub Copilot is schema-aware because it operates directly on your connected database.

For scenario-driven examples, see [Quickstart: Design schemas visually with embedded GitHub Copilot scenarios](schema-designer-scenarios.md).

## Data API builder with embedded GitHub Copilot

The Data API builder canvas also embeds a GitHub Copilot chat surface. You can use natural language to configure entities, permissions, and output types (REST, GraphQL, MCP). Like Schema Designer, this surface is schema-aware and uses its own prompt pipeline.

For the full workflow, see [Data API builder](../mssql/mssql-data-api-builder.md).

## Custom instructions apply across surfaces

Regardless of which surface you use, GitHub Copilot applies any *custom instructions* you've authored for your project. Instructions files (`.github/instructions/*.instructions.md`) teach GitHub Copilot your team's conventions, and they're injected into every request whose `applyTo` glob matches.

This means your naming conventions, file templates, and data type preferences influence ask mode, agent mode, plan mode, and inline completions alike. For how to set this up, see [Quickstart: Use custom instructions to align GitHub Copilot with your T-SQL conventions](custom-instructions.md).

## Privacy and data handling

Every surface routes through GitHub Copilot's privacy-preserving proxy. Prompts and completions aren't stored and aren't used to train the models. For details, see [Limitations and known issues](limitations-and-known-issues.md#privacy-and-system-generated-log-collection) and the [GitHub Copilot Trust Center](https://copilot.github.trust.page/).

## Share your experience

[!INCLUDE [copilot-feedback](../includes/copilot-feedback.md)]

## Related content

- [GitHub Copilot for MSSQL extension for Visual Studio Code](overview.md)
- [Quickstart: Chat with the `@mssql` participant (ask mode)](chat-ask-mode.md)
- [Quickstart: Use GitHub Copilot agent mode](agent-mode.md)
- [Quickstart: Use plan mode for spec-driven database design](plan-mode.md)
- [Quickstart: Use inline GitHub Copilot completions in SQL files](inline-completions.md)
- [Quickstart: Use custom instructions to align GitHub Copilot with your T-SQL conventions](custom-instructions.md)
- [Limitations and known issues](limitations-and-known-issues.md)
- [Visual Studio Code Copilot documentation](https://code.visualstudio.com/docs/copilot/overview)
