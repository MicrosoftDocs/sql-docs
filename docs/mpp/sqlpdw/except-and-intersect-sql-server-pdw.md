---
title: "EXCEPT and INTERSECT (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: fb87c5a4-1ec3-40ea-b2be-e6a58e512b1b
caps.latest.revision: 7
author: BarbKess
---
# EXCEPT and INTERSECT (SQL Server PDW)
Returns distinct rows by comparing the results of two queries.  
  
EXCEPT returns distinct rows from the left input query that aren’t output by the right input query.  
  
INTERSECT returns distinct rows that are output by both the left and right input queries.  
  
The basic rules for combining the result sets of two queries that use EXCEPT or INTERSECT are the following:  
  
-   The number and the order of the columns must be the same in all queries.  
  
-   The data types must be compatible.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
{ <query_specification> | ( <query_expression> ) }   
{ EXCEPT | INTERSECT }  
{ <query_specification> | ( <query_expression> ) }  
```  
  
## Arguments  
<*query_specification*> | ( <*query_expression*> )  
Is a query specification or query expression that returns data to be compared with the data from another query specification or query expression. The definitions of the columns that are part of an EXCEPT or INTERSECT operation do not have to be the same, but they must be comparable through implicit conversion. When data types differ, the type that is used to perform the comparison and return results is determined based on the rules for [data type precedence](http://msdn.microsoft.com/en-us/library/ms190309.aspx).  
  
When the types are the same but differ in precision, scale, or length, the result is determined based on the same rules for combining expressions. For more information, see [Precision, Scale, and Length (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms190476.aspx).  
  
The query specification or expression cannot return **xml**, **text**, **ntext**, **image**, or nonbinary CLR user-defined type columns because these data types are not comparable.  
  
EXCEPT  
EXCEPT returns distinct rows from the left input query that aren’t output by the right input query.  
  
INTERSECT  
Returns any distinct values that are returned by both the query on the left and right sides of the INTERSECT operator.  
  
## Remarks  
When the data types of comparable columns that are returned by the queries to the left and right of the EXCEPT or INTERSECT operators are character data types with different collations, the required comparison is performed according to the rules of [collation precedence](http://msdn.microsoft.com/en-us/library/ms179886.aspx). If this conversion cannot be performed, the Database Engine returns an error.  
  
When comparing column values for determining DISTINCT rows, two NULL values are considered equal.  
  
The column names of the result set that are returned by EXCEPT or INTERSECT are the same names as those returned by the query on the left side of the operator.  
  
Column names or aliases in ORDER BY clauses must reference column names returned by the left-side query.  
  
The nullability of any column in the result set returned by EXCEPT or INTERSECT is the same as the nullability of the corresponding column that is returned by the query on the left side of the operator.  
  
If EXCEPT or INTERSECT is used together with other operators in an expression, it is evaluated in the context of the following precedence:  
  
1.  Expressions in parentheses  
  
2.  The INTERSECT operator  
  
3.  EXCEPT and UNION evaluated from left to right based on their position in the expression  
  
If EXCEPT or INTERSECT is used to compare more than two sets of queries, data type conversion is determined by comparing two queries at a time, and following the previously mentioned rules of expression evaluation.  
  
## Examples  
The following examples show how to use the `INTERSECT` and `EXCEPT` operators. The first query returns all values from the `FactInternetSales` table for comparison to the results with `INTERSECT` and `EXCEPT`.  
  
```  
USE AdventureWorksPDW2012;  
GO  
SELECT CustomerKey   
FROM FactInternetSales;  
--Result: 60398 Rows  
```  
  
The following query returns any distinct values that are returned by both the query on the left and right sides of the `INTERSECT` operator.  
  
```  
USE AdventureWorksPDW2012;  
GO  
SELECT CustomerKey   
FROM FactInternetSales    
INTERSECT   
SELECT CustomerKey   
FROM DimCustomer   
WHERE DimCustomer.Gender = 'F'  
ORDER BY CustomerKey;  
--Result: 9133 Rows (Sales to customers that are female.)  
```  
  
The following query returns any distinct values from the query to the left of the `EXCEPT` operator that are not also found on the right query.  
  
```  
USE AdventureWorksPDW2012;  
GO  
SELECT CustomerKey   
FROM FactInternetSales    
EXCEPT   
SELECT CustomerKey   
FROM DimCustomer   
WHERE DimCustomer.Gender = 'F'  
ORDER BY CustomerKey;  
--Result: 9351 Rows (Sales to customers that are not female.)  
```  
  
## See Also  
[UNION &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/union-sql-server-pdw.md)  
  
