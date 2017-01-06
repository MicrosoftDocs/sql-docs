---
title: "Divide Operator (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: a9edeae3-9c44-4bf1-8b90-87c82f3704c8
caps.latest.revision: 21
author: BarbKess
---
# Divide Operator (SQL Server PDW)
Divides one number by another (an arithmetic division operator).  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
dividend /divisor  
```  
  
## Arguments  
*dividend*  
Any valid expression of any one of the data types of the numeric data type category.  
  
*divisor*  
Any valid expression of any one of the data types of the numeric data type category.  
  
## Result Types  
Returns the data type of the argument with the higher precedence. For more information, see Data Type Precedence in [Data Types &#40;SQL Server PDW&#41;](../sqlpdw/data-types-sql-server-pdw.md).  
  
## Remarks  
The value returned by the / operator is the quotient of the first expression divided by the second expression.  
  
If an integer *dividend* is divided by an integer *divisor*, the result is an integer that has any fractional part of the result truncated.  
  
## Examples  
The following example uses the division arithmetic operator to calculate a simple ratio of each employees’ vacation hours to sick hours.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT FirstName, LastName, VacationHours/SickLeaveHours AS PersonalTimeRatio  
FROM DimEmployee;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[&#42; &#40;Multiply&#41; &#40;SQL Server PDW&#41;](../sqlpdw/multiply-sql-server-pdw.md)  
[Data Types &#40;SQL Server PDW&#41;](../sqlpdw/data-types-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[Operators &#40;SQL Server PDW&#41;](../sqlpdw/operators-sql-server-pdw.md)  
[SELECT &#40;SQL Server PDW&#41;](../sqlpdw/select-sql-server-pdw.md)  
[WHERE &#40;SQL Server PDW&#41;](../sqlpdw/where-sql-server-pdw.md)  
  
