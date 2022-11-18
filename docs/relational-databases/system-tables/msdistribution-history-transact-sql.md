---
title: "MSdistribution_history (Transact-SQL)"
description: MSdistribution_history (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "MSdistribution_history"
  - "MSdistribution_history_TSQL"
helpviewer_keywords:
  - "MSdistribution_history system table"
dev_langs:
  - "TSQL"
ms.assetid: 55665bd2-9e1d-4efc-8f60-c63a24f66b28
---
# MSdistribution_history (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **MSdistribution_history** table contains history rows for the Distribution Agents associated with the local Distributor. This table is stored in the distribution database.  
  
## Definition  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**agent_id**|**int**|The ID of the Distribution Agent.|  
|**runstatus**|**int**|The Running status:<br /><br /> **1** = Start.<br /><br /> **2** = Succeed.<br /><br /> **3** = In progress.<br /><br /> **4** = Idle.<br /><br /> **5** = Retry.<br /><br /> **6** = Fail.|  
|**start_time**|**datetime**|The time to begin execution of the job.|  
|**time**|**datetime**|The time the message is logged.|  
|**duration**|**int**|The duration, in seconds, of the message session.|  
|**comments**|**nvarchar(4000)**|The message text.|  
|**xact_seqno**|**varbinary(16)**|The last processed transaction sequence number.|  
|**current_delivery_rate**|**float**|The average number of commands delivered per second since the last history entry.|  
|**current_delivery_latency**|**int**|The latency between the command entering the distribution database and being applied to the Subscriber since the last history entry. In milliseconds.|  
|**delivered_transactions**|**int**|The total number of transactions delivered in the session.|  
|**delivered_commands**|**int**|The total number of commands delivered in the session.|  
|**average_commands**|**int**|The average number of commands delivered in the session.|  
|**delivery_rate**|**float**|The average delivered commands per second.|  
|**delivery_latency**|**int**|The latency between the command entering the distribution database and being applied to the Subscriber. In milliseconds.|  
|**total_delivered_commands**|**bigint**|The total number of commands delivered since the subscription was created.|  
|**error_id**|**int**|The ID of the error in the **MSrepl_error** system table.|  
|**updateable_row**|**bit**|Set to **1** if the history row can be overwritten.|  
|**timestamp**|**timestamp**|The timestamp column of this table.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
