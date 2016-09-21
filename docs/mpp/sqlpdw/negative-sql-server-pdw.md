---
title: "- (Negative) (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: a47fdebe-9325-46a5-a460-19a08431761f
caps.latest.revision: 21
author: BarbKess
---
# - (Negative) (SQL Server PDW)
Reverses the sign of a numeric expression or value (a unary operator).  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
- numeric_expression  
```  
  
## Arguments  
*numeric_expression*  
Any valid expression of any one of the numeric data types.  
  
## Result Types  
Returns the data type of *numeric_expression*, except that an unsigned **tinyint** expression is promoted to a signed **smallint** result.  
  
## Examples  
  
### A. Returning the negative of a positive constant  
The following example returns the negative of a positive constant.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT TOP (1) - 17 FROM DimEmployee;  
```  
  
Returns  
  
```  
-17  
```  
  
### B. Returning the positive of a negative constant  
The following example returns the positive of a negative constant.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT TOP (1) – ( - 17) FROM DimEmployee;  
```  
  
Returns  
  
```  
17  
```  
  
### C. Returning the negative of a column  
The following example returns the negative of the `BaseRate` value for each employee in the `dimEmployee` table.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT - BaseRate FROM DimEmployee;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Data Types &#40;SQL Server PDW&#41;](../sqlpdw/data-types-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../sqlpdw/expressions-sql-server-pdw.md)  
[Operators &#40;SQL Server PDW&#41;](../sqlpdw/operators-sql-server-pdw.md)  
  
