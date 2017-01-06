---
title: "RIGHT (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: e0ec1629-d633-48dc-b3de-3504f2b637e6
caps.latest.revision: 20
author: BarbKess
---
# RIGHT (SQL Server PDW)
Returns the right part of a character string with the specified number of characters in SQL Server PDW.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
RIGHT (character_expression , integer_expression )  
```  
  
## Arguments  
*character_expression*  
An expressions of character data. *character_expression* can be a constant, variable, or column. *character_expression* can be of any data type that can be implicitly converted to **varchar** or **nvarchar**. Otherwise, use the CAST function to explicitly convert *character_expression*.  
  
*integer_expression*  
A positive integer that specifies how many characters of the *character_expression* will be returned. If *integer_expression* is negative, an error is returned.  
  
## Return Types  
Returns **varchar** or **nvarchar**.  
  
## Examples  
  
### A. Using RIGHT with a column  
The following example returns the five rightmost characters of each last name in the `DimEmployee` table.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT RIGHT(LastName, 5) AS Name  
FROM dbo.DimEmployee  
ORDER BY EmployeeKey;  
```  
  
Here is a partial result set.  
  
<pre>Name  
-----  
lbert  
Brown  
rello  
lters</pre>  
  
### B. Using RIGHT with a character string  
The following example uses `RIGHT` to return the two rightmost characters of the character string `abcdefg`.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT TOP(1) RIGHT('abcdefg',2) FROM dbo.DimProduct;  
```  
  
Here is the result set.  
  
<pre>-------  
fg</pre>  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Data Types &#40;SQL Server PDW&#41;](../sqlpdw/data-types-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../sqlpdw/expressions-sql-server-pdw.md)  
[LEFT &#40;SQL Server PDW&#41;](../sqlpdw/left-sql-server-pdw.md)  
  
