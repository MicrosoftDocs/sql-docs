---
title: "MSmerge_identity_range_allocations (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "MSmerge_identity_range_allocations"
  - "MSmerge_identity_range_allocations_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "MSmerge_identity_range_allocations system table"
ms.assetid: 6362e35e-0ab3-4638-855b-1ce013f5fd6d
author: stevestein
ms.author: sstein
manager: craigg
---
# MSmerge_identity_range_allocations (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  The **MSmerge_identity_range_allocations** table is used to track the history of identity range assignments, to both Publishers and Subscribers, for published articles. This table is stored in the distribution database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**publisher_id**|**smallint**|The ID of the Publisher.|  
|**publisher_db**|**nvarchar(128)**|The name of the publication database.|  
|**publication**|**nvarchar(128)**|The name of the publication.|  
|**article**|**nvarchar(128)**|The name of the article.|  
|**subscriber**|**nvarchar(128)**|The name of the Subscriber.|  
|**subscriber_db**|**nvarchar(128)**|The name of the subscription database.|  
|**is_pub_range**|**bit**|Lists whether the identity range is assigned to a Publisher.|  
|**ranges_allocated**|**tinyint**|The number of identity ranges assigned.|  
|**range_begin**|**numeric(38)**|The starting value of the range.|  
|**range_end**|**numeric(38)**|The last value in the range.|  
|**next_range_begin**|**numeric(38)**|The starting value of the next range to be assigned.|  
|**next_range_end**|**numeric(38)**|The last value of the next range to be assigned.|  
|**max_used**|**numeric(38)**|The highest identity value used.|  
|**time_of_allocation**|**datetime**|The time when the assignment was made.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
