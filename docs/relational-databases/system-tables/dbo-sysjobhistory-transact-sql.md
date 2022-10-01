---
title: "dbo.sysjobhistory (Transact-SQL)"
description: dbo.sysjobhistory (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "04/18/2022"
ms.prod: sql
ms.prod_service: "database-engine"
ms.technology: system-objects
ms.topic: "reference"
f1_keywords:
  - "dbo.sysjobhistory_TSQL"
  - "dbo.sysjobhistory"
  - "sysjobhistory"
  - "sysjobhistory_TSQL"
helpviewer_keywords:
  - "sysjobhistory system table"
dev_langs:
  - "TSQL"
ms.assetid: 1b1fcdbb-2af2-45e6-bf3f-e8279432ce13
---
# dbo.sysjobhistory (Transact-SQL)
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Contains information about the execution of scheduled jobs by the [SQL Server Agent](../../ssms/agent/sql-server-agent.md).
  
> [!NOTE]
> In most cases the data is updated only after the job step completes and the table typically contains no records for job steps that are currently in progress, but in some cases underlying processes *do* provide information about in progress job steps.

This table is stored in the **msdb** database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**instance_id**|**int**|Unique identifier for the row.|  
|**job_id**|**uniqueidentifier**|Job ID.|  
|**step_id**|**int**|ID of the step in the job.|  
|**step_name**|**sysname**|Name of the step.|  
|**sql_message_id**|**int**|ID of any [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error message returned if the job failed.|  
|**sql_severity**|**int**|Severity of any [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error.|  
|**message**|**nvarchar(4000)**|Text, if any, of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error.|  
|**run_status**|**int**|Status of the job execution:<br /><br /> **0** = Failed<br /><br /> **1** = Succeeded<br /><br /> **2** = Retry<br /><br /> **3** = Canceled<br /><br />**4** = In Progress|  
|**run_date**|**int**|Date the job or step started execution. For an In Progress history, this is the date/time the history was written.|  
|**run_time**|**int**|Time the job or step started in **HHMMSS** format.|  
|**run_duration**|**int**|Elapsed time in the execution of the job or step in **HHMMSS** format for time periods up to 24 hours. Find code to translate longer run durations in [Example](#example).|  
|**operator_id_emailed**|**int**|ID of the operator notified when the job completed.|  
|**operator_id_netsent**|**int**|ID of the operator notified by a message when the job completed.|  
|**operator_id_paged**|**int**|ID of the operator notified by pager when the job completed.|  
|**retries_attempted**|**int**|Number of retry attempts for the job or step.|  
|**server**|**sysname**|Name of the server where the job was executed.|  
  
## Example

The following [!INCLUDE[tsql](../../includes/tsql-md.md)] query converts the **run_time** and **run_duration** columns into a more user friendly format.  Execute the script in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].
 
 ```sql
SET NOCOUNT ON;
 
SELECT sj.name,
    sh.run_date,
    sh.step_name,
    STUFF(STUFF(STUFF(STUFF(STUFF(sh.run_time,13,0,':'),11,0,':'),9,0,' '),7,0,'-'),5,0,'-') AS LastRun_datetime,
    CASE 
        WHEN sh.run_duration > 235959 THEN
    		   CAST((CAST(LEFT(CAST(sh.run_duration AS VARCHAR), LEN(CAST(sh.run_duration AS VARCHAR)) - 4) AS INT) / 24) AS VARCHAR)
    		   + '.' + RIGHT('00' + CAST(CAST(LEFT(CAST(sh.run_duration AS VARCHAR), LEN(CAST(sh.run_duration AS VARCHAR)) - 4) AS INT) % 24 AS VARCHAR), 2)
    		   + ':' + STUFF(CAST(RIGHT(CAST(sh.run_duration AS VARCHAR), 4) AS VARCHAR(6)), 3, 0, ':')
    		ELSE
    		   STUFF(STUFF(RIGHT(REPLICATE('0', 6) + CAST(sh.run_duration AS VARCHAR(6)), 6), 3, 0, ':'), 6, 0, ':')
    		END AS 'LastRunDuration (d.HH:MM:SS) '
FROM msdb.dbo.sysjobs sj
JOIN msdb.dbo.sysjobhistory sh 
  ON sj.job_id = sh.job_id;
GO
```

## Next steps

Learn more about related concepts in the following articles:

- [dbo.sysjobactivity (Transact-SQL)](dbo-sysjobactivity-transact-sql.md)
- [dbo.sysjobs (Transact-SQL)](dbo-sysjobs-transact-sql.md)
- [dbo.sysjobsteps (Transact-SQL)](dbo-sysjobsteps-transact-sql.md)
- [SQL Server Agent Tables (Transact-SQL)](sql-server-agent-tables-transact-sql.md)
