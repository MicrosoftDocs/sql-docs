---
title: "sys.dm_db_fts_index_physical_stats (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/20/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_db_fts_index_physical_stats_TSQL"
  - "dm_db_fts_index_physical_stats"
  - "dm_db_fts_index_physical_stats_TSQL"
  - "sys.dm_db_fts_index_physical_stats"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_db_fts_index_physical_stats dynamic management view"
ms.assetid: 997c3278-3630-47f6-ada3-190b6c16ce0e
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_db_fts_index_physical_stats (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-asdb-xxxx-xxx-md.md)]

  Returns a row for each full-text or semantic index in each table that has an associated full-text or semantic index.  
  
||||  
|-|-|-|  
|**Column name**|**Type**|**Description**|  
|**object_id**|int|Object ID of the table that contains the index.|  
|**fulltext_index_page_count**|**bigint**|Logical size of the extraction in number of index pages.|  
|**keyphrase_index_page_count**|**bigint**|Logical size of the extraction in number of index pages.|  
|**similarity_index_page_count**|**bigint**|Logical size of the extraction in number of index pages.|  
  
## General Remarks  
 For more information, see [Manage and Monitor Semantic Search](../../relational-databases/search/manage-and-monitor-semantic-search.md).  
  
## Metadata  
 For information about the status of semantic indexing, query the following dynamic management views:  
  
-   [sys.dm_fts_index_population &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-fts-index-population-transact-sql.md)  
  
-   [sys.dm_fts_semantic_similarity_population &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-fts-semantic-similarity-population-transact-sql.md)  
  
## Permissions

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)], requires `VIEW SERVER STATE` permission.   
On [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)], requires the `VIEW DATABASE STATE` permission in the database.   

## Examples  
 The following example shows how to query for the logical size of each full-text or semantic index in every table that has an associated full-text or semantic index:  
  
```  
SELECT * FROM sys.dm_db_fts_index_physical_stats;  
GO  
```  
  
## See Also  
 [Manage and Monitor Semantic Search](../../relational-databases/search/manage-and-monitor-semantic-search.md)  
  
  
