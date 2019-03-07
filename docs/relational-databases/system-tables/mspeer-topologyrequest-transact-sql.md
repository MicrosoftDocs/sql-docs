---
title: "MSpeer_topologyrequest (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "MSpeer_topologyrequest_TSQL"
  - "MSpeer_topologyrequest"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "MSpeer_topologyrequest"
ms.assetid: c644814b-4e40-44d7-b6b4-5954b0d4db7c
author: stevestein
ms.author: sstein
manager: craigg
---
# MSpeer_topologyrequest (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Used in peer-to-peer replication to track topology status requests for a publication. This table is stored in the publication database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|id|**int**|Identifies a topology status request. The request_id column in [MSpeer_topologyresponse](../../relational-databases/system-tables/mspeer-topologyresponse-transact-sql.md) uses this value.|  
|publication|**sysname**|Name of the publication from which the topology status request originated.|  
|sent_date|**datetime**|Date and time that the topology status request was initiated.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
