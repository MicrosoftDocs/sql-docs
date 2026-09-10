---
title: "Performance Monitoring and Tuning Tools"
description: Learn about SQL Server monitoring and tuning tools and how to choose the right one depending on the type of monitoring and the events to monitor.
author: rwestMSFT
ms.author: randolphwest
ms.date: 01/26/2026
ms.service: sql
ms.subservice: performance
ms.topic: concept-article
ai-usage: ai-assisted
helpviewer_keywords:
  - "tools [SQL Server], monitoring performance"
  - "monitoring server performance [SQL Server], tools"
  - "monitoring performance [SQL Server], tools"
  - "database performance [SQL Server], tools"
  - "tuning databases [SQL Server], tools"
  - "database monitoring [SQL Server], tools"
  - "performance [SQL Server], monitoring tools"
  - "server performance [SQL Server], tools"
---
# Performance monitoring and tuning tools

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

[!INCLUDE [msCoName](../../includes/msconame-md.md)] [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] provides a comprehensive set of tools for monitoring events in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and for tuning the physical database design. The choice of tool depends on the type of monitoring or tuning to be done and the particular events to be monitored.

Following are the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] monitoring and tuning tools:

| Tool | Description |
| --- | --- |
| [What are the SQL database functions?](../../t-sql/functions/functions.md) | Built-in functions display snapshot statistics about [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] activity since the server was started; these statistics are stored in predefined [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] counters. For example, `@@CPU_BUSY` contains the amount of time the CPU has been executing [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] code; `@@CONNECTIONS` contains the number of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] connections or attempted connections; and `@@PACKET_ERRORS` contains the number of network packets occurring on [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] connections. |
| [DBCC](../../t-sql/database-console-commands/dbcc-transact-sql.md) | DBCC (Database Console Command) statements enable you to check performance statistics and the logical and physical consistency of a database. |
| [Database Engine Tuning Advisor](database-engine-tuning-advisor.md) | Database Engine Tuning Advisor analyzes the performance effects of [!INCLUDE [tsql](../../includes/tsql-md.md)] statements executed against databases you want to tune. Database Engine Tuning Advisor provides recommendations to add, remove, or modify indexes, indexed views, and partitioning. |
| Error Logs | The Windows Application event log provides an overall picture of events occurring on the Windows Server and Windows operating systems as a whole, as well as events in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent, and full-text search. It contains information about events in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that isn't available elsewhere. You can use the information in the error log to troubleshoot [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]-related problems. |
| [Extended Events overview](../extended-events/extended-events.md) | Extended Events is a lightweight performance monitoring system that uses very few performance resources. Extended Events provides three graphical user interfaces (New Session Wizard, New Session and the XE Profiler) to create, modify, display, and analyze your session data. |
| [Execution Related Dynamic Management Views and Functions](../system-dynamic-management-objects/execution-related-dynamic-management-views-and-functions-transact-sql.md) | Execution related DMVs enable you to check execution related information. |
| [Live Query Statistics](live-query-statistics.md) | Displays real-time statistics about query execution steps. Because this data is available while the query is executing, these execution statistics are extremely useful for debugging query performance issues. |
| [Monitor Resource Usage (Performance Monitor)](../performance-monitor/monitor-resource-usage-system-monitor.md) | System Monitor primarily tracks resource usage, such as the number of buffer manager page requests in use, enabling you to monitor server performance and activity using predefined objects and counters or user-defined counters to monitor events. System Monitor collects counts and rates rather than data about the events (for example, memory usage, number of active transactions, number of blocked locks, or CPU activity). You can set thresholds on specific counters to generate alerts that notify operators.<br /><br />System Monitor works on Windows Server and Windows operating systems. It can monitor (remotely or locally) an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on supported Windows versions.<br /><br />The key difference between [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] and System Monitor is that [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] monitors Database Engine events, whereas System Monitor monitors resource usage associated with server processes. |
| [Open Activity Monitor in SQL Server Management Studio (SSMS)](../performance-monitor/open-activity-monitor-sql-server-management-studio.md) | The Activity Monitor in [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] is useful for ad hoc views of current activity and graphically displays information about:<br /><br />- Processes running on an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]<br />- Blocked processes<br />- Locks<br />- User activity |
| [Performance Dashboard](performance-dashboard.md) | The Performance Dashboard in [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] helps to quickly identify whether there's any current performance bottleneck in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |
| [Upgrade databases using the Query Tuning Assistant](upgrade-dbcompat-using-qta.md) | The Query Tuning Assistant (QTA) feature will guide users through the recommended workflow to keep performance stability during upgrades to newer [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] versions, as documented in the section *Keep performance stability during the upgrade to newer SQL Server* of [Query Store Usage Scenarios](query-store-usage-scenarios.md#CEUpgrade). |
| [Monitor performance by using the Query Store](monitoring-performance-by-using-the-query-store.md) | The Query Store feature provides you with insight on query plan choice and performance. It simplifies performance troubleshooting by helping you quickly find performance differences caused by query plan changes. Query Store automatically captures a history of queries, plans, and runtime statistics, and retains these for your review. It separates data by time windows so you can see database usage patterns and understand when query plan changes happened on the server. |
| [SQL Trace](../sql-trace/sql-trace.md) | [!INCLUDE [tsql](../../includes/tsql-md.md)] stored procedures that create, filter, and define tracing:<br /><br />[sp_trace_create](../system-stored-procedures/sp-trace-create-transact-sql.md)<br />[sp_trace_generateevent](../system-stored-procedures/sp-trace-generateevent-transact-sql.md)<br />[sp_trace_setevent](../system-stored-procedures/sp-trace-setevent-transact-sql.md)<br />[sp_trace_setfilter](../system-stored-procedures/sp-trace-setfilter-transact-sql.md)<br />[sp_trace_setstatus](../system-stored-procedures/sp-trace-setstatus-transact-sql.md) |
| [SQL Server Distributed Replay overview](../../tools/distributed-replay/sql-server-distributed-replay.md) | [!INCLUDE [msCoName](../../includes/msconame-md.md)] [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Distributed Replay can use multiple computers to replay trace data, simulating a mission-critical workload. |
| [sp_trace_setfilter](../system-stored-procedures/sp-trace-setfilter-transact-sql.md) | [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] tracks engine process events, such as the start of a batch or a transaction, enabling you to monitor server and database activity (for example, deadlocks, fatal errors, or login activity). You can capture [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] data to a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] table or a file for later analysis, and you can also replay the events captured on [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] step by step, to see exactly what happened. |
| [System stored procedures](../system-stored-procedures/system-stored-procedures-transact-sql.md) | The following [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] system stored procedures provide a powerful alternative for many monitoring tasks:<br /><br />[sp_who](../system-stored-procedures/sp-who-transact-sql.md):<br />Reports snapshot information about current [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] users and processes, including the currently executing statement and whether the statement is blocked.<br /><br />[sp_lock](../system-stored-procedures/sp-lock-transact-sql.md):<br />Reports snapshot information about locks, including the object ID, index ID, type of lock, and type or resource to which the lock applies.<br /><br />[sp_spaceused](../system-stored-procedures/sp-spaceused-transact-sql.md):<br />Displays an estimate of the current amount of disk space used by a table (or a whole database).<br /><br />[sp_monitor](../system-stored-procedures/sp-monitor-transact-sql.md):<br />Displays statistics, including CPU usage, I/O usage, and the amount of time idle since `sp_monitor` was last executed. |
| [Trace flags](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md) | Trace flags display information about a specific activity within the server and are used to diagnose problems or performance issues (for example, deadlock chains). |

<a id="choosing-a-monitoring-tool"></a>

## Choose a monitoring tool

The choice of a monitoring tool depends on the event or activity to be monitored.

| Event or activity | Extended Events | SQL Server Profiler | Distributed Replay | System Monitor | Activity Monitor | Transact-SQL | Error logs | Performance Dashboard |
| --- | --- | --- | --- | --- | --- | --- | --- | --- |
| Trend analysis | Yes | Yes | | Yes | | | | |
| Replaying captured events | | Yes (From a single computer) | Yes (From multiple computers) | | | | | |
| Ad hoc monitoring | Yes <sup>1</sup> | Yes | | | Yes | Yes | Yes | Yes |
| Generating alerts | | | | Yes | | | | |
| Graphical interface | Yes | Yes | | Yes | Yes | | Yes | Yes |
| Using within custom application | Yes | Yes <sup>2</sup> | | | | Yes | | |

<sup>1</sup> Using [Use the SSMS XEvent Profiler](../extended-events/use-the-ssms-xe-profiler.md)
<sup>2</sup> Using [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] system stored procedures.

## Windows monitoring tools

 Windows operating systems also provide these monitoring tools.  
  
|Tool|Description|  
|----------|-----------------|  
|Task Manager|Shows a synopsis of the processes and applications running on the system.|  
|[Performance monitor](start-system-monitor-windows.md)|Monitors system resources.|  
|[Windows Application event log](view-the-windows-application-log-windows-10.md)|View application events generated by SQL Server and other applications.|
|[Windows Firewall](../../sql-server/install/configure-the-windows-firewall-to-allow-sql-server-access.md) | The Windows Firewall has monitoring capabilities of blocked and allowed traffic. |

> [!CAUTION]  
> Do not use the **Analyze Wait Chain** feature in the Windows Task Manager and Resource Monitor tools for the `sqlservr.exe` process. This feature is not supported for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and might cause runtime exceptions and process dumps.

For more information about Windows operating systems or Windows Server tools, see the Windows documentation.  

## Related content

- [Server Performance and Activity Monitoring](server-performance-and-activity-monitoring.md)
- [Monitor memory usage](../performance-monitor/monitor-memory-usage.md)
