---
title: "Modulo (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: fbbd4ac6-41dd-48f9-94bd-b301641b2916
caps.latest.revision: 21
author: BarbKess
---
# Modulo (SQL Server PDW)
Uses the % operator to returns the integer remainder of a division. For example, 12 % 5 = 2 because the remainder of 12 divided by 5 is 2.  
  
> [!NOTE]  
> For information about using % as a wildcard character, see [Wildcard Character&#40;s&#41; to Match &#40;SQL Server PDW&#41;](../sqlpdw/wildcard-character-s-to-match-sql-server-pdw.md).  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
numerator  %  denominator  
```  
  
## Arguments  
*numerator*  
An expression or constant of the numerator. Can be an exact numeric or approximate numeric data type category.  
  
*denominator*  
An expression or constant of the denominator. Can be an exact numeric or approximate numeric data type category.  
  
## Return Types  
**int**  
  
## Examples  
The following example shows results for the `%` operator when dividing 3 by 2.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT TOP(1) 3%2 FROM dimEmployee;  
```  
  
Here is the result set.  
  
```  
---------   
1  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Divide Operator &#40;SQL Server PDW&#41;](../sqlpdw/divide-operator-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../sqlpdw/expressions-sql-server-pdw.md)  
[Constants &#40;SQL Server PDW&#41;](../sqlpdw/constants-sql-server-pdw.md)  
[Data Types &#40;SQL Server PDW&#41;](../sqlpdw/data-types-sql-server-pdw.md)  
  
