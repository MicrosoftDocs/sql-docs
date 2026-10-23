---
title: "Quickstart: Use GitHub Copilot Agent Mode"
titleSuffix: MSSQL Extension for Visual Studio Code
description: Learn how to use GitHub Copilot Agent Mode with the MSSQL extension to connect to databases, explore schemas, and run SQL queries directly from the GitHub Copilot chat in Visual Studio Code.
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
ms.custom:
  - ignite-2025
ai-usage: ai-assisted
---

# Quickstart: Use GitHub Copilot agent mode

Agent mode lets GitHub Copilot orchestrate the tools contributed by the MSSQL extension for Visual Studio Code. When the extension is installed and active, GitHub Copilot can list Microsoft SQL Server connections, connect to a server and database, retrieve schema metadata, and execute queries, all from natural-language prompts, with your approval on each action.

All actions use the same connection context and credentials as the MSSQL extension. Agent mode doesn't introduce another authentication or permission changes.

> [!TIP]  
> Use agent mode for multi-step workflows, exploration at scale, and delegated changes. Use [ask mode](chat-ask-mode.md) when you need a single answer or a one-shot query. Use [plan mode](plan-mode.md) when you need to reason about a design before writing Transact-SQL (T-SQL) data definition language (DDL).

## Key takeaways

- Agent mode picks up MSSQL extension tools automatically. No `@mssql` mention required.
- Every tool call requires your approval before execution.
- Agent mode is *schema-aware* through its tools: each tool call returns real data from your connected database.
- For architectural context across all surfaces, see [How GitHub Copilot works with the MSSQL extension](how-it-works.md).

## When to use agent mode

Agent mode is best for:

- **Multi-step workflows.** "Connect to LocalDev, switch to AdventureWorks, then show me every table with a foreign key to Customer."
- **Exploration at scale.** "Find any stored procedures that reference SalesOrderHeader and summarize what each one does."
- **Delegated changes.** "Add audit columns to every table in the Sales schema and regenerate the related stored procedures."

Use [ask mode](chat-ask-mode.md) when a single question or a one-shot query answers your need. Use [slash commands](slash-commands.md) when you already know which action you want. Use [plan mode](plan-mode.md) when you want a written plan before any changes.

