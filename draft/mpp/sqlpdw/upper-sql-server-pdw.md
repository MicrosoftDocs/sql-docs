---
title: "UPPER (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: e0679b4e-89cc-42c3-af14-95a7efe4ac00
caps.latest.revision: 20
author: BarbKess
---
# UPPER (SQL Server PDW)
Returns a character expression with lowercase character data converted to uppercase in SQL Server PDW.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
UPPER (character_expression )  
```  
  
## Arguments  
*character_expression*  
An expression of character data. *character_expression* can be a constant or column of character data.  
  
*character_expression* must be of a data type that is implicitly convertible to **varchar** or **nvarchar**. Otherwise, use [CAST](../sqlpdw/cast-and-convert-sql-server-pdw.md) to explicitly convert *character_expression*.  
  
## Return Types  
**varchar** or **nvarchar**  
  
## Examples  
The following example uses the `UPPER` and `RTRIM` functions to return the last name of people in the `dbo.DimEmployee` table so that it is in uppercase, trimmed, and concatenated with the first name.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT UPPER(RTRIM(LastName)) + ', ' + FirstName AS Name  
FROM dbo.DimEmployee  
ORDER BY LastName;  
```  
  
Here is a partial result set.  
  
<pre>Name  
------------------------------  
ABBAS, Syed  
ABERCROMBIE, Kim  
ABOLROUS, Hazem</pre>  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../sqlpdw/expressions-sql-server-pdw.md)  
[CAST and CONVERT &#40;SQL Server PDW&#41;](../sqlpdw/cast-and-convert-sql-server-pdw.md)  
[Data Types &#40;SQL Server PDW&#41;](../sqlpdw/data-types-sql-server-pdw.md)  
[RTRIM &#40;SQL Server PDW&#41;](../sqlpdw/rtrim-sql-server-pdw.md)  
[LOWER &#40;SQL Server PDW&#41;](../sqlpdw/lower-sql-server-pdw.md)  
  
