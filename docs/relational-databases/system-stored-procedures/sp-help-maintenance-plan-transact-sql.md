---
title: "sp_help_maintenance_plan (Transact-SQL)"
description: Returns information about the specified maintenance plan.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/14/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_help_maintenance_plan_TSQL"
  - "sp_help_maintenance_plan"
helpviewer_keywords:
  - "sp_help_maintenance_plan"
dev_langs:
  - "TSQL"
---
# sp_help_maintenance_plan (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns information about the specified maintenance plan. If a plan isn't specified, this stored procedure returns information about all maintenance plans.

> [!NOTE]  
> This stored procedure is used with database maintenance plans. This feature has been replaced with maintenance plans which don't use this stored procedure. Use this procedure to maintain database maintenance plans on installations that were upgraded from a previous version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

[!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_help_maintenance_plan [ [ @plan_id = ] 'plan_id' ]
[ ; ]
```

## Arguments

#### [ @plan_id = ] '*plan_id*'

Specifies the plan ID of the maintenance plan. *@plan_id* is **uniqueidentifier**, with a default of `NULL`.

## Return code values

None.

## Result set

If *@plan_id* is specified, `sp_help_maintenance_plan` returns three tables: *Plan*, *Database*, and *Job*.

### Plan table

| Column name | Data type | Description |
| --- | --- | --- |
| `plan_id` | **uniqueidentifier** | Maintenance plan ID. |
| `plan_name` | **sysname** | Maintenance plan name. |
| `date_created` | **datetime** | Date the maintenance plan was created. |
| `owner` | **sysname** | Owner of the maintenance plan. |
| `max_history_rows` | **int** | Maximum number of rows allocated for recording the history of the maintenance plan in the system table. |
| `remote_history_server` | **int** | The name of the remote server to which the history report could be written. |
| `max_remote_history_rows` | **int** | Maximum number of rows allocated in the system table on a remote server to which the history report could be written. |
| `user_defined_1` | **int** | Default is `NULL`. |
| `user_defined_2` | **nvarchar(100)** | Default is `NULL`. |
| `user_defined_3` | **datetime** | Default is `NULL`. |
| `user_defined_4` | **uniqueidentifier** | Default is `NULL`. |

### Database table

| Column name | Description |
| --- | --- |
| `database_name` | Name of all databases associated with the maintenance plan. `database_name` is **sysname**. |

### Job table

| Column name | Description |
| --- | --- |
| `job_id` | ID of all jobs associated with the maintenance plan. `job_id` is **uniqueidentifier**. |

## Remarks

`sp_help_maintenance_plan` is in the `msdb` database.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

## Examples

This example descriptive information about the maintenance plan `FAD6F2AB-3571-11D3-9D4A-00C04FB925FC`.

```sql
EXEC sp_help_maintenance_plan
    N'FAD6F2AB-3571-11D3-9D4A-00C04FB925FC';
```

## Related content

- [Maintenance plans](../maintenance-plans/maintenance-plans.md)
- [Database Maintenance Plan stored procedures (Transact-SQL)](database-maintenance-plan-stored-procedures-transact-sql.md)
