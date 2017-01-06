---
title: "ABS (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: b16eef79-5f8f-44fc-822c-3d7a075bfac2
caps.latest.revision: 20
author: BarbKess
---
# ABS (SQL Server PDW)
Returns the absolute (positive) value of the specified numeric expression in SQL Server PDW.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
ABS (numeric_expression )  
```  
  
## Arguments  
*numeric_expression*  
An expression of the exact numeric or approximate numeric data type category, except for the bit data type.  
  
## Return Types  
Returns the same type as *numeric_expression*.  
  
## Error Handling  
The ABS function can produce an overflow error when the absolute value of a number is larger than the largest number that can be represented by the specified data type. For example, the **int** data type can hold only values that range from -2,147,483,648 to 2,147,483,647.  
  
## Examples  
The following example shows the results of using the `ABS` function on three different numbers.  
  
```  
SELECT ABS(-1.0), ABS(0.0), ABS(1.0);  
```  
  
Here is the result set.  
  
<pre>---- ---- ----  
1.0  0.0  1.0</pre>  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../sqlpdw/expressions-sql-server-pdw.md)  
[CAST and CONVERT &#40;SQL Server PDW&#41;](../sqlpdw/cast-and-convert-sql-server-pdw.md)  
[Data Types &#40;SQL Server PDW&#41;](../sqlpdw/data-types-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
  
