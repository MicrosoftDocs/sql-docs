---
title: "sp_help_jobserver (Transact-SQL)"
description: sp_help_jobserver returns information about the server for a given job.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/14/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_help_jobserver"
  - "sp_help_jobserver_TSQL"
helpviewer_keywords:
  - "sp_help_jobserver"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2016 || >=sql-server-linux-2017"
---
# sp_help_jobserver (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns information about the server for a given job.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_help_jobserver
    [ [ @job_id = ] 'job_id' ]
    [ , [ @job_name = ] N'job_name' ]
    [ , [ @show_last_run_details = ] show_last_run_details ]
[ ; ]
```

## Arguments

#### [ @job_id = ] '*job_id*'

The job identification number for which to return information. *@job_id* is **uniqueidentifier**, with a default of `NULL`.

Either *@job_id* or *@job_name* must be specified, but both can't be specified.

#### [ @job_name = ] N'*job_name*'

The job name for which to return information. *@job_name* is **sysname**, with a default of `NULL`.

Either *@job_id* or *@job_name* must be specified, but both can't be specified.

#### [ @show_last_run_details = ] *show_last_run_details*

Whether the last-run execution information is part of the result set. *@show_last_run_details* is **tinyint**, with a default of `0`.

- `0` doesn't include last-run information.
- `1` includes last-run information.

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `server_id` | **int** | Identification number of the target server. |
| `server_name` | **nvarchar(30)** | Computer name of the target server. |
| `enlist_date` | **datetime** | Date the target server enlisted into the master server. |
| `last_poll_date` | **datetime** | Date the target server last polled the master server. |

If `sp_help_jobserver` is executed with *@show_last_run_details* set to `1`, the result set has these extra columns.

| Column name | Data type | Description |
| --- | --- | --- |
| `last_run_date` | **int** | Date the job last started execution on this target server. |
| `last_run_time` | **int** | Time the job last started execution on this server. |
| `last_run_duration` | **int** | Duration of the job the last time it ran on this target server (in seconds). |
| `last_outcome_message` | **nvarchar(1024)** | Describes the last outcome of the job. |
| `last_run_outcome` | **int** | Outcome of the job the last time it ran on this server:<br /><br />`0` = Failed<br />`1` = Succeeded<br />`3` = Canceled<br />`5` = Unknown |

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

Other users must be granted one of the following [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the `msdb` database:

- **SQLAgentUserRole**
- **SQLAgentReaderRole**
- **SQLAgentOperatorRole**

For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).

Members of **SQLAgentUserRole** can only view information for jobs that they own.

## Examples

The following example returns information, including last-run information, about the `NightlyBackups` job.

```sql
USE msdb;
GO

EXEC dbo.sp_help_jobserver
    @job_name = N'NightlyBackups',
    @show_last_run_details = 1;
GO
```

## Related content

- [sp_add_jobserver (Transact-SQL)](sp-add-jobserver-transact-sql.md)
- [sp_delete_jobserver (Transact-SQL)](sp-delete-jobserver-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
