---
title: sp_add_jobstep (Transact-SQL)
description: "sp_add_jobstep adds a step (operation) to a SQL Server Agent job."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/02/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_add_jobstep_TSQL"
  - "sp_add_jobstep"
helpviewer_keywords:
  - "sp_add_jobstep"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_add_jobstep (Transact-SQL)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Adds a step (operation) to a [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent job.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent job types are supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

## Syntax

```syntaxsql
sp_add_jobstep
    [ [ @job_id = ] 'job_id' ]
    [ , [ @job_name = ] N'job_name' ]
    [ , [ @step_id = ] step_id ]
    , [ @step_name = ] N'step_name'
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
    [ , [ @step_uid = ] 'step_uid' OUTPUT ]
[ ; ]
```

## Arguments

#### [ @job_id = ] '*job_id*'

The identification number of the job to which to add the step. *@job_id* is **uniqueidentifier**, with a default of `NULL`.

Either *@job_id* or *@job_name* must be specified, but both can't be specified.

#### [ @job_name = ] N'*job_name*'

The name of the job to which to add the step. *@job_name* is **sysname**, with a default of `NULL`.

Either *@job_id* or *@job_name* must be specified, but both can't be specified.

#### [ @step_id = ] *step_id*

The sequence identification number for the job step. *@step_id* is **int**, with a default of `NULL`. Step identification numbers start at `1` and increment without gaps. If a step is inserted in the existing sequence, the sequence numbers are adjusted automatically. A value is provided if *@step_id* isn't specified.

#### [ @step_name = ] N'*step_name*'

The name of the step. *@step_name* is **sysname**, with no default.

#### [ @subsystem = ] N'*subsystem*'

The subsystem used by the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent service to execute *@command*. *@subsystem* is **nvarchar(40)**, and can be one of these values.

| Value | Description |
| --- | --- |
| `ActiveScripting` | Active Script<br /><br />**Important:** [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] |
| `CmdExec` | Operating-system command or executable program |
| `Distribution` | Replication Distribution Agent job |
| `Snapshot` | Replication Snapshot Agent job |
| `LogReader` | Replication Log Reader Agent job |
| `Merge` | Replication Merge Agent job |
| `QueueReader` | Replication Queue Reader Agent job |
| `ANALYSISQUERY` | Analysis Services query (MDX, DMX) |
| `ANALYSISCOMMAND` | Analysis Services command (XMLA) |
| `SSIS` | [!INCLUDE [ssISnoversion](../../includes/ssisnoversion-md.md)] package execution |
| `PowerShell` | PowerShell Script |
| `TSQL` (default) | [!INCLUDE [tsql](../../includes/tsql-md.md)] statement |

#### [ @command = ] N'*command*'

The commands to be executed by the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent service through *@subsystem*. *@command* is **nvarchar(max)**, with a default of `NULL`. [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent provides token substitution, which gives you the same flexibility that variables provide when you write software programs.

An escape macro must accompany all tokens used in job steps, or else those job steps fail. In addition, you must now enclose token names in parentheses and place a dollar sign (`$`) at the beginning of the token syntax. For example: `$(ESCAPE_<macro name>(DATE))`.

For more information about these tokens and updating your job steps to use the new token syntax, see [Use Tokens in Job Steps](../../ssms/agent/use-tokens-in-job-steps.md).

Any Windows user with write permissions on the Windows Event Log can access job steps that are activated by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent alerts or WMI alerts. To avoid this security risk, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent tokens that can be used in jobs activated by alerts are disabled by default. These tokens are: `A-DBN`, `A-SVR`, `A-ERR`, `A-SEV`, `A-MSG`, and `WMI(<property>)`. In this release, use of tokens is extended to all alerting.

If you need to use these tokens, first ensure that only members of trusted Windows security groups, such as the Administrators group, have write permissions on the Event Log of the computer where [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] resides. Then, right-click **SQL Server Agent** in Object Explorer, select **Properties**, and on the **Alert System** page, select **Replace tokens for all job responses to alerts** to enable these tokens.

#### [ @additional_parameters = ] N'*additional_parameters*'

[!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)]

#### [ @cmdexec_success_code = ] *cmdexec_success_code*

The value returned by a `CmdExec` subsystem command to indicate that *@command* executed successfully. *@cmdexec_success_code* is **int**, with a default of `0`.

#### [ @on_success_action = ] *on_success_action*

The action to perform if the step succeeds. *@on_success_action* is **tinyint**, and can be one of these values.

| Value | Description (action) |
| --- | --- |
| `1` (default) | Quit with success |
| `2` | Quit with failure |
| `3` | Go to next step |
| `4` | Go to step *@on_success_step_id* |

#### [ @on_success_step_id = ] *on_success_step_id*

The ID of the step in this job to execute if the step succeeds and *@on_success_action* is `4`. *@on_success_step_id* is **int**, with a default of `0`.

#### [ @on_fail_action = ] *on_fail_action*

The action to perform if the step fails. *@on_fail_action* is **tinyint**, and can be one of these values.

| Value | Description (action) |
| --- | --- |
| `1` | Quit with success |
| `2` (default) | Quit with failure |
| `3` | Go to next step |
| `4` | Go to step *@on_fail_step_id* |

#### [ @on_fail_step_id = ] *on_fail_step_id*

The ID of the step in this job to execute if the step fails and *@on_fail_action* is `4`. *@on_fail_step_id* is **int**, with a default of `0`.

#### [ @server = ] N'*server*'

[!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)]

