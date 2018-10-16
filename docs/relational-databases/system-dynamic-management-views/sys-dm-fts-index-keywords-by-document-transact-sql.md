---
title: "sys.dm_fts_index_keywords_by_document (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_fts_index_keywords_by_document_TSQL"
  - "dm_fts_index_keywords_by_document_TSQL"
  - "sys.dm_fts_index_keywords_by_document"
  - "dm_fts_index_keywords_by_document"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "full-text search [SQL Server], troubleshooting"
  - "sys.dm_fts_index_keywords_by_document dynamic management function"
  - "full-text search [SQL Server], viewing keywords"
ms.assetid: 793b978b-c8a1-428c-90c2-a3e49d81b5c9
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: ">=aps-pdw-2016||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_fts_index_keywords_by_document (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-pdw-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-pdw-md.md)]

  Returns information about the document-level content of a full-text index associated with the specified table.  
  
 sys.dm_fts_index_keywords_by_document is a dynamic management function.  
  
 **To view higher-level full-text index information**  
  
-   [sys.dm_fts_index_keywords &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-fts-index-keywords-transact-sql.md)  
  
 **To view information about property-level content related to a document property**  
  
-   [sys.dm_fts_index_keywords_by_property &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-fts-index-keywords-by-property-transact-sql.md)  
  
## Syntax  
  
```  
  
sys.dm_fts_index_keywords_by_document  
(   
    DB_ID('database_name'),     OBJECT_ID('table_name')   
)  
```  
  
## Arguments  
 db_id('*database_name*')  
 A call to the [DB_ID()](../../t-sql/functions/db-id-transact-sql.md) function. This function accepts a database name and returns the database ID, which sys.dm_fts_index_keywords_by_document uses to find the specified database. If *database_name* is omitted, the current database ID is returned.  
  
 object_id('*table_name*')  
 A call to the [OBJECT_ID()](../../t-sql/functions/object-id-transact-sql.md) function. This function accepts a table name and returns the table ID of the table containing the full-text index to inspect.  
  
## Table Returned  
  
|Column|Data type|Description|  
|------------|---------------|-----------------|  
|keyword|**nvarchar(4000)**|The hexadecimal representation of the keyword that is stored inside the full-text index.<br /><br /> Note: OxFF represents the special character that indicates the end of a file or dataset.|  
|display_term|**nvarchar(4000)**|The human-readable format of the keyword. This format is derived from the internal format that is stored in the full-text index.<br /><br /> Note: OxFF represents the special character that indicates the end of a file or dataset.|  
|column_id|**int**|ID of the column from which the current keyword was full-text indexed.|  
|document_id|**int**|ID of the document or row from which the current term was full-text indexed. This ID corresponds to the full-text key value of that document or row.|  
|occurrence_count|**int**|Number of occurrences of the current keyword in the document or row that is indicated by **document_id**. When '*search_property_name*' is specified, occurrence_count displays only the number of occurrences of the current keyword in the specified search property within the document or row.|  
  
## Remarks  
 The information returned by sys.dm_fts_index_keywords_by_document is useful for finding out the following, among other things:  
  
-   The total number of keywords that a full-text index contains.  
  
-   Whether a keyword is part of a given document or row.  
  
-   How many times a keyword appears in the whole full-text index; that is:  
  
     ([SUM](../../t-sql/functions/sum-transact-sql.md)(**occurrence_count**) WHERE **keyword**=*keyword_value* )  
  
-   How many times a keyword appears in a given document or row.  
  
-   How many keywords a given document or row contains.  
  
 Also, you can also use the information provided by sys.dm_fts_index_keywords_by_document to retrieve all the keywords belonging to a given document or row.  
  
 When the full-text key column is an integer data type, as recommended, the document_id maps directly to the full-text key value in the base table.  
  
 In contrast, when the full-text key column uses a non-integer data type, document_id does not represent the full-text key in the base table. In this case, to identify the row in the base table that is returned by dm_fts_index_keywords_by_document, you need to join this view with the results returned by [sp_fulltext_keymappings](../../relational-databases/system-stored-procedures/sp-fulltext-keymappings-transact-sql.md). Before you can join them, you must store the output of the stored procedure in a temp table. Then you can join the document_id column of dm_fts_index_keywords_by_document with the DocId column that is returned by this stored procedure. Note that a **timestamp** column cannot receive values at insert time, because they are auto-generated by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Therefore, the **timestamp** column must be converted to **varbinary(8)** columns. The following example shows these steps. In this example, *table_id* is the ID of your table, *database_name* is the name of your database, and *table_name* is the name of your table.  
  
```  
USE database_name;  
GO  
CREATE TABLE #MyTempTable   
   (  
      docid INT PRIMARY KEY ,  
      [key] INT NOT NULL  
   );  
DECLARE @db_id int = db_id(N'database_name');  
DECLARE @table_id int = OBJECT_ID(N'table_name');  
INSERT INTO #MyTempTable EXEC sp_fulltext_keymappings @table_id;  
SELECT * FROM sys.dm_fts_index_keywords_by_document   
   ( @db_id, @table_id ) kbd  
   INNER JOIN #MyTempTable tt ON tt.[docid]=kbd.document_id;  
GO  
  
```  
  
## Permissions  
 Requires SELECT permission on the columns covered by the full-text index and CREATE FULLTEXT CATALOG permissions.  
  
## Examples  
  
### A. Displaying full-text index content at the document level  
 The following example displays the content of the full-text index at the document level in the `HumanResources.JobCandidate` table of the `AdventureWorks2012` sample database.  
  
> [!NOTE]  
>  You can create this index by executing the example provided for the `HumanResources.JobCandidate` table in [CREATE FULLTEXT INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-fulltext-index-transact-sql.md).  
  
```  
SELECT * FROM sys.dm_fts_index_keywords_by_document(db_id('AdventureWorks'),   
object_id('HumanResources.JobCandidate'));  
GO  
```  
  
## See Also  
 [Full-Text Search and Semantic Search Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/full-text-and-semantic-search-dynamic-management-views-functions.md)   
 [Full-Text Search](../../relational-databases/search/full-text-search.md)   
 [sys.dm_fts_index_keywords &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-fts-index-keywords-transact-sql.md)   
 [sys.dm_fts_index_keywords_by_property &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-fts-index-keywords-by-property-transact-sql.md)   
 [sp_fulltext_keymappings &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-fulltext-keymappings-transact-sql.md)   
 [Improve the Performance of Full-Text Indexes](../../relational-databases/search/improve-the-performance-of-full-text-indexes.md)  
  
  
