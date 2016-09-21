---
title: "STDEV (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 1591465d-99e5-44b0-9c15-be4b1aaaccff
caps.latest.revision: 22
author: BarbKess
---
# STDEV (SQL Server PDW)
Returns the statistical standard deviation of all values in the specified expression in a partition or table in SQL Server PDW. Use this function in the select list or HAVING clause of a SELECT statement.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
-- Aggregate Function Syntax   
STDEV ( [ ALL | DISTINCT ] expression )  
```  
  
```  
-- Analytic Function Syntax   
STDEV (expression) OVER ( [ partition_by_clause ] order_by_clause)  
```  
  
## Arguments  
**ALL**  
Applies the aggregate function to all values. ALL is the default.  
  
DISTINCT  
Specifies that each unique value is considered.  
  
*expression*  
An expression of the exact numeric or approximate **numeric** data type category, except for the **bit** data type. Aggregate functions, analytic functions, and subqueries are not permitted.  
  
*partition_by_clause*  
Divides the result set produced by the [FROM &#40;SQL Server PDW&#41;](../sqlpdw/from-sql-server-pdw.md) clause into partitions to which the STDEV function is applied. For the PARTITION BY syntax, see [OVER Clause &#40;SQL Server PDW&#41;](../sqlpdw/over-clause-sql-server-pdw.md).  
  
*order_by_clause*  
Determines the order in which the STDEV values are applied to the rows in a partition. An integer cannot represent a column in *order_by_clause*. When the *order_by_clause* is absent, all rows in the same partition are assigned the same value for the STDEV() OVER computation. When the *order_by_clause* is present, the STDEV() OVER computation gives you a “cumulative stdev” in the partition. Within a partition, rows with identical values for all elements of the *order_by_clause* will return identical results for the STDEV() OVER computation. For more information, see [ORDER BY &#40;SQL Server PDW&#41;](../sqlpdw/order-by-sql-server-pdw.md).  
  
## Return Types  
**float**  
  
## General Remarks  
If STDEV is used on all items in a SELECT statement, each value in the result set is included in the calculation. STDEV can be used with numeric columns only. Null values are ignored.  
  
## Limitations and Restrictions  
The STDEV() OVER analytic function does not support the use of the ALL or DISTINCT modifiers.  
  
## Examples  
  
### Using STDEV  
The following example returns the standard deviation of the sales quota values in the table `dbo.FactSalesQuota`. The first column contains the standard deviation of all distinct values and the second column contains the standard deviation of all values including any duplicates values.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT STDEV(DISTINCT SalesAmountQuota)AS Distinct_Values, STDEV(SalesAmountQuota) AS All_Values  
FROM dbo.FactSalesQuota;  
```  
  
Here is the result set.  
  
<pre>Distinct_Values   All_Values  
----------------  ----------------  
398974.27         398450.57</pre>  
  
### B. Using STDEV with OVER  
The following example returns the standard deviation of the sales quota values for each quarter in a calendar year. Notice that the ORDER BY in the OVER clause orders the STDEV and the ORDER BY of the SELECT statement orders the result set.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT CalendarYear AS Year, CalendarQuarter AS Quarter, SalesAmountQuota AS SalesQuota,  
       STDEV(SalesAmountQuota) OVER (ORDER BY CalendarYear, CalendarQuarter) AS StdDeviation  
FROM dbo.FactSalesQuota  
WHERE EmployeeKey = 272 AND CalendarYear = 2002  
ORDER BY CalendarQuarter;  
```  
  
Here is the result set.  
  
<pre>Year  Quarter  SalesQuota              StdDeviation  
----  -------  ----------------------  -------------------  
2002  1         91000.0000             null  
2002  2        140000.0000             34648.23  
2002  3         70000.0000             35921.21  
2002  4        154000.0000             39752.36</pre>  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../sqlpdw/expressions-sql-server-pdw.md)  
[STDEVP &#40;SQL Server PDW&#41;](../sqlpdw/stdevp-sql-server-pdw.md)  
  
