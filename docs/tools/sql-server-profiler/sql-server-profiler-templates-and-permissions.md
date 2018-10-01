---
title: "SQL Server Profiler Templates and Permissions | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: profiler
ms.topic: conceptual
helpviewer_keywords: 
  - "Profiler [SQL Server Profiler], about SQL Server Profiler"
  - "SQL Server Profiler, about SQL Server Profiler"
ms.assetid: 6d00378a-5d74-463b-9ed6-a2685306a9d2
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# SQL Server Profiler Templates and Permissions
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] shows how [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] resolves queries internally. This allows administrators to see exactly what [!INCLUDE[tsql](../../includes/tsql-md.md)] statements or Multi-Dimensional Expressions are submitted to the server and how the server accesses the database or cube to return result sets.  
  
 Using [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)], you can do the following:  
  
-   Create a trace that is based on a reusable template  
  
-   Watch the trace results as the trace runs  
  
-   Store the trace results in a table  
  
-   Start, stop, pause, and modify the trace results as necessary  
  
-   Replay the trace results  
  
 Use [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] to monitor only the events in which you are interested. If traces are becoming too large, you can filter them based on the information you want, so that only a subset of the event data is collected. Monitoring too many events adds overhead to the server and the monitoring process, and can cause the trace file or trace table to grow very large, especially when the monitoring process takes place over a long period of time.  
  
> [!NOTE]  
>  Trace column values greater than 1 GB return an error and are truncated in the trace output.  
  
## In This Section  
  
|Topic|Description|  
|-----------|-----------------|  
|[SQL Server Profiler Templates](../../tools/sql-server-profiler/sql-server-profiler-templates.md)|Contains information about the predefined trace templates that ship with [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)].|  
|[Permissions Required to Run SQL Server Profiler](../../tools/sql-server-profiler/permissions-required-to-run-sql-server-profiler.md)|Contains information about the permissions that are required to run [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)].|  
|[Save Traces and Trace Templates](../../tools/sql-server-profiler/save-traces-and-trace-templates.md)|Contains information about saving trace output and about saving trace definitions into a template.|  
|[Modify Trace Templates](../../tools/sql-server-profiler/modify-trace-templates.md)|Contains information about modifying trace templates by using [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] or by using [!INCLUDE[tsql](../../includes/tsql-md.md)].|  
|[Start a Trace](../../tools/sql-server-profiler/start-a-trace.md)|Contains information about what happens when you start, pause, or stop a trace.|  
|[Correlate a Trace with Windows Performance Log Data](../../tools/sql-server-profiler/correlate-a-trace-with-windows-performance-log-data.md)|Contains information about correlating Windows performance log data with a trace by using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Profiler.|  
|[View and Analyze Traces with SQL Server Profiler](../../tools/sql-server-profiler/view-and-analyze-traces-with-sql-server-profiler.md)|Contains information about using traces to troubleshoot data, displaying object names in a trace, and finding events in a trace.|  
|[Analyze Deadlocks with SQL Server Profiler](../../tools/sql-server-profiler/analyze-deadlocks-with-sql-server-profiler.md)|Contains information about using [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] to identify the cause of a deadlock.|  
|[Analyze Queries with SHOWPLAN Results in SQL Server Profiler](../../tools/sql-server-profiler/analyze-queries-with-showplan-results-in-sql-server-profiler.md)|Contains information about using [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] to collect and display Showplan and Showplan Statistics results.|  
|[Filter Traces with SQL Server Profiler](../../tools/sql-server-profiler/filter-traces-with-sql-server-profiler.md)|Contains information about setting filters on data columns to filter trace output by using [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)].|  
|[Replay Traces](../../tools/sql-server-profiler/replay-traces.md)|Contains information that explains what replaying a trace means and what is required to replay a trace.|  
  
## See Also  
 [SQL Server Profiler](../../tools/sql-server-profiler/sql-server-profiler.md)   
 [Start SQL Server Profiler](../../tools/sql-server-profiler/start-sql-server-profiler.md)  
  
  
