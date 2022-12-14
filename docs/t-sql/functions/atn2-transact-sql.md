---
title: "ATN2 (Transact-SQL)"
description: "ATN2 (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "07/24/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ATN2"
  - "ATN2_TSQL"
helpviewer_keywords:
  - "arctangent"
  - "tangent"
  - "ATN2 function"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# ATN2 (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Returns the angle, in radians, between the positive x-axis and the ray from the origin to the point (y, x), where x and y are the values of the two specified float expressions.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```syntaxsql
ATN2 ( float_expression , float_expression )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
*float_expression*  
An [expression](../../t-sql/language-elements/expressions-transact-sql.md) of data type **float**.
  
## Return types
**float**
  
## Examples  
The following example calculates the `ATN2` for the specified `x` and `y` components.
  
```sql
DECLARE @x FLOAT = 35.175643, @y FLOAT = 129.44;  
SELECT 'The ATN2 of the angle is: ' + CONVERT(VARCHAR, ATN2(@y, @x));  
GO  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```
The ATN2 of the angle is: 1.30545                         
(1 row(s) affected)  
```  
  
## See also
[CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)  
[float and real &#40;Transact-SQL&#41;](../../t-sql/data-types/float-and-real-transact-sql.md)  
[Mathematical Functions &#40;Transact-SQL&#41;](../../t-sql/functions/mathematical-functions-transact-sql.md)
  
  

