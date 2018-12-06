---
title: "SQL Server Profiler - Replay Configuration (Advanced Replay Options) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
f1_keywords: 
  - "sql12.pro.replay.generaloptions.advanced.f1"
helpviewer_keywords: 
  - "Replay Configuration dialog box"
ms.assetid: b4eb34f7-3af6-4a44-ba7e-2b8430ec3cd7
author: mashamsft
ms.author: mathoma
manager: craigg
---
# SQL Server Profiler - Replay Configuration (Advanced Replay Options)
  In the **Replay Configuration** dialog box, use the **Advanced Replay Options** tab to specify how to replay a trace file.  
  
 To view this window, use [!INCLUDE[ssSqlProfiler](../includes/sssqlprofiler-md.md)] to open a trace file or table that contains the appropriate events for replay. For more information, see [Replay Requirements](../tools/sql-server-profiler/replay-requirements.md). While the trace file or table is open, on the **Replay** menu, click **Start**, connect to the instance of SQL Server where you want to replay the trace, and then click the **Advanced Replay Options** tab.  
  
## Options  
 **Replay system SPIDs**  
 Specifies whether [!INCLUDE[ssSqlProfiler](../includes/sssqlprofiler-md.md)] replays system process identifiers (SPIDs).  
  
 **Replay one SPID only**  
 Replays only the activity in the source trace file that is related to the selected SPID.  
  
 **SPID to replay**  
 Specify which SPID to replay.  
  
 **Limit replay by date and time**  
 Check to replay only a portion of the source trace file.  
  
 **Start time**  
 Date and time in the source trace file where the replay should start.  
  
 **End time**  
 Date and time in the source trace file where the replay should stop.  
  
 **Health monitor wait interval (sec)**  
 Specify the wait interval to replay in seconds. Default is 3600 seconds (1 hour). This setting affects the amount of time a process is allowed to run before being terminated by the health monitor.  
  
 **Health monitor poll interval (sec)**  
 Specify the health monitor poll interval during replay in seconds. Default is 60 seconds. This value allows the user to configure how often the health monitor polls for candidates for termination.  
  
 **Enable SQL Server blocked processes monitor**  
 Enables a process that searches for blocked or blocking processes.  
  
 **Blocked processes monitor wait interval (sec)**  
 Configures how often the blocked processes monitor searches for blocked or blocking processes.  
  
## See Also  
 [Replay a Trace Table &#40;SQL Server Profiler&#41;](../tools/sql-server-profiler/replay-a-trace-table-sql-server-profiler.md)   
 [Replay a Trace File &#40;SQL Server Profiler&#41;](../tools/sql-server-profiler/replay-a-trace-file-sql-server-profiler.md)   
 [Replay Traces](../tools/sql-server-profiler/replay-traces.md)  
  
  
