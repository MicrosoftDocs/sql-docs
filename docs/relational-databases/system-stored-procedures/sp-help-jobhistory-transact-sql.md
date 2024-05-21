---
title: "sp_help_jobhistory (Transact-SQL)"
description: Provides information about the jobs for servers in the multiserver administration domain.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/14/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_help_jobhistory_TSQL"
  - "sp_help_jobhistory"
helpviewer_keywords:
  - "sp_help_jobhistory"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2016 || >=sql-server-linux-2017"
---
# sp_help_jobhistory (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Provides information about the jobs for servers in the multiserver administration domain.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_help_jobhistory
    [ [ @job_id = ] 'job_id' ]
    [ , [ @job_name = ] N'job_name' ]
    [ , [ @step_id = ] step_id ]
    [ , [ @sql_message_id = ] sql_message_id ]
    [ , [ @sql_severity = ] sql_severity ]
    [ , [ @start_run_date = ] start_run_date ]
    [ , [ @end_run_date = ] end_run_date ]
    [ , [ @start_run_time = ] start_run_time ]
    [ , [ @end_run_time = ] end_run_time ]
    [ , [ @minimum_run_duration = ] minimum_run_duration ]
    [ , [ @run_status = ] run_status ]
    [ , [ @minimum_retries = ] minimum_retries ]
    [ , [ @oldest_first = ] oldest_first ]
    [ , [ @server = ] N'server' ]
    [ , [ @mode = ] 'mode' ]
