---
title: "TRY_CAST (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "TRY_CAST_TSQL"
  - "TRY_CAST"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "TRY_CAST function"
ms.assetid: ea3a16de-995b-415c-b5f0-9355cf7bb401
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# TRY_CAST (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-asdb-xxxx-xxx-md.md)]

  Returns a value cast to the specified data type if the cast succeeds; otherwise, returns null.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
TRY_CAST ( expression AS data_type [ ( length ) ] )  
```  
  
## Arguments  
 *expression*  
 The value to be cast. Any valid expression.  
  
 *data_type*  
 The data type into which to cast *expression*.  
  
 *length*  
 Optional integer that specifies the length of the target data type.  
  
 The range of acceptable values is determined by the value of *data_type*.  
  
## Return Types  
 Returns a value cast to the specified data type if the cast succeeds; otherwise, returns null.  
  
## Remarks  
 **TRY_CAST** takes the value passed to it and tries to convert it to the specified *data_type*. If the cast succeeds, **TRY_CAST** returns the value as the specified *data_type*; if an error occurs, null is returned. However if you request a conversion that is explicitly not permitted, then **TRY_CAST** fails with an error.  
  
 **TRY_CAST** is not a new reserved keyword and is available in all compatibility levels. **TRY_CAST** has the same semantics as **TRY_CONVERT** when connecting to remote servers.  
  
## Examples  
  
### A. TRY_CAST returns null  
 The following example demonstrates that TRY_CAST returns null when the cast fails.  
  
```sql  
SELECT   
    CASE WHEN TRY_CAST('test' AS float) IS NULL   
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
SELECT TRY_CAST('12/31/2010' AS datetime2) AS Result;  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Result  
----------------------  
NULL  
  
(1 row(s) affected)  
```  
  
### B. TRY_CAST fails with an error  
 The following example demonstrates that TRY_CAST returns an error when the cast is explicitly not permitted.  
  
```sql  
SELECT TRY_CAST(4 AS xml) AS Result;  
GO  
```  
  
 The result of this statement is an error, because an integer cannot be cast into an xml data type.  
  
```  
Explicit conversion from data type int to xml is not allowed.  
```  
  
### C. TRY_CAST succeeds  
 This example demonstrates that the expression must be in the expected format.  
  
```  
SET DATEFORMAT mdy;  
SELECT TRY_CAST('12/31/2010' AS datetime2) AS Result;  
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
 [TRY_CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/try-convert-transact-sql.md)   
 [CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)  
  
  
