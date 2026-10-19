---
title: Replay to a Cursor
titleSuffix: SQL Server Profiler
description: Discover how to use a cursor in SQL Server Profiler to pause a trace replay at a specific point. Make debugging easier by replaying to a cursor.
author: cwebb-msft
ms.author: chweb
ms.date: 06/05/2025
ms.service: sql
ms.subservice: profiler
ms.topic: how-to
ms.collection:
  - data-tools
---

# Replay to a cursor (SQL Server Profiler)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

This article describes how to replay trace files or tables that pause when a cursor is reached by using [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)]. Pausing traces at cursors supports debugging because you can break the replay of long trace scripts into short segments that can be analyzed incrementally.

## Replay to the cursor

1. Open the trace file or trace table you want to replay. For more information, see [Open a trace file (SQL Server Profiler)](open-a-trace-file-sql-server-profiler.md) or [Open a trace table (SQL Server Profiler)](open-a-trace-table-sql-server-profiler.md).

   Make sure that the trace file or table you open contains the event classes necessary for replay. For more information, see [Replay Requirements](replay-requirements.md).

1. In the trace window, select an event.

1. On the **Replay** menu, select **Run to Cursor**, and then connect to the server where you want to replay the trace.

1. In the **Replay Configuration** dialog box, verify the settings, and then select **OK**.

   The replay starts, pausing when the first cursor is reached.

1. Press F5 to resume the trace.

1. Repeat Step 5 through the end of the trace.

## Related content

- [Replay to a breakpoint (SQL Server Profiler)](replay-to-a-breakpoint-sql-server-profiler.md)
- [Replay Traces](replay-traces.md)
- [SQL Server Profiler](sql-server-profiler.md)
