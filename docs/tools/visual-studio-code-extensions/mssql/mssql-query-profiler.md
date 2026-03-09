---
title: "Query Profiler in MSSQL Extension for Visual Studio Code (Preview)"
description: Learn how to use Query Profiler in the MSSQL extension for Visual Studio Code to capture and monitor real-time database activity using Extended Events, directly inside your editor.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: roblescarlos
ms.date: 02/21/2026
ms.service: sql
ms.subservice: vs-code-sql-extensions
ms.topic: overview
ms.collection:
  - data-tools
ai-usage: ai-assisted
---

# Query Profiler (Preview)

The Query Profiler in the MSSQL extension for Visual Studio Code lets you capture and monitor database activity in real time, directly inside your editor. Powered by Extended Events, Query Profiler delivers low-overhead performance monitoring that helps you quickly observe live T-SQL activity, identify slow operations, and spot performance bottlenecks while you build.

Query Profiler supports SQL Server (on-premises or in private and public cloud environments) and Azure SQL Database. You can monitor active sessions, review execution details, and understand how your application interacts with your database during development and testing, without switching between monitoring tools and your editor.

:::image type="content" source="media/mssql-query-profiler/query-profiler-grid.png" alt-text="Screenshot of the Query Profiler live events grid showing captured database activity in the MSSQL extension for Visual Studio Code." lightbox="media/mssql-query-profiler/query-profiler-grid.png":::

