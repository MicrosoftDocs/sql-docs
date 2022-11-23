---
title: "CREATE FULLTEXT INDEX (Transact-SQL)"
description: CREATE FULLTEXT INDEX (Transact-SQL)
author: markingmyname
ms.author: maghan
ms.date: "04/05/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "FULLTEXT_INDEX_TSQL"
  - "CREATE_FULLTEXT_INDEX_TSQL"
  - "CREATE FULLTEXT INDEX"
  - "FULLTEXT INDEX"
helpviewer_keywords:
  - "full-text indexes [SQL Server], creating"
  - "index creation [SQL Server], CREATE FULLTEXT INDEX statement"
  - "CREATE FULLTEXT INDEX statement"
dev_langs:
  - "TSQL"
---
# CREATE FULLTEXT INDEX (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]	

  Creates a full-text index on a table or indexed view in a database in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Only one full-text index is allowed per table or indexed view, and each full-text index applies to a single table or indexed view. A full-text index can contain up to 1024 columns.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
CREATE FULLTEXT INDEX ON table_name  
   [ ( { column_name   
             [ TYPE COLUMN type_column_name ]  
             [ LANGUAGE language_term ]   
             [ STATISTICAL_SEMANTICS ]  
        } [ ,...n]   
      ) ]  
    KEY INDEX index_name   
    [ ON <catalog_filegroup_option> ]  
    [ WITH [ ( ] <with_option> [ ,...n] [ ) ] ]  
[;]  
  
<catalog_filegroup_option>::=  
 {  
    fulltext_catalog_name   
 | ( fulltext_catalog_name, FILEGROUP filegroup_name )  
 | ( FILEGROUP filegroup_name, fulltext_catalog_name )  
 | ( FILEGROUP filegroup_name )  
 }  
  
<with_option>::=  
 {  
   CHANGE_TRACKING [ = ] { MANUAL | AUTO | OFF [, NO POPULATION ] }   
 | STOPLIST [ = ] { OFF | SYSTEM | stoplist_name }  
 | SEARCH PROPERTY LIST [ = ] property_list_name   
 }  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
*table_name*       
Is the name of the table or indexed view that contains the column or columns included in the full-text index.  
  
*column_name*       
Is the name of the column included in the full-text index. Only columns of type **char**, **varchar**, **nchar**, **nvarchar**, **text**, **ntext**, **image**, **xml**, and **varbinary(max)** can be indexed for full-text search. To specify multiple columns, repeat the *column_name* clause as follows:  
  
CREATE FULLTEXT INDEX ON *table_name* (*column_name1* [...], *column_name2* [...]) ...  
  
TYPE COLUMN *type_column_name*       
Specifies the name of a table column, *type_column_name*, that is used to hold the document type for a **varbinary(max)** or **image** document. This column, known as the type column, contains a user-supplied file extension (.doc, .pdf, .xls, and so forth). The type column must be of type **char**, **nchar**, **varchar**, or **nvarchar**.  
  
Specify TYPE COLUMN *type_column_name* only if *column_name* specifies a **varbinary(max)** or **image** column, in which data is stored as binary data; otherwise, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns an error.  
  
> [!NOTE]  
> At indexing time, the Full-Text Engine uses the abbreviation in the type column of each table row to identify which full-text search filter to use for the document in *column_name*. The filter loads the document as a binary stream, removes the formatting information, and sends the text from the document to the word-breaker component. For more information, see [Configure and Manage Filters for Search](../../relational-databases/search/configure-and-manage-filters-for-search.md).  
  
LANGUAGE *language_term*       
Is the language of the data stored in *column_name*.  
  
*language_term* is optional and can be specified as a string, integer, or hexadecimal value corresponding to the locale identifier (LCID) of a language. If no value is specified, the default language of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance is used.  
  
If *language_term* is specified, the language it represents will be used to index data stored in **char**, **nchar**, **varchar**, **nvarchar**, **text**, and **ntext** columns. This language is the default language used at query time if *language_term* is not specified as part of a full-text predicate against the column.  
  
When specified as a string, *language_term* corresponds to the alias column value in the syslanguages system table. The string must be enclosed in single quotation marks, as in **'**_language\_term_**'**. When specified as an integer, *language_term* is the actual LCID that identifies the language. When specified as a hexadecimal value, *language_term* is 0x followed by the hex value of the LCID. The hex value must not exceed eight digits, including leading zeros.  
  
If the value is in double-byte character set (DBCS) format, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will convert it to Unicode.  
  
Resources, such as word breakers and stemmers, must be enabled for the language specified as *language_term*. If such resources do not support the specified language, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns an error.  
  
Use the sp_configure stored procedure to access information about the default full-text language of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. For more information, see [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md).  
  
