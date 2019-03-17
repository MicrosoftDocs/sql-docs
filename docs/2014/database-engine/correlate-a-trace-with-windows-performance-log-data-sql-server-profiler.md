---
title: "Correlate a Trace with Windows Performance Log Data (SQL Server Profiler) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "traces [SQL Server], logs"
ms.assetid: e1b3072c-8daf-49a7-9895-c8cccd2adb95
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Correlate a Trace with Windows Performance Log Data (SQL Server Profiler)
  [!INCLUDE[ssSqlProfiler](../includes/sssqlprofiler-md.md)] can correlate Microsoft Windows System Monitor counters with [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] or [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] events. Windows System Monitor logs system activity for specified counters in performance logs.  
  
> [!NOTE]  
>  For information about sharing logs among different versions of Windows, see the procedure at the end of this topic.  
  
### To correlate a trace with performance log data  
  
1.  In [!INCLUDE[ssSqlProfiler](../includes/sssqlprofiler-md.md)], open a saved trace file or trace table. You cannot correlate a running trace that is still collecting event data. For accurate correlation with System Monitor data, the trace must contain both **StartTime** and **EndTime** data columns.  
  
2.  On the [!INCLUDE[ssSqlProfiler](../includes/sssqlprofiler-md.md)] **File** menu, click **Import Performance Data**.  
  
3.  In the **Open** dialog box, select a file that contains a performance log. The performance log data must have been captured during the same time period in which the trace data is captured.  
  
4.  In the **Performance Counters Limit** dialog box, select the check boxes that correspond to the System Monitor objects and counters that you want to display alongside the trace. Click **OK.**  
  
5.  Select an event in the trace events window, or navigate through several adjacent rows in the trace events window by using the arrow keys. The vertical red bar in the **System Monitor data** window indicates the performance log data that is correlated with the selected trace event.  
  
6.  Click a point of interest in the System Monitor graph. The corresponding trace row that is nearest in time is selected. To zoom in on a time range, press and drag the mouse pointer in the System Monitor graph.  
  
### To create performance logs that can be shared among different versions of Windows  
  
1.  In Control Panel, open **Administrative Tools**, and then double click **Performance**.  
  
2.  In the **Performance** dialog box, expand **Performance Logs and Alerts**, right-click **Counter Logs**, and then click **New Log Settings**.  
  
3.  Type a name for the counter log, and then click **OK**.  
  
4.  On the **General** tab, click **Add Counters**.  
  
5.  In the **Performance object** list, select a performance object you want to monitor. The names of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] performance objects for default instances of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] start with [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] and named instances start with MSSQL$*instanceName*.  
  
6.  Add as many counters as necessary for your [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance and other important values, such as processor time and disk time.  
  
7.  When you have finished adding counters, click **Close**.  
  
8.  Set values for the **Sample data every** interval. Start with a modest sampling interval, such as 5 minutes, and then adjust the interval if necessary.  
  
9. On the **Log Files** tab, choose **TextFile (Comma delimited)** from the **Log file type** list. Comma-delimited text log files can be shared among different versions of Windows and can be viewed later in reporting tools such as Microsoft Excel.  
  
10. On the **Schedule** tab, specify a monitoring schedule.  
  
11. Click **OK** to create the performance log.  
  
## See Also  
 [SQL Server Profiler Templates and Permissions](../tools/sql-server-profiler/sql-server-profiler-templates-and-permissions.md)   
 [Start SQL Server Profiler](../tools/sql-server-profiler/start-sql-server-profiler.md)  
  
  
