---
title: "MSreplservers (Transact-SQL)"
description: MSreplservers (Transact-SQL)
author: briancarrig
ms.author: brcarrig
ms.date: "10/06/2021"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "MSreplservers_TSQL"
  - "MSreplservers"
helpviewer_keywords:
  - "MSreplservers system table"
dev_langs:
  - "TSQL"
ms.assetid: 6d572526-b523-454c-bd20-9e7ae10c9191
---
# MSreplservers (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **MSrepl_agent_jobs** table supports replication distribution databases in an always on availability group. This table is used to propagate replication agent jobs such as LogReader and Snapshot to the MSDB database in secondary replicas of this availability group. In addition, these replication agent jobs need to be enabled on the newly elected primary replica and disabled on the old primary replica in the event of a failover happens.
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**job_id**|**uniqueidentifier**|The sysjob id remains consistent across replicas|  
|**name**|**sysname**|The name of the agent job. This name is used to construct sysproxies record name also.|
|**enabled**|**tinyint**|Replication agent jobs can be enabled or disabled.|
|**description**|**nvarchar(512)**|The agent job description.|
|**category_name**|**sysname**|Category of the agent job. Maps to msdb.dbo.syscategories.|
|**owner_login_name**|**nvarchar(60)**|This is the login that creates the replication agent job. It is typically the replication distribution database linked server login.|
|**notify_level_eventlog**|**int**|A value indicating when to place an entry in the Windows application log.|
Value Description **0** Never **1** On success **2** (default) On failure **3** Always|
|**notify_level_email**|**sysname**|A value that indicates when to send an e-mail upon the completion of this job. email_levelis int, with a default of 0, which indicates never. email_leveluses the same values as eventlog_level.|
|**notify_email_operator_name**|**sysname**|The e-mail name of the person to send e-mail to when email_level is reached. email_name is sysname, with a default of NULL.|
|**job_step_uid**|**uniqueidentifier**|**job_step_uid** contains the same value as column **step_uid** in table **dbo**.**sysjobsteps**|
|**job_login**|**uniqueinvarchar(4000)**|Used to look for existing credential in the sys.credentials catalog view.|
|**subsystem**|**nvarchar(40)**|Maps to msdb.dbo.syssubsystems.|
|**command**|**nvarchar(3200)**|The replication agent command text. The "-distributor" value points to the AG listener URL when this distribution db is in an AG.|
|**cmdexec_success_code**|**int**|
|**server**|**sysname**|
|**database_user_name**|**sysname**|Always set with the default value of NULL.|
|**retry_attempts**|**int**|
|**retry_interval**|**int**|
|**os_run_priority**|**int**|
|**job_login**|**nvarchar(257)**|
|**freq_type**|**int**|
|**freq_interval**|**int**|
|**freq_subday_type**|**int**|
|**freq_subday_interval**|**int**|
|**freq_relative_interval**|**int**|
|**freq_recurrence_factor**|**int**|
|**active_start_date**|**int**|
|**active_start_end**|**int**|
|**active_start_time**|**int**|
|**active_end_time**|**int**|
|**agent_id**|**int**|
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
