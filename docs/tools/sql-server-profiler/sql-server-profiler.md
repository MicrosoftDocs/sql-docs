---
title: SQL Server Profiler
titleSuffix: SQL Server Profiler
description: Explore the features of SQL Server Profiler. Get help troubleshooting problems by using this tool to create traces and analyze and replay trace results.
ms.service: sql
ms.subservice: profiler
ms.topic: conceptual
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: seo-lt-2019
ms.date: 06/28/2021
---

# SQL Server Profiler

[!INCLUDE[sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

[!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] is an interface to create and manage traces and analyze and replay trace results. Events are saved in a trace file that can later be analyzed or used to replay a specific series of steps when diagnosing a problem.

> [!IMPORTANT]
> SQL Trace and [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] are deprecated. The *Microsoft.SqlServer.Management.Trace* namespace that contains the Microsoft SQL Server Trace and Replay objects are also deprecated.
> [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]
> Use Extended Events instead. For more information on [Extended Events](../../relational-databases/extended-events/extended-events.md), see [Quick Start: Extended events in SQL Server](../../relational-databases/extended-events/quick-start-extended-events-in-sql-server.md) and [SSMS XEvent Profiler](../../relational-databases/extended-events/use-the-ssms-xe-profiler.md).

> [!NOTE]
> [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] for Analysis Services workloads are supported.

> [!NOTE]
> When you try to connect to a Azure SQL Database from SQL Server profiler, it incorrectly throws a misleading error message as follows:
>
> - In order to run a trace against SQL Server, you must be a member of sysadmin fixed server role or have the ALTER TRACE permission.
>
> The message should have explained that Azure SQL Database is not supported by SQL Server profiler.

## Where is the Profiler?

You can start the Profiler within [SQL Server Management Studio](start-sql-server-profiler.md) or with the [Azure Data Studio using the SQL Server Profiler extension](../../azure-data-studio/extensions/sql-server-profiler-extension.md). 

## Capture and replay trace data

The following table shows the features we recommend using in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] to capture and replay your trace data.

|**Feature\Target Workload**|**Relational Engine**|**Analysis Services**|  
|-|-|-|
|**Trace Capture**|[Extended Events](../../relational-databases/extended-events/extended-events.md) graphical user interface in SQL Server Management Studio|[!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)]|  
|**Trace Replay**|[Distributed Replay](../distributed-replay/sql-server-distributed-replay.md)|[!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)]|

## Use SQL Server Profiler

Microsoft [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] is a graphical user interface to SQL Trace for monitoring an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] or Analysis Services. You can capture and save data about each event to a file or table to analyze later. For example, you can monitor a production environment to see which stored procedures are affecting performance by executing too slowly. [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] is used for activities such as:

- Stepping through problem queries to find the cause of the problem.

- Finding and diagnosing slow-running queries.

- Capturing the series of [!INCLUDE[tsql](../../includes/tsql-md.md)] statements that lead to a problem. The saved trace can then be used to replicate the problem on a test server where the problem can be diagnosed.

- Monitoring the performance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to tune workloads. For information about tuning the physical database design for database workloads, see [Database Engine Tuning Advisor](../../relational-databases/performance/database-engine-tuning-advisor.md).

- Correlating performance counters to diagnose problems.

[!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] also supports auditing the actions performed on instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Audits record security-related actions for later review by a security administrator.

## SQL Server Profiler concepts

To use [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)], you need to understand the terms that describe the way the tool functions.

> [!NOTE]
> Understanding SQL Trace really helps when working with [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)]. For more information, see [SQL Trace](../../relational-databases/sql-trace/sql-trace.md).

### Event

An event is an action generated within an instance of [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]. Examples of these are:  
  
