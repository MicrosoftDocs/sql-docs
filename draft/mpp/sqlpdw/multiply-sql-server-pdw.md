---
title: "* (Multiply) (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 8a330a72-0ad3-480f-872e-9b170b786ffd
caps.latest.revision: 19
author: BarbKess
---
# * (Multiply) (SQL Server PDW)
Multiplies two expressions (an arithmetic multiplication operator).  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
expression *expression  
```  
  
## Arguments  
*expression*  
Any valid expression of any one of the data types of the numeric data type category.  
  
## Result Types  
Returns the data type of the argument with the higher precedence. For more information, see Data Type Precedence in [Data Types &#40;SQL Server PDW&#41;](../sqlpdw/data-types-sql-server-pdw.md).  
  
## Examples  
The following example retrieves the first and last name of employees in the `dimEmployee` table, and calculates the pay for `VacationHours` for each..  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT FirstName, LastName, BaseRate * VacationHours AS VacationPay  
FROM DimEmployee  
ORDER BY lastName ASC;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Data Types &#40;SQL Server PDW&#41;](../sqlpdw/data-types-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../sqlpdw/expressions-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[Operators &#40;SQL Server PDW&#41;](../sqlpdw/operators-sql-server-pdw.md)  
[SELECT &#40;SQL Server PDW&#41;](../sqlpdw/select-sql-server-pdw.md)  
[WHERE &#40;SQL Server PDW&#41;](../sqlpdw/where-sql-server-pdw.md)  
  
