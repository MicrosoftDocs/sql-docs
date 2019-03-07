---
title: "SQL Server Profiler | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: profiler
ms.topic: conceptual
helpviewer_keywords: 
  - "Profiler [SQL Server Profiler], about SQL Server Profiler"
  - "traces [SQL Server], SQL Server Profiler"
  - "database monitoring [SQL Server], SQL Server Profiler"
  - "tuning databases [SQL Server], SQL Server Profiler"
  - "SQL Server Profiler"
  - "server performance [SQL Server], SQL Server Profiler"
  - "Profiler [SQL Server Profiler]"
  - "tracing [SQL Server]"
  - "monitoring performance [SQL Server], SQL Server Profiler"
  - "events [SQL Server], SQL Server Profiler"
  - "SQL Server Profiler, about SQL Server Profiler"
  - "tools [SQL Server], SQL Server Profiler"
  - "database performance [SQL Server], SQL Server Profiler"
  - "trace [SQL Server]"
ms.assetid: 3ad5f33d-559e-41a4-bde6-bb98792f7f1a
author: stevestein
ms.author: sstein
manager: craigg
---
# SQL Server Profiler
  [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] is a rich interface to create and manage traces and analyze and replay trace results. The events are saved in a trace file that can later be analyzed or used to replay a specific series of steps when trying to diagnose a problem.  
  
> [!IMPORTANT]  
>  We are announcing the deprecation of [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] for [!INCLUDE[ssDE](../../includes/ssde-md.md)] Trace Capture and Trace Replay. These features will be supported in the next version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], but will be removed in a later version. The specific version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has not been determined. The *Microsoft.SqlServer.Management.Trace* namespace that contains the Microsoft SQL Server Trace and Replay objects will also be deprecated. Note that [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] for the Analysis Services workloads is not being deprecated, and will continue to be supported.  
>   
>  The following table shows the features we recommend using in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] to capture and replay your trace data:  
  
||||  
|-|-|-|  
|**Feature\Target Workload**|**Relational Engine**|**Analysis Services**|  
|**Trace Capture**|Extended Events graphical user interface in SQL Server Management Studio|SQL Server Profiler|  
|**Trace Replay**|Distributed Replay|SQL Server Profiler|  
  
## Benefits of SQL Server Profiler  
 Microsoft [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] is a graphical user interface to SQL Trace for monitoring an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] or Analysis Services. You can capture and save data about each event to a file or table to analyze later. For example, you can monitor a production environment to see which stored procedures are affecting performance by executing too slowly. [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] is used for activities such as:  
  
-   Stepping through problem queries to find the cause of the problem.  
  
-   Finding and diagnosing slow-running queries.  
  
-   Capturing the series of [!INCLUDE[tsql](../../includes/tsql-md.md)] statements that lead to a problem. The saved trace can then be used to replicate the problem on a test server where the problem can be diagnosed.  
  
-   Monitoring the performance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to tune workloads. For information about tuning the physical database design for database workloads, see [Database Engine Tuning Advisor](../../relational-databases/performance/database-engine-tuning-advisor.md).  
  
-   Correlating performance counters to diagnose problems.  
  
 [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] also supports auditing the actions performed on instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Audits record security-related actions for later review by a security administrator.  
  
## SQL Server Profiler Concepts  
 To use [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)], you need to understand the terms that describe the way the tool functions.  
  
> [!NOTE]  
>  When working with [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)], it is helpful to understand SQL Trace. For more information, see [SQL Trace](../../relational-databases/sql-trace/sql-trace.md).  
  
 **Event**  
 An event is an action generated within an instance of [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]. Examples of these are:  
  
-   Login connections, failures, and disconnections.  
  
-   Transact-SQL SELECT, INSERT, UPDATE, and DELETE statements.  
  
-   Remote procedure call (RPC) batch status.  
  
-   The start or end of a stored procedure.  
  
-   The start or end of statements within stored procedures.  
  
-   The start or end of an SQL batch.  
  
-   An error written to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log.  
  
-   A lock acquired or released on a database object.  
  
-   An opened cursor.  
  
