---
title: "sp_start_job (Transact-SQL)"
description: sp_start_job instructs the SQL Server Agent to execute a job immediately.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 04/08/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_start_job"
  - "sp_start_job_TSQL"
helpviewer_keywords:
  - "sp_start_job"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_start_job (Transact-SQL)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Instructs [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent to execute a job immediately.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_start_job
    [ [ @job_name = ] N'job_name' ]
    [ , [ @job_id = ] 'job_id' ]
    [ , [ @error_flag = ] error_flag ]
    [ , [ @server_name = ] N'server_name' ]
    [ , [ @step_name = ] N'step_name' ]
    [ , [ @output_flag = ] output_flag ]
[ ; ]
```

## Arguments

#### [ @job_name = ] N'*job_name*'

The name of the job to start. *@job_name* is **sysname**, with a default of `NULL`.

Either *@job_id* or *@job_name* must be specified, but both can't be specified.

#### [ @job_id = ] '*job_id*'

The identification number of the job to start. *@job_id* is **uniqueidentifier**, with a default of `NULL`.

Either *@job_id* or *@job_name* must be specified, but both can't be specified.

#### [ @error_flag = ] *error_flag*

[!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)]

#### [ @server_name = ] N'*server_name*'

The target server on which to start the job. *@server_name* is **sysname**, with a default of `NULL`. *@server_name* must be one of the target servers to which the job is currently targeted.

#### [ @step_name = ] N'*step_name*'

The name of the step at which to begin execution of the job. *@step_name* is **sysname**, with a default of `NULL`. Applies only to local jobs.

#### [ @output_flag = ] *output_flag*

[!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)]

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

This stored procedure is in the `msdb` database.

This stored procedure shares the name of `sp_start_job` with a similar object for the [Azure Elastic Jobs service for Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true). For information about the elastic jobs version, see [jobs.sp_start_job (Azure Elastic Jobs) (Transact-SQL)](sp-start-job-elastic-jobs-transact-sql.md?view=azuresql-db&preserve-view=true).

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

Other users must be granted one of the following [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the `msdb` database:

- **SQLAgentUserRole**
- **SQLAgentReaderRole**
- **SQLAgentOperatorRole**

For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).

Members of **SQLAgentUserRole** and **SQLAgentReaderRole** can only start jobs that they own. Members of **SQLAgentOperatorRole** can start all local jobs, including jobs that are owned by other users. Members of **sysadmin** can start all local and multiserver jobs.

## Examples

The following example starts a job named `Weekly Sales Data Backup`.

```sql
USE msdb;
GO

EXEC dbo.sp_start_job N'Weekly Sales Data Backup';
GO
```

## Related content

- [sp_delete_job (Transact-SQL)](sp-delete-job-transact-sql.md)
- [sp_help_job (Transact-SQL)](sp-help-job-transact-sql.md)
- [sp_stop_job (Transact-SQL)](sp-stop-job-transact-sql.md)
- [sp_update_job (Transact-SQL)](sp-update-job-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
