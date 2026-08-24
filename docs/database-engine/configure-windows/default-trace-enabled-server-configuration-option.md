---
title: "Server Configuration: default trace enabled"
description: "Learn about the default trace enabled option. Find out how default tracing can help with troubleshooting."
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/26/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
helpviewer_keywords:
  - "logs [SQL Server], traces"
  - "traces [SQL Server], logs"
  - "default trace enabled option"
---
# Server configuration: default trace enabled

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Use the `default trace enabled` option to enable or disable the default trace log files. The default trace functionality provides a rich, persistent log of activity and changes primarily related to the configuration options.

> [!WARNING]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use Extended Events instead.

## Purpose

Default trace provides troubleshooting assistance to database administrators by ensuring that they have the log data necessary to diagnose problems the first time they occur.

## View default trace

The default trace logs can be opened and examined by [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] or queried with [!INCLUDE [tsql](../../includes/tsql-md.md)] by using the `fn_trace_gettable` system function. [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] can open the default trace log files just as it does normal trace output files. The default trace log is stored by default in the `\MSSQL\LOG` directory using a rollover trace file. The base file name for the default trace log file is `log.trc`. In a typical installation of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], the default trace is enabled and thus becomes TraceID 1. If enabled after installation and after creating other traces, the TraceID can become a larger number.

For more information about using [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Profiler to view this trace file, see [Open a trace file (SQL Server Profiler)](../../tools/sql-server-profiler/open-a-trace-file-sql-server-profiler.md)

### Example

The following statement opens the default trace log, in the default location:

```sql
SELECT *
FROM fn_trace_gettable (
    'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\LOG\log.trc',
    default
);
GO
```

## Configure

When set to 1, the `default trace enabled` option enables **Default Trace**. The default setting for this option is `1` (enabled). A value of `0` turns off the trace.

The `default trace enabled` option is an advanced option. If you're using the `sp_configure` system stored procedure to change the setting, you can change the `default trace enabled` option only when `show advanced options` is set to `1`. The setting takes effect immediately without a server restart.

## Related content

- [RECONFIGURE (Transact-SQL)](../../t-sql/language-elements/reconfigure-transact-sql.md)
- [Server configuration options](server-configuration-options-sql-server.md)
- [sys.sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
