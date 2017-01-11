---
title: "- (Subtract) (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 900e4a36-4d9b-47f3-878e-de1f541746fd
caps.latest.revision: 19
author: BarbKess
---
# - (Subtract) (SQL Server PDW)
Subtracts two numbers (an arithmetic subtraction operator).  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
expression -expression  
```  
  
## Arguments  
*expression*  
Any valid expression of any one of the data types of the numeric data type category.  
  
## Result Types  
Returns the data type of the argument with the higher precedence. For more information, see Data Type Precedence in the [Data Types &#40;SQL Server PDW&#41;](../sqlpdw/data-types-sql-server-pdw.md) topic.  
  
## Examples  
The following example calculates the difference in a base rate between the employee with the highest base rate and the employee with the lowest tax rate, from the `dimEmployee` table.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT MAX(BaseRate) - MIN(BaseRate) AS BaseRateDifference  
FROM DimEmployee;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Data Types &#40;SQL Server PDW&#41;](../sqlpdw/data-types-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../sqlpdw/expressions-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[SELECT &#40;SQL Server PDW&#41;](../sqlpdw/select-sql-server-pdw.md)  
  
