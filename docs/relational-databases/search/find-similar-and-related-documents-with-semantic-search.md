---
title: "Find Similar & Related Documents with Semantic Search"
description: Find similar or related documents or text values in columns that are configured for statistical semantic indexing.
author: thesqlsith
ms.author: derekw
ms.date: 07/24/2025
ms.service: sql
ms.subservice: search
ms.topic: how-to
helpviewer_keywords:
  - "semantic search [SQL Server], document similarity queries"
---
# Find similar and related documents with semantic search

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Describes how to find similar or related documents or text values, and information about how they're similar or related, in columns that are configured for statistical semantic indexing.

<a id="HowToQuerySimilar"></a>

## Find similar or related documents with SEMANTICSIMILARITYTABLE

To identify similar or related documents in a specific column, query the function [semanticsimilaritytable](../system-functions/semanticsimilaritytable-transact-sql.md).

`SEMANTICSIMILARITYTABLE` returns a table of zero, one, or more rows whose content in the specified column is semantically similar to the specified document. This rowset function can be referenced in the `FROM` clause of a `SELECT` statement like a regular table name.

You can't query across columns for similar documents. The `SEMANTICSIMILARITYTABLE` function only retrieves results from the same column as the source column, which is identified by the *source_key* argument.

For detailed information about the parameters required by the `SEMANTICSIMILARITYTABLE` function, and about the table of results that it returns, see [semanticsimilaritytable](../system-functions/semanticsimilaritytable-transact-sql.md).

> [!IMPORTANT]  
> The columns that you target must have full-text and semantic indexing enabled.

<a id="HowToQuerySimilarity"></a>

## Use SEMANTICSIMILARITYDETAILSTABLE to find out how documents are similar or related

To get information about the key phrases that make documents similar or related, you can query the function [semanticsimilaritydetailstable](../system-functions/semanticsimilaritydetailstable-transact-sql.md).

`SEMANTICSIMILARITYDETAILSTABLE` returns a table of zero, one, or more rows of key phrases common across two documents (a source document and a matched document) whose content is semantically similar. This rowset function can be referenced in the `FROM` clause of a `SELECT` statement like a regular table name.

For detailed information about the parameters required by the `SEMANTICSIMILARITYDETAILSTABLE` function, and about the table of results that it returns, see [semanticsimilaritydetailstable](../system-functions/semanticsimilaritydetailstable-transact-sql.md).

> [!IMPORTANT]  
> The columns that you target must have full-text and semantic indexing enabled.

## Examples

<a id="HowToIdentifySimilar"></a>

### A. Find the top documents that are similar to another document

The following example retrieves the top 10 candidates who are similar to the candidate specified by *\@CandidateID* from the HumanResources.JobCandidate table in the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] sample database.

```sql
SELECT TOP (10) KEY_TBL.matched_document_key AS Candidate_ID
FROM SEMANTICSIMILARITYTABLE (HumanResources.JobCandidate, Resume, @CandidateID) AS KEY_TBL
ORDER BY KEY_TBL.score DESC;
GO
```

<a id="HowToSimilarPhrases"></a>

### B. Find the top key phrases that are similar between documents

The following example retrieves the five key phrases that have the highest similarity score between the specified candidates in `HumanResources.JobCandidate` table of the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] sample database.

```sql
SELECT TOP (5) KEY_TBL.keyphrase,
               KEY_TBL.score
FROM SEMANTICSIMILARITYDETAILSTABLE (HumanResources.JobCandidate, Resume, @CandidateID, Resume, @MatchedID) AS KEY_TBL
ORDER BY KEY_TBL.score DESC;
GO
```
