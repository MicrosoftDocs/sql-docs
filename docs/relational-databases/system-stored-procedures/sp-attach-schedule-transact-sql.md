---
title: "sp_attach_schedule (Transact-SQL)"
description: sp_attach_schedule sets a schedule for a job.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 03/04/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_attach_schedule_TSQL"
  - "sp_attach_schedule"
helpviewer_keywords:
  - "sp_attach_schedule"
dev_langs:
  - "TSQL"
---
# sp_attach_schedule (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Sets a schedule for a job.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_attach_schedule
    [ [ @job_id = ] 'job_id' ]
    [ , [ @job_name = ] N'job_name' ]
    [ , [ @schedule_id = ] schedule_id ]
    [ , [ @schedule_name = ] N'schedule_name' ]
    [ , [ @automatic_post = ] automatic_post ]
[ ; ]
```

## Arguments

#### [ @job_id = ] '*job_id*'

The job identification number of the job to which the schedule is added. *@job_id* is **uniqueidentifier**, with a default of `NULL`.

Either *@job_id* or *@job_name* must be specified, but both can't be specified.

#### [ @job_name = ] N'*job_name*'

The name of the job to which the schedule is added. *@job_name* is **sysname**, with a default of `NULL`.

Either *@job_id* or *@job_name* must be specified, but both can't be specified.

#### [ @schedule_id = ] *schedule_id*

The schedule identification number of the schedule to set for the job. *@schedule_id* is **int**, with a default of `NULL`.

Either *@schedule_id* or *@schedule_name* must be specified, but both can't be specified.

#### [ @schedule_name = ] N'*schedule_name*'

The name of the schedule to set for the job. *@schedule_name* is **sysname**, with a default of `NULL`.

Either *@schedule_id* or *@schedule_name* must be specified, but both can't be specified.

#### [ @automatic_post = ] *automatic_post*

*@automatic_post* is **bit**, with a default of `1`.

## Remarks

The schedule and the job must have the same owner.

A schedule can be set for more than one job. A job can be run on more than one schedule.

This stored procedure must be run from the `msdb` database.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

Other users must be granted one of the following [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the `msdb` database:

- **SQLAgentUserRole**
- **SQLAgentReaderRole**
- **SQLAgentOperatorRole**

The job owner can attach a job to a schedule and detach a job from a schedule without also having to be the schedule owner. However, a schedule can't be deleted if the detach would leave it with no jobs, unless the caller is the schedule owner.

For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] checks if the user owns both the job and the schedule.

## Examples

The following example creates a schedule named `NightlyJobs`. Jobs that use this schedule execute every day when the time on the server is `01:00`. The example attaches the schedule to the job `BackupDatabase` and the job `RunReports`.

> [!NOTE]  
> This example assumes that the job `BackupDatabase` and the job `RunReports` already exist.

```sql
USE msdb;
GO

EXEC sp_add_schedule
    @schedule_name = N'NightlyJobs' ,
    @freq_type = 4,
    @freq_interval = 1,
    @active_start_time = 010000 ;
GO

EXEC sp_attach_schedule
   @job_name = N'BackupDatabase',
   @schedule_name = N'NightlyJobs' ;
GO

EXEC sp_attach_schedule
   @job_name = N'RunReports',
   @schedule_name = N'NightlyJobs' ;
GO
```

## Related content

- [sp_add_schedule (Transact-SQL)](sp-add-schedule-transact-sql.md)
- [sp_detach_schedule (Transact-SQL)](sp-detach-schedule-transact-sql.md)
- [sp_delete_schedule (Transact-SQL)](sp-delete-schedule-transact-sql.md)
