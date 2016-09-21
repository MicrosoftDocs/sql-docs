---
title: "+ (String Concatenation) (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: a59ea9c4-afa0-4104-85e9-a5040c883e63
caps.latest.revision: 21
author: BarbKess
---
# + (String Concatenation) (SQL Server PDW)
An operator in a string expression that concatenates two or more character or binary strings, columns, or a combination of strings and column names into one expression (a string operator).  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
expression +expression  
```  
  
## Arguments  
*expression*  
Is any valid expression of any one of the data types in the character and binary data type category. Both expressions must be of the same data type, or one expression must be implicitly convertible to the data type of the other expression.  
  
An explicit conversion to character data must be used when concatenating binary strings and any characters between the binary strings.  
  
## Result Types  
Returns the data type of the argument with the highest precedence. For more information, see Data Type Precedence in [Data Types &#40;SQL Server PDW&#41;](../sqlpdw/data-types-sql-server-pdw.md).  
  
When two **char** or **varchar** expressions are concatenated, the length of the resulting expression is the sum of the lengths of the two source expressions or 8,000 characters, whichever is less.  
  
When two **nchar** or **nvarchar** expressions are concatenated, the length of the resulting expression is the sum of the lengths of the two source expressions or 4,000 characters, whichever is less.  
  
## General Remarks  
The string concatenation operator behaves differently when it works with an empty, zero-length string than when it works with null or unknown values.  
  
A zero-length character string can be specified as two single quotation marks without any characters inside the quotation marks. A zero-length binary string can be specified as 0x without any byte values specified in the hexadecimal constant. Concatenating a zero-length string always concatenates the two specified strings.  
  
The result of the concatenation of a string and a null, by contrast, is a null result.  
  
If the result of the concatenation of strings exceeds the limit of 8,000 bytes, the result is truncated. However, if at least one of the strings concatenated is a large value type, truncation does not occur.  
  
## Examples  
  
### A. Using string concatenation  
The following example creates a single column under the column heading `Name` from multiple-character columns, with the last name of the contact followed by a comma, a single space, and then the first name of the contact. The result set is in ascending, alphabetical order by the last name, and then by the first name.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT (LastName + ', ' + FirstName) AS Name  
FROM DimEmployee  
ORDER BY LastName ASC, FirstName ASC;  
```  
  
### B. Using multiple string concatenation  
The following example concatenates multiple strings to form one long string to display the last name and the first initial of the vice presidents within a sample database. A comma is added after the last name and a period after the first initial.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT (LastName + ', ' + SUBSTRING(FirstName, 1, 1) + '.') AS Name, Title  
FROM DimEmployee  
WHERE Title LIKE '%Vice Pres%'  
ORDER BY LastName ASC;  
```  
  
Here is the result set.  
  
```  
Name               Title                                           
-------------      ---------------  
Duffy, T.          Vice President of Engineering  
Hamilton, J.       Vice President of Production  
Welcker, B.        Vice President of Sales  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[CAST and CONVERT &#40;SQL Server PDW&#41;](../sqlpdw/cast-and-convert-sql-server-pdw.md)  
[Data Types &#40;SQL Server PDW&#41;](../sqlpdw/data-types-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../sqlpdw/expressions-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[Operators &#40;SQL Server PDW&#41;](../sqlpdw/operators-sql-server-pdw.md)  
[SELECT &#40;SQL Server PDW&#41;](../sqlpdw/select-sql-server-pdw.md)  
  
