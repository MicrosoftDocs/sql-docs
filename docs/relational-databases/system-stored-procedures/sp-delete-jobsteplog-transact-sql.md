---
title: "sp_delete_jobsteplog (Transact-SQL)"
description: Removes all SQL Server Agent job step logs that are specified with the arguments.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 01/23/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_delete_jobsteplog"
  - "sp_delete_jobsteplog_TSQL"
helpviewer_keywords:
  - "sp_delete_jobsteplog"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_delete_jobsteplog (Transact-SQL)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Removes all [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent job step logs that are specified with the arguments. Use this stored procedure to maintain the [sysjobstepslogs](../system-tables/dbo-sysjobstepslogs-transact-sql.md) table in the `msdb` database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_delete_jobsteplog
    [ [ @job_id = ] 'job_id' ]
    [ , [ @job_name = ] N'job_name' ]
    [ , [ @step_id = ] step_id ]
    [ , [ @step_name = ] N'step_name' ]
    [ , [ @older_than = ] older_than ]
    [ , [ @larger_than = ] larger_than ]
[ ; ]
```

## Arguments

#### [ @job_id = ] '*job_id*'

The job identification number for the job that contains the job step log to be removed. *@job_id* is **uniqueidentifier**, with a default of `NULL`.

Either *@job_id* or *@job_name* must be specified, but both can't be specified.

#### [ @job_name = ] N'*job_name*'

The name of the job. *@job_name* is **sysname**, with a default of `NULL`.

Either *@job_id* or *@job_name* must be specified, but both can't be specified.

#### [ @step_id = ] *step_id*

The identification number of the step in the job for which the job step log is to be deleted. *@step_id* is **int**, with a default of `NULL`. If not included, all job step logs in the job are deleted unless *@older_than* or *@larger_than* are specified.

Either *@step_id* or *@step_name* can be specified, but both can't be specified.

#### [ @step_name = ] N'*step_name*'

The name of the step in the job for which the job step log is to be deleted. *@step_name* is **sysname**, with a default of `NULL`.

Either *@step_id* or *@step_name* can be specified, but both can't be specified.

#### [ @older_than = ] *older_than*

The date and time of the oldest job step log you want to keep. *@older_than* is **datetime**, with a default of `NULL`. All job step logs that are older than this date and time are removed.

Both *@older_than* and *@larger_than* can be specified.

#### [ @larger_than = ] *larger_than*

The size in bytes of the largest job step log you want to keep. *@larger_than* is **int**, with a default of `NULL`. All job step logs that are larger that this size are removed.

Both *@older_than* and *@larger_than* can be specified.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

`sp_delete_jobsteplog` is in the `msdb` database.

If no arguments except *@job_id* or *@job_name* are specified, all job step logs for the specified job are deleted.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

Other users must be granted one of the following [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the `msdb` database:

- **SQLAgentUserRole**
- **SQLAgentReaderRole**
- **SQLAgentOperatorRole**

For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).

Only members of **sysadmin** can delete a job step log that is owned by another user.

## Examples

### A. Remove all job step logs from a job

The following example removes all job step logs for the job `Weekly Sales Data Backup`.

```sql
USE msdb;
GO

EXEC dbo.sp_delete_jobsteplog
    @job_name = N'Weekly Sales Data Backup';
GO
```

### B. Remove the job step log for a particular job step

The following example removes the job step log for step 2 in the job `Weekly Sales Data Backup`.

```sql
USE msdb;
GO

EXEC dbo.sp_delete_jobsteplog
    @job_name = N'Weekly Sales Data Backup',
    @step_id = 2;
GO
```

### C. Remove all job step logs based on age and size

The following example removes all job steps logs that are older than noon October 25, 2005 and larger than 100 megabytes (MB) from the job `Weekly Sales Data Backup`.

```sql
USE msdb;
GO

EXEC dbo.sp_delete_jobsteplog
    @job_name = N'Weekly Sales Data Backup',
    @older_than = '10/25/2005 12:00:00',
    @larger_than = 104857600;
GO
```

## Related content

- [dbo.sysjobstepslogs (Transact-SQL)](../system-tables/dbo-sysjobstepslogs-transact-sql.md)
- [sp_help_jobsteplog (Transact-SQL)](sp-help-jobsteplog-transact-sql.md)
- [SQL Server Agent stored procedures (Transact-SQL)](sql-server-agent-stored-procedures-transact-sql.md)