[ ; ]
```

## Arguments

#### [ @job_id = ] '*job_id*'

The job identification number. *@job_id* is **uniqueidentifier**, with a default of `NULL`.

#### [ @job_name = ] N'*job_name*'

The name of the job. *@job_name* is **sysname**, with a default of `NULL`.

#### [ @step_id = ] *step_id*

The step identification number. *@step_id* is **int**, with a default of `NULL`.

#### [ @sql_message_id = ] *sql_message_id*

The identification number of the error message returned by [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] when executing the job. *@sql_message_id* is **int**, with a default of `NULL`.

#### [ @sql_severity = ] *sql_severity*

The severity level of the error message returned by [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] when executing the job. *@sql_severity* is **int**, with a default of `NULL`.

#### [ @start_run_date = ] *start_run_date*

The date the job was started. *@start_run_date* is **int**, with a default of `NULL`. *@start_run_date* must be entered in the form `yyyyMMdd`, where `yyyy` is a four-character year, `MM` is a two-character month name, and `dd` is a two-character day name.

#### [ @end_run_date = ] *end_run_date*

The date the job was completed. *@end_run_date* is **int**, with a default of `NULL`. *@end_run_date* must be entered in the form `yyyyMMdd`, where `yyyy` is a four-character year, `MM` is a two-character month name, and `dd` is a two-character day name.

#### [ @start_run_time = ] *start_run_time*

The time the job was started. *@start_run_time* is **int**, with a default of `NULL`. *@start_run_time* must be entered in the form `HHmmss`, where `HH` is a two-character hour of the day, `mm` is a two-character minute of the day, and `ss` is a two-character second of the day.

#### [ @end_run_time = ] *end_run_time*

The time the job completed its execution. *@end_run_time* is **int**, with a default of `NULL`. *@end_run_time* must be entered in the form `HHmmss`, where `HH` is a two-character hour of the day, `mm` is a two-character minute of the day, and `ss` is a two-character second of the day.

#### [ @minimum_run_duration = ] *minimum_run_duration*

The minimum length of time for the completion of the job. *@minimum_run_duration* is **int**, with a default of `NULL`. *@minimum_run_duration* must be entered in the form `HHmmss`, where `HH` is a two-character hour of the day, `mm` is a two-character minute of the day, and `ss` is a two-character second of the day.

#### [ @run_status = ] *run_status*

The execution status of the job.*@run_status* is **int**, and can be one of these values.

| Value | Description |
| --- | --- |
| `0` | Failed |
| `1` | Succeeded |
| `2` | Retry (step only) |
| `3` | Canceled |
| `4` | In-progress message |
| `5` | Unknown |

#### [ @minimum_retries = ] *minimum_retries*

The minimum number of times a job should retry running. *@minimum_retries* is **int**, with a default of `NULL`.

#### [ @oldest_first = ] *oldest_first*

Whether to present the output with the oldest jobs first. *@oldest_first* is **int**, with a default of `0`.

- `0` presents the newest jobs first.
- `1` presents the oldest jobs first.

#### [ @server = ] N'*server*'

The name of the server on which the job was performed. *@server* is **sysname**, with a default of `NULL`.

#### [ @mode = ] '*mode*'

Specifies whether [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] prints all columns in the result set (`FULL`) or a summary (`SUMMARY`) of the columns. *@mode* is **varchar(7)**, with a default of `SUMMARY`.

## Return code values

`0` (success) or `1` (failure).

## Result set

The actual column list depends on the value of *@mode*. The most comprehensive set of columns is shown in the following table, and is returned when *@mode* is `FULL`.

| Column name | Data type | Description |
| --- | --- | --- |
| `instance_id` | **int** | History entry identification number. |
| `job_id` | **uniqueidentifier** | Job identification number. |
| `job_name` | **sysname** | Job name. |
| `step_id` | **int** | Step identification number (`0` for a job history). |
| `step_name` | **sysname** | Step name (`NULL` for a job history). |
| `sql_message_id` | **int** | For a [!INCLUDE [tsql](../../includes/tsql-md.md)] step, the most recent [!INCLUDE [tsql](../../includes/tsql-md.md)] error number encountered while running the command. |
| `sql_severity` | **int** | For a [!INCLUDE [tsql](../../includes/tsql-md.md)] step, the highest [!INCLUDE [tsql](../../includes/tsql-md.md)] error severity encountered while running the command. |
| `message` | **nvarchar(1024)** | Job or step history message. |
| `run_status` | **int** | Outcome of the job or step. |
| `run_date` | **int** | Date the job or step began executing. |
| `run_time` | **int** | Time the job or step began executing. |
| `run_duration` | **int** | Elapsed time in the execution of the job or step in `HHmmss` format. |
| `operator_emailed` | **nvarchar(20)** | Operator who was e-mailed regarding this job (is `NULL` for step history). |
| `operator_netsent` | **nvarchar(20)** | Operator who was sent a network message regarding this job (is `NULL` for step history). |
| `operator_paged` | **nvarchar(20)** | Operator who was paged regarding this job (is `NULL` for step history). |
| `retries_attempted` | **int** | Number of times the step was retried (always 0 for a job history). |
| `server` | **nvarchar(30)** | Server the step or job executes on. Is always (`local`). |

## Remarks

`sp_help_jobhistory` returns a report with the history of the specified scheduled jobs. If no parameters are specified, the report contains the history for all scheduled jobs.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

Other users must be granted one of the following [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the `msdb` database:

- **SQLAgentUserRole**
- **SQLAgentReaderRole**
- **SQLAgentOperatorRole**

For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).

Members of the **SQLAgentUserRole** database role can only view the history for jobs that they own.

## Examples

### A. List all job information for a job

The following example lists all job information for the `NightlyBackups` job.

```sql
USE msdb;
GO

EXEC dbo.sp_help_jobhistory
    @job_name = N'NightlyBackups';
GO
```

### B. List information for jobs that match certain conditions

The following example prints all columns and all job information for any failed jobs and failed job steps with an error message of `50100` (a user-defined error message) and a severity of `20`.

```sql
USE msdb;
GO

EXEC dbo.sp_help_jobhistory
    @sql_message_id = 50100,
    @sql_severity = 20,
    @run_status = 0,
    @mode = N'FULL';
GO
```

## Related content

- [sp_purge_jobhistory (Transact-SQL)](sp-purge-jobhistory-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
