---
title: SQL Server Profiler
titleSuffix: SQL Server Profiler
description: Explore the features of SQL Server Profiler. Get help troubleshooting problems by using this tool to create traces and analyze and replay trace results.
author: rwestMSFT
ms.author: randolphwest
ms.date: 02/21/2026
ms.service: sql
ms.subservice: profiler
ms.topic: concept-article
ms.collection:
  - data-tools
---

# SQL Server Profiler

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

[!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] is an interface to create and manage traces and analyze and replay trace results. Events are saved in a trace file that you can later analyze or use to replay a specific series of steps when diagnosing a problem.

When you try to connect to an Azure SQL Database from the SQL Server Profiler, it incorrectly throws a misleading error message as follows:

```output
To run a trace against SQL Server, you must be a sysadmin fixed server role member or have the ALTER TRACE permission.
```

The message should state that Azure SQL Database isn't supported by SQL Server Profiler.

## Deprecation notice

> [!IMPORTANT]  
> SQL Trace and SQL Server Profiler are deprecated. Use [Extended Events](../../relational-databases/extended-events/extended-events.md) instead. [!INCLUDE [ssnotedepfutureavoid-md](../../includes/ssnotedepfutureavoid-md.md)]

The `Microsoft.SqlServer.Management.Trace` namespace that contains the SQL Server Trace and Replay objects is also deprecated. However, Analysis Services workloads are supported.

For more information on [Extended Events](../../relational-databases/extended-events/extended-events.md), see the following articles:

- [Quickstart: Extended Events](../../relational-databases/extended-events/quick-start-extended-events-in-sql-server.md)
- For SQL Server Management Studio, use [XEvent Profiler](../../relational-databases/extended-events/use-the-ssms-xe-profiler.md)
- For the [MSSQL extension for Visual Studio Code](../visual-studio-code-extensions/mssql/mssql-extension-visual-studio-code.md), use [Query Profiler (Preview)](../visual-studio-code-extensions/mssql/mssql-query-profiler.md).

## Where's the Profiler?

You can start the Profiler within [Run SQL Server Profiler](start-sql-server-profiler.md).

## Capture and replay trace data

The following table shows the features you can use in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] to capture and replay your trace data.

| Feature / target workload | Relational Engine | Analysis Services |
| --- | --- | --- |
| **Trace Capture** | [Extended Events overview](../../relational-databases/extended-events/extended-events.md) graphical user interface in SQL Server Management Studio | [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] |
| **Trace Replay** | [SQL Server Distributed Replay overview](../distributed-replay/sql-server-distributed-replay.md) | [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] |

## Use SQL Server Profiler

Microsoft [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] is a graphical user interface to SQL Trace for monitoring an instance of the [!INCLUDE [ssDE](../../includes/ssde-md.md)] or Analysis Services. You can capture and save data about each event to a file or table to analyze later. For example, you can monitor a production environment to see which stored procedures affect performance by executing too slowly. Use [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] for activities such as:

- Stepping through problem queries to find the cause of the problem.

- Finding and diagnosing slow-running queries.

- Capturing the series of [!INCLUDE [tsql](../../includes/tsql-md.md)] statements that lead to a problem. The saved trace can then replicate the problem on a test server where the problem can be diagnosed.

- Monitoring the performance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to tune workloads. For information about tuning the physical database design for database workloads, see [Database Engine Tuning Advisor](../../relational-databases/performance/database-engine-tuning-advisor.md).

- Correlating performance counters to diagnose problems.

[!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] also supports auditing the actions performed on instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Audits record security-related actions for later review by a security administrator.

## SQL Server Profiler concepts

To use [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)], you need to understand the terms that describe the way the tool functions.

Understanding SQL Trace helps when working with [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)]. For more information, see [SQL Trace](../../relational-databases/sql-trace/sql-trace.md).

### Event

An event is an action generated within an instance of [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)]. Examples of these events include:

- Login connections, failures, and disconnections.
- [!INCLUDE [tsql](../../includes/tsql-md.md)] `SELECT`, `INSERT`, `UPDATE`, and `DELETE` statements.
- Remote procedure call (RPC) batch status.
- The start or end of a stored procedure.
- The start or end of statements within stored procedures.
- The start or end of a SQL batch.
- An error written to the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] error log.
- A lock acquired or released on a database object.
- An opened cursor.
- Security permission checks.

