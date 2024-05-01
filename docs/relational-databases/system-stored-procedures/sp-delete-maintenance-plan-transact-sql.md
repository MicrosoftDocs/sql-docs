---
title: "sp_delete_maintenance_plan (Transact-SQL)"
description: sp_delete_maintenance_plan deletes the specified maintenance plan.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 01/23/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_delete_maintenance_plan"
  - "sp_delete_maintenance_plan_TSQL"
helpviewer_keywords:
  - "sp_delete_maintenance_plan"
dev_langs:
  - "TSQL"
---
# sp_delete_maintenance_plan (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Deletes the specified maintenance plan.

> [!NOTE]  
> This stored procedure is used with database maintenance plans. This feature has been replaced with maintenance plans that don't use this stored procedure. Use this procedure to maintain database maintenance plans on installations that were upgraded from a previous version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

[!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

```syntaxsql
sp_delete_maintenance_plan [ @plan_id = ] 'plan_id'
[ ; ]
```

## Arguments

#### [ @plan_id = ] '*plan_id*'

Specifies the ID of the maintenance plan to be deleted. *@plan_id* is **uniqueidentifier**, and must be a valid ID.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_delete_maintenance_plan` must be run from the `msdb` database.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

## Examples

Deletes the maintenance plan created by using `sp_add_maintenance_plan`.

```sql
EXEC sp_delete_maintenance_plan 'FAD6F2AB-3571-11D3-9D4A-00C04FB925FC';
```

## Related content

- [Maintenance plans](../maintenance-plans/maintenance-plans.md)
- [Database Maintenance Plan stored procedures (Transact-SQL)](database-maintenance-plan-stored-procedures-transact-sql.md)
