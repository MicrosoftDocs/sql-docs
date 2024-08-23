---
title: "sp_update_schedule (Transact-SQL)"
description: Changes the settings for a SQL Server Agent schedule.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_update_schedule"
  - "sp_update_schedule_TSQL"
helpviewer_keywords:
  - "sp_update_schedule"
dev_langs:
  - "TSQL"
---
# sp_update_schedule (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Changes the settings for a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent schedule.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_update_schedule
    [ [ @schedule_id = ] schedule_id ]
    [ , [ @name = ] N'name' ]
    [ , [ @new_name = ] N'new_name' ]
    [ , [ @enabled = ] enabled ]
    [ , [ @freq_type = ] freq_type ]
    [ , [ @freq_interval = ] freq_interval ]
    [ , [ @freq_subday_type = ] freq_subday_type ]
    [ , [ @freq_subday_interval = ] freq_subday_interval ]
    [ , [ @freq_relative_interval = ] freq_relative_interval ]
    [ , [ @freq_recurrence_factor = ] freq_recurrence_factor ]
    [ , [ @active_start_date = ] active_start_date ]
    [ , [ @active_end_date = ] active_end_date ]
    [ , [ @active_start_time = ] active_start_time ]
    [ , [ @active_end_time = ] active_end_time ]
    [ , [ @owner_login_name = ] N'owner_login_name' ]
    [ , [ @automatic_post = ] automatic_post ]
