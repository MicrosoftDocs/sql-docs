---
title: "Semantic Search DDL, Functions, Stored Procedures, and Views | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: search
ms.topic: conceptual
helpviewer_keywords: 
  - "semantic search [SQL Server], database objects"
ms.assetid: 182f395f-3168-48a4-b723-ef4403544f9f
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Semantic Search DDL, Functions, Stored Procedures, and Views
  Lists the Transact-SQL statements and the database objects that support statistical semantic search in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 For the list of statements and database objects that support full-text search, see [Full-Text Search DDL, Functions, Stored Procedures, and Views](../views/views.md).  
  
##  <a name="ddl"></a> Transact-SQL Data Definition Language (DDL) Statements  
  
|Object|More Information|  
|------------|----------------------|  
|[ALTER FULLTEXT INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-fulltext-index-transact-sql)|[Enable Semantic Search on Tables and Columns](enable-semantic-search-on-tables-and-columns.md)|  
|[CREATE FULLTEXT INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-fulltext-index-transact-sql)|[Enable Semantic Search on Tables and Columns](enable-semantic-search-on-tables-and-columns.md)|  
  
##  <a name="func"></a> System Functions  
  
|Object|More Information|  
|------------|----------------------|  
|[semantickeyphrasetable &#40;Transact-SQL&#41;](/sql/relational-databases/system-functions/semantickeyphrasetable-transact-sql)|[Find Key Phrases in Documents with Semantic Search](find-key-phrases-in-documents-with-semantic-search.md)|  
|[semanticsimilaritydetailstable &#40;Transact-SQL&#41;](/sql/relational-databases/system-functions/semanticsimilaritydetailstable-transact-sql)|[Find Similar and Related Documents with Semantic Search](find-similar-and-related-documents-with-semantic-search.md)|  
|[semanticsimilaritytable &#40;Transact-SQL&#41;](/sql/relational-databases/system-functions/semanticsimilaritytable-transact-sql)|[Find Similar and Related Documents with Semantic Search](find-similar-and-related-documents-with-semantic-search.md)|  
  
##  <a name="meta"></a> System Metadata Functions  
  
|Object|More Information|  
|------------|----------------------|  
|[COLUMNPROPERTY &#40;Transact-SQL&#41;](/sql/t-sql/functions/columnproperty-transact-sql)|[Enable Semantic Search on Tables and Columns](enable-semantic-search-on-tables-and-columns.md)|  
|[DATABASEPROPERTYEX &#40;Transact-SQL&#41;](/sql/t-sql/functions/databasepropertyex-transact-sql)|[Enable Semantic Search on Tables and Columns](enable-semantic-search-on-tables-and-columns.md)|  
|[FULLTEXTCATALOGPROPERTY &#40;Transact-SQL&#41;](/sql/t-sql/functions/fulltextcatalogproperty-transact-sql)|[Manage and Monitor Semantic Search](manage-and-monitor-semantic-search.md)|  
|[INDEXPROPERTY &#40;Transact-SQL&#41;](/sql/t-sql/functions/indexproperty-transact-sql)|[Manage and Monitor Semantic Search](manage-and-monitor-semantic-search.md)|  
|[OBJECTPROPERTYEX &#40;Transact-SQL&#41;](/sql/t-sql/functions/objectproperty-transact-sql)|[Enable Semantic Search on Tables and Columns](enable-semantic-search-on-tables-and-columns.md)|  
|[SERVERPROPERTY &#40;Transact-SQL&#41;](/sql/t-sql/functions/serverproperty-transact-sql)|[Install and Configure Semantic Search](install-and-configure-semantic-search.md)|  
  
##  <a name="sproc"></a> System Stored Procedures  
  
|Object|More Information|  
|------------|----------------------|  
|[sp_fulltext_semantic_register_language_statistics_db &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-fulltext-semantic-register-language-statistics-db-transact-sql)|[Install and Configure Semantic Search](install-and-configure-semantic-search.md)|  
|[sp_fulltext_semantic_unregister_language_statistics_db &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-fulltext-semantic-unregister-language-statistics-db-transact-sql)|[Install and Configure Semantic Search](install-and-configure-semantic-search.md)|  
  
##  <a name="cv"></a> System Views - Catalog Views  
  
|Object|More Information|  
|------------|----------------------|  
|[sys.fulltext_index_columns &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-fulltext-index-columns-transact-sql)|[Manage and Monitor Semantic Search](manage-and-monitor-semantic-search.md)|  
|[sys.fulltext_semantic_language_statistics_database &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-fulltext-semantic-language-statistics-database-transact-sql)|[Install and Configure Semantic Search](install-and-configure-semantic-search.md)|  
|[sys.fulltext_semantic_languages &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-fulltext-semantic-languages-transact-sql)|[Install and Configure Semantic Search](install-and-configure-semantic-search.md)|  
  
##  <a name="dmv"></a> System Views - Dynamic Management Views  
  
|Object|More Information|  
|------------|----------------------|  
|[sys.dm_db_fts_index_physical_stats &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-db-fts-index-physical-stats-transact-sql)|[Manage and Monitor Semantic Search](manage-and-monitor-semantic-search.md)|  
|[sys.dm_fts_index_population &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-fts-index-population-transact-sql)|[Manage and Monitor Semantic Search](manage-and-monitor-semantic-search.md)|  
|[sys.dm_fts_semantic_similarity_population &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-fts-semantic-similarity-population-transact-sql)|[Manage and Monitor Semantic Search](manage-and-monitor-semantic-search.md)|  
  
## See Also  
 [Manage and Monitor Semantic Search](manage-and-monitor-semantic-search.md)  
  
  
