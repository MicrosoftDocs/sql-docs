---
title: "COS (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 27ebb8b5-3ce1-40ee-9709-53e8fb6ed4e4
caps.latest.revision: 18
author: BarbKess
---
# COS (SQL Server PDW)
Returns the trigonometric cosine of the specified angle, in radians, represented by the specified expression in SQL Server PDW.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
COS (float_expression )  
```  
  
## Arguments  
*float_expression*  
Is an expression of type **float**.  
  
## Return Types  
**float**  
  
## Examples  
The following example returns the COS of the specific angle.  
  
```  
SELECT COS(14.76) AS cosCalc1, COS(-0.1472738) AS cosCalc2;  
```  
  
Here is the result set.  
  
<pre>cosCalc1  cosCalc2  
--------  --------  
-0.58     0.99</pre>  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../sqlpdw/expressions-sql-server-pdw.md)  
[Data Types &#40;SQL Server PDW&#41;](../sqlpdw/data-types-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
  
