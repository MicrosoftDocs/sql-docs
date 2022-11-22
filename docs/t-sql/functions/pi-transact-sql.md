---
title: "PI (Transact-SQL)"
description: "PI (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: ""
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "PI_TSQL"
  - "PI"
helpviewer_keywords:
  - "constant value of PI"
  - "PI function"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# PI (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns the constant value of PI.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
PI ( )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

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
  
  

