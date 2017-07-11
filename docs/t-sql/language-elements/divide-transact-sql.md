---
title: "(Divide) (Transact-SQL) | Microsoft Docs"
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
  - "/"
  - "/_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "/ (divide)"
  - "division [SQL Server]"
  - "divide operator (/)"
ms.assetid: 1d69893b-e5c3-441d-8dd8-0e5eb872ecfc
caps.latest.revision: 45
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# (Divide) (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Divides one number by another (an arithmetic division operator).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for SQL Server, Azure SQL Database, Azure SQL Data Warehouse, Parallel Data Warehouse  
  
dividend / divisor  
```  
  
## Arguments  
 *dividend*  
 Is the numeric expression to divide. *dividend* can be any valid [expression](../../t-sql/language-elements/expressions-transact-sql.md) of any one of the data types of the numeric data type category, except the **datetime** and **smalldatetime** data types.  
  
 *divisor*  
 Is the numeric expression by which to divide the dividend. *divisor* can be any valid expression of any one of the data types of the numeric data type category, except the **datetime** and **smalldatetime** data types.  
  
## Result Types  
 Returns the data type of the argument with the higher precedence. For more information, see [Data Type Precedence &#40;Transact-SQL&#41;](../../t-sql/data-types/data-type-precedence-transact-sql.md).  
  
 If an integer *dividend* is divided by an integer *divisor*, the result is an integer that has any fractional part of the result truncated.  
  
## Remarks  
 The actual value returned by the / operator is the quotient of the first expression divided by the second expression.  
  
## Examples  
 The following example uses the division arithmetic operator to calculate the sales target per month for the sales people at [!INCLUDE[ssSampleDBCoFull](../../includes/sssampledbcofull-md.md)].  
  
```  
-- Uses AdventureWorks  
  
SELECT s.BusinessEntityID AS SalesPersonID, FirstName, LastName, SalesQuota, SalesQuota/12 AS 'Sales Target Per Month'  
FROM Sales.SalesPerson AS s   
JOIN HumanResources.Employee AS e   
    ON s.BusinessEntityID = e.BusinessEntityID  
JOIN Person.Person AS p   
    ON e.BusinessEntityID = p.BusinessEntityID;  
```  
  
 Here is a partial result set.  
  
```  
  
SalesPersonID FirstName    LastName          SalesQuota  Sales Target Per Month  
------------- ------------ ----------------- ----------- ------------------  
274           Stephen      Jiang             NULL        NULL  
275           Michael      Blythe            300000.00   25000.00  
276           Linda        Mitchell          250000.00   20833.3333  
277           Jillian      Carson            250000.00   20833.3333  
  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 The following example uses the division arithmetic operator to calculate a simple ratio of each employees’ vacation hours to sick hours.  
  
```  
-- Uses AdventureWorks  
  
SELECT FirstName, LastName, VacationHours/SickLeaveHours AS PersonalTimeRatio  
FROM DimEmployee;  
  
```  
  
## See Also  
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)   
 [Built-in Functions &#40;Transact-SQL&#41;](~/t-sql/functions/functions.md)   
 [Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/operators-transact-sql.md)   
 [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)   
 [WHERE &#40;Transact-SQL&#41;](../../t-sql/queries/where-transact-sql.md)   
 [&#40;Divide EQUALS&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/divide-equals-transact-sql.md)   
 [Compound Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/compound-operators-transact-sql.md)  
  
  


