---
title: Extract a Script from a Trace
titleSuffix: SQL Server Profiler
description: Learn how to extract Transact-SQL events from a trace file or table in SQL Server Profiler and save them as a Transact-SQL script file.
author: cwebb-msft
ms.author: chweb
ms.date: 06/05/2025
ms.service: sql
ms.subservice: profiler
ms.topic: how-to
ms.collection:
  - data-tools
---

# Extract a script from a trace (SQL Server Profiler)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

This article describes how to extract [!INCLUDE [tsql](../../includes/tsql-md.md)] events from a trace file or table and save them as a [!INCLUDE [tsql](../../includes/tsql-md.md)] script file by using [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)].

## Extract a Transact-SQL script from a trace file or table

1. Open a trace file or table that contains [!INCLUDE [tsql](../../includes/tsql-md.md)] events that you want to save to a [!INCLUDE [tsql](../../includes/tsql-md.md)] script file. For more information, see [Open a trace file (SQL Server Profiler)](open-a-trace-file-sql-server-profiler.md) or [Open a trace table (SQL Server Profiler)](open-a-trace-table-sql-server-profiler.md).

1. On the **File** menu, point to **Export**, point to **Extract SQL Server Events**, and then select **Extract Transact-SQL Events**.

1. In the **Save As** dialog box, type a name for the [!INCLUDE [tsql](../../includes/tsql-md.md)] file, and select **Save**.

## Related content

- [SQL Server Profiler](sql-server-profiler.md)
