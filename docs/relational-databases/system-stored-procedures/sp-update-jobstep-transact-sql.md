---
title: "sp_update_jobstep (Transact-SQL)"
description: "sp_update_jobstep changes the settings for a step in a job in the SQL Server Agent service."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest, wiassaf
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_update_jobstep"
  - "sp_update_jobstep_TSQL"
helpviewer_keywords:
  - "sp_update_jobstep"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_update_jobstep (Transact-SQL)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Changes the setting for a step in a job that is used to perform automated activities in the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent service.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_update_jobstep
    [ [ @job_id = ] 'job_id' ]
    [ , [ @job_name = ] N'job_name' ]
    , [ @step_id = ] step_id
    [ , [ @step_name = ] N'step_name' ]
    [ , [ @subsystem = ] N'subsystem' ]
    [ , [ @command = ] N'command' ]
    [ , [ @additional_parameters = ] N'additional_parameters' ]
    [ , [ @cmdexec_success_code = ] cmdexec_success_code ]
    [ , [ @on_success_action = ] on_success_action ]
    [ , [ @on_success_step_id = ] on_success_step_id ]
    [ , [ @on_fail_action = ] on_fail_action ]
    [ , [ @on_fail_step_id = ] on_fail_step_id ]
    [ , [ @server = ] N'server' ]
    [ , [ @database_name = ] N'database_name' ]
    [ , [ @database_user_name = ] N'database_user_name' ]
    [ , [ @retry_attempts = ] retry_attempts ]
    [ , [ @retry_interval = ] retry_interval ]
    [ , [ @os_run_priority = ] os_run_priority ]
    [ , [ @output_file_name = ] N'output_file_name' ]
    [ , [ @flags = ] flags ]
    [ , [ @proxy_id = ] proxy_id ]
    [ , [ @proxy_name = ] N'proxy_name' ]
