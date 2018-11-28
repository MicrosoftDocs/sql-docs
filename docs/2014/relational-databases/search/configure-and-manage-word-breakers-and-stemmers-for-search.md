---
title: "Configure and Manage Word Breakers and Stemmers for Search | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: search
ms.topic: conceptual
helpviewer_keywords: 
  - "languages [full-text search]"
  - "full-text search [SQL Server], stemmers"
  - "linguistic analysis [full-text search]"
  - "full-text indexes [SQL Server], linguistic analysis"
  - "full-text search [SQL Server], word breakers"
  - "default full-text language option"
  - "stemmers [full-text search]"
  - "conjugating verbs [full-text search]"
  - "word breakers [full-text search]"
ms.assetid: d4bdd16b-a2db-4101-a946-583d1c674229
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Configure and Manage Word Breakers and Stemmers for Search
  Word breakers and stemmers perform linguistic analysis on all full-text indexed data. Linguistic analysis involves finding word boundaries (word-breaking) and conjugating verbs (stemming). Word breakers and stemmers are language specific, and the rules for linguistic analysis differ for different languages. For a given language, a *word breaker* identifies individual words by determining where word boundaries exist based on the lexical rules of the language. Each word (also known as a *token*) is inserted into the full-text index using a compressed representation to reduce its size. The *stemmer* generates inflectional forms of a particular word based on the rules of that language (for example, "running", "ran", and "runner" are various forms of the word "run").  
  
 Using language-specific word breakers enables the resulting terms to be more accurate for that language. Where there is a word breaker for the language family, but not for the specific sub-language, the major language is used. For example, the French word breaker is used to handle text that is French Canadian. If no word breaker is available for a particular language, the neutral word breaker is used. With the neutral word breaker, words are broken at neutral characters such as spaces and punctuation marks.  
  
##  <a name="register"></a> Registering Word Breakers  
 For the word breakers of a language to be used, they must be registered. For registered word breakers, associated linguistic resources-stemmers, noise words (stopwords), and thesaurus files-also become available to full-text indexing and querying operations. To view a list of the languages whose word breakers are currently registered with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], use the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statement:  
  
 SELECT * FROM sys.fulltext_languages  
  
 If you add, remove, or alter a word breaker, you need to refresh the list of Microsoft Windows locale identifiers (LCIDs) that are supported for full-text indexing and querying. For more information, see [View or Change Registered Filters and Word Breakers](view-or-change-registered-filters-and-word-breakers.md).  
  
##  <a name="default"></a> Setting the Default Full-Text Language Option  
 For a localized version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup sets the `default full-text language` option to the language of the server if an appropriate match exists. For a non-localized version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the `default full-text language` option is English.  
  
 When creating or altering a full-text index, you can specify a different language for each full-text indexed column. If no language is specified for a column, the default is the value of the configuration option `default full-text language`.  
  
> [!NOTE]  
>  All columns listed in a single full-text query function clause must use the same language, unless the LANGUAGE option is specified in the query. The language used for the full-text indexed column being queried determines the linguistic analysis performed on arguments of the full-text query predicates ([CONTAINS](/sql/t-sql/queries/contains-transact-sql) and [FREETEXT](/sql/t-sql/queries/freetext-transact-sql)) and functions ([CONTAINSTABLE](/sql/relational-databases/system-functions/containstable-transact-sql) and [FREETEXTTABLE](/sql/relational-databases/system-functions/freetexttable-transact-sql)).  
  
##  <a name="lang"></a> Choosing the Language for an Indexed Column  
 When creating a full-text index, we recommend that you specify a language for each indexed column. If a language is not specified for a column, the system default language is used. The language of a column determines which word breaker and stemmer are used for indexing that column. Also, the thesaurus file of that language will be used by full-text queries on the column.  
  
 There are a couple of things to consider when choosing the column language for creating a full-text index. These considerations relate to how your text is tokenized and then indexed by Full-Text Engine. For more information, see [Choose a Language When Creating a Full-Text Index](choose-a-language-when-creating-a-full-text-index.md).  
  
 **To view the word breaker language of a column**  
  
-   [Manage Full-Text Indexes](../indexes/indexes.md)  
  
-   [sys.fulltext_index_columns &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-fulltext-index-columns-transact-sql)  
  
    ```  
    SELECT 'language_id' AS "LCID" FROM sys.fulltext_index_columns;  
    ```  
  
##  <a name="info"></a> Obtaining Information about Word Breakers  
 **Viewing the tokenization result of a word breaker, thesaurus, and stoplist combination**  
  
-   [sys.dm_fts_parser &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-fts-parser-transact-sql).  
  
 **To return information about the registered word breakers**  
  
-   [sp_help_fulltext_system_components &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-help-fulltext-system-components-transact-sql)  
  
##  <a name="tshoot"></a> Troubleshooting Word-Breaking Time-out Errors  
 A word-breaking time-out error might occur in a variety of situations. For information about these situations and how to respond in each situation, see [MSSQLSERVER_30053](../errors-events/mssqlserver-30053-database-engine-error.md).  
  
##  <a name="impact"></a> Understanding the Impact of New Word Breakers  
 Each version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] typically includes new word breakers that have better linguistic rules and are more accurate than earlier word breakers. Potentially, the new word breakers might behave slightly differently from the word breakers in full-text indexes that were imported from previous versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This is significant if a full-text catalog was imported when a database was upgraded to the current version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. One or more languages used by the full-text indexes in the full-text catalog might now be associated with new word breakers. For more information, see [Upgrade Full-Text Search](upgrade-full-text-search.md).  
  
 For a complete list of all the word breakers, see [sys.fulltext_languages &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-fulltext-languages-transact-sql).  
  
## See Also  
 [ALTER FULLTEXT INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-fulltext-index-transact-sql)   
 [CREATE FULLTEXT INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-fulltext-index-transact-sql)   
 [sp_fulltext_service &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-fulltext-service-transact-sql)   
 [sys.fulltext_languages &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-fulltext-languages-transact-sql)   
 [Configure and Manage Stopwords and Stoplists for Full-Text Search](configure-and-manage-stopwords-and-stoplists-for-full-text-search.md)   
 [Upgrade Full-Text Search](upgrade-full-text-search.md)  
  
  
