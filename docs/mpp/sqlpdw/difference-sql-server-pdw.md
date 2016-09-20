---
title: "DIFFERENCE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 09a9dfda-f495-4bb2-b6fb-1ff11744280f
caps.latest.revision: 5
author: BarbKess
---
# DIFFERENCE (SQL Server PDW)
Returns an integer value that indicates the difference between the SOUNDEX values of two character expressions.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
DIFFERENCE (character_expression ,character_expression )  
```  
  
## Arguments  
*character_expression*  
Is an alphanumeric expression of character data. *character_expression* can be a constant, variable, or column.  
  
## Return Types  
**int**  
  
## Remarks  
The integer returned is the number of characters in the SOUNDEX values that are the same. The return value ranges from 0 through 4: 0 indicates weak or no similarity, and 4 indicates strong similarity or the same values.  
  
DIFFERENCE and SOUNDEX are collation sensitive.  
  
## Examples  
In the first part of the following example, the `SOUNDEX` values of two very similar strings are compared. For a Latin1_General collation `DIFFERENCE` returns a value of `4`. In the second part of the following example, the `SOUNDEX` values for two very different strings are compared, and for a Latin1_General collation `DIFFERENCE` returns a value of `0`.  
  
```  
-- Returns a DIFFERENCE value of 4, the least possible difference.  
SELECT SOUNDEX('Green'), SOUNDEX('Greene'), DIFFERENCE('Green','Greene');  
GO  
-- Returns a DIFFERENCE value of 0, the highest possible difference.  
SELECT SOUNDEX('Blotchet-Halls'), SOUNDEX('Greene'), DIFFERENCE('Blotchet-Halls', 'Greene');  
GO  
```  
  
Here is the result set.  
  
```  
----- ----- -----------   
G650  G650  4             
  
(1 row(s) affected)  
  
----- ----- -----------   
B432  G650  0             
  
(1 row(s) affected)  
```  
  
## See Also  
[SOUNDEX &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/soundex-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/functions-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