> [!TIP]  
> Query Profiler is currently in preview and might change based on feedback. Join our community at [GitHub Discussions](https://aka.ms/vscode-mssql-discussions) to share ideas or report issues.

## Features

Query Profiler in the MSSQL extension provides the following capabilities:

- Capture real-time query and database activity in a live events grid.
- Configure new profiling sessions with template selection and session naming.
- Control the session lifecycle with **Start**, **Pause**, **Resume**, **Stop**, and **Clear Data** actions.
- Run multiple concurrent profiling sessions and switch between them with the session selector.
- Filter captured events by text, database name, duration, and other fields.
- Toggle auto-scroll to follow the latest events or inspect older entries.
- Export captured events to CSV.
- Open and view existing `.xel` files in a read-only Profiler grid.
- Automatically select the appropriate template for Azure SQL Database targets (`Standard_Azure`).
- Works on Windows, macOS, and Linux across all supported Visual Studio Code platforms.

## Prerequisites

- Install the latest version of the [MSSQL extension for Visual Studio Code](https://aka.ms/vscode-mssql).
- An active connection to a supported SQL Server instance or Azure SQL Database.
- Appropriate server permissions. You need the `ALTER ANY EVENT SESSION` permission (or equivalent) to start a profiling session.

## Launch Query Profiler

You can launch Query Profiler from the **Connections** view or the **Command Palette**.

**From the Connections view**:

1. In the **Connections** view, right-click a SQL Server instance.

1. Select **Launch Query Profiler (Preview)**.

   :::image type="content" source="media/mssql-query-profiler/query-profiler-context-menu.png" alt-text="Screenshot of the Launch Query Profiler option in the Connections context menu.":::

**From the Command Palette**:

1. Open the Command Palette (**Ctrl+Shift+P** or **Cmd+Shift+P**).

1. Select **MSSQL: Launch Query Profiler**.

1. If no connection is currently active, select a connection when prompted.

The Query Profiler panel opens, tied to the selected connection.

## Create a profiling session

When the Query Profiler panel opens with no active session, it shows the **New Session** screen.

1. Verify or change the **Connection** for the profiling session.

1. Select a **Template** from the dropdown list. Available templates depend on your connection target:

   - **Standard_OnPrem**: Standard profiling template for on-premises SQL Server.
   - **TSQL_OnPrem**: T-SQL profiling template for on-premises SQL Server.
   - **TSQL_Locks**: T-SQL profiling template with lock events for on-premises SQL Server.
   - **TSQL_Duration**: T-SQL profiling template filtering by duration for on-premises SQL Server.
   - **Standard_Azure**: Standard profiling template for Azure SQL Database.

   :::image type="content" source="media/mssql-query-profiler/query-profiler-templates.png" alt-text="Screenshot of the template selection dropdown showing available profiler templates." lightbox="media/mssql-query-profiler/query-profiler-templates.png":::

   > [!NOTE]  
   > When you select Azure SQL Database as the target, the extension automatically selects the `Standard_Azure` template. If only one valid template is available, the extension selects it automatically, or shows it as read-only text.

1. Enter a **Session name**. Press **Enter** to confirm or **Escape** to cancel.

   :::image type="content" source="media/mssql-query-profiler/query-profiler-session-name.png" alt-text="Screenshot of the session name input prompt for a new Query Profiler session." lightbox="media/mssql-query-profiler/query-profiler-session-name.png":::

The panel transitions to the live events view, and a Profiler session is created through the SQL Tools Service.

## Monitor live events

When you run a profiling session, the events grid streams captured events in chronological order. The grid displays the following columns based on the selected template:

- **EventClass**: Type of event captured, such as `sql_batch_starting` or `rpc_completed`.
- **TextData**: The T-SQL statement or event text.
- **ApplicationName**: The application that generated the event.
- **LoginName**: The login associated with the event.
- **ClientProcessID**: Client process identifier.
- **SPID**: Server process ID.
- **StartTime**: Timestamp of the event.
- **CPU**: CPU time consumed.
- **Reads**: Number of logical reads.
- **Writes**: Number of logical writes.
- **Duration**: Execution time of the event.
- **DatabaseName**: The database where the event occurred.
- **HostName**: The host machine that generated the event.

### Toolbar actions

The toolbar provides the following controls for the active session:

| Action | Description |
| --- | --- |
| **New Session** | Opens the new session flow to create another profiling session. |
| **Start** | Resumes event streaming for a paused or stopped session. |
| **Stop** | Stops the profiling session. No new events arrive, but captured events remain visible for review, filtering, and export. |
| **Pause** | Temporarily stops updating the grid. The UI shows a **Paused** indicator. |
| **Clear Data** | Clears all rows from the current session's grid. If the session is still running, new events continue to populate the grid. |
| **Clear All Filters** | Removes all active column filters and restores the full event view. |
| **Quick filter all columns...** | Searches across all columns to quickly find matching events. |
| **Export to CSV** | Exports the current session's captured events to a CSV file. |
| **Auto-scroll** | Toggles whether the grid scrolls to the latest row when new events arrive. Auto-scroll is on by default for new sessions. |

## Filter events

Use column filters to narrow the events grid and focus on relevant activity. Each column header includes a filter icon that opens a filter specific to that column's data type.

### Filter by column values (for example, `DatabaseName` or `ApplicationName`)

1. Select the filter icon on a column header such as `DatabaseName`.

1. In the filter dropdown list, use the search box to locate values, or select and deselect specific entries from the checkbox list.

1. Select **Apply** to filter the grid. Select **Clear** to remove the filter for that column.

   :::image type="content" source="media/mssql-query-profiler/query-profiler-filters.png" alt-text="Screenshot of the DatabaseName column filter showing a checkbox list of available databases." lightbox="media/mssql-query-profiler/query-profiler-filters.png":::

### Filter by numeric values (for example, `Duration`)

1. Select the filter icon on a numeric column such as `Duration`.

1. Choose a comparison operator (for example, `=`, `>`, `<`) and enter a numeric threshold.

1. Select **Apply** to filter the grid.

   :::image type="content" source="media/mssql-query-profiler/query-profiler-filter-duration.png" alt-text="Screenshot of the Duration column filter with a numeric comparison operator and value input." lightbox="media/mssql-query-profiler/query-profiler-filter-duration.png":::

When one or more filters are active, filter chips appear below the toolbar showing which columns are filtered. Select the **X** on a chip to remove that filter, or select **Clear All Filters** in the toolbar to reset all filters at once.

:::image type="content" source="media/mssql-query-profiler/query-profiler-filters-applied.png" alt-text="Screenshot of active filter chips showing DatabaseName and ApplicationName filters applied." lightbox="media/mssql-query-profiler/query-profiler-filters-applied.png":::

> [!NOTE]  
> Filters apply client-side to both the currently displayed events and new events streamed during the session.

## Manage multiple sessions

Query Profiler supports running multiple profiling sessions concurrently, each with its own connection, template, and event buffer.

1. Select **New Session** from the toolbar to create another session. The new session can target the same or a different connection.

1. Use the **Session selector** dropdown list to switch between sessions. Each session displays its state (Running, Paused, or Stopped) with a visual indicator.

1. When you switch sessions:

   - The events grid updates to show the selected session's captured events.
   - The toolbar actions operate on the selected session.

## Export captured events

You can export the captured events from a profiling session to a CSV file for offline review or sharing.

1. Select **Export to CSV** from the toolbar. Export is available whether the session is running, paused, or stopped.

1. Choose a location and filename for the CSV file.

The exported file contains the events currently visible in the session's grid.

## Open an existing XEL file

You can open and review previously captured Extended Events trace files (`.xel`) in the Query Profiler grid.

1. Open the **Command Palette** (**Ctrl+Shift+P** or **Cmd+Shift+P**).

1. Select **MSSQL: Open Profiler XEL file...**.

1. Browse to and select the `.xel` file you want to open.

The file opens in a read-only Profiler panel. Events are loaded and displayed in the same grid with filtering capabilities. Start, Pause, and Stop controls aren't available because the file isn't a live session.

## Supported scenarios

Query Profiler works with the following SQL Server and Azure SQL targets:

| Target | Supported templates |
| --- | --- |
| SQL Server (on-premises, VMs, containers) | Standard_OnPrem, TSQL_OnPrem, TSQL_Locks, TSQL_Duration |
| Azure SQL Database | Standard_Azure |

> [!NOTE]  
> If you select an unsupported template or attempt a server-level session on Azure SQL Database, Query Profiler shows a targeted error message and suggests the appropriate template.

## Limitations

- No AI or GitHub Copilot integration in this preview.
- No cross-database or cross-server correlation beyond what Extended Events and the current templates provide.
- No saving or loading of filter presets.
- No replay of traces or script generation from captured events.
- No offline analysis without an existing `.xel` file or a live connection.
- Azure SQL Managed Instance and SQL database in Fabric endpoints aren't guaranteed to be supported in this preview.

## Feedback and support

[!INCLUDE [feedback](../includes/feedback.md)]

## Related content

- [Quickstart: Connect to and query a database with the MSSQL extension for Visual Studio Code](connect-database-visual-studio-code.md)
- [GitHub Copilot for MSSQL extension for Visual Studio Code](../github-copilot/overview.md)
- [Schema Designer](mssql-schema-designer.md)
- [Schema Compare](mssql-schema-compare.md)
- [Visual Studio Code documentation](https://code.visualstudio.com/docs)
- [MSSQL extension for Visual Studio Code repository on GitHub](https://github.com/Microsoft/vscode-mssql)
