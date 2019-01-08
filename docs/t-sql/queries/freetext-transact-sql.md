---
title: "FREETEXT (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "10/23/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "FREETEXT"
  - "FREETEXT_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "full-text search [SQL Server], meaning matches"
  - "meaning matches [full-text search]"
  - "FREETEXT predicate (Transact-SQL)"
  - "words in predicate [full-text search]"
  - "column searches [full-text search]"
ms.assetid: 2f199d3c-440e-4bcf-bdb5-82bb3994005d
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# FREETEXT (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Is a predicate used in the [!INCLUDE[tsql](../../includes/tsql-md.md)] [WHERE clause](../../t-sql/queries/where-transact-sql.md) of a [!INCLUDE[tsql](../../includes/tsql-md.md)] SELECT statement to perform a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] full-text search on full-text indexed columns containing character-based data types. This predicate searches for values that match the meaning and not just the exact wording of the words in the search condition. When FREETEXT is used, the full-text query engine internally performs the following actions on the *freetext_string*, assigns each term a weight, and then finds the matches:  
  
-   Separates the string into individual words based on word boundaries (word-breaking).  
  
-   Generates inflectional forms of the words (stemming).  
  
-   Identifies a list of expansions or replacements for the terms based on matches in the thesaurus.  
  
> [!NOTE]  
>  For information about the forms of full-text searches that are supported by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Query with Full-Text Search](../../relational-databases/search/query-with-full-text-search.md).  
  
