---
title: Create a Transact-SQL Script for Running a Trace
titleSuffix: SQL Server Profiler
description: Find out how to create a Transact-SQL script from an existing trace file or table in SQL Server Profiler.
author: cwebb-msft
ms.author: chweb
ms.date: 06/05/2025
ms.service: sql
ms.subservice: profiler
ms.topic: how-to
ms.collection:
  - data-tools
---

# Create a Transact-SQL script for running a trace (SQL Server Profiler)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

This article describes how to create a Transact-SQL script from an existing trace file or table by using [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)].

## Create a Transact-SQL script to run a trace

1. Open a trace file or table. For more information, see [Open a trace file (SQL Server Profiler)](open-a-trace-file-sql-server-profiler.md) or [Open a trace table (SQL Server Profiler)](open-a-trace-table-sql-server-profiler.md).

1. On the **File** menu, point to **Export**, point to **Script Trace Definition**, and then select the version that corresponds to the server you want to trace.

1. In the **Save As** dialog box, enter a name for the script file, and then select **Save**.

## Related content

- [SQL Server Profiler templates and permissions](sql-server-profiler-templates-and-permissions.md)
- [SQL Server Profiler](sql-server-profiler.md)
