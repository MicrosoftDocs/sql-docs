---
title: "IN (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: b3cba209-ed39-405f-93cb-99c777ce70fa
caps.latest.revision: 18
author: BarbKess
---
# IN (SQL Server PDW)
Determines whether a specified value matches any value in a subquery or a list.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
test_expression [ NOT ] IN   
    ( subquery | expression [ ,...n ]  
    )  
```  
  
## Arguments  
*test_expression*  
Any valid expression.  
  
*subquery*  
A subquery that has a result set of one column. This column must have the same data type as *test_expression*.  
  
*expression* [ **,**... *n* ]  
A list of expressions to test for a match. All expressions must be of the same type as *test_expression*.  
  
## Result Types  
**Boolean**  
  
## Result Value  
If the value of *test_expression* is equal to any value returned by *subquery* or is equal to any *expression* from the comma-separated list, the result value is TRUE; otherwise, the result value is FALSE.  
  
Using NOT IN negates the *subquery* value or *expression*.  
  
> [!CAUTION]  
> Any null values returned by *subquery* or *expression* that are compared to *test_expression* using IN or NOT IN return UNKNOWN. Using null values together with IN or NOT IN can produce unexpected results.  
  
## Examples  
  
### A. Using IN and NOT IN  
The following example finds all entries in the `FactInternetSales` table that match `SalesReasonKey` values in the `DimSalesReason` table.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT * FROM FactInternetSalesReason   
WHERE SalesReasonKey   
IN (SELECT SalesReasonKey FROM DimSalesReason);  
```  
  
The following example finds all entries in the `FactInternetSalesReason` table that do not match `SalesReasonKey` values in the `DimSalesReason` table.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT * FROM FactInternetSalesReason   
WHERE SalesReasonKey   
NOT IN (SELECT SalesReasonKey FROM DimSalesReason);  
```  
  
### B. Using IN with an expression list  
The following example finds all IDs for the salespeople in the `DimEmployee` table for employees who have a first name that is either `Mike` or `Michael`.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT FirstName, LastName  
FROM DimEmployee  
WHERE FirstName IN ('Mike', 'Michael');  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../sqlpdw/expressions-sql-server-pdw.md)  
[CASE &#40;SQL Server PDW&#41;](../sqlpdw/case-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[Operators &#40;SQL Server PDW&#41;](../sqlpdw/operators-sql-server-pdw.md)  
[SELECT &#40;SQL Server PDW&#41;](../sqlpdw/select-sql-server-pdw.md)  
[WHERE &#40;SQL Server PDW&#41;](../sqlpdw/where-sql-server-pdw.md)  
  
