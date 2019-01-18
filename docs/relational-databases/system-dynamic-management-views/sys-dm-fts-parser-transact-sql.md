---
title: "sys.dm_fts_parser (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_fts_parser_TSQL"
  - "dm_fts_parser"
  - "dm_fts_parser_TSQL"
  - "sys.dm_fts_parser"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_fts_parser dynamic management function"
  - "troubleshooting [SQL Server], full-text search"
ms.assetid: 2736d376-fb9d-4b28-93ef-472b7a27623a
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# sys.dm_fts_parser (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the final tokenization result after applying a given [word breaker](../../relational-databases/search/configure-and-manage-word-breakers-and-stemmers-for-search.md), [thesaurus](../../relational-databases/search/configure-and-manage-thesaurus-files-for-full-text-search.md), and [stoplist](../../relational-databases/search/configure-and-manage-stopwords-and-stoplists-for-full-text-search.md) combination to a query string input. The tokenization result is equivalent to the output of the Full-Text Engine for the specified query string.  
  
 sys.dm_fts_parser is a dynamic management function.  
  
## Syntax  
  
```  
  
sys.dm_fts_parser('query_string', lcid, stoplist_id, accent_sensitivity)  
```  
  
## Arguments  
 *query_string*  
 The query that you want to parse. *query_string* can be a string chain that [CONTAINS](../../t-sql/queries/contains-transact-sql.md) syntax support. For example, you can include inflectional forms, a thesaurus, and logical operators.  
  
 *lcid*  
 Locale identifier (LCID) of the word breaker to be used for parsing *query_string*.  
  
 *stoplist_id*  
 ID of the stoplist, if any, to be used by the word breaker identified by *lcid*. *stoplist_id* is **int**. If you specify 'NULL', no stoplist is used. If you specify 0, the system STOPLIST is used.  
  
 A stoplist ID is unique within a database. To obtain the stoplist ID for a full-text index on a given table use the [sys.fulltext_indexes](../../relational-databases/system-catalog-views/sys-fulltext-indexes-transact-sql.md) catalog view.  
  
 *accent_sensitivity*  
 Boolean value that controls whether full-text search is sensitive or insensitive to diacritics. *accent_sensitivity* is **bit**, with one of the following values:  
  
|Value|Accent sensitivity is...|  
|-----------|----------------------------|  
|0|Insensitive<br /><br /> Words such as "café" and "cafe" are treated identically.|  
|1|Sensitive<br /><br /> Words such as "café" and "cafe" are treated differently.|  
  
> [!NOTE]  
>  To view the current setting of this value for a full-text catalog, run the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statement: `SELECT fulltextcatalogproperty('`*catalog_name*`', 'AccentSensitivity');`.  
  
