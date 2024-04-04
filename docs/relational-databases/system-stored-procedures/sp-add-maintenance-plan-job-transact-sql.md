---
title: "sp_add_maintenance_plan_job (Transact-SQL)"
description: "Associates a maintenance plan with an existing job."
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 06/02/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_add_maintenance_plan_job_TSQL"
  - "sp_add_maintenance_plan_job"
helpviewer_keywords:
  - "sp_add_maintenance_plan_job"
dev_langs:
  - "TSQL"
---
# sp_add_maintenance_plan_job (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Associates a maintenance plan with an existing job.

> [!NOTE]  
> This stored procedure is used with database maintenance plans. This feature has been replaced with maintenance plans which do not use this stored procedure. Use this procedure to maintain database maintenance plans on installations that were upgraded from a previous version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

[!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_add_maintenance_plan_job
    [ @plan_id = ] N'plan_id'
    , [ @job_id = ] N'job_id'
[ ; ]
```

## Arguments

#### [ @plan_id = ] N'*plan_id*'

Specifies the ID of the maintenance plan. *@plan_id* is **uniqueidentifier**, and must be a valid ID.

#### [ @job_id = ] N'*job_id*'

Specifies the ID of the job to be associated with the maintenance plan. *@job_id* is **uniqueidentifier**, and must be a valid ID. To create a job or jobs, execute `sp_add_job`, or use SQL Server Management Studio.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_add_maintenance_plan_job` must be run from the `msdb` database.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

## Examples

This example adds the job `B8FCECB1-E22C-11D2-AA64-00C04F688EAE` to the maintenance plan created using `sp_add_maintenance_plan_job`.

```sql
EXEC sp_add_maintenance_plan_job
    @plan_id = N'FAD6F2AB-3571-11D3-9D4A-00C04FB925FC',
    @job_id = N'B8FCECB1-E22C-11D2-AA64-00C04F688EAE';
```

## Related content

- [Maintenance Plans](../maintenance-plans/maintenance-plans.md)
- [Database Maintenance Plan stored procedures (Transact-SQL)](database-maintenance-plan-stored-procedures-transact-sql.md)
