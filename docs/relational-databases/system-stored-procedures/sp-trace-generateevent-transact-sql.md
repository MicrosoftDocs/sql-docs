---
title: "sp_trace_generateevent (Transact-SQL)"
description: Fires a user-defined event to a trace or an event session.
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

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Creates a user-defined event. The event can be collected using [SQL Trace](../sql-trace/sql-trace.md) or [Extended Events](../extended-events/extended-events.md).

> [!NOTE]
> This stored procedure is **not** deprecated. All other SQL Trace related stored procedures are deprecated.

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

The ID of the event to fire. *@eventid* is **int**, with no default. The ID must be in the range from `82` through `91` inclusive. This range represents user-defined events. In SQL Trace, use [sp_trace_setevent](sp-trace-setevent-transact-sql.md) to add an event with this ID to a trace to capture events with the same ID fired from this stored procedure.

#### [ @userinfo = ] '*userinfo*'

The optional user-defined string. *@userinfo* is **nvarchar(128)**, with a default of `NULL`.

#### [ @userdata = ] *userdata*

The optional user-defined data for the event. *@userdata* is **varbinary(8000)**, with a default of `0x`.

## Return code values

The following table describes the return code values that users may get, following the completion of the stored procedure.

| Return code | Description |
| --- | --- |
| `0` | No error. |
| `1` | Unknown error. |
| `3` | The specified event isn't valid. The event may not exist or it isn't an appropriate one for the stored procedure. |
| `13` | Out of memory. Returned when there isn't enough memory to perform the specified action. |

## Remarks

To capture the events fired by this stored procedure using [Extended Events](../extended-events/extended-events.md), add the `user_info` event to an event session. For more information, see [CREATE EVENT SESSION](../../t-sql/statements/create-event-session-transact-sql.md). The `user_info` event is fired for any user-defined event ID value passed to the `@eventid` parameter.

Only ID numbers of user-defined events may be used with `sp_trace_generateevent`. An error is raised if any other event ID number is used.

The parameters of this stored procedure are strictly typed. If the data type of the value passed to a parameter does not match the parameter data type specified in its description, the stored procedure returns an error.

`sp_trace_generateevent` performs many of the actions previously executed by the `xp_trace_*` extended stored procedures. Use `sp_trace_generateevent` instead of `xp_trace_generate_event`.

## Permissions

In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and in Azure SQL Managed Instance, requires the `ALTER TRACE` permission. In Azure SQL Database, requires membership in the `public` database role.

## Examples

The following example fires a user-defined event when a row is inserted into a table. The event contains the data inserted into the table.

To collect the event fired by this example, [create an extended event session](../extended-events/quick-start-extended-events-in-sql-server.md) and include the `user_info` event, or [create a SQL trace](../sql-trace/create-a-trace-transact-sql.md) and include the `UserConfigurable:0` event.

```sql
-- Create a table
DROP TABLE IF EXISTS dbo.user_defined_event_example;

CREATE TABLE dbo.user_defined_event_example
(
Id int IDENTITY(1,1) PRIMARY KEY,
Data nvarchar(60) NOT NULL
);

DROP TRIGGER IF EXISTS fire_user_defined_event;
GO

-- Create an insert trigger on the table
CREATE TRIGGER fire_user_defined_event ON dbo.user_defined_event_example
FOR INSERT
AS
DECLARE @EventData varbinary(8000);

-- Convert inserted rows to JSON and cast it as a binary value
SELECT @EventData = CAST((
                         SELECT Id, Data
                         FROM inserted
                         FOR JSON AUTO
                         ) AS varbinary(8000));

-- Fire the event with the payload carrying inserted rows as JSON
EXEC dbo.sp_trace_generateevent
    @eventid = 82,
    @userinfo = N'Inserted rows into dbo.user_defined_event_example',
    @userdata = @EventData;
GO

-- Insert a row into the table. The trigger fires the event.
INSERT INTO dbo.user_defined_event_example (Data)
VALUES (N'Example data');

-- Copy the binary payload from the event and cast it to a string with the JSON value
SELECT CAST(0x5B007B0022004900640022003A0031002C002200440061007400610022003A0022004500780061006D0070006C0065002000640061007400610022007D005D00 AS nvarchar(max));
-- This returns: [{"Id":1,"Data":"Example data"}]
```

## Related content

- [CREATE EVENT SESSION](../../t-sql/statements/create-event-session-transact-sql.md)
- [Extended Events](../extended-events/extended-events.md)
- [sys.fn_trace_geteventinfo (Transact-SQL)](../system-functions/sys-fn-trace-geteventinfo-transact-sql.md)
- [sp_trace_setevent (Transact-SQL)](sp-trace-setevent-transact-sql.md)
- [SQL Trace](../sql-trace/sql-trace.md)