The trace displays all of the data generated by an event in a single row. Data columns that describe the event in detail intersect this row.

### EventClass

An event class is a type of event that you can trace. The event class contains all of the data that an event can report. The following list shows examples of event classes:

- **SQL:BatchCompleted**
- **Audit Login**
- **Audit Logout**
- **Lock: Acquired**
- **Lock: Released**

### EventCategory

An event category defines how [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] groups events. For example, the **Locks** event category groups all lock event classes. However, event categories exist only within [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)]. This term doesn't reflect how Engine events are grouped.

### DataColumn

A data column is an attribute of an event class captured in the trace. Because the event class determines the type of data that can be collected, not all data columns apply to all event classes. For example, in a trace that captures the **Lock: Acquired** event class, the **BinaryData** data column contains the value of the locked page ID or row, but the **Integer Data** data column doesn't contain any value because it doesn't apply to the event class being captured.

### Template

A template defines the default configuration for a trace. Specifically, it includes the event classes you want to monitor with [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)]. For example, you can create a template specifying the events, data columns, and filters. You can't execute a template directly. Instead, you save it as a file with a `.tdf` extension. Once saved, the template controls the trace data captured when a trace based on the template is launched.

### Trace

A trace captures data based on selected event classes, data columns, and filters. For example, you can create a trace to monitor exception errors. You select the **Exception** event class and the **Error**, **State**, and **Severity** data columns to do this. The trace results provide meaningful data only if data is collected from these three columns. You can run a trace configured in such a manner and collect data on any **Exception** events in the server. Save the trace data, or use it immediately for analysis. You can replay traces later, although certain events, such as **Exception** events, are never replayed. You can also save the trace as a template to build similar traces.

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] provides two ways to trace an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]: you can trace with [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)], or you can trace using system stored procedures.

### Filter

When you create a trace or template, you can define criteria to filter the data that the event collects. To keep traces from becoming too large, filter them so that you collect only a subset of the event data. For example, limiting the Microsoft Windows user names in the trace to specific users reduces the output data.

If you don't set a filter, the trace output returns all events of the selected event classes.

## SQL Server Profiler tasks

