---
title: "sys.dm_fts_outstanding_batches (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/29/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "dm_fts_outstanding_batches"
  - "dm_fts_outstanding_batches_TSQL"
  - "sys.dm_fts_outstanding_batches_TSQL"
  - "sys.dm_fts_outstanding_batches"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "troubleshooting [SQL Server], full-text search"
  - "sys.dm_fts_outstanding_batches dynamic management view"
ms.assetid: c4d697ed-c906-4c28-b137-036a25e13c84
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_fts_outstanding_batches (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns information about each full-text indexing batch.  
  
  |Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|database_id|**int**|ID of the database|  
|catalog_id|**int**|ID of the full-text catalog|  
|table_id|**int**|ID of the table ID that contains the full-text index|  
|batch_id|**int**|Batch ID|  
|memory_address|**varbinary(8)**|The batch object memory address|  
|crawl_memory_address|**varbinary(8)**|Crawl object memory address (parent object)|  
|memregion_memory_address|**varbinary(8)**|Memory region memory address of the outbound share memory of the filter daemon host (fdhost.exe)|  
|hr_batch|**int**|Most recent error code for the batch|  
|is_retry_batch|**bit**|Indicates whether this is a retry batch:<br /><br /> 0 = No<br /><br /> 1 = Yes|  
|retry_hints|**int**|Type of retry needed for the batch:<br /><br /> 0 = No retry<br /><br /> 1 = Multi thread retry<br /><br /> 2 = Single thread retry<br /><br /> 3 = Single and multi thread retry<br /><br /> 5 = Multi thread final retry<br /><br /> 6 = Single thread final retry<br /><br /> 7 = Single and multi thread final retry|  
|retry_hints_description|**nvarchar(120)**|Description for the type of retry needed:<br /><br /> NO RETRY<br /><br /> MULTI THREAD RETRY<br /><br /> SINGLE THREAD RETRY<br /><br /> SINGLE AND MULTI THREAD RETRY<br /><br /> MULTI THREAD FINAL RETRY<br /><br /> SINGLE THREAD FINAL RETRY<br /><br /> SINGLE AND MULTI THREAD FINAL RETRY|  
|doc_failed|**bigint**|Number of documents that failed in the batch|  
|batch_timestamp|**timestamp**|The timestamp value obtained when the batch was created|  
  
## Permissions  

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)], requires `VIEW SERVER STATE` permission.   
On [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)], requires the `VIEW DATABASE STATE` permission in the database.   
  
## Examples  
 The following example finds out how many batches are currently being processed for each table in the server instance.  
  
```  
SELECT database_id, table_id, COUNT(*) AS batch_count FROM sys.dm_fts_outstanding_batches GROUP BY database_id, table_id ;  
GO  
```  
  
## See Also  
 [Full-Text Search and Semantic Search Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/full-text-and-semantic-search-dynamic-management-views-functions.md)   
 [Full-Text Search](../../relational-databases/search/full-text-search.md)  
  
  
