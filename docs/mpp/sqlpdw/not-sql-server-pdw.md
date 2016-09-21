---
title: "NOT (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 1d87cfd9-829a-462d-999f-432b5dd2a3e2
caps.latest.revision: 17
author: BarbKess
---
# NOT (SQL Server PDW)
Negates a **Boolean** input.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
[ NOT ] boolean_expression  
```  
  
## Arguments  
*boolean_expression*  
Is any valid **Boolean** expression. For more information, see [Expressions &#40;SQL Server PDW&#41;](../sqlpdw/expressions-sql-server-pdw.md).  
  
## Result Types  
**Boolean**  
  
## Result Value  
NOT reverses the value of any **Boolean** expression.  
  
## Remarks  
Using NOT negates an expression.  
  
The following table shows the results of comparing TRUE and FALSE values using the NOT operator.  
  
||NOT|  
|----|-------|  
|TRUE|FALSE|  
|FALSE|TRUE|  
|UNKNOWN|UNKNOWN|  
  
## Examples  
The following example restricts results to `SalesOrderNumber` to values starting with `SO6` and `ProductKeys` greater than or equal to 400.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT ProductKey, CustomerKey, OrderDateKey, ShipDateKey  
FROM FactInternetSales  
WHERE SalesOrderNumber LIKE 'SO6%' AND NOT ProductKey < 400;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../sqlpdw/expressions-sql-server-pdw.md)  
  
