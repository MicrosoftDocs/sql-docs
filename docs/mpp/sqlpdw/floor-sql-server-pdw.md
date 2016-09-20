---
title: "FLOOR (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 04188cfc-f24b-4759-b5d3-47c68e3d2b69
caps.latest.revision: 18
author: BarbKess
---
# FLOOR (SQL Server PDW)
Returns the largest integer smaller than the specified numeric expression in SQL Server PDW.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
FLOOR (numeric_expression )  
```  
  
## Arguments  
*numeric_expression*  
An expression of the exact numeric or approximate numeric data type categories, except for the bit data type.  
  
## Return Types  
Returns the same type as *numeric_expression*.  
  
## Examples  
The following example shows positive numeric, negative numeric, and values with the `FLOOR` function.  
  
```  
SELECT FLOOR(123.45), FLOOR(-123.45), FLOOR($123.45);  
```  
  
The result is the integer part of the calculated value in the same data type as *numeric_expression*.  
  
<pre>-----   ---------    -----------  
123     -124         123</pre>  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/expressions-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/functions-sql-server-pdw.md)  
[Data Types &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/data-types-sql-server-pdw.md)  
  