| Task description | Article |
| --- | --- |
| Lists the predefined templates that SQL Server provides for monitoring certain events and the permissions required to use replay traces. | [SQL Server Profiler templates and permissions](sql-server-profiler-templates-and-permissions.md) |
| Describes how to run [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Profiler. | [Permissions required to run SQL Server Profiler](permissions-required-to-run-sql-server-profiler.md) |
| Describes how to create a trace. | [Create a trace (SQL Server Profiler)](create-a-trace-sql-server-profiler.md) |
| Describes how to specify events and data columns for a trace file. | [Specify events and data columns for a trace file (SQL Server Profiler)](specify-events-and-data-columns-for-a-trace-file-sql-server-profiler.md) |
| Describes how to save trace results to a file. | [Save trace results to a file (SQL Server Profiler)](save-trace-results-to-a-file-sql-server-profiler.md) |
| Describes how to save trace results to a table. | [Save trace results to a table (SQL Server Profiler)](save-trace-results-to-a-table-sql-server-profiler.md) |
| Describes how to filter events in a trace. | [Filter events in a trace (SQL Server Profiler)](filter-events-in-a-trace-sql-server-profiler.md) |
| Describes how to view filter information. | [View filter information (SQL Server Profiler)](view-filter-information-sql-server-profiler.md) |
| Describes how to modify a filter. | [Modify a filter (SQL Server Profiler)](modify-a-filter-sql-server-profiler.md) |
| Describes how to set a maximum file size for a trace file ([!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)]). | [Set a maximum file size for a trace file (SQL Server Profiler)](set-a-maximum-file-size-for-a-trace-file-sql-server-profiler.md). |
| Describes how to set a maximum table size for a trace table. | [Set a maximum table size for a trace table (SQL Server Profiler)](set-a-maximum-table-size-for-a-trace-table-sql-server-profiler.md) |
| Describes how to start a trace. | [Start a trace (SQL Server Profiler)](start-a-trace.md) |
| Describes how to start a trace automatically after connecting to a server. | [Start a trace automatically after connecting to a server (SQL Server Profiler)](start-a-trace-automatically-after-connecting-to-a-server-sql-server-profiler.md) |
| Describes how to filter events based on the event start time. | [Filter events based on the event start time (SQL Server Profiler)](filter-events-based-on-the-event-start-time-sql-server-profiler.md) |
| Describes how to filter events based on the event end time. | [Filter events based on the event end Time (SQL Server Profiler)](filter-events-based-on-the-event-end-time-sql-server-profiler.md) |
| Describes how to filter session IDs in a trace. | [Filter session IDs in a trace (SQL Server Profiler)](filter-server-process-ids-spids-in-a-trace-sql-server-profiler.md) |
| Describes how to pause a trace. | [Pause a trace (SQL Server Profiler)](pause-a-trace-sql-server-profiler.md) |
| Describes how to stop a trace. | [Stop a trace (SQL Server Profiler)](stop-a-trace-sql-server-profiler.md) |
| Describes how to run a trace after it has been paused or stopped. | [Run a trace after it has been paused or stopped (SQL Server Profiler)](run-a-trace-after-it-has-been-paused-or-stopped-sql-server-profiler.md) |
| Describes how to clear a trace window. | [Clear a trace window (SQL Server Profiler)](clear-a-trace-window-sql-server-profiler.md) |
| Describes how to close a trace window. | [Close a trace window (SQL Server Profiler)](close-a-trace-window-sql-server-profiler.md) |
| Describes how to set trace definition defaults. | [Set trace definition defaults (SQL Server Profiler)](set-trace-definition-defaults-sql-server-profiler.md) |
| Describes how to set trace display defaults. | [Set trace display defaults (SQL Server Profiler)](set-trace-display-defaults-sql-server-profiler.md) |
| Describes how to open a trace file. | [Open a trace file (SQL Server Profiler)](open-a-trace-file-sql-server-profiler.md) |
| Describes how to open a trace table. | [Open a trace table (SQL Server Profiler)](open-a-trace-table-sql-server-profiler.md) |
| Describes how to replay a trace table. | [Replay a trace table (SQL Server Profiler)](replay-a-trace-table-sql-server-profiler.md) |
| Describes how to replay a trace file. | [Replay a trace file (SQL Server Profiler)](replay-a-trace-file-sql-server-profiler.md) |
| Describes how to replay a single event at a time. | [Replay a single event at a time (SQL Server Profiler)](replay-a-single-event-at-a-time-sql-server-profiler.md) |
| Describes how to replay to a breakpoint. | [Replay to a breakpoint (SQL Server Profiler)](replay-to-a-breakpoint-sql-server-profiler.md) |
| Describes how to replay to a cursor. | [Replay to a cursor (SQL Server Profiler)](replay-to-a-cursor-sql-server-profiler.md) |
| Describes how to replay a [!INCLUDE [tsql](../../includes/tsql-md.md)] script. | [Replay a Transact-SQL script (SQL Server Profiler)](replay-a-transact-sql-script-sql-server-profiler.md) |
| Describes how to create a trace template. | [Create a trace template (SQL Server Profiler)](create-a-trace-template-sql-server-profiler.md) |
| Describes how to modify a trace template. | [Modify trace templates](modify-trace-templates.md) |
| Describes how to set global trace options. | [Set global trace options (SQL Server Profiler)](set-global-trace-options-sql-server-profiler.md) |
| Describes how to find a value or data column while tracing. | [Find a value or data column while tracing (SQL Server Profiler)](find-a-value-or-data-column-while-tracing-sql-server-profiler.md) |
| Describes how to derive a template from a running trace. | [Derive a template from a running trace (SQL Server Profiler)](derive-a-template-from-a-running-trace-sql-server-profiler.md) |
| Describes how to derive a template from a trace file or trace table. | [Derive a template from a trace file or trace table (SQL Server Profiler)](derive-a-template-from-a-trace-file-or-trace-table-sql-server-profiler.md) |
| Describes how to create a [!INCLUDE [tsql](../../includes/tsql-md.md)] script for running a trace. | [Create a Transact-SQL script for running a trace (SQL Server Profiler)](create-a-transact-sql-script-for-running-a-trace-sql-server-profiler.md) |
| Describes how to export a trace template. | [Export a trace template (SQL Server Profiler)](export-a-trace-template-sql-server-profiler.md) |
| Describes how to import a trace template. | [Import a trace template (SQL Server Profiler)](import-a-trace-template-sql-server-profiler.md) |
| Describes how to extract a script from a trace. | [Extract a script from a trace (SQL Server Profiler)](extract-a-script-from-a-trace-sql-server-profiler.md) |
| Describes how to correlate a trace with Windows performance log data. | [Correlate a trace with Windows performance log data](correlate-a-trace-with-windows-performance-log-data.md) |
| Describes how to organize columns displayed in a trace. | [Organize columns displayed in a trace (SQL Server Profiler)](organize-columns-displayed-in-a-trace-sql-server-profiler.md) |
| Describes how to start [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)]. | [Run SQL Server Profiler](start-sql-server-profiler.md) |
| Describes how to save traces and trace templates. | [Save traces and trace templates](save-traces-and-trace-templates.md) |
| Describes how to modify trace templates. | [Modify trace templates](modify-trace-templates.md) |
| Describes how to correlate a trace with Windows performance log data. | [Correlate a trace with Windows performance log data](correlate-a-trace-with-windows-performance-log-data.md) |
| Describes how to view and analyze traces with [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)]. | [View and analyze traces with SQL Server Profiler](view-and-analyze-traces-with-sql-server-profiler.md) |
| Describes how to analyze deadlocks with [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)]. | [Analyze deadlocks with SQL Server Profiler](analyze-deadlocks-with-sql-server-profiler.md) |
| Describes how to analyze queries with SHOWPLAN results in SQL Server Profiler. | [Analyze queries with SHOWPLAN results in SQL Server Profiler](analyze-queries-with-showplan-results-in-sql-server-profiler.md) |
| Describes how to filter traces with [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)]. | [Filter traces with SQL Server Profiler](filter-traces-with-sql-server-profiler.md) |
| Describes how to use the replay features of [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)]. | [Replay Traces](replay-traces.md) |
| Lists the context-sensitive help articles for [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)]. | [SQL Server Profiler F1 Help](../../tools/sql-server-profiler/sql-server-profiler-f1-help.md) |
| Lists the system stored procedures that are used by [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] to monitor performance and activity. | [SQL Server Profiler stored procedures](../../relational-databases/system-stored-procedures/sql-server-profiler-stored-procedures-transact-sql.md) |

