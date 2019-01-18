---
title: "MSmerge_dynamic_snapshots (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "MSmerge_dynamic_snapshots_TSQL"
  - "MSmerge_dynamic_snapshots"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "MSmerge_dynamic_snapshots system table"
ms.assetid: a5592b3c-731b-4fc9-ae4b-2602ed78248e
author: stevestein
ms.author: sstein
manager: craigg
---
# MSmerge_dynamic_snapshots (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  The **MSmerge_dynamic_snapshots** table tracks the location of the filtered data snapshot for each partition defined for a merge publication with parameterized row filters. This table is stored in the **publication** database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**partition_id**|**int**|The ID of the merge partition.|  
|**dynamic_snapshot_location**|**nvarchar(255)**|The location of the filtered data snapshot for the partition.|  
|**last_updated**|**datetime**|The date that the filtered data snapshot was refreshed.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)  
  
  
