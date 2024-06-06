---
title: "sp_help_schedule (Transact-SQL)"
description: sp_help_schedule lists information about schedules.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/14/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_help_schedule"
  - "sp_help_schedule_TSQL"
helpviewer_keywords:
  - "sp_help_schedule"
dev_langs:
  - "TSQL"
---
# sp_help_schedule (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Lists information about schedules.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_help_schedule
    [ [ @schedule_id = ] schedule_id ]
    [ , [ @schedule_name = ] N'schedule_name' ]
    [ , [ @attached_schedules_only = ] attached_schedules_only ]
    [ , [ @include_description = ] include_description ]
[ ; ]
```

## Arguments

#### [ @schedule_id = ] *schedule_id*

The identifier of the schedule to list. *@schedule_id* is **int**, with a default of `NULL`.

Either *@schedule_id* or *@schedule_name* can be specified.

#### [ @schedule_name = ] N'*schedule_name*'

The name of the schedule to list. *@schedule_name* is **sysname**, with a default of `NULL`.

Either *@schedule_id* or *@schedule_name* can be specified.

#### [ @attached_schedules_only = ] *attached_schedules_only*

Specifies whether to show only schedules that a job is attached to. *@attached_schedules_only* is **bit**, with a default of `0`. When *@attached_schedules_only* is `0`, all schedules are shown. When *@attached_schedules_only* is `1`, the result set contains only schedules that are attached to a job.

#### [ @include_description = ] *include_description*

Specifies whether to include descriptions in the result set. *@include_description* is **bit**, with a default of `0`. When *@include_description* is `0`, the *@schedule_description* column of the result set contains a placeholder. When *@include_description* is `1`, the description of the schedule is included in the result set.

## Return code values

`0` (success) or `1` (failure).

## Result set

This procedure returns the following result set:

| Column name | Data type | Description |
| --- | --- | --- |
| `schedule_id` | **int** | Schedule identifier number. |
| `schedule_uid` | **uniqueidentifier** | Identifier for the schedule. |
| `schedule_name` | **sysname** | Name of the schedule. |
| `enabled` | **int** | Whether the schedule enabled (`1`) or not enabled (`0`). |
| `freq_type` | **int** | Value indicating when the job is to be executed.<br /><br />`1` = Once<br />`4` = Daily<br />`8` = Weekly<br />`16` = Monthly<br />`32` = Monthly, relative to the `freq_interval`<br />`64` = Run when SQLServerAgent service starts. |
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
| `schedule_description` | **nvarchar(4000)** | An English description of the schedule (if requested). |
| `job_count` | **int** | Returns how many jobs reference this schedule. |

## Remarks

When no parameters are provided, `sp_help_schedule` lists information for all schedules in the instance.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

Other users must be granted one of the following [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the `msdb` database:

- **SQLAgentUserRole**
- **SQLAgentReaderRole**
- **SQLAgentOperatorRole**

For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).

Members of **SQLAgentUserRole** can only view the schedules that they own.

## Examples

### A. List information for all schedules in the instance

The following example lists information for all schedules in the instance.

```sql
USE msdb;
GO

EXEC dbo.sp_help_schedule;
GO
```

### B. List information for a specific schedule

The following example lists information for the schedule named `NightlyJobs`.

```sql
USE msdb;
GO

EXEC dbo.sp_help_schedule
    @schedule_name = N'NightlyJobs';
GO
```

## Related content

- [sp_add_schedule (Transact-SQL)](sp-add-schedule-transact-sql.md)
- [sp_attach_schedule (Transact-SQL)](sp-attach-schedule-transact-sql.md)
- [sp_delete_schedule (Transact-SQL)](sp-delete-schedule-transact-sql.md)
- [sp_detach_schedule (Transact-SQL)](sp-detach-schedule-transact-sql.md)
