---
title: "sp_detach_schedule (Transact-SQL)"
description: "sp_detach_schedule (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/02/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_detach_schedule"
  - "sp_detach_schedule_TSQL"
helpviewer_keywords:
  - "sp_detach_schedule"
dev_langs:
  - "TSQL"
---
# sp_detach_schedule (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Removes an association between a schedule and a job.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_detach_schedule
    [ [ @job_id = ] 'job_id' ]
    [ , [ @job_name = ] N'job_name' ]
    [ , [ @schedule_id = ] schedule_id ]
    [ , [ @schedule_name = ] N'schedule_name' ]
    [ , [ @delete_unused_schedule = ] delete_unused_schedule ]
    [ , [ @automatic_post = ] automatic_post ]
[ ; ]
```

## Arguments

#### [ @job_id = ] '*job_id*'

The job identification number of the job to remove the schedule from. *@job_id* is **uniqueidentifier**, with a default of `NULL`.

#### [ @job_name = ] N'*job_name*'

The name of the job to remove the schedule from. *@job_name* is **sysname**, with a default of `NULL`.

> [!NOTE]  
> Either *@job_id* or *@job_name* must be specified, but both can't be specified.

#### [ @schedule_id = ] *schedule_id*

The schedule identification number of the schedule to remove from the job. *@schedule_id* is **int**, with a default of `NULL`.

#### [ @schedule_name = ] N'*schedule_name*'

The name of the schedule to remove from the job. *@schedule_name* is **sysname**, with a default of `NULL`.

> [!NOTE]  
> Either *@schedule_id* or *@schedule_name* must be specified, but both can't be specified.

#### [ @delete_unused_schedule = ] *delete_unused_schedule*

Specifies whether to delete unused job schedules. *@delete_unused_schedule* is **bit**, with a default of `0`.

- If set to `0`, all schedules are kept, even if no jobs reference them.
- If set to `1`, unused job schedules are deleted if no jobs reference them.

#### [ @automatic_post = ] *automatic_post*

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

Other users must be granted one of the following [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the `msdb` database:

- **SQLAgentUserRole**
- **SQLAgentReaderRole**
- **SQLAgentOperatorRole**

For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).

The job owner can attach a job to a schedule and detach a job from a schedule without also having to be the schedule owner. However, a schedule can't be deleted if the detach would leave it with no jobs unless the caller is the schedule owner.

Only members of **sysadmin** can use this stored procedure to edit the attributes of jobs that are owned by other users.

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] checks to determine whether the user owns the schedule. Only members of the **sysadmin** fixed server role can detach schedules from jobs owned by another user.

## Examples

The following example removes an association between a `NightlyJobs` schedule and a `BackupDatabase` job.

```sql
USE msdb;
GO

EXEC dbo.sp_detach_schedule
    @job_name = 'BackupDatabase',
    @schedule_name = 'NightlyJobs';
GO
```

## Related content

- [sp_add_schedule (Transact-SQL)](sp-add-schedule-transact-sql.md)
- [sp_attach_schedule (Transact-SQL)](sp-attach-schedule-transact-sql.md)
- [sp_delete_schedule (Transact-SQL)](sp-delete-schedule-transact-sql.md)
