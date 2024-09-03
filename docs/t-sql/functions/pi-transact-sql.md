---
title: "PI (Transact-SQL)"
description: "PI (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "PI_TSQL"
  - "PI"
helpviewer_keywords:
  - "constant value of PI"
  - "PI function"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current||=fabric"
---
# PI (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

  Returns the constant value of PI.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
PI ( )  
```  
  
## Return Types
 **float**  
  
## Examples  
 The following example returns the value of `PI`.  
  
```sql  
SELECT PI();  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
------------------------  
3.14159265358979  
  
(1 row(s) affected)  
```  
  
## See Also  
 [Mathematical Functions &#40;Transact-SQL&#41;](../../t-sql/functions/mathematical-functions-transact-sql.md)  
  
  

