---
title: "dbo.sysjobactivity (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/05/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "dbo.sysjobactivity_TSQL"
  - "dbo.sysjobactivity"
  - "sysjobactivity"
  - "sysjobactivity_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sysjobactivity system table"
ms.assetid: fd17cac9-5d1f-4b44-b2dc-ee9346d8bf1e
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# dbo.sysjobactivity (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Records current [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job activity and status.  This table is stored in the **msdb** database.
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**session_id**|**int**|ID of the session stored in the **syssessions** table in the **msdb** database.|  
|**job_id**|**uniqueidentifier**|ID of the job.|  
|**run_requested_date**|**datetime**|Date and time that the job was requested to run.|  
|**run_requested_source**|**sysname(nvarchar(128))**|Who requested the job to run.<br /><br /> **1** = SOURCE_SCHEDULER<br /><br /> **2** = SOURCE_ALERTER<br /><br /> **3** = SOURCE_BOOT<br /><br /> **4** = SOURCE_USER<br /><br /> **6** = SOURCE_ON_IDLE_SCHEDULE|  
|**queued_date**|**datetime**|Date and time this job was queued. If the job is run directly, this column is NULL.|  
|**start_execution_date**|**datetime**|Date and time job has been scheduled to run.|  
|**last_executed_step_id**|**int**|ID of the last job step that ran.|  
|**last_executed_step_**<br /><br /> **date**|**datetime**|Date and time that the last job step began to run.|  
|**stop_execution_date**|**datetime**|Date and time that the job finished running.|  
|**job_history_id**|**int**|Used to identify a row in the **sysjobhistory** table.|  
|**next_scheduled_run_date**|**datetime**|Next date and time that the job is scheduled to run.|  

## Example
This example will return the run-time status for all SQL Server Agent jobs.  Execute the following [!INCLUDE[tsql](../../includes/tsql-md.md)] in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].
```sql
SELECT sj.Name, 
	CASE
		WHEN sja.start_execution_date IS NULL THEN 'Not running'
		WHEN sja.start_execution_date IS NOT NULL AND sja.stop_execution_date IS NULL THEN 'Running'
		WHEN sja.start_execution_date IS NOT NULL AND sja.stop_execution_date IS NOT NULL THEN 'Not running'
	END AS 'RunStatus'
FROM msdb.dbo.sysjobs sj
JOIN msdb.dbo.sysjobactivity sja
ON sj.job_id = sja.job_id
WHERE session_id = (
	SELECT MAX(session_id) FROM msdb.dbo.sysjobactivity); 
```
  
## See Also  
 [dbo.sysjobhistory &#40;Transact-SQL&#41;](../../relational-databases/system-tables/dbo-sysjobhistory-transact-sql.md)  
  
  
