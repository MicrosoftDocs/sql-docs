---
title: "sp_delete_schedule (Transact-SQL)"
description: sp_delete_schedule deletes a schedule.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 01/23/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_delete_schedule"
  - "sp_delete_schedule_TSQL"
helpviewer_keywords:
  - "sp_delete_schedule"
dev_langs:
  - "TSQL"
---
# sp_delete_schedule (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Deletes a schedule.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_delete_schedule
    [ [ @schedule_id = ] schedule_id ]
    [ , [ @schedule_name = ] N'schedule_name' ]
    [ , [ @force_delete = ] force_delete ]
    [ , [ @automatic_post = ] automatic_post ]
[ ; ]
```

## Arguments

#### [ @schedule_id = ] *schedule_id*

The schedule identification number of the schedule to delete. *@schedule_id* is **int**, with a default of `NULL`.

Either *@schedule_id* or *@schedule_name* must be specified, but both can't be specified.

#### [ @schedule_name = ] N'*schedule_name*'

The name of the schedule to delete. *@schedule_name* is **sysname**, with a default of `NULL`.

Either *@schedule_id* or *@schedule_name* must be specified, but both can't be specified.

#### [ @force_delete = ] *force_delete*

Specifies whether the procedure should fail if the schedule is attached to a job. *@force_delete* is **bit**, with a default of `0`.

- When *@force_delete* is `0`, the stored procedure fails if the schedule is attached to a job.
- When *@force_delete* is `1`, the schedule is deleted regardless of whether the schedule is attached to a job.

#### [ @automatic_post = ] *automatic_post*

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

By default, a schedule can't be deleted if the schedule is attached to a job. To delete a schedule that is attached to a job, specify a value of `1` for *@force_delete*. Deleting a schedule doesn't stop jobs that are currently running.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

Other users must be granted one of the following [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the `msdb` database:

- **SQLAgentUserRole**
- **SQLAgentReaderRole**
- **SQLAgentOperatorRole**

The job owner can attach a job to a schedule and detach a job from a schedule without also having to be the schedule owner. However, a schedule can't be deleted if the detach would leave it with no jobs, unless the caller is the schedule owner.

For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).

Only members of the **sysadmin** role can delete a job schedule that is owned by another user.

## Examples

### A. Delete a schedule

The following example deletes the schedule `NightlyJobs`. If the schedule is attached to any job, the example doesn't delete the schedule.

```sql
USE msdb;
GO

EXEC dbo.sp_delete_schedule
    @schedule_name = N'NightlyJobs';
GO
```

### B. Delete a schedule attached to a job

The following example deletes the schedule `RunOnce`, regardless of whether the schedule is attached to a job.

```sql
USE msdb;
GO

EXEC dbo.sp_delete_schedule
    @schedule_name = 'RunOnce',
    @force_delete = 1;
GO
```

## Related content

- [Implement Jobs](../../ssms/agent/implement-jobs.md)
- [sp_add_schedule (Transact-SQL)](sp-add-schedule-transact-sql.md)
