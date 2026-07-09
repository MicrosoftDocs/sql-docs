---
title: Full-Text Search and Semantic Search Functions (Transact-SQL)
description: Full-text search and semantic search functions (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/09/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
helpviewer_keywords:
  - "semantic search [SQL Server], system functions"
dev_langs:
  - TSQL
---
# Full-Text Search and semantic search functions (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This section describes the system functions related to full-text search and semantic search.

## Full-Text Search functions

| Article | Description |
| --- | --- |
| [CONTAINSTABLE](containstable-transact-sql.md) | Returns a table of zero, one, or more rows for those columns containing precise or fuzzy (less precise) matches for single words and phrases, the proximity of words within a certain distance of one another, or weighted matches. |
| [FREETEXTTABLE](freetexttable-transact-sql.md) | Returns a table of zero, one, or more rows for those columns containing values that match the meaning, and not just the exact wording, of the text in the specified *freetext_string*. |

## Semantic search functions

| Article | Description |
| --- | --- |
| [semantickeyphrasetable](semantickeyphrasetable-transact-sql.md) | Returns a table with zero, one, or more rows for those key phrases associated with columns in the specified table. |
| [semanticsimilaritydetailstable](semanticsimilaritydetailstable-transact-sql.md) | Returns a table of zero, one, or more rows of key phrases common across two documents (a source document and a matched document) whose content is semantically similar. |
| [semanticsimilaritytable](semanticsimilaritytable-transact-sql.md) | Returns a table of zero, one, or more rows for those columns whose content is semantically similar to a specified document. |