- Login connections, failures, and disconnections.    
- [!INCLUDE[tsql](../../includes/tsql-md.md)] `SELECT`, `INSERT`, `UPDATE`, and `DELETE` statements.
- Remote procedure call (RPC) batch status.  
- The start or end of a stored procedure.  
- The start or end of statements within stored procedures.  
- The start or end of an SQL batch.  
- An error written to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log.  
- A lock acquired or released on a database object.  
- An opened cursor.  
- Security permission checks.  
  
All of the data generated by an event is displayed in the trace in a single row. This row is intersected by data columns that describe the event in detail.  

### EventClass

An event class is a type of event that can be traced. The event class contains all of the data that can be reported by an event. The following are examples of event classes:

- **SQL:BatchCompleted**
- **Audit Login**
- **Audit Logout**
- **Lock: Acquired**
- **Lock: Released**

### EventCategory

An event category defines the way events are grouped within [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)]. For example, all lock events classes are grouped within the **Locks** event category. However, event categories only exist within [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)]. This term doesn't reflect the way Engine events are grouped.

### DataColumn

A data column is an attribute of an event class captured in the trace. Because the event class determines the type of data that can be collected, not all data columns are applicable to all event classes. For example, in a trace that captures the **Lock: Acquired** event class, the **BinaryData** data column contains the value of the locked page ID or row, but the **Integer Data** data column doesn't contain any value because it isn't applicable to the event class being captured.

### Template

A template defines the default configuration for a trace. Specifically, it includes the event classes you want to monitor with [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)]. For example, you can create a template that specifies the events, data columns, and filters to use. A template isn't executed, but rather is saved as a file with a .tdf extension. Once saved, the template controls the trace data that is captured when a trace based on the template is launched.

### Trace

A trace captures data based on selected event classes, data columns, and filters. For example, you can create a trace to monitor exception errors. To do this, you select the **Exception** event class and the **Error**, **State**, and **Severity** data columns. Data from these three columns needs to be collected in order for the trace results to provide meaningful data. You can then run a trace, configured in such a manner, and collect data on any **Exception** events that occur in the server. Trace data can be saved, or used immediately for analysis. Traces can be replayed at a later date, although certain events, such as **Exception** events, are never replayed. You can also save the trace as a template to build similar traces in the future.  

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides two ways to trace an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]: you can trace with [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)], or you can trace using system stored procedures.  

### Filter

When you create a trace or template, you can define criteria to filter the data collected by the event. To keep traces from becoming too large, you can filter them so that only a subset of the event data is collected. For example, you can limit the Microsoft Windows user names in the trace to specific users, thereby reducing the output data.  

If a filter isn't set, all events of the selected event classes are returned in the trace output.

## SQL Server Profiler tasks

