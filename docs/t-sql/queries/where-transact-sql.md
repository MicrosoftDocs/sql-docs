---
title: "WHERE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "WHERE_TSQL"
  - "WHERE"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "retrieving rows"
  - "clauses [SQL Server], WHERE"
  - "WHERE clause, about WHERE clause"
  - "row retrieval [SQL Server], WHERE clause"
  - "WHERE clause"
ms.assetid: a8430421-7bce-4fab-a2d2-56c00a3c6fa4
caps.latest.revision: 37
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# WHERE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Specifies the search condition for the rows returned by the query.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for SQL Server, Azure SQL Database, Azure SQL Data Warehouse, Parallel Data Warehouse  
  
[ WHERE <search_condition> ]  
```  
  
## Arguments  
 < *search_condition* >  
 Defines the condition to be met for the rows to be returned. There is no limit to the number of predicates that can be included in a search condition. For more information about search conditions and predicates, see [Search Condition &#40;Transact-SQL&#41;](../../t-sql/queries/search-condition-transact-sql.md).  
  
## Examples  
 The following examples show how to use some common search conditions in the `WHERE` clause.  
  
### A. Finding a row by using a simple equality  
  
```  
USE AdventureWorks2012  
GO  
SELECT ProductID, Name  
FROM Production.Product  
WHERE Name = 'Blade' ;  
GO  
```  
  
### B. Finding rows that contain a value as a part of a string  
  
```  
SELECT ProductID, Name, Color  
FROM Production.Product  
WHERE Name LIKE ('%Frame%');  
GO  
```  
  
### C. Finding rows by using a comparison operator  
  
```  
SELECT ProductID, Name  
FROM Production.Product  
WHERE ProductID <= 12 ;  
GO  
```  
  
### D. Finding rows that meet any of three conditions  
  
```  
SELECT ProductID, Name  
FROM Production.Product  
WHERE ProductID = 2  
OR ProductID = 4   
OR Name = 'Spokes' ;  
GO  
```  
  
### E. Finding rows that must meet several conditions  
  
```  
SELECT ProductID, Name, Color  
FROM Production.Product  
WHERE Name LIKE ('%Frame%')  
AND Name LIKE ('HL%')  
AND Color = 'Red' ;  
GO  
```  
  
### F. Finding rows that are in a list of values  
  
```  
SELECT ProductID, Name, Color  
FROM Production.Product  
WHERE Name IN ('Blade', 'Crown Race', 'Spokes');  
GO  
```  
  
### G. Finding rows that have a value between two values  
  
```  
SELECT ProductID, Name, Color  
FROM Production.Product  
WHERE ProductID BETWEEN 725 AND 734;  
GO  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 The following examples show how to use some common search conditions in the `WHERE` clause.  
  
### H. Finding a row by using a simple equality  
  
```  
-- Uses AdventureWorks  
  
SELECT EmployeeKey, LastName  
FROM DimEmployee  
WHERE LastName = 'Smith' ;  
```  
  
### I. Finding rows that contain a value as part of a string  
  
```  
-- Uses AdventureWorks  
  
SELECT EmployeeKey, LastName  
FROM DimEmployee  
WHERE LastName LIKE ('%Smi%');  
```  
  
### J. Finding rows by using a comparison operator  
  
```  
-- Uses AdventureWorks  
  
SELECT EmployeeKey, LastName  
FROM DimEmployee  
WHERE EmployeeKey  <= 500;  
```  
  
### K. Finding rows that meet any of three conditions  
  
```  
-- Uses AdventureWorks  
  
SELECT EmployeeKey, LastName  
FROM DimEmployee  
WHERE EmployeeKey = 1 OR EmployeeKey = 8 OR EmployeeKey = 12;  
```  
  
### L. Finding rows that must meet several conditions  
  
```  
-- Uses AdventureWorks  
  
SELECT EmployeeKey, LastName  
FROM DimEmployee  
WHERE EmployeeKey <= 500 AND LastName LIKE '%Smi%' AND FirstName LIKE '%A%';  
```  
  
### M. Finding rows that are in a list of values  
  
```  
-- Uses AdventureWorks  
  
SELECT EmployeeKey, LastName  
FROM DimEmployee  
WHERE LastName IN ('Smith', 'Godfrey', 'Johnson');  
```  
  
### N. Finding rows that have a value between two values  
  
```  
-- Uses AdventureWorks  
  
SELECT EmployeeKey, LastName  
FROM DimEmployee  
WHERE EmployeeKey Between 100 AND 200;  
```  
  
## See Also  
 [DELETE &#40;Transact-SQL&#41;](../../t-sql/statements/delete-transact-sql.md)   
 [Predicates &#40;Transact-SQL&#41;](~/t-sql/queries/predicates.md)   
 [Search Condition &#40;Transact-SQL&#41;](../../t-sql/queries/search-condition-transact-sql.md)   
 [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)   
 [UPDATE &#40;Transact-SQL&#41;](../../t-sql/queries/update-transact-sql.md)   
 [MERGE &#40;Transact-SQL&#41;](../../t-sql/statements/merge-transact-sql.md)  
  
  


