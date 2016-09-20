---
title: "SOUNDEX (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 61373413-0464-4f95-995d-fa702db2b8a0
caps.latest.revision: 5
author: BarbKess
---
# SOUNDEX (SQL Server PDW)
Returns a four-character (SOUNDEX) code to evaluate the similarity of two strings.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
SOUNDEX (character_expression )  
```  
  
## Arguments  
*character_expression*  
Is an alphanumeric expression of character data. *character_expression* can be a constant, variable, or column.  
  
## Return Types  
**varchar**  
  
## Remarks  
SOUNDEX converts an alphanumeric string to a four-character code that is based on how the string sounds when spoken. The first character of the code is the first character of *character_expression*, converted to upper case. The second through fourth characters of the code are numbers that represent the letters in the expression. The letters A, E, I, O, U, H, W, and Y are ignored unless they are the first letter of the string. Zeroes are added at the end if necessary to produce a four-character code. For more information about the SOUNDEX code, see [The Soundex Indexing System](http://www.archives.gov/research/census/soundex.html).  
  
SOUNDEX codes from different strings can be compared to see how similar the strings sound when spoken. The DIFFERENCE function performs a SOUNDEX on two strings, and returns an integer that represents how similar the SOUNDEX codes are for those strings.  
  
SOUNDEX is collation sensitive. String functions can be nested.  
  
## Examples  
The following example shows the SOUNDEX function and the related DIFFERENCE function. In the first example, the standard `SOUNDEX` values are returned for all consonants. Returning the `SOUNDEX` for `Smith` and `Smythe` returns the same SOUNDEX result because all vowels, the letter `y`, doubled letters, and the letter `h`, are not included.  
  
```  
-- Using SOUNDEX  
SELECT SOUNDEX ('Smith'), SOUNDEX ('Smythe');  
```  
  
Here is the result set. Valid for a Latin1_General collation.  
  
```  
----- -----   
S530  S530    
  
(1 row(s) affected)  
```  
  
The `DIFFERENCE` function compares the difference of the `SOUNDEX` pattern results. The following example shows two strings that differ only in vowels. The difference returned is `4`, the lowest possible difference.  
  
```  
-- Using DIFFERENCE  
SELECT DIFFERENCE('Smithers', 'Smythers');  
GO  
```  
  
Here is the result set. Valid for a Latin1_General collation.  
  
```  
-----------   
4             
  
(1 row(s) affected)  
```  
  
In the following example, the strings differ in consonants; therefore, the difference returned is `2`, the greater difference.  
  
```  
SELECT DIFFERENCE('Anothers', 'Brothers');  
GO  
```  
  
Here is the result set. Valid for a Latin1_General collation.  
  
```  
-----------   
2             
  
(1 row(s) affected)  
```  
  
## See Also  
[DIFFERENCE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/difference-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/functions-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
