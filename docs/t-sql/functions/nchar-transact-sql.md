---
title: "NCHAR (Transact-SQL)"
description: "NCHAR (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/13/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "nchar"
  - "nchar_TSQL"
helpviewer_keywords:
  - "NCHAR function"
  - "Unicode [SQL Server], NCHAR function"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# NCHAR (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns the Unicode character with the specified integer code, as defined by the Unicode standard.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
NCHAR ( integer_expression )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *integer_expression*  
 When the collation of the database does not contain the [Supplementary Character (SC)](../../relational-databases/collations/collation-and-unicode-support.md#Supplementary_Characters) flag, this is a positive integer from 0 through 65535 (0 through 0xFFFF). If a value outside this range is specified, NULL is returned. For more information about supplementary characters, see [Collation and Unicode Support](../../relational-databases/collations/collation-and-unicode-support.md).  
  
 When the collation of the database supports the SC flag, this is a positive integer from 0 through 1114111 (0 through 0x10FFFF). If a value outside this range is specified, NULL is returned.  
  
## Return Types  
 **nchar(1)** when the default database collation does not support supplementary characters.  
  
 **nvarchar(2)** when the default database collation supports supplementary characters.  
  
 If the parameter *integer_expression* lies in the range 0 - 0xFFFF, only one character is returned. For higher values, NCHAR returns the corresponding surrogate pair. Do not construct a surrogate pair by using `NCHAR(<High surrogate>) + NCHAR(\<Low Surrogate>)`. Instead, use a database collation that supports supplementary characters and then specify the Unicode codepoint for the surrogate pair. The following example demonstrates both the old style method of constructing a surrogate pair and the preferred method of specifying the Unicode codepoint.  
  
```sql  
CREATE DATABASE test COLLATE Finnish_Swedish_100_CS_AS_SC;  
DECLARE @d NVARCHAR(10) = N'𣅿';
-- Old style method.  
SELECT NCHAR(0xD84C) + NCHAR(0xDD7F);   
  
-- Preferred method.   
SELECT NCHAR(143743);   
  
-- Alternative preferred method.  
SELECT NCHAR(UNICODE(@d));    
```  
  
## Examples  
  
### A. Using NCHAR and UNICODE  
 The following example uses the `UNICODE` and `NCHAR` functions to print the `UNICODE` value and the `NCHAR` (Unicode character) of the second character of the `København` character string, and to print the actual second character, `ø`.  
  
```sql  
DECLARE @nstring NCHAR(8);  
SET @nstring = N'København';  
SELECT UNICODE(SUBSTRING(@nstring, 2, 1)),   
   NCHAR(UNICODE(SUBSTRING(@nstring, 2, 1)));  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
----------- -   
248         ø  
(1 row(s) affected)  
```  
  
### B. Using SUBSTRING, UNICODE, CONVERT, and NCHAR  
 The following example uses the `SUBSTRING`, `UNICODE`, `CONVERT`, and `NCHAR` functions to print the character number, the Unicode character, and the UNICODE value of each character in the string `København`.  
  
```sql  
-- The @position variable holds the position of the character currently  
-- being processed. The @nstring variable is the Unicode character   
-- string to process.  
DECLARE @position INT, @nstring NCHAR(9);  
-- Initialize the current position variable to the first character in   
-- the string.  
SET @position = 1;  
-- Initialize the character string variable to the string to process.  
-- Notice that there is an N before the start of the string. This   
-- indicates that the data following the N is Unicode data.  
SET @nstring = N'København';  
-- Print the character number of the position of the string you are at,   
-- the actual Unicode character you are processing, and the UNICODE   
-- value for this particular character.  
PRINT 'Character #' + ' ' + 'Unicode Character' + ' ' + 'UNICODE Value';  
WHILE @position <= DATALENGTH(@nstring)  
   BEGIN  
   SELECT @position,   
      NCHAR(UNICODE(SUBSTRING(@nstring, @position, 1))),  
      CONVERT(NCHAR(17), SUBSTRING(@nstring, @position, 1)),  
      UNICODE(SUBSTRING(@nstring, @position, 1))  
   SELECT @position = @position + 1  
   END;  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Character # Unicode Character UNICODE Value  
  
----------- ---- ----------------- -----------   
1           K    K                 75  
  
(1 row(s) affected)  
  
----------- ---- ----------------- -----------   
2           ø    ø                 248  
  
(1 row(s) affected)  
  
----------- ---- ----------------- -----------   
3           b    b                 98  
  
(1 row(s) affected)  
  
----------- ---- ----------------- -----------   
4           e    e                 101  
  
(1 row(s) affected)  
  
----------- ---- ----------------- -----------   
5           n    n                 110  
  
(1 row(s) affected)  
  
----------- ---- ----------------- -----------   
6           h    h                 104  
  
(1 row(s) affected)  
  
----------- ---- ----------------- -----------   
7           a    a                 97  
  
(1 row(s) affected)  
  
----------- ---- ----------------- -----------   
8           v    v                 118  
  
(1 row(s) affected)  
  
----------- ---- ----------------- -----------   
9           n    n                 110  
  
(1 row(s) affected)  
  
----------- ---- ----------------- -----------   
10          NULL                   NULL  
  
(1 row(s) affected)  
  
----------- ---- ----------------- -----------   
11          NULL                   NULL  
  
(1 row(s) affected)  
  
----------- ---- ----------------- -----------   
12          NULL                   NULL  
  
(1 row(s) affected)  
  
----------- ---- ----------------- -----------   
13          NULL                   NULL  
  
(1 row(s) affected)  
  
----------- ---- ----------------- -----------   
14          NULL                   NULL  
  
(1 row(s) affected)  
  
----------- ---- ----------------- -----------   
15          NULL                   NULL  
  
(1 row(s) affected)  
  
----------- ---- ----------------- -----------   
16          NULL                   NULL  
  
(1 row(s) affected)  
  
----------- ---- ----------------- -----------   
17          NULL                   NULL  
  
(1 row(s) affected)  
  
----------- ---- ----------------- -----------   
18          NULL                   NULL  
  
(1 row(s) affected)  
```  
  
## See Also  
 [ASCII &#40;Transact-SQL&#41;](../../t-sql/functions/ascii-transact-sql.md)  
 [CHAR &#40;Transact-SQL&#41;](../../t-sql/functions/char-transact-sql.md)  
 [UNICODE &#40;Transact-SQL&#41;](../../t-sql/functions/unicode-transact-sql.md)  
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)   
 [String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)   
  
  

