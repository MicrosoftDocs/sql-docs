---
description: "CONTAINSTABLE (Transact-SQL)"
title: "CONTAINSTABLE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/24/2015"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "CONTAINSTABLE"
  - "CONTAINSTABLE_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "precise or fuzzy (less precise) matches [full-text search]"
  - "fuzzy (less precise) word or phrase search [full-text search]"
  - "word searches [full-text search]"
  - "weighted values [full-text search]"
  - "values [SQL Server], ranked"
  - "LANGUAGE option"
  - "NEAR option [full-text search]"
  - "RANK column"
  - "phrase searches [full-text search]"
  - "conditions [SQL Server], CONTAINSTABLE"
  - "relevance ranking values [full-text search]"
  - "proximity searches [full-text search]"
  - "CONTAINSTABLE function (Transact-SQL)"
  - "ranked results [full-text search]"
  - "rankings [full-text search]"
  - "less precise (fuzzy) searches [full-text search]"
ms.assetid: e580c210-cf57-419d-9544-7f650f2ab814
author: MikeRayMSFT
ms.author: mikeray
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# CONTAINSTABLE (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns a table of zero, one, or more rows for those columns containing precise or fuzzy (less precise) matches to single words and phrases, the proximity of words within a certain distance of one another, or weighted matches. CONTAINSTABLE is used in the [FROM clause](../../t-sql/queries/from-transact-sql.md) of a [!INCLUDE[tsql](../../includes/tsql-md.md)] SELECT statement and is referenced as if it were a regular table name. It performs a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] full-text search on full-text indexed columns containing character-based data types.  
  
 CONTAINSTABLE is useful for the same kinds of matches as the [CONTAINS predicate](../../t-sql/queries/contains-transact-sql.md) and uses the same search conditions as CONTAINS.  
  
 Unlike CONTAINS, however, queries using CONTAINSTABLE return a relevance ranking value (RANK) and full-text key (KEY) for each row.  For information about the forms of full-text searches that are supported by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Query with Full-Text Search](../../relational-databases/search/query-with-full-text-search.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
CONTAINSTABLE   
( table , { column_name | ( column_list ) | * } , ' <contains_search_condition> '   
     [ , LANGUAGE language_term]   
  [ , top_n_by_rank ]   
)   
  
<contains_search_condition> ::=   
    { <simple_term>   
    | <prefix_term>   
    | <generation_term>   
    | <generic_proximity_term>   
    | <custom_proximity_term>   
    |  <weighted_term>   
    }   
    | { ( <contains_search_condition> )   
    { { AND | & } | { AND NOT | &! } | { OR | | } }   
     <contains_search_condition> [ ...n ]   
    }  
  
<simple_term> ::=   
     { word | "phrase" }  
<prefix term> ::=   
     { "word*" | "phrase*" }   
<generation_term> ::=   
     FORMSOF ( { INFLECTIONAL | THESAURUS } , <simple_term> [ ,...n ] )   
  
<generic_proximity_term> ::=   
     { <simple_term> | <prefix_term> } { { { NEAR | ~ }   
     { <simple_term> | <prefix_term> } } [ ...n ] }  
  
<custom_proximity_term> ::=   
  NEAR (   
     {  
        { <simple_term> | <prefix_term> } [ ,...n ]  
     |  
        ( { <simple_term> | <prefix_term> } [ ,...n ] )   
      [, <maximum_distance> [, <match_order> ] ]  
     }  
       )   
  
      <maximum_distance> ::= { integer | MAX }  
      <match_order> ::= { TRUE | FALSE }   
  
<weighted_term> ::=   
     ISABOUT  
    ( { {   
  <simple_term>   
  | <prefix_term>   
  | <generation_term>   
  | <proximity_term>   
  }   
   [ WEIGHT ( weight_value ) ]   
   } [ ,...n ]   
    )  
  
```  
  
## Arguments  
 *table*  
 Is the name of a table that has been full-text indexed. *table* can be a one-, two-, three-, or four-part database object name. When querying a view, only one full-text indexed base table can be involved.  
  
 *table* cannot specify a server name and cannot be used in queries against linked servers.  
  
 *column_name*  
 Is the name of one or more columns that are indexed for full-text searching. The columns can be of type **char**, **varchar**, **nchar**, **nvarchar**, **text**, **ntext**, **image**, **xml**, **varbinary**, or **varbinary(max)**.  
  
 *column_list*  
 Indicates that several columns, separated by a comma, can be specified. *column_list* must be enclosed in parentheses. Unless *language_term* is specified, the language of all columns of *column_list* must be the same.  
  
 \*  
 Specifies that all full-text indexed columns in *table* should be used to search for the given search condition. Unless *language_term* is specified, the language of all columns of the table must be the same.  
  
 LANGUAGE *language_term*  
 Is the language whose resources will be used for word breaking, stemming, and thesaurus and noise-word (or [stopword](../../relational-databases/search/configure-and-manage-stopwords-and-stoplists-for-full-text-search.md)) removal as part of the query. This parameter is optional and can be specified as a string, integer, or hexadecimal value corresponding to the locale identifier (LCID) of a language. If *language_term* is specified, the language it represents will be applied to all elements of the search condition. If no value is specified, the column full-text language is used.  
  
 If documents of different languages are stored together as binary large objects (BLOBs) in a single column, the locale identifier (LCID) of a given document determines what language is used to index its content. When querying such a column, specifying *LANGUAGE**language_term* can increase the probability of a good match.  
  
 When specified as a string, *language_term* corresponds to the **alias** column value in the [sys.syslanguages](../../relational-databases/system-compatibility-views/sys-syslanguages-transact-sql.md) compatibility view.  The string must be enclosed in single quotation marks, as in '*language_term*'. When specified as an integer, *language_term* is the actual LCID that identifies the language. When specified as a hexadecimal value, *language_term* is 0x followed by the hexadecimal value of the LCID. The hexadecimal value must not exceed eight digits, including leading zeros.  
  
 If the value is in double-byte character set (DBCS) format, [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will convert it to Unicode.  
  
 If the language specified is not valid or there are no resources installed that correspond to that language, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns an error. To use the neutral language resources, specify 0x0 as *language_term*.  
  
 *top_n_by_rank*  
 Specifies that only the *n* highest ranked matches, in descending order, are returned. Applies only when an integer value, *n*, is specified. If *top_n_by_rank* is combined with other parameters, the query could return fewer rows than the number of rows that actually match all the predicates. *top_n_by_rank* allows you to increase query performance by recalling only the most relevant hits.  
  
 <contains_search_condition>  
 Specifies the text to search for in *column_name* and the conditions for a match. For information about search conditions, see [CONTAINS &#40;Transact-SQL&#41;](../../t-sql/queries/contains-transact-sql.md).  
  
## Remarks  
 Full-text predicates and functions work on a single table, which is implied in the FROM predicate. To search on multiple tables, use a joined table in your FROM clause to search on a result set that is the product of two or more tables.  
  
 The table returned has a column named **KEY** that contains full-text key values. Each full-text indexed table has a column whose values are guaranteed to be unique, and the values returned in the **KEY** column are the full-text key values of the rows that match the selection criteria specified in the contains search condition. The **TableFulltextKeyColumn** property, obtained from the OBJECTPROPERTYEX function, provides the identity of this unique key column. To obtain the ID of the column associated with the full-text key of the full-text index, use **sys.fulltext_indexes**. For more information, see [sys.fulltext_indexes &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-indexes-transact-sql.md).  
  
 To obtain the rows you want from the original table, specify a join with the CONTAINSTABLE rows. The typical form of the FROM clause for a SELECT statement using CONTAINSTABLE is:  
  
```  
SELECT select_list  
FROM table AS FT_TBL INNER JOIN  
   CONTAINSTABLE(table, column, contains_search_condition) AS KEY_TBL  
   ON FT_TBL.unique_key_column = KEY_TBL.[KEY];  
```  
  
 The table produced by CONTAINSTABLE includes a column named **RANK**. The **RANK** column is a value (from 0 through 1000) for each row indicating how well a row matched the selection criteria. This rank value is typically used in one of these ways in the SELECT statement:  
  
-   In the ORDER BY clause to return the highest-ranking rows as the first rows in the table.  
  
-   In the select list to see the rank value assigned to each row.  
  
## Permissions  
 Execute permissions are available only by users with the appropriate SELECT privileges on the table or the referenced table's columns.  
  
## Examples  
  
### A. Simple Example  
 The following example creates and populates a simple table of two columns, listing 3 counties and the colors in their flags. The it creates and populates a full-text catalog and index on the table. Then the **CONTAINSTABLE** syntax is demonstrated. This example demonstrates how the rank value grows higher when the search value is met multiple times. In the last query, Tanzania which contains both green and black has a higher rank than Italy which contain only one of the queried colors.  
  
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
SELECT * FROM CONTAINSTABLE (Flags, FlagColors, 'Green') ORDER BY RANK DESC;  
SELECT * FROM CONTAINSTABLE (Flags, FlagColors, 'Green or Black') ORDER BY RANK DESC;  
```  
  
### B. Returning rank values  
 The following example searches for all product names containing the words "frame," "wheel," or "tire," and different weights are given to each word. For each returned row matching these search criteria, the relative closeness (ranking value) of the match is shown. In addition, the highest ranking rows are returned first.  
  
```  
USE AdventureWorks2012;  
GO  
  
SELECT FT_TBL.Name, KEY_TBL.RANK  
    FROM Production.Product AS FT_TBL   
        INNER JOIN CONTAINSTABLE(Production.Product, Name,   
        'ISABOUT (frame WEIGHT (.8),   
        wheel WEIGHT (.4), tire WEIGHT (.2) )' ) AS KEY_TBL  
            ON FT_TBL.ProductID = KEY_TBL.[KEY]  
ORDER BY KEY_TBL.RANK DESC;  
GO  
```  
  
### C. Returning rank values greater than a specified value  
  
||  
|-|  
|**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later.|  
  
 The following example uses NEAR to search for "`bracket`" and "`reflector`" close to each other in the `Production.Document` table. Only rows with a rank value of 50 or higher are returned.  
  
```  
USE AdventureWorks2012  
GO  
  
SELECT DocumentNode, Title, DocumentSummary  
FROM Production.Document AS DocTable   
INNER JOIN CONTAINSTABLE(Production.Document, Document,  
  'NEAR(bracket, reflector)' ) AS KEY_TBL  
  ON DocTable.DocumentNode = KEY_TBL.[KEY]  
WHERE KEY_TBL.RANK > 50  
ORDER BY KEY_TBL.RANK DESC;  
GO  
```  
  
> [!NOTE]  
>  If a full-text query does not specify an integer as the maximum distance, a document that contains only hits whose gap is greater than 100 logical terms will not meet the NEAR requirements, and its ranking will be 0.  
  
### D. Returning top 5 ranked results using top_n_by_rank  
 The following example returns the description of the top 5 products where the `Description` column contains the word "aluminum" near either the word "light" or the word "lightweight".  
  
```  
USE AdventureWorks2012;  
GO  
  
SELECT FT_TBL.ProductDescriptionID,  
   FT_TBL.Description,   
   KEY_TBL.RANK  
FROM Production.ProductDescription AS FT_TBL INNER JOIN  
   CONTAINSTABLE (Production.ProductDescription,  
      Description,   
      '(light NEAR aluminum) OR  
      (lightweight NEAR aluminum)',  
      5  
   ) AS KEY_TBL  
   ON FT_TBL.ProductDescriptionID = KEY_TBL.[KEY];  
GO  
```  
  
 `GO`  
  
### E. Specifying the LANGUAGE argument  
 The following example shows using the `LANGUAGE` argument.  
  
```  
USE AdventureWorks2012;  
GO  
  
SELECT FT_TBL.ProductDescriptionID,  
   FT_TBL.Description,   
   KEY_TBL.RANK  
FROM Production.ProductDescription AS FT_TBL INNER JOIN  
   CONTAINSTABLE (Production.ProductDescription,  
      Description,   
      '(light NEAR aluminum) OR  
      (lightweight NEAR aluminum)',  
      LANGUAGE N'English',  
      5  
   ) AS KEY_TBL  
   ON FT_TBL.ProductDescriptionID = KEY_TBL.[KEY];  
GO  
```  
  
> [!NOTE]  
>  The LANGUAGE *language_term* argumentis not required for using *top_n_by_rank.*  
  
## See Also  
 [Limit Search Results with RANK](../../relational-databases/search/limit-search-results-with-rank.md)   
 [Query with Full-Text Search](../../relational-databases/search/query-with-full-text-search.md)   
 [Create Full-Text Search Queries &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/create-full-text-search-queries-visual-database-tools.md)   
 [CONTAINS &#40;Transact-SQL&#41;](../../t-sql/queries/contains-transact-sql.md)   
 [Query with Full-Text Search](../../relational-databases/search/query-with-full-text-search.md)   
 [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)   
 [FROM &#40;Transact-SQL&#41;](../../t-sql/queries/from-transact-sql.md)  
  
