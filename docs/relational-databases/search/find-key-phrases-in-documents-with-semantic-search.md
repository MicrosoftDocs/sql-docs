---
title: "Find Key Phrases in Documents with Semantic Search | Microsoft Docs"
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "search, sql-database"
ms.technology: search
ms.topic: conceptual
helpviewer_keywords: 
  - "semantic search [SQL Server], key phrase queries"
ms.assetid: 6ee3676e-ed5d-43ec-aeca-1eed78967111
author: pmasl
ms.author: pelopes
ms.reviewer: mikeray
manager: craigg
---
# Find Key Phrases in Documents with Semantic Search
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Describes how to find the key phrases in documents or text columns that are configured for statistical semantic indexing.  

##  <a name="howtofind"></a> Find the key phrases in documents with SEMANTICKEYPHRASETABLE  
 To identify the key phrases in specific documents, or to identify documents that contain specific key phrases, query the function [semantickeyphrasetable &#40;Transact-SQL&#41;](../../relational-databases/system-functions/semantickeyphrasetable-transact-sql.md).  
  
 SEMANTICKEYPHRASETABLE returns a table with zero, one, or more rows for those key phrases associated with columns in the specified table. This rowset function can be referenced in the FROM clause of a SELECT statement as if it were a regular table name.  
  
> [!NOTE]  
>  In this release, only single words are indexed for semantic search; multi-word phrases (ngrams) are not indexed. Also, various forms of the same word are indexed separately; for example, "computer" and "computers" are indexed separately.  
  
 For detailed information about the parameters required by the SEMANTICKEYPHRASETABLE function, and about the table of results that it returns, see [semantickeyphrasetable &#40;Transact-SQL&#41;](../../relational-databases/system-functions/semantickeyphrasetable-transact-sql.md).  
  
> [!IMPORTANT]  
>  The columns that you target must have full-text and semantic indexing enabled.  
  
###  <a name="HowToTopPhrases"></a> Example 1: Find the top key phrases in a specific document  
 The following example retrieves the top 10 key phrases from the document specified by the @DocumentId variable in the Document column of the Production.Document table of the AdventureWorks sample database. The @DocumentId variable represents a value from the key column of the full-text index.  
  
```sql  
SELECT TOP(10) KEYP_TBL.keyphrase  
FROM SEMANTICKEYPHRASETABLE  
    (  
    Production.Document,  
    Document,  
    @DocumentId  
    ) AS KEYP_TBL  
ORDER BY KEYP_TBL.score DESC;  
GO  
```  
  
 The **SEMANTICKEYPHRASETABLE** function retrieves these results efficiently by using an index seek instead of a table scan.  
  
###  <a name="HowToTopDocuments"></a> Example 2: Find the top documents that contain a specific key phrase  
 The following example retrieves the top 25 documents that contain the key phrase "Bracket" from the Document column of the Production.Document table of the AdventureWorks sample database.  
  
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
GO  
```  
  
  
