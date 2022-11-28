---
title: "STUFF (Transact-SQL)"
description: "STUFF (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: ""
ms.date: "11/17/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "STUFF"
  - "STUFF_TSQL"
helpviewer_keywords:
  - "deleting characters"
  - "STUFF function"
  - "length characters"
  - "removing characters"
  - "replacing characters"
  - "characters [SQL Server], replacing"
  - "inserting data"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# STUFF (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  The STUFF function inserts a string into another string. It deletes a specified length of characters in the first string at the start position and then inserts the second string into the first string at the start position.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
STUFF ( character_expression , start , length , replaceWith_expression )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *character_expression*  
 Is an [expression](../../t-sql/language-elements/expressions-transact-sql.md) of character data. *character_expression* can be a constant, variable, or column of either character or binary data.  
  
 *start*  
 Is an integer value that specifies the location to start deletion and insertion. If *start* is negative or zero, a null string is returned. If *start* is longer than the first *character_expression*, a null string is returned. *start* can be of type **bigint**.  
  
 *length*  
 Is an integer that specifies the number of characters to delete. If *length* is negative, a null string is returned. If *length* is longer than the first *character_expression*, deletion occurs up to the last character in the last *character_expression*.  If *length* is zero, insertion occurs at *start* location and no characters are deleted. *length* can be of type **bigint**.

 *replaceWith_expression*  
 Is an [expression](../../t-sql/language-elements/expressions-transact-sql.md) of character data. *character_expression* can be a constant, variable, or column of either character or binary data. This expression replaces *length* characters of *character_expression* beginning at *start*. Providing `NULL` as the *replaceWith_expression*, removes characters without inserting anything.   
  
## Return Types  
 Returns character data if *character_expression* is one of the supported character data types. Returns binary data if *character_expression* is one of the supported binary data types.  
  
## Remarks  
 If the start position or the length is negative, or if the starting position is larger than length of the first string, a null string is returned. If the start position is 0, a null value is returned. If the length to delete is longer than the first string, it is deleted to the first character in the first string.  

An error is raised if the resulting value is larger than the maximum supported by the return type.  
  
## Supplementary Characters (Surrogate Pairs)  
 When using SC collations, both *character_expression* and *replaceWith_expression* can include surrogate pairs. The length parameter counts each surrogate in *character_expression* as a single character.  
  
## Examples  
 The following example returns a character string created by deleting three characters from the first string, `abcdef`, starting at position `2`, at `b`, and inserting the second string at the deletion point.  
  
```sql  
SELECT STUFF('abcdef', 2, 3, 'ijklmn');  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
---------   
aijklmnef   
  
(1 row(s) affected)  
```  
  
## See Also  
 [CONCAT &#40;Transact-SQL&#41;](../../t-sql/functions/concat-transact-sql.md)  
 [CONCAT_WS &#40;Transact-SQL&#41;](../../t-sql/functions/concat-ws-transact-sql.md)  
 [FORMATMESSAGE &#40;Transact-SQL&#41;](../../t-sql/functions/formatmessage-transact-sql.md)  
 [QUOTENAME &#40;Transact-SQL&#41;](../../t-sql/functions/quotename-transact-sql.md)  
 [REPLACE &#40;Transact-SQL&#41;](../../t-sql/functions/replace-transact-sql.md)  
 [REVERSE &#40;Transact-SQL&#41;](../../t-sql/functions/reverse-transact-sql.md)  
 [STRING_AGG &#40;Transact-SQL&#41;](../../t-sql/functions/string-agg-transact-sql.md)  
 [STRING_ESCAPE &#40;Transact-SQL&#41;](../../t-sql/functions/string-escape-transact-sql.md)  
 [TRANSLATE &#40;Transact-SQL&#41;](../../t-sql/functions/translate-transact-sql.md)  
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)   
 [String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)  
