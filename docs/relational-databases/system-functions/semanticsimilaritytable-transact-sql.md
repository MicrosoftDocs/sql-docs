---
title: "semanticsimilaritytable (Transact-SQL)"
description: Returns a table of zero, one, or more rows for documents whose content is semantically similar to a specified document.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
ms.date: 07/24/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "semanticsimilaritytable"
  - "semanticsimilaritytable_TSQL"
helpviewer_keywords:
  - "semanticsimilaritytable function"
dev_langs:
  - "TSQL"
---
# semanticsimilaritytable (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns a table of zero, one, or more rows for documents whose content in the specified columns is semantically similar to a specified document.

This rowset function can be referenced in the `FROM` clause of a `SELECT` statement like a regular table name.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
SEMANTICSIMILARITYTABLE
    (
      table
      , { column | (column_list) | * }
      , source_key
    )
```

## Arguments

#### table

The name of a table that has full-text and semantic indexing enabled.

This name can be a one to four part name, but a remote server name isn't allowed.

#### column

Name of the indexed column for which results should be returned. Column must have semantic indexing enabled.

#### column_list

Indicates several columns, separated by a comma and enclosed in parentheses. All columns must have semantic indexing enabled.

#### \*

Indicates that all columns that have semantic indexing enabled are included.

#### source_key

Unique key for the row, to request results for a specific row.

The key is implicitly converted to the type of the full-text unique key in the source table whenever possible. The key can be specified as a constant or a variable, but can't be an expression or the result of a scalar subquery.

## Table returned

The following table describes the information about similar or related documents that this rowset function returns.

Matched documents are returned on per-column basis if results are requested from more than one column.

| Column name | Type | Description |
| --- | --- | --- |
| `source_column_id` <sup>1</sup> | **int** | ID of the column from which a source document was used to find similar documents. |
| `matched_column_id` <sup>1</sup> | **int** | ID of the column from which a similar document was found. |
| `matched_document_key` | `*`<br /><br />This key matches the type of the unique key in the source table. | Full-text and semantic extraction unique key value of the document or row that was found to be similar to the specified document in the query. |
| `score` | **real** | A relative value for similarity for this document in its relationship to all the other similar documents. The value is a fractional decimal value in the range of [0.0, 1.0] where a higher score represents a closer match, and 1.0 is a perfect score. |

<sup>1</sup> See the [COL_NAME](../../t-sql/functions/col-name-transact-sql.md) and [COLUMNPROPERTY](../../t-sql/functions/columnproperty-transact-sql.md) functions for details on how to retrieve column name from `column_id` and vice versa.

## Remarks

For more information, see [Find Similar and Related Documents with Semantic Search](../search/find-similar-and-related-documents-with-semantic-search.md).

## Limitations

You can't query across columns for similar documents. The `SEMANTICSIMILARITYTABLE` function only retrieves similar documents from the same column as the source column, which is identified by the *source_key* argument.

## Metadata

For information and status about semantic similarity extraction and population, query the following dynamic management views:

- [sys.dm_db_fts_index_physical_stats](../system-dynamic-management-objects/sys-dm-db-fts-index-physical-stats-transact-sql.md)
- [sys.dm_fts_semantic_similarity_population](../system-dynamic-management-objects/sys-dm-fts-semantic-similarity-population-transact-sql.md)

## Permissions

Requires `SELECT` permissions on the base table on which the full-text and semantic indexes were created.

## Examples

The following example retrieves the top 10 candidates who are similar to a specified candidate from the `HumanResources.JobCandidate` table in the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] sample database.

```sql
SELECT TOP (10) KEY_TBL.matched_document_key AS Candidate_ID
FROM SEMANTICSIMILARITYTABLE (HumanResources.JobCandidate, Resume, @CandidateID) AS KEY_TBL
ORDER BY KEY_TBL.score DESC;
```
