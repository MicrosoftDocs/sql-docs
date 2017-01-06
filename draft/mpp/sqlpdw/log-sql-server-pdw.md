---
title: "LOG (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: ed63bcf8-6d01-4ae5-96ed-143d097fa44f
caps.latest.revision: 17
author: BarbKess
---
# LOG (SQL Server PDW)
Returns the natural logarithm of the specified **float** expression in SQL Server PDW.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
LOG (float_expression )  
```  
  
## Arguments  
*float_expression*  
Any valid **float** expression, or an expression of a type that can be implicitly converted to the float type. Otherwise, use the [CAST](../sqlpdw/cast-and-convert-sql-server-pdw.md) function to convert *float_expression*.  
  
## Return Types  
**float**  
  
## Remarks  
The constant **e** (2.71828182845905…) is the base of natural logarithms.  
  
The base of natural logarithms is the constant **e** (2.71828182845905…). LOG ( **e** ) = 1.0.  
  
The natural logarithm of the exponential of a number is the number itself: LOG ( EXP ( *n* ) ) = *n*. And the exponential of the natural logarithm of a number is the number itself: EXP ( LOG ( *n* ) ) = *n*.  
  
## Examples  
  
### A. Calculating the logarithm for a number  
The following example calculates the `LOG` for the specified **float** expression.  
  
```  
SELECT LOG(10);  
```  
  
Here is the result set.  
  
<pre>----------------  
2.30</pre>  
  
### B. Calculating the logarithm of the exponent of a number  
The following example calculates the `LOG` for the exponent of a number.  
  
```  
SELECT LOG(EXP (10));  
```  
  
Here is the result set.  
  
<pre>---------  
10.00</pre>  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[EXP &#40;SQL Server PDW&#41;](../sqlpdw/exp-sql-server-pdw.md)  
[LOG10 &#40;SQL Server PDW&#41;](../sqlpdw/log10-sql-server-pdw.md)  
  