## Extended Events vs. SQL Server Profiler

[Extended Events overview](../../relational-databases/extended-events/extended-events.md) and SQL Server Profiler are tools for monitoring and troubleshooting SQL Server performance. **SQL Server Profiler is deprecated and should only be used with Analysis Services**. Extended Events is the replacement for SQL Server Profiler and provides advanced troubleshooting capabilities not available elsewhere. The key differences are noted here to help with the migration from SQL Server Profiler to Extended Events.

## Extended Events tool

[Extended Events overview](../../relational-databases/extended-events/extended-events.md) is a lightweight, highly scalable, and flexible event-handling system built into SQL Server.

Extended Events sessions typically consume fewer resources than SQL Trace and SQL Server Profiler, making them more suitable for production environments. Extended Events supports capturing events that are available in modern versions of SQL.

In contrast, the events available in SQL Trace/SQL Server Profiler are limited to features available in SQL Server 2008R2 and earlier.
Extended Events provides superior filtering capabilities, a smaller default payload, and features not offered in Profiler, such as in-memory and aggregate targets and multi-target support.

For more information about Extended Events, see [Extended Events overview](../../relational-databases/extended-events/extended-events.md).

## SQL Server Profiler tool

SQL Server Profiler is a graphical user interface that uses SQL Trace to capture activity for an instance of SQL Server or Analysis Services.

SQL Server Profiler can be resource-intensive if improperly configured, affecting server performance, especially when used on production servers. It has built-in templates to support quick tracing.

In summary, though SQL Server Profiler is an older tool that might be familiar to many users, Extended Events is a modern alternative that offers better performance, more detailed event information, and capabilities for troubleshooting and monitoring SQL Server instances not available elsewhere. Due to its advantages over Profiler, Extended Events is recommended for new tracing and monitoring work.

## Related content

- [Locks Event Category](../../relational-databases/event-classes/locks-event-category.md)
- [Sessions Event Category](../../relational-databases/event-classes/sessions-event-category.md)
- [Stored Procedures Event Category](../../relational-databases/event-classes/stored-procedures-event-category.md)
- [TSQL Event Category](../../relational-databases/event-classes/tsql-event-category.md)
- [Server Performance and Activity Monitoring](../../relational-databases/performance/server-performance-and-activity-monitoring.md)
