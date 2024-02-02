---
title: "SQL Server Profiler stored procedures (Transact-SQL)"
description: "SQL Server Profiler stored procedures (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/24/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
helpviewer_keywords:
  - "system stored procedures [SQL Server], SQL Server Profiler"
  - "Profiler [SQL Server Profiler], stored procedures"
  - "SQL Server Profiler, stored procedures"
  - "monitoring performance [SQL Server], stored procedures"
  - "performance [SQL Server], stored procedures"
dev_langs:
  - "TSQL"
---
# SQL Server Profiler stored procedures (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] supports the following system stored procedures that are used by [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] to monitor performance and activity.

:::row:::
    :::column:::
        [sp_trace_create](sp-trace-create-transact-sql.md)

        [sp_trace_generateevent](sp-trace-generateevent-transact-sql.md)

        [sp_trace_setevent](sp-trace-setevent-transact-sql.md)
    :::column-end:::
    :::column:::
        [sp_trace_setfilter](sp-trace-setfilter-transact-sql.md)

        [sp_trace_setstatus](sp-trace-setstatus-transact-sql.md)
    :::column-end:::
:::row-end:::

For an example of using trace stored procedures, see [Create a Trace (Transact-SQL)](../sql-trace/create-a-trace-transact-sql.md).

## Related content

- [SQL Server Event Class Reference](../event-classes/sql-server-event-class-reference.md)
- [SQL Trace](../sql-trace/sql-trace.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
