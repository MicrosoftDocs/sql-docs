---
title: "CHARINDEX (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 111c4832-b6be-4029-8beb-b015c11b4101
caps.latest.revision: 23
author: BarbKess
---
# CHARINDEX (SQL Server PDW)
Returns the first occurrence of one expression within another expression in SQL Server PDW.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
CHARINDEX (string_pattern,string_expression , start_location)  
```  
  
## Arguments  
*string_pattern*  
The substring to be found. *string_pattern* must be of a character data type. *string_pattern* cannot be an empty string ('').  
  
*string_expression*  
The string expression to be searched. *string_expression* must be of a character data type.  
  
*start_location*  
An **int** or **bigint** expression at which the search starts. The first position in the string is '1'. If *start_location* is not specified, is a negative number, or is 0, the search starts at the beginning of *string_expression*.  
  
## Return Types  
**smallint**  
  
## General Remarks  
The first position in a string is position one ('1'). The location of *string_pattern* is based on the location of the first character in the string.  
  
If either *string_pattern* or *string_expression* is of a Unicode data type (**nvarchar** or **nchar**) and the other is not, the other is converted to a Unicode data type.  
  
When *string_pattern* is not found, CHARINDEX returns '0'.  
  
## Examples  
  
### A. Searching from the start of a string expression  
The following example returns the first location of the `is` string in `This is a string`, starting from position 1 (the first character) in the string.  
  
```  
SELECT CHARINDEX('is', 'This is a string');  
```  
  
Here is the result set.  
  
<pre>---------  
3</pre>  
  
### B. Searching from a position other than the first position  
The following example returns the first location of the `is` string in `This is a string`, starting with the fourth position.  
  
```  
SELECT CHARINDEX('is', 'This is a string', 4);  
```  
  
Here is the result set.  
  
<pre>---------  
6</pre>  
  
### C. Results when the string is not found  
The following example shows the return value when the *string_pattern* is not found in the searched string.  
  
```  
SELECT TOP(1) CHARINDEX('at', 'This is a string') FROM dbo.DimCustomer;  
```  
  
Here is the result set.  
  
<pre>---------  
0</pre>  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Data Types &#40;SQL Server PDW&#41;](../sqlpdw/data-types-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../sqlpdw/expressions-sql-server-pdw.md)  
[PATINDEX &#40;SQL Server PDW&#41;](../sqlpdw/patindex-sql-server-pdw.md)  
  