|Task description|Topic|  
|----------------------|-----------|  
|Lists the predefined templates that SQL Server provides for monitoring certain types of events, and the permissions required to use to replay traces.|[SQL Server Profiler Templates and Permissions](../../tools/sql-server-profiler/sql-server-profiler-templates-and-permissions.md)|  
|Describes how to run [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Profiler.|[Permissions Required to Run SQL Server Profiler](../../tools/sql-server-profiler/permissions-required-to-run-sql-server-profiler.md)|  
|Describes how to create a trace.|[Create a Trace &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/create-a-trace-sql-server-profiler.md)|  
|Describes how to specify events and data columns for a trace file.|[Specify Events and Data Columns for a Trace File &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/specify-events-and-data-columns-for-a-trace-file-sql-server-profiler.md)|  
|Describes how to save trace results to a file.|[Save Trace Results to a File &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/save-trace-results-to-a-file-sql-server-profiler.md)|  
|Describes how to save trace results to a table.|[Save Trace Results to a Table &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/save-trace-results-to-a-table-sql-server-profiler.md)|  
|Describes how to filter events in a trace.|[Filter Events in a Trace &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/filter-events-in-a-trace-sql-server-profiler.md)|  
|Describes how to view filter information.|[View Filter Information &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/view-filter-information-sql-server-profiler.md)|  
|Describes how to Modify a Filter.|[Modify a Filter &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/modify-a-filter-sql-server-profiler.md)|  
|Describes how to Set a Maximum File Size for a Trace File ([!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)]).|[Set a Maximum File Size for a Trace File &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/set-a-maximum-file-size-for-a-trace-file-sql-server-profiler.md)|  
|Describes how to set a maximum table size for a trace table.|[Set a Maximum Table Size for a Trace Table &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/set-a-maximum-table-size-for-a-trace-table-sql-server-profiler.md)|  
|Describes how to start a trace.|[Start a Trace](../../tools/sql-server-profiler/start-a-trace.md)|  
|Describes how to start a trace automatically after connecting to a server.|[Start a Trace Automatically after Connecting to a Server &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/start-a-trace-automatically-after-connecting-to-a-server-sql-server-profiler.md)|  
|Describes how to filter events based on the event start time.|[Filter Events Based on the Event Start Time &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/filter-events-based-on-the-event-start-time-sql-server-profiler.md)|  
|Describes how to filter events based on the event end time.|[Filter Events Based on the Event End Time &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/filter-events-based-on-the-event-end-time-sql-server-profiler.md)|  
|Describes how to filter server process IDs (SPIDs) in a trace.|[Filter Server Process IDs &#40;SPIDs&#41; in a Trace &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/filter-server-process-ids-spids-in-a-trace-sql-server-profiler.md)|  
|Describes how to pause a trace.|[Pause a Trace &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/pause-a-trace-sql-server-profiler.md)|  
|Describes how to stop a trace.|[Stop a Trace &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/stop-a-trace-sql-server-profiler.md)|  
|Describes how to run a trace after it has been paused or stopped.|[Run a Trace After It Has Been Paused or Stopped &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/run-a-trace-after-it-has-been-paused-or-stopped-sql-server-profiler.md)|  
|Describes how to clear a trace window.|[Clear a Trace Window &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/clear-a-trace-window-sql-server-profiler.md)|  
|Describes how to close a trace window.|[Close a Trace Window &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/close-a-trace-window-sql-server-profiler.md)|  
|Describes how to set trace definition defaults.|[Set Trace Definition Defaults &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/set-trace-definition-defaults-sql-server-profiler.md)|  
|Describes how to set trace display defaults.|[Set Trace Display Defaults &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/set-trace-display-defaults-sql-server-profiler.md)|  
|Describes how to open a trace file.|[Open a Trace File &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/open-a-trace-file-sql-server-profiler.md)|  
|Describes how to open a trace table.|[Open a Trace Table &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/open-a-trace-table-sql-server-profiler.md)|  
|Describes how to replay a trace table.|[Replay a Trace Table &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/replay-a-trace-table-sql-server-profiler.md)|  
|Describes how to replay a trace file.|[Replay a Trace File &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/replay-a-trace-file-sql-server-profiler.md)|  
|Describes how to replay a single event at a time.|[Replay a Single Event at a Time &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/replay-a-single-event-at-a-time-sql-server-profiler.md)|  
|Describes how to replay to a breakpoint.|[Replay to a Breakpoint &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/replay-to-a-breakpoint-sql-server-profiler.md)|  
|Describes how to replay to a cursor.|[Replay to a Cursor &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/replay-to-a-cursor-sql-server-profiler.md)|  
|Describes how to replay a [!INCLUDE[tsql](../../includes/tsql-md.md)] script.|[Replay a Transact-SQL Script &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/replay-a-transact-sql-script-sql-server-profiler.md)|  
|Describes how to create a trace template.|[Create a Trace Template &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/create-a-trace-template-sql-server-profiler.md)|  
|Describes how to modify a trace template.|[Modify a Trace Template &#40;SQL Server Profiler&#41;](./modify-trace-templates.md)|  
|Describes how to set global trace options.|[Set Global Trace Options &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/set-global-trace-options-sql-server-profiler.md)|  
|Describes how to find a value or data column while tracing.|[Find a Value or Data Column While Tracing &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/find-a-value-or-data-column-while-tracing-sql-server-profiler.md)|  
|Describes how to derive a template from a running trace.|[Derive a Template from a Running Trace &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/derive-a-template-from-a-running-trace-sql-server-profiler.md)|  
|Describes how to derive a template from a trace file or trace table.|[Derive a Template from a Trace File or Trace Table &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/derive-a-template-from-a-trace-file-or-trace-table-sql-server-profiler.md)|  
|Describes how to create a [!INCLUDE[tsql](../../includes/tsql-md.md)] script for running a trace.|[Create a Transact-SQL Script for Running a Trace &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/create-a-transact-sql-script-for-running-a-trace-sql-server-profiler.md)|  
|Describes how to export a trace template.|[Export a Trace Template &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/export-a-trace-template-sql-server-profiler.md)|  
|Describes how to import a trace template.|[Import a Trace Template &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/import-a-trace-template-sql-server-profiler.md)|  
|Describes how to extract a script from a trace.|[Extract a Script from a Trace &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/extract-a-script-from-a-trace-sql-server-profiler.md)|  
|Describes how to correlate a trace with Windows performance log data.|[Correlate a Trace with Windows Performance Log Data &#40;SQL Server Profiler&#41;](./correlate-a-trace-with-windows-performance-log-data.md)|  
|Describes how to organize columns displayed in a trace.|[Organize Columns Displayed in a Trace &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/organize-columns-displayed-in-a-trace-sql-server-profiler.md)|  
|Describes how to start [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)].|[Start SQL Server Profiler](../../tools/sql-server-profiler/start-sql-server-profiler.md)|  
|Describes how to save traces and trace templates.|[Save Traces and Trace Templates](../../tools/sql-server-profiler/save-traces-and-trace-templates.md)|  
|Describes how to modify trace templates.|[Modify Trace Templates](../../tools/sql-server-profiler/modify-trace-templates.md)|  
|Describes how to correlate a trace with Windows performance log data.|[Correlate a Trace with Windows Performance Log Data](../../tools/sql-server-profiler/correlate-a-trace-with-windows-performance-log-data.md)|  
|Describes how to view and analyze traces with [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)].|[View and Analyze Traces with SQL Server Profiler](../../tools/sql-server-profiler/view-and-analyze-traces-with-sql-server-profiler.md)|  
|Describes how to analyze deadlocks with [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)].|[Analyze Deadlocks with SQL Server Profiler](../../tools/sql-server-profiler/analyze-deadlocks-with-sql-server-profiler.md)|  
|Describes how to analyze queries with SHOWPLAN results in SQL Server Profiler.|[Analyze Queries with SHOWPLAN Results in SQL Server Profiler](../../tools/sql-server-profiler/analyze-queries-with-showplan-results-in-sql-server-profiler.md)|  
|Describes how to filter traces with [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)].|[Filter Traces with SQL Server Profiler](../../tools/sql-server-profiler/filter-traces-with-sql-server-profiler.md)|  
|Describes how to use the replay features of [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)].|[Replay Traces](../../tools/sql-server-profiler/replay-traces.md)|  
|Lists the context-sensitive help topics for [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)].|[SQL Server Profiler F1 Help](../../tools/sql-server-profiler/sql-server-profiler-f1-help.md)|  
|Lists the system stored procedures that are used by [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] to monitor performance and activity.|[SQL Server Profiler Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sql-server-profiler-stored-procedures-transact-sql.md)|  

## See also

- [Locks Event Category](../../relational-databases/event-classes/locks-event-category.md)
- [Sessions Event Category](../../relational-databases/event-classes/sessions-event-category.md)
- [Stored Procedures Event Category](../../relational-databases/event-classes/stored-procedures-event-category.md)
- [TSQL Event Category](../../relational-databases/event-classes/tsql-event-category.md)
- [Server Performance and Activity Monitoring](../../relational-databases/performance/server-performance-and-activity-monitoring.md)
