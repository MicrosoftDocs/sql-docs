---
title: "sp_delete_maintenance_plan (Transact-SQL)"
description: "sp_delete_maintenance_plan (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
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
>  This stored procedure is used with database maintenance plans. This feature has been replaced with maintenance plans which do not use this stored procedure. Use this procedure to maintain database maintenance plans on installations that were upgraded from a previous version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_delete_maintenance_plan [ @plan_id = ] 'plan_id'   
```  
  
## Arguments  
`[ @plan_id = ] 'plan\_id'`
 Specifies the ID of the maintenance plan to be deleted. *plan_id* is **uniqueidentifier**, and must be a valid ID.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 **sp_delete_maintenance_plan** must be run from the **msdb** database.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can execute **sp_delete_maintenance_plan**.  
  
## Examples  
 Deletes the maintenance plan created by using **sp_add_maintenance_plan**.  
  
```  
EXECUTE sp_delete_maintenance_plan 'FAD6F2AB-3571-11D3-9D4A-00C04FB925FC';  
```  
  
## See Also  
 [Maintenance Plans](../../relational-databases/maintenance-plans/maintenance-plans.md)   
 [Database Maintenance Plan Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-maintenance-plan-stored-procedures-transact-sql.md)  
  
  
