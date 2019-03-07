---
title: "Configure and Manage Word Breakers and Stemmers for Search | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "search, sql-database"
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
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Configure and Manage Word Breakers and Stemmers for Search
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
Word breakers and stemmers perform linguistic analysis on all full-text indexed data. Linguistic analysis does the following two things:

-   **Find word boundaries (word-breaking)**. The *word breaker* identifies individual words by determining where word boundaries exist based on the lexical rules of the language. Each word (also known as a *token*) is inserted into the full-text index using a compressed representation to reduce its size.

-   **Conjugate verbs (stemming)**. The *stemmer* generates inflectional forms of a particular word based on the rules of that language (for example, "running", "ran", and "runner" are various forms of the word "run").

## Word breakers and stemmers are language specific

Word breakers and stemmers are language specific, and the rules for linguistic analysis differ for different languages. Language-specific word breakers make the resulting terms more accurate for that language.

To use the word breakers and stemmers provided for all the languages supported by SQL Server, you typically don't have to take any action.

-   Where there is a word breaker for the language family, but not for the specific sub-language, the major language is used. For example, the French word breaker is used to handle text that is French Canadian.
-   If no word breaker is available for a particular language, the neutral word breaker is used. With the neutral word breaker, words are broken at neutral characters such as spaces and punctuation marks.

## Get the list of supported languages

To see the list of languages supported by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Full-Text Search, use the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statement. The presence of a language in this list indicates that word breakers are registered for the language. 
  
```sql
SELECT * FROM sys.fulltext_languages
```

##  <a name="register"></a> Get the list of registered word breakers

For Full-Text Search to use the word breakers for a language, they must be registered. For registered word breakers, associated linguistic resources - stemmers, noise words (stopwords), and thesaurus files - also become available to full-text indexing and querying operations.

To see the list of registered word breaker components, use the following statement.

```sql
EXEC sp_help_fulltext_system_components 'wordbreaker';  
GO  
```

For additional options and more info, see [sp_help_fulltext_system_components &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-fulltext-system-components-transact-sql.md).
 
## If you add or remove a word breaker  
If you add, remove, or alter a word breaker, you need to refresh the list of Microsoft Windows locale identifiers (LCIDs) that are supported for full-text indexing and querying. For more information, see [View or Change Registered Filters and Word Breakers](../../relational-databases/search/view-or-change-registered-filters-and-word-breakers.md).  
  
##  <a name="default"></a> Set the default full-text language option  
 For a localized version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup sets the **default full-text language** option to the language of the server if an appropriate match exists. For a non-localized version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the **default full-text language** option is English.  
  
 When you create or alter a full-text index, you can specify a different language for each full-text indexed column. If no language is specified for a column, the default is the value of the configuration option **default full-text language**.  
  
> [!NOTE]  
>  All columns listed in a single full-text query function clause must use the same language, unless the LANGUAGE option is specified in the query. The language used for the full-text indexed column being queried determines the linguistic analysis performed on arguments of the full-text query predicates ([CONTAINS](../../t-sql/queries/contains-transact-sql.md) and [FREETEXT](../../t-sql/queries/freetext-transact-sql.md)) and functions ([CONTAINSTABLE](../../relational-databases/system-functions/containstable-transact-sql.md) and [FREETEXTTABLE](../../relational-databases/system-functions/freetexttable-transact-sql.md)).  
  
##  <a name="lang"></a> Choose the language for an indexed column  
 When creating a full-text index, we recommend that you specify a language for each indexed column. If a language is not specified for a column, the system default language is used. The language of a column determines which word breaker and stemmer are used for indexing that column. Also, the thesaurus file of that language will be used by full-text queries on the column.  
  
 There are a couple of things to consider when choosing the column language for creating a full-text index. These considerations relate to how your text is tokenized and then indexed by Full-Text Engine. For more information, see [Choose a Language When Creating a Full-Text Index](../../relational-databases/search/choose-a-language-when-creating-a-full-text-index.md).  
  
To view the word breaker language of specific columns, run the following statement.
   
```sql 
SELECT 'language_id' AS "LCID" FROM sys.fulltext_index_columns;
```  

For additional options and more info, see [sys.fulltext_index_columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-index-columns-transact-sql.md).

##  <a name="tshoot"></a> Troubleshoot word-breaking time-out errors  
 A word-breaking time-out error may occur in a variety of situations. or information about these situations and how to respond in each situation, see [MSSQLSERVER_30053](../errors-events/mssqlserver-30053-database-engine-error.md).

### Info about the MSSQLSERVER_30053 error
  
|Property|Value|
|-|-|
|Product Name|SQL Server|  
|Event ID|30053|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|FTXT_QUERY_E_WORDBREAKINGTIMEOUT|  
|Message Text|Word breaking timed out for the full-text query string. This can happen if the wordbreaker took a long time to process the full-text query string, or if a large number of queries are running on the server. Try running the query again under a lighter load.|  
  
#### Explanation  
 A word-breaking timeout error can occur in the following situations:  
  
-   The word breaker for the query language is configured incorrectly; for example, its registry settings are incorrect.  
  
-   The word breaker malfunctions for a specific query string.  
  
-   The word breaker returns too much data for a specific query string. Excess data is treated as a potential buffer overrun attack, and shuts down the filter daemon process (fdhost.exe), which hosts the word-breaking services.  
  
-   The filter daemon process configuration is incorrect.  
  
     The most common configuration problems are password expiration or a domain policy that prevents the filter daemon account from logging on.  
  
-   A very heavy query workload is running on the server instance; for example, the word-breaker took a long time to process the full-text query string, or a large number of queries are running on the server. Note that this is the least likely cause.  
  
#### User Action  
 Select the user action that is appropriate to the probable cause of the timeout, as follows:  
  
|Probable cause|User action|  
|--------------------|-----------------|  
|The word breaker for the query language is configured incorrectly.|If you are using a third-party word breaker it might be incorrectly registered with the operating system. In this case, re-register the word breaker. For more information, see [Revert the Word Breakers Used by Search to the Previous Version](revert-the-word-breakers-used-by-search-to-the-previous-version.md).|  
|The word breaker malfunctions for a specific query string.|If the word breaker is supported by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], contact Microsoft Customer Service and Support.|  
|The word breaker returns too much data for a specific query string.|If the word breaker is supported by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], contact Microsoft Customer Service and Support.|  
|The filter daemon process configuration is incorrect.|Ensure that you are using the current password and that a domain policy is not preventing the filter daemon account from logging on.|  
|A very heavy query workload is running on the server instance.|Try running the query again under a lighter load.|  

##  <a name="impact"></a> Understand the impact of updated word breakers  
 Each version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] typically includes new word breakers that have better linguistic rules and are more accurate than earlier word breakers. Potentially, the new word breakers might behave slightly differently from the word breakers in full-text indexes that were imported from previous versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].
 
This is significant if a full-text catalog was imported when a database was upgraded to the current version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. One or more languages used by the full-text indexes in the full-text catalog might now be associated with new word breakers. For more information, see [Upgrade Full-Text Search](../../relational-databases/search/upgrade-full-text-search.md).  
  

## See Also  
 [CREATE FULLTEXT INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-fulltext-index-transact-sql.md)    
 [ALTER FULLTEXT INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-fulltext-index-transact-sql.md)   
 [Configure and Manage Stopwords and Stoplists for Full-Text Search](../../relational-databases/search/configure-and-manage-stopwords-and-stoplists-for-full-text-search.md)   
 
  
