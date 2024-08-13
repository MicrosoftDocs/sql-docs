---
title: Filter events in a trace
titleSuffix: SQL Server Profiler
description: Find out how to set a filter to limit the events that SQL Server Profiler captures during a trace. Read about the formats required for certain filters.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/17/2024
ms.service: sql
ms.subservice: profiler
ms.topic: conceptual
---

# Filter events in a trace (SQL Server Profiler)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

Filters limit the events collected in a trace. If a filter isn't set, all events of the selected event classes are returned in the trace output. It's not mandatory to set a filter for a trace. However, a filter minimizes the overhead that is incurred during tracing.

You add filters to trace definitions by using the **Events Selection** tab of the **Trace Properties** or **Trace Template Properties** dialog box.

## Filter events in a trace

1. In the **Trace Properties** or **Trace Template Properties** dialog box, select the **Events Selection** tab.

   The **Events Selection** tab contains a grid control. The grid control is a table that contains each of the traceable event classes. The table contains one row for each event class. The event classes might differ slightly, depending on the type and version of server to which you connect. The event classes are identified in the **Events** column of the grid and are grouped by event category. The remaining columns list the data columns that can be returned for each event class.

1. Select **Column Filters.**

   The **Edit Filter** dialog box appears. The **Edit Filter** dialog box contains a list of comparison operators that you can use to filter events in a trace.

1. To apply a filter, select the comparison operator, and type a value to use for the filter.

1. Select **OK**.

### Considerations

If you set filter conditions on the **StartTime** and **EndTime** data columns of the Events Selection tab, then make sure that:

- The date you enter matches this format: `YYYY/MM/DD HH:mm:sec`.

  -OR-

- **Use regional settings to display date and time values** is checked in the **General Options** dialog box. To view the **General Options** dialog box, on the [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] **Tools** menu, select **Option**.

  -AND-

  - The date you enter is between January 1, 1753 and December 31, 9999.

If tracing events from the **osql** utility or from the **sqlcmd** utility, always append **%** to filters on the **TextData** data column.

The checkbox for **Exclude rows that don't contain values** might filter out rows with the following conditions, while NOT filtering out events where the column itself isn't present:

- `NULL` values
- Empty strings (which technically are values)

## Related content

- [SQL Server Profiler](sql-server-profiler.md)
