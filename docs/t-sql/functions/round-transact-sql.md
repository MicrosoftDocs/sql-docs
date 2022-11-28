---
title: "ROUND (Transact-SQL)"
description: "ROUND (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: ""
ms.date: "12/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "ROUND_TSQL"
  - "ROUND"
helpviewer_keywords:
  - "rounding expressions"
  - "ROUND function [Transact-SQL]"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# ROUND (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Returns a numeric value, rounded to the specified length or precision.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
ROUND ( numeric_expression , length [ ,function ] )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *numeric_expression*  
 Is an [expression](../../t-sql/language-elements/expressions-transact-sql.md) of the exact numeric or approximate numeric data type category, except for the **bit** data type.  
  
 *length*  
 Is the precision to which *numeric_expression* is to be rounded. *length* must be an expression of type **tinyint**, **smallint**, or **int**. When *length* is a positive number, *numeric_expression* is rounded to the number of decimal positions specified by *length*. When *length* is a negative number, *numeric_expression* is rounded on the left side of the decimal point, as specified by *length*.  
  
 *function*  
 Is the type of operation to perform. *function* must be **tinyint**, **smallint**, or **int**. When *function* is omitted or has a value of 0 (default), *numeric_expression* is rounded. When a value other than 0 is specified, *numeric_expression* is truncated.  
  
## Return Types  
 Returns the following data types.  
  
|Expression result|Return type|  
|-----------------------|-----------------|  
|**tinyint**|**int**|  
|**smallint**|**int**|  
|**int**|**int**|  
|**bigint**|**bigint**|  
|**decimal** and **numeric** category (p, s)|**decimal(p, s)**|  
|**money** and **smallmoney** category|**money**|  
|**float** and **real** category|**float**|  
  
## Remarks  
 ROUND always returns a value. If *length* is negative and larger than the number of digits before the decimal point, ROUND returns 0.  
  
|Example|Result|  
|-------------|------------|  
|ROUND(748.58, -4)|0|  
  
 ROUND returns a rounded *numeric_expression*, regardless of data type, when *length* is a negative number.  
  
|Examples|Result|  
|--------------|------------|  
|ROUND(748.58, -1)|750.00|  
|ROUND(748.58, -2)|700.00|  
|ROUND(748.58, -3)|Results in an arithmetic overflow, because 748.58 defaults to decimal(5,2), which cannot return 1000.00.|  
|To round up to 4 digits, change the data type of the input. For example:<br /><br /> `SELECT ROUND(CAST (748.58 AS decimal (6,2)),-3);`|1000.00|  
  
## Examples  
  
### A. Using ROUND and estimates  
 The following example shows two expressions that demonstrate by using `ROUND` the last digit is always an estimate.  
  
```sql  
SELECT ROUND(123.9994, 3), ROUND(123.9995, 3);  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
----------- -----------  
123.9990    124.0000      
```  
  
### B. Using ROUND and rounding approximations  
 The following example shows rounding and approximations.  
  
```  
SELECT ROUND(123.4545, 2), ROUND(123.45, -2);  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  

 ```
----------  ----------
123.4500    100.00
```
  
### C. Using ROUND to truncate  
 The following example uses two `SELECT` statements to demonstrate the difference between rounding and truncation. The first statement rounds the result. The second statement truncates the result.  
  
```sql  
SELECT ROUND(150.75, 0);  
GO  
SELECT ROUND(150.75, 0, 1);  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
--------  
151.00  
  
(1 row(s) affected)  
  
--------  
150.00  
  
(1 row(s) affected)  
```
  
## See Also  
 [CEILING &#40;Transact-SQL&#41;](../../t-sql/functions/ceiling-transact-sql.md)   
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)   
 [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)   
 [FLOOR &#40;Transact-SQL&#41;](../../t-sql/functions/floor-transact-sql.md)   
 [Mathematical Functions &#40;Transact-SQL&#41;](../../t-sql/functions/mathematical-functions-transact-sql.md)
