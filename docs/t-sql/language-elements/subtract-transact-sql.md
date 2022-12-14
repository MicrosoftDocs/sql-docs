---
title: "- (Subtraction) (Transact-SQL)"
description: "- (Subtraction) (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: ""
ms.date: "03/15/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "subtract"
  - "-"
  - "-_TSQL"
helpviewer_keywords:
  - "- (subtract)"
  - "subtract operator (-)"
  - "minus operator (-)"
  - "subtracting numbers"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---

# - (Subtraction) (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Subtracts two numbers (an arithmetic subtraction operator). Can also subtract a number, in days, from a date.  
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
expression - expression  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *expression*  
 Is any valid [expression](../../t-sql/language-elements/expressions-transact-sql.md) of any one of the data types of the numeric data type category, except the **bit** data type. Cannot be used with **date**, **time**, **datetime2**, or **datetimeoffset** data types.  
  
## Result Types  
 Returns the data type of the argument with the higher precedence. For more information, see [Data Type Precedence &#40;Transact-SQL&#41;](../../t-sql/data-types/data-type-precedence-transact-sql.md).  
  
## Examples  
  
### A. Using subtraction in a SELECT statement  
 The following example calculates the difference in tax rate between the state or province with the highest tax rate and the state or province with the lowest tax rate.  
  
 **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssSDS](../../includes/sssds-md.md)].  
  
```sql  
-- Uses AdventureWorks  
  
SELECT MAX(TaxRate) - MIN(TaxRate) AS 'Tax Rate Difference'  
FROM Sales.SalesTaxRate  
WHERE StateProvinceID IS NOT NULL;  
GO  
```  
  
 You can change the order of execution by using parentheses. Calculations inside parentheses are evaluated first. If parentheses are nested, the most deeply nested calculation has precedence.  
  
### B. Using date subtraction  
 The following example subtracts a number of days from a `datetime` date.  
  
 Applies to: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssSDS](../../includes/sssds-md.md)].  
  
```sql  
-- Uses the AdventureWorks sample database
DECLARE @altstartdate DATETIME;  
SET @altstartdate = CONVERT(DATETIME, 'January 10, 1900 3:00 AM', 101);  
SELECT @altstartdate - 1.5 AS 'Subtract Date';  
```  
  
 Here is the result set:  
  
 ```
 Subtract Date  
 -----------------------  
 1900-01-08 15:00:00.000  

 (1 row(s) affected)
 ```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### C: Using subtraction in a SELECT statement  
 The following example calculates the difference in a base rate between the employee with the highest base rate and the employee with the lowest tax rate, from the `dimEmployee` table.  
  
```sql  
-- Uses AdventureWorks  
  
SELECT MAX(BaseRate) - MIN(BaseRate) AS BaseRateDifference  
FROM DimEmployee;  
```  
  
## See Also  
 [-= &#40;Subtraction Assignment&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/subtract-equals-transact-sql.md)   
 [Compound Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/compound-operators-transact-sql.md)  
 [Arithmetic Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/arithmetic-operators-transact-sql.md)   
 [- &#40;Negative&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/unary-operators-negative.md)   
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)   
 [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)   
 [Built-in Functions &#40;Transact-SQL&#41;](~/t-sql/functions/functions.md)   
 [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)   
  
  