#### [ @database_name = ] N'*database_name*'

The name of the database in which to execute a [!INCLUDE [tsql](../../includes/tsql-md.md)] step. *@database_name* is **sysname**, with a default of `NULL`, in which case the `master` database is used. Names that are enclosed in brackets (`[]`) aren't allowed. For an ActiveX job step, the *@database_name* is the name of the scripting language that the step uses.

#### [ @database_user_name = ] N'*database_user_name*'

The name of the user account to use when executing a [!INCLUDE [tsql](../../includes/tsql-md.md)] step. *@database_user_name* is **sysname**, with a default of `NULL`. When *@database_user_name* is `NULL`, the step runs in the job owner's user context on *@database_name*. [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent includes this parameter only if the job owner is a [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] sysadmin. If so, the given Transact-SQL step is executed in the context of the given [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] user name. If the job owner isn't a [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] sysadmin, then the Transact-SQL step is always executed in the context of the login that owns this job, and the *@database_user_name* parameter is ignored.

#### [ @retry_attempts = ] *retry_attempts*

The number of retry attempts to use if this step fails. *@retry_attempts* is **int**, with a default of `0`, which indicates no retry attempts.

#### [ @retry_interval = ] *retry_interval*

The amount of time in minutes between retry attempts. *@retry_interval* is **int**, with a default of `0`, which indicates a `0`-minute interval.

#### [ @os_run_priority = ] *os_run_priority*

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

#### [ @output_file_name = ] N'*output_file_name*'

The name of the file in which the output of this step is saved. *@output_file_name* is **nvarchar(200)**, with a default of `NULL`. *@output_file_name* can include one or more of the tokens listed under *@command*. This parameter is valid only with commands running on the [!INCLUDE [tsql](../../includes/tsql-md.md)], `CmdExec`, `PowerShell`, [!INCLUDE [ssISnoversion](../../includes/ssisnoversion-md.md)], or [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)] subsystems.

#### [ @flags = ] *flags*

An option that controls behavior. *@flags* is **int**, and can be one of these values.

| Value | Description |
| --- | --- |
| `0` (default) | Overwrite output file |
| `2` | Append to output file |
| `4` | Write [!INCLUDE [tsql](../../includes/tsql-md.md)] job step output to step history |
| `8` | Write log to table (overwrite existing history) |
| `16` | Write log to table (append to existing history) |
| `32` | Write all output to job history |
| `64` | Create a Windows event to use as a signal for the `cmd` job step to abort |

#### [ @proxy_id = ] *proxy_id*

The ID number of the proxy that the job step runs as. *@proxy_id* is **int**, with a default of `NULL`. If no *@proxy_id* is specified, no *@proxy_name* is specified, and no *@database_user_name* is specified, the job step runs as the service account for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent.

#### [ @proxy_name = ] N'*proxy_name*'

The name of the proxy that the job step runs as. *@proxy_name* is **sysname**, with a default of `NULL`. If no *@proxy_id* is specified, no *@proxy_name* is specified, and no *@database_user_name* is specified, the job step runs as the service account for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent.

#### [ @step_uid = ] '*step_uid*' OUTPUT

*@step_uid* is an OUTPUT parameter of type **uniqueidentifier**.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

`sp_add_jobstep` must be run from the `msdb` database.

SQL Server Management Studio provides an easy, graphical way to manage jobs, and is the recommended way to create and manage the job infrastructure.

By default, a job step runs as the service account for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent unless another proxy is specified. A requirement of this account is to be a member of the **sysadmin** fixed security role.

A proxy might be identified by *@proxy_name* or *@proxy_id*.

This stored procedure shares the name of `sp_add_jobstep` with a similar object for the [Azure Elastic Jobs service for Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true). For information about the elastic jobs version, see [jobs.sp_add_jobstep (Azure Elastic Jobs) (Transact-SQL)](sp-add-jobstep-elastic-jobs-transact-sql.md?view=azuresql-db&preserve-view=true).

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

Other users must be granted one of the following [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the `msdb` database:

- **SQLAgentUserRole**
- **SQLAgentReaderRole**
- **SQLAgentOperatorRole**

For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).

The creator of the job step must have access to the proxy for the job step. Members of the **sysadmin** fixed server role have access to all proxies. Other users must be explicitly granted access to a proxy.

## Examples

The following example creates a job step that changes database access to read-only for the Sales database. In addition, this example specifies five retry attempts, with each retry to occur after a 5-minute wait.

> [!NOTE]  
> This example assumes that the `Weekly Sales Data Backup` job already exists.

```sql
USE msdb;
GO
EXEC sp_add_jobstep
    @job_name = N'Weekly Sales Data Backup',
    @step_name = N'Set database to read only',
    @subsystem = N'TSQL',
    @command = N'ALTER DATABASE SALES SET READ_ONLY',
    @retry_attempts = 5,
    @retry_interval = 5;
GO
```

## Related content

- [View or Modify Jobs](../../ssms/agent/view-or-modify-jobs.md)
- [sp_add_job (Transact-SQL)](sp-add-job-transact-sql.md)
- [sp_add_schedule (Transact-SQL)](sp-add-schedule-transact-sql.md)
- [sp_delete_jobstep (Transact-SQL)](sp-delete-jobstep-transact-sql.md)
- [sp_help_job (Transact-SQL)](sp-help-job-transact-sql.md)
- [sp_help_jobstep (Transact-SQL)](sp-help-jobstep-transact-sql.md)
- [sp_update_jobstep (Transact-SQL)](sp-update-jobstep-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
