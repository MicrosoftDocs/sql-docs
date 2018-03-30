---
title: "Full-Text Search DDL, Functions, Stored Procedures, and Views | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "dbe-search"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 98c36715-4195-482e-a4a3-d93ff65b75f1
caps.latest.revision: 6
author: "craigg-msft"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Full-Text Search DDL, Functions, Stored Procedures, and Views
  Lists the Transact-SQL statements and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database objects that support full-text search, including the property search feature.  
  
 This list does not include deprecated objects.  
  
 For the list of database objects that support semantic search, see [Semantic Search DDL, Functions, Stored Procedures, and Views](../../2014/database-engine/semantic-search-ddl-functions-stored-procedures-and-views.md).  
  
##  <a name="ddl"></a> Transact-SQL Data Definition Language (DDL) Statements  
  
-   [CREATE FULLTEXT CATALOG &#40;Transact-SQL&#41;](../Topic/CREATE%20FULLTEXT%20CATALOG%20\(Transact-SQL\).md)  
  
-   [CREATE FULLTEXT INDEX &#40;Transact-SQL&#41;](../Topic/CREATE%20FULLTEXT%20INDEX%20\(Transact-SQL\).md)  
  
-   [CREATE FULLTEXT STOPLIST &#40;Transact-SQL&#41;](../Topic/CREATE%20FULLTEXT%20STOPLIST%20\(Transact-SQL\).md)  
  
-   [CREATE SEARCH PROPERTY LIST &#40;Transact-SQL&#41;](../Topic/CREATE%20SEARCH%20PROPERTY%20LIST%20\(Transact-SQL\).md)  
  
-   [ALTER FULLTEXT CATALOG &#40;Transact-SQL&#41;](../Topic/ALTER%20FULLTEXT%20CATALOG%20\(Transact-SQL\).md)  
  
-   [ALTER FULLTEXT INDEX &#40;Transact-SQL&#41;](../Topic/ALTER%20FULLTEXT%20INDEX%20\(Transact-SQL\).md)  
  
-   [ALTER FULLTEXT STOPLIST &#40;Transact-SQL&#41;](../Topic/ALTER%20FULLTEXT%20STOPLIST%20\(Transact-SQL\).md)  
  
-   [ALTER SEARCH PROPERTY LIST &#40;Transact-SQL&#41;](../Topic/ALTER%20SEARCH%20PROPERTY%20LIST%20\(Transact-SQL\).md)  
  
-   [DROP FULLTEXT CATALOG &#40;Transact-SQL&#41;](../Topic/DROP%20FULLTEXT%20CATALOG%20\(Transact-SQL\).md)  
  
-   [DROP FULLTEXT INDEX &#40;Transact-SQL&#41;](../Topic/DROP%20FULLTEXT%20INDEX%20\(Transact-SQL\).md)  
  
-   [DROP FULLTEXT STOPLIST &#40;Transact-SQL&#41;](../Topic/DROP%20FULLTEXT%20STOPLIST%20\(Transact-SQL\).md)  
  
-   [DROP SEARCH PROPERTY LIST &#40;Transact-SQL&#41;](../Topic/DROP%20SEARCH%20PROPERTY%20LIST%20\(Transact-SQL\).md)  
  
##  <a name="func"></a> System Predicates and Functions  
  
-   [CONTAINS &#40;Transact-SQL&#41;](../Topic/CONTAINS%20\(Transact-SQL\).md)  
  
-   [CONTAINSTABLE &#40;Transact-SQL&#41;](../Topic/CONTAINSTABLE%20\(Transact-SQL\).md)  
  
-   [FREETEXT &#40;Transact-SQL&#41;](../Topic/FREETEXT%20\(Transact-SQL\).md)  
  
-   [FREETEXTTABLE &#40;Transact-SQL&#41;](../Topic/FREETEXTTABLE%20\(Transact-SQL\).md)  
  
##  <a name="meta"></a> System Metadata Functions  
  
-   [COLUMNPROPERTY &#40;Transact-SQL&#41;](../Topic/COLUMNPROPERTY%20\(Transact-SQL\).md)  
  
-   [FULLTEXTCATALOGPROPERTY &#40;Transact-SQL&#41;](../Topic/FULLTEXTCATALOGPROPERTY%20\(Transact-SQL\).md)  
  
-   [FULLTEXTSERVICEPROPERTY &#40;Transact-SQL&#41;](../Topic/FULLTEXTSERVICEPROPERTY%20\(Transact-SQL\).md)  
  
-   [INDEXPROPERTY &#40;Transact-SQL&#41;](../Topic/INDEXPROPERTY%20\(Transact-SQL\).md)  
  
-   [OBJECTPROPERTY &#40;Transact-SQL&#41;](../Topic/OBJECTPROPERTY%20\(Transact-SQL\).md)  
  
-   [OBJECTPROPERTYEX &#40;Transact-SQL&#41;](../Topic/OBJECTPROPERTYEX%20\(Transact-SQL\).md)  
  
-   [SERVERPROPERTY &#40;Transact-SQL&#41;](../Topic/SERVERPROPERTY%20\(Transact-SQL\).md)  
  
##  <a name="proc"></a> System Stored Procedures  
  
-   [sp_fulltext_keymappings &#40;Transact-SQL&#41;](../Topic/sp_fulltext_keymappings%20\(Transact-SQL\).md)  
  
-   [sp_fulltext_load_thesaurus_file &#40;Transact-SQL&#41;](../Topic/sp_fulltext_load_thesaurus_file%20\(Transact-SQL\).md)  
  
-   [sp_fulltext_pendingchanges &#40;Transact-SQL&#41;](../Topic/sp_fulltext_pendingchanges%20\(Transact-SQL\).md)  
  
-   [sp_fulltext_service &#40;Transact-SQL&#41;](../Topic/sp_fulltext_service%20\(Transact-SQL\).md)  
  
-   [sp_help_fulltext_system_components &#40;Transact-SQL&#41;](../Topic/sp_help_fulltext_system_components%20\(Transact-SQL\).md)  
  
##  <a name="cat"></a> System Views – Catalog Views  
  
-   [sys.fulltext_catalogs &#40;Transact-SQL&#41;](../Topic/sys.fulltext_catalogs%20\(Transact-SQL\).md)  
  
-   [sys.fulltext_document_types &#40;Transact-SQL&#41;](../Topic/sys.fulltext_document_types%20\(Transact-SQL\).md)  
  
-   [sys.fulltext_index_catalog_usages &#40;Transact-SQL&#41;](../Topic/sys.fulltext_index_catalog_usages%20\(Transact-SQL\).md)  
  
-   [sys.fulltext_index_columns &#40;Transact-SQL&#41;](../Topic/sys.fulltext_index_columns%20\(Transact-SQL\).md)  
  
-   [sys.fulltext_index_fragments &#40;Transact-SQL&#41;](../Topic/sys.fulltext_index_fragments%20\(Transact-SQL\).md)  
  
-   [sys.fulltext_indexes &#40;Transact-SQL&#41;](../Topic/sys.fulltext_indexes%20\(Transact-SQL\).md)  
  
-   [sys.fulltext_languages &#40;Transact-SQL&#41;](../Topic/sys.fulltext_languages%20\(Transact-SQL\).md)  
  
-   [sys.fulltext_stoplists &#40;Transact-SQL&#41;](../Topic/sys.fulltext_stoplists%20\(Transact-SQL\).md)  
  
-   [sys.fulltext_stopwords &#40;Transact-SQL&#41;](../Topic/sys.fulltext_stopwords%20\(Transact-SQL\).md)  
  
-   [sys.fulltext_system_stopwords &#40;Transact-SQL&#41;](../Topic/sys.fulltext_system_stopwords%20\(Transact-SQL\).md)  
  
-   [sys.registered_search_properties &#40;Transact-SQL&#41;](../Topic/sys.registered_search_properties%20\(Transact-SQL\).md)  
  
-   [sys.registered_search_property_lists &#40;Transact-SQL&#41;](../Topic/sys.registered_search_property_lists%20\(Transact-SQL\).md)  
  
##  <a name="dmv"></a> System Views – Dynamic Management Views  
  
-   [sys.dm_fts_active_catalogs &#40;Transact-SQL&#41;](../Topic/sys.dm_fts_active_catalogs%20\(Transact-SQL\).md)  
  
-   [sys.dm_fts_fdhosts &#40;Transact-SQL&#41;](../Topic/sys.dm_fts_fdhosts%20\(Transact-SQL\).md)  
  
-   [sys.dm_fts_index_keywords &#40;Transact-SQL&#41;](../Topic/sys.dm_fts_index_keywords%20\(Transact-SQL\).md)  
  
-   [sys.dm_fts_index_keywords_by_document &#40;Transact-SQL&#41;](../Topic/sys.dm_fts_index_keywords_by_document%20\(Transact-SQL\).md)  
  
-   [sys.dm_fts_index_keywords_by_property &#40;Transact-SQL&#41;](../Topic/sys.dm_fts_index_keywords_by_property%20\(Transact-SQL\).md)  
  
-   [sys.dm_fts_index_population &#40;Transact-SQL&#41;](../Topic/sys.dm_fts_index_population%20\(Transact-SQL\).md)  
  
-   [sys.dm_fts_memory_buffers &#40;Transact-SQL&#41;](../Topic/sys.dm_fts_memory_buffers%20\(Transact-SQL\).md)  
  
-   [sys.dm_fts_memory_pools &#40;Transact-SQL&#41;](../Topic/sys.dm_fts_memory_pools%20\(Transact-SQL\).md)  
  
-   [sys.dm_fts_outstanding_batches &#40;Transact-SQL&#41;](../Topic/sys.dm_fts_outstanding_batches%20\(Transact-SQL\).md)  
  
-   [sys.dm_fts_parser &#40;Transact-SQL&#41;](../Topic/sys.dm_fts_parser%20\(Transact-SQL\).md)  
  
-   [sys.dm_fts_population_ranges &#40;Transact-SQL&#41;](../Topic/sys.dm_fts_population_ranges%20\(Transact-SQL\).md)  
  
  