---
title: "ASIN (Transact-SQL)"
description: "ASIN (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "07/24/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ASIN_TSQL"
  - "ASIN"
helpviewer_keywords:
  - "ASIN function"
  - "sine"
  - "arcsine"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# ASIN (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

A function that returns the angle, in radians, whose sine is the specified **float** expression. This is also called **arcsine**.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```syntaxsql
ASIN ( float_expression )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
*float_expression*  
An [expression](../../t-sql/language-elements/expressions-transact-sql.md) of either type **float** or of a type that can implicitly convert to float. Only a value ranging from -1.00 to 1.00 is valid. For values outside this range, no value is returned, and ASIN will report a domain error.
  
## Return types
**float**
  
## Examples  
This example takes a **float** expression and returns the ASIN value of the specified angle.
  
```sql
/* The first value will be -1.01. This fails because the value is   
outside the range.*/  
DECLARE @angle FLOAT  
SET @angle = -1.01  
SELECT 'The ASIN of the angle is: ' + CONVERT(VARCHAR, ASIN(@angle))  
GO  
  
-- The next value is -1.00.  
DECLARE @angle FLOAT  
SET @angle = -1.00  
SELECT 'The ASIN of the angle is: ' + CONVERT(VARCHAR, ASIN(@angle))  
GO  
  
-- The next value is 0.1472738.  
DECLARE @angle FLOAT  
SET @angle = 0.1472738  
SELECT 'The ASIN of the angle is: ' + CONVERT(VARCHAR, ASIN(@angle))  
GO  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```
-------------------------  
.Net SqlClient Data Provider: Msg 3622, Level 16, State 1, Line 3  
A domain error occurred.  
  
---------------------------------   
The ASIN of the angle is: -1.5708                          
  
(1 row(s) affected)  
  
----------------------------------   
The ASIN of the angle is: 0.147811                         
  
(1 row(s) affected)  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
This example returns the arcsine of 1.00.
  
```sql
SELECT ASIN(1.00) AS asinCalc;  
```  
  
This example returns an error, because it requests the arcsine for a value outside the allowed range.
  
```sql
SELECT ASIN(1.1472738) AS asinCalc;  
```  
  
## See also
[CEILING &#40;Transact-SQL&#41;](../../t-sql/functions/ceiling-transact-sql.md)  
[Mathematical Functions &#40;Transact-SQL&#41;](../../t-sql/functions/mathematical-functions-transact-sql.md)  
[SET ARITHIGNORE &#40;Transact-SQL&#41;](../../t-sql/statements/set-arithignore-transact-sql.md)  
[SET ARITHABORT &#40;Transact-SQL&#41;](../../t-sql/statements/set-arithabort-transact-sql.md)
  
  

