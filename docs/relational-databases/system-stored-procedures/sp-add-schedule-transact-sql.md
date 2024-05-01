---
title: "sp_add_schedule (Transact-SQL)"
description: "Creates a schedule that can be used by any number of jobs."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/28/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_add_schedule_TSQL"
  - "sp_add_schedule"
helpviewer_keywords:
  - "sp_add_schedule"
dev_langs:
  - "TSQL"
---
# sp_add_schedule (Transact-SQL)

[!INCLUDE [SQL Server - ASDBMI](../../includes/applies-to-version/sql-asdbmi.md)]

Creates a schedule that can be used by any number of jobs.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_add_schedule
    [ @schedule_name = ] 'schedule_name'
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
    [ , [ @owner_login_name = ] 'owner_login_name' ]
    [ , [ @schedule_uid = ] schedule_uid OUTPUT ]
    [ , [ @schedule_id = ] schedule_id OUTPUT ]
    [ , [ @originating_server = ] server_name ] /* internal */
[ ; ]
```

## Arguments

#### [ @schedule_name = ] '*schedule_name*'

The name of the schedule. *@schedule_name* is **sysname**, with no default.

#### [ @enabled = ] *enabled*

Indicates the current status of the schedule. *@enabled* is **tinyint**, with a default of `1` (enabled). If `0`, the schedule isn't enabled. When the schedule isn't enabled, no jobs run on this schedule.

#### [ @freq_type = ] *freq_type*

A value indicating when a job is to be executed. *@freq_type* is **int**, with a default of `0`, and can be one of these values.

| Value | Description |
| --- | --- |
| `1` | Once |
| `4` | Daily |
| `8` | Weekly |
| `16` | Monthly |
| `32` | Monthly, relative to *@freq_interval* |
| `64` | Run when [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent service starts |
| `128` | Run when the computer is idle (not supported in [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent)) |

#### [ @freq_interval = ] *@freq_interval*

The days that a job is executed. *@freq_interval* is **int**, with a default of `1`, and depends on the value of *@freq_type*.

| Value of *@freq_type* | Effect on *@freq_interval* |
| --- | --- |
| `1` (once) | *@freq_interval* is unused. |
| `4` (daily) | Every *@freq_interval* days. |
| `8` (weekly) | *@freq_interval* is one or more of the following (combined with an `OR` logical operator):<br /><br />`1` = Sunday<br />`2` = Monday<br />`4` = Tuesday<br />`8` = Wednesday<br />`16` = Thursday<br />`32` = Friday<br />`64` = Saturday |
| `16` (monthly) | On the *@freq_interval* day of the month. |
| `32` (monthly relative) | *@freq_interval* is one of the following:<br /><br />`1` = Sunday<br />`2` = Monday<br />`3` = Tuesday<br />`4` = Wednesday<br />`5` = Thursday<br />`6` = Friday<br />`7` = Saturday<br />`8` = Day<br />`9` = Weekday<br />`10` = Weekend day |
| `64` (when SQLServerAgent service starts) | *@freq_interval* is unused. |
| `128` | *@freq_interval* is unused. |

#### [ @freq_subday_type = ] *freq_subday_type*

Specifies the units for *@freq_subday_interval*. *@freq_subday_type* is **int**, with a default of `0`, and can be one of these values.

| Value | Description (unit) |
| --- | --- |
| `0x1` | At the specified time |
| `0x2` | Seconds |
| `0x4` | Minutes |
| `0x8` | Hours |

#### [ @freq_subday_interval = ] *freq_subday_interval*

The number of *@freq_subday_type* periods to occur between each execution of a job. *@freq_subday_interval* is **int**, with a default of `0`. The interval must be at least 10 seconds long. *@freq_subday_interval* is ignored in those cases where *@freq_subday_type* is equal to `1`.

#### [ @freq_relative_interval = ] *freq_relative_interval*

A job's occurrence of *@freq_interval* in each month, if *@freq_interval* is 32 (monthly relative). *@freq_relative_interval* is **int**, with a default of `0`, and can be one of these values. *@freq_relative_interval* is ignored in those cases where *@freq_type* isn't equal to 32.

| Value | Description (unit) |
| --- | --- |
| `1` | First |
| `2` | Second |
| `4` | Third |
| `8` | Fourth |
| `16` | Last |

#### [ @freq_recurrence_factor = ] *freq_recurrence_factor*

The number of weeks or months between the scheduled execution of a job. *@freq_recurrence_factor* is used only if *@freq_type* is `8`, `16`, or `32`. *@freq_recurrence_factor* is **int**, with a default of `0`.

#### [ @active_start_date = ] *active_start_date*

The date on which execution of a job can begin. *@active_start_date* is **int**, with a default of `NULL`, which indicates today's date. The date is formatted as `yyyyMMdd`. If *@active_start_date* isn't NULL, the date must be greater than or equal to 19900101.

After the schedule is created, review the start date and confirm that it's the correct date. For more information, see the section "Scheduling Start Date" in [Create and Attach Schedules to Jobs](../../ssms/agent/create-and-attach-schedules-to-jobs.md).

For weekly or monthly schedules, the Agent ignores if *@active_start_date* is in the past, and instead uses the current date. When a [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent schedule is created using `sp_add_schedule` there's an option to specify the parameter *@active_start_date* that is the date that job execution begins. If the schedule type is weekly or monthly, and the *@active_start_date* parameter is set to a date in the past, the *@active_start_date* parameter is ignored and the current date is used for *@active_start_date*.

#### [ @active_end_date = ] *active_end_date*

The date on which execution of a job can stop. *@active_end_date* is **int**, with a default of `99991231`, which indicates December 31, 9999. Formatted as `yyyyMMdd`.

#### [ @active_start_time = ] *active_start_time*

The time on any day between *@active_start_date* and *@active_end_date* to begin execution of a job. *@active_start_time* is **int**, with a default of `000000`, which indicates 12:00:00 A.M. on a 24-hour clock, and must be entered using the form `HHmmss`.

#### [ @active_end_time = ] *active_end_time*

The time on any day between *@active_start_date* and *@active_end_date* to end execution of a job. *@active_end_time* is **int**, with a default of `235959`, which indicates 11:59:59 P.M. on a 24-hour clock, and must be entered using the form `HHmmss`.

#### [ @owner_login_name = ] '*owner_login_name*'

The name of the server principal that owns the schedule. *@owner_login_name* is **sysname**, with a default of `NULL`, which indicates that the schedule is owned by the creator.

#### [ @schedule_uid = ] *schedule_uid* OUTPUT

A unique identifier for the schedule. *@schedule_uid* is a variable of type **uniqueidentifier**.

#### [ @schedule_id = ] *schedule_id* OUTPUT

An identifier for the schedule. *@schedule_id* is a variable of type **int**.

#### [ @originating_server = ] *server_name*

[!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)]

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

[!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] provides an easy, graphical way to manage jobs, and is the recommended way to create and manage the job infrastructure.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

Other users must be granted one of the following [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the `msdb` database:

- **SQLAgentUserRole**
- **SQLAgentReaderRole**
- **SQLAgentOperatorRole**

For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).

## Examples

### A. Create a schedule

The following example creates a schedule named `RunOnce`. The schedule runs one time, at `23:30` on the day that the schedule is created.

```sql
USE msdb;
GO

