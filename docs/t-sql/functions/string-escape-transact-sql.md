---
title: "STRING_ESCAPE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "02/25/2016"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "STRING_ESCAPE"
  - "STRING_ESCAPE_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STRING_ESCAPE function"
ms.assetid: 2163bc7a-3816-4304-9c40-8954804f5465
caps.latest.revision: 11
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# STRING_ESCAPE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

  Escapes special characters in texts and returns text with escaped characters. **STRING_ESCAPE** is a deterministic function.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
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
|Reverse solidus (\\)|\\\|  
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
  
```  
SELECT STRING_ESCAPE('\   /  
\\    "     ', 'json') AS escapedText;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 `escapedText`  
  
 `-------------------------------------------------------------`  
  
 `\\\t\/\n\\\\\t\"\t`  
  
### B. Format JSON object  
 The following query creates JSON text from number and string variables, and escapes any special JSON character in variables.  
  
```  
SET @json = FORMATMESSAGE('{ "id": %d,"name": "%s", "surname": "%s" }',   
    17, STRING_ESCAPE(@name,'json'), STRING_ESCAPE(@surname,'json') );  
```  
  
## See Also  
 [String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)   
 [SUBSTRING &#40;Transact-SQL&#41;](../../t-sql/functions/substring-transact-sql.md)  
  
  
