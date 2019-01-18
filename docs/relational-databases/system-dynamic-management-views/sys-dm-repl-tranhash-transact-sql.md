---
title: "sys.dm_repl_tranhash (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_repl_tranhash"
  - "sys.dm_repl_tranhash_TSQL"
  - "dm_repl_tranhash_TSQL"
  - "dm_repl_tranhash"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_repl_tranhash dynamic management view"
ms.assetid: 0cc52338-e805-4ed4-9835-b19bbf72448e
author: stevestein
ms.author: sstein
manager: craigg
---
# sys.dm_repl_tranhash (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns information about transactions being replicated in a transactional publication.  
  
|column_name|data_type|description|  
|------------------|----------------|-----------------|  
|**buckets**|**bigint**|Number of buckets in the hash table.|  
|**hashed_trans**|**bigint**|Number of committed transactions replicated in the current batch.|  
|**completed_trans**|**bigint**|Number of transactions competed so far.|  
|**compensated_trans**|**bigint**|Number of transactions that contain partial rollbacks.|  
|**first_begin_lsn**|**nvarchar(64)**|Earliest begin log sequence number (LSN) in the current batch.|  
|**last_commit_lsn**|**nvarchar(64)**|Last commit LSN in the current batch.|  
  
## Permissions  
 Requires VIEW DATABASE STATE permission on the publication database to call **dm_repl_tranhash**.  
  
## Remarks  
 Information is only returned for replicated database objects that are currently loaded in the replication article cache.  
  
## See Also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Replication Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/replication-related-dynamic-management-views-transact-sql.md)  
  
  
