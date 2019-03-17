---
title: "MSmerge_generation_partition_mappings (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "MSmerge_generation_partition_mappings_TSQL"
  - "MSmerge_generation_partition_mappings"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "MSmerge_generation_partition_mappings system table"
ms.assetid: 443a4024-ce48-4772-9ee5-95bd6fb6476b
author: stevestein
ms.author: sstein
manager: craigg
---
# MSmerge_generation_partition_mappings (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  The **MSmerge_generation_partition_mappings** table is used to track changes to partitions in a merge publication. This table is stored in the publication and scubscription databases.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**publication_number**|**smallint**|Identifies the merge publication|  
|**generation**|**bigint**|The generation value.|  
|**partition_id**|**int**|Identifies the partition.|  
|**changecount**|**int**|The number of times that the partition has changed.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
