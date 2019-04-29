---
title: "Operator Precedence (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "operators [Transact-SQL], precedence"
  - "operator precedence [Transact-SQL]"
  - "order of operator execution [Transact-SQL]"
  - "precedence [SQL Server], operators"
ms.assetid: f04d2439-6fff-4e4c-801f-cc62faef510a
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Operator Precedence (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  When a complex expression has multiple operators, operator precedence determines the sequence of operations. The order of execution can significantly affect the resulting value.  
  
 Operators have the precedence levels shown in the following table. An operator on higher levels is evaluated before an operator on a lower level. In the following table, 1 is the highest level and 8 is the lowest level.
  
|Level|Operators|  
|-----------|---------------|  
|1|~ (Bitwise NOT)|  
|2|* (Multiplication), / (Division), % (Modulus)|  
|3|+ (Positive), - (Negative), + (Addition), + (Concatenation), - (Subtraction), & (Bitwise AND), ^ (Bitwise Exclusive OR), &#124; (Bitwise OR)|  
|4|=, >, \<, >=, <=, <>, !=, !>, !< (Comparison operators)|  
|5|NOT|  
|6|AND|  
|7|ALL, ANY, BETWEEN, IN, LIKE, OR, SOME|  
|8|= (Assignment)|  
  
 When two operators in an expression have the same precedence level, they're evaluated left to right based on their position in the expression. For example, in the expression that is used in the following `SET` statement, the subtraction operator is evaluated before the addition operator.  
  
```sql  
DECLARE @MyNumber int;  
SET @MyNumber = 4 - 2 + 27;  
-- Evaluates to 2 + 27 which yields an expression result of 29.  
SELECT @MyNumber;  
```  
  
 Use parentheses to override the defined precedence of the operators in an expression. Everything within parentheses is evaluated to yield a single value. That value can be used by any operator outside those parentheses.  
  
 For example, in the expression used in the following `SET` statement, the multiplication operator has a higher precedence than the addition operator. The multiplication operation is evaluated first; the expression result is `13`.  
  
```sql  
DECLARE @MyNumber int;  
SET @MyNumber = 2 * 4 + 5;  
-- Evaluates to 8 + 5 which yields an expression result of 13.  
SELECT @MyNumber;  
```  
  
 In the expression used in the following `SET` statement, the parentheses cause the addition to be evaluated first. The expression result is `18`.  
  
```sql  
DECLARE @MyNumber int;  
SET @MyNumber = 2 * (4 + 5);  
-- Evaluates to 2 * 9 which yields an expression result of 18.  
SELECT @MyNumber;  
```  
  
 If an expression has nested parentheses, the most deeply nested expression is evaluated first. The following example contains nested parentheses, with the expression `5 - 3` in the most deeply nested set of parentheses. This expression yields a value of `2`. Then, the addition operator (`+`) adds this result to `4` which yields a value of `6`. Finally, the `6` is multiplied by `2` to yield an expression result of `12`.  
  
```sql  
DECLARE @MyNumber int;  
SET @MyNumber = 2 * (4 + (5 - 3) );  
-- Evaluates to 2 * (4 + 2) which then evaluates to 2 * 6, and   
-- yields an expression result of 12.  
SELECT @MyNumber;  
```  
  
## See Also  
 [Logical Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/logical-operators-transact-sql.md)   
 [Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/operators-transact-sql.md)   
 [Built-in Functions &#40;Transact-SQL&#41;](~/t-sql/functions/functions.md)  
  
