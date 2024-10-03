---
title: "QUOTENAME (Transact-SQL)"
description: "QUOTENAME (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "QUOTENAME_TSQL"
  - "QUOTENAME"
helpviewer_keywords:
  - "delimited identifiers [SQL Server]"
  - "input strings [SQL Server]"
  - "Unicode [SQL Server], delimited identifiers"
  - "QUOTENAME function"
  - "valid identifiers [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current||=fabric"
---
# QUOTENAME (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

  Returns a Unicode string with the delimiters added to make the input string a valid [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] delimited identifier.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
QUOTENAME ( 'character_string' [ , 'quote_character' ] )   
```  
  
## Arguments
 '*character_string*'  
 Is a string of Unicode character data. *character_string* is **sysname** and is limited to 128 characters. Inputs greater than 128 characters return NULL.  
  
 '*quote_character*'  
 Is a one-character string to use as the delimiter. Can be a single quotation mark ( **'** ), a left or right bracket ( **[]** ), a double quotation mark ( **"** ), a left or right parenthesis ( **()** ), a greater than or less than sign ( **><** ), a left or right brace ( **{}** ) or a backtick ( **\`** ). NULL returns if an unacceptable character is supplied. If *quote_character* is not specified, brackets are used.  
  
## Return Types  
 **nvarchar(258)**  
  
## Examples  
 The following example takes the character string `abc[]def` and uses the `[` and `]` characters to create a valid [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] delimited identifier.  
  
```sql
SELECT QUOTENAME('abc[]def');
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
[abc[]]def]
  
(1 row(s) affected)  
```  
  
 Notice that the right bracket in the string `abc[]def` is doubled to indicate an escape character.  
 
 The following example prepares a quoted string to use in naming a column.  
  
```sql
DECLARE @columnName NVARCHAR(255)='user''s "custom" name'
DECLARE @sql NVARCHAR(MAX) = 'SELECT FirstName AS ' + QUOTENAME(@columnName) + ' FROM dbo.DimCustomer'

EXEC sp_executesql @sql
```
  
## Examples: [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 The following example takes the character string `abc def` and uses the `[` and `]` characters to create a valid [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] delimited identifier.  
  
```sql
SELECT QUOTENAME('abc def');   
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
[abc def]  
  
(1 row(s) affected)  
```  
  
## See Also  
 [PARSENAME &#40;Transact-SQL&#41;](../../t-sql/functions/parsename-transact-sql.md)  
 [CONCAT &#40;Transact-SQL&#41;](../../t-sql/functions/concat-transact-sql.md)  
 [CONCAT_WS &#40;Transact-SQL&#41;](../../t-sql/functions/concat-ws-transact-sql.md)  
 [FORMATMESSAGE &#40;Transact-SQL&#41;](../../t-sql/functions/formatmessage-transact-sql.md)  
 [REPLACE &#40;Transact-SQL&#41;](../../t-sql/functions/replace-transact-sql.md)  
 [REVERSE &#40;Transact-SQL&#41;](../../t-sql/functions/reverse-transact-sql.md)  
 [STRING_AGG &#40;Transact-SQL&#41;](../../t-sql/functions/string-agg-transact-sql.md)  
 [STRING_ESCAPE &#40;Transact-SQL&#41;](../../t-sql/functions/string-escape-transact-sql.md)  
 [STUFF &#40;Transact-SQL&#41;](../../t-sql/functions/stuff-transact-sql.md)  
 [TRANSLATE &#40;Transact-SQL&#41;](../../t-sql/functions/translate-transact-sql.md)  
 [String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)  
