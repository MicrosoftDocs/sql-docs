---
title: Use Transact-SQL Editor to Edit and Execute Scripts
description: Become familiar with the Transact-SQL Editor. Learn how to open the editor, see what information its panes display, and view resources on its features.
author: dzsquared
ms.author: drskwier
ms.reviewer: randolphwest
ms.date: 09/09/2025
ms.service: sql
ms.subservice: ssdt
ms.topic: concept-article
f1_keywords:
  - "SQL.DATA.TOOLS.SQLEDITOR"
---

# Use Transact-SQL Editor to edit and execute scripts

The Transact-SQL Editor provides you with a rich editing and debugging experience when you're working with scripts. It's invoked when you use the **View Code** contextual menu to open a database entity in a connected database or a project. It's also automatically opened when you use the **New Query** contextual menu from the SQL Server Object Explorer, or add a new script object to a database project.

If you aren't connected to a database, but want to execute a query against it, you can also use the **New Query Connection** dialog box in the **SQL** > **Transact-SQL Editor** menu option to connect to a database and launch the Transact-SQL Editor.

The Transact-SQL Editor contains a main **T-SQL** pane where you can write and edit Transact-SQL scripts. The editor supports IntelliSense and color coding of syntax to improve the readability of complex statements. It also supports find and replace, bulk commenting, custom fonts and colors, and line numbering. You can also change the database which the script in the editor is executed against. For more information, see [How to: Clone an Existing Database](../t-sql/statements/create-table-as-clone-of-transact-sql.md). The **Results** pane displays query results in a grid or text. You can also redirect these query results to a file. The **Message** pane displays errors, warnings, and informational messages returned when a script is run. When client statistics is enabled, the **Statistics** pane displays information about the query execution grouped into categories. The **Execution Plan** pane displays the data retrieval methods chosen by SQL Server, and shows the execution cost of specific statements and queries.

## In this section

| Article | Description |
| --- | --- |
| [How to: Outline and Add Snippets to Transact-SQL Script](how-to-outline-and-add-snippets-to-transact-sql-script.md) | Use the Snippet Picker to insert ready-made Transact-SQL code to your query. |
| [How to: Navigate Between Scripts](how-to-navigate-between-scripts.md) | Use Go-To-Definition and Find All Reference to navigate between scripts. |
| [How to: Use rename and refactoring to make changes to your database objects](how-to-use-rename-and-refactoring-to-make-changes-to-your-database-objects.md) | Rename an object across all scripts and preview any changes. |
| [How to: Execute a Partial Query](how-to-execute-a-partial-query.md) | Highlight a specific segment of the script and execute it as a single query. |
| [How to: Debug Stored Procedures](debugger/debug-stored-procedures.md) | Create and debug a Transact-SQL stored procedure by stepping into it. |
| [Analyze script performance](analyze-script-performance.md) | Use execution plans, client statistics, and code analysis to determine whether you can improve performance of your query, stored procedures, or scripts. |

## Related content

- [How to: Create new database objects using queries](how-to-create-new-database-objects-using-queries.md)
