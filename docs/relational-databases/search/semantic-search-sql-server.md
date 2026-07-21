---
title: Semantic Search (SQL Server)
description: Semantic Search (SQL Server)
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/20/2026
ms.service: sql
ms.subservice: search
ms.topic: concept-article
helpviewer_keywords:
  - "semantic search [SQL Server]"
  - "semantic search [SQL Server], overview"
  - "statistical semantic search [SQL Server]"
  - "statistical semantic search [SQL Server], overview"
---
# Semantic search (SQL Server)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Statistical semantic search extracts and indexes statistically relevant *key phrases* from unstructured documents stored in [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] databases. It then uses these key phrases to identify documents that are similar or related.

<a id="whatcanido"></a>

## Overview

Semantic search extends full-text search by matching document meaning rather than exact words. Common use cases include automatic tag extraction, related content discovery, and hierarchical navigation across similar content. For example, you can query the key phrase index to build a taxonomy for a document corpus, or query the document similarity index to identify resumes that match a job description.

The following examples demonstrate the three Transact-SQL rowset functions that you use to query the semantic indexes and retrieve the results as structured data.

<a id="find1"></a>

### Find the key phrases in a document

The following query gets the key phrases that were identified in the sample document. It presents the results in descending order by the score that ranks the statistical significance of each key phrase.

This query calls the [semantickeyphrasetable](../system-functions/semantickeyphrasetable-transact-sql.md) function.

```sql
SET @Title = 'Sample Document.docx';

SELECT @DocID = DocumentID
FROM Documents
WHERE DocumentTitle = @Title;

SELECT @Title AS Title,
       keyphrase,
       score
FROM SEMANTICKEYPHRASETABLE (Documents, *, @DocID)
ORDER BY score DESC;
```

<a id="find2"></a>

### Find similar or related documents

The following query gets the documents that were identified as similar or related to the sample document. It presents the results in descending order by the score that ranks the similarity of the two documents.

This query calls the [semanticsimilaritytable](../system-functions/semanticsimilaritytable-transact-sql.md) function.

```sql
SET @Title = 'Sample Document.docx';

SELECT @DocID = DocumentID
FROM Documents
WHERE DocumentTitle = @Title;

SELECT @Title AS SourceTitle,
       DocumentTitle AS MatchedTitle,
       DocumentID,
       score
FROM SEMANTICSIMILARITYTABLE (Documents, *, @DocID)
     INNER JOIN Documents
         ON DocumentID = matched_document_key
ORDER BY score DESC;
```

<a id="find3"></a>

### Find the key phrases that make documents similar or related

The following query gets the key phrases that make the two sample documents similar or related to one another. It presents the results in descending order by the score that ranks the weight of each key phrase.

This query calls the [semanticsimilaritydetailstable](../system-functions/semanticsimilaritydetailstable-transact-sql.md) function.

```sql
SET @SourceTitle = 'first.docx';
SET @MatchedTitle = 'second.docx';

SELECT @SourceDocID = DocumentID
FROM Documents
WHERE DocumentTitle = @SourceTitle;

SELECT @MatchedDocID = DocumentID
FROM Documents
WHERE DocumentTitle = @MatchedTitle;

SELECT @SourceTitle AS SourceTitle,
       @MatchedTitle AS MatchedTitle,
       keyphrase,
       score
FROM SEMANTICSIMILARITYDETAILSTABLE (
    Documents, DocumentContent, @SourceDocID, DocumentContent, @MatchedDocID
)
ORDER BY score DESC;
```

<a id="store"></a>

## Store your documents in SQL Server

Before you can index documents with semantic search, store the documents in the [!INCLUDE [ssde-md](../../includes/ssde-md.md)].

The FileTable feature in SQL Server stores unstructured files and documents in the database, so you can manipulate them together with structured data in Transact-SQL set-based operations.

For more information about the FileTable feature, see [FileTables](../blob/filetables-sql-server.md). For information about the FILESTREAM feature, which is another option for storing documents in the database, see [FILESTREAM](../blob/filestream-sql-server.md).

<a id="reltasks"></a>

## Related tasks

| Article | Description |
| --- | --- |
| [Install and Configure Semantic Search](install-and-configure-semantic-search.md) | Describes the prerequisites for statistical semantic search and how to install or check them. |
| [Enable semantic search on tables and columns](enable-semantic-search-on-tables-and-columns.md) | Describes how to enable or disable statistical semantic indexing on selected columns that contain documents or text. |
| [Find Key Phrases in Documents with Semantic Search](find-key-phrases-in-documents-with-semantic-search.md) | Describes how to find the key phrases in documents or text columns that are configured for statistical semantic indexing. |
| [Find similar and related documents with semantic search](find-similar-and-related-documents-with-semantic-search.md) | Describes how to find similar or related documents or text values, and information about how they are similar or related, in columns that are configured for statistical semantic indexing. |
| [Manage and monitor Semantic Search](manage-and-monitor-semantic-search.md) | Describes the process of semantic indexing and the tasks related to monitoring and managing the indexes. |

## Related content

- [Semantic Search DDL, Functions, Stored Procedures, and Views](semantic-search-ddl-functions-stored-procedures-and-views.md)
