---
title: Derive a Template from a Trace File or Trace Table
titleSuffix: SQL Server Profiler
description: Discover how to use SQL Server Profiler to create a trace template from an existing trace file or from a trace table stored in a database.
author: cwebb-msft
ms.author: chweb
ms.date: 06/05/2025
ms.service: sql
ms.subservice: profiler
ms.topic: how-to
ms.collection:
  - data-tools
---

# Derive a template from a trace file or trace table (SQL Server Profiler)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

This article describes how to create a trace template from an existing trace file or table by using [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)].

## Derive a template from a trace file or trace table

1. Open the trace file or trace table on which you want to base your template. For more information, see [Open a trace file (SQL Server Profiler)](open-a-trace-file-sql-server-profiler.md) or [Open a trace table (SQL Server Profiler)](open-a-trace-table-sql-server-profiler.md).

1. On the **File** menu, point to **Save As**, and then select **Trace Template**.

1. Type a name or select one from the list. Select **OK**.

If you select an existing template file, you're asked if you want to overwrite the file.

## Related content

- [Create a trace template (SQL Server Profiler)](create-a-trace-template-sql-server-profiler.md)
- [Modify trace templates](modify-trace-templates.md)
- [Derive a template from a running trace (SQL Server Profiler)](derive-a-template-from-a-running-trace-sql-server-profiler.md)
- [SQL Server Profiler](sql-server-profiler.md)
