---
title: Correlate a Trace with Windows Performance Log Data
titleSuffix: SQL Server Profiler
description: Find out how to correlate Windows performance logs with a trace so that you can view data from System Monitor objects and counters in SQL Server Profiler.
ms.service: sql
ms.reviewer: ""
ms.subservice: profiler
ms.topic: conceptual
ms.assetid: 1e4412c8-d27c-4aae-9b35-214128d1d00a
author: markingmyname
ms.author: maghan
ms.custom: seo-lt-2019
ms.date: 07/12/2017
---

# Correlate a trace with Windows Performance Log data

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Using [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)], you can open a Microsoft Windows performance log, choose the counters you want to correlate with a trace, and display the selected performance counters alongside the trace in the [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] graphical user interface. When you select an event in the trace window, a vertical red bar in the System Monitor data window pane of [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] indicates the performance log data that correlates with the selected trace event.  
  
 To correlate a trace with performance counters, open a trace file or table that contains the **StartTime** and **EndTime** data columns, and then click **Import Performance Data** on the [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] **File** menu. You can then open a performance log, and select the System Monitor objects and counters that you want to correlate with the trace.  
  
### To correlate a trace with performance log data  
  
1.  In [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)], open a saved trace file or trace table. You cannot correlate a running trace that is still collecting event data. For accurate correlation with System Monitor data, the trace must contain both **StartTime** and **EndTime** data columns.  
  
2.  On the [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] **File** menu, click **Import Performance Data**.  
  
3.  In the **Open** dialog box, select a file that contains a performance log. The performance log data must have been captured during the same time period in which the trace data is captured.  
  
4.  In the **Performance Counters Limit** dialog box, select the check boxes that correspond to the System Monitor objects and counters that you want to display alongside the trace. Click **OK.**  
  
5.  Select an event in the trace events window, or navigate through several adjacent rows in the trace events window by using the arrow keys. The vertical red bar in the **System Monitor data** window indicates the performance log data that is correlated with the selected trace event.  
  
6.  Click a point of interest in the System Monitor graph. The corresponding trace row that is nearest in time is selected. To zoom in on a time range, press and drag the mouse pointer in the System Monitor graph.  
  
### To create performance logs that can be shared among different versions of Windows  
  
1.  In Control Panel, open **Administrative Tools**, and then double click **Performance**.  
  
2.  In the **Performance** dialog box, expand **Performance Logs and Alerts**, right-click **Counter Logs**, and then click **New Log Settings**.  
  
3.  Type a name for the counter log, and then click **OK**.  
  
4.  On the **General** tab, click **Add Counters**.  
  
5.  In the **Performance object** list, select a performance object you want to monitor. The names of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] performance objects for default instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] start with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and named instances start with MSSQL$*instanceName*.  
  
6.  Add as many counters as necessary for your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance and other important values, such as processor time and disk time.  
  
7.  When you have finished adding counters, click **Close**.  
  
8.  Set values for the **Sample data every** interval. Start with a modest sampling interval, such as 5 minutes, and then adjust the interval if necessary.  
  
9. On the **Log Files** tab, choose **TextFile (Comma delimited)** from the **Log file type** list. Comma-delimited text log files can be shared among different versions of Windows and can be viewed later in reporting tools such as Microsoft Excel.  
  
10. On the **Schedule** tab, specify a monitoring schedule.  
  
11. Click **OK** to create the performance log.  
