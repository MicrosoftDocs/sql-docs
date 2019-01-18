---
title: "MSmerge_altsyncpartners (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "MSmerge_altsyncpartners_TSQL"
  - "MSmerge_altsyncpartners"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "MSmerge_altsyncpartners system table"
ms.assetid: da51b0f8-5ad0-4aeb-96ed-2b3672a2a6e2
author: stevestein
ms.author: sstein
manager: craigg
---
# MSmerge_altsyncpartners (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  The **MSmerge_altsyncpartners** table tracks the association of who the current synchronization partners are for a Publisher. This table is stored in the publication and subscription databases.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**subid**|**uniqueidentifier**|The identifier for the original Publisher.|  
|**alternate_subid**|**uniqueidentifier**|The identifier for the Subscriber who is the alternate synchronization partner.|  
|**description**|**nvarchar(255)**|The description of the alternate synchronization partner.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
