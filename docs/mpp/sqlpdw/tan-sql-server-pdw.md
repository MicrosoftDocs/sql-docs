---
title: "TAN (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 560eb70a-f22c-41bf-9eb3-5e3f415e11ee
caps.latest.revision: 14
author: BarbKess
---
# TAN (SQL Server PDW)
Returns the tangent of the input expression SQL Server PDW.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
TAN (float_expression )  
```  
  
## Arguments  
*float_expression*  
An expression of type **float** or of a type that can be implicitly converted to **float**, interpreted as number of radians. *float_expression* must be a number between -1.0 and 1.0, inclusive.  
  
## Return Types  
**float**  
  
## Examples  
The following example returns the tangent of .45.  
  
```  
SELECT TAN(.45);  
```  
  
Here is the result set.  
  
<pre>--------  
0.48</pre>  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/functions-sql-server-pdw.md)  
[ATAN &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/atan-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/expressions-sql-server-pdw.md)  
[Data Types &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/data-types-sql-server-pdw.md)  
  
