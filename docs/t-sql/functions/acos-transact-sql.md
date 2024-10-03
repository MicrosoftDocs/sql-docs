---
title: "ACOS (Transact-SQL)"
description: "ACOS (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "07/24/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ACOS"
  - "ACOS_TSQL"
helpviewer_keywords:
  - "cosine"
  - "ACOS function"
  - "arccosine"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current||=fabric"
---
# ACOS (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

A function that returns the angle, in radians, whose cosine is the specified float expression. This is also called arccosine.
  
:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```syntaxsql
ACOS ( float_expression )  
```  
  
## Arguments

*float_expression*  
An [expression](../../t-sql/language-elements/expressions-transact-sql.md) of either type **float** or of a type that can implicitly convert to float. Only a value ranging from -1.00 to 1.00 is valid. For values outside this range, no value is returned, and ACOS will report a domain error.
  
## Return types


**float**
  
## Examples

This example returns the `ACOS` value of the specified number.
  
```sql
SET NOCOUNT OFF;  
DECLARE @cos FLOAT;  
SET @cos = -1.0;  
SELECT 'The ACOS of the number is: ' + CONVERT(VARCHAR, ACOS(@cos));  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```sql
---------------------------------   
The ACOS of the number is: 3.14159   
  
(1 row(s) affected)  
```  
  
## See also

[Mathematical Functions &#40;Transact-SQL&#41;](../../t-sql/functions/mathematical-functions-transact-sql.md)  
[Functions](../../t-sql/functions/functions.md)
