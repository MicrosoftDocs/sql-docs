---
title: "MSmerge_articlehistory (Transact-SQL)"
description: MSmerge_articlehistory (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "MSmerge_articlehistory"
  - "MSmerge_articlehistory_TSQL"
helpviewer_keywords:
  - "MSmerge_articlehistory system table"
dev_langs:
  - "TSQL"
ms.assetid: 2870e7ea-dbec-4636-9171-c2cee96018ac
---
# MSmerge_articlehistory (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **MSmerge_articlehistory** table tracks changes made to articles during a Merge Agent synchronization session, with one row for each article to which changes were made. This table is stored in the distribution database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**session_id**|**int**|The ID of a Merge Agent job session in the [MSmerge_sessions](../../relational-databases/system-tables/msmerge-sessions-transact-sql.md) system table.|  
|**phase_id**|**int**|The phase of the synchronization session, which can be one of the following:<br /><br /> **1** = Upload.<br /><br /> **2** = Download.<br /><br /> **4** = Clean-up.<br /><br /> **5** = Shutdown.<br /><br /> **6** = Schema changes.<br /><br /> **7** = BCP.|  
|**article_name**|**sysname**|The name of the article to which changes were made.|  
|**start_time**|**datetime**|The time when the agent began processing the article.|  
|**duration**|**int**|The length of time that the agent processed an article, in seconds.|  
|**inserts**|**int**|The number of inserts that have been applied to a specific article during synchronization. This value will increment during the synchronization process, and the ending value represents the total number.|  
|**updates**|**int**|The number of updates that have been applied to a specific article during synchronization. This value will increment during the synchronization process, and the ending value represents the total number.|  
|**deletes**|**int**|The number of deletes that have been applied to a specific article during synchronization. This value will increment during the synchronization process, and the ending value represents the total number.|  
|**conflicts**|**int**|The number of conflicts that occurred during synchronization. This value will increment during the synchronization process, and the ending value represents the total number.|  
|**conflicts_resolved**|**int**|The number of conflicts that occurred during synchronization that were resolved. This value will increment during the synchronization process, and the ending value represents the total number.|  
|**rows_retried**|**int**|The number of failed rows that were retried during synchronization. This value will increment during the synchronization process, and the ending value represents the total number.|  
|**percent_complete**|**decimal**|The percentage of the total synchronization time that the Merge Agent spent on the article during a session. This value is NULL until the session is complete.|  
|**estimated_changes**|**int**|An estimate of the number of row changes that must be applied to the article.|  
|**relative_cost**|**decimal**|The time spent applying changes for this article versus the total time for the entire session.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)  
  
  
