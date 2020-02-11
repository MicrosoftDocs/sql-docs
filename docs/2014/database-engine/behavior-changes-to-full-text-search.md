---
title: "Behavior Changes to Full-Text Search | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: search
ms.topic: conceptual
helpviewer_keywords: 
  - "full-text search [SQL Server], breaking changes"
  - "behavior changes [full-text search]"
  - "full-text indexes [SQL Server], breaking changes"
ms.assetid: 573444e8-51bc-4f3d-9813-0037d2e13b8f
author: craigg-msft
ms.author: craigg
manager: craigg
---
# Behavior Changes to Full-Text Search
  This topic describes behavior changes in full-text search. Behavior changes affect how features work or interact in [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] as compared to earlier versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
## Behavior Changes in Full-Text Search in [!INCLUDE[ssSQL14](../includes/sssql14-md.md)]  
 Information to come later.  
  
## Behavior Changes in Full-Text Search in [!INCLUDE[ssSQL11](../includes/sssql11-md.md)]  
 [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] installs a new version of the word breakers and stemmers for US English (LCID 1033) and UK English (LCID 2057). However you can switch to the previous version of these components if you want to retain the previous behavior. For more information, see [Change the Word Breaker Used for US English and UK English](../relational-databases/search/change-the-word-breaker-used-for-us-english-and-uk-english.md).  
  
### New Word Breakers and Stemmers Installed  
 [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] updates all the word breakers and stemmers used by Full-Text Search and Semantic Search. For consistency between the contents of indexes and the results of queries, we recommend that you repopulate existing full-text indexes.  
  
1.  There are new word breakers for English. If you have to retain the previous behavior, see [Change the Word Breaker Used for US English and UK English](../relational-databases/search/change-the-word-breaker-used-for-us-english-and-uk-english.md).  
  
2.  The third-party word breakers for Danish, Polish, and Turkish that were included with previous releases of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] have been replaced with [!INCLUDE[msCoName](../includes/msconame-md.md)] components. The new components are enabled by default.  
  
3.  There are new word breakers for Czech and Greek. Previous releases of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Full-Text Search did not include support for these two languages.  
  
### Behavior Changes of New Word Breakers and Stemmers  
 The new components might return different results than the older components when you populate and query full-text indexes. The following tables demonstrate some of the differences that can be expected in English results.  
  
 If you have to retain the previous behavior of the word breakers and stemmers, see the following topics:  
  
-   [Change the Word Breaker Used for US English and UK English](../relational-databases/search/change-the-word-breaker-used-for-us-english-and-uk-english.md)  
  
-   [Revert the Word Breakers Used by Search to the Previous Version](../relational-databases/search/revert-the-word-breakers-used-by-search-to-the-previous-version.md)  
  
 In some cases, the new components return *more* results:  
  
|**Term**|**Results with previous word breaker and stemmer**|**Results with new word breaker and stemmer**|  
|--------------|--------------------------------------------------------|---------------------------------------------------|  
|cat-dog|cat<br /><br /> dog|cat<br /><br /> cat-dog<br /><br /> dog|  
|cat@dog.com|cat<br /><br /> com<br /><br /> dog|cat<br /><br /> cat@dog.com<br /><br /> com<br /><br /> dog|  
|12/11/2011<br /><br /> *(where the term is a date)*|12/11/2011<br /><br /> dd20111211|11<br /><br /> 12<br /><br /> 12/11/2011<br /><br /> 2011<br /><br /> dd20111211|  
  
 In some cases, the new components return *similar* results:  
  
|**Term**|**Results with previous word breaker and stemmer**|**Results with new word breaker and stemmer**|  
|--------------|--------------------------------------------------------|---------------------------------------------------|  
|100$|100$<br /><br /> nn100$|100$<br /><br /> nn100usd|  
|022|022<br /><br /> nn022|022<br /><br /> nn22|  
|10:49AM<br /><br /> *(where the term is a time)*|10:49am<br /><br /> tt1049|10:49am<br /><br /> tt24104900|  
  
 In some cases the new components return *fewer* results or results that may be unexpected by applications:  
  
