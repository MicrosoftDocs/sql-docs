---
description: "semanticsimilaritydetailstable (Transact-SQL)"
title: "semanticsimilaritydetailstable (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "semanticsimilaritydetailstable"
  - "semanticsimilaritydetailstable_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "semanticsimilaritydetailstable function"
ms.assetid: 038d751a-fca5-4b4c-9129-cba741a4e173
author: MikeRayMSFT
ms.author: mikeray
---
# semanticsimilaritydetailstable (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns a table of zero, one, or more rows of key phrases that are common across two documents (a source document and a matched document) whose content is semantically similar.  
  
 This rowset function can be referenced in the FROM clause of a SELECT statement 
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```sql  
SEMANTICSIMILARITYDETAILSTABLE  
    (  
    table,  
    source_column,  
    source_key,  
    matched_column,  
    matched_key  
    )  
```  
  
##  <a name="Arguments"></a> Arguments  
 **table**  
 Is the name of a table that has full-text and semantic indexing enabled.  
  
 This name can be a one to four part name, but a remote server name is not allowed.  
  
 **source_column**  
 Name of the column in the source row that contains the content to be compared for similarity.  
  
 **source_key**  
 The unique key that represents the row of the source document.  
  
 This key is implicitly converted to the type of the full-text unique key in the source table whenever possible. The key can be specified as a constant or a variable, but cannot be an expression or the result of a scalar sub-query. If an invalid key is specified, no rows are returned.  
  
 **matched_column**  
 Name of the column in the matched row that contains the content to be compared for similarity.  
  
 **matched_key**  
 The unique key that represents the row of the matched document.  
  
 This key is implicitly converted to the type of the full-text unique key in the source table whenever possible. The key can be specified as a constant or a variable, but cannot be an expression or the result of a scalar sub-query.  
  
## Table Returned  
 The following table describes the information about key phrases that this rowset function returns.  
  
|Column_name|Type|Description|  
|------------------|----------|-----------------|  
|**keyphrase**|**NVARCHAR**|The key phrase that contributes to the similarity between source document and the matched document.|  
|**score**|**REAL**|A relative value for this key phrase in its relationship to all the other key phrases that are similar between the 2 documents.<br /><br /> The value is a fractional decimal value in the range of [0.0, 1.0] where a higher score represents a higher weighting and 1.0 is the perfect score.|  
  
## General Remarks  
 For more information, see [Find Similar and Related Documents with Semantic Search](../../relational-databases/search/find-similar-and-related-documents-with-semantic-search.md).  
  
## Metadata  
 For information and status about semantic similarity extraction and population, query the following dynamic management views:  
  
-   [sys.dm_db_fts_index_physical_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-fts-index-physical-stats-transact-sql.md)  
  
-   [sys.dm_fts_semantic_similarity_population &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-fts-semantic-similarity-population-transact-sql.md)  
  
## Security  
  
### Permissions  
 Requires SELECT permissions on the base table on which the full-text and semantic indexes were created.  
  
## Examples  
 The following example retrieves the 5 key phrases that had the highest similarity score between the specified candidates in **HumanResources.JobCandidate** table of the AdventureWorks2012 sample database. The @CandidateId and @MatchedID variables represent values from the key column of the full-text index.  
  
```sql  
SELECT TOP(5) KEY_TBL.keyphrase, KEY_TBL.score  
FROMSEMANTICSIMILARITYDETAILSTABLE  
    (  
    HumanResources.JobCandidate,  
    Resume, @CandidateID,  
    Resume, @MatchedID  
    ) AS KEY_TBL  
ORDER BY KEY_TBL.score DESC;  
  
```  
  
  
