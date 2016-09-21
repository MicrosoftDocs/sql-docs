---
title: "AVG (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: dac99c28-84d5-4df7-b612-f7ded51c3a01
caps.latest.revision: 33
author: BarbKess
---
# AVG (SQL Server PDW)
Returns the average of the values in a group in SQL Server PDW. Use this function in the select list or HAVING clause of a SELECT statement.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
-- Aggregate Function Syntax   
AVG ( [ ALL | DISTINCT ] expression )  
```  
  
```  
-- Analytic Function Syntax  
AVG (expression) OVER ( [ <partition_by_clause> ] [ <order_by_clause> ] )  
```  
  
## Arguments  
**ALL**  
Applies the aggregate function to all values. ALL is the default.  
  
DISTINCT  
Averages only each unique instance of a value, regardless of how many times the value occurs.  
  
*expression*  
An expression of the exact numeric or approximate numeric data type category, except for the **bit** data type. Aggregate functions, analytic functions, and subqueries are not permitted.  
  
OVER  
Determines the partitioning and ordering of the rowset before the analytic function is applied.  
  
< *partition_by_clause* >  
Divides the result set produced by the FROM clause into partitions to which the AVG function is applied. For the PARTITION BY syntax, see [OVER Clause &#40;SQL Server PDW&#41;](../sqlpdw/over-clause-sql-server-pdw.md).  
  
< *order_by_clause* >  
Determines the order in which the AVG calculation is applied to the rows in a partition. An **integer** cannot represent a column in *order_by_clause*. When the *order_by_clause* is absent, all rows in the same partition (as specified by the *partition_by_clause*) are assigned the same value for the AVG() OVER computation. When the *order_by_clause* is present, the AVG() OVER computation gives you a cumulative average in the partition. Within the same partitions, rows with identical values for all elements of the *order_by_clause* return identical results for the AVG() OVER computation.  
  
## Return Types  
The return type is determined by the type of the evaluated result of *expression*.  
  
|Expression Result|Return Type|  
|---------------------|---------------|  
|**tinyint**|**int**|  
|**smallint**|**int**|  
|**int**|**int**|  
|**bigint**|**bigint**|  
|**decimal** category (p, s)|**decimal(38, s)** divided by **decimal(10, 0)**|  
|**float**|**float**|  
  
## Interoperability  
A [GROUP BY](../sqlpdw/group-by-sql-server-pdw.md) clause must be included in the query if selecting an aggregate value (such as AVG) and at least one column or value that is not an aggregate function.  
  
## Limitations and Restrictions  
The AVG() OVER analytic function does not support the use of the ALL or DISTINCT modifiers.  
  
AVG() computes the average of a set of values by dividing the sum of those values by the count of non-null values. If the sum exceeds the maximum value for the data type of the return value an error will be returned.  
  
## Examples  
  
### A. Using the SUM and AVG functions for calculations  
The following example calculates the average vacation hours and the sum of sick leave hours that the vice presidents of a company have used. Each of these aggregate functions produces a single summary value for all the retrieved rows.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT AVG(VacationHours)AS AverageVacationHours,   
SUM(SickLeaveHours) AS TotalSickLeaveHours  
FROM dbo.DimEmployee  
WHERE Title LIKE 'Vice President%';  
```  
  
Here is the result set.  
  
<pre>AverageVacationHours TotalSickLeaveHours  
-------------------- --------------------------  
25                   97</pre>  
  
### B. Using the SUM and AVG functions together with a GROUP BY clause  
The following example uses the `GROUP BY` clause to return a single aggregate value for each group, instead of for the whole table. The example produces summary values for each sales territory. The summary lists the average sales by the sales representatives in each territory and the sum of sales for each territory.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT SalesTerritoryKey AS Territory,  
    CONVERT(varchar(12), AVG(SalesAmount),1) AS AverageSales,  
CONVERT(varchar(15), SUM(SalesAmount),1) AS TotalSales  
FROM dbo.FactResellerSales  
WHERE SalesTerritoryKey BETWEEN 1 and 4  
GROUP BY SalesTerritoryKey;  
```  
  
Here is the result set.  
  
<pre>Territory   AverageSales TotalSales  
----------- ------------ ---------------  
1           1,579.66     12,435,076.00  
2           1,193.47      6,932,842.01  
3           1,360.29      7,906,008.18  
4           1,380.26     18,466,458.79</pre>  
  
### C. Using AVG with DISTINCT  
The following example returns the average list price in the `Product` table. The function averages only each unique instance of `ListPrice`.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT AVG(DISTINCT ListPrice) FROM dbo.DimProduct;  
```  
  
Here is the result set.  
  
<pre>----------------  
512.2459</pre>  
  
### D. Using AVG without DISTINCT  
The following example returns the average list price of all products in the `Product` table.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT AVG(ListPrice) FROM dbo.DimProduct;  
```  
  
Here is the result set.  
  
<pre>----------------  
747.6617</pre>  
  
### E. Using AVG with OVER  
The following examples show using the `OVER` clause with the AVG function to return the cumulative average price of products within each product line.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT DISTINCT AVG(ListPrice)OVER(PARTITION BY ProductLine)AS AvgPrice  
,ProductLine  
FROM dbo.DimProduct  
WHERE ListPrice IS NOT NULL   
      AND ProductLine IS NOT NULL  
GROUP BY ProductLine,ListPrice  
ORDER BY AvgPrice;  
```  
  
Here is the result set.  
  
<pre>AvgPrice      ProductLine  
------------  ---------------  
40.5016       S  
414.8843      T  
555.1098      R  
617.2562      M</pre>  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../sqlpdw/expressions-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[ORDER BY &#40;SQL Server PDW&#41;](../sqlpdw/order-by-sql-server-pdw.md)  
  
