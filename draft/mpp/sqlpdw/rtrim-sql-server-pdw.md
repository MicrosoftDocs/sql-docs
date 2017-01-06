---
title: "RTRIM (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 515f3eb2-88f9-46bc-8402-099824e11850
caps.latest.revision: 20
author: BarbKess
---
# RTRIM (SQL Server PDW)
Returns a character string after truncating all trailing blanks in SQL Server PDW.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
RTRIM ( character_expression )  
```  
  
## Arguments  
*character_expression*  
An expression of character data. *character_expression* can be a constant, variable, or column of character data.  
  
*character_expression* must be a data type that is implicitly convertible to **varchar** or **nvarchar**. Otherwise, use CAST to explicitly convert *character_expression*.  
  
## Return Types  
**varchar** or **nvarchar**  
  
## Examples  
The following example demonstrates how to use `RTRIM` to remove trailing spaces from a character variable.  
  
```  
SELECT RTRIM('Four spaces are after the period in this sentence.    ') + 'Next string.';  
```  
  
Here is the result set.  
  
```  
------------------------------------------------------------------------  
Four spaces are after the period in this sentence.Next string.  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../sqlpdw/expressions-sql-server-pdw.md)  
[LTRIM &#40;SQL Server PDW&#41;](../sqlpdw/ltrim-sql-server-pdw.md)  
[CAST and CONVERT &#40;SQL Server PDW&#41;](../sqlpdw/cast-and-convert-sql-server-pdw.md)  
[Data Types &#40;SQL Server PDW&#41;](../sqlpdw/data-types-sql-server-pdw.md)  
  