For details about how agent mode selects and executes tools, see the [Visual Studio Code documentation on agent mode](https://code.visualstudio.com/docs/copilot/chat/copilot-chat#_builtin-chat-modes).

## What is agent mode?

Agent Mode lets GitHub Copilot perform SQL-related actions using the MSSQL extension, and user confirmation is required before execution.

You can invoke these actions by using chat variables such as `#mssql_connect`, or by issuing equivalent natural-language requests, for example:

```copilot-prompt
Connect to my Library database using my LocalDev profile
```

:::image type="content" source="media/agent-mode/agent-tool-chat.png" alt-text="Screenshot of GitHub Copilot Agent Mode chat interface." lightbox="media/agent-mode/agent-tool-chat.png":::

## MSSQL Agent Mode tool reference

This section provides a detailed reference for the SQL-specific tools available in GitHub Copilot Agent Mode. The MSSQL extension contributes these tools, enabling GitHub Copilot to execute actions through chat variables or natural language prompts. All tools require user confirmation before execution.

:::image type="content" source="media/agent-mode/agent-tools.png" alt-text="Screenshot of list of SQL-specific tools contributed by the MSSQL extension in Copilot Agent Mode.":::

> [!TIP]  
> You can also use chat variables like `#mssql_connect` to invoke these tools directly, or write prompts in natural language such as:
>
> ```copilot-prompt
> Connect to my development database
> ```

GitHub Copilot handles tool selection automatically.

### Connection management

| Tool name | Description |
| --- | --- |
| `connect` | Connects to a database by using a saved connection profile or a specified server and database. |
| `disconnect` | Ends the current active connection session. |
| `change_database` | Changes the database for an existing connection session. |
| `get_connection_details` | Gets connection details for a specific MSSQL connection. |
| `list_servers` | Lists all saved SQL Server connection profiles in your environment. |
| `list_databases` | Lists all available databases for a connected MSSQL server. |

#### Examples

Use the following phrases to interact with GitHub Copilot.

```copilot-prompt
- Connect to my LocalDev environment
- Disconnect from my current database
- List my available connection profiles
- List all databases in the localhost server
- Set the active connection to localhost
- Set AdventureWorks as the active database
- Get the connection string for AdventureWorks on localhost
```

:::image type="content" source="media/agent-mode/agent-tool-connect.png" alt-text="Screenshot of example using an agent tool to connect to a database in the GitHub Copilot chat." lightbox="media/agent-mode/agent-tool-connect.png":::

#### How connection logic works

GitHub Copilot Agent Mode supports flexible ways to connect to your SQL database, either by referencing saved profiles or by specifying a server and database directly. Here's how the connection logic works.

When you connect with a saved profile:

1. You connect by referencing the name of a saved connection profile.
1. GitHub Copilot uses the `mssql_list_servers` tool to verify the profile exists.
1. The `mssql_connect` tool then uses the saved `profileId` and its parameters to establish the connection.

When you connect by specifying a server and database:

- If a saved profile matches both the specified server and database:

  1. GitHub Copilot uses `mssql_list_servers` to find the match.
  1. It then calls `mssql_connect` using the full profile.

- If a saved profile matches only the server:

  1. GitHub Copilot finds the matching server profile.
  1. It attempts to connect by substituting the user-requested database into that profile.
  1. If the connection fails, an error is shown.

- If no profile matches the specified server:

  - GitHub Copilot reports an error.

This flexible matching system lets GitHub Copilot handle a range of connection scenarios. It minimizes user effort while ensuring secure, confirmable actions.

### Schema exploration

| Tool name | Description |
| --- | --- |
| `show_schema` | Displays a high-level diagram of your connected database schema, including tables and relationships. |
| `list_schemas` | Lists all schemas in a database for a connected MSSQL server. |
| `list_tables` | Lists all tables in a database for a connected MSSQL server. |
| `list_views` | Lists all views in a database for a connected MSSQL server. |
| `list_functions` | Lists all functions in a database for a connected MSSQL server. |

#### Examples

Use the following phrases to interact with GitHub Copilot.

```copilot-prompt
- Show me the schema for this database
- Show me all tables in the current database
- List all views from this MSSQL database
- Give me a list of all the functions available in this schema
- What schemas are available in this database?
```

:::image type="content" source="media/agent-mode/agent-tool-schema.gif" alt-text="Screenshot of animation showing the database schema visualizer tool in Copilot Agent Mode." lightbox="media/agent-mode/agent-tool-schema.gif":::

### Query execution

| Tool name | Description |
| --- | --- |
| `run_query` | Runs a SQL query against the connected database. |

#### Examples

Use the following phrases to interact with GitHub Copilot.

```copilot-prompt
- Give me the top five posts published this week
- Execute the current file to find how many comments each post has
- Get all categories along with the number of posts in each
```

:::image type="content" source="media/agent-mode/agent-tool-run-query-1.png" alt-text="Screenshot of example using an agent tool to connect to a database and retrieve data." lightbox="media/agent-mode/agent-tool-run-query-1.png":::

:::image type="content" source="media/agent-mode/agent-tool-run-query-2.png" alt-text="Screenshot of another example using an agent tool to connect to a database and retrieve data." lightbox="media/agent-mode/agent-tool-run-query-2.png":::

## How tools are managed in Agent Mode

GitHub Copilot can use MSSQL-specific tools and other extension-contributed tools while it processes your request. You can see these tools in the Agent Mode interface under the **Tools** menu, where you can also turn specific tools on or off.

When you invoke a tool, especially if it interacts with your machine or database, GitHub Copilot asks for confirmation to ensure secure execution. You can allow the tool for just the current session, the workspace, or permanently.

For more information about tool visibility and approvals, see [Manage tool approvals](https://code.visualstudio.com/docs/copilot/chat/copilot-chat#_builtin-chat-modes).

### Agent Mode confirmation workflow

When GitHub Copilot selects a tool, it prompts you with a confirmation dialog that shows details about the requested action. You must explicitly approve the request before it can execute any commands that interact with your machine or database:

- **Allow in this session**
- **Allow in this workspace**
- **Always allow**

This confirmation step helps ensure secure, intentional interactions with your development environment.

> [!NOTE]  
> For more information on how confirmation works across all tools in Agent Mode, see the [Visual Studio Code documentation on tool approvals](https://code.visualstudio.com/docs/copilot/chat/copilot-chat#_builtin-chat-modes).

## Agent mode prompt catalog

Use these natural-language prompts to invoke MSSQL extension tools through agent mode. For each category, an equivalent [slash command](slash-commands.md) or [ask mode prompt](chat-ask-mode.md) is cross-referenced.

### Connection management

```copilot-prompt
Connect to my LocalDev profile and set AdventureWorks as the active database.
```

```copilot-prompt
List all my saved connection profiles and tell me which one I'm currently connected to.
```

```copilot-prompt
Disconnect from my current database.
```

Equivalent slash commands: `/connect`, `/listServers`, `/changeDatabase`, `/disconnect`.

### Schema exploration

```copilot-prompt
Show me every table in the SalesLT schema, grouped by whether they're
referenced by a foreign key from another table.
```

```copilot-prompt
Find all stored procedures that reference SalesLT.SalesOrderHeader and
summarize what each one does in one sentence.
```

```copilot-prompt
Which tables in the current database have no primary key?
```

Equivalents ask prompts: see [Chat with the `@mssql` participant](chat-ask-mode.md#list-or-explore-objects-in-your-database-schema).

### Query execution

```copilot-prompt
Run a query to count the number of active customers in SalesLT.Customer,
then show me the top 10 by order total.
```

```copilot-prompt
Show me the execution plan for this query: SELECT ... FROM ...
```

```copilot-prompt
Execute the last query I ran against my Dev database instead.
```

### Multi-step workflows

```copilot-prompt
Connect to LocalDev, switch to AdventureWorks, list all tables with a
foreign key to SalesLT.Customer, and save the list to a file called
customer-dependents.md.
```

```copilot-prompt
Find every stored procedure that uses dynamic SQL and open each one
in a new editor tab so I can review them.
```

### Delegated schema changes

> [!NOTE]  
> Schema changes are good candidates to run through [plan mode](plan-mode.md) first. Plan the changes, review them, then hand the plan to agent mode for execution.

```copilot-prompt
Add createdAt and updatedAt audit columns to every table in the Sales
schema that doesn't already have them. Use DATETIME2(7) with a default
of GETUTCDATE().
```

```copilot-prompt
Regenerate every stored procedure that inserts into SalesLT.Customer
to include the new email column.
```

## Related content

- [How GitHub Copilot works with the MSSQL extension](how-it-works.md)
- [Quickstart: Chat with the `@mssql` participant (ask mode)](chat-ask-mode.md)
- [Quickstart: Use plan mode for spec-driven database design](plan-mode.md)
- [Quickstart: Use GitHub Copilot slash commands](slash-commands.md)
- [Quickstart: Use custom instructions to align GitHub Copilot with your T-SQL conventions](custom-instructions.md)
- [Quickstart: Generate code](code-generation.md)
- [Quickstart: Design schemas visually with embedded GitHub Copilot scenarios](schema-designer-scenarios.md)
- [Quickstart: Use the smart query builder](smart-query-builder.md)
- [Quickstart: Query optimizer assistant](query-optimizer-assistant.md)
- [Object-relational mapping integrations with GitHub Copilot](orm-integrations.md)
- [Limitations and known issues](limitations-and-known-issues.md)
