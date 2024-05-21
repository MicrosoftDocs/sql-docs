---
title: "sp_help_jobschedule (Transact-SQL)"
description: Returns information about the scheduling of jobs used by SQL Server Management Studio to perform automated activities.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/14/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_help_jobschedule"
  - "sp_help_jobschedule_TSQL"
helpviewer_keywords:
  - "sp_help_jobschedule"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_help_jobschedule (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns information about the scheduling of jobs used by [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to perform automated activities.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_help_jobschedule
    [ [ @job_id = ] 'job_id' ]
    [ , [ @job_name = ] N'job_name' ]
    [ , [ @schedule_name = ] N'schedule_name' ]
    [ , [ @schedule_id = ] schedule_id ]
    [ , [ @include_description = ] include_description ]
[ ; ]
```

## Arguments

#### [ @job_id = ] '*job_id*'

The job identification number. *@job_id* is **uniqueidentifier**, with a default of `NULL`.

Either *@job_id* or *@job_name* must be specified, but both can't be specified.

#### [ @job_name = ] N'*job_name*'

The name of the job. *@job_name* is **sysname**, with a default of `NULL`.

Either *@job_id* or *@job_name* must be specified, but both can't be specified.

#### [ @schedule_name = ] N'*schedule_name*'

The name of the schedule item for the job. *@schedule_name* is **sysname**, with a default of `NULL`.

#### [ @schedule_id = ] *schedule_id*

The identification number of the schedule item for the job. *@schedule_id* is **int**, with a default of `NULL`.

#### [ @include_description = ] *include_description*

Specifies whether to include the description of the schedule in the result set. *@include_description* is **bit**, with a default of `0`.

- When `0`, the description of the schedule isn't included in the result set.
- When `1`, the description of the schedule is included in the result set.

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `schedule_id` | **int** | Schedule identifier number. |
| `schedule_name` | **sysname** | Name of the schedule. |
| `enabled` | **int** | Whether the schedule enabled (`1`) or not enabled (`0`). |
| `freq_type` | **int** | Value indicating when the job is to be executed.<br /><br />`1` = Once<br />`4` = Daily<br />`8` = Weekly<br />`16` = Monthly<br />`32` = Monthly, relative to the `freq_interval`<br />`64` = Run when [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent service starts. |
| `freq_interval` | **int** | Days when the job is executed. The value depends on the value of `freq_type`. For more information, see [sp_add_schedule](sp-add-schedule-transact-sql.md). |
| `freq_subday_type` | **int** | Units for `freq_subday_interval`. For more information, see [sp_add_schedule](sp-add-schedule-transact-sql.md). |
| `freq_subday_interval` | **int** | Number of `freq_subday_type` periods to occur between each execution of the job. For more information, see [sp_add_schedule](sp-add-schedule-transact-sql.md). |
| `freq_relative_interval` | **int** | Scheduled job's occurrence of the `freq_interval` in each month. For more information, see [sp_add_schedule](sp-add-schedule-transact-sql.md). |
| `freq_recurrence_factor` | **int** | Number of months between the scheduled execution of the job. |
| `active_start_date` | **int** | Date the schedule is activated. |
| `active_end_date` | **int** | End date of the schedule. |
| `active_start_time` | **int** | Time of the day the schedule starts. |
| `active_end_time` | **int** | Time of the day schedule ends. |
| `date_created` | **datetime** | Date the schedule is created. |
| `schedule_description` | **nvarchar(4000)** | An English description of the schedule that is derived from values in `msdb.dbo.sysschedules`. When *@include_description* is `0`, this column contains text stating that the description wasn't requested. |
| `next_run_date` | **int** | Date the schedule next causes the job to run. |
| `next_run_time` | **int** | Time the schedule next causes the job to run. |
| `schedule_uid` | **uniqueidentifier** | Identifier for the schedule. |
| `job_count` | **int** | Count of jobs returned. |

> [!NOTE]  
> `sp_help_jobschedule` returns values from the `dbo.sysjobschedules` and `dbo.sysschedules` system tables in `msdb.sysjobschedules` updates every 20 minutes. This might affect the values that are returned by this stored procedure.

## Remarks

The parameters of `sp_help_jobschedule` can be used only in certain combinations. If *@schedule_id* is specified, *@job_id* and *@job_name* can't be specified. Otherwise, the *@job_id* or *@job_name* parameters can be used with *@schedule_name*.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

Other users must be granted one of the following [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the `msdb` database:

- **SQLAgentUserRole**
- **SQLAgentReaderRole**
- **SQLAgentOperatorRole**

For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).

Members of **SQLAgentUserRole** can only view properties of job schedules that they own.

## Examples

### A. Return the job schedule for a specific job

The following example returns the scheduling information for a job named `BackupDatabase`.

```sql
USE msdb;
GO

EXEC dbo.sp_help_jobschedule
    @job_name = N'BackupDatabase' ;
GO
```

### B. Return the job schedule for a specific schedule

The following example returns the information for the schedule named `NightlyJobs` and the job named `RunReports`.

```sql
USE msdb;
GO

EXEC dbo.sp_help_jobschedule
    @job_name = N'RunReports',
    @schedule_name = N'NightlyJobs';
GO
```

### C. Return the job schedule and schedule description for a specific schedule

The following example returns the information for the schedule named `NightlyJobs` and the job named `RunReports`. The result set returned includes a description of the schedule.

```sql
USE msdb;
GO

EXEC dbo.sp_help_jobschedule
    @job_name = N'RunReports',
    @schedule_name = N'NightlyJobs',
    @include_description = 1;
GO
```

## Related content

- [sp_add_schedule (Transact-SQL)](sp-add-schedule-transact-sql.md)
- [sp_delete_schedule (Transact-SQL)](sp-delete-schedule-transact-sql.md)
- [sp_update_schedule (Transact-SQL)](sp-update-schedule-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
