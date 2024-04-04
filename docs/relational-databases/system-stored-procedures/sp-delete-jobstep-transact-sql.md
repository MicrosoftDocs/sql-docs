---
title: "sp_delete_jobstep (Transact-SQL)"
description: Removes a job step from a job in the SQL Server Agent service.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 01/23/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_delete_jobstep"
  - "sp_delete_jobstep_TSQL"
helpviewer_keywords:
  - "sp_delete_jobstep"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_delete_jobstep (Transact-SQL)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Removes a job step from a job in the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent service.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_delete_jobstep
    [ [ @job_id = ] 'job_id' ]
    [ , [ @job_name = ] N'job_name' ]
    , [ @step_id = ] step_id
[ ; ]
```

## Arguments

#### [ @job_id = ] '*job_id*'

The identification number of the job from which the step will be removed. *@job_id* is **uniqueidentifier**, with a default of `NULL`.

Either *@job_id* or *@job_name* must be specified; both can't be specified.

#### [ @job_name = ] N'*job_name*'

The name of the job from which the step will be removed. *@job_name* is **sysname**, with a default of `NULL`.

Either *@job_id* or *@job_name* must be specified; both can't be specified.

#### [ @step_id = ] *step_id*

The identification number of the step being removed. *@step_id* is **int**, with no default.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

Removing a job step automatically updates the other job steps that reference the deleted step.

For more information about the steps associated with a particular job, run `sp_help_jobstep`.

> [!NOTE]  
> Calling `sp_delete_jobstep` with a *@step_id* value of zero deletes all job steps for the job.

SQL Server Management Studio provides an easy, graphical way to manage jobs, and is the recommended way to create and manage the job infrastructure.

This stored procedure shares the name of `sp_delete_jobstep` with a similar object for the [Azure Elastic Jobs service for Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true). For information about the elastic jobs version, see [jobs.sp_delete_jobstep (Azure Elastic Jobs) (Transact-SQL)](sp-delete-jobstep-elastic-jobs-transact-sql.md?view=azuresql-db&preserve-view=true).

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

Other users must be granted one of the following [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the `msdb` database:

- **SQLAgentUserRole**
- **SQLAgentReaderRole**
- **SQLAgentOperatorRole**

For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).

Only members of **sysadmin** can delete a job step that is owned by another user.

## Examples

The following example removes job step `1` from the job `Weekly Sales Data Backup`.

```sql
USE msdb;
GO

EXEC dbo.sp_delete_jobstep
    @job_name = N'Weekly Sales Data Backup',
    @step_id = 1;
GO
```

## Related content

- [View or Modify Jobs](../../ssms/agent/view-or-modify-jobs.md)
- [sp_add_jobstep (Transact-SQL)](sp-add-jobstep-transact-sql.md)
- [sp_update_jobstep (Transact-SQL)](sp-update-jobstep-transact-sql.md)
- [sp_help_jobstep (Transact-SQL)](sp-help-jobstep-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
