---
title: "MScached_peer_lsns (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "MScached_peer_lsns_TSQL"
  - "MScached_peer_lsns"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "MScached_peer_lsns system table"
ms.assetid: f8b6089a-0230-45f9-8c34-9fe0d2a3a74e
author: stevestein
ms.author: sstein
manager: craigg
---
# MScached_peer_lsns (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  The **MScached_peer_lsns** table is used to track the LSN values in the transaction log that are used to determine which commands to return to a given Subscriber in peer-to-peer replication. This table is stored in the distribution database.  
  
## Definition  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**agent_id**|**int**|The ID of the Distribution Agent.|  
|**originator**|**sysname**|The name of the originating Publisher.|  
|**originator_db**|**sysname**|The name of the originating publication database.|  
|**originator_publication_id**|**int**|Identifies the originating publication.|  
|**originator_db_version**|**int**|Identifies the version number of the originating database.|  
|**originator_lsn**|**varbinary(16)**|The LSN of the originating transaction.|  
  
## Remarks  
 LSN values are only used immediately after insertion, and they have no lasting meaning in the system.  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
