---
title: "Full-Text Search and Semantic Search Catalog Views (Transact-SQL)"
description: Full-Text Search and Semantic Search Catalog Views (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray
ms.date: "03/17/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
helpviewer_keywords:
  - "catalog views [SQL Server], full-text search"
  - "full-text search [SQL Server], catalog views"
  - "full-text indexes [SQL Server], catalog views"
dev_langs:
  - "TSQL"
ms.assetid: b08ad2fd-e3d8-458f-96f1-678217e0f419
---
# Full-Text Search and Semantic Search Catalog Views (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  This section describes the catalog views that provide information about full-text indexes and semantic indexes.  
  
## Full-Text Search Catalog Views  
 [sys.fulltext_catalogs](../../relational-databases/system-catalog-views/sys-fulltext-catalogs-transact-sql.md)  
 Contains a row for each full-text catalog.  
  
 [sys.fulltext_document_types](../../relational-databases/system-catalog-views/sys-fulltext-document-types-transact-sql.md)  
 Returns a row for each document type that is available for full-text indexing operations. Each row represents the **IFilter** interface that is registered in the instance of SQL Server.  
  
 [sys.fulltext_index_catalog_usages](../../relational-databases/system-catalog-views/sys-fulltext-index-catalog-usages-transact-sql.md)  
 Returns a row for each full-text catalog to full-text index reference.  
  
 [sys.fulltext_index_columns](../../relational-databases/system-catalog-views/sys-fulltext-index-columns-transact-sql.md)  
 Contains a row for each column that is part of a full-text index.  
  
 [sys.fulltext_index_fragments](../../relational-databases/system-catalog-views/sys-fulltext-index-fragments-transact-sql.md)  
 Contains a row for each full-text index fragment in every table that contains a full-text index.  
  
 [sys.fulltext_indexes](../../relational-databases/system-catalog-views/sys-fulltext-indexes-transact-sql.md)  
 Contains a row per full-text index of a tabular object.  
  
 [sys.fulltext_languages](../../relational-databases/system-catalog-views/sys-fulltext-languages-transact-sql.md)  
 Contains one row per language whose word breakers are registered with SQL Server. Each row displays the LCID and name of the language.  
  
 [sys.fulltext_stoplists](../../relational-databases/system-catalog-views/sys-fulltext-stoplists-transact-sql.md)  
 Contains a row per full-text stoplist in the database.  
  
 [sys.fulltext_stopwords](../../relational-databases/system-catalog-views/sys-fulltext-stopwords-transact-sql.md)  
 Contains a row per stopword for all stoplists in the database.  
  
 [sys.fulltext_system_stopwords](../../relational-databases/system-catalog-views/sys-fulltext-system-stopwords-transact-sql.md)  
 Provides access to the system stoplist.  
  
 [sys.registered_search_properties &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-registered-search-properties-transact-sql.md)  
 Contains a row for each search property contained by any search property list on the current database.  
  
 [sys.registered_search_property_lists &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-registered-search-property-lists-transact-sql.md)  
 Contains a row for each search property list on the current database.  
  
## Semantic Search Catalog Views  
 [sys.fulltext_semantic_language_statistics_database &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-semantic-language-statistics-database-transact-sql.md)  
 Returns a row about the semantic language statistics database installed on the current instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 [sys.fulltext_semantic_languages &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-semantic-languages-transact-sql.md)  
 Returns a row for each language whose statistics model is registered with the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. When a language model is registered, that language is enabled for semantic indexing.  
  
## See Also  
 [System Views &#40;Transact-SQL&#41;](../../t-sql/language-reference.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Full-Text Search and Semantic Search Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/full-text-and-semantic-search-dynamic-management-views-functions.md)  
  
