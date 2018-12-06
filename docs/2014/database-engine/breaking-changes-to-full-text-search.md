---
title: "Breaking Changes to Full-Text Search | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: search
ms.topic: conceptual
helpviewer_keywords: 
  - "full-text search [SQL Server], breaking changes"
  - "full-text catalogs [SQL Server], breaking changes"
  - "breaking changes [full-text search]"
  - "full-text indexes [SQL Server], breaking changes"
ms.assetid: c55a6748-e5d9-4fdb-9a1f-714475a419c5
author: craigg-msft
ms.author: craigg
manager: craigg
---
# Breaking Changes to Full-Text Search
  This topic describes breaking changes in full-text search. These changes might break applications, scripts, or functionalities that are based on earlier versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. You might encounter these issues when you upgrade. For more information, see [Use Upgrade Advisor to Prepare for Upgrades](../../2014/sql-server/install/use-upgrade-advisor-to-prepare-for-upgrades.md).  
  
## Breaking Changes in Full-Text Search in [!INCLUDE[ssSQL14](../includes/sssql14-md.md)]  
 Information to come later.  
  
## Breaking Changes in Full-Text Search in [!INCLUDE[ssSQL11](../includes/sssql11-md.md)]  
  
### Collation Changed for name Column in sys.fulltext_languages  
 The collation of the language **name** column in the catalog view [sys.fulltext_languages &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-fulltext-languages-transact-sql) has been changed from the fixed collation of the Resource database to the default collation selected for the instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. This change makes it possible to compare the values in the **name** column when you join the [sys.syslanguages &#40;Transact-SQL&#41;](/sql/relational-databases/system-compatibility-views/sys-syslanguages-transact-sql) view with **sys.fulltext_languages**. For example, you can query for all the databases where the default full-text language is different from the default database language.  
  
## Breaking Changes in Full-Text Search in SQL Server 2008  
 The following breaking changes apply to Full-Text Search between [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)] and [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)] and later versions.  
  
