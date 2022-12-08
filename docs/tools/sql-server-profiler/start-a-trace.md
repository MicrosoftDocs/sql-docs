---
title: Start a Trace
titleSuffix: SQL Server Profiler
description: Learn how to start a trace and locate its data after you have defined a new trace or created a template in SQL Server Profiler.
ms.service: sql
ms.reviewer: ""
ms.subservice: profiler
ms.topic: conceptual
ms.assetid: aeeb38eb-229a-4c8b-ad66-57e9ce45fb6a
author: markingmyname
ms.author: maghan
ms.custom: seo-lt-2019
ms.date: 03/14/2017
---

# Start a Trace

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

After you have defined a new trace or created a template by using [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)], you can start, pause, or stop capturing data by using the new trace definition or template.  
  
## Starting a Trace

When you start a trace and the defined source is an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] or [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)],
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] creates a queue that provides a temporary holding place for captured server events.  
  
When you use [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] to access SQL Trace, a new trace window opens (if one is not already open) when a trace is started, and data is immediately captured.  
  
When you use [!INCLUDE[tsql](../../includes/tsql-md.md)] system stored procedures to access SQL Trace, you must start a trace every time an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] starts for data to be captured. When a trace has been started, you can modify only the name of the trace.  
  
> [!NOTE]  
>  When working with existing traces, you can view the properties, but you cannot modify the properties. To modify the properties, stop or pause the trace.  
  
## See Also

[Start a Trace Automatically after Connecting to a Server &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/start-a-trace-automatically-after-connecting-to-a-server-sql-server-profiler.md)   

[Pause a Trace &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/pause-a-trace-sql-server-profiler.md)   

[Stop a Trace &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/stop-a-trace-sql-server-profiler.md)   

[Clear a Trace Window &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/clear-a-trace-window-sql-server-profiler.md)   

[Run a Trace After It Has Been Paused or Stopped &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/run-a-trace-after-it-has-been-paused-or-stopped-sql-server-profiler.md)