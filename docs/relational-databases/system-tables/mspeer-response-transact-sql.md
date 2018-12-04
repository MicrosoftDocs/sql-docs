---
title: "MSpeer_response (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "MSpeer_response"
  - "MSpeer_response_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "MSpeer_response system table"
ms.assetid: 510e24cf-0292-47a9-b1d9-71a30fef030f
author: stevestein
ms.author: sstein
manager: craigg
---
# MSpeer_response (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  The **MSpeer_response** table is used in Peer-to-Peer replication to store each node's response to a publication status request. This table is stored in the publication database.  
  
## Definition  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**request_id**|**int**|Identifies a status request entry in the [MSpeer_request](../../relational-databases/system-tables/mspeer-request-transact-sql.md) table.|  
|**peer**|**sysname**|The peer that generated the response.|  
|**peer_db**|**sysname**|The subscription database at the peer that generated the response.|  
|**received_date**|**datetime**|The date and time that the peer request was received.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)  
  
  
