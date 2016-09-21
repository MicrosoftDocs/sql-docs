---
title: "STR (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 2452a529-3ed8-4ce2-91f5-bd0de94424a4
caps.latest.revision: 6
author: BarbKess
---
# STR (SQL Server PDW)
Returns character data converted from numeric data.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
STR (float_expression [ , length [ ,decimal ] ] )  
```  
  
## Arguments  
*float_expression*  
Is an expression of approximate numeric (**float**) data type with a decimal point.  
  
*length*  
Is the total length. This includes decimal point, sign, digits, and spaces. The default is 10.  
  
*decimal*  
Is the number of places to the right of the decimal point. *decimal* must be less than or equal to 16. If *decimal* is more than 16 then the result is truncated to sixteen places to the right of the decimal point.  
  
## Return Types  
**varchar**  
  
## Remarks  
If supplied, the values for *length* and *decimal* parameters to STR should be positive. The number is rounded to an integer by default or if the decimal parameter is 0. The specified length should be greater than or equal to the part of the number before the decimal point plus the number's sign (if any). A short *float_expression* is right-justified in the specified length, and a long *float_expression* is truncated to the specified number of decimal places. For example, STR(12**,**10) yields the result of 12. This is right-justified in the result set. However, STR(1223**,**2) truncates the result set to **. String functions can be nested.  
  
> [!NOTE]  
> To convert to Unicode data, use STR inside a CONVERT or CAST conversion function. For more information, see [CAST and CONVERT &#40;SQL Server PDW&#41;](../sqlpdw/cast-and-convert-sql-server-pdw.md).  
  
## Examples  
The following example converts an expression that is made up of five digits and a decimal point to a six-position character string. The fractional part of the number is rounded to one decimal place.  
  
```  
SELECT STR(123.45, 6, 1);  
GO  
```  
  
Here is the result set.  
  
```  
------  
 123.5  
  
(1 row(s) affected)  
```  
  
When the expression exceeds the specified length, the string returns `**` for the specified length.  
  
```  
SELECT STR(123.45, 2, 2);  
GO  
```  
  
Here is the result set.  
  
```  
--  
**  
  
(1 row(s) affected)  
```  
  
Even when numeric data is nested within `STR`, the result is character data with the specified format.  
  
```  
SELECT STR (FLOOR (123.45), 8, 3);  
GO  
```  
  
Here is the result set.  
  
```  
--------  
 123.000  
  
(1 row(s) affected)  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[String Functions (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms181984.aspx)  
  
