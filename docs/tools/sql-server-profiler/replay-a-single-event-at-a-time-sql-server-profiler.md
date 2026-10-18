---
title: Replay a Single Event at a Time
titleSuffix: (SQL Server Profiler
description: Discover how to replay one trace event at a time in SQL Server Profiler by stepping through a replay trace file or table.
author: cwebb-msft
ms.author: chweb
ms.date: 06/05/2025
ms.service: sql
ms.subservice: profiler
ms.topic: how-to
ms.collection:
  - data-tools
---

# Replay a single event at a time (SQL Server Profiler)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

This article describes how to replay one event at a time in a replay trace file or table by using [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)].

## Replay a single event at a time

1. Open the trace file or trace table you want to replay. For more information, see [Open a trace file (SQL Server Profiler)](open-a-trace-file-sql-server-profiler.md) or [Open a trace table (SQL Server Profiler)](open-a-trace-table-sql-server-profiler.md).

   Make sure that the trace file or table you open contains the event classes necessary for replay. For more information, see [Replay Requirements](replay-requirements.md).

1. On the **Replay** menu, select **Step**, and connect to the server instance where you want to replay the trace.

1. In the **Replay Configuration** dialog box, verify the settings, and then select **OK**. For more information about specifying settings on the **Replay Configuration** dialog box, see [Replay a trace file (SQL Server Profiler)](replay-a-trace-file-sql-server-profiler.md) or [Replay a trace table (SQL Server Profiler)](replay-a-trace-table-sql-server-profiler.md).

1. To replay the first event, select **OK** in the **Replay Configuration** dialog box.

1. To replay subsequent events, on the **Replay** menu, select **Step**, or press F10. Repeat selecting **Step** or pressing F10 for each event.

## Related content

- [Replay Traces](replay-traces.md)
- [SQL Server Profiler](sql-server-profiler.md)
