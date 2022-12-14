---
title: "ALTER FULLTEXT STOPLIST (Transact-SQL)"
description: ALTER FULLTEXT STOPLIST (Transact-SQL)
author: markingmyname
ms.author: maghan
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ALTER FULLTEXT STOPLIST"
  - "ALTER_FULLTEXT_STOPLIST_TSQL"
helpviewer_keywords:
  - "stoplists [full-text search]"
  - "full-text search [SQL Server], stoplists"
  - "ALTER FULLTEXT STOPLIST statement"
  - "full-text search [SQL Server], stopwords"
  - "stopwords [full-text search]"
dev_langs:
  - "TSQL"
---
# ALTER FULLTEXT STOPLIST (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]	

  Inserts or deletes a stop word in the default full-text stoplist of the current database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
ALTER FULLTEXT STOPLIST stoplist_name  
{   
        ADD [N] 'stopword' LANGUAGE language_term    
  | DROP   
    {  
        'stopword' LANGUAGE language_term   
      | ALL LANGUAGE language_term   
      | ALL  
     }  
;  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *stoplist_name*  
 Is the name of the stoplist being altered. *stoplist_name* can be a maximum of 128 characters.  
  
 **'** *stopword* **'**  
 Is a string that could be a word with linguistic meaning in the specified language or a token that does not have a linguistic meaning. *stopword* is limited to the maximum token length (64 characters). A stopword can be specified as a Unicode string.  
  
 LANGUAGE *language_term*  
 Specifies the language to associate with the *stopword* being added or dropped.  
  
 *language_term* can be specified as a string, integer, or hexadecimal value corresponding to the locale identifier (LCID) of the language, as follows:  
  
|Format|Description|  
|------------|-----------------|  
|String|*language_term* corresponds to the **alias** column value in the [sys.syslanguages (Transact-SQL)](../../relational-databases/system-compatibility-views/sys-syslanguages-transact-sql.md) compatibility view. The string must be enclosed in single quotation marks, as in **'***language_term***'**.|  
|Integer|*language_term* is the LCID of the language.|  
|Hexadecimal|*language_term* is 0x followed by the hexadecimal value of the LCID. The hexadecimal value must not exceed eight digits, including leading zeros. If the value is in double-byte character set (DBCS) format, SQL Server converts it to Unicode.|  
  
 ADD **'***stopword***'** LANGUAGE *language_term*  
 Adds a stop word to the stoplist for the language specified by LANGUAGE *language_term*.  
  
 If the specified combination of keyword and the LCID value of the language is not unique in the STOPLIST, an error is returned.  If the LCID value does not correspond to a registered language, an error is generated.  
  
 DROP { **'***stopword***'** LANGUAGE *language_term* | ALL LANGUAGE *language_term* | ALL }  
 Drops a stop word from the stop list.  
  
 **'** *stopword* **'** LANGUAGE *language_term*  
 Drops the specified stop word for the language specified by *language_term*.  
  
 ALL LANGUAGE *language_term*  
 Drops all of the stop words for the language specified by *language_term*.  
  
 ALL  
 Drops all of the stop words in the stoplist.  
  
## Remarks  
 CREATE FULLTEXT STOPLIST is supported only for compatibility level 100 and higher. For compatibility levels 80 and 90, the system stoplist is always assigned to the database.  
  
## Permissions  
 To designate a stoplist as the default stoplist of the database requires ALTER DATABASE permission. To otherwise alter a stoplist requires being the stoplist owner or membership in the **db_owner** or **db_ddladmin** fixed database roles.  
  
## Examples  
 The following example alters a stoplist named `CombinedFunctionWordList`, adding the word 'en', first for Spanish and then for French.  
  
```sql  
ALTER FULLTEXT STOPLIST CombinedFunctionWordList ADD 'en' LANGUAGE 'Spanish';  
ALTER FULLTEXT STOPLIST CombinedFunctionWordList ADD 'en' LANGUAGE 'French';  
```  
  
## See Also  
 [CREATE FULLTEXT STOPLIST &#40;Transact-SQL&#41;](../../t-sql/statements/create-fulltext-stoplist-transact-sql.md)   
 [DROP FULLTEXT STOPLIST &#40;Transact-SQL&#41;](../../t-sql/statements/drop-fulltext-stoplist-transact-sql.md)   
 [Configure and Manage Stopwords and Stoplists for Full-Text Search](../../relational-databases/search/configure-and-manage-stopwords-and-stoplists-for-full-text-search.md)   
 [sys.fulltext_stoplists &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-stoplists-transact-sql.md)   
 [sys.fulltext_stopwords &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-stopwords-transact-sql.md)   
 [Configure and Manage Stopwords and Stoplists for Full-Text Search](../../relational-databases/search/configure-and-manage-stopwords-and-stoplists-for-full-text-search.md)  
  
  
