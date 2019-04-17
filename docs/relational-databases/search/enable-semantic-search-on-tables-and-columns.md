---
title: "Enable Semantic Search on Tables and Columns | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "search, sql-database"
ms.technology: search
ms.topic: conceptual
helpviewer_keywords: 
  - "semantic search [SQL Server], enabling"
ms.assetid: 895d220c-6749-4954-9dd3-2ea4c6a321ff
author: pmasl
ms.author: pelopes
ms.reviewer: mikeray
manager: craigg
---
# Enable Semantic Search on Tables and Columns
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Describes how to enable or disable statistical semantic indexing on selected columns that contain documents or text.  
  
 Statistical Semantic Search uses the indexes that are created by Full-Text Search, and creates additional indexes. As a result of this dependency on full-text search, you create a new semantic index when you define a new full-text index, or when you alter an existing full-text index. You can create a new semantic index by using [!INCLUDE[tsql](../../includes/tsql-md.md)] statements, or by using the Full-Text Indexing Wizard and other dialog boxes in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], as described in this topic.  
  
##  <a name="BasicEnabling"></a> Create a semantic index  
  
###  <a name="reqenable"></a> Requirements and restrictions for creating a semantic index  
  
-   You can create an index on any of the database objects that are supported for full-text indexing, including tables and indexed views.  
  
-   Before you can enable semantic indexing for specific columns, the following prerequisites must exist:  
  
    -   A full-text catalog must exist for the database.  
  
    -   The table must have a full-text index.  
  
    -   The selected columns must participate in the full-text index.  
  
     You can create and enable all these requirements at the same time.  
  
-   You can create a semantic index on columns that have any of the data types that are supported for full-text indexing. For more information, see [Create and Manage Full-Text Indexes](../../relational-databases/search/create-and-manage-full-text-indexes.md).  
  
