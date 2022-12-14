---
title: "ABS (Transact-SQL)"
description: "ABS (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "07/24/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ABS_TSQL"
  - "ABS"
helpviewer_keywords:
  - "values [SQL Server], positive"
  - "values [SQL Server], absolute"
  - "ABS function"
  - "absolute positive value"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---

# ABS (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

A mathematical function that returns the absolute (positive) value of the specified numeric expression. (`ABS` changes negative values to positive values. `ABS` has no effect on zero or positive values.)
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```syntaxsql
ABS ( numeric_expression )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

*numeric_expression*  
An expression of the exact numeric or approximate numeric data type category.
  
## Return types


Returns the same type as *numeric_expression*.
  
## Examples

This example shows the results of using the `ABS` function on three different numbers.
  
```sql
SELECT ABS(-1.0), ABS(0.0), ABS(1.0);  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]

```sql
---- ---- ----  
1.0  .0   1.0  
```  
  
The `ABS` function can produce an overflow error when the absolute value of a number exceeds the largest number that the specified data type can represent. For example, the `int` data type has a value range from `-2,147,483,648` to `2,147,483,647`. Computing the absolute value for the signed integer `-2,147,483,648` will cause an overflow error because its absolute value exceeds the positive range limit for the `int` data type.
  
```sql
DECLARE @i INT;  
SET @i = -2147483648;  
SELECT ABS(@i);  
GO  
```  
  
Returns this error message:
  
"Msg 8115, Level 16, State 2, Line 3"
  
"Arithmetic overflow error converting expression to data type int."

## See also

[CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)  
[Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)  
[Mathematical Functions &#40;Transact-SQL&#41;](../../t-sql/functions/mathematical-functions-transact-sql.md)  
[Built-in Functions &#40;Transact-SQL&#41;](../../t-sql/functions/functions.md)
