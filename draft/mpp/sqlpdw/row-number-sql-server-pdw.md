---
title: "ROW_NUMBER (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: bec10e55-b87c-4916-84f6-c89d2221fd80
caps.latest.revision: 20
author: BarbKess
---
# ROW_NUMBER (SQL Server PDW)
Returns the sequential number of a row in a partition of a result set, starting at 1 for the first row in each partition in SQL Server PDW.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
ROW_NUMBER () OVER ( [ <partition_by_clause> ] <order_by_clause> )  
```  
  
## Arguments  
<*partition_by_clause*>  
Divides the result set produced by the [FROM](../sqlpdw/from-sql-server-pdw.md) clause into partitions to which the ROW_NUMBER function is applied. For the PARTITION BY syntax, see [OVER Clause &#40;SQL Server PDW&#41;](../sqlpdw/over-clause-sql-server-pdw.md).  
  
<*order_by_clause*>  
Determines the order in which the ROW_NUMBER value is assigned to the rows in a partition. For more information, see [ORDER BY &#40;SQL Server PDW&#41;](../sqlpdw/order-by-sql-server-pdw.md). An integer cannot represent a column when the <order_by_clause> is used.  
  
## Return Types  
**bigint**  
  
## Remarks  
The ORDER BY clause determines the sequence in which the rows are assigned their unique ROW_NUMBER in a specified partition.  
  
## Examples  
  
### A. Returning the row number for salespeople  
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
  
### B. Using ROW_NUMBER() with PARTITION  
The following example shows using the ROW_NUMBER function with the PARTITION BY argument. This causes the ROW_NUMBER function to number the rows in each partition.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT ROW_NUMBER() OVER(PARTITION BY SalesTerritoryKey ORDER BY SUM(SalesAmountQuota) DESC) AS RowNumber,  
    LastName, SalesTerritoryKey AS Territory,  
    CONVERT(varchar(13), SUM(SalesAmountQuota),1) AS SalesQuota   
FROM dbo.DimEmployee AS e  
INNER JOIN dbo.FactSalesQuota AS sq  
    ON e.EmployeeKey = sq.EmployeeKey  
WHERE e.SalesPersonFlag = 1  
GROUP BY LastName, FirstName, SalesTerritoryKey;  
```  
  
Here is a partial result set.  
  
<pre>RowNumber  LastName            Territory  SalesQuota  
---------  ------------------  ---------  -------------  
1          Campbell            1           4,025,000.00  
2          Ansman-Wolfe        1           3,551,000.00  
3          Mensa-Annan         1           2,275,000.00  
1          Blythe              2          11,162,000.00  
1          Carson              3          12,198,000.00  
1          Mitchell            4          11,786,000.00  
2          Ito                 4           7,804,000.00</pre>  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Data Types &#40;SQL Server PDW&#41;](../sqlpdw/data-types-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
  
