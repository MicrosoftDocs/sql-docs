---
title: "sys.sp_cdc_help_jobs (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_cdc_help_jobs"
  - "sys.sp_cdc_help_jobs_TSQL"
  - "sp_cdc_help_jobs_TSQL"
  - "sys.sp_cdc_help_jobs"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_cdc_help_jobs"
ms.assetid: 9399b4bc-8293-408f-b3cb-f904e0657fb5
author: rothja
ms.author: jroth
manager: craigg
---
# sys.sp_cdc_help_jobs (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Reports information about all change data capture cleanup or capture jobs in the current database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sys.sp_cdc_help_jobs  
```  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**job_id**|**uniqueidentifier**|The ID of the job.|  
|**job_type**|**nvarchar(20)**|The type of job.|  
|**maxtrans**|**int**|The maximum number of transactions to process in each scan cycle.<br /><br /> **maxtrans** is valid only for capture jobs.|  
|**maxscans**|**int**|The maximum number of scan cycles to execute in order to extract all rows from the log.<br /><br /> **maxscans** is valid only for capture jobs.|  
|**continuous**|**bit**|A flag indicating whether the capture job is to run continuously (1), or run in one-time mode (0). For more information, see [sys.sp_cdc_add_job &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sys-sp-cdc-add-job-transact-sql.md).<br /><br /> **continuous** is valid only for capture jobs.|  
|**pollinginterval**|**bigint**|The number of seconds between log scan cycles.<br /><br /> **pollinginterval** is valid only for capture jobs.|  
|**retention**|**bigint**|The number of minutes that change rows are to be retained in change tables.<br /><br /> **retention** is valid only for cleanup jobs.|  
|**threshold**|**bigint**|The maximum number of delete entries that can be deleted using a single statement on cleanup.|  
  
## Permissions  
 Requires membership in the **db_owner** fixed database role.  
  
## Examples  
 The following example returns information about the defined capture and cleanup jobs for the `AdventureWorks2012` database.  
  
```  
USE AdventureWorks2012;  
GO  
EXEC sys.sp_cdc_help_jobs;  
GO  
```  
  
## See Also  
 [dbo.cdc_jobs &#40;Transact-SQL&#41;](../../relational-databases/system-tables/dbo-cdc-jobs-transact-sql.md)   
 [sys.sp_cdc_add_job &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sys-sp-cdc-add-job-transact-sql.md)  
  
  
