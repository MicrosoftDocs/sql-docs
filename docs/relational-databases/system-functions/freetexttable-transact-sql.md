---
title: "FREETEXTTABLE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "FREETEXTTABLE_TSQL"
  - "FREETEXTTABLE"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "search conditions [SQL Server], meaning matches"
  - "meaning matches [full-text search]"
  - "FREETEXTTABLE function (Transact-SQL)"
  - "ranked results [full-text search]"
  - "column searches [full-text search]"
ms.assetid: 4523ae15-4260-40a7-a53c-8df15e1fee79
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# FREETEXTTABLE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Is a function used in the [FROM clause](../../t-sql/queries/from-transact-sql.md) of a [!INCLUDE[tsql](../../includes/tsql-md.md)] SELECT statement to perform a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] full-text search on full-text indexed columns containing character-based data types. This function returns a table of zero, one, or more rows for those columns containing values that match the meaning and not just the exact wording, of the text in the specified *freetext_string*. FREETEXTTABLE is referenced as if it were a regular table name.  
  
 FREETEXTTABLE is useful for the same kinds of matches as the [FREETEXT &#40;Transact-SQL&#41;](../../t-sql/queries/freetext-transact-sql.md),  
  
 Queries using FREETEXTTABLE return a relevance ranking value (RANK) and full-text key (KEY) for each row.  
  
> [!NOTE]  
>  For information about the forms of full-text searches that are supported by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Query with Full-Text Search](../../relational-databases/search/query-with-full-text-search.md).  
  
