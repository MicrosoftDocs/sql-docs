---
title: "BETWEEN (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "BETWEEN"
  - "BETWEEN_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "inclusive ranges"
  - "testing range"
  - "exclusive ranges"
  - "NOT BETWEEN operator"
  - "BETWEEN operator"
  - "range to test [SQL Server]"
ms.assetid: a5d5b050-203e-4355-ac85-e08ef5ca7823
caps.latest.revision: 34
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# BETWEEN (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Specifies a range to test.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for SQL Server, Azure SQL Database, Azure SQL Data Warehouse, Parallel Data Warehouse  
  
test_expression [ NOT ] BETWEEN begin_expression AND end_expression  
```  
  
## Arguments  
 *test_expression*  
 Is the [expression](../../t-sql/language-elements/expressions-transact-sql.md) to test for in the range defined by *begin_expression*and *end_expression*. *test_expression* must be the same data type as both *begin_expression* and *end_expression*.  
  
 NOT  
 Specifies that the result of the predicate be negated.  
  
 *begin_expression*  
 Is any valid expression. *begin_expression* must be the same data type as both *test_expression* and *end_expression*.  
  
 *end_expression*  
 Is any valid expression. *end_expression* must be the same data type as both *test_expression*and *begin_expression*.  
  
 AND  
 Acts as a placeholder that indicates *test_expression* should be within the range indicated by *begin_expression* and *end_expression*.  
  
## Result Types  
 **Boolean**  
  
## Result Value  
 BETWEEN returns **TRUE** if the value of *test_expression* is greater than or equal to the value of *begin_expression* and less than or equal to the value of *end_expression*.  
  
 NOT BETWEEN returns **TRUE** if the value of *test_expression* is less than the value of *begin_expression* or greater than the value of *end_expression*.  
  
## Remarks  
 To specify an exclusive range, use the greater than (>) and less than operators (<). If any input to the BETWEEN or NOT BETWEEN predicate is NULL, the result is UNKNOWN.  
  
## Examples  
  
### A. Using BETWEEN  
 The following example returns the employees of [!INCLUDE[ssSampleDBCoFull](../../includes/sssampledbcofull-md.md)] that have an hourly pay rate between `27` and `30`.  
  
```  
-- Uses AdventureWorks  
  
SELECT e.FirstName, e.LastName, ep.Rate  
FROM HumanResources.vEmployee e   
JOIN HumanResources.EmployeePayHistory ep   
    ON e.BusinessEntityID = ep.BusinessEntityID  
WHERE ep.Rate BETWEEN 27 AND 30  
ORDER BY ep.Rate;  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 `FirstName    LastName            Rate`  
  
 `-----------  ------------------  ------------------`  
  
 `Paula        Barreto de Mattos   27.1394`  
  
 `Karen        Berg                27.4038`  
  
 `Ramesh       Meyyappan           27.4038`  
  
 `Dan          Bacon               27.4038`  
  
 `Janaina      Bueno               27.4038`  
  
 `David        Bradley             28.7500`  
  
 `Hazem        Abolrous            28.8462`  
  
 `Ovidiu       Cracium             28.8462`  
  
 `Rob          Walters             29.8462`  
  
 `Sheela       Word                30.0000`  
  
 `(10 row(s) affected)`  
  
### B. Using > and < instead of BETWEEN  
 The following example uses greater than (`>`) and less than (`<`) operators and, because these operators are not inclusive, returns nine rows instead of ten that were returned in the previous example.  
  
