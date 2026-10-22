---
title: Stop a Trace
titleSuffix: SQL Server Profiler
description: Discover how to stop a trace that is running in SQL Server Profiler, change any properties you want to adjust, and save the captured data.
author: cwebb-msft
ms.author: chweb
ms.date: 06/05/2025
ms.service: sql
ms.subservice: profiler
ms.topic: how-to
ms.collection:
  - data-tools
---

# Stop a trace (SQL Server Profiler)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

This article describes how to stop a trace that is running by using [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)].

Stopping a trace stops data from being captured. After a trace is stopped, it can't be restarted without losing previously captured data, unless the data has been captured to a trace file or trace table. You can also save the collected data to a table or file after stopping a trace. All trace properties that were previously selected are preserved when a trace is stopped. When a trace is stopped, you can change the name, events, columns, and filters.

## Stop a trace

1. Select a trace that is running.

1. On the **File** menu, select **Stop Trace**.

## Related content

- [SQL Server Profiler](sql-server-profiler.md)
