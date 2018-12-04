---
title: "Replay a Single Event at a Time (SQL Server Profiler) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: profiler
ms.topic: conceptual
helpviewer_keywords: 
  - "events [SQL Server], replaying single event at a time"
  - "traces [SQL Server], replaying"
  - "replaying traces"
ms.assetid: 220fb192-9636-41a2-b15c-62af6cab8fff
author: stevestein
ms.author: sstein
manager: craigg
---
# Replay a Single Event at a Time (SQL Server Profiler)
  This topic describes how to replay one event at a time in a replay trace file or table by using [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)].  
  
### To replay a single event at a time  
  
1.  Open the trace file or trace table you want to replay. For more information, see [Open a Trace File &#40;SQL Server Profiler&#41;](open-a-trace-file-sql-server-profiler.md) or [Open a Trace Table &#40;SQL Server Profiler&#41;](open-a-trace-table-sql-server-profiler.md).  
  
     Make sure that the trace file or table you open contains the event classes necessary for replay. For more information, see [Replay Requirements](replay-requirements.md).  
  
2.  On the **Replay** menu, click **Step**, and connect to the server instance where you want to replay the trace.  
  
3.  In the **Replay Configuration** dialog box, verify the settings, and then click **OK**. For more information about specifying settings on the **Replay Configuration** dialog box, see [Replay a Trace File &#40;SQL Server Profiler&#41;](replay-a-trace-file-sql-server-profiler.md) or [Replay a Trace Table &#40;SQL Server Profiler&#41;](replay-a-trace-table-sql-server-profiler.md).  
  
4.  To replay the first event, click **OK** in the **Replay Configuration** dialog box.  
  
5.  To replay subsequent events, on the **Replay** menu, click **Step**, or press F10. Repeat clicking **Step** or pressing F10 for each event.  
  
## See Also  
 [Replay Traces](replay-traces.md)   
 [SQL Server Profiler](sql-server-profiler.md)  
  
  
