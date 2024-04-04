---
title: "sp_trace_setstatus (Transact-SQL)"
description: Modifies the current state of the specified trace.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 09/07/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_trace_setstatus_TSQL"
  - "sp_trace_setstatus"
helpviewer_keywords:
  - "sp_trace_setstatus"
dev_langs:
  - "TSQL"
---
# sp_trace_setstatus (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Modifies the current state of the specified trace.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use Extended Events instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_trace_setstatus
    [ @traceid = ] traceid
    , [ @status = ] status
[ ; ]
```

## Arguments

#### [ @traceid = ] *traceid*

The ID of the trace to be modified. *@traceid* is **int**, with no default. The user employs this *@traceid* value to identify, modify, and control the trace. For information about retrieving the *@traceid*, see [sys.fn_trace_getinfo (Transact-SQL)](../system-functions/sys-fn-trace-getinfo-transact-sql.md).

#### [ @status = ] *status*

Specifies the action to implement on the trace. *@status* is **int**, with no default.

The following table lists the status that may be specified.

| Status | Description |
| --- | --- |
| `0` | Stops the specified trace. |
| `1` | Starts the specified trace. |
| `2` | Closes the specified trace and deletes its definition from the server. |

> [!NOTE]  
> A trace must be stopped first before it can be closed. A trace must be stopped and closed first before it can be viewed.

## Return code values

The following table describes the code values that users may get following completion of the stored procedure.

| Return code | Description |
| --- | --- |
| `0` | No error. |
| `1` | Unknown error. |
| `8` | The specified Status isn't valid. |
| `9` | The specified Trace Handle isn't valid. |
| `13` | Out of memory. Returned when there isn't enough memory to perform the specified action. |

If the trace is already in the state specified, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] returns `0`.

## Remarks

Parameters of all SQL Trace stored procedures (`sp_trace_*`) are strictly typed. If these parameters aren't called with the correct input parameter data types, as specified in the argument description, the stored procedure returns an error.

For an example of using trace stored procedures, see [Create a Trace (Transact-SQL)](../sql-trace/create-a-trace-transact-sql.md).

## Permissions

Requires ALTER TRACE permission.

## Related content

- [sys.fn_trace_geteventinfo (Transact-SQL)](../system-functions/sys-fn-trace-geteventinfo-transact-sql.md)
- [sys.fn_trace_getfilterinfo (Transact-SQL)](../system-functions/sys-fn-trace-getfilterinfo-transact-sql.md)
- [sp_trace_generateevent (Transact-SQL)](sp-trace-generateevent-transact-sql.md)
- [sp_trace_setevent (Transact-SQL)](sp-trace-setevent-transact-sql.md)
- [sp_trace_setfilter (Transact-SQL)](sp-trace-setfilter-transact-sql.md)
- [SQL Trace](../sql-trace/sql-trace.md)
