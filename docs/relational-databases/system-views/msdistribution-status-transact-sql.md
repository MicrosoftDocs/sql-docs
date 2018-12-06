---
title: "MSdistribution_status (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "MSdistribution_status_TSQL"
  - "MSdistribution_status"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "MSdistribution_status view"
ms.assetid: 90d447de-3a4a-4f3e-aeab-e8fff6348361
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# MSdistribution_status (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  The **MSdistribution_status** view exposes additional information on the status commands in the distribution database. This view is stored in the distribution database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**article_id**|**int**|Identifies an article.|  
|**agent_id**|**int**|Identifies a replication agent.|  
|**UndelivCmdsInDistDB**|**int**|The number of commands pending delivery to Subscribers.|  
|**DelivCmdsInDistDB**|**int**|The number of commands delivered to Subscribers.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
