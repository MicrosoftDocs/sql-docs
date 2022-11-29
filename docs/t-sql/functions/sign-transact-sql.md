---
title: "SIGN (Transact-SQL)"
description: "SIGN (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: ""
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "SIGN_TSQL"
  - "SIGN"
helpviewer_keywords:
  - "- (negative)"
  - "+ (positive sign)"
  - "zero (0)"
  - "SIGN function"
  - "positive values [SQL Server]"
  - "0 (zero)"
  - "negative values"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# SIGN (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns the positive (+1), zero (0), or negative (-1) sign of the specified expression.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
SIGN ( numeric_expression )  
```  
  

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *numeric_expression*  
 Is an [expression](../../t-sql/language-elements/expressions-transact-sql.md) of the exact numeric or approximate numeric data type category, except for the **bit** data type.  
  
## Return Types  
  
|Specified expression|Return type|  
|--------------------------|-----------------|  
|**bigint**|**bigint**|  
|**int/smallint/tinyint**|**int**|  
|**money/smallmoney**|**money**|  
|**numeric/decimal**|**numeric/decimal**|  
|**Other types**|**float**|  
  
## Examples  
 The following example returns the SIGN values of numbers from -1 to 1.  
  
```sql  
DECLARE @value REAL  
SET @value = -1  
WHILE @value < 2  
   BEGIN  
      SELECT SIGN(@value)  
      SET NOCOUNT ON  
      SELECT @value = @value + 1  
      SET NOCOUNT OFF  
   END  
SET NOCOUNT OFF  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
(1 row(s) affected)  
  
------------------------   
-1.0                       
  
(1 row(s) affected)  
  
------------------------   
0.0                        
  
(1 row(s) affected)  
  
------------------------   
1.0                        
  
(1 row(s) affected)  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 The following example returns the SIGN values of three numbers.  
  
```sql  
SELECT SIGN(-125), SIGN(0), SIGN(564);  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```
-----  -----  -----  
-1     0      1
```  
  
## See Also  
 [Mathematical Functions &#40;Transact-SQL&#41;](../../t-sql/functions/mathematical-functions-transact-sql.md)  
  
  

