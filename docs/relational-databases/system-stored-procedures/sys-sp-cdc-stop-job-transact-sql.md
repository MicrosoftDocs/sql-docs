---
title: "sys.sp_cdc_stop_job (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.sp_cdc_stop_job_TSQL"
  - "sys.sp_cdc_stop_job"
  - "sp_cdc_stop_job_TSQL"
  - "sp_cdc_stop_job"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_cdc_stop_job"
ms.assetid: 421fc21c-c7a4-407c-8b31-359273b68c63
author: rothja
ms.author: jroth
manager: craigg
---
# sys.sp_cdc_stop_job (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Stops a change data capture cleanup or capture job for the current database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sys.sp_cdc_stop_job [ [ @job_type = ] 'job_type' ]  
```  
  
## Arguments  
 [ [ **@job_type=** ] **'**_job_type_' ]  
 Type of job to add. *job_type* is **nvarchar(20)** with a default of **capture**. Valid inputs are **capture** and **cleanup**.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 sys.sp_cdc_stop_job can be used by an administrator to explicitly stop either the capture job or the cleanup job.  
  
## Permissions  
 Requires membership in the db_owner fixed database role.  
  
## Examples  
 The following example stops the cleanup job for the `AdventureWorks2012` database.  
  
```  
USE AdventureWorks2012;  
GO  
EXEC sys.sp_cdc_stop_job @job_type = N'capture';  
GO  
```  
  
## See Also  
 [dbo.cdc_jobs &#40;Transact-SQL&#41;](../../relational-databases/system-tables/dbo-cdc-jobs-transact-sql.md)   
 [sys.sp_cdc_start_job &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sys-sp-cdc-start-job-transact-sql.md)  
  
  
