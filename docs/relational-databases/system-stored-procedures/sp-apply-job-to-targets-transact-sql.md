---
title: "sp_apply_job_to_targets (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_apply_job_to_targets"
  - "sp_apply_job_to_targets_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_apply_job_to_targets"
ms.assetid: 4a3e9173-7e3c-4100-a9ac-2f5d2c60a8b0
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_apply_job_to_targets (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Applies a job to one or more target servers or to the target servers belonging to one or more target server groups.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_apply_job_to_targets { [ @job_id = ] job_id | [ @job_name = ] 'job_name' }  
     [ , [ @target_server_groups = ] 'target_server_groups' ]   
     [ , [ @target_servers = ] 'target_servers' ]   
     [ , [ @operation = ] 'operation' ]   
```  
  
## Arguments  
 [ **@job_id =**] *job_id*  
 The job identification number of the job to apply to the specified target servers or target server groups. *job_id* is **uniqueidentifier**, with a default of NULL.  
  
 [ **@job_name =**] **'**_job_name_**'**  
 The name of the job to apply to the specified the associated target servers or target server groups. *job_name* is **sysname**, with a default of NULL.  
  
> [!NOTE]  
>  Either *job_id* or *job_name* must be specified, but both cannot be specified.  
  
 [ **@target_server_groups =**]  **'**_target_server_groups_**'**  
 A comma-separated list of target server groups to which the specified job is to be applied. *target_server_groups* is **nvarchar(2048)**, with a default of NULL.  
  
 [ **@target_servers=** ] **'**_target_servers_**'**  
 A comma-separated list of target servers to which the specified job is to be applied. *target_servers*is **nvarchar(2048)**, with a default of NULL.  
  
 [ **@operation=** ]  **'**_operation_**'**  
 Is whether the specified job should be applied to or removed from the specified target servers or target server groups. *operation*is **varchar(7)**, with a default of APPLY. Valid operations are **APPLY** and **REMOVE**.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_apply_job_to_targets** provides an easy way to apply (or remove) a job from multiple target servers, and is an alternative to calling **sp_add_jobserver** (or **sp_delete_jobserver**) once for each target server required.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can execute this procedure.  
  
## Examples  
 The following example applies the previously created `Backup Customer Information` job to all the target servers in the `Servers Maintaining Customer Information` group.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_apply_job_to_targets  
    @job_name = N'Backup Customer Information',  
    @target_server_groups = N'Servers Maintaining Customer Information',   
    @operation = N'APPLY' ;  
GO  
```  
  
## See Also  
 [sp_add_jobserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-jobserver-transact-sql.md)   
 [sp_delete_jobserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-jobserver-transact-sql.md)   
 [sp_remove_job_from_targets &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-remove-job-from-targets-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
