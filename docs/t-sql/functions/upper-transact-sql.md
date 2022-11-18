---
title: "UPPER (Transact-SQL)"
description: "UPPER (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: ""
ms.date: "03/13/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "UPPER_TSQL"
  - "UPPER"
helpviewer_keywords:
  - "UPPER function"
  - "characters [SQL Server], lowercase"
  - "converting lowercase to uppercase"
  - "uppercase characters [SQL Server]"
  - "characters [SQL Server], uppercase"
  - "lowercase characters"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# UPPER (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns a character expression with lowercase character data converted to uppercase.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
UPPER ( character_expression )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *character_expression*  
 Is an [expression](../../t-sql/language-elements/expressions-transact-sql.md) of character data. *character_expression* can be a constant, variable, or column of either character or binary data.  
  
 *character_expression* must be of a data type that is implicitly convertible to **varchar**. Otherwise, use [CAST](../../t-sql/functions/cast-and-convert-transact-sql.md) to explicitly convert *character_expression*.  
  
## Return Types  
 **varchar** or **nvarchar**  
  
## Examples  
 The following example uses the `UPPER` and `RTRIM` functions to return the last name of people in the `dbo.DimEmployee` table so that it is in uppercase, trimmed, and concatenated with the first name.  
  
```sql
-- Uses AdventureWorks  
  
SELECT UPPER(RTRIM(LastName)) + ', ' + FirstName AS Name  
FROM dbo.DimEmployee  
ORDER BY LastName;  
```  
  
 Here is a partial result set.  
  
 ```
Name
------------------------------
ABBAS, Syed
ABERCROMBIE, Kim
ABOLROUS, Hazem
 ```  
  
## See Also  
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)   
 [String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)  
 [LOWER &#40;Transact-SQL&#41;](../../t-sql/functions/lower-transact-sql.md)  
  
  

