---
title: "sp_help_jobactivity (Transact-SQL)"
description: Lists information about the runtime state of SQL Server Agent jobs.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/14/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_help_jobactivity_TSQL"
  - "sp_help_jobactivity"
helpviewer_keywords:
  - "sp_help_jobactivity"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_help_jobactivity (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Lists information about the runtime state of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_help_jobactivity
    [ [ @job_id = ] 'job_id' ]
    [ , [ @job_name = ] N'job_name' ]
    [ , [ @session_id = ] session_id ]
[ ; ]
```

## Arguments

#### [ @job_id = ] '*job_id*'

The job identification number. *@job_id* is **uniqueidentifier**, with a default of `NULL`.

Either *@job_id* or *@job_name* must be specified, but both can't be specified.

#### [ @job_name = ] N'*job_name*'

The name of the job. *@job_name* is **sysname**, with a default of `NULL`.

Either *@job_id* or *@job_name* must be specified, but both can't be specified.

#### [ @session_id = ] *session_id*

The session ID to report information about. *@session_id* is **int**, with a default of `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Result set

Returns the following result set:

| Column name | Data type | Description |
| --- | --- | --- |
| `session_id` | **int** | Agent session identification number. |
| `job_id` | **uniqueidentifier** | Identifier for the job. |
| `job_name` | **sysname** | Name of the job. |
| `run_requested_date` | **datetime** | When that the job was requested to run. |
| `run_requested_source` | **sysname** | The source of the request to run the job. One of:<br /><br />`1` = Run on a schedule<br />`2` = Run in response to an alert<br />`3` = Run on startup<br />`4` = Run by user<br />`6` = Run on CPU idle schedule |
| `queued_date` | **datetime** | Specifies when the request was queued. `NULL` if the job was run directly. |
| `start_execution_date` | **datetime** | When the job was assigned to a runnable thread. |
| `last_executed_step_id` | **int** | The step ID of the most recently run job step. |
| `last_executed_step_date` | **datetime** | The time that the most recently run job step started to run. |
| `stop_execution_date` | **datetime** | The time that the job stopped running. |
| `next_scheduled_run_date` | **datetime** | When the job is next scheduled to run. |
| `job_history_id` | **int** | Identifier for the job history in the job history table. |
| `message` | **nvarchar(1024)** | Message produced during the last run of the job. |
| `run_status` | **int** | Status returned from the last run of the job:<br /><br />`0` = Error failed<br />`1` = Succeeded<br />`3` = Canceled<br />`5` = Status unknown |
| `operator_id_emailed` | **int** | ID number of the operator notified via email at completion of the job. |
| `operator_id_netsent` | **int** | ID number of the operator notified via **net send** at completion of the job. |
| `operator_id_paged` | **int** | ID number of the operator notified via pager at completion of the job. |

## Remarks

This procedure provides a snapshot of the current state of the jobs. The results returned represent information at the time that the request is processed.

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent creates a session ID each time that the Agent service starts. The session ID is stored in the table `msdb`.dbo.syssessions**.

When no *@session_id* is provided, lists information about the most recent session.

When no *@job_name* or *@job_id* is provided, lists information for all jobs.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

Other users must be granted one of the following [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the `msdb` database:

- **SQLAgentUserRole**
- **SQLAgentReaderRole**
- **SQLAgentOperatorRole**

For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).

Only members of **sysadmin** can view the activity for jobs owned by other users.

## Examples

The following example lists activity for all jobs that the current user has permission to view.

```sql
USE msdb;
GO

EXEC dbo.sp_help_jobactivity;
GO
```

## Related content

- [SQL Server Agent stored procedures (Transact-SQL)](sql-server-agent-stored-procedures-transact-sql.md)
