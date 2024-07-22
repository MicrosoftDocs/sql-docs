---
title: "Set a trace filter (Transact-SQL)"
description: Learn how to use stored procedures to create a filter that retrieves only the information you need on an event being traced.
author: "MashaMSFT"
ms.author: "mathoma"
ms.reviewer: randolphwest
ms.date: 07/17/2024
ms.service: sql
ms.topic: conceptual
helpviewer_keywords:
  - "filters [SQL Server], traces"
  - "traces [SQL Server], filters"
---
# Set a trace filter (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to use stored procedures to create a filter that retrieves only the information you need on an event being traced.

## Set a trace filter

1. If the trace is already running, execute `sp_trace_setstatus` by specifying `@status = 0` to stop the trace.

1. Execute `sp_trace_setfilter` to configure the type of information to retrieve for the event being traced.

> [!IMPORTANT]  
> Unlike regular stored procedures, parameters of all [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] stored procedures (`sp_trace_*`) are strictly typed and don't support automatic data type conversion. If these parameters aren't called with the correct input parameter data types, as specified in the argument description, the stored procedure will return an error.

## Related content

- [Filter a trace](filter-a-trace.md)
- [sp_trace_setfilter (Transact-SQL)](../system-stored-procedures/sp-trace-setfilter-transact-sql.md)
- [sp_trace_setstatus (Transact-SQL)](../system-stored-procedures/sp-trace-setstatus-transact-sql.md)
- [System stored procedures (Transact-SQL)](../system-stored-procedures/system-stored-procedures-transact-sql.md)
- [SQL Server Profiler stored procedures (Transact-SQL)](../system-stored-procedures/sql-server-profiler-stored-procedures-transact-sql.md)
