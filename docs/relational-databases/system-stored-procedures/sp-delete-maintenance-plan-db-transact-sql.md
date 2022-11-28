---
description: "sp_delete_maintenance_plan_db (Transact-SQL)"
title: "sp_delete_maintenance_plan_db (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_delete_maintenance_plan_db_TSQL"
  - "sp_delete_maintenance_plan_db"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_delete_maintenance_plan_db"
  - "maintenance plans [SQL Server], deleting"
  - "removing maintenance plan"
  - "disassociating maintenance plan"
ms.assetid: d1e8afb5-12ee-492b-a770-ba708ed7c8a4
author: markingmyname
ms.author: maghan
---
# sp_delete_maintenance_plan_db (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Disassociates the specified maintenance plan from the specified database.  
  
> [!NOTE]  
>  This stored procedure is used with database maintenance plans. This feature has been replaced with maintenance plans which do not use this stored procedure. Use this procedure to maintain database maintenance plans on installations that were upgraded from a previous version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_delete_maintenance_plan_db [ @plan_id = ] 'plan_id' ,   
     [ @db_name = ] 'database_name'   
```  
  
## Arguments  
`[ @plan_id = ] 'plan\_id'`
 Specifies the maintenance plan ID. *plan_id* is **uniqueidentifier**.  
  
`[ @db_name = ] 'database\_name'`
 Specifies the database name to be deleted from the maintenance plan. *database_name* is **sysname**.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 **sp_delete_maintenance_plan_db** must be run from the **msdb** database.  
  
 The **sp_delete_maintenance_plan_db** stored procedure removes the association between the maintenance plan and the specified database; it does not drop or destroy the database.  
  
 When **sp_delete_maintenance_plan_db** removes the last database from the maintenance plan, the stored procedure also deletes the maintenance plan.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can execute **sp_delete_maintenance_plan_db**.  
  
## Examples  
 Deletes the maintenance plan in the **AdventureWorks2012** database, previously added by using **sp_add_maintenance_plan_db**.  
  
```  
EXECUTE   sp_delete_maintenance_plan_db N'FAD6F2AB-3571-11D3-9D4A-00C04FB925FC', N'AdventureWorks2012';  
```  
  
## See Also  
 [Maintenance Plans](../../relational-databases/maintenance-plans/maintenance-plans.md)   
 [Database Maintenance Plan Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-maintenance-plan-stored-procedures-transact-sql.md)  
  
  
