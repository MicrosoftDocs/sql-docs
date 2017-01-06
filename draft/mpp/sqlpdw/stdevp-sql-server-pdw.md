---
title: "STDEVP (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 6b85f41c-2e71-4b4f-aedb-1eb0f3b52056
caps.latest.revision: 20
author: BarbKess
---
# STDEVP (SQL Server PDW)
Returns the statistical standard deviation for the population for all values within the specified expression in a table or partition in SQL Server PDW. Use this function in the select list or HAVING clause of a SELECT statement.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
-- Aggregate Function Syntax   
STDEVP ( [ ALL | DISTINCT ] expression )  
```  
  
```  
-- Analytic Function Syntax   
STDEVP (expression) OVER ( [ partition_by_clause ] order_by_clause)  
```  
  
## Arguments  
**ALL**  
Applies the aggregate function to all values. ALL is the default.  
  
DISTINCT  
Specifies that each unique value is considered.  
  
*expression*  
An expression of the exact numeric or approximate numeric data type category, except for the **bit** data type. Aggregate functions, analytic functions, and subqueries are not permitted.  
  
*partition_by_clause*  
Divides the result set produced by the [FROM &#40;SQL Server PDW&#41;](../sqlpdw/from-sql-server-pdw.md) clause into partitions to which the STDEVP function is applied. For the PARTITION BY syntax, see [OVER Clause &#40;SQL Server PDW&#41;](../sqlpdw/over-clause-sql-server-pdw.md).  
  
*order_by_clause*  
Determines the order in which the STDEVP values are applied to the rows in a partition. An integer cannot represent a column in *order_by_clause.* When the *order_by_clause* is absent, all rows in the same partition are assigned the same value for the STDEVP() OVER computation. When the *order_by_clause* is present, the STDEVP() OVER computation gives you a “cumulative stdevp” in the partition. Within a partition, rows with identical values for all elements of the *order_by_clause* will return identical results for the STDEVP() OVER computation. For more information, see [ORDER BY &#40;SQL Server PDW&#41;](../sqlpdw/order-by-sql-server-pdw.md).  
  
## Return Types  
**float**  
  
## General Remarks  
If STDEVP is used on all items in a SELECT statement, each value in the result set is included in the calculation. STDEVP can be used with numeric columns only. Null values are ignored.  
  
## Limitations and Restrictions  
The STDEVP() OVER analytic function does not support the use of the ALL or DISTINCT modifiers.  
  
## Examples  
  
### Using STDEVP  
The following example returns the `STDEVP` of the sales quota values in the table `dbo.FactSalesQuota`. The first column contains the standard deviation of all distinct values and the second column contains the standard deviation of all values including any duplicates values.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT STDEVP(DISTINCT SalesAmountQuota)AS Distinct_Values, STDEVP(SalesAmountQuota) AS All_Values  
FROM dbo.FactSalesQuota;SELECT STDEVP(DISTINCT Quantity)AS Distinct_Values, STDEVP(Quantity) AS All_Values  
FROM ProductInventory;  
```  
  
Here is the result set.  
  
<pre>Distinct_Values   All_Values  
----------------  ----------------  
397676.79         397226.44</pre>  
  
### B. Using STDEVP with OVER  
The following example returns the `STDEVP` of the sales quota values for each quarter in a calendar year. Notice that the `ORDER BY` in the `OVER` clause orders the `STDEVP` and the `ORDER BY` of the `SELECT` statement orders the result set.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT CalendarYear AS Year, CalendarQuarter AS Quarter, SalesAmountQuota AS SalesQuota,  
       STDEVP(SalesAmountQuota) OVER (ORDER BY CalendarYear, CalendarQuarter) AS StdDeviation  
FROM dbo.FactSalesQuota  
WHERE EmployeeKey = 272 AND CalendarYear = 2002  
ORDER BY CalendarQuarter;  
```  
  
Here is the result set.  
  
<pre>Year  Quarter  SalesQuota              StdDeviation  
----  -------  ----------------------  -------------------  
2002  1         91000.0000             0.00  
2002  2        140000.0000             24500.00  
2002  3         70000.0000             29329.55  
2002  4        154000.0000             34426.55</pre>  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../sqlpdw/expressions-sql-server-pdw.md)  
[STDEV &#40;SQL Server PDW&#41;](../sqlpdw/stdev-sql-server-pdw.md)  
  
