---
title: "STRING_ESCAPE (Transact-SQL)"
description: "STRING_ESCAPE (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: ""
ms.date: "02/25/2016"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "STRING_ESCAPE"
  - "STRING_ESCAPE_TSQL"
helpviewer_keywords:
  - "STRING_ESCAPE function"
dev_langs:
  - "TSQL"
monikerRange: "= azuresqldb-current || >= sql-server-2016 || >= sql-server-linux-2017"
---
# STRING_ESCAPE (Transact-SQL)


[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

Escapes special characters in texts and returns text with escaped characters. **STRING_ESCAPE** is a deterministic function, introduced in SQL Server 2016. 
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
STRING_ESCAPE( text , type )  
```  

## Arguments

 *text*  
 Is a **nvarchar**[expression](../../t-sql/language-elements/expressions-transact-sql.md) expression representing the object that should be escaped.  
  
 *type*  
 Escaping rules that will be applied. Currently the value supported is `'json'`.  
  
## Return Types

 **nvarchar(max)** text with escaped special and control characters. Currently **STRING_ESCAPE** can only escape JSON special characters shown in the following tables.  
  
|Special character|Encoded sequence|  
|-----------------------|----------------------|  
|Quotation mark (")|\\"|  
|Reverse solidus (\\)| \\\\ |  
|Solidus (/)|\\/|  
|Backspace|\b|  
|Form feed|\f|  
|New line|\n|  
|Carriage return|\r|  
|Horizontal tab|\t|  
  
|Control character|Encoded sequence|  
|-----------------------|----------------------|  
|CHAR(0)|\u0000|  
|CHAR(1)|\u0001|  
|...|...|  
|CHAR(31)|\u001f|  
  
## Remarks  
  
## Examples  
  
### A.  Escape text according to the JSON formatting rules

 The following query escapes special characters using JSON rules and returns escaped text.  
  
```sql
SELECT STRING_ESCAPE('\   /  
\\    "     ', 'json') AS escapedText;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```
escapedText  
-------------------------------------------------------------  
\\\t\/\n\\\\\t\"\t
```  
  
### B. Format JSON object

 The following query creates JSON text from number and string variables, and escapes any special JSON character in variables.  
  
```
SET @json = FORMATMESSAGE('{ "id": %d,"name": "%s", "surname": "%s" }',
    17, STRING_ESCAPE(@name,'json'), STRING_ESCAPE(@surname,'json') );  
```  
  
## See Also

- [CONCAT &#40;Transact-SQL&#41;](../../t-sql/functions/concat-transact-sql.md)  
- [CONCAT_WS &#40;Transact-SQL&#41;](../../t-sql/functions/concat-ws-transact-sql.md)  
- [FORMATMESSAGE &#40;Transact-SQL&#41;](../../t-sql/functions/formatmessage-transact-sql.md)  
- [QUOTENAME &#40;Transact-SQL&#41;](../../t-sql/functions/quotename-transact-sql.md)  
- [REPLACE &#40;Transact-SQL&#41;](../../t-sql/functions/replace-transact-sql.md)  
- [REVERSE &#40;Transact-SQL&#41;](../../t-sql/functions/reverse-transact-sql.md)  
- [STRING_AGG &#40;Transact-SQL&#41;](../../t-sql/functions/string-agg-transact-sql.md)  
- [STUFF &#40;Transact-SQL&#41;](../../t-sql/functions/stuff-transact-sql.md)  
- [TRANSLATE &#40;Transact-SQL&#41;](../../t-sql/functions/translate-transact-sql.md)  
- [String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)
