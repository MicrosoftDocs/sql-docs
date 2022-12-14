---
title: "dbo.cdc_jobs (Transact-SQL)"
description: dbo.cdc_jobs (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "cdc_jobs"
  - "cdc_jobs_TSQL"
helpviewer_keywords:
  - "dbo.cdc_jobs"
dev_langs:
  - "TSQL"
ms.assetid: 85e2d580-1c54-4b81-b7e6-2e12997199fd
---
# dbo.cdc_jobs (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Stores the change data capture configuration parameters for capture and cleanup jobs. This table is stored in **msdb**.  
  
 
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**database_id**|**int**|The ID of the database in which the job is run.|  
|**job_type**|**nvarchar(20)**|The type of job, either 'capture' or 'cleanup'.|  
|**job_id**|**uniqueidentifier**|A unique ID associated with the job.|  
|**maxtrans**|**int**|The maximum number of transactions to process in each scan cycle.<br /><br /> **maxtrans** is valid only for capture jobs.|  
|**maxscans**|**int**|The maximum number of scan cycles to execute in order to extract all rows from the log.<br /><br /> **maxscans** is valid only for capture jobs.|  
|**continuous**|**bit**|A flag indicating whether the capture job is to run continuously (1), or run in one-time mode (0). For more information, see [sys.sp_cdc_add_job &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sys-sp-cdc-add-job-transact-sql.md).<br /><br /> **continuous** is valid only for capture jobs.|  
|**pollinginterval**|**bigint**|The number of seconds between log scan cycles.<br /><br /> **pollinginterval** is valid only for capture jobs.|  
|**retention**|**bigint**|The number of minutes that change rows are to be retained in change tables.<br /><br /> **retention** is valid only for cleanup jobs.|  
|**threshold**|**bigint**|The maximum number of delete entries that can be deleted using a single statement on cleanup.|  
  
## See Also  
 [sys.sp_cdc_add_job &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sys-sp-cdc-add-job-transact-sql.md)   
 [sys.sp_cdc_change_job &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sys-sp-cdc-change-job-transact-sql.md)   
 [sys.sp_cdc_help_jobs &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sys-sp-cdc-help-jobs-transact-sql.md)   
 [sys.sp_cdc_drop_job &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sys-sp-cdc-drop-job-transact-sql.md)  
  
  
