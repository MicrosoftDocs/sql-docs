---
title: "sp_delete_maintenance_plan_db (Transact-SQL)"
description: Disassociates the specified maintenance plan from the specified database.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 01/23/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_delete_maintenance_plan_db_TSQL"
  - "sp_delete_maintenance_plan_db"
helpviewer_keywords:
  - "sp_delete_maintenance_plan_db"
  - "maintenance plans [SQL Server], deleting"
  - "removing maintenance plan"
  - "disassociating maintenance plan"
dev_langs:
  - "TSQL"
---
# sp_delete_maintenance_plan_db (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Disassociates the specified maintenance plan from the specified database.

> [!NOTE]  
> This stored procedure is used with database maintenance plans. This feature has been replaced with maintenance plans which don't use this stored procedure. Use this procedure to maintain database maintenance plans on installations that were upgraded from a previous version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

[!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_delete_maintenance_plan_db
    [ @plan_id = ] 'plan_id'
    , [ @db_name = ] N'db_name'
[ ; ]
```

## Arguments

#### [ @plan_id = ] '*plan_id*'

Specifies the maintenance plan ID. *@plan_id* is **uniqueidentifier**, with no default.

#### [ @db_name = ] N'*db_name*'

Specifies the database name to be deleted from the maintenance plan. *@db_name* is **sysname**, with no default.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_delete_maintenance_plan_db` must be run from the `msdb` database.

The `sp_delete_maintenance_plan_db` stored procedure removes the association between the maintenance plan and the specified database; it doesn't drop or destroy the database.

When `sp_delete_maintenance_plan_db` removes the last database from the maintenance plan, the stored procedure also deletes the maintenance plan.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

## Examples

Deletes the maintenance plan in the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database, previously added by using `sp_add_maintenance_plan_db`.

```sql
EXEC sp_delete_maintenance_plan_db
    N'FAD6F2AB-3571-11D3-9D4A-00C04FB925FC',
    N'AdventureWorks2022';
```

## Related content

- [Maintenance plans](../maintenance-plans/maintenance-plans.md)
- [Database Maintenance Plan stored procedures (Transact-SQL)](database-maintenance-plan-stored-procedures-transact-sql.md)