|**Term**|**Results with previous word breaker and stemmer**|**Results with new word breaker and stemmer**|  
|--------------|--------------------------------------------------------|---------------------------------------------------|  
|jěˊÿｑℭžl<br /><br /> *(where the terms are not valid English characters)*|'jěˊÿｑℭžl'|je yq zl|  
|table's|table's<br /><br /> table|table's|  
|cat-|cat<br /><br /> cat-|cat|  
|v-z*(where v and z are noise words)*|*(no results)*|v-z|  
|$100 000 USD|$100<br /><br /> 000<br /><br /> nn000<br /><br /> nn100$<br /><br /> usd|$100 000 usd<br /><br /> nn100000usd|  
|beautiful U.S land|beautiful<br /><br /> land<br /><br /> u.s<br /><br /> us|beautiful<br /><br /> land|  
|Mt. Kent and Mt Challenger|challenger<br /><br /> kent<br /><br /> mt<br /><br /> mt.|mt<br /><br /> kent<br /><br /> challenger|  
  
## Behavior Changes in Full-Text Search in SQL Server 2008  
 In [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)] and later versions, the Full-Text Engine is integrated as a database service into the relational database as part of the server query and storage engine infrastructure. The new full-text search architecture achieves the following goals:  
  
-   Integrated storage and management-Full-text search is now integrated directly with the inherent storage and management features of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], and the MSFTESQL service no longer exists.  
  
    -   Full-text indexes are stored inside the database filegroups, rather than in the file system. Administrative operations on a database, such as creating a backup, automatically affect its full-text indexes.  
  
    -   A full-text catalog is now a virtual object that does not belong to any filegroup; it is a logical concept that refers to a group of full-text indexes. Therefore, many catalog-management features have been deprecated, and deprecation has created breaking changes for some features. For more information, see [Deprecated Database Engine Features in SQL Server 2014](deprecated-database-engine-features-in-sql-server-2016.md) and [Breaking Changes to Full-Text Search](breaking-changes-to-full-text-search.md).  
  
        > [!NOTE]  
        >  [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)] [!INCLUDE[tsql](../includes/tsql-md.md)] DDL statements that specify full-text catalogs work correctly.  
  
-   Integrated query processing-The new full-text search query processor is part of the Database Engine and is fully integrated with the SQL Server Query processor. This means that, the query optimizer recognizes full-text query predicates and automatically executes them as efficiently as possible.  
  
-   Enhanced administration and troubleshooting-Integrated full-text search provides tools to help you analyze search structures such as the full-text index, the output of a given word breaker, stopword configuration, and so forth.  
  
-   Stopwords and stoplists have replaced noise words and noise-word files. A stoplist is a database object that facilitates manageability tasks for stopwords and improves the integrity between different server instances and environments. For more information, see [Configure and Manage Stopwords and Stoplists for Full-Text Search](../relational-databases/search/configure-and-manage-stopwords-and-stoplists-for-full-text-search.md).  
  
-   [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)] and later versions include new word breakers for many of the languages that exist in [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)]. Only the word breakers for English, Korean, Thai, and Chinese (all forms) remain the same. For other languages, if a full-text catalog was imported when a [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)] database was upgraded to [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)] or a later version, one or more languages used by the full-text indexes in full-text catalog might now be associated with new word breakers that might behave slightly differently from the imported word breakers. For more information about how to ensure consistency between queries and the full-text index content, see [Upgrade Full-Text Search](../relational-databases/search/upgrade-full-text-search.md).  
  
-   A new FDHOST Launcher (MSSQLFDLauncher) service has been added. For more information, see [Get Started with Full-Text Search](../relational-databases/search/get-started-with-full-text-search.md).  
  
-   Full-text indexing works with a [FILESTREAM](../relational-databases/blob/filestream-sql-server.md) column in the same way that it does with a `varbinary(max)` column. The FILESTREAM table must have a column that contains the file name extension for each FILESTREAM BLOB. For more information, see [Query with Full-Text Search](../relational-databases/search/query-with-full-text-search.md),[Configure and Manage Filters for Search](../relational-databases/search/configure-and-manage-filters-for-search.md), and [sys.fulltext_document_types &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-fulltext-document-types-transact-sql).  
  
     The full-text engine indexes the contents of the FILESTREAM BLOBs. Indexing files such as images might not be useful. When a FILESTREAM BLOB is updated it is reindexed.  
  
## See Also  
 [Full-Text Search](../relational-databases/search/full-text-search.md)   
 [Full-Text Search Backward Compatibility](../../2014/database-engine/full-text-search-backward-compatibility.md)   
 [Upgrade Full-Text Search](../relational-databases/search/upgrade-full-text-search.md)   
 [Get Started with Full-Text Search](../relational-databases/search/get-started-with-full-text-search.md)  
  
  
