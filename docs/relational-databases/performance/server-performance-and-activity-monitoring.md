---
title: "Server Performance and Activity Monitoring | Microsoft Docs"
ms.custom: ""
ms.date: "11/27/2018"
ms.prod: sql
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "activity monitoring [SQL Server]"
  - "traces [SQL Server], how-to topics"
  - "monitoring server performance [SQL Server], activity monitoring"
  - "stored procedures [SQL Server], traces"
  - "performance [SQL Server], servers"
  - "servers [SQL Server], performance"
  - "SQL Server Profiler, how-to topics"
  - "SQL Server Management Studio [SQL Server], monitoring system"
  - "Profiler [SQL Server Profiler], how-to topics"
ms.assetid: f9abe48d-d6e9-4c38-a355-fc5eb5a95a25
author: julieMSFT
ms.author: jrasnick
manager: craigg
---
# Server Performance and Activity Monitoring
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  The goal of monitoring databases is to assess how a server is performing. Effective monitoring involves taking periodic snapshots of current performance to isolate processes that are causing problems, and gathering data continuously over time to track performance trends. [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows operating system provide utilities that let you view the current condition of the database and to track performance as conditions change.  
  
 The following section contains topics that describe how to use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows performance and activity monitoring tools. It contains the following topics:  
  
## To perform monitoring tasks with Windows tools 
  
-   [Start System Monitor &#40;Windows&#41;](../../relational-databases/performance/start-system-monitor-windows.md)  
  
-   [View the Windows Application Log &#40;Windows&#41;](../../relational-databases/performance/view-the-windows-application-log-windows-10.md)  
  
## To create SQL Server database alerts with Windows tools  
  
-   [Set Up a SQL Server Database Alert &#40;Windows&#41;](../../relational-databases/performance/set-up-a-sql-server-database-alert-windows.md)  

## To perform monitoring tasks with Extended Events  
 
 -   [Extended Events](../../relational-databases/extended-events/extended-events.md)
 
 -   [Quick Start: Extended events in SQL Server](../../relational-databases/extended-events/quick-start-extended-events-in-sql-server.md)
 
 -   [Manage Event Sessions in the Object Explorer](../../relational-databases/extended-events/manage-event-sessions-in-the-object-explorer.md)
 
 -   [Alter an Extended Events Session](../../relational-databases/extended-events/alter-an-extended-events-session.md)
 
 -   [Convert an Existing SQL Trace Script to an Extended Events Session](../../relational-databases/extended-events/convert-an-existing-sql-trace-script-to-an-extended-events-session.md)
 
 -   [View the Extended Events Equivalents to SQL Trace Event Classes](../../relational-databases/extended-events/view-the-extended-events-equivalents-to-sql-trace-event-classes.md)
   
## To perform monitoring tasks with SQL Server Management Studio  
  
-   [View the SQL Server Error Log &#40;SQL Server Management Studio&#41;](../../relational-databases/performance/view-the-sql-server-error-log-sql-server-management-studio.md)  
  
-   [Open Activity Monitor &#40;SQL Server Management Studio&#41;](../../relational-databases/performance-monitor/open-activity-monitor-sql-server-management-studio.md)  

-   [Monitoring performance by using the Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md)

## To perform monitoring tasks with SQL Trace and SQL Server Profiler

> [!IMPORTANT]
> The next sections describe methods of using SQL Trace and [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)].  
> SQL Trace and [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] are deprecated. The *Microsoft.SqlServer.Management.Trace* namespace that contains the Microsoft SQL Server Trace and Replay objects are also deprecated.   
> [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]  
> Use Extended Events instead. For more information on [Extended Events](../../relational-databases/extended-events/extended-events.md), see [Quick Start: Extended events in SQL Server](../../relational-databases/extended-events/quick-start-extended-events-in-sql-server.md) and [SSMS XEvent Profiler](../../relational-databases/extended-events/use-the-ssms-xe-profiler.md).

> [!NOTE] 
> [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] for Analysis Services workloads is NOT deprecated, and will continue to be supported.

### To perform monitoring tasks with SQL Trace by using Transact-SQL stored procedures  
  
-   [Create a Trace &#40;Transact-SQL&#41;](../../relational-databases/sql-trace/create-a-trace-transact-sql.md)  
  
-   [Set a Trace Filter &#40;Transact-SQL&#41;](../../relational-databases/sql-trace/set-a-trace-filter-transact-sql.md)  
  
-   [Modify an Existing Trace &#40;Transact-SQL&#41;](../../relational-databases/sql-trace/modify-an-existing-trace-transact-sql.md)  
  
-   [View a Saved Trace &#40;Transact-SQL&#41;](../../relational-databases/sql-trace/view-a-saved-trace-transact-sql.md)  
  
-   [View Filter Information &#40;Transact-SQL&#41;](../../relational-databases/sql-trace/view-filter-information-transact-sql.md)  
  
-   [Delete a Trace &#40;Transact-SQL&#41;](../../relational-databases/sql-trace/delete-a-trace-transact-sql.md)  
  
### To create and modify traces by using SQL Server Profiler  
  
-   [Create a Trace &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/create-a-trace-sql-server-profiler.md)  
  
-   [Set Global Trace Options &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/set-global-trace-options-sql-server-profiler.md)  
  
-   [Specify Events and Data Columns for a Trace File &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/specify-events-and-data-columns-for-a-trace-file-sql-server-profiler.md)  
  
-   [Create a Transact-SQL Script for Running a Trace &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/create-a-transact-sql-script-for-running-a-trace-sql-server-profiler.md)  
  
-   [Save Trace Results to a File &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/save-trace-results-to-a-file-sql-server-profiler.md)  
  
-   [Set a Maximum File Size for a Trace File &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/set-a-maximum-file-size-for-a-trace-file-sql-server-profiler.md)  
  
-   [Save Trace Results to a Table &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/save-trace-results-to-a-table-sql-server-profiler.md)  
  
-   [Set a Maximum Table Size for a Trace Table &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/set-a-maximum-table-size-for-a-trace-table-sql-server-profiler.md)  
  
-   [Filter Events in a Trace &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/filter-events-in-a-trace-sql-server-profiler.md)  
  
-   [View Filter Information &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/view-filter-information-sql-server-profiler.md)  
  
-   [Modify a Filter &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/modify-a-filter-sql-server-profiler.md)  
  
-   [Filter Events Based on the Event Start Time &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/filter-events-based-on-the-event-start-time-sql-server-profiler.md)  
  
-   [Filter Events Based on the Event End Time &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/filter-events-based-on-the-event-end-time-sql-server-profiler.md)  
  
-   [Filter Server Process IDs &#40;SPIDs&#41; in a Trace &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/filter-server-process-ids-spids-in-a-trace-sql-server-profiler.md)  
  
-   [Organize Columns Displayed in a Trace &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/organize-columns-displayed-in-a-trace-sql-server-profiler.md)  
  
### To start, pause, and stop traces by using SQL Server Profiler  
  
-   [Start a Trace Automatically after Connecting to a Server &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/start-a-trace-automatically-after-connecting-to-a-server-sql-server-profiler.md)  
  
-   [Pause a Trace &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/pause-a-trace-sql-server-profiler.md)  
  
-   [Stop a Trace &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/stop-a-trace-sql-server-profiler.md)  
  
-   [Run a Trace After It Has Been Paused or Stopped &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/run-a-trace-after-it-has-been-paused-or-stopped-sql-server-profiler.md)  
  
### To open traces and configure how traces are displayed by using SQL Server Profiler  
  
-   [Open a Trace File &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/open-a-trace-file-sql-server-profiler.md)  
  
-   [Open a Trace Table &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/open-a-trace-table-sql-server-profiler.md)  
  
-   [Clear a Trace Window &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/clear-a-trace-window-sql-server-profiler.md)  
  
-   [Close a Trace Window &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/close-a-trace-window-sql-server-profiler.md)  
  
-   [Set Trace Definition Defaults &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/set-trace-definition-defaults-sql-server-profiler.md)  
  
-   [Set Trace Display Defaults &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/set-trace-display-defaults-sql-server-profiler.md)  
  
### To replay traces by using SQL Server Profiler  
  
-   [Replay a Trace File &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/replay-a-trace-file-sql-server-profiler.md)  
  
-   [Replay a Trace Table &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/replay-a-trace-table-sql-server-profiler.md)  
  
-   [Replay a Single Event at a Time &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/replay-a-single-event-at-a-time-sql-server-profiler.md)  
  
-   [Replay to a Breakpoint &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/replay-to-a-breakpoint-sql-server-profiler.md)  
  
-   [Replay to a Cursor &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/replay-to-a-cursor-sql-server-profiler.md)  
  
-   [Replay a Transact-SQL Script &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/replay-a-transact-sql-script-sql-server-profiler.md)  
  
### To create, modify, and use trace templates by using SQL Server Profiler  
  
-   [Create a Trace Template &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/create-a-trace-template-sql-server-profiler.md)  
  
-   [Modify a Trace Template &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/modify-a-trace-template-sql-server-profiler.md)  
  
-   [Derive a Template from a Running Trace &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/derive-a-template-from-a-running-trace-sql-server-profiler.md)  
  
-   [Derive a Template from a Trace File or Trace Table &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/derive-a-template-from-a-trace-file-or-trace-table-sql-server-profiler.md)  
  
-   [Export a Trace Template &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/export-a-trace-template-sql-server-profiler.md)  
  
-   [Import a Trace Template &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/import-a-trace-template-sql-server-profiler.md)  
  
### To use SQL Server Profiler traces to collect and monitor server performance  
  
-   [Find a Value or Data Column While Tracing &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/find-a-value-or-data-column-while-tracing-sql-server-profiler.md)  
  
-   [Save Deadlock Graphs &#40;SQL Server Profiler&#41;](../../relational-databases/performance/save-deadlock-graphs-sql-server-profiler.md)  
  
-   [Save Showplan XML Events Separately &#40;SQL Server Profiler&#41;](../../relational-databases/performance/save-showplan-xml-events-separately-sql-server-profiler.md)  
  
-   [Save Showplan XML Statistics Profile Events Separately &#40;SQL Server Profiler&#41;](../../relational-databases/performance/save-showplan-xml-statistics-profile-events-separately-sql-server-profiler.md)  
  
-   [Extract a Script from a Trace &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/extract-a-script-from-a-trace-sql-server-profiler.md)  
  
-   [Correlate a Trace with Windows Performance Log Data &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/correlate-a-trace-with-windows-performance-log-data-sql-server-profiler.md)  
  
  
