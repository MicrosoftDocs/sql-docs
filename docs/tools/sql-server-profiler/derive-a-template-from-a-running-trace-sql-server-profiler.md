---
title: Derive a Template from a Running Trace
titleSuffix: SQL Server Profiler
description: Discover how to create a template from an existing trace in SQL Server Profiler while the trace is running.
author: cwebb-msft
ms.author: chweb
ms.date: 06/05/2025
ms.service: sql
ms.subservice: profiler
ms.topic: how-to
ms.collection:
  - data-tools
---

# Derive a template from a running trace (SQL Server Profiler)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

This article describes how to create a trace template from an existing trace while it's running by using [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)].

## Derive a template from a running trace

1. If necessary, switch to the window that contains the trace.

1. On the **File** menu, point to **Save As**, and then select **Trace Template**.

1. Type a name or select one from the list. Select **OK**.

If you select an existing template file, you're asked if you want to overwrite the file. You can only select a user-defined template. Predefined system trace templates can't be overwritten.

## Related content

- [SQL Server Profiler templates and permissions](sql-server-profiler-templates-and-permissions.md)
- [Create a trace template (SQL Server Profiler)](create-a-trace-template-sql-server-profiler.md)
- [Modify trace templates](modify-trace-templates.md)
- [SQL Server Profiler](sql-server-profiler.md)
