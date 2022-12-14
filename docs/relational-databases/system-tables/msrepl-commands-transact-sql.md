---
title: "MSrepl_commands (Transact-SQL)"
description: MSrepl_commands (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "MSrepl_commands"
  - "MSrepl_commands_TSQL"
helpviewer_keywords:
  - "MSrepl_commands system table"
dev_langs:
  - "TSQL"
ms.assetid: 53b9f9cd-9429-47a0-aba2-908fc60e7036
---
# MSrepl_commands (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **MSrepl_commands** table contains rows of replicated commands. This table is stored in the distribution database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**publisher_database_id**|**int**|The ID of the Publisher database.|  
|**xact_seqno**|**varbinary(16)**|The transaction sequence number.|  
|**type**|**int**|The command type.|  
|**article_id**|**int**|The ID of the article.|  
|**originator_id**|**int**|The ID of the originator.|  
|**command_id**|**int**|The ID of the command.|  
|**partial_command**|**bit**|Indicates whether this is a partial command or not.|  
|**command**|**varbinary(1024)**|The command value.|  
|**hashkey**|**int**|Internal-use only.|  
|**originator_lsn**|**varbinary(16)**|Identifies the LSN for the command in the originating publication. This is used in peer-to-peer transactional replication.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)   
 [sp_replcmds &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-replcmds-transact-sql.md)  
  
  
