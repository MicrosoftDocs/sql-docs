---
title: "COT (Transact-SQL)"
description: "COT (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "07/24/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "COT_TSQL"
  - "COT"
helpviewer_keywords:
  - "COT function"
  - "cotangent"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# COT (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

A mathematical function that returns the trigonometric cotangent of the specified angle - in radians - in the specified **float** expression.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```syntaxsql
COT ( float_expression )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
*float_expression*  
An [expression](../../t-sql/language-elements/expressions-transact-sql.md) of type **float**, or of a type that can implicitly convert to **float**.
  
## Return types
**float**
  
## Examples  
This example returns the `COT` value for the specific angle:
  
```sql
DECLARE @angle FLOAT;  
SET @angle = 124.1332;  
SELECT 'The COT of the angle is: ' + CONVERT(VARCHAR, COT(@angle));  
GO  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```
The COT of the angle is: -0.040312                
  
(1 row(s) affected)  
```  
  
## See also
[Mathematical Functions &#40;Transact-SQL&#41;](../../t-sql/functions/mathematical-functions-transact-sql.md)
  
  

