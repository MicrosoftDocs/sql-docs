---
title: "HAVING (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 4402733b-c051-478c-bb6c-1e662bcb0e60
caps.latest.revision: 22
author: BarbKess
---
# HAVING (SQL Server PDW)
Specifies a search condition for a group or an aggregate. HAVING can be used only with the SELECT statement. HAVING is typically used in a GROUP BY clause. When GROUP BY is not used, HAVING behaves like a WHERE clause.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
[ HAVING search_condition ]  
```  
  
## Arguments  
*search_condition*  
Specifies the search condition for the group or the aggregate to meet. For more information, see [Search Condition &#40;SQL Server PDW&#41;](../sqlpdw/search-condition-sql-server-pdw.md).  
  
## Remarks  
  
## Examples  
The following example uses a `HAVING` clause to retrieve the total for each `SalesAmount` from the `FactInternetSales` table when the `OrderDateKey` is in the year 2004 or later.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT OrderDateKey, SUM(SalesAmount) AS TotalSales   
FROM FactInternetSales  
GROUP BY OrderDateKey   
HAVING SUM(SalesAmount) > 80000  
ORDER BY OrderDateKey;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[GROUP BY &#40;SQL Server PDW&#41;](../sqlpdw/group-by-sql-server-pdw.md)  
[WHERE &#40;SQL Server PDW&#41;](../sqlpdw/where-sql-server-pdw.md)  
  
