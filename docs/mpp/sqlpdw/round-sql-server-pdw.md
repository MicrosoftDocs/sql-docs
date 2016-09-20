---
title: "ROUND (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 67c4efd0-0a09-446b-bfa0-5ee2ef484556
caps.latest.revision: 18
author: BarbKess
---
# ROUND (SQL Server PDW)
Returns a numeric value, rounded to the specified length or precision in SQL Server PDW.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
ROUND (numeric_expression , length )  
```  
  
## Arguments  
*numeric_expression*  
An expression of the exact numeric or approximate numeric data type category, except for the bit data type.  
  
*length*  
The precision to which *numeric_expression* is to be rounded. *length* can be of any numeric type. When *length* is a positive number, *numeric_expression* is rounded to the number of decimal positions specified by *length*. When *length* is a negative number, *numeric_expression* is rounded on the left side of the decimal point, as specified by *length*.  
  
## Return Types  
Returns the same type as *numeric_expression**.*  
  
## Remarks  
ROUND always returns a value. If *length* is negative and larger in absolute value than the number of digits before the decimal point, ROUND returns 0. ROUND returns a rounded *numeric_expression*, regardless of data type, when *length* is a negative number.  
  
|Examples|Result|  
|------------|----------|  
|ROUND(748.58, -1)|750.00|  
|ROUND(748.58, -2)|700.00|  
|ROUND(748.58, -3)|1000.00|  
|ROUND(748.58, -4)|0|  
  
## Examples  
  
### A. Using ROUND and estimates  
The following example shows two expressions that demonstrate by using `ROUND` the last digit is always an estimate.  
  
```  
SELECT ROUND(123.994999, 3), ROUND(123.995444, 3);  
```  
  
Here is the result set.  
  
<pre>--------  ---------  
123.995000    123.995444</pre>  
  
### B. Using ROUND and rounding approximations  
The following example shows rounding and approximations.  
  
```  
SELECT ROUND(123.4545, 2), ROUND(123.45, -2);  
```  
  
Here is the result set.  
  
<pre>--------  ----------  
123.45    100.00</pre>  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/functions-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/expressions-sql-server-pdw.md)  
[Data Types &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/data-types-sql-server-pdw.md)  
  
