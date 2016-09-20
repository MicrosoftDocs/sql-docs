---
title: "MIN (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: b4b0bb38-80b0-48de-bad5-9678fbbcf59f
caps.latest.revision: 26
author: BarbKess
---
# MIN (SQL Server PDW)
Returns the minimum value in the expression in SQL Server PDW. Use this function in the select list or HAVING clause of a SELECT statement to return the single lowest value for the specified expression in a table or partition.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
-- Aggregation Function Syntax  
MIN ( [ ALL | DISTINCT ] expression )  
```  
  
```  
-- Aggregation Function Syntax   
MIN ( expression ) OVER ( [ <partition_by_clause> ] [ <order_by_clause> ] )  
```  
  
## Arguments  
**ALL**  
Applies the aggregate function to all values. ALL is the default.  
  
DISTINCT  
Considers only each unique value. DISTINCT is not meaningful with MIN and is available for ISO compatibility only.  
  
*expression*  
A constant, column name, or function, and any combination of arithmetic, bitwise, and string operators. MIN can be used with numeric, character, and date and time columns, except for the **bit** data type. Aggregate functions, subqueries, and analytic functions are not permitted.  
  
For more information, see [Expressions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/expressions-sql-server-pdw.md).  
  
OVER  
Determines the partitioning and ordering of the rowset before the analytic function is applied.  
  
< *partition_by_clause* >  
Divides the result set produced by the FROM clause into partitions to which the MIN function is applied. For the PARTITION BY syntax, see [OVER Clause &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/over-clause-sql-server-pdw.md).  
  
< *order_by_clause* >  
Determines the order in which the MIN calculation is applied to the rows in a partition. An **integer** cannot represent a column in *order_by_clause*. When the *order_by_clause* is absent, all rows in the same partition (as specified by the *partition_by_clause*) are assigned the same value for the MIN() OVER computation. When the *order_by_clause* is specified, the MIN() OVER computation gives you a cumulative minimum value in the partition. Within the a partition, rows with identical values for all elements of the *order_by_clause* return identical results for the MIN() OVER computation.  
  
## Return Types  
Returns the same value type as *expression*.  
  
## General Remarks  
MIN ignores any null values.  
  
For character columns, MIN finds the lowest value in the collating sequence.  
  
## Limitations and Restrictions  
Unlike the MIN() aggregate function, the MIN() OVER analytic function does not support the use of the ALL or DISTINCT modifiers for the expression to compute the MIN against.  
  
## Examples  
  
### A. Using MIN  
The following example uses the MIN aggregate function to return the price of the least expensive (minimum) product in a specified set of sales orders.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT DISTINCT MIN(UnitPrice)  
FROM dbo.FactResellerSales   
WHERE SalesOrderNumber IN (N'SO43659', N'SO43660', N'SO43664');  
```  
  
Here is the result set.  
  
<pre>------  
5.1865</pre>  
  
### B. Using MIN with OVER  
The following examples use the MIN OVER() analytic function to return the price of the least expensive product in each sales order. The result set is partitioned by the `SalesOrderID` column.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT DISTINCT MIN(UnitPrice) OVER(PARTITION BY SalesOrderNumber) AS LeastExpensiveProduct,  
       SalesOrderNumber  
FROM dbo.FactResellerSales    
WHERE SalesOrderNumber IN (N'SO43659', N'SO43660', N'SO43664')  
ORDER BY SalesOrderNumber;  
```  
  
Here is the result set.  
  
<pre>LeastExpensiveProduct SalesOrderID  
--------------------- ----------  
5.1865                SO43659  
419.4589              SO43660  
28.8404               SO43664</pre>  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/expressions-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/functions-sql-server-pdw.md)  
[Data Types &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/data-types-sql-server-pdw.md)  
[MIN &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/min-sql-server-pdw.md)  
  
