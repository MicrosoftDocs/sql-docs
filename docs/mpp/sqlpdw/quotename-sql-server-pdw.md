---
title: "QUOTENAME (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: ea861d7f-f8f7-46fe-b7ba-57985c01dba4
caps.latest.revision: 9
author: BarbKess
---
# QUOTENAME (SQL Server PDW)
Returns a Unicode string with the delimiters added to make the input string a valid SQL Server delimited identifier.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
QUOTENAME ('character_string' [ ,'quote_character' ] )  
```  
  
## Arguments  
'*character_string*'  
Is a string of Unicode character data. *character_string* is **sysname** and is limited to 128 characters. Inputs greater than 128 characters return NULL.  
  
'*quote_character*'  
Is a one-character string to use as the delimiter. Can be a single quotation mark ( **'** ), a left or right bracket ( **[]** ), or a double quotation mark ( **"** ). If *quote_character* is not specified, brackets are used.  
  
## Return Types  
**nvarchar(258)**  
  
## Examples  
The following example takes the character string `abc def` and uses the `[` and `]` characters to create a valid SQL Server delimited identifier.  
  
```  
SELECT QUOTENAME('abc def');  
```  
  
Here is the result set.  
  
```  
[abc def]  
  
(1 row(s) affected)  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
  
