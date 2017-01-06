---
title: "IS [NOT] NULL (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: c95959f4-0dda-44cc-9c1c-1a63a5a5078b
caps.latest.revision: 18
author: BarbKess
---
# IS [NOT] NULL (SQL Server PDW)
Determines whether a specified expression is null.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
expression IS [ NOT ] NULL  
```  
  
## Arguments  
*expression*  
Any valid expression.  
  
NOT  
Negates the Boolean result. The predicate reverses its return values, returning TRUE if the value is not null, and FALSE if the value is NULL.  
  
## Result Types  
**Boolean**  
  
## Return Values  
If the value of *expression* is null, IS NULL returns TRUE; otherwise, it returns FALSE.  
  
If the value of *expression* is null, IS NOT NULL returns FALSE; otherwise, it returns TRUE.  
  
To replace NULL values with a specified value, use [ISNULL](../sqlpdw/isnull-sql-server-pdw.md).  
  
## Remarks  
To determine whether an expression is null, use IS NULL or IS NOT NULL instead of comparison operators (such as = or <>). Comparison operators return UNKNOWN when either or both arguments are null.  
  
## Examples  
The following example returns the full names of all employees with middle initials.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT FirstName, LastName, MiddleName  
FROM DIMEmployee  
WHERE MiddleName IS NOT NULL  
ORDER BY LastName DESC;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[CASE &#40;SQL Server PDW&#41;](../sqlpdw/case-sql-server-pdw.md)  
[Data Types &#40;SQL Server PDW&#41;](../sqlpdw/data-types-sql-server-pdw.md)  
[NULL and UNKNOWN &#40;SQL Server PDW&#41;](../sqlpdw/null-and-unknown-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../sqlpdw/expressions-sql-server-pdw.md)  
[LIKE &#40;SQL Server PDW&#41;](../sqlpdw/like-sql-server-pdw.md)  
[Operators &#40;SQL Server PDW&#41;](../sqlpdw/operators-sql-server-pdw.md)  
[WHERE &#40;SQL Server PDW&#41;](../sqlpdw/where-sql-server-pdw.md)  
  
