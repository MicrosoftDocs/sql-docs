---
title: "UNICODE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 1d8fbc05-fafa-44b3-9882-27f0b6c644cc
caps.latest.revision: 5
author: BarbKess
---
# UNICODE (SQL Server PDW)
Returns the integer value, as defined by the Unicode standard, for the first character of the input expression.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
UNICODE ('ncharacter_expression')  
```  
  
## Arguments  
**'***ncharacter_expression***'**  
Is an **nchar** or **nvarchar** expression.  
  
## Return Types  
**int**  
  
## Remarks  
When using SC collations, UNICODE returns a UTF-16 codepoint in the range 0 through 0x10FFFF.  
  
## Examples  
  
### A. Using UNICODE and the NCHAR function  
The following example uses the `UNICODE` and `NCHAR` functions to print the UNICODE value of the first character of the `Åkergatan` 24-character string, and to print the actual first character, `Å`.  
  
```  
DECLARE @nstring nchar(12);  
SET @nstring = N'Åkergatan 24';  
SELECT UNICODE(@nstring), NCHAR(UNICODE(@nstring));  
```  
  
Here is the result set.  
  
```  
----------- -   
197         Å  
```  
  
### B. Using SUBSTRING, UNICODE, and CONVERT  
The following example uses the `SUBSTRING`, `UNICODE`, and `CONVERT` functions to print the character number, the Unicode character, and the UNICODE value of each of the characters in the string `Åkergatan 24`.  
  
```  
-- The @position variable holds the position of the character currently  
-- being processed. The @nstring variable is the Unicode character   
-- string to process.  
DECLARE @position int, @nstring nchar(12);  
-- Initialize the current position variable to the first character in   
-- the string.  
SET @position = 1;  
-- Initialize the character string variable to the string to process.   
-- Notice that there is an N before the start of the string, which   
-- indicates that the data following the N is Unicode data.  
SET @nstring = N'Åkergatan 24';  
-- Print the character number of the position of the string you are at,   
-- the actual Unicode character you are processing, and the UNICODE   
-- value for this particular character.  
PRINT 'Character #' + ' ' + 'Unicode Character' + ' ' + 'UNICODE Value';  
WHILE @position <= DATALENGTH(@nstring)  
-- While these are still characters in the character string,  
   BEGIN;  
   SELECT @position,   
      CONVERT(char(17), SUBSTRING(@nstring, @position, 1)),  
      UNICODE(SUBSTRING(@nstring, @position, 1));  
   SELECT @position = @position + 1;  
   END;  
```  
  
Here is the result set.  
  
```  
Character # Unicode Character UNICODE Value  
  
----------- ----------------- -----------   
1           Å                 197           
  
----------- ----------------- -----------   
2           k                 107           
  
----------- ----------------- -----------   
3           e                 101           
  
----------- ----------------- -----------   
4           r                 114           
  
----------- ----------------- -----------   
5           g                 103           
  
----------- ----------------- -----------   
6           a                 97            
  
----------- ----------------- -----------   
7           t                 116           
  
----------- ----------------- -----------   
8           a                 97            
  
----------- ----------------- -----------   
9           n                 110           
  
----------- ----------------- -----------   
10                            32            
  
----------- ----------------- -----------   
11          2                 50            
  
----------- ----------------- -----------   
12          4                 52  
```  
  
## See Also  
[Collations &#40;SQL Server PDW&#41;](../sqlpdw/collations-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
