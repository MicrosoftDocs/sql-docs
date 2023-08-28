---
title: "sp_update_jobschedule (Transact-SQL)"
description: Changes the schedule settings for the specified job.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/28/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_update_jobschedule_TSQL"
  - "sp_update_jobschedule"
helpviewer_keywords:
  - "sp_update_jobschedule"
dev_langs:
  - "TSQL"
---
# sp_update_jobschedule (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Changes the schedule settings for the specified job.

`sp_update_jobschedule` is provided for backward compatibility only.

Job schedules can now be managed independently of jobs. To update a schedule, use [sp_update_schedule (Transact-SQL)](sp-update-schedule-transact-sql.md).

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

Other users must be granted one of the following [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the `msdb` database:

- **SQLAgentUserRole**
- **SQLAgentReaderRole**
- **SQLAgentOperatorRole**

For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).

Only members of **sysadmin** can use this stored procedure to update job schedules that are owned by other users.

## See also

- [SQL Server Agent stored procedures (Transact-SQL)](sql-server-agent-stored-procedures-transact-sql.md)
- [sp_update_schedule (Transact-SQL)](sp-update-schedule-transact-sql.md)
