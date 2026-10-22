---
title: Replay a Transact-SQL Script
titleSuffix: SQL Server Profiler
description: Discover how to use SQL Server Profiler to replay Transact-SQL scripts so that you can compare different possible solutions to a performance problem.
author: cwebb-msft
ms.author: chweb
ms.date: 06/05/2025
ms.service: sql
ms.subservice: profiler
ms.topic: how-to
ms.collection:
  - data-tools
---

# Replay a Transact-SQL script (SQL Server Profiler)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

When you test possible solutions to a performance problem, use [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] to replay [!INCLUDE [tsql](../../includes/tsql-md.md)] scripts, and compare performance before and after your changes.

## Replay a Transact-SQL script

1. On the **File** menu, point to **Open**, and then select **Script File**.

1. Select the [!INCLUDE [tsql](../../includes/tsql-md.md)] script file you want to open. Make sure that the [!INCLUDE [tsql](../../includes/tsql-md.md)] script contains events necessary for replay. For more information, see [Replay Requirements](replay-requirements.md).

1. On the **Replay** menu, select **Start**, and connect to the server where you want to replay the script.

1. In the **Replay Configuration** dialog box, verify the settings, and then select **OK**.

## Related content

- [Replay Traces](replay-traces.md)
- [SQL Server Profiler](sql-server-profiler.md)
