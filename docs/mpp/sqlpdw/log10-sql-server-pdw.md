---
title: "LOG10 (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: a0ef4dc0-e98d-49a8-821e-9f12637eda92
caps.latest.revision: 19
author: BarbKess
---
# LOG10 (SQL Server PDW)
Returns the base 10 logarithm of the specified **float** expression in SQL Server PDW.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
LOG10 (float_expression )  
```  
  
## Arguments  
*float_expression*  
Is an expression of type **float** or of a type that can be implicitly converted to **float**. Otherwise, use [CAST](../sqlpdw/cast-and-convert-sql-server-pdw.md) to convert the expression to the float type.  
  
## Return Types  
**float**  
  
## Examples  
The following example calculates the `LOG10` of the specified variable.  
  
```  
SELECT LOG10(145.175642);  
```  
  
Here is the result set.  
  
<pre>-------------------  
2.16</pre>  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[LOG &#40;SQL Server PDW&#41;](../sqlpdw/log-sql-server-pdw.md)  
[EXP &#40;SQL Server PDW&#41;](../sqlpdw/exp-sql-server-pdw.md)  
  
