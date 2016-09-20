---
title: "EXP (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: f98956c8-cb7b-4189-92ba-6c39c2a12fa1
caps.latest.revision: 15
author: BarbKess
---
# EXP (SQL Server PDW)
Returns the exponential value of the specified **float** expression in SQL Server PDW.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
EXP (float_expression )  
```  
  
## Arguments  
*float_expression*  
Is an expression of type **float** or of a type that can be implicitly converted to **float**.  
  
## Return Types  
**float**  
  
## Remarks  
The constant **e** (2.718281…), is the base of natural logarithms.  
  
The exponential value of a number is the constant **e** raised to the power of the number. For example EXP(1.0) = e^1.0 = 2.71828182845905 and EXP(10) = e^10 = 22026.4657948067.  
  
The exponential value is the inverse function of the natural logarithm. The exponent of the natural logarithm of a number is the number itself: EXP (LOG (*n*)) = *n*. And the natural logarithm of the exponent of a number is the number itself: LOG (EXP (*n*)) = *n*.  
  
## Examples  
  
### A. Finding the exponent of a number  
The following example returns the exponential value of the specified value (`10`).  
  
```  
SELECT EXP(10);  
```  
  
Here is the result set.  
  
```  
----------  
22026.4657948067  
```  
  
### B. Finding exponential values and natural logarithms  
The following example returns the exponential value of the natural logarithm of `20` and the natural logarithm of the exponential of `20`. Because these functions are inverse functions of one another, the return value in both cases is `20`.  
  
```  
SELECT EXP( LOG(20)), LOG( EXP(20));  
```  
  
Here is the result set.  
  
```  
-------------- -----------------  
20                  20  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/expressions-sql-server-pdw.md)  
[POWER &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/power-sql-server-pdw.md)  
[LOG &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/log-sql-server-pdw.md)  
[&#42; &#40;Multiply&#41; &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/multiply-sql-server-pdw.md)  
  