For non-BLOB and non-XML columns containing text data in multiple languages, or for cases when the language of the text stored in the column is unknown, it might be appropriate for you to use the neutral (0x0) language resource. However, first you should understand the possible consequences of using the neutral (0x0) language resource. For information about the possible solutions and consequences of using the neutral (0x0) language resource, see [Choose a Language When Creating a Full-Text Index](../../relational-databases/search/choose-a-language-when-creating-a-full-text-index.md).  
  
For documents stored in XML- or BLOB-type columns, the language encoding within the document will be used at indexing time. For example, in XML columns, the **xml:lang** attribute in XML documents will identify the language. At query time, the value previously specified in *language_term* becomes the default language used for full-text queries unless *language_term* is specified as part of a full-text query.  
  
STATISTICAL_SEMANTICS       
**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later) 
  
Creates the additional key phrase and document similarity indexes that are part of statistical semantic indexing. For more information, see [Semantic Search &#40;SQL Server&#41;](../../relational-databases/search/semantic-search-sql-server.md).  
  
KEY INDEX *index_name*       
Is the name of the unique key index on *table_name*. The KEY INDEX must be a unique, single-key, non-nullable column. Select the smallest unique key index for the full-text unique key.  For the best performance, we recommend an integer data type for the full-text key.  
  
*fulltext_catalog_name*       
Is the full-text catalog used for the full-text index. The catalog must already exist in the database. This clause is optional. If it is not specified, a default catalog is used. If no default catalog exists, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns an error.  
  
FILEGROUP *filegroup_name*       
Creates the specified full-text index on the specified filegroup. The filegroup must already exist. If the FILEGROUP clause is not specified, the full-text index is placed in the same filegroup as base table or view for a nonpartitioned table or in the primary filegroup for a partitioned table.  
  
 CHANGE_TRACKING [ = ] { MANUAL | **AUTO** | OFF [ , NO POPULATION ] }       
 Specifies whether changes (updates, deletes or inserts) made to table columns that are covered by the full-text index will be propagated by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to the full-text index. Data changes through WRITETEXT and UPDATETEXT are not reflected in the full-text index, and are not picked up with change tracking.  
  
MANUAL       
Specifies that the tracked changes must be propagated manually by calling the ALTER FULLTEXT INDEX ... START UPDATE POPULATION [!INCLUDE[tsql](../../includes/tsql-md.md)] statement (*manual population*). You can use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent to call this [!INCLUDE[tsql](../../includes/tsql-md.md)] statement periodically.  
  
**AUTO**       
Specifies that the tracked changes will be propagated automatically as data is modified in the base table (*automatic population*). Although changes are propagated automatically, these changes might not be reflected immediately in the full-text index. AUTO is the default.  
  
OFF [ `,` NO POPULATION]       
Specifies that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not keep a list of changes to the indexed data. When NO POPULATION is not specified, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] populates the index fully after it is created.  
  
The NO POPULATION option can be used only when CHANGE_TRACKING is OFF. When NO POPULATION is specified, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not populate an index after it is created. The index is only populated after the user executes the ALTER FULLTEXT INDEX command with the START FULL POPULATION or START INCREMENTAL POPULATION clause.  
  
STOPLIST [ = ] { OFF | **SYSTEM** | *stoplist_name* }       
Associates a full-text stoplist with the index. The index is not populated with any tokens that are part of the specified stoplist. If STOPLIST is not specified, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] associates the system full-text stoplist with the index.  
  
OFF       
Specifies that no stoplist be associated with the full-text index.  
  
**SYSTEM**       
Specifies that the default full-text system STOPLIST should be used for this full-text index.  
  
*stoplist_name*       
Specifies the name of the stoplist to be associated with the full-text index.  
  
SEARCH PROPERTY LIST [ = ] *property_list_name*       
**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later)  
  
Associates a search property list with the index.  
 
OFF       
Specifies that no property list be associated with the full-text index.  
  
*property_list_name*       
Specifies the name of the search property list to associate with the full-text index.  
  
## Remarks  
For more information about full-text indexes, see [Create and Manage Full-Text Indexes](../../relational-databases/search/create-and-manage-full-text-indexes.md).  
  
On **xml** columns, you can create a full-text index that indexes the content of the XML elements, but ignores the XML markup. Attribute values are full-text indexed unless they are numeric values. Element tags are used as token boundaries. Well-formed XML or HTML documents and fragments containing multiple languages are supported. For more information, see [Use Full-Text Search with XML Columns](../../relational-databases/xml/use-full-text-search-with-xml-columns.md).  
  
We recommend that the index key column is an integer data type. This provides optimizations at query execution time.  
  
## Interactions of Change Tracking and NO POPULATION Parameter  
 Whether the full-text index is populated depends on whether change-tracking is enabled and whether WITH NO POPULATION is specified in the ALTER FULLTEXT INDEX statement. The following table summarizes the result of their interaction.  
  
