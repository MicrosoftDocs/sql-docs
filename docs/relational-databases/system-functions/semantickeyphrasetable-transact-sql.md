---
description: "semantickeyphrasetable (Transact-SQL)"
title: "semantickeyphrasetable (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "semantickeyphrasetable"
  - "semantickeyphrasetable_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "semantickeyphrasetable function"
ms.assetid: d33b973a-2724-4d4b-aaf7-67675929c392
author: MikeRayMSFT
ms.author: mikeray
---
# semantickeyphrasetable (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns a table with zero, one, or more rows for key phrases associated with the specified columns in the specified table.  
  
 This rowset function can be referenced in the FROM clause of a SELECT statement as if it were a regular table name.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```sql  
SEMANTICKEYPHRASETABLE  
    (  
    table,  
    { column | (column_list) | * }  
     [ , source_key ]  
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
  
 The key is implicitly converted to the type of the full-text unique key in the source table whenever possible. The key can be specified as a constant or a variable, but cannot be an expression or the result of a scalar sub-query. If source_key is omitted, then results are returned for all rows.  
  
## Table Returned  
 The following table describes the information about key phrases that this rowset function returns.  
  
|Column_name|Type|Description|  
|------------------|----------|-----------------|  
|**column_id**|**int**|ID of the column from which the current key phrase was extracted and indexed.<br /><br /> See the COL_NAME and COLUMNPROPERTY functions for details on how to retrieve column name from column_id and vice versa.|  
|**document_key**|**\***<br /><br /> This key matches the type of the unique key in the source table.|Unique key value of the document or row from which the current key phrase was indexed.|  
|**keyphrase**|**NVARCHAR**|The key phrase found in the column identified by column_id, and associated with the document specified by document_key.|  
|**score**|**REAL**|A relative value for this key phrase in its relationship to all the other key phrases in the same document in the indexed column.<br /><br /> The value is a fractional decimal value in the range of [0.0, 1.0] where a higher score represents a higher weighting and 1.0 is the perfect score.|  
  
## General Remarks  
 For more information, see [Find Key Phrases in Documents with Semantic Search](../../relational-databases/search/find-key-phrases-in-documents-with-semantic-search.md).  
  
## Metadata  
 For information and status about semantic key phrase extraction and population, query the following dynamic management views:  
  
-   [sys.dm_db_fts_index_physical_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-fts-index-physical-stats-transact-sql.md)  
  
-   [sys.dm_fts_index_population &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-fts-index-population-transact-sql.md)  
  
## Security  
  
### Permissions  
 Requires SELECT permissions on the base table on which the full-text and semantic indexes were created.  
  
## Examples  
  
###  <a name="HowToTopPhrases"></a> Example 1: Find the Top Key Phrases in a Specific Document  
 The following example retrieves the top 10 key phrases from the document specified by the @DocumentId variable in the Document column of the Production.Document table of the AdventureWorks sample database. The @DocumentId variable represents a value from the key column of the full-text index. The **SEMANTICKEYPHRASETABLE** function retrieves these results efficiently by using an index seek instead of a table scan. This example assumes that the column is configured for full-text and semantic indexing.  
  
```sql  
SELECT TOP(10) KEYP_TBL.keyphrase  
FROM SEMANTICKEYPHRASETABLE  
    (  
    Production.Document,  
    Document,  
    @DocumentId  
    ) AS KEYP_TBL  
ORDER BY KEYP_TBL.score DESC;  
  
```  
  
###  <a name="HowToTopDocuments"></a> Example 2: Find the Top Documents that Contain a Specific Key Phrase  
 The following example retrieves the top 25 documents that contain the key phrase "Bracket" from the Document column of the Production.Document table of the AdventureWorks sample database. This example assumes that the column is configured for full-text and semantic indexing.  
  
```sql  
SELECT TOP (25) DOC_TBL.DocumentID, DOC_TBL.DocumentSummary  
FROM Production.Document AS DOC_TBL  
    INNER JOIN SEMANTICKEYPHRASETABLE  
    (  
    Production.Document,  
    Document  
    ) AS KEYP_TBL  
ON DOC_TBL.DocumentID = KEYP_TBL.document_key  
WHERE KEYP_TBL.keyphrase = 'Bracket'  
ORDER BY KEYP_TBL.Score DESC;  
  
```  
  
  
