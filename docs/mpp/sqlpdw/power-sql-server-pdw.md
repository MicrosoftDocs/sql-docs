---
title: "POWER (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: c4e0339c-01b6-494d-ad49-24ed2d43f6ed
caps.latest.revision: 17
author: BarbKess
---
# POWER (SQL Server PDW)
Returns the exponential value of one expression raised to the power of the second expression in SQL Server PDW.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
POWER (float_expression , y )  
```  
  
## Arguments  
*float_expression*  
An [expression](../../mpp/sqlpdw/expressions-sql-server-pdw.md) of type **float** or of a type that can be implicitly converted to **float**, except for the bit data type.  
  
*y*  
The power to which to raise *float_expression*. *y* can be an expression of the exact numeric or approximate numeric data type category, except for the **bit** data type.  
  
## Return Types  
Same as *float_expression*.  
  
## Examples  
The following example shows returns `POWER` results for `2.0` to the 3rd power.  
  
```  
SELECT POWER(2.0, 3);  
```  
  
Here is the result set.  
  
<pre>------------  
8.0</pre>  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[SQUARE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/square-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/expressions-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/functions-sql-server-pdw.md)  
  
