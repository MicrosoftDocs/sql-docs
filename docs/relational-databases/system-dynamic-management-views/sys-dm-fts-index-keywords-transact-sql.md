---
title: "sys.dm_fts_index_keywords (Transact-SQL)"
description: sys.dm_fts_index_keywords (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_fts_index_keywords"
  - "sys.dm_fts_index_keywords"
  - "sys.dm_fts_index_keywords_TSQL"
  - "dm_fts_index_keywords_TSQL"
helpviewer_keywords:
  - "sys.dm_fts_index_keywords dynamic management function"
  - "full-text search [SQL Server], viewing keywords"
  - "troubleshooting [SQL Server], full-text search"
dev_langs:
  - "TSQL"
ms.assetid: fce7b2a1-7e74-4769-86a8-c77c7628decd
---
# sys.dm_fts_index_keywords (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns information about the content of a full-text index for the specified table.  
  
 **sys.dm_fts_index_keywords** is a dynamic management function.  
  
> [!NOTE]  
>  To view lower-level full-text index information, use the [sys.dm_fts_index_keywords_by_document](../../relational-databases/system-dynamic-management-views/sys-dm-fts-index-keywords-by-document-transact-sql.md) dynamic management function at the document level.  
  
## Syntax  
  
```  
  
sys.dm_fts_index_keywords( DB_ID('database_name'), OBJECT_ID('table_name') )  
```  
  
## Arguments  
 db_id('*database_name*')  
 A call to the [DB_ID()](../../t-sql/functions/db-id-transact-sql.md) function. This function accepts a database name and returns the database ID, which **sys.dm_fts_index_keywords** uses to find the specified database. If *database_name* is omitted, the current database ID is returned.  
  
 object_id('*table_name*')  
 A call to the [OBJECT_ID()](../../t-sql/functions/object-id-transact-sql.md) function. This function accepts a table name and returns the table ID of the table containing the full-text index to inspect.  
  
## Table Returned  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**keyword**|**nvarchar(4000)**|The hexadecimal representation of the keyword stored inside the full-text index.<br /><br /> Note: OxFF represents the special character that indicates the end of a file or dataset.|  
|**display_term**|**nvarchar(4000)**|The human-readable format of the keyword. This format is derived from the hexadecimal format.<br /><br /> Note: The **display_term** value for OxFF is "END OF FILE."|  
|**column_id**|**int**|ID of the column from which the current keyword was full-text indexed.|  
|**document_count**|**int**|Number of documents or rows containing the current term.|  
  
## Remarks  
 The information returned by **sys.dm_fts_index_keywords** is useful for finding out the following, among other things:  
  
-   Whether a keyword is part of the full-text index.  
  
-   How many documents or rows contain a given keyword.  
  
-   The most common keyword in the full-text index:  
  
    -   **document_count** of each *keyword_value* compared to the total **document_count**, the document count of 0xFF.  
  
    -   Typically, common keywords are likely to be appropriate to declare as stopwords.  
  
> [!NOTE]  
>  The **document_count** returned by **sys.dm_fts_index_keywords** may be less accurate for a specific document than the count returned by **sys.dm_fts_index_keywords_by_document** or a **CONTAINS** query. This potential inaccuracy is estimated to be less than 1%. This inaccuracy can occur because a **document_id** may be counted twice when it continues across more than one row in the index fragment, or when it appears more than once in the same row. To obtain a more accurate count for a specific document, use **sys.dm_fts_index_keywords_by_document** or a **CONTAINS** query.  
  
## Permissions  
 Requires membership in the **sysadmin** fixed server role.  
  
## Examples  
  
### A. Displaying high-level full-text index content  
 The following example displays information about the high-level content of the full-text index in the `HumanResources.JobCandidate` table.  
  
```  
SELECT * FROM sys.dm_fts_index_keywords(db_id('AdventureWorks2012'), object_id('HumanResources.JobCandidate'))  
GO  
```  
  
## See Also  
 [Full-Text Search and Semantic Search Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/full-text-and-semantic-search-dynamic-management-views-functions.md)   
 [Full-Text Search](../../relational-databases/search/full-text-search.md)   
 [sys.dm_fts_index_keywords_by_document &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-fts-index-keywords-by-document-transact-sql.md)  
  
  
