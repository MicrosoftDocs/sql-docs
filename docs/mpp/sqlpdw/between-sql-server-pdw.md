---
title: "BETWEEN (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: f41b1c26-c138-47b1-9b61-6e32ee230345
caps.latest.revision: 19
author: BarbKess
---
# BETWEEN (SQL Server PDW)
Specifies a range to test.  
  
BETWEEN returns TRUE if the value of *test_expression* is greater than or equal to the value of *begin_expression* and less than or equal to the value of *end_expression*.  
  
NOT BETWEEN returns TRUE if the value of *test_expression* is less than the value of *begin_expression* or greater than the value of *end_expression*.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
test_expression [ NOT ] BETWEEN begin_expression AND end_expression  
```  
  
## Arguments  
*test_expression*  
The expression to test for in the range defined by *begin_expression*and *end_expression*. *test_expression* must be the same data type as both *begin_expression* and *end_expression*.  
  
NOT  
Negates the result of the predicate.  
  
*begin_expression*  
Any valid expression. *begin_expression* must be the same data type as both *test_expression* and *end_expression*.  
  
*end_expression*  
Any valid expression. *end_expression* must be the same data type as both *test_expression*and *begin_expression*.  
  
AND  
Acts as a placeholder that indicates *test_expression* should be within the range indicated by *begin_expression* and *end_expression*.  
  
## Result Types  
**Boolean**  
  
## Remarks  
To specify an exclusive range, use the greater-than (>) and less-than operators (<). If any input to the BETWEEN or NOT BETWEEN predicate is NULL, the result is UNKNOWN.  
  
## Examples  
  
### A. Using BETWEEN  
The following example returns the employees of a company that have an hourly pay rate between `27` and `30`, inclusive.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT FirstName, LastName, BaseRate  
FROM dimEmployee  
WHERE BaseRate BETWEEN 27 AND 30  
ORDER BY BaseRate DESC;  
```  
  
### B. Using >= and <= instead of BETWEEN  
The following example uses the greater than or equal to (`>=`) and less than or equal to (`<=`) operators to perform the same query as the BETWEEN query above.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT FirstName, LastName, BaseRate  
FROM dimEmployee  
WHERE BaseRate >= 27 AND BaseRate <= 30  
ORDER BY BaseRate DESC;  
```  
  
### C. Using NOT BETWEEN  
The following example finds all rows outside the range of `27` through `30`.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT FirstName, LastName, BaseRate  
FROM dimEmployee  
WHERE BaseRate NOT BETWEEN 27 AND 30  
ORDER BY BaseRate DESC;  
```  
  
### D. Using BETWEEN with date values  
The following example retrieves employees with **BirthDate** values are between `'1950-01-01'` and `'1969-12-31'`, inclusive.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT FirstName, LastName, BirthDate  
FROM dimEmployee  
WHERE BirthDate BETWEEN '1950-01-01' AND '1969-12-31'  
ORDER BY BirthDate ASC;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[&#62; &#40;Greater Than&#41; &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/greater-than-sql-server-pdw.md)  
[&#60; &#40;Less Than&#41; &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/less-than-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/expressions-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/functions-sql-server-pdw.md)  
[Operators &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/operators-sql-server-pdw.md)  
[SELECT &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/select-sql-server-pdw.md)  
[WHERE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/where-sql-server-pdw.md)  
  
