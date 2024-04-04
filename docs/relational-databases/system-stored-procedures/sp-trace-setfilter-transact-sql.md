---
title: "sp_trace_setfilter (Transact-SQL)"
description: Applies a filter to a trace.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 09/07/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_trace_setfilter"
  - "sp_trace_setfilter_TSQL"
helpviewer_keywords:
  - "sp_trace_setfilter"
dev_langs:
  - "TSQL"
---
# sp_trace_setfilter (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Applies a filter to a trace. `sp_trace_setfilter` may be executed only on existing traces that are stopped (*@status* is `0`). [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] returns an error if this stored procedure is executed on a trace that doesn't exist or whose *@status* isn't `0`.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use Extended Events instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_trace_setfilter
    [ @traceid = ] traceid
    , [ @columnid = ] columnid
    , [ @logical_operator = ] logical_operator
    , [ @comparison_operator = ] comparison_operator
    , [ @value = ] value
[ ; ]
```

## Arguments

#### [ @traceid = ] *traceid*

The ID of the trace to which the filter is set. *@traceid* is **int**, with no default. The user employs this *@traceid* value to identify, modify, and control the trace.

#### [ @columnid = ] *columnid*

The ID of the column on which the filter is applied. *@columnid* is **int**, with no default. If *@columnid* is `NULL`, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] clears all filters for the specified trace.

#### [ @logical_operator = ] *logical_operator*

Specifies whether the AND (`0`) or OR (`1`) operator is applied. *@logical_operator* is **int**, with no default.

#### [ @comparison_operator = ] *comparison_operator*

Specifies the type of comparison to be made. *@comparison_operator* is **int**, with no default. The table contains the comparison operators and their representative values.

| Value | Comparison operator |
| --- | --- |
| `0` | `=` (Equal) |
| `1` | `<>` (Not Equal) |
| `2` | `>` (Greater Than) |
| `3` | `<` (Less Than) |
| `4` | `>=` (Greater Than Or Equal) |
| `5` | `<=` (Less Than Or Equal) |
| `6` | `LIKE` |
| `7` | `NOT LIKE` |

#### [ @value = ] *value*

Specifies the value on which to filter. The data type of *@value* must match the data type of the column to be filtered. For example, if the filter is set on an Object ID column that is an **int** data type, *@value* must be **int**. If *@value* is **nvarchar** or **varbinary**, it can have a maximum length of 8000.

When the comparison operator is `LIKE` or `NOT LIKE`, the logical operator can include `%` or other filter appropriate for the `LIKE` operation.

You can specify `NULL` for *@value* to filter out events with `NULL` column values. Only `0` (`=` Equal) and `1` (`<>` Not Equal) operators are valid with `NULL`. In this case, these operators are equivalent to the [!INCLUDE [tsql](../../includes/tsql-md.md)] `IS NULL` and `IS NOT NULL` operators.

To apply the filter between a range of column values, `sp_trace_setfilter` must be executed twice: once with a greater-than-or-equals (`>=`) comparison operator, and another time with a less-than-or-equals (`<=`) operator.

For more information about data column data types, see the [SQL Server Event Class Reference](../event-classes/sql-server-event-class-reference.md).

## Return code values

The following table describes the code values that users may get following completion of the stored procedure.

| Return code | Description |
| --- | --- |
| `0` | No error. |
| `1` | Unknown error. |
| `2` | The trace is currently running. Changing the trace at this time results in an error. |
| `4` | The specified Column isn't valid. |
| `5` | The specified Column isn't allowed for filtering. This value is returned only from `sp_trace_setfilter`. |
| `6` | The specified Comparison Operator isn't valid. |
| `7` | The specified Logical Operator isn't valid. |
| `9` | The specified Trace Handle isn't valid. |
| `13` | Out of memory. Returned when there isn't enough memory to perform the specified action. |
| `16` | The function isn't valid for this trace. |

## Remarks

`sp_trace_setfilter` is a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] stored procedure that performs many of the actions previously executed by extended stored procedures available in earlier versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Use `sp_trace_setfilter` instead of the `xp_trace_set*filter` extended stored procedures to create, apply, remove, or manipulate filters on traces. For more information, see [Filter a Trace](../sql-trace/filter-a-trace.md).

All filters for a particular column must be enabled together in one execution of `sp_trace_setfilter`. For example, if a user intends to apply two filters on the application name column and one filter on the username column, the user must specify the filters on application name in sequence. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] returns an error if the user attempts to specify a filter on application name in one stored procedure call, followed by a filter on username, then another filter on application name.

Parameters of all SQL Trace stored procedures (`sp_trace_*`) are strictly typed. If these parameters aren't called with the correct input parameter data types, as specified in the argument description, the stored procedure returns an error.

## Permissions

Requires ALTER TRACE permission.

## Examples

The following example sets three filters on Trace `1`. The filters `N'SQLT%'` and `N'MS%'` operate on one column (`AppName`, value `10`) using the "`LIKE`" comparison operator. The filter `N'joe'` operates on a different column (`UserName`, value `11`) using the "`EQUAL`" comparison operator.

```sql
EXEC sp_trace_setfilter 1, 10, 0, 6, N'SQLT%';
EXEC sp_trace_setfilter 1, 10, 0, 6, N'MS%';
EXEC sp_trace_setfilter 1, 11, 0, 0, N'joe';
```

## Related content

- [sys.fn_trace_getfilterinfo (Transact-SQL)](../system-functions/sys-fn-trace-getfilterinfo-transact-sql.md)
- [sys.fn_trace_getinfo (Transact-SQL)](../system-functions/sys-fn-trace-getinfo-transact-sql.md)
- [SQL Trace](../sql-trace/sql-trace.md)