EXEC dbo.sp_add_schedule
    @schedule_name = N'RunOnce',
    @freq_type = 1,
    @active_start_time = 233000;
GO
```

### B. Create a schedule, attaching the schedule to multiple jobs

The following example creates a schedule named `NightlyJobs`. Jobs that use this schedule execute every day when the time on the server is `01:00`. The example attaches the schedule to the job `BackupDatabase` and the job `RunReports`.

> [!NOTE]  
> This example assumes that the job `BackupDatabase` and the job `RunReports` already exist.

```sql
USE msdb;
GO

EXEC sp_add_schedule
    @schedule_name = N'NightlyJobs',
    @freq_type = 4,
    @freq_interval = 1,
    @active_start_time = 010000;
GO

EXEC sp_attach_schedule
    @job_name = N'BackupDatabase',
    @schedule_name = N'NightlyJobs';
GO

EXEC sp_attach_schedule
    @job_name = N'RunReports',
    @schedule_name = N'NightlyJobs';
GO
```

## Related content

- [Create and Attach Schedules to Jobs](../../ssms/agent/create-and-attach-schedules-to-jobs.md)
- [Schedule a Job](../../ssms/agent/schedule-a-job.md)
- [Create a Schedule](../../ssms/agent/create-a-schedule.md)
- [SQL Server Agent stored procedures (Transact-SQL)](sql-server-agent-stored-procedures-transact-sql.md)
- [sp_add_jobschedule (Transact-SQL)](sp-add-jobschedule-transact-sql.md)
- [sp_update_schedule (Transact-SQL)](sp-update-schedule-transact-sql.md)
- [sp_delete_schedule (Transact-SQL)](sp-delete-schedule-transact-sql.md)
- [sp_help_schedule (Transact-SQL)](sp-help-schedule-transact-sql.md)
- [sp_attach_schedule (Transact-SQL)](sp-attach-schedule-transact-sql.md)
