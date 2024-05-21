---
title: "sp_help_jobstep (Transact-SQL)"
description: Returns information for the steps in a job used by SQL Server Agent service to perform automated activities.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/14/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_help_jobstep_TSQL"
  - "sp_help_jobstep"
helpviewer_keywords:
  - "sp_help_jobstep"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_help_jobstep (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns information for the steps in a job used by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent service to perform automated activities.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_help_jobstep
    [ [ @job_id = ] 'job_id' ]
    [ , [ @job_name = ] N'job_name' ]
    [ , [ @step_id = ] step_id ]
    [ , [ @step_name = ] N'step_name' ]
    [ , [ @suffix = ] suffix ]
[ ; ]
```

## Arguments

#### [ @job_id = ] '*job_id*'

The job identification number for which to return job information. *@job_id* is **uniqueidentifier**, with a default of `NULL`.

Either *@job_id* or *@job_name* must be specified, but both can't be specified.

#### [ @job_name = ] N'*job_name*'

The name of the job. *@job_name* is **sysname**, with a default of `NULL`.

Either *@job_id* or *@job_name* must be specified, but both can't be specified.

#### [ @step_id = ] *step_id*

The identification number of the step in the job. If not included, all steps in the job are included. *@step_id* is **int**, with a default of `NULL`.

#### [ @step_name = ] N'*step_name*'

The name of the step in the job. *@step_name* is **sysname**, with a default of `NULL`.

#### [ @suffix = ] *suffix*

A flag indicating whether a text description is appended to the **flags** column in the output. *@suffix* is **bit**, with a default of `0`. If @*suffix* is `1`, a description is appended.

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `step_id` | **int** | Unique identifier for the step. |
| `step_name` | **sysname** | Name of the step in the job. |
| `subsystem` | **nvarchar(40)** | Subsystem in which to execute the step command. |
| `command` | **nvarchar(max)** | Command executed in the step. |
| `flags` | **int** | A bitmask of values that control step behavior. |
| `cmdexec_success_code` | **int** | For a **CmdExec** step, this value is the process exit code of a successful command. |
| `on_success_action` | **tinyint** | Action to take if the step succeeds:<br /><br />`1` = Quit the job reporting success.<br />`2` = Quit the job reporting failure.<br />`3` = Go to the next step.<br />`4` = Go to step. |
| `on_success_step_id` | **int** | If **on_success_action** is 4, this value indicates the next step to execute. |
| `on_fail_action` | **tinyint** | What to do if the step fails. Values are same as `on_success_action`. |
| `on_fail_step_id` | **int** | If `on_fail_action` is `4`, this value indicates the next step to execute. |
| `server` | **sysname** | Reserved. |
| `database_name` | **sysname** | For a [!INCLUDE [tsql](../../includes/tsql-md.md)] step, this value is the database in which the command executes. |
| `database_user_name` | **sysname** | For a [!INCLUDE [tsql](../../includes/tsql-md.md)] step, this value is the database user context in which the command executes. |
| `retry_attempts` | **int** | Maximum number of times the command should be retried (if it's unsuccessful). |
| `retry_interval` | **int** | Interval (in minutes) for any retry attempts. |
| `os_run_priority` | **int** | Reserved. |
| `output_file_name` | **nvarchar(200)** | File to which command output should be written ([!INCLUDE [tsql](../../includes/tsql-md.md)], **CmdExec**, and **PowerShell** steps only). |
| `last_run_outcome` | **int** | Outcome of the step the last time it ran:<br /><br />`0` = Failed<br />`1` = Succeeded<br />`2` = Retry<br />`3` = Canceled<br />`5` = Unknown |
| `last_run_duration` | **int** | Duration (`hhmmss`) of the step the last time it ran. |
| `last_run_retries` | **int** | Number of times the command was retried the last time the step ran. |
| `last_run_date` | **int** | Date the step last started execution. |
| `last_run_time` | **int** | Time the step last started execution. |
| `proxy_id` | **int** | Proxy for the job step. |

## Remarks

`sp_help_jobstep` is in the `msdb` database.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

Other users must be granted one of the following [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the `msdb` database:

- **SQLAgentUserRole**
- **SQLAgentReaderRole**
- **SQLAgentOperatorRole**

For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).

Members of **SQLAgentUserRole** can only view job steps for jobs that they own.

## Examples

### A. Return information for all steps in a specific job

The following example returns all the job steps for the job named `Weekly Sales Data Backup`.

```sql
USE msdb;
GO

EXEC dbo.sp_help_jobstep
    @job_name = N'Weekly Sales Data Backup';
GO
```

### B. Return information about a specific job step

The following example returns information about the first job step for the job named `Weekly Sales Data Backup`.

```sql
USE msdb;
GO

EXEC dbo.sp_help_jobstep
    @job_name = N'Weekly Sales Data Backup',
    @step_id = 1;
GO
```

## Related content

- [sp_add_jobstep (Transact-SQL)](sp-add-jobstep-transact-sql.md)
- [sp_delete_jobstep (Transact-SQL)](sp-delete-jobstep-transact-sql.md)
- [sp_help_job (Transact-SQL)](sp-help-job-transact-sql.md)
- [sp_update_jobstep (Transact-SQL)](sp-update-jobstep-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
