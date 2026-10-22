---
title: Specify Events and Data Columns for a Trace File
titleSuffix: SQL Server Profiler
description: Find out how to specify which event classes and data columns SQL Server Profiler includes when it captures event data in traces.
author: cwebb-msft
ms.author: chweb
ms.date: 06/05/2025
ms.service: sql
ms.subservice: profiler
ms.topic: how-to
ms.collection:
  - data-tools
---

# Specify events and data columns for a trace file (SQL Server Profiler)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

This article describes how to specify event classes and data columns for traces by using [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)].

## Specify events and data columns for a trace

1. On the **Trace Properties** or **Trace Template Properties** dialog box, select the **Events Selection** tab.

   The **Events Selection** tab contains a grid control. The grid control is a table that contains each of the traceable event classes. The table contains one row for each event class. The event classes can differ slightly depending on the type and version of server to which you're connected. The event classes are identified in the **Events** column of the grid and are grouped by event category. The remaining columns list the data columns that can be returned for each event class.

1. On the **Events Selection** tab, use the grid control to add or remove events and data columns from the trace file.

1. To remove events from the trace, clear the check box in the **Events** column for each event class.

1. To include events in a trace, check the box in the **Events** column for each event class, or check a data column that corresponds to an event.

> [!IMPORTANT]  
> If the trace is going be correlated with System Monitor or Performance Monitor data, both **Start Time** and **End Time** data columns must be included in the trace.

When you include an event class, every associated data column is also included to the trace, if you have checked the box corresponding to an event. If you checked the box for a particular column, only that column is included in the trace.

1. To remove data columns from an event class, clear the check boxes from the data column in the event class row, or right-click on the column header and select the **Deselect column** option.

1. Optionally, apply filters to the trace. For more information, see [Filter events in a trace (SQL Server Profiler)](filter-events-in-a-trace-sql-server-profiler.md)

## Related content

- [SQL Server Profiler](sql-server-profiler.md)
