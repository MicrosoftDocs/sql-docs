---
title: "LEFT (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: ccc3e08d-7dd3-4e4b-b7fd-8c365eb7a017
caps.latest.revision: 20
author: BarbKess
---
# LEFT (SQL Server PDW)
Returns the left part of a character string with the specified number of characters in SQL Server PDW.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
LEFT (character_expression , integer_expression )  
```  
  
## Arguments  
*character_expression*  
An expression of character or binary data. *character_expression* can be a constant, variable, or column. *character_expression* can be of any data type that can be implicitly converted to **varchar** or **nvarchar**. Otherwise, use the CAST function to explicitly convert *character_expression*.  
  
*integer_expression*  
A positive integer that specifies how many characters of the *character_expression* will be returned. If *integer_expression* is negative, an error is returned.  
  
## Return Types  
Returns **varchar** when *character_expression* is a non-Unicode character data type.  
  
Returns **nvarchar** when *character_expression* is a Unicode character data type.  
  
## Examples  
  
### A. Using LEFT with a column  
The following example returns the five leftmost characters of each product name.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT LEFT(EnglishProductName, 5)   
FROM dbo.DimProduct  
ORDER BY ProductKey;  
```  
  
### B. Using LEFT with a character string  
The following example uses `LEFT` to return the two leftmost characters of the character string `abcdefg`.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT LEFT('abcdefg',2) FROM dbo.DimProduct;  
```  
  
Here is the result set.  
  
```  
--   
ab  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[CAST and CONVERT &#40;SQL Server PDW&#41;](../sqlpdw/cast-and-convert-sql-server-pdw.md)  
[Data Types &#40;SQL Server PDW&#41;](../sqlpdw/data-types-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[RIGHT &#40;SQL Server PDW&#41;](../sqlpdw/right-sql-server-pdw.md)  
  