[ ; ]
```

## Arguments

#### [ @schedule_id = ] *schedule_id*

The identifier of the schedule to modify. *@schedule_id* is **int**, with a default of `NULL`.

Either *@schedule_id* or *@name* must be specified.

#### [ @name = ] N'*name*'

The name of the schedule to modify. *@name* is **sysname**, with a default of `NULL`.

Either *@schedule_id* or *@name* must be specified.

#### [ @new_name = ] N'*new_name*'

The new name for the schedule. *@new_name* is **sysname**, with a default of `NULL`. When *@new_name* is `NULL`, the name of the schedule is unchanged.

#### [ @enabled = ] *enabled*

Indicates the current status of the schedule. *@enabled* is **tinyint**, with a default of `1` (enabled). If `0`, the schedule isn't enabled. When the schedule isn't enabled, no jobs will run on this schedule.

#### [ @freq_type = ] *freq_type*

A value indicating when a job is to be executed. *@freq_type* is **int**, and can be one of these values.

| Value | Description |
| --- | --- |
| `1` | Once |
| `4` | Daily |
| `8` | Weekly |
| `16` | Monthly |
| `32` | Monthly, relative to *@freq_interval* |
| `64` | Run when [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent service starts |
| `128` | Run when the computer is idle |

#### [ @freq_interval = ] *freq_interval*

The days that a job is executed. *@freq_interval* is **int**, with a default of `0`, and depends on the value of *@freq_type* as indicated in the following table:

| Value of *@freq_type* | Effect on *@freq_interval* |
| --- | --- |
| `1` (once) | *@freq_interval* is unused. |
| `4` (daily) | Every *@freq_interval* days. |
| `8` (weekly) | *@freq_interval* is one or more of the following (combined with an `OR` logical operator):<br /><br />`1` = Sunday<br />`2` = Monday<br />`4` = Tuesday<br />`8` = Wednesday<br />`16` = Thursday<br />`32` = Friday<br />`64` = Saturday |
| `16` (monthly) | On the *@freq_interval* day of the month. |
| `32` (monthly relative) | *@freq_interval* is one of the following:<br /><br />`1` = Sunday<br />`2` = Monday<br />`3` = Tuesday<br />`4` = Wednesday<br />`5` = Thursday<br />`6` = Friday<br />`7` = Saturday<br />`8` = Day<br />`9` = Weekday<br />`10` = Weekend day |
| `64` (when the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent service starts) | *@freq_interval* is unused. |
| `128` | *@freq_interval* is unused. |

#### [ @freq_subday_type = ] *freq_subday_type*

Specifies the units for *@freq_subday_interval*. *@freq_subday_type* is **int**, and can be one of these values.

| Value | Description (unit) |
| --- | --- |
| `0x1` | At the specified time |
| `0x2` | Seconds |
| `0x4` | Minutes |
| `0x8` | Hours |

#### [ @freq_subday_interval = ] *freq_subday_interval*

The number of *@freq_subday_type* periods to occur between each execution of a job. *@freq_subday_interval* is **int**, with a default of `0`.

#### [ @freq_relative_interval = ] *freq_relative_interval*

Further defines the *@freq_interval* when *@freq_type* is set to `32` (monthly relative).

*@freq_relative_interval* is **int**, and can be one of these values.

| Value | Description (unit) |
| --- | --- |
| `1` | First |
| `2` | Second |
| `4` | Third |
| `8` | Fourth |
| `16` | Last |

*@freq_relative_interval* indicates the occurrence of the interval. For example, if *@freq_relative_interval* is set to `2`, *@freq_type* is set to `32`, and *@freq_interval* is set to `3`, the scheduled job would occur on the second Tuesday of each month.

#### [ @freq_recurrence_factor = ] *freq_recurrence_factor*

The number of weeks or months between the scheduled execution of a job. *@freq_recurrence_factor* is **int**, with a default of `0`. *@freq_recurrence_factor* is used only if *@freq_type* is set to `8`, `16`, or `32`.

#### [ @active_start_date = ] *active_start_date*

The date on which job execution can begin. *@active_start_date* is **int**, with a default of `NULL`. The date is formatted as `yyyyMMdd`. If *@active_start_date* is set, the date must be greater than or equal to `19900101`.

After the schedule is created, review the start date and confirm that it's the correct date. For more information, see the section "Scheduling Start Date" in [Create and Attach Schedules to Jobs](../../ssms/agent/create-and-attach-schedules-to-jobs.md).

#### [ @active_end_date = ] *active_end_date*

Date on which job execution can stop. *@active_end_date* is **int**, with a default of `99991231`. The date is formatted as `yyyyMMdd`.

#### [ @active_start_time = ] *active_start_time*

The time on any day between *@active_start_date* and *@active_end_date* to begin job execution. *@active_start_time* is **int**, with a default of `000000`. The time is formatted as `HHmmss` on a 24-hour clock.

#### [ @active_end_time = ] *active_end_time*

The time on any day between *active_start_date* and *@active_end_date* to end job execution. *@active_end_time* is **int**, with a default of `235959`. The time is formatted as `HHmmss` on a 24-hour clock.

#### [ @owner_login_name = ] N'*owner_login_name*'

The name of the server principal that owns the schedule. *@owner_login_name* is **sysname**, with a default of `NULL`, which indicates that the schedule is owned by the creator.

#### [ @automatic_post = ] *automatic_post*

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

## Return code values

`0` (success) or `1` (failure).

## Remarks

All jobs that use the schedule immediately use the new settings. However, changing a schedule doesn't stop jobs that are currently running.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

Other users must be granted one of the following [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the `msdb` database:

- **SQLAgentUserRole**
- **SQLAgentReaderRole**
- **SQLAgentOperatorRole**

For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).

Only members of **sysadmin** can modify a schedule owned by another user.

## Examples

The following example changes the enabled status of the `NightlyJobs` schedule to `0` and sets the owner to `terrid`.

```sql
USE msdb;
GO

EXEC dbo.sp_update_schedule
    @name = 'NightlyJobs',
    @enabled = 0,
    @owner_login_name = 'terrid';
GO
```

## Related content

- [Create and Attach Schedules to Jobs](../../ssms/agent/create-and-attach-schedules-to-jobs.md)
- [Schedule a Job](../../ssms/agent/schedule-a-job.md)
- [Create a Schedule](../../ssms/agent/create-a-schedule.md)
- [SQL Server Agent stored procedures (Transact-SQL)](sql-server-agent-stored-procedures-transact-sql.md)
- [sp_add_schedule (Transact-SQL)](sp-add-schedule-transact-sql.md)
- [sp_add_jobschedule (Transact-SQL)](sp-add-jobschedule-transact-sql.md)
- [sp_delete_schedule (Transact-SQL)](sp-delete-schedule-transact-sql.md)
- [sp_help_schedule (Transact-SQL)](sp-help-schedule-transact-sql.md)
- [sp_attach_schedule (Transact-SQL)](sp-attach-schedule-transact-sql.md)
