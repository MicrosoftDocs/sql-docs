---
title: "SPACE (Transact-SQL)"
description: "SPACE (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: ""
ms.date: "03/15/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "SPACE_TSQL"
  - "SPACE"
helpviewer_keywords:
  - "strings [SQL Server], repeated spaces"
  - "repeated spaces"
  - "SPACE function"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# SPACE (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns a string of repeated spaces.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
SPACE ( integer_expression )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *integer_expression*  
 Is a positive integer that indicates the number of spaces. If *integer_expression* is negative, a null string is returned.  
  
 For more information, see [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)  
  
## Return Types  
 **varchar**  
  
## Remarks  
 To include spaces in Unicode data, or to return more than 8000 character spaces, use REPLICATE instead of SPACE.  
  
## Examples  
 The following example trims the last names and concatenates a comma, two spaces, and the first names of people listed in the `Person` table in `AdventureWorks2012`.  
  
```sql  
USE AdventureWorks2012;  
GO  
SELECT RTRIM(LastName) + ',' + SPACE(2) +  LTRIM(FirstName)  
FROM Person.Person  
ORDER BY LastName, FirstName;  
GO  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 The following example trims the last names and concatenates a comma, two spaces, and the first names of people listed in the `DimCustomer` table in `AdventureWorksPDW2012`.  
  
```sql  
-- Uses AdventureWorks  
  
SELECT RTRIM(LastName) + ',' + SPACE(2) +  LTRIM(FirstName)  
FROM dbo.DimCustomer  
ORDER BY LastName, FirstName;  
GO  
```  
  
## See Also  
 [REPLICATE &#40;Transact-SQL&#41;](../../t-sql/functions/replicate-transact-sql.md)   
 [String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)   
 [Built-in Functions &#40;Transact-SQL&#41;](~/t-sql/functions/functions.md)  
  
  


