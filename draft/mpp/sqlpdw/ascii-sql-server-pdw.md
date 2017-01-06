---
title: "ASCII (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 5f0a8fc1-1080-490e-adfa-978f9708df86
caps.latest.revision: 6
author: BarbKess
---
# ASCII (SQL Server PDW)
Returns the ASCII code value of the leftmost character of a character expression.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
ASCII ( character_expression )  
```  
  
## Arguments  
*character_expression*  
Is an expression of the type **char** or **varchar**.  
  
## Return Types  
**int**  
  
## Examples  
The following example assumes an ASCII character set and returns the `ASCII` value for 6 characters.  
  
```  
SELECT ASCII('A') AS A, ASCII('B') AS B,   
ASCII('a') AS a, ASCII('b') AS b,  
ASCII(1) AS [1], ASCII(2) AS [2];  
```  
  
Here is the result set.  
  
```  
A           B           a           b           1           2  
----------- ----------- ----------- ----------- ----------- -----------  
65          66          97          98          49          50  
```  
  
## See Also  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[CHAR &#40;SQL Server PDW&#41;](../sqlpdw/char-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
