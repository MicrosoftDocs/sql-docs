---
title: "ABS (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/24/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ABS_TSQL"
  - "ABS"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "values [SQL Server], positive"
  - "values [SQL Server], absolute"
  - "ABS function"
  - "absolute positive value"
ms.assetid: e2ea7a6d-3e2f-472c-afbc-437d3b835c03
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# ABS (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

A mathematical function that returns the absolute (positive) value of the specified numeric expression. (`ABS` changes negative values to positive values. `ABS` has no effect on zero or positive values.)
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
ABS ( numeric_expression )  
```  
  
## Arguments  
*numeric_expression*  
An expression of the exact numeric or approximate numeric data type category.
  
## Return Types  
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
DECLARE @i int;  
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
  
  

