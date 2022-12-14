---
title: "MSmerge_history (Transact-SQL)"
description: MSmerge_history (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "MSmerge_history_TSQL"
  - "MSmerge_history"
helpviewer_keywords:
  - "MSmerge_history system table"
dev_langs:
  - "TSQL"
ms.assetid: 936195ad-ca07-41a8-a1a0-6699b6e63403
---
# MSmerge_history (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **MSmerge_history** table contains history rows with detailed descriptions of the outcomes of previous Merge Agent job sessions. This table contains one row for each line of agent output. This table is used in the distribution database and in each subscription database. In the distribution database, it contains history for all merge publications and subscriptions that use the Distributor. In each subscription database, it contains the history for publications to which the Subscriber is subscribed.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**session_id**|**int**|The ID of the Merge Agent job.|  
|**agent_id**|**int**|The ID of the Merge Agent.|  
|**comments**|**nvarchar(255)**|The message text.|  
|**error_id**|**int**|The ID of an error in the [MSrepl_errors](../../relational-databases/system-tables/msrepl-errors-transact-sql.md) system table.|  
|**timestamp**|**timestamp**|The timestamp column of this table.|  
|**updatable_row**|**bit**|Set to **1** if the history row can be overwritten.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
