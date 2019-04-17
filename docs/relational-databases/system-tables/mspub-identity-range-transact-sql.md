---
title: "MSpub_identity_range (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "MSpub_identity_range_TSQL"
  - "MSpub_identity_range"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "MSpub_identity_range system table"
ms.assetid: 68746eef-32e1-42bc-aff0-9798cd0e88b8
author: stevestein
ms.author: sstein
manager: craigg
---
# MSpub_identity_range (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  The **MSpub_identity_range** table provides identity range management support. This table is stored in the publication and subscription database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**objid**|**int**|The ID of the table that has the identity column being managed by replication.|  
|**range**|**bigint**|Controls the range size of the consecutive identity values that would be assigned at the subscription in an adjustment.|  
|**pub_range**|**bigint**|Controls the range size of the consecutive identity values that would be assigned at the publication in an adjustment.|  
|**current_pub_range**|**bigint**|The current range being used by the publication. It can be different than *pub_range* if viewed after being changed by **sp_changearticle** and before the next range adjustment.|  
|**threshold**|**int**|The percentage value that controls when the Distribution Agent assigns a new identity range. When the percentage of values specified in *threshold* is used, the Distribution Agent creates a new identity range.|  
|**last_seed**|**bigint**|The lower bound of the current range.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
