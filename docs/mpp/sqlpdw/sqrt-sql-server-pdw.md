---
title: "SQRT (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 1b3161ff-a671-464e-a0c6-5df590daa760
caps.latest.revision: 15
author: BarbKess
---
# SQRT (SQL Server PDW)
Returns the square root of the specified **float** value in SQL Server PDW.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
SQRT (float_expression )  
```  
  
## Arguments  
*float_expression*  
An expression of type **float** or of a type that can be implicitly converted to float. Otherwise, use [CAST](../../mpp/sqlpdw/cast-and-convert-sql-server-pdw.md) to explicitly convert *expression*.  
  
## Return Types  
**float**  
  
## Examples  
The following example returns the square root of numbers `1.00` and `10.00`.  
  
```  
SELECT SQRT(1.00), SQRT(10.00);  
```  
  
Here is the result set.  
  
<pre>----------  ------------  
1.00        3.16</pre>  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/expressions-sql-server-pdw.md)  
[Data Types &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/data-types-sql-server-pdw.md)  
[CAST and CONVERT &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/cast-and-convert-sql-server-pdw.md)  
[EXP &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/exp-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/functions-sql-server-pdw.md)  
  
