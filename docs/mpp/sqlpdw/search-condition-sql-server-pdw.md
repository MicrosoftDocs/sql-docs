---
title: "Search Condition (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 52def05a-8cff-45e1-991f-1c5c7d19b61e
caps.latest.revision: 17
author: BarbKess
---
# Search Condition (SQL Server PDW)
The search condition, in SQL Server PDW, is a combination of one or more predicates that use the logical operators AND, OR, and NOT.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
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
<search_condition>  
Specifies the conditions for the rows returned in the result set for a SELECT statement, query expression, or subquery. For an UPDATE statement, specifies the rows to be updated. For a DELETE statement, specifies the rows to be deleted. There is no limit to the number of predicates that can be included in a SQL statement search condition.  
  
NOT  
Negates the Boolean expression specified by the predicate. For more information, see [NOT &#40;SQL Server PDW&#41;](../sqlpdw/not-sql-server-pdw.md).  
  
AND  
Combines two conditions and evaluates to TRUE when both of the conditions are TRUE. For more information, see [AND &#40;SQL Server PDW&#41;](../sqlpdw/and-sql-server-pdw.md).  
  
OR  
Combines two conditions and evaluates to TRUE when either condition is TRUE. For more information, see [OR &#40;SQL Server PDW&#41;](../sqlpdw/or-sql-server-pdw.md).  
  
< predicate >  
An expression that returns TRUE, FALSE, or UNKNOWN.  
  
*expression*  
A column name, a constant, a function, a variable, a scalar subquery, or any combination of column names, constants, and functions connected by an operator(s) or a subquery. The expression can also contain the CASE expression.  
  
=  
The operator used to test the equality between two expressions. For more information, see [= &#40;Equals&#41; &#40;SQL Server PDW&#41;](../sqlpdw/equals-sql-server-pdw.md).  
  
<>  
The operator used to test the condition of two expressions not being equal to each other. For more information, see [&#60;&#62; &#40;Not Equal To&#41; &#40;SQL Server PDW&#41;](../sqlpdw/not-equal-to-sql-server-pdw.md).  
  
!=  
The operator used to test the condition of two expressions not being equal to each other. For more information, see [!= &#40;Not Equal To&#41; &#40;SQL Server PDW&#41;](../sqlpdw/not-equal-to-sql-server-pdw.md).  
  
>  
The operator used to test the condition of one expression being greater than the other. For more information, see [&#62; &#40;Greater Than&#41; &#40;SQL Server PDW&#41;](../sqlpdw/greater-than-sql-server-pdw.md).  
  
>=  
The operator used to test the condition of one expression being greater than or equal to the other expression. For more information, see [&#62;= &#40;Greater Than or Equal To&#41; &#40;SQL Server PDW&#41;](../sqlpdw/greater-than-or-equal-to-sql-server-pdw.md).  
  
<  
The operator used to test the condition of one expression being less than the other. For more information, see [&#60; &#40;Less Than&#41; &#40;SQL Server PDW&#41;](../sqlpdw/less-than-sql-server-pdw.md).  
  
<=  
The operator used to test the condition of one expression being less than or equal to the other expression. For more information, see [&#60;= &#40;Less Than or Equal To&#41; &#40;SQL Server PDW&#41;](../sqlpdw/less-than-or-equal-to-sql-server-pdw.md).  
  
*string_expression*  
A string of characters and wildcard characters.  
  
[ NOT ] LIKE  
Indicates that the subsequent character string is to be used with pattern matching. For more information, see [LIKE &#40;SQL Server PDW&#41;](../sqlpdw/like-sql-server-pdw.md).  
  
[ NOT ] BETWEEN  
Specifies an inclusive range of values. Use AND to separate the starting and ending values. For more information, see [BETWEEN &#40;SQL Server PDW&#41;](../sqlpdw/between-sql-server-pdw.md).  
  
IS [ NOT ] NULL  
Specifies a search for null values, or for values that are not null, depending on the keywords used. An expression with a bitwise or arithmetic operator evaluates to NULL if any one of the operands is NULL.  For more information, see [IS &#91;NOT&#93; NULL &#40;SQL Server PDW&#41;](../sqlpdw/is-not-null-sql-server-pdw.md).  
  
[ NOT ] IN  
Specifies the search for an expression, based on whether the expression is included in or excluded from a list. The search expression can be a constant or a column name. The list can be a set of constants or, more typically, a subquery. Enclose the list of values in parentheses. For more information, see [IN &#40;SQL Server PDW&#41;](../sqlpdw/in-sql-server-pdw.md).  
  
*subquery*  
Can be considered a restricted SELECT statement and is similar to <query_expression> in the SELECT statement. The ORDER BY clause, the COMPUTE clause, and the INTO keyword are not allowed. For more information, see [SELECT &#40;SQL Server PDW&#41;](../sqlpdw/select-sql-server-pdw.md).  
  
EXISTS  
Used with a subquery to test for the existence of rows returned by the subquery. For more information, see [EXISTS &#40;SQL Server PDW&#41;](../sqlpdw/exists-sql-server-pdw.md).  
  
## General Remarks  
The order of precedence for the logical operators from highest to lowest is:  NOT, AND, OR. Use parantheses to override this precedence in a search condition.  
  
The order of evaluation of logical operators can vary, depending on choices made by the query optimizer. For more information, see [NOT &#40;SQL Server PDW&#41;](../sqlpdw/not-sql-server-pdw.md), [AND &#40;SQL Server PDW&#41;](../sqlpdw/and-sql-server-pdw.md), and [OR &#40;SQL Server PDW&#41;](../sqlpdw/or-sql-server-pdw.md).  
  
## Remarks  
Aggregate functions are not supported in [WHERE](../sqlpdw/where-sql-server-pdw.md) or [HAVING](../sqlpdw/having-sql-server-pdw.md) clauses.  
  
## Examples  
  
### A. Using WHERE with LIKE  
The following example searches for the rows in which the `LastName` column has the characters `and`.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT EmployeeKey, LastName  
FROM DimEmployee  
WHERE LastName LIKE '%and%';  
```  
  
### B. Using WHERE and LIKE syntax with Unicode data  
The following example uses the `WHERE` clause to perform a Unicode search on the `LastName` column.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT EmployeeKey, LastName  
FROM DimEmployee  
WHERE LastName LIKE N'%and%';  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[CASE &#40;SQL Server PDW&#41;](../sqlpdw/case-sql-server-pdw.md)  
[DELETE &#40;SQL Server PDW&#41;](../sqlpdw/delete-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../sqlpdw/expressions-sql-server-pdw.md)  
[FROM &#40;SQL Server PDW&#41;](../sqlpdw/from-sql-server-pdw.md)  
[Operators &#40;SQL Server PDW&#41;](../sqlpdw/operators-sql-server-pdw.md)  
[UPDATE &#40;SQL Server PDW&#41;](../sqlpdw/update-sql-server-pdw.md)  
  
