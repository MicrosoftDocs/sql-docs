---
title: "Database Maintenance Plan stored procedures (Transact-SQL)"
description: "Database Maintenance Plan stored procedures (Transact-SQL)"
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 05/24/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
helpviewer_keywords:
  - "database maintenance plans [SQL Server]"
  - "system stored procedures [SQL Server], database maintenance plans"
  - "maintenance plans [SQL Server], stored procedures"
dev_langs:
  - "TSQL"
---
# Database Maintenance Plan stored procedures (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] supports the following system stored procedures that are used to set up maintenance tasks. These stored procedures are used with database maintenance plans. This feature has been replaced with maintenance plans which do not use these stored procedures. Use these procedures to maintain database maintenance plans on installations that were upgraded from a previous version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

[!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]

- [sp_add_maintenance_plan](sp-add-maintenance-plan-transact-sql.md)
- [sp_add_maintenance_plan_db](sp-add-maintenance-plan-db-transact-sql.md)
- [sp_add_maintenance_plan_job](sp-add-maintenance-plan-job-transact-sql.md)
- [sp_delete_maintenance_plan](sp-delete-maintenance-plan-transact-sql.md)
- [sp_delete_maintenance_plan_db](sp-delete-maintenance-plan-db-transact-sql.md)
- [sp_delete_maintenance_plan_job](sp-delete-maintenance-plan-job-transact-sql.md)
- [sp_help_maintenance_plan](sp-help-maintenance-plan-transact-sql.md)
- [sp_delete_maintenance_plan](sp-delete-maintenance-plan-transact-sql.md)

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [Maintenance Plans](../maintenance-plans/maintenance-plans.md)
