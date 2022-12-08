---
title: "Monitor SQL Server Components | Microsoft Docs"
description: Learn how monitoring lets you identify performance trends. SQL Server provides a service in a dynamic environment, so changes may be necessary over time.
ms.custom: ""
ms.date: "11/27/2018"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
ms.assetid: e8f1b16b-ea40-4e12-886c-967ebda4e6e4
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# Monitor SQL Server Components
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Monitoring is important because [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides a service in a dynamic environment. The data in the application changes. The type of access that users require changes. The way that users connect changes. The types of applications accessing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] may even change, but [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] automatically manages system-level resources, such as memory and disk space, to minimize the need for extensive system-level manual tuning. Monitoring lets administrators identify performance trends to determine if changes are necessary.  
  
To monitor any component of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] effectively:  
  
1.  Determine your monitoring goals.  
2.  Select the appropriate tool.    
3.  Identify components to monitor.  
4.  Select metrics for those components.  
5.  Monitor the server.  
6.  Analyze the data.  
  
These steps are discussed in turn below.  
  
## Determine Your Monitoring Goals  
To monitor [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] effectively you should clearly identify your reason for monitoring. Reasons can include the following:  
  
-   Establish a baseline for performance.  
-   Identify performance changes over time.  
-   Diagnose specific performance problems.  
-   Identify components or processes to optimize.  
-   Compare the effect of different client applications on performance.  
-   Audit user activity.  
-   Test a server under different loads.  
-   Test database architecture.  
-   Test maintenance schedules.  
-   Test backup and restore plans.  
-   Determining when to modify your hardware configuration.  
  
## Select the Appropriate Tool  
After determining why you are monitoring, you should select the appropriate tools for that type of monitoring. The Windows operating system and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provide a complete set of tools to monitor servers in transaction-intensive environments. These tools clearly reveal the condition of an instance of the SQL Server Database Engine or an instance of SQL Server Analysis Services.  
  
Windows provides the following tools for monitoring applications that are running on a server:  
  
-   [System Monitor](../../relational-databases/performance/start-system-monitor-windows.md), which lets you collect and view real-time data about activities such as memory, disk, and processor usage.  
-   Performance logs and alerts  
-   Task Manager  
  
For more information about Windows Server or Windows tools, see the Windows documentation.  
  
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides the following tools for monitoring components of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:  
  
-   [Extended Events](../../relational-databases/extended-events/extended-events.md)
-   [SQL Trace](../../relational-databases/sql-trace/sql-trace.md)  
-   [SQL Server Profiler](../../tools/sql-server-profiler/sql-server-profiler.md)  
-   [Distributed Replay Utility](../../tools/distributed-replay/sql-server-distributed-replay.md)  
-   [Activity Monitor](../../relational-databases/performance-monitor/activity-monitor.md)  
-   [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] Graphical Showplan  
-   [System Stored procedures](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)
-   [Database Console Commands (DBCC)](../../t-sql/database-console-commands/dbcc-transact-sql.md)  
-   [Dynamic Management Views and Functions](../../relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
-   [Functions](../../t-sql/functions/functions.md)   
-   [Trace flags](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md)   

> [!IMPORTANT]
> SQL Trace and [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] are deprecated. The *Microsoft.SqlServer.Management.Trace* namespace that contains the Microsoft SQL Server Trace and Replay objects are also deprecated. 
> [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] 
> Use Extended Events instead. For more information on [Extended Events](../../relational-databases/extended-events/extended-events.md), see [Quick Start: Extended events in SQL Server](../../relational-databases/extended-events/quick-start-extended-events-in-sql-server.md) and [SSMS XEvent Profiler](../../relational-databases/extended-events/use-the-ssms-xe-profiler.md).

> [!NOTE]
> [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] for Analysis Services workloads is NOT deprecated, and will continue to be supported.

For more information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] monitoring tools, see [Performance Monitoring and Tuning Tools](../../relational-databases/performance/performance-monitoring-and-tuning-tools.md).  
  
## Identify the Components to Monitor  
The third step to monitoring an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is to identify the components that you monitor. For example, if you are using [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] to trace a server you can define the trace to collect data about specific events. You can also exclude events that do not apply to your situation.  
  
## Select Metrics for Monitored Components  
After identifying the components to monitor, determine the metrics for components you monitor. For example, after selecting the events to include in a trace, you can choose to include only specific data about the events. Limiting the trace to data that is relevant to the trace minimizes the system resources required to perform the tracing.  
  
## Monitor the Server  
To monitor the server, run the monitoring tool that you have configured to gather data. For example, after a trace is defined, you can run the trace to gather data about events raised in the server.  

## Analyze the Data  
After the trace has finished, analyze the data to see if you have achieved your monitoring goal. If you have not, modify the components or metrics that you used to monitor the server.  
  
The following outlines the process for capturing event data and putting it to use.  
  
