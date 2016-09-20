---
title: "MAX (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: a38e56a2-c291-42c6-9498-578d861594ab
caps.latest.revision: 27
author: BarbKess
---
# MAX (SQL Server PDW)
Returns the maximum value of an expression in SQL Server PDW. Use this function in the SELECT list or HAVING clause of a SELECT statement to return the single highest value for the specified expression in a table or partition.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
-- Aggregation Function Syntax  
MAX ( [ ALL | DISTINCT ] expression )  
```  
  
```  
-- Aggregation Function Syntax   
MAX ( expression ) OVER ( [ <partition_by_clause> ] [ <order_by_clause> ] )  
```  
  
## Arguments  
**ALL**  
Applies the aggregate function to all values. ALL is the default.  
  
DISTINCT  
Considers only each unique value. DISTINCT is not meaningful with MAX and is available for ISO compatibility only.  
  
*expression*  
A constant, column name, or function, and any combination of arithmetic, bitwise, and string operators. MAX can be used with numeric, character, and date and time columns, except for the **bit** data type. Aggregate functions, subqueries, and analytic functions are not permitted.  
  
For more information, see [Expressions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/expressions-sql-server-pdw.md).  
  
OVER  
Determines the partitioning and ordering of the rowset before the analytic function is applied.  
  
< *partition_by_clause* >  
Divides the result set produced by the FROM clause into partitions to which the MAX function is applied. For the PARTITION BY syntax, see [OVER Clause &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/over-clause-sql-server-pdw.md).  
  
< *order_by_clause* >  
Determines the order in which the MAX calculation is applied to the rows in a partition. An **integer** cannot represent a column in *order_by_clause*. When the *order_by_clause* is absent, all rows within the same partition (as specified by the *partition_by_clause*) are assigned the same value for the MAX() OVER computation. When the *order_by_clause* is specified, the MAX() OVER computation gives you a cumulative maximum value within the partition. Within the a partition, rows with identical values for all elements of the *order_by_clause* return identical results for the MAX() OVER computation.  
  
## Return Types  
Returns the same value type as *expression*.  
  
## General Remarks  
MAX ignores any null values.  
  
For character columns, MAX finds the highest value in the collating sequence.  
  
## Limitations and Restrictions  
Unlike the MAX() aggregate function, the MAX() OVER analytic function does not support the use of the ALL or DISTINCT modifiers for the expression to compute the MAX against.  
  
## Examples  
  
### A. Using MAX  
The following example uses the MAX aggregate function to return the price of the most expensive (maximum) product in a specified set of sales orders.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT DISTINCT MAX(UnitPrice)   
FROM dbo.FactResellerSales   
WHERE SalesOrderNumber IN (N'SO43659', N'SO43660', N'SO43664');  
```  
  
Here is the result set.  
  
<pre>----------  
2039.9940</pre>  
  
### B. Using MAX with OVER  
The following examples use the MAX OVER() analytic function to return the price of the most expensive product in each sales order.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT DISTINCT MAX(UnitPrice) OVER(PARTITION BY SalesOrderNumber) AS MostExpensiveProduct,  
       SalesOrderNumber  
FROM dbo.FactResellerSales    
WHERE SalesOrderNumber IN (N'SO43659', N'SO43660', N'SO43664')  
ORDER BY SalesOrderNumber  
;  
```  
  
Here is the result set.  
  
<pre>MostExpensiveProduct SalesOrderNumber  
--------------------- ----------------  
2039.9940             SO43659  
879.7940             SO43660  
2039.9940             SO43664</pre>  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/expressions-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/functions-sql-server-pdw.md)  
[Data Types &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/data-types-sql-server-pdw.md)  
[MIN &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/min-sql-server-pdw.md)  
  
