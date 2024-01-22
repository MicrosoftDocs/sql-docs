---
title: "sp_help_jobs_in_schedule (Transact-SQL)"
description: Returns information about the jobs that a particular schedule is attached to.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/14/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_help_jobs_in_schedule_TSQL"
  - "sp_help_jobs_in_schedule"
helpviewer_keywords:
  - "sp_help_jobs_in_schedule"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_help_jobs_in_schedule (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns information about the jobs that a particular schedule is attached to.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_help_jobs_in_schedule
    [ [ @schedule_name = ] N'schedule_name' ]
    [ , [ @schedule_id = ] schedule_id ]
[ ; ]
```

## Arguments

#### [ @schedule_id = ] *schedule_id*

The identifier of the schedule to list information for. *@schedule_id* is **int**, with a default of `NULL`.

Either *@schedule_id* or *@schedule_name* can be specified.

#### [ @schedule_name = ] N'*schedule_name*'

The name of the schedule to list information for. *@schedule_name* is **sysname**, with a default of `NULL`.

Either *@schedule_id* or *@schedule_name* can be specified.

## Return code values

`0` (success) or `1` (failure).

## Result set

Returns the following result set:

| Column name | Data type | Description |
| --- | --- | --- |
| `job_id` | **uniqueidentifier** | Unique ID of the job. |
| `originating_server` | **nvarchar(30)** | Name of the server from which the job came. |
| `name` | **sysname** | Name of the job. |
| `enabled` | **tinyint** | Indicates whether the job is enabled, so that it can execute. |
| `description` | **nvarchar(512)** | Description for the job. |
| `start_step_id` | **int** | ID of the step in the job where execution should begin. |
| `category` | **sysname** | Job category. |
| `owner` | **sysname** | Job owner. |
| `notify_level_eventlog` | **int** | Bitmask indicating under what circumstances a notification event should be logged to the Microsoft Windows application log. Can be one of these values:<br /><br />`0` = Never<br />`1` = When a job succeeds<br />`2` = When the job fails<br />`3` = Whenever the job completes (regardless of the job outcome) |
| `notify_level_email` | **int** | Bitmask indicating under what circumstances a notification e-mail should be sent when a job completes. Possible values are the same as for `notify_level_eventlog`. |
| `notify_level_netsend` | **int** | Bitmask indicating under what circumstances a network message should be sent when a job completes. Possible values are the same as for `notify_level_eventlog`. |
| `notify_level_page` | **int** | Bitmask indicating under what circumstances a page should be sent when a job completes. Possible values are the same as for `notify_level_eventlog`. |
| `notify_email_operator` | **sysname** | E-mail name of the operator to notify. |
| `notify_netsend_operator` | **sysname** | Name of the computer or user used when sending network messages. |
| `notify_page_operator` | **sysname** | Name of the computer or user used when sending a page. |
| `delete_level` | **int** | Bitmask indicating under what circumstances the job should be deleted when a job completes. Possible values are the same as for `notify_level_eventlog`. |
| `date_created` | **datetime** | Date the job was created. |
| `date_modified` | **datetime** | Date the job was last modified. |
| `version_number` | **int** | Version of the job (automatically updated each time the job is modified). |
| `last_run_date` | **int** | Date the job last started execution. |
| `last_run_time` | **int** | Time the job last started execution. |
| `last_run_outcome` | **int** | Outcome of the job the last time it ran:<br /><br />`0` = Failed<br />`1` = Succeeded<br />`3` = Canceled<br />`5` = Unknown |
| `next_run_date` | **int** | Date the job is scheduled to run next. |
| `next_run_time` | **int** | Time the job is scheduled to run next. |
| `next_run_schedule_id` | **int** | Identification number of the next run schedule. |
| `current_execution_status` | **int** | Current execution status. |
| `current_execution_step` | **sysname** | Current execution step in the job. |
| `current_retry_attempt` | **int** | If the job is running and the step has been retried, this value is the current retry attempt. |
| `has_step` | **int** | Number of job steps the job has. |
| `has_schedule` | **int** | Number of job schedules the job has. |
| `has_target` | **int** | Number of target servers the job has. |
| `type` | **int** | Type of the job:<br /><br />`1` = Local job.<br />`2` = Multiserver job.<br />`0` = Job has no target servers. |

## Remarks

This procedure lists information about jobs attached to the specified schedule.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

Other users must be granted one of the following [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the `msdb` database:

- **SQLAgentUserRole**
- **SQLAgentReaderRole**
- **SQLAgentOperatorRole**

For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).

Members of **SQLAgentUserRole** can only view the status of jobs that they own.

## Examples

The following example lists the jobs attached to the `NightlyJobs` schedule.

```sql
USE msdb;
GO

EXEC sp_help_jobs_in_schedule
    @schedule_name = N'NightlyJobs';
GO
```

## Related content

- [SQL Server Agent stored procedures (Transact-SQL)](sql-server-agent-stored-procedures-transact-sql.md)
- [sp_add_schedule (Transact-SQL)](sp-add-schedule-transact-sql.md)
- [sp_attach_schedule (Transact-SQL)](sp-attach-schedule-transact-sql.md)
- [sp_delete_schedule (Transact-SQL)](sp-delete-schedule-transact-sql.md)
- [sp_detach_schedule (Transact-SQL)](sp-detach-schedule-transact-sql.md)
