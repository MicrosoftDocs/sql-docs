---
title: "Search Condition (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/15/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "search"
  - "Search Condition"
  - "condition"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "OR operator [Transact-SQL]"
  - "CONTAINS predicate (Transact-SQL)"
  - "ESCAPE keyword"
  - "operators [Transact-SQL], search condition"
  - "search conditions [SQL Server]"
  - "WHERE clause, search conditions"
  - "ALL keyword"
  - "FREETEXT predicate (Transact-SQL)"
  - "EXISTS keyword"
  - "search conditions [SQL Server], about search conditions"
  - "NOT operator [Transact-SQL]"
  - "BETWEEN operator"
  - "SOME | ANY keyword"
  - "predicates [full-text search]"
  - "AND, about search conditions"
  - "logical operators [SQL Server], about search conditions"
  - "precedence [SQL Server], logical operators"
  - "logical operators [SQL Server], precedence"
  - "LIKE comparisons"
ms.assetid: 09974469-c5d2-4be8-bc5a-78e404660b2c
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Search Condition (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Is a combination of one or more predicates that use the logical operators AND, OR, and NOT.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for SQL Server and Azure SQL Database  
  
<search_condition> ::=  
    MATCH(<graph_search_pattern>) | <search_condition_without_match> | <search_condition> AND <search_condition>

<search_condition_without_match> ::= 
    { [ NOT ] <predicate> | ( <search_condition_without_match> ) }   
    [ { AND | OR } [ NOT ] { <predicate> | ( <search_condition_without_match> ) } ]   
[ ,...n ]   
  
<predicate> ::=   
    { expression { = | < > | ! = | > | > = | ! > | < | < = | ! < } expression   
    | string_expression [ NOT ] LIKE string_expression   
  [ ESCAPE 'escape_character' ]   
    | expression [ NOT ] BETWEEN expression AND expression   
    | expression IS [ NOT ] NULL   
    | CONTAINS   
  ( { column | * } , '<contains_search_condition>' )   
    | FREETEXT ( { column | * } , 'freetext_string' )   
    | expression [ NOT ] IN ( subquery | expression [ ,...n ] )   
    | expression { = | < > | ! = | > | > = | ! > | < | < = | ! < }   
  { ALL | SOME | ANY} ( subquery )   
    | EXISTS ( subquery )     }   
    
<graph_search_pattern> ::=
    { <node_alias> { 
                      { <-( <edge_alias> )- } 
                    | { -( <edge_alias> )-> }
                    <node_alias> 
                   } 
    }
  
<node_alias> ::=
    node_table_name | node_table_alias 

<edge_alias> ::=
    edge_table_name | edge_table_alias
```  
  
```  
-- Syntax for Azure SQL Data Warehouse and Parallel Data Warehouse  
  
< search_condition > ::=   
    { [ NOT ] <predicate> | ( <search_condition> ) }   
    [ { AND | OR } [ NOT ] { <predicate> | ( <search_condition> ) } ]   
[ ,...n ]   
  
<predicate> ::=   
    { expression { = | < > | ! = | > | > = | < | < = } expression   
    | string_expression [ NOT ] LIKE string_expression   
    | expression [ NOT ] BETWEEN expression AND expression   
    | expression IS [ NOT ] NULL   
    | expression [ NOT ] IN (subquery | expression [ ,...n ] )   
    | expression [ NOT ] EXISTS (subquery)     }   
```  
  
## Arguments  
 \<search_condition>  
 Specifies the conditions for the rows returned in the result set for a SELECT statement, query expression, or subquery. For an UPDATE statement, specifies the rows to be updated. For a DELETE statement, specifies the rows to be deleted. There is no limit to the number of predicates that can be included in a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement search condition.  
  
 \<graph_search_pattern>  
 Specifies the graph match pattern. For more information about the arguments for this clause, see [MATCH &#40;Transact-SQL&#41;](../../t-sql/queries/match-sql-graph.md)
 
 NOT  
 Negates the Boolean expression specified by the predicate. For more information, see [NOT &#40;Transact-SQL&#41;](../../t-sql/language-elements/not-transact-sql.md).  
  
 AND  
 Combines two conditions and evaluates to TRUE when both of the conditions are TRUE. For more information, see [AND &#40;Transact-SQL&#41;](../../t-sql/language-elements/and-transact-sql.md).  
  
 OR  
 Combines two conditions and evaluates to TRUE when either condition is TRUE. For more information, see [OR &#40;Transact-SQL&#41;](../../t-sql/language-elements/or-transact-sql.md).  
  
 \< predicate >  
 Is an expression that returns TRUE, FALSE, or UNKNOWN.  
  
 *expression*  
 Is a column name, a constant, a function, a variable, a scalar subquery, or any combination of column names, constants, and functions connected by an operator or operators, or a subquery. The expression can also contain the CASE expression.  
  
> [!NOTE]  
>  Non-Unicode string constants and variables use the code page that corresponds to the default collation of the database. Code page conversions can occur when working with only non-Unicode character data and referencing the non-Unicode character data types **char**, **varchar**, and **text**. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] converts non-Unicode string constants and variables to the code page that corresponds to the collation of the referenced column or specified using COLLATE, if that code page is different than the code page that corresponds to the default collation of the database. Any characters not found in the new code page will be translated to a similar character, if a [best-fit mapping](https://www.unicode.org/Public/MAPPINGS/VENDORS/MICSFT/WindowsBestFit/) can be found, or else will be converted to the default replacement character of "?".  
>  
> When working with multiple code pages, character constants can be prefixed with the uppercase letter 'N', and Unicode variables can be used, to avoid code page conversions.  
  
 =  
 Is the operator used to test the equality between two expressions.  
  
 <>  
 Is the operator used to test the condition of two expressions not being equal to each other.  
  
 !=  
 Is the operator used to test the condition of two expressions not being equal to each other.  
  
 \>  
 Is the operator used to test the condition of one expression being greater than the other.  
  
 \>=  
 Is the operator used to test the condition of one expression being greater than or equal to the other expression.  
  
 !>  
 Is the operator used to test the condition of one expression not being greater than the other expression.  
  
 <  
 Is the operator used to test the condition of one expression being less than the other.  
  
 <=  
 Is the operator used to test the condition of one expression being less than or equal to the other expression.  
  
 !<  
 Is the operator used to test the condition of one expression not being less than the other expression.  
  
 *string_expression*  
 Is a string of characters and wildcard characters.  
  
 [ NOT ] LIKE  
 Indicates that the subsequent character string is to be used with pattern matching. For more information, see [LIKE &#40;Transact-SQL&#41;](../../t-sql/language-elements/like-transact-sql.md).  
  
 ESCAPE **'***escape_ character***'**  
 Allows for a wildcard character to be searched for in a character string instead of functioning as a wildcard. *escape_character* is the character that is put in front of the wildcard character to indicate this special use.  
  
 [ NOT ] BETWEEN  
 Specifies an inclusive range of values. Use AND to separate the starting and ending values. For more information, see [BETWEEN &#40;Transact-SQL&#41;](../../t-sql/language-elements/between-transact-sql.md).  
  
 IS [ NOT ] NULL  
 Specifies a search for null values, or for values that are not null, depending on the keywords used. An expression with a bitwise or arithmetic operator evaluates to NULL if any one of the operands is NULL.  
  
 CONTAINS  
 Searches columns that contain character-based data for precise or less precise (*fuzzy*) matches to single words and phrases, the proximity of words within a certain distance of one another, and weighted matches. This option can only be used with SELECT statements. For more information, see [CONTAINS &#40;Transact-SQL&#41;](../../t-sql/queries/contains-transact-sql.md).  
  
 FREETEXT  
 Provides a simple form of natural language query by searching columns that contain character-based data for values that match the meaning instead of the exact words in the predicate. This option can only be used with SELECT statements. For more information, see [FREETEXT &#40;Transact-SQL&#41;](../../t-sql/queries/freetext-transact-sql.md).  
  
 [ NOT ] IN  
 Specifies the search for an expression, based on whether the expression is included in or excluded from a list. The search expression can be a constant or a column name, and the list can be a set of constants or, more typically, a subquery. Enclose the list of values in parentheses. For more information, see [IN &#40;Transact-SQL&#41;](../../t-sql/language-elements/in-transact-sql.md).  
  
 *subquery*  
 Can be considered a restricted SELECT statement and is similar to \<query_expression> in the SELECT statement. The ORDER BY clause and the INTO keyword are not allowed. For more information, see [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md).  
  
 ALL  
 Used with a comparison operator and a subquery. Returns TRUE for \<predicate> when all values retrieved for the subquery satisfy the comparison operation, or FALSE when not all values satisfy the comparison or when the subquery returns no rows to the outer statement. For more information, see [ALL &#40;Transact-SQL&#41;](../../t-sql/language-elements/all-transact-sql.md).  
  
 { SOME | ANY }  
 Used with a comparison operator and a subquery. Returns TRUE for \<predicate> when any value retrieved for the subquery satisfies the comparison operation, or FALSE when no values in the subquery satisfy the comparison or when the subquery returns no rows to the outer statement. Otherwise, the expression is UNKNOWN. For more information, see [SOME &#124; ANY &#40;Transact-SQL&#41;](../../t-sql/language-elements/some-any-transact-sql.md).  
  
 EXISTS  
 Used with a subquery to test for the existence of rows returned by the subquery. For more information, see [EXISTS &#40;Transact-SQL&#41;](../../t-sql/language-elements/exists-transact-sql.md).  
  
## Remarks  
 The order of precedence for the logical operators is NOT (highest), followed by AND, followed by OR. Parentheses can be used to override this precedence in a search condition. The order of evaluation of logical operators can vary depending on choices made by the query optimizer. For more information about how the logical operators operate on logic values, see [AND &#40;Transact-SQL&#41;](../../t-sql/language-elements/and-transact-sql.md), [OR &#40;Transact-SQL&#41;](../../t-sql/language-elements/or-transact-sql.md), and [NOT &#40;Transact-SQL&#41;](../../t-sql/language-elements/not-transact-sql.md).  
  
## Examples  
  
### A. Using WHERE with LIKE and ESCAPE syntax  
 The following example searches for the rows in which the `LargePhotoFileName` column has the characters `green_`, and uses the `ESCAPE` option because _ is a wildcard character. Without specifying the `ESCAPE` option, the query would search for any description values that contain the word `green` followed by any single character other than the _ character.  
  
```  
USE AdventureWorks2012 ;  
GO  
SELECT *   
FROM Production.ProductPhoto  
WHERE LargePhotoFileName LIKE '%greena_%' ESCAPE 'a' ;  
```  
  
### B. Using WHERE and LIKE syntax with Unicode data  
 The following example uses the `WHERE` clause to retrieve the mailing address for any company that is outside the United States (`US`) and in a city whose name starts with `Pa`.  
  
```  
USE AdventureWorks2012 ;  
GO  
SELECT AddressLine1, AddressLine2, City, PostalCode, CountryRegionCode    
FROM Person.Address AS a  
JOIN Person.StateProvince AS s ON a.StateProvinceID = s.StateProvinceID  
WHERE CountryRegionCode NOT IN ('US')  
AND City LIKE N'Pa%' ;  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### C. Using WHERE with LIKE  
 The following example searches for the rows in which the `LastName` column has the characters `and`.  
  
```  
-- Uses AdventureWorks  
  
SELECT EmployeeKey, LastName  
FROM DimEmployee  
WHERE LastName LIKE '%and%';  
```  
  
### D. Using WHERE and LIKE syntax with Unicode data  
 The following example uses the `WHERE` clause to perform a Unicode search on the `LastName` column.  
  
```  
-- Uses AdventureWorks  
  
SELECT EmployeeKey, LastName  
FROM DimEmployee  
WHERE LastName LIKE N'%and%';  
```  
  
## See Also  
 [Aggregate Functions &#40;Transact-SQL&#41;](../../t-sql/functions/aggregate-functions-transact-sql.md)   
 [CASE &#40;Transact-SQL&#41;](../../t-sql/language-elements/case-transact-sql.md)   
 [CONTAINSTABLE &#40;Transact-SQL&#41;](../../relational-databases/system-functions/containstable-transact-sql.md)   
 [Cursors &#40;Transact-SQL&#41;](../../t-sql/language-elements/cursors-transact-sql.md)   
 [DELETE &#40;Transact-SQL&#41;](../../t-sql/statements/delete-transact-sql.md)   
 [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)   
 [FREETEXTTABLE &#40;Transact-SQL&#41;](../../relational-databases/system-functions/freetexttable-transact-sql.md)   
 [FROM &#40;Transact-SQL&#41;](../../t-sql/queries/from-transact-sql.md)   
 [Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/operators-transact-sql.md)   
 [UPDATE &#40;Transact-SQL&#41;](../../t-sql/queries/update-transact-sql.md)  
  
  

