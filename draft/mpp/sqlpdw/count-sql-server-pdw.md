---
title: "COUNT (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 25eebe4a-7602-4544-b7dc-8e5505893b52
caps.latest.revision: 28
author: BarbKess
---
# COUNT (SQL Server PDW)
Returns the number of items in a group in SQL Server PDW. Use this function in the select list or HAVING clause of a SELECT statement to aggregate the number of rows in a table or partition.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
-- Aggregation Function Syntax  
COUNT ( { [ [ ALL | DISTINCT ] expression ] | * } )  
```  
  
```  
-- Analytic Function Syntax  
COUNT ( { expression | * } ) OVER ( [ <partition_by_clause> ] )  
```  
  
## Arguments  
**ALL**  
Applies the aggregate function to all values. COUNT (ALL *expression*) evaluates *expression* for each row in a group and returns the number of non-null values. ALL is the default.  
  
DISTINCT  
Specifies that COUNT returns the number of unique non-null values. COUNT (DISTINCT *expression*) evaluates *expression* for each row in a group or partition and returns the number of unique, non-null values.  
  
*expression*  
Is an expression of any type. Aggregate functions and subqueries are not permitted.  
  
**\***  
Specifies that all rows (including null and duplicate values) should be counted to return the total number of rows in a table or partition. COUNT(**\***) takes no parameters and cannot be used with ALL or DISTINCT.  
  
OVER  
Determines the partitioning and ordering of the rowset before the analytic function is applied.  
  
< *partition_by_clause* >  
Divides the result set produced by the FROM clause into partitions to which the COUNT function is applied. For the PARTITION BY syntax, see [OVER Clause &#40;SQL Server PDW&#41;](../sqlpdw/over-clause-sql-server-pdw.md).  
  
## Return Types  
**int**  
  
## Limitation and Restrictions  
The COUNT() OVER analytic function does not support the use of the ALL or DISTINCT modifiers for the expression to compute the COUNT against.  
  
## Examples  
  
### A. Using COUNT and DISTINCT  
The following example lists the number of different titles that an employee who works at a specific company can hold.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT COUNT(DISTINCT Title)  
FROM dbo.DimEmployee;  
```  
  
Here is the result set.  
  
<pre>-----------  
67</pre>  
  
### B. Using COUNT(*)  
The following example returns the total number of rows in the `dbo.DimEmployee` table.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT COUNT(*)  
FROM dbo.DimEmployee;  
```  
  
Here is the result set.  
  
<pre>-------------  
296</pre>  
  
### C. Using COUNT(*) with other aggregates  
The following example combines `COUNT(*)` with other aggregate functions in the SELECT list. The query returns the number of sales representatives with a annual sales quota greater than $500,000 and the average sales quota.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT COUNT(EmployeeKey) AS TotalCount, AVG(SalesAmountQuota) AS [Average Sales Quota]  
FROM dbo.FactSalesQuota  
WHERE SalesAmountQuota > 500000 AND CalendarYear = 2001;  
```  
  
Here is the result set.  
  
<pre>TotalCount  Average Sales Quota  
----------  -------------------  
10          683800.0000</pre>  
  
### D. Using COUNT with HAVING  
The following example uses COUNT with the HAVING clause to return the departments in a company that have more than 15 employees.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT DepartmentName,   
       COUNT(EmployeeKey)AS EmployeesInDept  
FROM dbo.DimEmployee  
GROUP BY DepartmentName  
HAVING COUNT(EmployeeKey) > 15;  
```  
  
Here is the result set.  
  
<pre>DepartmentName  EmployeesInDept  
--------------  ---------------  
Sales           18  
Production      179</pre>  
  
### E. Using COUNT with OVER  
The following example uses COUNT with the OVER clause to return the number of products that are contained in each of the specified sales orders.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT DISTINCT COUNT(ProductKey) OVER(PARTITION BY SalesOrderNumber) AS ProductCount  
    ,SalesOrderNumber  
FROM dbo.FactInternetSales  
WHERE SalesOrderNumber IN (N'SO53115',N'SO55981');  
```  
  
Here is the result set.  
  
<pre>ProductCount   SalesOrderID  
------------   -----------------  
3              SO53115  
1              SO55981</pre>  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../sqlpdw/expressions-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[OVER Clause &#40;SQL Server PDW&#41;](../sqlpdw/over-clause-sql-server-pdw.md)  
[COUNT_BIG &#40;SQL Server PDW&#41;](../sqlpdw/count-big-sql-server-pdw.md)  
  
