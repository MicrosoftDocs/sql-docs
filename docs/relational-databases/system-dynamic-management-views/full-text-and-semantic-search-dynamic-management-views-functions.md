---
title: "Full-Text and Semantic Search Dynamic Management Views - Functions | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "dynamic management objects [SQL Server], full-text search"
  - "full-text search [SQL Server], dynamic management views"
ms.assetid: 199dbd5a-29f6-4ef0-8e65-86e32c0aaa3a
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Full-Text and Semantic Search Dynamic Management Views - Functions
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  This section contains the following dynamic management views and functions that are related to full-text search and semantic search.  
  
## Full-Text Search Dynamic Management Views and Functions  
 [sys.dm_fts_active_catalogs &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-fts-active-catalogs-transact-sql.md)  
 Returns information on the full-text catalogs that have some population activity in progress on the server.  
  
 [sys.dm_fts_fdhosts](../../relational-databases/system-dynamic-management-views/sys-dm-fts-fdhosts-transact-sql.md)  
 Returns information on the current activity of the filter daemon host or hosts on the server instance.  
  
 [sys.dm_fts_index_keywords](../../relational-databases/system-dynamic-management-views/sys-dm-fts-index-keywords-transact-sql.md)  
 Returns information about the content of a full-text index for the specified table.  
  
 [sys.dm_fts_index_keywords_by_document](../../relational-databases/system-dynamic-management-views/sys-dm-fts-index-keywords-by-document-transact-sql.md)  
 Returns information about the document-level content of a full-text index for the specified table. A given keyword can appear in several documents.  
  
 [sys.dm_fts_index_keywords_by_property](../../relational-databases/system-dynamic-management-views/sys-dm-fts-index-keywords-by-property-transact-sql.md)  
 Returns all property-related content in the full-text index of a given table. This includes all data that belongs to any property registered by the search property list associated with that full-text index.  
  
 sys.dm_fts_index_keywords_position_by_document  
 Returns the position of keywords in a document.  
  
 [sys.dm_fts_index_population](../../relational-databases/system-dynamic-management-views/sys-dm-fts-index-population-transact-sql.md)  
 Returns information about the full-text index populations currently in progress.  
  
 [sys.dm_fts_memory_buffers](../../relational-databases/system-dynamic-management-views/sys-dm-fts-memory-buffers-transact-sql.md)  
 Returns information about memory buffers belonging to a specific memory pool that are used as part of a full-text crawl or a full-text crawl range.  
  
 [sys.dm_fts_memory_pools](../../relational-databases/system-dynamic-management-views/sys-dm-fts-memory-pools-transact-sql.md)  
 Returns information about the shared memory pools available to the Full-Text Gatherer component for a full-text crawl or a full-text crawl range.  
  
 [sys.dm_fts_outstanding_batches](../../relational-databases/system-dynamic-management-views/sys-dm-fts-outstanding-batches-transact-sql.md)  
 Returns information about each full-text indexing batch.  
  
 [sys.dm_fts_parser](../../relational-databases/system-dynamic-management-views/sys-dm-fts-parser-transact-sql.md)  
 Returns the final tokenization result after applying a given word breaker, thesaurus, and stoplist combination to a query string input. The output is equivalent to the output if the specified given query string were issued to the Full-Text Engine.  
  
 [sys.dm_fts_population_ranges](../../relational-databases/system-dynamic-management-views/sys-dm-fts-population-ranges-transact-sql.md)  
 Returns information about the specific ranges related to a full-text index population currently in progress.  
  
## Semantic Search Dynamic Management Views and Functions  
 [sys.dm_fts_semantic_similarity_population &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-fts-semantic-similarity-population-transact-sql.md)  
 Returns one row of status information about the population of the document similarity index for each similarity index in each table that has an associated semantic index.  
  
## See Also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [System Views &#40;Transact-SQL&#41;](https://msdn.microsoft.com/library/35a6161d-7f43-4e00-bcd3-3091f2015e90)  
  
  
