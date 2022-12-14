---
description: "sys.sp_cdc_set_scheduler_job (Transact-SQL)"
title: "sys.sp_cdc_set_scheduler_job (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "04/21/2022"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
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
monikerRange: "= azuresqldb-current"
---
# sys.sp_cdc_set_scheduler_job (Transact-SQL)
[!INCLUDE [Azure SQL Database](../../includes/applies-to-version/asdb.md)]

Instruct the change data capture (CDC) scheduler to pause or resume scheduling of CDC scan and/or CDC cleanup jobs for Azure SQL Database, or abort the currently running CDC scan and/or CDC cleanup job.

This system stored procedure is applicable to Azure SQL Database only. 

 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  

## Syntax  

```
sys.sp_cdc_set_scheduler_job  [ @jobType = ] N'JobType'    
     , [ @state = ] N'state'    
     , [ @abortTask = ] abortTask    
```  

## Arguments  
`[ @jobType = ] N'JobType'`
 The type of CDC job, such as a capture job, or clean up job. Valid values are `capture`, `cleanup` or `both`. There is no default value.    

`[ @state = ] N'state'`is 
 Instructs the CDC scheduler to pause or resume scheduling the job.  Valid values are `pause` or `resume`. There is no default value.  

`[ @abortTask = ] abortTask`
 Indicates whether you want to abort the current running task or not. Valid **int** values are `1` or `0`, with no default value. The `abortTask` value is only used when the _state_ value is `pause`. However, currently it only **accepts `1` as the valid input.**

## Return Code Values  
 **0** (success) or **1** (failure)  

## Result Sets  
 None  

## Remarks  

- This system stored procedure applies to Azure SQL Database only.
- The CDC scheduler periodically schedules CDC scan and CDC cleanup jobs.  Use `sp_cdc_set_scheduler_job` to instruct the scheduler to either pause the scheduling or resume scheduling of these jobs.
- The `abortTask` variable is used to indicate whether the currently running job should be aborted or not.

## Permissions  
 Requires membership in the `db_owner` fixed database role.  

## See Also
 [dbo.cdc_jobs &#40;Transact-SQL&#41;](../../relational-databases/system-tables/dbo-cdc-jobs-transact-sql.md)
