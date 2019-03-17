---
title: "Performance Monitoring and Tuning Tools | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "tools [SQL Server], monitoring performance"
  - "monitoring server performance [SQL Server], tools"
  - "monitoring performance [SQL Server], tools"
  - "database performance [SQL Server], tools"
  - "tuning databases [SQL Server], tools"
  - "database monitoring [SQL Server], tools"
  - "performance [SQL Server], monitoring tools"
  - "server performance [SQL Server], tools"
ms.assetid: 31529dfe-68e7-49f7-b3c2-39fcecf33a95
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Performance Monitoring and Tuning Tools
  [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides a comprehensive set of tools for monitoring events in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and for tuning the physical database design. The choice of tool depends on the type of monitoring or tuning to be done and the particular events to be monitored.  
  
 Following are the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] monitoring and tuning tools:  
  
|Tool|Description|  
|----------|-----------------|  
|[sp_trace_setfilter &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-trace-setfilter-transact-sql)|[!INCLUDE[ssSqlProfiler](../../../includes/sssqlprofiler-md.md)] tracks engine process events, such as the start of a batch or a transaction, enabling you to monitor server and database activity (for example, deadlocks, fatal errors, or login activity). You can capture [!INCLUDE[ssSqlProfiler](../../../includes/sssqlprofiler-md.md)] data to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table or a file for later analysis, and you can also replay the events captured on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] step by step, to see exactly what happened.|  
|[SQL Server Distributed Replay](../../tools/distributed-replay/sql-server-distributed-replay.md)|[!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Distributed Replay can use multiple computers to replay trace data, simulating a mission-critical workload.|  
|[Monitor Resource Usage &#40;System Monitor&#41;](../performance-monitor/monitor-resource-usage-system-monitor.md)|System Monitor primarily tracks resource usage, such as the number of buffer manager page requests in use, enabling you to monitor server performance and activity using predefined objects and counters or user-defined counters to monitor events. System Monitor (Performance Monitor in Microsoft Windows NT 4.0) collects counts and rates rather than data about the events (for example, memory usage, number of active transactions, number of blocked locks, or CPU activity). You can set thresholds on specific counters to generate alerts that notify operators.<br /><br /> System Monitor works on Microsoft Windows Server and Windows operating systems. It can monitor (remotely or locally) an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on Windows NT 4.0 or later.<br /><br /> The key difference between [!INCLUDE[ssSqlProfiler](../../../includes/sssqlprofiler-md.md)] and System Monitor is that [!INCLUDE[ssSqlProfiler](../../../includes/sssqlprofiler-md.md)] monitors Database Engine events, whereas System Monitor monitors resource usage associated with server processes.|  
|[Open Activity Monitor &#40;SQL Server Management Studio&#41;](../performance-monitor/open-activity-monitor-sql-server-management-studio.md)|The Activity Monitor in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] is useful for ad hoc views of current activity and graphically displays information about:<br /><br /> Processes running on an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br /> Blocked processes.<br /><br /> Locks.<br /><br /> User activity.|  
|[SQL Trace](../sql-trace/sql-trace.md)|[!INCLUDE[tsql](../../../includes/tsql-md.md)] stored procedures that create, filter, and define tracing:<br /><br /> [sp_trace_create &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-trace-create-transact-sql)<br /><br /> [sp_trace_generateevent &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-trace-generateevent-transact-sql)<br /><br /> [sp_trace_setevent &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql)<br /><br /> [sp_trace_setfilter &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-trace-setfilter-transact-sql)<br /><br /> [sp_trace_setstatus &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-trace-setstatus-transact-sql)|  
|Error Logs|The Windows application event log provides an overall picture of events occurring on the Windows Server and Windows operating systems as a whole, as well as events in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent, and full-text search. It contains information about events in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that is not available elsewhere. You can use the information in the error log to troubleshoot [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]-related problems.|  
|[System Stored Procedures &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/system-stored-procedures-transact-sql)|The following [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system stored procedures provide a powerful alternative for many monitoring tasks:<br /><br /> [sp_who &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-who-transact-sql): <br />                    Reports snapshot information about current [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] users and processes, including the currently executing statement and whether the statement is blocked.<br /><br /> [sp_lock &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-lock-transact-sql): Reports snapshot information about locks, including the object ID, index ID, type of lock, and type or resource to which the lock applies.<br /><br /> [sp_spaceused &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-spaceused-transact-sql): Displays an estimate of the current amount of disk space used by a table (or a whole database).<br /><br /> [sp_monitor &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-monitor-transact-sql): Displays statistics, including CPU usage, I/O usage, and the amount of time idle since **sp_monitor** was last executed.|  
|[DBCC &#40;Transact-SQL&#41;](/sql/t-sql/database-console-commands/dbcc-transact-sql)|DBCC (Database Console Command) statements enable you to check performance statistics and the logical and physical consistency of a database.|  
|[Built-in Functions &#40;Transact-SQL&#41;](/sql/t-sql/functions/functions)|Built-in functions display snapshot statistics about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] activity since the server was started; these statistics are stored in predefined [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] counters. For example, **@@CPU_BUSY** contains the amount of time the CPU has been executing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] code; **@@CONNECTIONS** contains the number of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] connections or attempted connections; and **@@PACKET_ERRORS** contains the number of network packets occurring on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] connections.|  
|[Trace Flags &#40;Transact-SQL&#41;](/sql/t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql)|Trace flags display information about a specific activity within the server and are used to diagnose problems or performance issues (for example, deadlock chains).|  
|[Database Engine Tuning Advisor](database-engine-tuning-advisor.md)|Database Engine Tuning Advisor analyzes the performance effects of [!INCLUDE[tsql](../../../includes/tsql-md.md)] statements executed against databases you want to tune. Database Engine Tuning Advisor provides recommendations to add, remove, or modify indexes, indexed views, and partitioning.|  
  
## Choosing a Monitoring Tool  
 The choice of a monitoring tool depends on the event or activity to be monitored.  
  
|Event or activity|SQL Server Profiler|Distributed Replay|System Monitor|Activity Monitor|Transact-SQL|Error logs|  
|-----------------------|-------------------------|------------------------|--------------------|----------------------|-------------------|----------------|  
|Trend analysis|Yes||Yes||||  
|Replaying captured events|Yes (From a single computer)|Yes (From multiple computers)|||||  
|Ad hoc monitoring|Yes|||Yes|Yes|Yes|  
|Generating alerts|||Yes||||  
|Graphical interface|Yes||Yes|Yes||Yes|  
|Using within custom application|Yes <sup>1</sup>||||Yes||  
  
 <sup>1</sup> Using [!INCLUDE[ssSqlProfiler](../../../includes/sssqlprofiler-md.md)] system stored procedures.  
  
## Windows Monitoring Tools  
 Windows operating systems and Windows Server 2003 also provide these monitoring tools.  
  
|Tool|Description|  
|----------|-----------------|  
|Task Manager|Shows a synopsis of the processes and applications running on the system.|  
|Network Monitor Agent|Monitors network traffic.|  
  
 For more information about Windows operating systems or Windows Server tools, see the Windows documentation.  
  
  
