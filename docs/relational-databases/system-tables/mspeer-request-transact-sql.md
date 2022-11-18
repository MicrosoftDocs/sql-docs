---
title: "MSpeer_request (Transact-SQL)"
description: MSpeer_request (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "MSpeer_request"
  - "MSpeer_request_TSQL"
helpviewer_keywords:
  - "MSpeer_request system table"
dev_langs:
  - "TSQL"
ms.assetid: ed048c46-7a2f-4ad0-bc7c-c2d65e83b4fb
---
# MSpeer_request (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The MSpeer_request table is used in Peer-to-Peer replication to track status requests for a given publication. This table is stored in the publication database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|id|**int**|Identifies a request.|  
|publication|**sysname**|Name of the publication for which the status request was initiated.|  
|sent_date|**datetime**|Date and time that the status request was initiated.|  
|description|**nvarchar(4000)**|User-defined information that can be used to identify individual status requests.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
