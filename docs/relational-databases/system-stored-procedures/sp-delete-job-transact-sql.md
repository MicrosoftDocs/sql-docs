---
title: "sp_delete_job (Transact-SQL)"
description: "sp_delete_job (Transact-SQL) deletes an existing job from the SQL Server Agent service."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_delete_job"
  - "sp_delete_job_TSQL"
helpviewer_keywords:
  - "sp_delete_job"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_delete_job (Transact-SQL)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Deletes a job from the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent service.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_delete_job
    [ [ @job_id = ] 'job_id' ]
    [ , [ @job_name = ] N'job_name' ]
    [ , [ @originating_server = ] N'originating_server' ]
    [ , [ @delete_history = ] delete_history ]
    [ , [ @delete_unused_schedule = ] delete_unused_schedule ]
[ ; ]
```

## Arguments

#### [ @job_id = ] '*job_id*'

The identification number of the job to be deleted. *@job_id* is **uniqueidentifier**, with a default of `NULL`.

Either *@job_id* or *@job_name* must be specified; both can't be specified.

#### [ @job_name = ] N'*job_name*'

The name of the job to be deleted. *@job_name* is **sysname**, with a default of `NULL`.

Either *@job_id* or *@job_name* must be specified; both can't be specified.

#### [ @originating_server = ] N'*originating_server*'

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

#### [ @delete_history = ] *delete_history*

Specifies whether to delete the history for the job. *@delete_history* is **bit**, with a default of `1`.

- When *@delete_history* is `1`, the job history for the job is deleted.
- When *@delete_history* is `0`, the job history isn't deleted.

When a job is deleted and the history isn't deleted, historical information for the job doesn't display in the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent graphical user interface job history, but the information still resides in the `sysjobhistory` table in the `msdb` database.

#### [ @delete_unused_schedule = ] *delete_unused_schedule*

Specifies whether to delete the schedules attached to this job if they aren't attached to any other job. *@delete_unused_schedule* is **bit**, with a default of `1`.

- When *@delete_unused_schedule* is `1`, schedules attached to this job are deleted if no other jobs reference the schedule.
- When *@delete_unused_schedule* is `0`, the schedules aren't deleted.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

The *@originating_server* argument is reserved for internal use.

The *@delete_unused_schedule* argument provides backward compatibility with previous versions of SQL Server by automatically removing schedules that aren't attached to any job. This parameter defaults to the backward-compatible behavior. To retain schedules that aren't attached to a job, you must provide the value `0` as the *@delete_unused_schedule* argument.

[!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] provides an easy, graphical way to manage jobs, and is the recommended way to create and manage the job infrastructure.

This stored procedure can't delete maintenance plans, and can't delete jobs that are part of maintenance plans. Instead, use [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to delete maintenance plans.

This stored procedure shares the name of `sp_delete_job` with a similar object for the [Azure Elastic Jobs service for Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true). For information about the elastic jobs version, see [jobs.sp_delete_job (Azure Elastic Jobs)](sp-delete-job-elastic-jobs-transact-sql.md?view=azuresql-db&preserve-view=true).

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

Other users must be granted one of the following [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the `msdb` database:

- **SQLAgentUserRole**
- **SQLAgentReaderRole**
- **SQLAgentOperatorRole**

For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).

Members of the **sysadmin** fixed server role can execute `sp_delete_job` to delete any job. A user that isn't a member of the **sysadmin** fixed server role can only delete jobs owned by that user.

## Examples

The following example deletes the job `NightlyBackups`.

```sql
USE msdb;
GO

EXEC sp_delete_job
    @job_name = N'NightlyBackups';
GO
```

## Related content

- [sp_add_job (Transact-SQL)](sp-add-job-transact-sql.md)
- [sp_help_job (Transact-SQL)](sp-help-job-transact-sql.md)
- [sp_update_job (Transact-SQL)](sp-update-job-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
