---
title: "Get information about a saved trace (Transact-SQL)"
description: "This article describes how to use built-in functions to view information about a saved trace."
author: "MashaMSFT"
ms.author: "mathoma"
ms.reviewer: randolphwest
ms.date: 06/19/2023
ms.service: sql
ms.topic: conceptual
helpviewer_keywords:
  - "traces [SQL Server], viewing"
  - "displaying traces"
  - "viewing traces"
---
# Get information about a saved trace (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to use built-in functions to view a saved trace.

> [!IMPORTANT]  
> [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use Extended Events instead.

## View a specific trace

1. Execute `sys.fn_trace_getinfo` by specifying the ID of the trace about which information is needed. This function returns a table that lists the trace, trace property, and information about the property.

   Invoke the function this way:

   ```sql
   SELECT *
   FROM ::fn_trace_getinfo(trace_id);
   ```

## View all existing traces

1. Execute `sys.fn_trace_getinfo` by specifying `0` or `default`. This function returns a table that lists all the traces, their properties, and information about these properties.

   Invoke the function as follows:

   ```sql
   SELECT *
   FROM ::fn_trace_getinfo(default);
   ```

## .NET Framework security

Requires ALTER TRACE on the server.

## See also

- [sys.fn_trace_getinfo (Transact-SQL)](../system-functions/sys-fn-trace-getinfo-transact-sql.md)
- [View and analyze traces with SQL Server Profiler](../../tools/sql-server-profiler/view-and-analyze-traces-with-sql-server-profiler.md)
