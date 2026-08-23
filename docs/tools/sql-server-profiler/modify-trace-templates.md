---
title: Modify Trace Templates
titleSuffix: SQL Server Profiler
description: Discover how to modify trace templates in SQL Server Profiler. Add or remove event classes and data columns, and change filters.
author: rwestMSFT
ms.author: randolphwest
ms.date: 06/05/2025
ms.service: sql
ms.subservice: profiler
ms.topic: how-to
ms.collection:
  - data-tools
---

# Modify trace templates

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

You can modify templates that are saved in a file on the local computer on which [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] is running. You can also modify templates derived from those files. When you modify existing templates, you edit template properties such as event classes and data columns, in the same order that the properties were set originally, on the **Events Selection** tab of the **Trace Properties** dialog box. Event classes and data columns can be added or removed, and filters can be changed. After the template is modified, a user-specific template is created and the original system template is left intact. For more information, see [Save traces and trace templates](save-traces-and-trace-templates.md).

You might need to derive a template from an existing trace file if you can't remember (or haven't saved) the original template that was used to create the trace, or if you want to run the same trace at a later date. When working with existing traces, you can view the properties, but you can't modify the properties. To modify the properties, stop or pause the trace. For more information, see [Derive a template from a trace file or trace table (SQL Server Profiler)](derive-a-template-from-a-trace-file-or-trace-table-sql-server-profiler.md) and [Derive a template from a running trace (SQL Server Profiler)](derive-a-template-from-a-running-trace-sql-server-profiler.md).

## Modify a trace template

1. On the **File** menu, point to **Templates**, and then select **Edit Template**.

1. In the **Trace Template Properties** dialog box, on the **General** tab, you can modify the server type and template name, or choose to use a default template for the server type.

1. On the **Events Selection** tab, use the grid control to add or remove events and data columns from the trace file as follows.

   - To add an event, expand the appropriate event category in the **Events** column, and then select the event name.

   - When you add an event, all relevant data columns are included by default. To remove a data column for an event from a trace, clear the check box in the data column for the event.

   - To add filters, select the data column name and specify the filter criteria in the **Edit Filter** dialog box. You can also right-click the data column name, and select **Edit Column Filter** to launch the **Edit Filter** dialog box. Select **OK** to add the filter.

1. Select **Save**, or select **Save As** to save the trace template under another name.

## Related content

- [Start a trace (SQL Server Profiler)](start-a-trace.md)
- [Create a trace (SQL Server Profiler)](create-a-trace-sql-server-profiler.md)
- [Modify an Existing Trace (Transact-SQL)](../../relational-databases/sql-trace/modify-an-existing-trace-transact-sql.md)
- [Specify events and data columns for a trace file (SQL Server Profiler)](specify-events-and-data-columns-for-a-trace-file-sql-server-profiler.md)
- [sp_trace_setevent (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql.md)