|Change Tracking|WITH NO POPULATION|Result|  
|---------------------|------------------------|------------|  
|Not Enabled|Not specified|A full population is performed on the index.|  
|Not Enabled|Specified|No population of the index occurs until an ALTER FULLTEXT INDEX...START POPULATION statement is issued.|  
|Enabled|Specified|An error is raised, and the index is not altered.|  
|Enabled|Not specified|A full population is performed on the index.|  
  
 For more information about populating full-text indexes, see [Populate Full-Text Indexes](../../relational-databases/search/populate-full-text-indexes.md).  
  
## Permissions  
 User must have `REFERENCES` permission on the full-text catalog and have `ALTER` permission on the table or indexed view, or be a member of the `sysadmin` fixed server role, or `db_owner`, or `db_ddladmin` fixed database roles.  
  
If `SET STOPLIST` is specified, the user must have REFERENCES permission on the specified stoplist. The owner of the STOPLIST can grant this permission.  
  
> [!NOTE]  
> The public is granted REFERENCE permission to the default stoplist that is shipped with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Examples  
  
### A. Creating a unique index, a full-text catalog, and a full-text index  
 The following example creates a unique index on the `JobCandidateID` column of the `HumanResources.JobCandidate` table of the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] sample database. The example then creates a default full-text catalog, `ft`. Finally, the example creates a full-text index on the `Resume` column, using the `ft` catalog and the system stoplist.  
  
```sql  
CREATE UNIQUE INDEX ui_ukJobCand ON HumanResources.JobCandidate(JobCandidateID);  
CREATE FULLTEXT CATALOG ft AS DEFAULT;  
CREATE FULLTEXT INDEX ON HumanResources.JobCandidate(Resume)   
   KEY INDEX ui_ukJobCand   
   WITH STOPLIST = SYSTEM;  
GO  
```  
  
### B. Creating a full-text index on several table columns  
 The following example creates a full-text catalog, `production_catalog`, in the `AdventureWorks` sample database. The example then creates a full-text index that uses this new catalog. The full-text index is on the on the `ReviewerName`, `EmailAddress`, and `Comments` columns of the `Production.ProductReview`. For each column, the example specifies the LCID of English, `1033`, which is the language of the data in the columns. This full-text index uses an existing unique key index, `PK_ProductReview_ProductReviewID`. As recommended, this index key is on an integer column, `ProductReviewID`.  
  
```sql  
CREATE FULLTEXT CATALOG production_catalog;  
GO  
CREATE FULLTEXT INDEX ON Production.ProductReview  
 (   
  ReviewerName  
     Language 1033,  
  EmailAddress  
     Language 1033,  
  Comments   
     Language 1033       
 )   
  KEY INDEX PK_ProductReview_ProductReviewID   
      ON production_catalog;   
GO  
```  
  
### C. Creating a full-text index with a search property list without populating it  
 The following example creates a full-text index on the `Title`, `DocumentSummary`, and `Document` columns of the `Production.Document` table. The example specifies the LCID of English, `1033`, which is the language of the data in the columns. This full-text index uses the default full-text catalog and an existing unique key index, `PK_Document_DocumentID`. As recommended, this index key is on an integer column, `DocumentID`.  
  
 The example specifies the SYSTEM stoplist. It also specifies a search property list, `DocumentPropertyList`; for an example that creates this property list, see [CREATE SEARCH PROPERTY LIST &#40;Transact-SQL&#41;](../../t-sql/statements/create-search-property-list-transact-sql.md).  
  
 The example specifies that change tracking is off with no population. Later, during off-peak hours, the example uses an ALTER FULLTEXT INDEX statement to start a full population on the new index and enable automatic change tracking.  
  
```sql  
CREATE FULLTEXT INDEX ON Production.Document  
  (   
  Title  
      Language 1033,   
  DocumentSummary  
      Language 1033,   
  Document   
      TYPE COLUMN FileExtension  
      Language 1033   
  )  
  KEY INDEX PK_Document_DocumentID  
          WITH STOPLIST = SYSTEM, SEARCH PROPERTY LIST = DocumentPropertyList, CHANGE_TRACKING OFF, NO POPULATION;  
   GO  
```  
  
 Later, at an off-peak time, the index is populated:  
  
```sql  
ALTER FULLTEXT INDEX ON Production.Document SET CHANGE_TRACKING AUTO;  
GO  
```  
  
## See Also  
[Create and Manage Full-Text Indexes](../../relational-databases/search/create-and-manage-full-text-indexes.md)       
[ALTER FULLTEXT INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-fulltext-index-transact-sql.md)       
[DROP FULLTEXT INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/drop-fulltext-index-transact-sql.md)       
[Full-Text Search](../../relational-databases/search/full-text-search.md)       
[GRANT &#40;Transact-SQL&#41;](../../t-sql/statements/grant-transact-sql.md)       
[sys.fulltext_indexes &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-indexes-transact-sql.md)       
[Search Document Properties with Search Property Lists](../../relational-databases/search/search-document-properties-with-search-property-lists.md)       
  
  
