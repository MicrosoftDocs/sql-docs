---
title: "STUFF (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: cb3901bf-453d-43f4-b875-4a2117ca767f
caps.latest.revision: 5
author: BarbKess
---
# STUFF (SQL Server PDW)
The STUFF function inserts a string into another string. It deletes a specified length of characters in the first string at the start position and then inserts the second string into the first string at the start position.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
STUFF (character_expression , start , length , replaceWith_expression )  
```  
  
## Arguments  
*character_expression*  
Is an expression of character data. *character_expression* can be a constant, variable, or column of either character or binary data.  
  
*start*  
Is an integer value that specifies the location to start deletion and insertion. If *start* or *length* is negative, a null string is returned. If *start* is longer than the first *character_expression*, a null string is returned. *start* can be of type **bigint**.  
  
*length*  
Is an integer that specifies the number of characters to delete. If *length* is longer than the first *character_expression*, deletion occurs up to the last character in the last *character_expression*. *length* can be of type **bigint**.  
  
*replaceWith_expression*  
Is an expression of character data. *character_expression* can be a constant, variable, or column of either character or binary data. This expression will replace *length* characters of *character_expression* beginning at *start*.  
  
## Return Types  
Returns character data if *character_expression* is one of the supported character data types. Returns binary data if *character_expression* is one of the supported binary data types.  
  
## Remarks  
If the start position or the length is negative, or if the starting position is larger than length of the first string, a null string is returned. If the start position is 0, a null value is returned. If the length to delete is longer than the first string, it is deleted to the first character in the first string.  
  
An error is raised if the resulting value is larger than the maximum supported by the return type.  
  
## Supplementary Characters (Surrogate Pairs)  
When using SC collations, both *character_expression* and *replaceWith_expression* can include surrogate pairs. The length parameter will count each surrogate in *character_expression* as a single character.  
  
## Examples  
The following example returns a character string created by deleting three characters from the first string, `abcdef`, starting at position `2`, at `b`, and inserting the second string at the deletion point.  
  
```  
SELECT STUFF('abcdef', 2, 3, 'ijklmn');  
GO  
```  
  
Here is the result set.  
  
```  
---------   
aijklmnef   
  
(1 row(s) affected)  
```  
  
## See Also  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
