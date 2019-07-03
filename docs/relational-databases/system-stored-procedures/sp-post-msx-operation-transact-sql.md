---
title: "sp_post_msx_operation (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_post_msx_operation"
  - "sp_post_msx_operation_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_post_msx_operation"
ms.assetid: 085deef8-2709-4da9-bb97-9ab32effdacf
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_post_msx_operation (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Inserts operations (rows) into the **sysdownloadlist** system table for target servers to download and execute.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_post_msx_operation  
     [ @operation = ] 'operation'  
     [ , [ @object_type = ] 'object' ]   
     { , [ @job_id = ] job_id }   
     [ , [ @specific_target_server = ] 'target_server' ]   
     [ , [ @value = ] value ]  
     [ , [ @schedule_uid = ] schedule_uid ]  
```  
  
## Arguments  
`[ @operation = ] 'operation'`
 The type of operation for the posted operation. *operation*is **varchar(64)**, with no default. Valid operations depend upon *object_type*.  
  
|Object type|Operation|  
|-----------------|---------------|  
|**JOB**|INSERT<br /><br /> UPDATE<br /><br /> DELETE<br /><br /> START<br /><br /> STOP|  
|**SERVER**|RE-ENLIST<br /><br /> DEFECT<br /><br /> SYNC-TIME<br /><br /> SET-POLL|  
|**SCHEDULE**|INSERT<br /><br /> UPDATE<br /><br /> DELETE|  
  
`[ @object_type = ] 'object'`
 The type of object for which to post an operation. Valid types are **JOB**, **SERVER**, and **SCHEDULE**. *object* is **varchar(64)**, with a default of **JOB**.  
  
`[ @job_id = ] job_id`
 The job identification number of the job to which the operation applies. *job_id* is **uniqueidentifier**, with no default. **0x00** indicates ALL jobs. If *object* is **SERVER**, then *job_id*is not required.  
  
`[ @specific_target_server = ] 'target_server'`
 The name of the target server for which the specified operation applies. If *job_id* is specified, but *target_server* is not specified, the operations are posted for all job servers of the job. *target_server* is **nvarchar(30)**, with a default of NULL.  
  
`[ @value = ] value`
 The polling interval, in seconds. *value* is **int**, with a default of NULL. Specify this parameter only if *operation* is **SET-POLL**.  
  
`[ @schedule_uid = ] schedule_uid`
 The unique identifier for the schedule to which the operation applies. *schedule_uid* is **uniqueidentifier**, with no default.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 **sp_post_msx_operation** must be run from the **msdb** database.  
  
 **sp_post_msx_operation** can always be called safely because it first determines if the current server is a multiserver Microsoft SQL Server Agent and, if so, whether *object*is a multiserver job.  
  
 After an operation has been posted, it appears in the **sysdownloadlist** table. After a job has been created and posted, subsequent changes to that job must also be communicated to the target servers (TSX). This is also accomplished using the download list.  
  
 We highly recommend that the download list be managed by using the SQL Server Management Studio. For more information, see [View or Modify Jobs](../../ssms/agent/view-or-modify-jobs.md).  
  
## Permissions  
 To run this stored procedure, users must be granted the **sysadmin** fixed server role.  
  
## See Also  
 [sp_add_jobserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-jobserver-transact-sql.md)   
 [sp_delete_job &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-job-transact-sql.md)   
 [sp_delete_jobserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-jobserver-transact-sql.md)   
 [sp_delete_targetserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-targetserver-transact-sql.md)   
 [sp_resync_targetserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-resync-targetserver-transact-sql.md)   
 [sp_start_job &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-start-job-transact-sql.md)   
 [sp_stop_job &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-stop-job-transact-sql.md)   
 [sp_update_job &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-update-job-transact-sql.md)   
 [sp_update_operator &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-update-operator-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
