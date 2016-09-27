---
title: "OVER Clause (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: b03f2cbd-71de-421b-9fc9-e3daf030ba32
caps.latest.revision: 18
author: BarbKess
---
# OVER Clause (SQL Server PDW)
Determines the partitioning and ordering of the rowset before the analytic function is applied.  
  
For more information, see [Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md).  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
OVER ( [ PARTITION BY value_expression ] [ order_by_clause ] )  
```  
  
## Arguments  
PARTITION BY  
Divides the result set into partitions. The analytic function is applied to each partition separately and computation restarts for each partition.  
  
*value_expression*  
Specifies the column by which the rowset produced by the corresponding FROM clause is partitioned. *value_expression* can only refer to columns made available by the FROM clause. *value_expression* cannot refer to expressions or aliases in the select list. *value_expression* can be a column expression, scalar subquery, or scalar function.  
  
*order_by_clause*  
Specifies the order to apply the analytic function. For more information, see [ORDER BY](../sqlpdw/order-by-sql-server-pdw.md).  
  
> [!IMPORTANT]  
> When used in the context of an analytic function, *order_by_clause* can only refer to columns made available by the FROM clause. An **integer** cannot be specified to represent the position of the name or alias of a column in the select list.  
  
## Remarks  
More than one analytic function can be used in a single query with a single FROM clause. However, the OVER clause for each function can differ in partitioning and also ordering.  
  
## Examples  
  
### A. Using the OVER clause with the ROW_NUMBER function  
The following example returns the ROW_NUMBER for sales representatives based on their assigned sales quota.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT ROW_NUMBER() OVER(ORDER BY SUM(SalesAmountQuota) DESC) AS RowNumber,  
    FirstName, LastName,   
CONVERT(varchar(13), SUM(SalesAmountQuota),1) AS SalesQuota   
FROM dbo.DimEmployee AS e  
INNER JOIN dbo.FactSalesQuota AS sq  
    ON e.EmployeeKey = sq.EmployeeKey  
WHERE e.SalesPersonFlag = 1  
GROUP BY LastName, FirstName;  
```  
  
Here is a partial result set.  
  
<pre>RowNumber  FirstName  LastName            SalesQuota  
---------  ---------  ------------------  -------------  
1          Jillian    Carson              12,198,000.00  
2          Linda      Mitchell            11,786,000.00  
3          Michael    Blythe              11,162,000.00  
4          Jae        Pak                 10,514,000.00</pre>  
  
### B. Using the OVER clause with aggregate functions  
The following examples show using the OVER clause with aggregate functions. In this example, using the OVER clause is more efficient than using subqueries.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT SalesOrderNumber AS OrderNumber, ProductKey,   
       OrderQuantity AS Qty,   
       SUM(OrderQuantity) OVER(PARTITION BY SalesOrderNumber) AS Total,  
       AVG(OrderQuantity) OVER(PARTITION BY SalesOrderNumber) AS Avg,  
       COUNT(OrderQuantity) OVER(PARTITION BY SalesOrderNumber) AS Count,  
       MIN(OrderQuantity) OVER(PARTITION BY SalesOrderNumber) AS Min,  
       MAX(OrderQuantity) OVER(PARTITION BY SalesOrderNumber) AS Max  
FROM dbo.FactResellerSales   
WHERE SalesOrderNumber IN(N'SO43659',N'SO43664') AND  
      ProductKey LIKE '2%'  
ORDER BY SalesOrderNumber,ProductKey;  
```  
  
Here is the result set.  
  
<pre>OrderNumber  Product  Qty  Total  Avg  Count  Min  Max  
-----------  -------  ---  -----  ---  -----  ---  ---  
SO43659      218      6    16     3    5      1    6  
SO43659      220      4    16     3    5      1    6  
SO43659      223      2    16     3    5      1    6  
SO43659      229      3    16     3    5      1    6  
SO43659      235      1    16     3    5      1    6  
SO43664      229      1     2     1    2      1    1  
SO43664      235      1     2     1    2      1    1</pre>  
  
The following example shows using the OVER clause with an aggregate function in a calculated value. Notice that the aggregates are calculated by `SalesOrderNumber` and the percentage of the total sales order is calculated for each line of each `SalesOrderNumber`.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT SalesOrderNumber AS OrderNumber, ProductKey AS Product,   
       OrderQuantity AS Qty,   
       SUM(OrderQuantity) OVER(PARTITION BY SalesOrderNumber) AS Total,  
       CAST(1. * OrderQuantity / SUM(OrderQuantity)   
        OVER(PARTITION BY SalesOrderNumber)   
            *100 AS DECIMAL(5,2)) AS PctByProduct  
FROM dbo.FactResellerSales   
WHERE SalesOrderNumber IN(N'SO43659',N'SO43664') AND  
      ProductKey LIKE '2%'  
ORDER BY SalesOrderNumber,ProductKey;  
```  
  
The first start of this result set is:  
  
<pre>OrderNumber  Product  Qty  Total  PctByProduct  
-----------  -------  ---  -----  ------------  
SO43659      218      6    16     37.50  
SO43659      220      4    16     25.00  
SO43659      223      2    16     12.50  
SO43659      229      2    16     18.75</pre>  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
  
