---
title: Replay Traces
titleSuffix: SQL Server Profiler
description: See how to replay SQL Server Profiler data from a single computer and how to use breakpoints and simulated user connections in replay to troubleshoot problems.
ms.service: sql
ms.reviewer: ""
ms.subservice: profiler
ms.topic: conceptual
ms.assetid: da958d3c-7f3e-44c9-aecc-8a9493bea7c0
author: markingmyname
ms.author: maghan
ms.custom: seo-lt-2019
ms.date: 03/14/2017
---

# Replay Traces

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Replay is the ability to reproduce activity that has been captured in a trace. When you create or edit a trace, you can save the trace to a file and replay it later. You can use [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] to replay trace activity from a single computer. For large workloads, use the Distributed Replay Utility to replay trace data from multiple computers.  
  
 This section describes how to use the replay features of [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)]. For more information about the Distributed Replay Utility, see [SQL Server Distributed Replay](../../tools/distributed-replay/sql-server-distributed-replay.md).  
  
 [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] features a multithreaded playback engine that can simulate user connections and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. Replay is useful to troubleshoot an application or process problem. When you have identified the problem and implemented corrections, run the trace that found the potential problem against the corrected application or process. Then, replay the original trace and compare results.  
  
 Trace replay supports debugging by using the **Toggle Breakpoint** and the **Run to Cursor** options on the [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] **Replay** menu. These options especially improve the analysis of long scripts because they can break the replay of the trace into short segments so they can be analyzed incrementally.  
  
 For information about the permissions required to replay traces, see [Permissions Required to Run SQL Server Profiler](../../tools/sql-server-profiler/permissions-required-to-run-sql-server-profiler.md).  
  
## In This Section  
  
|Topic|Description|  
|-----------|-----------------|  
|[Replay Requirements](../../tools/sql-server-profiler/replay-requirements.md)|Describes the events that must be included in a trace definition so that it can be replayed with [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)].|  
|[Replay Options &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/replay-options-sql-server-profiler.md)|Describes the options you can set in the **Replay Configuration** dialog box of [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)].|  
|[Considerations for Replaying Traces &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/considerations-for-replaying-traces-sql-server-profiler.md)|Describes trace events that can not be replayed with [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] and the effects on server performance of replaying traces.|  
  
## See Also  
 [SQL Server Distributed Replay](../../tools/distributed-replay/sql-server-distributed-replay.md)  
  
  
