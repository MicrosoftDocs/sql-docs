---
title: View Filter Information
titleSuffix: SQL Server Profiler
description: Find out how to view the filters that SQL Server Profiler is currently applying to data columns to limit the events that are traced.
author: cwebb-msft
ms.author: chweb
ms.date: 06/05/2025
ms.service: sql
ms.subservice: profiler
ms.topic: how-to
ms.collection:
  - data-tools
---

# View filter information (SQL Server Profiler)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

This article describes how to view filters on data columns for event classes by using [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)].

## View filter information

1. Open a trace file, trace table, or SQL script, and on the **File** menu, select **Properties**. If you're editing a trace template or creating a new trace, skip this step.

1. On the **Events Selection** tab, right-click the data column name for the filter you want to view, and select **Edit Column Filter**.

1. In the **Edit Filter** dialog box, expand the filter comparison operators to see the assigned value for the specified criterion. Repeat Steps 2 and 3 for all columns for which you want to view filter information.

Comparison operators with assigned values are formatted bold.

## Related content

- [SQL Server Profiler](sql-server-profiler.md)
