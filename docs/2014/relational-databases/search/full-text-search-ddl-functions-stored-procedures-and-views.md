---
title: "Full-Text Search DDL, Functions, Stored Procedures, and Views | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: search
ms.topic: conceptual
ms.assetid: 98c36715-4195-482e-a4a3-d93ff65b75f1
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Full-Text Search DDL, Functions, Stored Procedures, and Views
  Lists the Transact-SQL statements and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database objects that support full-text search, including the property search feature.  
  
 This list does not include deprecated objects.  
  
 For the list of database objects that support semantic search, see [Semantic Search DDL, Functions, Stored Procedures, and Views](../views/views.md).  
  
##  <a name="ddl"></a> Transact-SQL Data Definition Language (DDL) Statements  
  
-   [CREATE FULLTEXT CATALOG &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-fulltext-catalog-transact-sql)  
  
-   [CREATE FULLTEXT INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-fulltext-index-transact-sql)  
  
-   [CREATE FULLTEXT STOPLIST &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-fulltext-stoplist-transact-sql)  
  
-   [CREATE SEARCH PROPERTY LIST &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-search-property-list-transact-sql)  
  
-   [ALTER FULLTEXT CATALOG &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-fulltext-catalog-transact-sql)  
  
-   [ALTER FULLTEXT INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-fulltext-index-transact-sql)  
  
-   [ALTER FULLTEXT STOPLIST &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-fulltext-stoplist-transact-sql)  
  
-   [ALTER SEARCH PROPERTY LIST &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-search-property-list-transact-sql)  
  
-   [DROP FULLTEXT CATALOG &#40;Transact-SQL&#41;](/sql/t-sql/statements/drop-fulltext-catalog-transact-sql)  
  
-   [DROP FULLTEXT INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/drop-fulltext-index-transact-sql)  
  
-   [DROP FULLTEXT STOPLIST &#40;Transact-SQL&#41;](/sql/t-sql/statements/drop-fulltext-stoplist-transact-sql)  
  
-   [DROP SEARCH PROPERTY LIST &#40;Transact-SQL&#41;](/sql/t-sql/statements/drop-search-property-list-transact-sql)  
  
##  <a name="func"></a> System Predicates and Functions  
  
-   [CONTAINS &#40;Transact-SQL&#41;](/sql/t-sql/queries/contains-transact-sql)  
  
-   [CONTAINSTABLE &#40;Transact-SQL&#41;](/sql/relational-databases/system-functions/containstable-transact-sql)  
  
-   [FREETEXT &#40;Transact-SQL&#41;](/sql/t-sql/queries/freetext-transact-sql)  
  
-   [FREETEXTTABLE &#40;Transact-SQL&#41;](/sql/relational-databases/system-functions/freetexttable-transact-sql)  
  
##  <a name="meta"></a> System Metadata Functions  
  
-   [COLUMNPROPERTY &#40;Transact-SQL&#41;](/sql/t-sql/functions/columnproperty-transact-sql)  
  
-   [FULLTEXTCATALOGPROPERTY &#40;Transact-SQL&#41;](/sql/t-sql/functions/fulltextcatalogproperty-transact-sql)  
  
-   [FULLTEXTSERVICEPROPERTY &#40;Transact-SQL&#41;](/sql/t-sql/functions/fulltextserviceproperty-transact-sql)  
  
-   [INDEXPROPERTY &#40;Transact-SQL&#41;](/sql/t-sql/functions/indexproperty-transact-sql)  
  
-   [OBJECTPROPERTY &#40;Transact-SQL&#41;](/sql/t-sql/functions/objectpropertyex-transact-sql)  
  
-   [OBJECTPROPERTYEX &#40;Transact-SQL&#41;](/sql/t-sql/functions/objectproperty-transact-sql)  
  
-   [SERVERPROPERTY &#40;Transact-SQL&#41;](/sql/t-sql/functions/serverproperty-transact-sql)  
  
##  <a name="proc"></a> System Stored Procedures  
  
-   [sp_fulltext_keymappings &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-fulltext-keymappings-transact-sql)  
  
-   [sp_fulltext_load_thesaurus_file &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-fulltext-load-thesaurus-file-transact-sql)  
  
-   [sp_fulltext_pendingchanges &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-fulltext-pendingchanges-transact-sql)  
  
-   [sp_fulltext_service &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-fulltext-service-transact-sql)  
  
-   [sp_help_fulltext_system_components &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-help-fulltext-system-components-transact-sql)  
  
##  <a name="cat"></a> System Views - Catalog Views  
  
-   [sys.fulltext_catalogs &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-fulltext-catalogs-transact-sql)  
  
-   [sys.fulltext_document_types &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-fulltext-document-types-transact-sql)  
  
-   [sys.fulltext_index_catalog_usages &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-fulltext-index-catalog-usages-transact-sql)  
  
-   [sys.fulltext_index_columns &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-fulltext-index-columns-transact-sql)  
  
-   [sys.fulltext_index_fragments &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-fulltext-index-fragments-transact-sql)  
  
-   [sys.fulltext_indexes &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-fulltext-indexes-transact-sql)  
  
-   [sys.fulltext_languages &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-fulltext-languages-transact-sql)  
  
-   [sys.fulltext_stoplists &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-fulltext-stoplists-transact-sql)  
  
-   [sys.fulltext_stopwords &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-fulltext-stopwords-transact-sql)  
  
-   [sys.fulltext_system_stopwords &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-fulltext-system-stopwords-transact-sql)  
  
-   [sys.registered_search_properties &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-registered-search-properties-transact-sql)  
  
-   [sys.registered_search_property_lists &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-registered-search-property-lists-transact-sql)  
  
##  <a name="dmv"></a> System Views - Dynamic Management Views  
  
-   [sys.dm_fts_active_catalogs &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-fts-active-catalogs-transact-sql)  
  
-   [sys.dm_fts_fdhosts &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-fts-fdhosts-transact-sql)  
  
-   [sys.dm_fts_index_keywords &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-fts-index-keywords-transact-sql)  
  
-   [sys.dm_fts_index_keywords_by_document &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-fts-index-keywords-by-document-transact-sql)  
  
-   [sys.dm_fts_index_keywords_by_property &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-fts-index-keywords-by-property-transact-sql)  
  
-   [sys.dm_fts_index_population &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-fts-index-population-transact-sql)  
  
-   [sys.dm_fts_memory_buffers &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-fts-memory-buffers-transact-sql)  
  
-   [sys.dm_fts_memory_pools &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-fts-memory-pools-transact-sql)  
  
-   [sys.dm_fts_outstanding_batches &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-fts-outstanding-batches-transact-sql)  
  
-   [sys.dm_fts_parser &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-fts-parser-transact-sql)  
  
-   [sys.dm_fts_population_ranges &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-fts-population-ranges-transact-sql)  
  
  
