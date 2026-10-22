---
title: Start a Trace
titleSuffix: SQL Server Profiler
description: Learn how to start a trace and locate its data after you have defined a new trace or created a template in SQL Server Profiler.
author: cwebb-msft
ms.author: chweb
ms.date: 06/05/2025
ms.service: sql
ms.subservice: profiler
ms.topic: concept-article
ms.collection:
  - data-tools
---

# Start a trace (SQL Server Profiler)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

After you have defined a new trace or created a template by using [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)], you can start, pause, or stop capturing data by using the new trace definition or template.

## Start a Profiler trace

When you start a trace and the defined source is an instance of the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] or [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)], [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] creates a queue that provides a temporary holding place for captured server events.

When you use [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] to access SQL Trace, a new trace window opens (if one isn't already open) when a trace is started, and data is immediately captured.

When you use [!INCLUDE [tsql](../../includes/tsql-md.md)] system stored procedures to access SQL Trace, you must start a trace every time an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] starts for data to be captured. When a trace has been started, you can modify only the name of the trace.

When working with existing traces, you can view the properties, but you can't modify the properties. To modify the properties, stop or pause the trace.

## Related content

- [Start a trace automatically after connecting to a server (SQL Server Profiler)](start-a-trace-automatically-after-connecting-to-a-server-sql-server-profiler.md)
- [Pause a trace (SQL Server Profiler)](pause-a-trace-sql-server-profiler.md)
- [Stop a trace (SQL Server Profiler)](stop-a-trace-sql-server-profiler.md)
- [Clear a trace window (SQL Server Profiler)](clear-a-trace-window-sql-server-profiler.md)
- [Run a trace after it has been paused or stopped (SQL Server Profiler)](run-a-trace-after-it-has-been-paused-or-stopped-sql-server-profiler.md)
