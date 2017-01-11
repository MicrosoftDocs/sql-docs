---
title: "UNION (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 82c5689d-9fa1-4322-b97a-1f3a2c2a772b
caps.latest.revision: 20
author: BarbKess
---
# UNION (SQL Server PDW)
Combines the results of two or more queries into a single result set that includes all rows that belong to all queries in the union. The UNION operation is different from using joins that combine columns from two tables.  
  
Use the following basic rules to combine the result sets of queries by using UNION:  
  
-   The number and the order of the columns must be the same in all queries.  
  
-   The data types must be compatible.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
{ <query_specification> | ( <query_expression> ) }   
  UNION [ ALL ]   
  <query_specification | ( <query_expression> )   
 [ UNION [ ALL ] <query_specification> | ( <query_expression> )   
    [ ...n ] ]  
```  
  
## Arguments  
<query_specification> | ( <query_expression> )  
A query specification or query expression that returns data to be combined with the data from another query specification or query expression. The definitions of the columns that are part of a UNION operation do not have to be the same, but they must be compatible through implicit conversion.  
  
ALL  
Incorporates all rows into the results. This includes duplicates. If not specified, duplicate rows are removed.  
  
## General Remarks  
When data types differ, the resulting data type is determined based on the rules for data type precedence.  
  
When the data types are the same but differ in precision, scale, or length, the result is determined based on the same rules for combining expressions.  
  
When two expressions of the same data type but different lengths are compared by using UNION, the resulting length is the length of the longer expression.  
  
## Examples  
  
### A. Using a simple UNION  
In the following example, the result set includes the contents of the `CustomerKey` columns of both the `FactInternetSales` and `DimCustomer` tables. Since the ALL keyword is not used, duplicates are excluded from the results.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT CustomerKey   
FROM FactInternetSales    
UNION   
SELECT CustomerKey   
FROM DimCustomer   
ORDER BY CustomerKey;  
```  
  
### B. Using UNION of two SELECT statements with ORDER BY  
When any SELECT statement in a UNION statement includes an ORDER BY clause, that clause should be placed after all SELECT statements. The following example shows the incorrect and correct use of `UNION` in two `SELECT` statements in which a column is ordered with ORDER BY.  
  
```  
USE AdventureWorksPDW2012;  
  
-- INCORRECT  
SELECT CustomerKey   
FROM FactInternetSales    
ORDER BY CustomerKey  
UNION   
SELECT CustomerKey   
FROM DimCustomer  
ORDER BY CustomerKey;  
  
-- CORRECT   
USE AdventureWorksPDW2012;  
  
SELECT CustomerKey   
FROM FactInternetSales    
UNION   
SELECT CustomerKey   
FROM DimCustomer   
ORDER BY CustomerKey;  
```  
  
### C. Using UNION of two SELECT statements with WHERE and ORDER BY  
The following example shows the incorrect and correct use of `UNION` in two `SELECT` statements where WHERE and ORDER BY are needed.  
  
```  
USE AdventureWorksPDW2012;  
  
-- INCORRECT   
SELECT CustomerKey   
FROM FactInternetSales   
WHERE CustomerKey >= 11000  
ORDER BY CustomerKey   
UNION   
SELECT CustomerKey   
FROM DimCustomer   
ORDER BY CustomerKey;  
  
-- CORRECT  
USE AdventureWorksPDW2012;  
  
SELECT CustomerKey   
FROM FactInternetSales   
WHERE CustomerKey >= 11000  
UNION   
SELECT CustomerKey   
FROM DimCustomer   
ORDER BY CustomerKey;  
```  
  
### D. Using UNION of three SELECT statements to show effects of ALL and parentheses  
The following examples use `UNION` to combine the results of **the same table** in order to demonstrate the effects of ALL and parentheses when using `UNION`.  
  
The first example uses `UNION ALL` to show duplicated records and returns each row in the source table three times. The second example uses `UNION` without `ALL` to eliminate the duplicate rows from the combined results of the three `SELECT` statements and returns only the unduplicated rows from the source table.  
  
The third example uses `ALL` with the first `UNION` and parentheses enclosing the second `UNION` that is not using `ALL`. The second `UNION` is processed first because it is in parentheses. It returns only the unduplicated rows from the table because the `ALL` option is not used and duplicates are removed. These rows are combined with the results of the first `SELECT` by using the `UNION ALL` keywords. This does not remove the duplicates between the two sets.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT CustomerKey, FirstName, LastName  
FROM DimCustomer  
UNION ALL   
SELECT CustomerKey, FirstName, LastName  
FROM DimCustomer  
UNION ALL   
SELECT CustomerKey, FirstName, LastName  
FROM DimCustomer;  
  
SELECT CustomerKey, FirstName, LastName  
FROM DimCustomer  
UNION   
SELECT CustomerKey, FirstName, LastName  
FROM DimCustomer  
UNION   
SELECT CustomerKey, FirstName, LastName  
FROM DimCustomer;  
  
SELECT CustomerKey, FirstName, LastName  
FROM DimCustomer  
UNION ALL  
(  
SELECT CustomerKey, FirstName, LastName  
FROM DimCustomer  
UNION   
SELECT CustomerKey, FirstName, LastName  
FROM DimCustomer  
);  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[SELECT &#40;SQL Server PDW&#41;](../sqlpdw/select-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../sqlpdw/expressions-sql-server-pdw.md)  
[EXCEPT and INTERSECT &#40;SQL Server PDW&#41;](../sqlpdw/except-and-intersect-sql-server-pdw.md)  
  
