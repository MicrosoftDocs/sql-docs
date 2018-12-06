---
title: "Replay to a Breakpoint (SQL Server Profiler) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: profiler
ms.topic: conceptual
helpviewer_keywords: 
  - "breakpoints [SQL Server]"
  - "traces [SQL Server], replaying"
ms.assetid: 3caf751e-df3b-40c7-b5e8-4490ae178e0c
author: stevestein
ms.author: sstein
manager: craigg
---
# Replay to a Breakpoint (SQL Server Profiler)
  This topic describes how to set breakpoints in a trace file or table that you want to replay by using [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)]. Setting breakpoints in a trace file or table before you start to replay the trace, enables you to pause replay of the trace at specific events. Using breakpoints while replaying a trace supports debugging because you can break the replay of long trace scripts into short segments that can be analyzed incrementally.  
  
### To replay to a breakpoint  
  
1.  Open the trace file or trace table you want to replay. For more information, see [Open a Trace File &#40;SQL Server Profiler&#41;](open-a-trace-file-sql-server-profiler.md) or [Open a Trace Table &#40;SQL Server Profiler&#41;](open-a-trace-table-sql-server-profiler.md).  
  
     Make sure that the trace file or table you open contains the event classes necessary for replay. For more information, see [Replay Requirements](replay-requirements.md).  
  
2.  In the trace window, click an event that you want to use as a breakpoint. Use one of the following three methods to set a breakpoint:  
  
    -   Press F9.  
  
    -   On the **Replay** menu, click **Toggle Breakpoint**.  
  
    -   Right-click the event, and then click **Toggle Breakpoint**.  
  
     A red bullet appears next to the selected trace event, indicating that it is the trace breakpoint.  
  
     Repeat this step to set several breakpoints.  
  
3.  On the **Replay** menu, click **Start**, and connect to the server where you want to replay the trace.  
  
4.  In the **Replay Configuration** dialog box, verify the settings, and then click **OK**.  
  
     The replay starts, pausing when the breakpoint is reached.  
  
5.  Press F5 to resume the replay and proceed to the next breakpoint.  
  
6.  Repeat Step 5 through the end of the trace.  
  
## See Also  
 [Replay to a Cursor &#40;SQL Server Profiler&#41;](replay-to-a-cursor-sql-server-profiler.md)   
 [Replay Traces](replay-traces.md)   
 [SQL Server Profiler](sql-server-profiler.md)  
  
  
