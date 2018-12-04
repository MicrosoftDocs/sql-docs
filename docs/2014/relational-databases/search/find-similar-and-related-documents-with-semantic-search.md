---
title: "Find Similar and Related Documents with Semantic Search | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: search
ms.topic: conceptual
helpviewer_keywords: 
  - "semantic search [SQL Server], document similarity queries"
ms.assetid: 9f527883-031b-442f-8e95-24bc0151ecbf
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Find Similar and Related Documents with Semantic Search
  Describes how to find similar or related documents or text values, and information about how they are similar or related, in columns that are configured for statistical semantic indexing.  
  
##  <a name="BasicsQuerySimilar"></a> Finding Similar or Related Documents  
  
###  <a name="HowToQuerySimilar"></a> How To: Find Similar or Related Documents with SEMANTICSIMILARITYTABLE  
 To identify similar or related documents in a specific column, query the function [semanticsimilaritytable &#40;Transact-SQL&#41;](/sql/relational-databases/system-functions/semanticsimilaritytable-transact-sql).  
  
 **SEMANTICSIMILARITYTABLE** returns a table of zero, one, or more rows whose content in the specified column is semantically similar to the specified document. This rowset function can be referenced in the FROM clause of a SELECT statement like a regular table name.  
  
 You cannot query across columns for similar documents. The **SEMANTICSIMILARITYTABLE** function only retrieves results from the same column as the source column, which is identified by the **source_key** argument.  
  
 For detailed information about the parameters required by the **SEMANTICSIMILARITYTABLE** function, and about the table of results that it returns, see [semanticsimilaritytable &#40;Transact-SQL&#41;](/sql/relational-databases/system-functions/semanticsimilaritytable-transact-sql).  
  
> [!IMPORTANT]  
>  The columns that you target must have full-text and semantic indexing enabled.  
  
###  <a name="HowToIdentifySimilar"></a> Example: Find the Top Documents That Are Similar to Another Document  
 The following example retrieves the top 10 candidates who are similar to the candidate specified by *@CandidateID* from the HumanResources.JobCandidate table in the AdventureWorks2012 sample database.  
  
```scr  
SELECT TOP(10) KEY_TBL.matched_document_key AS Candidate_ID  
FROM SEMANTICSIMILARITYTABLE  
    (  
    HumanResources.JobCandidate,  
    Resume,  
    @CandidateID  
    ) AS KEY_TBL  
ORDER BY KEY_TBL.score DESC;  
GO  
```  
  
##  <a name="BasicsQuerySimilarity"></a> Finding Information about How Documents Are Similar or Related  
  
###  <a name="HowToQuerySimilarity"></a> How To: Find Information about How Documents Are Similar or Related with SEMANTICSIMILARITYDETAILSTABLE  
 To get information about the key phrases that make documents similar or related, you can query the function [semanticsimilaritydetailstable &#40;Transact-SQL&#41;](/sql/relational-databases/system-functions/semanticsimilaritydetailstable-transact-sql).  
  
 **SEMANTICSIMILARITYDETAILSTABLE** returns a table of zero, one, or more rows of key phrases common across two documents (a source document and a matched document) whose content is semantically similar. This rowset function can be referenced in the FROM clause of a SELECT statement like a regular table name.  
  
 For detailed information about the parameters required by the **SEMANTICSIMILARITYDETAILSTABLE** function, and about the table of results that it returns, see [semanticsimilaritydetailstable &#40;Transact-SQL&#41;](/sql/relational-databases/system-functions/semanticsimilaritydetailstable-transact-sql).  
  
> [!IMPORTANT]  
>  The columns that you target must have full-text and semantic indexing enabled.  
  
###  <a name="HowToSimilarPhrases"></a> Example: Find the Top Key Phrases That Are Similar between Documents  
 The following example retrieves the 5 key phrases that have the highest similarity score between the specified candidates in **HumanResources.JobCandidate** table of the AdventureWorks2012 sample database.  
  
```tsql  
SELECT TOP(5) KEY_TBL.keyphrase, KEY_TBL.score  
FROM SEMANTICSIMILARITYDETAILSTABLE  
    (  
    HumanResources.JobCandidate,  
    Resume, @CandidateID,  
    Resume, @MatchedID  
    ) AS KEY_TBL  
ORDER BY KEY_TBL.score DESC;  
GO  
```  
  
  