**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ( [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [current version](https://go.microsoft.com/fwlink/p/?LinkId=299658)).
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
FREETEXT ( { column_name | (column_list) | * }   
          , 'freetext_string' [ , LANGUAGE language_term ] )  
```  
  
## Arguments  
 *column_name*  
 Is the name of one or more full-text indexed columns of the table specified in the FROM clause. The columns can be of type **char**, **varchar**, **nchar**, **nvarchar**, **text**, **ntext**, **image**, **xml**, **varbinary**, or **varbinary(max)**.  
  
 *column_list*  
 Indicates that several columns, separated by a comma, can be specified. *column_list* must be enclosed in parentheses. Unless *language_term* is specified, the language of all columns of *column_list* must be the same.  
  
 \*  
 Specifies that all columns that have been registered for full-text searching should be used to search for the given *freetext_string*. If more than one table is in the FROM clause, \* must be qualified by the table name. Unless *language_term* is specified, the language of all columns of the table must be the same.  
  
 *freetext_string*  
 Is text to search for in the *column_name*. Any text, including words, phrases or sentences, can be entered. Matches are generated if any term or the forms of any term is found in the full-text index.  
  
 Unlike in the CONTAINS and CONTAINSTABLE search condition where AND is a keyword, when used in *freetext_string* the word 'and' is considered a noise word, or [stopword](../../relational-databases/search/configure-and-manage-stopwords-and-stoplists-for-full-text-search.md), and will be discarded.  
  
 Use of WEIGHT, FORMSOF, wildcards, NEAR and other syntax is not allowed. *freetext_string* is wordbroken, stemmed, and passed through the thesaurus.  
  
 *freetext_string* is **nvarchar**. An implicit conversion occurs when another character data type is used as input. Large string data types nvarchar(max) and varchar(max) cannot be used. In the following example, the `@SearchWord` variable, which is defined as `varchar(30)`, causes an implicit conversion in the `FREETEXT` predicate.  
  
```sql  
  
USE AdventureWorks2012;  
GO  
DECLARE @SearchWord varchar(30)  
SET @SearchWord ='performance'  
SELECT Description   
FROM Production.ProductDescription   
WHERE FREETEXT(Description, @SearchWord);  
  
```  
  
 Because "parameter sniffing" does not work across conversion, use **nvarchar** for better performance. In the example, declare `@SearchWord` as `nvarchar(30)`.  
  
```sql  
  
USE AdventureWorks2012;  
GO  
DECLARE @SearchWord nvarchar(30)  
SET @SearchWord = N'performance'  
SELECT Description   
FROM Production.ProductDescription   
WHERE FREETEXT(Description, @SearchWord);  
  
```  
  
 You can also use the OPTIMIZE FOR query hint for cases in which a nonoptimal plan is generated.  
  
 LANGUAGE *language_term*  
 Is the language whose resources will be used for word breaking, stemming, and thesaurus and stopword removal as part of the query. This parameter is optional and can be specified as a string, integer, or hexadecimal value corresponding to the locale identifier (LCID) of a language. If *language_term* is specified, the language it represents will be applied to all elements of the search condition. If no value is specified, the column full-text language is used.  
  
 If documents of different languages are stored together as binary large objects (BLOBs) in a single column, the locale identifier (LCID) of a given document determines what language is used to index its content. When querying such a column, specifying *LANGUAGE language_term* can increase the probability of a good match.  
  
 When specified as a string, *language_term* corresponds to the **alias** column value in he [sys.syslanguages &#40;Transact-SQL&#41;](../../relational-databases/system-compatibility-views/sys-syslanguages-transact-sql.md) compatibility view.  The string must be enclosed in single quotation marks, as in '*language_term*'. When specified as an integer, *language_term* is the actual LCID that identifies the language. When specified as a hexadecimal value, *language_term* is 0x followed by the hexadecimal value of the LCID. The hexadecimal value must not exceed eight digits, including leading zeros.  
  
 If the value is in double-byte character set (DBCS) format, [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will convert it to Unicode.  
  
 If the language specified is not valid or there are no resources installed that correspond to that language, [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns an error. To use the neutral language resources, specify 0x0 as *language_term*.  
  
## General Remarks  
 Full-text predicates and functions work on a single table, which is implied in the FROM predicate. To search on multiple tables, use a joined table in your FROM clause to search on a result set that is the product of two or more tables.  
  
Full-text queries using FREETEXT are less precise than those full-text queries using CONTAINS. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] full-text search engine identifies important words and phrases. No special meaning is given to any of the reserved keywords or wildcard characters that typically have meaning when specified in the \<contains_search_condition> parameter of the CONTAINS predicate.
  
 Full-text predicates are not allowed in the [OUTPUT clause](../../t-sql/queries/output-clause-transact-sql.md) when the database compatibility level is set to 100.  
  
> [!NOTE]  
>  The FREETEXTTABLE function is useful for the same kinds of matches as the FREETEXT predicate. You can reference this function like a regular table name in the [FROM clause](../../t-sql/queries/from-transact-sql.md) of a SELECT statement. For more information, see [FREETEXTTABLE &#40;Transact-SQL&#41;](../../relational-databases/system-functions/freetexttable-transact-sql.md).  
  
## Querying Remote Servers  
 You can use a four-part name in the [CONTAINS](../../t-sql/queries/contains-transact-sql.md) or FREETEXT predicate to query full-text indexed columns of the target tables on a linked server. To prepare a remote server to receive full-text queries, create a full-text index on the target tables and columns on the remote server and then add the remote server as a linked server.  
  
## Comparison of LIKE to Full-Text Search  
 In contrast to full-text search, the [LIKE](../../t-sql/language-elements/like-transact-sql.md)[!INCLUDE[tsql](../../includes/tsql-md.md)] predicate works on character patterns only. Also, you cannot use the LIKE predicate to query formatted binary data. Furthermore, a LIKE query against a large amount of unstructured text data is much slower than an equivalent full-text query against the same data. A LIKE query against millions of rows of text data can take minutes to return; whereas a full-text query can take only seconds or less against the same data, depending on the number of rows that are returned.  
  
## Examples  
  
### A. Using FREETEXT to search for words containing specified character values  
 The following example searches for all documents containing the words related to vital, safety, components.  
  
```sql  
USE AdventureWorks2012;  
GO  
SELECT Title  
FROM Production.Document  
WHERE FREETEXT (Document, 'vital safety components' );  
GO  
```  
  
### B. Using FREETEXT with variables  
 The following example uses a variable instead of a specific search term.  
  
```sql  
USE AdventureWorks2012;  
GO  
DECLARE @SearchWord nvarchar(30);  
SET @SearchWord = N'high-performance';  
SELECT Description   
FROM Production.ProductDescription   
WHERE FREETEXT(Description, @SearchWord);  
GO  
```  
  
## See Also  
 [Get Started with Full-Text Search](../../relational-databases/search/get-started-with-full-text-search.md)   
 [Create and Manage Full-Text Catalogs](../../relational-databases/search/create-and-manage-full-text-catalogs.md)   
 [CREATE FULLTEXT CATALOG &#40;Transact-SQL&#41;](../../t-sql/statements/create-fulltext-catalog-transact-sql.md)   
 [CREATE FULLTEXT INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-fulltext-index-transact-sql.md)   
 [Create and Manage Full-Text Indexes](../../relational-databases/search/create-and-manage-full-text-indexes.md)   
 [Query with Full-Text Search](../../relational-databases/search/query-with-full-text-search.md)   
 [Create Full-Text Search Queries &#40;Visual Database Tools&#41;](https://msdn.microsoft.com/library/537fa556-390e-4c88-9b8e-679848d94abc)   
 [CONTAINS &#40;Transact-SQL&#41;](../../t-sql/queries/contains-transact-sql.md)   
 [CONTAINSTABLE &#40;Transact-SQL&#41;](../../relational-databases/system-functions/containstable-transact-sql.md)   
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)   
 [FREETEXTTABLE &#40;Transact-SQL&#41;](../../relational-databases/system-functions/freetexttable-transact-sql.md)   
 [WHERE &#40;Transact-SQL&#41;](../../t-sql/queries/where-transact-sql.md)  
  
  
