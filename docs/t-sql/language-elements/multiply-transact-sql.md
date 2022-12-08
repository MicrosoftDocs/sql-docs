---
title: "* (Multiplication) (Transact-SQL)"
description: "* (Multiplication) (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: ""
ms.date: "03/15/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "*_TSQL"
  - "*"
helpviewer_keywords:
  - "* (multiply operator)"
  - "multiplication [SQL Server]"
  - "multiply operator (*)"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---

# * (Multiplication) (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Multiplies two expressions (an arithmetic multiplication operator).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
expression * expression  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *expression*  
 Is any valid [expression](../../t-sql/language-elements/expressions-transact-sql.md) of any one of the data types of the numeric data type category, except the **datetime** and **smalldatetime** data types.  
  
## Result Types  
 Returns the data type of the argument with the higher precedence. For more information, see [Data Type Precedence &#40;Transact-SQL&#41;](../../t-sql/data-types/data-type-precedence-transact-sql.md).  
  
## Examples  
 The following example retrieves the product identification number, name, the list price and the new list price of all the mountain bicycles in the `Product` table. The new list price is calculated by using the `*` arithmetic operator to multiply `ListPrice` by `1.15`.  
  
```sql  
-- Uses AdventureWorks  
  
SELECT ProductID, Name, ListPrice, ListPrice * 1.15 AS NewPrice  
FROM Production.Product  
WHERE Name LIKE 'Mountain-%'  
ORDER BY ProductID ASC;  
GO  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 The following example retrieves the first and last name of employees in the `dimEmployee` table, and calculates the pay for `VacationHours` for each..  
  
```sql  
-- Uses AdventureWorks  
  
SELECT FirstName, LastName, BaseRate * VacationHours AS VacationPay  
FROM DimEmployee  
ORDER BY lastName ASC;  
```  
  
## See Also  
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)   
 [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)   
 [Built-in Functions &#40;Transact-SQL&#41;](~/t-sql/functions/functions.md)   
 [Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/operators-transact-sql.md)   
 [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)   
 [WHERE &#40;Transact-SQL&#41;](../../t-sql/queries/where-transact-sql.md)   
 [&#42;= &#40;Multiplication Assignment&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/multiply-equals-transact-sql.md)   
 [Compound Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/compound-operators-transact-sql.md)  
  
  


