---
title: "MSmerge_identity_range (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "MSmerge_identity_range_TSQL"
  - "MSmerge_identity_range"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "MSmerge_identity_range system table"
ms.assetid: 493a2028-88a0-4e83-ad89-ae5661d9f477
author: stevestein
ms.author: sstein
manager: craigg
---
# MSmerge_identity_range (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  The **MSmerge_identity_range** table is used to track the numeric ranges assigned to identity columns for subscription to publications on which replication is automatically managing these range assignments. This table is stored in the publication and subscription databases.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**subid**|**uniqueidentifier**|The unique identification number for a given subscription.|  
|**artid**|**uniqueidentifier**|The unique identification number for the given article.|  
|**range_begin**|**numeric(38)**|The identity value at the start of the current range.|  
|**range_end**|**numeric(38)**|The identity value at the end of the current range.|  
|**next_range_begin**|**numeric(38)**|The identity value at the start of the next range to be assigned.|  
|**next_range_end**|**numeric(38)**|The identity value at the end of the next range to be assigned.|  
|**is_pub_range**|**bit**|A value of **1** if the identity range is assigned to the publication.|  
|**max_used**|**numeric(38)**|The maximum identity value that can be assigned.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
