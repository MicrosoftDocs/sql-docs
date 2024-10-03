---
title: "FLOOR (Transact-SQL)"
description: "FLOOR (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "FLOOR_TSQL"
  - "FLOOR"
helpviewer_keywords:
  - "integers [SQL Server]"
  - "largest integers"
  - "FLOOR function [Transact-SQL]"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current||=fabric"
---
# FLOOR (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

  Returns the largest integer less than or equal to the specified numeric expression.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
FLOOR ( numeric_expression )  
```  
  
## Arguments
 *numeric_expression*  
 Is an expression of the exact numeric or approximate numeric data type category.  
  
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
 The following example shows positive numeric, negative numeric, and currency values with the `FLOOR` function.  
  
```sql  
SELECT FLOOR(123.45), FLOOR(-123.45), FLOOR($123.45);  
```  
  
 The result is the integer part of the calculated value in the same data type as *numeric_expression*.  
  
```  
---------      ---------     -----------  
123            -124          123.0000     
```  
  
## Examples: [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 The following example shows positive numeric, negative numeric, and values with the `FLOOR` function.  
  
```sql  
SELECT FLOOR(123.45), FLOOR(-123.45), FLOOR($123.45);  
```  
  
 The result is the integer part of the calculated value in the same data type as *numeric_expression*.  
  
 ```
 -----   ---------    -----------  
  
 123     -124         123
 ```  
  
## See Also  
 [Mathematical Functions &#40;Transact-SQL&#41;](../../t-sql/functions/mathematical-functions-transact-sql.md)  
  
  

