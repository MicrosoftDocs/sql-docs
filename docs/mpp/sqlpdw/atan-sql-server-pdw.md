---
title: "ATAN (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 755464be-beba-49f7-831e-ff63c40ff8f2
caps.latest.revision: 20
author: BarbKess
---
# ATAN (SQL Server PDW)
Returns the angle in radians whose tangent is a specified **float** expression in SQL Server PDW. This is also referred to as the arctangent.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
ATAN (float_expression )  
```  
  
## Arguments  
*float_expression*  
Is an expression of type **float** or of a type that can be implicitly converted to **float**.  
  
## Return Types  
**float**  
  
## Examples  
The following example takes a **float** expression and returns the arctangent of the specified angle.  
  
```  
SELECT ATAN(45.87) AS atanCalc1,  
    ATAN(-181.01) AS atanCalc2,  
    ATAN(0) AS atanCalc3,  
    ATAN(0.1472738) AS atanCalc4,  
    ATAN(197.1099392) AS atanCalc5;  
```  
  
Here is the result set.  
  
<pre>atanCalc1  atanCalc2  atanCalc3  atanCalc4  atanCalc5  
---------  ---------  ---------  ---------  ---------  
1.55       -1.57       0.00       0.15       1.57</pre>  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../sqlpdw/expressions-sql-server-pdw.md)  
[Data Types &#40;SQL Server PDW&#41;](../sqlpdw/data-types-sql-server-pdw.md)  
[TAN &#40;SQL Server PDW&#41;](../sqlpdw/tan-sql-server-pdw.md)  
  
