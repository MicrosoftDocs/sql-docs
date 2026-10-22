---
title: Create a Trace
titleSuffix: SQL Server Profiler
description: Learn how to capture event data in SQL Server Profiler by creating a trace. Read about the various options you can specify for traces.
author: cwebb-msft
ms.author: chweb
ms.date: 06/05/2025
ms.service: sql
ms.subservice: profiler
ms.topic: how-to
ms.collection:
  - data-tools
---

# Create a trace (SQL Server Profiler)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

This article describes how to use [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] to create a trace.

## Create a trace

1. On the **File** menu, select **New Trace**, and connect to an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

   The **Trace Properties** dialog box appears.

   if **Start tracing immediately after making connection** is selected, the **Trace Properties** dialog box fails to appear, and the trace begins instead. To turn off this setting, navigate to **Tools** > **Options**, and clear the **Start tracing immediately after making connection** check box.

1. In the **Trace name** box, type a name for the trace.

1. In the **Use the template** list, select a trace template on which to base the trace, or select **Blank** if you don't want to use a template.

## Save a trace

1. To save the trace results, do one of the following steps:

   - Select **Save to file** to capture the trace to a file. Specify a value for **Set maximum file size**. The default value is 5 megabytes (MB).

     Optionally, select **Enable file rollover** to automatically create new files when the maximum file size is reached. You can also optionally select **Server processes trace data**, which causes the service that is running the trace to process trace data instead of the client application. When the server processes trace data, no events are skipped even under stress conditions, but server performance might be affected.

   - Select **Save to table** to capture the trace to a database table.

     Optionally, select **Set maximum rows**, and specify a value.

   > [!CAUTION]  
   > When you don't save the trace results to a file or table, you can view the trace while [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] is open. However, you lose the trace results after you stop the trace and close [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)]. To avoid losing the trace results in this way, select **Save** on the **File** menu to save the results before you close [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)].

1. Optionally, select the **Enable trace stop time** check box, and specify a stop date and time.

1. To add or remove events, data columns, or filters, select the **Events Selection** tab. For more information, see: [Specify events and data columns for a trace file (SQL Server Profiler)](specify-events-and-data-columns-for-a-trace-file-sql-server-profiler.md)

1. Select **Run** to start the trace.

## Related content

- [Permissions required to run SQL Server Profiler](permissions-required-to-run-sql-server-profiler.md)
- [SQL Server Profiler templates and permissions](sql-server-profiler-templates-and-permissions.md)
- [SQL Server Profiler](sql-server-profiler.md)
- [Correlate a trace with Windows performance log data](correlate-a-trace-with-windows-performance-log-data.md)