|Feature|Scenario|SQL Server 2005|SQL Server 2008 and later versions|  
|-------------|--------------|---------------------|----------------------------------------|  
|[CONTAINSTABLE](/sql/relational-databases/system-functions/containstable-transact-sql) with user-defined types (UDTs)|The full-text key is a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] user-defined type, for example, `MyType = char(1)`.|The returned key is of the type assigned to the user-defined type.<br /><br /> In the example, this would be **char(1)**.|The returned key is of the user-defined type. In the example, this would be **MyType**.|  
|*top_n_by_rank* parameter (of the CONTAINSTABLE and [FREETEXTTABLE](/sql/relational-databases/system-functions/freetexttable-transact-sql)[!INCLUDE[tsql](../includes/tsql-md.md)] statements)|*top_n_by_rank* queries using 0 as the parameter.|Fails with an error message stating that you must use a value greater than zero.|Succeeds, returning zero rows.|  
|CONTAINSTABLE and **ItemCount**|Delete rows from base table before it pushes changes to MSSearch.|CONTAINSTABLE returns ghost record. **ItemCount** is not changed.|CONTAINSTABLE does not return any ghost records.|  
|**ItemCount**|Table contain null documents or type columns.|In addition to indexed documents, documents that are null or that have null types are counted in the **ItemCount** value.|Only indexed documents are counted in the **ItemCount** value.|  
|Catalog **ItemCount**|Blob column with a NULL extension.|It is counted in **ItemCount** of catalog|It is not counted in **ItemCount** of catalog.|  
|**UniqueKeyCount**|Querying a unique key count from a catalog, for example, two tables (table1 and table2) each with three words: word1, word2, and word3.|**UniqueKeyCount** = 9. The following table summarizes how this value is attained:<br /><br /> table1 = 3<br /><br /> EOF for full-text index of table1 = 1<br /><br /> table2 = 3<br /><br /> EOF for full-text index of table2 = 1<br /><br /> full-text catalog  = 1|For each table, **UniqueKeyCount** is the number of distinct keywords + 1 (0xFF).  This does NOT treat same words in > 1 doc as new unique key.<br /><br /> For a catalog, **UniqueKeyCount** is the sum of **UniqueKeyCount** of each of the tables under the catalog. Identical words from different tables are treated as unique keys. In this case the unique key count is 8.|  
|**precompute rank** server-level option|Performance optimization of FREETEXTTABLE queries.|When the option is set to 1, FREETEXTTABLE queries specified with *top_n_by_rank* use precomputed rank data stored in the full-text catalogs.|Is not supported.|  
|[sp_fulltext_pendingchanges](/sql/relational-databases/system-stored-procedures/sp-fulltext-pendingchanges-transact-sql) when updating key column|Update the full-text key column on one row of a 2-row table, and run sp_fulltext_pendingchanges.|Both rows appear.|Only one row appears.|  
|Inline functions|Inline functions with a full-text operator|Return an error message.|Return the relevant rows.|  
|[sp_fulltext_database](/sql/relational-databases/system-stored-procedures/sp-fulltext-database-transact-sql)|Enable or disable full-text search by using sp_fulltext_database.|No results are returned for full-text queries. If full-text is disabled for the database, full-text operations are not allowed.|Returns results to full-text queries, and full-text operations allowed, even if full-text is disabled for the database.|  
|Locale-specific stop words|Queries inlocale-specific variants of a parent language, such as Belgian French and Canadian French.|Queries inlocale-specific variants are processed by the components (word breakers, stemmers, and stop words) of their parent language. For example, the French (France) components are used to parse French (Belgium).|You must add stop words explicitly for each locale identifier (LCID). For example, you would need to specify an LCID for Belgium, Canada, and France.|  
|Thesaurus stemming process|Using thesaurus and Inflectional forms (stemming).|A thesaurus word is automatically stemmed after its expansion.|If you want the stemmed form in the expansion, you need to explicitly add the stemmed form.|  
|Full-text catalog path and filegroup|Working with full-text catalogs.|Each full-text catalog has a physical path and belongs to a filegroup. It is treated as a database file.|A full-text catalog is a virtual object and does not belong to any filegroup. A full-text catalog is a logical concept that refers to a group of full-text indexes.<br /><br /> Note: [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)][!INCLUDE[tsql](../includes/tsql-md.md)] DDL statements that specify full-text catalogs work correctly.|  
|[sys.fulltext_catalogs](/sql/relational-databases/system-catalog-views/sys-fulltext-catalogs-transact-sql)|Using the path, data_space_id, and file_id of this catalog view.|These columns return a specific value.|These columns return NULL because the full-text catalog is no longer located in the file system.|  
|[sys.sysfulltextcatalogs](/sql/relational-databases/system-compatibility-views/sys-sysfulltextcatalogs-transact-sql)|Using the path column of this deprecated system table.|Returns the file system path of the full-text catalog.|Returns NULL because the full-text catalog is no longer located in the file system.|  
|[sp_help_fulltext_catalogs](/sql/relational-databases/system-stored-procedures/sp-help-fulltext-catalogs-transact-sql)<br /><br /> [sp_help_fulltext_catalogs_cursor](/sql/relational-databases/system-stored-procedures/sp-help-fulltext-catalogs-cursor-transact-sql)|Using the PATH column of these deprecated stored procedures.|Returns the file system path of the full-text catalog.|Returns NULL because the full-text catalog is no longer located in the file system.|  
|[sp_help_fulltext_catalog_components](/sql/relational-databases/system-stored-procedures/sp-help-fulltext-catalog-components-transact-sql)|Using sp_help_fulltext_catalog_components of this stored procedure.|Returns a list of all components (filters, word-breakers, and protocol handlers), used for all full-text catalogs in the current database.|Returns empty rows.|  
|[DATABASEPROPERTYEX](/sql/t-sql/functions/databasepropertyex-transact-sql)|Using the **IsFullTextEnabled** property.|The **IsFullTextEnabled** setting indicates whether full-text search is enabled in a given database.|The value of this column has no effect. User databases are always enabled for full-text search.|  
  
## See Also  
 [Behavior Changes to Full-Text Search](../relational-databases/search/full-text-search.md)   
 [Full-Text Search](../relational-databases/search/full-text-search.md)  
  
  