```  
-- Uses AdventureWorks  
  
SELECT e.FirstName, e.LastName, ep.Rate  
FROM HumanResources.vEmployee e   
JOIN HumanResources.EmployeePayHistory ep   
    ON e.BusinessEntityID = ep.BusinessEntityID  
WHERE ep.Rate > 27 AND ep.Rate < 30  
ORDER BY ep.Rate;  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 `FirstName   LastName             Rate`  
  
 `---------   -------------------  ---------`  
  
 `Paula       Barreto de Mattos    27.1394`  
  
 `Janaina     Bueno                27.4038`  
  
 `Dan         Bacon                27.4038`  
  
 `Ramesh      Meyyappan            27.4038`  
  
 `Karen       Berg                 27.4038`  
  
 `David       Bradley              28.7500`  
  
 `Hazem       Abolrous             28.8462`  
  
 `Ovidiu      Cracium              28.8462`  
  
 `Rob         Walters              29.8462`  
  
 `(9 row(s) affected)`  
  
### C. Using NOT BETWEEN  
 The following example finds all rows outside a specified range of `27` through `30`.  
  
```  
-- Uses AdventureWorks  
  
SELECT e.FirstName, e.LastName, ep.Rate  
FROM HumanResources.vEmployee e   
JOIN HumanResources.EmployeePayHistory ep   
    ON e.BusinessEntityID = ep.BusinessEntityID  
WHERE ep.Rate NOT BETWEEN 27 AND 30  
ORDER BY ep.Rate;  
GO  
```  
  
### D. Using BETWEEN with datetime values  
 The following example retrieves rows in which **datetime** values are between `'20011212'` and `'20020105'`, inclusive.  
  
```  
-- Uses AdventureWorks  
  
SELECT BusinessEntityID, RateChangeDate  
FROM HumanResources.EmployeePayHistory  
WHERE RateChangeDate BETWEEN '20011212' AND '20020105';  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 `BusinessEntityID RateChangeDate`  
  
 `----------- -----------------------`  
  
 `3           2001-12-12 00:00:00.000`  
  
 `4           2002-01-05 00:00:00.000`  
  
 The query retrieves the expected rows because the date values in the query and the **datetime** values stored in the `RateChangeDate` column have been specified without the time part of the date. When the time part is unspecified, it defaults to 12:00 A.M. Note that a row that contains a time part that is after 12:00 A.M. on 2002-01-05 would not be returned by this query because it falls outside the range.  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### E. Using BETWEEN  
 The following example returns the employees of a company that have an hourly pay rate between `27` and `30`, inclusive.  
  
```  
-- Uses AdventureWorks  
  
SELECT FirstName, LastName, BaseRate  
FROM dimEmployee  
WHERE BaseRate BETWEEN 27 AND 30  
ORDER BY BaseRate DESC;  
```  
  
### F. Using >= and <= instead of BETWEEN  
 The following example uses the greater than or equal to (`>=`) and less than or equal to (`<=`) operators to perform the same query as the BETWEEN query above.  
  
```  
-- Uses AdventureWorks  
  
SELECT FirstName, LastName, BaseRate  
FROM dimEmployee  
WHERE BaseRate >= 27 AND BaseRate <= 30  
ORDER BY BaseRate DESC;  
```  
  
### G. Using NOT BETWEEN  
 The following example finds all rows outside the range of `27` through `30`.  
  
```  
-- Uses AdventureWorks  
  
SELECT FirstName, LastName, BaseRate  
FROM dimEmployee  
WHERE BaseRate NOT BETWEEN 27 AND 30  
ORDER BY BaseRate DESC;  
```  
  
### H. Using BETWEEN with date values  
 The following example retrieves employees with **BirthDate** values are between `'1950-01-01'` and `'1969-12-31'`, inclusive.  
  
```  
-- Uses AdventureWorks  
  
SELECT FirstName, LastName, BirthDate  
FROM dimEmployee  
WHERE BirthDate BETWEEN '1950-01-01' AND '1969-12-31'  
ORDER BY BirthDate ASC;  
```  
  
## See Also  
 [&#62; &#40;Greater Than&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/greater-than-transact-sql.md)   
 [&#60; &#40;Less Than&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/less-than-transact-sql.md)   
 [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)   
 [Built-in Functions &#40;Transact-SQL&#41;](~/t-sql/functions/functions.md)   
 [Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/operators-transact-sql.md)   
 [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)   
 [WHERE &#40;Transact-SQL&#41;](../../t-sql/queries/where-transact-sql.md)  
  
  


