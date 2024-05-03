---
title: "sp_add_maintenance_plan (Transact-SQL)"
description: "Adds a maintenance plan and returns the plan ID."
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_add_maintenance_plan"
  - "sp_add_maintenance_plan_TSQL"
helpviewer_keywords:
  - "sp_add_maintenance_plan"
dev_langs:
  - "TSQL"
---
# sp_add_maintenance_plan (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Adds a maintenance plan and returns the plan ID.

> [!NOTE]  
> This stored procedure is used with database maintenance plans. This feature has been replaced with maintenance plans which don't use this stored procedure. Use this procedure to maintain database maintenance plans on installations that were upgraded from a previous version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

[!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_add_maintenance_plan
    [ @plan_name = ] 'plan_name'
    , [ @plan_id = ] 'plan_id' OUTPUT
[ ; ]
```

## Arguments

#### [ @plan_name = ] N'*plan_name*'

Specifies the name of the maintenance plan to be added. *@plan_name* is **varchar(128)**.

#### [ @plan_id = ] N'*plan_id*' OUTPUT

Specifies the ID of the maintenance plan. *@plan_id* is **uniqueidentifier**.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_add_maintenance_plan` must be run from the `msdb` database and creates a new, but empty, maintenance plan. To add one or more databases and associate them with a job or jobs, execute `sp_add_maintenance_plan_db` and `sp_add_maintenance_plan_job`.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

## Examples

Create a maintenance plan called `MyPlan`.

```sql
DECLARE @myplan_id UNIQUEIDENTIFIER;

EXECUTE sp_add_maintenance_plan
    @plan_name = N'MyPlan',
    @plan_id = @myplan_id OUTPUT

PRINT 'The ID for the maintenance plan "MyPlan" is:' + convert(VARCHAR(256), @myplan_id);
GO
```

Success in creating the maintenance plan returns the plan ID.

```output
The ID for the maintenance plan "MyPlan" is: FAD6F2AB-3571-11D3-9D4A-00C04FB925FC
```

## Related content

- [Maintenance plans](../maintenance-plans/maintenance-plans.md)
- [Database Maintenance Plan stored procedures (Transact-SQL)](database-maintenance-plan-stored-procedures-transact-sql.md)
