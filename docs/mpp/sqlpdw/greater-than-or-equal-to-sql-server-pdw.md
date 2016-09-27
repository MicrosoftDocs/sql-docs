---
title: "&gt;= (Greater Than or Equal To) (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 1ad5e0b0-d569-4439-8e92-dbb634de50fd
caps.latest.revision: 14
author: BarbKess
---
# &gt;= (Greater Than or Equal To) (SQL Server PDW)
Compares two expressions (a comparison operator). When you compare non-null expressions, the result is TRUE if the right operand has a value lower than or equal to the left operand; otherwise, the result is FALSE.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
expression > = expression  
```  
  
## Arguments  
*expression*  
Is any valid expression. Both expressions must have implicitly convertible data types. The conversion depends on the rules of data type precedence.  For more information, see Data Type Precedence in [Data Types &#40;SQL Server PDW&#41;](../sqlpdw/data-types-sql-server-pdw.md).  
  
## Result Types  
**Boolean**  
  
## Remarks  
When you compare non-null expressions, the result is TRUE if the left operand has a value greater than or equal to that of the right operand; otherwise, the result is FALSE.  
  
Comparisons involving null values, such as (NULL >= 100) or (100 >= NULL) return UNKNOWN; a comparison that results in UNKNOWN is not returned with either TRUE or FALSE results.  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../sqlpdw/expressions-sql-server-pdw.md)  
[Data Types &#40;SQL Server PDW&#41;](../sqlpdw/data-types-sql-server-pdw.md)  
[Operators &#40;SQL Server PDW&#41;](../sqlpdw/operators-sql-server-pdw.md)  
[= &#40;Equals&#41; &#40;SQL Server PDW&#41;](../sqlpdw/equals-sql-server-pdw.md)  
[&#62; &#40;Greater Than&#41; &#40;SQL Server PDW&#41;](../sqlpdw/greater-than-sql-server-pdw.md)  
[&#60;= &#40;Less Than or Equal To&#41; &#40;SQL Server PDW&#41;](../sqlpdw/less-than-or-equal-to-sql-server-pdw.md)  
  
