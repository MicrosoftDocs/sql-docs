---
title: "MSpeer_topologyrequest (Transact-SQL)"
description: MSpeer_topologyrequest (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "MSpeer_topologyrequest_TSQL"
  - "MSpeer_topologyrequest"
helpviewer_keywords:
  - "MSpeer_topologyrequest"
dev_langs:
  - "TSQL"
ms.assetid: c644814b-4e40-44d7-b6b4-5954b0d4db7c
---
# MSpeer_topologyrequest (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Used in peer-to-peer replication to track topology status requests for a publication. This table is stored in the publication database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|id|**int**|Identifies a topology status request. The request_id column in [MSpeer_topologyresponse](../../relational-databases/system-tables/mspeer-topologyresponse-transact-sql.md) uses this value.|  
|publication|**sysname**|Name of the publication from which the topology status request originated.|  
|sent_date|**datetime**|Date and time that the topology status request was initiated.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