-   You can specify any document type that is supported for full-text indexing for **varbinary(max)** columns. For more information, see [How To: Determine Which Document Types Can Be Indexed](#doctypes) in this topic.  
  
-   Semantic indexing creates two types of indexes for the columns that you select - an index of key phrases, and an index of document similarity. You cannot select only one type of index or the other when you enable semantic indexing. However you can query these two indexes independently. For more information, see [Find Key Phrases in Documents with Semantic Search](../../relational-databases/search/find-key-phrases-in-documents-with-semantic-search.md) and [Find Similar and Related Documents with Semantic Search](../../relational-databases/search/find-similar-and-related-documents-with-semantic-search.md).  
  
-   If you do not explicitly specify an LCID for a semantic index, then only the primary language and its associated language statistics are used for semantic indexing.  
  
-   If you specify a language for a column for which the language model is not available, the creation of the index fails and returns an error message.  
  
##  <a name="HowToEnableCreate"></a> Create a semantic index when there is no full-text index  
 When you create a new full-text index with the **CREATE FULLTEXT INDEX** statement, you can enable semantic indexing at the column level by specifying the keyword **STATISTICAL_SEMANTICS** as part of the column definition. You can also enable semantic indexing when you use the Full-Text Indexing Wizard to create a new full-text index.  
  
 ### Create a new semantic index by using Transact-SQL  
 
 Call the **CREATE FULLTEXT INDEX** statement and specify **STATISTICAL_SEMANTICS** for each column on which you want to create a semantic index. For more information about all the options for this statement, see [CREATE FULLTEXT INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-fulltext-index-transact-sql.md).  
  
 **Example 1: Create a unique index, full-text index, and semantic index**  
  
 The following example creates a default full-text catalog, **ft**. The example then creates a unique index on the **JobCandidateID** column of the **HumanResources.JobCandidate** table of the AdventureWorks2012 sample database. This unique index is required as the key column for a full-text index. The example then creates a full-text index and a semantic index on the **Resume** column.  
  
```sql  
CREATE FULLTEXT CATALOG ft AS DEFAULT  
GO  
  
CREATE UNIQUE INDEX ui_ukJobCand  
    ON HumanResources.JobCandidate(JobCandidateID)  
GO  
  
CREATE FULLTEXT INDEX ON HumanResources.JobCandidate  
    (Resume  
        Language 1033  
        Statistical_Semantics  
    )   
    KEY INDEX JobCandidateID   
    WITH STOPLIST = SYSTEM  
GO  
```  
  
 **Example 2: Create a full-text and semantic index on several columns with delayed index population**  
  
 The following example creates a full-text catalog, **documents_catalog**, in the AdventureWorks2012 sample database. The example then creates a full-text index that uses this new catalog. The full-text index is created on the **Title**, **DocumentSummary**, and **Document** columns of the **Production.Document** table, while the semantic index is only created on the **Document** column. This full-text index uses the newly-created full-text catalog and an existing unique key index, **PK_Document_DocumentID**. As recommended, this index key is created on an integer column, **DocumentID**. The example specifies the LCID for English, 1033, which is the language of the data in the columns.  
  
 This example also specifies that change tracking is off with no population. Later, during off-peak hours, the example uses an **ALTER FULLTEXT INDEX** statement to start a full population on the new index and enable automatic change tracking.  
  
```sql  
CREATE FULLTEXT CATALOG documents_catalog  
GO  
  
CREATE FULLTEXT INDEX ON Production.Document  
    (   
    Title  
        Language 1033,   
    DocumentSummary  
        Language 1033,   
    Document   
        TYPE COLUMN FileExtension  
        Language 1033  
        Statistical_Semantics  
    )  
    KEY INDEX PK_Document_DocumentID  
        ON documents_catalog  
        WITH CHANGE_TRACKING OFF, NO POPULATION  
GO  
```  
  
 Later, at an off-peak time, the index is populated:  
  
```sql  
ALTER FULLTEXT INDEX ON Production.Document SET CHANGE_TRACKING AUTO  
GO  
```  
  
### Create a new semantic index by using SQL Server Management Studio  
 Run the Full-Text Indexing Wizard and enable **Statistical Semantics** on the **Select Table Columns** page for each column on which you want to create a semantic index. For more information, including information about how to start the Full-Text Indexing Wizard, see [Use the Full-Text Indexing Wizard](../../relational-databases/search/use-the-full-text-indexing-wizard.md).  
  
##  <a name="HowToEnableAlter"></a> Create a semantic index when there is an existing full-text index  
 You can add semantic indexing when you alter an existing full-text index with the **ALTER FULLTEXT INDEX** statement. You can also add semantic indexing by using various dialog boxes in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
### Add a semantic index by using Transact-SQL  
 Call the **ALTER FULLTEXT INDEX** statement with the options described below for each column on which you want to add a semantic index. For more information about all the options for this statement, see [ALTER FULLTEXT INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-fulltext-index-transact-sql.md).  
  
 Both full-text and semantic indexes are repopulated after a call to **ALTER**, unless you specify otherwise.  
  
-   To add full-text indexing only to a column, use the **ADD** syntax.  
  
-   To add both full-text and semantic indexing to a column, use the **ADD** syntax with the **STATISTICAL_SEMANTICS** option.  
  
-   To add semantic indexing to a column that is already enabled for full-text indexing, use the **ADD STATISTICAL_SEMANTICS** option. You can only add semantic indexing to one column in a single **ALTER** statement.  
  
 **Example: Add semantic indexing to a column that already has full-text indexing**  
  
 The following example alters an existing full-text index on **Production.Document** table in AdventureWorks2012 sample database. The example adds a semantic index on the **Document** column of the **Production.Document** table, which already has a full-text index. The example specifies that the index will not be repopulated automatically.  
  
```sql  
ALTER FULLTEXT INDEX ON Production.Document  
    ALTER COLUMN Document  
        ADD Statistical_Semantics  
    WITH NO POPULATION  
GO  
```  
  
### Add a semantic index by using SQL Server Management Studio  
 You can change the columns that are enabled for semantic and full-text indexing on the **Full-Text Index Columns** page of the **Full-Text Index Properties** dialog box. For more information, see [Manage Full-Text Indexes](https://msdn.microsoft.com/library/28ff17dc-172b-4ac4-853f-990b5dc02fd1).  

## Alter a semantic index
  
###  <a name="addreq"></a> Requirements and restrictions for altering an existing index  
  
-   You cannot alter an existing index while population of the index is in progress. For more information on monitoring the progress of index population, see [Manage and Monitor Semantic Search](../../relational-databases/search/manage-and-monitor-semantic-search.md).  
  
-   You cannot add indexing to a column, and alter or drop indexing for the same column, in a single call to the **ALTER FULLTEXT INDEX** statement.  
  
##  <a name="dropping"></a> Drop a semantic index  
You can drop semantic indexing when you alter an existing full-text index with the **ALTER FULLTEXT INDEX** statement. You can also drop semantic indexing by using various dialog boxes in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
 ### Drop a semantic index by using Transact-SQL  
To drop semantic indexing only from a column or columns, call the **ALTER FULLTEXT INDEX** statement with the **ALTER COLUMN**_column\_name_**DROP STATISTICAL_SEMANTICS** option. You can drop the indexing from multiple columns in a single **ALTER** statement.  
  
```sql  
USE database_name  
GO  

ALTER FULLTEXT INDEX  
    ALTER COLUMN column_name  
    DROP STATISTICAL_SEMANTICS  
GO  
```  
  
To drop both full-text and semantic indexing from a column, call the **ALTER FULLTEXT INDEX** statement with the **ALTER COLUMN**_column\_name_**DROP** option.  
  
```sql  
USE database_name  
GO  
  
ALTER FULLTEXT INDEX  
    ALTER COLUMN column_name  
    DROP  
GO  
```  
  
 ### Drop a semantic index by using SQL Server Management Studio  
 You can change the columns that are enabled for semantic and full-text indexing on the **Full-Text Index Columns** page of the **Full-Text Index Properties** dialog box. For more information, see [Manage Full-Text Indexes](https://msdn.microsoft.com/library/28ff17dc-172b-4ac4-853f-990b5dc02fd1).  
  
###  <a name="dropreq"></a> Requirements and restrictions for dropping a semantic index  
  
-   You cannot drop full-text indexing from a column while retaining semantic indexing. Semantic indexing depends on full-text indexing for document similarity results.  
  
-   You cannot specify the **NO POPULATION** option when you drop semantic indexing from the last column in a table for which semantic indexing was enabled. A population cycle is required to remove the results that were indexed previously.  
  
## Check whether semantic search is enabled on database objects  
### Is semantic search enabled for a database?
  
 Query the **IsFullTextEnabled** property of the [DATABASEPROPERTYEX &#40;Transact-SQL&#41;](../../t-sql/functions/databasepropertyex-transact-sql.md) metadata function.  
  
 A return value of 1 indicates that full-text search and semantic search are enabled for the database; a return value of 0 indicates that they are not enabled.  
  
```sql  
SELECT DATABASEPROPERTYEX('database_name', 'IsFullTextEnabled')  
GO  
```  
  
### Is semantic search enabled for a table?  
 
 Query the **TableFullTextSemanticExtraction** property of the [OBJECTPROPERTYEX &#40;Transact-SQL&#41;](../../t-sql/functions/objectpropertyex-transact-sql.md) metadata function.  
  
 A return value of 1 indicates that semantic search is enabled for the table; a return value of 0 indicates that it is not enabled.  
  
```scr  
SELECT OBJECTPROPERTYEX(OBJECT_ID('table_name'), 'TableFullTextSemanticExtraction')  
GO  
```  
  
 ### Is semantic search enabled for a column?
   
 To determine whether semantic search is enabled for a specific column:  
  
-   Query the **StatisticalSemantics** property of the [COLUMNPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/columnproperty-transact-sql.md) metadata function.  
  
     A return value of 1 indicates that semantic search is enabled for the column; a return value of 0 indicates that it is not enabled.  
  
    ```sql  
    SELECT COLUMNPROPERTY(OBJECT_ID('table_name'), 'column_name', 'StatisticalSemantics')  
    GO  
    ```  
  
-   Query the catalog view [sys.fulltext_index_columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-index-columns-transact-sql.md) for the full-text index.  
  
     A value of 1 in the **statistical_semantics** column indicates that the specified column is enabled for semantic indexing in addition to full-text indexing.  
  
    ```sql  
    SELECT * FROM sys.fulltext_index_columns WHERE object_id = OBJECT_ID('table_name')  
    GO  
    ```  
  
-   In Object Explorer in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], right-click on a column and select **Properties**. On the **General** page of the **Column Properties** dialog box, check the value of the **Statistical Semantics** property.  
  
     A value of True indicates that the specified column is enabled for semantic indexing in addition to full-text indexing.  
  
## Determine what can be indexed for Semantic Search  
  
###  <a name="HowToCheckLanguages"></a> Check which languages are supported for Semantic Search  
  
> [!IMPORTANT]  
>  Fewer languages are supported for semantic indexing than for full-text indexing. As a result, there may be columns that you can index for full-text search, but not for semantic search.  
  
 Query the catalog view [sys.fulltext_semantic_languages &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-semantic-languages-transact-sql.md).  
  
```sql  
SELECT * FROM sys.fulltext_semantic_languages  
GO  
```  
  
 The following languages are supported for semantic indexing. This list represents the output of the catalog view [sys.fulltext_semantic_languages &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-semantic-languages-transact-sql.md), ordered by LCID.  
  
|Language|LCID|  
|--------------|----------|  
|German|1031|  
|English (US)|1033|  
|French|1036|  
|Italian|1040|  
|Portuguese (Brazil)|1046|  
|Russian|1049|  
|Swedish|1053|  
|English (UK)|2057|  
|Portuguese (Portugal)|2070|  
|Spanish|3082|  
  
###  <a name="doctypes"></a> Determine which document types can be indexed  
 Query the catalog view [sys.fulltext_document_types &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-document-types-transact-sql.md).  
  
 If the document type that you want to index is not in the list of supported types, then you may have to locate, download, and install additional filters. For more information, see [View or Change Registered Filters and Word Breakers](../../relational-databases/search/view-or-change-registered-filters-and-word-breakers.md).  
  
##  <a name="BestPracticeFilegroup"></a> Best practice: Consider creating a separate filegroup for the full-text and semantic indexes  
 Consider creating a separate filegroup for the full-text and semantic indexes if disk space allocation is a concern. The semantic indexes are created in the same filegroup as the full-text index. A fully populated semantic index may contain large amount of data.  
 
##  <a name="IssueNoResults"></a> Issue: Searching on specific column returns no results  
 **Was a non-Unicode LCID specified for a Unicode language?**  
 It is possible to enable semantic indexing on a non-Unicode column type with an LCID for a language that only has Unicode words, such as LCID 1049 for Russian. In this case, no results will ever be returned from the semantic indexes on this column.  
  
  
