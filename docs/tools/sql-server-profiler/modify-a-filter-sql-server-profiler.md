---
title: Modify a filter
titleSuffix: SQL Server Profiler
description: Learn how to modify a trace filter in SQL Server Profiler so you can gather the information you need. Read about how trace filters affect database performance.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/17/2024
ms.service: sql
ms.subservice: profiler
ms.topic: conceptual
---

# Modify a filter (SQL Server Profiler)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

You add filters to trace templates, which contain the trace definitions, to limit the number of events gathered by a trace. Limiting the number of events gathered can reduce the performance effects of tracing. If you set filters for a trace template, and find that the trace isn't gathering the kind of information that you need, you can edit the filter.

## Modify a filter

1. In [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)], open the template for the trace filter that you want to modify. On the **File** menu, select **Templates**, and then choose **Edit Template**.

1. In the **General** tab of the **Trace Template Properties** dialog, select a template from the **Select template name** list.

1. Select the **Events Selection** tab.

   The **Events Selection** tab contains a grid control. The grid control is a table that contains each of the traceable event classes. The table contains one row for each event class. The event classes might differ slightly, depending on the type and version of server to which you connect. The event classes are identified in the **Events** column of the grid and are grouped by event category. The remaining columns list the data columns that can be returned for each event class.

1. Select **Column Filters**.

1. In the **Edit Filter** dialog box, select the value next to the comparison operator that you want to edit, and type the new value or delete a value. You can also add extra filters.

1. Select **OK** and save the template.

## Related content

- [SQL Server Profiler](sql-server-profiler.md)
