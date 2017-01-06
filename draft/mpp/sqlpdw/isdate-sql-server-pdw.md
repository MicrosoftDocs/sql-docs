---
title: "ISDATE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 82354530-f43c-4a47-a2b0-287c971c8494
caps.latest.revision: 5
author: BarbKess
---
# ISDATE (SQL Server PDW)
Returns 1 if the *expression* is a valid **date**, **time**, or **datetime** value; otherwise, 0.  
  
ISDATE returns 0 if the *expression* is a **datetime2** value.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
ISDATE (expression )  
```  
  
## Arguments  
*expression*  
Is a character string or expression that can be converted to a character string. The expression must be less than 4,000 characters. Date and time data types, except datetime and smalldatetime, are not allowed as the argument for ISDATE.  
  
## Return Type  
**int**  
  
## Remarks  
ISDATE is deterministic only if you use it with the [CONVERT](../sqlpdw/cast-and-convert-sql-server-pdw.md) function, if the CONVERT style parameter is specified, and style is not equal to 0, 100, 9, or 109.  
  
The return value of ISDATE depends on the language settings.  
  
## ISDATE expression Formats  
For examples of valid formats for which ISDATE will return 1, see the section "Supported String Literal Formats for datetime" in the datetime and smalldatetime topics. For additional examples, also see the Input/Output column of the "Arguments" section of CAST and CONVERT.  
  
The following table summarizes input expression formats that are not valid and that return 0 or an error.  
  
|ISDATE expression|ISDATE return value|  
|---------------------|-----------------------|  
|NULL|0|  
|Values of data types listed in any data type category other than character strings, Unicode character strings, or date and time.|0|  
|Values of **text**, **ntext**, or **image** data types.|0|  
|Any value that has a seconds precision scale greater than 3, (.0000 through .0000000...n). ISDATE will return 0 if the *expression* is a **datetime2** value, but will return 1 if the *expression* is a valid **datetime** value.|0|  
|Any value that mixes a valid date with an invalid value, for example 1995-10-1a.|0|  
  
## Examples  
  
### A. Using ISDATE to test for a valid datetime expression  
The following example shows you how to use `ISDATE` to test whether a character string is a valid **datetime**.  
  
```  
IF ISDATE('2009-05-12 10:19:41.177') = 1  
    SELECT 'VALID';  
ELSE  
    SELECT 'INVALID';  
```  
  
## See Also  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[CAST and CONVERT &#40;SQL Server PDW&#41;](../sqlpdw/cast-and-convert-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
