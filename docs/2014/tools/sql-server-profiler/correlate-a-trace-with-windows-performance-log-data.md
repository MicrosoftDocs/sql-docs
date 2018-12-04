---
title: "Correlate a Trace with Windows Performance Log Data | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: profiler
ms.topic: conceptual
helpviewer_keywords: 
  - "correlating trace with log data"
  - "logs [SQL Server], traces"
  - "Profiler [SQL Server Profiler], correlating trace with log data"
  - "traces [SQL Server], logs"
  - "SQL Server Profiler, correlating trace with log data"
ms.assetid: 1e4412c8-d27c-4aae-9b35-214128d1d00a
author: stevestein
ms.author: sstein
manager: craigg
---
# Correlate a Trace with Windows Performance Log Data
  Using [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)], you can open a Microsoft Windows performance log, choose the counters you want to correlate with a trace, and display the selected performance counters alongside the trace in the [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] graphical user interface. When you select an event in the trace window, a vertical red bar in the System Monitor data window pane of [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] indicates the performance log data that correlates with the selected trace event.  
  
 To correlate a trace with performance counters, open a trace file or table that contains the **StartTime** and **EndTime** data columns, and then click **Import Performance Data** on the [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] **File** menu. You can then open a performance log, and select the System Monitor objects and counters that you want to correlate with the trace.  
  
## See Also  
 [Correlate a Trace with Windows Performance Log Data &#40;SQL Server Profiler&#41;](correlate-a-trace-with-windows-performance-log-data.md)  
  
  
