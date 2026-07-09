---
title: Full-Text Search and Semantic Search Catalog Views (Transact-SQL)
description: Learn about the catalog views that provide information about full-text indexes and semantic indexes.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/09/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
helpviewer_keywords:
  - "catalog views [SQL Server], full-text search"
  - "full-text search [SQL Server], catalog views"
  - "full-text indexes [SQL Server], catalog views"
dev_langs:
  - TSQL
---
# Full-Text Search and semantic search catalog views (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This section describes the catalog views that provide information about full-text indexes and semantic indexes.

## Full-Text Search catalog views

| Article | Description |
| --- | --- |
| [sys.fulltext_catalogs](sys-fulltext-catalogs-transact-sql.md) | Contains a row for each full-text catalog. |
| [sys.fulltext_document_types](sys-fulltext-document-types-transact-sql.md) | Returns a row for each document type that is available for full-text indexing operations. Each row represents the **IFilter** interface that is registered in the instance of SQL Server. |
| [sys.fulltext_index_catalog_usages](sys-fulltext-index-catalog-usages-transact-sql.md) | Returns a row for each full-text catalog to full-text index reference. |
| [sys.fulltext_index_columns](sys-fulltext-index-columns-transact-sql.md) | Contains a row for each column that is part of a full-text index. |
| [sys.fulltext_index_fragments](sys-fulltext-index-fragments-transact-sql.md) | Contains a row for each full-text index fragment in every table that contains a full-text index. |
| [sys.fulltext_indexes](sys-fulltext-indexes-transact-sql.md) | Contains a row per full-text index of a tabular object. |
| [sys.fulltext_languages](sys-fulltext-languages-transact-sql.md) | Contains one row per language whose word breakers are registered with SQL Server. Each row displays the LCID and name of the language. |
| [sys.fulltext_stoplists](sys-fulltext-stoplists-transact-sql.md) | Contains a row per full-text stoplist in the database. |
| [sys.fulltext_stopwords](sys-fulltext-stopwords-transact-sql.md) | Contains a row per stopword for all stoplists in the database. |
| [sys.fulltext_system_stopwords](sys-fulltext-system-stopwords-transact-sql.md) | Provides access to the system stoplist. |
| [sys.registered_search_properties](sys-registered-search-properties-transact-sql.md) | Contains a row for each search property contained by any search property list on the current database. |
| [sys.registered_search_property_lists](sys-registered-search-property-lists-transact-sql.md) | Contains a row for each search property list on the current database. |

## Semantic search catalog views

| Article | Description |
| --- | --- |
| [sys.fulltext_semantic_language_statistics_database](sys-fulltext-semantic-language-statistics-database-transact-sql.md) | Returns a row about the semantic language statistics database installed on the current instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |
| [sys.fulltext_semantic_languages](sys-fulltext-semantic-languages-transact-sql.md) | Returns a row for each language whose statistics model is registered with the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. When a language model is registered, that language is enabled for semantic indexing. |

## Related content

- [Transact-SQL reference (Database Engine)](../../t-sql/language-reference.md)
- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
- [Full-text and semantic search dynamic management views and functions](../system-dynamic-management-objects/full-text-and-semantic-search-dynamic-management-views-functions.md)
