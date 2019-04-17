---
title: "dbo.sysjobhistory (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "dbo.sysjobhistory_TSQL"
  - "dbo.sysjobhistory"
  - "sysjobhistory"
  - "sysjobhistory_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sysjobhistory system table"
ms.assetid: 1b1fcdbb-2af2-45e6-bf3f-e8279432ce13
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# dbo.sysjobhistory (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Contains information about the execution of scheduled jobs by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent. This table is stored in the **msdb** database.  
  
> **NOTE:** Data is updated only after the jobstep completes.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**instance_id**|**int**|Unique identifier for the row.|  
|**job_id**|**uniqueidentifier**|Job ID.|  
|**step_id**|**int**|ID of the step in the job.|  
|**step_name**|**sysname**|Name of the step.|  
|**sql_message_id**|**int**|ID of any [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error message returned if the job failed.|  
|**sql_severity**|**int**|Severity of any [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error.|  
|**message**|**nvarchar(4000)**|Text, if any, of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error.|  
|**run_status**|**int**|Status of the job execution:<br /><br /> **0** = Failed<br /><br /> **1** = Succeeded<br /><br /> **2** = Retry<br /><br /> **3** = Canceled<br /><br /> **4** = In Progress|  
|**run_date**|**int**|Date the job or step started execution. For an In Progress history, this is the date/time the history was written.|  
|**run_time**|**int**|Time the job or step started.|  
|**run_duration**|**int**|Elapsed time in the execution of the job or step in **HHMMSS** format.|  
|**operator_id_emailed**|**int**|ID of the operator notified when the job completed.|  
|**operator_id_netsent**|**int**|ID of the operator notified by a message when the job completed.|  
|**operator_id_paged**|**int**|ID of the operator notified by pager when the job completed.|  
|**retries_attempted**|**int**|Number of retry attempts for the job or step.|  
|**server**|**sysname**|Name of the server where the job was executed.|  
  
  ## Example
 The following [!INCLUDE[tsql](../../includes/tsql-md.md)] query will convert the **run_time** and **run_duration** columns into a more user friendly format.  Execute the script in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].
 
 ```sql
 SET NOCOUNT ON;
 
 SELECT sj.name,
		sh.run_date,
		sh.step_name,
		STUFF(STUFF(RIGHT(REPLICATE('0', 6) +  CAST(sh.run_time as varchar(6)), 6), 3, 0, ':'), 6, 0, ':') 'run_time',
		STUFF(STUFF(STUFF(RIGHT(REPLICATE('0', 8) + CAST(sh.run_duration as varchar(8)), 8), 3, 0, ':'), 6, 0, ':'), 9, 0, ':') 'run_duration (DD:HH:MM:SS)  '
FROM msdb.dbo.sysjobs sj
JOIN msdb.dbo.sysjobhistory sh
ON sj.job_id = sh.job_id
```
