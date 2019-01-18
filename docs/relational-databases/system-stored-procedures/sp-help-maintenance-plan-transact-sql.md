---
title: "sp_help_maintenance_plan (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_help_maintenance_plan_TSQL"
  - "sp_help_maintenance_plan"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_help_maintenance_plan"
ms.assetid: e972a510-960e-41d6-93c5-c71cd581a585
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_help_maintenance_plan (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns information about the specified maintenance plan. If a plan is not specified, this stored procedure returns information about all maintenance plans.  
  
> **NOTE:** This stored procedure is used with database maintenance plans. This feature has been replaced with maintenance plans which do not use this stored procedure. Use this procedure to maintain database maintenance plans on installations that were upgraded from a previous version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]  
  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_help_maintenance_plan [ [ @plan_id = ] 'plan_id' ]  
```  
  
## Arguments  
 [ **@plan_id =**] **'**_plan\_id_**'**  
 Specifies the plan ID of the maintenance plan. *plan_id* is **UNIQUEIDENTIFIER**. The default is NULL.  
  
## Return Code Values  
 None  
  
## Result Sets  
 If *plan_id* is specified, **sp_help_maintenance_plan** will return three tables: Plan, Database, and Job.  
  
### Plan Table  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**plan_id**|**uniqueidentifier**|Maintenance plan ID.|  
|**plan_name**|**sysname**|Maintenance plan name.|  
|**date_created**|**datetime**|Date the maintenance plan was created.|  
|**owner**|**sysname**|Owner of the maintenance plan.|  
|**max_history_rows**|**int**|Maximum number of rows allocated for recording the history of the maintenance plan in the system table.|  
|**remote_history_server**|**int**|The name of the remote server to which the history report could be written.|  
|**max_remote_history_rows**|**int**|Maximum number of rows allocated in the system table on a remote server to which the history report could be written.|  
|**user_defined_1**|**int**|Default is NULL.|  
|**user_defined_2**|**nvarchar(100)**|Default is NULL.|  
|**user_defined_3**|**datetime**|Default is NULL.|  
|**user_defined_4**|**uniqueidentifier**|Default is NULL.|  
  
### Database Table  
  
|Column name|Description|  
|-----------------|-----------------|  
|**database_name**|Name of all databases associated with the maintenance plan. *database_name* is **sysname**.|  
  
### Job Table  
  
|Column name|Description|  
|-----------------|-----------------|  
|**job_id**|ID of all jobs associated with the maintenance plan. *job_id* is **uniqueidentifier**.|  
  
## Remarks  
 **sp_help_maintenance_plan** is in the **msdb** database.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can execute **sp_help_maintenance_plan**.  
  
## Examples  
 This example descriptive information about the maintenance plan FAD6F2AB-3571-11D3-9D4A-00C04FB925FC.  
  
```  
EXECUTE   sp_help_maintenance_plan   
   N'FAD6F2AB-3571-11D3-9D4A-00C04FB925FC';  
```  
  
## See Also  
 [Maintenance Plans](../../relational-databases/maintenance-plans/maintenance-plans.md)   
 [Database Maintenance Plan Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-maintenance-plan-stored-procedures-transact-sql.md)  
  
  
