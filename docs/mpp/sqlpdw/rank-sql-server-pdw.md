---
title: "RANK (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 678a59ef-a717-42bd-87df-a3994919e5d2
caps.latest.revision: 20
author: BarbKess
---
# RANK (SQL Server PDW)
Returns the rank of each row that is contained in each partition of a result set in SQL Server PDW. The rank of a row is one plus the number of ranks that come before the row in question. Use this analytic function to assign ranking values to a set of rows according to some criterion such as an increasing numeric value.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
RANK () OVER ( [ < partition_by_clause > ] < order_by_clause > )  
```  
  
## Arguments  
< *partition_by_clause* >  
Divides the result set produced by the FROM clause into partitions to which the RANK function is applied. For the PARTITION BY syntax, see [OVER Clause &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/over-clause-sql-server-pdw.md).  
  
< *order_by_clause* >  
Determines the order in which the RANK values are applied to the rows in a partition. For more information, see [ORDER BY &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/order-by-sql-server-pdw.md). An integer cannot represent a column when *order_by_clause* is used.  
  
## Return Types  
**bigint**  
  
## Remarks  
If two or more rows tie for a rank, each tied rows receives the same rank. For example, if the two top salespeople have the same year-to-date sales value, they are both ranked one. The salesperson with the next highest year-to-date sales value is ranked number three, because there are two rows that are ranked higher. Therefore, the RANK function does not always return consecutive integers. This behavior differs from the DENSE_RANK function. For more information, see [DENSE_RANK &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/dense-rank-sql-server-pdw.md).  
  
The sort order that is used for the whole query determines the order in which the rows appear in a result set.  
  
## Examples  
The following example ranks the sales representatives in each sales territory according to their total sales. The rowset is partitioned by `SalesTerritoryGroup` and sorted by `SalesAmountQuota`.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT LastName, SUM(SalesAmountQuota) AS TotalSales, SalesTerritoryRegion,  
    RANK() OVER (PARTITION BY SalesTerritoryRegion ORDER BY SUM(SalesAmountQuota) DESC ) AS RankResult  
FROM dbo.DimEmployee AS e  
INNER JOIN dbo.FactSalesQuota AS sq ON e.EmployeeKey = sq.EmployeeKey  
INNER JOIN dbo.DimSalesTerritory AS st ON e.SalesTerritoryKey = st.SalesTerritoryKey  
WHERE SalesPersonFlag = 1 AND SalesTerritoryRegion != N'NA'  
GROUP BY LastName, SalesTerritoryRegion;  
```  
  
Here is the result set.  
  
<pre>LastName          TotalSales     SalesTerritoryGroup  RankResult  
----------------  -------------  -------------------  --------  
Tsoflias          1687000.0000   Australia            1  
Saraiva           7098000.0000   Canada               1  
Vargas            4365000.0000   Canada               2  
Carson            12198000.0000  Central              1  
Varkey Chudukatil 5557000.0000   France               1  
Valdez            2287000.0000   Germany              1  
Blythe            11162000.0000  Northeast            1  
Campbell          4025000.0000   Northwest            1  
Ansman-Wolfe      3551000.0000   Northwest            2  
Mensa-Annan       2753000.0000   Northwest            3  
Reiter            8541000.0000   Southeast            1  
Mitchell          11786000.0000  Southwest            1  
Ito               7804000.0000   Southwest            2  
Pak               10514000.0000  United Kingdom       1</pre>  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/functions-sql-server-pdw.md)  
[DENSE_RANK &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/dense-rank-sql-server-pdw.md)  
  
