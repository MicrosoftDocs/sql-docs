---
title: "DENSE_RANK (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 8b8c9151-91c5-45ca-aff3-3384bf87fc65
caps.latest.revision: 22
author: BarbKess
---
# DENSE_RANK (SQL Server PDW)
Returns the rank of rows that is contained in each partition of a result set, without any gaps in the ranking in SQL Server PDW. The rank of a row is one plus the number of distinct ranks that come before the row in question. Use this analytic function to assign a ranking value to a set of rows according to some criterion such as an increasing numeric value.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
DENSE_RANK () OVER ( [ [ partition_by_clause ] order_by_clause ] )  
```  
  
## Arguments  
OVER  
Determines the partitioning and ordering of the rowset before the analytic function is applied.  
  
*partition_by_clause*  
Divides the result set produced by the [FROM &#40;SQL Server PDW&#41;](../sqlpdw/from-sql-server-pdw.md) clause into partitions to which the DENSE_RANK function is applied. For the PARTITION BY syntax, see [OVER Clause &#40;SQL Server PDW&#41;](../sqlpdw/over-clause-sql-server-pdw.md).  
  
*order_by_clause*  
Determines the order in which the DENSE_RANK values are applied to the rows in a partition. An integer cannot represent a column in *order_by_clause.* For more information about *order_by_clause*, see [ORDER BY &#40;SQL Server PDW&#41;](../sqlpdw/order-by-sql-server-pdw.md).  
  
## Return Types  
**bigint**  
  
## General Remarks  
If two or more rows tie for a rank in the same partition, each tied rows receives the same rank. For example, if the two top salespeople have the same SalesYTD value, they are both ranked as number one. The salesperson with the next highest SalesYTD is ranked as number two. This is one more than the number of distinct rows that come before this row. Therefore, the numbers returned by the DENSE_RANK function do not have gaps and always have consecutive ranks. This behavior differs from the [RANK](../sqlpdw/rank-sql-server-pdw.md) function.  
  
The sort order used for the whole query determines the order in which the rows appear in a result. This implies that a row ranked number one does not have to be the first row in the partition.  
  
## Examples  
The following example ranks the sales representatives in each sales territory according to their total sales. The rowset is partitioned by `SalesTerritoryGroup` and sorted by `SalesAmountQuota`.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT LastName, SUM(SalesAmountQuota) AS TotalSales, SalesTerritoryGroup,  
    DENSE_RANK() OVER (PARTITION BY SalesTerritoryGroup ORDER BY SUM(SalesAmountQuota) DESC ) AS RankResult  
FROM dbo.DimEmployee AS e  
INNER JOIN dbo.FactSalesQuota AS sq ON e.EmployeeKey = sq.EmployeeKey  
INNER JOIN dbo.DimSalesTerritory AS st ON e.SalesTerritoryKey = st.SalesTerritoryKey  
WHERE SalesPersonFlag = 1 AND SalesTerritoryGroup != N'NA'  
GROUP BY LastName,SalesTerritoryGroup;  
```  
  
Here is the result set.  
  
<pre>LastName          TotalSales     SalesTerritoryGroup  RankResult  
----------------  -------------  -------------------  --------  
Pak               10514000.0000  Europe               1  
Varkey Chudukatil  5557000.0000  Europe               2  
Valdez             2287000.0000  Europe               3  
Carson            12198000.0000  North America        1  
Mitchell          11786000.0000  North America        2  
Blythe            11162000.0000  North America        3  
Reiter             8541000.0000  North America        4  
Ito                7804000.0000  North America        5  
Saraiva            7098000.0000  North America        6  
Vargas             4365000.0000  North America        7  
Campbell           4025000.0000  North America        8  
Ansman-Wolfe       3551000.0000  North America        9  
Mensa-Annan        2753000.0000  North America        10  
Tsoflias           1687000.0000  Pacific              1</pre>  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[RANK &#40;SQL Server PDW&#41;](../sqlpdw/rank-sql-server-pdw.md)  
[ROW_NUMBER &#40;SQL Server PDW&#41;](../sqlpdw/row-number-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
  
