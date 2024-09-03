---
title: "DEGREES (Transact-SQL)"
description: "DEGREES (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DEGREES"
  - "DEGREES_TSQL"
helpviewer_keywords:
  - "DEGREES function"
  - "number of degrees"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current||=fabric"
---
# DEGREES (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

This function returns the corresponding angle, in degrees, for an angle specified in radians.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
DEGREES ( numeric_expression )  
```  
  
## Arguments
 *numeric_expression*  
An [expression](../../t-sql/language-elements/expressions-transact-sql.md) of the exact numeric or approximate numeric data type category.  
  
## Return Types  
The return type depends on the input type of *numeric_expression*:
 
|Input type|Return type|  
|----------|-----------|  
|**float**, **real**|**float**|
|**decimal(*p*, *s*)**|**decimal(38, *s*)**|
|**int**, **smallint**, **tinyint**|**int**|
|**bigint**|**bigint**|
|**money**, **smallmoney**|**money**|
|**bit**|**float**|

If the result does not fit in the return type, an arithmetic overflow error occurs.  
  
## Examples  
This example returns the number of degrees in an angle of PI/2 radians.  
  
```sql  
SELECT 'The number of degrees in PI/2 radians is: ' +   
CONVERT(VARCHAR, DEGREES((PI()/2)));  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
The number of degrees in PI/2 radians is 90         
  
(1 row(s) affected)  
```  
  
## See Also  
 [Mathematical Functions &#40;Transact-SQL&#41;](../../t-sql/functions/mathematical-functions-transact-sql.md)  
  
