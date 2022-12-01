---
title: "MSmerge_identity_range_allocations (Transact-SQL)"
description: MSmerge_identity_range_allocations (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/04/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "MSmerge_identity_range_allocations"
  - "MSmerge_identity_range_allocations_TSQL"
helpviewer_keywords:
  - "MSmerge_identity_range_allocations system table"
dev_langs:
  - "TSQL"
ms.assetid: 6362e35e-0ab3-4638-855b-1ce013f5fd6d
---
# MSmerge_identity_range_allocations (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

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
  
  
