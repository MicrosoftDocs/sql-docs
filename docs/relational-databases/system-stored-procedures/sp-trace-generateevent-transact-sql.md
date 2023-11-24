---
title: "sp_trace_generateevent (Transact-SQL)"
description: Creates a user-defined event in SQL Server.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 09/07/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_trace_generateevent_TSQL"
  - "sp_trace_generateevent"
helpviewer_keywords:
  - "sp_trace_generateevent"
dev_langs:
  - "TSQL"
---
# sp_trace_generateevent (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Creates a user-defined event in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

> [!NOTE]  
> This stored procedure is **not** deprecated. All other trace-related stored procedures are deprecated.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_trace_generateevent
    [ @eventid = ] eventid
    [ , [ @userinfo = ] N'userinfo' ]
    [ , [ @userdata = ] userdata ]
[ ; ]
```

## Arguments

#### [ @eventid = ] *eventid*

The ID of the event to turn on. *@eventid* is **int**, with no default. The ID must be one of the event numbers from `82` through `91`, which represent user-defined events as set with [sp_trace_setevent](sp-trace-setevent-transact-sql.md).

#### [ @userinfo = ] '*userinfo*'

The optional user-defined string identifying the reason for the event. *@userinfo* is **nvarchar(128)**, with a default of `NULL`.

#### [ @userdata = ] *userdata*

The optional user-specified data for the event. *@userdata* is **varbinary(8000)**, with a default of `NULL`.

## Return code values

The following table describes the code values that users may get, following completion of the stored procedure.

| Return code | Description |
| --- | --- |
| `0` | No error. |
| `1` | Unknown error. |
| `3` | The specified event isn't valid. The event may not exist or it isn't an appropriate one for the store procedure. |
| `13` | Out of memory. Returned when there isn't enough memory to perform the specified action. |

## Remarks

`sp_trace_generateevent` performs many of the actions previously executed by the `xp_trace_*` extended stored procedures. Use `sp_trace_generateevent` instead of `xp_trace_generate_event`.

Only ID numbers of user-defined events may be used with `sp_trace_generateevent`. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] raises an error if other event ID numbers are used.

Parameters of all SQL Trace stored procedures (`sp_trace_*`) are strictly typed. If these parameters aren't called with the correct input parameter data types, as specified in the argument description, the stored procedure returns an error.

## Permissions

Requires ALTER TRACE permission.

## Examples

The following example creates a user-configurable event on a sample table.

```sql
-- Create a sample table.
CREATE TABLE user_config_test (col1 INT, col2 CHAR(10));

-- DROP the trigger if it already exists.
IF EXISTS (
    SELECT *
    FROM sysobjects
    WHERE name = 'userconfig_trg'
)
DROP TRIGGER userconfig_trg;

-- Create an ON INSERT trigger on the sample table.
CREATE TRIGGER userconfig_trg
    ON user_config_test FOR INSERT;
AS
EXEC master.dbo.sp_trace_generateevent
    @event_class = 82,
    @userinfo = N'Inserted row into user_config_test';

-- When an insert action happens, the user-configurable
-- event fires. If you were capturing the event id = 82,
-- you will see it in the Profiler output.
INSERT INTO user_config_test
VALUES (1, 'abc');
```

## See also

- [sys.fn_trace_geteventinfo (Transact-SQL)](../system-functions/sys-fn-trace-geteventinfo-transact-sql.md)
- [sp_trace_setevent (Transact-SQL)](sp-trace-setevent-transact-sql.md)
- [SQL Trace](../sql-trace/sql-trace.md)
