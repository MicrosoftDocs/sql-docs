---
title: Replay to a Breakpoint
titleSuffix: SQL Server Profiler
description: Make debugging easier by setting breakpoints so replays pause at specific events. Use SQL Server Profiler to set breakpoints in a trace file or table.
author: cwebb-msft
ms.author: chweb
ms.date: 06/05/2025
ms.service: sql
ms.subservice: profiler
ms.topic: how-to
ms.collection:
  - data-tools
---

# Replay to a breakpoint (SQL Server Profiler)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

This article describes how to set breakpoints in a trace file or table that you want to replay by using [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)]. Setting breakpoints in a trace file or table before you start to replay the trace, enables you to pause replay of the trace at specific events. Using breakpoints while replaying a trace supports debugging because you can break the replay of long trace scripts into short segments that can be analyzed incrementally.

## Replay to a breakpoint

1. Open the trace file or trace table you want to replay. For more information, see [Open a trace file (SQL Server Profiler)](open-a-trace-file-sql-server-profiler.md) or [Open a trace table (SQL Server Profiler)](open-a-trace-table-sql-server-profiler.md).

   Make sure that the trace file or table you open contains the event classes necessary for replay. For more information, see [Replay Requirements](replay-requirements.md).

1. In the trace window, select an event that you want to use as a breakpoint. Use one of the following three methods to set a breakpoint:

   - Press F9.
   - On the **Replay** menu, select **Toggle Breakpoint**.
   - Right-click the event, and then select **Toggle Breakpoint**.

   A red bullet appears next to the selected trace event, indicating that it's the trace breakpoint.

   Repeat this step to set several breakpoints.

1. On the **Replay** menu, select **Start**, and connect to the server where you want to replay the trace.

1. In the **Replay Configuration** dialog box, verify the settings, and then select **OK**.

   The replay starts, pausing when the breakpoint is reached.

1. Press F5 to resume the replay and proceed to the next breakpoint.

1. Repeat Step 5 through the end of the trace.

## Related content

- [Replay to a cursor (SQL Server Profiler)](replay-to-a-cursor-sql-server-profiler.md)
- [Replay Traces](replay-traces.md)
- [SQL Server Profiler](sql-server-profiler.md)
