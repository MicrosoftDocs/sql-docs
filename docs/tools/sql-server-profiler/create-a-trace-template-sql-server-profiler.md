---
title: Create a Trace Template
titleSuffix: SQL Server Profiler
description: Discover how to create a new trace template in SQL Server Profiler. Learn how to add filters to the template and how to add or remove events and data columns.
author: markingmyname
ms.author: maghan
ms.reviewer: maghan
ms.date: 12/26/2022
ms.service: sql
ms.subservice: profiler
ms.topic: conceptual
---

# Create a Trace Template (SQL Server Profiler)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

This article describes how to create a new trace template by using [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)].

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

1. Select **Save.**

## See also

- [Specify Events and Data Columns for a Trace File (SQL Server Profiler)](../../tools/sql-server-profiler/specify-events-and-data-columns-for-a-trace-file-sql-server-profiler.md)
- [Derive a Template from a Running Trace (SQL Server Profiler)](../../tools/sql-server-profiler/derive-a-template-from-a-running-trace-sql-server-profiler.md)
- [Derive a Template from a Trace File or Trace Table (SQL Server Profiler)](../../tools/sql-server-profiler/derive-a-template-from-a-trace-file-or-trace-table-sql-server-profiler.md)
- [SQL Server Profiler Templates and Permissions](../../tools/sql-server-profiler/sql-server-profiler-templates-and-permissions.md)
- [SQL Server Profiler](../../tools/sql-server-profiler/sql-server-profiler.md)
