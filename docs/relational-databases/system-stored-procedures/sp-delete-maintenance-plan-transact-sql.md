---
title: "sp_delete_maintenance_plan (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_delete_maintenance_plan"
  - "sp_delete_maintenance_plan_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_delete_maintenance_plan"
ms.assetid: 6f36b63f-3d18-4d42-9469-2febb6926530
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_delete_maintenance_plan (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Deletes the specified maintenance plan.  
  
> [!NOTE]  
>  This stored procedure is used with database maintenance plans. This feature has been replaced with maintenance plans which do not use this stored procedure. Use this procedure to maintain database maintenance plans on installations that were upgraded from a previous version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_delete_maintenance_plan [ @plan_id = ] 'plan_id'   
```  
  
## Arguments  
 [ **@plan_id =**] **'**_plan\_id_**'**  
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
  
  
