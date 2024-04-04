---
title: "sp_delete_jobschedule (Transact-SQL)"
description: "sp_delete_jobschedule deletes a schedule for a job in the SQL Server Agent service."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 01/23/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_delete_jobschedule"
  - "sp_delete_jobschedule_TSQL"
helpviewer_keywords:
  - "sp_delete_jobschedule"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_delete_jobschedule (Transact-SQL)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Deletes a schedule for a job in the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent service.

`sp_delete_jobschedule` is provided for backward compatibility only.

[!INCLUDE [ssnotedepfutureavoid-md](../../includes/ssnotedepfutureavoid-md.md)]

## Remarks

Job schedules can now be managed independently of jobs. To remove a schedule from a job, use `sp_detach_schedule`. To delete a schedule, use `sp_delete_schedule`.

`sp_delete_jobschedule` doesn't support schedules that are attached to multiple jobs. If an existing script calls `sp_delete_jobschedule` to remove a schedule that is attached to more than one job, the procedure returns an error.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

Other users must be granted one of the following [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the `msdb` database:

- **SQLAgentUserRole**
- **SQLAgentReaderRole**
- **SQLAgentOperatorRole**

For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).

Members of the **sysadmin** role can delete any job schedule. Users who aren't members of the **sysadmin** role can only delete job schedules that they own.

## Related content

- [sp_delete_schedule (Transact-SQL)](sp-delete-schedule-transact-sql.md)
- [sp_detach_schedule (Transact-SQL)](sp-detach-schedule-transact-sql.md)
- [View or Modify Jobs](../../ssms/agent/view-or-modify-jobs.md)
- [sp_add_schedule (Transact-SQL)](sp-add-schedule-transact-sql.md)
- [sp_help_jobschedule (Transact-SQL)](sp-help-jobschedule-transact-sql.md)
- [sp_update_jobschedule (Transact-SQL)](sp-update-jobschedule-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
