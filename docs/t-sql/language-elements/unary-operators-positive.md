---
title: "+ (Unary Plus) (Transact-SQL)"
description: "+ (Unary Plus) (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: ""
ms.date: "03/13/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "+ (positive)"
  - "positive"
helpviewer_keywords:
  - "+ (positive operator)"
  - "positive operator (+)"
  - "positive values [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---

# Unary Operators - Positive

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

Returns the value of a numeric expression (a unary operator). Unary operators perform an operation on only one expression of any one of the data types of the numeric data type category.   
  
|Operator|Meaning|  
|--------------|-------------|  
|[+ (Positive)](../../t-sql/language-elements/unary-operators-positive.md)|Numeric value is positive.|  
|[- (Negative)](../../t-sql/language-elements/unary-operators-negative.md)|Numeric value is negative.|  
|[~ (Bitwise NOT)](../../t-sql/language-elements/bitwise-not-transact-sql.md)|Returns the ones complement of the number.|  
  
 The + (Positive) and - (Negative) operators can be used on any expression of any one of the data types of the numeric data type category. The ~ (Bitwise NOT) operator can be used only on expressions of any one of the data types of the integer data type category.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
+ numeric_expression  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *numeric_expression*  
 Is any valid [expression](../../t-sql/language-elements/expressions-transact-sql.md) of any one of the data types in the numeric data type category, except the **datetime** and **smalldatetime** data types.  
  
## Result Types  
 Returns the data type of *numeric_expression*.  
  
## Remarks  
 Although a unary plus can appear before any numeric expression, it performs no operation on the value returned from the expression. Specifically, it will not return the positive value of a negative expression. To return positive value of a negative expression, use the [ABS](../../t-sql/functions/abs-transact-sql.md) function.  
  
## Examples  
  
### A. Setting a variable to a positive value  
 The following example sets a variable to a positive value.  
  
```sql  
DECLARE @MyNumber DECIMAL(10,2);  
SET @MyNumber = +123.45;  
SELECT @MyNumber;  
GO  
```  
  
 Here is the result set:  
  
```  
-----------   
123.45            
  
(1 row(s) affected)  
```  
  
### B. Using the unary plus operator with a negative value  
 The following example shows using the unary plus with a negative expression and the ABS() function on the same negative expression. The unary plus does not affect the expression, but the ABS function returns the positive value of the expression.  
  
```sql  
USE tempdb;  
GO  
DECLARE @Num1 INT;  
SET @Num1 = -5;  
SELECT +@Num1, ABS(@Num1);  
GO  
```  
  
 Here is the result set:  
  
```  
----------- -----------  
-5          5  
  
(1 row(s) affected)  
```  
  
## See Also  
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)   
 [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)   
 [Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/operators-transact-sql.md)   
 [ABS &#40;Transact-SQL&#41;](../../t-sql/functions/abs-transact-sql.md)  
  
  
