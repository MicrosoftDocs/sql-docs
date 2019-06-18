---
title: "sys.sp_cdc_add_job (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_cdc_add_job_TSQL"
  - "sys.sp_cdc_add_job"
  - "sys.sp_cdc_add_job_TSQL"
  - "sp_cdc_add_job"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_cdc_add_job"
ms.assetid: c4458738-ed25-40a6-8294-a26ca5a05bd9
author: rothja
ms.author: jroth
manager: craigg
---
# sys.sp_cdc_add_job (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Creates a change data capture cleanup or capture job in the current database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sys.sp_cdc_add_job [ @job_type = ] 'job_type'  
    [ , [ @start_job = ] start_job ]   
    [ , [ @maxtrans = ] max_trans ]   
    [ , [ @maxscans = ] max_scans ]   
    [ , [ @continuous = ] continuous ]   
    [ , [ @pollinginterval = ] polling_interval ]   
    [ , [ @retention ] = retention ]   
    [ , [ @threshold ] = 'delete_threshold' ]  
```  
  
## Arguments  
`[ @job_type = ] 'job\_type'`
 Type of job to add. *job_type* is **nvarchar(20)** and cannot be NULL. Valid inputs are **'capture'** and **'cleanup'**.  
  
`[ @start_job = ] start_job`
 Flag indicating whether the job should be started immediately after it is added. *start_job* is **bit** with a default of 1.  
  
`[ @maxtrans ] = max_trans`
 Maximum number of transactions to process in each scan cycle. *max_trans* is **int** with a default of 500. If specified, the value must be a positive integer.  
  
 *max_trans* is valid only for capture jobs.  
  
`[ @maxscans ] = max\_scans_`
 Maximum number of scan cycles to execute in order to extract all rows from the log. *max_scans* is **int** with a default of 10.  
  
 *max_scan* is valid only for capture jobs.  
  
`[ @continuous ] = continuous_`
 Indicates whether the capture job is to run continuously (1), or run only once (0). *continuous* is **bit** with a default of 1.  
  
 When *continuous* = 1, the [sp_cdc_scan](../../relational-databases/system-stored-procedures/sys-sp-cdc-scan-transact-sql.md) job scans the log and processes up to (*max_trans* \* *max_scans*) transactions. It then waits the number of seconds specified in *polling_interval* before beginning the next log scan.  
  
 When *continuous* = 0, the **sp_cdc_scan** job executes up to *max_scans* scans of the log, processing up to *max_trans* transaction during each scan, and then exits.  
  
 *continuous* is valid only for capture jobs.  
  
`[ @pollinginterval ] = polling\_interval_`
 Number of seconds between log scan cycles. *polling_interval* is **bigint** with a default of 5.  
  
 *polling_interval* is valid only for capture jobs when *continuous* is set to 1. If specified, the value cannot be negative and cannot exceed 24 hours. If a value of 0 is specified, there is no wait between log scans.  
  
`[ @retention ] = retention_`
 Number of minutes that change data rows are to be retained in change tables. *retention* is **bigint** with a default of 4320 (72 hours). The maximum value is 52494800 (100 years). If specified, the value must be a positive integer.  
  
 *retention* is valid only for cleanup jobs.  
  
`[ @threshold = ] 'delete\_threshold'`
 Maximum number of delete entries that can be deleted by using a single statement on cleanup. *delete_threshold* is **bigint** with a default of 5000.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 A cleanup job is created using the default values when the first table in the database is enabled for change data capture. A capture job is created using the default values when the first table in the database is enabled for change data capture and no transactional publications exist for the database. When a transactional publication exists, the transactional log reader is used to drive the capture mechanism, and a separate capture job is neither required nor allowed.  
  
 Because the cleanup and capture jobs are created by default, this stored procedure is necessary only when a job has been explicitly dropped and must be recreated.  
  
 The name of the job is **cdc.**_\<database\_name\>_**\_cleanup** or **cdc.**_\<database\_name\>_**\_capture**, where *<database_name>* is the name of the current database. If a job with the same name already exists, the name is appended with a period (**.**) followed by a unique identifier, for example: **cdc.AdventureWorks_capture.A1ACBDED-13FC-428C-8302-10100EF74F52**.  
  
 To view the current configuration of a cleanup or capture job, use [sp_cdc_help_jobs](../../relational-databases/system-stored-procedures/sys-sp-cdc-help-jobs-transact-sql.md). To change the configuration of a job, use [sp_cdc_change_job](../../relational-databases/system-stored-procedures/sys-sp-cdc-change-job-transact-sql.md).  
  
## Permissions  
 Requires membership in the **db_owner** fixed database role.  
  
## Examples  
  
### A. Creating a capture job  
 The following example creates a capture job. This example assumes that the existing cleanup job was explicitly dropped and must be recreated. The job is created using the default values.  
  
```  
USE AdventureWorks2012;  
GO  
EXEC sys.sp_cdc_add_job @job_type = N'capture';  
GO  
```  
  
### B. Creating a cleanup job  
 The following example creates a cleanup job in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database. The parameter `@start_job` is set to 0 and `@retention` is set to 5760 minutes (96 hours). This example assumes that the existing cleanup job was explicitly dropped and must be recreated.  
  
```  
USE AdventureWorks2012;  
GO  
EXEC sys.sp_cdc_add_job  
     @job_type = N'cleanup'  
    ,@start_job = 0  
    ,@retention = 5760;  
```  
  
## See Also  
 [dbo.cdc_jobs &#40;Transact-SQL&#41;](../../relational-databases/system-tables/dbo-cdc-jobs-transact-sql.md)   
 [sys.sp_cdc_enable_table &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sys-sp-cdc-enable-table-transact-sql.md)   
 [About Change Data Capture &#40;SQL Server&#41;](../../relational-databases/track-changes/about-change-data-capture-sql-server.md)  
  
  
