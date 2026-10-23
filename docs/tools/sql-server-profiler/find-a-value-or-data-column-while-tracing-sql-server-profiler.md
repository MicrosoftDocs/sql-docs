---
title: Find a Value or Data Column While Tracing
titleSuffix: SQL Server Profiler
description: Discover how to use SQL Server Profiler to search for a specific value or data column in trace output while the trace is running.
author: cwebb-msft
ms.author: chweb
ms.date: 06/05/2025
ms.service: sql
ms.subservice: profiler
ms.topic: how-to
ms.collection:
  - data-tools
---

# Find a value or data column while tracing (SQL Server Profiler)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

This article describes how to search for a specific value or data column in the trace output while the trace is running by using [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)].

## Find a value or data column while running a trace

1. Select any row in the trace results while the trace is running.

1. On the **Edit** menu, select **Find**.

1. In the **Find** dialog box, enter a search value in the **Find what** text box and optionally specify a data column.

1. To find the next occurrence of a value, select **Find Next**.

1. To find the previous occurrence of a value, select **Find Previous**.

   The search starts at the row you selected in Step 1.

## Related content

- [SQL Server Profiler](sql-server-profiler.md)
- [SQL Server Profiler templates and permissions](sql-server-profiler-templates-and-permissions.md)