1.  Apply filters to limit the event data collected.  
  
    Limiting the event data allows for the system to focus on the events pertinent to the monitoring scenario. For example, if you want to monitor slow queries, you can use a filter to monitor only those queries issued by the application that take more than 30 seconds to run against a particular database. 
    
    For more information on filtering Extended Event traces, see [Quick Start: Extended events in SQL Server](../../relational-databases/extended-events/quick-start-extended-events-in-sql-server.md#demo-of-ssms-integration). 
    
    For more information on filtering SQL Trace, see [Set a Trace Filter &#40;Transact-SQL&#41;](../../relational-databases/sql-trace/set-a-trace-filter-transact-sql.md) and [Filter Events in a Trace &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/filter-events-in-a-trace-sql-server-profiler.md).  
  
2.  Monitor (capture) events.  
  
    As soon as it is enabled, active monitoring captures data from the specified application, instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], or operating system. For example, when disk activity is monitored using System Monitor, monitoring captures event data, such as disk reads and writes, and displays it on the screen. For more information, see [Monitor Resource Usage &#40;System Monitor&#41;](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md).  
  
3.  Save captured event data.  
  
    Saving captured event data lets you analyze it later. Captured event data that is saved to a file that can be loaded back into the tool that originally created it for analysis. Saving captured event data is important when you are creating a performance baseline. The performance baseline data is saved and used, when comparing recently captured event data, to determine whether performance is optimal.
    
    Extended Events permits event data to be saved to an event file, event counter, histogram, and ring buffer. For more information, see [Targets for Extended Events in SQL Server](../../relational-databases/extended-events/targets-for-extended-events-in-sql-server.md).
    
    SQL Trace event data can even be replayed using the Distributed Replay Utility or [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)]. [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] permits event data to be saved to a file or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table. For more information, see [SQL Server Profiler Templates and Permissions](../../tools/sql-server-profiler/sql-server-profiler-templates-and-permissions.md).  
  
4.  Create trace templates that contain the settings specified to capture the events.  
  
    Trace templates include specifications about the events themselves, event data, and filters that are used to capture data. These templates can be used to monitor a specific set of events later without redefining the events, event data, and filters. For example, if you want to frequently monitor the number of deadlocks, and the users involved in those deadlocks, you can create a template defining those events, event data, and event filters; save the template; and reapply the filter the next time that you want to monitor deadlocks.
    
    An Extended Event session definition is a template that can be scripted and re-used. To create and manage sessions, see [Manage Event Sessions in the Object Explorer](../../relational-databases/extended-events/manage-event-sessions-in-the-object-explorer.md). The [!INCLUDE[ssManStudio](../../includes/ssManStudio-md.md)] XEvent Profiler already provides templates that are ready to use. For more information, see [Use the SSMS XEvent Profiler](../../relational-databases/extended-events/use-the-ssms-xe-profiler.md).
       
    [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] uses trace templates for this purpose. For more information, see [Set Trace Definition Defaults &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/set-trace-definition-defaults-sql-server-profiler.md) and [Create a Trace Template &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/create-a-trace-template-sql-server-profiler.md).  
    
    > [!TIP]
    > A SQL Trace definition can be converted to an Extended Event session. For more information, see [Convert an Existing SQL Trace Script to an Extended Events Session](../../relational-databases/extended-events/convert-an-existing-sql-trace-script-to-an-extended-events-session.md).
  
5.  Analyze captured event data.  
  
     To be analyzed, the captured event data is loaded into the application that captured the data. 
     
     For example, a captured Extended Event trace can be reloaded into [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] for viewing and analysis. For more information, see [Advanced Viewing of Target Data from Extended Events in SQL Server](../../relational-databases/extended-events/advanced-viewing-of-target-data-from-extended-events-in-sql-server.md).

     SQL Trace data can be reloaded into [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] for viewing and analysis. For more information, see [View and Analyze Traces with SQL Server Profiler](../../tools/sql-server-profiler/view-and-analyze-traces-with-sql-server-profiler.md).  
  
     Analyzing event data involves determining what is occurring and why. This information lets you make changes that can improve performance, such as adding more memory, changing indexes, correcting coding problems with [!INCLUDE[tsql](../../includes/tsql-md.md)] statements or stored procedures, and so on, depending on the type of analysis performed. For example, you can use the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor to analyze a captured trace from Extended Events or [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] and make index recommendations based on the results.  
  
6.  Replay captured event data (optional).  
  
     Event replay lets you establish a test copy of the database environment from which the data was captured, and then repeat the captured events as they occurred originally on the real system. This capability is only available with the Distributed Replay Utility or [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)]. You can replay the events at the same speed as they originally occurred, as fast as possible (to stress the system), or more likely, one step at a time (to analyze the system after each event has occurred). By analyzing the exact events in a test environment, you can prevent harm to the production system. For more information, see [Replay Traces](../../tools/sql-server-profiler/replay-traces.md).  
  
  
