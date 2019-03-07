---
title: "sys.dm_fts_semantic_similarity_population (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "dm_fts_semantic_similarity_population_TSQL"
  - "sys.dm_fts_semantic_similarity_population"
  - "dm_fts_semantic_similarity_population"
  - "sys.dm_fts_semantic_similarity_population_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_fts_semantic_similarity_population dynamic management view"
ms.assetid: 33666f28-c370-47e2-a932-190316ed5f69
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# sys.dm_fts_semantic_similarity_population (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns one row of status information about the population of the document similarity index for each similarity index in each table that has an associated semantic index.  
  
 The population step follows the extraction step. For status information about the similarity extraction step, see [sys.dm_fts_index_population &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-fts-index-population-transact-sql.md).  
    
||||  
|-|-|-|  
|**Column name**|**Type**|**Description**|  
|**database_id**|**int**|ID of the database that contains the full-text index being populated.|  
|**catalog_id**|**int**|ID of the full-text catalog that contains this full-text index.|  
|**table_id**|**int**|ID of the table for which the full-text index is being populated.|  
|**document_count**|**int**|Number of total documents in the population|  
|**document_processed_count**|**int**|Number of documents processed since start of this population cycle|  
|**completion_type**|**int**|Status of how this population completed.|  
|**completion_type_description**|**nvarchar(120)**|Description of the completion type.|  
|**worker_count**|**int**|Number of worker threads associated with similarity extraction|  
|**status**|**int**|Status of this Population. Note: some of the states are transient. One of the following:<br /><br /> 3 = Starting<br /><br /> 5 = Processing normally<br /><br /> 7 = Has stopped processing<br /><br /> 11 = Population aborted|  
|**status_description**|**nvarchar(120)**|Description of status of the population.|  
|**start_time**|**datetime**|Time that the population started.|  
|**incremental_timestamp**|**timestamp**|Represents the starting timestamp for a full population. For all other population types this value is the last committed checkpoint representing the progress of the populations.|  
  
## General Remarks  
 For more information, see [Manage and Monitor Semantic Search](../../relational-databases/search/manage-and-monitor-semantic-search.md).  
  
## Metadata  
 For more information about the status of semantic indexing, query [sys.dm_fts_index_population &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-fts-index-population-transact-sql.md).  
  
## Security  
  
### Permissions  
 Requires VIEW SERVER STATE permission on the server.  
  
## Examples  
 The following example shows how to query the status of document similarity index populations for all tables that have an associated semantic index:  
  
```  
SELECT * FROM sys.dm_fts_semantic_similarity_population;  
GO  
```  
  
## See Also  
 [Manage and Monitor Semantic Search](../../relational-databases/search/manage-and-monitor-semantic-search.md)  
  
  