[ ; ]
```

## Arguments

#### [ @job_id = ] '*job_id*'

The identification number of the job to which the step belongs. *@job_id* is **uniqueidentifier**, with a default of `NULL`.

Either *@job_id* or *@job_name* must be specified, but both can't be specified.

#### [ @job_name = ] N'*job_name*'

The name of the job to which the step belongs. *@job_name* is **sysname**, with a default of `NULL`.

Either *@job_id* or *@job_name* must be specified, but both can't be specified.

#### [ @step_id = ] *step_id*

The identification number for the job step to be modified. *@step_id* is **int**, with no default. This number can't be changed.

#### [ @step_name = ] N'*step_name*'

A new name for the step. *@step_name* is **sysname**, with a default of `NULL`.

#### [ @subsystem = ] N'*subsystem*'

The subsystem used by [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent to execute *@command*. *@subsystem* is **nvarchar(40)**, with a default of `NULL`.

#### [ @command = ] N'*command*'

The command(s) to be executed through *@subsystem*. *@command* is **nvarchar(max)**, with a default of `NULL`.

#### [ @additional_parameters = ] N'*additional_parameters*'

[!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)]

#### [ @cmdexec_success_code = ] *cmdexec_success_code*

The value returned by a **CmdExec** subsystem command to indicate that *command* executed successfully. *@cmdexec_success_code* is **int**, with a default of `NULL`.

#### [ @on_success_action = ] *on_success_action*

The action to perform if the step succeeds. *@on_success_action* is **tinyint**, with a default of `NULL`, and can be one of these values.

| Value | Description (action) |
| --- | --- |
| `1` | Quit with success |
| `2` | Quit with failure |
| `3` | Go to next step |
| `4` | Go to step *@on_success_step_id* |

#### [ @on_success_step_id = ] *on_success_step_id*

The identification number of the step in this job to execute if step succeeds and *@on_success_action* is `4`. *@on_success_step_id* is **int**, with a default of `NULL`.

#### [ @on_fail_action = ] *on_fail_action*

The action to perform if the step fails. *@on_fail_action* is **tinyint**, and can have one of these values.

| Value | Description (action) |
| --- | --- |
| `1` | Quit with success |
| `2` | Quit with failure |
| `3` | Go to next step |
| `4` | Go to step *@on_fail_step_id* |

#### [ @on_fail_step_id = ] *on_fail_step_id*

The identification number of the step in this job to execute if the step fails and *@on_fail_action* is `4`. *@on_fail_step_id* is **int**, with a default of `NULL`.

#### [ @server = ] N'*server*'

[!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)]

#### [ @database_name = ] N'*database_name*'

The name of the database in which to execute a [!INCLUDE [tsql](../../includes/tsql-md.md)] step. *@database_name* is **sysname**, with a default of `NULL`. Names that are enclosed in brackets (`[]`) aren't allowed.

#### [ @database_user_name = ] N'*database_user_name*'

The name of the database user to use when executing a [!INCLUDE [tsql](../../includes/tsql-md.md)] step. *@database_user_name* is **sysname**, with a default of `NULL`.

#### [ @retry_attempts = ] *retry_attempts*

The number of retry attempts to use if this step fails. *@retry_attempts* is **int**, with a default of `NULL`.

#### [ @retry_interval = ] *retry_interval*

The amount of time in minutes between retry attempts. *@retry_interval* is **int**, with a default of `NULL`.

#### [ @os_run_priority = ] *os_run_priority*

[!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)]

#### [ @output_file_name = ] N'*output_file_name*'

The name of the file in which the output of this step is saved. *@output_file_name* is **nvarchar(200)**, with a default of `NULL`. This parameter is only valid with commands running in [!INCLUDE [tsql](../../includes/tsql-md.md)] or **CmdExec** subsystems.

To set *@output_file_name* back to `NULL`, you must set *@output_file_name* to an empty string, or to a string of blank characters, but you can't use the `CHAR(32)` function.

For example, set this argument to an empty string as follows:

`@output_file_name = ' '`

#### [ @flags = ] *flags*

An option that controls behavior. *@flags* is **int**, with a default of `NULL`.

| Value | Description |
| --- | --- |
| `0` (default) | Overwrite output file |
| `2` | Append to output file |
| `4` | Write Transact-SQL job step output to step history |
| `8` | Write log to table (overwrite existing history) |
| `16` | Write log to table (append to existing history) |

#### [ @proxy_id = ] *proxy_id*

The ID number of the proxy that the job step runs as. *@proxy_id* is **int**, with a default of `NULL`. If no *@proxy_id* is specified, no *@proxy_name* is specified, and no *@database_user_name* is specified, the job step runs as the service account for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent.

#### [ @proxy_name = ] N'*proxy_name*'

The name of the proxy that the job step runs as. *@proxy_name* is **sysname**, with a default of `NULL`. If no *@proxy_id* is specified, no *@proxy_name* is specified, and no *@database_user_name* is specified, the job step runs as the service account for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_update_jobstep` must be run from the `msdb` database.

Updating a job step increments the job version number.

This stored procedure shares the name of `sp_update_jobstep` with a similar object for the [Azure Elastic Jobs service for Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true). For information about the elastic jobs version, see [jobs.sp_update_jobstep (Azure Elastic Jobs)](sp-update-jobstep-elastic-jobs-transact-sql.md?view=azuresqldb-current&preserve-view=true).

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

Other users must be granted one of the following [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the `msdb` database:

- **SQLAgentUserRole**
- **SQLAgentReaderRole**
- **SQLAgentOperatorRole**

For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).

Only members of **sysadmin** can update a job step of a job owned by another user.

If the job step requires access to a proxy, the creator of the job step must have access to the proxy for the job step. All subsystems, except Transact-SQL, require a proxy account. Members of **sysadmin** have access to all proxies, and can use the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent service account for the proxy.

## Examples

The following example changes the number of retry attempts for the first step of the `Weekly Sales Data Backup` job. After you run this example, the number of retry attempts is `10`.

```sql
USE msdb;
GO

EXEC dbo.sp_update_jobstep
    @job_name = N'Weekly Sales Data Backup',
    @step_id = 1,
    @retry_attempts = 10;
GO
```

## Related content

- [View or Modify Jobs](../../ssms/agent/view-or-modify-jobs.md)
- [sp_delete_jobstep (Transact-SQL)](sp-delete-jobstep-transact-sql.md)
- [sp_help_jobstep (Transact-SQL)](sp-help-jobstep-transact-sql.md)
- [SQL Server Agent stored procedures (Transact-SQL)](sql-server-agent-stored-procedures-transact-sql.md)
