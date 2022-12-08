---
title: "% (Modulus) (Transact-SQL)"
description: "% (Modulus) (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: ""
ms.date: "03/15/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "modulo"
  - "modulus"
  - "% (Modulo)"
  - "% (Modulus)"
  - "MOD_TSQL"
helpviewer_keywords:
  - "% (modulo operator)"
  - "% (modulus operator)"
  - "remainder of division operation"
  - "modulo operator (%)"
  - "modulus operator (%)"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---

# % (Modulus) (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns the remainder of one number divided by another.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
dividend % divisor  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *dividend*  
 Is the numeric expression to divide. *dividend* must be a valid [expression](../../t-sql/language-elements/expressions-transact-sql.md) of any one of the data types in the integer and monetary data type categories, or the **numeric** data type.  
  
 *divisor*  
 Is the numeric expression by which to divide the dividend. *divisor* must be any valid expression of any one of the data types in the integer and monetary data type categories, or the **numeric** data type.  
  
## Result Types  
 Determined by data types of the two arguments.  
  
## Remarks  
 You can use the modulo arithmetic operator in the select list of the SELECT statement with any combination of column names, numeric constants, or any valid expression of the integer and monetary data type categories or the **numeric** data type.  
  
## Examples  
  
### A. Simple example  
 The following example divides the number 38 by 5. This results in 7 as the integer portion of the result and demonstrates how modulo returns the remainder of 3.  
  
```sql  
SELECT 38 / 5 AS Integer, 38 % 5 AS Remainder;
```  
  
### B. Example using columns in a table  
 The following example returns the product ID number, the unit price of the product, and the modulo (remainder) of dividing the price of each product, converted to an integer value, into the number of products ordered.  
  
```sql  
-- Uses AdventureWorks  
  
SELECT TOP(100)ProductID, UnitPrice, OrderQty,  
   CAST((UnitPrice) AS INT) % OrderQty AS Modulo  
FROM Sales.SalesOrderDetail;  
GO  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### C: Simple example  
 The following example shows results for the `%` operator when dividing 3 by 2.  
  
```sql  
-- Uses AdventureWorks  
  
SELECT TOP(1) 3%2 FROM dimEmployee;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
---------   
1         
```  
  
## See Also  
 [Built-in Functions &#40;Transact-SQL&#41;](~/t-sql/functions/functions.md)   
 [LIKE &#40;Transact-SQL&#41;](../../t-sql/language-elements/like-transact-sql.md)   
 [Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/operators-transact-sql.md)   
 [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)   
 [%= &#40;Modulus Assignment&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/modulo-equals-transact-sql.md)   
 [Compound Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/compound-operators-transact-sql.md)  
  
  


