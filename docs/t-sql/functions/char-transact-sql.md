---
title: "CHAR (Transact-SQL)"
description: "CHAR (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "11/14/2019"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "char_TSQL"
  - "char"
helpviewer_keywords:
  - "converting int ASCII code to character"
  - "control characters"
  - "tab"
  - "ASCII conversions"
  - "CHAR function"
  - "carriage return"
  - "inserting control characters"
  - "characters [SQL Server], control"
  - "line feed"
  - "printing ASCII values"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# CHAR (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Returns the single-byte character with the specified integer code, as defined by the character set and encoding of the default collation of the current database.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```syntaxsql
CHAR ( integer_expression )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
*integer_expression*  
An integer from 0 through 255. `CHAR` returns a `NULL` value for integer expressions outside this input range or not representing a complete character.
`CHAR` also returns a `NULL` value when the character exceeds the length of the return type.
Many common character sets share ASCII as a sub-set and will return the same character for integer values in the range 0 through 127.

> [!NOTE]
> Some character sets, such as [Unicode](https://en.wikipedia.org/wiki/Unicode#Mapping_and_encodings) and [Shift Japanese Industrial Standards](https://www.wikipedia.org/wiki/Shift_JIS), include characters that can be represented in a single-byte coding scheme, but require multibyte encoding. For more information on character sets, refer to [Single-Byte and Multibyte Character Sets](/cpp/c-runtime-library/single-byte-and-multibyte-character-sets). 
  
## Return types
**char(1)**
  
## Remarks  
Use `CHAR` to insert control characters into character strings. This table shows some frequently used control characters.
  
|Control character|Value|  
|---|---|
|Tab|**char(9)**|  
|Line feed|**char(10)**|  
|Carriage return|**char(13)**|  
  
## Examples  
  
### A. Using ASCII and CHAR to print ASCII values from a string  
This example prints the ASCII value and character for each character in the string `New Moon`.
  
```sql
SET TEXTSIZE 0;  
-- Create variables for the character string and for the current   
-- position in the string.  
DECLARE @position INT, @string CHAR(8);  
-- Initialize the current position and the string variables.  
SET @position = 1;  
SET @string = 'New Moon';  
WHILE @position <= DATALENGTH(@string)  
   BEGIN  
   SELECT ASCII(SUBSTRING(@string, @position, 1)),   
      CHAR(ASCII(SUBSTRING(@string, @position, 1)))  
   SET @position = @position + 1  
   END;  
GO  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```
----------- -
78          N  
----------- -  
101         e  
----------- -  
119         w  
----------- -  
32  
----------- -  
77          M  
----------- -  
111         o  
----------- -  
111         o  
----------- - 
110         n  
```
  
### B. Using CHAR to insert a control character  
This example uses `CHAR(13)` to print the name and e-mail address of an employee on separate lines, when the query returns its results as text. This example uses the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.
  
```sql
SELECT p.FirstName + ' ' + p.LastName, + CHAR(13)  + pe.EmailAddress   
FROM Person.Person p 
INNER JOIN Person.EmailAddress pe ON p.BusinessEntityID = pe.BusinessEntityID  
  AND p.BusinessEntityID = 1;  
GO  
```
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```
Ken Sanchez
ken0@adventure-works.com
  
(1 row(s) affected)
```
  
### C. Using ASCII and CHAR to print ASCII values from a string  
This example assumes an ASCII character set. It returns the character value for six different ASCII character number values.
  
```sql
SELECT CHAR(65) AS [65], CHAR(66) AS [66],   
CHAR(97) AS [97], CHAR(98) AS [98],   
CHAR(49) AS [49], CHAR(50) AS [50];  
```
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```
65   66   97   98   49   50  
---- ---- ---- ---- ---- ----  
A    B    a    b    1    2  
```
  
### D. Using CHAR to insert a control character  
This example uses `CHAR(13)` to return information from sys.databases on separate lines, when the query returns its results as text.
  
```sql
SELECT name, 'was created on ', create_date, CHAR(13), name, 'is currently ', state_desc   
FROM sys.databases;  
GO  
```
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```
name                                      create_date               name                                  state_desc  
--------------------------------------------------------------------------------------------------------------------  
master                    was created on  2003-04-08 09:13:36.390   master                  is currently  ONLINE 
tempdb                    was created on  2014-01-10 17:24:24.023   tempdb                  is currently  ONLINE   
AdventureWorksPDW2012     was created on  2014-05-07 09:05:07.083   AdventureWorksPDW2012   is currently  ONLINE 
```

### E. Using CHAR to return single-byte characters  
This example uses the integer and hex values in the valid range for ASCII. The CHAR function is able to output the single-byte Japanese character.
  
```sql
SELECT CHAR(188) AS single_byte_representing_complete_character, 
  CHAR(0xBC) AS single_byte_representing_complete_character;  
GO  
```
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```
single_byte_representing_complete_character single_byte_representing_complete_character
------------------------------------------- -------------------------------------------
ｼ                                           ｼ                                         
```

### F. Using CHAR to return multibyte characters
This example uses integer and hex values in the valid range for Extended ASCII.
However, the `CHAR` function returns `NULL` because the parameter represents only the first byte of a multibyte character.
A CHAR(2) double-byte character cannot be partially represented nor divided without some conversion operation.
The individual bytes of a double-byte character don't generally represent valid CHAR(1) values.
  
```sql
SELECT CHAR(129) AS first_byte_of_double_byte_character, 
  CHAR(0x81) AS first_byte_of_double_byte_character;  
GO  
```
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```
first_byte_of_double_byte_character first_byte_of_double_byte_character
----------------------------------- -----------------------------------
NULL                                NULL                                         
```
  
### G. Using CONVERT instead of CHAR to return multibyte characters
This example accepts the binary value as an encoded multibyte character consistent with the default codepage of the current database,
subject to validation.
Character conversion is more broadly supported and may be an alternative to working with encoding at a lower level.

```sql
CREATE DATABASE [multibyte-char-context]
  COLLATE Japanese_CI_AI
GO
USE [multibyte-char-context]
GO
SELECT NCHAR(0x266A) AS [eighth-note]
  , CONVERT(CHAR(2), 0x81F4) AS [context-dependent-convert]
  , CAST(0x81F4 AS CHAR(2)) AS [context-dependent-cast]
```

[!INCLUDE[ssResult](../../includes/ssresult-md.md)]

```
eighth-note context-dependent-convert context-dependent-cast
----------- ------------------------- ----------------------
♪           ♪                         ♪
```

### H. Using NCHAR instead of CHAR to look up UTF-8 characters
This example highlights the distinction the Unicode standard makes between a character's _code point_ and the _code unit sequence_ under a given _encoding form_.
The binary code assigned to a character in a classic character set is its only numeric identifier.
In contrast, the UTF-8 byte sequence associated with a character is an algorithmic encoding of its assigned numeric identifier: the code point.
UTF-8 **char** and UTF-16 **nchar** are different _encoding forms_ using 8-bit and 16-bit _code units_, of the same character set: the Unicode Character Database.

```sql
; WITH uni(c) AS (
    -- BMP character
    SELECT NCHAR(9835)
    UNION ALL
    -- non-BMP supplementary character or, under downlevel collation, NULL
    SELECT NCHAR(127925)
  ),
  enc(u16c, u8c) AS (
    SELECT c, CONVERT(VARCHAR(4), c COLLATE Latin1_General_100_CI_AI_SC_UTF8)
    FROM uni
  )
  SELECT u16c AS [Music note]
    , u8c AS [Music note (UTF-8)]
    , UNICODE(u16c) AS [Code Point]
    , CONVERT(VARBINARY(4), u16c) AS [UTF-16LE bytes]
    , CONVERT(VARBINARY(4), u8c)  AS [UTF-8 bytes]
  FROM enc
```

[!INCLUDE[ssResult](../../includes/ssresult-md.md)] Generated under a `_SC` collation with supplementary character support.

```
Music note Music note (UTF-8) Code Point  UTF-16LE bytes UTF-8 bytes
---------- ------------------ ----------- -------------- -----------
♫          ♫                  9835        0x6B26         0xE299AB
🎵         🎵                 127925      0x3CD8B5DF     0xF09F8EB5
```

## See also
 [ASCII &#40;Transact-SQL&#41;](../../t-sql/functions/ascii-transact-sql.md)  
 [NCHAR &#40;Transact-SQL&#41;](../../t-sql/functions/nchar-transact-sql.md)  
 [UNICODE &#40;Transact-SQL&#41;](../../t-sql/functions/unicode-transact-sql.md)  
 [+ &#40;String Concatenation&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/string-concatenation-transact-sql.md)  
 [String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)
  
  

