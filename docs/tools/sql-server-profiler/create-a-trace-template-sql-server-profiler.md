---
title: Create a Trace Template
titleSuffix: SQL Server Profiler
description: Discover how to create a new trace template in SQL Server Profiler. Learn how to add filters to the template and how to add or remove events and data columns.
author: cwebb-msft
ms.author: chweb
ms.reviewer: vanto
ms.date: 06/05/2025
ms.service: sql
ms.subservice: profiler
ms.topic: how-to
ms.collection:
  - data-tools
---

# Create a trace template (SQL Server Profiler)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

This article describes how to create a new trace template by using [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)].

## Create a trace template

1. On the **File** menu, point to **Templates**, and then select **New Template**.

1. In the **Trace Template Properties** dialog box, select a server type from the **Select server type** list.

1. In the **New template name** box, enter a template name.

1. Optionally, select **Base new template on existing one**, and then select a template from the list.

   All events, data columns, and filters are initially set as specified in the selected template.

1. Optionally, select **Use as a default template for selected server type**.

1. On the **Events Selection** tab, add, remove, or modify events, data columns, or filters.

1. On the **Events Selection** tab, use the grid control to add or remove events and data columns from the trace file as follows:

   - To add an event, expand the appropriate event category in the **Events** column and select the event name.

   - All relevant data columns are included by default when you add an event. To remove a data column for an event from a trace, clear the check box in the data column for the event.

   - To add filters, select the data column name and specify the filter criteria in the **Edit Filter** dialog box. You can also right-click the data column name and select **Edit Column Filter** to launch the **Edit Filter** dialog box. Select **OK** to add the filter.

1. Select **Save**.

## Related content

- [Specify events and data columns for a trace file (SQL Server Profiler)](specify-events-and-data-columns-for-a-trace-file-sql-server-profiler.md)
- [Derive a template from a running trace (SQL Server Profiler)](derive-a-template-from-a-running-trace-sql-server-profiler.md)
- [Derive a template from a trace file or trace table (SQL Server Profiler)](derive-a-template-from-a-trace-file-or-trace-table-sql-server-profiler.md)
- [SQL Server Profiler templates and permissions](sql-server-profiler-templates-and-permissions.md)
- [SQL Server Profiler](sql-server-profiler.md)
