---
title: "+ (Add) (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: b8d49f7e-223f-4fc2-bf53-d6c11ba59d05
caps.latest.revision: 22
author: BarbKess
---
# + (Add) (SQL Server PDW)
Adds two numbers (an arithmetic addition operator).  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
expression +expression  
```  
  
## Arguments  
*expression*  
Any valid expression of any one of the data types in the numeric category.  
  
## Result Types  
Returns the data type of the argument with the higher precedence. For more information, see Data Type Precedence in the [Data Types &#40;SQL Server PDW&#41;](../sqlpdw/data-types-sql-server-pdw.md) topic.  
  
## Examples  
  
### Using the addition operator to calculate the total number of hours away from work for each employee  
The following example finds the total number of hours away from work for each employee by adding the number of hours taken for vacation and the number of hours taken as sick leave and sorts the results in ascending order.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT FirstName, LastName, VacationHours, SickLeaveHours,   
    VacationHours + SickLeaveHours AS TotalHoursAway  
FROM DimEmployee  
ORDER BY TotalHoursAway ASC;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Operators &#40;SQL Server PDW&#41;](../sqlpdw/operators-sql-server-pdw.md)  
[CAST and CONVERT &#40;SQL Server PDW&#41;](../sqlpdw/cast-and-convert-sql-server-pdw.md)  
[Data Types &#40;SQL Server PDW&#41;](../sqlpdw/data-types-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[SELECT &#40;SQL Server PDW&#41;](../sqlpdw/select-sql-server-pdw.md)  
  
