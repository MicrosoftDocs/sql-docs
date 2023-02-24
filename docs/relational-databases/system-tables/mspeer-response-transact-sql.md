---
title: "MSpeer_response (Transact-SQL)"
description: MSpeer_response (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "MSpeer_response"
  - "MSpeer_response_TSQL"
helpviewer_keywords:
  - "MSpeer_response system table"
dev_langs:
  - "TSQL"
ms.assetid: 510e24cf-0292-47a9-b1d9-71a30fef030f
---
# MSpeer_response (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

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
  
  