## Table Returned  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|keyword|**varbinary(128)**|The hexadecimal representation of a given keyword returned by a word breaker. This representation is used to store the keyword in the full-text index. This value is not human-readable, but it is useful for relating a given keyword to output returned by other dynamic management views that return the content of a full-text index, such as [sys.dm_fts_index_keywords](../../relational-databases/system-dynamic-management-views/sys-dm-fts-index-keywords-transact-sql.md) and [sys.dm_fts_index_keywords_by_document](../../relational-databases/system-dynamic-management-views/sys-dm-fts-index-keywords-by-document-transact-sql.md).<br /><br /> **Note:** OxFF represents the special character that indicates the end of a file or dataset.|  
|group_id|**int**|Contain an integer value that is useful for differentiating the logical group from which a given term was generated. For example, '`Server AND DB OR FORMSOF(THESAURUS, DB)"`' produces the following group_id values in English:<br /><br /> 1: Server<br />2: DB<br />3: DB|  
|phrase_id|**int**|Contains an integer value that is useful for differentiating the cases in which alternative forms of compound words, such as full-text, are issued by the word breaker. Sometimes, with presence of compound words ('multi-million'), alternative forms are issued by the word breaker. These alternative forms (phrases) need to be differentiated sometimes.<br /><br /> For example, '`multi-million`' produces the following phrase_id values in English:<br /><br /> 1 for `multi`<br />1 for `million`<br />2 for `multimillion`|  
|occurrence|**int**|Indicates the order of each term in the parsing result. For example, for the phrase "`SQL Server query processor`" occurrence would contain the following occurrence values for the terms in the phrase, in English:<br /><br /> 1 for `SQL`<br />2 for `Server`<br />3 for `query`<br />4 for `processor`|  
|special_term|**nvarchar(4000)**|Contains information about the characteristics of the term that is being issued by the word breaker, one of:<br /><br /> Exact match<br /><br /> Noise word<br /><br /> End of Sentence<br /><br /> End of paragraph<br /><br /> End of Chapter|  
|display_term|**nvarchar(4000)**|Contains the human-readable form of the keyword. As with the functions designed to access the content of the full-text index, this displayed term might not be identical to the original term due to the denormalization limitation. However, it should be precise enough to help you identify it from the original input.|  
|expansion_type|**int**|Contains information about the nature of the expansion of a given term, one of:<br /><br /> 0 =Single word case<br /><br /> 2=Inflectional expansion<br /><br /> 4=Thesaurus expansion/replacement<br /><br /> For example, consider a case in which the thesaurus defines run as an expansion of `jog`:<br /><br /> `<expansion>`<br /><br /> `<sub>run</sub>`<br /><br /> `<sub>jog</sub>`<br /><br /> `</expansion>`<br /><br /> The term `FORMSOF (FREETEXT, run)` generates the following output:<br /><br /> `run` with expansion_type=0<br /><br /> `runs` with expansion_type=2<br /><br /> `running` with expansion_type=2<br /><br /> `ran` with expansion_type=2<br /><br /> `jog` with expansion_type=4|  
|source_term|**nvarchar(4000)**|The term or phrase from which a given term was generated or parsed. For example, a query on the '"`word breakers" AND stemmers'` produces the following source_term values in English:<br /><br /> `word breakers` for the display_term`word`<br />`word breakers` for the display_term`breakers`<br />`stemmers` for the display_term`stemmers`|  
  
## Remarks  
 **sys.dm_fts_parser** supports the syntax and features of full-text predicates, such as [CONTAINS](../../t-sql/queries/contains-transact-sql.md) and [FREETEXT](../../t-sql/queries/freetext-transact-sql.md), and functions, such as [CONTAINSTABLE](../../relational-databases/system-functions/containstable-transact-sql.md) and [FREETEXTTABLE](../../relational-databases/system-functions/freetexttable-transact-sql.md).  
  
## Using Unicode for Parsing Special Characters  
 When you parse a query string, **sys.dm_fts_parser** uses the collation of the database to which you are connected, unless you specify the query string as Unicode. Therefore, for a non-Unicode string that contains special characters, such as ü or ç, the output might be unexpected, depending on the collation of the database. To process a query string independently of the database collation, prefix the string with `N`, that is, `N'`*query_string*`'`.  
  
 For more information, see "C. Displaying the Output of a String that Contains Special Characters," later in this topic.  
  
## When to Use sys.dm_fts_parser  
 **sys.dm_fts_parser** can be very powerful for debugging purposes. Some major usage scenarios include:  
  
-   To understand how a given word breaker treats a given input  
  
     When a query returns unexpected results, a likely cause is the way that the word breaker is parsing and breaking the data. By using sys.dm_fts_parser, you discover the result that a word breaker passes to the full-text index. In addition, you can see which terms are stopwords, which are not searched in the full-text index. Whether a term is a stopword for a given language depends on whether it is in the stoplist specified by the *stoplist_id* value that is declared in the function.  
  
     Note as well the accent sensitivity flag, which will allow the user to see how the word breaker will parse the input having in mind its accent sensitivity information.  
  
-   To understand how the stemmer works on a given input  
  
     You can find out how the word breaker and the stemmer parse a query term and its stemming forms, by specifying a CONTAINS or CONTAINSTABLE query containing the following FORMSOF clause:  
  
    ```  
    FORMSOF( INFLECTIONAL, query_term )  
    ```  
  
     The results tell you what terms are being passed to the full-text index.  
  
-   To understand how the thesaurus expands or replaces all or part of the input  
  
     You can also specify:  
  
    ```  
    FORMSOF( THESAURUS, query_term )  
    ```  
  
     The results of this query show how the word breaker and thesaurus interact for the query term. you can see the expansion or replacements from the thesaurus and identify the resulting query that is actually being issued against the full-text index.  
  
     Note that if the user issues:  
  
    ```  
    FORMSOF( FREETEXT, query_term )  
    ```  
  
     The inflectional and Thesaurus capabilities will take place automatically.  
  
 In addition to the preceding usage scenarios, sys.dm_fts_parser can help significantly to understand and troubleshoot many other issues with full-text query.  
  
## Permissions  
 Requires membership in the **sysadmin** fixed server role and access rights to the specified stoplist.  
  
## Examples  
  
### A. Displaying the output of a given word breaker for a keyword or phrase  
 The following example returns the output from using the English word breaker, whose LCID is 1033, and no stoplist on the following query string:  
  
 `The Microsoft business analysis`  
  
 Accent sensitivity is disabled.  
  
```  
SELECT * FROM sys.dm_fts_parser (' "The Microsoft business analysis" ', 1033, 0, 0);  
```  
  
### B. Displaying the output of a given word breaker in the context of stoplist filtering  
 The following example returns the output from using the English word breaker, whose LCID is 1033, and an English stoplist, whose ID is 77, on the following query string:  
  
 `"The Microsoft business analysis" OR "MS revenue"`  
  
 Accent sensitivity is disabled.  
  
```  
SELECT * FROM sys.dm_fts_parser (' "The Microsoft business analysis"  OR " MS revenue" ', 1033, 77, 0);  
```  
  
### C. Displaying the Output of a String that Contains Special Characters  
 The following example uses Unicode to parse the following French string:  
  
 `français`  
  
 The example specifies the LCID for the French language, `1036`, and the ID of a user-defined stoplist, `5`. Accent sensitivity is enabled.  
  
```  
SELECT * FROM sys.dm_fts_parser(N'français', 1036, 5, 1);  
```  
  
## See Also  
 [Full-Text Search and Semantic Search Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/full-text-and-semantic-search-dynamic-management-views-functions.md)   
 [Full-Text Search](../../relational-databases/search/full-text-search.md)   
 [Configure and Manage Word Breakers and Stemmers for Search](../../relational-databases/search/configure-and-manage-word-breakers-and-stemmers-for-search.md)   
 [Configure and Manage Thesaurus Files for Full-Text Search](../../relational-databases/search/configure-and-manage-thesaurus-files-for-full-text-search.md)   
 [Configure and Manage Stopwords and Stoplists for Full-Text Search](../../relational-databases/search/configure-and-manage-stopwords-and-stoplists-for-full-text-search.md)   
 [Query with Full-Text Search](../../relational-databases/search/query-with-full-text-search.md)   
 [Query with Full-Text Search](../../relational-databases/search/query-with-full-text-search.md)   
 [Securables](../../relational-databases/security/securables.md)  
  
  
