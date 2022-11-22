---
title: "TRY_CONVERT (Transact-SQL)"
description: "TRY_CONVERT (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: ""
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "TRY_CONVERT_TSQL"
  - "TRY_CONVERT"
helpviewer_keywords:
  - "TRY_CONVERT function"
dev_langs:
  - "TSQL"
monikerRange: "= azuresqldb-current || >= sql-server-2016 || >= sql-server-linux-2017 || >= aps-pdw-2016 || = azure-sqldw-latest"
---
# TRY_CONVERT (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns a value cast to the specified data type if the cast succeeds; otherwise, returns null.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
TRY_CONVERT ( data_type [ ( length ) ], expression [, style ] )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *data_type [ ( length ) ]*  
 The data type into which to cast *expression*.  
  
 *expression*  
 The value to be cast.  
  
 *style*  
 Optional integer expression that specifies how the **TRY_CONVERT** function is to translate *expression*.  
  
 *style* accepts the same values as the *style* parameter of the **CONVERT** function. For more information, see [CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md).  
  
 The range of acceptable values is determined by the value of *data_type*. If *style* is null, then **TRY_CONVERT** returns null.  
  
## Return Types  
 Returns a value cast to the specified data type if the cast succeeds; otherwise, returns null.  
  
## Remarks  
 **TRY_CONVERT** takes the value passed to it and tries to convert it to the specified *data_type*. If the cast succeeds, **TRY_CONVERT** returns the value as the specified *data_type*; if an error occurs, null is returned. However if you request a conversion that is explicitly not permitted, then **TRY_CONVERT** fails with an error.  
  
 **TRY_CONVERT** is a reserved keyword in compatibility level 110 and higher.  
  
 This function is capable of being remoted to servers that have a version of [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and above. It will not be remoted to servers that have a version below [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)].  
  
## Examples  
  
### A. TRY_CONVERT returns null  
 The following example demonstrates that TRY_CONVERT returns null when the cast fails.  
  
```sql  
SELECT   
    CASE WHEN TRY_CONVERT(float, 'test') IS NULL   
    THEN 'Cast failed'  
    ELSE 'Cast succeeded'  
END AS Result;  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Result  
------------  
Cast failed  
  
(1 row(s) affected)  
```  
  
 The following example demonstrates that the expression must be in the expected format.  
  
```sql  
SET DATEFORMAT dmy;  
SELECT TRY_CONVERT(datetime2, '12/31/2010') AS Result;  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Result  
----------------------  
NULL  
  
(1 row(s) affected)  
```  
  
### B. TRY_CONVERT fails with an error  
 The following example demonstrates that TRY_CONVERT returns an error when the cast is explicitly not permitted.  
  
```sql  
SELECT TRY_CONVERT(xml, 4) AS Result;  
GO  
```  
  
 The result of this statement is an error, because an integer cannot be cast into an xml data type.  
  
```  
Explicit conversion from data type int to xml is not allowed.  
```  
  
### C. TRY_CONVERT succeeds  
 This example demonstrates that the expression must be in the expected format.  
  
```sql
SET DATEFORMAT mdy;  
SELECT TRY_CONVERT(datetime2, '12/31/2010') AS Result;  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Result  
----------------------------------  
2010-12-31 00:00:00.0000000  
  
(1 row(s) affected)  
```  
  
## See Also  
 [CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)  
  
  
