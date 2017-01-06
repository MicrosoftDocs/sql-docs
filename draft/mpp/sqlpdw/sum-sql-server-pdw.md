---
title: "SUM (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 7c0f4d2a-2755-446f-a507-5fd1f83a3d80
caps.latest.revision: 21
author: BarbKess
---
# SUM (SQL Server PDW)
Returns the sum of all the values, or only the DISTINCT values, in the expression. SUM can be used with numeric columns only. Null values are ignored. May be followed by the [OVER Clause &#40;SQL Server PDW&#41;](../sqlpdw/over-clause-sql-server-pdw.md).  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
SUM ( [ ALL | DISTINCT ] expression )  
```  
  
## Arguments  
ALL  
Applies the aggregate function to all values. ALL is the default.  
  
DISTINCT  
Specifies that SUM return the sum of unique values.  
  
*expression*  
A constant or column, and any combination of arithmetic and string operators. *expression* is an expression of the exact numeric or approximate numeric data type category, except for the bit data type. Aggregate functions and subqueries are not permitted. For more information, see [Expressions &#40;SQL Server PDW&#41;](../sqlpdw/expressions-sql-server-pdw.md).  
  
## Return Types  
Returns the sum of all *expression* values in the most precise *expression* data type.  
  
|Expression result|Return type|  
|---------------------|---------------|  
|**tinyint**|**int**|  
|**smallint**|**int**|  
|**int**|**int**|  
|**bigint**|**bigint**|  
|**decimal**(p, s)|**decimal(38, s)**|  
|**float**|**float**|  
  
## Examples  
  
### A. A simple SUM example  
The following example returns the total number of each product sold in the year 2003.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT ProductKey, SUM(SalesAmount) AS TotalPerProduct  
FROM dbo.FactInternetSales  
WHERE OrderDateKey >= '20030101'   
      AND OrderDateKey < '20040101'  
GROUP BY ProductKey  
ORDER BY ProductKey;  
```  
  
Here is a partial result set.  
  
<pre>ProductKey  TotalPerProduct  
----------  ---------------  
214         31421.0200  
217         31176.0900  
222         29986.4300  
225          7956.1500</pre>  
  
### B. Calculating group totals with more than one column  
The following example calculates the sum of the `ListPrice` and `StandardCost` for each color listed in the `Product` table.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT Color, SUM(ListPrice)AS TotalList,   
       SUM(StandardCost) AS TotalCost  
FROM dbo.DimProduct  
GROUP BY Color  
ORDER BY Color;  
```  
  
The first part of the result set is shown below:  
  
<pre>Color       TotalList      TotalCost  
----------  -------------  --------------  
Black       101295.7191    57490.5378  
Blue         24082.9484    14772.0524  
Grey           125.0000       51.5625  
Multi          880.7468      526.4095  
NA            3162.3564     1360.6185</pre>  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Data Types &#40;SQL Server PDW&#41;](../sqlpdw/data-types-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../sqlpdw/expressions-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
  
