---
title: "PI (Transact-SQL)"
description: "PI (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "PI_TSQL"
  - "PI"
helpviewer_keywords:
  - "constant value of PI"
  - "PI function"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# PI (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

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
  
## Related content

- [Mathematical functions (Transact-SQL)](mathematical-functions-transact-sql.md)
