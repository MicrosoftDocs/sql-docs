---
description: "sys.sp_cdc_set_scheduler_job (Transact-SQL)"
title: "sys.sp_cdc_set_scheduler_job (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "04/21/2022"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sys.sp_cdc_set_scheduler_job_TSQL"
  - "sp_cdc_set_scheduler_job"
  - "sys.sp_cdc_set_scheduler_job"
  - "sp_cdc_set_scheduler_job_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_cdc_set_scheduler_job"
ms.assetid:
author: abhimantiwari
ms.author: abhtiwar
---
# sys.sp_cdc_set_scheduler_job (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Executes the change data capture log scan operation.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  

## Syntax  

```
sys.sp_cdc_set_scheduler_job  [ @jobType = ] N'JobType'    
     , [ @state = ] N'state'    
     , [ @abortTask = ] abortTask    
```  

## Arguments  
`[ @jobType = ] N'JobType'`
 Type of CDC job. Depending on which job needs to be addressed, valid values of  _jobType_ can be `capture`, `cleanup` or `both`. No default value.  

`[ @state = ] N'state'`
 Instruction to the CDC Scheduler whether we want to pause scheduling the job or resume scheduling. Valid values of _state_ are `pause` or `resume`. No default value

`[ @abortTask = ] abortTask`
 Indicates whether you want to abort the current running task or not. valid integer values for _abortTask_ are `1` or `0` . No Default value. Also note the abortTask value is used only when _state_ value is `pause`

## Return Code Values  
 **0** (success) or **1** (failure)  

## Result Sets  
 None  

## Remarks  
CDC Scheduler periodically schedules CDC scan and CDC Cleanup jobs. `sp_cdc_set_scheduler_job` is used to instruct the scheduler to either pause the scheduling or resume scheduling of these jobs.
This applies for a Azure SQL database. In addition, abortTask variable is used to indicate whether currently running job should be aborted or not.

## Permissions  
 Requires membership in the `db_owner` fixed database role.  

## See Also
 [dbo.cdc_jobs &#40;Transact-SQL&#41;](../../relational-databases/system-tables/dbo-cdc-jobs-transact-sql.md)
