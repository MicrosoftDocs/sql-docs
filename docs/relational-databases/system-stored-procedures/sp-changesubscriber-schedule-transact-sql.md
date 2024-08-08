---
title: "sp_changesubscriber_schedule (Transact-SQL)"
description: sp_changesubscriber_schedule changes the Distribution Agent or Merge Agent schedule for a subscriber.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/05/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_changesubscriber_schedule"
  - "sp_changesubscriber_schedule_TSQL"
helpviewer_keywords:
  - "sp_changesubscriber_schedule"
dev_langs:
  - "TSQL"
---
# sp_changesubscriber_schedule (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Changes the Distribution Agent or Merge Agent schedule for a subscriber. This stored procedure is executed at the Publisher on any database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_changesubscriber_schedule
    [ @subscriber = ] N'subscriber'
    , [ @agent_type = ] agent_type
    [ , [ @frequency_type = ] frequency_type ]
    [ , [ @frequency_interval = ] frequency_interval ]
    [ , [ @frequency_relative_interval = ] frequency_relative_interval ]
    [ , [ @frequency_recurrence_factor = ] frequency_recurrence_factor ]
    [ , [ @frequency_subday = ] frequency_subday ]
    [ , [ @frequency_subday_interval = ] frequency_subday_interval ]
    [ , [ @active_start_time_of_day = ] active_start_time_of_day ]
    [ , [ @active_end_time_of_day = ] active_end_time_of_day ]
    [ , [ @active_start_date = ] active_start_date ]
    [ , [ @active_end_date = ] active_end_date ]
    [ , [ @publisher = ] N'publisher' ]
[ ; ]
```

## Arguments

#### [ @subscriber = ] N'*subscriber*'

The name of the Subscriber. *@subscriber* is **sysname**, with no default. The name of the Subscriber must be unique in the database, must not already exist, and can't be `NULL`.

#### [ @agent_type = ] *agent_type*

The type of agent. *@agent_type* is **smallint**, with a default of `0`.

- `0` indicates a Distribution Agent.
- `1` indicates a Merge Agent.

#### [ @frequency_type = ] *frequency_type*

Specifies the frequency with which to schedule the distribution task. *@frequency_type* is **int**, and can be one of these values.

| Value | Description |
| --- | --- |
| `1` | One time |
| `2` | On demand |
| `4` | Daily |
| `8` | Weekly |
| `16` | Monthly |
| `32` | Monthly relative |
| `64` (default) | Autostart |
| `128` | Recurring |

#### [ @frequency_interval = ] *frequency_interval*

The value applied to the frequency set by *@frequency_type*. *@frequency_interval* is **int**, and depends on the value of *@frequency_type*.

| Value of *@frequency_type* | Effect on *@frequency_interval* |
| --- | --- |
| `1` (default) | *@frequency_interval* is unused. |
| `4` | Every *@frequency_interval* days. |
| `8` | *@frequency_interval* is one or more of the following (combined with a [&#124; (Bitwise OR) (Transact-SQL)](../../t-sql/language-elements/bitwise-or-transact-sql.md) logical operator):<br /><br />`1` = Sunday<br />`2` = Monday<br />`4` = Tuesday<br />`8` = Wednesday<br />`16` = Thursday<br />`32` = Friday<br />`64` = Saturday |
| `16` | On the *@frequency_interval* day of the month. |
| `32` | *@frequency_interval* is one of the following options:<br /><br />`1` = Sunday<br />`2` = Monday<br />`3` = Tuesday<br />`4` = Wednesday<br />`5` = Thursday<br />`6` = Friday<br />`7` = Saturday<br />`8` = Day<br />`9` = Weekday<br />`10` = Weekend day |
| `64` | *@frequency_interval* is unused. |
| `128` | *@frequency_interval* is unused. |

#### [ @frequency_relative_interval = ] *frequency_relative_interval*

The date of the distribution task. *@frequency_relative_interval* is **int**, and can be one of these values.

| Value | Description |
| --- | --- |
| `1` (default) | First |
| `2` | Second |
| `4` | Third |
| `8` | Fourth |
| `16` | Last |

#### [ @frequency_recurrence_factor = ] *frequency_recurrence_factor*

The recurrence factor used by *@frequency_type*. *@frequency_recurrence_factor* is **int**, with a default of `0`.

#### [ @frequency_subday = ] *frequency_subday*

Specifies how often, in minutes, to reschedule during the defined period. *@frequency_subday* is **int**, and can be one of these values.

| Value | Description |
| --- | --- |
| `1` | Once |
| `2` | Second |
| `4` (default) | Minute |
| `8` | Hour |

#### [ @frequency_subday_interval = ] *frequency_subday_interval*

The number of *frequency_subday* periods that occur between each execution of the job. *@frequency_subday_interval* is **int**, with a default of `5`.

#### [ @active_start_time_of_day = ] *active_start_time_of_day*

The time of day when the distribution task is first scheduled. *@active_start_time_of_day* is **int**, with a default of `NULL`.

#### [ @active_end_time_of_day = ] *active_end_time_of_day*

The time of day when the distribution task stops being scheduled. *@active_end_time_of_day* is **int**, with a default of `235959`, which means 11:59:59 P.M. on a 24-hour clock.

#### [ @active_start_date = ] *active_start_date*

The date when the distribution task is first scheduled, formatted as `yyyyMMdd`. *@active_start_date* is **int**, with a default of `NULL`.

#### [ @active_end_date = ] *active_end_date*

The date when the distribution task stops being scheduled, formatted as `yyyyMMdd`. *@active_end_date* is **int**, with a default of `99991231`, which means December 31, 9999.

#### [ @publisher = ] N'*publisher*'

Specifies a non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher. *@publisher* is **sysname**, with a default of `NULL`.

*@publisher* shouldn't be used when changing article properties on a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_changesubscriber_schedule` is used in all types of replication.

## Permissions

Only members of the **sysadmin** fixed server role can execute `sp_changesubscriber_schedule`.

## Related content

- [sp_addsubscriber_schedule (Transact-SQL)](sp-addsubscriber-schedule-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
