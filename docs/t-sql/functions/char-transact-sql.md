---
title: "CHAR (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "10/19/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "char_TSQL"
  - "char"
dev_langs: 
  - "TSQL"
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
ms.assetid: 955afe94-539c-465d-af22-16ec45da432a
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# CHAR (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

Returns the single-byte character with the specified integer code, as defined by the character set and encoding of the default collation of the current database.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```
CHAR ( integer_expression )  
```  
  
## Arguments  
*integer_expression*  
An integer from 0 through 255. `CHAR` returns a `NULL` value for integer expressions outside this range, or when no one character matches exactly the integer by both codepoint value and encoded value. Many common character sets share ASCII as a sub-set and will return the same character for integer values in the range 0 through 127.

> [!NOTE]
> Some character sets, such as [Unicode](https://en.wikipedia.org/wiki/Unicode#Mapping_and_encodings) and [Shift Japanese Industrial Standards](https://www.wikipedia.org/wiki/Shift_JIS), have multibyte encodings than can represent some characters in a single-byte, but require as many as four for others. For more information on character sets, refer to [Single-Byte and Multibyte Character Sets](/cpp/c-runtime-library/single-byte-and-multibyte-character-sets). 
  
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
DECLARE @position int, @string char(8);  
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

### F. CHAR inability to return multibyte characters
This example uses the an integer and hex values in the valid range for ASCII. However, the CHAR function returns NULL because the parameter matches the first byte of a number of multibyte characters in the collation, but no single complete character.
  
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
  
### G. Using CONVERT instead of CHAR to return legacy multibyte characters
This example relies on the default codepage of the current database to map a two-byte legacy codepoint to a single character.

```sql
CREATE DATABASE [multibyte-char-context]
  COLLATE Japanese_CI_AI
GO
USE [multibyte-char-context]
GO
SELECT NCHAR(0x266a) AS [eighth-note]
  , CONVERT(CHAR(2), 0x81f4) AS [context-dependent-convert]
  , CAST(0x81f4 AS CHAR(2)) AS [context-dependent-cast]
```

[!INCLUDE[ssResult](../../includes/ssresult-md.md)]

```
eighth-note context-dependent-convert context-dependent-cast
----------- ------------------------- ----------------------
♪           ♪                         ♪
```

### H. Using NCHAR instead of CHAR to return UTF-8 characters
This example highlights the distinction between Unicode codepoints and Unicode encodings.
Unlike classic character sets, UTF-8 bytes representing a given character are not synonymous with that character's codepoint.
The character set associated with UTF-8 is Unicode, thus UTF-8 codepoints have the same identity as other Unicode encodings such as that used by NCHAR and NVARCHAR, differing only in encoded binary representation.

```sql
SELECT NCHAR(0x266a) AS [beamed-eighth-note]
  , CONVERT(VARCHAR(4), NCHAR(0x266b) COLLATE Latin1_General_100_CI_AI_UTF8) AS [utf-8 beamed-eight-notes]
```

[!INCLUDE[ssResult](../../includes/ssresult-md.md)]

```
beamed-eighth-note utf-8 beamed-eight-notes
------------------ ------------------------
♫                  ♫
```

## See also
 [ASCII &#40;Transact-SQL&#41;](../../t-sql/functions/ascii-transact-sql.md)  
 [NCHAR &#40;Transact-SQL&#41;](../../t-sql/functions/nchar-transact-sql.md)  
 [UNICODE &#40;Transact-SQL&#41;](../../t-sql/functions/unicode-transact-sql.md)  
 [+ &#40;String Concatenation&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/string-concatenation-transact-sql.md)  
 [String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)
  
  

