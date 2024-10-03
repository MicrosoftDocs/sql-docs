---
title: "sp_update_jobschedule (Transact-SQL)"
description: "sp_update_jobschedule changes the schedule settings for the specified job in the SQL Server Agent service."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest, wiassaf
ms.date: 08/21/2024
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
monikerRange: ">=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_update_jobschedule (Transact-SQL)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Changes the schedule settings for the specified job in the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent service.

`sp_update_jobschedule` is provided for backward compatibility only.

Job schedules can now be managed independently of jobs. To update a schedule, use [sp_update_schedule](sp-update-schedule-transact-sql.md).

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

Other users must be granted one of the following [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the `msdb` database:

- **SQLAgentUserRole**
- **SQLAgentReaderRole**
- **SQLAgentOperatorRole**

For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).

Only members of **sysadmin** can use this stored procedure to update job schedules that are owned by other users.

## Related content

- [SQL Server Agent stored procedures (Transact-SQL)](sql-server-agent-stored-procedures-transact-sql.md)
- [sp_update_schedule (Transact-SQL)](sp-update-schedule-transact-sql.md)