(https://azure.microsoft.com/documentation/articles/sql-database-preview-whats-new/?WT.mc_id=TSQL_GetItTag)).|  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
FREETEXTTABLE (table , { column_name | (column_list) | * }   
          , 'freetext_string'   
     [ , LANGUAGE language_term ]   
     [ , top_n_by_rank ] )  
```  
  
## Arguments  
 *table*  
 Is the name of the table that has been marked for full-text querying. *table* or *view*can be a one-, two-, or three-part database object name. When querying a view, only one full-text indexed base table can be involved.  
  
 *table* cannot specify a server name and cannot be used in queries against linked servers.  
  
 *column_name*  
 Is the name of one or more full-text indexed columns of the table specified in the FROM clause. The columns can be of type **char**, **varchar**, **nchar**, **nvarchar**, **text**, **ntext**, **image**, **xml**, **varbinary**, or **varbinary(max)**.  
  
 *column_list*  
 Indicates that several columns, separated by a comma, can be specified. *column_list* must be enclosed in parentheses. Unless *language_term* is specified, the language of all columns of *column_list* must be the same.  
  
 \*  
 Specifies that all columns that have been registered for full-text searching should be used to search for the given *freetext_string*. Unless *language_term* is specified, the language of all full-text indexed columns in the table must be the same.  
  
 *freetext_string*  
 Is text to search for in the *column_name*. Any text, including words, phrases or sentences, can be entered. Matches are generated if any term or the forms of any term is found in the full-text index.  
  
 Unlike in the CONTAINS search condition where AND is a keyword, when used in *freetext_string* the word 'and' is considered a noise word, or [stopword](../../relational-databases/search/configure-and-manage-stopwords-and-stoplists-for-full-text-search.md), and will be discarded.  
  
 Use of WEIGHT, FORMSOF, wildcards, NEAR and other syntax is not allowed. *freetext_string* is wordbroken, stemmed, and passed through the thesaurus.  
  
 LANGUAGE *language_term*  
 Is the language whose resources will be used for word breaking, stemming, and thesaurus and stopword removal as part of the query. This parameter is optional and can be specified as a string, integer, or hexadecimal value corresponding to the locale identifier (LCID) of a language. If *language_term* is specified, the language it represents will be applied to all elements of the search condition. If no value is specified, the column full-text language is used.  
  
 If documents of different languages are stored together as binary large objects (BLOBs) in a single column, the locale identifier (LCID) of a given document determines what language is used to index its content. When querying such a column, specifying *LANGUAGE language_term* can increase the probability of a good match.  
  
 When specified as a string, *language_term* corresponds to the **alias** column value in the [sys.syslanguages &#40;Transact-SQL&#41;](../../relational-databases/system-compatibility-views/sys-syslanguages-transact-sql.md) compatibility view.  The string must be enclosed in single quotation marks, as in '*language_term*'. When specified as an integer, *language_term* is the actual LCID that identifies the language. When specified as a hexadecimal value, *language_term* is 0x followed by the hexadecimal value of the LCID. The hexadecimal value must not exceed eight digits, including leading zeros.  
  
 If the value is in double-byte character set (DBCS) format, [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will convert it to Unicode.  
  
 If the language specified is not valid or there are no resources installed that correspond to that language, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns an error. To use the neutral language resources, specify 0x0 as *language_term*.  
  
 *top_n_by_rank*  
 Specifies that only the *n*highest ranked matches, in descending order, are returned. Applies only when an integer value, *n*, is specified. If *top_n_by_rank* is combined with other parameters, the query could return fewer rows than the number of rows that actually match all the predicates. *top_n_by_rank* allows you to increase query performance by recalling only the most relevant hits.  
  
## Remarks  
 Full-text predicates and functions work on a single table, which is implied in the FROM predicate. To search on multiple tables, use a joined table in your FROM clause to search on a result set that is the product of two or more tables.  
  
 FREETEXTTABLE uses the same search conditions as the FREETEXT predicate.  
  
 Like CONTAINSTABLE, the table returned has columns named **KEY** and **RANK**, which are referenced within the query to obtain the appropriate rows and use the row ranking values.  
  
## Permissions  
 FREETEXTTABLE can be invoked only by users with appropriate SELECT privileges for the specified table or the referenced columns of the table.  
  
## Examples  
  
### A. Simple Example  
 The following example creates and populates a simple table of two columns, listing 3 counties and the colors in their flags. The it creates and populates a full-text catalog and index on the table. Then the **FREETEXTTABLE** syntax is demonstrated.  
  
```  
CREATE TABLE Flags (Country nvarchar(30) NOT NULL, FlagColors varchar(200));  
CREATE UNIQUE CLUSTERED INDEX FlagKey ON Flags(Country);  
INSERT Flags VALUES ('France', 'Blue and White and Red');  
INSERT Flags VALUES ('Italy', 'Green and White and Red');  
INSERT Flags VALUES ('Tanzania', 'Green and Yellow and Black and Yellow and Blue');  
SELECT * FROM Flags;  
GO  
  
CREATE FULLTEXT CATALOG TestFTCat;  
CREATE FULLTEXT INDEX ON Flags(FlagColors) KEY INDEX FlagKey ON TestFTCat;  
GO   
  
SELECT * FROM Flags;  
SELECT * FROM FREETEXTTABLE (Flags, FlagColors, 'Blue');  
SELECT * FROM FREETEXTTABLE (Flags, FlagColors, 'Yellow');  
```  
  
### B. Using FREETEXT in an INNER JOIN  
 The following example returns the description and rank of any products with a description that matches the meaning of `high level of performance`.  
  
```  
USE AdventureWorks2012;  
GO  
  
SELECT FT_TBL.Description  
    ,KEY_TBL.RANK  
FROM Production.ProductDescription AS FT_TBL   
    INNER JOIN FREETEXTTABLE(Production.ProductDescription,  
    Description,   
    'high level of performance') AS KEY_TBL  
ON FT_TBL.ProductDescriptionID = KEY_TBL.[KEY]  
ORDER BY RANK DESC;  
GO  
```  
  
### C. Specifying Language and Highest Ranked Matches  
 The following example is identical and shows the use of the `LANGUAGE`*language_term* and *top_n_by_rank* parameters.  
  
```  
USE AdventureWorks2012;  
GO  
  
SELECT FT_TBL.Description  
    ,KEY_TBL.RANK  
FROM Production.ProductDescription AS FT_TBL   
    INNER JOIN FREETEXTTABLE(Production.ProductDescription,  
    Description,   
    'high level of performance',  
    LANGUAGE N'English', 2) AS KEY_TBL  
ON FT_TBL.ProductDescriptionID = KEY_TBL.[KEY]  
ORDER BY RANK DESC;  
GO  
```  
  
> [!NOTE]
>  The LANGUAGE *language_term* paramete*r* is not required to use the *top_n_by_rank* parameter*.*  
  
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
 [FREETEXT &#40;Transact-SQL&#41;](../../t-sql/queries/freetext-transact-sql.md)   
 [Rowset Functions &#40;Transact-SQL&#41;](../../t-sql/functions/rowset-functions-transact-sql.md)   
 [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)   
 [WHERE &#40;Transact-SQL&#41;](../../t-sql/queries/where-transact-sql.md)   
 [precompute rank Server Configuration Option](../../database-engine/configure-windows/precompute-rank-server-configuration-option.md)  
  
  
