---
title: "sys.dm_fts_index_keywords_by_property (Transact-SQL)"
description: sys.dm_fts_index_keywords_by_property (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_fts_index_keywords_by_property"
  - "dm_fts_index_keywords_by_property_TSQL"
  - "sys.dm_fts_index_keywords_by_property"
  - "sys.dm_fts_index_keywords_by_property_TSQL"
helpviewer_keywords:
  - "full-text search [SQL Server], troubleshooting"
  - "search property lists [SQL Server], viewing keywords by property"
  - "full-text search [SQL Server], viewing keywords"
  - "sys.dm_fts_index_keywords_by_property dynamic management view"
dev_langs:
  - "TSQL"
ms.assetid: fa41e052-a79a-4194-9b1a-2885f7828500
---
# sys.dm_fts_index_keywords_by_property (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns all property-related content in the full-text index of a given table. This includes all data that belongs to any property registered by the search property list associated with that full-text index.  
  
 sys.dm_fts_index_keywords_by_property is a dynamic management function that enables you to see what registered properties were emitted by IFilters at index time, as well as the exact content of every property in each indexed document.  
  
 **To view all document-level content (including property-related content)**  
  
-   [sys.dm_fts_index_keywords_by_document &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-fts-index-keywords-by-document-transact-sql.md)  
  
 **To view higher-level full-text index information**  
  
-   [sys.dm_fts_index_keywords &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-fts-index-keywords-transact-sql.md)  
  
> [!NOTE]  
>  For information about search property lists, see [Search Document Properties with Search Property Lists](../../relational-databases/search/search-document-properties-with-search-property-lists.md).  
  
## Syntax  
  
```  
  
sys.dm_fts_index_keywords_by_property  
(   
    DB_ID('database_name'),   
OBJECT_ID('table_name')   
)  
```  
  
## Arguments  
 db_id('*database_name*')  
 A call to the [DB_ID()](../../t-sql/functions/db-id-transact-sql.md) function. This function accepts a database name and returns the database ID, which sys.dm_fts_index_keywords_by_property uses to find the specified database. If *database_name* is omitted, the current database ID is returned.  
  
 object_id('*table_name*')  
 A call to the [OBJECT_ID()](../../t-sql/functions/object-id-transact-sql.md) function. This function accepts a table name and returns the table ID of the table containing the full-text index to inspect.  
  
## Table Returned  
  
|Column|Data type|Description|  
|------------|---------------|-----------------|  
|keyword|**nvarchar(4000)**|The hexadecimal representation of the keyword that is stored inside the full-text index.<br /><br /> Note: OxFF represents the special character that indicates the end of a file or dataset.|  
|display_term|**nvarchar(4000)**|The human-readable format of the keyword. This format is derived from the internal format that is stored in the full-text index.<br /><br /> Note: OxFF represents the special character that indicates the end of a file or dataset.|  
|column_id|**int**|ID of the column from which the current keyword was full-text indexed.|  
|document_id|**int**|ID of the document or row from which the current term was full-text indexed. This ID corresponds to the full-text key value of that document or row.|  
|property_id|**int**|Internal property ID of the search property within the full-text index of the table that you specified in the  OBJECT_ID('*table_name*') parameter.<br /><br /> When a given property is added to a search property list, the Full-Text Engine registers the property and assigns it an internal property ID that is specific to that property list. The internal property ID, which is an integer, is unique to a given search property list. If a given property is registered for multiple search property lists, a different internal property ID might be assigned for each search property list.<br /><br /> Note: The internal property ID is distinct from the property integer identifier that is specified when adding the property to the search property list. For more information, see [Search Document Properties with Search Property Lists](../../relational-databases/search/search-document-properties-with-search-property-lists.md).<br /><br /> To view the association between property_id and the property name:<br />                    [sys.registered_search_properties &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-registered-search-properties-transact-sql.md)|  
  
## Remarks  
 This dynamic management view can answer questions such as the following:  
  
-   What content is stored on a given property for a given DocID?  
  
-   How common is a given property among the indexed documents?  
  
-   What documents actually contain a given property? This is useful if querying on a given search property does not return a document that you expected to find.  
  
 When the full-text key column is an integer data type, as recommended, the document_id maps directly to the full-text key value in the base table.  
  
 In contrast, when the full-text key column uses a non-integer data type, document_id does not represent the full-text key in the base table. In this case, to identify the row in the base table that is returned by dm_fts_index_keywords_by_property, you need to join this view with the results returned by [sp_fulltext_keymappings](../../relational-databases/system-stored-procedures/sp-fulltext-keymappings-transact-sql.md). Before you can join them, you must store the output of the stored procedure in a temp table. Then you can join the document_id column of dm_fts_index_keywords_by_property with the DocId column that is returned by this stored procedure. Note that a **timestamp** column cannot receive values at insert time, because they are auto-generated by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Therefore, the **timestamp** column must be converted to **varbinary(8)** columns. The following example shows these steps. In this example, *table_id* is the ID of your table, *database_name* is the name of your database, and *table_name* is the name of your table.  
  
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
SELECT * FROM sys.dm_fts_index_keywords_by_property   
   ( @db_id, @table_id ) kbd  
   INNER JOIN #MyTempTable tt ON tt.[docid]=kbd.document_id;  
GO  
  
```  
  
## Permissions  
 Requires SELECT permission on the columns covered by the full-text index and CREATE FULLTEXT CATALOG permissions.  
  
## Examples  
 The following example returns keywords from the `Author` property in the full-text index of the `Production.Document` table of the `AdventureWorks` sample database. The example uses the alias `KWBPOP` for the table returned by **sys.dm_fts_index_keywords_by_property**. The example uses inner joins to combine columns from [sys.registered_search_properties](../../relational-databases/system-catalog-views/sys-registered-search-properties-transact-sql.md) and [sys.fulltext_indexes](../../relational-databases/system-catalog-views/sys-fulltext-indexes-transact-sql.md).  
  
```  
-- Once the full-text index is configured to support property searching  
-- on the Author property, return any keywords indexed for this property.  
USE AdventureWorks2012;  
GO   
SELECT KWBPOP.* FROM   
   sys.dm_fts_index_keywords_by_property( DB_ID(),   
   object_id('Production.Document') ) AS KWBPOP  
   INNER JOIN  
      sys.registered_search_properties AS RSP ON(   
         (KWBPOP.property_id = RSP.property_id)   
         AND (RSP.property_name = 'Author') )  
   INNER JOIN  
      sys.fulltext_indexes AS FTI ON(   
         (FTI.[object_id] = object_id('Production.Document'))   
         AND (RSP.property_list_id = FTI.property_list_id) );  
GO  
```  
  
## See Also  
 [Full-Text Search](../../relational-databases/search/full-text-search.md)   
 [Improve the Performance of Full-Text Indexes](../../relational-databases/search/improve-the-performance-of-full-text-indexes.md)   
 [sp_fulltext_keymappings &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-fulltext-keymappings-transact-sql.md)   
 [sys.dm_fts_index_keywords_by_document &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-fts-index-keywords-by-document-transact-sql.md)   
 [sys.dm_fts_index_keywords &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-fts-index-keywords-transact-sql.md)   
 [sys.registered_search_properties &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-registered-search-properties-transact-sql.md)   
 [sys.registered_search_property_lists &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-registered-search-property-lists-transact-sql.md)   
 [Search Document Properties with Search Property Lists](../../relational-databases/search/search-document-properties-with-search-property-lists.md)  
  
  
