---
title: "RADIANS (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: b2ca387c-d775-4984-9ac8-9c9d7d96a8b7
caps.latest.revision: 5
author: BarbKess
---
# RADIANS (SQL Server PDW)
Returns radians when a numeric expression, in degrees, is entered.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
RADIANS (numeric_expression )  
```  
  
## Arguments  
*numeric_expression*  
Is an expression of the exact numeric or approximate numeric data type category, except for the **bit** data type.  
  
## Return Types  
Returns the same type as *numeric_expression*.  
  
## Examples  
  
### A. Using RADIANS to show 0.0  
The following example returns a result of `0.0` because the numeric expression to convert to radians is too small for the `RADIANS` function.  
  
```  
SELECT RADIANS(1e-307)  
GO  
```  
  
Here is the result set.  
  
```  
-------------------   
0.0                        
(1 row(s) affected)  
```  
  
## See Also  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
