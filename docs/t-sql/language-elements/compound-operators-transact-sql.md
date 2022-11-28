---
title: "Compound Operators (Transact-SQL)"
description: "Compound Operators (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: ""
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
helpviewer_keywords:
  - "compound operators"
  - "compound operators, described"
dev_langs:
  - "TSQL"
---
# Compound Operators (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Compound operators execute some operation and set an original value to the result of the operation. For example, if a variable @x equals 35, then @x += 2 takes the original value of @x, add 2 and sets @x to that new value (37).  
  
 [!INCLUDE[tsql](../../includes/tsql-md.md)] provides the following compound operators:  
  
|Operator|Link to more information|Action|  
|--------------|------------------------------|------------|  
|+=|[+= &#40;Add Assignment&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/add-equals-transact-sql.md)|Adds some amount to the original value and sets the original value to the result.|  
|-=|[-= &#40;Subtract Assignment&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/subtract-equals-transact-sql.md)|Subtracts some amount from the original value and sets the original value to the result.|  
|*=|[&#42;= &#40;Multiply Assignment&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/multiply-equals-transact-sql.md)|Multiplies by an amount and sets the original value to the result.|  
|/=|[&#40;Divide Assignment&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/divide-equals-transact-sql.md)|Divides by an amount and sets the original value to the result.|  
|%=|[Modulus Assignment &#40;Transact-SQL&#41;](../../t-sql/language-elements/modulo-equals-transact-sql.md)|Divides by an amount and sets the original value to the modulo.|  
|&=|[&= &#40;Bitwise AND Assignment&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/bitwise-and-equals-transact-sql.md)|Performs a bitwise AND and sets the original value to the result.|  
|^=|[^= &#40;Bitwise Exclusive OR Assignment&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/bitwise-exclusive-or-equals-transact-sql.md)|Performs a bitwise exclusive OR and sets the original value to the result.|  
|&#124;=|[&#124;= &#40;Bitwise OR Assignment&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/bitwise-or-equals-transact-sql.md)|Performs a bitwise OR and sets the original value to the result.|  
  
## Syntax  
  
```syntaxsql
expression operator expression  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *expression*  
 Is any valid [expression](../../t-sql/language-elements/expressions-transact-sql.md) of any one of the data types in the numeric category.  
  
## Result Types  
 Returns the data type of the argument with the higher precedence. For more information, see [Data Type Precedence &#40;Transact-SQL&#41;](../../t-sql/data-types/data-type-precedence-transact-sql.md).  
  
## Remarks  
 For more information, see the topics related to each operator.  
  
## Examples  
 The following examples demonstrate compound operations.  
  
```sql  
DECLARE @x1 INT = 27;  
SET @x1 += 2 ;  
SELECT @x1 AS Added_2;  
  
DECLARE @x2 INT = 27;  
SET @x2 -= 2 ;  
SELECT @x2 AS Subtracted_2;  
  
DECLARE @x3 INT = 27;  
SET @x3 *= 2 ;  
SELECT @x3 AS Multiplied_by_2;  
  
DECLARE @x4 INT = 27;  
SET @x4 /= 2 ;  
SELECT @x4 AS Divided_by_2;  
  
DECLARE @x5 INT = 27;  
SET @x5 %= 2 ;  
SELECT @x5 AS Modulo_of_27_divided_by_2;  
  
DECLARE @x6 INT = 9;  
SET @x6 &= 13 ;  
SELECT @x6 AS Bitwise_AND;  
  
DECLARE @x7 INT = 27;  
SET @x7 ^= 2 ;  
SELECT @x7 AS Bitwise_Exclusive_OR;  
  
DECLARE @x8 INT = 27;  
SET @x8 |= 2 ;  
SELECT @x8 AS Bitwise_OR;  
```  
  
## See Also  
 [Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/operators-transact-sql.md)   
 [Bitwise Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/bitwise-operators-transact-sql.md)  
  
  
