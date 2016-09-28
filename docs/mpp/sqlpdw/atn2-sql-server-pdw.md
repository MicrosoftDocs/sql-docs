---
title: "ATN2 (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 39ec5496-3b18-47a1-b11c-ba5c724778c6
caps.latest.revision: 5
author: BarbKess
---
# ATN2 (SQL Server PDW)
Returns the angle, in radians, between the positive x-axis and the ray from the origin to the point (y, x), where x and y are the values of the two specified float expressions.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
ATN2 (float_expression , float_expression )  
```  
  
## Arguments  
*float_expression*  
Is an expression of the **float** data type.  
  
## Return Types  
**float**  
  
## Examples  
The following example calculates the `ATN2` for the specified `x` and `y` components.  
  
```  
DECLARE @x float = 35.175643, @y float = 129.44;  
SELECT 'The ATN2 of the angle is: ' + CONVERT(varchar,ATN2(@x,@y ));  
GO  
```  
  
Here is the result set.  
  
```  
The ATN2 of the angle is: 0.265345                         
(1 row(s) affected)  
```  
  
## See Also  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
