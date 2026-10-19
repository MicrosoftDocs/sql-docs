---
title: Export a Trace Template
titleSuffix: SQL Server Profiler
description: Learn how to use SQL Server Profiler to export a trace template as a template file so you can use the template with another instance of SQL Server Profiler.
author: cwebb-msft
ms.author: chweb
ms.date: 06/05/2025
ms.service: sql
ms.subservice: profiler
ms.topic: how-to
ms.collection:
  - data-tools
---

# Export a trace template (SQL Server Profiler)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

This article describes how to export an existing trace template to save as a template file (.tdf) by using [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)]. When you export a trace template and save it as a file, you can use the template with another instance of [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)].

## Export a trace template

1. On the **File** menu, point to **Templates**, and then select **Export Template**.

1. In the **Select Template Name** dialog box, select the template to export in the **Select template name** list, and then select **OK**.

   The **Save As** dialog box appears.

1. Enter a file name for the exported template file, and then select **Save**.

## Related content

- [Create a trace template (SQL Server Profiler)](create-a-trace-template-sql-server-profiler.md)
- [Modify trace templates](modify-trace-templates.md)
- [Derive a template from a running trace (SQL Server Profiler)](derive-a-template-from-a-running-trace-sql-server-profiler.md)
- [Derive a template from a trace file or trace table (SQL Server Profiler)](derive-a-template-from-a-trace-file-or-trace-table-sql-server-profiler.md)
- [SQL Server Profiler](sql-server-profiler.md)
