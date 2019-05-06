---
title: "semanticsimilaritytable (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "semanticsimilaritytable"
  - "semanticsimilaritytable_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "semanticsimilaritytable function"
ms.assetid: b49d40ab-7552-438b-ad67-6237dcccb75b
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# semanticsimilaritytable (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Returns a table of zero, one, or more rows for documents whose content in the specified columns is semantically similar to a specified document.  
  
 This rowset function can be referenced in the FROM clause of a SELECT statement like a regular table name.  

 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```sql  
SEMANTICSIMILARITYTABLE  
    (  
    table,  
    { column | (column_list) | * },  
    source_key  
    )  
```  
  
##  <a name="Arguments"></a> Arguments  
 **table**  
 Is the name of a table that has full-text and semantic indexing enabled.  
  
 This name can be a one to four part name, but a remote server name is not allowed.  
  
 **column**  
 Name of the indexed column for which results should be returned. Column must have semantic indexing enabled.  
  
 **column_list**  
 Indicates several columns, separated by a comma and enclosed in parentheses. All columns must have semantic indexing enabled.  
  
 **\***  
 Indicates that all columns that have semantic indexing enabled are included.  
  
 **source_key**  
 Unique key for the row, to request results for a specific row.  
  
 The key is implicitly converted to the type of the full-text unique key in the source table whenever possible. The key can be specified as a constant or a variable, but cannot be an expression or the result of a scalar sub-query.  
  
## Table Returned  
 The following table describes the information about similar or related documents that this rowset function returns.  
  
 Matched documents are returned on per-column basis if results are requested from more than one column.  
  
|Column_name|Type|Description|  
|------------------|----------|-----------------|  
|**source_column_id**|**int**|ID of the column from which a source document was used to find similar documents.<br /><br /> See the COL_NAME and COLUMNPROPERTY functions for details on how to retrieve column name from column_id and vice versa.|  
|**matched_column_id**|**int**|ID of the column from which a similar document was found.<br /><br /> See the COL_NAME and COLUMNPROPERTY functions for details on how to retrieve column name from column_id and vice versa.|  
|**matched_document_key**|**\***<br /><br /> This key matches the type of the unique key in the source table.|Full-text and semantic extraction unique key value of the document or row that was found to be similar to the specified document in the query.|  
|**score**|**REAL**|A relative value for similarity for this document in its relationship to all the other similar documents.<br /><br /> The value is a fractional decimal value in the range of [0.0, 1.0] where a higher score represents a closer match and 1.0 is a perfect score.|  
  
## General Remarks  
 For more information, see [Find Similar and Related Documents with Semantic Search](../../relational-databases/search/find-similar-and-related-documents-with-semantic-search.md).  
  
## Limitations and Restrictions  
 You cannot query across columns for similar documents. The **SEMANTICSIMILARITYTABLE** function only retrieves similar documents from the same column as the source column, which is identified by the **source_key** argument.  
  
## Metadata  
 For information and status about semantic similarity extraction and population, query the following dynamic management views:  
  
-   [sys.dm_db_fts_index_physical_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-fts-index-physical-stats-transact-sql.md)  
  
-   [sys.dm_fts_semantic_similarity_population &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-fts-semantic-similarity-population-transact-sql.md)  
  
## Security  
  
### Permissions  
 Requires SELECT permissions on the base table on which the full-text and semantic indexes were created.  
  
## Examples  
 The following example retrieves the top 10 candidates who are similar to a specified candidate from the HumanResources.JobCandidate table in the AdventureWorks2012 sample database.  
  
```scr  
SELECT TOP(10) KEY_TBL.matched_document_key AS Candidate_ID  
FROMSEMANTICSIMILARITYTABLE  
    (  
    HumanResources.JobCandidate,  
    Resume,  
    @CandidateID  
    ) AS KEY_TBL  
ORDER BY KEY_TBL.score DESC;  
  
```  
  
  
