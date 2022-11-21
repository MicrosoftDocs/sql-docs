---
title: "MSmerge_sessions (Transact-SQL)"
description: MSmerge_sessions (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "MSmerge_sessions"
  - "MSmerge_sessions_TSQL"
helpviewer_keywords:
  - "MSmerge_sessions system table"
dev_langs:
  - "TSQL"
ms.assetid: 09ada8fc-c148-4379-9524-7826b1b0216c
---
# MSmerge_sessions (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **MSmerge_sessions** table contains history rows with the outcomes of previous Merge Agent job sessions. Each time that the Merge Agent is run, a new row is added to this table. This table is stored in the distribution database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**session_id**|**int**|The ID of the Merge Agent job session.|  
|**agent_id**|**int**|The ID of the Merge Agent.|  
|**start_time**|**datetime**|The time execution of the job began.|  
|**end_time**|**datetime**|The time execution of the job completed.|  
|**duration**|**int**|The cumulative duration, in seconds, of this job session.|  
|**delivery_time**|**int**|The number of seconds it took to apply a batch of changes.|  
|**upload_time**|**int**|The number of seconds it took to upload changes to the Publisher.|  
|**download_time**|**int**|The number of seconds it took to download changes to the Subscriber.|  
|**delivery_rate**|**float**|The average number of delivered commands per second.|  
|**time_remaining**|**int**|The estimated number of seconds left in an active session.|  
|**percent_complete**|**decimal**|The estimated percent of the total changes that have already been delivered in an active session.|  
|**upload_inserts**|**int**|The number of inserts applied at the Publisher.|  
|**upload_updates**|**int**|The number of updates applied at the Publisher.|  
|**upload_deletes**|**int**|The number of deletes applied at the Publisher.|  
|**upload_conflicts**|**int**|The number of conflicts that occurred while applying changes at the Publisher.|  
|**upload_conflicts_resolved**|**int**|The number of conflicts that occurred while applying changes at the Publisher that have been resolved.|  
|**upload_rows_retried**|**int**|The number of rows being uploaded to the Publisher that were retried.|  
|**download_inserts**|**int**|The number of inserts applied at the Subscriber.|  
|**download_updates**|**int**|The number of updates applied at the Subscriber.|  
|**download_deletes**|**int**|The number of deletes applied at the Subscriber.|  
|**download_conflicts**|**int**|The number of conflicts that occurred while applying changes at the Subscriber.|  
|**download_conflicts_resolved**|**int**|The number of conflicts that occurred while applying changes at the Subscriber that have been resolved.|  
|**download_rows_retried**|**int**|The number of rows being downloaded to the Subscriber that were retried.|  
|**schema_changes**|**int**|The number of schema changes applied during the session.|  
|**metadata_rows_cleanedup**|**int**|The number of rows of metadata cleaned-up during the session.|  
|**runstatus**|**int**|The running status:<br /><br /> **1** = Start.<br /><br /> **2** = Succeed.<br /><br /> **3** = In progress.<br /><br /> **4** = Idle.<br /><br /> **5** = Retry.<br /><br /> **6** = Fail.|  
|**estimated_upload_changes**|**int**|The estimated number of changes the need to be applied at the Publisher.|  
|**estimated_download_changes**|**int**|The estimated number of changes the need to be applied at the Subscriber.|  
|**connection_type**|**int**|The connection used during upload:<br /><br /> **1** = local area network (LAN).<br /><br /> **2** = dial-up network connection.<br /><br /> **3** = Web synchronization.|  
|**timestamp**|**timestamp**|The timestamp column of this table.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
