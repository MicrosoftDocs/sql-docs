---
title: "SIN (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 79e1f885-5f50-47c2-9f7e-c1d2fa428ed7
caps.latest.revision: 15
author: BarbKess
---
# SIN (SQL Server PDW)
Returns the trigonometric sine of the specified angle, in radians, and in an approximate numeric (**float**) expression SQL Server PDW.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
SIN (float_expression )  
```  
  
## Arguments  
*float_expression*  
An expression of type **float** or of a type that can be implicitly converted to **float**.  
  
## Return Types  
**float**  
  
## Examples  
The following example calculates the sine for a specified angle.  
  
```  
SELECT SIN(45.175643);  
```  
  
Here is the result set.  
  
<pre>---------  
0.929607</pre>  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../sqlpdw/expressions-sql-server-pdw.md)  
[Data Types &#40;SQL Server PDW&#41;](../sqlpdw/data-types-sql-server-pdw.md)  
[ASIN &#40;SQL Server PDW&#41;](../sqlpdw/asin-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
  
