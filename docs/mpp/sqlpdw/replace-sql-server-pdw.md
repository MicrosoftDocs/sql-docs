---
title: "REPLACE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 5846729f-bded-44a1-8a75-a3251dec2549
caps.latest.revision: 20
author: BarbKess
---
# REPLACE (SQL Server PDW)
Replaces all occurrences of a specified string value with another string value in SQL Server PDW.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
REPLACE (string_expression,string_pattern,string_replacement)  
```  
  
## Arguments  
*string_expression*  
The string expression to be searched. *string_expression* must be a character data type.  
  
*string_*pattern  
The substring to be found. *string_pattern* must be a character. *string_pattern* cannot be an empty string ('').  
  
*string_*replacement  
The replacement string. *string_replacement* must be a character data type.  
  
## Return Types  
Returns the same type as *string_expression*.  
  
Returns NULL if any one of the arguments is NULL.  
  
## Examples  
The following example replaces the string `cde` in `abcdefghi` with `xxx`.  
  
```  
SELECT REPLACE('abcdefghicde','cde','xxx');  
```  
  
Here is the result set.  
  
<pre>--------------------------  
abxxxfghixxx</pre>  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/expressions-sql-server-pdw.md)  
[Data Types &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/data-types-sql-server-pdw.md)  
[CHARINDEX &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/charindex-sql-server-pdw.md)  
  
