---
title: "sp_add_maintenance_plan_job (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_add_maintenance_plan_job_TSQL"
  - "sp_add_maintenance_plan_job"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_add_maintenance_plan_job"
ms.assetid: 7205855c-964f-4f55-bf75-39a55f6fe7bd
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# sp_add_maintenance_plan_job (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Associates a maintenance plan with an existing job.  
  
> [!NOTE]  
>  This stored procedure is used with database maintenance plans. This feature has been replaced with maintenance plans which do not use this stored procedure. Use this procedure to maintain database maintenance plans on installations that were upgraded from a previous version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_add_maintenance_plan_job [ @plan_id = ] 'plan_id' , [ @job_id = ] 'job_id'  
```  
  
## Arguments  
 [ **@plan_id =**] **'**_plan_id_**'**  
 Specifies the ID of the maintenance plan. *plan_id* is **uniqueidentifier**, and must be a valid ID.  
  
 [ **@job_id =**] **'**_job_id_**'**  
 Specifies the ID of the job to be associated with the maintenance plan. *job_id* is **uniqueidentifier**, and must be a valid ID. To create a job or jobs, execute **sp_add_job**, or use SQL Server Management Studio.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 **sp_add_maintenance_plan_job** must be run from the **msdb** database.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can execute **sp_add_maintenance_plan_job**.  
  
## Examples  
 This example adds the job "B8FCECB1-E22C-11D2-AA64-00C04F688EAE" to the maintenance plan created by using **sp_add_maintenance_plan_job**.  
  
```  
EXECUTE   sp_add_maintenance_plan_job N'FAD6F2AB-3571-11D3-9D4A-00C04FB925FC', N'B8FCECB1-E22C-11D2-AA64-00C04F688EAE';  
```  
  
## See Also  
 [Maintenance Plans](../../relational-databases/maintenance-plans/maintenance-plans.md)   
 [Database Maintenance Plan Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-maintenance-plan-stored-procedures-transact-sql.md)  
  
  
