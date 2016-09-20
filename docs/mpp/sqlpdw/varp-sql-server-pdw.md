---
title: "VARP (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 54e9267c-d85b-475b-8d65-0ba46bc965f9
caps.latest.revision: 16
author: BarbKess
---
# VARP (SQL Server PDW)
Returns the statistical variance for the population of all values within the specified expression in SQL Server PDW.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
-- Aggregate Function Syntax   
VARP ( [ ALL | DISTINCT ] expression )  
```  
  
```  
-- Analytic Function Syntax  
VARP (expression) OVER ( [ partition_by_clause ] order_by_clause)  
```  
  
## Arguments  
**ALL**  
Applies the aggregate function to all values. ALL is the default.  
  
DISTINCT  
Specifies that each unique value is considered.  
  
*expression*  
An expression of the exact numeric or approximate numeric data type category, except for the **bit** data type. Aggregate functions, analytic functions, and subqueries are not permitted.  
  
*partition_by_clause*  
Divides the result set produced by the [FROM &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/from-sql-server-pdw.md) clause into partitions to which the VARP function is applied. For the PARTITION BY syntax, see [OVER Clause &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/over-clause-sql-server-pdw.md).  
  
*order_by_clause*  
Determines the order in which the VARP values are applied to the rows in a partition. An integer cannot represent a column in *order_by_clause*. When the *order_by_clause* is absent, all rows in the same partition are assigned the same value for the VARP() OVER computation. When the *order_by_clause* is present, the VARP() OVER computation gives you a “cumulative varp” in the partition. Within a partition, rows with identical values for all elements of the *order_by_clause* will return identical results for the VARP() OVER computation. For more information, see [ORDER BY &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/order-by-sql-server-pdw.md).  
  
## Return Types  
**float**  
  
## General Remarks  
If VARP is used on all items in a SELECT statement, each value in the result set is included in the calculation. VARP can be used with numeric columns only. Null values are ignored.  
  
## Limitations and Restrictions  
The VARP() OVER analytic function does not support the use of the ALL or DISTINCT modifiers.  
  
## Examples  
  
### Using VARP  
The following example returns the `VARP`of the sales quota values in the table `dbo.FactSalesQuota`. The first column contains the variance of all distinct values and the second column contains the variance of all values including any duplicates values.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT VARP(DISTINCT SalesAmountQuota)AS Distinct_Values, VARP(SalesAmountQuota) AS All_Values  
FROM dbo.FactSalesQuota;  
```  
  
Here is the result set.  
  
<pre>Distinct_Values   All_Values  
----------------  ----------------  
158146830494.18   157788848582.94</pre>  
  
### B. Using VARP with OVER  
The following example returns the `VARP` of the sales quota values for each quarter in a calendar year. Notice that the ORDER BY in the OVER clause orders the statistical variance and the ORDER BY of the SELECT statement orders the result set.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT CalendarYear AS Year, CalendarQuarter AS Quarter, SalesAmountQuota AS SalesQuota,  
       VARP(SalesAmountQuota) OVER (ORDER BY CalendarYear, CalendarQuarter) AS Variance  
FROM dbo.FactSalesQuota  
WHERE EmployeeKey = 272 AND CalendarYear = 2002  
ORDER BY CalendarQuarter;  
```  
  
Here is the result set.  
  
<pre>Year  Quarter  SalesQuota              Variance  
----  -------  ----------------------  -------------------  
2002  1         91000.0000             0.00  
2002  2        140000.0000             600250000.00  
2002  3         70000.0000             860222222.22  
2002  4        154000.0000             1185187500.00</pre>  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/functions-sql-server-pdw.md)  
  
