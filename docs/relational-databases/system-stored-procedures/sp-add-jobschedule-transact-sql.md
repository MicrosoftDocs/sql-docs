---
title: "sp_add_jobschedule (Transact-SQL)"
description: "sp_add_jobschedule creates a schedule for a SQL Server Agent job."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_add_jobschedule"
  - "sp_add_jobschedule_TSQL"
helpviewer_keywords:
  - "sp_add_jobschedule"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_add_jobschedule (Transact-SQL)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Creates a schedule for a [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent job.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

## Syntax

```syntaxsql
sp_add_jobschedule
    [ [ @job_id = ] 'job_id' ]
    [ , [ @job_name = ] N'job_name' ]
    , [ @name = ] N'name'
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
    [ , [ @schedule_id = ] schedule_id OUTPUT ]
    [ , [ @automatic_post = ] automatic_post ]
    [ , [ @schedule_uid = ] 'schedule_uid' OUTPUT ]
[ ; ]
```

## Arguments

#### [ @job_id = ] '*job_id*'

Job identification number of the job to which the schedule is added. *@job_id* is **uniqueidentifier**, with a default of `NULL`.

Either *@job_id* or *@job_name* must be specified, but both can't be specified.

#### [ @job_name = ] N'*job_name*'

Name of the job to which the schedule is added. *@job_name* is **sysname**, with a default of `NULL`.

Either *@job_id* or *@job_name* must be specified, but both can't be specified.

#### [ @name = ] N'*name*'

Name of the schedule. *@name* is **sysname**, with no default.

#### [ @enabled = ] *enabled*

Indicates the current status of the schedule. *@enabled* is **tinyint**, with a default of `1` (enabled). If `0`, the schedule isn't enabled. When the schedule is disabled, the job doesn't be run.

#### [ @freq_type = ] *freq_type*

Value that indicates when the job is to be executed. *@freq_type* is **int**, and can be one of the following values:

| Value | Description |
| --- | --- |
| `1` | Once |
| `4` | Daily |
| `8` | Weekly |
| `16` | Monthly |
| `32` | Monthly, relative to *@freq_interval*. |
| `64` | Run when the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent service starts. |
| `128` | Run when the computer is idle. |

#### [ @freq_interval = ] *freq_interval*

Day that the job is executed. *@freq_interval* is **int**, with a default of `0`, and depends on the value of *@freq_type* as indicated in the following table:

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

Specifies the units for *@freq_subday_interval*. *@freq_subday_type* is **int**, and can be one of these values:

| Value | Description (unit) |
| --- | --- |
| `0x1` | At the specified time |
| `0x2` | Seconds |
| `0x4` | Minutes |
| `0x8` | Hours |

#### [ @freq_subday_interval = ] *freq_subday_interval*

Number of *@freq_subday_type* periods to occur between each execution of the job. *@freq_subday_interval* is **int**, with a default of `0`.

#### [ @freq_relative_interval = ] *freq_relative_interval*

Further defines the *@freq_interval* when *@freq_type* is set to `32` (monthly relative).

*@freq_relative_interval* is **int**, and can be one of these values:

| Value | Description (unit) |
| --- | --- |
| `1` | First |
| `2` | Second |
| `4` | Third |
| `8` | Fourth |
| `16` | Last |

*@freq_relative_interval* indicates the occurrence of the interval. For example, if *@freq_relative_interval* is set to `2`, *@freq_type* is set to `32`, and *@freq_interval* is set to `3`, the scheduled job would occur on the second Tuesday of each month.

#### [ @freq_recurrence_factor = ] *freq_recurrence_factor*

Number of weeks or months between the scheduled execution of the job. *@freq_recurrence_factor* is **int**, with a default of `0`. *@freq_recurrence_factor* is used only if *@freq_type* is set to `8`, `16`, or `32`.

#### [ @active_start_date = ] *active_start_date*

The date on which job execution can begin. *@active_start_date* is **int**, with a default of `NULL`. The date is formatted as `yyyyMMdd`. If *@active_start_date* is set, the date must be greater than or equal to `19900101`.

After the schedule is created, review the start date and confirm that it's the correct date. For more information, see the section "Scheduling Start Date" in [Create and Attach Schedules to Jobs](../../ssms/agent/create-and-attach-schedules-to-jobs.md).

#### [ @active_end_date = ] *active_end_date*

The date on which job execution can stop. *@active_end_date* is **int**, with a default of `99991231`. The date is formatted as `yyyyMMdd`.

#### [ @active_start_time = ] *active_start_time*

The time on any day between *@active_start_date* and *@active_end_date* to begin job execution. *@active_start_time* is **int**, with a default of `000000`. The time is formatted as `HHmmss` on a 24-hour clock.

#### [ @active_end_time = ] *active_end_time*

The time on any day between *active_start_date* and *@active_end_date* to end job execution. *@active_end_time* is **int**, with a default of `235959`. The time is formatted as `HHmmss` on a 24-hour clock.

#### [ @schedule_id = ] *schedule_id* OUTPUT

Schedule identification number assigned to the schedule if it's created successfully. *@schedule_id* is an OUTPUT parameter of type **int**.

#### [ @automatic_post = ] *automatic_post*

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

#### [ @schedule_uid = ] '*schedule_uid*' OUTPUT

A unique identifier for the schedule. *@schedule_uid* is an OUTPUT parameter of type **uniqueidentifier**.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

Job schedules can now be managed independently of jobs. To add a schedule to a job, use `sp_add_schedule` to create the schedule and `sp_attach_schedule` to attach the schedule to a job.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

Other users must be granted one of the following [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the `msdb` database:

- **SQLAgentUserRole**
- **SQLAgentReaderRole**
- **SQLAgentOperatorRole**

For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).

## Examples

The following example assigns a job schedule to `SaturdayReports`, which executes every Saturday at 2:00 AM.

```sql
EXEC msdb.dbo.sp_add_jobschedule
    @job_name = N'SaturdayReports', -- Job name
    @name = N'Weekly_Sat_2AM', -- Schedule name
    @freq_type = 8, -- Weekly
    @freq_interval = 64, -- Saturday
    @freq_recurrence_factor = 1, -- every week
    @active_start_time = 20000 -- 2:00 AM
```

## Related content

- [Create and Attach Schedules to Jobs](../../ssms/agent/create-and-attach-schedules-to-jobs.md)
- [Schedule a Job](../../ssms/agent/schedule-a-job.md)
- [Create a Schedule](../../ssms/agent/create-a-schedule.md)
- [SQL Server Agent stored procedures (Transact-SQL)](sql-server-agent-stored-procedures-transact-sql.md)
- [sp_add_schedule (Transact-SQL)](sp-add-schedule-transact-sql.md)
- [sp_update_schedule (Transact-SQL)](sp-update-schedule-transact-sql.md)
- [sp_delete_schedule (Transact-SQL)](sp-delete-schedule-transact-sql.md)
- [sp_help_schedule (Transact-SQL)](sp-help-schedule-transact-sql.md)
- [sp_attach_schedule (Transact-SQL)](sp-attach-schedule-transact-sql.md)
