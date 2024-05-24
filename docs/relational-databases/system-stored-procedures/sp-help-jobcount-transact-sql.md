---
title: "sp_help_jobcount (Transact-SQL)"
description: sp_help_jobcount provides the number of jobs that a schedule is attached to.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/14/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_help_jobcount"
  - "sp_help_jobcount_TSQL"
helpviewer_keywords:
  - "sp_help_jobcount"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_help_jobcount (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Provides the number of jobs that a schedule is attached to.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_help_jobcount
    [ [ @schedule_name = ] N'schedule_name' ]
    [ , [ @schedule_id = ] schedule_id ]
[ ; ]
```

## Arguments

#### [ @schedule_id = ] *schedule_id*

The identifier of the schedule to list. *@schedule_id* is **int**, with no default.

Either *@schedule_id* or *@schedule_name* can be specified.

#### [ @schedule_name = ] N'*schedule_name*'

The name of the schedule to list. *@schedule_name* is **sysname**, with no default.

Either *@schedule_id* or *@schedule_name* can be specified.

## Return code values

`0` (success) or `1` (failure).

## Result set

Returns the following result set:

| Column name | Data type | Description |
| --- | --- | --- |
| `JobCount` | **int** | Number of jobs for the specified schedule. |

## Remarks

This procedure lists the number of jobs attached to the specified schedule.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

Other users must be granted one of the following [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the `msdb` database:

- **SQLAgentUserRole**
- **SQLAgentReaderRole**
- **SQLAgentOperatorRole**

For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).

Only members of **sysadmin** can view counts for jobs that are owned by others.

## Examples

The following example lists the number of jobs attached to the `NightlyJobs` schedule.

```sql
USE msdb;
GO

EXEC sp_help_jobcount
    @schedule_name = N'NightlyJobs';
GO
```

## Related content

- [SQL Server Agent stored procedures (Transact-SQL)](sql-server-agent-stored-procedures-transact-sql.md)
- [sp_add_schedule (Transact-SQL)](sp-add-schedule-transact-sql.md)
- [sp_attach_schedule (Transact-SQL)](sp-attach-schedule-transact-sql.md)
- [sp_delete_schedule (Transact-SQL)](sp-delete-schedule-transact-sql.md)
- [sp_detach_schedule (Transact-SQL)](sp-detach-schedule-transact-sql.md)
