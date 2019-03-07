---
title: "sys.sp_cdc_scan (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.sp_cdc_scan_TSQL"
  - "sp_cdc_scan"
  - "sys.sp_cdc_scan"
  - "sp_cdc_scan_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_cdc_scan"
ms.assetid: 46e4294c-97b8-47d6-9ed9-b436a9929353
author: rothja
ms.author: jroth
manager: craigg
---
# sys.sp_cdc_scan (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Executes the change data capture log scan operation.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sys.sp_cdc_scan [ [ @maxtrans = ] max_trans ]   
     [ , [ @maxscans = ] max_scans ]   
     [ , [ @continuous = ] continuous ]   
     [ , [ @pollinginterval = ] polling_interval ]   
```  
  
## Arguments  
 [ **@maxtrans=** ] *max_trans*  
 Maximum number of transactions to process in each scan cycle. *max_trans* is **int** with a default of 500.  
  
 [ **@maxscans=** ] *max_scans*  
 Maximum number of scan cycles to execute in order to extract all rows from the log. *max_scans* is **int** with a default of 10.  
  
 [ **@continuous=** ] *continuous*  
 Indicates whether the stored procedure should end after executing a single scan cycle (0) or run continuously, pausing for the time specified by *polling_interval* before reexecuting the scan cycle (1). *continuous* is **tinyint** with a default of 0.  
  
 [ **@pollinginterval=** ] *polling_interval*  
 Number of seconds between log scan cycles. *polling_interval* is **bigint** with a default of 0.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 sys.sp_cdc_scan is called internally by sys.sp_MScdc_capture_job if the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent capture job is being used by change data capture. The procedure cannot be executed explicitly when a change data capture log scan operation is already active or when the database is enabled for transactional replication. This stored procedure should be used by administrators who want to customize the behavior of the capture job that is automatically configured.  
  
## Permissions  
 Requires membership in the db_owner fixed database role.  
  
## See Also  
 [dbo.cdc_jobs &#40;Transact-SQL&#41;](../../relational-databases/system-tables/dbo-cdc-jobs-transact-sql.md)  
  
  
