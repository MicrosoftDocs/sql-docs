---
title: "LTRIM (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: dd486e16-3f83-4d84-ad1a-5fa2aac0eff7
caps.latest.revision: 18
author: BarbKess
---
# LTRIM (SQL Server PDW)
Returns a character expression after it removes leading blanks in SQL Server PDW.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
LTRIM (character_expression )  
```  
  
## Arguments  
*character_expression*  
An expression of character or binary data. *character_expression* can be a constant or column. *character_expression* must be of a data type that is implicitly convertible to **varchar** or **nvarchar**. Otherwise, use CAST to explicitly convert *character_expression*.  
  
## Return Type  
**varchar** or **nvarchar**  
  
## Examples  
The following example uses LTRIM to remove leading spaces from a character expression.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT LTRIM('     Five spaces are at the beginning of this string.') FROM dbo.DimCustomer;  
```  
  
Here is the result set.  
  
<pre>---------------------------------------------------------------  
Five spaces are at the beginning of this string.</pre>  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[CAST and CONVERT &#40;SQL Server PDW&#41;](../sqlpdw/cast-and-convert-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../sqlpdw/expressions-sql-server-pdw.md)  
[Data Types &#40;SQL Server PDW&#41;](../sqlpdw/data-types-sql-server-pdw.md)  
[RTRIM &#40;SQL Server PDW&#41;](../sqlpdw/rtrim-sql-server-pdw.md)  
  
