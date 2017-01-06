---
title: "CEILING (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: c126997b-ccf8-4671-8d52-b12a9fb413d5
caps.latest.revision: 18
author: BarbKess
---
# CEILING (SQL Server PDW)
Returns the smallest integer greater than, or equal to, the specified numeric expression in SQL Server PDW.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
CEILING (numeric_expression )  
```  
  
## Arguments  
*numeric_expression*  
An expression of the exact numeric or approximate numeric data type category, except for the **bit** data type.  
  
## Return Types  
Returns the same type as *numeric_expression*.  
  
## Examples  
The following example shows use of positive numeric, negative, and zero values with the CEILING function.  
  
```  
SELECT CEILING(123.45), CEILING(-123.45), CEILING(0.0);  
```  
  
Here is the result set.  
  
<pre>------- --------- --------  
124.00  -123.00   0.00</pre>  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../sqlpdw/expressions-sql-server-pdw.md)  
[FLOOR &#40;SQL Server PDW&#41;](../sqlpdw/floor-sql-server-pdw.md)  
  
