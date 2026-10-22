---
title: Pause a Trace
titleSuffix: SQL Server Profiler
description: Find out how to pause a trace so that SQL Server Profiler stops capturing event data, and see which properties you can change while the trace is paused.
author: cwebb-msft
ms.author: chweb
ms.date: 06/05/2025
ms.service: sql
ms.subservice: profiler
ms.topic: how-to
ms.collection:
  - data-tools
---

# Pause a trace (SQL Server Profiler)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

Pausing a trace prevents further event data from being captured until the trace is restarted.

When you pause a trace, you prevent event data from being captured until the trace is restarted. Restarting a trace lets trace operations resume. No previously captured data is lost after a restart. When the trace is restarted, data capturing resumes from that point onward. While a trace is paused, you can change the name, events, columns, and filters. However, you can't change the destinations to which you're sending the trace data, nor change the server connection.

## Pause a trace

1. Select a window that contains a running trace.

1. On the **File** menu, select **Pause Trace**.

## Related content

- [SQL Server Profiler](sql-server-profiler.md)
