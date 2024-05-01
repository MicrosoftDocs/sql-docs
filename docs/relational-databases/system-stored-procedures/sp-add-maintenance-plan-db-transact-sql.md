---
title: "sp_add_maintenance_plan_db (Transact-SQL)"
description: "Associates a database with a maintenance plan."
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 06/02/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_add_maintenance_plan_db_TSQL"
  - "sp_add_maintenance_plan_db"
helpviewer_keywords:
  - "sp_add_maintenance_plan_db"
dev_langs:
  - "TSQL"
---
# sp_add_maintenance_plan_db (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Associates a database with a maintenance plan.

> [!NOTE]  
> This stored procedure is used with database maintenance plans. This feature has been replaced with maintenance plans which do not use this stored procedure. Use this procedure to maintain database maintenance plans on installations that were upgraded from a previous version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

[!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_add_maintenance_plan_db
    [ @plan_id = ] 'plan_id'
    , [ @db_name = ] 'database_name'
[ ; ]
```

## Arguments

#### [ @plan_id = ] '*plan_id*'

Specifies the plan ID of the maintenance plan. *@plan_id* is **uniqueidentifier**, and must be a valid ID.

#### [ @db_name = ] '*database_name*'

Specifies the name of the database to be added to the maintenance plan. The database must be created or exist before its addition to the plan. *@database_name* is **sysname**.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_add_maintenance_plan_db` must be run from the `msdb` database.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

## Examples

This example adds the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] database to the maintenance plan created in `sp_add_maintenance_plan`.

```sql
EXEC sp_add_maintenance_plan_db
    N'FAD6F2AB-3571-11D3-9D4A-00C04FB925FC',
    N'AdventureWorks2022';
```

## Related content

- [Maintenance Plans](../maintenance-plans/maintenance-plans.md)
- [Database Maintenance Plan stored procedures (Transact-SQL)](database-maintenance-plan-stored-procedures-transact-sql.md)
