---
title: "sys.dm_fts_index_keywords_position_by_document (Transact-SQL)"
description: sys.dm_fts_index_keywords_position_by_document (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_fts_index_keywords_position_by_document_TSQL"
  - "dm_fts_index_keywords_position_by_document_TSQL"
  - "dm_fts_index_keywords_position_by_document"
  - "sys.dm_fts_index_keywords_position_by_document"
helpviewer_keywords:
  - "sys.dm_fts_index_keywords_position_by_document dynamic management view"
dev_langs:
  - "TSQL"
ms.assetid: 0d70184f-baa2-411b-a32d-a4c5af890edd
---
# sys.dm_fts_index_keywords_position_by_document (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns keyword positional information in the indexed documents.  
  
## Syntax  
  
```  
sys.dm_fts_index_keywords_position_by_document  
(   
    DB_ID('database_name'),   
OBJECT_ID('table_name')   
)  
```  
  
## Arguments  
 db_id('*database_name*')  
 A call to the [DB_ID()](../../t-sql/functions/db-id-transact-sql.md) function. This function accepts a database name and returns the database ID, which sys.dm_fts_index_keywords_position_by_document uses to find the specified database.  
  
 object_id('*table_name*')  
 A call to the [OBJECT_ID()](../../t-sql/functions/object-id-transact-sql.md) function. This function accepts a table name and returns the table ID of the table containing the full-text index to inspect.  
  
## Table Returned  
  
|Column|Data type|Description|  
|------------|---------------|-----------------|  
|keyword|**varbinary(128)**|Binary string representing the keyword.|  
|display_term|**nvarchar(4000)**|The human-readable format of the keyword. This format is derived from the internal format that is stored in the full-text index.|  
|column_id|**int**|ID of the column from which the current keyword was full-text indexed.|  
|document_id|**bigint**|ID of the document or row from which the current term was full-text indexed. This ID corresponds to the full-text key value of that document or row.|  
|position|**int**|The position of the keyword in the document.|  
  
## Remarks  
 Use the DMV to identify the location of indexed words in indexed documents. This DMV can be used to troubleshoot issues when **sys.dm_fts_index_keywords_by_document** indicates the words are in the full-text index, but when you run a query using those words, the document is not returned.  
  
## Permissions  
 Requires SELECT permission on the columns covered by the full-text index and CREATE FULLTEXT CATALOG permissions.  
  
## Examples  
 The following example returns keywords from the full-text index of the `Production.Document` table of the `AdventureWorks` sample database.  
  
```  
USE AdventureWorks2012;  
GO   
  
SELECT * FROM sys.dm_fts_index_keywords_position_by_document  
(   
    DB_ID('AdventureWorks2012'),  
    OBJECT_ID('AdventureWorks2012.Production.Document')   
);   
GO  
```  
  
 You can add a predicate on the other columns_id as in the following example query, to further isolate the locations.  
  
```  
SELECT * FROM sys.dm_fts_index_keywords_position_by_document  
(   
    DB_ID('AdventureWorks2012'),  
    OBJECT_ID('AdventureWorks2012.Production.Document')   
)  
WHERE document_id = 7 AND display_term = 'performance';  
```  
  
## See Also  
 [Full-Text Search](../../relational-databases/search/full-text-search.md)   
 [Improve the Performance of Full-Text Indexes](../../relational-databases/search/improve-the-performance-of-full-text-indexes.md)   
 [Full-Text Search and Semantic Search Functions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/full-text-search-and-semantic-search-functions-transact-sql.md)   
 [Full-Text Search and Semantic Search Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/full-text-and-semantic-search-dynamic-management-views-functions.md)   
 [Full-Text Search and Semantic Search Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/full-text-search-and-semantic-search-stored-procedures-transact-sql.md)   
 [Search Document Properties with Search Property Lists](../../relational-databases/search/search-document-properties-with-search-property-lists.md)   
 [sys.dm_fts_index_keywords_by_document &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-fts-index-keywords-by-document-transact-sql.md)  
  
  