-   Security permission checks.  
  
 All of the data generated by an event is displayed in the trace in a single row. This row is intersected by data columns that describe the event in detail.  
  
 **EventClass**  
 An event class is a type of event that can be traced. The event class contains all of the data that can be reported by an event. Examples of event classes are the following:  
  
-   **SQL:BatchCompleted**  
  
-   **Audit Login**  
  
-   **Audit Logout**  
  
-   **Lock:Acquired**  
  
-   **Lock:Released**  
  
 **EventCategory**  
 An event category defines the way events are grouped within [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)]. For example, all lock events classes are grouped within the **Locks** event category. However, event categories only exist within [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)]. This term does not reflect the way Engine events are grouped.  
  
 **DataColumn**  
 A data column is an attribute of an event classes captured in the trace. Because the event class determines the type of data that can be collected, not all data columns are applicable to all event classes. For example, in a trace that captures the **Lock:Acquired** event class, the **BinaryData** data column contains the value of the locked page ID or row, but the **Integer Data** data column does not contain any value because it is not applicable to the event class being captured.  
  
 **Template**  
 A template defines the default configuration for a trace. Specifically, it includes the event classes you want to monitor with [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)]. For example, you can create a template that specifies the events, data columns, and filters to use. A template is not executed, but rather is saved as a file with a .tdf extension. Once saved, the template controls the trace data that is captured when a trace based on the template is launched.  
  
 **Trace**  
 A trace captures data based on selected event classes, data columns, and filters. For example, you can create a trace to monitor exception errors. To do this, you select the **Exception** event class and the **Error**, **State**, and **Severity** data columns. Data from these three columns needs to be collected in order for the trace results to provide meaningful data. You can then run a trace, configured in such a manner, and collect data on any **Exception** events that occur in the server. Trace data can be saved, or used immediately for analysis. Traces can be replayed at a later date, although certain events, such as **Exception** events, are never replayed. You can also save the trace as a template to build similar traces in the future.  
  
 SQL Server provides two ways to trace an instance of SQL Server: you can trace with [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)], or you can trace using system stored procedures.  
  
 **Filter**  
 When you create a trace or template, you can define criteria to filter the data collected by the event. To keep traces from becoming too large, you can filter them so that only a subset of the event data is collected. For example, you can limit the Microsoft Windows user names in the trace to specific users, thereby reducing the output data.  
  
 If a filter is not set, all events of the selected event classes are returned in the trace output.  
  
