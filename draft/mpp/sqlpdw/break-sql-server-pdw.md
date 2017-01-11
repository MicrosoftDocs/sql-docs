---
title: "BREAK (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: b3d55658-e7e7-4ad9-9f72-7538fb1d4bc2
caps.latest.revision: 10
author: BarbKess
---
# BREAK (SQL Server PDW)
Exits the innermost loop in a WHILE statement or an IF…ELSE statement inside a WHILE loop. Any statements appearing after the END keyword, marking the end of the loop, are run.  
  
## Examples  
  
```  
USE AdventureWorksPDW2012;  
  
WHILE ((SELECT AVG(ListPrice) FROM dbo.DimProduct) < $300)  
BEGIN  
    UPDATE DimProduct  
        SET ListPrice = ListPrice * 2;  
     IF ((SELECT MAX(ListPrice) FROM dbo.DimProduct) > $500)  
         BREAK;  
END  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
