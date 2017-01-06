---
title: "OR (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: ee4c0ce7-ea4c-463b-b7ee-e347c18b07ca
caps.latest.revision: 17
author: BarbKess
---
# OR (SQL Server PDW)
Combines two conditions. When more than one logical operator is used in a statement, OR operators are evaluated after AND operators. However, you can change the order of evaluation by using parentheses.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
boolean_expression OR boolean_expression  
```  
  
## Arguments  
*boolean_expression*  
Any valid expressions that returns TRUE, FALSE, or UNKNOWN. For more information about the UNKNOWN return value, see [NULL and UNKNOWN &#40;SQL Server PDW&#41;](../sqlpdw/null-and-unknown-sql-server-pdw.md).  
  
## Result Types  
**Boolean**  
  
## Result Value  
OR returns TRUE when either of the conditions is TRUE.  
  
## Remarks  
The following table shows the result of the OR operator.  
  
|Expression1|Expression2|Result|  
|---------------|---------------|----------|  
|TRUE|TRUE|TRUE|  
|TRUE|FALSE|TRUE|  
|TRUE|UNKNOWN|TRUE|  
|FALSE|UNKNOWN|UNKNOWN|  
|FALSE|FALSE|FALSE|  
|UNKNOWN|UNKNOWN|UNKNOWN|  
  
## Examples  
The following example retrieves the names of employees who either earn a `BaseRate` less than 20 or have a `HireDate` January 1, 2001 or later.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT FirstName, LastName, BaseRate, HireDate   
FROM DimEmployee  
WHERE BaseRate < 10 OR HireDate >= '2001-01-01';  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[AND &#40;SQL Server PDW&#41;](../sqlpdw/and-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../sqlpdw/expressions-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[Operators &#40;SQL Server PDW&#41;](../sqlpdw/operators-sql-server-pdw.md)  
[SELECT &#40;SQL Server PDW&#41;](../sqlpdw/select-sql-server-pdw.md)  
[WHERE &#40;SQL Server PDW&#41;](../sqlpdw/where-sql-server-pdw.md)  
  
