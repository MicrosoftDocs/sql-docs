---
title: Query history in the mssql extension
description: Access your recent query history in the mssql extension for VS Code.
ms.topic: conceptual
ms.prod: sql
ms.technology: tools-other
author: dzsquared
ms.author: drskwier
ms.reviewer: maghan
ms.date: 5/24/2022
---

# Query History in the mssql extension

This article introduces the functionality of query history in the mssql extension for VS Code. Query History is displayed as a section in the SQL Server view, which is available in the side bar by default when the [mssql extension for VS Code](mssql-extensions-vscode.md) is installed.

::image type="content" alt-text="Screenshot of Query History viewlet with a blank area below the header." source="./media/mssql-query-history/query-history-tab.png":::


## View query history

Initially the query history view will be empty but once you execute a query it will be captured and displayed in the window - with a separate row displayed for every execution.

::image type="content" alt-text="Screenshot of Query History tab with 2 select * queries listed with green check marks on the left." source="./media/mssql-query-history/query-history-tab-with-queries.png":::

Each row consists of three parts:
- Status icon: The status icon will be a "✔️" if the query executed successfully or an "❌" if an error occurred.
- Query Text: The text of the query that was executed
- Connection Info: The server and database the query was executed against

## Query History row actions

Right clicking on a history row will bring up a menu with the following actions available:

- Open Query
- Run Query
- Delete

### Open query

The "open query" option will open a new query editor window populated with the query text from the query executed and using the connection of that query.

### Run query

The "run query" option will do the same thing as Open Query but will additionally run the statement immediately.

### Delete

The "delete" option will permanently delete the selected history row.

## Query History management

Query history capture can be managed with the ability to clear all the history or dynamically pause/start history capture.

### Data storage

Currently all information is stored in memory and not persisted upon application exit.  Query history from a session isn't available in a new VS Code window.

### Clear all history

The action to clear all query history is also available from the command palette (**MS SQL: Clear All History**) and as an action button on the view. This action will permanently delete **ALL** history rows.

### Pause/start query history capture

The ability to pause and start query history capture is available from the command palette (**MS SQL: Toggle Query History Capture**) and as an action button on the panel. While paused, no query execution data will be stored by query history.


## Next steps
- [Use the mssql extension to query a SQL instance](sql-server-develop-use-vscode.md)
- [Learn more about Visual Studio Code](https://code.visualstudio.com/docs)
- [Learn more about contributing to the mssql extension](https://github.com/Microsoft/vscode-mssql/wiki)