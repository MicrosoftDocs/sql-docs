---
title: "sp_addsubscriber_schedule (Transact-SQL)"
description: sp_addsubscriber_schedule adds a schedule for the Distribution Agent and Merge Agent.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 01/23/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_addsubscriber_schedule_TSQL"
  - "sp_addsubscriber_schedule"
helpviewer_keywords:
  - "sp_addsubscriber_schedule"
dev_langs:
  - "TSQL"
---
# sp_addsubscriber_schedule (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Adds a schedule for the Distribution Agent and Merge Agent. This stored procedure is executed at the Publisher on any database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_addsubscriber_schedule
    [ @subscriber = ] N'subscriber'
    [ , [ @agent_type = ] agent_type ]
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

The name of the Subscriber. *@subscriber* is **sysname**, with no default. *@subscriber* must be unique in the database, must not already exist, and can't be `NULL`.

#### [ @agent_type = ] *agent_type*

The type of agent. *@agent_type* is **smallint**, and can be one of these values.

| Value | Description |
| --- | --- |
| `0` (default) | Distribution Agent |
| `1` | Merge Agent |

#### [ @frequency_type = ] *frequency_type*

Specifies the frequency with which to schedule the Distribution Agent. *@frequency_type* is **int**, and can be one of these values.

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

The value to apply to the frequency set by *frequency_type*. *@frequency_interval* is **int**, with a default of `1`.

#### [ @frequency_relative_interval = ] *frequency_relative_interval*

The date of the Distribution Agent. This parameter is used when *frequency_type* is set to `32` (monthly relative). *@frequency_relative_interval* is **int**, and can be one of these values.

| Value | Description |
| --- | --- |
| `1` (default) | First |
| `2` | Second |
| `4` | Third |
| `8` | Fourth |
| `16` | Last |

#### [ @frequency_recurrence_factor = ] *frequency_recurrence_factor*

The recurrence factor used by *frequency_type*. *@frequency_recurrence_factor* is **int**, with a default of `0`.

#### [ @frequency_subday = ] *frequency_subday*

How often to reschedule during the defined period. *@frequency_subday* is **int**, and can be one of these values.

| Value | Description |
| --- | --- |
| `1` | Once |
| `2` | Second |
| `4` (default) | Minute |
| `8` | Hour |

#### [ @frequency_subday_interval = ] *frequency_subday_interval*

The interval for *frequency_subday*. *@frequency_subday_interval* is **int**, with a default of `5`.

#### [ @active_start_time_of_day = ] *active_start_time_of_day*

The time of day when the Distribution Agent is first scheduled, formatted as `HHmmss`. *@active_start_time_of_day* is **int**, with a default of `0`.

#### [ @active_end_time_of_day = ] *active_end_time_of_day*

The time of day when the Distribution Agent stops being scheduled, formatted as `HHmmss`. *@active_end_time_of_day* is **int**, with a default of `235959`, which means 11:59:59 P.M. as measured on a 24-hour clock.

#### [ @active_start_date = ] *active_start_date*

The date when the Distribution Agent is first scheduled, formatted as `yyyyMMdd`. *@active_start_date* is **int**, with a default of `0`.

#### [ @active_end_date = ] *active_end_date*

The date when the Distribution Agent stops being scheduled, formatted as `yyyyMMdd`. *@active_end_date* is **int**, with a default of `99991231`, which means December 31, 9999.

#### [ @publisher = ] N'*publisher*'

Specifies a non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher. *@publisher* is **sysname**, with a default of `NULL`.

*@publisher* shouldn't be specified for a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_addsubscriber_schedule` is used in snapshot replication, transactional replication, and merge replication.

## Permissions

Only members of the **sysadmin** fixed server role can execute `sp_addsubscriber_schedule`.

## Related content

- [sp_changesubscriber_schedule (Transact-SQL)](sp-changesubscriber-schedule-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