## SQL Server Profiler Tasks  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Lists the predefined templates that SQL Server provides for monitoring certain types of events, and the permissions required to use to replay traces.|[SQL Server Profiler Templates and Permissions](sql-server-profiler-templates-and-permissions.md)|  
|Describes how to run SQL Server Profiler.|[Permissions Required to Run SQL Server Profiler](permissions-required-to-run-sql-server-profiler.md)|  
|Describes how to create a trace.|[Create a Trace &#40;SQL Server Profiler&#41;](create-a-trace-sql-server-profiler.md)|  
|Describes how to specify events and data columns for a trace file.|[Specify Events and Data Columns for a Trace File &#40;SQL Server Profiler&#41;](specify-events-and-data-columns-for-a-trace-file-sql-server-profiler.md)|  
|Describes how to save trace results to a file.|[Save Trace Results to a File &#40;SQL Server Profiler&#41;](save-trace-results-to-a-file-sql-server-profiler.md)|  
|Describes how to save trace results to a table.|[Save Trace Results to a Table &#40;SQL Server Profiler&#41;](save-trace-results-to-a-table-sql-server-profiler.md)|  
|Describes how to filter events in a trace.|[Filter Events in a Trace &#40;SQL Server Profiler&#41;](filter-events-in-a-trace-sql-server-profiler.md)|  
|Describes how to view filter information.|[View Filter Information &#40;SQL Server Profiler&#41;](view-filter-information-sql-server-profiler.md)|  
|Describes how to Modify a Filter.|[Modify a Filter &#40;SQL Server Profiler&#41;](modify-a-filter-sql-server-profiler.md)|  
|Describes how to Set a Maximum File Size for a Trace File (SQL Server Profiler).|[Set a Maximum File Size for a Trace File &#40;SQL Server Profiler&#41;](set-a-maximum-file-size-for-a-trace-file-sql-server-profiler.md)|  
|Describes how to set a maximum table size for a trace table.|[Set a Maximum Table Size for a Trace Table &#40;SQL Server Profiler&#41;](set-a-maximum-table-size-for-a-trace-table-sql-server-profiler.md)|  
|Describes how to start a trace.|[Start a Trace](start-a-trace.md)|  
|Describes how to start a trace automatically after connecting to a server.|[Start a Trace Automatically after Connecting to a Server &#40;SQL Server Profiler&#41;](start-a-trace-automatically-after-connecting-to-a-server-sql-server-profiler.md)|  
|Describes how to filter events based on the event start time.|[Filter Events Based on the Event Start Time &#40;SQL Server Profiler&#41;](filter-events-based-on-the-event-start-time-sql-server-profiler.md)|  
|Describes how to filter events based on the event end time.|[Filter Events Based on the Event End Time &#40;SQL Server Profiler&#41;](filter-events-based-on-the-event-end-time-sql-server-profiler.md)|  
|Describes how to filter server process IDs (SPIDs) in a trace.|[Filter Server Process IDs &#40;SPIDs&#41; in a Trace &#40;SQL Server Profiler&#41;](filter-server-process-ids-spids-in-a-trace-sql-server-profiler.md)|  
|Describes how to pause a trace.|[Pause a Trace &#40;SQL Server Profiler&#41;](pause-a-trace-sql-server-profiler.md)|  
|Describes how to stop a trace.|[Stop a Trace &#40;SQL Server Profiler&#41;](stop-a-trace-sql-server-profiler.md)|  
|Describes how to run a trace after it has been paused or stopped.|[Run a Trace After It Has Been Paused or Stopped &#40;SQL Server Profiler&#41;](run-a-trace-after-it-has-been-paused-or-stopped-sql-server-profiler.md)|  
|Describes how to clear a trace window.|[Clear a Trace Window &#40;SQL Server Profiler&#41;](clear-a-trace-window-sql-server-profiler.md)|  
|Describes how to close a trace window.|[Close a Trace Window &#40;SQL Server Profiler&#41;](close-a-trace-window-sql-server-profiler.md)|  
|Describes how to set trace definition defaults.|[Set Trace Definition Defaults &#40;SQL Server Profiler&#41;](set-trace-definition-defaults-sql-server-profiler.md)|  
|Describes how to set trace display defaults.|[Set Trace Display Defaults &#40;SQL Server Profiler&#41;](set-trace-display-defaults-sql-server-profiler.md)|  
|Describes how to open a trace file.|[Open a Trace File &#40;SQL Server Profiler&#41;](open-a-trace-file-sql-server-profiler.md)|  
|Describes how to open a trace table.|[Open a Trace Table &#40;SQL Server Profiler&#41;](open-a-trace-table-sql-server-profiler.md)|  
|Describes how to replay a trace table.|[Replay a Trace Table &#40;SQL Server Profiler&#41;](replay-a-trace-table-sql-server-profiler.md)|  
|Describes how to replay a trace file.|[Replay a Trace File &#40;SQL Server Profiler&#41;](replay-a-trace-file-sql-server-profiler.md)|  
|Describes how to replay a single event at a time.|[Replay a Single Event at a Time &#40;SQL Server Profiler&#41;](replay-a-single-event-at-a-time-sql-server-profiler.md)|  
|Describes how to replay to a breakpoint.|[Replay to a Breakpoint &#40;SQL Server Profiler&#41;](replay-to-a-breakpoint-sql-server-profiler.md)|  
|Describes how to replay to a cursor.|[Replay to a Cursor &#40;SQL Server Profiler&#41;](replay-to-a-cursor-sql-server-profiler.md)|  
|Describes how to replay a Transact-SQL script.|[Replay a Transact-SQL Script &#40;SQL Server Profiler&#41;](replay-a-transact-sql-script-sql-server-profiler.md)|  
|Describes how to create a trace template.|[Create a Trace Template &#40;SQL Server Profiler&#41;](create-a-trace-template-sql-server-profiler.md)|  
|Describes how to modify a trace template.|[Modify a Trace Template &#40;SQL Server Profiler&#41;](../../database-engine/modify-a-trace-template-sql-server-profiler.md)|  
|Describes how to set global trace options.|[Set Global Trace Options &#40;SQL Server Profiler&#41;](set-global-trace-options-sql-server-profiler.md)|  
|Describes how to find a value or data column while tracing.|[Find a Value or Data Column While Tracing &#40;SQL Server Profiler&#41;](find-a-value-or-data-column-while-tracing-sql-server-profiler.md)|  
|Describes how to derive a template from a running trace.|[Derive a Template from a Running Trace &#40;SQL Server Profiler&#41;](derive-a-template-from-a-running-trace-sql-server-profiler.md)|  
|Describes how to derive a template from a trace file or trace table.|[Derive a Template from a Trace File or Trace Table &#40;SQL Server Profiler&#41;](derive-a-template-from-a-trace-file-or-trace-table-sql-server-profiler.md)|  
|Describes how to create a Transact-SQL script for running a trace.|[Create a Transact-SQL Script for Running a Trace &#40;SQL Server Profiler&#41;](create-a-transact-sql-script-for-running-a-trace-sql-server-profiler.md)|  
|Describes how to export a trace template.|[Export a Trace Template &#40;SQL Server Profiler&#41;](export-a-trace-template-sql-server-profiler.md)|  
|Describes how to import a trace template.|[Import a Trace Template &#40;SQL Server Profiler&#41;](import-a-trace-template-sql-server-profiler.md)|  
|Describes how to extract a script from a trace.|[Extract a Script from a Trace &#40;SQL Server Profiler&#41;](extract-a-script-from-a-trace-sql-server-profiler.md)|  
|Describes how to correlate a trace with Windows performance log data.|[Correlate a Trace with Windows Performance Log Data &#40;SQL Server Profiler&#41;](../../database-engine/correlate-a-trace-with-windows-performance-log-data-sql-server-profiler.md)|  
|Describes how to organize columns displayed in a trace.|[Organize Columns Displayed in a Trace &#40;SQL Server Profiler&#41;](organize-columns-displayed-in-a-trace-sql-server-profiler.md)|  
|Describes how to start SQL Server Profiler.|[Start SQL Server Profiler](start-sql-server-profiler.md)|  
|Describes how to save traces and trace templates.|[Save Traces and Trace Templates](save-traces-and-trace-templates.md)|  
|Describes how to modify trace templates.|[Modify Trace Templates](modify-trace-templates.md)|  
|Describes how to correlate a trace with Windows performance log data.|[Correlate a Trace with Windows Performance Log Data](correlate-a-trace-with-windows-performance-log-data.md)|  
|Describes how to view and analyze traces with SQL Server Profiler.|[View and Analyze Traces with SQL Server Profiler](view-and-analyze-traces-with-sql-server-profiler.md)|  
|Describes how to analyze deadlocks with SQL Server Profiler.|[Analyze Deadlocks with SQL Server Profiler](analyze-deadlocks-with-sql-server-profiler.md)|  
|Describes how to analyze queries with SHOWPLAN results in SQL Server Profiler.|[Analyze Queries with SHOWPLAN Results in SQL Server Profiler](analyze-queries-with-showplan-results-in-sql-server-profiler.md)|  
|Describes how to filter traces with SQL Server Profiler.|[Filter Traces with SQL Server Profiler](filter-traces-with-sql-server-profiler.md)|  
|Describes how to use the replay features of SQL Server Profiler.|[Replay Traces](replay-traces.md)|  
|Lists the context-sensitive help topics for SQL Server Profiler.|[SQL Server Profiler F1 Help](sql-server-profiler-f1-help.md)|  
|Lists the system stored procedures that are used by [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] to monitor performance and activity.|[SQL Server Profiler Stored Procedures &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sql-server-profiler-stored-procedures-transact-sql)|  
  
## See Also  
 [Locks Event Category](../../relational-databases/event-classes/locks-event-category.md)   
 [Sessions Event Category](../../relational-databases/event-classes/sessions-event-category.md)   
 [Stored Procedures Event Category](../../relational-databases/event-classes/stored-procedures-event-category.md)   
 [TSQL Event Category](../../relational-databases/event-classes/tsql-event-category.md)   
 [Server Performance and Activity Monitoring](../../relational-databases/performance/server-performance-and-activity-monitoring.md)  
  
  
