---
title: "NTILE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 5ccb9289-b830-4901-b199-50a944390c70
caps.latest.revision: 18
author: BarbKess
---
# NTILE (SQL Server PDW)
Distributes the rows in an ordered partition into a specified number of groups in SQL Server PDW. The groups are numbered, starting at one. Use this analytic function to return the number of the group to which each row belongs.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
NTILE (integer_expression) OVER ( [ <partition_by_clause> ] < order_by_clause > )  
```  
  
## Arguments  
*integer_expression*  
Is a positive integer constant expression that specifies the number of groups into which each partition must be divided. *integer_expression* can be of type **int**, or **bigint**. *integer_expression* cannot contain column references or subqueries.  
  
OVER  
Determines the partitioning and ordering of the rowset before the analytic function is applied.  
  
<partition_by_clause>  
Divides the result set produced by the [FROM](../../mpp/sqlpdw/from-sql-server-pdw.md) clause into partitions to which the NTILE function is applied. For the PARTITION BY syntax, see [OVER Clause &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/over-clause-sql-server-pdw.md).  
  
< order_by_clause >  
Determines the order in which the NTILE values are assigned to the rows in a partition. An integer cannot represent a column in <order_by_clause>. For more information about *order_by_clause*, see [ORDER BY &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/order-by-sql-server-pdw.md).  
  
## Return Types  
**bigint**  
  
## General Remarks  
When the number of rows in a partition is not divisible by *integer_expression*, the function returns groups of two sizes that differ by one member. Larger groups come before smaller groups in the order specified by the OVER clause. For example, if the total number of rows is 53 and the number of groups is five, the first three groups will have 11 rows and the two remaining groups will have 10 rows each. If, on the other hand, the total number of rows is divisible by the number of groups, the rows will be evenly distributed among the groups. For example, if the total number of rows is 50, and there are five groups, each bucket will contain 10 rows.  
  
## Examples  
  
### A. Dividing rows into groups  
The following example uses the NTILE function to divide a set of salespersons into four groups based on their assigned sales quota for the year 2003. Because the total number of rows is not divisible by the number of groups, the first group has five rows and the remaining groups have four rows each.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT e.LastName, NTILE(4) OVER(ORDER BY SUM(SalesAmountQuota) DESC) AS Quartile,  
       CONVERT (varchar(13), SUM(SalesAmountQuota), 1) AS SalesQuota  
FROM dbo.DimEmployee AS e   
INNER JOIN dbo.FactSalesQuota AS sq   
    ON e.EmployeeKey = sq.EmployeeKey  
WHERE sq.CalendarYear = 2003  
    AND SalesTerritoryKey IS NOT NULL AND SalesAmountQuota <> 0  
GROUP BY e.LastName  
ORDER BY Quartile, e.LastName;  
```  
  
Here is the result set.  
  
<pre>LastName          Quartile SalesYTD  
----------------- -------- ------------  
Blythe            1        4,716,000.00  
Carson            1        4,350,000.00  
Mitchell          1        4,682,000.00  
Pak               1        5,142,000.00  
Varkey Chudukatil 1        2,940,000.00  
Ito               2        2,644,000.00  
Reiter            2        2,768,000.00  
Saraiva           2        2,293,000.00  
Vargas            2        1,617,000.00  
Ansman-Wolfe      3        1,183,000.00  
Campbell          3        1,438,000.00  
Mensa-Annan       3        1,481,000.00  
Valdez            3        1,294,000.00  
Abbas             4          172,000.00  
Albert            4          651,000.00  
Jiang             4          544,000.00  
Tsoflias          4          867,000.00</pre>  
  
### B. Dividing the result set by using PARTITION BY  
The following example adds the PARTITION BY argument to the code in example A. The rows are first partitioned by `SalesTerritoryCountry` and then divided into two groups within each `SalesTerritoryCountry`. Notice that the ORDER BY in the OVER clause orders the NTILE and the ORDER BY of the SELECT statement orders the result set.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT e.LastName, NTILE(2) OVER(PARTITION BY e.SalesTerritoryKey ORDER BY SUM(SalesAmountQuota) DESC) AS Quartile,  
       CONVERT (varchar(13), SUM(SalesAmountQuota), 1) AS SalesQuota  
   ,st.SalesTerritoryCountry  
FROM dbo.DimEmployee AS e   
INNER JOIN dbo.FactSalesQuota AS sq   
    ON e.EmployeeKey = sq.EmployeeKey  
INNER JOIN dbo.DimSalesTerritory AS st  
    ON e.SalesTerritoryKey = st.SalesTerritoryKey  
WHERE sq.CalendarYear = 2003  
GROUP BY e.LastName,e.SalesTerritoryKey,st.SalesTerritoryCountry  
ORDER BY st.SalesTerritoryCountry, Quartile;  
```  
  
Here is the result set.  
  
<pre>LastName          Quartile SalesYTD       SalesTerritoryCountry  
----------------- -------- -------------- ------------------  
Tsoflias          1         867,000.00     Australia  
Saraiva           1        2,293,000.00    Canada  
Varkey Chudukatil 1        2,940,000.00    France  
Valdez            1        1,294,000.00    Germany  
Alberts           1          651,000.00    NA  
Jiang             1          544,000.00    NA  
Pak               1        5,142,000.00    United Kingdom  
Mensa-Annan       1        1,481,000.00    United States  
Campbell          1        1,438,000.00    United States  
Reiter            1        2,768,000.00    United States  
Blythe            1        4,716,000.00    United States  
Carson            1        4,350,000.00     United States  
Mitchell          1        4,682,000.00     United States  
Vargas            2        1,617,000.00     Canada  
Abbas             2          172,000.00     NA  
Ito               2        2,644,000.00     United States  
Ansman-Wolfe      2        1,183,000.00     United States</pre>  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[RANK &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/rank-sql-server-pdw.md)  
[DENSE_RANK &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/dense-rank-sql-server-pdw.md)  
[ROW_NUMBER &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/row-number-sql-server-pdw.md)  
  
