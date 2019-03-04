---
title: "Precision, scale, and length (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "7/22/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "data types [SQL Server], length"
  - "data types [SQL Server], scale"
  - "precision [SQL Server], data types"
  - "lengths [SQL Server], data types"
  - "number of digits"
  - "scale [SQL Server]"
  - "scale [SQL Server], data types"
  - "data types [SQL Server], precision"
ms.assetid: fbc9ad2c-0d3b-4e98-8fdd-4d912328e40a
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Precision, scale, and Length (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

Precision is the number of digits in a number. Scale is the number of digits to the right of the decimal point in a number. For example, the number 123.45 has a precision of 5 and a scale of 2.
  
In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the default maximum precision of **numeric** and **decimal** data types is 38. In earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the default maximum is 28.
  
Length for a numeric data type is the number of bytes that are used to store the number. Length for a character string or Unicode data type is the number of characters. The length for **binary**, **varbinary**, and **image** data types is the number of bytes. For example, an **int** data type can hold 10 digits, is stored in 4 bytes, and does not accept decimal points. The **int** data type has a precision of 10, a length of 4, and a scale of 0.
  
When two **char**, **varchar**, **binary**, or **varbinary** expressions are concatenated, the length of the resulting expression is the sum of the lengths of the two source expressions or 8,000 characters, whichever is less.
  
When two **nchar** or **nvarchar** expressions are concatenated, the length of the resulting expression is the sum of the lengths of the two source expressions or 4,000 characters, whichever is less.
  
When two expressions of the same data type but different lengths are compared by using UNION, EXCEPT, or INTERSECT, the resulting length is the maximum length of the two expressions.
  
The precision and scale of the numeric data types besides **decimal** are fixed. If an arithmetic operator has two expressions of the same type, the result has the same data type with the precision and scale defined for that type. If an operator has two expressions with different numeric data types, the rules of data type precedence define the data type of the result. The result has the precision and scale defined for its data type.
  
The following table defines how the precision and scale of the result are calculated when the result of an operation is of type **decimal**. The result is **decimal** when either of the following is true:
-   Both expressions are **decimal**.  
-   One expression is **decimal** and the other is a data type with a lower precedence than **decimal**.  
  
The operand expressions are denoted as expression e1, with precision p1 and scale s1, and expression e2, with precision p2 and scale s2. The precision and scale for any expression that is not **decimal** is the precision and scale defined for the data type of the expression. The function max(a,b) means the following: take the greater value of "a" or "b". Similarly, min(a,b) indicates to take the smaller value of "a" or "b".
  
|Operation|Result precision|Result scale *|  
|---|---|---|
|e1 + e2|max(s1, s2) + max(p1-s1, p2-s2) + 1|max(s1, s2)|  
|e1 - e2|max(s1, s2) + max(p1-s1, p2-s2) + 1|max(s1, s2)|  
|e1 * e2|p1 + p2 + 1|s1 + s2|  
|e1 / e2|p1 - s1 + s2 + max(6, s1 + p2 + 1)|max(6, s1 + p2 + 1)|  
|e1 { UNION &#124; EXCEPT &#124; INTERSECT } e2|max(s1, s2) + max(p1-s1, p2-s2)|max(s1, s2)|  
|e1 % e2|min(p1-s1, p2 -s2) + max( s1,s2 )|max(s1, s2)|  
  
\* The result precision and scale have an absolute maximum of 38. When a result precision is greater than 38, it is reduced to 38, and the corresponding scale is reduced to try to prevent the integral part of a result from being truncated. In some cases such as multiplication or division, scale factor will not be reduced in order to keep decimal precision, although the overflow error can be raised.

In addition and subtraction operations we need `max(p1 - s1, p2 - s2)` places to store integral part of the decimal number. If there is not enough space to store them i.e. `max(p1 - s1, p2 - s2) < min(38, precision) - scale`, the scale is reduced to provide enough space for integral part. Resulting scale is `MIN(precision, 38) - max(p1 - s1, p2 - s2)`, so the fractional part might be rounded to fit into the resulting scale.

In multiplication and division operations we need `precision - scale` places to store the integral part of the result. The scale might be reduced using the following rules:
1.  The resulting scale is reduced to `min(scale, 38 - (precision-scale))` if the integral part is less than 32, because it cannot be greater than `38 - (precision-scale)`. Result might be rounded in this case.
1. The scale will not be changed if it is less than 6 and if the integral part is greater than 32. In this case, overflow error might be raised if it cannot fit into decimal(38, scale) 
1. The scale will be set to 6 if it is greater than 6 and if the integral part is greater than 32. In this case, both integral part and scale would be reduced and resulting type is decimal(38,6). Result might be rounded to 6 decimal places or overflow error will be thrown if integral part cannot fit into 32 digits.

## Examples
The following expression returns result `0.00000090000000000` without rounding, because result can fit into `decimal(38,17)`:
```sql
select cast(0.0000009000 as decimal(30,20)) * cast(1.0000000000 as decimal(30,20)) [decimal 38,17]
```
In this case precision is 61, and scale is 40.
Integral part (precision-scale = 21) is less than 32, so this is case (1) in multiplication rules and scale is calculated as `min(scale, 38 - (precision-scale)) = min(40, 38 - (61-40)) = 17`. Result type is `decimal(38,17)`.

The following expression returns result `0.000001` to fit into `decimal(38,6)`:
```sql
select cast(0.0000009000 as decimal(30,10)) * cast(1.0000000000 as decimal(30,10)) [decimal(38, 6)]
```
In this case precision is 61, and scale is 20.
Scale is greater than 6 and integral part (`precision-scale = 41`) is greater than 32. This is case (3) in multiplication rules and result type is `decimal(38,6)`.

## See also
[Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)  
[Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)
  
  
