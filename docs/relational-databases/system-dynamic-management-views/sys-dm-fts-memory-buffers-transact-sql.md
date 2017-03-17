---
title: "sys.dm_fts_memory_buffers (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "2016-06-10"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_fts_memory_buffers"
  - "dm_fts_memory_buffers_TSQL"
  - "dm_fts_memory_buffers"
  - "sys.dm_fts_memory_buffers_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_fts_memory_buffers dynamic management view"
ms.assetid: 56895fe5-e8df-4d75-9adc-c1f7757cdef8
caps.latest.revision: 33
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# sys.dm_fts_memory_buffers (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns information about memory buffers belonging to a specific memory pool that are used as part of a full-text crawl or a full-text crawl range.  
  
> [!NOTE]
> The following column will be removed in a future release of [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]: **row_count**. Avoid using this column in new development work, and plan to modify applications that currently use it.  

  
|Column|Data type|Description|  
|------------|---------------|-----------------|  
|**pool_id**|**int**|ID of the allocated memory pool.<br /><br /> 0 = Small buffers<br /><br /> 1 = Large buffers|  
|**memory_address**|**varbinary(8)**|Address of the allocated memory buffer.|  
|**name**|**nvarchar(4000)**|Name of the shared memory buffer for which this allocation was made.|  
|**is_free**|**bit**|Current state of memory buffer.<br /><br /> 0 = Free<br /><br /> 1 = Busy|  
|**row_count**|**int**|Number of rows that this buffer is currently handling.|  
|**bytes_used**|**int**|Amount, in bytes, of memory in use in this buffer.|  
|**percent_used**|**int**|Percentage of allocated memory used.|  
  
## Permissions  
 Requires VIEW SERVER STATE permission on the server.  
  
## Physical Joins  
 ![Significant joins of this dynamic management view](../../relational-databases/system-dynamic-management-views/media/join-dm-fts-memory-buffers-1.gif "Significant joins of this dynamic management view")  
  
## Relationship Cardinalities  
  
|From|To|Relationship|  
|----------|--------|------------------|  
|dm_fts_memory_buffers.pool_id|dm_fts_memory_pools.pool_id|Many-to-one|  
  
## See Also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../Topic/Dynamic%20Management%20Views%20and%20Functions%20\(Transact-SQL\).md)   
 [Full-Text Search and Semantic Search Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/full-text-and-semantic-search-dynamic-management-views-functions.md)  
  
  