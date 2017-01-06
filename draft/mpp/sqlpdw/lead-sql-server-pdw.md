---
title: "LEAD (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: e7d12147-bac9-4a88-874b-1043477eca9b
caps.latest.revision: 19
author: BarbKess
---
# LEAD (SQL Server PDW)
Accesses data from a subsequent row of data in the same result set without the use of a self join in SQL Server PDW. Use this analytic function in a SELECT statement to compare values in the current row with values in a following row.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
LEAD (value_expression [,offset] [,default]) OVER ( [ <partition_by_clause> ] <order_by_clause> )  
```  
  
## Arguments  
*value_expression*  
An expression of any type that returns a single (scalar) value. Aggregate functions, analytic functions, and subqueries are not permitted.  
  
> [!NOTE]  
> *value_expression* can only reference columns in the PARTITION BY clause and cannot reference columns listed in the FROM clause.  
  
*offset*  
The number of rows forward from the current row from which to obtain a value. If not specified, the default is 1. *offset* can be a column, a CASE expression, or other expression that evaluates to an integer. *offset* cannot be a negative value or an analytic function.  
  
*default*  
The value to assign to the column when no LEAD value is available. For example, when the offset value is higher than the number of previous rows in the partition. If not specified, the default is NULL. *default* can be a column, CASE expression, or other expression, but it cannot be an analytic function.  
  
OVER  
Determines the partitioning and ordering of the rowset before the analytic function is applied.  
  
<*partition_by_clause*>  
Divides the result set produced by the FROM clause into partitions to which the LEAD function is applied. For example, `PARTITION BY FirstName`. *partition_by_clause* can include one or more columns, a CASE expression, or another expression. NULL values in the partitioned columns are ignored. For more information about *partition_by_clause*, see [OVER Clause &#40;SQL Server PDW&#41;](../sqlpdw/over-clause-sql-server-pdw.md).  
  
<*order_by_clause*>  
Specifies the return order of the data values within each partition. For more information, see [ORDER BY &#40;SQL Server PDW&#41;](../sqlpdw/order-by-sql-server-pdw.md). An **integer** cannot represent a column in *order_by_clause*.  
  
## Return Types  
The data type of the specified *value_expression*.  
  
## Examples  
The following example demonstrates the LEAD function. The query obtains the difference in sales quota values for a specified employee over subsequent calendar quarters. Notice that because there is no lead value available after the last row, the default of zero (0) is used.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT CalendarYear AS Year, CalendarQuarter AS Quarter, SalesAmountQuota AS SalesQuota,  
       LEAD(SalesAmountQuota,1,0) OVER (ORDER BY CalendarYear, CalendarQuarter) AS NextQuota,  
   SalesAmountQuota - LEAD(Sale sAmountQuota,1,0) OVER (ORDER BY CalendarYear, CalendarQuarter) AS Diff  
FROM dbo.FactSalesQuota  
WHERE EmployeeKey = 272 AND CalendarYear IN (2001,2002)  
ORDER BY CalendarYear, CalendarQuarter;  
```  
  
Here is the result set.  
  
<pre>Year Quarter  SalesQuota  NextQuota  Diff  
---- -------  ----------  ---------  -------------  
2001 3        28000.0000   7000.0000   21000.0000  
2001 4         7000.0000  91000.0000  -84000.0000  
2001 1        91000.0000 140000.0000  -49000.0000  
2002 2       140000.0000   7000.0000    7000.0000  
2002 3         7000.0000 154000.0000   84000.0000  
2002 4       154000.0000      0.0000  154000.0000</pre>  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[LAG &#40;SQL Server PDW&#41;](../sqlpdw/lag-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
  
