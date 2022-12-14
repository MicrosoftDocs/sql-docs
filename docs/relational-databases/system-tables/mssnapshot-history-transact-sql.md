---
title: "MSsnapshot_history (Transact-SQL)"
description: MSsnapshot_history (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/04/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "MSsnapshot_history"
  - "MSsnapshot_history_TSQL"
helpviewer_keywords:
  - "MSsnapshot_history system table"
dev_langs:
  - "TSQL"
ms.assetid: 56bf4128-1689-4963-9343-432dd0898d31
---
# MSsnapshot_history (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **MSsnapshot_history** table contains history rows for the Snapshot Agents associated with the local Distributor. This table is stored in the distribution database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**agent_id**|**int**|The ID of the Snapshot Agent.|  
|**runstatus**|**int**|The running status:<br /><br /> **1** = Start.<br /><br /> **2** = Succeed.<br /><br /> **3** = In progress.<br /><br /> **4** = Idle.<br /><br /> **5** = Retry.<br /><br /> **6** = Fail.|  
|**start_time**|**datetime**|The time to begin execution of the job.|  
|**time**|**datetime**|The time the message is logged.|  
|**duration**|**int**|The duration, in seconds, of the message session.|  
|**comments**|**nvarchar(255)**|The message text.|  
|**delivered_transactions**|**int**|The total number of transactions delivered in the session.|  
|**delivered_commands**|**int**|The number of delivered commands per second.|  
|**delivery_rate**|**float(53)**|The average delivered commands per second.|  
|**error_id**|**int**|The ID of the error in the [MSrepl_errors](../../relational-databases/system-tables/msrepl-errors-transact-sql.md) system table.|  
|**timestamp**|**timestamp**|The timestamp column of this table.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
