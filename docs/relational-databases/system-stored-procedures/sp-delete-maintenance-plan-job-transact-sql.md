---
title: "sp_delete_maintenance_plan_job (Transact-SQL)"
description: Disassociates the specified maintenance plan from the specified job.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 01/23/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_delete_maintenance_plan_job"
  - "sp_delete_maintenance_plan_job_TSQL"
helpviewer_keywords:
  - "sp_delete_maintenance_plan_job"
dev_langs:
  - "TSQL"
---
# sp_delete_maintenance_plan_job (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Disassociates the specified maintenance plan from the specified job.

> [!NOTE]  
> This stored procedure is used with database maintenance plans. This feature has been replaced with maintenance plans which don't use this stored procedure. Use this procedure to maintain database maintenance plans on installations that were upgraded from a previous version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

[!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_delete_maintenance_plan_job
    [ @plan_id = ] 'plan_id'
    , [ @job_id = ] 'job_id'
[ ; ]
```

## Arguments

#### [ @plan_id = ] '*plan_id*'

Specifies the ID of the maintenance plan. *@plan_id* is **uniqueidentifier**, and must be a valid ID.

#### [ @job_id = ] '*job_id*'

Specifies the ID of the job with which the maintenance plan is associated. *@job_id* is **uniqueidentifier**, and must be a valid ID.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_delete_maintenance_plan_job` must be run from the `msdb` database.

When all jobs are removed from the maintenance plan, you should run `sp_delete_maintenance_plan_db` to remove the remaining databases from the plan.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

## Examples

This example deletes the job `B8FCECB1-E22C-11D2-AA64-00C04F688EAE` from the maintenance plan.

```sql
EXEC sp_delete_maintenance_plan_job
    N'FAD6F2AB-3571-11D3-9D4A-00C04FB925FC',
    N'B8FCECB1-E22C-11D2-AA64-00C04F688EAE';
```

## Related content

- [Maintenance plans](../maintenance-plans/maintenance-plans.md)
- [Database Maintenance Plan stored procedures (Transact-SQL)](database-maintenance-plan-stored-procedures-transact-sql.md)
