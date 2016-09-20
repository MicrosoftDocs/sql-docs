---
title: "Constants (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: e8ac7fe5-6354-48a4-b69f-5750e5917d37
caps.latest.revision: 17
author: BarbKess
---
# Constants (SQL Server PDW)
A constant, also known as a literal or a scalar value, is a symbol that represents a specific data value. The format of a constant depends on the data type of the value it represents.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
Character string constants  
Character string constants are enclosed in single quotation marks and include alphanumeric characters (a-z, A-Z, and 0-9) and special characters, such as an exclamation point (!), at sign (@), or number sign (#). Character string constants are assigned the default collation.  
  
An extra single quote (`'`) is the syntactic escape character. If a character string enclosed in single quotation marks contains an embedded quotation mark, represent the embedded single quotation mark with two single quotation marks.  
  
The following are examples of character strings:  
  
```  
'Cincinnati'  
'O''Brien'  
'Process X is 50% complete.'  
```  
  
Empty strings are represented as two single quotation marks with nothing in between them.  
  
Date and time constants  
Date and time constants are represented by using character date values in specific formats, enclosed in single quotation marks. For more information about the formats for date and time constants, see [Data Types &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/data-types-sql-server-pdw.md).  
  
The following are examples of date and time constants:  
  
```  
'December 5, 1985'  
'5 December, 1985'  
'851205'  
'12/5/98'  
```  
  
Examples of time constants are:  
  
```  
'14:30:24'  
'04:24 PM'  
```  
  
**integer** constants  
**integer** constants are represented by a string of numbers that are not enclosed in quotation marks and do not contain decimals.  
  
The following are examples of **integer** constants:  
  
```  
1894  
2  
```  
  
**decimal** constants  
**decimal** constants are represented by a string of numbers that are not enclosed in quotation marks and contain a decimal point.  
  
The following are examples of **decimal** constants:  
  
```  
1894.1204  
2.0  
```  
  
**float** constants  
**float** constants are represented by using scientific notation.  
  
The following are examples of **float** values:  
  
```  
101.5E5  
0.5E-2  
```  
  
## Specifying Negative and Positive Numbers  
To indicate whether a number is positive or negative, apply the **+** or **-** unary operators to a numeric constant. This creates a numeric expression that represents the signed numeric value. Numeric constants use positive expressions when the **+** or **-** unary operators are not applied.  
  
-   Signed **integer** expressions:  
  
    ```  
    +145345234  
    -2147483648  
    ```  
  
-   Signed **decimal** expressions:  
  
    ```  
    +145345234.2234  
    -2147483648.10  
    ```  
  
-   Signed **float** expressions:  
  
    ```  
    +123E-3  
    -12E5  
    ```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Data Types &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/data-types-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/expressions-sql-server-pdw.md)  
[Operators &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/operators-sql-server-pdw.md)  
  
